' Copyright 2018 Google LLC
'
' Licensed under the Apache License, Version 2.0 (the "License")
' you may not use this file except in compliance with the License.
' You may obtain a copy of the License at
'
'     http:'www.apache.org/licenses/LICENSE-2.0
'
' Unless required by applicable law or agreed to in writing, software
' distributed under the License is distributed on an "AS IS" BASIS,
' WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
' See the License for the specific language governing permissions and
' limitations under the License.

Imports Google.Api.Ads.AdWords.Lib
Imports Google.Api.Ads.AdWords.v201809

Namespace Google.Api.Ads.AdWords.Examples.VB.v201809
    ''' <summary>
    ''' This code example adds a universal app campaign. To get campaigns, run
    ''' GetCampaigns.vb. To upload image assets for this campaign, use
    ''' UploadImage.vb.
    ''' </summary>
    Public Class AddUniversalAppCampaign
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New AddUniversalAppCampaign
            Console.WriteLine(codeExample.Description)
            Try
                codeExample.Run(New AdWordsUser())
            Catch e As Exception
                Console.WriteLine("An exception occurred while running this code example. {0}",
                                  ExampleUtilities.FormatException(e))
            End Try
        End Sub

        ''' <summary>
        ''' Returns a description about the code example.
        ''' </summary>
        Public Overrides ReadOnly Property Description() As String
            Get
                Return "This code example adds a universal app campaign. To get campaigns, run" &
                       " GetCampaigns.vb. To upload image assets for this campaign, use " &
                       "UploadImage.vb."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        Public Sub Run(ByVal user As AdWordsUser)
            ' [START prepareUAC] MOE:strip_line
            Using campaignService As CampaignService = CType(
                user.GetService(
                    AdWordsService.v201809.CampaignService),
                CampaignService)

                ' Create the campaign.
                Dim campaign As New Campaign
                campaign.name = "Interplanetary Cruise App #" + ExampleUtilities.GetRandomString

                ' Recommendation: Set the campaign to PAUSED when creating it to prevent
                ' the ads from immediately serving. Set to ENABLED once you've added
                ' targeting and the ads are ready to serve.
                campaign.status = CampaignStatus.PAUSED

                ' Set the advertising channel and subchannel types for universal app campaigns.
                campaign.advertisingChannelType = AdvertisingChannelType.MULTI_CHANNEL
                campaign.advertisingChannelSubType =
                    AdvertisingChannelSubType.UNIVERSAL_APP_CAMPAIGN

                ' Set the campaign's bidding strategy. Universal app campaigns
                ' only support TARGET_CPA bidding strategy.
                Dim biddingConfig As New BiddingStrategyConfiguration()
                biddingConfig.biddingStrategyType = BiddingStrategyType.TARGET_CPA

                ' Set the target CPA to $1 / app install.
                Dim biddingScheme As New TargetCpaBiddingScheme()
                biddingScheme.targetCpa = New Money()
                biddingScheme.targetCpa.microAmount = 1000000

                biddingConfig.biddingScheme = biddingScheme
                campaign.biddingStrategyConfiguration = biddingConfig

                ' Set the campaign's budget.
                campaign.budget = New Budget()
                campaign.budget.budgetId = CreateBudget(user).budgetId

                ' Optional: Set the start date.
                campaign.startDate = DateTime.Now.AddDays(1).ToString("yyyyMMdd")

                ' Optional: Set the end date.
                campaign.endDate = DateTime.Now.AddYears(1).ToString("yyyyMMdd")
                ' [END prepareUAC] MOE:strip_line

                ' [START setUACAssets] MOE:strip_line
                ' Set the campaign's assets and ad text ideas. These values will be used to
                ' generate ads.
                Dim universalAppSetting As New UniversalAppCampaignSetting()
                universalAppSetting.appId = "com.labpixies.colordrips"
                universalAppSetting.appVendor = MobileApplicationVendor.VENDOR_GOOGLE_MARKET
                universalAppSetting.description1 = "A cool puzzle game"
                universalAppSetting.description2 = "Remove connected blocks"
                universalAppSetting.description3 = "3 difficulty levels"
                universalAppSetting.description4 = "4 colorful fun skins"

                ' Optional: You can set up to 20 image assets for your campaign.
                ' See UploadImage.cs for an example on how to upload images.
                '
                ' universalAppSetting.imageMediaIds = new long[] { INSERT_IMAGE_MEDIA_ID_HERE };
                ' [END setUACAssets] MOE:strip_line

                ' [START optimizeUAC] MOE:strip_line
                ' Optimize this campaign for getting new users for your app.
                universalAppSetting.universalAppBiddingStrategyGoalType =
                    UniversalAppBiddingStrategyGoalType.OPTIMIZE_FOR_INSTALL_CONVERSION_VOLUME

                ' Optional: If you select the OPTIMIZE_FOR_IN_APP_CONVERSION_VOLUME goal
                ' type, then also specify your in-app conversion types so AdWords can
                ' focus your campaign on people who are most likely to complete the
                ' corresponding in-app actions.
                ' Conversion type IDs can be retrieved using ConversionTrackerService.get.
                '
                ' campaign.selectiveOptimization = new SelectiveOptimization();
                ' campaign.selectiveOptimization.conversionTypeIds =
                '    new long[] {
                '        INSERT_CONVERSION_TYPE_ID_1_HERE,
                '        INSERT_CONVERSION_TYPE_ID_2_HERE };

                ' Optional: Set the campaign settings for Advanced location options.
                Dim geoSetting As New GeoTargetTypeSetting()
                geoSetting.positiveGeoTargetType =
                    GeoTargetTypeSettingPositiveGeoTargetType.LOCATION_OF_PRESENCE
                geoSetting.negativeGeoTargetType =
                    GeoTargetTypeSettingNegativeGeoTargetType.DONT_CARE

                campaign.settings = New Setting() {universalAppSetting, geoSetting}
                ' [END optimizeUAC] MOE:strip_line

                ' [START createUAC] MOE:strip_line
                ' Create the operation.
                Dim operation As New CampaignOperation()
                operation.operator = [Operator].ADD
                operation.operand = campaign

                Try
                    ' Add the campaign.
                    Dim retVal As CampaignReturnValue = campaignService.mutate(
                        New CampaignOperation() {operation})

                    ' Display the results.
                    If Not (retVal Is Nothing) AndAlso Not (retVal.value Is Nothing) Then
                        For Each newCampaign As Campaign In retVal.value
                            Console.WriteLine(
                                "Universal app campaign with name = '{0}' and id = '{1}' " +
                                "was added.", newCampaign.name, newCampaign.id)

                            ' Optional: Set the campaign's location and language targeting. No other
                            ' targeting criteria can be used for universal app campaigns.
                            SetCampaignTargetingCriteria(user, newCampaign)
                        Next
                    Else
                        Console.WriteLine("No universal app campaigns were added.")
                    End If
                Catch e As Exception
                    Throw _
                        New System.ApplicationException("Failed to add universal app campaigns.", e)
                End Try
            End Using
            ' [END createUAC] MOE:strip_line
        End Sub

        ''' <summary>
        ''' Creates the budget for the campaign.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <returns>The budget.</returns>
        Private Shared Function CreateBudget(ByVal user As AdWordsUser) As Budget
            ' [START createBudget] MOE:strip_line
            Using budgetService As BudgetService = CType(
                user.GetService(
                    AdWordsService.v201809.BudgetService),
                BudgetService)

                ' Create the campaign budget.
                Dim budget As New Budget()
                budget.name = "Interplanetary Cruise App Budget #" &
                              ExampleUtilities.GetRandomString()
                budget.deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD
                budget.amount = New Money()
                budget.amount.microAmount = 5000000

                ' Universal app campaigns don't support shared budgets.
                budget.isExplicitlyShared = False

                Dim budgetOperation As New BudgetOperation()
                budgetOperation.operator = [Operator].ADD
                budgetOperation.operand = budget

                Dim budgetRetval As BudgetReturnValue = budgetService.mutate(
                    New BudgetOperation() {budgetOperation})
                ' [END createBudget] MOE:strip_line
                Dim newBudget As Budget = budgetRetval.value(0)

                Console.WriteLine("Budget with ID = '{0}' and name = '{1}' was created.",
                                  newBudget.budgetId, newBudget.name)
                Return newBudget
            End Using
        End Function

        ''' <summary>
        ''' Sets the campaign's targeting criteria.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="campaign">The campaign for which targeting criteria is
        ''' created.</param>
        Private Shared Sub SetCampaignTargetingCriteria(ByVal user As AdWordsUser,
                                                        ByVal campaign As Campaign)
            ' [START setCampaignTargetingCriteria] MOE:strip_line
            Using campaignCriterionService As CampaignCriterionService = CType(
                user.GetService(
                    AdWordsService.v201809.CampaignCriterionService),
                CampaignCriterionService)

                ' Create locations. The IDs can be found in the documentation or
                ' retrieved with the LocationCriterionService.
                Dim california As New Location()
                california.id = 21137L

                Dim mexico As New Location()
                mexico.id = 2484L

                ' Create languages. The IDs can be found in the documentation or
                ' retrieved with the ConstantDataService.
                Dim english As New Language()
                english.id = 1000L

                Dim spanish As New Language()
                spanish.id = 1003L

                Dim criteria As Criterion() = {california, mexico, english, spanish}

                ' Create operations to add each of the criteria above.
                Dim operations As New List(Of CampaignCriterionOperation)()
                For Each criterion As Criterion In criteria
                    Dim campaignCriterion As New CampaignCriterion
                    campaignCriterion.campaignId = campaign.id
                    campaignCriterion.criterion = criterion

                    Dim operation As New CampaignCriterionOperation()
                    operation.operand = campaignCriterion
                    operation.operator = [Operator].ADD

                    operations.Add(operation)
                Next

                ' Set the campaign targets.
                Dim retVal As CampaignCriterionReturnValue = campaignCriterionService.mutate(
                    operations.ToArray())

                If Not (retVal Is Nothing) AndAlso Not (retVal.value Is Nothing) Then
                    ' Display the added campaign targets.
                    For Each criterion As CampaignCriterion In retVal.value
                        Console.WriteLine("Campaign criteria of type '{0}' and id '{1}' was added.",
                                          criterion.criterion.CriterionType, criterion.criterion.id)
                    Next
                End If
            End Using
            ' [END setCampaignTargetingCriteria] MOE:strip_line
        End Sub
    End Class
End Namespace

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
    ''' This code example adds a Dynamic Search Ads campaign. To get campaigns, run GetCampaigns.vb.
    ''' </summary>
    Public Class AddDynamicSearchAdsCampaign
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New AddDynamicSearchAdsCampaign
            Console.WriteLine(codeExample.Description)
            Try
                codeExample.Run(New AdWordsUser)
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
                Return "This code example adds a Dynamic Search Ads campaign. To get campaigns, " +
                       "run GetCampaigns.vb."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        Public Sub Run(ByVal user As AdWordsUser)
            Try
                Dim budget As Budget = CreateBudget(user)
                Dim campaign As Campaign = CreateCampaign(user, budget)
                Dim adGroup As AdGroup = CreateAdGroup(user, campaign.id)
                Dim expandedDSA As ExpandedDynamicSearchAd = CreateExpandedDSA(user, adGroup.id)
                Console.WriteLine("Dynamic Search Ads campaign setup is complete.")
            Catch e As Exception
                Throw _
                    New System.ApplicationException("Failed to setup Dynamic Search Ads campaign.",
                                                    e)
            End Try
        End Sub

        ''' <summary>
        ''' Creates the budget.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <returns>The newly created budget.</returns>
        Private Function CreateBudget(ByVal user As AdWordsUser) As Budget
            ' [START createBudget] MOE:strip_line
            ' Get the BudgetService.
            Using budgetService As BudgetService = CType(
                user.GetService(
                    AdWordsService.v201809.BudgetService),
                BudgetService)

                ' Create the campaign budget.
                Dim budget As New Budget()
                budget.name = "Interplanetary Cruise Budget #" + ExampleUtilities.GetRandomString()
                budget.deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD
                budget.amount = New Money()
                budget.amount.microAmount = 500000

                Dim budgetOperation As New BudgetOperation()
                budgetOperation.operator = [Operator].ADD
                budgetOperation.operand = budget

                Try
                    Dim budgetRetval As BudgetReturnValue = budgetService.mutate(
                        New BudgetOperation() {budgetOperation})
                    Dim newBudget As Budget = budgetRetval.value(0)
                    Console.WriteLine("Budget with ID = '{0}' and name = '{1}' was created.",
                                      newBudget.budgetId, newBudget.name)
                    Return newBudget
                Catch e As Exception
                    Throw New System.ApplicationException("Failed to add budget.", e)
                End Try
                ' [END createBudget] MOE:strip_line
            End Using
        End Function

        ''' <summary>
        ''' Creates the campaign.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="budget">The campaign budget.</param>
        ''' <returns>The newly created campaign.</returns>
        Private Function CreateCampaign(ByVal user As AdWordsUser, ByVal budget As Budget) _
            As Campaign
            ' [START createCampaign] MOE:strip_line
            Using campaignService As CampaignService = CType(
                user.GetService(
                    AdWordsService.v201809.CampaignService),
                CampaignService)

                ' Create a Dynamic Search Ads campaign.
                Dim campaign As New Campaign()
                campaign.name = "Interplanetary Cruise #" + ExampleUtilities.GetRandomString()
                campaign.advertisingChannelType = AdvertisingChannelType.SEARCH

                ' Recommendation: Set the campaign to PAUSED when creating it to prevent
                ' the ads from immediately serving. Set to ENABLED once you've added
                ' targeting and the ads are ready to serve.
                campaign.status = CampaignStatus.PAUSED

                Dim biddingConfig As New BiddingStrategyConfiguration()
                biddingConfig.biddingStrategyType = BiddingStrategyType.MANUAL_CPC
                campaign.biddingStrategyConfiguration = biddingConfig

                campaign.budget = New Budget()
                campaign.budget.budgetId = budget.budgetId

                ' Required: Set the campaign's Dynamic Search Ads settings.
                Dim dynamicSearchAdsSetting As New DynamicSearchAdsSetting()
                ' Required: Set the domain name And language.
                dynamicSearchAdsSetting.domainName = "example.com"
                dynamicSearchAdsSetting.languageCode = "en"

                ' Set the campaign settings.
                campaign.settings = New Setting() {dynamicSearchAdsSetting}

                ' Optional: Set the start date.
                campaign.startDate = DateTime.Now.AddDays(1).ToString("yyyyMMdd")

                ' Optional: Set the end date.
                campaign.endDate = DateTime.Now.AddYears(1).ToString("yyyyMMdd")

                ' Create the operation.
                Dim operation As New CampaignOperation()
                operation.operator = [Operator].ADD
                operation.operand = campaign

                Try
                    ' Add the campaign.
                    Dim retval As CampaignReturnValue = campaignService.mutate(
                        New CampaignOperation() {operation})

                    ' Display the results.
                    Dim newCampaign As Campaign = retval.value(0)
                    Console.WriteLine("Campaign with id = '{0}' and name = '{1}' was added.",
                                      newCampaign.id, newCampaign.name)
                    Return newCampaign
                Catch e As Exception
                    Throw New System.ApplicationException("Failed to add campaigns.", e)
                End Try
                ' [END createCampaign] MOE:strip_line
            End Using
        End Function

        ''' <summary>
        ''' Creates an ad group.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="campaignId">The campaign ID.</param>
        ''' <returns>the newly created ad group.</returns>
        Private Function CreateAdGroup(ByVal user As AdWordsUser, ByVal campaignId As Long) _
            As AdGroup
            ' [START createAdGroup] MOE:strip_line
            Using adGroupService As AdGroupService = CType(
                user.GetService(
                    AdWordsService.v201809.AdGroupService),
                AdGroupService)

                ' Create the ad group.
                Dim adGroup As New AdGroup()

                ' Required: set the ad group's type to Dynamic Search Ads.
                adGroup.adGroupType = AdGroupType.SEARCH_DYNAMIC_ADS

                adGroup.name = String.Format("Earth to Mars Cruises #{0}",
                                             ExampleUtilities.GetRandomString())
                adGroup.campaignId = campaignId
                adGroup.status = AdGroupStatus.PAUSED

                ' Set the ad group bids.
                Dim biddingConfig As New BiddingStrategyConfiguration()

                Dim cpcBid As New CpcBid()
                cpcBid.bid = New Money()
                cpcBid.bid.microAmount = 3000000

                biddingConfig.bids = New Bids() {cpcBid}

                adGroup.biddingStrategyConfiguration = biddingConfig

                ' Create the operation.
                Dim Operation As New AdGroupOperation()
                Operation.operator = [Operator].ADD
                Operation.operand = adGroup

                Try
                    ' Create the ad group.
                    Dim retVal As AdGroupReturnValue = adGroupService.mutate(
                        New AdGroupOperation() {Operation})

                    ' Display the results.
                    Dim newAdGroup As AdGroup = retVal.value(0)
                    Console.WriteLine("Ad group with id = '{0}' and name = '{1}' was created.",
                                      newAdGroup.id, newAdGroup.name)
                    Return newAdGroup
                Catch e As Exception
                    Throw New System.ApplicationException("Failed to create ad group.", e)
                End Try
                ' [END createAdGroup] MOE:strip_line
            End Using
        End Function

        ''' <summary>
        ''' Creates the expanded Dynamic Search Ad.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="adGroupId">ID of the ad group in which ad is created.</param>
        ''' <returns>The newly created ad.</returns>
        Private Function CreateExpandedDSA(ByVal user As AdWordsUser, ByVal adGroupId As Long) _
            As ExpandedDynamicSearchAd
            ' [START createExpandedDSA] MOE:strip_line
            Using adGroupAdService As AdGroupAdService = CType(
                user.GetService(
                    AdWordsService.v201809.AdGroupAdService),
                AdGroupAdService)

                ' Create an Expanded Dynamic Search Ad. This ad will have its headline, display URL
                ' and final URL auto-generated at serving time according to domain name specific
                ' information provided by DynamicSearchAdsSetting at the campaign level.
                Dim expandedDSA As New ExpandedDynamicSearchAd()
                ' Set the ad description.
                expandedDSA.description = "Buy your tickets now!"
                expandedDSA.description2 = "Discount ends soon"

                ' Create the ad group ad.
                Dim adGroupAd As New AdGroupAd()
                adGroupAd.adGroupId = adGroupId
                adGroupAd.ad = expandedDSA

                ' Optional: Set the status.
                adGroupAd.status = AdGroupAdStatus.PAUSED

                ' Create the operation.
                Dim operation As New AdGroupAdOperation()
                operation.operator = [Operator].ADD
                operation.operand = adGroupAd

                Try
                    ' Create the ads.
                    Dim retval As AdGroupAdReturnValue = adGroupAdService.mutate(
                        New AdGroupAdOperation() {operation})

                    ' Display the results.
                    Dim newAdGroupAd As AdGroupAd = retval.value(0)
                    Dim newAd As ExpandedDynamicSearchAd = CType(newAdGroupAd.ad,
                                                                 ExpandedDynamicSearchAd)
                    Console.WriteLine(
                        "Expanded Dynamic Search Ad with ID '{0}' and description '{1}' " +
                        "was added.", newAd.id, newAd.description)
                    Return newAd
                Catch e As Exception
                    Throw _
                        New System.ApplicationException(
                            "Failed to create Expanded Dynamic Search Ad.", e)
                End Try
                ' [END createExpandedDSA] MOE:strip_line
            End Using
        End Function

        ''' <summary>
        ''' Adds a web page criteria to target Dynamic Search Ads.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="adGroupId">The ad group ID.</param>
        ''' <returns>The newly created web page criterion.</returns>
        Private Function AddWebPageCriteria(ByVal user As AdWordsUser, ByVal adGroupId As Long) _
            As BiddableAdGroupCriterion
            ' [START createWebPageCriteria] MOE:strip_line
            Using adGroupCriterionService As AdGroupCriterionService = CType(
                user.GetService(
                    AdWordsService.v201809.AdGroupCriterionService),
                AdGroupCriterionService)

                ' Create a webpage criterion for special offers for mars cruise.
                Dim param As New WebpageParameter()
                param.criterionName = "Special offers for mars"

                Dim urlCondition As New WebpageCondition()
                urlCondition.operand = WebpageConditionOperand.URL
                urlCondition.argument = "/marscruise/special"

                Dim titleCondition As New WebpageCondition()
                titleCondition.operand = WebpageConditionOperand.PAGE_TITLE
                titleCondition.argument = "Special Offer"

                param.conditions = New WebpageCondition() {urlCondition, titleCondition}

                Dim Webpage As New Webpage()
                Webpage.parameter = param

                ' Create biddable ad group criterion.
                Dim biddableAdGroupCriterion As New BiddableAdGroupCriterion()
                biddableAdGroupCriterion.adGroupId = adGroupId
                biddableAdGroupCriterion.criterion = Webpage
                biddableAdGroupCriterion.userStatus = UserStatus.PAUSED

                ' Optional: Set a custom bid.
                Dim biddingStrategyConfiguration As New BiddingStrategyConfiguration()
                Dim bid As New CpcBid()
                bid.bid = New Money()
                bid.bid.microAmount = 10000000L

                biddingStrategyConfiguration.bids = New Bids() {bid}
                biddableAdGroupCriterion.biddingStrategyConfiguration = biddingStrategyConfiguration

                ' Create operations.
                Dim operation As New AdGroupCriterionOperation()
                operation.operator = [Operator].ADD
                operation.operand = biddableAdGroupCriterion

                Try
                    Dim result As AdGroupCriterionReturnValue = adGroupCriterionService.mutate(
                        New AdGroupCriterionOperation() {operation})

                    Dim newCriterion As BiddableAdGroupCriterion = CType(result.value(0),
                                                                         BiddableAdGroupCriterion)
                    Console.WriteLine("Webpage criterion with '{0}' was added to ad group ID " &
                                      "'{1}'.",
                                      newCriterion.adGroupId, newCriterion.criterion.id)
                    Return newCriterion
                Catch e As Exception
                    Throw New System.ApplicationException("Failed to create webpage criterion.", e)
                End Try
                ' [END createWebPageCriteria] MOE:strip_line
            End Using
        End Function
    End Class
End Namespace

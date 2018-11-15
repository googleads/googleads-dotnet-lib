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
Imports Google.Api.Ads.AdWords.Util.Shopping.v201809
Imports Google.Api.Ads.AdWords.v201809

Namespace Google.Api.Ads.AdWords.Examples.VB.v201809
    ''' <summary>
    ''' This code example adds a Shopping campaign.
    ''' </summary>
    Public Class AddShoppingCampaign
        Inherits ExampleBase

        ''' <summary>
        ''' Returns a description about the code example.
        ''' </summary>
        Public Overrides ReadOnly Property Description() As String
            Get
                Return "This code example adds a Shopping campaign."
            End Get
        End Property

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New AddShoppingCampaign
            Console.WriteLine(codeExample.Description)
            Try
                Dim budgetId As Long = Long.Parse("INSERT_BUDGET_ID_HERE")
                Dim merchantId As Long = Long.Parse("INSERT_MERCHANT_ID_HERE")
                Dim createDefaultPartition As Boolean = False
                codeExample.Run(New AdWordsUser, budgetId, merchantId, createDefaultPartition)
            Catch e As Exception
                Console.WriteLine("An exception occurred while running this code example. {0}",
                                  ExampleUtilities.FormatException(e))
            End Try
        End Sub

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="budgetId">The budget id.</param>
        ''' <param name="merchantId">The Merchant Center account id.</param>
        ''' <param name="createDefaultPartition">If set to true, a default
        ''' partition will be created. If running the AddProductPartition.cs
        ''' example right after this example, make sure this stays set to
        ''' false.</param>
        Public Sub Run(ByVal user As AdWordsUser, ByVal budgetId As Long, ByVal merchantId As Long,
                       ByVal createDefaultPartition As Boolean)
            Try
                Dim campaign As Campaign = CreateCampaign(user, budgetId, merchantId)
                Console.WriteLine("Campaign with name '{0}' and ID '{1}' was added.", campaign.name,
                                  campaign.id)

                Dim adGroup As AdGroup = CreateAdGroup(user, campaign)
                Console.WriteLine("Ad group with name '{0}' and ID '{1}' was added.", adGroup.name,
                                  adGroup.id)

                Dim adGroupAd As AdGroupAd = CreateProductAd(user, adGroup)
                Console.WriteLine("Product ad with ID {0}' was added.", adGroupAd.ad.id)

                If (createDefaultPartition) Then
                    CreateDefaultPartitionTree(user, adGroup.id)
                End If

            Catch e As Exception
                Throw New System.ApplicationException("Failed to create shopping campaign.", e)
            End Try
        End Sub

        ''' <summary>
        ''' Creates the default partition.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="adGroupId">The ad group ID.</param>
        Private Sub CreateDefaultPartitionTree(ByVal user As AdWordsUser, ByVal adGroupId As Long)
            ' Get the AdGroupCriterionService.
            Dim adGroupCriterionService As AdGroupCriterionService = CType(
                user.GetService(
                    AdWordsService.v201809.AdGroupCriterionService),
                AdGroupCriterionService)

            ' Build a New ProductPartitionTree using an empty set of criteria.
            Dim partitionTree As ProductPartitionTree =
                    ProductPartitionTree.CreateAdGroupTree(adGroupId,
                                                           New List(Of AdGroupCriterion)())
            partitionTree.Root.AsBiddableUnit().CpcBid = 1000000

            Try
                ' Make the mutate request, using the operations returned by the
                ' ProductPartitionTree.
                Dim mutateOperations As AdGroupCriterionOperation() =
                        partitionTree.GetMutateOperations()

                If mutateOperations.Length = 0 Then
                    Console.WriteLine(
                        "Skipping the mutate call because the original tree and the current " +
                        "tree are logically identical.")
                Else
                    adGroupCriterionService.mutate(mutateOperations)
                End If

                ' The request was successful, so create a New ProductPartitionTree based on the
                ' updated state of the ad group.
                partitionTree = ProductPartitionTree.DownloadAdGroupTree(user, adGroupId)

                Console.WriteLine("Final tree: {0}", partitionTree)
            Catch e As Exception
                Throw _
                    New System.ApplicationException("Failed to set shopping product partition.", e)
            End Try
        End Sub

        ''' <summary>
        ''' Creates the Product Ad.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="adGroup">The ad group.</param>
        ''' <returns>The Product Ad.</returns>
        Private Function CreateProductAd(ByVal user As AdWordsUser,
                                         ByVal adGroup As AdGroup) As AdGroupAd
            Using adGroupAdService As AdGroupAdService = CType(
                user.GetService(
                    AdWordsService.v201809.AdGroupAdService),
                AdGroupAdService)

                ' Create product ad.
                Dim productAd As New ProductAd()

                ' Create ad group ad.
                Dim adGroupAd As New AdGroupAd()
                adGroupAd.adGroupId = adGroup.id
                adGroupAd.ad = productAd

                ' Create operation.
                Dim operation As New AdGroupAdOperation()
                operation.operand = adGroupAd
                operation.operator = [Operator].ADD

                ' Make the mutate request.
                Dim retval As AdGroupAdReturnValue = adGroupAdService.mutate(
                    New AdGroupAdOperation() {operation})

                Return retval.value(0)
            End Using
        End Function

        ''' <summary>
        ''' Creates the ad group in a Shopping campaign.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="campaign">The Shopping campaign.</param>
        ''' <returns>The ad group.</returns>
        Private Function CreateAdGroup(ByVal user As AdWordsUser,
                                       ByVal campaign As Campaign) As AdGroup
            Using adGroupService As AdGroupService = CType(
                user.GetService(
                    AdWordsService.v201809.AdGroupService),
                AdGroupService)
                ' Create ad group.
                Dim adGroup As New AdGroup()
                adGroup.campaignId = campaign.id
                adGroup.name = "Ad Group #" & ExampleUtilities.GetRandomString()

                ' Create operation.
                Dim operation As New AdGroupOperation()
                operation.operand = adGroup
                operation.operator = [Operator].ADD

                ' Make the mutate request.
                Dim retval As AdGroupReturnValue = adGroupService.mutate(
                    New AdGroupOperation() {operation})
                Return retval.value(0)
            End Using
        End Function

        '  [START createCampaign] MOE:strip_line
        ''' <summary>
        ''' Creates the shopping campaign.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="budgetId">The budget id.</param>
        ''' <param name="merchantId">The Merchant Center id.</param>
        ''' <returns>The Shopping campaign.</returns>
        Private Function CreateCampaign(ByVal user As AdWordsUser, ByVal budgetId As Long,
                                        ByVal merchantId As Long) As Campaign
            ' Get the required services.
            Dim campaignService As CampaignService = CType(
                user.GetService(
                    AdWordsService.v201809.CampaignService),
                CampaignService)

            ' Create campaign.
            Dim campaign As New Campaign()
            campaign.name = "Shopping campaign #" & ExampleUtilities.GetRandomString()

            ' The advertisingChannelType is what makes this a Shopping campaign.
            campaign.advertisingChannelType = AdvertisingChannelType.SHOPPING

            ' Recommendation: Set the campaign to PAUSED when creating it to prevent
            ' the ads from immediately serving. Set to ENABLED once you've added
            ' targeting and the ads are ready to serve.
            campaign.status = CampaignStatus.PAUSED

            ' Set shared budget (required).
            campaign.budget = New Budget()
            campaign.budget.budgetId = budgetId

            ' Set bidding strategy (required).
            Dim biddingStrategyConfiguration As New BiddingStrategyConfiguration()
            biddingStrategyConfiguration.biddingStrategyType = BiddingStrategyType.MANUAL_CPC

            campaign.biddingStrategyConfiguration = biddingStrategyConfiguration

            ' All Shopping campaigns need a ShoppingSetting.
            Dim shoppingSetting As New ShoppingSetting()
            shoppingSetting.salesCountry = "US"
            shoppingSetting.campaignPriority = 0
            shoppingSetting.merchantId = merchantId

            ' Enable Local Inventory Ads in your campaign.
            shoppingSetting.enableLocal = True
            campaign.settings = New Setting() {shoppingSetting}

            ' Create operation.
            Dim campaignOperation As New CampaignOperation()
            campaignOperation.operand = campaign
            campaignOperation.operator = [Operator].ADD

            ' Make the mutate request.
            Dim retval As CampaignReturnValue = campaignService.mutate(
                New CampaignOperation() {campaignOperation})

            Return retval.value(0)
        End Function
        '  [END createCampaign] MOE:strip_line
    End Class
End Namespace

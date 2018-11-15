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
Imports Google.Api.Ads.AdWords.Util.Shopping.v201806
Imports Google.Api.Ads.AdWords.v201806
Imports Google.Api.Ads.Common.Util

Namespace Google.Api.Ads.AdWords.Examples.VB.v201806
    ''' <summary>
    ''' This code example adds a Shopping campaign for Showcase ads.
    ''' </summary>
    Public Class AddShoppingCampaignForShowcaseAds
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New AddShoppingCampaignForShowcaseAds
            Console.WriteLine(codeExample.Description)
            Try
                Dim budgetId As Long = Long.Parse("INSERT_BUDGET_ID_HERE")
                Dim merchantId As Long = Long.Parse("INSERT_MERCHANT_ID_HERE")
                codeExample.Run(New AdWordsUser(), budgetId, merchantId)
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
                Return "This code example adds a Shopping campaign for Showcase ads."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="budgetId">The budget id.</param>
        ''' <param name="merchantId">The Merchant Center account ID.</param>
        Public Sub Run(ByVal user As AdWordsUser, ByVal budgetId As Long, ByVal merchantId As Long)
            Try
                Dim campaign As Campaign = CreateCampaign(user, budgetId, merchantId)
                Console.WriteLine("Campaign with name '{0}' and ID '{1}' was added.", campaign.name,
                                  campaign.id)

                Dim adGroup As AdGroup = CreateAdGroup(user, campaign)
                Console.WriteLine("Ad group with name '{0}' and ID '{1}' was added.", adGroup.name,
                                  adGroup.id)

                Dim adGroupAd As AdGroupAd = CreateShowcaseAd(user, adGroup)
                Console.WriteLine("Showcase ad with ID '{0}' was added.", adGroupAd.ad.id)

                Dim partitionTree As ProductPartitionTree = CreateProductPartition(user, adGroup.id)
                Console.WriteLine("Final tree: {0}", partitionTree)
            Catch e As Exception
                Throw New System.ApplicationException("Failed to create shopping campaign for " &
                                                      "showcase ads", e)
            End Try
        End Sub

        ''' <summary>
        ''' Creates the Shopping campaign.
        ''' </summary>
        ''' <param name="user">The AdWords user for which the campaign is created.</param>
        ''' <param name="budgetId">The budget ID.</param>
        ''' <param name="merchantId">The Merchant Center ID.</param>
        ''' <returns>The newly created Shopping campaign.</returns>
        Private Shared Function CreateCampaign(ByVal user As AdWordsUser, ByVal budgetId As Long,
                                               ByVal merchantId As Long) As Campaign
            Using campaignService As CampaignService = CType(
                user.GetService(
                    AdWordsService.v201806.CampaignService),
                CampaignService)

                ' Create the campaign.
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

                ' Note: Showcase Ads require that the campaign has a ManualCpc
                ' BiddingStrategyConfiguration.
                biddingStrategyConfiguration.biddingStrategyType = BiddingStrategyType.MANUAL_CPC

                campaign.biddingStrategyConfiguration = biddingStrategyConfiguration

                ' All Shopping campaigns need a ShoppingSetting.
                Dim shoppingSetting As New ShoppingSetting()
                shoppingSetting.salesCountry = "US"
                shoppingSetting.campaignPriority = 0
                shoppingSetting.merchantId = merchantId

                ' Set to "true" to enable Local Inventory Ads in your campaign.
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
            End Using
        End Function

        ''' <summary>
        ''' Creates the ad group in a Shopping campaign.
        ''' </summary>
        ''' <param name="user">The AdWords user for which the ad group is created.</param>
        ''' <param name="campaign">The Shopping campaign.</param>
        ''' <returns>The newly created ad group.</returns>
        Private Shared Function CreateAdGroup(ByVal user As AdWordsUser,
                                              ByVal campaign As Campaign) _
            As AdGroup
            Using adGroupService As AdGroupService = CType(
                user.GetService(
                    AdWordsService.v201806.AdGroupService),
                AdGroupService)

                ' Create ad group.
                Dim adGroup As New AdGroup()
                adGroup.campaignId = campaign.id
                adGroup.name = "Ad Group #" & ExampleUtilities.GetRandomString()

                ' Required: Set the ad group type to SHOPPING_SHOWCASE_ADS.
                adGroup.adGroupType = AdGroupType.SHOPPING_SHOWCASE_ADS

                ' Required: Set the ad group's bidding strategy configuration.
                Dim biddingConfiguration As New BiddingStrategyConfiguration()

                ' Optional: Set the bids.
                Dim cpcBid As New CpcBid()
                cpcBid.bid = New Money()
                cpcBid.bid.microAmount = 100000

                biddingConfiguration.bids = New Bids() {cpcBid}

                adGroup.biddingStrategyConfiguration = biddingConfiguration

                ' Create the operation.
                Dim operation As New AdGroupOperation()
                operation.operand = adGroup
                operation.operator = [Operator].ADD

                ' Make the mutate request.
                Dim retval As AdGroupReturnValue = adGroupService.mutate(
                    New AdGroupOperation() {operation})
                Return retval.value(0)
            End Using
        End Function

        ''' <summary>
        ''' Creates the Showcase ad.
        ''' </summary>
        ''' <param name="user">The AdWords user for which the ad is created.</param>
        ''' <param name="adGroup">The ad group in which the ad is created.</param>
        ''' <returns>The newly created Showcase ad.</returns>
        Private Shared Function CreateShowcaseAd(ByVal user As AdWordsUser,
                                                 ByVal adGroup As AdGroup) _
            As AdGroupAd
            Using adGroupAdService As AdGroupAdService = CType(
                user.GetService(
                    AdWordsService.v201806.AdGroupAdService),
                AdGroupAdService)

                ' Create the Showcase ad.
                Dim showcaseAd As New ShowcaseAd()

                ' Required: set the ad's name, final URLs and display URL.
                showcaseAd.name = "Showcase ad " & ExampleUtilities.GetShortRandomString()
                showcaseAd.finalUrls = New String() {"http://example.com/showcase"}
                showcaseAd.displayUrl = "example.com"

                ' Required: Set the ad's expanded image.
                Dim expandedImage As New Image()
                expandedImage.mediaId = UploadImage(user, "https://goo.gl/IfVlpF")
                showcaseAd.expandedImage = expandedImage

                ' Optional: Set the collapsed image.
                Dim collapsedImage As New Image()
                collapsedImage.mediaId = UploadImage(user, "https://goo.gl/NqTxAE")
                showcaseAd.collapsedImage = collapsedImage

                ' Create ad group ad.
                Dim adGroupAd As New AdGroupAd()
                adGroupAd.adGroupId = adGroup.id
                adGroupAd.ad = showcaseAd

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
        ''' Creates a product partition tree.
        ''' </summary>
        ''' <param name="user">The AdWords user for which the product partition is created.</param>
        ''' <param name="adGroupId">Ad group ID.</param>
        ''' <returns>The product partition.</returns>
        Private Shared Function CreateProductPartition(ByVal user As AdWordsUser,
                                                       ByVal adGroupId As Long) _
            As ProductPartitionTree

            Using adGroupCriterionService As AdGroupCriterionService = CType(
                user.GetService(
                    AdWordsService.v201806.AdGroupCriterionService),
                AdGroupCriterionService)

                ' Build a new ProductPartitionTree using the ad group's current set of criteria.
                Dim partitionTree As ProductPartitionTree =
                        ProductPartitionTree.DownloadAdGroupTree(user, adGroupId)

                Console.WriteLine("Original tree: {0}", partitionTree)

                ' Clear out any existing criteria.
                Dim rootNode As ProductPartitionNode = partitionTree.Root.RemoveAllChildren()

                ' Make the root node a subdivision.
                rootNode = rootNode.AsSubdivision()

                ' Add a unit node for condition = NEW to include it.
                rootNode.AddChild(
                    ProductDimensions.CreateCanonicalCondition(
                        ProductCanonicalConditionCondition.NEW))

                ' Add a unit node for condition = USED to include it.
                rootNode.AddChild(
                    ProductDimensions.CreateCanonicalCondition(
                        ProductCanonicalConditionCondition.USED))

                ' Exclude everything else.
                rootNode.AddChild(ProductDimensions.CreateCanonicalCondition()).AsExcludedUnit()

                ' Make the mutate request, using the operations returned by the 
                ' ProductPartitionTree.
                Dim mutateOperations As AdGroupCriterionOperation() =
                        partitionTree.GetMutateOperations()

                If mutateOperations.Length = 0 Then
                    Console.WriteLine(
                        "Skipping the mutate call because the original tree and the current " &
                        "tree are logically identical.")
                Else
                    adGroupCriterionService.mutate(mutateOperations)
                End If

                ' The request was successful, so create a new ProductPartitionTree based on the 
                ' updated state of the ad group.
                partitionTree = ProductPartitionTree.DownloadAdGroupTree(user, adGroupId)
                Return partitionTree
            End Using
        End Function

        ''' <summary>
        ''' Uploads an image.
        ''' </summary>
        ''' <param name="user">The AdWords user for which the image is uploaded.</param>
        ''' <param name="url">The image URL.</param>
        ''' <returns>The uploaded image.</returns>
        Private Shared Function UploadImage(ByVal user As AdWordsUser, ByVal url As String) As Long
            Using mediaService As MediaService = CType(
                user.GetService(
                    AdWordsService.v201806.MediaService),
                MediaService)

                ' Create the image.
                Dim image As New Image()
                image.data = MediaUtilities.GetAssetDataFromUrl(url, user.Config)
                image.type = MediaMediaType.IMAGE

                ' Upload the image.
                Dim result As Media() = mediaService.upload(New Media() {image})
                Return result(0).mediaId
            End Using
        End Function
    End Class
End Namespace

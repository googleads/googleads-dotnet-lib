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
Imports Google.Api.Ads.Common.Util

Namespace Google.Api.Ads.AdWords.Examples.VB.v201809
    ''' <summary>
    ''' This code example adds a Shopping dynamic remarketing campaign for the Display Network
    ''' via the following steps:
    ''' <list type="bullet">
    '''   <item>
    '''     <description>Creates a new Display Network campaign.</description>
    '''   </item>
    '''   <item>
    '''     <description>Links the campaign with Merchant Center.</description>
    '''   </item>
    '''   <item>
    '''     <description>Links the user list to the ad group.</description>
    '''   </item>
    '''   <item>
    '''     <description>Creates a responsive display ad to render the dynamic text.</description>
    '''   </item>
    ''' </list>
    ''' </summary>
    Public Class AddShoppingDynamicRemarketingCampaign
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New AddShoppingDynamicRemarketingCampaign
            Console.WriteLine(codeExample.Description)
            Try
                ' The ID of the merchant center account from which to source product feed data.
                Dim merchantId As Long = Long.Parse("INSERT_MERCHANT_CENTER_ID_HERE")

                ' The ID of a shared budget to associate with the campaign.
                Dim budgetId As Long = Long.Parse("INSERT_BUDGET_ID_HERE")

                ' The ID of a user list to target.
                Dim userListId As Long = Long.Parse("INSERT_USER_LIST_ID_HERE")
                codeExample.Run(New AdWordsUser(), merchantId, budgetId, userListId)
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
                Return "This code example adds a Shopping dynamic remarketing campaign for the " &
                       "Display Network via the following steps:\n" &
                       "*  Creates a new Display Network campaign.\n" &
                       "*  Links the campaign with Merchant Center.\n" &
                       "*  Links the user list to the ad group.\n" +
                       "*  Creates a responsive display ad to render the dynamic text."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="merchantId">The ID of the merchant center account from which to source
        ''' product feed data.</param>
        ''' <param name="budgetId">The ID of a shared budget to associate with the campaign.</param>
        ''' <param name="userListId">The ID of a user list to target.</param>
        Public Sub Run(ByVal user As AdWordsUser, ByVal merchantId As Long, ByVal budgetId As Long,
                       ByVal userListId As Long)
            Try
                Dim campaign As Campaign = CreateCampaign(user, merchantId, budgetId)
                Console.WriteLine("Campaign with name '{0}' and ID {1} was added.",
                                  campaign.name, campaign.id)

                Dim adGroup As AdGroup = CreateAdGroup(user, campaign)
                Console.WriteLine("Ad group with name '{0}' and ID {1} was added.",
                                  adGroup.name, adGroup.id)

                Dim adGroupAd As AdGroupAd = CreateAd(user, adGroup)
                Console.WriteLine("Responsive display ad with ID {0} was added.", adGroupAd.ad.id)

                AttachUserList(user, adGroup, userListId)
                Console.WriteLine("User list with ID {0} was attached to ad group with ID {1}.",
                                  userListId, adGroup.id)
            Catch e As Exception
                Throw _
                    New System.ApplicationException(
                        "Failed to create Shopping dynamic remarketing " +
                        "campaign for the Display Network.", e)
            End Try
        End Sub

        ''' <summary>
        ''' Creates a Shopping dynamic remarketing campaign object (not including ad group level and
        ''' below). This creates a Display campaign with the merchant center feed attached.
        ''' Merchant Center is used for the product information in combination with a user list
        ''' which contains hits with <code>ecomm_prodid</code> specified. See
        ''' <a href="https://developers.google.com/adwords-remarketing-tag/parameters#retail">
        ''' the guide</a> for more detail.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="merchantId">The ID of the Merchant Center account.</param>
        ''' <param name="budgetId">The ID of the budget to use for the campaign.</param>
        ''' <returns>The campaign that was created.</returns>
        Private Shared Function CreateCampaign(ByVal user As AdWordsUser, ByVal merchantId As Long,
                                               ByVal budgetId As Long) As Campaign
            Using campaignService As CampaignService = DirectCast(
                user.GetService(
                    AdWordsService.v201809.CampaignService),
                CampaignService)
                Dim campaign As New Campaign()
                campaign.name = "Shopping campaign #" + ExampleUtilities.GetRandomString()
                ' Dynamic remarketing campaigns are only available on the Google Display Network.
                campaign.advertisingChannelType = AdvertisingChannelType.DISPLAY
                campaign.status = CampaignStatus.PAUSED

                Dim budget As New Budget()
                budget.budgetId = budgetId
                campaign.budget = budget

                ' This example uses a Manual CPC bidding strategy, but you should select the
                ' strategy that best aligns with your sales goals. More details here:
                '   https://support.google.com/adwords/answer/2472725
                Dim biddingStrategyConfiguration As New BiddingStrategyConfiguration()
                biddingStrategyConfiguration.biddingStrategyType = BiddingStrategyType.MANUAL_CPC
                campaign.biddingStrategyConfiguration = biddingStrategyConfiguration

                Dim setting As New ShoppingSetting()
                ' Campaigns with numerically higher priorities take precedence over those with lower
                ' priorities.
                setting.campaignPriority = 0

                ' Set the Merchant Center account ID from which to source products.
                setting.merchantId = merchantId

                ' Display Network campaigns do not support partition by country. The only supported
                ' value is "ZZ". This signals that products from all countries are available in the
                ' campaign. The actual products which serve are based on the products tagged in the
                ' user list entry.
                setting.salesCountry = "ZZ"

                ' Optional: Enable local inventory ads (items for sale in physical stores.)
                setting.enableLocal = True

                campaign.settings = New Setting() {setting}

                Dim op As New CampaignOperation()
                op.operand = campaign
                op.operator = [Operator].ADD

                Dim result As CampaignReturnValue = campaignService.mutate(
                    New CampaignOperation() _
                                                                              {op})
                Return result.value(0)
            End Using
        End Function

        ''' <summary>
        ''' Creates an ad group in the specified campaign.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="campaign">The campaign to which the ad group should be attached.</param>
        ''' <returns>The ad group that was created.</returns>
        Private Shared Function CreateAdGroup(ByVal user As AdWordsUser,
                                              ByVal campaign As Campaign) _
            As AdGroup
            Using adGroupService As AdGroupService = DirectCast(
                user.GetService(
                    AdWordsService.v201809.AdGroupService),
                AdGroupService)
                Dim group As New AdGroup()
                group.name = "Dynamic remarketing ad group"
                group.campaignId = campaign.id
                group.status = AdGroupStatus.ENABLED

                Dim op As New AdGroupOperation()
                op.operand = group
                op.operator = [Operator].ADD
                Dim result As AdGroupReturnValue = adGroupService.mutate(
                    New AdGroupOperation() {op})
                Return result.value(0)
            End Using
        End Function

        ''' <summary>
        ''' Attach a user list to an ad group. The user list provides positive targeting and feed
        ''' information to drive the dynamic content of the ad.
        ''' </summary>
        ''' <param name="user">The user.</param>
        ''' <param name="adGroup">The ad group which will have the user list attached.</param>
        ''' <param name="userListId">The user list to use for targeting and dynamic content.</param>
        ''' <remarks>User lists must be attached at the ad group level for positive targeting in
        ''' Shopping dynamic remarketing campaigns.</remarks>
        Private Shared Sub AttachUserList(ByVal user As AdWordsUser, ByVal adGroup As AdGroup,
                                          ByVal userListId As Long)
            Using adGroupCriterionService As AdGroupCriterionService = DirectCast(
                user.GetService(
                    AdWordsService.v201809.AdGroupCriterionService),
                AdGroupCriterionService)
                Dim userList As New CriterionUserList()
                userList.userListId = userListId
                Dim adGroupCriterion As New BiddableAdGroupCriterion()
                adGroupCriterion.criterion = userList
                adGroupCriterion.adGroupId = adGroup.id

                Dim op As New AdGroupCriterionOperation()
                op.operand = adGroupCriterion
                op.operator = [Operator].ADD

                adGroupCriterionService.mutate(New AdGroupCriterionOperation() {op})
            End Using
        End Sub

        ''' <summary>
        ''' Creates an ad for serving dynamic content in a remarketing campaign.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="adGroup">The ad group under which to create the ad.</param>
        ''' <returns>The ad that was created.</returns>
        Private Shared Function CreateAd(ByVal user As AdWordsUser, ByVal adGroup As AdGroup) _
            As AdGroupAd
            Using adService As AdGroupAdService = DirectCast(
                user.GetService(
                    AdWordsService.v201809.AdGroupAdService),
                AdGroupAdService)
                Dim ad As New ResponsiveDisplayAd()

                ' This ad format does not allow the creation of an image using the
                ' Image.data field. An image must first be created using the MediaService,
                ' and Image.mediaId must be populated when creating the ad.
                ad.marketingImage = UploadImage(user, "https://goo.gl/3b9Wfh")

                ad.shortHeadline = "Travel"
                ad.longHeadline = "Travel the World"
                ad.description = "Take to the air!"
                ad.businessName = "Interplanetary Cruises"
                ad.finalUrls = New String() {"http://www.example.com/"}

                ' Optional: Call to action text.
                ' Valid texts: https://support.google.com/adwords/answer/7005917
                ad.callToActionText = "Apply Now"

                ' Optional: Set dynamic display ad settings, composed of landscape logo
                ' image, promotion text, and price prefix.
                ad.dynamicDisplayAdSettings = CreateDynamicDisplayAdSettings(user)

                ' Optional: Create a logo image and set it to the ad.
                ad.logoImage = UploadImage(user, "https://goo.gl/mtt54n")

                ' Optional: Create a square marketing image and set it to the ad.
                ad.squareMarketingImage = UploadImage(user, "https://goo.gl/mtt54n")

                ' Whitelisted accounts only: Set color settings using hexadecimal values.
                ' Set allowFlexibleColor to false if you want your ads to render by always
                ' using your colors strictly.
                ' ad.mainColor = "#0000ff"
                ' ad.accentColor = "#ffff00"
                ' ad.allowFlexibleColor = False

                ' Whitelisted accounts only: Set the format setting that the ad will be
                ' served in.
                ' ad.formatSetting = DisplayAdFormatSetting.NON_NATIVE

                Dim adGroupAd As New AdGroupAd()
                adGroupAd.ad = ad
                adGroupAd.adGroupId = adGroup.id

                Dim op As New AdGroupAdOperation()
                op.operand = adGroupAd
                op.operator = [Operator].ADD

                Dim result As AdGroupAdReturnValue = adService.mutate(New AdGroupAdOperation() {op})
                Return result.value(0)
            End Using
        End Function

        ''' <summary>
        ''' Creates the additional content (images, promo text, etc.) supported by dynamic ads.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <returns>The DynamicSettings object to be used.</returns>
        Private Shared Function CreateDynamicDisplayAdSettings(ByVal user As AdWordsUser) _
            As DynamicSettings
            Dim logo As Image = UploadImage(user, "https://goo.gl/dEvQeF")

            Dim dynamicSettings As New DynamicSettings()
            dynamicSettings.landscapeLogoImage = logo
            dynamicSettings.pricePrefix = "as low as"
            dynamicSettings.promoText = "Free shipping!"
            Return dynamicSettings
        End Function

        ''' <summary>
        ''' Uploads the image from the specified <paramref name="url"/>.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="url">The image URL.</param>
        ''' <returns>ID of the uploaded image.</returns>
        Private Shared Function UploadImage(ByVal user As AdWordsUser, ByVal url As String) As Image
            Using mediaService As MediaService = DirectCast(
                user.GetService(
                    AdWordsService.v201809.MediaService),
                MediaService)
                ' Create the image.
                Dim image As New Image()
                image.data = MediaUtilities.GetAssetDataFromUrl(url, user.Config)
                image.type = MediaMediaType.IMAGE

                ' Upload the image And return the ID.
                Return DirectCast(mediaService.upload(New Media() {image})(0), Image)
            End Using
        End Function
    End Class
End Namespace

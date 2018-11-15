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
    ''' This code example adds a responsive display ad (MultiAssetResponsiveDisplayAd)
    ''' to an ad group. Image assets are uploaded using AssetService. To get ad groups,
    ''' run GetAdGroups.vb.
    ''' </summary>
    Public Class AddMultiAssetResponsiveDisplayAd
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New AddMultiAssetResponsiveDisplayAd
            Console.WriteLine(codeExample.Description)
            Try
                Dim adGroupId As Long = Long.Parse("INSERT_ADGROUP_ID_HERE")
                codeExample.Run(New AdWordsUser, adGroupId)
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
                Return "This code example adds a responsive display ad " +
                       "(MultiAssetResponsiveDisplayAd) to an ad group. Image assets are uploaded" +
                       " using AssetService. To get ad groups, run GetAdGroups.vb."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="adGroupId">ID of the adgroup to which ad is added.</param>
        Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long)
            Using adGroupAdService As AdGroupAdService = CType(
                user.GetService(
                    AdWordsService.v201809.AdGroupAdService),
                AdGroupAdService)
                Try
                    ' Create the ad.
                    Dim ad As New MultiAssetResponsiveDisplayAd()

                    ' Text assets can be specified directly in the asset field when
                    ' creating the ad.
                    Dim textAsset1 As New TextAsset
                    textAsset1.assetText = "Travel to Mars"
                    Dim headline1 As New AssetLink
                    headline1.asset = textAsset1

                    Dim textAsset2 As New TextAsset
                    textAsset2.assetText = "Travel to Jupiter"
                    Dim headline2 As New AssetLink
                    headline2.asset = textAsset2

                    Dim textAsset3 As New TextAsset
                    textAsset3.assetText = "Travel to Pluto"
                    Dim headline3 As New AssetLink
                    headline3.asset = textAsset3

                    ad.headlines = New AssetLink() {headline1, headline2, headline3}

                    Dim textAsset4 As New TextAsset
                    textAsset4.assetText = "Visit the planet in a luxury spaceship."
                    Dim description1 As New AssetLink
                    description1.asset = textAsset1

                    Dim textAsset5 As New TextAsset
                    textAsset5.assetText = "Travel to Jupiter"
                    Dim description2 As New AssetLink
                    description2.asset = textAsset5

                    Dim textAsset6 As New TextAsset
                    textAsset6.assetText = "Travel to Pluto"
                    Dim description3 As New AssetLink
                    description3.asset = textAsset6

                    ad.descriptions = New AssetLink() {description1, description2, description3}
                    ad.businessName = "Galactic Luxury Cruises"

                    Dim textAsset7 As New TextAsset
                    textAsset7.assetText = "Travel to Pluto"
                    Dim longHeadline As New AssetLink
                    longHeadline.asset = textAsset7

                    ad.longHeadline = longHeadline

                    ' This ad format does not allow the creation of an image asset by setting
                    ' the asset.imageData field. An image asset must first be created using the
                    ' AssetService, and asset.assetId must be populated when creating the ad.

                    Dim imageAsset1 As New ImageAsset
                    imageAsset1.assetId = UploadImageAsset(user, "https://goo.gl/3b9Wfh")
                    Dim marketingImage As New AssetLink
                    marketingImage.asset = imageAsset1
                    ad.marketingImages = New AssetLink() {marketingImage}

                    Dim imageAsset2 As New ImageAsset
                    imageAsset2.assetId = UploadImageAsset(user, "https://goo.gl/mtt54n")
                    Dim squareMarketingImage As New AssetLink
                    squareMarketingImage.asset = imageAsset2
                    ad.squareMarketingImages = New AssetLink() {squareMarketingImage}

                    ad.finalUrls = New String() {"http://www.example.com"}

                    ' Optional: set call to action text.
                    ad.callToActionText = "Shop Now"

                    ' Set color settings using hexadecimal values. Set allowFlexibleColor to false
                    ' if you want your ads to render by always using your colors strictly.
                    ad.mainColor = "#0000ff"
                    ad.accentColor = "#ffff00"
                    ad.allowFlexibleColor = False

                    ' Set the format setting that the ad will be served in.
                    ad.formatSetting = DisplayAdFormatSetting.NON_NATIVE

                    ' Optional: set dynamic display ad settings, composed of landscape logo
                    ' image, promotion text, And price prefix.
                    ad.dynamicSettingsPricePrefix = "as low as"
                    ad.dynamicSettingsPromoText = "Free shipping!"

                    Dim imageAsset3 As New ImageAsset
                    imageAsset3.assetId = UploadImageAsset(user, "https://goo.gl/mtt54n")
                    Dim logoImages As New AssetLink
                    logoImages.asset = imageAsset3
                    ad.logoImages = New AssetLink() {logoImages}

                    ' Create the ad group ad.
                    Dim adGroupAd As New AdGroupAd()
                    adGroupAd.ad = ad
                    adGroupAd.adGroupId = adGroupId

                    ' Create the operation.
                    Dim operation As New AdGroupAdOperation()
                    operation.operand = adGroupAd
                    operation.operator = [Operator].ADD

                    ' Make the mutate request.
                    Dim result As AdGroupAdReturnValue = adGroupAdService.mutate(
                        New AdGroupAdOperation() {operation})

                    ' Display results.
                    If Not (result Is Nothing) AndAlso Not (result.value Is Nothing) Then
                        For Each newAdGroupAd As AdGroupAd In result.value
                            Dim newAd As MultiAssetResponsiveDisplayAd =
                                    CType(newAdGroupAd.ad, MultiAssetResponsiveDisplayAd)
                            Console.WriteLine(
                                "Responsive display ad with ID '{0}' and long headline '{1}'" +
                                " was added.", newAd.id,
                                CType(newAd.longHeadline.asset, TextAsset).assetText)
                        Next
                    End If
                Catch e As Exception
                    Throw New System.ApplicationException(
                        "Failed to add expanded text ad to adgroup.",
                        e)
                End Try
            End Using
        End Sub

        ''' <summary>
        ''' Uploads the image from the specified <paramref name="url"/>.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="url">The image URL.</param>
        ''' <returns>ID of the uploaded image.</returns>
        Private Shared Function UploadImageAsset(ByVal user As AdWordsUser, ByVal url As String) _
            As Long
            Using assetService As AssetService = CType(
                user.GetService(
                    AdWordsService.v201809.AssetService),
                AssetService)

                ' Create the image asset.
                Dim imageAsset = New ImageAsset()
                ' Optional: Provide a unique friendly name to identify your asset. If you specify
                ' the assetName field, then both the asset name and the image being uploaded should
                ' be unique, and should not match another ACTIVE asset in this customer account.
                ' imageAsset.assetName = "Image asset " + ExampleUtilities.GetRandomString()
                imageAsset.imageData = MediaUtilities.GetAssetDataFromUrl(url, user.Config)

                ' Create the operation.
                Dim operation As New AssetOperation()
                operation.operator = [Operator].ADD
                operation.operand = imageAsset

                ' Create the asset And return the ID.
                Return assetService.mutate(New AssetOperation() {operation}).value(0).assetId
            End Using
        End Function
    End Class
End Namespace

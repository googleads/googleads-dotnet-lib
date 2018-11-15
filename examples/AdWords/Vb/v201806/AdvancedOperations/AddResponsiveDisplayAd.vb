' Copyright 2018 Google LLC
'
' Licensed under the Apache License, Version 2.0 (the "License");
' you may not use this file except in compliance with the License.
' You may obtain a copy of the License at
'
'     http://www.apache.org/licenses/LICENSE-2.0
'
' Unless required by applicable law or agreed to in writing, software
' distributed under the License is distributed on an "AS IS" BASIS,
' WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
' See the License for the specific language governing permissions and
' limitations under the License.

Imports Google.Api.Ads.AdWords.Lib
Imports Google.Api.Ads.AdWords.v201806
Imports Google.Api.Ads.Common.Util

Namespace Google.Api.Ads.AdWords.Examples.VB.v201806
    ''' <summary>
    ''' This code example adds an image representing the ad using the MediaService
    ''' and then adds a responsive display ad to an ad group. To get ad groups,
    ''' run GetAdGroups.vb.
    ''' </summary>
    Public Class AddResponsiveDisplayAd
        Inherits ExampleBase

        ''' <summary>
        ''' Number of items being added / updated in this code example.
        ''' </summary>
        Const NUM_ITEMS As Integer = 5

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New AddResponsiveDisplayAd
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
                Return _
                    "This code example adds an image representing the ad using the MediaService" &
                    "and then adds a responsive display ad to an ad group. To get ad groups, " &
                    "run GetAdGroups.vb."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="adGroupId">Id of the ad group to which ads are added.
        ''' </param>
        Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long)
            ' [START addResponsiveDisplayAd] MOE:strip_line
            Using adGroupAdService As AdGroupAdService = CType(
                user.GetService(
                    AdWordsService.v201806.AdGroupAdService),
                AdGroupAdService)

                Try
                    ' Create a responsive display ad.
                    Dim responsiveDisplayAd As New ResponsiveDisplayAd()

                    ' This ad format does not allow the creation of an image using the
                    ' Image.data field. An image must first be created using the MediaService,
                    ' and Image.mediaId must be populated when creating the ad.
                    responsiveDisplayAd.marketingImage = New Image()
                    responsiveDisplayAd.marketingImage.mediaId =
                        UploadImage(user,
                                    "https://goo.gl/3b9Wfh")

                    responsiveDisplayAd.shortHeadline = "Travel"
                    responsiveDisplayAd.longHeadline = "Travel the World"
                    responsiveDisplayAd.description = "Take to the air!"
                    responsiveDisplayAd.businessName = "Google"
                    responsiveDisplayAd.finalUrls = New String() {"http://www.example.com"}

                    ' Optional: Create a square marketing image Using MediaService, And set it
                    ' to the ad.
                    responsiveDisplayAd.squareMarketingImage = New Image()
                    responsiveDisplayAd.squareMarketingImage.mediaId =
                        UploadImage(user, "https://goo.gl/mtt54n")

                    ' Optional: Set call to action text.
                    responsiveDisplayAd.callToActionText = "Shop Now"

                    ' Optional: Set dynamic display ad settings, composed of landscape logo
                    ' image, promotion text, And price prefix.
                    responsiveDisplayAd.dynamicDisplayAdSettings =
                        CreateDynamicDisplayAdSettings(user)

                    ' Whitelisted accounts only Set color settings using hexadecimal values.
                    ' Set allowFlexibleColor to false if you want your ads to render by always
                    ' using your colors strictly.

                    ' responsiveDisplayAd.mainColor = "#0000ff"
                    ' responsiveDisplayAd.accentColor = "#ffff00"
                    ' responsiveDisplayAd.allowFlexibleColor = false

                    ' Whitelisted accounts only Set the format setting that the ad will be
                    ' served in.

                    ' responsiveDisplayAd.formatSetting = DisplayAdFormatSetting.NON_NATIVE;

                    ' Create ad group ad.
                    Dim adGroupAd As New AdGroupAd()
                    adGroupAd.adGroupId = adGroupId
                    adGroupAd.ad = responsiveDisplayAd
                    adGroupAd.status = AdGroupAdStatus.PAUSED

                    ' Create operation.
                    Dim operation As New AdGroupAdOperation()
                    operation.operand = adGroupAd
                    operation.operator = [Operator].ADD

                    ' Make the mutate request.
                    Dim result As AdGroupAdReturnValue = adGroupAdService.mutate(
                        New AdGroupAdOperation() {operation})

                    ' Display results.
                    If (Not result Is Nothing) AndAlso (Not result.value Is Nothing) Then
                        For Each newAdGroupAd As AdGroupAd In result.value
                            Dim newAd As ResponsiveDisplayAd = CType(newAdGroupAd.ad,
                                                                     ResponsiveDisplayAd)
                            Console.WriteLine(
                                "Responsive display ad with ID '{0}' and short headline '{1}'" &
                                " was added.", newAd.id, newAd.shortHeadline)
                        Next
                    Else
                        Console.WriteLine("No responsive display ads were created.")
                    End If
                Catch e As Exception
                    Throw _
                        New System.ApplicationException("Failed to create responsive display ads.",
                                                        e)
                End Try
            End Using
            ' [END addResponsiveDisplayAd] MOE:strip_line
        End Sub

        ''' <summary>
        ''' Creates the dynamic display ad settings.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <returns></returns>
        Private Shared Function CreateDynamicDisplayAdSettings(ByVal user As AdWordsUser) _
            As DynamicSettings
            Dim logoImageMediaId As Long = UploadImage(user, "https://goo.gl/dEvQeF")
            Dim logo As New Image()
            logo.mediaId = logoImageMediaId

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
                Return mediaService.upload(New Media() {image})(0).mediaId
            End Using
        End Function
    End Class
End Namespace

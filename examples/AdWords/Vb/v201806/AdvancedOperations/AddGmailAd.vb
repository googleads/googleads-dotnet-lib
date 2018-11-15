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
Imports Google.Api.Ads.AdWords.v201806
Imports Google.Api.Ads.Common.Util

Namespace Google.Api.Ads.AdWords.Examples.VB.v201806
    ''' <summary>
    ''' This code example adds a Gmail ad to a given ad group. The ad group's
    ''' campaign needs to have an AdvertisingChannelType of DISPLAY and
    ''' AdvertisingChannelSubType of DISPLAY_GMAIL_AD.
    ''' To get ad groups, run GetAdGroups.cs.
    ''' </summary>
    Public Class AddGmailAd
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New AddGmailAd
            Console.WriteLine(codeExample.Description)
            Try
                Dim adGroupId As Long = Long.Parse("INSERT_ADGROUP_ID_HERE")
                codeExample.Run(New AdWordsUser(), adGroupId)
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
                    "This code example adds a Gmail ad to a given ad group. The ad group's " & 
                    "campaign needs to have an AdvertisingChannelType of DISPLAY and " &
                    "AdvertisingChannelSubType of DISPLAY_GMAIL_AD. To get ad groups, run " &
                    "GetAdGroups.vb."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="adGroupId">Id of the adgroup to which ads are added.</param>
        Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long)
            Using adGroupAdService As AdGroupAdService = DirectCast(
                user.GetService(
                    AdWordsService.v201806.AdGroupAdService),
                AdGroupAdService)
                ' This ad format does not allow the creation of an image using the
                ' Image.data field. An image must first be created using the
                ' MediaService, and Image.mediaId must be populated when creating the
                ' ad.
                Dim logoImage As New Image()
                logoImage.mediaId = UploadImage(user, "https://goo.gl/mtt54n").mediaId

                Dim marketingImage As New Image()
                marketingImage.mediaId = UploadImage(user, "https://goo.gl/3b9Wfh").mediaId

                Dim teaser As New GmailTeaser()
                teaser.headline = "Dream"
                teaser.description = "Create your own adventure"
                teaser.businessName = "Interplanetary Ships"
                teaser.logoImage = logoImage

                ' Creates a Gmail ad.
                Dim gmailAd As New GmailAd()
                gmailAd.teaser = teaser
                gmailAd.marketingImage = marketingImage
                gmailAd.marketingImageHeadline = "Travel"
                gmailAd.marketingImageDescription = "Take to the skies!"
                gmailAd.finalUrls = New String() {"http://www.example.com/"}

                ' Creates ad group ad for the Gmail ad.
                Dim adGroupAd As New AdGroupAd()
                adGroupAd.adGroupId = adGroupId
                adGroupAd.ad = gmailAd

                ' Optional: Set additional settings.
                adGroupAd.status = AdGroupAdStatus.PAUSED

                ' Creates ad group ad operation and add it to the list.
                Dim operation As New AdGroupAdOperation()
                operation.operand = adGroupAd
                operation.operator = [Operator].ADD

                Try
                    ' Adds a responsive display ad on the server.
                    Dim result As AdGroupAdReturnValue = adGroupAdService.mutate(
                        New AdGroupAdOperation() {operation})

                    If result Is Nothing Or result.value Is Nothing Or result.value.Length = 0 Then
                        Console.WriteLine("No Gmail ads were added.")
                        Return
                    End If

                    ' Prints out some information for each created Gmail ad.
                    For Each newAdGroupAd As AdGroupAd In result.value
                        Console.WriteLine("A Gmail ad with ID {0} and headline '{1}' was added.",
                                          newAdGroupAd.ad.id,
                                          DirectCast(newAdGroupAd.ad, GmailAd).teaser.headline)
                    Next
                Catch e As Exception
                    Throw New System.ApplicationException("Failed to add Gmail ads.", e)
                End Try
            End Using
        End Sub

        ''' <summary>
        ''' Uploads an image to the server.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="url">The URL of image to upload.</param>
        ''' <returns>The created image.</returns>
        Private Shared Function UploadImage(ByVal user As AdWordsUser, ByVal url As String) As Media
            Using mediaService As MediaService = DirectCast(
                user.GetService(
                    AdWordsService.v201806.MediaService),
                MediaService)
                Dim image As New Image()
                image.data = MediaUtilities.GetAssetDataFromUrl(url, user.Config)
                image.type = MediaMediaType.IMAGE
                Return mediaService.upload(New Media() {image})(0)
            End Using
        End Function
    End Class
End Namespace

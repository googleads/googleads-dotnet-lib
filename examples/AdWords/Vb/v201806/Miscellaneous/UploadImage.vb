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
    ''' This code example uploads an image. To get images, run GetAllVideosAndImages.vb.
    ''' </summary>
    Public Class UploadImage
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New UploadImage
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
                Return _
                    "This code example uploads an image. To get images, run " &
                    "GetAllVideosAndImages.vb."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        Public Sub Run(ByVal user As AdWordsUser)
            Using mediaService As MediaService = CType(
                user.GetService(
                    AdWordsService.v201806.MediaService),
                MediaService)

                ' Create the image.
                Dim image As New Image
                image.data = MediaUtilities.GetAssetDataFromUrl("https://goo.gl/3b9Wfh",
                                                                user.Config)
                image.type = MediaMediaType.IMAGE

                Try
                    ' Upload the image.
                    Dim result As Media() = mediaService.upload(New Media() {image})

                    ' Display the results.
                    If ((Not result Is Nothing) AndAlso (result.Length > 0)) Then
                        Dim newImage As Media = result(0)

                        ' Preferred: Use newImage.dimensions.ToDict() if you are not on Mono.
                        Dim dimensions As Dictionary(Of MediaSize, Dimensions) =
                                MapEntryExtensions.ToDict (Of MediaSize, Dimensions)(
                                    newImage.dimensions)

                        Console.WriteLine(
                            "Image with id '{0}', dimensions '{1}x{2}', and MIME type '{3}'" &
                            " was uploaded.", newImage.mediaId,
                            dimensions.Item(MediaSize.FULL).width,
                            dimensions.Item(MediaSize.FULL).height, newImage.mimeType)
                    Else
                        Console.WriteLine("No images were uploaded.")
                    End If
                Catch e As Exception
                    Throw New System.ApplicationException("Failed to upload images.", e)
                End Try
            End Using
        End Sub
    End Class
End Namespace

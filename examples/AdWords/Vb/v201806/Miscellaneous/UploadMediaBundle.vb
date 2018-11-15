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
    ''' This code example uploads an HTML5 zip file.
    ''' </summary>
    Public Class UploadMediaBundle
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New UploadMediaBundle
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
                Return "This code example uploads an HTML5 zip file."
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

                Try
                    ' Create HTML5 media.
                    Dim html5Zip As Byte() =
                            MediaUtilities.GetAssetDataFromUrl("https://goo.gl/9Y7qI2",
                                                               user.Config)
                    ' Create a media bundle containing the zip file with all the HTML5 components.
                    Dim mediaBundleArray(1) As Media

                    Dim mediaBundle As New MediaBundle()
                    mediaBundle.data = html5Zip
                    mediaBundle.type = MediaMediaType.MEDIA_BUNDLE

                    mediaBundleArray(0) = mediaBundle

                    ' Upload HTML5 zip.
                    mediaBundleArray = mediaService.upload(mediaBundleArray)

                    ' Display HTML5 zip.
                    If (Not mediaBundleArray Is Nothing) AndAlso (mediaBundleArray.Length > 0) Then
                        Dim newBundle As Media = mediaBundleArray(0)

                        ' Preferred: Use newBundle.dimensions.ToDict() if you are not on Mono.
                        Dim dimensions As Dictionary(Of MediaSize, Dimensions) =
                                MapEntryExtensions.ToDict (Of MediaSize, Dimensions)(
                                    newBundle.dimensions)

                        Console.WriteLine("HTML5 media with id '{0}', dimensions '{1}x{2}', and " &
                                          "MIME type '{3}' was uploaded.", newBundle.mediaId,
                                          dimensions(MediaSize.FULL).width,
                                          dimensions(MediaSize.FULL).height,
                                          newBundle.mimeType)
                    Else
                        Console.WriteLine("No HTML5 zip was uploaded.")
                    End If
                Catch e As Exception
                    Throw New System.ApplicationException("Failed to upload HTML5 zip file.", e)
                End Try
            End Using
        End Sub
    End Class
End Namespace

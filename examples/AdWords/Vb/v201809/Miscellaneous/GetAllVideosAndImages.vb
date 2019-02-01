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
Imports Google.Api.Ads.AdWords.v201809

Namespace Google.Api.Ads.AdWords.Examples.VB.v201809
    ''' <summary>
    ''' This code example gets all videos And images. Use the Google Ads website to upload New
    ''' videos. To upload image, run UploadImage.vb.
    ''' </summary>
    Public Class GetAllVideosAndImages
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New GetAllVideosAndImages
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
                  "This code example gets all videos and images. Use the Google Ads " &
                      "website to upload new videos. To upload image, run UploadImage.vb."
      End Get
    End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        Public Sub Run(ByVal user As AdWordsUser)
            Using mediaService As MediaService = CType(
                user.GetService(
                    AdWordsService.v201809.MediaService),
                MediaService)

                ' Create the video selector.
                Dim selector As New Selector
                selector.fields = New String() { _
                                                   Media.Fields.MediaId, Dimensions.Fields.Width,
                                                   Dimensions.Fields.Height, Media.Fields.MimeType
                                               }

                ' Set the filter.
                Dim mediaTypes = New String() { _
                                                  MediaMediaType.VIDEO.ToString(),
                                                  MediaMediaType.IMAGE.ToString()
                                              }
                selector.predicates = New Predicate() { _
                                                          Predicate.In(Media.Fields.Type,
                                                                       mediaTypes)
                                                      }

                selector.paging = Paging.Default

                Dim page As New MediaPage

                Try
                    Do
                        page = mediaService.get(selector)

                        If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
                            Dim i As Integer = selector.paging.startIndex

                            For Each media As Media In page.entries
                                If TypeOf media Is Video Then
                                    Dim video As Video = CType(media, [Video])
                                    Console.WriteLine(
                                        "{0}) Video with id '{1}' and name '{2}' was found.",
                                        i + 1, video.mediaId, video.name)
                                ElseIf TypeOf media Is Image Then
                                    Dim image As Image = CType(media, [Image])

                                    ' Preferred: Use image.dimensions.ToDict() if you are not on
                                    ' Mono.
                                    Dim dimensions As Dictionary(Of MediaSize, Dimensions) =
                                            MapEntryExtensions.ToDict (Of MediaSize, Dimensions)(
                                                image.dimensions)

                                    Console.WriteLine(
                                        "{0}) Image with id '{1}', dimensions '{2}x{3}', and " &
                                        "MIME type '{4}' was found.", i + 1, image.mediaId,
                                        dimensions(MediaSize.FULL).width,
                                        dimensions(MediaSize.FULL).height, image.mimeType)
                                End If
                                i = i + 1
                            Next
                        End If
                        selector.paging.IncreaseOffset()
                    Loop While (selector.paging.startIndex < page.totalNumEntries)
                    Console.WriteLine("Number of images and videos found: {0}",
                                      page.totalNumEntries)
                Catch e As Exception
                    Throw New System.ApplicationException("Failed to get images and videos.", e)
                End Try
            End Using
        End Sub
    End Class
End Namespace

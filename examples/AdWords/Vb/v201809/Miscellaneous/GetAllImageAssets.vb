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
    ''' This code example gets all image assets. To upload an image asset, run UploadImageAsset.vb.
    ''' </summary>
    Public Class GetAllImageAssets
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New GetAllImageAssets
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
                Return "This code example gets all image assets. To upload an image asset, run " +
                       "UploadImageAsset.vb."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        Public Sub Run(ByVal user As AdWordsUser)
            ' [START get_all_image_assets] MOE:strip_line
            Using assetService As AssetService = CType(
                user.GetService(
                    AdWordsService.v201809.AssetService),
                AssetService)

                ' Create the selector.
                Dim selector As New Selector()
                selector.fields = New String() { _
                                                   Asset.Fields.AssetName, Asset.Fields.AssetStatus,
                                                   ImageAsset.Fields.ImageFileSize,
                                                   ImageDimensionInfo.Fields.ImageWidth,
                                                   ImageDimensionInfo.Fields.ImageHeight,
                                                   ImageDimensionInfo.Fields.ImageFullSizeUrl
                                               }
                ' Filter for image assets only.
                selector.predicates = New Predicate() { _
                                                          Predicate.Equals(Asset.Fields.AssetSubtype,
                                                                           AssetType.IMAGE.ToString())
                                                      }
                selector.paging = Paging.Default

                Dim page As New AssetPage()

                Try
                    Do
                        ' Get the image assets.
                        page = assetService.get(selector)

                        ' Display the results.
                        If Not (page Is Nothing) AndAlso Not (page.entries Is Nothing) Then
                            Dim i As Integer = selector.paging.startIndex
                            For Each imageAsset As ImageAsset In page.entries
                                Console.WriteLine(
                                    "{0}) Image asset with id = '{1}', name = '{2}' and " +
                                    "status = '{3}' was found.", i + 1, imageAsset.assetId,
                                    imageAsset.assetName, imageAsset.assetStatus)
                                Console.WriteLine("  Size is {0}x{1} and asset URL is {2}.",
                                                  imageAsset.fullSizeInfo.imageWidth,
                                                  imageAsset.fullSizeInfo.imageHeight,
                                                  imageAsset.fullSizeInfo.imageUrl)
                                i = i + 1
                            Next
                        End If
                        selector.paging.IncreaseOffset()
                    Loop While (selector.paging.startIndex < page.totalNumEntries)
                    Console.WriteLine("Number of image assets found: {0}", page.totalNumEntries)
                    ' [END get_all_image_assets] MOE:strip_line
                Catch e As Exception
                    Throw New System.ApplicationException("Failed to retrieve image assets.", e)
                End Try
            End Using
        End Sub
    End Class
End Namespace
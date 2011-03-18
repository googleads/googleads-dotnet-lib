' Copyright 2011, Google Inc. All Rights Reserved.
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

' Author: api.anash@gmail.com (Anash P. Oommen)

Imports Google.Api.Ads.AdWords.Lib
Imports Google.Api.Ads.AdWords.v201008

Imports System
Imports System.Collections.Generic

Namespace Google.Api.Ads.AdWords.Examples.VB.v201008
  ''' <summary>
  ''' This code example gets all images. To upload an image, run
  ''' UploadImage.vb.
  '''
  ''' Tags: MediaService.get
  ''' </summary>
  Class GetAllImages
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example gets all images. To upload an image, run UploadImage.vb."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New GetAllImages
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the MediaService.
      Dim mediaService As MediaService = user.GetService(AdWordsService.v201008.MediaService)

      ' Create selector.
      Dim selector As New MediaSelector
      selector.mediaType = MediaMediaType.IMAGE

      Try
        ' Get all images.
        Dim page As MediaPage = mediaService.get(selector)

        ' Display images.
        If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing) AndAlso _
            (page.entries.Length > 0)) Then
          For Each image As Image In page.entries
            Dim dimensions As Dictionary(Of MediaSize, Dimensions) = _
                CreateMediaDimensionMap(image.dimensions)
            Console.WriteLine("Image with id '{0}', dimensions '{1}x{2}', and MIME type " & _
                "'{3}' was found.", image.mediaId, dimensions.Item(MediaSize.FULL).width, _
                dimensions.Item(MediaSize.FULL).height, image.mimeType)
          Next
        Else
          Console.WriteLine("No images were found.")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to get all images. Exception says ""{0}""", ex.Message)
      End Try
    End Sub

    ''' <summary>
    ''' Converts an array of Media_Size_DimensionsMapEntry into a dictionary.
    ''' </summary>
    ''' <param name="dimensions">The array of Media_Size_DimensionsMapEntry to be
    ''' converted into a dictionary.</param>
    ''' <returns>A dictionary with key as MediaSize, and value as Dimensions.
    ''' </returns>
    Private Function CreateMediaDimensionMap(ByVal dimensions As Media_Size_DimensionsMapEntry()) _
            As Dictionary(Of MediaSize, Dimensions)
      Dim mediaMap As New Dictionary(Of MediaSize, Dimensions)
      For Each dimension As Media_Size_DimensionsMapEntry In dimensions
        mediaMap.Add(dimension.key, dimension.value)
      Next
      Return mediaMap
    End Function
  End Class
End Namespace

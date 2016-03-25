' Copyright 2016, Google Inc. All Rights Reserved.
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
Imports Google.Api.Ads.AdWords.v201603

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201603
  ''' <summary>
  ''' This code example gets all videos and images. To upload video, see
  ''' http://adwords.google.com/support/aw/bin/answer.py?hl=en&amp;answer=39454.
  ''' To upload image, run UploadImage.vb.
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
        Console.WriteLine("An exception occurred while running this code example. {0}", _
            ExampleUtilities.FormatException(e))
      End Try
    End Sub

    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example gets all videos and images. To upload video, see " & _
            "http://adwords.google.com/support/aw/bin/answer.py?hl=en&amp;answer=39454. To " & _
            "upload image, run UploadImage.vb."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    Public Sub Run(ByVal user As AdWordsUser)
      ' Get the MediaService.
      Dim mediaService As MediaService = CType(user.GetService( _
          AdWordsService.v201603.MediaService), MediaService)

      ' Create the video selector.
      Dim selector As New Selector
      selector.fields = New String() {
        Media.Fields.MediaId, Dimensions.Fields.Width,
        Dimensions.Fields.Height, Media.Fields.MimeType
      }

      ' Set the filter.
      Dim predicate As New Predicate
      predicate.operator = PredicateOperator.IN
      predicate.field = "Type"
      predicate.values = New String() {MediaMediaType.VIDEO.ToString(), _
          MediaMediaType.IMAGE.ToString()}

      selector.predicates = New Predicate() {
        predicate.In(Media.Fields.Type, New String() {
          MediaMediaType.VIDEO.ToString(),
          MediaMediaType.IMAGE.ToString()
        })
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
                Console.WriteLine("{0}) Video with id '{1}' and name '{2}' was found.", _
                    i + 1, video.mediaId, video.name)
              ElseIf TypeOf media Is Image Then
                Dim image As Image = CType(media, [Image])
                Dim dimensions As Dictionary(Of MediaSize, Dimensions) = _
                    CreateMediaDimensionMap(image.dimensions)
                Console.WriteLine("{0}) Image with id '{1}', dimensions '{2}x{3}', and MIME " & _
                    "type '{4}' was found.", i + 1, image.mediaId, _
                    dimensions(MediaSize.FULL).width, _
                    dimensions(MediaSize.FULL).height, image.mimeType)
              End If
              i = i + 1
            Next
          End If
          selector.paging.IncreaseOffset()
        Loop While (selector.paging.startIndex < page.totalNumEntries)
        Console.WriteLine("Number of images and videos found: {0}", page.totalNumEntries)
      Catch e As Exception
        Throw New System.ApplicationException("Failed to get images and videos.", e)
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

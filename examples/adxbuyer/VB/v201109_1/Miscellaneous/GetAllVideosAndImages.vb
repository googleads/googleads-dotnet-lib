' Copyright 2012, Google Inc. All Rights Reserved.
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
Imports Google.Api.Ads.AdWords.v201109_1

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201109_1
  ''' <summary>
  ''' This code example gets all videos and images. To upload video, see
  ''' http://adwords.google.com/support/aw/bin/answer.py?hl=en&amp;answer=39454.
  ''' To upload image, run UploadImage.vb.
  '''
  ''' Tags: MediaService.get
  ''' </summary>
  Public Class GetAllVideosAndImages
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As ExampleBase = New GetAllVideosAndImages
      Console.WriteLine(codeExample.Description)
      Try
        codeExample.Run(New AdWordsUser, codeExample.GetParameters, Console.Out)
      Catch ex As Exception
        Console.WriteLine("An exception occurred while running this code example. {0}", _
            ExampleUtilities.FormatException(ex))
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
    ''' Gets the list of parameter names required to run this code example.
    ''' </summary>
    ''' <returns>
    ''' A list of parameter names for this code example.
    ''' </returns>
    Public Overrides Function GetParameterNames() As String()
      Return New String() {}
    End Function

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="parameters">The parameters for running the code
    ''' example.</param>
    ''' <param name="writer">The stream writer to which script output should be
    ''' written.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser, ByVal parameters As  _
        Dictionary(Of String, String), ByVal writer As TextWriter)
      ' Get the MediaService.
      Dim mediaService As MediaService = user.GetService(AdWordsService.v201109_1.MediaService)

      ' Create the video selector.
      Dim selector As New Selector
      selector.fields = New String() {"MediaId", "Width", "Height", "MimeType"}

      ' Set the filter.
      Dim predicate As New Predicate
      predicate.operator = PredicateOperator.IN
      predicate.field = "Type"
      predicate.values = New String() {MediaMediaType.VIDEO.ToString(), _
          MediaMediaType.IMAGE.ToString()}

      selector.predicates = New Predicate() {predicate}

      ' Select selector paging.
      selector.paging = New Paging

      Dim offset As Integer = 0
      Dim pageSize As Integer = 500

      Dim page As New MediaPage

      Try
        Do
          selector.paging.startIndex = offset
          selector.paging.numberResults = pageSize

          page = mediaService.get(selector)

          If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
            Dim i As Integer = offset

            For Each media As Media In page.entries
              If TypeOf media Is Video Then
                Dim video As Video = media
                writer.WriteLine("{0}) Video with id '{1}' and name '{2}' was found.", _
                    i, video.mediaId, video.name)
              ElseIf TypeOf media Is Image Then
                Dim image As Image = media
                Dim dimensions As Dictionary(Of MediaSize, Dimensions) = _
                    CreateMediaDimensionMap(image.dimensions)
                writer.WriteLine("{0}) Image with id '{1}', dimensions '{2}x{3}', and MIME " & _
                    "type '{4}' was found.", i, image.mediaId, dimensions(MediaSize.FULL).width, _
                    dimensions(MediaSize.FULL).height, image.mimeType)
              End If
              i = i + 1
            Next
          End If
          offset = offset + pageSize
        Loop While (offset < page.totalNumEntries)
        writer.WriteLine("Number of images and videos found: {0}", page.totalNumEntries)
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to get images and videos.", ex)
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

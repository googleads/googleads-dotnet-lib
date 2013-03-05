' Copyright 2013, Google Inc. All Rights Reserved.
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
Imports Google.Api.Ads.AdWords.v201302

Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Text

Namespace Google.Api.Ads.AdWords.Examples.VB.v201302
  ''' <summary>
  ''' This code example retrieves keywords that are related to a given keyword.
  '''
  ''' Tags: TargetingIdeaService.get
  ''' </summary>
  Public Class GetKeywordIdeas
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New GetKeywordIdeas
      Console.WriteLine(codeExample.Description)
      Try
        codeExample.Run(New AdWordsUser)
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
        Return "This code example retrieves keywords that are related to a given keyword."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    Public Sub Run(ByVal user As AdWordsUser)
      ' Get the TargetingIdeaService.
      Dim targetingIdeaService As TargetingIdeaService = user.GetService( _
          AdWordsService.v201302.TargetingIdeaService)

      Dim keywordText As String = "mars cruise"

      ' Create selector.
      Dim selector As New TargetingIdeaSelector()
      selector.requestType = RequestType.IDEAS
      selector.ideaType = IdeaType.KEYWORD
      selector.requestedAttributeTypes = New AttributeType() { _
        AttributeType.KEYWORD_TEXT, AttributeType.SEARCH_VOLUME, _
        AttributeType.CATEGORY_PRODUCTS_AND_SERVICES}

      ' Create related to query search parameter.
      Dim relatedToQuerySearchParameter As New RelatedToQuerySearchParameter()
      relatedToQuerySearchParameter.queries = New String() {keywordText}
      selector.searchParameters = New SearchParameter() {relatedToQuerySearchParameter}

      ' Set selector paging (required for targeting idea service).
      Dim paging As New Paging()
      paging.startIndex = 0
      paging.numberResults = 500
      selector.paging = paging

      Dim offset As Long = 0
      Dim pageSize As Long = 500

      Dim page As New TargetingIdeaPage()

      Try
        Do
          selector.paging.startIndex = offset
          selector.paging.numberResults = pageSize

          ' Get related keywords.
          page = targetingIdeaService.get(selector)

          'Display the results.

          If Not page.entries Is Nothing AndAlso page.entries.Length > 0 Then
            Dim i As Integer = offset
            For Each targetingIdea As TargetingIdea In page.entries
              Dim keyword As String = Nothing
              Dim categories As String = Nothing
              Dim averageMonthlySearches As Long = 0

              For Each entry As Type_AttributeMapEntry In targetingIdea.data
                If entry.key = AttributeType.KEYWORD_TEXT Then
                  keyword = DirectCast(entry.value, StringAttribute).value
                End If
                If entry.key = AttributeType.CATEGORY_PRODUCTS_AND_SERVICES Then
                  Dim categorySet As IntegerSetAttribute = entry.value
                  Dim builder As New StringBuilder()
                  For Each value As Integer In categorySet.value
                    builder.AppendFormat("{0}, ", value)
                  Next
                  Dim trimChars As Char() = New Char() {",", " "}
                  categories = builder.ToString().Trim(trimChars)
                End If
                If entry.key = AttributeType.SEARCH_VOLUME Then
                  averageMonthlySearches = DirectCast(entry.value, LongAttribute).value
                End If
              Next
              Console.WriteLine("Keyword with text '{0}', and average monthly search volume " & _
                  "'{1}' was found with categories: {2}", keywordText, averageMonthlySearches, _
                  categories)
              i = i + 1
            Next
          End If
          offset = offset + pageSize
        Loop While (offset < page.totalNumEntries)
        Console.WriteLine("Number of related keywords found: {0}", page.totalNumEntries)
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to retrieve related keywords.", ex)
      End Try
    End Sub
  End Class
End Namespace

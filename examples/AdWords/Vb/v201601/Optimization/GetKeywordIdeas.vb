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
Imports Google.Api.Ads.AdWords.v201601

Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Text

Namespace Google.Api.Ads.AdWords.Examples.VB.v201601
  ''' <summary>
  ''' This code example retrieves keywords that are related to a given keyword.
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
        Return "This code example retrieves keywords that are related to a given keyword."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    Public Sub Run(ByVal user As AdWordsUser)
      ' Get the TargetingIdeaService.
      Dim targetingIdeaService As TargetingIdeaService = CType(user.GetService( _
          AdWordsService.v201601.TargetingIdeaService), TargetingIdeaService)

      ' Create selector.
      Dim selector As New TargetingIdeaSelector()
      selector.requestType = RequestType.IDEAS
      selector.ideaType = IdeaType.KEYWORD
      selector.requestedAttributeTypes = New AttributeType() { _
        AttributeType.KEYWORD_TEXT,
        AttributeType.SEARCH_VOLUME,
        AttributeType.CATEGORY_PRODUCTS_AND_SERVICES _
      }

      ' Create the search parameters.
      Dim keywordText As String = "mars cruise"

      ' Create related to query search parameter.
      Dim relatedToQuerySearchParameter As New RelatedToQuerySearchParameter()
      relatedToQuerySearchParameter.queries = new String() {keywordText}

      ' Add a language search parameter (optional).
      ' The ID can be found in the documentation:
      '   https://developers.google.com/adwords/api/docs/appendix/languagecodes
      Dim languageParameter As New LanguageSearchParameter()
      Dim english As New Language()
      english.id = 1000
      languageParameter.languages = New Language() {english}

      ' Add network search parameter (optional).
      Dim networkSetting As New NetworkSetting()
      networkSetting.targetGoogleSearch = True
      networkSetting.targetSearchNetwork = False
      networkSetting.targetContentNetwork = False
      networkSetting.targetPartnerSearchNetwork = False

      Dim networkSearchParameter As New NetworkSearchParameter()
      networkSearchParameter.networkSetting = networkSetting

      ' Set the search parameters.
      selector.searchParameters = New SearchParameter() { _
        relatedToQuerySearchParameter, languageParameter, networkSearchParameter
      }

      ' Set selector paging (required for targeting idea service).
      Dim paging As Paging = paging.Default
      Dim page As New TargetingIdeaPage()

      Try
        Dim i As Integer = 0
        Do
          ' Get related keywords.
          page = targetingIdeaService.get(selector)

          'Display the results.

          If Not page.entries Is Nothing AndAlso page.entries.Length > 0 Then
            For Each targetingIdea As TargetingIdea In page.entries
              Dim keyword As String = Nothing
              Dim categories As String = Nothing
              Dim averageMonthlySearches As Long = 0

              For Each entry As Type_AttributeMapEntry In targetingIdea.data
                If entry.key = AttributeType.KEYWORD_TEXT Then
                  keyword = DirectCast(entry.value, StringAttribute).value
                End If
                If entry.key = AttributeType.CATEGORY_PRODUCTS_AND_SERVICES Then
                  Dim categorySet As IntegerSetAttribute = CType(entry.value, IntegerSetAttribute)
                  Dim builder As New StringBuilder()
                  For Each value As Integer In categorySet.value
                    builder.AppendFormat("{0}, ", value)
                  Next
                  Dim trimChars As Char() = New Char() {CChar(","), CChar(" ")}
                  categories = builder.ToString().Trim(trimChars)
                End If
                If entry.key = AttributeType.SEARCH_VOLUME Then
                  averageMonthlySearches = DirectCast(entry.value, LongAttribute).value
                End If
              Next
              Console.WriteLine("Keyword with text '{0}', and average monthly search volume " & _
                  "'{1}' was found with categories: {2}", keyword, averageMonthlySearches, _
                  categories)
              i = i + 1
            Next
          End If
          selector.paging.IncreaseOffset()
        Loop While (selector.paging.startIndex < page.totalNumEntries)
        Console.WriteLine("Number of related keywords found: {0}", page.totalNumEntries)
      Catch e As Exception
        Throw New System.ApplicationException("Failed to retrieve related keywords.", e)
      End Try
    End Sub
  End Class
End Namespace

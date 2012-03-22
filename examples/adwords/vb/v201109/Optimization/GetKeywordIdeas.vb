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
Imports Google.Api.Ads.AdWords.v201109

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201109
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
      Dim codeExample As ExampleBase = New GetKeywordIdeas
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
        Return "This code example retrieves keywords that are related to a given keyword."
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
      ' Get the TargetingIdeaService.
      Dim targetingIdeaService As TargetingIdeaService = user.GetService( _
          AdWordsService.v201109.TargetingIdeaService)

      Dim keywordText As String = "mars cruise"

      ' Create seed keyword.
      Dim keyword As New Keyword
      keyword.text = keywordText
      keyword.matchType = KeywordMatchType.BROAD

      ' Create the selector.
      Dim selector As New TargetingIdeaSelector
      selector.requestType = RequestType.IDEAS
      selector.ideaType = IdeaType.KEYWORD
      selector.requestedAttributeTypes = New AttributeType() {AttributeType.CRITERION, _
          AttributeType.AVERAGE_TARGETED_MONTHLY_SEARCHES}

      ' Create related to keyword search parameter.
      Dim relatedToKeywordSearchParameter As New RelatedToKeywordSearchParameter
      relatedToKeywordSearchParameter.keywords = New Keyword() {keyword}

      ' Create keyword match type search parameter to ensure unique results.
      Dim keywordMatchTypeSearchParameter As New KeywordMatchTypeSearchParameter
      keywordMatchTypeSearchParameter.keywordMatchTypes = _
          New KeywordMatchType() {KeywordMatchType.BROAD}

      selector.searchParameters = New SearchParameter() {relatedToKeywordSearchParameter, _
          keywordMatchTypeSearchParameter}

      ' Select selector paging.
      selector.paging = New Paging

      Dim offset As Integer = 0
      Dim pageSize As Integer = 500

      Dim page As New TargetingIdeaPage

      Try
        Do
          selector.paging.startIndex = offset
          selector.paging.numberResults = pageSize

          ' Get related keywords.
          page = targetingIdeaService.get(selector)

          'Display the results.
          If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
            Dim i As Integer = offset
            For Each idea As TargetingIdea In page.entries
              For Each entry As Type_AttributeMapEntry In idea.data
                If (entry.key = AttributeType.CRITERION) Then
                  Dim kwdAttribute As CriterionAttribute = entry.value
                  Dim relatedKeyword As Keyword = kwdAttribute.value
                  writer.WriteLine("{0}) Related keyword with text = '{1}' and match type = " & _
                      "'{2}' was found.", i, relatedKeyword.text, relatedKeyword.matchType)
                End If
              Next
            Next
            i += 1
          End If
          offset = offset + pageSize
        Loop While (offset < page.totalNumEntries)
        writer.WriteLine("Number of related keywords found: {0}", page.totalNumEntries)
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to retrieve related keywords.", ex)
      End Try
    End Sub
  End Class
End Namespace

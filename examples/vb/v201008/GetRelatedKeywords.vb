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

Namespace Google.Api.Ads.AdWords.Examples.VB.v201008
  ''' <summary>
  ''' This code example retrieves keywords that are related to a given keyword.
  '''
  ''' Tags: TargetingIdeaService.get
  ''' </summary>
  Class GetRelatedKeywords
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example retrieves keywords that are related to a given keyword."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New GetRelatedKeywords
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the TargetingIdeaService.
      Dim targetingIdeaService As TargetingIdeaService = user.GetService( _
          AdWordsService.v201008.TargetingIdeaService)

      Dim keywordText As String = "space cruise"

      Dim keyword As New Keyword
      keyword.text = keywordText
      keyword.matchType = KeywordMatchType.EXACT

      Dim searchParameter As New RelatedToKeywordSearchParameter
      searchParameter.keywords = New Keyword() {keyword}

      Dim selector As New TargetingIdeaSelector
      selector.searchParameters = New SearchParameter() {searchParameter}
      selector.ideaType = IdeaType.KEYWORD
      selector.requestType = RequestType.IDEAS

      Dim paging As New Paging
      paging.startIndex = 0
      paging.numberResults = 10

      selector.paging = paging

      Try
        Dim page As TargetingIdeaPage = targetingIdeaService.get(selector)

        If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
          Console.WriteLine("There are a total of {0} keywords related to '{1}'. The first " & _
              "{2} entries are displayed below: \n", page.totalNumEntries, keywordText, _
              page.entries.Length)
          For Each idea As TargetingIdea In page.entries
            For Each entry As Type_AttributeMapEntry In idea.data
              If (entry.key = AttributeType.KEYWORD) Then
                Dim kwdAttribute As KeywordAttribute = entry.value
                Console.WriteLine("Related keyword with text = '{0}' and match type = '{1}' " & _
                    "was found.", kwdAttribute.value.text, kwdAttribute.value.matchType)
              End If
            Next
          Next
        Else
          Console.WriteLine("No related keywords were found for your keyword.")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to retrieve related keywords. Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace

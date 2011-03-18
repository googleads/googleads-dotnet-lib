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
Imports Google.Api.Ads.AdWords.v13
Imports Google.Api.Ads.AdWords.v200909

Imports System
Imports System.Collections.Generic

Namespace Google.Api.Ads.AdWords.Examples.VB.Both
  ''' <summary>
  ''' This code example shows how to use both v13 and v200909 APIs in a
  ''' single method.
  ''' </summary>
  Class UsingTrafficEstimatorDemo
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    ''' <value></value>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "Shows how to use both v13 and v200909 APIs in a single method."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New UsingTrafficEstimatorDemo
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      Dim targetingIdeaService As TargetingIdeaService = user.GetService( _
          AdWordsService.v200909.TargetingIdeaService)

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

      Dim suggestions As New List(Of Keyword)

      Try
        Dim page As TargetingIdeaPage = targetingIdeaService.get(selector)

        If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
          Console.WriteLine("There are a total of {0} keywords related to '{1}'. The first {2} " & _
              "entries are displayed below: \n", page.totalNumEntries, keywordText, _
              page.entries.Length)
          For Each idea As TargetingIdea In page.entries
            For Each entry As Type_AttributeMapEntry In idea.data
              If (entry.key = AttributeType.KEYWORD) Then
                Dim kwdAttribute As KeywordAttribute = entry.value
                suggestions.Add(kwdAttribute.value)
              End If
            Next
          Next
        Else
          Console.WriteLine("No related keywords were found for your keyword.")
        End If

        Dim service As TrafficEstimatorService = user.GetService( _
            AdWordsService.v13.TrafficEstimatorService)

        Dim keywordTrafficRequests As New List(Of KeywordTrafficRequest)

        For Each suggestion As Keyword In suggestions
          Dim trafficRequest As New KeywordTrafficRequest
          trafficRequest.keywordText = suggestion.text
          Select Case suggestion.matchType
            Case KeywordMatchType.EXACT
              trafficRequest.keywordType = KeywordType.Exact
              Exit Select
            Case KeywordMatchType.PHRASE
              trafficRequest.keywordType = KeywordType.Phrase
              Exit Select
            Case KeywordMatchType.BROAD
              trafficRequest.keywordType = KeywordType.Broad
              Exit Select
          End Select
          trafficRequest.language = "en_US"
          keywordTrafficRequests.Add(trafficRequest)
        Next

        Dim traffics As KeywordTraffic() = service.checkKeywordTraffic( _
            keywordTrafficRequests.ToArray)

        If (Not traffics Is Nothing) Then
          For i As Integer = 0 To traffics.Length - 1
            Console.WriteLine("Keyword is '{0}' and traffic estimate is '{1}'", _
                suggestions.Item(i).text, traffics(i))
          Next i
        Else
          Console.WriteLine("Could not estimate traffic for keywords.")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to retrieve traffic estimates for related keywords. " & _
            "Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace

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
  ''' This code example gets a bid landscape for an ad group and a keyword.
  ''' To get ad groups, run GetAdGroups.vb. To get keywords, run
  ''' GetKeywords.vb.
  '''
  ''' Tags: DataService.getCriterionBidLandscape
  ''' </summary>
  Class GetKeywordBidSimulations
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As ExampleBase = New GetKeywordBidSimulations
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser(), codeExample.GetParameters(), Console.Out)
    End Sub

    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example gets a bid landscape for an ad group and a keyword. To " & _
            "get ad groups, run GetAdGroups.vb. To get keywords, run GetKeywords.vb."
      End Get
    End Property

    ''' <summary>
    ''' Gets the list of parameter names required to run this code example.
    ''' </summary>
    ''' <returns>
    ''' A list of parameter names for this code example.
    ''' </returns>
    Public Overrides Function GetParameterNames() As String()
      Return New String() {"ADGROUP_ID", "KEYWORD_ID"}
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
      ' Get the DataService.
      Dim dataService As DataService = user.GetService(AdWordsService.v201109.DataService)

      Dim adGroupId As Long = Long.Parse(parameters("ADGROUP_ID"))
      Dim keywordId As Long = Long.Parse(parameters("KEYWORD_ID"))

      ' Create the selector.
      Dim selector As New Selector
      selector.fields = New String() {"AdGroupId", "CriterionId", "StartDate", "EndDate", _
          "Bid", "LocalClicks", "LocalCost", "MarginalCpc", "LocalImpressions"}

      ' Set the filters.
      Dim adGroupPredicate As New Predicate
      adGroupPredicate.field = "AdGroupId"
      adGroupPredicate.operator = PredicateOperator.IN
      adGroupPredicate.values = New String() {adGroupId.ToString}

      Dim keywordPredicate As New Predicate
      keywordPredicate.field = "CriterionId"
      keywordPredicate.operator = PredicateOperator.IN
      keywordPredicate.values = New String() {keywordId.ToString}

      selector.predicates = New Predicate() {adGroupPredicate, keywordPredicate}

      ' Select selector paging.
      selector.paging = New Paging

      Dim offset As Integer = 0
      Dim pageSize As Integer = 500

      Dim page As New CriterionBidLandscapePage

      Try
        Do
          selector.paging.startIndex = offset
          selector.paging.numberResults = pageSize

          ' Get bid landscape for keywords.
          page = dataService.getCriterionBidLandscape(selector)

          ' Display bid landscapes.
          If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
            Dim i As Integer = offset

            For Each bidLandscape As CriterionBidLandscape In page.entries
              writer.WriteLine("{0}) Found keyword bid landscape with ad group id ""{1}"", " & _
                    "keyword id ""{2}"", start date ""{3}"", end date ""{4}"", and " & _
                    "landscape points:", i, bidLandscape.adGroupId, bidLandscape.criterionId, _
                    bidLandscape.startDate, bidLandscape.endDate)
              For Each bidLandscapePoint As BidLandscapeLandscapePoint _
                  In bidLandscape.landscapePoints
                writer.WriteLine("- bid: {0} => clicks: {1}, cost: {2}, marginalCpc: {3}, " & _
                    "impressions: {4}", bidLandscapePoint.bid.microAmount, _
                    bidLandscapePoint.clicks, bidLandscapePoint.cost.microAmount, _
                    bidLandscapePoint.marginalCpc.microAmount, bidLandscapePoint.impressions)
              Next
            Next
            i += 1
          End If
          offset = offset + pageSize
        Loop While (offset < page.totalNumEntries)
        writer.WriteLine("Number of keyword bid landscapes found: {0}", page.totalNumEntries)
      Catch ex As Exception
        writer.WriteLine("Failed to retrieve keyword bid landscapes. Exception says ""{0}""", _
            ex.Message)
      End Try
    End Sub
  End Class
End Namespace

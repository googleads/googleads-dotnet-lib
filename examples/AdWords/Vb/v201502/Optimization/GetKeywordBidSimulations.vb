' Copyright 2015, Google Inc. All Rights Reserved.
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
Imports Google.Api.Ads.AdWords.v201502

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201502
  ''' <summary>
  ''' This code example gets a bid landscape for an ad group and a keyword.
  ''' To get ad groups, run GetAdGroups.vb. To get keywords, run
  ''' GetKeywords.vb.
  '''
  ''' Tags: DataService.getCriterionBidLandscape
  ''' </summary>
  Public Class GetKeywordBidSimulations
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New GetKeywordBidSimulations
      Console.WriteLine(codeExample.Description)
      Try
        Dim adGroupId As Long = Long.Parse("INSERT_ADGROUP_ID_HERE")
        Dim keywordId As Long = Long.Parse("INSERT_KEYWORD_ID_HERE")
        codeExample.Run(New AdWordsUser, adGroupId, keywordId)
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
        Return "This code example gets a bid landscape for an ad group and a keyword. To " & _
            "get ad groups, run GetAdGroups.vb. To get keywords, run GetKeywords.vb."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="adGroupId">Id of the ad group for which keyword bid
    ''' simulations are retrieved.</param>
    ''' <param name="keywordId">Id of the keyword for which bid simulations are
    ''' retrieved.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long, ByVal keywordId As Long)
      ' Get the DataService.
      Dim dataService As DataService = CType(user.GetService( _
          AdWordsService.v201502.DataService), AdWords.v201502.DataService)

      ' Create the selector.
      Dim selector As New Selector
      selector.fields = New String() {"AdGroupId", "CriterionId", "StartDate", "EndDate", _
          "Bid", "LocalClicks", "LocalCost", "LocalImpressions"}

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
              Console.WriteLine("{0}) Found keyword bid landscape with ad group id ""{1}"", " & _
                    "keyword id ""{2}"", start date ""{3}"", end date ""{4}"", and " & _
                    "landscape points:", i, bidLandscape.adGroupId, bidLandscape.criterionId, _
                    bidLandscape.startDate, bidLandscape.endDate)
              For Each bidLandscapePoint As BidLandscapeLandscapePoint _
                  In bidLandscape.landscapePoints
                Console.WriteLine("- bid: {0} => clicks: {1}, cost: {2}, impressions: {3}", _
                    bidLandscapePoint.bid.microAmount, bidLandscapePoint.clicks, _
                    bidLandscapePoint.cost.microAmount, bidLandscapePoint.impressions)
              Next
            Next
            i += 1
          End If
          offset = offset + pageSize
        Loop While (offset < page.totalNumEntries)
        Console.WriteLine("Number of keyword bid landscapes found: {0}", page.totalNumEntries)
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to retrieve keyword bid landscapes.", ex)
      End Try
    End Sub
  End Class
End Namespace

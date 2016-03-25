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
  ''' This code example gets a bid landscape for an ad group and a keyword.
  ''' To get ad groups, run GetAdGroups.vb. To get keywords, run
  ''' GetKeywords.vb.
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
          AdWordsService.v201603.DataService), DataService)

      ' Create the selector.
      Dim selector As New Selector
      selector.fields = New String() {
        CriterionBidLandscape.Fields.AdGroupId, CriterionBidLandscape.Fields.CriterionId,
        CriterionBidLandscape.Fields.StartDate, CriterionBidLandscape.Fields.EndDate,
        BidLandscapeLandscapePoint.Fields.Bid, BidLandscapeLandscapePoint.Fields.LocalClicks,
        BidLandscapeLandscapePoint.Fields.LocalCost,
        BidLandscapeLandscapePoint.Fields.LocalImpressions
      }

      selector.predicates = New Predicate() {
        Predicate.Equals(CriterionBidLandscape.Fields.AdGroupId, adGroupId),
        Predicate.Equals(CriterionBidLandscape.Fields.CriterionId, keywordId)
      }

      ' Select selector paging.
      selector.paging = Paging.Default

      Dim page As New CriterionBidLandscapePage

      Dim landscapePointsFound As Integer = 0
      Dim landscapePointsInLastResponse As Integer = 0

      Try
        Do
          ' Get bid landscape for keywords.
          page = dataService.getCriterionBidLandscape(selector)
          landscapePointsInLastResponse = 0

          ' Display bid landscapes.
          If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
            Dim i As Integer = selector.paging.startIndex

            For Each bidLandscape As CriterionBidLandscape In page.entries
              Console.WriteLine("Found keyword bid landscape with ad group id ""{0}"", " & _
                    "keyword id ""{1}"", start date ""{2}"", end date ""{3}"", and " & _
                    "landscape points:", bidLandscape.adGroupId, _
                    bidLandscape.criterionId, bidLandscape.startDate, bidLandscape.endDate)
              For Each bidLandscapePoint As BidLandscapeLandscapePoint _
                  In bidLandscape.landscapePoints
                Console.WriteLine("- bid: {0} => clicks: {1}, cost: {2}, impressions: {3}", _
                    bidLandscapePoint.bid.microAmount, bidLandscapePoint.clicks, _
                    bidLandscapePoint.cost.microAmount, bidLandscapePoint.impressions)
                landscapePointsInLastResponse += 1
                landscapePointsFound += 1
              Next
            Next
          End If

          ' Offset by the number of landscape points, NOT the number
          ' of entries (bid landscapes) in the last response.
          selector.paging.IncreaseOffsetBy(landscapePointsInLastResponse)
        Loop While (landscapePointsInLastResponse > 0)
        Console.WriteLine("Number of keyword bid landscape points found: {0}", landscapePointsFound)
      Catch e As Exception
        Throw New System.ApplicationException("Failed to retrieve keyword bid landscapes.", e)
      End Try
    End Sub
  End Class
End Namespace

' Copyright 2018 Google LLC
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
Imports Google.Api.Ads.AdWords.Util.Reports.v201809
Imports Google.Api.Ads.AdWords.v201809

Namespace Google.Api.Ads.AdWords.Examples.VB.v201809
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
                Console.WriteLine("An exception occurred while running this code example. {0}",
                                  ExampleUtilities.FormatException(e))
            End Try
        End Sub

        ''' <summary>
        ''' Returns a description about the code example.
        ''' </summary>
        Public Overrides ReadOnly Property Description() As String
            Get
                Return "This code example gets a bid landscape for an ad group and a keyword. To " &
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
            ' [START prepareRequest] MOE:strip_line
            Using dataService As DataService = CType(
                user.GetService(
                    AdWordsService.v201809.DataService),
                DataService)

                ' Create the query.
                Dim query As SelectQuery = New SelectQueryBuilder().Select(
                    CriterionBidLandscape.Fields.AdGroupId,
                    CriterionBidLandscape.Fields.CriterionId,
                    CriterionBidLandscape.Fields.StartDate,
                    CriterionBidLandscape.Fields.EndDate,
                    BidLandscapeLandscapePoint.Fields.Bid,
                    BidLandscapeLandscapePoint.Fields.LocalClicks,
                    BidLandscapeLandscapePoint.Fields.LocalCost,
                    BidLandscapeLandscapePoint.Fields.LocalImpressions,
                    BidLandscapeLandscapePoint.Fields.BiddableConversions,
                    BidLandscapeLandscapePoint.Fields.BiddableConversionsValue
                    ) _
                        .Where(CriterionBidLandscape.Fields.AdGroupId).Equals(adGroupId) _
                        .Where(CriterionBidLandscape.Fields.CriterionId).Equals(keywordId) _
                        .DefaultLimit() _
                        .Build()
                ' [END prepareRequest] MOE:strip_line

                ' [START requestPages] MOE:strip_line
                Dim page As New CriterionBidLandscapePage

                Dim landscapePointsFound As Integer = 0

                Try
                    Do
                        ' Get bid landscape for keywords.
                        ' [START sendRequest] MOE:strip_line
                        page = dataService.queryCriterionBidLandscape(query)
                        ' [END sendRequest] MOE:strip_line

                        ' Display bid landscapes.
                        If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then

                            For Each bidLandscape As CriterionBidLandscape In page.entries
                                Console.WriteLine(
                                    "Found keyword bid landscape with ad group id ""{0}"", " &
                                    "keyword id ""{1}"", start date ""{2}"", end date ""{3}"", " &
                                    "and landscape points:", bidLandscape.adGroupId,
                                    bidLandscape.criterionId, bidLandscape.startDate,
                                    bidLandscape.endDate)
                                For Each bidLandscapePoint As BidLandscapeLandscapePoint _
                                    In bidLandscape.landscapePoints
                                    Console.WriteLine(
                                        "- bid: {0} => clicks: {1}, cost: {2}, impressions: {3}, " &
                                        "biddable conversions: {4:0.00}, biddable conversions " &
                                        "value:{5:0.00}",
                                        bidLandscapePoint.bid.microAmount, bidLandscapePoint.clicks,
                                        bidLandscapePoint.cost.microAmount,
                                        bidLandscapePoint.impressions,
                                        bidLandscapePoint.biddableConversions,
                                        bidLandscapePoint.biddableConversionsValue)
                                    landscapePointsFound += 1
                                Next
                            Next
                        End If
                        query.NextPage(page)
                    Loop While (query.HasNextPage(page))
                    ' [END requestPages] MOE:strip_line
                    Console.WriteLine("Number of keyword bid landscape points found: {0}",
                                      landscapePointsFound)
                Catch e As Exception
                    Throw New _
                        System.ApplicationException("Failed to retrieve keyword bid landscapes.",
                                                    e)
                End Try
            End Using
        End Sub
    End Class
End Namespace

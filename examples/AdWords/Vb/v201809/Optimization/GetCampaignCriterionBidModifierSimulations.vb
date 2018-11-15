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
Imports Google.Api.Ads.AdWords.v201809

Namespace Google.Api.Ads.AdWords.Examples.VB.v201809
    ''' <summary>
    ''' This code example gets all available campaign mobile bid modifier
    ''' landscapes for a given campaign. To get campaigns, run GetCampaigns.cs.
    ''' </summary>
    Public Class GetCampaignCriterionBidModifierSimulations
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New GetCampaignCriterionBidModifierSimulations
            Console.WriteLine(codeExample.Description)
            Try
                Dim campaignId As Long = Long.Parse("INSERT_CAMPAIGN_ID_HERE")
                codeExample.Run(New AdWordsUser, campaignId)
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
                Return _
                    "This code example gets all available campaign mobile bid modifier landscapes" &
                    " for a given campaign. To get campaigns, run GetCampaigns.vb."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="campaignId">Id of the campaign for which bid simulations are
        ''' retrieved.</param>
        Public Sub Run(ByVal user As AdWordsUser, ByVal campaignId As Long)
            ' [START prepareRequest] MOE:strip_line
            Using _
                dataService As DataService =
                    CType(user.GetService(AdWordsService.v201809.DataService), DataService)

                ' Create selector.
                Dim selector As New Selector()
                selector.fields = New String() { _
                                                   CriterionBidLandscape.Fields.CampaignId,
                                                   CriterionBidLandscape.Fields.CriterionId,
                                                   CriterionBidLandscape.Fields.StartDate,
                                                   CriterionBidLandscape.Fields.EndDate,
                                                   BidLandscapeLandscapePoint.Fields.LocalClicks,
                                                   BidLandscapeLandscapePoint.Fields.LocalCost,
                                                   BidLandscapeLandscapePoint.Fields.
                                                       LocalImpressions,
                                                   BidLandscapeLandscapePoint.Fields.
                                                       TotalLocalClicks,
                                                   BidLandscapeLandscapePoint.Fields.TotalLocalCost,
                                                   BidLandscapeLandscapePoint.Fields.
                                                       TotalLocalImpressions,
                                                   BidLandscapeLandscapePoint.Fields.RequiredBudget,
                                                   BidLandscapeLandscapePoint.Fields.BidModifier
                                               }
                selector.predicates =
                    New Predicate() { _
                                        Predicate.Equals(
                                            CriterionBidLandscape.Fields.CampaignId, campaignId)
                                    }
                selector.paging = Paging.Default
                ' [END prepareRequest] MOE:strip_line

                Dim landscapePointsInLastResponse As Integer = 0
                Dim landscapePointsFound As Integer = 0

                Try
                    Dim page As CriterionBidLandscapePage = Nothing

                    Do
                        ' When retrieving bid landscape, page.totalNumEntities cannot be used to
                        ' determine if there are more entries, since it shows only the total number
                        ' of bid landscapes and not the number of bid landscape points. So you need
                        ' to iterate until you no longer get back any bid landscapes.

                        ' Get bid landscape for campaign.
                        ' [START sendRequest] MOE:strip_line
                        page = dataService.getCampaignCriterionBidLandscape(selector)
                        ' [END sendRequest] MOE:strip_line
                        landscapePointsInLastResponse = 0

                        If (Not page Is Nothing) AndAlso (Not page.entries Is Nothing) Then
                            For Each bidLandscape As CriterionBidLandscape In page.entries
                                Console.WriteLine(
                                    "Found campaign-level criterion bid modifier landscapes for" &
                                    " criterion with ID {0}, start date '{1}', end date '{2}', " &
                                    "and landscape points:",
                                    bidLandscape.criterionId,
                                    bidLandscape.startDate,
                                    bidLandscape.endDate
                                    )

                                For Each point As BidLandscapeLandscapePoint In _
                                    bidLandscape.landscapePoints
                                    Console.WriteLine(
                                        "- bid modifier: {0:0.00} => clicks: {1}, cost: {2}, " &
                                        "impressions: {3}, total clicks: {4}, total cost: {5}, " &
                                        "total impressions: {6}, and required budget: {7}",
                                        point.bidModifier, point.clicks, point.cost.microAmount,
                                        point.impressions, point.totalLocalClicks,
                                        point.totalLocalCost.microAmount,
                                        point.totalLocalImpressions,
                                        point.requiredBudget.microAmount)
                                    landscapePointsInLastResponse += 1
                                    landscapePointsFound += 1
                                Next
                            Next
                        End If
                        ' Offset by the number of landscape points, NOT the number
                        ' of entries (bid landscapes) in the last response.
                        selector.paging.IncreaseOffsetBy(landscapePointsInLastResponse)
                    Loop While landscapePointsInLastResponse > 0
                    Console.WriteLine("Number of bid landscape points found: {0}",
                                      landscapePointsFound)
                Catch e As Exception
                    Throw _
                        New System.ApplicationException(
                            "Failed to retrieve campaign bid landscapes.", e)
                End Try
            End Using
        End Sub
    End Class
End Namespace

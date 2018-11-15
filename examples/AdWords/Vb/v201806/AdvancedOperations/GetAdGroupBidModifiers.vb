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
Imports Google.Api.Ads.AdWords.v201806

Namespace Google.Api.Ads.AdWords.Examples.VB.v201806
    ''' <summary>
    ''' This code example illustrates how to retrieve ad group level mobile bid
    ''' modifiers for a campaign.
    '''
    ''' AdGroupBidModifierService.get
    ''' </summary>
    Public Class GetAdGroupBidModifiers
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New GetAdGroupBidModifiers
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
                Return "This code example illustrates how to retrieve ad group level mobile bid" &
                       " modifiers for a campaign."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="campaignId">Id of the campaign for which adgroup bid
        ''' modifiers are retrieved.</param>
        Public Sub Run(ByVal user As AdWordsUser, ByVal campaignId As Long)
            ' Get the AdGroupBidModifierService.
            Using adGroupBidModifierService As AdGroupBidModifierService = CType(
                user.GetService(
                    AdWordsService.v201806.AdGroupBidModifierService),
                AdGroupBidModifierService)

                ' Get all ad group bid modifiers for the campaign.
                Dim selector As New Selector()
                selector.fields = New String() { _
                                                   AdGroupBidModifier.Fields.CampaignId,
                                                   AdGroupBidModifier.Fields.AdGroupId,
                                                   AdGroupBidModifier.Fields.BidModifier,
                                                   AdGroupBidModifier.Fields.BidModifierSource,
                                                   Criterion.Fields.CriteriaType,
                                                   Criterion.Fields.Id
                                               }

                Dim predicate As New Predicate()
                predicate.field = "CampaignId"
                predicate.[operator] = PredicateOperator.EQUALS
                predicate.values = New String() {campaignId.ToString()}
                selector.predicates = New Predicate() { _
                                                          Predicate.Equals(
                                                              AdGroupBidModifier.Fields.CampaignId,
                                                              campaignId)
                                                      }
                selector.paging = Paging.Default

                Dim page As New AdGroupBidModifierPage()

                Try
                    Do
                        ' Get the ad group bids.
                        page = adGroupBidModifierService.get(selector)

                        ' Display the results.
                        If (Not page Is Nothing) AndAlso (Not page.entries Is Nothing) Then
                            Dim i As Integer = selector.paging.startIndex
                            For Each adGroupBidModifier As AdGroupBidModifier In page.entries
                                Dim bidModifier As String = ""
                                If adGroupBidModifier.bidModifierSpecified Then
                                    bidModifier = adGroupBidModifier.bidModifier.ToString()
                                Else
                                    bidModifier = "UNSET"
                                End If
                                Console.WriteLine(
                                    "{0}) Campaign ID {1}, AdGroup ID {2}, Criterion ID {3} has " &
                                    "ad group level modifier: {4}, source = {5}.",
                                    i + 1, adGroupBidModifier.campaignId,
                                    adGroupBidModifier.adGroupId, adGroupBidModifier.criterion.id,
                                    bidModifier, adGroupBidModifier.bidModifierSource)
                                i = i + 1
                            Next
                        End If
                        selector.paging.IncreaseOffset()
                    Loop While selector.paging.startIndex < page.totalNumEntries
                    Console.WriteLine("Number of adgroup bid modifiers found: {0}",
                                      page.totalNumEntries)
                Catch e As Exception
                    Throw _
                        New System.ApplicationException("Failed to retrieve adgroup bid modifiers.",
                                                        e)
                End Try
            End Using
        End Sub
    End Class
End Namespace

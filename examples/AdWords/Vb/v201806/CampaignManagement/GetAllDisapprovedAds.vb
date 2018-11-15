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
    ''' This code example retrieves all the disapproved ads in a given campaign.
    ''' </summary>
    Public Class GetAllDisapprovedAds
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New GetAllDisapprovedAds
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
                Return "This code example retrieves all the disapproved ads in a given campaign."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="campaignId">Id of the campaign for which disapproved ads
        ''' are retrieved.</param>
        Public Sub Run(ByVal user As AdWordsUser, ByVal campaignId As Long)
            Using service As AdGroupAdService = CType(
                user.GetService(
                    AdWordsService.v201806.AdGroupAdService),
                AdGroupAdService)

                ' Create the selector.
                Dim selector As New Selector
                selector.fields = New String() { _
                                                   Ad.Fields.Id, AdGroupAd.Fields.PolicySummary
                                               }

                ' Set the filters.
                selector.predicates =
                    New Predicate() { _
                                        Predicate.Equals(AdGroup.Fields.CampaignId, campaignId),
                                        Predicate.Equals(
                                            AdGroupAdPolicySummary.Fields.CombinedApprovalStatus,
                                            PolicyApprovalStatus.DISAPPROVED.ToString())
                                    }

                ' Set the selector paging.
                selector.paging = Paging.Default

                Dim page As New AdGroupAdPage
                Dim disapprovedAdsCount As Integer = 0

                Try
                    Do
                        ' Get the disapproved ads.
                        page = service.get(selector)

                        ' Display the results.
                        If Not (page Is Nothing) AndAlso Not (page.entries Is Nothing) Then
                            For Each AdGroupAd As AdGroupAd In page.entries
                                Dim policySummary As AdGroupAdPolicySummary =
                                        AdGroupAd.policySummary
                                disapprovedAdsCount += 1
                                Console.WriteLine(
                                    "Ad with ID {0} and type '{1}' was disapproved with the " +
                                    "following policy topic entries: ", AdGroupAd.ad.id,
                                    AdGroupAd.ad.AdType)
                                ' Display the policy topic entries related to the ad disapproval.
                                For Each PolicyTopicEntry As PolicyTopicEntry In _
                                    policySummary.policyTopicEntries
                                    Console.WriteLine("  topic id: {0}, topic name: '{1}'",
                                                      PolicyTopicEntry.policyTopicId,
                                                      PolicyTopicEntry.policyTopicName)
                                    ' Display the attributes And values that triggered the policy 
                                    ' topic.
                                    If Not PolicyTopicEntry.policyTopicEvidences Is Nothing Then
                                        For Each evidence As PolicyTopicEvidence In _
                                            PolicyTopicEntry.policyTopicEvidences
                                            Console.WriteLine("    evidence type: {0}",
                                                              evidence.policyTopicEvidenceType)
                                            If Not evidence.evidenceTextList Is Nothing Then
                                                For i As Integer = 0 To _
                                                    evidence.evidenceTextList.Length
                                                    Console.WriteLine(
                                                        "      evidence text[{0}]: {1}",
                                                        i, evidence.evidenceTextList(i))
                                                Next
                                            End If
                                        Next
                                    End If
                                Next
                            Next
                        End If

                        selector.paging.IncreaseOffset()
                    Loop While selector.paging.startIndex < page.totalNumEntries
                    Console.WriteLine("Number of disapproved ads found: {0}", disapprovedAdsCount)
                Catch e As Exception
                    Throw New System.ApplicationException("Failed to get disapproved ads.", e)
                End Try
            End Using
        End Sub
    End Class
End Namespace

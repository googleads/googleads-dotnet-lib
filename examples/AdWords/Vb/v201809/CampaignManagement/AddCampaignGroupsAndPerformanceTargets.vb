' Copyright 2018 Google LLC
'
' Licensed under the Apache License, Version 2.0 (the "License")
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
    ''' This code example adds a campaign group and sets a performance target for that group. To
    ''' get campaigns, run GetCampaigns.vb. To download reports, run
    ''' DownloadCriteriaReportWithAwql.vb.
    ''' </summary>
    Public Class AddCampaignGroupsAndPerformanceTargets
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New AddCampaignGroupsAndPerformanceTargets
            Console.WriteLine(codeExample.Description)
            Try
                Dim campaignId1 As Long = Long.Parse("INSERT_CAMPAIGN_ID1_HERE")
                Dim campaignId2 As Long = Long.Parse("INSERT_CAMPAIGN_ID2_HERE")

                codeExample.Run(New AdWordsUser, campaignId1, campaignId2)
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
                    "This code example adds a campaign group and sets a performance target for " &
                    "that group. To get campaigns, run GetCampaigns.vb. To download reports, run" &
                    " DownloadCriteriaReportWithAwql.vb."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="campaignId1">Id of the campaign to be added to the campaign group.</param>
        ''' <param name="campaignId2">Id of the campaign to be added to the campaign group.</param>
        Public Sub Run(ByVal user As AdWordsUser, ByVal campaignId1 As Long,
                       ByVal campaignId2 As Long)
            Dim campaignGroup As CampaignGroup = CreateCampaignGroup(user)
            AddCampaignsToGroup(user, campaignGroup.id, New Long() {campaignId1, campaignId2})
            CreatePerformanceTarget(user, campaignGroup.id)
            Console.WriteLine("Campaign group and its performance target were setup successfully.")
        End Sub

        ''' <summary>
        ''' Create a campaign group.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <returns>The campaign group.</returns>
        Private Function CreateCampaignGroup(ByVal user As AdWordsUser) As CampaignGroup
            ' [START createCampaignGroup] MOE:strip_line
            Using campaignGroupService As CampaignGroupService = CType(
                user.GetService(
                    AdWordsService.v201809.CampaignGroupService),
                CampaignGroupService)

                ' Create the campaign group.
                Dim campaignGroup As New CampaignGroup()
                campaignGroup.name = "Mars campaign group - " +
                                     ExampleUtilities.GetShortRandomString()

                ' Create the operation.
                Dim operation As New CampaignGroupOperation()
                operation.operand = campaignGroup
                operation.operator = [Operator].ADD

                Try
                    Dim retval As CampaignGroupReturnValue = campaignGroupService.mutate(
                        New CampaignGroupOperation() {operation})

                    ' Display the results.
                    Dim newCampaignGroup As CampaignGroup = retval.value(0)
                    Console.WriteLine("Campaign group with ID = '{0}' and name = '{1}' was " &
                                      "created.", newCampaignGroup.id, newCampaignGroup.name)
                    Return newCampaignGroup
                Catch e As Exception
                    Throw New System.ApplicationException("Failed to add campaign group.", e)
                End Try
            End Using
            ' [END createCampaignGroup] MOE:strip_line
        End Function

        ''' <summary>
        ''' Adds multiple campaigns to a campaign group.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="campaignGroupId">The campaign group ID.</param>
        ''' <param name="campaignIds">IDs of the campaigns to be added to the campaign group.
        ''' </param>
        Private Sub AddCampaignsToGroup(ByVal user As AdWordsUser, ByVal campaignGroupId As Long,
                                        ByVal campaignIds() As Long)
            ' [START addCampaignsToGroup] MOE:strip_line
            Using campaignService As CampaignService = CType(
                user.GetService(
                    AdWordsService.v201809.CampaignService),
                CampaignService)

                Dim operations As New List(Of CampaignOperation)

                For i As Integer = 0 To campaignIds.Length - 1
                    Dim campaign As New Campaign()
                    campaign.id = campaignIds(i)
                    campaign.campaignGroupId = campaignGroupId

                    Dim operation As New CampaignOperation()
                    operation.operand = campaign
                    operation.operator = [Operator].SET
                    operations.Add(operation)
                Next

                Try
                    Dim retval As CampaignReturnValue = campaignService.mutate(operations.ToArray())

                    Dim updatedCampaignIds As New List(Of Long)()
                    For Each updatedCampaign As Campaign In retval.value
                        updatedCampaignIds.Add(updatedCampaign.id)
                    Next

                    ' Display the results.
                    Console.WriteLine(
                        "The following campaign IDs were added to the campaign group " +
                        "with ID '{0}':\n\t{1}'", campaignGroupId, String.Join(", ", campaignIds))
                Catch e As Exception
                    Throw New _
                        System.ApplicationException("Failed to add campaigns to campaign group.",
                                                    e)
                End Try
            End Using
            ' [END addCampaignsToGroup] MOE:strip_line
        End Sub

        ''' <summary>
        ''' Creates a performance target for the campaign group.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="campaignGroupId">Campaign group ID.</param>
        ''' <returns>The newly created performance target.</returns>
        Private Function CreatePerformanceTarget(ByVal user As AdWordsUser,
                                                 ByVal campaignGroupId As Long) _
            As CampaignGroupPerformanceTarget
            ' [START createPerformanceTarget] MOE:strip_line
            Using campaignGroupPerformanceTargetService As CampaignGroupPerformanceTargetService =
                CType(user.GetService(AdWordsService.v201809.CampaignGroupPerformanceTargetService),
                      CampaignGroupPerformanceTargetService)

                ' Create the performance target.
                Dim campaignGroupPerformanceTarget As New CampaignGroupPerformanceTarget()
                campaignGroupPerformanceTarget.campaignGroupId = campaignGroupId

                Dim performanceTarget As New PerformanceTarget()
                ' Keep the CPC for the campaigns <$3.
                performanceTarget.efficiencyTargetType =
                    EfficiencyTargetType.CPC_LESS_THAN_OR_EQUAL_TO
                performanceTarget.efficiencyTargetValue = 3000000

                ' Keep the maximum spend under $50.
                performanceTarget.spendTargetType = SpendTargetType.MAXIMUM
                Dim maxSpend As New Money()
                maxSpend.microAmount = 500000000
                performanceTarget.spendTarget = maxSpend

                ' Aim for at least 3000 clicks.
                performanceTarget.volumeTargetValue = 3000
                performanceTarget.volumeGoalType = VolumeGoalType.MAXIMIZE_CLICKS

                ' Start the performance target today, And run it for the next 90 days.
                Dim startDate As System.DateTime = System.DateTime.Now
                Dim endDate As System.DateTime = startDate.AddDays(90)

                performanceTarget.startDate = startDate.ToString("yyyyMMdd")
                performanceTarget.endDate = endDate.ToString("yyyyMMdd")

                campaignGroupPerformanceTarget.performanceTarget = performanceTarget

                ' Create the operation.
                Dim operation As New CampaignGroupPerformanceTargetOperation()
                operation.operand = campaignGroupPerformanceTarget
                operation.operator = [Operator].ADD

                Try
                    Dim retval As CampaignGroupPerformanceTargetReturnValue =
                            campaignGroupPerformanceTargetService.mutate(
                                New CampaignGroupPerformanceTargetOperation() {operation})

                    ' Display the results.
                    Dim newCampaignPerfTarget As CampaignGroupPerformanceTarget = retval.value(0)

                    Console.WriteLine("Campaign performance target with id = '{0}' was added for " +
                                      "campaign group ID '{1}'.", newCampaignPerfTarget.id,
                                      newCampaignPerfTarget.campaignGroupId)
                    Return newCampaignPerfTarget
                Catch e As Exception
                    Throw _
                        New System.ApplicationException(
                            "Failed to create campaign performance target.", e)
                End Try
            End Using
        End Function

        ' [END createPerformanceTarget] MOE:strip_line
    End Class
End Namespace

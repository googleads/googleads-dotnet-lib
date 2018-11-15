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
    ''' This code example updates a campaign. To get campaigns, run
    ''' GetCampaigns.vb.
    ''' </summary>
    Public Class UpdateCampaign
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New UpdateCampaign
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
                    "This code example updates a campaign. To get campaigns, run GetCampaigns.vb."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="campaignId">Id of the campaign to be updated.</param>
        Public Sub Run(ByVal user As AdWordsUser, ByVal campaignId As Long)
            Using campaignService As CampaignService = CType(
                user.GetService(
                    AdWordsService.v201806.CampaignService),
                CampaignService)

                ' Create campaign with updated budget.
                Dim campaign As New Campaign
                campaign.id = campaignId
                campaign.status = CampaignStatus.PAUSED

                ' Create the operations.
                Dim operation As New CampaignOperation
                operation.operator = [Operator].SET
                operation.operand = campaign

                Try
                    ' Update the campaign.
                    Dim retVal As CampaignReturnValue = campaignService.mutate(
                        New CampaignOperation() {operation})

                    ' Display the results.
                    If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing) AndAlso
                        (retVal.value.Length > 0)) Then
                        Dim updatedCampaign As Campaign = retVal.value(0)
                        Console.WriteLine("Campaign with name = '{0}' and id = '{1}' was updated.",
                                          updatedCampaign.name, updatedCampaign.id)
                    Else
                        Console.WriteLine("No campaigns were updated.")
                    End If
                Catch e As Exception
                    Throw New System.ApplicationException("Failed to update campaigns.", e)
                End Try
            End Using
        End Sub
    End Class
End Namespace

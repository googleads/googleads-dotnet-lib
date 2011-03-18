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
Imports Google.Api.Ads.AdWords.v201008

Imports System

Namespace Google.Api.Ads.AdWords.Examples.VB.v201008
  ''' <summary>
  ''' This code example adds a campaign. To get campaigns, run GetAllCampaigns.vb.
  '''
  ''' Tags: CampaignService.mutate
  ''' </summary>
  Class AddCampaign
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example adds a campaign. To get campaigns, run GetAllCampaigns.vb."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New AddCampaign
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the CampaignService.
      Dim campaignService As CampaignService = user.GetService( _
          AdWordsService.v201008.CampaignService)

      ' Create campaign.
      Dim campaign As New Campaign
      campaign.name = ("Interplanetary Cruise #" & GetTimeStamp)
      campaign.status = CampaignStatus.PAUSED
      campaign.biddingStrategy = New ManualCPC

      Dim budget As New Budget
      budget.period = BudgetBudgetPeriod.DAILY
      budget.deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD
      budget.amount = New Money
      budget.amount.microAmount = 50000000

      campaign.budget = budget

      ' Create operations.
      Dim operation As New CampaignOperation
      operation.operator = [Operator].ADD
      operation.operand = campaign

      Try
        ' Add campaign.
        Dim retVal As CampaignReturnValue = campaignService.mutate( _
            New CampaignOperation() {operation})

        ' Display campaigns.
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing)) Then
          Dim campaignResult As Campaign
          For Each campaignResult In retVal.value
            Console.WriteLine("Campaign with name = '{0}' and id = '{1}' was added.", _
                campaignResult.name, campaignResult.id)
          Next
        Else
          Console.WriteLine("No campaigns were added.")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to add Campaign. Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace

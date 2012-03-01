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
  ''' This code example adds a campaign. To get campaigns, run GetCampaigns.vb.
  '''
  ''' Tags: CampaignService.mutate
  ''' </summary>
  Class AddCampaign
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As ExampleBase = New AddCampaign
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser(), codeExample.GetParameters(), Console.Out)
    End Sub

    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example adds a campaign. To get campaigns, run GetCampaigns.vb."
      End Get
    End Property

    ''' <summary>
    ''' Gets the list of parameter names required to run this code example.
    ''' </summary>
    ''' <returns>
    ''' A list of parameter names for this code example.
    ''' </returns>
    Public Overrides Function GetParameterNames() As String()
      Return New String() {}
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
      ' Get the CampaignService.
      Dim campaignService As CampaignService = user.GetService( _
          AdWordsService.v201109.CampaignService)

      ' Create the campaign.
      Dim campaign As New Campaign
      campaign.name = "Interplanetary Cruise #" & ExampleUtilities.GetTimeStamp
      campaign.status = CampaignStatus.PAUSED
      campaign.biddingStrategy = New ManualCPC

      ' Set the campaign budget.
      Dim budget As New Budget
      budget.period = BudgetBudgetPeriod.DAILY
      budget.deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD
      budget.amount = New Money
      budget.amount.microAmount = 50000000

      campaign.budget = budget

      ' Set the campaign network options to GoogleSearch and SearchNetwork
      ' only. Set ContentNetwork, PartnerSearchNetwork and ContentContextual
      ' to false.
      campaign.networkSetting = New NetworkSetting
      campaign.networkSetting.targetGoogleSearch = True
      campaign.networkSetting.targetSearchNetwork = True
      campaign.networkSetting.targetContentContextual = False
      campaign.networkSetting.targetContentNetwork = False
      campaign.networkSetting.targetPartnerSearchNetwork = False

      ' Create the operation.
      Dim operation As New CampaignOperation
      operation.operator = [Operator].ADD
      operation.operand = campaign

      Try
        ' Add the campaign.
        Dim retVal As CampaignReturnValue = campaignService.mutate( _
            New CampaignOperation() {operation})

        ' Display the results.
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing) AndAlso _
            (retVal.value.Length > 0)) Then
          Dim newCampaign As Campaign = retVal.value(0)
          writer.WriteLine("Campaign with name = '{0}' and id = '{1}' was added.", _
                newCampaign.name, newCampaign.id)
        Else
          writer.WriteLine("No campaigns were added.")
        End If
      Catch ex As Exception
        writer.WriteLine("Failed to add campaign. Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace

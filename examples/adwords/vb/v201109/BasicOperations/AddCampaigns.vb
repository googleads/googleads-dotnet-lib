' Copyright 2012, Google Inc. All Rights Reserved.
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
  ''' This code example adds campaigns. To get campaigns, run GetCampaigns.vb.
  '''
  ''' Tags: CampaignService.mutate
  ''' </summary>
  Public Class AddCampaigns
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As ExampleBase = New AddCampaigns
      Console.WriteLine(codeExample.Description)
      Try
        codeExample.Run(New AdWordsUser, codeExample.GetParameters, Console.Out)
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
        Return "This code example adds campaigns. To get campaigns, run GetCampaigns.vb."
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
      Dim campaign1 As New Campaign
      campaign1.name = "Interplanetary Cruise #" & ExampleUtilities.GetTimeStamp
      campaign1.status = CampaignStatus.PAUSED
      campaign1.biddingStrategy = New ManualCPC

      ' Set the campaign budget.
      Dim budget1 As New Budget
      budget1.period = BudgetBudgetPeriod.DAILY
      budget1.deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD
      budget1.amount = New Money
      budget1.amount.microAmount = 50000000

      campaign1.budget = budget1

      ' Set the campaign network options.
      campaign1.networkSetting = New NetworkSetting
      campaign1.networkSetting.targetGoogleSearch = True
      campaign1.networkSetting.targetSearchNetwork = True
      campaign1.networkSetting.targetContentContextual = False
      campaign1.networkSetting.targetContentNetwork = False
      campaign1.networkSetting.targetPartnerSearchNetwork = False

      ' Optional: Set the start date.
      campaign1.startDate = DateTime.Now.AddDays(1).ToString("yyyyMMdd")

      ' Optional: Set the end date.
      campaign1.endDate = DateTime.Now.AddYears(1).ToString("yyyyMMdd")

      ' Optional: Set the campaign ad serving optimization status.
      campaign1.adServingOptimizationStatus = AdServingOptimizationStatus.ROTATE

      ' Optional: Set the frequency cap.
      Dim frequencyCap1 As New FrequencyCap
      frequencyCap1.impressions = 5
      frequencyCap1.level = Level.ADGROUP
      frequencyCap1.timeUnit = TimeUnit.DAY
      campaign1.frequencyCap = frequencyCap1

      ' Create the operation.
      Dim operation1 As New CampaignOperation
      operation1.operator = [Operator].ADD
      operation1.operand = campaign1

      ' Create the campaign.
      Dim campaign2 As New Campaign
      campaign2.name = "Interplanetary Cruise #" & ExampleUtilities.GetTimeStamp
      campaign2.status = CampaignStatus.PAUSED
      campaign2.biddingStrategy = New ManualCPC

      ' Set the campaign budget.
      Dim budget2 As New Budget
      budget2.period = BudgetBudgetPeriod.DAILY
      budget2.deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD
      budget2.amount = New Money
      budget2.amount.microAmount = 30000000

      campaign2.budget = budget2

      ' Set the campaign network options.
      campaign2.networkSetting = New NetworkSetting
      campaign2.networkSetting.targetGoogleSearch = True
      campaign2.networkSetting.targetSearchNetwork = True
      campaign2.networkSetting.targetContentContextual = False
      campaign2.networkSetting.targetContentNetwork = False
      campaign2.networkSetting.targetPartnerSearchNetwork = False

      ' Optional: Set the start date.
      campaign2.startDate = DateTime.Now.AddDays(1).ToString("yyyyMMdd")

      ' Optional: Set the end date.
      campaign2.endDate = DateTime.Now.AddYears(1).ToString("yyyyMMdd")

      ' Optional: Set the campaign ad serving optimization status.
      campaign2.adServingOptimizationStatus = AdServingOptimizationStatus.ROTATE

      ' Optional: Set the frequency cap.
      Dim frequencyCap2 As New FrequencyCap
      frequencyCap2.impressions = 5
      frequencyCap2.level = Level.ADGROUP
      frequencyCap2.timeUnit = TimeUnit.DAY
      campaign2.frequencyCap = frequencyCap2

      ' Create the operation.
      Dim operation2 As New CampaignOperation
      operation2.operator = [Operator].ADD
      operation2.operand = campaign2

      Try
        ' Add the campaign.
        Dim retVal As CampaignReturnValue = campaignService.mutate( _
            New CampaignOperation() {operation1, operation2})

        ' Display the results.
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing) AndAlso _
            (retVal.value.Length > 0)) Then
          For Each newCampaign As Campaign In retVal.value
            writer.WriteLine("Campaign with name = '{0}' and id = '{1}' was added.", _
                  newCampaign.name, newCampaign.id)
          Next
        Else
          writer.WriteLine("No campaigns were added.")
        End If
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to add campaigns.", ex)
      End Try
    End Sub
  End Class
End Namespace

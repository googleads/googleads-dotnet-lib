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
  ''' This code example adds campaigns. To get campaigns, run GetCampaigns.vb.
  ''' </summary>
  Public Class AddCampaigns
    Inherits ExampleBase
    ''' <summary>
    ''' Number of items being added / updated in this code example.
    ''' </summary>
    Const NUM_ITEMS As Integer = 5

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New AddCampaigns
      Console.WriteLine(codeExample.Description)
      Try
        codeExample.Run(New AdWordsUser)
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
        Return "This code example adds campaigns. To get campaigns, run GetCampaigns.vb."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    Public Sub Run(ByVal user As AdWordsUser)
      ' Get the BudgetService.
      Dim budgetService As BudgetService = CType(user.GetService( _
          AdWordsService.v201603.BudgetService), BudgetService)

      ' Get the CampaignService.
      Dim campaignService As CampaignService = CType(user.GetService( _
          AdWordsService.v201603.CampaignService), CampaignService)

      ' Create the campaign budget.
      Dim budget As New Budget
      budget.name = "Interplanetary Cruise Budget #" & ExampleUtilities.GetRandomString
      budget.deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD
      budget.amount = New Money
      budget.amount.microAmount = 50000000

      Dim budgetOperation As New BudgetOperation
      budgetOperation.operator = [Operator].ADD
      budgetOperation.operand = budget

      Try
        Dim budgetRetval As BudgetReturnValue = budgetService.mutate(New BudgetOperation() {budgetOperation})
        budget = budgetRetval.value(0)
      Catch e As Exception
        Throw New System.ApplicationException("Failed to add shared budget.", e)
      End Try

      Dim operations As New List(Of CampaignOperation)

      For i As Integer = 1 To NUM_ITEMS
        ' Create the campaign.
        Dim campaign As New Campaign
        campaign.name = "Interplanetary Cruise #" & ExampleUtilities.GetRandomString
        campaign.status = CampaignStatus.PAUSED
        campaign.advertisingChannelType = AdvertisingChannelType.SEARCH

        Dim biddingConfig As New BiddingStrategyConfiguration()
        biddingConfig.biddingStrategyType = BiddingStrategyType.MANUAL_CPC
        campaign.biddingStrategyConfiguration = biddingConfig

        ' Set the campaign budget.
        campaign.budget = New Budget
        campaign.budget.budgetId = budget.budgetId

        ' Set the campaign network options.
        campaign.networkSetting = New NetworkSetting
        campaign.networkSetting.targetGoogleSearch = True
        campaign.networkSetting.targetSearchNetwork = True
        campaign.networkSetting.targetContentNetwork = False
        campaign.networkSetting.targetPartnerSearchNetwork = False

        ' Set the campaign geo target and keyword match settings.
        Dim geoSetting As New GeoTargetTypeSetting
        geoSetting.positiveGeoTargetType = GeoTargetTypeSettingPositiveGeoTargetType.DONT_CARE
        geoSetting.negativeGeoTargetType = GeoTargetTypeSettingNegativeGeoTargetType.DONT_CARE

        campaign.settings = New Setting() {geoSetting}

        ' Optional: Set the start date.
        campaign.startDate = DateTime.Now.AddDays(1).ToString("yyyyMMdd")

        ' Optional: Set the end date.
        campaign.endDate = DateTime.Now.AddYears(1).ToString("yyyyMMdd")

        ' Optional: Set the campaign ad serving optimization status.
        campaign.adServingOptimizationStatus = AdServingOptimizationStatus.ROTATE

        ' Optional: Set the frequency cap.
        Dim frequencyCap As New FrequencyCap
        frequencyCap.impressions = 5
        frequencyCap.level = Level.ADGROUP
        frequencyCap.timeUnit = TimeUnit.DAY
        campaign.frequencyCap = frequencyCap

        ' Create the operation.
        Dim operation As New CampaignOperation
        operation.operator = [Operator].ADD
        operation.operand = campaign
        operations.Add(operation)
      Next

      Try
        ' Add the campaign.
        Dim retVal As CampaignReturnValue = campaignService.mutate(operations.ToArray())

        ' Display the results.
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing) AndAlso _
            (retVal.value.Length > 0)) Then
          For Each newCampaign As Campaign In retVal.value
            Console.WriteLine("Campaign with name = '{0}' and id = '{1}' was added.", _
                  newCampaign.name, newCampaign.id)
          Next
        Else
          Console.WriteLine("No campaigns were added.")
        End If
      Catch e As Exception
        Throw New System.ApplicationException("Failed to add campaigns.", e)
      End Try
    End Sub
  End Class
End Namespace

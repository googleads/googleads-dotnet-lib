' Copyright 2014, Google Inc. All Rights Reserved.
'
' Licensed under the Apache License, Version 2.0 (the "License")
' you may not use this file except in compliance with the License.
' You may obtain a copy of the License at
'
'     http:'www.apache.org/licenses/LICENSE-2.0
'
' Unless required by applicable law or agreed to in writing, software
' distributed under the License is distributed on an "AS IS" BASIS,
' WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
' See the License for the specific language governing permissions and
' limitations under the License.

' Author: api.anash@gmail.com (Anash P. Oommen)

Imports Google.Api.Ads.AdWords.Lib
Imports Google.Api.Ads.AdWords.v201402

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201402
  ''' <summary>
  ''' This code example adds a Shared Bidding Strategy and uses it to construct
  ''' a campaign.
  '''
  ''' Tags: BiddingStrategyService.mutate
  ''' Tags: BudgetService.mutate, CampaignService.mutate
  ''' </summary>
  Public Class UseSharedBiddingStrategy
    Inherits ExampleBase

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New UseSharedBiddingStrategy
      Console.WriteLine(codeExample.Description)
      Try
        codeExample.Run(New AdWordsUser)
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
        Return "This code example adds a Shared Bidding Strategy and uses it to construct " & _
            "a campaign."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    Public Sub Run(ByVal user As AdWordsUser)
      ' Get the services.
      Dim biddingStrategyService As BiddingStrategyService = CType(user.GetService( _
          AdWordsService.v201402.BiddingStrategyService), BiddingStrategyService)

      Dim budgetService As BudgetService = CType(user.GetService( _
          AdWordsService.v201402.BudgetService), BudgetService)

      Dim campaignService As CampaignService = CType(user.GetService( _
          AdWordsService.v201402.CampaignService), CampaignService)

      Dim biddingStrategyName As String = "Maximize Clicks " & ExampleUtilities.GetRandomString()
      Dim bidCeiling As Long = 2000000
      Dim spendTarget As Long = 20000000

      Dim budgetName As String = "Shared Interplanetary Budget #" & _
          ExampleUtilities.GetRandomString()
      Dim budgetAmount As Long = 30000000

      Dim campaignName As String = "Interplanetary Cruise #" + ExampleUtilities.GetRandomString()

      Try
        Dim sharedBiddingStrategy As SharedBiddingStrategy = CreateBiddingStrategy( _
            biddingStrategyService, biddingStrategyName, bidCeiling, spendTarget)
        Console.WriteLine("Shared bidding strategy with name '{0}' and ID {1} of type {2} " & _
            "was created.", sharedBiddingStrategy.name, sharedBiddingStrategy.id, _
            sharedBiddingStrategy.biddingScheme.BiddingSchemeType)

        Dim sharedBudget As Budget = CreateSharedBudget(budgetService, budgetName, budgetAmount)

        Dim newCampaign As Campaign = CreateCampaignWithBiddingStrategy(campaignService, _
            campaignName, sharedBiddingStrategy.id, sharedBudget.budgetId)

        Console.WriteLine("Campaign with name '{0}', ID {1} and bidding scheme ID {2} was " & _
            "created.", newCampaign.name, newCampaign.id, _
            newCampaign.biddingStrategyConfiguration.biddingStrategyId)

      Catch ex As Exception
        Throw New System.ApplicationException("Failed to create campaign that uses shared " & _
            "bidding strategy.", ex)
      End Try
    End Sub


    ''' <summary>
    ''' Creates the shared bidding strategy.
    ''' </summary>
    ''' <param name="biddingStrategyService">The bidding strategy service.</param>
    ''' <param name="name">The bidding strategy name.</param>
    ''' <param name="bidCeiling">The bid ceiling.</param>
    ''' <param name="spendTarget">The spend target.</param>
    ''' <returns>The bidding strategy object.</returns>
    Private Function CreateBiddingStrategy(ByVal biddingStrategyService As BiddingStrategyService, _
        ByVal name As String, ByVal bidCeiling As Long, ByVal spendTarget As Long) _
        As SharedBiddingStrategy
      ' Create a shared bidding strategy.
      Dim sharedBiddingStrategy As New SharedBiddingStrategy()
      sharedBiddingStrategy.name = name

      Dim biddingScheme As New TargetSpendBiddingScheme()
      ' Optionally set additional bidding scheme parameters.
      biddingScheme.bidCeiling = New Money()
      biddingScheme.bidCeiling.microAmount = bidCeiling

      biddingScheme.spendTarget = New Money()
      biddingScheme.spendTarget.microAmount = spendTarget

      sharedBiddingStrategy.biddingScheme = biddingScheme

      ' Create operation.
      Dim operation As New BiddingStrategyOperation()
      operation.operator = [Operator].ADD
      operation.operand = sharedBiddingStrategy

      Return biddingStrategyService.mutate(New BiddingStrategyOperation() {operation}).value(0)
    End Function

    ''' <summary>
    ''' Creates an explicit budget to be used only to create the Campaign.
    ''' </summary>
    ''' <param name="budgetService">The budget service.</param>
    ''' <param name="name">The budget name.</param>
    ''' <param name="amount">The budget amount.</param>
    ''' <returns>The budget object.</returns>
    Private Function CreateSharedBudget(ByVal budgetService As BudgetService, _
        ByVal name As String, ByVal amount As Long) As Budget
      ' Create a shared budget
      Dim budget As New Budget()
      budget.name = name
      budget.period = BudgetBudgetPeriod.DAILY
      budget.amount = New Money()
      budget.amount.microAmount = amount
      budget.deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD
      budget.isExplicitlyShared = True

      ' Create operation.
      Dim operation As New BudgetOperation()
      operation.operand = budget
      operation.operator = [Operator].ADD

      ' Make the mutate request.
      Return budgetService.mutate(New BudgetOperation() {operation}).value(0)
    End Function

    ''' <summary>
    ''' Creates the campaign with a shared bidding strategy.
    ''' </summary>
    ''' <param name="campaignService">The campaign service.</param>
    ''' <param name="name">The campaign name.</param>
    ''' <param name="biddingStrategyId">The bidding strategy id.</param>
    ''' <param name="sharedBudgetId">The shared budget id.</param>
    ''' <returns>The campaign object.</returns>
    Private Function CreateCampaignWithBiddingStrategy(ByVal campaignService As CampaignService, _
        ByVal name As String, ByVal biddingStrategyId As Long, ByVal sharedBudgetId As Long) _
        As Campaign
      ' Create campaign.
      Dim campaign As New Campaign()
      campaign.name = name
      campaign.advertisingChannelType = AdvertisingChannelType.SEARCH

      ' Set the budget.
      campaign.budget = New Budget()
      campaign.budget.budgetId = sharedBudgetId

      ' Set bidding strategy (required).
      Dim biddingStrategyConfiguration As New BiddingStrategyConfiguration()
      biddingStrategyConfiguration.biddingStrategyId = biddingStrategyId

      campaign.biddingStrategyConfiguration = biddingStrategyConfiguration

      ' Set keyword matching setting (required).
      Dim keywordMatchSetting As New KeywordMatchSetting()
      keywordMatchSetting.optIn = True
      campaign.settings = New Setting() {keywordMatchSetting}

      ' Set network targeting (recommended).
      Dim networkSetting As New NetworkSetting()
      networkSetting.targetGoogleSearch = True
      networkSetting.targetSearchNetwork = True
      networkSetting.targetContentNetwork = True
      campaign.networkSetting = networkSetting

      ' Create operation.
      Dim operation As New CampaignOperation()
      operation.operand = campaign
      operation.operator = [Operator].ADD

      Return campaignService.mutate(New CampaignOperation() {operation}).value(0)
    End Function
  End Class
End Namespace

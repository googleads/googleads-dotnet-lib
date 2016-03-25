' Copyright 2015, Google Inc. All Rights Reserved.
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

Imports Google.Api.Ads.AdWords.Lib
Imports Google.Api.Ads.AdWords.v201509

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201509
  ''' <summary>
  ''' This code example adds a portfolio bidding strategy and uses it to
  ''' construct a campaign.
  ''' </summary>
  Public Class UsePortfolioBiddingStrategy
    Inherits ExampleBase

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New UsePortfolioBiddingStrategy
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
        Return "This code example adds a portfolio bidding strategy and uses it to construct " & _
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
          AdWordsService.v201509.BiddingStrategyService), BiddingStrategyService)

      Dim budgetService As BudgetService = CType(user.GetService( _
          AdWordsService.v201509.BudgetService), BudgetService)

      Dim campaignService As CampaignService = CType(user.GetService( _
          AdWordsService.v201509.CampaignService), CampaignService)

      Dim biddingStrategyName As String = "Maximize Clicks " & ExampleUtilities.GetRandomString()
      Dim bidCeiling As Long = 2000000
      Dim spendTarget As Long = 20000000

      Dim budgetName As String = "Shared Interplanetary Budget #" & _
          ExampleUtilities.GetRandomString()
      Dim budgetAmount As Long = 30000000

      Dim campaignName As String = "Interplanetary Cruise #" + ExampleUtilities.GetRandomString()

      Try
        Dim portfolioBiddingStrategy As SharedBiddingStrategy = CreateBiddingStrategy( _
            biddingStrategyService, biddingStrategyName, bidCeiling, spendTarget)
        Console.WriteLine("Portfolio bidding strategy with name '{0}' and ID {1} of type {2} " & _
            "was created.", portfolioBiddingStrategy.name, portfolioBiddingStrategy.id, _
            portfolioBiddingStrategy.biddingScheme.BiddingSchemeType)

        Dim sharedBudget As Budget = CreateSharedBudget(budgetService, budgetName, budgetAmount)

        Dim newCampaign As Campaign = CreateCampaignWithBiddingStrategy(campaignService, _
            campaignName, portfolioBiddingStrategy.id, sharedBudget.budgetId)

        Console.WriteLine("Campaign with name '{0}', ID {1} and bidding scheme ID {2} was " & _
            "created.", newCampaign.name, newCampaign.id, _
            newCampaign.biddingStrategyConfiguration.biddingStrategyId)

      Catch e As Exception
        Throw New System.ApplicationException("Failed to create campaign that uses portfolio " & _
            "bidding strategy.", e)
      End Try
    End Sub


    ''' <summary>
    ''' Creates the portfolio bidding strategy.
    ''' </summary>
    ''' <param name="biddingStrategyService">The bidding strategy service.</param>
    ''' <param name="name">The bidding strategy name.</param>
    ''' <param name="bidCeiling">The bid ceiling.</param>
    ''' <param name="spendTarget">The spend target.</param>
    ''' <returns>The bidding strategy object.</returns>
    Private Function CreateBiddingStrategy(ByVal biddingStrategyService As BiddingStrategyService, _
        ByVal name As String, ByVal bidCeiling As Long, ByVal spendTarget As Long) _
        As SharedBiddingStrategy
      ' Create a portfolio bidding strategy.
      Dim portfolioBiddingStrategy As New SharedBiddingStrategy()
      portfolioBiddingStrategy.name = name

      Dim biddingScheme As New TargetSpendBiddingScheme()
      ' Optionally set additional bidding scheme parameters.
      biddingScheme.bidCeiling = New Money()
      biddingScheme.bidCeiling.microAmount = bidCeiling

      biddingScheme.spendTarget = New Money()
      biddingScheme.spendTarget.microAmount = spendTarget

      portfolioBiddingStrategy.biddingScheme = biddingScheme

      ' Create operation.
      Dim operation As New BiddingStrategyOperation()
      operation.operator = [Operator].ADD
      operation.operand = portfolioBiddingStrategy

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
    ''' Creates the campaign with a portfolio bidding strategy.
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

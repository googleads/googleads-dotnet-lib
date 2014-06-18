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
  Public Class UploadOfflineConversions
    Inherits ExampleBase

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim conversionName As String = "INSERT_CONVERSION_NAME_HERE"
      ' GCLID needs to be newer than 30 days.
      Dim gClId As String = "INSERT_GOOGLE_CLICK_ID_HERE"
      '  The conversion time should be higher than the click time.
      Dim conversionTime As String = "INSERT_CONVERSION_TIME_HERE"
      Dim conversionValue As Double = Double.Parse("INSERT_CONVERSION_VALUE_HERE")

      Dim codeExample As New UploadOfflineConversions
      Console.WriteLine(codeExample.Description)
      Try
        codeExample.Run(New AdWordsUser, conversionName, gClId, conversionTime, conversionValue)
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
    ''' <param name="conversionName">The name of the upload conversion to be
    ''' created.</param>
    ''' <param name="gClid">The Google Click ID of the click for which offline
    ''' conversions are uploaded.</param>
    ''' <param name="conversionValue">The conversion value to be uploaded.
    ''' </param>
    ''' <param name="conversionTime">The conversion time, in yyyymmdd hhmmss
    ''' format.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal conversionName As String, _
        ByVal gClid As String, ByVal conversionTime As String, ByVal conversionValue As Double)
      ' Get the ConversionTrackerService.
      Dim conversionTrackerService As ConversionTrackerService = user.GetService( _
          AdWordsService.v201402.ConversionTrackerService)

      ' Get the OfflineConversionFeedService.
      Dim offlineConversionFeedService As OfflineConversionFeedService = user.GetService( _
              AdWordsService.v201402.OfflineConversionFeedService)

      Const VIEWTHROUGH_LOOKBACK_WINDOW As Integer = 30
      Const CTC_LOOKBACK_WINDOW As Integer = 90

      Try
        ' Create an upload conversion. Once created, this entry will be visible
        ' under Tools and Analysis->Conversion and will have "Source = Import".
        Dim uploadConversion As New UploadConversion()
        uploadConversion.category = ConversionTrackerCategory.PAGE_VIEW
        uploadConversion.name = conversionName
        uploadConversion.viewthroughLookbackWindow = VIEWTHROUGH_LOOKBACK_WINDOW
        uploadConversion.ctcLookbackWindow = CTC_LOOKBACK_WINDOW

        Dim uploadConversionOperation As New ConversionTrackerOperation()
        uploadConversionOperation.operator = [Operator].ADD
        uploadConversionOperation.operand = uploadConversion

        Dim conversionTrackerRetval As ConversionTrackerReturnValue = _
            conversionTrackerService.mutate(New ConversionTrackerOperation() { _
                uploadConversionOperation})

        Dim newUploadConversion As UploadConversion = conversionTrackerRetval.value(0)

        Console.WriteLine("New upload conversion type with name = '{0}' and id = {1} was " & _
            "created.", newUploadConversion.name, newUploadConversion.id)

        ' Associate offline conversions with the upload conversion we created.
        Dim feed As New OfflineConversionFeed()
        feed.conversionName = conversionName
        feed.conversionTime = conversionTime
        feed.conversionValue = conversionValue
        feed.googleClickId = gClid

        Dim offlineConversionOperation As New OfflineConversionFeedOperation()
        offlineConversionOperation.operator = [Operator].ADD
        offlineConversionOperation.operand = feed

        Dim offlineConversionRetval As OfflineConversionFeedReturnValue = _
            offlineConversionFeedService.mutate( _
                New OfflineConversionFeedOperation() {offlineConversionOperation})

        Dim newFeed As OfflineConversionFeed = offlineConversionRetval.value(0)

        Console.WriteLine("Uploaded offline conversion value of {0} for Google Click ID = " & _
            "'{1}' to '{2}'.", newFeed.conversionValue, newFeed.googleClickId, _
            newFeed.conversionName)
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to upload offline conversions.", ex)
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

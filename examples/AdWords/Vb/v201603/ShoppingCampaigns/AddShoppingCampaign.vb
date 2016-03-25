' Copyright 2016, Google Inc. All Rights Reserved.
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
Imports Google.Api.Ads.AdWords.v201603

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201603
  ''' <summary>
  ''' This code example adds a Shopping campaign.
  ''' </summary>
  Public Class AddShoppingCampaign
    Inherits ExampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example adds a Shopping campaign."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New AddShoppingCampaign
      Console.WriteLine(codeExample.Description)
      Try
        Dim budgetId As Long = Long.Parse("INSERT_BUDGET_ID_HERE")
        Dim merchantId As Long = Long.Parse("INSERT_MERCHANT_ID_HERE")
        codeExample.Run(New AdWordsUser, budgetId, merchantId)
      Catch e As Exception
        Console.WriteLine("An exception occurred while running this code example. {0}", _
            ExampleUtilities.FormatException(e))
      End Try
    End Sub

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="budgetId">The budget id.</param>
    ''' <param name="merchantId">The Merchant Center account id.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal budgetId As Long, ByVal merchantId As Long)
      ' Get the required services.
      Dim campaignService As CampaignService = CType(user.GetService( _
          AdWordsService.v201603.CampaignService), CampaignService)
      Dim adGroupService As AdGroupService = CType(user.GetService( _
          AdWordsService.v201603.AdGroupService), AdGroupService)
      Dim adGroupAdService As AdGroupAdService = CType(user.GetService( _
          AdWordsService.v201603.AdGroupAdService), AdGroupAdService)

      Try
        Dim campaign As Campaign = CreateCampaign(budgetId, merchantId, campaignService)
        Console.WriteLine("Campaign with name '{0}' and ID '{1}' was added.", campaign.name, _
              campaign.id)

        Dim adGroup As AdGroup = CreateAdGroup(adGroupService, campaign)
        Console.WriteLine("Ad group with name '{0}' and ID '{1}' was added.", adGroup.name, _
              adGroup.id)

        Dim adGroupAd As AdGroupAd = CreateProductAd(adGroupAdService, adGroup)
        Console.WriteLine("Product ad with ID {0}' was added.", adGroupAd.ad.id)
      Catch e As Exception
        Throw New System.ApplicationException("Failed to create shopping campaign.", e)
      End Try
    End Sub

    ''' <summary>
    ''' Creates the Product Ad.
    ''' </summary>
    ''' <param name="adGroupAdService">The AdGroupAdService instance.</param>
    ''' <param name="adGroup">The ad group.</param>
    ''' <returns>The Product Ad.</returns>
    Private Function CreateProductAd(ByVal adGroupAdService As AdGroupAdService, _
                                     ByVal adGroup As AdGroup) As AdGroupAd
      ' Create product ad.
      Dim productAd As New ProductAd()

      ' Create ad group ad.
      Dim adGroupAd As New AdGroupAd()
      adGroupAd.adGroupId = adGroup.id
      adGroupAd.ad = productAd

      ' Create operation.
      Dim operation As New AdGroupAdOperation()
      operation.operand = adGroupAd
      operation.operator = [Operator].ADD

      ' Make the mutate request.
      Dim retval As AdGroupAdReturnValue = adGroupAdService.mutate( _
          New AdGroupAdOperation() {operation})

      Return retval.value(0)
    End Function

    ''' <summary>
    ''' Creates the ad group in a Shopping campaign.
    ''' </summary>
    ''' <param name="adGroupService">The AdGroupService instance.</param>
    ''' <param name="campaign">The Shopping campaign.</param>
    ''' <returns>The ad group.</returns>
    Private Function CreateAdGroup(ByVal adGroupService As AdGroupService, _
                                   ByVal campaign As Campaign) As AdGroup
      ' Create ad group.
      Dim adGroup As New AdGroup()
      adGroup.campaignId = campaign.id
      adGroup.name = "Ad Group #" & ExampleUtilities.GetRandomString()

      ' Create operation.
      Dim operation As New AdGroupOperation()
      operation.operand = adGroup
      operation.operator = [Operator].ADD

      ' Make the mutate request.
      Dim retval As AdGroupReturnValue = adGroupService.mutate( _
          New AdGroupOperation() {operation})
      Return retval.value(0)
    End Function

    ''' <summary>
    ''' Creates the shopping campaign.
    ''' </summary>
    ''' <param name="budgetId">The budget id.</param>
    ''' <param name="merchantId">The Merchant Center id.</param>
    ''' <param name="campaignService">The CampaignService instance.</param>
    ''' <returns>The Shopping campaign.</returns>
    Private Function CreateCampaign(ByVal budgetId As Long, ByVal merchantId As Long, _
                                    ByVal campaignService As CampaignService) As Campaign
      ' Create campaign.
      Dim campaign As New Campaign()
      campaign.name = "Shopping campaign #" & ExampleUtilities.GetRandomString()
      ' The advertisingChannelType is what makes this a Shopping campaign.
      campaign.advertisingChannelType = AdvertisingChannelType.SHOPPING

      ' Set shared budget (required).
      campaign.budget = New Budget()
      campaign.budget.budgetId = budgetId

      ' Set bidding strategy (required).
      Dim biddingStrategyConfiguration As New BiddingStrategyConfiguration()
      biddingStrategyConfiguration.biddingStrategyType = BiddingStrategyType.MANUAL_CPC

      campaign.biddingStrategyConfiguration = biddingStrategyConfiguration

      ' All Shopping campaigns need a ShoppingSetting.
      Dim shoppingSetting As New ShoppingSetting()
      shoppingSetting.salesCountry = "US"
      shoppingSetting.campaignPriority = 0
      shoppingSetting.merchantId = merchantId

      ' Enable Local Inventory Ads in your campaign.
      shoppingSetting.enableLocal = True
      campaign.settings = New Setting() {shoppingSetting}

      ' Create operation.
      Dim campaignOperation As New CampaignOperation()
      campaignOperation.operand = campaign
      campaignOperation.operator = [Operator].ADD

      ' Make the mutate request.
      Dim retval As CampaignReturnValue = campaignService.mutate( _
          New CampaignOperation() {campaignOperation})

      Return retval.value(0)
    End Function
  End Class

End Namespace

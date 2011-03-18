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
Imports Google.Api.Ads.AdWords.v201101

Imports System

Namespace Google.Api.Ads.AdWords.Examples.VB.v201101
  ''' <summary>
  ''' This code example shows how to use the validateOnly header to validate
  ''' an API request. No objects will be created, but exceptions will
  ''' still be thrown.
  '''
  ''' Tags: CampaignService.mutate
  ''' </summary>
  Class CheckCampaigns
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example shows how to use the validateOnly header to validate an " & _
            "API request. No objects will be created, but exceptions will still be thrown."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New CheckCampaigns
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
          AdWordsService.v201101.CampaignService)

      ' Turn on the validation mode.
      campaignService.RequestHeader.validateOnly = True

      ' Create the good campaign.
      Dim goodCampaign As New Campaign
      goodCampaign.name = ("Campaign #" & GetTimeStamp)
      goodCampaign.status = CampaignStatus.PAUSED
      goodCampaign.biddingStrategy = New ManualCPC

      Dim budget As New Budget
      budget.deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD
      budget.period = BudgetBudgetPeriod.DAILY
      budget.amount = New Money
      budget.amount.microAmount = 50000000

      goodCampaign.budget = budget

      Dim operation As New CampaignOperation
      operation.operator = [Operator].ADD
      operation.operand = goodCampaign

      Try
        Dim retVal As CampaignReturnValue = campaignService.mutate( _
            New CampaignOperation() {operation})

        ' Since validation is ON, result will be null.
        Console.WriteLine("Campaign request validated successfully.")
      Catch ex As AdWordsApiException
        ' This block will be hit if there is a validation error from the server.
        Console.WriteLine("There were validation error(s) while adding campaigns.")
        If (Not ex.ApiException Is Nothing) Then
          For Each apiError As ApiError In DirectCast(ex.ApiException, ApiException).errors
            Console.WriteLine("  Error type is '{0}' and fieldPath is '{1}'.", _
                apiError.ApiErrorType, apiError.fieldPath)
          Next
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to validate campaign. Exception says ""{0}""", ex.Message)
      End Try

      ' Create the bad campaign.
      Dim badCampaign As New Campaign
      badCampaign.name = ("Campaign #" & GetTimeStamp)
      badCampaign.status = CampaignStatus.PAUSED
      badCampaign.budget = budget

      ' Provide an invalid bidding strategy that will cause an exception
      ' during validation. The error thrown is
      ' RequiredError.REQUIRED @ operations[0].operand.biddingStrategy.
      badCampaign.biddingStrategy = Nothing

      operation = New CampaignOperation
      operation.operator = [Operator].ADD
      operation.operand = badCampaign

      Try
        Dim retVal As CampaignReturnValue = campaignService.mutate( _
            New CampaignOperation() {operation})
        ' Since we have purposefully added a validation error, the next
        ' line won't be hit.
        Console.WriteLine("Campaign request validated successfully.")
      Catch ex As AdWordsApiException
        ' This block will be hit if there is a validation error from the server.
        Console.WriteLine("There were validation error(s) while adding campaigns.")
        If (Not ex.ApiException Is Nothing) Then
          For Each apiError As ApiError In DirectCast(ex.ApiException, ApiException).errors
            Console.WriteLine("  Error type is '{0}' and fieldPath is '{1}'.", _
                apiError.ApiErrorType, apiError.fieldPath)
          Next
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to validate campaign. Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace

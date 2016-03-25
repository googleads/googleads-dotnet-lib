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
  ''' This code example illustrates how to graduate a trial. See the Campaign
  ''' Drafts and Experiments guide for more information: 
  ''' https://developers.google.com/adwords/api/docs/guides/campaign-drafts-experiments
  ''' </summary>
  Public Class GraduateTrial
    Inherits ExampleBase
    
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New GraduateTrial
      Console.WriteLine(codeExample.Description)
      Try
        Dim trialId as Long = Long.Parse("INSERT_TRIAL_ID_HERE")
        codeExample.Run(new AdWordsUser(), trialId)
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
        return "This code example illustrates how to graduate a trial. See the Campaign " & _
            "Drafts and Experiments guide for more information: " & _
            "https://developers.google.com/adwords/api/docs/guides/campaign-drafts-experiments"
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="trialId">Id of the trial to be graduated.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal trialId As Long)
      ' Get the TrialService and BudgetService.
      Dim trialService as TrialService = CType(user.GetService(
          AdWordsService.v201603.TrialService), TrialService)
      Dim budgetService as BudgetService = CType(user.GetService(
          AdWordsService.v201603.BudgetService), BudgetService)

      Try
        ' To graduate a trial, you must specify a different budget from the
        ' base campaign. The base campaign (in order to have had a trial based
        ' on it) must have a non-shared budget, so it cannot be shared with
        ' the new independent campaign created by graduation.
        Dim budget As new Budget()
        budget.name = "Budget #" + ExampleUtilities.GetRandomString()
        budget.amount = new Money()
        budget.amount.microAmount = 50000000L
        budget.deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD

        Dim budgetOperation As new BudgetOperation()
        budgetOperation.operator = [Operator].ADD
        budgetOperation.operand = budget

        ' Add budget.
        Dim budgetId as Long = budgetService.mutate(
            new BudgetOperation() {budgetOperation}).value(0).budgetId

        Dim trial As new Trial()
        trial.id = trialId
        trial.budgetId = budgetId
        trial.status = TrialStatus.GRADUATED

        Dim trialOperation As new TrialOperation()
        trialOperation.operator = [Operator].SET
        trialOperation.operand = trial
 
        ' Update the trial.
        trial = trialService.mutate(new TrialOperation() {trialOperation}).value(0)

        ' Graduation is a synchronous operation, so the campaign is already
        ' ready. If you promote instead, make sure to see the polling scheme
        ' demonstrated in AddTrial.cs to wait for the asynchronous operation
        ' to finish.
        Console.WriteLine("Trial ID {0} graduated. Campaign ID {1} was given a new budget " & _
            "ID {1} and is no Longer dependent on this trial.", trial.id, trial.trialCampaignId,
            budgetId)
      Catch e As Exception
        Throw New System.ApplicationException("Failed to graduate trial.", e)
      End Try
    End Sub
  End Class
End Namespace

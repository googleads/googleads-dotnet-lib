' Copyright 2016, Google Inc. All Rights Reserved.
'
' Licensed under the Apache License, Version 2.0 (the "License");
' you may not use this file except in compliance with the License.
' You may obtain a copy of the License at
'
'     http://www.apache.org/licenses/LICENSE-2.0
'
' Unless required by applicable law or agreed to in writing, software
' distributed under the License is distributed on an "As IS" BASIS,
' WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
' See the License for the specific language governing permissions and
' limitations under the License.

Imports Google.Api.Ads.AdWords.Lib
Imports Google.Api.Ads.AdWords.v201603

Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Threading

Namespace Google.Api.Ads.AdWords.Examples.VB.v201603
  ''' <summary>
  ''' This code example illustrates how to create a trial and wait for it to
  ''' complete. See the Campaign Drafts and Experiments guide for more
  ''' information:
  ''' https://developers.google.com/adwords/api/docs/guides/campaign-drafts-experiments
  ''' </summary>
  Public Class AddTrial
    Inherits ExampleBase

    ''' <summary>
    ''' The polling interval base to be used for exponential backoff.
    ''' </summary>
    Private const POLL_INTERVAL_SECONDS_BASE As Integer = 30

    ''' <summary>
    ''' The maximum number of retries.
    ''' </summary>
    Private const MAX_RETRIES As Long = 5
        
    ''' <summary>
    ''' Main method, to run this code example As a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New AddTrial
      Console.WriteLine(codeExample.Description)
      Try
        Dim draftId As Long = Long.Parse("INSERT_DRAFT_ID_HERE")
        Dim baseCampaignId As Long = Long.Parse("INSERT_BASE_CAMPAIGN_ID_HERE")
        codeExample.Run(New AdWordsUser(), draftId, baseCampaignId)
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
        return "This code example illustrates how to create a trial and wait for it to " & _
            "complete. See the Campaign Drafts and Experiments guide for more information: " & _
            "https://developers.google.com/adwords/api/docs/guides/campaign-drafts-experiments"
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="baseCampaignId">Id of the campaign to use as base of the
    ''' trial.</param>
    ''' <param name="draftId">Id of the draft.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal draftId as Long, ByVal baseCampaignId As Long)
      ' Get the TrialService.
      Dim trialService As TrialService = CType(user.GetService(
        AdWordsService.v201603.TrialService), TrialService)

      Dim newTrial As New Trial
      newTrial.draftId = draftId
      newTrial.baseCampaignId = baseCampaignId
      newTrial.name = "Test Trial #" & ExampleUtilities.GetRandomString()
      newTrial.trafficSplitPercent = 50

      Dim trialOperation As New TrialOperation()
      trialOperation.operator = [Operator].ADD
      trialOperation.operand = newTrial

      Try
        Dim trialId As Long = trialService.mutate(
            New TrialOperation() { trialOperation }).value(0).id

        ' Since creating a trial is asynchronous, we have to poll it to wait
        ' for it to finish.
        Dim trialSelector As New Selector()
        trialSelector.fields = New string() {
            Trial.Fields.Id, Trial.Fields.Status, Trial.Fields.BaseCampaignId,
            Trial.Fields.TrialCampaignId
        }
        trialSelector.predicates = New Predicate() {
            Predicate.Equals(Trial.Fields.Id, trialId)
        }
        newTrial = Nothing
        Dim isPending As Boolean = true
        Dim pollAttempts As Integer = 0

        Do
          Dim sleepMillis As Integer = CType(Math.Pow(2, pollAttempts) * _
              POLL_INTERVAL_SECONDS_BASE * 1000, Integer)
          Console.WriteLine("Sleeping {0} millis...", sleepMillis)
          Thread.Sleep(sleepMillis)

          newTrial = trialService.get(trialSelector).entries(0)

          Console.WriteLine("Trial ID {0} has status '{1}'.", newTrial.id, newTrial.status)
          pollAttempts = pollAttempts + 1
          isPending = (newTrial.status = TrialStatus.CREATING)
        Loop while isPending AndAlso (pollAttempts <= MAX_RETRIES)

        If newTrial.status = TrialStatus.ACTIVE Then
          ' The trial creation was successful.
          Console.WriteLine("Trial created with ID {0} and trial campaign ID {1}.",
              newTrial.id, newTrial.trialCampaignId)
        Else If newTrial.status = TrialStatus.CREATION_FAILED Then
          ' The trial creation failed, and errors can be fetched from the
          ' TrialAsyncErrorService.
          Dim errorsSelector As New Selector()
          errorsSelector.fields = New string() {
              TrialAsyncError.Fields.TrialId, TrialAsyncError.Fields.AsyncError
          }
          errorsSelector.predicates = New Predicate() {
            Predicate.Equals(TrialAsyncError.Fields.TrialId, newTrial.id)
          }

          Dim trialAsyncErrorService As TrialAsyncErrorService = _
              CType(user.GetService(AdWordsService.v201603.TrialAsyncErrorService),
                  TrialAsyncErrorService)

          Dim trialAsyncErrorPage As TrialAsyncErrorPage = trialAsyncErrorService.get(
              errorsSelector)
          If trialAsyncErrorPage.entries Is Nothing OrElse _
              trialAsyncErrorPage.entries.Length = 0 Then
            Console.WriteLine("Could not retrieve errors for trial {0}.", newTrial.id)
          Else
            Console.WriteLine("Could not create trial ID {0} for draft ID {1} due to the " & _
                "following errors:", trialId, draftId)
            Dim i As Integer = 1
            For Each err As TrialAsyncError In trialAsyncErrorPage.entries
              Dim asyncError As ApiError = err.asyncError
              Console.WriteLine("Error #{0}: errorType='{1}', errorString='{2}', trigger='{3}'," & _
                " fieldPath='{4}'", i, asyncError.ApiErrorType, asyncError.errorString, _
                asyncError.trigger, asyncError.fieldPath)
              i += 1
            Next
          End If
        Else
          ' Most likely, the trial is still being created. You can continue
          ' polling, but we have limited the number of attempts in the
          ' example.
          Console.WriteLine("Timed out waiting to create trial from draft ID {0} with " +
              "base campaign ID {1}.", draftId, baseCampaignId)
        End If
      Catch e As Exception
        Throw New System.ApplicationException("Failed to create trial from draft.", e)
      End Try
    End Sub
  End Class
End Namespace

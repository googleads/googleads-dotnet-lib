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
Imports Google.Api.Ads.AdWords.Util.BatchJob.v201603
Imports Google.Api.Ads.AdWords.v201603

Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Threading

Namespace Google.Api.Ads.AdWords.Examples.VB.v201603
  ''' <summary>
  ''' This code sample illustrates how to perform asynchronous requests using
  ''' BatchJobService and incremental uploads of operations. It also
  ''' demonstrates how to cancel a running batch job.
  ''' </summary>
  Public Class AddKeywordsUsingIncrementalBatchJob
    Inherits ExampleBase

    Private Const NUMBER_OF_KEYWORDS_TO_ADD As Long = 100

    ''' <summary>
    ''' The chunk size to use when uploading operations.
    ''' </summary>
    Private Const CHUNK_SIZE As Integer = 4 * 1024 * 1024

    ''' <summary>
    ''' The polling interval base to be used for exponential backoff.
    ''' </summary>
    Private Const POLL_INTERVAL_SECONDS_BASE As Integer = 30

    ''' <summary>
    ''' The maximum number of retries.
    ''' </summary>
    Private Const MAX_RETRIES As Long = 5

    Private Shared ReadOnly PENDING_STATUSES As ISet(Of BatchJobStatus) = _
        New HashSet(Of BatchJobStatus)

    ''' <summary>
    ''' Static constructor.
    ''' </summary>
    Shared Sub New()
      ' Initialize the pending statuses Hashset. The 'from' keyword cannot be
      ' used due to mono compiler limitations.
      PENDING_STATUSES.Add(BatchJobStatus.ACTIVE)
      PENDING_STATUSES.Add(BatchJobStatus.AWAITING_FILE)
      PENDING_STATUSES.Add(BatchJobStatus.CANCELING)    
    End Sub
    
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New AddKeywordsUsingIncrementalBatchJob
      Console.WriteLine(codeExample.Description)
      Try
        Dim adGroupId As Long = Long.Parse("INSERT_ADGROUP_ID_HERE")
        codeExample.Run(New AdWordsUser, adGroupId)
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
        Return "This code sample illustrates how to perform asynchronous requests using " & _
            "BatchJobService and incremental uploads of operations. It also demonstrates " & _
            "how to cancel a running batch job."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="adGroupId">Id of the ad groups to which keywords are
    ''' added.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long)
      ' Get the BatchJobService.
      Dim batchJobService As BatchJobService = CType(user.GetService( _
          AdWordsService.v201603.BatchJobService), BatchJobService)

      Dim addOp As New BatchJobOperation()
      addOp.operator = [Operator].ADD
      addOp.operand = New BatchJob()

      Try
        Dim batchJob As BatchJob = batchJobService.mutate(
            New BatchJobOperation() {addOp}).value(0)

        Console.WriteLine("Created BatchJob with ID {0}, status '{1}' and upload URL {2}.",
            batchJob.id, batchJob.status, batchJob.uploadUrl.url)

        Dim operations As List(Of AdGroupCriterionOperation) = CreateOperations(adGroupId)

        ' Create a BatchJobUtilities instance for uploading operations. Use a
        ' chunked upload.
        Dim batchJobUploadHelper As New BatchJobUtilities(user, True, CHUNK_SIZE)

        ' Create a resumable Upload URL to upload the operations.
        Dim resumableUploadUrl As String = batchJobUploadHelper.GetResumableUploadUrl(
            batchJob.uploadUrl.url)

        ' Use the BatchJobUploadHelper to upload all operations.
        batchJobUploadHelper.Upload(resumableUploadUrl, operations.ToArray())

        Dim pollAttempts As Long = 0
        Dim isPending As Boolean = False
        batchJob = WaitWhileJobIsPending(batchJobService, batchJob, isPending,
            pollAttempts)

        ' A flag to determine if the job was requested to be cancelled. This
        ' typically comes from the user.
        Dim wasCancelRequested As Boolean = False

        ' Optional: Cancel the job if it has not completed after retrying
        ' MAX_RETRIES times.
        If isPending AndAlso (Not wasCancelRequested) AndAlso (pollAttempts = MAX_RETRIES) Then
          batchJob = CancelJob(batchJobService, batchJob)
          batchJob = WaitWhileJobIsPending(batchJobService, batchJob, isPending,
              pollAttempts)
        End If

        If isPending Then
          Throw New TimeoutException("Job is still in pending state after polling " & _
              MAX_RETRIES & " times.")
        End If

        If Not (batchJob.processingErrors Is Nothing) Then
          For Each processingError As BatchJobProcessingError In batchJob.processingErrors
            Console.WriteLine("  Processing error: {0}, {1}, {2}, {3}, {4}",
                processingError.ApiErrorType, processingError.trigger,
                processingError.errorString, processingError.fieldPath,
                processingError.reason)
          Next
        End If

        If (Not batchJob.downloadUrl Is Nothing) AndAlso _
          (Not batchJob.downloadUrl.url Is Nothing) Then
          Dim mutateResponse As BatchJobMutateResponse = batchJobUploadHelper.Download(
              batchJob.downloadUrl.url)
          Console.WriteLine("Downloaded results from {0}.", batchJob.downloadUrl.url)
          For Each mutateResult As MutateResult In mutateResponse.rval
            Dim outcome As String = ""
            If mutateResult.errorList Is Nothing Then
              outcome = "SUCCESS"
            Else
              outcome = "FAILURE"
            End If
            Console.WriteLine("  Operation [{0}] - {1}", mutateResult.index, outcome)
          Next
        Else
          Console.WriteLine("No results available for download.")
        End If
      Catch e As Exception
        Throw New System.ApplicationException("Failed to add keywords using batch job.", e)
      End Try
    End Sub

    ''' <summary>
    ''' Cancels the job.
    ''' </summary>
    ''' <param name="batchJobService">The batch job service.</param>
    ''' <param name="batchJob">The batch job.</param>
    Shared Function CancelJob(ByVal batchJobService As BatchJobService, _
                              ByVal batchJob As BatchJob) As BatchJob
      Try
        batchJob.status = BatchJobStatus.CANCELING
        Dim batchJobSetOperation As New BatchJobOperation()
        batchJobSetOperation.operator = [Operator].SET
        batchJobSetOperation.operand = batchJob

        batchJob = batchJobService.mutate(New BatchJobOperation() {batchJobSetOperation}).value(0)
        Console.WriteLine("Requested cancellation of batch job with ID {0}.", batchJob.id)
      Catch e As AdWordsApiException
        Dim innerException As ApiException = CType(e.ApiException, ApiException)
        If innerException Is Nothing Then
          ' This means that the API call failed, but not due to an error on
          ' the operations. You can still examine the innerException property
          ' of the original exception to get more details.
          Throw New Exception("Failed to retrieve ApiError. See inner exception for more " & _
              "details.", e)
        End If

        ' Examine each ApiError received from the server.
        For Each apiError As ApiError In innerException.errors
          If TypeOf apiError Is BatchJobError Then
            Dim batchJobError As BatchJobError = CType(apiError, BatchJobError)
            If batchJobError.reason = BatchJobErrorReason.INVALID_STATE_CHANGE Then
              Console.WriteLine("Attempt to cancel batch job with ID {0} was rejected because " & _
                  "the job already completed or was canceled.", batchJob.id)
              Continue For
            End If
          End If
        Next
        Throw
      End Try
      Return batchJob
    End Function

    ''' <summary>
    ''' Waits while the batch job is pending.
    ''' </summary>
    ''' <param name="batchJobService">The batch job service.</param>
    ''' <param name="batchJob">The batch job.</param>
    ''' <param name="isPending">True, if the job status is pending, false
    ''' otherwise.</param>
    ''' <param name="pollAttempts">The poll attempts to make while waiting for
    ''' batchjob completion or cancellation.</param>
    Shared Function WaitWhileJobIsPending(ByVal batchJobService As BatchJobService, _
        ByVal job As BatchJob, ByRef isPending As Boolean, ByRef pollAttempts As Long) _
        As BatchJob
      pollAttempts = 0
      isPending = True
      Do
        Dim sleepMillis As Integer = CType(Math.Pow(2, pollAttempts) * _
            POLL_INTERVAL_SECONDS_BASE * 1000, Integer)
        Console.WriteLine("Sleeping {0} millis...", sleepMillis)
        Thread.Sleep(sleepMillis)

        Dim selector As New Selector()
        selector.fields = New String() {BatchJob.Fields.Id, BatchJob.Fields.Status,
                BatchJob.Fields.DownloadUrl, BatchJob.Fields.ProcessingErrors,
                BatchJob.Fields.ProgressStats}
        selector.predicates = New Predicate() {
            Predicate.Equals(BatchJob.Fields.Id, job.id)
        }
        job = batchJobService.get(selector).entries(0)

        Console.WriteLine("Batch job ID {0} has status '{1}'.", job.id, job.status)
        isPending = PENDING_STATUSES.Contains(job.status)
      Loop While isPending AndAlso (++pollAttempts) <= MAX_RETRIES
      Return job
    End Function

    ''' <summary>
    ''' Creates the operations for uploading via batch job.
    ''' </summary>
    ''' <param name="adGroupId">The ad group ID.</param>
    ''' <returns>The list of operations.</returns>
    Shared Function CreateOperations(ByVal adGroupId As Long) As List(Of AdGroupCriterionOperation)
      Dim operations As New List(Of AdGroupCriterionOperation)()

      ' Create AdGroupCriterionOperations to add keywords, and upload every 10 operations
      ' incrementally.
      For i As Integer = 0 To NUMBER_OF_KEYWORDS_TO_ADD
        ' Create Keyword.
        Dim text As String = String.Format("mars{0}", i)

        ' Make 10% of keywords invalid to demonstrate error handling.
        If (i Mod 10) = 0 Then
          text = text + "!!!"
        End If

        ' Create BiddableAdGroupCriterion.
        Dim bagc As New BiddableAdGroupCriterion()
        bagc.adGroupId = adGroupId

        Dim keyword As New Keyword()
        keyword.text = text
        keyword.matchType = KeywordMatchType.BROAD

        bagc.criterion = keyword

        ' Create AdGroupCriterionOperation.
        Dim agco As New AdGroupCriterionOperation()
        agco.operand = bagc
        agco.operator = [Operator].ADD

        ' Add to the list of operations.
        operations.Add(agco)
      Next
      Return operations
    End Function
  End Class
End Namespace

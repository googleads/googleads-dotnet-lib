' Copyright 2018 Google LLC
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
Imports Google.Api.Ads.AdWords.Util.BatchJob.v201806
Imports Google.Api.Ads.AdWords.v201806

Namespace Google.Api.Ads.AdWords.Examples.VB.v201806
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
        Private Const CHUNK_SIZE As Integer = 4*1024*1024

        ''' <summary>
        ''' The maximum milliseconds to wait for completion.
        ''' </summary>
        Private Const TIME_TO_WAIT_FOR_COMPLETION As Integer = 15*60*1000 ' 15 minutes

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
                Console.WriteLine("An exception occurred while running this code example. {0}",
                                  ExampleUtilities.FormatException(e))
            End Try
        End Sub

        ''' <summary>
        ''' Returns a description about the code example.
        ''' </summary>
        Public Overrides ReadOnly Property Description() As String
            Get
                Return "This code sample illustrates how to perform asynchronous requests using " &
                       "BatchJobService and incremental uploads of operations. It also " &
                       "demonstrates how to cancel a running batch job."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        ''' <param name="adGroupId">Id of the ad groups to which keywords are
        ''' added.</param>
        Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long)
            Using batchJobService As BatchJobService = CType(
                user.GetService(
                    AdWordsService.v201806.BatchJobService),
                BatchJobService)

                Dim addOp As New BatchJobOperation
                addOp.operator = [Operator].ADD
                addOp.operand = New BatchJob()

                Try
                    Dim batchJob As BatchJob = batchJobService.mutate(
                        New BatchJobOperation() {addOp}).value(0)

                    Console.WriteLine(
                        "Created BatchJob with ID {0}, status '{1}' and upload URL {2}.",
                        batchJob.id, batchJob.status, batchJob.uploadUrl.url)

                    Dim operations As List(Of AdGroupCriterionOperation) =
                            CreateOperations(adGroupId)

                    ' Create a BatchJobUtilities instance for uploading operations. Use a
                    ' chunked upload.
                    Dim batchJobUploadHelper As New BatchJobUtilities(user, True, CHUNK_SIZE)

                    ' Create a resumable Upload URL to upload the operations.
                    Dim resumableUploadUrl As String = batchJobUploadHelper.GetResumableUploadUrl(
                        batchJob.uploadUrl.url)

                    ' Use the BatchJobUploadHelper to upload all operations.
                    batchJobUploadHelper.Upload(resumableUploadUrl, operations.ToArray())

                    Dim waitHandler As WaitHandler
                    Dim wasCancelRequested As Boolean = False

                    ' Create a wait handler.
                    waitHandler = New WaitHandler(batchJob, wasCancelRequested)

                    Dim isComplete As Boolean =
                            batchJobUploadHelper.WaitForPendingJob(batchJob.id,
                                                                   TIME_TO_WAIT_FOR_COMPLETION,
                                                                   AddressOf _
                                                                      waitHandler.
                                                                      OnJobWaitForCompletion)

                    ' Restore the latest value for batchJob from waithandler.
                    batchJob = waitHandler.Job
                    wasCancelRequested = waitHandler.WasCancelRequested

                    ' Optional: Cancel the job if it has not completed after waiting for
                    ' TIME_TO_WAIT_FOR_COMPLETION.
                    Dim shouldWaitForCancellation As Boolean = False
                    If Not isComplete AndAlso wasCancelRequested Then
                        Dim cancellationError As BatchJobError = Nothing
                        Try
                            batchJobUploadHelper.TryToCancelJob(batchJob.id)
                        Catch e As AdWordsApiException
                            cancellationError = GetBatchJobError(e)
                        End Try
                        If cancellationError Is Nothing Then
                            Console.WriteLine("Successfully requested job cancellation.")
                            shouldWaitForCancellation = True
                        Else
                            Console.WriteLine("Job cancellation failed. Error says: {0}.",
                                              cancellationError.reason)
                        End If

                        If shouldWaitForCancellation Then
                            waitHandler = New WaitHandler(batchJob, wasCancelRequested)

                            isComplete =
                                batchJobUploadHelper.WaitForPendingJob(batchJob.id,
                                                                       TIME_TO_WAIT_FOR_COMPLETION,
                                                                       AddressOf _
                                                                          waitHandler.
                                                                          OnJobWaitForCancellation)

                            batchJob = waitHandler.Job
                            wasCancelRequested = waitHandler.WasCancelRequested
                        End If
                    End If

                    If Not isComplete Then
                        Throw _
                            New TimeoutException(
                                "Job is still in pending state after waiting for " &
                                TIME_TO_WAIT_FOR_COMPLETION & " seconds.")
                    End If

                    If Not batchJob.processingErrors Is Nothing Then
                        For Each processingError As BatchJobProcessingError In _
                            batchJob.processingErrors
                            Console.WriteLine("  Processing error: {0}, {1}, {2}, {3}, {4}",
                                              processingError.ApiErrorType, processingError.trigger,
                                              processingError.errorString,
                                              processingError.fieldPath,
                                              processingError.reason)
                        Next
                    End If

                    If (Not batchJob.downloadUrl Is Nothing) AndAlso
                       (Not batchJob.downloadUrl.url Is Nothing) Then
                        Dim mutateResponse As BatchJobMutateResponse =
                                batchJobUploadHelper.Download(
                                    batchJob.downloadUrl.url)
                        Console.WriteLine("Downloaded results from {0}.", batchJob.downloadUrl.url)
                        For Each mutateResult As MutateResult In mutateResponse.rval
                            Dim outcome As String
                            If mutateResult.errorList Is Nothing Then
                                outcome = "SUCCESS"
                            Else
                                outcome = "FAILURE"
                            End If
                            Console.WriteLine("  Operation [{0}] - {1}", mutateResult.index,
                                              outcome)
                        Next
                    Else
                        Console.WriteLine("No results available for download.")
                    End If
                Catch e As Exception
                    Throw _
                        New System.ApplicationException(
                            "Failed to create keywords using batch job.",
                            e)
                End Try
            End Using
        End Sub

        ''' <summary>
        ''' A class that handles wait callbacks for the batch job.
        ''' </summary>
        Class WaitHandler
            ''' <summary>
            ''' The batch job to wait for.
            ''' </summary>
            Private batchJob As BatchJob

            ''' <summary>
            ''' A flag to determine if the job was requested to be cancelled. This
            ''' typically comes from the user.
            ''' </summary>
            Private cancelRequested As Boolean

            ''' <summary>
            ''' Initializes a new instance of the <see cref="WaitHandler"/> class.
            ''' </summary>
            ''' <param name="batchJob">The batch job to wait for.</param>
            ''' <param name="wasCancelRequested">A flag to determine if the job was
            ''' requested to be cancelled. This typically comes from the user.</param>
            Sub New(ByVal batchJob As BatchJob, ByVal wasCancelRequested As Boolean)
                Me.batchJob = batchJob
                Me.WasCancelRequested = wasCancelRequested
            End Sub

            ''' <summary>
            ''' Gets or sets the batch job to wait for.
            ''' </summary>
            Public Property Job As BatchJob
                Get
                    Return Me.batchJob
                End Get
                Set(value As BatchJob)
                    Me.batchJob = value
                End Set
            End Property

            ''' <summary>
            ''' Gets or sets a flag to determine if the job was requested to be
            ''' cancelled. This typically comes from the user.
            ''' </summary>
            Public Property WasCancelRequested As Boolean
                Get
                    Return Me.cancelRequested
                End Get
                Set(value As Boolean)
                    Me.cancelRequested = value
                End Set
            End Property

            ''' <summary>
            ''' Callback method when the job is waiting for cancellation.
            ''' </summary>
            ''' <param name="waitBatchJob">The updated batch job being waited for.</param>
            ''' <param name="timeElapsed">The time elapsed.</param>
            ''' <returns>True, if the wait loop should be cancelled, false otherwise.
            '''</returns>
            Public Function OnJobWaitForCancellation(ByVal waitBatchJob As BatchJob,
                                                     ByVal timeElapsed As Long) As Boolean
                Console.WriteLine("[{0} seconds]: Batch job ID {1} has status '{2}'.",
                                  timeElapsed/1000, waitBatchJob.id, waitBatchJob.status)
                batchJob = waitBatchJob
                Return False
            End Function

            ''' <summary>
            ''' Callback method when the job is waiting for completion.
            ''' </summary>
            ''' <param name="waitBatchJob">The updated batch job being waited for.</param>
            ''' <param name="timeElapsed">The time elapsed.</param>
            ''' <returns>True, if the wait loop should be cancelled, false otherwise.
            '''</returns>
            Public Function OnJobWaitForCompletion(ByVal waitBatchJob As BatchJob,
                                                   ByVal timeElapsed As Long) As Boolean
                Console.WriteLine("[{0} seconds]: Batch job ID {1} has status '{2}'.",
                                  timeElapsed/1000, waitBatchJob.id, waitBatchJob.status)
                batchJob = waitBatchJob
                Return Me.WasCancelRequested
            End Function
        End Class

        ''' <summary>
        ''' Gets the batch job error.
        ''' </summary>
        ''' <param name="e">The AdWords API Exception.</param>
        ''' <returns>The underlying batch job error if available, null otherwise.</returns>
        Private Function GetBatchJobError(ByVal e As AdWordsApiException) As BatchJobError
            Dim temp As List(Of BatchJobError) = TryCast(e.ApiException, ApiException).
                    GetAllErrorsByType (Of BatchJobError)()
            ' MOE:begin_strip
            ' Reinvent FirstOrDefault since you cannot use FirstOrDefault with Mono and VBNC. It
            ' works in C# though.
            ' MOE:end_strip
            If temp.Count = 0 Then
                Return Nothing
            Else
                Return temp(0)
            End If
        End Function

        ''' <summary>
        ''' Creates the operations for uploading via batch job.
        ''' </summary>
        ''' <param name="adGroupId">The ad group ID.</param>
        ''' <returns>The list of operations.</returns>
        Private Shared Function CreateOperations(ByVal adGroupId As Long) _
            As List(Of AdGroupCriterionOperation)
            Dim operations As New List(Of AdGroupCriterionOperation)

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

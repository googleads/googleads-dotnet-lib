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

Imports System.Threading
Imports Google.Api.Ads.AdWords.Lib
Imports Google.Api.Ads.AdWords.Util.BatchJob
Imports Google.Api.Ads.AdWords.Util.BatchJob.v201809
Imports Google.Api.Ads.AdWords.v201809

Namespace Google.Api.Ads.AdWords.Examples.VB.v201809
    ''' <summary>
    ''' This code example illustrates how to use BatchJobService to create multiple
    ''' complete campaigns, including ad groups and keywords.
    ''' </summary>
    Public Class AddCompleteCampaignsUsingStreamingBatchJob
        Inherits ExampleBase

        ''' <summary>
        ''' The last ID that was automatically generated.
        ''' </summary>
        Private Shared LAST_ID As Long = - 1

        ''' <summary>
        ''' The number of campaigns to be added.
        ''' </summary>
        Private Const NUMBER_OF_CAMPAIGNS_TO_ADD As Long = 2

        ''' <summary>
        ''' The number of ad groups to be added per campaign.
        ''' </summary>
        Private Const NUMBER_OF_ADGROUPS_TO_ADD As Long = 2

        ''' <summary>
        ''' The number of keywords to be added per campaign.
        ''' </summary>
        Private Const NUMBER_OF_KEYWORDS_TO_ADD As Long = 5

        ''' <summary>
        ''' The maximum milliseconds to wait for completion.
        ''' </summary>
        Private Const TIME_TO_WAIT_FOR_COMPLETION As Integer = 15*60*1000 ' 15 minutes

        ''' <summary>
        ''' Create a temporary ID generator that will produce a sequence of descending
        ''' negative numbers.
        ''' </summary>
        ''' <returns></returns>
        Private Shared Function NextId() As Long
            Return Interlocked.Decrement(LAST_ID)
        End Function

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New AddCompleteCampaignsUsingStreamingBatchJob
            Console.WriteLine(codeExample.Description)
            Try
                codeExample.Run(New AdWordsUser)
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
                Return _
                    "This code example illustrates how to use BatchJobService to create multiple" &
                    " complete campaigns, including ad groups and keywords."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        Public Sub Run(ByVal user As AdWordsUser)
            Using batchJobService As BatchJobService = CType(
                user.GetService(
                    AdWordsService.v201809.BatchJobService),
                BatchJobService)

                Try
                    ' Create a BatchJob.
                    Dim addOp As New BatchJobOperation()
                    addOp.operator = [Operator].ADD
                    addOp.operand = New BatchJob()

                    Dim batchJob As BatchJob = batchJobService.mutate(
                        New BatchJobOperation() {addOp}).value(0)

                    ' Get the upload URL from the new job.
                    Dim uploadUrl As String = batchJob.uploadUrl.url

                    Console.WriteLine(
                        "Created BatchJob with ID {0}, status '{1}' and upload URL {2}.",
                        batchJob.id, batchJob.status, batchJob.uploadUrl.url)

                    Dim batchJobUploadHelper As New BatchJobUtilities(user)

                    ' Create a resumable Upload URL to upload the operations.
                    Dim resumableUploadUrl As String =
                            batchJobUploadHelper.GetResumableUploadUrl(uploadUrl)

                    Dim uploadProgress As BatchUploadProgress =
                            batchJobUploadHelper.BeginStreamUpload(resumableUploadUrl)

                    ' Create and add an operation to create a new budget.
                    Dim budgetOperation As BudgetOperation = BuildBudgetOperation()
                    uploadProgress = batchJobUploadHelper.StreamUpload(uploadProgress,
                                                                       New Operation() _
                                                                          {budgetOperation})

                    ' Create and add operations to create new campaigns.
                    Dim campaignOperations As List(Of CampaignOperation) =
                            BuildCampaignOperations(budgetOperation.operand.budgetId)
                    uploadProgress = batchJobUploadHelper.StreamUpload(uploadProgress,
                                                                       campaignOperations.ToArray())

                    ' Create and add operations to create new ad groups.
                    Dim adGroupOperations As New List(Of AdGroupOperation)()
                    For Each campaignOperation As CampaignOperation In campaignOperations
                        adGroupOperations.AddRange(
                            BuildAdGroupOperations(campaignOperation.operand.id))
                    Next
                    uploadProgress = batchJobUploadHelper.StreamUpload(uploadProgress,
                                                                       adGroupOperations.ToArray())

                    ' Create and add operations to create new ad group ads (expanded text ads).
                    Dim adOperations As New List(Of AdGroupAdOperation)()
                    For Each adGroupOperation As AdGroupOperation In adGroupOperations
                        adOperations.AddRange(BuildAdGroupAdOperations(adGroupOperation.operand.id))
                    Next
                    uploadProgress = batchJobUploadHelper.StreamUpload(uploadProgress,
                                                                       adOperations.ToArray())

                    ' Create and add operations to create new ad group criteria (keywords).
                    Dim keywordOperations As New List(Of AdGroupCriterionOperation)()
                    For Each adGroupOperation As AdGroupOperation In adGroupOperations
                        keywordOperations.AddRange(
                            BuildAdGroupCriterionOperations(
                                adGroupOperation.operand.id))
                    Next
                    uploadProgress = batchJobUploadHelper.StreamUpload(uploadProgress,
                                                                       keywordOperations.ToArray())

                    ' Mark the upload as complete.
                    batchJobUploadHelper.EndStreamUpload(uploadProgress)

                    Dim waitHandler As WaitHandler

                    ' Create a wait handler.
                    waitHandler = New WaitHandler(batchJob, False)

                    Dim isComplete As Boolean = batchJobUploadHelper.WaitForPendingJob(
                        batchJob.id,
                        TIME_TO_WAIT_FOR_COMPLETION,
                        AddressOf waitHandler.OnJobWaitForCompletion)

                    ' Restore the latest value for batchJob from waithandler.
                    batchJob = waitHandler.Job

                    If Not isComplete Then
                        Throw _
                            New TimeoutException(
                                "Job is still in pending state after waiting for " &
                                TIME_TO_WAIT_FOR_COMPLETION & " seconds.")
                    End If

                    If Not (batchJob.processingErrors Is Nothing) Then
                        For Each processingError As BatchJobProcessingError In _
                            batchJob.processingErrors
                            Console.WriteLine("  Processing error: {0}, {1}, {2}, {3}, {4}",
                                              processingError.ApiErrorType, processingError.trigger,
                                              processingError.errorString,
                                              processingError.fieldPath,
                                              processingError.reason)
                        Next
                    End If

                    If (Not (batchJob.downloadUrl Is Nothing)) AndAlso
                       (Not (batchJob.downloadUrl.url Is Nothing)) Then
                        Dim mutateResponse As BatchJobMutateResponse =
                                batchJobUploadHelper.Download(
                                    batchJob.downloadUrl.url)
                        Console.WriteLine("Downloaded results from {0}.", batchJob.downloadUrl.url)
                        For Each mutateResult As MutateResult In mutateResponse.rval
                            Dim outcome As String = ""
                            If mutateResult.errorList Is Nothing Then
                                outcome = "SUCCESS"
                            Else
                                outcome = "FAILURE"
                            End If
                            Console.WriteLine("  Operation [{0}] - {1}", mutateResult.index,
                                              outcome)
                        Next
                    End If
                Catch e As Exception
                    Throw _
                        New System.ApplicationException("Failed to add campaigns using batch job.",
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
        ''' Builds the operation for creating an ad within an ad group.
        ''' </summary>
        ''' <param name="adGroupId">ID of the ad group for which ads are created.</param>
        ''' <returns>A list of operations for creating ads.</returns>
        Private Shared Function BuildAdGroupAdOperations(ByVal adGroupId As Long) _
            As List(Of AdGroupAdOperation)
            Dim operations As New List(Of AdGroupAdOperation)()

            Dim adGroupAd As New AdGroupAd()
            adGroupAd.adGroupId = adGroupId

            Dim expandedTextAd As New ExpandedTextAd
            expandedTextAd.headlinePart1 = "Luxury Cruise to Mars"
            expandedTextAd.headlinePart2 = "Visit the Red Planet in style."
            expandedTextAd.description = "Low-gravity fun for everyone!"
            expandedTextAd.finalUrls = New String() {"http://www.example.com/1"}
            adGroupAd.ad = expandedTextAd

            Dim operation As New AdGroupAdOperation()
            operation.operand = adGroupAd
            operation.operator = [Operator].ADD
            operations.Add(operation)
            Return operations
        End Function

        ''' <summary>
        ''' Builds the operations for creating keywords within an ad group.
        ''' </summary>
        ''' <param name="adGroupId">ID of the ad group for which keywords are
        ''' created.</param>
        ''' <returns>A list of operations for creating keywords.</returns>
        Private Shared Function BuildAdGroupCriterionOperations(ByVal adGroupId As Long) _
            As List(Of AdGroupCriterionOperation)
            Dim adGroupCriteriaOperations As New List(Of AdGroupCriterionOperation)()

            ' Create AdGroupCriterionOperations to add keywords.

            For i As Integer = 0 To NUMBER_OF_KEYWORDS_TO_ADD
                ' Create Keyword.
                Dim text As String = String.Format("mars{0}", i)

                ' Make 50% of keywords invalid to demonstrate error handling.
                If (i Mod 2) = 0 Then
                    text = text & "!!!"
                End If

                ' Create AdGroupCriterionOperation.
                Dim operation As New AdGroupCriterionOperation()
                operation.operand = New BiddableAdGroupCriterion()
                operation.operand.adGroupId = adGroupId

                Dim keyword As New Keyword
                keyword.text = text
                keyword.matchType = KeywordMatchType.BROAD
                operation.operand.criterion = keyword

                operation.operator = [Operator].ADD
                ' Add to list.
                adGroupCriteriaOperations.Add(operation)
            Next

            Return adGroupCriteriaOperations
        End Function

        ''' <summary>
        ''' Builds the operations for creating ad groups within a campaign.
        ''' </summary>
        ''' <param name="campaignId">ID of the campaign for which ad groups are
        ''' created.</param>
        ''' <returns>A list of operations for creating ad groups.</returns>
        Private Shared Function BuildAdGroupOperations(ByVal campaignId As Long) As _
            List(Of AdGroupOperation)
            Dim operations As New List(Of AdGroupOperation)()
            For i As Integer = 0 To NUMBER_OF_ADGROUPS_TO_ADD
                Dim adGroup As New AdGroup()
                adGroup.campaignId = campaignId
                adGroup.id = NextId()
                adGroup.name = "Batch Ad Group # " & ExampleUtilities.GetRandomString()

                Dim cpcBid As New CpcBid
                cpcBid.bid = New Money()
                cpcBid.bid.microAmount = 10000000L
                Dim biddingStrategyConfiguration As New BiddingStrategyConfiguration()
                biddingStrategyConfiguration.bids = New Bids() {cpcBid}

                adGroup.biddingStrategyConfiguration = biddingStrategyConfiguration

                Dim operation As New AdGroupOperation()
                operation.operand = adGroup
                operation.operator = [Operator].ADD

                operations.Add(operation)
            Next
            Return operations
        End Function

        ''' <summary>
        ''' Builds the operations for creating new campaigns.
        ''' </summary>
        ''' <param name="budgetId">ID of the budget to be used for the campaign.
        ''' </param>
        ''' <returns>A list of operations for creating campaigns.</returns>
        Private Shared Function BuildCampaignOperations(ByVal budgetId As Long) _
            As List(Of CampaignOperation)
            Dim operations As New List(Of CampaignOperation)()

            For i As Integer = 0 To NUMBER_OF_CAMPAIGNS_TO_ADD
                Dim campaign As New Campaign()
                campaign.name = "Batch Campaign " + ExampleUtilities.GetRandomString()

                ' Recommendation: Set the campaign to PAUSED when creating it to prevent
                ' the ads from immediately serving. Set to ENABLED once you've added
                ' targeting and the ads are ready to serve.
                campaign.status = CampaignStatus.PAUSED

                campaign.id = NextId()
                campaign.advertisingChannelType = AdvertisingChannelType.SEARCH

                Dim budget As New Budget()
                budget.budgetId = budgetId
                campaign.budget = budget

                Dim biddingStrategyConfiguration As New BiddingStrategyConfiguration()
                biddingStrategyConfiguration.biddingStrategyType = BiddingStrategyType.MANUAL_CPC

                ' You can optionally provide a bidding scheme in place of the type.
                Dim biddingScheme As New ManualCpcBiddingScheme()
                biddingStrategyConfiguration.biddingScheme = biddingScheme

                campaign.biddingStrategyConfiguration = biddingStrategyConfiguration

                Dim operation As New CampaignOperation()
                operation.operand = campaign
                operation.operator = [Operator].ADD

                operations.Add(operation)
            Next
            Return operations
        End Function

        ''' <summary>
        ''' Builds an operation for creating a budget.
        ''' </summary>
        ''' <returns>The operation for creating a budget.</returns>
        Private Shared Function BuildBudgetOperation() As BudgetOperation
            Dim budget As New Budget()
            budget.budgetId = NextId()
            budget.name = "Interplanetary Cruise #" & ExampleUtilities.GetRandomString()

            Dim amount As New Money()
            amount.microAmount = 50000000L
            budget.amount = amount

            budget.deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD

            Dim budgetOperation As New BudgetOperation()
            budgetOperation.operand = budget
            budgetOperation.operator = [Operator].ADD

            Return budgetOperation
        End Function
    End Class
End Namespace

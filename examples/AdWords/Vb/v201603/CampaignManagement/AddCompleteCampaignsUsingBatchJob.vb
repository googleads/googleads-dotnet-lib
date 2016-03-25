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
Imports System.Threading
Imports Google.Api.Ads.AdWords.Util.BatchJob.v201603

Namespace Google.Api.Ads.AdWords.Examples.VB.v201603
  ''' <summary>
  ''' This code example illustrates how to use BatchJobService to create multiple
  ''' complete campaigns, including ad groups and keywords.
  ''' </summary>
  Public Class AddCompleteCampaignsUsingBatchJob
    Inherits ExampleBase

    ''' <summary>
    ''' The last ID that was automatically generated.
    ''' </summary>
    Shared LAST_ID As Long = -1

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
    ''' The polling interval base to be used for exponential backoff.
    ''' </summary>
    Private Const POLL_INTERVAL_SECONDS_BASE As Integer = 30

    ''' <summary>
    ''' The maximum number of retries.
    ''' </summary>
    Private Const MAX_RETRIES As Long = 5

    ''' <summary>
    ''' The list of batch job statuses that corresponds to the job being in a
    ''' pending state.
    ''' </summary>
    Private ReadOnly PENDING_STATUSES As New HashSet(Of BatchJobStatus)

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
      Dim codeExample As New AddCompleteCampaignsUsingBatchJob
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
        Return "This code example illustrates how to use BatchJobService to create multiple" & _
            " complete campaigns, including ad groups and keywords."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    Public Sub Run(ByVal user As AdWordsUser)
      ' Get the BatchJobService.
      Dim batchJobService As BatchJobService = CType(user.GetService( _
          AdWordsService.v201603.BatchJobService), BatchJobService)

      Try
        ' Create a BatchJob.
        Dim addOp As New BatchJobOperation()
        addOp.operator = [Operator].ADD
        addOp.operand = New BatchJob()

        Dim job As BatchJob = batchJobService.mutate( _
            New BatchJobOperation() {addOp}).value(0)

        ' Get the upload URL from the new job.
        Dim uploadUrl As String = job.uploadUrl.url

        Console.WriteLine("Created BatchJob with ID {0}, status '{1}' and upload URL {2}.",
            job.id, job.status, job.uploadUrl.url)

        ' Create the mutate request that will be sent to the upload URL.
        Dim operations As New List(Of Operation)()

        ' Create and add an operation to create a new budget.
        Dim budgetOperation As BudgetOperation = BuildBudgetOperation()
        operations.Add(budgetOperation)

        ' Create and add operations to create new campaigns.
        Dim campaignOperations As List(Of CampaignOperation) = _
            BuildCampaignOperations(budgetOperation.operand.budgetId)
        operations.AddRange(campaignOperations.ToArray())

        Dim adGroupOperations As New List(Of AdGroupOperation)()

        ' Create and add operations to create new ad groups.
        For Each campaignOperation As CampaignOperation In campaignOperations
          adGroupOperations.AddRange(BuildAdGroupOperations(campaignOperation.operand.id))
        Next
        operations.AddRange(adGroupOperations.ToArray())

        ' Create and add operations to create new ad group criteria (keywords).
        For Each adGroupOperation As AdGroupOperation In adGroupOperations
          operations.AddRange(BuildAdGroupAdOperations(adGroupOperation.operand.id).ToArray())
        Next

        ' Create and add operations to create new ad group ads (text ads).
        For Each adGroupOperation As AdGroupOperation In adGroupOperations
          operations.AddRange(BuildAdGroupCriterionOperations(adGroupOperation.operand.id).ToArray())
        Next

        Dim batchJobUploadHelper As New BatchJobUtilities(user)

        ' Create a resumable Upload URL to upload the operations.
        Dim resumableUploadUrl As String = batchJobUploadHelper.GetResumableUploadUrl(uploadUrl)

        ' Use the BatchJobUploadHelper to upload all operations.
        batchJobUploadHelper.Upload(resumableUploadUrl, operations.ToArray())

        Dim pollAttempts As Long = 0
        Dim isPending As Boolean = True
        Do
          Dim sleepMillis As Integer = CType(Math.Pow(2, pollAttempts) * _
              POLL_INTERVAL_SECONDS_BASE * 1000, Integer)
          Console.WriteLine("Sleeping {0} millis...", sleepMillis)
          Thread.Sleep(sleepMillis)
          Dim selector As New Selector()
          selector.fields = New String() { _
              BatchJob.Fields.Id, BatchJob.Fields.Status, BatchJob.Fields.DownloadUrl, _
              BatchJob.Fields.ProcessingErrors, BatchJob.Fields.ProgressStats
          }
          selector.predicates = New Predicate() { _
            Predicate.Equals(BatchJob.Fields.Id, job.id)
          }
          job = batchJobService.get(selector).entries(0)
          Console.WriteLine("Batch job ID {0} has status '{1}'.", job.id, job.status)
          isPending = PENDING_STATUSES.Contains(job.status)
          pollAttempts += 1
        Loop While isPending AndAlso pollAttempts <= MAX_RETRIES

        If isPending Then
          Throw New TimeoutException("Job is still in pending state after polling " & _
              MAX_RETRIES.ToString() & " times.")
        End If

        If Not (job.processingErrors Is Nothing) Then
          For Each processingError As BatchJobProcessingError In job.processingErrors
            Console.WriteLine("  Processing error: {0}, {1}, {2}, {3}, {4}", _
                processingError.ApiErrorType, processingError.trigger, _
                processingError.errorString, processingError.fieldPath, _
                processingError.reason)
          Next
        End If

        If (Not (job.downloadUrl Is Nothing)) AndAlso _
           (Not (job.downloadUrl.url Is Nothing)) Then
          Dim mutateResponse As BatchJobMutateResponse = batchJobUploadHelper.Download( _
              job.downloadUrl.url)
          Console.WriteLine("Downloaded results from {0}.", job.downloadUrl.url)
          For Each mutateResult As MutateResult In mutateResponse.rval
            Dim outcome As String = ""
            If mutateResult.errorList Is Nothing Then
              outcome = "SUCCESS"
            Else
              outcome = "FAILURE"
            End If
            Console.WriteLine("  Operation [{0}] - {1}", mutateResult.index, outcome)
          Next
        End If
      Catch e As Exception
        Throw New System.ApplicationException("Failed to add campaigns using batch job.", e)
      End Try
    End Sub

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

      Dim textAd As New TextAd
      textAd.headline = "Luxury Cruise to Mars"
      textAd.description1 = "Visit the Red Planet in style."
      textAd.description2 = "Low-gravity fun for everyone!"
      textAd.displayUrl = "www.example.com"
      textAd.finalUrls = New String() {"http://www.example.com/1"}
      adGroupAd.ad = textAd

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
    Private Shared Function BuildAdGroupOperations(ByVal campaignId As Long) As  _
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
        biddingScheme.enhancedCpcEnabled = False
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

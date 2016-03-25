// Copyright 2016, Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Google.Api.Ads.AdWords.Util.BatchJob.v201603;
using Google.Api.Ads.AdWords.v201603;

using System;
using System.Collections.Generic;
using System.Threading;
using Google.Api.Ads.AdWords.Lib;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {

  /// <summary>
  /// This code sample illustrates how to perform asynchronous requests using
  /// BatchJobService and incremental uploads of operations. It also
  /// demonstrates how to cancel a running batch job.
  /// </summary>
  public class AddKeywordsUsingIncrementalBatchJob : ExampleBase {
    private const long NUMBER_OF_KEYWORDS_TO_ADD = 100;

    // The chunk size to use when uploading operations.
    private const int CHUNK_SIZE = 4 * 1024 * 1024;

    /// <summary>
    /// The polling interval base to be used for exponential backoff.
    /// </summary>
    private const int POLL_INTERVAL_SECONDS_BASE = 30;

    /// <summary>
    /// The maximum number of retries.
    /// </summary>
    private const long MAX_RETRIES = 5;

    private readonly ISet<BatchJobStatus> PENDING_STATUSES = new HashSet<BatchJobStatus>() {
      BatchJobStatus.ACTIVE, BatchJobStatus.AWAITING_FILE, BatchJobStatus.CANCELING
    };

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddKeywordsUsingIncrementalBatchJob codeExample = new AddKeywordsUsingIncrementalBatchJob();
      Console.WriteLine(codeExample.Description);
      try {
        long adGroupId = long.Parse("INSERT_ADGROUP_ID_HERE");
        codeExample.Run(new AdWordsUser(), adGroupId);
      } catch (Exception e) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(e));
      }
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code sample illustrates how to perform asynchronous requests using " +
            "BatchJobService and incremental uploads of operations. It also demonstrates " +
            "how to cancel a running batch job.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">Id of the ad groups to which keywords are
    /// added.</param>
    public void Run(AdWordsUser user, long adGroupId) {
      // Get the MutateJobService.
      BatchJobService batchJobService = (BatchJobService) user.GetService(
          AdWordsService.v201603.BatchJobService);

      BatchJobOperation addOp = new BatchJobOperation() {
        @operator = Operator.ADD,
        operand = new BatchJob()
      };

      try {
        BatchJob batchJob = batchJobService.mutate(new BatchJobOperation[] { addOp }).value[0];

        Console.WriteLine("Created BatchJob with ID {0}, status '{1}' and upload URL {2}.",
            batchJob.id, batchJob.status, batchJob.uploadUrl.url);

        List<AdGroupCriterionOperation> operations = CreateOperations(adGroupId);

        // Create a BatchJobUtilities instance for uploading operations. Use a
        // chunked upload.
        BatchJobUtilities batchJobUploadHelper = new BatchJobUtilities(user, true, CHUNK_SIZE);

        // Create a resumable Upload URL to upload the operations.
        string resumableUploadUrl = batchJobUploadHelper.GetResumableUploadUrl(
            batchJob.uploadUrl.url);

        // Use the BatchJobUploadHelper to upload all operations.
        batchJobUploadHelper.Upload(resumableUploadUrl, operations.ToArray());

        long pollAttempts = 0;
        bool isPending = false;
        batchJob = WaitWhileJobIsPending(batchJobService, batchJob, out isPending,
            out pollAttempts);

        // A flag to determine if the job was requested to be cancelled. This
        // typically comes from the user.
        bool wasCancelRequested = false;

        // Optional: Cancel the job if it has not completed after retrying
        // MAX_RETRIES times.
        if (isPending && !wasCancelRequested && pollAttempts == MAX_RETRIES) {
          batchJob = CancelJob(batchJobService, batchJob);
          batchJob = WaitWhileJobIsPending(batchJobService, batchJob, out isPending,
              out pollAttempts);
        }

        if (isPending) {
          throw new TimeoutException("Job is still in pending state after polling " +
              MAX_RETRIES + " times.");
        }

        if (batchJob.processingErrors != null) {
          foreach (BatchJobProcessingError processingError in batchJob.processingErrors) {
            Console.WriteLine("  Processing error: {0}, {1}, {2}, {3}, {4}",
                processingError.ApiErrorType, processingError.trigger,
                processingError.errorString, processingError.fieldPath,
                processingError.reason);
          }
        }

        if (batchJob.downloadUrl != null && batchJob.downloadUrl.url != null) {
          BatchJobMutateResponse mutateResponse = batchJobUploadHelper.Download(
              batchJob.downloadUrl.url);
          Console.WriteLine("Downloaded results from {0}.", batchJob.downloadUrl.url);
          foreach (MutateResult mutateResult in mutateResponse.rval) {
            String outcome = mutateResult.errorList == null ? "SUCCESS" : "FAILURE";
            Console.WriteLine("  Operation [{0}] - {1}", mutateResult.index, outcome);
          }
        } else {
          Console.WriteLine("No results available for download.");
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to create keywords using batch job.", e);
      }
    }

    /// <summary>
    /// Cancels the job.
    /// </summary>
    /// <param name="batchJobService">The batch job service.</param>
    /// <param name="batchJob">The batch job.</param>
    private BatchJob CancelJob(BatchJobService batchJobService, BatchJob batchJob) {
      try {
        batchJob.status = BatchJobStatus.CANCELING;
        BatchJobOperation batchJobSetOperation = new BatchJobOperation() {
          @operator = Operator.SET,
          operand = batchJob
        };

        batchJob = batchJobService.mutate(
            new BatchJobOperation[] { batchJobSetOperation }).value[0];
        Console.WriteLine("Requested cancellation of batch job with ID {0}.", batchJob.id);
      } catch (AdWordsApiException e) {
        ApiException innerException = e.ApiException as ApiException;
        if (innerException == null) {
          // This means that the API call failed, but not due to an error on
          // the operations. You can still examine the innerException property
          // of the original exception to get more details.
          throw new Exception("Failed to retrieve ApiError. See inner exception for more " +
              "details.", e);
        }

        // Examine each ApiError received from the server.
        foreach (ApiError apiError in innerException.errors) {
          if (apiError is BatchJobError) {
            BatchJobError batchJobError = (BatchJobError) apiError;
            if (batchJobError.reason == BatchJobErrorReason.INVALID_STATE_CHANGE) {
              Console.WriteLine("Attempt to cancel batch job with ID {0} was rejected because " +
                  "the job already completed or was canceled.", batchJob.id);
              continue;
            }
          }
        }
        throw;
      }
      return batchJob;
    }

    /// <summary>
    /// Waits while the batch job is pending.
    /// </summary>
    /// <param name="batchJobService">The batch job service.</param>
    /// <param name="batchJob">The batch job.</param>
    /// <param name="isPending">True, if the job status is pending, false
    /// otherwise.</param>
    /// <param name="pollAttempts">The poll attempts to make while waiting for
    /// batchjob completion or cancellation.</param>
    private BatchJob WaitWhileJobIsPending(BatchJobService batchJobService, BatchJob batchJob,
        out bool isPending, out long pollAttempts) {
      pollAttempts = 0;
      isPending = true;
      do {
        int sleepMillis = (int) Math.Pow(2, pollAttempts) *
            POLL_INTERVAL_SECONDS_BASE * 1000;
        Console.WriteLine("Sleeping {0} millis...", sleepMillis);
        Thread.Sleep(sleepMillis);

        Selector selector = new Selector() {
          fields = new string[] { BatchJob.Fields.Id, BatchJob.Fields.Status,
                BatchJob.Fields.DownloadUrl, BatchJob.Fields.ProcessingErrors,
                BatchJob.Fields.ProgressStats },
          predicates = new Predicate[] {
              Predicate.Equals(BatchJob.Fields.Id, batchJob.id)
            }
        };
        batchJob = batchJobService.get(selector).entries[0];

        Console.WriteLine("Batch job ID {0} has status '{1}'.", batchJob.id, batchJob.status);
        isPending = PENDING_STATUSES.Contains(batchJob.status);
      } while (isPending && ++pollAttempts <= MAX_RETRIES);
      return batchJob;
    }

    /// <summary>
    /// Creates the operations for uploading via batch job.
    /// </summary>
    /// <param name="adGroupId">The ad group ID.</param>
    /// <returns>The list of operations.</returns>
    private static List<AdGroupCriterionOperation> CreateOperations(long adGroupId) {
      List<AdGroupCriterionOperation> operations = new List<AdGroupCriterionOperation>();

      // Create AdGroupCriterionOperations to add keywords, and upload every 10 operations
      // incrementally.
      for (int i = 0; i < NUMBER_OF_KEYWORDS_TO_ADD; i++) {
        // Create Keyword.
        String text = String.Format("mars{0}", i);

        // Make 10% of keywords invalid to demonstrate error handling.
        if (i % 10 == 0) {
          text = text + "!!!";
        }

        // Create BiddableAdGroupCriterion.
        BiddableAdGroupCriterion bagc = new BiddableAdGroupCriterion() {
          adGroupId = adGroupId,
          criterion = new Keyword() {
            text = text,
            matchType = KeywordMatchType.BROAD
          }
        };

        // Create AdGroupCriterionOperation.
        AdGroupCriterionOperation agco = new AdGroupCriterionOperation() {
          operand = bagc,
          @operator = Operator.ADD
        };

        // Add to the list of operations.
        operations.Add(agco);
      }
      return operations;
    }
  }
}

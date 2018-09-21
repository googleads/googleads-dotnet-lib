// Copyright 2018 Google LLC
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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.Util.BatchJob.v201809;
using Google.Api.Ads.AdWords.v201809;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201809
{
    /// <summary>
    /// This code sample illustrates how to perform asynchronous requests using
    /// BatchJobService and incremental uploads of operations. It also
    /// demonstrates how to cancel a running batch job.
    /// </summary>
    public class AddKeywordsUsingIncrementalBatchJob : ExampleBase
    {
        private const long NUMBER_OF_KEYWORDS_TO_ADD = 100;

        // The chunk size to use when uploading operations.
        private const int CHUNK_SIZE = 4 * 1024 * 1024;

        /// <summary>
        /// The maximum milliseconds to wait for completion.
        /// </summary>
        private const int TIME_TO_WAIT_FOR_COMPLETION = 15 * 60 * 1000; // 15 minutes

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            AddKeywordsUsingIncrementalBatchJob codeExample =
                new AddKeywordsUsingIncrementalBatchJob();
            Console.WriteLine(codeExample.Description);
            try
            {
                long adGroupId = long.Parse("INSERT_ADGROUP_ID_HERE");
                codeExample.Run(new AdWordsUser(), adGroupId);
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception occurred while running this code example. {0}",
                    ExampleUtilities.FormatException(e));
            }
        }

        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
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
        public void Run(AdWordsUser user, long adGroupId)
        {
            using (BatchJobService batchJobService =
                (BatchJobService) user.GetService(AdWordsService.v201809.BatchJobService))
            {
                BatchJobOperation addOp = new BatchJobOperation()
                {
                    @operator = Operator.ADD,
                    operand = new BatchJob()
                };

                try
                {
                    BatchJob batchJob = batchJobService.mutate(new BatchJobOperation[]
                    {
                        addOp
                    }).value[0];

                    Console.WriteLine(
                        "Created BatchJob with ID {0}, status '{1}' and upload URL {2}.",
                        batchJob.id, batchJob.status, batchJob.uploadUrl.url);

                    List<AdGroupCriterionOperation> operations = CreateOperations(adGroupId);

                    // Create a BatchJobUtilities instance for uploading operations. Use a
                    // chunked upload.
                    BatchJobUtilities batchJobUploadHelper =
                        new BatchJobUtilities(user, true, CHUNK_SIZE);

                    // Create a resumable Upload URL to upload the operations.
                    string resumableUploadUrl =
                        batchJobUploadHelper.GetResumableUploadUrl(batchJob.uploadUrl.url);

                    // Use the BatchJobUploadHelper to upload all operations.
                    batchJobUploadHelper.Upload(resumableUploadUrl, operations.ToArray());

                    // A flag to determine if the job was requested to be cancelled. This
                    // typically comes from the user.
                    bool wasCancelRequested = false;

                    bool isComplete = batchJobUploadHelper.WaitForPendingJob(batchJob.id,
                        TIME_TO_WAIT_FOR_COMPLETION,
                        delegate(BatchJob waitBatchJob, long timeElapsed)
                        {
                            Console.WriteLine("[{0} seconds]: Batch job ID {1} has status '{2}'.",
                                timeElapsed / 1000, waitBatchJob.id, waitBatchJob.status);
                            batchJob = waitBatchJob;
                            return wasCancelRequested;
                        });

                    // Optional: Cancel the job if it has not completed after waiting for
                    // TIME_TO_WAIT_FOR_COMPLETION.
                    bool shouldWaitForCancellation = false;
                    if (!isComplete && wasCancelRequested)
                    {
                        BatchJobError cancellationError = null;
                        try
                        {
                            batchJobUploadHelper.TryToCancelJob(batchJob.id);
                        }
                        catch (AdWordsApiException e)
                        {
                            cancellationError = GetBatchJobError(e);
                        }

                        if (cancellationError == null)
                        {
                            Console.WriteLine("Successfully requested job cancellation.");
                            shouldWaitForCancellation = true;
                        }
                        else
                        {
                            Console.WriteLine("Job cancellation failed. Error says: {0}.",
                                cancellationError.reason);
                        }

                        if (shouldWaitForCancellation)
                        {
                            isComplete = batchJobUploadHelper.WaitForPendingJob(batchJob.id,
                                TIME_TO_WAIT_FOR_COMPLETION,
                                delegate(BatchJob waitBatchJob, long timeElapsed)
                                {
                                    Console.WriteLine(
                                        "[{0} seconds]: Batch job ID {1} has status '{2}'.",
                                        timeElapsed / 1000, waitBatchJob.id, waitBatchJob.status);
                                    batchJob = waitBatchJob;
                                    return false;
                                });
                        }
                    }

                    if (!isComplete)
                    {
                        throw new TimeoutException(
                            "Job is still in pending state after waiting for " +
                            TIME_TO_WAIT_FOR_COMPLETION + " seconds.");
                    }

                    if (batchJob.processingErrors != null)
                    {
                        foreach (BatchJobProcessingError processingError in batchJob
                            .processingErrors)
                        {
                            Console.WriteLine("  Processing error: {0}, {1}, {2}, {3}, {4}",
                                processingError.ApiErrorType, processingError.trigger,
                                processingError.errorString, processingError.fieldPath,
                                processingError.reason);
                        }
                    }

                    if (batchJob.downloadUrl != null && batchJob.downloadUrl.url != null)
                    {
                        BatchJobMutateResponse mutateResponse =
                            batchJobUploadHelper.Download(batchJob.downloadUrl.url);
                        Console.WriteLine("Downloaded results from {0}.", batchJob.downloadUrl.url);
                        foreach (MutateResult mutateResult in mutateResponse.rval)
                        {
                            string outcome = mutateResult.errorList == null ? "SUCCESS" : "FAILURE";
                            Console.WriteLine("  Operation [{0}] - {1}", mutateResult.index,
                                outcome);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No results available for download.");
                    }
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException(
                        "Failed to create keywords using batch job.", e);
                }
            }
        }

        /// <summary>
        /// Gets the batch job error.
        /// </summary>
        /// <param name="e">The AdWords API Exception.</param>
        /// <returns>The underlying batch job error if available, null otherwise.</returns>
        private BatchJobError GetBatchJobError(AdWordsApiException e)
        {
            return (e.ApiException as ApiException).GetAllErrorsByType<BatchJobError>()
                .FirstOrDefault();
        }

        /// <summary>
        /// Creates the operations for uploading via batch job.
        /// </summary>
        /// <param name="adGroupId">The ad group ID.</param>
        /// <returns>The list of operations.</returns>
        private static List<AdGroupCriterionOperation> CreateOperations(long adGroupId)
        {
            List<AdGroupCriterionOperation> operations = new List<AdGroupCriterionOperation>();

            // Create AdGroupCriterionOperations to add keywords, and upload every 10 operations
            // incrementally.
            for (int i = 0; i < NUMBER_OF_KEYWORDS_TO_ADD; i++)
            {
                // Create Keyword.
                string text = string.Format("mars{0}", i);

                // Make 10% of keywords invalid to demonstrate error handling.
                if (i % 10 == 0)
                {
                    text = text + "!!!";
                }

                // Create BiddableAdGroupCriterion.
                BiddableAdGroupCriterion bagc = new BiddableAdGroupCriterion()
                {
                    adGroupId = adGroupId,
                    criterion = new Keyword()
                    {
                        text = text,
                        matchType = KeywordMatchType.BROAD
                    }
                };

                // Create AdGroupCriterionOperation.
                AdGroupCriterionOperation agco = new AdGroupCriterionOperation()
                {
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

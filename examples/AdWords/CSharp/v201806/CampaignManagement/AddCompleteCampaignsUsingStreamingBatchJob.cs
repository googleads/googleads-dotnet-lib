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
using Google.Api.Ads.AdWords.Util.BatchJob;
using Google.Api.Ads.AdWords.Util.BatchJob.v201806;
using Google.Api.Ads.AdWords.v201806;

using System;
using System.Collections.Generic;
using System.Threading;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201806
{
    /// <summary>
    /// This code sample illustrates how to use BatchJobService to create multiple
    /// complete campaigns, including ad groups and keywords.
    /// </summary>
    public class AddCompleteCampaignsUsingStreamingBatchJob : ExampleBase
    {
        /// <summary>
        /// The last ID that was automatically generated.
        /// </summary>
        private static long LAST_ID = -1;

        /// <summary>
        /// The number of campaigns to be added.
        /// </summary>
        private const long NUMBER_OF_CAMPAIGNS_TO_ADD = 2;

        /// <summary>
        /// The number of ad groups to be added per campaign.
        /// </summary>
        private const long NUMBER_OF_ADGROUPS_TO_ADD = 2;

        /// <summary>
        /// The number of keywords to be added per campaign.
        /// </summary>
        private const long NUMBER_OF_KEYWORDS_TO_ADD = 5;

        /// <summary>
        /// The polling interval base to be used for exponential backoff.
        /// </summary>
        private const int POLL_INTERVAL_SECONDS_BASE = 30;

        /// <summary>
        /// The maximum milliseconds to wait for completion.
        /// </summary>
        private const int TIME_TO_WAIT_FOR_COMPLETION = 15 * 60 * 1000; // 15 minutes

        /// <summary>
        /// Create a temporary ID generator that will produce a sequence of descending
        /// negative numbers.
        /// </summary>
        /// <returns></returns>
        private static long NextId()
        {
            return Interlocked.Decrement(ref LAST_ID);
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            AddCompleteCampaignsUsingStreamingBatchJob codeExample =
                new AddCompleteCampaignsUsingStreamingBatchJob();
            Console.WriteLine(codeExample.Description);
            try
            {
                codeExample.Run(new AdWordsUser());
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
                return
                    "This code sample illustrates how to use BatchJobService to create multiple " +
                    "complete campaigns, including ad groups and keywords.";
            }
        }

        /// <summary>
        /// Runs the code example.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        public void Run(AdWordsUser user)
        {
            using (BatchJobService batchJobService =
                (BatchJobService) user.GetService(AdWordsService.v201806.BatchJobService))
            {
                try
                {
                    // Create a BatchJob.
                    BatchJobOperation addOp = new BatchJobOperation()
                    {
                        @operator = Operator.ADD,
                        operand = new BatchJob()
                    };

                    BatchJob batchJob = batchJobService.mutate(new BatchJobOperation[]
                    {
                        addOp
                    }).value[0];

                    // Get the upload URL from the new job.
                    string uploadUrl = batchJob.uploadUrl.url;

                    Console.WriteLine(
                        "Created BatchJob with ID {0}, status '{1}' and upload URL {2}.",
                        batchJob.id, batchJob.status, batchJob.uploadUrl.url);

                    BatchJobUtilities batchJobUploadHelper = new BatchJobUtilities(user);

                    // Create a resumable Upload URL to upload the operations.
                    string resumableUploadUrl =
                        batchJobUploadHelper.GetResumableUploadUrl(uploadUrl);

                    BatchUploadProgress uploadProgress =
                        batchJobUploadHelper.BeginStreamUpload(resumableUploadUrl);

                    // Create and add an operation to create a new budget.
                    BudgetOperation budgetOperation = BuildBudgetOperation();

                    uploadProgress = batchJobUploadHelper.StreamUpload(uploadProgress,
                        new List<Operation>()
                        {
                            budgetOperation
                        });

                    // Create and add operations to create new campaigns.
                    List<Operation> campaignOperations = new List<Operation>();
                    campaignOperations.AddRange(
                        BuildCampaignOperations(budgetOperation.operand.budgetId));

                    uploadProgress =
                        batchJobUploadHelper.StreamUpload(uploadProgress, campaignOperations);

                    // Create and add operations to create new ad groups.
                    List<Operation> adGroupOperations = new List<Operation>();
                    foreach (CampaignOperation campaignOperation in campaignOperations)
                    {
                        adGroupOperations.AddRange(
                            BuildAdGroupOperations(campaignOperation.operand.id));
                    }

                    uploadProgress =
                        batchJobUploadHelper.StreamUpload(uploadProgress, adGroupOperations);

                    // Create and add operations to create new ad group ads (expanded text ads).
                    List<Operation> adOperations = new List<Operation>();
                    foreach (AdGroupOperation adGroupOperation in adGroupOperations)
                    {
                        adOperations.AddRange(
                            BuildAdGroupAdOperations(adGroupOperation.operand.id));
                    }

                    uploadProgress =
                        batchJobUploadHelper.StreamUpload(uploadProgress, adOperations);

                    // Create and add operations to create new ad group criteria (keywords).
                    List<Operation> keywordOperations = new List<Operation>();
                    foreach (AdGroupOperation adGroupOperation in adGroupOperations)
                    {
                        keywordOperations.AddRange(
                            BuildAdGroupCriterionOperations(adGroupOperation.operand.id));
                    }

                    uploadProgress =
                        batchJobUploadHelper.StreamUpload(uploadProgress, keywordOperations);

                    // Mark the upload as complete.
                    batchJobUploadHelper.EndStreamUpload(uploadProgress);

                    bool isCompleted = batchJobUploadHelper.WaitForPendingJob(batchJob.id,
                        TIME_TO_WAIT_FOR_COMPLETION,
                        delegate(BatchJob waitBatchJob, long timeElapsed)
                        {
                            Console.WriteLine("[{0} seconds]: Batch job ID {1} has status '{2}'.",
                                timeElapsed / 1000, waitBatchJob.id, waitBatchJob.status);
                            batchJob = waitBatchJob;
                            return false;
                        });

                    if (!isCompleted)
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
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException(
                        "Failed to add campaigns using batch job.", e);
                }
            }
        }

        /// <summary>
        /// Builds the operation for creating an ad within an ad group.
        /// </summary>
        /// <param name="adGroupId">ID of the ad group for which ads are created.</param>
        /// <returns>A list of operations for creating ads.</returns>
        private static List<AdGroupAdOperation> BuildAdGroupAdOperations(long adGroupId)
        {
            List<AdGroupAdOperation> operations = new List<AdGroupAdOperation>();
            AdGroupAd adGroupAd = new AdGroupAd()
            {
                adGroupId = adGroupId,
                ad = new ExpandedTextAd()
                {
                    headlinePart1 = "Luxury Cruise to Mars",
                    headlinePart2 = "Visit the Red Planet in style.",
                    description = "Low-gravity fun for everyone!",
                    finalUrls = new string[]
                    {
                        "http://www.example.com/1"
                    }
                }
            };

            AdGroupAdOperation operation = new AdGroupAdOperation()
            {
                operand = adGroupAd,
                @operator = Operator.ADD
            };
            operations.Add(operation);
            return operations;
        }

        /// <summary>
        /// Builds the operations for creating keywords within an ad group.
        /// </summary>
        /// <param name="adGroupId">ID of the ad group for which keywords are
        /// created.</param>
        /// <returns>A list of operations for creating keywords.</returns>
        private static List<AdGroupCriterionOperation> BuildAdGroupCriterionOperations(
            long adGroupId)
        {
            List<AdGroupCriterionOperation> adGroupCriteriaOperations =
                new List<AdGroupCriterionOperation>();

            // Create AdGroupCriterionOperations to add keywords.

            for (int i = 0; i < NUMBER_OF_KEYWORDS_TO_ADD; i++)
            {
                // Create Keyword.
                string text = string.Format("mars{0}", i);

                // Make 50% of keywords invalid to demonstrate error handling.
                if ((i % 2) == 0)
                {
                    text = text + "!!!";
                }

                // Create AdGroupCriterionOperation.
                AdGroupCriterionOperation operation = new AdGroupCriterionOperation()
                {
                    operand = new BiddableAdGroupCriterion()
                    {
                        adGroupId = adGroupId,
                        criterion = new Keyword()
                        {
                            text = text,
                            matchType = KeywordMatchType.BROAD
                        }
                    },
                    @operator = Operator.ADD
                };

                // Add to list.
                adGroupCriteriaOperations.Add(operation);
            }

            return adGroupCriteriaOperations;
        }

        /// <summary>
        /// Builds the operations for creating ad groups within a campaign.
        /// </summary>
        /// <param name="campaignId">ID of the campaign for which ad groups are
        /// created.</param>
        /// <returns>A list of operations for creating ad groups.</returns>
        private static List<AdGroupOperation> BuildAdGroupOperations(long campaignId)
        {
            List<AdGroupOperation> operations = new List<AdGroupOperation>();
            for (int i = 0; i < NUMBER_OF_ADGROUPS_TO_ADD; i++)
            {
                AdGroup adGroup = new AdGroup()
                {
                    campaignId = campaignId,
                    id = NextId(),
                    name = "Batch Ad Group # " + ExampleUtilities.GetRandomString(),
                    biddingStrategyConfiguration = new BiddingStrategyConfiguration()
                    {
                        bids = new Bids[]
                        {
                            new CpcBid()
                            {
                                bid = new Money()
                                {
                                    microAmount = 10000000L
                                }
                            }
                        }
                    }
                };

                AdGroupOperation operation = new AdGroupOperation()
                {
                    operand = adGroup,
                    @operator = Operator.ADD
                };

                operations.Add(operation);
            }

            return operations;
        }

        /// <summary>
        /// Builds the operations for creating new campaigns.
        /// </summary>
        /// <param name="budgetId">ID of the budget to be used for the campaign.
        /// </param>
        /// <returns>A list of operations for creating campaigns.</returns>
        private static List<CampaignOperation> BuildCampaignOperations(long budgetId)
        {
            List<CampaignOperation> operations = new List<CampaignOperation>();

            for (int i = 0; i < NUMBER_OF_CAMPAIGNS_TO_ADD; i++)
            {
                Campaign campaign = new Campaign()
                {
                    name = "Batch Campaign " + ExampleUtilities.GetRandomString(),

                    // Recommendation: Set the campaign to PAUSED when creating it to prevent
                    // the ads from immediately serving. Set to ENABLED once you've added
                    // targeting and the ads are ready to serve.
                    status = CampaignStatus.PAUSED,
                    id = NextId(),
                    advertisingChannelType = AdvertisingChannelType.SEARCH,
                    budget = new Budget()
                    {
                        budgetId = budgetId
                    },
                    biddingStrategyConfiguration = new BiddingStrategyConfiguration()
                    {
                        biddingStrategyType = BiddingStrategyType.MANUAL_CPC,

                        // You can optionally provide a bidding scheme in place of the type.
                        biddingScheme = new ManualCpcBiddingScheme()
                    }
                };

                CampaignOperation operation = new CampaignOperation()
                {
                    operand = campaign,
                    @operator = Operator.ADD
                };
                operations.Add(operation);
            }

            return operations;
        }

        /// <summary>
        /// Builds an operation for creating a budget.
        /// </summary>
        /// <returns>The operation for creating a budget.</returns>
        private static BudgetOperation BuildBudgetOperation()
        {
            Budget budget = new Budget()
            {
                budgetId = NextId(),
                name = "Interplanetary Cruise #" + ExampleUtilities.GetRandomString(),
                amount = new Money()
                {
                    microAmount = 50000000L,
                },
                deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD,
            };

            BudgetOperation budgetOperation = new BudgetOperation()
            {
                operand = budget,
                @operator = Operator.ADD
            };
            return budgetOperation;
        }
    }
}

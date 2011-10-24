// Copyright 2011, Google Inc. All Rights Reserved.
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

// Author: api.anash@gmail.com (Anash P. Oommen)

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201109;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109 {
  /// <summary>
  /// This code example gets a bulk mutate job by id. To add a bulk mutate
  /// job, run PerformBulkMutateJob.cs.
  ///
  /// Tags: BulkMutateJobService.get
  /// </summary>
  class GetBulkMutateJob : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets a bulk mutate job by id. To add a bulk mutate job, " +
            "run PerformBulkMutateJob.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetBulkMutateJob();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the BulkMutateJobService.
      BulkMutateJobService bulkMutateJobService =
          (BulkMutateJobService) user.GetService(AdWordsService.v201109.
              BulkMutateJobService);

      long jobId = long.Parse(_T("INSERT_BULK_MUTATE_JOBID_HERE"));
      // Create selector.
      BulkMutateJobSelector selector = new BulkMutateJobSelector();
      selector.includeStats = true;
      selector.jobIds = new long[] {jobId};

      try {
        // Get all bulk mutate jobs.
        BulkMutateJob[] bulkMutateJobs = bulkMutateJobService.get(selector);

        // Display bulk mutate jobs.
        if (bulkMutateJobs != null) {
          foreach (BulkMutateJob bulkMutateJob in bulkMutateJobs) {
            Console.WriteLine("Bulk mutate job with id '{0}' and status '{1}' was found.",
                bulkMutateJob.id, bulkMutateJob.status);

            switch(bulkMutateJob.status) {
              case BasicJobStatus.PENDING:
                Console.WriteLine("  Total parts: {0}, parts received: {1}.",
                    bulkMutateJob.numRequestParts, bulkMutateJob.numRequestPartsReceived);
                break;

              case BasicJobStatus.PROCESSING:
                Console.WriteLine("  Percent complete: {0}.", bulkMutateJob.stats.progressPercent);
                break;

              case BasicJobStatus.COMPLETED:
                Console.WriteLine("  Total operations: {0}, failed: {1}, unprocessed: {2}.",
                  ((BulkMutateJobStats) bulkMutateJob.stats).numOperations,
                  ((BulkMutateJobStats) bulkMutateJob.stats).numFailedOperations,
                  ((BulkMutateJobStats) bulkMutateJob.stats).numUnprocessedOperations);
                break;

              case BasicJobStatus.FAILED:
                  Console.WriteLine("  Failure reason: %s.", bulkMutateJob.failureReason.Item);
                  break;
            }
          }
        } else {
          Console.WriteLine("No bulk mutate jobs were found.\n");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to get all bulk mutate jobs. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}

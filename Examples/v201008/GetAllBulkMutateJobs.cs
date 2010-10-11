// Copyright 2010, Google Inc. All Rights Reserved.
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

using com.google.api.adwords.lib;
using com.google.api.adwords.v201008;

using System;
using System.Collections.Generic;
using System.Text;

namespace com.google.api.adwords.examples.v201008 {
  /// <summary>
  /// This code example gets all bulk mutate jobs in the account. To add a
  /// bulk mutate job, run PerformBulkMutateJob.cs.
  ///
  /// Tags: BulkMutateJobService.get
  /// </summary>
  class GetAllBulkMutateJobs : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all bulk mutate jobs in the account. To add a bulk " +
            "mutate job, run PerformBulkMutateJob.cs.";
      }
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the BulkMutateJobService.
      BulkMutateJobService bulkMutateJobService =
          (BulkMutateJobService) user.GetService(AdWordsService.v201008.
              BulkMutateJobService);

      // Create selector.
      BulkMutateJobSelector selector = new BulkMutateJobSelector();
      selector.includeStats = true;

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
        Console.WriteLine("Failed to get all bulk mutate jobs. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

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
  /// This code example deletes a bulk mutate job using the 'REMOVE' operator.
  /// Jobs may only deleted if they are in the 'PENDING' state and have not
  /// yet receieved all of their request parts. To get bulk mutate jobs,
  /// run GetAllBulkMutateJobs.cs.
  ///
  /// Tags: BulkMutateJobService.mutate
  /// </summary>
  class DeleteBulkMutateJob : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example deletes a bulk mutate job using the 'REMOVE' operator. Jobs " +
            "may only deleted if they are in the 'PENDING' state and have not yet receieved all " +
            "of their request parts. To get bulk mutate jobs, run GetAllBulkMutateJobs.cs.";
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

      long bulkMutateJobId = long.Parse(_T("INSERT_BULK_MUTATE_JOB_ID_HERE"));

      // Create BulkMutateJob.
      BulkMutateJob bulkMutateJob = new BulkMutateJob();
      bulkMutateJob.id = bulkMutateJobId;

      // Create operation.
      JobOperation operation = new JobOperation();
      operation.operand = bulkMutateJob;
      operation.@operator = Operator.REMOVE;

      try {
        // Delete bulk mutate job.
        bulkMutateJob = bulkMutateJobService.mutate(operation);

        // Display bulk mutate jobs.
        if (bulkMutateJob != null) {
          Console.WriteLine("Bulk mutate job with id '{0}' was deleted.", bulkMutateJob.id);
        } else {
          Console.WriteLine("No bulk mutate jobs were deleted.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to delete bulk mutate jobs. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

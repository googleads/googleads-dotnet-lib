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
using System.Text.RegularExpressions;
using System.Threading;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109 {
  /// <summary>
  /// This code example shows how to perform asynchronous requests using the
  /// MutateJobService.
  ///
  /// Tags: MutateJobService.mutate, MutateJobService.get
  /// Tags: MutateJobService.getResult
  /// </summary>
  class PerformMutateJob : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example shows how to perform asynchronous requests using the" +
            " MutateJobService.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new PerformMutateJob();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the MutateJobService.
      MutateJobService mutateJobService = (MutateJobService) user.GetService(
          AdWordsService.v201109.MutateJobService);

      const int RETRY_INTERVAL = 30;
      const int RETRIES_COUNT = 30;
      const int KEYWORD_NUMBER = 100;
      const string INDEX_REGEX = "operations\\[(\\d+)\\].operand";

      long adGroupId = long.Parse(_T("INSERT_ADGROUP_ID_HERE"));

      List<Operation> operations = new List<Operation>();

      // Create an AdGroupAdOperation to add a text ad.
      AdGroupAdOperation adGroupAdOperation = new AdGroupAdOperation();
      adGroupAdOperation.@operator = Operator.ADD;

      TextAd textAd = new TextAd();
      textAd.headline = "Luxury Cruise to Mars";
      textAd.description1 = "Visit the Red Planet in style.";
      textAd.description2 = "Low-gravity fun for everyone!";
      textAd.displayUrl = "www.example.com";
      textAd.url = "http://www.example.com";

      AdGroupAd adGroupAd = new AdGroupAd();
      adGroupAd.adGroupId = adGroupId;
      adGroupAd.ad = textAd;

      adGroupAdOperation.operand = adGroupAd;

      operations.Add(adGroupAdOperation);

      // Create AdGroupCriterionOperation to add keywords.
      for (int i = 0; i < KEYWORD_NUMBER; i++) {
        Keyword keyword = new Keyword();
        keyword.text = string.Format("mars cruise {0}", i);
        keyword.matchType = KeywordMatchType.BROAD;

        BiddableAdGroupCriterion criterion = new BiddableAdGroupCriterion();
        criterion.adGroupId = adGroupId;
        criterion.criterion = keyword;

        AdGroupCriterionOperation adGroupCriterionOperation = new AdGroupCriterionOperation();
        adGroupCriterionOperation.@operator = Operator.ADD;

        adGroupCriterionOperation.operand = criterion;
        operations.Add(adGroupCriterionOperation);
      }

      BulkMutateJobPolicy policy = new BulkMutateJobPolicy();

      policy.prerequisiteJobIds = new long[] {};
      SimpleMutateJob job = mutateJobService.mutate(operations.ToArray(), policy);

      // Wait for the job to complete.
      bool completed = false;
      int retryCount = 0;
      Console.WriteLine("Retrieving job status...");
      while (completed == false && retryCount < RETRIES_COUNT) {
        BulkMutateJobSelector selector = new BulkMutateJobSelector();
        selector.jobIds = new long[] {job.id};

        try {
          Job[] allJobs = mutateJobService.get(selector);
          if (allJobs != null && allJobs.Length > 0) {
            job = (SimpleMutateJob) allJobs[0];
            if (job.status == BasicJobStatus.COMPLETED || job.status == BasicJobStatus.FAILED) {
              completed = true;
              break;
            } else {
              Console.WriteLine("{0}: Current status is {1}, waiting {2} seconds to retry...",
                  retryCount, job.status, RETRY_INTERVAL);
              Thread.Sleep(RETRY_INTERVAL * 1000);
              retryCount++;
            }
          }
        } catch (Exception ex) {
          Console.WriteLine("Failed to fetch bulk mutate job with id = {0}. Exception says \"{1}\"",
              job.id, ex.Message);
          return;
        }
      }

      if (job.status == BasicJobStatus.COMPLETED) {
        BulkMutateJobSelector selector = new BulkMutateJobSelector();
        selector.jobIds = new long[] {job.id};
        JobResult jobResult = mutateJobService.getResult(selector);
        if (jobResult != null) {
          SimpleMutateResult results = (SimpleMutateResult) jobResult.Item;
          if (results != null) {
            if (results.results != null) {
              for (int i = 0; i < results.results.Length; i++) {
                Operand operand = results.results[i];
                Console.WriteLine("Operation {0} - {1}", i, (operand.Item is PlaceHolder) ?
                    "FAILED" : "SUCCEEDED");
              }
            }
            if (results.errors != null) {
              foreach (ApiError error in results.errors) {
                Match match = Regex.Match(error.fieldPath, INDEX_REGEX, RegexOptions.IgnoreCase);
                string index = (match.Success)? match.Groups[1].Value : "???";
                Console.WriteLine("Operation index {0} failed due to reason: '{1}', " +
                    "trigger: '{2}'", index, error.errorString, error.trigger);
              }
            }
          }
        }
        Console.WriteLine("Job completed successfully!");
      } else if (job.status == BasicJobStatus.FAILED) {
        Console.WriteLine("Job failed with reason: " + job.failureReason);
      } else if (job.status == BasicJobStatus.PROCESSING || job.status == BasicJobStatus.PENDING) {
        Console.WriteLine("Job did not complete in {0} secconds.",
            RETRY_INTERVAL * RETRIES_COUNT);
      }
    }
  }
}

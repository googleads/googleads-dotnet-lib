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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201603;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {
  /// <summary>
  /// This code example shows how to handle RateExceededError in your
  /// application. To trigger the rate exceeded error, this code example runs
  /// 100 threads in parallel, each thread attempting to validate 100 keywords
  /// in a single request. Note that spawning 100 parallel threads is for
  /// illustrative purposes only, you shouldn't do this in your application.
  /// </summary>
  public class HandleRateExceededError: ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      HandleRateExceededError codeExample = new HandleRateExceededError();
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
        return "This code example shows how to handle RateExceededError in your application. " +
            "To trigger the rate exceeded error, this code example runs 100 threads in " +
            "parallel, each thread attempting to validate 100 keywords in a single request. " +
            "Note that spawning 100 parallel threads is for illustrative purposes only, you " +
            "shouldn't do this in your application.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">Id of the ad group to which keywords are added.
    /// </param>
    public void Run(AdWordsUser user, long adGroupId) {
      const int NUM_THREADS = 100;

      // Increase the maximum number of parallel HTTP connections that .NET
      // framework allows. By default, this is set to 2 by the .NET framework.
      System.Net.ServicePointManager.DefaultConnectionLimit = NUM_THREADS;

      List<Thread> threads = new List<Thread>();

      for (int i = 0; i < NUM_THREADS; i++) {
        Thread thread = new Thread(new KeywordThread(user, i, adGroupId).Run);
        threads.Add(thread);
      }

      for (int i = 0; i < NUM_THREADS; i++) {
        threads[i].Start(i);
      }

      for (int i = 0; i < NUM_THREADS; i++) {
        threads[i].Join();
      }
    }

    /// <summary>
    /// Thread class for validating keywords.
    /// </summary>
    class KeywordThread {
      /// <summary>
      /// Index of this thread, for identifying and debugging.
      /// </summary>
      int threadIndex;

      /// <summary>
      /// The ad group id to which keywords are added.
      /// </summary>
      long adGroupId;

      /// <summary>
      /// The AdWords user who owns this ad group.
      /// </summary>
      AdWordsUser user;

      /// <summary>
      /// Number of keywords to be validated in each API call.
      /// </summary>
      const int NUM_KEYWORDS = 100;

      /// <summary>
      /// Initializes a new instance of the <see cref="KeywordThread" /> class.
      /// </summary>
      /// <param name="threadIndex">Index of the thread.</param>
      /// <param name="adGroupId">The ad group id.</param>
      /// <param name="user">The AdWords user who owns the ad group.</param>
      public KeywordThread(AdWordsUser user, int threadIndex, long adGroupId) {
        this.user = user;
        this.threadIndex = threadIndex;
        this.adGroupId = adGroupId;
      }

      /// <summary>
      /// Main method for the thread.
      /// </summary>
      /// <param name="obj">The thread parameter.</param>
      public void Run(Object obj) {
        // Create the operations.
        List<AdGroupCriterionOperation> operations = new List<AdGroupCriterionOperation>();

        for (int j = 0; j < NUM_KEYWORDS; j++) {
          // Create the keyword.
          Keyword keyword = new Keyword();
          keyword.text = "mars cruise thread " + threadIndex.ToString() + " seed " + j.ToString();
          keyword.matchType = KeywordMatchType.BROAD;

          // Create the biddable ad group criterion.
          AdGroupCriterion keywordCriterion = new BiddableAdGroupCriterion();
          keywordCriterion.adGroupId = adGroupId;
          keywordCriterion.criterion = keyword;

          // Create the operations.
          AdGroupCriterionOperation keywordOperation = new AdGroupCriterionOperation();
          keywordOperation.@operator = Operator.ADD;
          keywordOperation.operand = keywordCriterion;

          operations.Add(keywordOperation);
        }

        // Get the AdGroupCriterionService. This should be done within the
        // thread, since a service can only handle one outgoing HTTP request
        // at a time.
        AdGroupCriterionService service = (AdGroupCriterionService) user.GetService(
            AdWordsService.v201603.AdGroupCriterionService);
        service.RequestHeader.validateOnly = true;
        int retryCount = 0;
        const int NUM_RETRIES = 3;
        try {
          while (retryCount < NUM_RETRIES) {
            try {
              // Validate the keywords.
              AdGroupCriterionReturnValue retval = service.mutate(operations.ToArray());
              break;
            } catch (AdWordsApiException e) {
              // Handle API errors.
              ApiException innerException = e.ApiException as ApiException;
              if (innerException == null) {
                throw new Exception("Failed to retrieve ApiError. See inner exception for more " +
                    "details.", e);
              }
              foreach (ApiError apiError in innerException.errors) {
                if (!(apiError is RateExceededError)) {
                  // Rethrow any errors other than RateExceededError.
                  throw;
                }
                // Handle rate exceeded errors.
                RateExceededError rateExceededError = (RateExceededError) apiError;
                Console.WriteLine("Got Rate exceeded error - rate name = '{0}', scope = '{1}', " +
                    "retry After {2} seconds.", rateExceededError.rateScope,
                    rateExceededError.rateName, rateExceededError.retryAfterSeconds);
                Thread.Sleep(rateExceededError.retryAfterSeconds * 1000);
                retryCount = retryCount + 1;
              }
            } finally {
              if (retryCount == NUM_RETRIES) {
                throw new Exception(String.Format("Could not recover after making {0} attempts.",
                    retryCount));
              }
            }
          }
        } catch (Exception e) {
          throw new System.ApplicationException("Failed to validate keywords.", e);
        }
      }
    }
  }
}

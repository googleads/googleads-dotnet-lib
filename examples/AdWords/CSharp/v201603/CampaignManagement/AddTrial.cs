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
using System.Threading;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {

  /// <summary>
  /// This code example illustrates how to create a trial and wait for it to
  /// complete. See the Campaign Drafts and Experiments guide for more
  /// information:
  /// https://developers.google.com/adwords/api/docs/guides/campaign-drafts-experiments
  /// </summary>
  public class AddTrial : ExampleBase {

    /// <summary>
    /// The polling interval base to be used for exponential backoff.
    /// </summary>
    private const int POLL_INTERVAL_SECONDS_BASE = 30;

    /// <summary>
    /// The maximum number of retries.
    /// </summary>
    private const long MAX_RETRIES = 5;

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddTrial codeExample = new AddTrial();
      Console.WriteLine(codeExample.Description);
      try {
        long draftId = long.Parse("INSERT_DRAFT_ID_HERE");
        long baseCampaignId = long.Parse("INSERT_BASE_CAMPAIGN_ID_HERE");
        codeExample.Run(new AdWordsUser(), draftId, baseCampaignId);
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
        return "This code example illustrates how to create a trial and wait for it to " +
            "complete. See the Campaign Drafts and Experiments guide for more information: " +
            "https://developers.google.com/adwords/api/docs/guides/campaign-drafts-experiments";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="baseCampaignId">Id of the campaign to use as base of the
    /// trial.</param>
    /// <param name="draftId">Id of the draft.</param>
    public void Run(AdWordsUser user, long draftId, long baseCampaignId) {
      // Get the TrialService.
      TrialService trialService = (TrialService) user.GetService(
        AdWordsService.v201603.TrialService);

      Trial trial = new Trial() {
        draftId = draftId,
        baseCampaignId = baseCampaignId,
        name = "Test Trial #" + ExampleUtilities.GetRandomString(),
        trafficSplitPercent = 50
      };

      TrialOperation trialOperation = new TrialOperation() {
        @operator = Operator.ADD,
        operand = trial
      };

      try {
        long trialId = trialService.mutate(new TrialOperation[] { trialOperation }).value[0].id;

        // Since creating a trial is asynchronous, we have to poll it to wait
        // for it to finish.
        Selector trialSelector = new Selector() {
          fields = new string[] {
            Trial.Fields.Id, Trial.Fields.Status, Trial.Fields.BaseCampaignId,
            Trial.Fields.TrialCampaignId
          },
          predicates = new Predicate[] {
            Predicate.Equals(Trial.Fields.Id, trialId)
          }
        };

        trial = null;
        bool isPending = true;
        int pollAttempts = 0;

        do {
          int sleepMillis = (int) Math.Pow(2, pollAttempts) *
              POLL_INTERVAL_SECONDS_BASE * 1000;
          Console.WriteLine("Sleeping {0} millis...", sleepMillis);
          Thread.Sleep(sleepMillis);

          trial = trialService.get(trialSelector).entries[0];

          Console.WriteLine("Trial ID {0} has status '{1}'.", trial.id, trial.status);
          pollAttempts++;
          isPending = (trial.status == TrialStatus.CREATING);
        } while (isPending && pollAttempts <= MAX_RETRIES);

        if (trial.status == TrialStatus.ACTIVE) {
          // The trial creation was successful.
          Console.WriteLine("Trial created with ID {0} and trial campaign ID {1}.",
              trial.id, trial.trialCampaignId);
        } else if (trial.status == TrialStatus.CREATION_FAILED) {
          // The trial creation failed, and errors can be fetched from the
          // TrialAsyncErrorService.
          Selector errorsSelector = new Selector() {
            fields = new string[] {
              TrialAsyncError.Fields.TrialId, TrialAsyncError.Fields.AsyncError
            },
            predicates = new Predicate[] {
              Predicate.Equals(TrialAsyncError.Fields.TrialId, trial.id)
            }
          };

          TrialAsyncErrorService trialAsyncErrorService =
              (TrialAsyncErrorService) user.GetService(
                  AdWordsService.v201603.TrialAsyncErrorService);

          TrialAsyncErrorPage trialAsyncErrorPage = trialAsyncErrorService.get(errorsSelector);
          if (trialAsyncErrorPage.entries == null || trialAsyncErrorPage.entries.Length == 0) {
            Console.WriteLine("Could not retrieve errors for trial {0}.", trial.id);
          } else {
            Console.WriteLine("Could not create trial ID {0} for draft ID {1} due to the " +
                "following errors:", trial.id, draftId);
            int i = 0;
            foreach (TrialAsyncError error in trialAsyncErrorPage.entries) {
              ApiError asyncError = error.asyncError;
              Console.WriteLine("Error #{0}: errorType='{1}', errorString='{2}', trigger='{3}'," +
                " fieldPath='{4}'", i++, asyncError.ApiErrorType, asyncError.errorString,
                asyncError.trigger, asyncError.fieldPath);
            }
          }
        } else {
            // Most likely, the trial is still being created. You can continue
            // polling, but we have limited the number of attempts in the
            // example.
            Console.WriteLine("Timed out waiting to create trial from draft ID {0} with " +
                "base campaign ID {1}.", draftId, baseCampaignId);
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to create trial from draft.", e);
      }
    }
  }
}

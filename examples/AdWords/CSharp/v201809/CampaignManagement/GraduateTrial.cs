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
using Google.Api.Ads.AdWords.v201809;

using System;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201809
{
    /// <summary>
    /// This code example illustrates how to graduate a trial. See the Campaign
    /// Drafts and Experiments guide for more information:
    /// https://developers.google.com/adwords/api/docs/guides/campaign-drafts-experiments
    /// </summary>
    public class GraduateTrial : ExampleBase
    {
        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            GraduateTrial codeExample = new GraduateTrial();
            Console.WriteLine(codeExample.Description);
            try
            {
                long trialId = long.Parse("INSERT_TRIAL_ID_HERE");
                codeExample.Run(new AdWordsUser(), trialId);
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
                return "This code example illustrates how to graduate a trial. See the Campaign " +
                    "Drafts and Experiments guide for more information: " +
                    "https://developers.google.com/adwords/api/docs/guides/campaign-drafts-" +
                    "experiments";
            }
        }

        /// <summary>
        /// Runs the code example.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="trialId">Id of the trial to be graduated.</param>
        public void Run(AdWordsUser user, long trialId)
        {
            using (TrialService trialService =
                (TrialService) user.GetService(AdWordsService.v201809.TrialService))
            {
                // To graduate a trial, you must specify a different budget from the
                // base campaign. The base campaign (in order to have had a trial based
                // on it) must have a non-shared budget, so it cannot be shared with
                // the new independent campaign created by graduation.
                Budget budget = CreateBudget(user);

                Trial trial = new Trial()
                {
                    id = trialId,
                    budgetId = budget.budgetId,
                    status = TrialStatus.GRADUATED
                };

                TrialOperation trialOperation = new TrialOperation()
                {
                    @operator = Operator.SET,
                    operand = trial
                };
                try
                {
                    // Update the trial.
                    trial = trialService.mutate(new TrialOperation[]
                    {
                        trialOperation
                    }).value[0];

                    // Graduation is a synchronous operation, so the campaign is already
                    // ready. If you promote instead, make sure to see the polling scheme
                    // demonstrated in AddTrial.cs to wait for the asynchronous operation
                    // to finish.
                    Console.WriteLine(
                        "Trial ID {0} graduated. Campaign ID {1} was given a new budget " +
                        "ID {2} and is no longer dependent on this trial.", trial.id,
                        trial.trialCampaignId, budget.budgetId);
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to graduate trial.", e);
                }
            }
        }

        /// <summary>
        /// Creates the budget.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The new budget.</returns>
        public Budget CreateBudget(AdWordsUser user)
        {
            using (BudgetService budgetService =
                (BudgetService) user.GetService(AdWordsService.v201809.BudgetService))
            {
                Budget budget = new Budget()
                {
                    name = "Budget #" + ExampleUtilities.GetRandomString(),
                    amount = new Money()
                    {
                        microAmount = 50000000L
                    },
                    deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD
                };

                BudgetOperation budgetOperation = new BudgetOperation()
                {
                    @operator = Operator.ADD,
                    operand = budget
                };

                return budgetService.mutate(new BudgetOperation[]
                {
                    budgetOperation
                }).value[0];
            }
        }
    }
}

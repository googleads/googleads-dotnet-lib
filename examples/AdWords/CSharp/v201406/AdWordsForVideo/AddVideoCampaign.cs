// Copyright 2014, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201406;

using System;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201406 {

  /// <summary>
  /// This code example illustrates how to create a video campaign.
  ///
  /// Tags: VideoCampaignService.mutate, BudgetService.mutate
  /// </summary>
  /// <remarks>AdWords for Video API is a Beta feature.</remarks>
  public class AddVideoCampaign : ExampleBase {

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example illustrates how to create a video campaign.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddVideoCampaign codeExample = new AddVideoCampaign();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new AdWordsUser());
      } catch (Exception ex) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(ex));
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    public void Run(AdWordsUser user) {
      // Get the BudgetService.
      BudgetService budgetService =
          (BudgetService) user.GetService(AdWordsService.v201406.BudgetService);

      // Get the VideoCampaignService.
      VideoCampaignService videoCampaignService =
          (VideoCampaignService) user.GetService(AdWordsService.v201406.VideoCampaignService);

      // Create the campaign budget.
      Budget budget = new Budget();
      budget.name = "Interplanetary Cruise Budget #" + ExampleUtilities.GetRandomString();
      budget.period = BudgetBudgetPeriod.DAILY;
      budget.deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD;
      budget.amount = new Money();
      budget.amount.microAmount = 500000;

      BudgetOperation budgetOperation = new BudgetOperation();
      budgetOperation.@operator = Operator.ADD;
      budgetOperation.operand = budget;

      try {
        BudgetReturnValue budgetRetval = budgetService.mutate(new BudgetOperation[] { budgetOperation });
        budget = budgetRetval.value[0];
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to add shared budget.", ex);
      }

      // Create video campaign.
      VideoCampaign videoCampaign = new VideoCampaign();
      videoCampaign.name = ("Interplanetary Cruise #" + ExampleUtilities.GetRandomString());
      videoCampaign.status = VideoCampaignStatus.PAUSED;
      videoCampaign.budgetId = budget.budgetId;

      // You can optionally provide these field(s). The dates will be
      // interpreted using the account's timezone.
      videoCampaign.startDate = DateTime.Now.AddDays(1).ToString("yyyyMMdd");

      try {
        // Create operations.
        VideoCampaignOperation operation = new VideoCampaignOperation();
        operation.operand = videoCampaign;
        operation.@operator = Operator.ADD;

        VideoCampaignOperation[] operations = new VideoCampaignOperation[] { operation };

        // Add video campaigns.
        VideoCampaignReturnValue retval = videoCampaignService.mutate(operations);

        // Display video campaigns.
        if (retval != null && retval.value != null && retval.value.Length > 0) {
          VideoCampaign newVideoCampaign = retval.value[0];
          Console.WriteLine("Campaign with name '{0}' and id = {1} was added.",
              newVideoCampaign.name, newVideoCampaign.id);
        } else {
          Console.WriteLine("No video campaigns were added.");
        }
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to add video campaign.", ex);
      }
    }
  }
}
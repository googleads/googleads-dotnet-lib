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
using Google.Api.Ads.AdWords.v201601;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201601 {
  /// <summary>
  /// This code example gets the changes in the account during the last 24
  /// hours.
  /// </summary>
  public class GetAccountChanges : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      GetAccountChanges codeExample = new GetAccountChanges();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new AdWordsUser());
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
        return "This code example gets the changes in the account during the last 24 hours.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    public void Run(AdWordsUser user) {
      // Get the CustomerSyncService.
      CustomerSyncService customerSyncService =
          (CustomerSyncService) user.GetService(AdWordsService.v201601.
              CustomerSyncService);

      // The date time string should be of the form  yyyyMMdd HHmmss zzz
      string minDateTime = DateTime.Now.AddDays(-1).ToUniversalTime().ToString(
          "yyyyMMdd HHmmss") + " UTC";
      string maxDateTime = DateTime.Now.ToUniversalTime().ToString("yyyyMMdd HHmmss") +
          " UTC";

      // Create date time range.
      DateTimeRange dateTimeRange = new DateTimeRange();
      dateTimeRange.min = minDateTime;
      dateTimeRange.max = maxDateTime;

      try {
        // Create the selector.
        CustomerSyncSelector selector = new CustomerSyncSelector();
        selector.dateTimeRange = dateTimeRange;
        selector.campaignIds = GetAllCampaignIds(user);

        // Get all account changes for campaign.
        CustomerChangeData accountChanges = customerSyncService.get(selector);

        // Display the changes.
        if (accountChanges != null && accountChanges.changedCampaigns != null) {
          Console.WriteLine("Displaying changes up to: {0}", accountChanges.lastChangeTimestamp);
          foreach (CampaignChangeData campaignChanges in accountChanges.changedCampaigns) {
            Console.WriteLine("Campaign with id \"{0}\" was changed:", campaignChanges.campaignId);
            Console.WriteLine("  Campaign changed status: {0}",
                campaignChanges.campaignChangeStatus);
            if (campaignChanges.campaignChangeStatus != ChangeStatus.NEW) {
              Console.WriteLine("  Added ad extensions: {0}", GetFormattedList(
                  campaignChanges.addedAdExtensions));
              Console.WriteLine("  Added campaign criteria: {0}",
                  GetFormattedList(campaignChanges.addedCampaignCriteria));
              Console.WriteLine("  Removed ad extensions: {0}",
                  GetFormattedList(campaignChanges.removedAdExtensions));
              Console.WriteLine("  Removed campaign criteria: {0}",
                  GetFormattedList(campaignChanges.removedCampaignCriteria));

              if (campaignChanges.changedAdGroups != null) {
                foreach (AdGroupChangeData adGroupChanges in campaignChanges.changedAdGroups) {
                  Console.WriteLine("  Ad group with id \"{0}\" was changed:",
                      adGroupChanges.adGroupId);
                  Console.WriteLine("    Ad group changed status: {0}",
                      adGroupChanges.adGroupChangeStatus);
                  if (adGroupChanges.adGroupChangeStatus != ChangeStatus.NEW) {
                    Console.WriteLine("    Ads changed: {0}",
                        GetFormattedList(adGroupChanges.changedAds));
                    Console.WriteLine("    Criteria changed: {0}",
                        GetFormattedList(adGroupChanges.changedCriteria));
                    Console.WriteLine("    Criteria removed: {0}",
                        GetFormattedList(adGroupChanges.removedCriteria));
                  }
                }
              }
            }
            Console.WriteLine();
          }
        } else {
          Console.WriteLine("No account changes were found.");
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to get account changes.", e);
      }
    }

    /// <summary>
    /// Formats a list of ids as a comma separated string.
    /// </summary>
    /// <param name="ids">The list of ids.</param>
    /// <returns>The comma separed formatted string, enclosed in square braces.
    /// </returns>
    private string GetFormattedList(long[] ids) {
      StringBuilder builder = new StringBuilder();
      if (ids != null) {
        foreach (long id in ids) {
          builder.AppendFormat("{0}, ", id);
        }
      }
      return "[" + builder.ToString().TrimEnd(',', ' ') + "]";
    }

    /// <summary>
    /// Gets all campaign ids in the account.
    /// </summary>
    /// <param name="user">The user for which campaigns are retrieved.</param>
    /// <returns>The list of campaign ids.</returns>
    private long[] GetAllCampaignIds(AdWordsUser user) {
      // Get the CampaignService.
      CampaignService campaignService =
          (CampaignService) user.GetService(AdWordsService.v201601.CampaignService);

      List<long> allCampaigns = new List<long>();

      // Create the selector.
      Selector selector = new Selector() {
        fields = new string[] { Campaign.Fields.Id }
      };

      // Get all campaigns.
      CampaignPage page = campaignService.get(selector);

      // Return the results.
      if (page != null && page.entries != null) {
        foreach (Campaign campaign in page.entries) {
          allCampaigns.Add(campaign.id);
        }
      }
      return allCampaigns.ToArray();
    }
  }
}

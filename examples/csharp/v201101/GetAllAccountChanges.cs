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
using Google.Api.Ads.AdWords.v201101;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201101 {
  /// <summary>
  /// This code example gets all account changes between the two dates
  /// specified, for all campaigns.
  ///
  /// Tags: CustomerSyncService.get
  /// </summary>
  class GetAllAccountChanges : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all account changes between the two dates specified, " +
            "for all campaigns.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetAllAccountChanges();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the CustomerSyncService.
      CustomerSyncService customerSyncService =
          (CustomerSyncService) user.GetService(AdWordsService.v201101.
              CustomerSyncService);

      // The date time string should be of the form  yyyyMMdd HHmmss zzz
      string minDateTime = _T("INSERT_START_DATE_TIME_HERE (yyyyMMdd HHmmss zzz format)");
      string maxDateTime = _T("INSERT_END_DATE_TIME_HERE (yyyyMMdd HHmmss zzz format)");

      // Create date time range.
      DateTimeRange dateTimeRange = new DateTimeRange();
      dateTimeRange.min = minDateTime;
      dateTimeRange.max = maxDateTime;

      try {
        // Create selector.
        CustomerSyncSelector selector = new CustomerSyncSelector();
        selector.dateTimeRange = dateTimeRange;
        selector.campaignIds = GetAllCampaignIds(user);

        // Get all account changes for campaign.
        CustomerChangeData accountChanges = customerSyncService.get(selector);

        // Display changes.
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
              Console.WriteLine("  Added campaign targeting: {0}",
                  campaignChanges.campaignTargetingChanged? "yes" : "no");
              Console.WriteLine("  Deleted ad extensions: {0}",
                  GetFormattedList(campaignChanges.deletedAdExtensions));
              Console.WriteLine("  Deleted campaign criteria: {0}",
                  GetFormattedList(campaignChanges.deletedCampaignCriteria));

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
                    Console.WriteLine("    Criteria deleted: {0}",
                        GetFormattedList(adGroupChanges.deletedCriteria));
                  }
                }
              }
            }
            Console.WriteLine();
          }
        } else {
          Console.WriteLine("No account changes were found.");;
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to get account changes. Exception says \"{0}\"", ex.Message);
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
          (CampaignService) user.GetService(AdWordsService.v201101.CampaignService);

      List<long> allCampaigns = new List<long>();

      // Create a selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"Id"};

      // Get all campaigns.
      CampaignPage page = campaignService.get(selector);

      if (page != null && page.entries != null) {
        if (page.entries.Length > 0) {
          foreach (Campaign campaign in page.entries) {
            allCampaigns.Add(campaign.id);
          }
        }
      }
      return allCampaigns.ToArray();
    }
  }
}

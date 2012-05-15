// Copyright 2012, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201109_1;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109_1 {
  /// <summary>
  /// This code example gets the changes in the account during the last 24
  /// hours.
  ///
  /// Tags: CustomerSyncService.get
  /// </summary>
  public class GetAccountChanges : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      ExampleBase codeExample = new GetAccountChanges();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new AdWordsUser(), codeExample.GetParameters(), Console.Out);
      } catch (Exception ex) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(ex));
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
    /// Gets the list of parameter names required to run this code example.
    /// </summary>
    /// <returns>
    /// A list of parameter names for this code example.
    /// </returns>
    public override string[] GetParameterNames() {
      return new string[] {};
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="parameters">The parameters for running the code
    /// example.</param>
    /// <param name="writer">The stream writer to which script output should be
    /// written.</param>
    public override void Run(AdWordsUser user, Dictionary<string, string> parameters,
        TextWriter writer) {
      // Get the CustomerSyncService.
      CustomerSyncService customerSyncService =
          (CustomerSyncService) user.GetService(AdWordsService.v201109_1.
              CustomerSyncService);

      // The date time string should be of the form  yyyyMMdd HHmmss zzz
      string minDateTime = DateTime.Now.AddDays(-1).ToUniversalTime().ToString("yyyyMMdd HHmmss")
          + " UTC";
      string maxDateTime = DateTime.Now.ToUniversalTime().ToString("yyyyMMdd HHmmss") + " UTC";

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
          writer.WriteLine("Displaying changes up to: {0}", accountChanges.lastChangeTimestamp);
          foreach (CampaignChangeData campaignChanges in accountChanges.changedCampaigns) {
            writer.WriteLine("Campaign with id \"{0}\" was changed:", campaignChanges.campaignId);
            writer.WriteLine("  Campaign changed status: {0}",
                campaignChanges.campaignChangeStatus);
            if (campaignChanges.campaignChangeStatus != ChangeStatus.NEW) {
              writer.WriteLine("  Added ad extensions: {0}", GetFormattedList(
                  campaignChanges.addedAdExtensions));
              writer.WriteLine("  Added campaign criteria: {0}",
                  GetFormattedList(campaignChanges.addedCampaignCriteria));
              writer.WriteLine("  Added campaign targeting: {0}",
                  campaignChanges.campaignTargetingChanged? "yes" : "no");
              writer.WriteLine("  Deleted ad extensions: {0}",
                  GetFormattedList(campaignChanges.deletedAdExtensions));
              writer.WriteLine("  Deleted campaign criteria: {0}",
                  GetFormattedList(campaignChanges.deletedCampaignCriteria));

              if (campaignChanges.changedAdGroups != null) {
                foreach (AdGroupChangeData adGroupChanges in campaignChanges.changedAdGroups) {
                  writer.WriteLine("  Ad group with id \"{0}\" was changed:",
                      adGroupChanges.adGroupId);
                  writer.WriteLine("    Ad group changed status: {0}",
                      adGroupChanges.adGroupChangeStatus);
                  if (adGroupChanges.adGroupChangeStatus != ChangeStatus.NEW) {
                    writer.WriteLine("    Ads changed: {0}",
                        GetFormattedList(adGroupChanges.changedAds));
                    writer.WriteLine("    Criteria changed: {0}",
                        GetFormattedList(adGroupChanges.changedCriteria));
                    writer.WriteLine("    Criteria deleted: {0}",
                        GetFormattedList(adGroupChanges.deletedCriteria));
                  }
                }
              }
            }
            writer.WriteLine();
          }
        } else {
          writer.WriteLine("No account changes were found.");;
        }
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to get account changes.", ex);
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
          (CampaignService) user.GetService(AdWordsService.v201109_1.CampaignService);

      List<long> allCampaigns = new List<long>();

      // Create the selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"Id"};

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

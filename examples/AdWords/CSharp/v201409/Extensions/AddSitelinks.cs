// Copyright 2015, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201409;

using System;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201409 {

  /// <summary>
  /// This code example adds sitelinks to a campaign. To create a campaign,
  /// run AddCampaign.cs.
  ///
  /// Tags: CampaignExtensionSettingService.mutate
  /// </summary>
  public class AddSitelinks : ExampleBase {

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddSitelinks codeExample = new AddSitelinks();
      Console.WriteLine(codeExample.Description);
      try {
        long campaignId = long.Parse("INSERT_CAMPAIGN_ID_HERE");
        codeExample.Run(new AdWordsUser(), campaignId);
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
        return "This code example adds sitelinks to a campaign. To create a campaign, run " +
            "AddCampaign.cs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="campaignId">Id of the campaign to which sitelinks will
    /// be added.</param>
    public void Run(AdWordsUser user, long campaignId) {
      // Get the CampaignExtensionSettingService.
      CampaignExtensionSettingService campaignExtensionSettingService =
          (CampaignExtensionSettingService) user.GetService(
               AdWordsService.v201409.CampaignExtensionSettingService);

      // Create your sitelinks.
      SitelinkFeedItem sitelink1 = new SitelinkFeedItem() {
        sitelinkText = "Store Hours",
        sitelinkFinalUrls = new string[] { "http://www.example.com/storehours" }
      };

      // Show the Thanksgiving specials link only from 20 - 27 Nov.
      SitelinkFeedItem sitelink2 = new SitelinkFeedItem() {
        sitelinkText = "Thanksgiving Specials",
        sitelinkFinalUrls = new string[] { "http://www.example.com/thanksgiving" },
        startTime = string.Format("{0}1120 000000 EST", DateTime.Now.Year),
        endTime = string.Format("{0}1127 235959 EST",  DateTime.Now.Year)
      };

      // Show the wifi details primarily for high end mobile users.
      SitelinkFeedItem sitelink3 = new SitelinkFeedItem() {
        sitelinkText = "Wifi available",
        sitelinkFinalUrls = new string[] { "http://www.example.com/mobile/wifi" },
        devicePreference = new FeedItemDevicePreference() {
          // See https://developers.google.com/adwords/api/docs/appendix/platforms
          // for device criteria IDs.
          devicePreference = 30001
        }
      };

      // Show the happy hours link only during Mon - Fri 6PM to 9PM.
      SitelinkFeedItem sitelink4 = new SitelinkFeedItem() {
        sitelinkText = "Happy hours",
        sitelinkFinalUrls = new string[] { "http://www.example.com/happyhours" },
        scheduling = new FeedItemSchedule[] {
            new FeedItemSchedule() {
                dayOfWeek = AdWords.v201409.DayOfWeek.MONDAY,
                startHour = 18,
                startMinute = MinuteOfHour.ZERO,
                endHour = 21,
                endMinute = MinuteOfHour.ZERO
            },
            new FeedItemSchedule() {
                dayOfWeek = AdWords.v201409.DayOfWeek.TUESDAY,
                startHour = 18,
                startMinute = MinuteOfHour.ZERO,
                endHour = 21,
                endMinute = MinuteOfHour.ZERO
            },
            new FeedItemSchedule() {
                dayOfWeek = AdWords.v201409.DayOfWeek.WEDNESDAY,
                startHour = 18,
                startMinute = MinuteOfHour.ZERO,
                endHour = 21,
                endMinute = MinuteOfHour.ZERO
            },
            new FeedItemSchedule() {
                dayOfWeek = AdWords.v201409.DayOfWeek.THURSDAY,
                startHour = 18,
                startMinute = MinuteOfHour.ZERO,
                endHour = 21,
                endMinute = MinuteOfHour.ZERO
            },
            new FeedItemSchedule() {
                dayOfWeek = AdWords.v201409.DayOfWeek.FRIDAY,
                startHour = 18,
                startMinute = MinuteOfHour.ZERO,
                endHour = 21,
                endMinute = MinuteOfHour.ZERO
            }
        }
      };

      // Create your campaign extension settings. This associates the sitelinks
      // to your campaign.
      CampaignExtensionSetting campaignExtensionSetting = new CampaignExtensionSetting();
      campaignExtensionSetting.campaignId = campaignId;
      campaignExtensionSetting.extensionType = FeedType.SITELINK;
      campaignExtensionSetting.extensionSetting = new ExtensionSetting() {
        extensions = new ExtensionFeedItem[] {
            sitelink1, sitelink2, sitelink3, sitelink4
        }
      };

      CampaignExtensionSettingOperation operation = new CampaignExtensionSettingOperation() {
        operand = campaignExtensionSetting,
        @operator = Operator.ADD
      };

      try {
        // Add the extensions.
        CampaignExtensionSettingReturnValue retVal = campaignExtensionSettingService.mutate(
            new CampaignExtensionSettingOperation[] { operation });

        // Display the results.
        if (retVal.value != null && retVal.value.Length > 0) {
          CampaignExtensionSetting newExtensionSetting = retVal.value[0];
          Console.WriteLine("Extension setting with type = {0} was added to campaign ID {1}.",
              newExtensionSetting.extensionType, newExtensionSetting.campaignId);
        } else {
          Console.WriteLine("No extension settings were created.");
        }
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to create extension settings.", ex);
      }
    }
  }
}

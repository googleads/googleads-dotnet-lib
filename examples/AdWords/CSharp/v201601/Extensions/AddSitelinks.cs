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

using DayOfWeek = Google.Api.Ads.AdWords.v201601.DayOfWeek;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201601 {

  /// <summary>
  /// This code example adds sitelinks to a campaign. To create a campaign,
  /// run AddCampaign.cs.
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
               AdWordsService.v201601.CampaignExtensionSettingService);

      CustomerService customerService = (CustomerService) user.GetService(
          AdWordsService.v201601.CustomerService);
      Customer customer = customerService.get();

      List<ExtensionFeedItem> extensions = new List<ExtensionFeedItem>();

      // Create your sitelinks.
      SitelinkFeedItem sitelink1 = new SitelinkFeedItem() {
        sitelinkText = "Store Hours",
        sitelinkFinalUrls = new UrlList() {
          urls = new string[] { "http://www.example.com/storehours" }
        }
      };
      extensions.Add(sitelink1);

      DateTime startOfThanksGiving = new DateTime(DateTime.Now.Year, 11, 20, 0, 0, 0);
      DateTime endOfThanksGiving = new DateTime(DateTime.Now.Year, 11, 27, 23, 59, 59);

      // Add check to make sure we don't create a sitelink with end date in the
      // past.
      if (DateTime.Now < endOfThanksGiving) {
        // Show the Thanksgiving specials link only from 20 - 27 Nov.
        SitelinkFeedItem sitelink2 = new SitelinkFeedItem() {
          sitelinkText = "Thanksgiving Specials",
          sitelinkFinalUrls = new UrlList() {
            urls = new string[] { "http://www.example.com/thanksgiving" }
          },
          startTime = string.Format("{0} {1}", startOfThanksGiving.ToString("yyyyMMdd HHmmss"),
              customer.dateTimeZone),
          endTime = string.Format("{0} {1}", endOfThanksGiving.ToString("yyyyMMdd HHmmss"),
              customer.dateTimeZone)
        };
        extensions.Add(sitelink2);
      }
      // Show the wifi details primarily for high end mobile users.
      SitelinkFeedItem sitelink3 = new SitelinkFeedItem() {
        sitelinkText = "Wifi available",
        sitelinkFinalUrls = new UrlList() {
          urls = new string[] { "http://www.example.com/mobile/wifi" }
        },
        devicePreference = new FeedItemDevicePreference() {
          // See https://developers.google.com/adwords/api/docs/appendix/platforms
          // for device criteria IDs.
          devicePreference = 30001
        }
      };
      extensions.Add(sitelink3);

      // Show the happy hours link only during Mon - Fri 6PM to 9PM.
      SitelinkFeedItem sitelink4 = new SitelinkFeedItem() {
        sitelinkText = "Happy hours",
        sitelinkFinalUrls = new UrlList() {
          urls = new string[] { "http://www.example.com/happyhours" }
        },
        scheduling = new FeedItemSchedule[] {
            new FeedItemSchedule() {
                dayOfWeek = DayOfWeek.MONDAY,
                startHour = 18,
                startMinute = MinuteOfHour.ZERO,
                endHour = 21,
                endMinute = MinuteOfHour.ZERO
            },
            new FeedItemSchedule() {
                dayOfWeek = DayOfWeek.TUESDAY,
                startHour = 18,
                startMinute = MinuteOfHour.ZERO,
                endHour = 21,
                endMinute = MinuteOfHour.ZERO
            },
            new FeedItemSchedule() {
                dayOfWeek = DayOfWeek.WEDNESDAY,
                startHour = 18,
                startMinute = MinuteOfHour.ZERO,
                endHour = 21,
                endMinute = MinuteOfHour.ZERO
            },
            new FeedItemSchedule() {
                dayOfWeek = DayOfWeek.THURSDAY,
                startHour = 18,
                startMinute = MinuteOfHour.ZERO,
                endHour = 21,
                endMinute = MinuteOfHour.ZERO
            },
            new FeedItemSchedule() {
                dayOfWeek = DayOfWeek.FRIDAY,
                startHour = 18,
                startMinute = MinuteOfHour.ZERO,
                endHour = 21,
                endMinute = MinuteOfHour.ZERO
            }
        }
      };
      extensions.Add(sitelink4);

      // Create your campaign extension settings. This associates the sitelinks
      // to your campaign.
      CampaignExtensionSetting campaignExtensionSetting = new CampaignExtensionSetting();
      campaignExtensionSetting.campaignId = campaignId;
      campaignExtensionSetting.extensionType = FeedType.SITELINK;
      campaignExtensionSetting.extensionSetting = new ExtensionSetting() {
        extensions = extensions.ToArray()
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
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to create extension settings.", e);
      }
    }
  }
}

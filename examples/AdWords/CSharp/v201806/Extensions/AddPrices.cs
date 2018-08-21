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
using Google.Api.Ads.AdWords.v201806;

using System;
using System.Collections.Generic;

using DayOfWeek = Google.Api.Ads.AdWords.v201806.DayOfWeek;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201806
{
    /// <summary>
    /// This code example adds a price extension and associates it with an
    /// account. Campaign targeting is also set using the specified campaign ID.
    /// To get campaigns, run AddCampaigns.cs.
    /// </summary>
    public class AddPrices : ExampleBase
    {
        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            AddPrices codeExample = new AddPrices();
            Console.WriteLine(codeExample.Description);
            try
            {
                long campaignId = long.Parse("INSERT_CAMPAIGN_ID_HERE");
                codeExample.Run(new AdWordsUser(), campaignId);
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
                return "This code example adds a price extension and associates it with an " +
                    "account. Campaign targeting is also set using the specified campaign ID. " +
                    "To get campaigns, run AddCampaigns.cs.";
            }
        }

        /// <summary>
        /// Runs the code example.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="campaignId">Id of the campaign to which sitelinks will
        /// be added.</param>
        public void Run(AdWordsUser user, long campaignId)
        {
            using (CustomerExtensionSettingService customerExtensionSettingService =
                (CustomerExtensionSettingService) user.GetService(AdWordsService.v201806
                    .CustomerExtensionSettingService))
            {
                // Create the price extension feed item.
                PriceFeedItem priceFeedItem = new PriceFeedItem()
                {
                    priceExtensionType = PriceExtensionType.SERVICES,

                    // Price qualifier is optional.
                    priceQualifier = PriceExtensionPriceQualifier.FROM,
                    trackingUrlTemplate = "http://tracker.example.com/?u={lpurl}",
                    language = "en",

                    campaignTargeting = new FeedItemCampaignTargeting()
                    {
                        TargetingCampaignId = campaignId,
                    },
                    scheduling = new FeedItemSchedule[]
                    {
                        new FeedItemSchedule()
                        {
                            dayOfWeek = DayOfWeek.SATURDAY,
                            startHour = 10,
                            startMinute = MinuteOfHour.ZERO,
                            endHour = 22,
                            endMinute = MinuteOfHour.ZERO
                        },
                        new FeedItemSchedule()
                        {
                            dayOfWeek = DayOfWeek.SUNDAY,
                            startHour = 10,
                            startMinute = MinuteOfHour.ZERO,
                            endHour = 18,
                            endMinute = MinuteOfHour.ZERO
                        }
                    }
                };

                // To create a price extension, at least three table rows are needed.
                List<PriceTableRow> priceTableRows = new List<PriceTableRow>();
                string currencyCode = "USD";
                priceTableRows.Add(CreatePriceTableRow("Scrubs", "Body Scrub, Salt Scrub",
                    "http://www.example.com/scrubs", "http://m.example.com/scrubs", 60000000,
                    currencyCode, PriceExtensionPriceUnit.PER_HOUR));
                priceTableRows.Add(CreatePriceTableRow("Hair Cuts", "Once a month",
                    "http://www.example.com/haircuts", "http://m.example.com/haircuts", 75000000,
                    currencyCode, PriceExtensionPriceUnit.PER_MONTH));
                priceTableRows.Add(CreatePriceTableRow("Skin Care Package", "Four times a month",
                    "http://www.example.com/skincarepackage", null, 250000000, currencyCode,
                    PriceExtensionPriceUnit.PER_MONTH));

                priceFeedItem.tableRows = priceTableRows.ToArray();

                // Create your campaign extension settings. This associates the sitelinks
                // to your campaign.
                CustomerExtensionSetting customerExtensionSetting = new CustomerExtensionSetting()
                {
                    extensionType = FeedType.PRICE,
                    extensionSetting = new ExtensionSetting()
                    {
                        extensions = new ExtensionFeedItem[]
                        {
                            priceFeedItem
                        }
                    }
                };

                CustomerExtensionSettingOperation operation =
                    new CustomerExtensionSettingOperation()
                    {
                        operand = customerExtensionSetting,
                        @operator = Operator.ADD
                    };

                try
                {
                    // Add the extensions.
                    CustomerExtensionSettingReturnValue retVal =
                        customerExtensionSettingService.mutate(
                            new CustomerExtensionSettingOperation[]
                            {
                                operation
                            });
                    if (retVal.value != null && retVal.value.Length > 0)
                    {
                        CustomerExtensionSetting newExtensionSetting = retVal.value[0];
                        Console.WriteLine("Extension setting with type '{0}' was added.",
                            newExtensionSetting.extensionType);
                    }
                    else
                    {
                        Console.WriteLine("No extension settings were created.");
                    }
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to create extension settings.",
                        e);
                }
            }
        }

        /// <summary>
        /// Creates a price table row.
        /// </summary>
        /// <param name="header">The row header.</param>
        /// <param name="description">The description text.</param>
        /// <param name="finalUrl">The final URL.</param>
        /// <param name="finalMobileUrl">The mobile final URL, or null if this field
        /// should not be set.</param>
        /// <param name="priceInMicros">The price in micros.</param>
        /// <param name="currencyCode">The currency code.</param>
        /// <param name="priceUnit">The price unit.</param>
        /// <returns>A price table row for creating price extension.</returns>
        private static PriceTableRow CreatePriceTableRow(string header, string description,
            string finalUrl, string finalMobileUrl, long priceInMicros, string currencyCode,
            PriceExtensionPriceUnit priceUnit)
        {
            PriceTableRow retval = new PriceTableRow()
            {
                header = header,
                description = description,
                finalUrls = new UrlList()
                {
                    urls = new string[]
                    {
                        finalUrl
                    }
                },
                price = new MoneyWithCurrency()
                {
                    currencyCode = currencyCode,
                    money = new Money()
                    {
                        microAmount = priceInMicros
                    }
                },
                priceUnit = priceUnit
            };

            // Optional: set the mobile final URLs.
            if (!string.IsNullOrEmpty(finalMobileUrl))
            {
                retval.finalMobileUrls = new UrlList()
                {
                    urls = new string[]
                    {
                        finalMobileUrl
                    }
                };
            }

            return retval;
        }

    }
}

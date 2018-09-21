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
    /// This code example illustrates how to update an ad group, setting its
    /// status to 'PAUSED', and its CPC bid to a new value if specified.
    /// To create an ad group, run AddAdGroup.cs.
    /// </summary>
    public class UpdateAdGroup : ExampleBase
    {
        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            UpdateAdGroup codeExample = new UpdateAdGroup();
            Console.WriteLine(codeExample.Description);
            try
            {
                long adGroupId = long.Parse("INSERT_ADGROUP_ID_HERE");
                long? bidMicroAmount = null;

                // Optional: Provide a cpc bid for the ad group, in micro amounts.
                long tempVal = 0;
                if (long.TryParse("INSERT_CPC_BID_IN_MICROS_HERE", out tempVal))
                {
                    bidMicroAmount = tempVal;
                }

                codeExample.Run(new AdWordsUser(), adGroupId, bidMicroAmount);
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
                return " This code example illustrates how to update an ad group, setting its " +
                    "status to 'PAUSED', and its CPC bid to a new value if specified. To create " +
                    "an ad group, run AddAdGroup.cs.";
            }
        }

        /// <summary>
        /// Runs the code example.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="adGroupId">Id of the ad group to be updated.</param>
        /// <param name="bidMicroAmount">The CPC bid amount in micros.</param>
        public void Run(AdWordsUser user, long adGroupId, long? bidMicroAmount)
        {
            using (AdGroupService adGroupService =
                (AdGroupService) user.GetService(AdWordsService.v201809.AdGroupService))
            {
                // Create an ad group with the specified ID.
                AdGroup adGroup = new AdGroup
                {
                    id = adGroupId,

                    // Pause the ad group.
                    status = AdGroupStatus.PAUSED
                };

                // Update the CPC bid if specified.
                if (bidMicroAmount != null)
                {
                    BiddingStrategyConfiguration biddingStrategyConfiguration =
                        new BiddingStrategyConfiguration();
                    Money cpcBidMoney = new Money
                    {
                        microAmount = bidMicroAmount.Value
                    };
                    CpcBid cpcBid = new CpcBid
                    {
                        bid = cpcBidMoney
                    };
                    biddingStrategyConfiguration.bids = new Bids[]
                    {
                        cpcBid
                    };
                    adGroup.biddingStrategyConfiguration = biddingStrategyConfiguration;
                }

                // Create the operation.
                AdGroupOperation operation = new AdGroupOperation
                {
                    @operator = Operator.SET,
                    operand = adGroup
                };

                try
                {
                    // Update the ad group.
                    AdGroupReturnValue retVal = adGroupService.mutate(new AdGroupOperation[]
                    {
                        operation
                    });

                    // Display the results.
                    if (retVal != null && retVal.value != null && retVal.value.Length > 0)
                    {
                        AdGroup adGroupResult = retVal.value[0];
                        BiddingStrategyConfiguration bsConfig =
                            adGroupResult.biddingStrategyConfiguration;

                        // Find the CpcBid in the bidding strategy configuration's bids collection.
                        long cpcBidMicros = 0L;
                        if (bsConfig != null && bsConfig.bids != null)
                        {
                            foreach (Bids bid in bsConfig.bids)
                            {
                                if (bid is CpcBid)
                                {
                                    cpcBidMicros = ((CpcBid) bid).bid.microAmount;
                                    break;
                                }
                            }
                        }

                        Console.WriteLine(
                            "Ad group with ID {0} and name '{1}' updated to have status '{2}'" +
                            " and CPC bid {3}", adGroupResult.id, adGroupResult.name,
                            adGroupResult.status, cpcBidMicros);
                    }
                    else
                    {
                        Console.WriteLine("No ad groups were updated.");
                    }
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to update ad group.", e);
                }
            }
        }
    }
}

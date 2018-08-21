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

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201806
{
    /// <summary>
    /// This code example pauses a given ad. To list all ads, run GetExpandedTextAds.cs.
    /// </summary>
    public class PauseAd : ExampleBase
    {
        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            PauseAd codeExample = new PauseAd();
            Console.WriteLine(codeExample.Description);
            try
            {
                long adGroupId = long.Parse("INSERT_ADGROUP_ID_HERE");
                long adId = long.Parse("INSERT_AD_ID_HERE");
                codeExample.Run(new AdWordsUser(), adGroupId, adId);
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
                return "This code example pauses a given ad. To list all ads, " +
                    "run GetExpandedTextAds.cs.";
            }
        }

        /// <summary>
        /// Runs the code example.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="adGroupId">Id of the ad group that contains the ad.
        /// </param>
        /// <param name="adId">Id of the ad to be paused.</param>
        public void Run(AdWordsUser user, long adGroupId, long adId)
        {
            using (AdGroupAdService adGroupAdService =
                (AdGroupAdService) user.GetService(AdWordsService.v201806.AdGroupAdService))
            {
                AdGroupAdStatus status = AdGroupAdStatus.PAUSED;

                // Create the ad group ad.
                AdGroupAd adGroupAd = new AdGroupAd
                {
                    status = status,
                    adGroupId = adGroupId,

                    ad = new Ad
                    {
                        id = adId
                    }
                };

                // Create the operation.
                AdGroupAdOperation adGroupAdOperation = new AdGroupAdOperation
                {
                    @operator = Operator.SET,
                    operand = adGroupAd
                };

                try
                {
                    // Update the ad.
                    AdGroupAdReturnValue retVal = adGroupAdService.mutate(new AdGroupAdOperation[]
                    {
                        adGroupAdOperation
                    });

                    // Display the results.
                    if (retVal != null && retVal.value != null && retVal.value.Length > 0)
                    {
                        AdGroupAd pausedAdGroupAd = retVal.value[0];
                        Console.WriteLine("Ad with id \"{0}\" and ad group id \"{1}\"was paused.",
                            pausedAdGroupAd.ad.id, pausedAdGroupAd.adGroupId);
                    }
                    else
                    {
                        Console.WriteLine("No ads were paused.");
                    }
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to pause ad.", e);
                }
            }
        }
    }
}

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
    /// This code example illustrates how to create a text ad with ad parameters.
    /// To add an ad group, run AddAdGroup.cs. To add a keyword, run
    /// run AddKeyword.cs.
    /// </summary>
    public class SetAdParameters : ExampleBase
    {
        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            SetAdParameters codeExample = new SetAdParameters();
            Console.WriteLine(codeExample.Description);
            try
            {
                long adGroupId = long.Parse("INSERT_ADGROUP_ID_HERE");
                long criterionId = long.Parse("INSERT_CRITERION_ID_HERE");
                codeExample.Run(new AdWordsUser(), adGroupId, criterionId);
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
                return "This code example illustrates how to create a text ad with ad parameters." +
                    " To add an ad group, run AddAdGroup.cs. To add a keyword, run AddKeyword.vb.";
            }
        }

        /// <summary>
        /// Runs the code example.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="adGroupId">Id of the ad group that contains the criterion.
        /// </param>
        /// <param name="criterionId">Id of the keyword for which the ad
        /// parameters are set.</param>
        public void Run(AdWordsUser user, long adGroupId, long criterionId)
        {
            using (AdGroupAdService adGroupAdService =
                (AdGroupAdService) user.GetService(AdWordsService.v201806.AdGroupAdService))
                using (AdParamService adParamService =
                    (AdParamService) user.GetService(AdWordsService.v201806.AdParamService))
                {
                    // Create the expanded text ad.
                    ExpandedTextAd expandedTextAd = new ExpandedTextAd
                    {
                        headlinePart1 = "Mars Cruises",
                        headlinePart2 = "Low-gravity fun for {param1:cheap}.",
                        description = "Only {param2:a few} seats left!",
                        finalUrls = new string[]
                        {
                            "http://www.example.com"
                        }
                    };

                    AdGroupAd adOperand = new AdGroupAd
                    {
                        adGroupId = adGroupId,
                        status = AdGroupAdStatus.ENABLED,
                        ad = expandedTextAd
                    };

                    // Create the operation.
                    AdGroupAdOperation adOperation = new AdGroupAdOperation
                    {
                        operand = adOperand,
                        @operator = Operator.ADD
                    };


                    // Create the text ad.
                    AdGroupAdReturnValue retVal = adGroupAdService.mutate(new AdGroupAdOperation[]
                    {
                        adOperation
                    });

                    // Display the results.
                    if (retVal != null && retVal.value != null && retVal.value.Length > 0)
                    {
                        Console.WriteLine("Expanded text ad with id ='{0}' was successfully added.",
                            retVal.value[0].ad.id);
                    }
                    else
                    {
                        throw new System.ApplicationException(
                            "Failed to create expanded text ads.");
                    }

                    // Create the ad param for price.
                    AdParam priceParam = new AdParam
                    {
                        adGroupId = adGroupId,
                        criterionId = criterionId,
                        paramIndex = 1,
                        insertionText = "$100"
                    };

                    // Create the ad param for seats.
                    AdParam seatParam = new AdParam
                    {
                        adGroupId = adGroupId,
                        criterionId = criterionId,
                        paramIndex = 2,
                        insertionText = "50"
                    };

                    // Create the operations.
                    AdParamOperation priceOperation = new AdParamOperation
                    {
                        @operator = Operator.SET,
                        operand = priceParam
                    };

                    AdParamOperation seatOperation = new AdParamOperation
                    {
                        @operator = Operator.SET,
                        operand = seatParam
                    };

                    try
                    {
                        // Set the ad parameters.
                        AdParam[] newAdParams = adParamService.mutate(new AdParamOperation[]
                        {
                            priceOperation,
                            seatOperation
                        });

                        // Display the results.
                        if (newAdParams != null)
                        {
                            Console.WriteLine("Ad parameters were successfully updated.");
                        }
                        else
                        {
                            Console.WriteLine("No ad parameters were set.");
                        }
                    }
                    catch (Exception e)
                    {
                        throw new System.ApplicationException("Failed to set ad parameters.", e);
                    }
                }
        }
    }
}

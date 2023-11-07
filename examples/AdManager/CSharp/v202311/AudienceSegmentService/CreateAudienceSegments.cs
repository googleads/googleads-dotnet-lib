// Copyright 2019 Google LLC
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

using Google.Api.Ads.AdManager.Lib;
using Google.Api.Ads.AdManager.v202311;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v202311
{
    /// <summary>
    /// This code example creates new rule based first party audience segments. To
    /// determine which audience segments exist, run GetAllAudienceSegments.cs.
    /// </summary>
    public class CreateAudienceSegments : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return
                    "This code example creates new rule based first party audience segments. To " +
                    "determine which audience segments exist, run GetAllAudienceSegments.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            CreateAudienceSegments codeExample = new CreateAudienceSegments();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (AudienceSegmentService audienceSegmentService =
                user.GetService<AudienceSegmentService>())
            {
                // Get the NetworkService.
                NetworkService networkService =
                    user.GetService<NetworkService>();

                long customTargetingKeyId = long.Parse(_T("INSERT_CUSTOM_TARGETING_KEY_ID_HERE"));
                long customTargetingValueId =
                    long.Parse(_T("INSERT_CUSTOM_TARGETING_VALUE_ID_HERE"));

                try
                {
                    // Get the root ad unit ID used to target the whole site.
                    String rootAdUnitId = networkService.getCurrentNetwork().effectiveRootAdUnitId;

                    // Create inventory targeting.
                    InventoryTargeting inventoryTargeting = new InventoryTargeting();

                    // Create ad unit targeting for the root ad unit (i.e. the whole network).
                    AdUnitTargeting adUnitTargeting = new AdUnitTargeting();
                    adUnitTargeting.adUnitId = rootAdUnitId;
                    adUnitTargeting.includeDescendants = true;

                    inventoryTargeting.targetedAdUnits = new AdUnitTargeting[]
                    {
                        adUnitTargeting
                    };

                    // Create the custom criteria to be used in the segment rule.
                    // CUSTOM_TARGETING_KEY_ID == CUSTOM_TARGETING_VALUE_ID
                    CustomCriteria customCriteria = new CustomCriteria();
                    customCriteria.keyId = customTargetingKeyId;
                    customCriteria.@operator = CustomCriteriaComparisonOperator.IS;
                    customCriteria.valueIds = new long[]
                    {
                        customTargetingValueId
                    };

                    // Create the custom criteria expression.
                    CustomCriteriaSet topCustomCriteriaSet = new CustomCriteriaSet();
                    topCustomCriteriaSet.logicalOperator = CustomCriteriaSetLogicalOperator.AND;
                    topCustomCriteriaSet.children = new CustomCriteriaNode[]
                    {
                        customCriteria
                    };

                    // Create the audience segment rule.
                    FirstPartyAudienceSegmentRule rule = new FirstPartyAudienceSegmentRule();
                    rule.inventoryRule = inventoryTargeting;
                    rule.customCriteriaRule = topCustomCriteriaSet;

                    // Create an audience segment.
                    RuleBasedFirstPartyAudienceSegment audienceSegment =
                        new RuleBasedFirstPartyAudienceSegment();
                    audienceSegment.name =
                        "Sports enthusiasts audience segment #" + this.GetTimeStamp();
                    audienceSegment.description =
                        "Sports enthusiasts between the ages of 20 and 30.";
                    audienceSegment.pageViews = 6;
                    audienceSegment.recencyDays = 6;
                    audienceSegment.membershipExpirationDays = 88;
                    audienceSegment.rule = rule;

                    // Create the audience segment on the server.
                    AudienceSegment[] audienceSegments =
                        audienceSegmentService.createAudienceSegments(
                            new FirstPartyAudienceSegment[]
                            {
                                audienceSegment
                            });

                    foreach (AudienceSegment createdAudienceSegment in audienceSegments)
                    {
                        Console.WriteLine(
                            "An audience segment with ID \"{0}\", name \"{1}\", and " +
                            "type \"{2}\" was created.", createdAudienceSegment.id,
                            createdAudienceSegment.name, createdAudienceSegment.type);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to get audience segment. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}

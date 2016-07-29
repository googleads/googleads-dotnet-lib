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
using Google.Api.Ads.AdWords.v201605;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201605 {

  /// <summary>
  /// This code example adds various types of targeting criteria to a campaign.
  /// To get a list of campaigns, run GetCampaigns.cs.
  /// </summary>
  public class AddCampaignTargetingCriteria : ExampleBase {

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddCampaignTargetingCriteria codeExample = new AddCampaignTargetingCriteria();
      Console.WriteLine(codeExample.Description);
      try {
        long campaignId = long.Parse("INSERT_CAMPAIGN_ID_HERE");
        string feedIdText = "INSERT_LOCATION_FEED_ID_HERE";

        long? feedId = null;
        long temp;

        if (long.TryParse(feedIdText, out temp)) {
          feedId = temp;
        }

        codeExample.Run(new AdWordsUser(), campaignId, feedId);
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
        return "This code example adds various types of targeting criteria to a campaign. To " +
            "get a list of campaigns, run GetCampaigns.cs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="campaignId">Id of the campaign to which targeting criteria
    /// are added.</param>
    /// <param name="feedId">ID of a feed that has been configured for location
    /// targeting, meaning it has an ENABLED FeedMapping with criterionType of
    /// 77. Feeds linked to a GMB account automatically have this FeedMapping.
    /// If you don't have such a feed, set this value to null.</param>
    public void Run(AdWordsUser user, long campaignId, long? feedId) {
      // Get the CampaignCriterionService.
      CampaignCriterionService campaignCriterionService =
          (CampaignCriterionService) user.GetService(
              AdWordsService.v201605.CampaignCriterionService);

      // Create locations. The IDs can be found in the documentation or
      // retrieved with the LocationCriterionService.
      Location california = new Location() {
        id = 21137L
      };

      Location mexico = new Location() {
        id = 2484L
      };

      // Create languages. The IDs can be found in the documentation or
      // retrieved with the ConstantDataService.
      Language english = new Language() {
        id = 1000L
      };

      Language spanish = new Language() {
        id = 1003L
      };

      // Location groups criteria. These represent targeting by household income
      // or places of interest. The IDs can be found in the documentation or
      // retrieved with the LocationCriterionService.
      LocationGroups locationGroupTier3 = new LocationGroups();
      Function tier3MatchingFunction = new Function();
      tier3MatchingFunction.lhsOperand = new FunctionArgumentOperand[] {
        // Tiers are numbered 1-10, and represent 10% segments of earners.
        // For example, TIER_1 is the top 10%, TIER_2 is the 80-90%, etc.
        // Tiers 6 through 10 are grouped into TIER_6_TO_10.
        new IncomeOperand() {
          tier = IncomeTier.TIER_3
        }
      };
      tier3MatchingFunction.@operator = FunctionOperator.AND;
      tier3MatchingFunction.rhsOperand = new FunctionArgumentOperand[] {
        new GeoTargetOperand() {
          locations = new long[] { 1015116L } // Miami, FL
        }
      };

      locationGroupTier3.matchingFunction = tier3MatchingFunction;

      LocationGroups locationGroupDowntown = new LocationGroups() {
        matchingFunction = new Function() {
          lhsOperand = new FunctionArgumentOperand[] {
            new PlacesOfInterestOperand() {
              category = PlacesOfInterestOperandCategory.DOWNTOWN
            }
          },
          @operator = FunctionOperator.AND,
          rhsOperand = new FunctionArgumentOperand[] {
            new GeoTargetOperand() {
              locations = new long[] { 1015116L } // Miami, FL
            }
          }
        }
      };
      
      List<Criterion> criteria = new List<Criterion>() {
          california, mexico, english, spanish, locationGroupTier3};

      // Distance targeting. Area of 10 miles around the locations in the location feed.
      if (feedId != null) {
        LocationGroups radiusLocationGroup = new LocationGroups() {
          feedId = feedId.Value,
          matchingFunction = new Function() {
            @operator = FunctionOperator.IDENTITY,
            lhsOperand = new FunctionArgumentOperand[] {
              new LocationExtensionOperand() {
                radius = new ConstantOperand() {
                  type = ConstantOperandConstantType.DOUBLE,
                  unit  = ConstantOperandUnit.MILES,
                  doubleValue = 10
                }
              }
            }
          }
        };

        criteria.Add(radiusLocationGroup);
      }

      // Create operations to add each of the criteria above.
      List<CampaignCriterionOperation> operations = new List<CampaignCriterionOperation>();
      foreach (Criterion criterion in criteria) {
        CampaignCriterionOperation operation = new CampaignCriterionOperation() {
          operand = new CampaignCriterion() {
            campaignId = campaignId,
            criterion = criterion
          },
          @operator = Operator.ADD
        };

        operations.Add(operation);
      }

      // Add a negative campaign criterion.

      CampaignCriterion negativeCriterion = new NegativeCampaignCriterion() {
        campaignId = campaignId,
        criterion = new Keyword() {
          text = "jupiter cruise",
          matchType = KeywordMatchType.BROAD
        }
      };

      CampaignCriterionOperation negativeCriterionOperation = new CampaignCriterionOperation() {
        operand = negativeCriterion,
        @operator = Operator.ADD
      };

      operations.Add(negativeCriterionOperation);

      try {
        // Set the campaign targets.
        CampaignCriterionReturnValue retVal = campaignCriterionService.mutate(operations.ToArray());

        if (retVal != null && retVal.value != null) {
          // Display campaign targets.
          foreach (CampaignCriterion criterion in retVal.value) {
            Console.WriteLine("Campaign criteria of type '{0}' was set to campaign with" +
                " id = '{1}'.", criterion.criterion.CriterionType, criterion.campaignId);
          }
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to set Campaign criteria.", e);
      }
    }
  }
}

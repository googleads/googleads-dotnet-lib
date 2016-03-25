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
using Google.Api.Ads.AdWords.v201603;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {

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
              AdWordsService.v201603.CampaignCriterionService);

      // Create language criteria.
      // See http://code.google.com/apis/adwords/docs/appendix/languagecodes.html
      // for a detailed list of language codes.
      Language language1 = new Language();
      language1.id = 1002; // French
      CampaignCriterion languageCriterion1 = new CampaignCriterion();
      languageCriterion1.campaignId = campaignId;
      languageCriterion1.criterion = language1;

      Language language2 = new Language();
      language2.id = 1005; // Japanese
      CampaignCriterion languageCriterion2 = new CampaignCriterion();
      languageCriterion2.campaignId = campaignId;
      languageCriterion2.criterion = language2;

      // Target Tier 3 income group near Miami, Florida.
      LocationGroups incomeLocationGroups = new LocationGroups();

      IncomeOperand incomeOperand = new IncomeOperand();
      // Tiers are numbered 1-10, and represent 10% segments of earners.
      // For example, TIER_1 is the top 10%, TIER_2 is the 80-90%, etc.
      // Tiers 6 through 10 are grouped into TIER_6_TO_10.
      incomeOperand.tier = IncomeTier.TIER_3;

      GeoTargetOperand geoTargetOperand1 = new GeoTargetOperand();
      geoTargetOperand1.locations = new long[] { 1015116 }; // Miami, FL.

      incomeLocationGroups.matchingFunction = new Function();
      incomeLocationGroups.matchingFunction.lhsOperand =
          new FunctionArgumentOperand[] { incomeOperand };
      incomeLocationGroups.matchingFunction.@operator = FunctionOperator.AND;
      incomeLocationGroups.matchingFunction.rhsOperand =
          new FunctionArgumentOperand[] { geoTargetOperand1 };

      CampaignCriterion locationGroupCriterion1 = new CampaignCriterion();
      locationGroupCriterion1.campaignId = campaignId;
      locationGroupCriterion1.criterion = incomeLocationGroups;

      // Target places of interest near Downtown Miami, Florida.
      LocationGroups interestLocationGroups = new LocationGroups();

      PlacesOfInterestOperand placesOfInterestOperand = new PlacesOfInterestOperand();
      placesOfInterestOperand.category = PlacesOfInterestOperandCategory.DOWNTOWN;

      GeoTargetOperand geoTargetOperand2 = new GeoTargetOperand();
      geoTargetOperand2.locations = new long[] { 1015116 }; // Miami, FL.

      interestLocationGroups.matchingFunction = new Function();
      interestLocationGroups.matchingFunction.lhsOperand =
          new FunctionArgumentOperand[] { placesOfInterestOperand };
      interestLocationGroups.matchingFunction.@operator = FunctionOperator.AND;
      interestLocationGroups.matchingFunction.rhsOperand =
          new FunctionArgumentOperand[] { geoTargetOperand2 };

      CampaignCriterion locationGroupCriterion2 = new CampaignCriterion();
      locationGroupCriterion2.campaignId = campaignId;
      locationGroupCriterion2.criterion = interestLocationGroups;

      CampaignCriterion locationGroupCriterion3 = new CampaignCriterion();

      if (feedId.HasValue) {
        // Distance targeting. Area of 10 miles around targets above.
        ConstantOperand radius = new ConstantOperand();
        radius.type = ConstantOperandConstantType.DOUBLE;
        radius.unit = ConstantOperandUnit.MILES;
        radius.doubleValue = 10.0;
        LocationExtensionOperand distance = new LocationExtensionOperand();
        distance.radius = radius;

        LocationGroups radiusLocationGroups = new LocationGroups();
        radiusLocationGroups.matchingFunction = new Function();
        radiusLocationGroups.matchingFunction.@operator = FunctionOperator.IDENTITY;
        radiusLocationGroups.matchingFunction.lhsOperand =
            new FunctionArgumentOperand[] { distance };

        // FeedID should be the ID of a feed that has been configured for location
        // targeting, meaning it has an ENABLED FeedMapping with criterionType of
        // 77. Feeds linked to a GMB account automatically have this FeedMapping.
        radiusLocationGroups.feedId = feedId.Value;

        locationGroupCriterion3.campaignId = campaignId;
        locationGroupCriterion3.criterion = radiusLocationGroups;
      }

      // Create location criteria.
      // See http://code.google.com/apis/adwords/docs/appendix/countrycodes.html
      // for a detailed list of country codes.
      Location location1 = new Location();
      location1.id = 2840; // USA
      CampaignCriterion locationCriterion1 = new CampaignCriterion();
      locationCriterion1.campaignId = campaignId;
      locationCriterion1.criterion = location1;

      Location location2 = new Location();
      location2.id = 2392; // Japan
      CampaignCriterion locationCriterion2 = new CampaignCriterion();
      locationCriterion2.campaignId = campaignId;
      locationCriterion2.criterion = location2;

      // Add a negative campaign keyword.
      NegativeCampaignCriterion negativeCriterion = new NegativeCampaignCriterion();
      negativeCriterion.campaignId = campaignId;

      Keyword keyword = new Keyword();
      keyword.matchType = KeywordMatchType.BROAD;
      keyword.text = "jupiter cruise";

      negativeCriterion.criterion = keyword;

      List<CampaignCriterion> criteria = new List<CampaignCriterion>(
          new CampaignCriterion[] {languageCriterion1,
          languageCriterion2, locationCriterion1, locationCriterion2, negativeCriterion,
          locationGroupCriterion1, locationGroupCriterion2});

      if (feedId.HasValue) {
        criteria.Add(locationGroupCriterion3);
      }

      List<CampaignCriterionOperation> operations = new List<CampaignCriterionOperation>();

      foreach (CampaignCriterion criterion in criteria) {
        CampaignCriterionOperation operation = new CampaignCriterionOperation();
        operation.@operator = Operator.ADD;
        operation.operand = criterion;
        operations.Add(operation);
      }

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

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
using Google.Api.Ads.AdWords.Util.Shopping.v201603;
using Google.Api.Ads.AdWords.v201603;

using System;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {

  /// <summary>
  /// This code example creates a ProductPartition tree.
  /// </summary>
  public class AddProductPartitionTree : ExampleBase {

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddProductPartitionTree codeExample = new AddProductPartitionTree();
      Console.WriteLine(codeExample.Description);
      try {
        long adGroupId = long.Parse("INSERT_ADGROUP_ID_HERE");
        codeExample.Run(new AdWordsUser(), adGroupId);
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
        return "This code example creates a ProductPartition tree.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">The ad group to which product partition is
    /// added.</param>
    public void Run(AdWordsUser user, long adGroupId) {
      // Get the AdGroupCriterionService.
      AdGroupCriterionService adGroupCriterionService =
          (AdGroupCriterionService) user.GetService(
              AdWordsService.v201603.AdGroupCriterionService);

      // Build a new ProductPartitionTree using the ad group's current set of criteria.
      ProductPartitionTree partitionTree =
          ProductPartitionTree.DownloadAdGroupTree(user, adGroupId);

      Console.WriteLine("Original tree: {0}", partitionTree);

      // Clear out any existing criteria.
      ProductPartitionNode rootNode = partitionTree.Root.RemoveAllChildren();

      // Make the root node a subdivision.
      rootNode = rootNode.AsSubdivision();

      // Add a unit node for condition = NEW.
      ProductPartitionNode newConditionNode = rootNode.AddChild(
          ProductDimensions.CreateCanonicalCondition(ProductCanonicalConditionCondition.NEW));
      newConditionNode.AsBiddableUnit().CpcBid = 200000;

      ProductPartitionNode usedConditionNode = rootNode.AddChild(
          ProductDimensions.CreateCanonicalCondition(ProductCanonicalConditionCondition.USED));
      usedConditionNode.AsBiddableUnit().CpcBid = 100000;

      // Add a subdivision node for condition = null (everything else).
      ProductPartitionNode otherConditionNode =
          rootNode.AddChild(ProductDimensions.CreateCanonicalCondition()).AsSubdivision();

      // Add a unit node under condition = null for brand = "CoolBrand".
      ProductPartitionNode coolBrandNode = otherConditionNode.AddChild(
          ProductDimensions.CreateBrand("CoolBrand"));
      coolBrandNode.AsBiddableUnit().CpcBid = 900000L;

      // Add a unit node under condition = null for brand = "CheapBrand".
      ProductPartitionNode cheapBrandNode = otherConditionNode.AddChild(
          ProductDimensions.CreateBrand("CheapBrand"));
      cheapBrandNode.AsBiddableUnit().CpcBid = 10000L;

      // Add a subdivision node under condition = null for brand = null (everything else).
      ProductPartitionNode otherBrandNode = otherConditionNode.AddChild(
          ProductDimensions.CreateBrand(null)).AsSubdivision();

      // Add unit nodes under condition = null/brand = null.
      // The value for each bidding category is a fixed ID for a specific
      // category. You can retrieve IDs for categories from the ConstantDataService.
      // See the 'GetProductCategoryTaxonomy' example for more details.

      // Add a unit node under condition = null/brand = null for product type
      // level 1 = 'Luggage & Bags'.
      ProductPartitionNode luggageAndBagNode = otherBrandNode.AddChild(
          ProductDimensions.CreateBiddingCategory(ProductDimensionType.BIDDING_CATEGORY_L1,
          -5914235892932915235L));
      luggageAndBagNode.AsBiddableUnit().CpcBid = 750000L;

      // Add a unit node under condition = null/brand = null for product type
      // level 1 = null (everything else).
      ProductPartitionNode everythingElseNode = otherBrandNode.AddChild(
          ProductDimensions.CreateBiddingCategory(ProductDimensionType.BIDDING_CATEGORY_L1));
      everythingElseNode.AsBiddableUnit().CpcBid = 110000L;

      try {
        // Make the mutate request, using the operations returned by the ProductPartitionTree.
        AdGroupCriterionOperation[] mutateOperations = partitionTree.GetMutateOperations();

        if (mutateOperations.Length == 0) {
          Console.WriteLine("Skipping the mutate call because the original tree and the current " +
              "tree are logically identical.");
        } else {
          adGroupCriterionService.mutate(mutateOperations);
        }

        // The request was successful, so create a new ProductPartitionTree based on the updated
        // state of the ad group.
        partitionTree = ProductPartitionTree.DownloadAdGroupTree(user, adGroupId);

        Console.WriteLine("Final tree: {0}", partitionTree);

      } catch (Exception e) {
        throw new System.ApplicationException("Failed to set shopping product partition.", e);
      }
    }
  }
}
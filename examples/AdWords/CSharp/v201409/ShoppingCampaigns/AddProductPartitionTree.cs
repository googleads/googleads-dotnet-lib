// Copyright 2014, Google Inc. All Rights Reserved.
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
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201409 {

  /// <summary>
  /// This code example creates a ProductPartition tree.
  ///
  /// Tags: AdGroupCriterionService.mutate
  /// </summary>
  public class AddProductPartitionTree : ExampleBase {

    /// <summary>
    /// A helper class for creating ProductPartition trees.
    /// </summary>
    private class ProductPartitionHelper {

      /// <summary>
      /// The ID of the AdGroup that we wish to attach the partition tree to.
      /// </summary>
      private long adGroupId;

      /// <summary>
      /// The next temporary critertion ID to be used.
      ///
      /// When creating our tree we need to specify the parent-child
      /// relationships between nodes. However, until a criterion has been
      /// created on the server we do not have a criterionId with which to
      /// refer to it.
      ///
      /// Instead we can specify temporary IDs that are specific to a single
      /// mutate request. Once the criteria have been created they are assigned
      /// an ID as normal and the temporary ID will no longer refer to it.
      ///
      /// A valid temporary ID is any negative integer.
      /// </summary>
      private long nextId = -1;

      /// <summary>
      /// The set of mutate operations needed to create the current tree.
      /// </summary>
      private List<AdGroupCriterionOperation> operations = new List<AdGroupCriterionOperation>();

      /// <summary>
      /// Overloaded constructor.
      /// </summary>
      /// <param name="adGroupId">The ID of the AdGroup that we wish to attach
      /// the partition tree to.</param>
      public ProductPartitionHelper(long adGroupId) {
        this.adGroupId = adGroupId;
      }

      public AdGroupCriterionOperation[] Operations {
        get {
          return operations.ToArray();
        }
      }

      /// <summary>
      /// Creates a subdivision node.
      /// </summary>
      /// <param name="parent">The node that should be this node's parent.
      /// </param>
      /// <param name="value">The value being paritioned on.</param>
      /// <returns>A new subdivision node.</returns>
      public ProductPartition CreateSubdivision(ProductPartition parent, ProductDimension value) {
        ProductPartition division = new ProductPartition();
        division.partitionType = ProductPartitionType.SUBDIVISION;
        division.id = this.nextId--;

        // The root node has neither a parent nor a value.
        if (parent != null) {
          division.parentCriterionId = parent.id;
          division.caseValue = value;
        }

        BiddableAdGroupCriterion criterion = new BiddableAdGroupCriterion();
        criterion.adGroupId = this.adGroupId;
        criterion.criterion = division;

        this.CreateAddOperation(criterion);
        return division;
      }

      /// <summary>
      /// Creates the unit.
      /// </summary>
      /// <param name="parent">The node that should be this node's parent.
      /// </param>
      /// <param name="value">The value being paritioned on.</param>
      /// <param name="bidAmount">The amount to bid for matching products,
      /// in micros.</param>
      /// <param name="isNegative">True, if this is negative criterion, false
      /// otherwise.</param>
      /// <returns>A new unit node.</returns>
      public ProductPartition CreateUnit(ProductPartition parent, ProductDimension value,
          long bidAmount, bool isNegative) {
        ProductPartition unit = new ProductPartition();
        unit.partitionType = ProductPartitionType.UNIT;

        // The root node has neither a parent nor a value.
        if (parent != null) {
          unit.parentCriterionId = parent.id;
          unit.caseValue = value;
        }

        AdGroupCriterion criterion;

        if (isNegative) {
          criterion = new NegativeAdGroupCriterion();
        } else {
          BiddingStrategyConfiguration biddingStrategyConfiguration =
              new BiddingStrategyConfiguration();

          CpcBid cpcBid = new CpcBid();
          cpcBid.bid = new Money();
          cpcBid.bid.microAmount = bidAmount;
          biddingStrategyConfiguration.bids = new Bids[] { cpcBid };

          criterion = new BiddableAdGroupCriterion();
          (criterion as BiddableAdGroupCriterion).biddingStrategyConfiguration =
              biddingStrategyConfiguration;
        }

        criterion.adGroupId = this.adGroupId;
        criterion.criterion = unit;

        this.CreateAddOperation(criterion);

        return unit;
      }

      /// <summary>
      /// Creates an AdGroupCriterionOperation for the given criterion
      /// </summary>
      /// <param name="criterion">The criterion we want to add</param>
      private void CreateAddOperation(AdGroupCriterion criterion) {
        AdGroupCriterionOperation operation = new AdGroupCriterionOperation();
        operation.operand = criterion;
        operation.@operator = Operator.ADD;
        this.operations.Add(operation);
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
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddProductPartitionTree codeExample = new AddProductPartitionTree();
      Console.WriteLine(codeExample.Description);
      try {
        long adGroupId = long.Parse("INSERT_ADGROUP_ID_HERE");
        codeExample.Run(new AdWordsUser(), adGroupId);
      } catch (Exception ex) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(ex));
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
              AdWordsService.v201409.AdGroupCriterionService);

      ProductPartitionHelper helper = new ProductPartitionHelper(adGroupId);

      // The most trivial partition tree has only a unit node as the root:
      //   helper.createUnit(null, null, 100000);

      ProductPartition root = helper.CreateSubdivision(null, null);

      ProductCanonicalCondition newCondition = new ProductCanonicalCondition();
      newCondition.condition = ProductCanonicalConditionCondition.NEW;
      ProductPartition newPartition = helper.CreateUnit(root, newCondition, 200000, false);

      ProductCanonicalCondition usedCondition = new ProductCanonicalCondition();
      usedCondition.condition = ProductCanonicalConditionCondition.USED;
      ProductPartition usedPartition = helper.CreateUnit(root, usedCondition, 100000, false);

      ProductPartition otherCondition = helper.CreateSubdivision(root,
          new ProductCanonicalCondition());

      ProductBrand coolBrand = new ProductBrand();
      coolBrand.value = "CoolBrand";
      helper.CreateUnit(otherCondition, coolBrand, 900000, false);

      ProductBrand cheapBrand = new ProductBrand();
      cheapBrand.value = "CheapBrand";
      helper.CreateUnit(otherCondition, cheapBrand, 10000, false);

      ProductPartition otherBrand =
          helper.CreateSubdivision(otherCondition, new ProductBrand());

      // The value for the bidding category is a fixed ID for the 'Luggage & Bags'
      // category. You can retrieve IDs for categories from the ConstantDataService.
      // See the 'GetProductCategoryTaxonomy' example for more details.

      ProductBiddingCategory luggageAndBags = new ProductBiddingCategory();
      luggageAndBags.type = ProductDimensionType.BIDDING_CATEGORY_L1;
      luggageAndBags.value = -5914235892932915235;
      helper.CreateUnit(otherBrand, luggageAndBags, 750000, false);

      ProductBiddingCategory everythingElse = new ProductBiddingCategory();
      everythingElse.type = ProductDimensionType.BIDDING_CATEGORY_L1;

      helper.CreateUnit(otherBrand, everythingElse, 110000, false);

      try {
        // Make the mutate request.
        AdGroupCriterionReturnValue retval = adGroupCriterionService.mutate(helper.Operations);

        Dictionary<long, List<ProductPartition>> children =
            new Dictionary<long, List<ProductPartition>>();
        ProductPartition rootNode = null;
        // For each criterion, make an array containing each of its children
        // We always create the parent before the child, so we can rely on that here.
        foreach (AdGroupCriterion adGroupCriterion in retval.value) {
          ProductPartition newCriterion = (ProductPartition) adGroupCriterion.criterion;
          children[newCriterion.id] = new List<ProductPartition>();

          if (newCriterion.parentCriterionIdSpecified) {
            children[newCriterion.parentCriterionId].Add(newCriterion);
          } else {
            rootNode = (ProductPartition) adGroupCriterion.criterion;
          }
        }

        // Show the tree
        StringWriter writer = new StringWriter();
        DisplayTree(rootNode, children, 0, writer);
        Console.WriteLine(writer.ToString());

      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to set shopping product partition.", ex);
      }
    }

    /// <summary>
    /// Displays the product partition tree.
    /// </summary>
    /// <param name="node">The root node.</param>
    /// <param name="children">The child node.</param>
    /// <param name="level">The tree level.</param>
    /// <param name="writer">The stream to write output to.</param>
    private void DisplayTree(ProductPartition node, Dictionary<long,
        List<ProductPartition>> children, int level, StringWriter writer) {
      // Recursively display a node and each of its children.
      object value = null;
      string type = "";

      if (node.caseValue != null) {
        type = node.caseValue.ProductDimensionType;
        switch (type) {
          case "ProductCanonicalCondition":
            value = (node.caseValue as ProductCanonicalCondition).condition.ToString();
            break;

          case "ProductBiddingCategory":
            value = (node.caseValue as ProductBiddingCategory).type.ToString() + "(" +
                (node.caseValue as ProductBiddingCategory).value + ")";
            break;

          default:
            value = node.caseValue.GetType().GetProperty("value").GetValue(node.caseValue, null);
            break;
        }
      }

      writer.WriteLine("{0}id: {1}, type: {2}, value: {3}", "".PadLeft(level, ' '), node.id,
          type, value);
      foreach (ProductPartition childNode in children[node.id]) {
        DisplayTree(childNode, children, level + 1, writer);
      }
    }
  }
}

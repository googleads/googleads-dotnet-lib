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
using Google.Api.Ads.AdWords.Tests.v201603;
using Google.Api.Ads.AdWords.Util.Shopping.v201603;
using Google.Api.Ads.AdWords.v201603;

using NUnit.Framework;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.AdWords.Tests.Util.Shopping.v201603 {

  /// <summary>
  /// Integration tests for <see cref="ProductPartitionTree"/> class.
  /// </summary>
  internal class ProductPartitionTreeIntegrationTests : VersionedExampleTestsBase {

    /// <summary>
    /// The campaign ID for running tests.
    /// </summary>
    private long CAMPAIGN_ID = 0;

    /// <summary>
    /// The adgroup ID for running tests.
    /// </summary>
    private long ADGROUP_ID = 0;

    /// <summary>
    /// The utility class for running tests.
    /// </summary>
    private ShoppingTestUtils shoppingTestUtils = new ShoppingTestUtils();

    private ProductPartitionTree tree = null;

    /// <summary>
    /// Initializes this instance.
    /// </summary>
    [SetUp]
    public void Init() {
      CAMPAIGN_ID = utils.CreateShoppingCampaign(user, BiddingStrategyType.MANUAL_CPC);
      ADGROUP_ID = utils.CreateAdGroup(user, CAMPAIGN_ID);

      tree = ProductPartitionTree.CreateAdGroupTree(ADGROUP_ID,
          new List<AdGroupCriterion>());
      ProductPartitionNode root = tree.Root.AsSubdivision();

      ProductPartitionNode clothing = root.AddChild(ProductDimensions.CreateType(
          ProductDimensionType.PRODUCT_TYPE_L1, "clothing"));
      clothing.AsBiddableUnit().CpcBid = 200000;
      ProductPartitionNode shoes = root.AddChild(ProductDimensions.CreateType(
          ProductDimensionType.PRODUCT_TYPE_L1, "shoes"));
      shoes.AsBiddableUnit().CpcBid = 400000;
      ProductPartitionNode otherNode = root.AddChild(ProductDimensions.CreateType(
          ProductDimensionType.PRODUCT_TYPE_L1));
      otherNode.AsBiddableUnit().CpcBid = 300000;

      tree = ExecuteTreeOperations();
    }

    /// <summary>
    /// Executes the tree operations.
    /// </summary>
    /// <returns>The synced product partition tree.</returns>
    private ProductPartitionTree ExecuteTreeOperations() {
      AdGroupCriterionOperation[] operations = tree.GetMutateOperations();
      AdGroupCriterionService service = (AdGroupCriterionService) user.GetService(
          AdWordsService.v201603.AdGroupCriterionService);
      service.mutate(operations);
      return ProductPartitionTree.DownloadAdGroupTree(user, ADGROUP_ID);
    }

    /// <summary>
    /// Does a sequence of tree transformations.
    /// </summary>
    [Test]
    public void DoEverything() {
      SetRootToEmpty();
      RebuildSingleNodeTree();
      RebuildMultiNodeTree();
      RebuildComplexTree();

      RebuildMultiNodeTree();
      SubdivideShoes();
      CollapseShoes();
      SubdivideShoes();
      SubdivideNewShoes();
      CollapseNewShoes();
      SubdivideThenCollapseNewShoes();
      RemoveShoes();

      UpdateClothingBid();
      UpdateClothingBidTwice();

      UseObsoleteCategory();
      CreateTreeFromCriteria();
    }

    /// <summary>
    /// Removes the root of the tree.
    /// </summary>
    private void SetRootToEmpty() {
      ProductPartitionNode root = tree.Root;
      if (root != null && root.ProductPartitionId >= 0L) {
        AdGroupCriterion rootCriterion = new AdGroupCriterion() {
          adGroupId = ADGROUP_ID,
          criterion = new Criterion() {
            id = root.ProductPartitionId
          }
        };

        AdGroupCriterionOperation removeOp = new AdGroupCriterionOperation() {
          @operator = Operator.REMOVE,
          operand = rootCriterion
        };

        AdGroupCriterionService adGroupCriterionService = (AdGroupCriterionService)
            user.GetService(AdWordsService.v201603.AdGroupCriterionService);
        adGroupCriterionService.mutate(new AdGroupCriterionOperation[] { removeOp });
      }

      Assert.DoesNotThrow(delegate() {
        tree = ProductPartitionTree.DownloadAdGroupTree(user, ADGROUP_ID);
      });
    }

    /// <summary>
    /// Creates a tree of the form:
    ///
    /// <pre>
    ///   ROOT $2.50
    /// </pre>
    /// </summary>
    private void RebuildSingleNodeTree() {
      // Clear out the tree and set the root bid.
      tree.Root.RemoveAllChildren().AsBiddableUnit().CpcBid = 2500000L;
      Assert.DoesNotThrow(delegate() {
        tree = ExecuteTreeOperations();
      });
    }

    private void RebuildMultiNodeTree() {
      // Clear out the tree.
      ProductPartitionNode rootNode = tree.Root.RemoveAllChildren().AsSubdivision();

      long[] bids = new long[] { 2500000L, 1500000L, 1000000L };
      String[] productTypeValues = new String[] { "shoes", "clothing", null };
      for (int i = 0; i < productTypeValues.Length; i++) {
        rootNode.AddChild(
            ProductDimensions.CreateType(ProductDimensionType.PRODUCT_TYPE_L1,
                productTypeValues[i])).AsBiddableUnit().CpcBid = bids[i];
      }
      Assert.DoesNotThrow(delegate() {
        tree = ExecuteTreeOperations();
      });
    }

    /// <summary>
    /// Creates a tree of the form:
    ///
    /// <pre>
    /// ROOT
    ///   ProductType Level1 shoes
    ///     ProductType Level2 athletic shoes
    ///       Condition new $2.50
    ///       Condition used $1.00
    ///       Other - exclude from bidding
    ///     ProductType Level2 walking shoes
    ///       Condition new $3.50
    ///       Condition used $1.25
    ///       Other $1.00
    ///     ProductType Level2 null (everything else) - exclude from bidding
    ///   ProductType Level1 clothing
    ///     ProductType Level2 winter clothing
    ///       Condition new $1.00
    ///       Condition used $1.25
    ///       Other $1.50
    ///     ProductType Level2 summer clothing
    ///       Condition new $1.10
    ///       Condition used $1.00
    ///       Other $1.25
    ///     ProductType Level2 null (everything else)
    ///       Condition new $0.90
    ///       Condition used $0.85
    ///       Other $0.75
    ///   ProductType Level1 null (everything else) - exclude from bidding
    /// </pre>
    /// </summary>
    private void RebuildComplexTree() {
      // Clear out the tree.
      ProductPartitionNode rootNode = tree.Root.RemoveAllChildren().AsSubdivision();

      ProductPartitionNode shoesLevel1 = rootNode.AddChild(ProductDimensions.CreateType(
          ProductDimensionType.PRODUCT_TYPE_L1, "shoes")).AsSubdivision();

      ProductPartitionNode athleticShoesLevel2 = shoesLevel1.AddChild(
          ProductDimensions.CreateType(ProductDimensionType.PRODUCT_TYPE_L2, "athletic shoes"))
              .AsSubdivision();
      athleticShoesLevel2.AddChild(ProductDimensions.CreateCanonicalCondition(
            ProductCanonicalConditionCondition.NEW)).AsBiddableUnit().CpcBid = 2500000L;
      athleticShoesLevel2.AddChild(ProductDimensions.CreateCanonicalCondition(
          ProductCanonicalConditionCondition.USED)).AsBiddableUnit().CpcBid = 1000000L;
      athleticShoesLevel2.AddChild(ProductDimensions.CreateCanonicalCondition()).AsExcludedUnit();

      ProductPartitionNode walkingShoesLevel2 = shoesLevel1.AddChild(
          ProductDimensions.CreateType(ProductDimensionType.PRODUCT_TYPE_L2, "walking shoes"))
              .AsSubdivision();
      walkingShoesLevel2.AddChild(ProductDimensions.CreateCanonicalCondition(
          ProductCanonicalConditionCondition.NEW)).AsBiddableUnit().CpcBid = 3500000L;
      walkingShoesLevel2.AddChild(ProductDimensions.CreateCanonicalCondition(
          ProductCanonicalConditionCondition.USED)).AsBiddableUnit().CpcBid = 1250000L;
      walkingShoesLevel2.AddChild(ProductDimensions.CreateCanonicalCondition()).AsBiddableUnit()
          .CpcBid = 1000000L;

      shoesLevel1.AddChild(ProductDimensions.CreateType(ProductDimensionType.PRODUCT_TYPE_L2))
          .AsExcludedUnit();

      ProductPartitionNode clothingLevel1 = rootNode.AddChild(ProductDimensions.CreateType(
          ProductDimensionType.PRODUCT_TYPE_L1, "clothing")).AsSubdivision();

      ProductPartitionNode winterClothingLevel2 = clothingLevel1.AddChild(
          ProductDimensions.CreateType(ProductDimensionType.PRODUCT_TYPE_L2, "winter clothing"))
              .AsSubdivision();
      winterClothingLevel2.AddChild(ProductDimensions.CreateCanonicalCondition(
          ProductCanonicalConditionCondition.NEW)).AsBiddableUnit().CpcBid = 1000000L;
      winterClothingLevel2.AddChild(ProductDimensions.CreateCanonicalCondition(
          ProductCanonicalConditionCondition.USED)).AsBiddableUnit().CpcBid = 1250000L;
      winterClothingLevel2.AddChild(ProductDimensions.CreateCanonicalCondition()).AsBiddableUnit()
          .CpcBid = 1500000L;

      ProductPartitionNode summerClothingLevel2 = clothingLevel1.AddChild(
          ProductDimensions.CreateType(ProductDimensionType.PRODUCT_TYPE_L2, "summer clothing"))
          .AsSubdivision();
      summerClothingLevel2.AddChild(ProductDimensions.CreateCanonicalCondition(
          ProductCanonicalConditionCondition.NEW)).AsBiddableUnit().CpcBid = 1100000L;
      summerClothingLevel2.AddChild(ProductDimensions.CreateCanonicalCondition(
          ProductCanonicalConditionCondition.USED)).AsBiddableUnit().CpcBid = 1000000L;
      summerClothingLevel2.AddChild(ProductDimensions.CreateCanonicalCondition()).AsBiddableUnit()
          .CpcBid = 1250000L;

      ProductPartitionNode otherClothingLevel2 = clothingLevel1.AddChild(
          ProductDimensions.CreateType(ProductDimensionType.PRODUCT_TYPE_L2, null))
              .AsSubdivision();
      otherClothingLevel2.AddChild(ProductDimensions.CreateCanonicalCondition(
          ProductCanonicalConditionCondition.NEW)).AsBiddableUnit().CpcBid = 900000L;
      otherClothingLevel2.AddChild(ProductDimensions.CreateCanonicalCondition(
          ProductCanonicalConditionCondition.USED)).AsBiddableUnit().CpcBid = 850000L;
      otherClothingLevel2.AddChild(ProductDimensions.CreateCanonicalCondition()).AsBiddableUnit()
          .CpcBid = 750000L;

      rootNode.AddChild(ProductDimensions.CreateType(ProductDimensionType.PRODUCT_TYPE_L1, null))
          .AsExcludedUnit();

      Assert.DoesNotThrow(delegate() {
        tree = ExecuteTreeOperations();
      });
    }

    /// <summary>
    /// Takes a tree with product type L1 "shoes" UNIT and subdivides that node into UNITs:
    ///
    /// <pre>
    /// shoes
    ///   new $1.00
    ///   refurbished $1.50
    ///   other - excluded
    /// </pre>
    /// </summary>
    private void SubdivideShoes() {
      ProductPartitionNode shoesLevel1 = tree.Root.GetChild(ProductDimensions.CreateType(
          ProductDimensionType.PRODUCT_TYPE_L1, "shoes"))
              .AsSubdivision();
      shoesLevel1.AddChild(ProductDimensions.CreateCanonicalCondition(
          ProductCanonicalConditionCondition.NEW)).AsBiddableUnit().CpcBid = 1000000L;
      shoesLevel1.AddChild(ProductDimensions.CreateCanonicalCondition(
          ProductCanonicalConditionCondition.REFURBISHED)).AsBiddableUnit().CpcBid = 1500000L;
      shoesLevel1.AddChild(ProductDimensions.CreateCanonicalCondition()).AsExcludedUnit();

      Assert.DoesNotThrow(delegate() {
        tree = ExecuteTreeOperations();
      });
    }

    /// <summary>
    /// Subdivides the new shoes.
    /// </summary>
    /// Takes a tree with:
    /// <pre>
    /// ROOT
    ///   ...
    ///   shoes
    ///     new some bid
    /// </pre>
    ///
    /// and changes it to:
    ///
    /// <pre>
    ///   ROOT
    ///     ...
    ///     shoes
    ///       new
    ///       other offerId $1.00
    ///       offerId=2 $2.00
    ///       ...
    ///       offerId=20 $20.00
    /// </pre>
    private void SubdivideNewShoes() {
      ProductPartitionNode shoesLevel1 = tree.Root.GetChild(ProductDimensions.CreateType(
          ProductDimensionType.PRODUCT_TYPE_L1, "shoes")).AsSubdivision();
      ProductPartitionNode newShoesLevel2 = shoesLevel1.GetChild(
          ProductDimensions.CreateCanonicalCondition(ProductCanonicalConditionCondition.NEW))
              .AsSubdivision();

      for (int i = 1; i <= 20; i++) {
        ProductOfferId offerId = ProductDimensions.CreateOfferId();
        if (i > 1) {
          offerId.value = i.ToString();
        }

        newShoesLevel2.AddChild(offerId).AsBiddableUnit().CpcBid = i * 1000000L;
      }

      Assert.DoesNotThrow(delegate() {
        tree = ExecuteTreeOperations();
      });
    }

    /// <summary>
    /// Takes a tree with:
    ///
    /// <pre>
    /// ROOT
    ///  ...
    ///  shoes
    ///    new
    ///      offerId=1 $1.00
    ///      offerId=2 $2.00
    ///      ...
    ///      offerId=20 $20.00
    ///    other - excluded
    /// </pre>
    ///
    ///  and changes it to:
    ///
    ///  <pre>
    /// ROOT
    ///  ...
    ///  shoes
    ///    new $1.25
    ///    other $0.50
    /// </pre>
    /// </summary>
    private void CollapseNewShoes() {
      ProductPartitionNode shoesLevel1 = tree.Root.GetChild(ProductDimensions.CreateType(
          ProductDimensionType.PRODUCT_TYPE_L1, "shoes"));
      shoesLevel1.GetChild(ProductDimensions.CreateCanonicalCondition(
          ProductCanonicalConditionCondition.NEW)).AsBiddableUnit().CpcBid = 1250000L;
      shoesLevel1.GetChild(ProductDimensions.CreateCanonicalCondition()).AsBiddableUnit()
          .CpcBid = 500000L;

      Assert.DoesNotThrow(delegate() {
        tree = ExecuteTreeOperations();
      });
    }

    /// <summary>
    /// Subdivides and then collapse new shoes.
    /// </summary>
    private void SubdivideThenCollapseNewShoes() {
      SubdivideNewShoes();
      CollapseNewShoes();
    }

    /// <summary>
    /// Removes product type L1 "shoes".
    /// </summary>
    private void RemoveShoes() {
      tree.Root.RemoveChild(ProductDimensions.CreateType(ProductDimensionType.PRODUCT_TYPE_L1,
          "shoes"));

      Assert.DoesNotThrow(delegate() {
        tree = ExecuteTreeOperations();
      });
    }

    /// <summary>
    /// Takes a tree with product type L1 "shoes" SUBDIVISION and collapses that node to a UNIT.
    /// </summary>
    private void CollapseShoes() {
      tree.Root.GetChild(ProductDimensions.CreateType(ProductDimensionType.PRODUCT_TYPE_L1,
          "shoes")).AsBiddableUnit().CpcBid = (1500000L);
      Assert.DoesNotThrow(delegate() {
        tree = ExecuteTreeOperations();
      });
    }

    /// <summary>
    /// Takes a tree with product type L1 "clothing" and sets its bid.
    /// </summary>
    private void UpdateClothingBid() {
      UpdateClothingBid(990000L);
    }

    /// <summary>
    /// Takes a tree with product type L1 "clothing" and sets its bid.
    /// </summary>
    /// <param name="bid">The bid.</param>
    private void UpdateClothingBid(long bid) {
      tree.Root.GetChild(ProductDimensions.CreateType(
        ProductDimensionType.PRODUCT_TYPE_L1, "clothing")).AsBiddableUnit().CpcBid = bid;

      Assert.DoesNotThrow(delegate() {
        tree = ExecuteTreeOperations();
      });
    }

    /// <summary>
    /// Updates the clothing bid twice.
    /// </summary>
    private void UpdateClothingBidTwice() {
      UpdateClothingBid(1500000L);
      UpdateClothingBid(2500000L);
    }

    /// <summary>
    /// Uses obsolete category Hardware > Flooring (5264193646140135688). This is expected to
    /// fail with the error {@code CriterionError.INVALID_PRODUCT_BIDDING_CATEGORY}.
    /// </summary>
    private void UseObsoleteCategory() {
      tree.Root.RemoveAllChildren();
      ProductPartitionNode hardwareLevel1 = tree.Root.AsSubdivision().AddChild(
          ProductDimensions.CreateBiddingCategory(ProductDimensionType.BIDDING_CATEGORY_L1,
              1689639310991627077L)).AsSubdivision();
      hardwareLevel1.AddChild(ProductDimensions.CreateBiddingCategory(
          ProductDimensionType.BIDDING_CATEGORY_L2, 5264193646140135688L)).CpcBid = 1000000L;
      hardwareLevel1.AddChild(ProductDimensions.CreateBiddingCategory(
          ProductDimensionType.BIDDING_CATEGORY_L2)).AsExcludedUnit();
      tree.Root.AddChild(ProductDimensions.CreateBiddingCategory(
          ProductDimensionType.BIDDING_CATEGORY_L1)).AsExcludedUnit();
      try {
        tree = ExecuteTreeOperations();
        Assert.Fail("Did not throw CriterionError.INVALID_PRODUCT_BIDDING_CATEGORY");
      } catch (AdWordsApiException e) {
        ApiError[] errors = (e.ApiException as ApiException).errors;
        Assert.That(errors != null && errors.Length == 1);
        Assert.That(errors[0] is CriterionError);
        Assert.That((errors[0] as CriterionError).reason ==
            CriterionErrorReason.INVALID_PRODUCT_BIDDING_CATEGORY);
      }
    }

    /// <summary>
    /// Creates a tree from criteria passed into the factory method.
    /// </summary>
    private void CreateTreeFromCriteria() {
      AdGroupCriterionService service = (AdGroupCriterionService) user.GetService(
          AdWordsService.v201603.AdGroupCriterionService);

      string[] REQUIRED_SELECTOR_FIELD_ENUMS = new string[] {
          AdGroupCriterion.Fields.AdGroupId,
          Criterion.Fields.Id,
          ProductPartition.Fields.ParentCriterionId,
          ProductPartition.Fields.PartitionType,
          ProductPartition.Fields.CriteriaType,
          ProductPartition.Fields.CaseValue,
          CpcBid.Fields.CpcBid,
          CpcBid.Fields.CpcBidSource,
          BiddableAdGroupCriterion.Fields.Status
      };

      Selector selector = new Selector() {
        fields = REQUIRED_SELECTOR_FIELD_ENUMS,
        predicates = new Predicate[] {
        Predicate.Equals(AdGroupCriterion.Fields.AdGroupId, ADGROUP_ID)
      }
      };

      AdGroupCriterionPage retval = service.get(selector);

      tree = ProductPartitionTree.CreateAdGroupTree(ADGROUP_ID,
          new List<AdGroupCriterion>(retval.entries));
    }
  }
}

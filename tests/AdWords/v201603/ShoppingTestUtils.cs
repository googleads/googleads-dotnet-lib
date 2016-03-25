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

using Google.Api.Ads.AdWords.Util.Shopping.v201603;
using Google.Api.Ads.AdWords.v201603;

using System.Collections.Generic;

namespace Google.Api.Ads.AdWords.Tests.v201603 {

  /// <summary>
  /// Utility functions for testing shopping utility classes.
  /// </summary>
  class ShoppingTestUtils {
    /// <summary>
    /// A ProductBrand node for Google brand.
    /// </summary>
    internal readonly ProductDimension BRAND_GOOGLE = ProductDimensions.CreateBrand("google");

    /// <summary>
    /// A ProductBrand node for Motorola brand.
    /// </summary>
    internal readonly ProductDimension BRAND_MOTOROLA = ProductDimensions.CreateBrand("motorola");

    /// <summary>
    /// A ProductBrand node for Everything else.
    /// </summary>
    internal readonly ProductDimension BRAND_OTHER = ProductDimensions.CreateBrand();

    /// <summary>
    /// A ProductOfferId node for Offer A.
    /// </summary>
    internal readonly ProductDimension OFFER_A = ProductDimensions.CreateOfferId("A");
    
    /// <summary>
    /// A ProductOfferId node for Offer B.
    /// </summary>
    internal readonly ProductDimension OFFER_B = ProductDimensions.CreateOfferId("B");

    /// <summary>
    /// A ProductOfferId node for Offer C.
    /// </summary>
    internal readonly ProductDimension OFFER_C = ProductDimensions.CreateOfferId("C");
    
    /// <summary>
    /// A ProductOfferId node for Everything else.
    /// </summary>
    internal readonly ProductDimension OFFER_OTHER = ProductDimensions.CreateOfferId();

    /// <summary>
    /// Creates a tree for testing various ADD operations.
    /// </summary>
    /// <param name="adGroupId">The ad group ID.</param>
    /// <returns>A product partition tree for running tests.</returns>
    internal ProductPartitionTree CreateTestTreeForAddition(long adGroupId) {
      return CreateTestTree(adGroupId, ProductPartitionTree.NEW_ROOT_ID);
    }

    /// <summary>
    /// Creates a tree for testing various tree transformation operations.
    /// </summary>
    /// <param name="adGroupId">The ad group ID.</param>
    /// <returns>A product partition tree for running tests.</returns>
    internal ProductPartitionTree CreateTestTreeForTransformation(long adGroupId) {
      // The root ID is kept high enough so that the tree doesn't interpret
      // the nodes as candidates for ADD operation.
      return CreateTestTree(adGroupId, ProductPartitionTree.NEW_ROOT_ID + 10);
    }

    /// <summary>
    /// Creates the test tree.
    /// </summary>
    /// <param name="adGroupId">The ad group ID.</param>
    /// <param name="rootId">The root product partition ID.</param>
    /// <returns>A product partition tree for running tests.</returns>
    private ProductPartitionTree CreateTestTree(long adGroupId, long rootId) {
      // The tree structure is:
      // root
      //   Google
      //     offerA,
      //     offerB,
      //     Other
      //   Motorola
      //   Other

      long counterStart = rootId - 1;

      List<AdGroupCriterion> adGroupCriteria = new List<AdGroupCriterion>() {
        CreateCriterionForProductPartition(rootId,
            ProductPartitionTree.ROOT_PARENT_ID, null, false, false),  // root
        CreateCriterionForProductPartition(counterStart - 1L, rootId,
            BRAND_GOOGLE, true, false, 100000L), // google
        CreateCriterionForProductPartition(counterStart - 2L, rootId,
            BRAND_MOTOROLA, true, false, 50000L), // Motorola
        CreateCriterionForProductPartition(counterStart - 3L, rootId,
            BRAND_OTHER, true, true), // Other

        CreateCriterionForProductPartition(counterStart - 4L, counterStart - 1L, OFFER_A, true,
            false, 30000L), // offerA
        CreateCriterionForProductPartition(counterStart - 5L, counterStart - 1L, OFFER_B, true,
            true, 30000L), // offerB
        CreateCriterionForProductPartition(counterStart - 6L, counterStart - 1L, OFFER_OTHER, true,
            false, 50000L), // Other
      };

      return ProductPartitionTree.CreateAdGroupTree(adGroupId, adGroupCriteria);
    }

    /// <summary>
    /// Creates the criterion for product partition.
    /// </summary>
    /// <param name="partitionId">The product partition ID.</param>
    /// <param name="parentPartitionId">The proudct partition ID for parent node.</param>
    /// <param name="caseValue">The case value.</param>
    /// <param name="isUnit">True, if the node is UNIT node, false otherwise.</param>
    /// <param name="isExcluded">True, if the node is EXCLUDE node, false otherwise.</param>
    /// <returns>An ad group criterion node for the product partition.</returns>
    internal static AdGroupCriterion CreateCriterionForProductPartition(long partitionId,
        long parentPartitionId, ProductDimension caseValue, bool isUnit, bool isExcluded) {
      return CreateCriterionForProductPartition(partitionId, parentPartitionId, caseValue,
          isUnit, isExcluded, 0);
    }

    /// <summary>
    /// Creates the criterion for product partition.
    /// </summary>
    /// <param name="partitionId">The product partition ID.</param>
    /// <param name="parentPartitionId">The proudct partition ID for parent node.</param>
    /// <param name="caseValue">The case value.</param>
    /// <param name="isUnit">True, if the node is UNIT node, false otherwise.</param>
    /// <param name="isExcluded">True, if the node is EXCLUDE node, false otherwise.</param>
    /// <param name="bid">The bid to be set on a node, if it is UNIT.</param>
    /// <returns>An ad group criterion node for the product partition.</returns>
    internal static AdGroupCriterion CreateCriterionForProductPartition(long partitionId,
        long parentPartitionId, ProductDimension caseValue, bool isUnit, bool isExcluded,
        long bid) {
      AdGroupCriterion adGroupCriterion;
      ProductPartition partition = new ProductPartition() {
        id = partitionId,
        parentCriterionId = parentPartitionId,
        caseValue = caseValue,
        partitionType = isUnit ? ProductPartitionType.UNIT : ProductPartitionType.SUBDIVISION
      };

      if (isExcluded) {
        NegativeAdGroupCriterion negative = new NegativeAdGroupCriterion();
        adGroupCriterion = negative;
      } else {
        BiddableAdGroupCriterion biddable = new BiddableAdGroupCriterion();
        biddable.userStatus = UserStatus.ENABLED;

        BiddingStrategyConfiguration biddingConfig = new BiddingStrategyConfiguration();
        if (isUnit && bid != 0) {
          CpcBid cpcBid = new CpcBid() {
            bid = new Money() {
              microAmount = bid
            },
            cpcBidSource = BidSource.CRITERION
          };
          biddingConfig.bids = new Bids[] { cpcBid };
        }
        biddable.biddingStrategyConfiguration = biddingConfig;
        adGroupCriterion = biddable;
      }
      adGroupCriterion.criterion = partition;
      return adGroupCriterion;
    }

    /// <summary>
    /// Gets the operations for node.
    /// </summary>
    /// <param name="partitionNode">The partition node.</param>
    /// <param name="mutateOperations">The list of all mutate operations.</param>
    /// <returns>The list of operations that apply to partitionNode.</returns>
    internal List<AdGroupCriterionOperation> GetOperationsForNode(
        ProductPartitionNode partitionNode, AdGroupCriterionOperation[] mutateOperations) {
      ProductDimensionEqualityComparer comparer = new ProductDimensionEqualityComparer();
      List<AdGroupCriterionOperation> retval = new List<AdGroupCriterionOperation>();

      foreach (AdGroupCriterionOperation operation in mutateOperations) {
        switch (operation.@operator) {
          case Operator.SET:
          case Operator.REMOVE:
            if (operation.operand.criterion.id == partitionNode.ProductPartitionId) {
              retval.Add(operation);
            }
            break;

          case Operator.ADD:
            if (comparer.Equals((operation.operand.criterion as ProductPartition).caseValue,
                partitionNode.Dimension)) {
              retval.Add(operation);
            }
            break;
        }
      }
      return retval;
    }
  }
}

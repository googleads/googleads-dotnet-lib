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

using Google.Api.Ads.AdWords.v201603;

using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.AdWords.Util.Shopping.v201603 {

  /// <summary>
  /// Adapter that translates <see cref="ProductPartitionNode"/> objects into
  /// <see cref="AdGroupCriterion"/> objects for various operations.
  /// </summary>
  internal class ProductPartitionNodeAdapter {

    /// <summary>
    /// Creates a new AdGroupCriterion configured for a REMOVE operation.
    /// </summary>
    /// <param name="node">The node whose criterion should be removed.</param>
    /// <param name="adGroupId">The ad group ID of the criterion.</param>
    /// <returns>The AdGroupCriterion to be removed.</returns>
    internal static AdGroupCriterion CreateCriterionForRemove(ProductPartitionNode node,
        long adGroupId) {
      PreconditionUtilities.CheckNotNull(node, ShoppingMessages.NodeCannotBeNull);

      return new AdGroupCriterion() {
        adGroupId = adGroupId,
        criterion = new ProductPartition() {
          id = node.ProductPartitionId
        }
      };
    }

    /// <summary>
    /// Creates a new <see cref="AdGroupCriterion"/> configured for an
    /// <code>ADD</code> operation.
    /// </summary>
    /// <param name="node">The node whose criterion should be added.</param>
    /// <param name="adGroupId">The ad group ID of the criterion.</param>
    /// <param name="idGenerator">The temporary ID generator for new nodes.</param>
    /// <returns>An <see cref="AdGroupCriterion"/> object for <code>ADD</code>
    /// operation.</returns>
    internal static AdGroupCriterion CreateCriterionForAdd(ProductPartitionNode node,
        long adGroupId, TemporaryIdGenerator idGenerator) {
      PreconditionUtilities.CheckNotNull(node, ShoppingMessages.NodeCannotBeNull);

      AdGroupCriterion adGroupCriterion;

      if (node.IsExcludedUnit) {
        adGroupCriterion = new NegativeAdGroupCriterion();
      } else {
        adGroupCriterion = new BiddableAdGroupCriterion() {
          biddingStrategyConfiguration = node.GetBiddingConfig()
        };
      }
      adGroupCriterion.adGroupId = adGroupId;
      adGroupCriterion.criterion = node.GetCriterion();

      adGroupCriterion.criterion.id = node.ProductPartitionId;
      if (node.Parent != null) {
        (adGroupCriterion.criterion as ProductPartition).parentCriterionId =
           node.Parent.ProductPartitionId;
      }

      return adGroupCriterion;
    }

    /// <summary>
    /// Creates a new AdGroupCriterion configured for a SET operation that will
    /// set the criterion's bid.
    /// </summary>
    /// <param name="node">The node whose criterion should be updated.</param>
    /// <param name="adGroupId">The ad group ID of the criterion.</param>
    /// <param name="biddingConfig">The bidding strategy configuration of the
    /// criterion.</param>
    /// <returns>The AdGroupCriterion for SET operation.</returns>
    internal static AdGroupCriterion CreateCriterionForSetBid(ProductPartitionNode node,
        long adGroupId) {
      PreconditionUtilities.CheckNotNull(node, ShoppingMessages.NodeCannotBeNull);
      PreconditionUtilities.CheckArgument(node.IsBiddableUnit,
          string.Format(ShoppingMessages.NodeForBidUpdateIsNotBiddable, node.ProductPartitionId));

      return new BiddableAdGroupCriterion() {
        adGroupId = adGroupId,
        criterion = node.GetCriterion(),
        biddingStrategyConfiguration = node.GetBiddingConfig()
      };
    }
  }
}

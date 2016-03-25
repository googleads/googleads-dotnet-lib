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
using Google.Api.Ads.Common.Logging;
using Google.Api.Ads.Common.Util;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.AdWords.Util.Shopping.v201603 {

  /// <summary>
  /// A ProductPartitionTree is a container for a root <see cref="ProductPartitionNode"/>
  /// that also handles applying changes made to the tree under the root.
  /// </summary>
  public class ProductPartitionTree {

    /// <summary>
    /// The feature ID for this class.
    /// </summary>
    private const AdsFeatureUsageRegistry.Features FEATURE_ID =
        AdsFeatureUsageRegistry.Features.ProductPartitionTree;

    /// <summary>
    /// The registry for saving feature usage information..
    /// </summary>
    private readonly AdsFeatureUsageRegistry featureUsageRegistry =
        AdsFeatureUsageRegistry.Instance;

    /// <summary>
    /// The ad group ID for this tree.
    /// </summary>
    private readonly long adGroupId;

    /// <summary>
    /// The root node of this tree.
    /// </summary>
    private readonly ProductPartitionNode root;

    /// <summary>
    /// The <em>original</em> root node of this tree. This will be null if this
    /// tree's ad group originally contained no nodes, e.g., the ad group was
    /// created via the API. Otherwise, it will be a deep copy of the
    /// ad group's original root node.
    /// </summary>
    /// <remarks>This root will be used to detect changes made to the tree
    /// under <see cref="root"/>. See <see cref="CreateMutateOperationPairs"/>
    /// </remarks>
    private readonly ProductPartitionNode originalRoot;

    /// <summary>
    /// The comparer for comparing the product dimensions.
    /// </summary>
    private readonly ProductDimensionEqualityComparer dimensionComparator;

    /// <summary>
    /// The parent ID for root node.
    /// </summary>
    public const int ROOT_PARENT_ID = 0;

    /// <summary>
    /// The ID for a new root node.
    /// </summary>
    public const int NEW_ROOT_ID = -1;

    /// <summary>
    /// Required fields for any <see cref="Selector"/> used to fetch
    /// <see cref="AdGroupCriterion"/>{@link AdGroupCriterion} objects used by
    /// an instance of this class.
    /// </summary>
    private static readonly string[] REQUIRED_SELECTOR_FIELD_ENUMS = new string[] {
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

    /// <summary>
    /// Gets the ad group ID for this tree.
    /// </summary>
    public long AdGroupId {
      get {
        return adGroupId;
      }
    }

    /// <summary>
    /// Gets the root node of this tree.
    /// </summary>
    public ProductPartitionNode Root {
      get {
        return root;
      }
    }

    /// <summary>
    /// Downloads the product partition criteria from an ad group.
    /// </summary>
    /// <param name="user">The AdWords user instance that owns the ad group.
    /// </param>
    /// <param name="adGroupId">The ad group ID.</param>
    /// <returns>A list of product partition criteria.</returns>
    private static List<AdGroupCriterion> DownloadCriteria(AdWordsUser user, long adGroupId) {
      List<AdGroupCriterion> retval = new List<AdGroupCriterion>();

      // Get the AdGroupCriterionService.
      AdGroupCriterionService adGroupCriterionService = (AdGroupCriterionService)
          user.GetService(AdWordsService.v201603.AdGroupCriterionService);

      Selector selector = new Selector() {
        fields = REQUIRED_SELECTOR_FIELD_ENUMS,
        predicates = new Predicate[] {
          new Predicate() {
            field = "AdGroupId",
            @operator = PredicateOperator.EQUALS,
            values = new string[] {adGroupId.ToString()}
          },
          new Predicate() {
            field = "CriteriaType",
            @operator = PredicateOperator.EQUALS,
            values = new string[] {"PRODUCT_PARTITION"}
          },
          new Predicate() {
            field = "Status",
            @operator = PredicateOperator.IN,
            values = new string[] {
                UserStatus.ENABLED.ToString(),
                UserStatus.PAUSED.ToString()
            }
          },
        },
        paging = Paging.Default
      };

      AdGroupCriterionPage page;

      do {
        // Get the next page of results.
        page = adGroupCriterionService.get(selector);

        if (page != null && page.entries != null) {
          retval.AddRange(page.entries);
          selector.paging.IncreaseOffset();
        }
      } while (selector.paging.startIndex < page.totalNumEntries);

      return retval;
    }

    /// <summary>
    /// Creates a map with key as the parent criterion ID and value as the list of child nodes.
    /// </summary>
    /// <param name="adGroupCriteria">The list of ad group criteria.</param>
    /// <returns>A criteria map.</returns>
    private static Dictionary<long, List<AdGroupCriterion>> CreateParentIdMap(
        List<AdGroupCriterion> adGroupCriteria) {
      PreconditionUtilities.CheckNotNull(adGroupCriteria, ShoppingMessages.CriteriaListNull);

      Dictionary<long, List<AdGroupCriterion>> parentIdMap =
          new Dictionary<long, List<AdGroupCriterion>>();

      foreach (AdGroupCriterion adGroupCriterion in adGroupCriteria) {
        PreconditionUtilities.CheckNotNull(adGroupCriterion.criterion,
            ShoppingMessages.AdGroupCriterionNull);
        if (adGroupCriterion is BiddableAdGroupCriterion) {
          BiddableAdGroupCriterion biddableCriterion = (BiddableAdGroupCriterion) adGroupCriterion;

          PreconditionUtilities.CheckState(biddableCriterion.userStatusSpecified,
              string.Format(ShoppingMessages.UserStatusNotSpecified,
              biddableCriterion.criterion.id));

          if (biddableCriterion.userStatus == UserStatus.REMOVED) {
            continue;
          }
        }

        if (adGroupCriterion.criterion is ProductPartition) {
          ProductPartition partition = (ProductPartition) adGroupCriterion.criterion;
          List<AdGroupCriterion> children = null;
          if (!parentIdMap.TryGetValue(partition.parentCriterionId, out children)) {
            children = new List<AdGroupCriterion>();
            parentIdMap[partition.parentCriterionId] = children;
          }
          children.Add(adGroupCriterion);
        }
      }
      return parentIdMap;
    }

    /// <summary>
    /// Creates a new tree by retrieving the product partition of the specified
    /// ad group.
    /// </summary>
    /// <param name="user">The user that owns the ad group..</param>
    /// <param name="adGroupId">The ad group ID.</param>
    /// <returns>An in-memory representation of the product partition tree
    /// in this ad group.</returns>
    public static ProductPartitionTree DownloadAdGroupTree(AdWordsUser user, long adGroupId) {
      List<AdGroupCriterion> criteria = DownloadCriteria(user, adGroupId);
      return CreateAdGroupTree(adGroupId, criteria);
    }

    /// <summary>
    /// Creates a new ProductPartitionTree based on the collection of ad group
    /// criteria provided.
    /// </summary>
    /// <remarks>If retrieving existing criteria for use with this method,
    /// you must include all of the fields in <see cref="REQUIRED_SELECTOR_FIELDS" />
    /// in your selector.</remarks>
    /// <param name="adGroupCriteria">The list of ad group criteria.</param>
    /// <returns></returns>
    public static ProductPartitionTree CreateAdGroupTree(List<AdGroupCriterion> adGroupCriteria) {
      // Mark the usage.
      return CreateAdGroupTree(0, adGroupCriteria);
    }

    /// <summary>
    /// Creates a new ProductPartitionTree based on the collection of ad group
    /// criteria provided.
    /// </summary>
    /// <remarks>If retrieving existing criteria for use with this method,
    /// you must include all of the fields in <see cref="REQUIRED_SELECTOR_FIELDS" />
    /// in your selector.</remarks>
    /// <param name="adGroupId">The ad group ID.</param>
    /// <param name="adGroupCriteria">The list of ad group criteria.</param>
    /// <returns></returns>
    public static ProductPartitionTree CreateAdGroupTree(long adGroupId,
        List<AdGroupCriterion> adGroupCriteria) {
      PreconditionUtilities.CheckNotNull(adGroupCriteria, ShoppingMessages.CriteriaListNull);

      Dictionary<long, List<AdGroupCriterion>> parentIdMap = CreateParentIdMap(adGroupCriteria);
      
      ProductPartitionTree retval = CreateAdGroupTree(adGroupId, parentIdMap);

      // Mark the usage.
      retval.featureUsageRegistry.MarkUsage(FEATURE_ID);
      return retval;
    }

    /// <summary>
    /// Returns a new tree based on a non-empty collection of ad group criteria.
    /// </summary>
    /// <param name="adGroupId">The ad group ID.</param>
    /// <param name="parentIdMap">The multimap from parent product partition ID
    /// to child criteria.</param>
    /// <returns>a new product partition tree.</returns>
    private static ProductPartitionTree CreateAdGroupTree(long adGroupId,
        Dictionary<long, List<AdGroupCriterion>> parentIdMap) {
      ProductPartitionNode rootNode = null;

      if (parentIdMap.Count == 0) {
        rootNode = new ProductPartitionNode(null, null, NEW_ROOT_ID);
      } else {
        PreconditionUtilities.CheckState(parentIdMap.ContainsKey(ROOT_PARENT_ID),
            string.Format(ShoppingMessages.RootCriteriaNotFoundInCriteriaList, adGroupId));

        PreconditionUtilities.CheckState(parentIdMap[ROOT_PARENT_ID].Count == 1,
            string.Format(ShoppingMessages.MoreThanOneRootFound, adGroupId));

        AdGroupCriterion rootCriterion = parentIdMap[ROOT_PARENT_ID][0];

        PreconditionUtilities.CheckState(rootCriterion is BiddableAdGroupCriterion,
            string.Format(ShoppingMessages.RootCriterionIsNotBiddable, adGroupId));

        BiddableAdGroupCriterion biddableRootCriterion = (BiddableAdGroupCriterion) rootCriterion;

        rootNode = new ProductPartitionNode(null, null, rootCriterion.criterion.id,
            new ProductDimensionEqualityComparer());

        // Set the root's bid if a bid exists on the BiddableAdGroupCriterion.
        Money rootNodeBid = GetBid(biddableRootCriterion);

        if (rootNodeBid != null) {
          rootNode.AsBiddableUnit().CpcBid = rootNodeBid.microAmount;
        }

        AddChildNodes(rootNode, parentIdMap);
      }
      return new ProductPartitionTree(adGroupId, rootNode);
    }

    /// <summary>
    /// Constructor that initializes the temp ID generator based on the ID of
    /// the root node.
    /// </summary>
    /// <param name="adGroupId">the ID of the ad group.</param>
    /// <param name="root">The root node of the tree.</param>
    private ProductPartitionTree(long adGroupId, ProductPartitionNode root) {
      PreconditionUtilities.CheckNotNull(root, ShoppingMessages.RootNodeCannotBeNull);

      this.adGroupId = adGroupId;
      this.root = root;
      this.dimensionComparator = new ProductDimensionEqualityComparer();

      // If this is an existing node in an ad group, then the tree should be
      // cloned so that we can keep track of the original state of the tree.
      if (this.Root.ProductPartitionId > 0) {
        originalRoot = this.Root.Clone();
      }
    }

    /// <summary>
    /// Gets the criterion-level bid, or null if no such bid exists.
    /// </summary>
    /// <param name="biddableCriterion">The biddable criterion.</param>
    /// <returns>The criterion-level bid, or null if no such bid exists.
    /// </returns>
    private static Money GetBid(BiddableAdGroupCriterion biddableCriterion) {
      BiddingStrategyConfiguration biddingConfig = biddableCriterion.biddingStrategyConfiguration;
      Money cpcBidAmount = null;
      if (biddingConfig != null && biddingConfig.bids != null) {
        foreach (Bids bid in biddingConfig.bids) {
          if (bid is CpcBid) {
            CpcBid cpcBid = (CpcBid) bid;
            if (cpcBid.cpcBidSource == BidSource.CRITERION) {
              cpcBidAmount = cpcBid.bid;
              break;
            }
          }
        }
      }
      return cpcBidAmount;
    }

    /// <summary>
    /// Using the criteria in <paramref name="parentIdMap"/>, recursively adds
    /// all children under the partition ID of <paramref name="parentNode"/> to
    /// <paramref name="parentNode"/>.
    /// </summary>
    /// <param name="parentNode">The parent node.</param>
    /// <param name="parentIdMap">The multimap from parent partition ID to list
    /// of child criteria</param>
    private static void AddChildNodes(ProductPartitionNode parentNode,
        Dictionary<long, List<AdGroupCriterion>> parentIdMap) {
      List<AdGroupCriterion> childCriteria = null;
      if (parentIdMap.ContainsKey(parentNode.ProductPartitionId)) {
        childCriteria = parentIdMap[parentNode.ProductPartitionId];
      }

      // no children, return.
      if (childCriteria == null || childCriteria.Count == 0) {
        return;
      }

      // Ensure that the parent is a subdivision.
      parentNode.AsSubdivision();

      foreach (AdGroupCriterion childCriterion in childCriteria) {
        ProductPartition partition = (ProductPartition) childCriterion.criterion;
        ProductPartitionNode childNode = parentNode.AddChild(partition.caseValue);
        childNode.ProductPartitionId = partition.id;

        if (childCriterion is BiddableAdGroupCriterion) {
          childNode = childNode.AsBiddableUnit();
          Money cpcBidAmount = GetBid((BiddableAdGroupCriterion) childCriterion);
          if (cpcBidAmount != null) {
            childNode.CpcBid = cpcBidAmount.microAmount;
          }
        } else {
          childNode = childNode.AsExcludedUnit();
        }

        AddChildNodes(childNode, parentIdMap);
      }
    }

    /// <summary>
    /// Gets the mutate operations that will apply the changes made to this tree.
    /// </summary>
    /// <returns>The list of mutate operations.</returns>
    public AdGroupCriterionOperation[] GetMutateOperations() {
      // Mark the usage.
      featureUsageRegistry.MarkUsage(FEATURE_ID);;

      List<AdGroupCriterionOperation> operations = new List<AdGroupCriterionOperation>();
      foreach (OperationPair operationPair in CreateMutateOperationPairs()) {
        operations.Add(operationPair.Operation);
      }
      return operations.ToArray();
    }

    /// <summary>
    /// Creates and returns the pairs of node/operation that will apply the
    /// changes made to this tree.
    /// </summary>
    /// <returns>The list of operation pairs.</returns>
    internal List<OperationPair> CreateMutateOperationPairs() {
      TemporaryIdGenerator idGenerator = new TemporaryIdGenerator();
      List<OperationPair> ops = new List<OperationPair>();
      AddMutateOperations(this.originalRoot, this.root, ops, idGenerator);
      return ops;
    }

    /// <summary>
    /// Adds to the operations list all operations required to mutate
    /// <paramref name="originalNode"/> to the state* of
    /// <paramref name="newNode"/>.
    /// </summary>
    /// <param name="originalNode">The original node.</param>
    /// <param name="newNode">The new node.</param>
    /// <param name="ops">The operations list to add to.</param>
    /// <param name="idGenerator">The temporary ID generator for ADD operations.</param>
    /// <returns>The set of child product dimensions that require further
    /// processing.</returns>
    private void AddMutateOperations(ProductPartitionNode originalNode,
        ProductPartitionNode newNode, List<OperationPair> ops, TemporaryIdGenerator idGenerator) {
      NodeDifference nodeDifference = Diff(originalNode, newNode, dimensionComparator);
      bool isProcessChildren;

      switch (nodeDifference) {
        case NodeDifference.NEW_NODE:
          ops.AddRange(CreateAddOperations(newNode, idGenerator));
          // No need to further process children. The ADD operations above will include operations
          // for all children of newNode.
          isProcessChildren = false;
          break;

        case NodeDifference.REMOVED_NODE:
          ops.Add(CreateRemoveOperation(originalNode));
          // No need to further process children. The REMOVE operation above will perform a
          // cascading delete of all children of newNode.
          isProcessChildren = false;
          break;

        case NodeDifference.PARTITION_TYPE_CHANGE:
        case NodeDifference.EXCLUDED_UNIT_CHANGE:
          ops.Add(CreateRemoveOperation(originalNode));
          ops.AddRange(CreateAddOperations(newNode, idGenerator));
          // No need to further process children. The ADD operations above will include operations
          // for all children of newNode.
          isProcessChildren = false;
          break;

        case NodeDifference.BID_CHANGE:
          // Ensure that the new node has the proper ID (this may have been lost if the node
          // was removed and then re-added).
          newNode.ProductPartitionId = originalNode.ProductPartitionId;
          ops.Add(CreateSetBidOperation(newNode));
          // Process the children of newNode. The SET operation above will only handle changes
          // made to newNode, not its children.
          isProcessChildren = true;
          break;

        case NodeDifference.NONE:
          // Ensure that the new node has the proper ID (this may have been lost if the node
          // was removed and then re-added).
          newNode.ProductPartitionId = originalNode.ProductPartitionId;
          // This node does not have changes, but its children may.
          isProcessChildren = true;
          break;

        default:
          throw new InvalidOperationException("Unrecognized difference: " + nodeDifference);
      }

      if (isProcessChildren) {
        // Try to match the children in new and original trees to identify the
        // matching dimensions.

        foreach (ProductPartitionNode newChild in newNode.Children) {
          if (originalNode.HasChild(newChild.Dimension)) {
            // this is probably an edit.
            AddMutateOperations(originalNode.GetChild(newChild.Dimension), newChild, ops,
                idGenerator);
          } else {
            // this is a new node.
            AddMutateOperations(null, newChild, ops, idGenerator);
          }
        }

        foreach (ProductPartitionNode originalChild in originalNode.Children) {
          if (newNode.HasChild(originalChild.Dimension)) {
            // this is probably an edit. We dealt with it before
            continue;
          } else {
            // this is a removed node.
            AddMutateOperations(originalChild, null, ops, idGenerator);
          }
        }
      }
    }

    /// <summary>
    /// Creates a SET operation for the specified node.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <returns>An operation pair for the specified operation and node.
    /// </returns>
    private OperationPair CreateSetBidOperation(ProductPartitionNode node) {
      // TODO(Anash): This check is dangerous, since we should only depend on parent-child
      // relationships, not ID relationships.
      PreconditionUtilities.CheckArgument(node.ProductPartitionId >= NEW_ROOT_ID,
          string.Format(ShoppingMessages.NodeForSetCannotHaveNegativeId, node));
      AdGroupCriterionOperation setOp = new AdGroupCriterionOperation();
      setOp.@operator = Operator.SET;
      setOp.operand = ProductPartitionNodeAdapter.CreateCriterionForSetBid(node, adGroupId);

      return new OperationPair(node, setOp);
    }

    /// <summary>
    /// Creates ADD operations for the node and ALL of its children.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="idGenerator">The temporary ID generator for new nodes.</param>
    /// <returns>A list of operation pair for the specified operation and nodes.
    /// </returns>
    private List<OperationPair> CreateAddOperations(ProductPartitionNode node,
        TemporaryIdGenerator idGenerator) {
      AdGroupCriterionOperation addOp = new AdGroupCriterionOperation();
      addOp.@operator = Operator.ADD;

      // Overwrite the ID set by the user when doing ADD operations. This
      // minimizes the chances of a malformed tree.
      node.ProductPartitionId = idGenerator.Next;

      addOp.operand = ProductPartitionNodeAdapter.CreateCriterionForAdd(node, adGroupId,
          idGenerator);

      List<OperationPair> operationsList = new List<OperationPair>();
      operationsList.Add(new OperationPair(node, addOp));

      // Recursively add all of this node's children to the operations list.
      foreach (ProductPartitionNode child in node.Children) {
        operationsList.AddRange(CreateAddOperations(child, idGenerator));
      }
      return operationsList;
    }

    /// <summary>
    /// Creates a REMOVE operation for the specified node.
    /// </summary>
    /// <param name="node">The node to be removed.</param>
    /// <returns>An operation pair for the node and the REMOVE operation.
    /// </returns>
    private OperationPair CreateRemoveOperation(ProductPartitionNode node) {
      PreconditionUtilities.CheckArgument(node.ProductPartitionId >= NEW_ROOT_ID,
          string.Format(ShoppingMessages.NodeForRemoveCannotHaveNegativeId,
              node.ProductPartitionId));

      AdGroupCriterionOperation removeOp = new AdGroupCriterionOperation();
      removeOp.@operator = Operator.REMOVE;
      removeOp.operand = ProductPartitionNodeAdapter.CreateCriterionForRemove(node, adGroupId);

      return new OperationPair(node, removeOp);
    }

    /// <summary>
    /// Returns the <see cref="NodeDifference"/> between the original node and
    /// the new node.
    /// </summary>
    /// <param name="originalNode">The original node.</param>
    /// <param name="newNode">The new node.</param>
    /// <param name="dimensionComparator">The dimension comparator.</param>
    private static NodeDifference Diff(ProductPartitionNode originalNode,
        ProductPartitionNode newNode, IEqualityComparer<ProductDimension> dimensionComparator) {
      NodeDifference nodeDifference;
      if (originalNode == null && newNode == null) {
        nodeDifference = NodeDifference.NONE;
      } else if (originalNode == null) {
        nodeDifference = NodeDifference.NEW_NODE;
      } else if (newNode == null) {
        nodeDifference = NodeDifference.REMOVED_NODE;
      } else if (!dimensionComparator.Equals(originalNode.Dimension, newNode.Dimension)) {
        throw new InvalidOperationException(string.Format(
            ShoppingMessages.ProductDimensionMismatch, originalNode.Dimension, newNode.Dimension));
      } else if (originalNode.IsUnit != newNode.IsUnit) {
        nodeDifference = NodeDifference.PARTITION_TYPE_CHANGE;
      } else if (originalNode.IsExcludedUnit != newNode.IsExcludedUnit) {
        nodeDifference = NodeDifference.EXCLUDED_UNIT_CHANGE;
      } else if (!originalNode.IsExcludedUnit && originalNode.IsUnit && newNode.IsUnit) {
        // Both nodes are non-excluded units - the only possible difference
        // left is the bid.
        nodeDifference = (originalNode.CpcBid != newNode.CpcBid) ? NodeDifference.BID_CHANGE :
            NodeDifference.NONE;
      } else {
        nodeDifference = NodeDifference.NONE;
      }
      return nodeDifference;
    }

    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String" /> that represents this instance.
    /// </returns>
    public override string ToString() {
      // Mark the usage.
      featureUsageRegistry.MarkUsage(FEATURE_ID);;

      return String.Format("AdGroupID: {0}\nTree:\n{1}", this.adGroupId, this.Root);
    }
  }
}

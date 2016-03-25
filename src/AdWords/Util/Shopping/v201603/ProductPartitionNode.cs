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

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.AdWords.Util.Shopping.v201603 {

  public class ProductPartitionNode {

    /// <summary>
    /// The parent of this node.
    /// </summary>
    private readonly ProductPartitionNode parentNode;

    /// <summary>
    /// The product partition ID.
    /// </summary>
    private long productPartitionId;

    /// <summary>
    /// The node state.
    /// </summary>
    private NodeState nodeState;

    /// <summary>
    /// The product dimension node that this node wraps.
    /// </summary>
    private readonly ProductDimension dimension;

    /// <summary>
    /// A map from ProductDimension to child ProductPartitionNode.
    /// </summary>
    /// <remarks>This map uses a custom comparator for sorting purposes.</remarks>
    private readonly Dictionary<ProductDimension, ProductPartitionNode> children;

    /// <summary>
    /// Gets or sets the product partition ID.
    /// </summary>
    public long ProductPartitionId {
      get {
        return productPartitionId;
      }
      set {
        productPartitionId = value;
      }
    }

    /// <summary>
    /// Gets the product dimension.
    /// </summary>
    public ProductDimension Dimension {
      get {
        return dimension;
      }
    }

    /// <summary>
    /// Returns true if this node's partition type is SUBDIVISION, false
    /// otherwise.
    /// </summary>
    public bool IsSubdivision {
      get {
        return nodeState.NodeType == NodeType.SUBDIVISION;
      }
    }

    /// <summary>
    /// Returns true if this node's partition type is UNIT, false otherwise.
    /// </summary>
    public bool IsUnit {
      get {
        return nodeState.NodeType == NodeType.BIDDABLE_UNIT
            || nodeState.NodeType == NodeType.EXCLUDED_UNIT;
      }
    }

    /// <summary>
    /// Gets all the children of this node.
    /// </summary>
    public IEnumerable<ProductPartitionNode> Children {
      get {
        return children.Values;
      }
    }

    /// <summary>
    /// Returns the parent node of this node. The returned node will be
    /// <code>null</code> if this is the root node.
    /// </summary>
    public ProductPartitionNode Parent {
      get {
        return parentNode;
      }
    }

    /// <summary>
    /// Determines whether this instance has any children. Returns true if this
    /// instance has child nodes, false otherwise.
    /// </summary>
    private bool HasChildren {
      get {
        return children.Count != 0;
      }
    }

    /// <summary>
    /// Returns true if this node's partition type is UNIT and is biddable
    /// (not excluded), false otherwise.
    /// </summary>
    public bool IsBiddableUnit {
      get {
        return nodeState.NodeType == NodeType.BIDDABLE_UNIT;
      }
    }

    /// <summary>
    /// Returns true if this node's partition type is UNIT and is excluded
    /// (not biddable), false otherwise.
    /// </summary>
    public bool IsExcludedUnit {
      get {
        return nodeState.NodeType == NodeType.EXCLUDED_UNIT;
      }
    }

    /// <summary>
    /// Gets or sets the bid.
    /// </summary>
    public long CpcBid {
      get {
        return nodeState.BidInMicros;
      }
      set {
        nodeState.BidInMicros = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether bid is specified.
    /// </summary>
    public bool CpcBidSpecified {
      get {
        return nodeState.BidInMicrosSpecified;
      }
      set {
        nodeState.BidInMicrosSpecified = value;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductPartitionNode"/>
    /// class.
    /// </summary>
    /// <param name="parentNode">The parent node.</param>
    /// <param name="dimension">The product dimension that this node wraps.</param>
    public ProductPartitionNode(ProductPartitionNode parentNode, ProductDimension dimension)
      : this(parentNode, dimension, 0, new ProductDimensionEqualityComparer()) {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductPartitionNode"/>
    /// class.
    /// </summary>
    /// <param name="parentNode">The parent node.</param>
    /// <param name="dimension">The product dimension that this node wraps.</param>
    /// <param name="productPartitionId">The product partition ID.</param>
    public ProductPartitionNode(ProductPartitionNode parentNode, ProductDimension dimension,
        long productPartitionId)
      : this(parentNode, dimension, productPartitionId,
        new ProductDimensionEqualityComparer()) {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductPartitionNode"/>
    /// class.
    /// </summary>
    /// <param name="parentNode">The parent node.</param>
    /// <param name="dimension">The product dimension that this node wraps.</param>
    /// <param name="productPartitionId">The product partition ID.</param>
    /// <param name="comparer">The comparer for comparing instances of this
    /// product dimension.</param>
    public ProductPartitionNode(ProductPartitionNode parentNode, ProductDimension dimension,
        long productPartitionId, IEqualityComparer<ProductDimension> comparer) {
      this.parentNode = parentNode;
      this.dimension = dimension;
      this.children = new Dictionary<ProductDimension, ProductPartitionNode>(comparer);
      this.productPartitionId = productPartitionId;
      this.nodeState = new BiddableUnitState();
    }

    /// <summary>
    /// Modifies this node to be a SUBDIVISION node.
    /// </summary>
    /// <returns>This node, updated to a subdivision node.</returns>
    public ProductPartitionNode AsSubdivision() {
      nodeState = nodeState.TransitionTo(NodeType.SUBDIVISION);
      return this;
    }

    /// <summary>
    /// Returns the child node with the specified ProductDimension.
    /// </summary>
    /// <param name="dimension">The product dimension.</param>
    /// <returns></returns>
    public ProductPartitionNode GetChild(ProductDimension dimension) {
      if (!HasChild(dimension)) {
        throw new ArgumentException(string.Format(
            ShoppingMessages.NoChildNodeFoundForDimension, dimension));
      }
      return children[dimension];
    }

    /// <summary>
    /// Determines whether this node has a child with the specified dimension.
    /// </summary>
    /// <param name="dimension">The child dimension.</param>
    /// <returns>True, if the child node exists, false otherwise.</returns>
    public bool HasChild(ProductDimension dimension) {
      return dimension != null ? children.ContainsKey(dimension) : false;
    }

    /// <summary>
    /// Adds a NEW child for <code>childDimension</code> under this node.
    /// </summary>
    /// <param name="childDimension">The <code>ProductDimension</code> for the
    /// new child</param>
    /// <returns>The newly created child node.</returns>
    public ProductPartitionNode AddChild(ProductDimension childDimension) {
      // Passing a productPartitionId = 0 is insignificant here.
      ProductPartitionNode newChild = new ProductPartitionNode(this, childDimension, 0,
          children.Comparer);
      return AddChild(newChild);
    }

    /// <summary>
    /// Adds the childNode as a child under this node.
    /// </summary>
    /// <param name="childNode">The child node.</param>
    /// <returns>The child node.</returns>
    /// <exception cref="System.ArgumentException"> if the parent node already
    /// contains the child node's dimension.</exception>
    public ProductPartitionNode AddChild(ProductPartitionNode childNode) {
      if (!this.IsSubdivision) {
        throw new ArgumentException(string.Format(ShoppingMessages.ParentNodeIsNotSubdivision,
            this.Dimension));
      }
      if (children.ContainsKey(childNode.Dimension)) {
        throw new ArgumentException(string.Format(ShoppingMessages.ChildNodeExists,
            childNode.Dimension));
      }
      children.Add(childNode.Dimension, childNode);
      return childNode;
    }

    /// <summary>
    /// Removes a child node that has matching dimension with the child node.
    /// </summary>
    /// <param name="childDimension">The child node.</param>
    /// <returns>This node.</returns>
    public ProductPartitionNode RemoveChild(ProductPartitionNode childNode) {
      return RemoveChild(childNode.Dimension);
    }

    /// <summary>
    /// Removes the child with the specified dimension.
    /// </summary>
    /// <param name="childDimension">The child dimension.</param>
    /// <returns>This node.</returns>
    public ProductPartitionNode RemoveChild(ProductDimension childDimension) {
      if (!children.ContainsKey(childDimension)) {
        throw new ArgumentException(string.Format(ShoppingMessages.ChildNodeDoesNotExist,
            childDimension));
      }
      children.Remove(childDimension);
      return this;
    }

    /// <summary>
    /// Removes all children of this node.
    /// </summary>
    /// <returns>This node.</returns>
    public ProductPartitionNode RemoveAllChildren() {
      children.Clear();
      return this;
    }

    /// <summary>
    /// Removes all children from this node and modifies this node to be a UNIT
    /// node excluded from bidding.
    /// </summary>
    /// <returns>This node, updated to an excluded node.</returns>
    public ProductPartitionNode AsExcludedUnit() {
      PreconditionUtilities.CheckState(this.Parent != null, ShoppingMessages.RootCannotBeExcluded);
      nodeState = nodeState.TransitionTo(NodeType.EXCLUDED_UNIT);
      RemoveAllChildren();
      return this;
    }

    /// <summary>
    /// Removes all children from this node and modifies this node to be a UNIT
    /// node that is biddable.
    /// </summary>
    /// <returns>This node, updated to a biddable node.</returns>
    public ProductPartitionNode AsBiddableUnit() {
      nodeState = nodeState.TransitionTo(NodeType.BIDDABLE_UNIT);
      RemoveAllChildren();
      return this;
    }

    /// <summary>
    /// Constructs a <see cref="ProductPartition" /> criterion corresponding to
    /// the node.
    /// </summary>
    /// <returns>The <see cref="ProductPartition" /> node.</returns>
    internal ProductPartition GetCriterion() {
      ProductPartition partition = new ProductPartition();
      partition.id = this.ProductPartitionId;

      if (this.Parent != null) {
        partition.parentCriterionId = this.Parent.ProductPartitionId;
      }
      partition.caseValue = this.Dimension;
      partition.partitionType = this.IsUnit ? ProductPartitionType.UNIT :
          ProductPartitionType.SUBDIVISION;
      return partition;
    }

    /// <summary>
    /// Constructs a the bidding configuration object corresponding to the node.
    /// </summary>
    /// <returns>The bidding configuration node.</returns>
    /// <remarks>A <code>null</code> may be returned if a bid is not allowed
    /// on this node, or if the bid is not specified.</remarks>
    internal BiddingStrategyConfiguration GetBiddingConfig() {
      BiddingStrategyConfiguration biddingConfig = null;
      if (this.CpcBidSpecified && this.IsUnit) {
        biddingConfig = new BiddingStrategyConfiguration();

        Money bidMoney = new Money() {
          microAmount = this.CpcBid
        };

        CpcBid cpcBid = new CpcBid() {
          bid = bidMoney,
          cpcBidSource = BidSource.CRITERION
        };

        biddingConfig.bids = new Bids[] { cpcBid };
      }
      return biddingConfig;
    }

    /// <summary>
    /// Clones this instance.
    /// </summary>
    /// <returns>The new node.</returns>
    internal ProductPartitionNode Clone() {
      ProductDimension newDimension = null;

      if (this.Dimension != null) {
        newDimension = (ProductDimension) SerializationUtilities.CloneObject(
         this.Dimension);
      }
      ProductPartitionNode newNode = new ProductPartitionNode(null, newDimension,
            this.ProductPartitionId, this.children.Comparer);
      newNode = CopyProperties(this, newNode);
      newNode.CloneChildrenFrom(this.Children);
      return newNode;
    }

    /// <summary>
    /// Deeply clones each child in <paramref name="children"/> and attaches it
    /// to <paramref name="newParent"/>.
    /// </summary>
    /// <param name="newParent">The new parent to which the cloned children
    /// will be added</param>
    /// <param name="children">The children to clone</param>
    /// <param name="minimumId">The minimum ID to compare to.</param>
    /// <returns>The minimum product partition ID found within the subtrees
    /// under <paramref name="children"/>.</returns>
    private void CloneChildrenFrom(IEnumerable<ProductPartitionNode> children) {
      foreach (ProductPartitionNode childNode in children) {
        if (!this.IsSubdivision) {
          this.AsSubdivision();
        }

        // Clone the child and add it to newParent's collection of children.
        ProductDimension newDimension = (ProductDimension) SerializationUtilities.CloneObject(
            childNode.Dimension);
        ProductPartitionNode newChild = this.AddChild(newDimension);
        newChild = CopyProperties(childNode, newChild);

        newChild.CloneChildrenFrom(childNode.Children);
      }
    }

    /// <summary>
    /// Performs a <em>shallow</em> copy of properties from
    /// <paramref name="fromNode"/> to <paramref name="toNode"/>.
    /// </summary>
    /// <param name="fromNode">The node to copy from.</param>
    /// <param name="toNode">The node to copy to.</param>
    /// <returns><paramref name="toNode"/>, with its properties updated.</returns>
    /// <remarks>Does <em>not</em> change the parent node of
    /// <paramref name="toNode"/>.</remarks>
    public static ProductPartitionNode CopyProperties(ProductPartitionNode fromNode,
        ProductPartitionNode toNode) {
      switch (fromNode.nodeState.NodeType) {
        case NodeType.BIDDABLE_UNIT:
          toNode = toNode.AsBiddableUnit();
          toNode.CpcBid = fromNode.CpcBid;
          break;

        case NodeType.EXCLUDED_UNIT:
          toNode = toNode.AsExcludedUnit();
          break;

        case NodeType.SUBDIVISION:
          toNode = toNode.AsSubdivision();
          break;

        default:
          throw new InvalidOperationException(
              "Unrecognized node state: " + fromNode.nodeState.NodeType);
      }

      toNode.ProductPartitionId = fromNode.ProductPartitionId;
      return toNode;
    }

    /// <summary>
    /// Generates a debug string that represents this node.
    /// </summary>
    /// <param name="level">The level.</param>
    /// <returns></returns>
    private string DebugString(int level) {
      string dimensionType = (this.dimension != null) ?
          this.dimension.ProductDimensionType : "ROOT";
      List<string> parts = new List<string>();
      string debugText = string.Format("{0} * Dimension = {1}[{2}], State={3}, ID={4}",
          string.Empty.PadLeft(level * 2, ' '), dimensionType, this.dimension,
          this.nodeState.NodeType, this.productPartitionId);

      parts.Add(debugText);
      foreach (ProductPartitionNode childNode in this.children.Values) {
        parts.Add(childNode.DebugString(level + 1));
      }
      return(String.Join<string>("\n", parts));
    }

    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String" /> that represents this instance.
    /// </returns>
    public override string ToString() {
      return this.DebugString(0);
    }
  }
}

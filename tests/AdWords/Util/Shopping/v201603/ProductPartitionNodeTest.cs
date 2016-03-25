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

using NUnit.Framework;

using System;
using System.Linq;

namespace Google.Api.Ads.AdWords.Tests.Util.Shopping.v201603 {

  /// <summary>
  /// Tests for the <see cref="ProductPartitionNode"/> class.
  /// </summary>
  public class ProductPartitionNodeTest {

    /// <summary>
    /// The root node.
    /// </summary>
    private ProductPartitionNode rootNode;

    private const int ROOT_NODE_ID = ProductPartitionTree.NEW_ROOT_ID;

    /// <summary>
    /// Initializes this instance.
    /// </summary>
    [SetUp]
    public void Init() {
      rootNode = new ProductPartitionNode(null, null, ROOT_NODE_ID);
    }

    /// <summary>
    /// Tests the basic functionality for a root node.
    /// </summary>
    [Test]
    public void TestRootNodeBasicFunctionality() {
      Assert.Null(rootNode.Parent, "Parent of parentNode should be null.");
      Assert.Null(rootNode.Dimension, "ParentNode dimension should be null.");
      Assert.Null(rootNode.Dimension, "ParentNode should be null.");
      Assert.AreEqual(ROOT_NODE_ID, rootNode.ProductPartitionId,
          "Partition ID is incorrect after constructor.");

      rootNode.ProductPartitionId = -2L;
      Assert.AreEqual(-2L, rootNode.ProductPartitionId,
          "Partition ID is incorrect after setting productPartitionId.");

      Assert.That(rootNode.Children.Count() == 0, "ParentNode should not have any children.");
      Assert.True(rootNode.IsUnit, "New node should be a unit node by default");
      Assert.False(rootNode.IsSubdivision, "New node should not be a subdivision by default.");
      Assert.True(rootNode.IsBiddableUnit, "New node should be a biddable unit node by default.");
    }

    /// <summary>
    /// Tests the basic functionality for a child node.
    /// </summary>
    [Test]
    public void TestChildNodeBasicFunctionality() {
      rootNode = rootNode.AsSubdivision();
      Assert.False(rootNode.IsUnit, "Parent should not be a unit.");
      Assert.True(rootNode.IsSubdivision, "Parent should be a subdivision.");
      ProductBrand childDimension = ProductDimensions.CreateBrand("google");
      ProductPartitionNode childNode = rootNode.AddChild(childDimension);

      Assert.AreSame(childDimension, childNode.Dimension, "Child node merely wraps the " +
          "underlying dimension node.");
      Assert.AreSame(rootNode, childNode.Parent, "child.GetParent should return parentNode.");
      Assert.That(childNode.ProductPartitionId == 0, "Partition ID is incorrect.");

      Assert.That(childNode.Children.Count() == 0, "ChildNode should not have any children.");
      Assert.True(childNode.IsUnit, "New node should be a unit node by default.");
      Assert.True(childNode.IsBiddableUnit, "New node should be a biddable unit node by default.");

      Assert.That(rootNode.HasChild(childDimension), "rootNode.HasChild should return true when " +
          "passed the dimension of the added child");
      Assert.False(rootNode.HasChild(ProductDimensions.CreateBrand("xyz")), "rootNode.HasChild " +
          "should return false when passed a dimension for a nonexistent child");
      Assert.False(rootNode.HasChild(null), "rootNode.HasChild should return false when passed " +
          "a dimension for a nonexistent child");
    }

    /// <summary>
    /// Make sure that you cannot add a child node to a Unit node.
    /// </summary>
    [Test]
    public void TestAddChildToUnitFails() {
      Assert.True(rootNode.IsUnit, "Root should be a unit by default.");
      Assert.Throws<ArgumentException>(delegate() {
        rootNode.AddChild(ProductDimensions.CreateBrand("google"));
      });
    }

    /// <summary>
    /// Make sure that you cannot exclude the root node.
    /// </summary>
    [Test]
    public void TestCreateExcludedRootFails() {
      Assert.Throws<InvalidOperationException>(delegate() {
        rootNode.AsExcludedUnit();
      });
    }

    /// <summary>
    /// Make sure you cannot retrieve a child node that doesn't exist.
    /// </summary>
    [Test]
    public void TestGetChildThatDoesNotExistFails() {
      rootNode = rootNode.AsSubdivision();
      Assert.Throws<ArgumentException>(delegate() {
        rootNode.GetChild(ProductDimensions.CreateBrand("google"));
      });
    }

    /// <summary>
    /// Make sure you cannot remove a child node that doesn't exist.
    /// </summary>
    [Test]
    public void TestRemoveChildThatDoesNotExistFails() {
      rootNode = rootNode.AsSubdivision();
      Assert.Throws<ArgumentException>(delegate() {
        rootNode.RemoveChild(ProductDimensions.CreateBrand("google"));
      });
    }

    /// <summary>
    /// Make sure you cannot add an existing node again.
    /// </summary>
    [Test]
    public void TestAddChildThatExistsFails() {
      rootNode = rootNode.AsSubdivision();
      rootNode.AddChild(ProductDimensions.CreateBrand("google"));

      // Add the same child again. The call should fail.
      Assert.Throws<ArgumentException>(delegate() {
        rootNode.AddChild(ProductDimensions.CreateBrand("google"));
      });

      // Add the same child again, this time with a different case.
      // The call should fail.
      Assert.Throws<ArgumentException>(delegate() {
        rootNode.AddChild(ProductDimensions.CreateBrand("GOOGLE"));
      });
    }

    /// <summary>
    /// Make sure that setting a bid on a SUBDIVISION node fails.
    /// </summary>
    [Test]
    public void TestSetBidOnSubdivisionFails() {
      rootNode = rootNode.AsSubdivision();
      Assert.Throws<InvalidOperationException>(delegate() {
        rootNode.CpcBid = 1;
      });
    }

    /// <summary>
    /// Make sure that setting a negative bid on a UNIT node fails.
    /// </summary>
    [Test]
    public void TestSetNegativeBidFails() {
      Assert.True(rootNode.IsUnit, "root should be a unit by default.");
      Assert.Throws<ArgumentException>(delegate() {
        rootNode.CpcBid = -1;
      });
    }

    /// <summary>
    /// Tests setting bids on a <see cref="ProductPartitionNode"/>.
    /// </summary>
    [Test]
    public void TestSetBidOnUnit() {
      rootNode = rootNode.AsSubdivision();
      ProductBrand childDimension = ProductDimensions.CreateBrand("google");
      ProductPartitionNode childNode = rootNode.AddChild(childDimension);

      Assert.That(childNode.CpcBidSpecified == false, "Bid should be null by default.");

      childNode.CpcBid = 1L;

      Assert.AreEqual(1L, childNode.CpcBid, "Bid does not reflect setBid.");
      Assert.True(childNode.IsBiddableUnit, "Node should be a biddable unit.");

      childNode = childNode.AsExcludedUnit();
      Assert.True(childNode.IsExcludedUnit, "Node should be an excluded unit.");
      Assert.False(childNode.IsBiddableUnit, "Node should not be a biddable unit.");
      Assert.False(childNode.CpcBidSpecified, "Excluded unit should have a null bid");

      // Set back to biddable.
      childNode = childNode.AsBiddableUnit();
      Assert.True(childNode.IsBiddableUnit, "Node should be a biddable unit.");
    }

    /// <summary>
    /// Tests the navigation on a Product partition node and its children.
    /// </summary>
    [Test]
    public void TestNavigation() {
      rootNode = rootNode.AsSubdivision();
      ProductBrand brandGoogle = ProductDimensions.CreateBrand("google");
      ProductBrand brandOther = ProductDimensions.CreateBrand(null);
      ProductCanonicalCondition conditionNew =
          ProductDimensions.CreateCanonicalCondition(ProductCanonicalConditionCondition.NEW);
      ProductCanonicalCondition conditionUsed =
          ProductDimensions.CreateCanonicalCondition(ProductCanonicalConditionCondition.USED);
      ProductCanonicalCondition conditionOther = ProductDimensions.CreateCanonicalCondition();

      // Build up the brand = Google node under the root.
      ProductPartitionNode brandGoogleNode = rootNode.AddChild(brandGoogle).AsSubdivision();
      brandGoogleNode.AddChild(conditionNew);
      brandGoogleNode.AddChild(conditionUsed);
      brandGoogleNode.AddChild(conditionOther);

      Assert.True(brandGoogleNode.HasChild(conditionNew),
          "HasChild should return true for existing child dimension.");
      Assert.AreSame(brandGoogleNode, brandGoogleNode.GetChild(conditionNew).Parent,
          "parent->GetChild->getParent should return parent.");
      Assert.True(brandGoogleNode.HasChild(conditionUsed),
          "HasChild should return true for existing child dimension.");
      Assert.AreSame(brandGoogleNode, brandGoogleNode.GetChild(conditionUsed).Parent,
          "parent->GetChild->getParent should return parent.");
      Assert.True(brandGoogleNode.HasChild(conditionOther),
          "HasChild should return true for existing child dimension.");
      Assert.AreSame(brandGoogleNode, brandGoogleNode.GetChild(conditionOther).Parent,
          "parent->GetChild->getParent should return parent.");

      // Build up the brand = null (other) node under the root.
      ProductPartitionNode brandOtherNode = rootNode.AddChild(brandOther).AsSubdivision();
      brandOtherNode.AddChild(conditionNew);
      Assert.True(brandOtherNode.HasChild(conditionNew),
          "HasChild should return true for existing child dimension.");
      Assert.AreSame(brandOtherNode, brandOtherNode.GetChild(conditionNew).Parent,
          "parent->GetChild->getParent should return parent.");
      Assert.False(brandOtherNode.HasChild(conditionUsed),
          "HasChild should return false for nonexistent child dimension.");
      Assert.False(brandOtherNode.HasChild(conditionOther),
          "HasChild should return false for nonexistent child dimension.");
      brandOtherNode.AddChild(conditionOther);
      Assert.True(brandOtherNode.HasChild(conditionOther),
          "HasChild should return true for existing child dimension.");
      Assert.AreSame(brandOtherNode, brandOtherNode.GetChild(conditionOther).Parent,
          "parent->GetChild->getParent should return parent.");

      // Remove one of the children of brand = null.
      brandOtherNode.RemoveChild(conditionOther);
      Assert.False(brandOtherNode.HasChild(conditionOther),
          "HasChild should return false for a removed child dimension.");

      // Remove the rest of the children of brand = null.
      brandOtherNode.RemoveAllChildren();
      Assert.False(brandOtherNode.HasChild(conditionNew),
          "HasChild should return false for any removed child dimension.");
      Assert.False(brandOtherNode.HasChild(conditionUsed),
          "HasChild should return false for any removed child dimension.");
    }
  }
}

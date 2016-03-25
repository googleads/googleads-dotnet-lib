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

using System.Collections.Generic;
using System.Linq;


namespace Google.Api.Ads.AdWords.Tests.Util.Shopping.v201603 {

  /// <summary>
  /// Tests for ProductPartitionTree class.
  /// </summary>
  internal class ProductPartitionTreeTest : VersionedExampleTestsBase {

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

    /// <summary>
    /// Initializes this instance.
    /// </summary>
    [SetUp]
    public void Init() {
      CAMPAIGN_ID = utils.CreateShoppingCampaign(user, BiddingStrategyType.MANUAL_CPC);
      ADGROUP_ID = utils.CreateAdGroup(user, CAMPAIGN_ID);
    }

    /// <summary>
    /// Tests if a product partition tree can be constructed from criteria
    /// downloaded from an ad group.
    /// </summary>
    [Test]
    public void TestDownloadAdGroupTree() {
      ProductPartitionTree partitionTree = shoppingTestUtils.CreateTestTreeForAddition(ADGROUP_ID);
      AdGroupCriterionOperation[] operations = partitionTree.GetMutateOperations();
      AdGroupCriterionService adGroupCriterionService =
          (AdGroupCriterionService) user.GetService(
              AdWordsService.v201603.AdGroupCriterionService);

      AdGroupCriterionReturnValue value = adGroupCriterionService.mutate(operations);

      ProductPartitionTree newPartitionTree = ProductPartitionTree.DownloadAdGroupTree(user,
          ADGROUP_ID);

      CompareTree(newPartitionTree.Root, partitionTree.Root);
    }

    /// <summary>
    /// Compares two product partition nodes.
    /// </summary>
    /// <param name="leftNode">The left node.</param>
    /// <param name="rightNode">The right node.</param>
    private static void CompareTree(ProductPartitionNode leftNode,
        ProductPartitionNode rightNode) {
      ProductDimensionEqualityComparer comparer = new ProductDimensionEqualityComparer();
      Assert.True(comparer.Equals(leftNode.Dimension, rightNode.Dimension));

      Assert.AreEqual(leftNode.Children.Count(), rightNode.Children.Count());

      for (int i = 0; i < leftNode.Children.Count(); i++) {
        ProductPartitionNode leftChildNode = leftNode.Children.ElementAt(i);
        bool foundMatch = false;
        for (int j = 0; j < rightNode.Children.Count(); j++) {
          ProductPartitionNode rightChildNode = rightNode.Children.ElementAt(j);

          if (comparer.Equals(leftChildNode.Dimension, rightChildNode.Dimension)) {
            CompareTree(leftChildNode, rightChildNode);
            foundMatch = true;
            break;
          }
        }
        Assert.True(foundMatch, "Matching node not found.");
      }
    }

    /// <summary>
    /// Tests creating an empty ad group tree. In this case, all operations
    /// generated should be ADD operations.
    /// </summary>
    [Test]
    public void TestCreateEmptyTree() {
      ProductPartitionTree tree = ProductPartitionTree.CreateAdGroupTree(
          new List<AdGroupCriterion>());
      Assert.NotNull(tree.Root, "Even an empty tree should automatically have a root node.");
      Assert.True(tree.Root.ProductPartitionId < 0L,
          "The root node for an empty tree should have a negative (temporary) ID.");
      Assert.True(tree.Root.IsUnit, "The root node for an empty tree should be a UNIT.");

      AdGroupCriterionOperation[] mutateOperations = tree.GetMutateOperations();

      Assert.That(mutateOperations.Count() == 1, "Number of operations is incorrect.");
      AdGroupCriterionOperation operation = mutateOperations[0];
      Assert.AreEqual(Operator.ADD, operation.@operator,
          "Should have a single operation to ADD the root node.");
      BiddableAdGroupCriterion adGroupCriterion = (BiddableAdGroupCriterion) operation.operand;
      Assert.Null(((ProductPartition) adGroupCriterion.criterion).caseValue,
          "Product dimension of operation's operand should be null.");
      Assert.True(adGroupCriterion.criterion.id < 0L);
    }

    /// <summary>
    /// Tests creating a tree that in its <em>final</em> state is just an
    /// empty tree.
    /// </summary>
    [Test]
    public void TestCreateUltimatelyEmptyTree() {
      ProductPartitionTree tree = ProductPartitionTree.CreateAdGroupTree(
          new List<AdGroupCriterion>());

      ProductPartitionNode rootNode = tree.Root.AsSubdivision();
      ProductPartitionNode brand1 =
          rootNode.AddChild(ProductDimensions.CreateBrand("google")).AsSubdivision();
      ProductPartitionNode offerNode = brand1.AddChild(ProductDimensions.CreateOfferId("A"));
      offerNode.AsBiddableUnit().CpcBid = 1000000L;

      brand1.AddChild(ProductDimensions.CreateOfferId()).AsExcludedUnit();
      ProductPartitionNode brand2 =
          rootNode.AddChild(ProductDimensions.CreateBrand()).AsExcludedUnit();

      // Now remove the two child nodes under the root and set the root back
      // to a UNIT. This should result in operations that simply create the
      // root node.
      rootNode.RemoveChild(brand1.Dimension);
      rootNode.RemoveChild(brand2.Dimension);
      rootNode = rootNode.AsBiddableUnit();

      AdGroupCriterionOperation[] mutateOperations = tree.GetMutateOperations();

      Assert.AreEqual(mutateOperations.Count(), 1, "Number of operations is incorrect.");
      AdGroupCriterionOperation operation = mutateOperations[0];
      Assert.AreEqual(Operator.ADD, operation.@operator,
          "Should have a single operation to ADD the root node.");
      BiddableAdGroupCriterion adGroupCriterion = (BiddableAdGroupCriterion) operation.operand;
      Assert.Null(((ProductPartition) adGroupCriterion.criterion).caseValue,
          "Product dimension of operation's operand should be null.");
      Assert.True(adGroupCriterion.criterion.id < 0L,
          "Partition ID of the operand should be negative.");
    }

    /// <summary>
    /// Tests mutating an existing tree with multiple nodes.
    /// </summary>
    [Test]
    public void TestMutateMultiNodeTree() {
      ProductPartitionTree tree = shoppingTestUtils.CreateTestTreeForTransformation(ADGROUP_ID);
      Assert.AreEqual(ADGROUP_ID, tree.AdGroupId, "ad group ID is incorrect");

      // Change the bids on leaf nodes.
      ProductPartitionNode brandGoogleNode = tree.Root.GetChild(shoppingTestUtils.BRAND_GOOGLE);
      ProductPartitionNode offerANode = brandGoogleNode.GetChild(shoppingTestUtils.OFFER_A);
      // This should produce 1 SET operation.
      offerANode.CpcBid = offerANode.CpcBid * 10;

      // Offer B is changed from Exclude to Biddable. This should produce 1
      // REMOVE operation + 1 ADD operation.
      ProductPartitionNode offerBNode = brandGoogleNode.GetChild(shoppingTestUtils.OFFER_B);
      offerBNode.AsBiddableUnit().CpcBid = 5000000L;

      // Other Brand node is changed from Exclude to Biddable. This should
      // produce 1 REMOVE operation + 1 ADD operation.
      ProductPartitionNode brandOtherNode = tree.Root.GetChild(shoppingTestUtils.BRAND_OTHER);
      brandOtherNode = brandOtherNode.AsBiddableUnit();

      // Add an offer C node. This should produce 1 ADD operation.
      ProductPartitionNode offerCNode = brandGoogleNode.AddChild(shoppingTestUtils.OFFER_C);
      offerCNode.AsBiddableUnit().CpcBid = 1500000L;

      // Remove the brand Motorola node. This should produce 1 REMOVE operation.
      ProductPartitionNode brandMotorolaNode = tree.Root.GetChild(
          shoppingTestUtils.BRAND_MOTOROLA);
      tree.Root.RemoveChild(shoppingTestUtils.BRAND_MOTOROLA);

      // Get the mutate operations generated by the modifications made to the tree.
      AdGroupCriterionOperation[] mutateOperations = tree.GetMutateOperations();
      Assert.AreEqual(7, mutateOperations.Length);

      List<AdGroupCriterionOperation> operations = null;

      // Since Offer A node only has modified attributes, there should only be
      // one SET operation.
      operations = shoppingTestUtils.GetOperationsForNode(offerANode, mutateOperations);
      Assert.That(operations.Count == 1);
      Assert.That(operations[0].@operator == Operator.SET);

      // Since Offer B node is being converted from Exclude to Biddable node,
      // there should be one REMOVE operation, and another ADD operation.
      operations = shoppingTestUtils.GetOperationsForNode(offerBNode, mutateOperations);
      Assert.That(operations.Count == 2);
      Assert.That(operations[0].@operator == Operator.REMOVE);
      Assert.That(operations[1].@operator == Operator.ADD);

      // Since Offer C node is being added, there should be one ADD operation.
      operations = shoppingTestUtils.GetOperationsForNode(offerCNode, mutateOperations);
      Assert.That(operations.Count == 1);
      Assert.That(operations[0].@operator == Operator.ADD);

      // Since Other Brand node is being converted from Exclude to Biddable node,
      // there should be one REMOVE operation, and another ADD operation.
      operations = shoppingTestUtils.GetOperationsForNode(brandOtherNode, mutateOperations);
      Assert.That(operations.Count == 2);
      Assert.That(operations[0].@operator == Operator.REMOVE);
      Assert.That(operations[1].@operator == Operator.ADD);

      // Since Offer B node is being removed, there should be one REMOVE
      // operation.
      operations = shoppingTestUtils.GetOperationsForNode(brandMotorolaNode, mutateOperations);
      Assert.That(operations.Count == 1);
      Assert.That(operations[0].@operator == Operator.REMOVE);
    }

    /// <summary>
    /// Tests creating an empty tree and then adding several levels of nodes.
    /// </summary>
    [Test]
    public void TestCreateMultiNodeTreeFromScratch() {
      ProductPartitionTree tree = ProductPartitionTree.CreateAdGroupTree(
          new List<AdGroupCriterion>());

      ProductPartitionNode rootNode = tree.Root.AsSubdivision();
      ProductPartitionNode brand1 =
          rootNode.AddChild(ProductDimensions.CreateBrand("google")).AsSubdivision();
      ProductPartitionNode brand1Offer1 =
          brand1.AddChild(ProductDimensions.CreateOfferId("A"));
      brand1Offer1.AsBiddableUnit().CpcBid = 1000000L;
      ProductPartitionNode brand1Offer2 =
          brand1.AddChild(ProductDimensions.CreateOfferId()).AsExcludedUnit();
      ProductPartitionNode brand2 =
          rootNode.AddChild(ProductDimensions.CreateBrand()).AsExcludedUnit();

      ProductPartitionNode[] nodes = new ProductPartitionNode[] { rootNode, brand1, brand1Offer1,
        brand1Offer2, brand2 };

      AdGroupCriterionOperation[] mutateOperations = tree.GetMutateOperations();

      for (int i = 0; i < nodes.Length; i++) {
        List<AdGroupCriterionOperation> nodeOperations =
            shoppingTestUtils.GetOperationsForNode(nodes[i], mutateOperations);
        Assert.That(nodeOperations.Count == 1);
        Assert.That(nodeOperations[0].@operator == Operator.ADD);
        ProductPartition partition = (ProductPartition) nodeOperations[0].operand.criterion;
        Assert.That(partition.id == nodes[i].ProductPartitionId);
        if (nodes[i].Parent == null) {
          Assert.That(partition.parentCriterionIdSpecified == false);
        } else {
          Assert.That(partition.parentCriterionId == nodes[i].Parent.ProductPartitionId);
        }
        Assert.That(partition.caseValue == nodes[i].Dimension);
      }
    }

    /// <summary>
    /// Tests that the factory method ignores removed criteria.
    /// </summary>
    [Test]
    public void TestRemovedCriteriaIgnored() {
      AdGroupCriterion rootCriterion = ShoppingTestUtils.CreateCriterionForProductPartition(
          1L, 0L, null, true, false);
      List<AdGroupCriterion> criteria = new List<AdGroupCriterion>();
      criteria.Add(rootCriterion);

      // Create a criteria for a child node and set its UserStatus to REMOVED.
      ProductBrand brandGoogle = ProductDimensions.CreateBrand("google");
      AdGroupCriterion removedCriterion = ShoppingTestUtils.CreateCriterionForProductPartition(
          2L, 1L, brandGoogle, true, false);
      ((BiddableAdGroupCriterion) removedCriterion).userStatus = UserStatus.REMOVED;
      criteria.Add(removedCriterion);

      ProductPartitionTree tree =
          ProductPartitionTree.CreateAdGroupTree(criteria);

      Assert.False(tree.Root.HasChild(brandGoogle),
          "Brand = google criteria had status removed, but it is in the tree.");
    }
  }
}

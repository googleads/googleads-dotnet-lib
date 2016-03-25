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

namespace Google.Api.Ads.AdWords.Tests.Util.Shopping.v201603 {

  /// <summary>
  /// Tests for <see cref="ProductDimensions"/> class.
  /// </summary>
  public class ProductDimensionsTest {

    /// <summary>
    /// The <see cref="ProductDimensionEqualityComparer"/> class to check for
    /// equality of newly created <see cref="ProductionDimension"/> objects with
    /// existing instances.
    /// </summary>
    private ProductDimensionEqualityComparer comparer = new ProductDimensionEqualityComparer();

    /// <summary>
    /// Tests the creation of <see cref="ProductBiddingCategory"/> objects.
    /// </summary>
    [Test]
    public void TestCreateBiddingCategory() {
      ProductBiddingCategory categoryA = new ProductBiddingCategory() {
        type = ProductDimensionType.BIDDING_CATEGORY_L1,
        value = 2L
      };

      ProductBiddingCategory categoryB = ProductDimensions.CreateBiddingCategory(
          ProductDimensionType.BIDDING_CATEGORY_L1, 2L);

      Assert.True(comparer.Equals(categoryA, categoryB));

      ProductBiddingCategory categoryC = new ProductBiddingCategory() {
        type = ProductDimensionType.BIDDING_CATEGORY_L1,
      };

      ProductBiddingCategory categoryD = ProductDimensions.CreateBiddingCategory(
          ProductDimensionType.BIDDING_CATEGORY_L1);

      Assert.True(comparer.Equals(categoryC, categoryD));
    }

    /// <summary>
    /// Tests the creation of <see cref="ProductBrand"/> objects.
    /// </summary>
    [Test]
    public void TestCreateBrand() {
      ProductBrand brandA = new ProductBrand() {
        value = "google"
      };

      ProductBrand brandB = ProductDimensions.CreateBrand("google");

      Assert.True(comparer.Equals(brandA, brandB));

      ProductBrand brandC = new ProductBrand() {
      };

      ProductBrand brandD = ProductDimensions.CreateBrand();

      Assert.True(comparer.Equals(brandC, brandD));
    }

    /// <summary>
    /// Tests the creation of <see cref="ProductCanonicalCondition"/> objects.
    /// </summary>
    [Test]
    public void TestCreateCanonicalCondition() {
      ProductCanonicalCondition conditionA = new ProductCanonicalCondition() {
        condition = ProductCanonicalConditionCondition.NEW
      };

      ProductCanonicalCondition conditionB =
          ProductDimensions.CreateCanonicalCondition(ProductCanonicalConditionCondition.NEW);

      Assert.True(comparer.Equals(conditionA, conditionB));

      ProductCanonicalCondition conditionC = new ProductCanonicalCondition() {
      };

      ProductCanonicalCondition conditionD = ProductDimensions.CreateCanonicalCondition();

      Assert.True(comparer.Equals(conditionC, conditionD));
    }

    /// <summary>
    /// Tests the creation of <see cref="ProductCustomAttribute"/> objects.
    /// </summary>
    [Test]
    public void TestCreateCustomAttribute() {
      ProductCustomAttribute customAttributeA = new ProductCustomAttribute() {
        type = ProductDimensionType.BIDDING_CATEGORY_L1,
        value = "google"
      };

      ProductCustomAttribute customAttributeB = ProductDimensions.CreateCustomAttribute(
          ProductDimensionType.BIDDING_CATEGORY_L1, "google");

      Assert.True(comparer.Equals(customAttributeA, customAttributeB));

      ProductCustomAttribute customAttributeC = new ProductCustomAttribute() {
        type = ProductDimensionType.BIDDING_CATEGORY_L1,
      };

      ProductCustomAttribute customAttributeD = ProductDimensions.CreateCustomAttribute(
          ProductDimensionType.BIDDING_CATEGORY_L1);

      Assert.True(comparer.Equals(customAttributeC, customAttributeD));
    }

    /// <summary>
    /// Tests the creation of <see cref="ProductOfferId"/> objects.
    /// </summary>
    [Test]
    public void TestCreateOfferId() {
      ProductOfferId productOfferIdA = new ProductOfferId() {
        value = "google"
      };

      ProductOfferId productOfferIdB = ProductDimensions.CreateOfferId("google");

      Assert.True(comparer.Equals(productOfferIdA, productOfferIdB));

      ProductOfferId productOfferIdC = new ProductOfferId() {
      };

      ProductOfferId productOfferIdD = ProductDimensions.CreateOfferId();

      Assert.True(comparer.Equals(productOfferIdC, productOfferIdD));
    }

    /// <summary>
    /// Tests the creation of <see cref="ProductType"/> objects.
    /// </summary>
    [Test]
    public void TestProductTypeEquals() {
      ProductType productTypeA = new ProductType() {
        type = ProductDimensionType.BRAND,
        value = "google",
      };

      ProductType productTypeB = ProductDimensions.CreateType(ProductDimensionType.BRAND,
          "google");

      Assert.True(comparer.Equals(productTypeA, productTypeB));

      ProductType productTypeC = new ProductType() {
        type = ProductDimensionType.BRAND,
      };

      ProductType productTypeD = ProductDimensions.CreateType(ProductDimensionType.BRAND);

      Assert.True(comparer.Equals(productTypeC, productTypeD));
    }

    /// <summary>
    /// Tests the creation of <see cref="ProductChannelExclusivity"/> objects.
    /// </summary>
    [Test]
    public void TestProductChannelExclusivityEquals() {
      ProductChannelExclusivity channelExclusivityA = new ProductChannelExclusivity() {
        channelExclusivity = ShoppingProductChannelExclusivity.MULTI_CHANNEL
      };

      ProductChannelExclusivity channelExclusivityB =
          ProductDimensions.CreateChannelExclusivity(
              ShoppingProductChannelExclusivity.MULTI_CHANNEL);

      Assert.True(comparer.Equals(channelExclusivityA, channelExclusivityB));

      ProductChannelExclusivity channelExclusivityC = new ProductChannelExclusivity() {
      };

      ProductChannelExclusivity channelExclusivityD = ProductDimensions.CreateChannelExclusivity();

      Assert.True(comparer.Equals(channelExclusivityC, channelExclusivityD));
    }
  }
}
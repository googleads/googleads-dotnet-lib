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
  /// Tests for <see cref="ProductDimensionEqualityComparer"/> class.
  /// </summary>
  public class ProductDimensionEqualityComparerTest {

    /// <summary>
    /// The <see cref="ProductDimensionEqualityComparer"/> instance for running tests.
    /// </summary>
    private ProductDimensionEqualityComparer comparer = new ProductDimensionEqualityComparer();

    /// <summary>
    /// Initializes this instance.
    /// </summary>
    [SetUp]
    public void Init() {
    }

    /// <summary>
    /// Tests equality for null <see cref="ProductDimension"/> objects.
    /// </summary>
    [Test]
    public void TestNullEquals() {
      ProductDimension dimension = new ProductBiddingCategory() {
        type = ProductDimensionType.BIDDING_CATEGORY_L1,
        value = 2L
      };

      Assert.False(comparer.Equals(dimension, null));
      Assert.False(comparer.Equals(null, dimension));
      Assert.True(comparer.Equals(null, null));
    }

    /// <summary>
    /// Tests equality for <see cref="ProductBiddingCategory"/> objects.
    /// </summary>
    [Test]
    public void TestProductBiddingCategoryEquals() {
      ProductBiddingCategory categoryA = new ProductBiddingCategory() {
        type = ProductDimensionType.BIDDING_CATEGORY_L1,
        value = 2L
      };

      ProductBiddingCategory categoryB = new ProductBiddingCategory() {
        type = ProductDimensionType.BIDDING_CATEGORY_L1,
        value = 2L
      };

      ProductBiddingCategory categoryC = new ProductBiddingCategory() {
        type = ProductDimensionType.BIDDING_CATEGORY_L2,
        value = 2L
      };

      ProductBiddingCategory categoryD = new ProductBiddingCategory() {
        type = ProductDimensionType.BIDDING_CATEGORY_L1,
        value = 5L
      };

      ProductBiddingCategory categoryE = new ProductBiddingCategory() {
        type = ProductDimensionType.BIDDING_CATEGORY_L1,
      };

      ProductBiddingCategory categoryF = new ProductBiddingCategory() {
        type = ProductDimensionType.BIDDING_CATEGORY_L1,
      };

      ProductBiddingCategory categoryG = new ProductBiddingCategory();
      ProductBiddingCategory categoryH = new ProductBiddingCategory();

      Assert.True(comparer.Equals(categoryA, categoryB));
      Assert.False(comparer.Equals(categoryA, categoryC));
      Assert.False(comparer.Equals(categoryA, categoryD));

      // Null value is handled gracefully.
      Assert.False(comparer.Equals(categoryA, categoryD));
      Assert.False(comparer.Equals(categoryA, categoryE));
      Assert.True(comparer.Equals(categoryE, categoryF));
      Assert.True(comparer.Equals(categoryG, categoryH));
    }

    /// <summary>
    /// Tests equality for <see cref="ProductBrand"/> objects.
    /// </summary>
    [Test]
    public void TestProductBrandEquals() {
      ProductBrand brandA = new ProductBrand() {
        value = "google"
      };

      ProductBrand brandATitleCase = new ProductBrand() {
        value = "Google"
      };

      ProductBrand brandB = new ProductBrand() {
        value = "google"
      };

      ProductBrand brandC = new ProductBrand() {
        value = "motorola"
      };

      ProductBrand brandD = new ProductBrand();
      ProductBrand brandE = new ProductBrand();

      Assert.True(comparer.Equals(brandA, brandB));
      Assert.False(comparer.Equals(brandA, brandC));

      // Case of value is ignored.
      Assert.True(comparer.Equals(brandA, brandATitleCase));

      //Null value is handled gracefully.
      Assert.False(comparer.Equals(brandA, brandD));
      Assert.True(comparer.Equals(brandD, brandE));
    }

    /// <summary>
    /// Tests equality for <see cref="ProductCanonicalCondition"/> objects.
    /// </summary>
    [Test]
    public void TestProductCanonicalConditionEquals() {
      ProductCanonicalCondition conditionA = new ProductCanonicalCondition() {
        condition = ProductCanonicalConditionCondition.NEW
      };

      ProductCanonicalCondition conditionB = new ProductCanonicalCondition() {
        condition = ProductCanonicalConditionCondition.NEW
      };

      ProductCanonicalCondition conditionC = new ProductCanonicalCondition() {
        condition = ProductCanonicalConditionCondition.REFURBISHED
      };

      ProductCanonicalCondition conditionD = new ProductCanonicalCondition();
      ProductCanonicalCondition conditionE = new ProductCanonicalCondition();

      Assert.True(comparer.Equals(conditionA, conditionB));
      Assert.False(comparer.Equals(conditionA, conditionC));

      //Null value is handled gracefully.
      Assert.False(comparer.Equals(conditionA, conditionD));
      Assert.True(comparer.Equals(conditionD, conditionE));
    }

    /// <summary>
    /// Tests equality for <see cref="ProductCustomAttribute"/> objects.
    /// </summary>
    [Test]
    public void TestProductCustomAttributeEquals() {
      ProductCustomAttribute customAttributeA = new ProductCustomAttribute() {
        type = ProductDimensionType.BIDDING_CATEGORY_L1,
        value = "google"
      };

      ProductCustomAttribute customAttributeATitleCase = new ProductCustomAttribute() {
        type = ProductDimensionType.BIDDING_CATEGORY_L1,
        value = "Google"
      };

      ProductCustomAttribute customAttributeB = new ProductCustomAttribute() {
        type = ProductDimensionType.BIDDING_CATEGORY_L1,
        value = "google"
      };

      ProductCustomAttribute customAttributeC = new ProductCustomAttribute() {
        type = ProductDimensionType.BIDDING_CATEGORY_L1,
        value = "motorola"
      };

      ProductCustomAttribute customAttributeD = new ProductCustomAttribute() {
        type = ProductDimensionType.BIDDING_CATEGORY_L2,
        value = "google"
      };

      ProductCustomAttribute customAttributeE = new ProductCustomAttribute() {
        type = ProductDimensionType.BIDDING_CATEGORY_L1,
      };

      ProductCustomAttribute customAttributeF = new ProductCustomAttribute() {
        type = ProductDimensionType.BIDDING_CATEGORY_L1,
      };

      ProductCustomAttribute customAttributeG = new ProductCustomAttribute();
      ProductCustomAttribute customAttributeH = new ProductCustomAttribute();

      Assert.True(comparer.Equals(customAttributeA, customAttributeB));
      Assert.False(comparer.Equals(customAttributeA, customAttributeC));
      Assert.False(comparer.Equals(customAttributeA, customAttributeD));
      Assert.False(comparer.Equals(customAttributeA, customAttributeE));

      // Case of value is ignored.
      Assert.True(comparer.Equals(customAttributeA, customAttributeATitleCase));

      //Null value is handled gracefully.
      Assert.False(comparer.Equals(customAttributeA, customAttributeE));
      Assert.True(comparer.Equals(customAttributeE, customAttributeF));
      Assert.True(comparer.Equals(customAttributeG, customAttributeH));
    }

    /// <summary>
    /// Tests equality for <see cref="ProductOfferId"/> objects.
    /// </summary>
    [Test]
    public void TestProductOfferIdEquals() {
      ProductOfferId productOfferIdA = new ProductOfferId() {
        value = "google"
      };

      ProductOfferId productOfferIdATitleCase = new ProductOfferId() {
        value = "Google"
      };

      ProductOfferId productOfferIdB = new ProductOfferId() {
        value = "google"
      };

      ProductOfferId productOfferIdC = new ProductOfferId() {
        value = "motorola"
      };

      ProductOfferId productOfferIdD = new ProductOfferId();
      ProductOfferId productOfferIdE = new ProductOfferId();

      Assert.True(comparer.Equals(productOfferIdA, productOfferIdB));
      Assert.False(comparer.Equals(productOfferIdA, productOfferIdC));

      // Case of value is ignored.
      Assert.True(comparer.Equals(productOfferIdA, productOfferIdATitleCase));

      //Null value is handled gracefully.
      Assert.False(comparer.Equals(productOfferIdA, productOfferIdD));
      Assert.True(comparer.Equals(productOfferIdD, productOfferIdE));
    }

    /// <summary>
    /// Tests equality for <see cref="ProductType"/> objects.
    /// </summary>
    [Test]
    public void TestProductTypeEquals() {
      ProductType productTypeA = new ProductType() {
        type = ProductDimensionType.BRAND,
        value = "google",
      };

      ProductType productTypeATitleCase = new ProductType() {
        type = ProductDimensionType.BRAND,
        value = "Google",
      };

      ProductType productTypeB = new ProductType() {
        type = ProductDimensionType.BRAND,
        value = "google"
      };

      ProductType productTypeC = new ProductType() {
        type = ProductDimensionType.CHANNEL,
        value = "google"
      };

      ProductType productTypeD = new ProductType() {
        type = ProductDimensionType.BRAND,
        value = "motorola"
      };

      ProductType productTypeE = new ProductType() {
        type = ProductDimensionType.BRAND,
      };

      ProductType productTypeF = new ProductType() {
        type = ProductDimensionType.BRAND,
      };

      ProductType productTypeG = new ProductType();
      ProductType productTypeH = new ProductType();

      Assert.True(comparer.Equals(productTypeA, productTypeB));
      Assert.False(comparer.Equals(productTypeA, productTypeC));
      Assert.False(comparer.Equals(productTypeA, productTypeD));

      // Case of value is ignored.
      Assert.True(comparer.Equals(productTypeA, productTypeATitleCase));

      //Null value is handled gracefully.
      Assert.False(comparer.Equals(productTypeA, productTypeE));
      Assert.True(comparer.Equals(productTypeE, productTypeF));
      Assert.True(comparer.Equals(productTypeG, productTypeH));
    }

    /// <summary>
    /// Tests equality for <see cref="ProductChannelExclusivity"/> objects.
    /// </summary>
    [Test]
    public void TestProductChannelExclusivityEquals() {
      ProductChannelExclusivity channelExclusivityA = new ProductChannelExclusivity() {
        channelExclusivity = ShoppingProductChannelExclusivity.MULTI_CHANNEL
      };

      ProductChannelExclusivity channelExclusivityB = new ProductChannelExclusivity() {
        channelExclusivity = ShoppingProductChannelExclusivity.MULTI_CHANNEL
      };

      ProductChannelExclusivity channelExclusivityC = new ProductChannelExclusivity() {
        channelExclusivity = ShoppingProductChannelExclusivity.SINGLE_CHANNEL
      };

      ProductChannelExclusivity channelExclusivityD = new ProductChannelExclusivity();
      ProductChannelExclusivity channelExclusivityE = new ProductChannelExclusivity();

      Assert.True(comparer.Equals(channelExclusivityA, channelExclusivityB));
      Assert.False(comparer.Equals(channelExclusivityA, channelExclusivityC));

      //Null value is handled gracefully.
      Assert.False(comparer.Equals(channelExclusivityA, channelExclusivityE));
      Assert.True(comparer.Equals(channelExclusivityD, channelExclusivityE));
    }

    /// <summary>
    /// Tests the calculation of hashcodes for <see cref="ProductBiddingCategory"/> objects.
    /// </summary>
    [Test]
    public void TestProductBiddingCategoryGetHashcode() {
      ProductBiddingCategory categoryA = new ProductBiddingCategory() {
        type = ProductDimensionType.BIDDING_CATEGORY_L1,
        value = 2L
      };

      ProductBiddingCategory categoryB = new ProductBiddingCategory() {
        type = ProductDimensionType.BIDDING_CATEGORY_L1,
        value = 2L
      };

      ProductBiddingCategory categoryC = new ProductBiddingCategory() {
        type = ProductDimensionType.BIDDING_CATEGORY_L2,
        value = 2L
      };

      ProductBiddingCategory categoryD = new ProductBiddingCategory() {
        type = ProductDimensionType.BIDDING_CATEGORY_L1,
        value = 5L
      };

      Assert.AreEqual(comparer.GetHashCode(categoryA), comparer.GetHashCode(categoryB));
      Assert.AreNotEqual(comparer.GetHashCode(categoryA), comparer.GetHashCode(categoryC));
      Assert.AreNotEqual(comparer.GetHashCode(categoryA), comparer.GetHashCode(categoryD));
    }

    /// <summary>
    /// Tests the calculation of hashcodes for <see cref="ProductBrand"/> objects.
    /// </summary>
    [Test]
    public void TestProductBrandGetHashcode() {
      ProductBrand brandA = new ProductBrand() {
        value = "google"
      };

      ProductBrand brandATitleCase = new ProductBrand() {
        value = "Google"
      };

      ProductBrand brandB = new ProductBrand() {
        value = "google"
      };

      ProductBrand brandC = new ProductBrand() {
        value = "motorola"
      };

      Assert.AreEqual(comparer.GetHashCode(brandA), comparer.GetHashCode(brandB));
      Assert.AreNotEqual(comparer.GetHashCode(brandA), comparer.GetHashCode(brandC));

      // Case of value is ignored.
      Assert.AreEqual(comparer.GetHashCode(brandA), comparer.GetHashCode(brandATitleCase));
    }

    /// <summary>
    /// Tests the calculation of hashcodes for <see cref="ProductCanonicalCondition"/> objects.
    /// </summary>
    [Test]
    public void TestProductCanonicalConditionGetHashcode() {
      ProductCanonicalCondition conditionA = new ProductCanonicalCondition() {
        condition = ProductCanonicalConditionCondition.NEW
      };

      ProductCanonicalCondition conditionB = new ProductCanonicalCondition() {
        condition = ProductCanonicalConditionCondition.NEW
      };

      ProductCanonicalCondition conditionC = new ProductCanonicalCondition() {
        condition = ProductCanonicalConditionCondition.REFURBISHED
      };

      Assert.AreEqual(comparer.GetHashCode(conditionA), comparer.GetHashCode(conditionB));
      Assert.AreNotEqual(comparer.GetHashCode(conditionA), comparer.GetHashCode(conditionC));
    }

    /// <summary>
    /// Tests the calculation of hashcodes for <see cref="ProductCustomAttribute"/> objects.
    /// </summary>
    [Test]
    public void TestProductCustomAttributeGetHashcode() {
      ProductCustomAttribute customAttributeA = new ProductCustomAttribute() {
        type = ProductDimensionType.BIDDING_CATEGORY_L1,
        value = "google"
      };

      ProductCustomAttribute customAttributeATitleCase = new ProductCustomAttribute() {
        type = ProductDimensionType.BIDDING_CATEGORY_L1,
        value = "Google"
      };

      ProductCustomAttribute customAttributeB = new ProductCustomAttribute() {
        type = ProductDimensionType.BIDDING_CATEGORY_L1,
        value = "google"
      };

      ProductCustomAttribute customAttributeC = new ProductCustomAttribute() {
        type = ProductDimensionType.BIDDING_CATEGORY_L1,
        value = "motorola"
      };

      ProductCustomAttribute customAttributeD = new ProductCustomAttribute() {
        type = ProductDimensionType.BIDDING_CATEGORY_L2,
        value = "google"
      };

      Assert.AreEqual(comparer.GetHashCode(customAttributeA),
          comparer.GetHashCode(customAttributeB));
      Assert.AreNotEqual(comparer.GetHashCode(customAttributeA),
          comparer.GetHashCode(customAttributeC));
      Assert.AreNotEqual(comparer.GetHashCode(customAttributeA),
          comparer.GetHashCode(customAttributeD));

      // Case of value is ignored.
      Assert.AreEqual(comparer.GetHashCode(customAttributeA),
          comparer.GetHashCode(customAttributeATitleCase));
    }

    /// <summary>
    /// Tests the calculation of hashcodes for <see cref="ProductOfferId"/> objects.
    /// </summary>
    [Test]
    public void TestProductOfferIdGetHashcode() {
      ProductOfferId productOfferIdA = new ProductOfferId() {
        value = "google"
      };

      ProductOfferId productOfferIdATitleCase = new ProductOfferId() {
        value = "Google"
      };

      ProductOfferId productOfferIdB = new ProductOfferId() {
        value = "google"
      };

      ProductOfferId productOfferIdC = new ProductOfferId() {
        value = "motorola"
      };

      Assert.AreEqual(comparer.GetHashCode(productOfferIdA),
          comparer.GetHashCode(productOfferIdA));
      Assert.AreNotEqual(comparer.GetHashCode(productOfferIdA),
          comparer.GetHashCode(productOfferIdC));

      // Case of value is ignored.
      Assert.AreEqual(comparer.GetHashCode(productOfferIdA),
          comparer.GetHashCode(productOfferIdATitleCase));
    }

    /// <summary>
    /// Tests the calculation of hashcodes for <see cref="ProductType"/> objects.
    /// </summary>
    [Test]
    public void TestProductTypeGetHashcode() {
      ProductType productTypeA = new ProductType() {
        type = ProductDimensionType.BRAND,
        value = "google",
      };

      ProductType productTypeATitleCase = new ProductType() {
        type = ProductDimensionType.BRAND,
        value = "Google",
      };

      ProductType productTypeB = new ProductType() {
        type = ProductDimensionType.BRAND,
        value = "google"
      };

      ProductType productTypeC = new ProductType() {
        type = ProductDimensionType.CHANNEL,
        value = "google"
      };

      ProductType productTypeD = new ProductType() {
        type = ProductDimensionType.BRAND,
        value = "motorola"
      };

      Assert.AreEqual(comparer.GetHashCode(productTypeA), comparer.GetHashCode(productTypeB));
      Assert.AreNotEqual(comparer.GetHashCode(productTypeA), comparer.GetHashCode(productTypeC));
      Assert.AreNotEqual(comparer.GetHashCode(productTypeA), comparer.GetHashCode(productTypeD));

      Assert.AreEqual(comparer.GetHashCode(productTypeA),
          comparer.GetHashCode(productTypeATitleCase));
    }

    /// <summary>
    /// Tests the calculation of hashcodes for <see cref="ProductChannelExclusivity"/> objects.
    /// </summary>
    [Test]
    public void TestProductChannelExclusivityGetHashcode() {
      ProductChannelExclusivity channelExclusivityA = new ProductChannelExclusivity() {
        channelExclusivity = ShoppingProductChannelExclusivity.MULTI_CHANNEL
      };

      ProductChannelExclusivity channelExclusivityB = new ProductChannelExclusivity() {
        channelExclusivity = ShoppingProductChannelExclusivity.MULTI_CHANNEL
      };

      ProductChannelExclusivity channelExclusivityC = new ProductChannelExclusivity() {
        channelExclusivity = ShoppingProductChannelExclusivity.SINGLE_CHANNEL
      };

      Assert.AreEqual(comparer.GetHashCode(channelExclusivityA),
          comparer.GetHashCode(channelExclusivityB));
      Assert.AreNotEqual(comparer.GetHashCode(channelExclusivityA),
          comparer.GetHashCode(channelExclusivityC));
    }
  }
}

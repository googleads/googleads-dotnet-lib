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

namespace Google.Api.Ads.AdWords.Util.Shopping.v201603 {

  /// <summary>
  /// Factory methods for creating ProductDimension objects. Each
  /// ProductDimension subclass has a create method, and each of those methods
  /// has a signature that reflects the attributes that logically identify an
  /// instance of that subclass.
  ///
  /// <p>E.g. <see cref="createBrand"/> method has a single <code>brand</code>
  /// parameter since the <code>brand</code> attribute is what uniquely
  /// identifies a <see cref="ProductBrand"/>, while
  /// <see cref="createBiddingCategory"/> method has both a
  /// <code>productDimensionType</code> and a <code>biddingCategoryId</code>
  /// parameter since both attributes are required to uniquely identify a
  /// <see cref="ProductBiddingCategory"/>.
  ///
  /// <p>Note that this factory only includes methods for subclasses of
  /// ProductDimension that are supported by Shopping Campaigns.
  /// </summary>
  public static class ProductDimensions {

    /// <summary>
    /// Creates a new ProductType for the <b>Everything else</b> case.
    /// </summary>
    /// <param name="productDimensionType">Type of the product dimension.</param>
    /// <returns>The product type criterion.</returns>
    public static ProductType CreateType(ProductDimensionType productDimensionType) {
      return CreateType(productDimensionType, null);
    }

    /// <summary>
    /// Creates a new ProductType.
    /// </summary>
    /// <param name="productDimensionType">Type of the product dimension.</param>
    /// <param name="productTypeValue">The product type value. May be null if
    /// creating an "other" dimension</param>
    /// <returns>The product type criterion</returns>
    public static ProductType CreateType(ProductDimensionType productDimensionType,
        string productTypeValue) {
      return new ProductType() {
        type = productDimensionType,
        value = productTypeValue
      };
    }

    /// <summary>
    /// Creates a new ProductCanonicalCondition for the <b>Everything else</b>
    /// case.
    /// </summary>
    /// <returns>The product canonical condition criterion</returns>
    public static ProductCanonicalCondition CreateCanonicalCondition() {
      return new ProductCanonicalCondition() {
        conditionSpecified = false
      };
    }

    /// <summary>
    /// Creates a new ProductCanonicalCondition.
    /// </summary>
    /// <param name="condition">The condition.</param>
    /// <returns>The new product canonical condition criterion.</returns>
    public static ProductCanonicalCondition CreateCanonicalCondition(
        ProductCanonicalConditionCondition condition) {
      return new ProductCanonicalCondition() {
        condition = condition
      };
    }

    /// <summary>
    /// Creates a new ProductBiddingCategory for <b>Everything else</b>
    /// case.
    /// </summary>
    /// <param name="productDimensionType">Type of the product dimension.</param>
    /// <returns>The product bidding category criterion.</returns>
    public static ProductBiddingCategory CreateBiddingCategory(
        ProductDimensionType productDimensionType) {
      return new ProductBiddingCategory() {
        type = productDimensionType
      };
    }

    /// <summary>
    /// Creates a new ProductBiddingCategory.
    /// </summary>
    /// <param name="productDimensionType">Type of the product dimension.</param>
    /// <param name="biddingCategoryId">The bidding category ID.</param>
    /// <returns>The product bidding category criterion.</returns>
    public static ProductBiddingCategory CreateBiddingCategory(
        ProductDimensionType productDimensionType, long biddingCategoryId) {
      ProductBiddingCategory retval = CreateBiddingCategory(productDimensionType);
      retval.value = biddingCategoryId;
      return retval;
    }

    /// <summary>
    /// Creates a new ProductOfferId for the <b>Everything else</b> case.
    /// </summary>
    /// <param name="offerId">The offer ID.</param>
    /// <returns>The product offer ID criterion.</returns>
    public static ProductOfferId CreateOfferId() {
      return CreateOfferId(null);
    }

    /// <summary>
    /// Creates a new ProductOfferId.
    /// </summary>
    /// <param name="offerId">The offer ID.</param>
    /// <returns>The product offer ID criterion.</returns>
    public static ProductOfferId CreateOfferId(string offerId) {
      return new ProductOfferId() {
        value = offerId
      };
    }

    /// <summary>
    /// Creates a new ProductBrand for the <b>Everything else</b> case.
    /// </summary>
    /// <returns>The product brand criterion.</returns>
    public static ProductBrand CreateBrand() {
      return CreateBrand(null);
    }

    /// <summary>
    /// Creates a new ProductBrand.
    /// </summary>
    /// <param name="brand">The brand.</param>
    /// <returns>The product brand criterion.</returns>
    public static ProductBrand CreateBrand(string brand) {
      return new ProductBrand() {
        value = brand
      };
    }

    /// <summary>
    /// Creates a new ProductCustomAttribute for the <b>Everything else</b> case.
    /// </summary>
    /// <param name="productDimensionType">Type of the product dimension.</param>
    /// <returns>The product custom attribute criterion.</returns>
    public static ProductCustomAttribute CreateCustomAttribute(
        ProductDimensionType productDimensionType) {
      return CreateCustomAttribute(productDimensionType, null);
    }

    /// <summary>
    /// Creates a new ProductCustomAttribute for the <b>Everything else</b> case.
    /// </summary>
    /// <param name="productDimensionType">Type of the product dimension.</param>
    /// <param name="attributeValue">The attribute value.</param>
    /// <returns>The product custom attribute criterion.</returns>
    public static ProductCustomAttribute CreateCustomAttribute(
        ProductDimensionType productDimensionType, string attributeValue) {
      return new ProductCustomAttribute() {
        type = productDimensionType,
        value = attributeValue
      };
    }

    /// <summary>
    /// Creates a new ProductChannel.
    /// </summary>
    /// <returns>The product channel criterion.</returns>
    public static ProductChannel CreateChannel() {
      return new ProductChannel();
    }

    /// <summary>
    /// Creates a new ProductChannel.
    /// </summary>
    /// <param name="channel">The channel.</param>
    /// <returns>The product channel criterion.</returns>
    public static ProductChannel CreateChannel(ShoppingProductChannel channel) {
      ProductChannel retval = CreateChannel();
      retval.channel = channel;
      return retval;
    }

    /// <summary>
    /// Creates a new ProductChannelExclusivity.
    /// </summary>
    /// <returns>The new product channel exclusivity criterion.</returns>
    public static ProductChannelExclusivity CreateChannelExclusivity() {
      return new ProductChannelExclusivity();
    }

    /// <summary>
    /// Creates a new ProductChannelExclusivity.
    /// </summary>
    /// <param name="channelExclusivity">The channel exclusivity.</param>
    /// <returns>The new product channel exclusivity criterion.</returns>
    public static ProductChannelExclusivity CreateChannelExclusivity(
        ShoppingProductChannelExclusivity channelExclusivity) {
      return new ProductChannelExclusivity() {
        channelExclusivity = channelExclusivity
      };
    }
  }
}


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

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.AdWords.Util.Shopping.v201603 {

  /// <summary>
  /// Subclass-aware comparator for <see cref="ProductDimension" /> objects.
  /// Performs a <em>logical</em> comparison between instances. This comparator
  /// is <em>not</em> consistent with {@link #equals(Object)}.
  ///
  /// <p>The {@link #compare(ProductDimension, ProductDimension)} method handles
  /// nulls, ordering nulls
  /// last.
  /// </summary>
  public class ProductDimensionEqualityComparer : IEqualityComparer<ProductDimension> {

    /// <summary>
    /// Determines whether the specified objects are equal.
    /// </summary>
    /// <param name="x">The first object to compare.</param>
    /// <param name="y">The second object to compare.</param>
    /// <returns>
    /// True if the specified objects are equal; otherwise, false.
    /// </returns>
    public bool Equals(ProductDimension x, ProductDimension y) {
      if (Object.ReferenceEquals(x, y)) {
        return true;
      }

      if (x == null || y == null || x.GetType() != y.GetType()) {
        return false;
      }

      if (x is ProductBiddingCategory) {
        return Equals(x as ProductBiddingCategory, y as ProductBiddingCategory);
      } else if (x is ProductBrand) {
        return Equals(x as ProductBrand, y as ProductBrand);
      } else if (x is ProductCanonicalCondition) {
        return Equals(x as ProductCanonicalCondition, y as ProductCanonicalCondition);
      } else if (x is ProductCustomAttribute) {
        return Equals(x as ProductCustomAttribute, y as ProductCustomAttribute);
      } else if (x is ProductOfferId) {
        return Equals(x as ProductOfferId, y as ProductOfferId);
      } else if (x is ProductChannel) {
        return Equals(x as ProductChannel, y as ProductChannel);
      } else if (x is ProductType) {
        return Equals(x as ProductType, y as ProductType);
      } else if (x is ProductChannelExclusivity) {
        return Equals(x as ProductChannelExclusivity, y as ProductChannelExclusivity);
      } else {
        return false;
      }
    }

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms
    /// and data structures like a hash table.
    /// </returns>
    public int GetHashCode(ProductDimension obj) {
      if (obj == null) {
        return 0;
      }

      if (obj is ProductBiddingCategory) {
        return GetHashCodeInternal(obj as ProductBiddingCategory);
      } else if (obj is ProductBrand) {
        return GetHashCodeInternal(obj as ProductBrand);
      } else if (obj is ProductCanonicalCondition) {
        return GetHashCodeInternal(obj as ProductCanonicalCondition);
      } else if (obj is ProductCustomAttribute) {
        return GetHashCodeInternal(obj as ProductCustomAttribute);
      } else if (obj is ProductOfferId) {
        return GetHashCodeInternal(obj as ProductOfferId);
      } else if (obj is ProductChannel) {
        return GetHashCodeInternal(obj as ProductChannel);
      } else if (obj is ProductChannelExclusivity) {
        return GetHashCodeInternal(obj as ProductChannelExclusivity);
      } else if (obj is ProductType) {
        return GetHashCodeInternal(obj as ProductType);
      } else {
        return obj.GetHashCode();
      }
    }

    /// <summary>
    /// Gets the hash code for a <see cref="ProductBiddingCategory"/>.
    /// </summary>
    /// <param name="obj">The object to calculate hash code for.</param>
    /// <returns>The hash code.</returns>
    private int GetHashCodeInternal(ProductBiddingCategory obj) {
      return Tuple.Create<bool, ProductDimensionType, long>(
          obj.typeSpecified, obj.type, obj.value).GetHashCode();
    }

    /// <summary>
    /// Gets the hash code for a <see cref="ProductBrand"/>.
    /// </summary>
    /// <param name="obj">The object to calculate hash code for.</param>
    /// <returns>The hash code.</returns>
    private int GetHashCodeInternal(ProductBrand obj) {
      if (obj.value == null) {
        return obj.GetType().Name.GetHashCode();
      } else {
        return obj.value.ToLower().GetHashCode();
      }
    }

    /// <summary>
    /// Gets the hash code for a <see cref="ProductCanonicalCondition"/>.
    /// </summary>
    /// <param name="obj">The object to calculate hash code for.</param>
    /// <returns>The hash code.</returns>
    private int GetHashCodeInternal(ProductCanonicalCondition obj) {
      return Tuple.Create<bool, ProductCanonicalConditionCondition>(
          obj.conditionSpecified, obj.condition).GetHashCode();
    }

    /// <summary>
    /// Gets the hash code for a <see cref="ProductCustomAttribute"/>.
    /// </summary>
    /// <param name="obj">The object to calculate hash code for.</param>
    /// <returns>The hash code.</returns>
    private int GetHashCodeInternal(ProductCustomAttribute obj) {
      if (obj.value == null) {
        return Tuple.Create<bool, ProductDimensionType>(
          obj.typeSpecified, obj.type).GetHashCode();
      } else {
        return Tuple.Create<bool, ProductDimensionType, string>(
            obj.typeSpecified, obj.type, obj.value.ToLower()).GetHashCode();
      }
    }

    /// <summary>
    /// Gets the hash code for a <see cref="ProductOfferId"/>.
    /// </summary>
    /// <param name="obj">The object to calculate hash code for.</param>
    /// <returns>The hash code.</returns>
    private int GetHashCodeInternal(ProductOfferId obj) {
      if (obj.value == null) {
        return obj.GetType().Name.GetHashCode();
      } else {
        return obj.value.ToLower().GetHashCode();
      }
    }

    /// <summary>
    /// Gets the hash code for a <see cref="ProductType"/>.
    /// </summary>
    /// <param name="obj">The object to calculate hash code for.</param>
    /// <returns>The hash code.</returns>
    private int GetHashCodeInternal(ProductType obj) {
      if (obj.value == null) {
        return Tuple.Create<bool, ProductDimensionType>(
            obj.typeSpecified, obj.type).GetHashCode();
      } else {
        return Tuple.Create<bool, ProductDimensionType, string>(
            obj.typeSpecified, obj.type, obj.value.ToLower()).GetHashCode();
      }
    }

    /// <summary>
    /// Gets the hash code for a <see cref="ProductChannel"/>.
    /// </summary>
    /// <param name="obj">The object to calculate hash code for.</param>
    /// <returns>The hash code.</returns>
    private int GetHashCodeInternal(ProductChannel obj) {
      return Tuple.Create<bool, ShoppingProductChannel>(
          obj.channelSpecified, obj.channel).GetHashCode();
    }

    /// <summary>
    /// Gets the hash code for a <see cref="ProductChannelExclusivity"/>.
    /// </summary>
    /// <param name="obj">The object to calculate hash code for.</param>
    /// <returns>The hash code.</returns>
    private int GetHashCodeInternal(ProductChannelExclusivity obj) {
      return Tuple.Create<bool, ShoppingProductChannelExclusivity>(
          obj.channelExclusivitySpecified, obj.channelExclusivity).GetHashCode();
    }

    /// <summary>
    /// Determines whether the specified <see cref="ProductBiddingCategory"/> objects
    /// are equal.
    /// </summary>
    /// <param name="x">The first object to compare.</param>
    /// <param name="y">The second object to compare.</param>
    /// <returns>
    /// True if the specified objects are equal; otherwise, false.
    /// </returns>
    private bool Equals(ProductBiddingCategory x, ProductBiddingCategory y) {
      return x.typeSpecified == y.typeSpecified && x.type == y.type && x.value == y.value;
    }

    /// <summary>
    /// Determines whether the specified <see cref="ProductBrand"/> objects
    /// are equal.
    /// </summary>
    /// <param name="x">The first object to compare.</param>
    /// <param name="y">The second object to compare.</param>
    /// <returns>
    /// True if the specified objects are equal; otherwise, false.
    /// </returns>
    private bool Equals(ProductBrand x, ProductBrand y) {
      return AreValuesEqual(x.value, y.value);
    }

    /// <summary>
    /// Determines whether the specified <see cref="ProductCanonicalCondition"/> objects
    /// are equal.
    /// </summary>
    /// <param name="x">The first object to compare.</param>
    /// <param name="y">The second object to compare.</param>
    /// <returns>
    /// True if the specified objects are equal; otherwise, false.
    /// </returns>
    private bool Equals(ProductCanonicalCondition x, ProductCanonicalCondition y) {
      return x.conditionSpecified == y.conditionSpecified && x.condition == y.condition;
    }

    /// <summary>
    /// Determines whether the specified <see cref="ProductCustomAttribute"/> objects
    /// are equal.
    /// </summary>
    /// <param name="x">The first object to compare.</param>
    /// <param name="y">The second object to compare.</param>
    /// <returns>
    /// True if the specified objects are equal; otherwise, false.
    /// </returns>
    private bool Equals(ProductCustomAttribute x, ProductCustomAttribute y) {
      return x.typeSpecified == y.typeSpecified && x.type == y.type &&
          AreValuesEqual(x.value, y.value);
    }

    /// <summary>
    /// Determines whether the specified <see cref="ProductOfferId"/> objects
    /// are equal.
    /// </summary>
    /// <param name="x">The first object to compare.</param>
    /// <param name="y">The second object to compare.</param>
    /// <returns>
    /// True if the specified objects are equal; otherwise, false.
    /// </returns>
    private bool Equals(ProductOfferId x, ProductOfferId y) {
      return AreValuesEqual(x.value, y.value);
    }

    /// <summary>
    /// Determines whether the specified <see cref="ProductType"/> objects
    /// are equal.
    /// </summary>
    /// <param name="x">The first object to compare.</param>
    /// <param name="y">The second object to compare.</param>
    /// <returns>
    /// True if the specified objects are equal; otherwise, false.
    /// </returns>
    private bool Equals(ProductType x, ProductType y) {
      return x.typeSpecified == y.typeSpecified && x.type == y.type &&
          AreValuesEqual(x.value, y.value);
    }

    /// <summary>
    /// Determines whether the specified <see cref="ProductChannel"/> objects
    /// are equal.
    /// </summary>
    /// <param name="x">The first object to compare.</param>
    /// <param name="y">The second object to compare.</param>
    /// <returns>
    /// True if the specified objects are equal; otherwise, false.
    /// </returns>
    private bool Equals(ProductChannel x, ProductChannel y) {
      return x.channelSpecified == y.channelSpecified && x.channel == y.channel;
    }

    /// <summary>
    /// Determines whether the specified <see cref="ProductChannelExclusivity"/>
    /// objects are equal.
    /// </summary>
    /// <param name="x">The first object to compare.</param>
    /// <param name="y">The second object to compare.</param>
    /// <returns>
    /// True if the specified objects are equal; otherwise, false.
    /// </returns>
    private bool Equals(ProductChannelExclusivity x, ProductChannelExclusivity y) {
      return x.channelExclusivitySpecified == y.channelExclusivitySpecified &&
          x.channelExclusivity == y.channelExclusivity;
    }

    /// <summary>
    /// Checks if two string values are equal.
    /// </summary>
    /// <param name="x">The first value.</param>
    /// <param name="y">The second value.</param>
    /// <returns>true if x and y are equal, false otherwise.</returns>
    /// <remarks>If both strings are null, then they are considered equal.
    /// Strings are converted to lowercase before comparison.</remarks>
    private bool AreValuesEqual(string x, string y) {
      if (x == null && y == null) {
        return true;
      }
      return x != null && y != null && x.ToLower() == y.ToLower();
    }
  }
}

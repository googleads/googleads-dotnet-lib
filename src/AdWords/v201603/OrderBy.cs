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

using Google.Api.Ads.Common.Logging;

namespace Google.Api.Ads.AdWords.v201603 {

  /// <summary>
  /// Specifies how the resulting information should be sorted.
  /// </summary>
  public partial class OrderBy {

    /// <summary>
    /// The registry for saving feature usage information..
    /// </summary>
    private static readonly AdsFeatureUsageRegistry featureUsageRegistry =
        AdsFeatureUsageRegistry.Instance;

    /// <summary>
    /// The feature ID for this class.
    /// </summary>
    private const AdsFeatureUsageRegistry.Features FEATURE_ID =
        AdsFeatureUsageRegistry.Features.SelectorBuilder;

    /// <summary>
    /// Creates an ascending sorting order to be used with a selector.
    /// </summary>
    /// <param name="field">The field to sort on.</param>
    /// <returns>A new <see cref="Orderby"/> object that sorts the result in
    /// ascending order by <paramref name="field"/> value.</returns>
    public static OrderBy Asc(string field) {
      // Mark the usage.
      featureUsageRegistry.MarkUsage(FEATURE_ID);;

      return new OrderBy() {
        field = field,
        sortOrder = SortOrder.ASCENDING
      };
    }

    /// <summary>
    /// Creates a descending sorting order to be used with a selector.
    /// </summary>
    /// <param name="field">The field to sort on.</param>
    /// <returns>A new <see cref="Orderby"/> object that sorts the result in
    /// descending order by <paramref name="field"/> value.</returns>
    public static OrderBy Desc(string field) {
      // Mark the usage.
      featureUsageRegistry.MarkUsage(FEATURE_ID);;

      return new OrderBy() {
        field = field,
        sortOrder = SortOrder.DESCENDING
      };
    }
  }
}

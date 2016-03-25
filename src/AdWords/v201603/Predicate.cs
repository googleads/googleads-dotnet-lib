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

using System.Collections.Generic;

namespace Google.Api.Ads.AdWords.v201603 {

  /// <summary>
  /// Specifies how an entity (eg. adgroup, campaign, criterion, ad) should be
  /// filtered.
  /// </summary>
  public partial class Predicate {

    /// <summary>
    /// The feature ID for this class.
    /// </summary>
    private const AdsFeatureUsageRegistry.Features FEATURE_ID =
        AdsFeatureUsageRegistry.Features.SelectorBuilder;

    /// <summary>
    /// The registry for saving feature usage information..
    /// </summary>
    private static readonly AdsFeatureUsageRegistry featureUsageRegistry =
        AdsFeatureUsageRegistry.Instance;

    /// <summary>
    /// Adds a filter to select only items where the value of
    /// <paramref name="field"/> satisfies a particular condition.
    /// </summary>
    /// <param name="field">The field to filter on.</param>
    /// <param name="operator">The predicate operator.</param>
    /// <param name="values">The predicate values.</param>
    /// <returns>A new <see cref="Predicate"/> object that filters by specified
    /// conditions.</returns>
    private static Predicate WithCondition(string field, PredicateOperator @operator,
        string[] values) {
      // Mark the usage.
      featureUsageRegistry.MarkUsage(FEATURE_ID);;

      return new Predicate() {
        field = field,
        @operator = @operator,
        values = values
      };
    }

    /// <summary>
    /// Adds a filter to select only items where the value of
    /// <paramref name="field"/> is equal to <paramref name="value"/>.
    /// </summary>
    /// <param name="field">The field to filter on.</param>
    /// <param name="value">The predicate value.</param>
    /// <returns>A new <see cref="Predicate"/> object that filters only items
    /// where the value of <paramref name="field"/> is equal to
    /// <paramref name="value"/></returns>
    public static Predicate Equals(string field, long value) {
      return Equals(field, value.ToString());
    }

    /// <summary>
    /// Adds a filter to select only items where the value of
    /// <paramref name="field"/> is equal to <paramref name="value"/>.
    /// </summary>
    /// <param name="field">The field to filter on.</param>
    /// <param name="value">The predicate value.</param>
    /// <returns>A new <see cref="Predicate"/> object that filters only items
    /// where the value of <paramref name="field"/> is equal to
    /// <paramref name="value"/></returns>
    public static Predicate Equals(string field, string value) {
      return WithCondition(field, PredicateOperator.EQUALS, new string[] { value });
    }

    /// <summary>
    /// Adds a filter to select only items where the value of
    /// <paramref name="field"/> is not equal to <paramref name="value"/>.
    /// </summary>
    /// <param name="field">The field to filter on.</param>
    /// <param name="value">The predicate value.</param>
    /// <returns>A new <see cref="Predicate"/> object that filters only items
    /// where the value of <paramref name="field"/> is not equal to
    /// <paramref name="value"/></returns>
    public static Predicate NotEquals(string field, string value) {
      return WithCondition(field, PredicateOperator.NOT_EQUALS, new string[] { value });
    }

    /// <summary>
    /// Adds a filter to select only items where the value of
    /// <paramref name="field"/> is equal to one of the values provided in
    /// <paramref name="values"/>.
    /// </summary>
    /// <param name="field">The field to filter on.</param>
    /// <param name="values">The predicate values.</param>
    /// <returns>A new <see cref="Predicate"/> object that filters only items
    /// where the value of <paramref name="field"/> is equal to one of the
    /// values provided in <paramref name="values"/>.</returns>
    public static Predicate In(string field, List<string> values) {
      return In(field, values.ToArray());
    }

    /// <summary>
    /// Adds a filter to select only items where the value of
    /// <paramref name="field"/> is equal of the values provided in
    /// <paramref name="values"/>.
    /// </summary>
    /// <param name="field">The field to filter on.</param>
    /// <param name="values">The predicate values.</param>
    /// <returns>A new <see cref="Predicate"/> object that filters only items
    /// where the value of <paramref name="field"/> is equal to one of the
    /// values provided in <paramref name="values"/>.</returns>
    public static Predicate In(string field, string[] values) {
      return WithCondition(field, PredicateOperator.IN, values);
    }

    /// <summary>
    /// Adds a filter to select only items where the value of
    /// <paramref name="field"/> is not equal to any of the values provided
    /// in <paramref name="values"/>.
    /// </summary>
    /// <param name="field">The field to filter on.</param>
    /// <param name="values">The predicate values.</param>
    /// <returns>A new <see cref="Predicate"/> object that filters only items
    /// where the value of <paramref name="field"/> is not equal to any of the
    /// values provided in <paramref name="values"/>.</returns>
    public static Predicate NotIn(string field, string[] values) {
      return WithCondition(field, PredicateOperator.NOT_IN, values);
    }

    /// <summary>
    /// Adds a filter to select only items where the value of
    /// <paramref name="field"/> is greater than <paramref name="value"/>.
    /// </summary>
    /// <param name="field">The field to filter on.</param>
    /// <param name="value">The predicate value.</param>
    /// <returns>A new <see cref="Predicate"/> object that filters only items
    ///  where the value of <paramref name="field"/> is greater than
    ///  <paramref name="value"/>.</returns>
    public static Predicate GreaterThan(string field, string value) {
      return WithCondition(field, PredicateOperator.GREATER_THAN, new string[] { value });
    }

    /// <summary>
    /// Adds a filter for items where <paramref name="field"/> is greater than
    /// or equal to <paramref name="value"/>.
    /// </summary>
    /// <param name="field">The field to filter on.</param>
    /// <param name="value">The predicate value.</param>
    /// <returns>A new <see cref="Predicate"/> object that filters only items
    ///  where the value of <paramref name="field"/> is greater than
    /// or equal to <paramref name="value"/>.</returns>
    public static Predicate GreaterThanEquals(string field, string value) {
      return WithCondition(field, PredicateOperator.GREATER_THAN_EQUALS, new string[] { value });
    }

    /// <summary>
    /// Adds a filter to select only items where the value of
    /// <paramref name="field"/> is less than <paramref name="value"/>.
    /// </summary>
    /// <param name="field">The field to filter on.</param>
    /// <param name="value">The predicate value.</param>
    /// <returns>A new <see cref="Predicate"/> object that filters only items
    ///  where the value of <paramref name="field"/> is less than
    ///  <paramref name="value"/>.</returns>
    public static Predicate LessThan(string field, string value) {
      return WithCondition(field, PredicateOperator.LESS_THAN, new string[] { value });
    }

    /// <summary>
    /// Adds a filter to select only items where the value of
    /// <paramref name="field"/> is less than or equal to
    /// <paramref name="value"/>.
    /// </summary>
    /// <param name="field">The field to filter on.</param>
    /// <param name="value">The predicate value.</param>
    /// <returns>A new <see cref="Predicate"/> object that filters only items
    ///  where the value of <paramref name="field"/> is less than or equal to
    ///  <paramref name="value"/>.</returns>
    public static Predicate LessThanEquals(string field, string value) {
      return WithCondition(field, PredicateOperator.LESS_THAN_EQUALS, new string[] { value });
    }

    /// <summary>
    /// Adds a filter to select only items where the value of
    /// <paramref name="field"/> starts with <paramref name="value"/>.
    /// </summary>
    /// <param name="field">The field to filter on.</param>
    /// <param name="value">The predicate value.</param>
    /// <returns>A new <see cref="Predicate"/> object that filters only items
    ///  where the value of <paramref name="field"/> starts with
    ///  <paramref name="value"/>.</returns>
    public static Predicate StartsWith(string field, string value) {
      return WithCondition(field, PredicateOperator.STARTS_WITH, new string[] { value });
    }

    /// <summary>
    /// Adds a filter to select only items where the value of
    /// <paramref name="field"/> starts with <paramref name="value"/> when
    /// letter case is ignored.
    /// </summary>
    /// <param name="field">The field to filter on.</param>
    /// <param name="value">The predicate value.</param>
    /// <returns>A new <see cref="Predicate"/> object that filters only items
    ///  where the value of <paramref name="field"/> starts with
    ///  <paramref name="value"/> when letter case is ignored.</returns>
    public static Predicate StartsWithIgnoreCase(string field, string value) {
      return WithCondition(field, PredicateOperator.STARTS_WITH_IGNORE_CASE,
          new string[] { value });
    }

    /// <summary>
    /// Adds a filter to select only items where the value of
    /// <paramref name="field"/> contains <paramref name="value"/>.
    /// </summary>
    /// <param name="field">The field to filter on.</param>
    /// <param name="value">The predicate value.</param>
    /// <returns>A new <see cref="Predicate"/> object that filters only items
    ///  where the value of <paramref name="field"/> contains
    ///  <paramref name="value"/>.</returns>
    public static Predicate Contains(string field, string value) {
      return WithCondition(field, PredicateOperator.CONTAINS, new string[] { value });
    }

    /// <summary>
    /// Adds a filter to select only items where the value of
    /// <paramref name="field"/> contains <paramref name="value"/> when letter
    /// case is ignored.
    /// </summary>
    /// <param name="field">The field to filter on.</param>
    /// <param name="value">The predicate value.</param>
    /// <returns>A new <see cref="Predicate"/> object that filters only items
    ///  where the value of <paramref name="field"/> contains
    ///  <paramref name="value"/> when letter case is ignored.</returns>
    public static Predicate ContainsIgnoreCase(string field, string value) {
      return WithCondition(field, PredicateOperator.CONTAINS_IGNORE_CASE, new string[] { value });
    }

    /// <summary>
    /// Adds a filter to select only items where the value of
    /// <paramref name="field"/> does not contain <paramref name="value"/>.
    /// </summary>
    /// <param name="field">The field to filter on.</param>
    /// <param name="value">The predicate value.</param>
    /// <returns>A new <see cref="Predicate"/> object that filters only items
    ///  where the value of <paramref name="field"/> does not contain
    ///  <paramref name="value"/>.</returns>
    public static Predicate DoesNotContain(string field, string value) {
      return WithCondition(field, PredicateOperator.DOES_NOT_CONTAIN, new string[] { value });
    }

    /// <summary>
    /// Adds a filter to select only items where the value of
    /// <paramref name="field"/> does not contain <paramref name="value"/> when
    /// letter case is ignored.
    /// </summary>
    /// <param name="field">The field to filter on.</param>
    /// <param name="value">The predicate value.</param>
    /// <returns>A new <see cref="Predicate"/> object that filters only items
    ///  where the value of <paramref name="field"/> does not contain
    ///  <paramref name="value"/> when letter case is ignored.</returns>
    public static Predicate DoesNotContainIgnoreCase(string field, string value) {
      return WithCondition(field, PredicateOperator.DOES_NOT_CONTAIN_IGNORE_CASE,
          new string[] { value });
    }

    /// <summary>
    /// Adds a filter to select only items where the value of
    /// <paramref name="field"/> contains any of the values provided in
    /// <paramref name="values"/>.
    /// </summary>
    /// <param name="field">The field to filter on.</param>
    /// <param name="values">The predicate value.</param>
    /// <returns>A new <see cref="Predicate"/> object that filters only items
    ///  where the value of <paramref name="field"/> contains any of the values
    ///  provided in <paramref name="value"/>.</returns>
    public static Predicate ContainsAny(string field, string[] values) {
      return WithCondition(field, PredicateOperator.CONTAINS_ANY, values);
    }

    /// <summary>
    /// Adds a filter to select only items where the value of
    /// <paramref name="field"/> contains all of the values provided in
    /// <paramref name="values"/>.
    /// </summary>
    /// <param name="field">The field to filter on.</param>
    /// <param name="values">The predicate value.</param>
    /// <returns>A new <see cref="Predicate"/> object that filters only items
    ///  where the value of <paramref name="field"/> contains all of the values
    ///  provided in <paramref name="value"/>.</returns>
    public static Predicate ContainsAll(string field, string[] values) {
      return WithCondition(field, PredicateOperator.CONTAINS_ALL, values);
    }

    /// <summary>
    /// Adds a filter to select only items where the value of
    /// <paramref name="field"/> contains none of the values provided in
    /// <paramref name="values"/>.
    /// </summary>
    /// <param name="field">The field to filter on.</param>
    /// <param name="values">The predicate value.</param>
    /// <returns>A new <see cref="Predicate"/> object that filters only items
    ///  where the value of <paramref name="field"/> contains none of the values
    ///  provided in <paramref name="value"/>.</returns>
    public static Predicate ContainsNone(string field, string[] values) {
      return WithCondition(field, PredicateOperator.CONTAINS_NONE, values);
    }
  }
}

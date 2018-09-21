// Copyright 2018, Google Inc. All Rights Reserved.
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


namespace Google.Api.Ads.AdWords.Util.Reports
{
    /// <summary>
    /// Builder interface for WHERE clause.
    /// </summary>
    /// <typeparam name="TParent">The parent builder type.</typeparam>
    public interface IWhereBuilder<TParent>
    {
        /// <summary>
        /// Adds the predicate <b>EQUAL_TO</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value as a string independent of the field
        ///  type. The caller should take care of the formatting if it is necessary.</param>
        /// <returns>The parent query builder for call chaining.</returns>
        TParent Equals(string propertyValue);

        /// <summary>
        /// Adds the predicate <b>EQUAL_TO</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value to be used in query.</param>
        /// <returns>The parent query builder for call chaining.</returns>
        TParent Equals(int propertyValue);

        /// <summary>
        /// Adds the predicate <b>EQUAL_TO</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value to be used in query.</param>
        /// <returns>The parent query builder for call chaining.</returns>
        TParent Equals(long propertyValue);

        /// <summary>
        /// Adds the predicate <b>EQUAL_TO</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value to be used in query.</param>
        /// <returns>The parent query builder for call chaining.</returns>
        TParent Equals(bool propertyValue);

        /// <summary>
        /// Adds the predicate <b>NOT_EQUAL_TO</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value as a string independent of the field
        ///  type. The caller should take care of the formatting if it is necessary.</param>
        /// <returns>The parent query builder for call chaining.</returns>
        TParent NotEquals(string propertyValue);

        /// <summary>
        /// Adds the predicate <b>NOT_EQUAL_TO</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value to be used in query.</param>
        /// <returns>The parent query builder for call chaining.</returns>
        TParent NotEquals(int propertyValue);

        /// <summary>
        /// Adds the predicate <b>NOT_EQUAL_TO</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value to be used in query.</param>
        /// <returns>The parent query builder for call chaining.</returns>
        TParent NotEquals(long propertyValue);

        /// <summary>
        /// Adds the predicate <b>NOT_EQUAL_TO</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value to be used in query.</param>
        /// <returns>The parent query builder for call chaining.</returns>
        TParent NotEquals(bool propertyValue);

        /// <summary>
        /// Adds the predicate <b>CONTAINS</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value as a string independent of the field
        ///  type. The caller should take care of the formatting if it is necessary.</param>
        /// <returns>The parent query builder for call chaining.</returns>
        TParent Contains(string propertyValue);

        /// <summary>
        /// Adds the predicate <b>CONTAINS_IGNORE_CASE</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value as a string independent of the field
        ///  type. The caller should take care of the formatting if it is necessary.</param>
        /// <returns>The parent query builder for call chaining.</returns>
        TParent ContainsIgnoreCase(string propertyValue);

        /// <summary>
        /// Adds the predicate <b>DOES_NOT_CONTAIN</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value as a string independent of the field
        ///  type. The caller should take care of the formatting if it is necessary.</param>
        /// <returns>The parent query builder for call chaining.</returns>
        TParent DoesNotContain(string propertyValue);

        /// <summary>
        /// Adds the predicate <b>DOES_NOT_CONTAIN_IGNORE_CASE</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value as a string independent of the field
        ///  type. The caller should take care of the formatting if it is necessary.</param>
        /// <returns>The parent query builder for call chaining.</returns>
        TParent DoesNotContainIgnoreCase(string propertyValue);

        /// <summary>
        /// Adds the predicate <b>GREATER_THAN</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value for comparison.</param>
        /// <returns>The parent query builder for call chaining.</returns>
        TParent GreaterThan(long propertyValue);

        /// <summary>
        /// Adds the predicate <b>GREATER_THAN</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value for comparison.</param>
        /// <returns>The parent query builder for call chaining.</returns>
        TParent GreaterThan(int propertyValue);

        /// <summary>
        /// Adds the predicate <b>GREATER_THAN</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value for comparison.</param>
        /// <returns>The parent query builder for call chaining.</returns>
        TParent GreaterThan(double propertyValue);

        /// <summary>
        /// Adds the predicate <b>GREATER_THAN_OR_EQUAL_TO</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value for comparison.</param>
        /// <returns>The parent query builder for call chaining.</returns>
        TParent GreaterThanOrEqualTo(long propertyValue);

        /// <summary>
        /// Adds the predicate <b>GREATER_THAN_OR_EQUAL_TO</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value for comparison.</param>
        /// <returns>The parent query builder for call chaining.</returns>
        TParent GreaterThanOrEqualTo(int propertyValue);

        /// <summary>
        /// Adds the predicate <b>LESS_THAN</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value for comparison.</param>
        /// <returns>
        /// The containing query builder.
        /// </returns>
        TParent LessThan(long propertyValue);

        /// <summary>
        /// Adds the predicate <b>LESS_THAN</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value for comparison.</param>
        /// <returns>
        /// The containing query builder.
        /// </returns>
        TParent LessThan(int propertyValue);

        /// <summary>
        /// Adds the predicate <b>LESS_THAN</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value for comparison.</param>
        /// <returns>
        /// The containing query builder.
        /// </returns>
        TParent LessThan(double propertyValue);

        /// <summary>
        /// Adds the predicate <b>LESS_THAN_OR_EQUAL_TO</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value for comparison.</param>
        /// <returns>The parent query builder for call chaining.</returns>
        TParent LessThanOrEqualTo(long propertyValue);

        /// <summary>
        /// Adds the predicate <b>STARTS_WITH</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The prefix of property value for comparison.</param>
        /// <returns>The parent query builder for call chaining.</returns>
        TParent StartsWith(string propertyValue);

        /// <summary>
        /// Adds the predicate <b>STARTS_WITH_IGNORE_CASE</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The prefix of property value for comparison.</param>
        /// <returns>The parent query builder for call chaining.</returns>
        TParent StartsWithIgnoreCase(string propertyValue);

        /// <summary>
        /// Adds the predicate <b>IN</b> to the query for the given set of values.
        /// </summary>
        /// <param name="values">The list of values for comparison.</param>
        /// <returns>The parent query builder for call chaining.</returns>
        TParent In(params string[] values);

        /// <summary>
        /// Adds the predicate <b>IN</b> to the query for the given set of values.
        /// </summary>
        /// <param name="values">The list of values for comparison.</param>
        /// <returns>The parent query builder for call chaining.</returns>
        TParent In(params long[] values);

        /// <summary>
        /// Adds the predicate <b>NOT_IN</b> to the query for the given set of values.
        /// </summary>
        /// <param name="values">The list of values for comparison.</param>
        /// <returns>The parent query builder for call chaining.</returns>
        TParent NotIn(params string[] values);

        /// <summary>
        /// Adds the predicate <b>NOT_IN</b> to the query for the given set of values.
        /// </summary>
        /// <param name="values">The list of values for comparison.</param>
        /// <returns>The parent query builder for call chaining.</returns>
        TParent NotIn(params long[] values);

        /// <summary>
        /// Adds the predicate <b>CONTAINS_ANY</b> to the query for the given set of values.
        /// </summary>
        /// <param name="values">The list of values for comparison.</param>
        /// <returns>The parent query builder for call chaining.</returns>
        TParent ContainsAny(params string[] values);

        /// <summary>
        /// Adds the predicate <b>CONTAINS_ANY</b> to the query for the given set of values.
        /// </summary>
        /// <param name="values">The list of values for comparison.</param>
        /// <returns>The parent query builder for call chaining.</returns>
        TParent ContainsAny(params long[] values);

        /// <summary>
        /// Adds the predicate <b>CONTAINS_ALL</b> to the query for the given set of values.
        /// </summary>
        /// <param name="values">The list of values for comparison.</param>
        /// <returns>The parent query builder for call chaining.</returns>
        TParent ContainsAll(params string[] values);

        /// <summary>
        /// Adds the predicate <b>CONTAINS_ALL</b> to the query for the given set of values.
        /// </summary>
        /// <param name="values">The list of values for comparison.</param>
        /// <returns>The parent query builder for call chaining.</returns>
        TParent ContainsAll(params long[] values);

        /// <summary>
        /// Adds the predicate <b>CONTAINS_NONE</b> to the query for the given set of values.
        /// </summary>
        /// <param name="values">The list of values for comparison.</param>
        /// <returns>The parent query builder for call chaining.</returns>
        TParent ContainsNone(params string[] values);

        /// <summary>
        /// Adds the predicate <b>CONTAINS_NONE</b> to the query for the given set of values.
        /// </summary>
        /// <param name="values">The list of values for comparison.</param>
        /// <returns>The parent query builder for call chaining.</returns>
        TParent ContainsNone(params long[] values);

        /// <summary>
        /// Adds the predicate <b>CONTAINS_NONE</b> to the query for the given set of values.
        /// </summary>
        /// <param name="values">The list of values for comparison.</param>
        /// <returns>The parent query builder for call chaining.</returns>
        TParent ContainsNone(params int[] values);

        /// <summary>
        /// Adds the predicate <b>CONTAINS_NONE</b> to the query for the given set of values.
        /// </summary>
        /// <param name="values">The list of values for comparison.</param>
        /// <returns>The parent query builder for call chaining.</returns>
        TParent ContainsNone(params double[] values);
    }
}

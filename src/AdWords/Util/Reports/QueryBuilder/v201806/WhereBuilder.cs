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

using Google.Api.Ads.AdWords.v201806;

using System;

namespace Google.Api.Ads.AdWords.Util.Reports.v201806
{
    /// <summary>
    /// Builder for WHERE clause.
    /// </summary>
    /// <typeparam name="TParent">Type of the parent query builder.</typeparam>
    public class WhereBuilder<TParent> : IWhereBuilder<TParent>
    {
        /// <summary>
        /// The predicate to keep intermediate state.
        /// </summary>
        private Predicate predicate;

        /// <summary>
        /// The parent query builder for call chaining.
        /// </summary>
        private TParent queryBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="WhereBuilder{TParent}"/> class.
        /// </summary>
        /// <param name="predicate">The predicate to store internal state.</param>
        /// <param name="queryBuilder">The parent query builder for call chaining.</param>
        internal WhereBuilder(Predicate predicate, TParent queryBuilder)
        {
            this.queryBuilder = queryBuilder;
            this.predicate = predicate;
        }

        /// <summary>
        /// Protected constructor for testing purposes.
        /// </summary>
        protected WhereBuilder()
        {
        }

        /// <summary>
        /// Protected creation method for testing purposes.
        /// </summary>
        /// <param name="field">The field to filter on.</param>
        /// <param name="queryBuilder">The parent query builder for call chaining.</param>
        protected void Init(string field, TParent queryBuilder)
        {
            this.predicate = new Predicate()
            {
                field = field
            };
            this.queryBuilder = queryBuilder;
        }

        /// <summary>
        /// Sets the condition.
        /// </summary>
        /// <typeparam name="T">Type of predicate values.</typeparam>
        /// <param name="operator">The predicate operator.</param>
        /// <param name="values">The values.</param>
        /// <returns>The parent query builder for call chaining.</returns>
        private TParent SetCondition<T>(PredicateOperator @operator, T[] values)
        {
            predicate.@operator = @operator;
            if (typeof(T) == typeof(string))
            {
                predicate.values = Array.ConvertAll(values, delegate(T value)
                {
                    // backslash, singlequote and doublequotes should be escaped. See
                    // https://developers.google.com/adwords/api/docs/guides/awql#notes_1
                    return "'" + value.ToString()
                        // Replace backslashes first, so double-escaping won't happen.
                        .Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("'", "\\'") + "'";
                });
            }
            else if (typeof(T) == typeof(bool))
            {
                // When using a Boolean condition in a predicate, it should be sent as TRUE or FALSE.
                predicate.values = Array.ConvertAll(values, value => value.ToString().ToUpper());
            }
            else
            {
                predicate.values = Array.ConvertAll(values, value => value.ToString());
            }

            return queryBuilder;
        }

        /// <summary>
        /// Returns a string that represents this instance.
        /// </summary>
        /// <returns>
        /// A string that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return predicate.ToString();
        }

        /// <summary>
        /// Adds the predicate <b>EQUAL_TO</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value as a string independent of the field type.
        /// The caller should take care of the formatting if it is necessary.</param>
        /// <returns>
        /// The parent query builder for call chaining.
        /// </returns>
        public TParent Equals(string propertyValue)
        {
            return SetCondition(PredicateOperator.EQUALS, new string[]
            {
                propertyValue
            });
        }

        /// <summary>
        /// Adds the predicate <b>EQUAL_TO</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value to be used in query.</param>
        /// <returns>
        /// The parent query builder for call chaining.
        /// </returns>
        public TParent Equals(int propertyValue)
        {
            return SetCondition(PredicateOperator.EQUALS, new int[]
            {
                propertyValue
            });
        }

        /// <summary>
        /// Adds the predicate <b>EQUAL_TO</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value to be used in query.</param>
        /// <returns>
        /// The parent query builder for call chaining.
        /// </returns>
        public TParent Equals(long propertyValue)
        {
            return SetCondition(PredicateOperator.EQUALS, new long[]
            {
                propertyValue
            });
        }

        /// <summary>
        /// Adds the predicate <b>EQUAL_TO</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value to be used in query.</param>
        /// <returns>
        /// The parent query builder for call chaining.
        /// </returns>
        public TParent Equals(bool propertyValue)
        {
            return SetCondition(PredicateOperator.EQUALS, new bool[]
            {
                propertyValue
            });
        }

        /// <summary>
        /// Adds the predicate <b>NOT_EQUAL_TO</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value as a string independent of the field type.
        /// The caller should take care of the formatting if it is necessary.</param>
        /// <returns>
        /// The parent query builder for call chaining.
        /// </returns>
        public TParent NotEquals(string propertyValue)
        {
            return SetCondition(PredicateOperator.NOT_EQUALS, new string[]
            {
                propertyValue
            });
        }

        /// <summary>
        /// Adds the predicate <b>NOT_EQUAL_TO</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value to be used in query.</param>
        /// <returns>
        /// The parent query builder for call chaining.
        /// </returns>
        public TParent NotEquals(int propertyValue)
        {
            return SetCondition(PredicateOperator.NOT_EQUALS, new int[]
            {
                propertyValue
            });
        }

        /// <summary>
        /// Adds the predicate <b>NOT_EQUAL_TO</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value to be used in query.</param>
        /// <returns>
        /// The parent query builder for call chaining.
        /// </returns>
        public TParent NotEquals(long propertyValue)
        {
            return SetCondition(PredicateOperator.NOT_EQUALS, new long[]
            {
                propertyValue
            });
        }

        /// <summary>
        /// Adds the predicate <b>NOT_EQUAL_TO</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value to be used in query.</param>
        /// <returns>
        /// The parent query builder for call chaining.
        /// </returns>
        public TParent NotEquals(bool propertyValue)
        {
            return SetCondition(PredicateOperator.NOT_EQUALS, new bool[]
            {
                propertyValue
            });
        }

        /// <summary>
        /// Adds the predicate <b>CONTAINS</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value as a string independent of the field type.
        /// The caller should take care of the formatting if it is necessary.</param>
        /// <returns>
        /// The parent query builder for call chaining.
        /// </returns>
        public TParent Contains(string propertyValue)
        {
            return SetCondition(PredicateOperator.CONTAINS, new string[]
            {
                propertyValue
            });
        }

        /// <summary>
        /// Adds the predicate <b>CONTAINS_ALL</b> to the query for the given set of values.
        /// </summary>
        /// <param name="values">The list of values for comparison.</param>
        /// <returns>
        /// The parent query builder for call chaining.
        /// </returns>
        public TParent ContainsAll(params string[] values)
        {
            return SetCondition(PredicateOperator.CONTAINS_ALL, values);
        }

        /// <summary>
        /// Adds the predicate <b>CONTAINS_ALL</b> to the query for the given set of values.
        /// </summary>
        /// <param name="values">The list of values for comparison.</param>
        /// <returns>
        /// The parent query builder for call chaining.
        /// </returns>
        public TParent ContainsAll(params long[] values)
        {
            return SetCondition(PredicateOperator.CONTAINS_ALL, values);
        }

        /// <summary>
        /// Adds the predicate <b>CONTAINS_ANY</b> to the query for the given set of values.
        /// </summary>
        /// <param name="values">The list of values for comparison.</param>
        /// <returns>
        /// The parent query builder for call chaining.
        /// </returns>
        public TParent ContainsAny(params string[] values)
        {
            return SetCondition(PredicateOperator.CONTAINS_ANY, values);
        }

        /// <summary>
        /// Adds the predicate <b>CONTAINS_ANY</b> to the query for the given set of values.
        /// </summary>
        /// <param name="values">The list of values for comparison.</param>
        /// <returns>
        /// The parent query builder for call chaining.
        /// </returns>
        public TParent ContainsAny(params long[] values)
        {
            return SetCondition(PredicateOperator.CONTAINS_ANY, values);
        }

        /// <summary>
        /// Adds the predicate <b>CONTAINS_IGNORE_CASE</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value as a string independent of the field type.
        /// The caller should take care of the formatting if it is necessary.</param>
        /// <returns>
        /// The parent query builder for call chaining.
        /// </returns>
        public TParent ContainsIgnoreCase(string propertyValue)
        {
            return SetCondition(PredicateOperator.CONTAINS_IGNORE_CASE, new string[]
            {
                propertyValue
            });
        }

        /// <summary>
        /// Adds the predicate <b>CONTAINS_NONE</b> to the query for the given set of values.
        /// </summary>
        /// <param name="values">The list of values for comparison.</param>
        /// <returns>
        /// The parent query builder for call chaining.
        /// </returns>
        public TParent ContainsNone(params string[] values)
        {
            return SetCondition(PredicateOperator.CONTAINS_NONE, values);
        }

        /// <summary>
        /// Adds the predicate <b>CONTAINS_NONE</b> to the query for the given set of values.
        /// </summary>
        /// <param name="values">The list of values for comparison.</param>
        /// <returns>
        /// The parent query builder for call chaining.
        /// </returns>
        public TParent ContainsNone(params long[] values)
        {
            return SetCondition(PredicateOperator.CONTAINS_NONE, values);
        }

        /// <summary>
        /// Adds the predicate <b>CONTAINS_NONE</b> to the query for the given set of values.
        /// </summary>
        /// <param name="values">The list of values for comparison.</param>
        /// <returns>
        /// The parent query builder for call chaining.
        /// </returns>
        public TParent ContainsNone(params int[] values)
        {
            return SetCondition(PredicateOperator.CONTAINS_NONE, values);
        }

        /// <summary>
        /// Adds the predicate <b>CONTAINS_NONE</b> to the query for the given set of values.
        /// </summary>
        /// <param name="values">The list of values for comparison.</param>
        /// <returns>
        /// The parent query builder for call chaining.
        /// </returns>
        public TParent ContainsNone(params double[] values)
        {
            return SetCondition(PredicateOperator.CONTAINS_NONE, values);
        }

        /// <summary>
        /// Adds the predicate <b>DOES_NOT_CONTAIN</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value as a string independent of the field type.
        /// The caller should take care of the formatting if it is necessary.</param>
        /// <returns>
        /// The parent query builder for call chaining.
        /// </returns>
        public TParent DoesNotContain(string propertyValue)
        {
            return SetCondition(PredicateOperator.DOES_NOT_CONTAIN, new string[]
            {
                propertyValue
            });
        }

        /// <summary>
        /// Adds the predicate <b>DOES_NOT_CONTAIN_IGNORE_CASE</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value as a string independent of the field type.
        /// The caller should take care of the formatting if it is necessary.</param>
        /// <returns>
        /// The parent query builder for call chaining.
        /// </returns>
        public TParent DoesNotContainIgnoreCase(string propertyValue)
        {
            return SetCondition(PredicateOperator.DOES_NOT_CONTAIN_IGNORE_CASE, new string[]
            {
                propertyValue
            });
        }

        /// <summary>
        /// Adds the predicate <b>GREATER_THAN</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value for comparison.</param>
        /// <returns>
        /// The parent query builder for call chaining.
        /// </returns>
        public TParent GreaterThan(long propertyValue)
        {
            return SetCondition(PredicateOperator.GREATER_THAN, new long[]
            {
                propertyValue
            });
        }

        /// <summary>
        /// Adds the predicate <b>GREATER_THAN</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value for comparison.</param>
        /// <returns>
        /// The parent query builder for call chaining.
        /// </returns>
        public TParent GreaterThan(int propertyValue)
        {
            return SetCondition(PredicateOperator.GREATER_THAN, new int[]
            {
                propertyValue
            });
        }

        /// <summary>
        /// Adds the predicate <b>GREATER_THAN</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value for comparison.</param>
        /// <returns>
        /// The parent query builder for call chaining.
        /// </returns>
        public TParent GreaterThan(double propertyValue)
        {
            return SetCondition(PredicateOperator.GREATER_THAN, new double[]
            {
                propertyValue
            });
        }

        /// <summary>
        /// Adds the predicate <b>GREATER_THAN_OR_EQUAL_TO</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value for comparison.</param>
        /// <returns>
        /// The parent query builder for call chaining.
        /// </returns>
        public TParent GreaterThanOrEqualTo(long propertyValue)
        {
            return SetCondition(PredicateOperator.GREATER_THAN_EQUALS, new long[]
            {
                propertyValue
            });
        }

        /// <summary>
        /// Adds the predicate <b>GREATER_THAN_OR_EQUAL_TO</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value for comparison.</param>
        /// <returns>
        /// The parent query builder for call chaining.
        /// </returns>
        public TParent GreaterThanOrEqualTo(int propertyValue)
        {
            return SetCondition(PredicateOperator.GREATER_THAN_EQUALS, new int[]
            {
                propertyValue
            });
        }

        /// <summary>
        /// Adds the predicate <b>IN</b> to the query for the given set of values.
        /// </summary>
        /// <param name="values">The list of values for comparison.</param>
        /// <returns>
        /// The parent query builder for call chaining.
        /// </returns>
        public TParent In(params string[] values)
        {
            return SetCondition(PredicateOperator.IN, values);
        }

        /// <summary>
        /// Adds the predicate <b>IN</b> to the query for the given set of values.
        /// </summary>
        /// <param name="values">The list of values for comparison.</param>
        /// <returns>
        /// The parent query builder for call chaining.
        /// </returns>
        public TParent In(params long[] values)
        {
            return SetCondition(PredicateOperator.IN, values);
        }

        /// <summary>
        /// Adds the predicate <b>LESS_THAN</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value for comparison.</param>
        /// <returns>
        /// The containing query builder.
        /// </returns>
        public TParent LessThan(long propertyValue)
        {
            return SetCondition(PredicateOperator.LESS_THAN, new long[]
            {
                propertyValue
            });
        }

        /// <summary>
        /// Adds the predicate <b>LESS_THAN</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value for comparison.</param>
        /// <returns>
        /// The containing query builder.
        /// </returns>
        public TParent LessThan(int propertyValue)
        {
            return SetCondition(PredicateOperator.LESS_THAN, new int[]
            {
                propertyValue
            });
        }

        /// <summary>
        /// Adds the predicate <b>LESS_THAN</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value for comparison.</param>
        /// <returns>
        /// The containing query builder.
        /// </returns>
        public TParent LessThan(double propertyValue)
        {
            return SetCondition(PredicateOperator.LESS_THAN, new double[]
            {
                propertyValue
            });
        }

        /// <summary>
        /// Adds the predicate <b>LESS_THAN_OR_EQUAL_TO</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The property value for comparison.</param>
        /// <returns>
        /// The parent query builder for call chaining.
        /// </returns>
        public TParent LessThanOrEqualTo(long propertyValue)
        {
            return SetCondition(PredicateOperator.LESS_THAN_EQUALS, new long[]
            {
                propertyValue
            });
        }

        /// <summary>
        /// Adds the predicate <b>NOT_IN</b> to the query for the given set of values.
        /// </summary>
        /// <param name="values">The list of values for comparison.</param>
        /// <returns>
        /// The parent query builder for call chaining.
        /// </returns>
        public TParent NotIn(params string[] values)
        {
            return SetCondition(PredicateOperator.NOT_IN, values);
        }

        /// <summary>
        /// Adds the predicate <b>NOT_IN</b> to the query for the given set of values.
        /// </summary>
        /// <param name="values">The list of values for comparison.</param>
        /// <returns>
        /// The parent query builder for call chaining.
        /// </returns>
        public TParent NotIn(params long[] values)
        {
            return SetCondition(PredicateOperator.NOT_IN, values);
        }

        /// <summary>
        /// Adds the predicate <b>STARTS_WITH</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The prefix of property value for comparison.</param>
        /// <returns>
        /// The parent query builder for call chaining.
        /// </returns>
        public TParent StartsWith(string propertyValue)
        {
            return SetCondition(PredicateOperator.STARTS_WITH, new string[]
            {
                propertyValue
            });
        }

        /// <summary>
        /// Adds the predicate <b>STARTS_WITH_IGNORE_CASE</b> to the query for the given value.
        /// </summary>
        /// <param name="propertyValue">The prefix of property value for comparison.</param>
        /// <returns>
        /// The parent query builder for call chaining.
        /// </returns>
        public TParent StartsWithIgnoreCase(string propertyValue)
        {
            return SetCondition(PredicateOperator.STARTS_WITH_IGNORE_CASE, new string[]
            {
                propertyValue
            });
        }
    }
}

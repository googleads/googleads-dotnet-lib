// Copyright 2018 Google LLC
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

using System.Linq;

using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.AdWords.v201806
{
    /// <summary>
    /// A generic selector to specify the type of information to return.
    /// </summary>
    public partial class Selector
    {
        /// <summary>
        /// Adds a new <see cref="OrderBy"/> condition to the ordering.
        /// </summary>
        /// <param name="orderBy">The new <see cref="OrderBy"/> condition.</param>
        internal void AddOrdering(OrderBy orderBy)
        {
            this.orderingField = CollectionUtilities.AddValueToArray(orderingField, orderBy);
        }

        /// <summary>
        /// Adds a new <see cref="Predicate"/> condition to the predicates.
        /// </summary>
        /// <param name="predicate">The new <see cref="Predicate"/> condition.</param>
        internal void AddPredicate(Predicate predicate)
        {
            this.predicates = CollectionUtilities.AddValueToArray(predicates, predicate);
        }

        /// <summary>
        /// Gets the SELECT clause for AWQL query.
        /// </summary>
        /// <returns>The SELECT clause for AWQL query.</returns>
        internal string GetSelectClause()
        {
            if (fields != null && fields.Length > 0)
            {
                return string.Format("SELECT {0}", string.Join<string>(", ", fields));
            }
            else
            {
                throw new System.ApplicationException("List of fields cannot be empty.");
            }
        }

        /// <summary>
        /// Gets the WHERE clause for AWQL query.
        /// </summary>
        /// <returns>The WHERE clause for AWQL query.</returns>
        internal string GetWhereClause()
        {
            if (predicates != null && predicates.Length > 0)
            {
                return string.Format("WHERE {0}", string.Join<Predicate>(" AND ", predicates));
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the ORDER BY clause for AWQL query.
        /// </summary>
        /// <returns>The ORDER BY clause for AWQL query.</returns>
        internal string GetOrderByClause()
        {
            if (ordering != null && ordering.Length > 0)
            {
                return string.Format("ORDER BY {0}", string.Join<OrderBy>(", ", ordering));
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the LIMIT clause for AWQL query.
        /// </summary>
        /// <returns>The LIMIT clause for AWQL query.</returns>
        internal string GetLimitClause()
        {
            if (this.paging != null)
            {
                return paging.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Converts this object into an AWQL query.
        /// </summary>
        /// <returns>The AWQL query.</returns>
        internal string ToQuery()
        {
            string[] parts = new string[]
            {
                GetSelectClause(),
                GetWhereClause(),
                GetOrderByClause(),
                GetLimitClause()
            }.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
            return string.Join<string>(" ", parts);
        }
    }
}

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

using System.Linq;

namespace Google.Api.Ads.AdWords.Util.Reports.v201806
{
    /// <summary>
    /// Class for building selector queries.
    /// </summary>
    public class SelectQueryBuilder : ISelectQueryBuilder<SelectQueryBuilder, SelectQuery>
    {
        /// <summary>
        /// The selector instance for maintaining internal state.
        /// </summary>
        private Selector selector = new Selector();

        /// <summary>
        /// Gets the selector.
        /// </summary>
        internal Selector Selector
        {
            get { return selector; }
        }

        /// <summary>
        /// Adds a SELECT clause to the query.
        /// </summary>
        /// <param name="fields">The fields to be selected.</param>
        /// <returns>
        /// The parent builder for call chaining.
        /// </returns>
        public SelectQueryBuilder Select(params string[] fields)
        {
            // Order doesn't matter, duplicate elements should be removed.
            selector.fields = fields.Distinct().ToArray();
            return this;
        }

        /// <summary>
        /// Adds a WHERE clause to the query.
        /// </summary>
        /// <param name="fieldName">Name of the field to filter on.</param>
        /// <returns>A builder for building the WHERE clause.</returns>
        public IWhereBuilder<SelectQueryBuilder> Where(string fieldName)
        {
            Predicate predicate = new Predicate()
            {
                field = fieldName
            };
            selector.AddPredicate(predicate);
            return new WhereBuilder<SelectQueryBuilder>(predicate, this);
        }

        /// <summary>
        /// Adds an ASC clause to the query.
        /// </summary>
        /// <param name="fieldName">Name of the field to sort by.</param>
        /// <returns>The parent builder for call chaining.</returns>
        public SelectQueryBuilder OrderByAscending(string fieldName)
        {
            return AddOrdering(fieldName, SortOrder.ASCENDING);
        }

        /// <summary>
        /// Adds a DESC clause to the query.
        /// </summary>
        /// <param name="fieldName">Name of the field to sort by.</param>
        /// <returns>The parent builder for call chaining.</returns>
        public SelectQueryBuilder OrderByDescending(string fieldName)
        {
            return AddOrdering(fieldName, SortOrder.DESCENDING);
        }

        /// <summary>
        /// Adds a LIMIT clause to the query.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="numberResults">The number of results.</param>
        /// <returns>The parent builder for call chaining.</returns>
        public SelectQueryBuilder Limit(uint startIndex, uint numberResults)
        {
            selector.paging = new Paging()
            {
                startIndex = (int) startIndex,
                numberResults = (int) numberResults
            };
            return this;
        }

        /// <summary>
        /// Adds an ordering condition to the selector.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns>The parent builder for call chaining.</returns>
        private SelectQueryBuilder AddOrdering(string fieldName, SortOrder sortOrder)
        {
            selector.AddOrdering(new OrderBy()
            {
                field = fieldName,
                sortOrder = sortOrder
            });
            return this;
        }

        /// <summary>
        /// Determines whether the query has next page.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns>True if there's a next page, false otherwise.</returns>
        public bool HasNextPage(Page page)
        {
            if (page is AdGroupBidLandscapePage)
            {
                return GetTotalLandscapePointsInPage(page as AdGroupBidLandscapePage) > 0;
            }
            else if (page is CriterionBidLandscapePage)
            {
                return GetTotalLandscapePointsInPage(page as CriterionBidLandscapePage) > 0;
            }
            else
            {
                return selector.paging.startIndex < page.totalNumEntries;
            }
        }

        /// <summary>
        /// Advances the query by a page.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns>The parent builder for call chaining.</returns>
        public SelectQueryBuilder NextPage(Page page)
        {
            if (page is AdGroupBidLandscapePage)
            {
                selector.paging.IncreaseOffsetBy(
                    GetTotalLandscapePointsInPage(page as AdGroupBidLandscapePage));
            }
            else if (page is CriterionBidLandscapePage)
            {
                selector.paging.IncreaseOffsetBy(
                    GetTotalLandscapePointsInPage(page as CriterionBidLandscapePage));
            }
            else
            {
                selector.paging.IncreaseOffset();
            }

            return this;
        }

        /// <summary>
        /// Gets the total landscape points in the ad group bid landscape page. If the page has a null
        /// <code>entries</code> array, returns <code>0</code>.
        /// </summary>
        /// <param name="page">The ad group bid landscape page.</param>
        /// <returns>The total landscape points.</returns>
        private int GetTotalLandscapePointsInPage(AdGroupBidLandscapePage page)
        {
            if (page.entries == null)
            {
                return 0;
            }

            int totalLandscapePointsInPage = 0;
            foreach (AdGroupBidLandscape adGroupBidLandscape in page.entries)
            {
                totalLandscapePointsInPage += adGroupBidLandscape.landscapePoints.Length;
            }

            return totalLandscapePointsInPage;
        }

        /// <summary>
        /// Gets the total landscape points in the criterion bid landscape page. If the page has a null
        /// <code>entries</code> array, returns <code>0</code>.
        /// </summary>
        /// <param name="page">The criterion bid landscape page.</param>
        /// <returns>The total landscape points.</returns>
        private int GetTotalLandscapePointsInPage(CriterionBidLandscapePage page)
        {
            if (page.entries == null)
            {
                return 0;
            }

            int totalLandscapePointsInPage = 0;
            foreach (CriterionBidLandscape criterionBidLandscape in page.entries)
            {
                totalLandscapePointsInPage += criterionBidLandscape.landscapePoints.Length;
            }

            return totalLandscapePointsInPage;
        }

        /// <summary>
        /// Builds the query.
        /// </summary>
        /// <returns>
        /// The query.
        /// </returns>
        public SelectQuery Build()
        {
            return new SelectQuery(this);
        }

        /// <summary>
        /// Adds a LIMIT clause to the query with default values.
        /// </summary>
        /// <returns>
        /// The parent builder for call chaining.
        /// </returns>
        public SelectQueryBuilder DefaultLimit()
        {
            selector.paging = Paging.Default;
            return this;
        }
    }
}

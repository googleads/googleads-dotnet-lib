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

using Google.Api.Ads.AdWords.v201806;

namespace Google.Api.Ads.AdWords.Util.Reports.v201806
{
    /// <summary>
    /// A select query, returned by <see cref="SelectQueryBuilder"/>
    /// </summary>
    public class SelectQuery : ISelectQuery<SelectQuery, SelectQueryBuilder, Page>
    {
        /// <summary>
        /// The builder associated with this select query.
        /// </summary>
        private SelectQueryBuilder builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectQuery"/> class.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public SelectQuery(SelectQueryBuilder builder)
        {
            this.builder = builder;
        }

        /// <summary>
        /// Gets the builder.
        /// </summary>
        public SelectQueryBuilder Builder
        {
            get { return builder; }
        }

        /// <summary>
        /// Adds a LIMIT clause to the query.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="numberResults">The number of results.</param>
        /// <returns>
        /// The parent builder for call chaining.
        /// </returns>
        public SelectQuery Limit(uint startIndex, uint numberResults)
        {
            builder.Limit(startIndex, numberResults);
            return this;
        }

        /// <summary>
        /// Determines whether the query has next page.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns>True if there's a next page, false otherwise.</returns>
        public bool HasNextPage(Page page)
        {
            Selector selector = this.Builder.Selector;
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
        public SelectQuery NextPage(Page page)
        {
            Selector selector = this.Builder.Selector;
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
                if (adGroupBidLandscape.landscapePoints != null)
                {
                    totalLandscapePointsInPage += adGroupBidLandscape.landscapePoints.Length;
                }
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
                if (criterionBidLandscape.landscapePoints != null)
                {
                    totalLandscapePointsInPage += criterionBidLandscape.landscapePoints.Length;
                }
            }

            return totalLandscapePointsInPage;
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return builder.Selector.ToQuery();
        }

        /// <summary>
        /// Adds a LIMIT clause to the query with default values.
        /// </summary>
        /// <returns>
        /// The parent builder for call chaining.
        /// </returns>
        public SelectQuery DefaultLimit()
        {
            builder.DefaultLimit();
            return this;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="SelectQuery"/> to <see cref="string"/>.
        /// </summary>
        /// <param name="x">The <see cref="SelectQuery"/> to convert.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator string(SelectQuery x)
        {
            return x.ToString();
        }
    }
}

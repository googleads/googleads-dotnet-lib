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

using Google.Api.Ads.AdWords.v201710;
using System.Linq;

namespace Google.Api.Ads.AdWords.Util.Reports.v201710 {

  /// <summary>
  /// Class for building selector queries.
  /// </summary>
  public class SelectQueryBuilder : ISelectQueryBuilder<SelectQueryBuilder> {

    /// <summary>
    /// The selector instance for maintaining internal state.
    /// </summary>
    private Selector selector = new Selector();

    /// <summary>
    /// Adds a SELECT clause to the query.
    /// </summary>
    /// <param name="fields">The fields to be selected.</param>
    /// <returns>
    /// The parent builder for call chaining.
    /// </returns>
    public SelectQueryBuilder Select(params string[] fields) {
      // Order doesn't matter, duplicate elements should be removed.
      selector.fields = fields.Distinct().ToArray();
      return this;
    }

    /// <summary>
    /// Adds a WHERE clause to the query.
    /// </summary>
    /// <param name="fieldName">Name of the field to filter on.</param>
    /// <returns>A builder for building the WHERE clause.</returns>
    public IWhereBuilder<SelectQueryBuilder> Where(string fieldName) {
      Predicate predicate = new Predicate() {
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
    public SelectQueryBuilder OrderByAscending(string fieldName) {
      return AddOrdering(fieldName, SortOrder.ASCENDING);
    }

    /// <summary>
    /// Adds a DESC clause to the query.
    /// </summary>
    /// <param name="fieldName">Name of the field to sort by.</param>
    /// <returns>The parent builder for call chaining.</returns>
    public SelectQueryBuilder OrderByDescending(string fieldName) {
      return AddOrdering(fieldName, SortOrder.DESCENDING);
    }

    /// <summary>
    /// Adds a LIMIT clause to the query.
    /// </summary>
    /// <param name="startIndex">The start index.</param>
    /// <param name="numberResults">The number of results.</param>
    /// <returns>The parent builder for call chaining.</returns>
    public SelectQueryBuilder Limit(uint startIndex, uint numberResults) {
      selector.paging = new Paging() {
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
    private SelectQueryBuilder AddOrdering(string fieldName, SortOrder sortOrder) {
      selector.AddOrdering(new OrderBy() {
        field = fieldName,
        sortOrder = sortOrder
      });
      return this;
    }

    /// <summary>
    /// Builds the query.
    /// </summary>
    /// <returns>
    /// The query.
    /// </returns>
    public string Build() {
      return selector.ToQuery();
    }
  }
}

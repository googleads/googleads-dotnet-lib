// Copyright 2011, Google Inc. All Rights Reserved.
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

// Author: api.anash@gmail.com (Anash P. Oommen)

using Google.Api.Ads.Dfp.v201101;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.Dfp.Util.v201101 {
  /// <summary>
  /// A utility class that allows you to build a DFP Statement from query
  /// and parameters.
  /// </summary>
  public class StatementBuilder {
    /// <summary>
    /// The DFP statement.
    /// </summary>
    Statement statement;

    /// <summary>
    /// The list of query parameters.
    /// </summary>
    List<String_ValueMapEntry> valueEntries;

    /// <summary>
    /// Parameterized public constructor.
    /// </summary>
    /// <param name="query">The query string.</param>
    public StatementBuilder(string query) {
      statement = new Statement();
      statement.query = query;
      valueEntries = new List<String_ValueMapEntry>();
    }

    /// <summary>
    /// Gets or sets the statement query.
    /// </summary>
    public string Query {
      get {
        return statement.query;
      } set {
        statement.query = value;
      }
    }

    /// <summary>
    /// Adds a new string value to the list of query parameters.
    /// </summary>
    /// <param name="key">The parameter name.</param>
    /// <param name="value">The parameter value.</param>
    /// <returns>The statement builder, for chaining method calls.</returns>
    public StatementBuilder AddValue(string key, string value) {
      TextValue queryValue = new TextValue();
      queryValue.value = value;
      return AddValue(key, queryValue);
    }

    /// <summary>
    /// Adds a new boolean value to the list of query parameters.
    /// </summary>
    /// <param name="key">The parameter name.</param>
    /// <param name="value">The parameter value.</param>
    /// <returns>The statement builder, for chaining method calls.</returns>
    public StatementBuilder AddValue(string key, bool value) {
      BooleanValue queryValue = new BooleanValue();
      queryValue.value = value;
      return AddValue(key, queryValue);
    }

    /// <summary>
    /// Adds a new decimal value to the list of query parameters.
    /// </summary>
    /// <param name="key">The parameter name.</param>
    /// <param name="value">The parameter value.</param>
    /// <returns>The statement builder, for chaining method calls.</returns>
    public StatementBuilder AddValue(string key, decimal value) {
      NumberValue queryValue = new NumberValue();
      queryValue.value = value.ToString();
      return AddValue(key, queryValue);
    }

    /// <summary>
    /// Adds a new value to the list of query parameters.
    /// </summary>
    /// <param name="key">The parameter name.</param>
    /// <param name="value">The parameter value.</param>
    /// <returns>The statement builder, for chaining method calls.</returns>
    private StatementBuilder AddValue(string key, Value value) {
      String_ValueMapEntry queryValue = new String_ValueMapEntry();
      queryValue.key = key;
      queryValue.value = value;
      valueEntries.Add(queryValue);
      return this;
    }

    /// <summary>
    /// Gets the statement built by this builder.
    /// </summary>
    /// <returns>The statement.</returns>
    public Statement ToStatement() {
      statement.values = valueEntries.ToArray();
      return statement;
    }
  }
}

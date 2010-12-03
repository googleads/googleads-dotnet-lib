// Copyright 2010, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Dfp.v201010;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.Dfp.Util.v201010 {
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
    List<String_ParamMapEntry> paramEntries;

    /// <summary>
    /// Parameterized public constructor.
    /// </summary>
    /// <param name="query">The query string.</param>
    public StatementBuilder(string query) {
      statement = new Statement();
      statement.query = query;
      paramEntries = new List<String_ParamMapEntry>();
    }

    /// <summary>
    /// Adds a new string parameter to the list of query parameters.
    /// </summary>
    /// <param name="key">The parameter name.</param>
    /// <param name="value">The parameter value.</param>
    /// <returns>The statement builder, for chaining method calls.</returns>
    public StatementBuilder AddParam(string key, string value) {
      StringParam param = new StringParam();
      param.value = value;
      return AddParam(key, param);
    }

    /// <summary>
    /// Adds a new long parameter to the list of query parameters.
    /// </summary>
    /// <param name="key">The parameter name.</param>
    /// <param name="value">The parameter value.</param>
    /// <returns>The statement builder, for chaining method calls.</returns>
    public StatementBuilder AddParam(string key, long value) {
      LongParam param = new LongParam();
      param.value = value;
      return AddParam(key, param);
    }

    /// <summary>
    /// Adds a new double parameter to the list of query parameters.
    /// </summary>
    /// <param name="key">The parameter name.</param>
    /// <param name="value">The parameter value.</param>
    /// <returns>The statement builder, for chaining method calls.</returns>
    public StatementBuilder AddParam(string key, double value) {
      DoubleParam param = new DoubleParam();
      param.value = value;
      return AddParam(key, param);
    }

    /// <summary>
    /// Adds a new parameter to the list of query parameters.
    /// </summary>
    /// <param name="key">The parameter name.</param>
    /// <param name="param">The parameter value.</param>
    /// <returns>The statement builder, for chaining method calls.</returns>
    private StatementBuilder AddParam(string key, Param param) {
      String_ParamMapEntry paramEntry = new String_ParamMapEntry();
      paramEntry.key = key;
      paramEntry.value = param;
      paramEntries.Add(paramEntry);
      return this;
    }

    /// <summary>
    /// Gets the statement built by this builder.
    /// </summary>
    /// <returns>The statement.</returns>
    public Statement ToStatement() {
      statement.@params = paramEntries.ToArray();
      return statement;
    }
  }
}

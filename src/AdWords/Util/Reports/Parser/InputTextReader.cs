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

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.AdWords.Util.Reports
{
    /// <summary>
    /// An Interface for use by the report parsing classes.
    /// </summary>
    public interface InputTextReader : IDisposable
    {
        /// <summary>
        /// Advances the InputTextReader to the next row in the report text.
        /// </summary>
        /// <returns>True if moved to next row, and false if no rows remain in report.</returns>
        bool Read();

        /// <summary>
        /// Returns the column names with their values at the current row.
        /// </summary>
        /// <returns>The column names and corresponding values.</returns>
        IEnumerable<ColumnValuePair> GetAttributes();
    }
}

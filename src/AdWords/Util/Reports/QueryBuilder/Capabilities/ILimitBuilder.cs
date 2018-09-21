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
    /// Builder interface for LIMIT BY capability.
    /// </summary>
    /// <typeparam name="TParent">The parent builder type.</typeparam>
    internal interface ILimitBuilder<TParent>
    {
        /// <summary>
        /// Adds a LIMIT clause to the query.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="numberResults">The number of results.</param>
        /// <returns>The parent builder for call chaining.</returns>
        TParent Limit(uint startIndex, uint numberResults);

        /// <summary>
        /// Adds a LIMIT clause to the query with default values.
        /// </summary>
        /// <returns>The parent builder for call chaining.</returns>
        TParent DefaultLimit();
    }
}

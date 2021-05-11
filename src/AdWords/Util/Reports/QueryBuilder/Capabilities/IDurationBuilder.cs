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

using System;

namespace Google.Api.Ads.AdWords.Util.Reports
{
    /// <summary>
    /// Builder interface for DURING capability.
    /// </summary>
    /// <typeparam name="TParent">The parent builder type.</typeparam>
    /// <typeparam name="TDateRangeType">The enum type for predefined date ranges.</typeparam>
    internal interface IDurationBuilder<TParent, TDateRangeType>
    {
        /// <summary>
        /// Adds a DURING clause to the query.
        /// </summary>
        /// <param name="dateRange">The predefined date range.</param>
        /// <returns>The parent builder for call chaining.</returns>
        TParent During(string dateRange);

        /// <summary>
        /// Adds a DURING clause to the query.
        /// </summary>
        /// <param name="dateRangeType">The predefined date range.</param>
        /// <returns>The parent builder for call chaining.</returns>
        TParent During(TDateRangeType dateRangeType);

        /// <summary>
        /// Adds a DURING clause to the query.
        /// </summary>
        /// <param name="minDate">The minimum date in yyyyMMdd format.</param>
        /// <param name="maxDate">The maximum date in yyyyMMdd format.</param>
        /// <returns>The parent builder for call chaining.</returns>
        TParent During(string minDate, string maxDate);

        /// <summary>
        /// Adds a DURING clause to the query.
        /// </summary>
        /// <param name="minDate">The minimum date.</param>
        /// <param name="maxDate">The maximum date.</param>
        /// <returns>The parent builder for call chaining.</returns>
        TParent During(DateTime minDate, DateTime maxDate);
    }
}

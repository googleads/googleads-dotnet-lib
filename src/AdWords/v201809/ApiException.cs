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

using System.Collections.Generic;
using System.Linq;

namespace Google.Api.Ads.AdWords.v201809
{
    /// <summary>
    /// Exception class for holding a list of service errors.
    /// </summary>
    public partial class ApiException
    {
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (this.errors == null || this.errors.Length == 0)
            {
                return base.ToString();
            }
            else
            {
                return string.Join<ApiError>("\n", errors);
            }
        }

        /// <summary>
        /// Gets all errors of a given type.
        /// </summary>
        /// <typeparam name="T">The error type to get.</typeparam>
        /// <returns>A list of errors of specified type.</returns>
        public List<T> GetAllErrorsByType<T>() where T : ApiError
        {
            return new List<T>(this.errors.Where(x => x is T).Cast<T>());
        }
    }
}

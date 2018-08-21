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

using Google.Api.Ads.AdManager.Lib;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Google.Api.Ads.AdManager.Tests
{
    /// <summary>
    /// Base class for all test suites.
    /// </summary>
    public class BaseTests
    {
        /// <summary>
        /// The AdManagerUser to be used for tests.
        /// </summary>
        protected AdManagerUser user = new AdManagerUser();

        /// <summary>
        /// Default public constructor.
        /// </summary>
        /// <remarks>The constructor adds a 200 ms delay between running individual
        /// tests so that we don't hit the server frequently and cause quota errors.
        /// </remarks>
        public BaseTests()
        {
            Thread.Sleep(200);
        }
    }
}

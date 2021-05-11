// Copyright 2012, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Common.Tests;

namespace Google.Api.Ads.AdWords.Tests
{
    /// <summary>
    /// Contains mock data for a given code example.
    /// </summary>
    public class ExamplesMockData
    {
        /// <summary>
        /// List of HTTP messages to be mocked.
        /// </summary>
        private HttpMessage[] mockMessages = null;

        /// <summary>
        /// Expected output from the code example.
        /// </summary>
        private string expectedOutput = "";

        /// <summary>
        /// Overloaded constructor.
        /// </summary>
        /// <param name="mockMessages">The list of HTTP messages to be mocked.
        /// </param>
        /// <param name="expectedOutput">Expected output from the code example.
        /// </param>
        public ExamplesMockData(HttpMessage[] mockMessages, string expectedOutput)
        {
            this.mockMessages = mockMessages;
            this.expectedOutput = expectedOutput;
        }

        /// <summary>
        /// Gets the mock messages.
        /// </summary>
        public HttpMessage[] MockMessages
        {
            get { return mockMessages; }
        }

        /// <summary>
        /// Gets the expected output.
        /// </summary>
        public string ExpectedOutput
        {
            get { return expectedOutput; }
        }
    }
}

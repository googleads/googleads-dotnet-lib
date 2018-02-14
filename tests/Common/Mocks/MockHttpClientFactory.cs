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

using Google.Apis.Http;
using System.Net.Http;

namespace Google.Api.Ads.Common.Tests.Mocks {
  /// <summary>
  /// Mock HttpClientFactory for testing purposes.
  /// </summary>
  class MockHttpClientFactory : HttpClientFactory {

    /// <summary>
    /// Gets or sets the message handler for testing purposes.
    /// </summary>
    public MockHttpMessageHandler messageHandler { get; set; }

    protected override HttpMessageHandler CreateHandler(CreateHttpClientArgs args) {
      return messageHandler;
    }
  }
}

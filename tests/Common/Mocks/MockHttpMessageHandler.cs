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
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Google.Api.Ads.Common.Tests.Mocks {
  /// <summary>
  /// Mock HttpMessageHandler for testing purposes.
  /// </summary>
  class MockHttpMessageHandler : HttpMessageHandler {
    internal String LastRequest { get; private set; }
    internal String Response { get; set; }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken) {
      FormUrlEncodedContent content = request.Content as FormUrlEncodedContent;
      LastRequest = request.Content.ReadAsStringAsync().Result;
      var taskCompletion = new TaskCompletionSource<HttpResponseMessage>();
      taskCompletion.SetResult(new HttpResponseMessage() {
        Content = new StringContent(Response)
      });
      return taskCompletion.Task;
    }
  }
}

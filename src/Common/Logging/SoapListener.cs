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

using Google.Api.Ads.Common.Lib;

using System.Xml;

namespace Google.Api.Ads.Common.Logging
{
    /// <summary>
    /// Listens to SOAP messages sent and received by this library.
    /// </summary>
    public interface SoapListener : Configurable
    {
        /// <summary>
        /// Initializes the listener for handling an API call.
        /// </summary>
        void InitForCall();

        /// <summary>
        /// Handles the request and response for a message.
        /// </summary>
        /// <param name="requestInfo">Request info.</param>
        /// <param name="responseInfo">Response info.</param>
        void HandleMessage(RequestInfo requestInfo, ResponseInfo responseInfo);

        /// <summary>
        /// Cleans up any resources after an API call.
        /// </summary>
        void CleanupAfterCall();
    }
}

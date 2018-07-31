// Copyright 2017, Google Inc. All Rights Reserved.
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
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.ServiceModel;

namespace Google.Api.Ads.Common.Lib
{
    /// <summary>
    /// OAuth2 client message inspector that adds encoding HTTP headers.
    /// </summary>
    public class GzipHeaderInspector : IClientMessageInspector
    {
        internal const string ENCODING_HEADER = "Accept-Encoding";

        internal readonly string[] ACCEPT_VALUES =
        {
            "gzip",
            "deflate"
        };

        /// <summary>
        /// Adds an Accept-Encoding header to outbound requests.
        /// </summary>
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            object httpProp;
            if (!request.Properties.TryGetValue(HttpRequestMessageProperty.Name, out httpProp))
            {
                httpProp = new HttpRequestMessageProperty();
                request.Properties.Add(HttpRequestMessageProperty.Name, httpProp);
            }

            ((HttpRequestMessageProperty) httpProp).Headers.Add(ENCODING_HEADER,
                string.Join(", ", ACCEPT_VALUES));
            return null;
        }

        /// <summary>
        /// A no-op for this inspector.
        /// </summary>
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
        }
    }
}

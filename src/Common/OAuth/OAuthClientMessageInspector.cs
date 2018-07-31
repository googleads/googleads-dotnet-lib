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

using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.ServiceModel;

using Google.Api.Ads.Common.Lib;

namespace Google.Api.Ads.Common.OAuth
{
    /// <summary>
    /// OAuth2 client message inspector that adds authorization HTTP headers.
    /// </summary>
    public class OAuthClientMessageInspector : IClientMessageInspector
    {
        internal const string AUTHORIZATION_HEADER = "Authorization";

        AdsOAuthProvider oauthProvider;

        /// <summary>
        /// Initializes a new instance of the OAuth2ClientMessageInspector class.
        /// </summary>
        public OAuthClientMessageInspector(AdsOAuthProvider oauthProvider)
        {
            this.oauthProvider = oauthProvider;
        }

        /// <summary>
        /// Adds an OAuth2 authorization header to outbound requests.
        /// </summary>
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            if (this.oauthProvider == null)
            {
                throw new AdsOAuthException("OAuth provider cannot be null");
            }

            object httpProp;
            if (!request.Properties.TryGetValue(HttpRequestMessageProperty.Name, out httpProp))
            {
                httpProp = new HttpRequestMessageProperty();
                request.Properties.Add(HttpRequestMessageProperty.Name, httpProp);
            }

            ((HttpRequestMessageProperty) httpProp).Headers.Add(AUTHORIZATION_HEADER,
                this.oauthProvider.GetAuthHeader());
            return null;
        }

        /// <summary>
        /// Performs any operations after receiving the SOAP response.
        /// </summary>
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
        }
    }
}

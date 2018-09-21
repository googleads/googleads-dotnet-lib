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

using Google.Api.Ads.AdWords.Headers;
using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.Common.Logging;

using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Google.Api.Ads.AdWords.Lib
{
    /// <summary>
    /// Base class for AdWords API services.
    /// </summary>
    public class AdWordsSoapClient<TChannel> : AdsSoapClient<TChannel> where TChannel : class
    {
        /// <summary>
        /// Gets this service's SOAP header inspector if it exists. Returns null otherwise.
        /// </summary>
        internal AdWordsSoapHeaderInspector HeaderInspector
        {
            get
            {
#if NET452
        AdsServiceInspectorBehavior behavior = (AdsServiceInspectorBehavior)
            this.Endpoint.Behaviors[typeof(AdsServiceInspectorBehavior)];
#else
                AdsServiceInspectorBehavior behavior =
                    (AdsServiceInspectorBehavior) this.Endpoint.EndpointBehaviors[
                        typeof(AdsServiceInspectorBehavior)];
#endif
                if (behavior != null)
                {
                    return behavior.GetInspector<AdWordsSoapHeaderInspector>();
                }

                return null;
            }
        }

        /// <summary>
        /// Gets or sets the request header.
        /// </summary>
        /// <value>The request header.</value>
        public RequestHeader RequestHeader
        {
            get { return HeaderInspector != null ? HeaderInspector.RequestHeader : null; }
            set
            {
                if (HeaderInspector != null)
                {
                    HeaderInspector.RequestHeader = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the response header.
        /// </summary>
        /// <value>The response header.</value>
        public ResponseHeader ResponseHeader
        {
            get { return HeaderInspector != null ? HeaderInspector.ResponseHeader : null; }
            set
            {
                if (HeaderInspector != null)
                {
                    HeaderInspector.ResponseHeader = value;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the AdWordsSoapClient class.
        /// </summary>
        /// <param name="endpointConfigurationName">
        /// The name of the endpoint in the application configuration file.
        /// </param>
        public AdWordsSoapClient(string endpointConfigurationName) : base(endpointConfigurationName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AdWordsSoapClient class.
        /// </summary>
        /// <param name="endpointConfigurationName">
        /// The name of the endpoint in the application configuration file.
        /// </param>
        /// <param name="remoteAddress">Remote address of the service.</param>
        public AdWordsSoapClient(string endpointConfigurationName, EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AdWordsSoapClient class.
        /// </summary>
        /// <param name="binding">The binding with which to make calls to the service.</param>
        /// <param name="remoteAddress">Remote address of the service.</param>
        public AdWordsSoapClient(Binding binding, EndpointAddress remoteAddress) : base(binding,
            remoteAddress)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AdWordsSoapClient class.
        /// </summary>
        /// <param name="endpointConfigurationName">
        /// The name of the endpoint in the application configuration file.
        /// </param>
        /// <param name="remoteAddress">Remote address of the service.</param>
        public AdWordsSoapClient(string endpointConfigurationName, string remoteAddress) : base(
            endpointConfigurationName, remoteAddress)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AdWordsSoapClient class.
        /// </summary>
        public AdWordsSoapClient() : base()
        {
        }

        /// <summary>
        /// Cleans up the service after MakeApiCall.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameters">The method parameters.</param>
        protected override void CleanupAfterCall(string methodName, object[] parameters)
        {
            AdsFeatureUsageRegistry.Instance.Clear();
            base.CleanupAfterCall(methodName, parameters);
        }
    }
}

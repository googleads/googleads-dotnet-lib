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

using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.Common.Logging;
using Google.Api.Ads.Common.OAuth;
using Google.Api.Ads.AdManager.Headers;

using System;
using System.Globalization;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;

namespace Google.Api.Ads.AdManager.Lib
{
    /// <summary>
    /// The factory class for all Ad Manager API services.
    /// </summary>
    public class AdManagerServiceFactory : ServiceFactory
    {
        private static readonly string ENDPOINT_TEMPLATE = "{0}apis/ads/publisher/{1}/{2}";

        /// <summary>
        /// The request header to be used with Ad Manager API services.
        /// </summary>
        private RequestHeader requestHeader;

        /// <summary>
        /// Default public constructor.
        /// </summary>
        public AdManagerServiceFactory()
        {
        }

        /// <summary>
        /// Create a service object.
        /// </summary>
        /// <param name="signature">Signature of the service being created.</param>
        /// <param name="user">The user for which the service is being created.
        /// <param name="serverUrl">The server to which the API calls should be
        /// made.</param>
        /// </param>
        /// <returns>An object of the desired service type.</returns>
        public override AdsClient CreateService(ServiceSignature signature, AdsUser user,
            Uri serverUrl)
        {
            AdManagerAppConfig adManagerConfig = (AdManagerAppConfig) Config;
            if (serverUrl == null)
            {
                serverUrl = new Uri(adManagerConfig.AdManagerApiServer);
            }

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            CheckServicePreconditions(signature);

            AdManagerServiceSignature adManagerSignature = signature as AdManagerServiceSignature;
            EndpointAddress endpoint = new EndpointAddress(string.Format(ENDPOINT_TEMPLATE,
                serverUrl, adManagerSignature.Version, adManagerSignature.ServiceName));

            // Create the binding for the service
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.Security.Mode = BasicHttpSecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            binding.MaxReceivedMessageSize = int.MaxValue;
            binding.TextEncoding = Encoding.UTF8;

            AdsClient service = (AdsClient) Activator.CreateInstance(adManagerSignature.ServiceType,
                new object[]
                {
                    binding,
                    endpoint
                });

            ServiceEndpoint serviceEndpoint =
                (ServiceEndpoint) service.GetType().GetProperty("Endpoint").GetValue(service, null);

            AdsServiceInspectorBehavior inspectorBehavior = new AdsServiceInspectorBehavior();
            inspectorBehavior.Add(new OAuthClientMessageInspector(user.OAuthProvider));

            RequestHeader clonedHeader = (RequestHeader) requestHeader.Clone();
            clonedHeader.Version = adManagerSignature.Version;
            inspectorBehavior.Add(new AdManagerSoapHeaderInspector()
            {
                RequestHeader = clonedHeader,
                Config = adManagerConfig
            });
            inspectorBehavior.Add(new SoapListenerInspector(user, adManagerSignature.ServiceName));
            inspectorBehavior.Add(new SoapFaultInspector<AdManagerApiException>()
            {
                ErrorType =
                    adManagerSignature.ServiceType.Assembly.GetType(
                        adManagerSignature.ServiceType.Namespace + ".ApiException"),
            });
#if NET452
      serviceEndpoint.Behaviors.Add(inspectorBehavior);
#else
            serviceEndpoint.EndpointBehaviors.Add(inspectorBehavior);
#endif

            if (adManagerConfig.Proxy != null)
            {
                service.Proxy = adManagerConfig.Proxy;
            }

            service.EnableDecompression = adManagerConfig.EnableGzipCompression;
            service.Timeout = adManagerConfig.Timeout;
            service.UserAgent = adManagerConfig.GetUserAgent();

            service.Signature = signature;
            service.User = user;
            return service;
        }

        /// <summary>
        /// Reads the headers from App.config.
        /// </summary>
        /// <param name="config">The configuration class.</param>
        protected override void ReadHeadersFromConfig(AppConfig config)
        {
            AdManagerAppConfig adManagerConfig = (AdManagerAppConfig) config;

            this.requestHeader = new RequestHeader();
            this.requestHeader.networkCode = adManagerConfig.NetworkCode;
            this.requestHeader.applicationName = adManagerConfig.GetUserAgent();
        }

        /// <summary>
        /// Checks preconditions of the service signature and throws and exception if the service
        /// cannot be generated.
        /// </summary>
        /// <param name="signature">the service signature for generating the service</param>
        protected override void CheckServicePreconditions(ServiceSignature signature)
        {
            if (signature == null)
            {
                throw new ArgumentNullException("signature");
            }

            if (!(signature is AdManagerServiceSignature))
            {
                throw new InvalidCastException(string.Format(CultureInfo.InvariantCulture,
                    AdManagerErrorMessages.SignatureIsOfWrongType,
                    typeof(AdManagerServiceSignature)));
            }
        }
    }
}

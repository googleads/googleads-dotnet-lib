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

using System;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Google.Api.Ads.Common.Lib
{
    /// <summary>
    /// Base class for all SOAP services supported by this library.
    /// </summary>
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public abstract class AdsSoapClient<TChannel> : ClientBase<TChannel>, AdsClient, IDisposable
        where TChannel : class
    {
        /// <summary>
        /// The timeout for the request in milliseconds.
        /// </summary>
        public int Timeout
        {
            get { return this.Endpoint.Binding.SendTimeout.Milliseconds; }
            set
            {
                long ticks = value * TimeSpan.TicksPerMillisecond;
                this.Endpoint.Binding.SendTimeout = new TimeSpan(ticks);
                this.Endpoint.Binding.ReceiveTimeout = new TimeSpan(ticks);
            }
        }

        /// <summary>
        /// The url endpoint for the service.
        /// </summary>
        public string Url
        {
            get { return this.Endpoint.Address.Uri.ToString(); }
            set { this.Endpoint.Address = new EndpointAddress(value); }
        }

        /// <summary>
        /// Gets or sets whether gzip compression is enabled.
        /// </summary>
        public bool EnableDecompression
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
                return behavior != null && behavior.GetInspector<GzipHeaderInspector>() != null;
            }
            set
            {
#if NET452
                AdsServiceInspectorBehavior behavior = (AdsServiceInspectorBehavior)
                    this.Endpoint.Behaviors[typeof(AdsServiceInspectorBehavior)];
#else
                AdsServiceInspectorBehavior behavior =
                    (AdsServiceInspectorBehavior) this.Endpoint.EndpointBehaviors[
                        typeof(AdsServiceInspectorBehavior)];
#endif
                if (value && behavior.GetInspector<GzipHeaderInspector>() == null)
                {
                    behavior.Add(new GzipHeaderInspector());
                }
                else if (!value && behavior.GetInspector<GzipHeaderInspector>() != null)
                {
                    behavior.Remove<GzipHeaderInspector>();
                }
            }
        }

        /// <summary>
        /// Gets or sets proxy information for making a service request through
        /// a firewall.
        /// </summary>
        public IWebProxy Proxy
        {
            get
            {
                BasicHttpBinding binding = (BasicHttpBinding) this.Endpoint.Binding;
                return binding.UseDefaultWebProxy == true ? WebRequest.DefaultWebProxy : null;
            }
            set
            {
                BasicHttpBinding binding = (BasicHttpBinding) this.Endpoint.Binding;
                if (value == null)
                {
                    binding.UseDefaultWebProxy = false;
                }
                else
                {
                    binding.UseDefaultWebProxy = true;
                    WebRequest.DefaultWebProxy = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the value for the user agent header that is sent with each
        /// request.
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// The user that created this service instance.
        /// </summary>
        public AdsUser User { get; set; }

        /// <summary>
        /// The signature for this service.
        /// </summary>
        public ServiceSignature Signature { get; set; }

        /// <summary>
        /// Initializes a new instance of the AdsSoapClient class.
        /// </summary>
        /// <param name="endpointConfigurationName">
        /// The name of the endpoint in the application configuration file.
        /// </param>
        public AdsSoapClient(string endpointConfigurationName) : base(endpointConfigurationName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AdsSoapClient class.
        /// </summary>
        /// <param name="endpointConfigurationName">
        /// The name of the endpoint in the application configuration file.
        /// </param>
        /// <param name="remoteAddress">Remote address of the service.</param>
        public AdsSoapClient(string endpointConfigurationName, EndpointAddress remoteAddress)
            : base(endpointConfigurationName, remoteAddress)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AdsSoapClient class.
        /// </summary>
        /// <param name="binding">The binding with which to make calls to the service.</param>
        /// <param name="remoteAddress">Remote address of the service.</param>
        public AdsSoapClient(Binding binding, EndpointAddress remoteAddress)
            : base(binding, remoteAddress)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AdsSoapClient class.
        /// </summary>
        /// <param name="endpointConfigurationName">
        /// The name of the endpoint in the application configuration file.
        /// </param>
        /// <param name="remoteAddress">Remote address of the service.</param>
        public AdsSoapClient(string endpointConfigurationName, string remoteAddress)
            : base(endpointConfigurationName, remoteAddress)
        {
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public AdsSoapClient() : base()
        {
        }

        /// <summary>
        /// Initializes the service before MakeApiCall.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameters">The method parameters.</param>
        protected virtual void InitForCall(string methodName, object[] parameters)
        {
            ContextStore.AddKey("SoapService", this);
            ContextStore.AddKey("SoapMethod", methodName);
            User.InitListeners();
        }

        /// <summary>
        /// Cleans up the service after MakeApiCall.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameters">The method parameters.</param>
        protected virtual void CleanupAfterCall(string methodName, object[] parameters)
        {
            User.CleanupListeners();
            ContextStore.RemoveKey("SoapService");
            ContextStore.RemoveKey("SoapMethod");
        }

        /// <summary>
        /// Disposes of this AdsSoapClient.
        /// </summary>
        public void Dispose()
        {
            ((ICommunicationObject) this).Close();
        }
    }
}

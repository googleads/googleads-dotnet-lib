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

using System.Collections.Generic;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;

namespace Google.Api.Ads.Common.Lib
{
    /// <summary>
    /// Adds inspectors for authentication, SOAP headers, and logging to Ads API services.
    /// </summary>
    public class AdsServiceInspectorBehavior : IEndpointBehavior
    {
        KeyedByTypeCollection<IClientMessageInspector> inspectors;

        /// <summary>
        /// Initializes a new instance of the AdsServiceInspectorBehavior class.
        /// </summary>
        public AdsServiceInspectorBehavior()
        {
            this.inspectors = new KeyedByTypeCollection<IClientMessageInspector>();
        }

        /// <summary>
        /// Adds the inspector to be applied to the service.
        /// </summary>
        /// <param name="inspector">Inspector.</param>
        public void Add(IClientMessageInspector inspector)
        {
            inspectors.Add(inspector);
        }


        /// <summary>
        /// Removes inspector of type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Remove<T>()
        {
            inspectors.Remove<T>();
        }

        /// <summary>
        /// Gets the inspector of the specified Type, if it exists.
        /// Returns null otherwise.
        /// </summary>
        /// <typeparam name="T">The type of inspector to retrieve.</typeparam>
        /// <returns></returns>
        public T GetInspector<T>()
        {
            return inspectors.Find<T>();
        }

        /// <summary>
        /// Adds the binding parameters.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        /// <param name="bindingParameters">Binding parameters.</param>
        public void AddBindingParameters(ServiceEndpoint endpoint,
            BindingParameterCollection bindingParameters)
        {
        }

        /// <summary>
        /// Applies the client behavior.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        /// <param name="clientRuntime">Client runtime.</param>
        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            foreach (IClientMessageInspector inspector in inspectors)
            {
#if NET452
                clientRuntime.MessageInspectors.Add(inspector);
#else
                clientRuntime.ClientMessageInspectors.Add(inspector);
#endif
            }
        }

        /// <summary>
        /// Applies the dispatch behavior.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        /// <param name="endpointDispatcher">Endpoint dispatcher.</param>
        public void ApplyDispatchBehavior(ServiceEndpoint endpoint,
            EndpointDispatcher endpointDispatcher)
        {
        }

        /// <summary>
        /// Validate the specified endpoint.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }
}

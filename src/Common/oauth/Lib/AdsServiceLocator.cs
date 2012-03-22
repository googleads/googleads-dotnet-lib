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

// Author: api.anash@gmail.com (Anash P. Oommen)

using Microsoft.Practices.ServiceLocation;

using OAuth.Net.Common;
using OAuth.Net.Components;
using OAuth.Net.Consumer.Components;
using OAuth.Net.Consumer;

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Google.Api.Ads.Common.OAuth.Lib {
  /// <summary>
  /// Provides a default implementation of Microsoft Common Service Locator, to
  /// be used with OAuth.Net library.
  /// </summary>
  /// <remarks>OAuth.net library uses Microsoft Common Service Locator library
  /// (http://commonservicelocator.codeplex.com/) for dependency injection. The
  /// default library implementation uses Castle Project, which adds another
  /// external library dependency on the Ads client libraries. To avoid this,
  /// we are providing a simple replacement to suit just the need of OAuth.Net
  /// library.</remarks>
  public class AdsServiceLocator : IServiceLocator {
    /// <summary>
    /// A map to store the registered types.
    /// </summary>
    Dictionary<Type, Dictionary<string, AdsServiceComponent>> serviceLookup =
        new Dictionary<Type, Dictionary<string, AdsServiceComponent>>();

    /// <summary>
    /// Choose whether to use an in-memory store or session store for storing
    /// OAuth state.
    /// </summary>
    Boolean useMemoryStore;

    /// <summary>
    /// Gets or sets whether to use an in-memory store or session store for
    /// storing OAuth state.
    /// </summary>
    public Boolean UseMemoryStore {
      get {
        return useMemoryStore;
      }
      set {
        useMemoryStore = value;
        Dictionary<string, AdsServiceComponent> serviceMap =
            serviceLookup[typeof(IRequestStateStore)];
        if (value) {
          serviceMap["state-store-inmemory"].IsDefault = true;
          serviceMap["state-store-session"].IsDefault = false;
        } else {
          serviceMap["state-store-inmemory"].IsDefault = false;
          serviceMap["state-store-session"].IsDefault = true;
        }
      }
    }

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public AdsServiceLocator() {
      RegisterDefaultServices();
      this.UseMemoryStore = false;
    }

    /// <summary>
    /// Registers the default set of services provided by this service locator.
    /// </summary>
    /// <remarks>These are the types required by OAuth.Net library.</remarks>
    private void RegisterDefaultServices() {
      RegisterService(new AdsServiceComponent("signing.provider:HMAC-SHA1",
          typeof(ISigningProvider), typeof(HmacSha1SigningProvider)));

      RegisterService(new AdsServiceComponent("state-store-session", typeof(IRequestStateStore),
        typeof(SessionRequestStateStore)));
      RegisterService(new AdsServiceComponent("state-store-inmemory", typeof(IRequestStateStore),
        typeof(InMemoryRequestStateStore)));

      RegisterService(new AdsServiceComponent("nonce.provider", typeof(INonceProvider),
          typeof(GuidNonceProvider)));
    }

    /// <summary>
    /// Registers a service component with this locator.
    /// </summary>
    /// <param name="component">The component.</param>
    public void RegisterService(AdsServiceComponent component) {
      if (!serviceLookup.ContainsKey(component.ServiceType)) {
        serviceLookup[component.ServiceType] = new Dictionary<string, AdsServiceComponent>();
      }
      Dictionary<string, AdsServiceComponent> serviceMap = serviceLookup[component.ServiceType];
      serviceMap[component.Id] = component;
    }

    /// <summary>
    /// Gets all instances of TService.
    /// </summary>
    /// <typeparam name="TService">The type of the service.</typeparam>
    /// <returns>An enumerator that can be used to get all instances of
    /// TService.</returns>
    public IEnumerable<TService> GetAllInstances<TService>() {
      return (IEnumerable<TService>) GetAllInstances(typeof(TService));
    }

    /// <summary>
    /// Gets all instances of service type.
    /// </summary>
    /// <param name="serviceType">Type of the service.</param>
    /// <returns>An enumerator that can be used to get all instances of
    /// service type.</returns>
    public IEnumerable<object> GetAllInstances(Type serviceType) {
      List<object> allInstances = new List<object>();
      if (!serviceLookup.ContainsKey(serviceType)) {
        Dictionary<string, AdsServiceComponent> serviceMap = serviceLookup[serviceType];
        foreach (string key in serviceMap.Keys) {
          allInstances.Add(GetInstance(serviceMap[key].ServiceType, key));
        }
      }
      return allInstances;
    }

    /// <summary>
    /// Gets a default instance of the specified service type.
    /// </summary>
    /// <param name="serviceType">Type of the service.</param>
    /// <returns>An instance of service type.</returns>
    /// <exception cref="ActivationException">Thrown if the object cannot be
    /// created. The inner exception contains more details about why the object
    /// could not be created.</exception>
    /// <remarks>This method never returns a null, failure to create an object
    /// always causes an ActivationException to be thrown.</remarks>
    public object GetInstance(Type serviceType) {
      return GetInstance(serviceType, null);
    }

    /// <summary>
    /// Gets a default instance of the specified service type.
    /// </summary>
    /// <param name="serviceType">The service type.</param>
    /// <returns>An instance of service type.</returns>
    /// <exception cref="ActivationException">Thrown if the object cannot be
    /// created. The inner exception contains more details about why the object
    /// could not be created.</exception>
    /// <remarks>This method never returns a null, failure to create an object
    /// always causes an ActivationException to be thrown.</remarks>
    public object GetService(Type serviceType) {
      return GetInstance(serviceType);
    }

    /// <summary>
    /// Gets a default instance of the specified service type.
    /// </summary>
    /// <typeparam name="TService">The type of the service.</typeparam>
    /// <returns>An instance of service type.</returns>
    /// <exception cref="ActivationException">Thrown if the object cannot be
    /// created. The inner exception contains more details about why the object
    /// could not be created.</exception>
    /// <remarks>This method never returns a null, failure to create an object
    /// always causes an ActivationException to be thrown.</remarks>
    public TService GetInstance<TService>() {
      return GetInstance<TService>(null);
    }

    /// <summary>
    /// Gets a instance of the specified service type, registered with an
    /// identifier key.
    /// </summary>
    /// <typeparam name="TService">The type of the service.</typeparam>
    /// <param name="key">The identifier key.</param>
    /// <returns>An instance of service type.</returns>
    /// <exception cref="ActivationException">Thrown if the object cannot be
    /// created. The inner exception contains more details about why the object
    /// could not be created.</exception>
    /// <remarks>This method never returns a null, failure to create an object
    /// always causes an ActivationException to be thrown.</remarks>
    public TService GetInstance<TService>(string key) {
      return (TService) GetInstance(typeof(TService), key);
    }

    /// <summary>
    /// Gets a instance of the specified service type, registered with an
    /// identifier key.
    /// </summary>
    /// <param name="serviceType">Type of the service.</param>
    /// <param name="key">The identifier key.</param>
    /// <returns>An instance of service type.</returns>
    /// <exception cref="ActivationException">Thrown if the object cannot be
    /// created. The inner exception contains more details about why the object
    /// could not be created.</exception>
    /// <remarks>This method never returns a null, failure to create an object
    /// always causes an ActivationException to be thrown.</remarks>
    public object GetInstance(Type serviceType, string key) {
      if (!serviceLookup.ContainsKey(serviceType)) {
        throw new ActivationException("Could not create object.",
            new Exception("Service type not registered."));
      }
      Dictionary<string, AdsServiceComponent> serviceMap = serviceLookup[serviceType];
      if (key != null) {
        if (!serviceMap.ContainsKey(key)) {
          throw new ActivationException("Could not create object.",
              new Exception("Service type for key not registered."));
        } else {
          try {
            return Activator.CreateInstance(serviceMap[key].InstanceType);
          } catch (Exception ex) {
            throw new ActivationException("Could not create object.", ex);
          }
        }
      } else {
        foreach (string id in serviceMap.Keys) {
          if (serviceMap[id].IsDefault) {
            try {
              return Activator.CreateInstance(serviceMap[id].InstanceType);
            } catch (Exception ex) {
              throw new ActivationException("Could not create object.", ex);
            }
          }
        }
        throw new ActivationException("Could not create object.",
            new Exception("Default instance for Service type not registered."));
      }
    }
  }
}

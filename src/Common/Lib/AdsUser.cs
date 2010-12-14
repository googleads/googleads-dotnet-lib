// Copyright 2010, Google Inc. All Rights Reserved.
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;

namespace Google.Api.Ads.Common.Lib {
  /// <summary>
  /// Represents an Ads API user.
  /// </summary>
  public abstract class AdsUser {
    /// <summary>
    /// The list of SOAP listeners.
    /// </summary>
    List<SoapListener> listeners = new List<SoapListener>();

    /// <summary>
    /// Gets the listeners.
    /// </summary>
    public List<SoapListener> Listeners {
      get {
        return listeners;
      }
    }

    /// <summary>
    /// Stores all the registered services and their factories.
    /// </summary>
    private Dictionary<string, ServiceFactory> serviceFactoryMap =
        new Dictionary<string, ServiceFactory>();

    /// <summary>
    /// Protected constructor. Use this version from a derived class if you want
    /// the library to use all settings from App.config.
    /// </summary>
    protected AdsUser() {
      RegisterServices(GetServiceTypes());
      listeners.AddRange(GetDefaultListeners());
      SetHeadersFromConfig();
    }

    /// <summary>
    /// Protected parameterized constructor. Use this version if you want to
    /// construct an AdsUser with a custom set of headers.
    /// </summary>
    /// <param name="headers">The custom set of headers.</param>
    protected AdsUser(Dictionary<string, string> headers) {
      RegisterServices(GetServiceTypes());
      listeners.AddRange(GetDefaultListeners());
      SetHeaders(headers);
    }

    /// <summary>
    /// Registers a family of services against this user.
    /// </summary>
    /// <param name="servicesFamilies">The family of services that should be
    /// registered against this user.</param>
    /// <remarks>
    /// Every family of services that should be registered with an AdsUser
    /// should be like follows:
    ///
    /// <code>
    ///   public class vX {
    ///     /// Type of the factory that can create services of SomeService.vX
    ///     public Type factoryType = typeof(SomeServiceVxFactory);
    ///
    ///     /// Various services under vX.
    ///
    ///     public readonly ServiceSignature Service1;
    ///     public readonly ServiceSignature Service2;
    ///   }
    /// </code>
    ///
    /// The method uses reflection to
    /// - Find all the fields of type ServiceSignature.
    /// - Extract the factory type from factoryType field.
    /// - Register each found service type with the user.
    ///
    /// </remarks>
    protected void RegisterServices(Type[] servicesFamilies) {
      if (servicesFamilies == null) {
        return;
      }
      foreach (Type serviceFamily in servicesFamilies) {
        FieldInfo fieldInfo = serviceFamily.GetField("factoryType", BindingFlags.Public
            | BindingFlags.Static);
        Type serviceFactoryType = null;
        if (fieldInfo != null) {
          serviceFactoryType = (Type) fieldInfo.GetValue(null);
        } else {
          throw new ArgumentException(string.Format(CommonErrorMessages.MissingFactoryType,
              serviceFactoryType.AssemblyQualifiedName));
        }
        if (serviceFactoryType != null) {
          ServiceFactory serviceFactory =
              (ServiceFactory) Activator.CreateInstance(serviceFactoryType);
          FieldInfo[] fields = serviceFamily.GetFields();
          foreach (FieldInfo field in fields) {
            ServiceSignature signature = field.GetValue(null) as ServiceSignature;
            if (signature != null) {
              RegisterService(signature.Id, serviceFactory);
            }
          }
        }
      }
    }

    /// <summary>
    /// Gets all the service types to be registered against this user.
    /// </summary>
    /// <returns>The type of all service classes to be registered.</returns>
    public abstract Type[] GetServiceTypes();

    /// <summary>
    /// Gets the default listeners.
    /// </summary>
    /// <returns>A list of default listeners</returns>
    public abstract SoapListener[] GetDefaultListeners();

    /// <summary>
    /// Set the user headers from App.config.
    /// </summary>
    protected void SetHeadersFromConfig() {
      List<ServiceFactory> uniqueFactories = GetUniqueFactories();
      foreach (ServiceFactory uniqueFactory in uniqueFactories) {
        uniqueFactory.SetHeaders(uniqueFactory.ReadHeadersFromConfig(uniqueFactory.AppConfig));
      }
    }

    /// <summary>
    /// Set the user headers.
    /// </summary>
    /// <param name="headers">The headers as a set of key-value pairs.</param>
    protected void SetHeaders(Dictionary<string, string> headers) {
      List<ServiceFactory> uniqueFactories = GetUniqueFactories();
      foreach (ServiceFactory uniqueFactory in uniqueFactories) {
        uniqueFactory.SetHeaders(headers);
      }
    }

    /// <summary>
    /// Gets the unique set of factories from the list of registered factories.
    /// </summary>
    private List<ServiceFactory> GetUniqueFactories() {
      List<ServiceFactory> uniqueFactories = new List<ServiceFactory>();
      foreach (string id in serviceFactoryMap.Keys) {
        ServiceFactory serviceFactory = serviceFactoryMap[id];
        if (!uniqueFactories.Contains(serviceFactory)) {
          uniqueFactories.Add(serviceFactory);
        }
      }
      return uniqueFactories;
    }

    /// <summary>
    /// Register a service with AdsUser.
    /// </summary>
    /// <param name="serviceId">A unique id for the service being registered.
    /// </param>
    /// <param name="serviceFactory">The factory that will create this
    /// service.</param>
    public void RegisterService(string serviceId, ServiceFactory serviceFactory) {
      serviceFactoryMap.Add(serviceId, serviceFactory);
    }

    /// <summary>
    /// Creates an object of the requested type of service.
    /// </summary>
    /// <param name="serviceSignature">Signature of the service being requested.
    /// </param>
    /// <returns>An object of the requested type of service. The
    /// caller should cast this object to the desired type.</returns>
    public object GetService(ServiceSignature serviceSignature) {
      return GetService(serviceSignature, String.Empty);
    }

    /// <summary>
    /// Creates an object of the requested type of service.
    /// </summary>
    /// <param name="serviceSignature">Signature of the service being requested.
    /// </param>
    /// <param name="serverUrl">The server url for Ads service.</param>
    /// <returns>An object of the requested type of service. The
    /// caller should cast this object to the desired type.</returns>
    public AdsClient GetService(ServiceSignature serviceSignature, string serverUrl) {
      Uri uri = null;
      if (!string.IsNullOrEmpty(serverUrl)) {
        uri = new Uri(serverUrl);
      }
      return GetService(serviceSignature, uri);
    }

    /// <summary>
    /// Creates an object of the requested type of service.
    /// </summary>
    /// <param name="serviceSignature">Signature of the service being requested.
    /// </param>
    /// <param name="serverUrl">The server url for Ads service.</param>
    /// <returns>An object of the requested type of service. The
    /// caller should cast this object to the desired type.</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public AdsClient GetService(ServiceSignature serviceSignature, Uri serverUrl) {
      if (serviceSignature == null) {
        throw new ArgumentNullException("serviceSignature");
      } else if (!serviceFactoryMap.ContainsKey(serviceSignature.Id)) {
        throw new ArgumentException(string.Format(
            CommonErrorMessages.UnregisteredServiceTypeRequested, serviceSignature.ServiceName));
      } else {
        return serviceFactoryMap[serviceSignature.Id].CreateService(serviceSignature,
            this, serverUrl);
      }
    }

    /// <summary>
    /// Calls the SOAP listeners.
    /// </summary>
    /// <param name="document">The SOAP message as an xml document.</param>
    /// <param name="service">The service for which call is being made.</param>
    /// <param name="direction">The direction of SOAP message.</param>
    internal void CallListeners(XmlDocument document, AdsClient service,
        SoapListener.Direction direction) {
      foreach (SoapListener listener in listeners) {
        listener.HandleMessage(document, service, direction);
      }
    }

    /// <summary>
    /// Initialize the SOAP listeners before an API call.
    /// </summary>
    internal void InitListeners() {
      foreach (SoapListener listener in listeners) {
        listener.InitForCall();
      }
    }

    /// <summary>
    /// Cleans up the SOAP listeners after an API call.
    /// </summary>
    internal void CleanupListeners() {
      foreach (SoapListener listener in listeners) {
        listener.CleanupAfterCall();
      }
    }
  }
}

// Copyright 2009, Google Inc. All Rights Reserved.
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

using com.google.api.adwords.lib.util;
using com.google.api.adwords;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace com.google.api.adwords.lib {
  /// <summary>
  /// Represents an AdWords API user.
  /// </summary>
  public partial class AdWordsUser {
    /// <summary>
    /// Stores all the registered services and their factories.
    /// </summary>
    private Dictionary<string, ServiceFactory> serviceFactoryMap =
        new Dictionary<string, ServiceFactory>();

    /// <summary>
    /// An internal table to cache all the service objects created so far.
    /// </summary>
    private Hashtable objectCache = new Hashtable();

    /// <summary>
    /// Units consumed for API calls during this session.
    /// </summary>
    private List<ApiUnitsEntry> units = new List<ApiUnitsEntry>();

    /// <summary>
    /// Public constructor. Use this version if you want the library to
    /// use all settings from App.config.
    /// </summary>
    public AdWordsUser() {
      AdWordsService.RegisterServices(this);
    }

    /// <summary>
    /// Register a service with AdWordsUser.
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
    /// <param name="serviceType">Signature of the service being requested.
    /// </param>
    /// <returns>An object of the requested type of service. The
    /// caller should cast this object to the desired type.</returns>
    /// <example>
    /// AdWordsUser user = new AdWordsUser();
    /// CampaignService campaignService = (CampaignService)
    ///     user.getService(AdWordsService.v200909.CampaignService);
    /// </example>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public object GetService(ServiceSignature serviceType) {
      if (serviceType == null) {
        throw new ArgumentException("Servicetype cannot be null.");
      } else if (!serviceFactoryMap.ContainsKey(serviceType.id)) {
        throw new ArgumentException("Unknown service type.");
      } else if (objectCache.ContainsKey(serviceType.id)) {
        return objectCache[serviceType.id];
      } else {
        object service = serviceFactoryMap[serviceType.id].CreateService(serviceType, this);
        objectCache.Add(serviceType.id, service);
        return service;
      }
    }

    /// <summary>
    /// Adds addUnits to the total usage against token.
    /// </summary>
    /// <param name="addUnits">The amount of units to be added.</param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void AddUnits(ApiUnitsEntry addUnits) {
      units.Add(addUnits);
    }

    /// <summary>
    /// Gets the units consumed by the services of this object.
    /// </summary>
    /// <returns>The units used by the services of this object,
    /// or 0 if no units are consumed.</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int GetUnits() {
      int totalUnits = 0;
      foreach (ApiUnitsEntry entry in units) {
        totalUnits += entry.Units;
      }
      return totalUnits;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public int GetUnitsForLastOperation() {
      if (units.Count == 0) {
        return 0;
      } else {
        return units[units.Count - 1].Units;
      }
    }

    /// <summary>
    /// Resets the units consumed by the services of this object.
    /// </summary>
    public void ResetUnits() {
      units.RemoveAll(new Predicate<ApiUnitsEntry>(
          delegate(ApiUnitsEntry entry) {
            return true;
          }
      ));
    }

    /// <summary>
    /// Turn on sandbox for all services in AdWordsUser.
    /// </summary>
    public void UseSandbox() {
      foreach (string id in serviceFactoryMap.Keys) {
        serviceFactoryMap[id].UseSandbox();
      }
    }
  }
}

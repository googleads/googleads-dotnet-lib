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

using Google.Api.Ads.Common.Lib;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace Google.Api.Ads.AdWords.Lib {
  /// <summary>
  /// Represents an AdWords API user.
  /// </summary>
  public partial class AdWordsUser : AdsUser {
    /// <summary>
    /// Units consumed for API calls during this session.
    /// </summary>
    private List<ApiUnitsEntry> units = new List<ApiUnitsEntry>();

    /// <summary>
    /// Public constructor. Use this version if you want the library to
    /// use all settings from App.config.
    /// </summary>
    public AdWordsUser() : base(new AdWordsAppConfig()) {
    }

    /// <summary>
    /// Parameterized constructor. Use this version if you want to construct
    /// an AdWordsUser with a custom set of headers.
    /// </summary>
    /// <param name="headers">The custom set of headers.</param>
    public AdWordsUser(Dictionary<string, string> headers) : base(new AdWordsAppConfig(), headers) {
    }

    /// <summary>
    /// Gets all the service types to be registered against this user.
    /// </summary>
    /// <returns>The type of all service classes to be registered.</returns>
    public override Type[] GetServiceTypes() {
      return new AdWordsService().GetServiceTypes();
    }

    /// <summary>
    /// Gets the list of default SOAP listeners.
    /// </summary>
    /// <returns>
    /// A list of default SOAP listeners.
    /// </returns>
    public override SoapListener[] GetDefaultListeners() {
      return new SoapListener[] {AdWordsTraceListener.Instance, AdWordsUnitsListener.Instance};
    }

    /// <summary>
    /// Adds addUnits to the total usage against token.
    /// </summary>
    /// <param name="unit">The amount of units to be added.</param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void AddUnits(ApiUnitsEntry unit) {
      units.Add(unit);
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

    /// <summary>
    /// Gets the units consumed by the last operation.
    /// </summary>
    /// <returns></returns>
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
  }
}

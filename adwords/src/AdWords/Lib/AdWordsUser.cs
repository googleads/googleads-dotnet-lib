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
    /// Keeps track of the API calls made this user.
    /// </summary>
    private List<ApiCallEntry> apiCalls = new List<ApiCallEntry>();

    /// <summary>
    /// Public constructor. Use this version if you want to construct
    /// an AdWordsUser with a custom configuration.
    /// </summary>
    public AdWordsUser(AdWordsAppConfig config)
      : base(config) {
    }

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
      return new SoapListener[] {AdWordsTraceListener.Instance, AdWordsCallListener.Instance};
    }

    /// <summary>
    /// Adds an API call detail to this user instance.
    /// </summary>
    /// <param name="apiCall">The API call details to be added.</param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void AddCallDetails(ApiCallEntry apiCall) {
      apiCalls.Add(apiCall);
    }

    /// <summary>
    /// Gets the details of the API calls made by this user.
    /// </summary>
    /// <returns>The list of all call details.</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public ApiCallEntry[] GetCallDetails() {
      return apiCalls.ToArray();
    }

    /// <summary>
    /// Gets the total number of operations made by this user.
    /// </summary>
    /// <returns>The total number of operations made by this user, or 0 if no
    /// calls were made.</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int GetTotalOperationCount() {
      int totalOperationCount = 0;
      foreach (ApiCallEntry entry in apiCalls) {
        totalOperationCount += entry.OperationCount;
      }
      return totalOperationCount;
    }

    /// <summary>
    /// Gets the number of operations for the last API call.
    /// </summary>
    /// <returns>The number of operations for the last API call, or 0 if no API
    /// calls have been made so far.</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int GetOperationCountForLastCall() {
      if (apiCalls.Count == 0) {
        return 0;
      } else {
        return apiCalls[apiCalls.Count - 1].OperationCount;
      }
    }

    /// <summary>
    /// Resets the call history for this user.
    /// </summary>
    public void ResetCallHistory() {
      apiCalls.Clear();
    }
  }
}

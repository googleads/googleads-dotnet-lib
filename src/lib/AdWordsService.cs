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
using System.Collections.Generic;
using System.Reflection;
using System.Web.Services.Protocols;

namespace com.google.api.adwords.lib {
  /// <summary>
  /// Lists all the services available through this library.
  /// </summary>
  public partial class AdWordsService {
    /// <summary>
    /// Creates a service creation parameter for defining an AdWords service.
    /// </summary>
    /// <param name="version">Service version, for instance, v200909.</param>
    /// <param name="groupName">Group name, for instance, cm.</param>
    /// <param name="serviceName">Service name, for instance, CampaignService.
    /// </param>
    /// <returns>A service creation parameter defining this service.</returns>
    internal static ServiceSignature MakeServiceSignature(string version, string groupName,
        string serviceName) {
      AdWordsApiServiceSignature signature = new AdWordsApiServiceSignature();
      signature.id = version + "." + serviceName;
      signature.serviceName = serviceName;
      signature.groupName = groupName;
      signature.version = version;

      return signature;
    }

    /// <summary>
    /// Creates a service creation parameter for defining a v13 service.
    /// </summary>
    /// <param name="version">Service version, for instance, v13.</param>
    /// <param name="serviceName">Service name, for instance, CampaignService.
    /// </param>
    /// <returns>A service creation parameter defining this service.</returns>
    protected static ServiceSignature MakeLegacyServiceSignature(string version,
        string serviceName) {
      LegacyAdwordsApiServiceSignature param = new LegacyAdwordsApiServiceSignature();
      param.id = version + "." + serviceName;
      param.serviceName = serviceName;
      param.version = version;

      return param;
    }

    /// <summary>
    /// Registers all known services against a given user.
    /// </summary>
    /// <param name="user">The user object to which service type registration
    /// should be done.</param>
    internal static void RegisterServices(AdWordsUser user) {
      Type[] enumTypes = {typeof(v13), typeof(v200909), typeof(v201003)};
      ServiceFactory[] factories = {new LegacyAdWordsApiServiceFactory(),
          new AdWordsApiServiceFactory(), new AdWordsApiServiceFactory()};

      for (int i = 0; i < enumTypes.Length; i++) {
        Type enumType = enumTypes[i];
        ServiceFactory serviceFactory = factories[i];

        FieldInfo[] fields = enumType.GetFields();
        foreach (FieldInfo field in fields) {
          ServiceSignature signature = (ServiceSignature) field.GetValue(null);
          user.RegisterService(signature.id, serviceFactory);
        }
      }
    }
  }

  public partial class AdWordsUser {
    /// <summary>
    /// Extended AdWordsUser constructor to support AdWords API services
    /// (Legacy and new).
    /// </summary>
    /// <param name="headers">The headers as a set of key-value pairs.</param>
    public AdWordsUser(Dictionary<string, string> headers) : this() {
      Dictionary<string, SoapHeader> legacyHeader = null;
      RequestHeader requestHeader = null;

      foreach (string id in serviceFactoryMap.Keys) {
        ServiceFactory serviceFactory = serviceFactoryMap[id];
        if (serviceFactory is LegacyAdWordsApiServiceFactory) {
          LegacyAdWordsApiServiceFactory legacyFactory =
                (serviceFactory as LegacyAdWordsApiServiceFactory);
          if (legacyHeader == null) {
            legacyHeader = legacyFactory.MakeSoapHeaders(headers);
          }
          legacyFactory.Headers = legacyHeader;
        } else if (serviceFactory is AdWordsApiServiceFactory) {
          AdWordsApiServiceFactory awapiFactory = (serviceFactory as AdWordsApiServiceFactory);
          if (requestHeader == null) {
            requestHeader = awapiFactory.MakeRequestHeaders(headers);
          }
          awapiFactory.RequestHeader = requestHeader;
        }
      }
    }
  }
}

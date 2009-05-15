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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web.Services.Protocols;
using com.google.api.adwords.lib.util;

namespace com.google.api.adwords.lib {
  /// <summary>
  /// Represents an AdWords API user. This class wraps features
  /// like creating the services transparently, creating the login
  /// token for v200902 and keeping track of API units used in a session.
  /// </summary>
  public class AdWordsUser {
    /// <summary>
    /// Url for v13 API.
    /// </summary>
    private string urlV13 = ApplicationConfiguration.urlV13;
    /// <summary>
    /// Url for v200902 API.
    /// </summary>
    private string urlV200902 = ApplicationConfiguration.urlV200902;

    /// <summary>
    /// The auth token to be used with v200902 services.
    /// </summary>
    private AuthToken authToken = null;

    /// <summary>
    /// The overridden SOAP headers to be used with v13 services.
    /// If the settings from App.config is not overridden, this
    /// field will be null.
    /// </summary>
    private Dictionary<string, SoapHeader> headers = null;

    /// <summary>
    /// An internal table to cache all the service objects created so far.
    /// </summary>
    private Hashtable objectCache = new Hashtable();

    /// <summary>
    /// Units consumed for API calls during this session.
    /// </summary>
    private int units = 0;

    /// <summary>
    /// Public constructor. Use this version if you want the library to
    /// use all settings from App.config.
    /// </summary>
    public AdWordsUser() {
      // Load authToken and headers from configuration file.
      authToken = ReadAuthTokenFromConfig();
      headers = ReadV13HeadersFromConfig();
    }

    /// <summary>
    /// Public constructor. Use this version if you want to use v13 services
    /// authentication from App.config, but want to override v200902 services
    /// authentication.
    /// </summary>
    /// <param name="authToken">The overridden authToken to be used with
    /// v200902 services.</param>
    public AdWordsUser(AuthToken authToken) {
      this.authToken = authToken;
      headers = ReadV13HeadersFromConfig();
    }

    /// <summary>
    /// Public constructor. Use this version if you want to use v200902
    /// services authentication from App.config, but want to override
    /// v13 SOAP headers.
    /// </summary>
    /// <param name="headers">The custom SOAP headers to be used
    /// with v13 services.</param>
    public AdWordsUser(Dictionary<string, SoapHeader> headers) {
      // Load the authToken from configuration file.
      authToken = ReadAuthTokenFromConfig();
      this.headers = headers;
    }

    /// <summary>
    /// Public constructor. Use this version if you want to override
    /// both v200902 and v13 authentication settings in App.config.
    /// </summary>
    /// <param name="authToken">The overridden authToken to be used with
    /// v200902 services.</param>
    /// <param name="headers">The custom SOAP headers to be used
    /// with v13 services.</param>
    public AdWordsUser(AuthToken authToken,
        Dictionary<string, SoapHeader> headers) {
      this.authToken = authToken;
      this.headers = headers;
    }

    /// <summary>
    /// Creates an object of the requested type of v200902 service.
    /// </summary>
    /// <param name="v2009Service">The name of the service to be
    /// created.</param>
    /// <returns>An object of the requested type of v200902 service. The
    /// caller should cast this object to the desired type.</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public object GetService(ApiServices.v200902 v2009Service) {
      string serviceName = "v200902." + v2009Service.ToString();
      if (!objectCache.ContainsKey(serviceName)) {
        string serviceType = "com.google.api.adwords.v200902." +
            v2009Service.ToString() + "." + v2009Service.ToString();
        object service = Assembly.GetExecutingAssembly().
            CreateInstance(serviceType);
        objectCache[serviceName] = service;

        object requestHeader = Assembly.GetExecutingAssembly().
            CreateInstance("com.google.api.adwords.v200902.RequestHeader");

        Type type = requestHeader.GetType();
        PropertyInfo propInfo = type.GetProperty("clientEmail");

        if (propInfo != null) {
          propInfo.SetValue(requestHeader,
              ApplicationConfiguration.clientEmail, null);
        }

        propInfo = type.GetProperty("clientCustomerId");

        if (propInfo != null) {
          propInfo.SetValue(requestHeader, ApplicationConfiguration.clientCustomerId, null);
        }

        propInfo = type.GetProperty("authToken");

        if (propInfo != null) {
          propInfo.SetValue(requestHeader, authToken.GetToken(), null);
        }

        type = service.GetType();
        propInfo = type.GetProperty("RequestHeader");
        if (propInfo != null) {
          propInfo.SetValue(service, requestHeader, null);
        }

        if (ApplicationConfiguration.proxy != null) {
          propInfo = type.GetProperty("Proxy");
          if (propInfo != null) {
            propInfo.SetValue(service, ApplicationConfiguration.proxy, null);
          }
        }
        if (!string.IsNullOrEmpty(urlV200902)) {
          string fullUrl = urlV200902 +
              "/api/adwords/cm/v200902/" + v2009Service.ToString();
          propInfo = type.GetProperty("Url");
          if (propInfo != null) {
            propInfo.SetValue(service, fullUrl, null);
          }
        }
      }
      return objectCache[serviceName];
    }

    /// <summary>
    /// Creates an object of the requested type of v13 service.
    /// </summary>
    /// <param name="v13Service">The name of the service to be
    /// created.</param>
    /// <returns>An object of the requested type of v13 service. The
    /// caller should cast this object to the desired type.</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public object GetService(ApiServices.v13 v13Service) {
      string serviceName = "v13." + v13Service.ToString();
      if (!objectCache.ContainsKey(serviceName)) {
        string serviceType = "com.google.api.adwords.v13." + v13Service.ToString();
        object service = Assembly.GetExecutingAssembly().CreateInstance(serviceType);
        objectCache[serviceName] = service;
        Type type = service.GetType();
        PropertyInfo propInfo = null;

        if (this.headers != null) {
          foreach (string key in headers.Keys) {
            propInfo = type.GetProperty(key);
            if (propInfo != null) {
              propInfo.SetValue(service, headers[key], null);
            }
          }
        }

        if (ApplicationConfiguration.proxy != null) {
          propInfo = type.GetProperty("Proxy");
          if (propInfo != null) {
            propInfo.SetValue(service, ApplicationConfiguration.proxy, null);
          }
        }
        if (!string.IsNullOrEmpty(urlV13)) {
          string fullUrl = urlV13 + "/api/adwords/v13/" + v13Service.ToString();
          propInfo = type.GetProperty("Url");
          if (propInfo != null) {
            propInfo.SetValue(service, fullUrl, null);
          }
        }
        propInfo = type.GetProperty("Parent");
        if (propInfo != null) {
          propInfo.SetValue(service, this, null);
        }

      }
      return objectCache[serviceName];
    }

    /// <summary>
    /// Adds addUnits to the total usage against token.
    /// </summary>
    /// <param name="addUnits">The amount of units to be added.</param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void AddUnits(int addUnits) {
      units += addUnits;
    }

    /// <summary>
    /// Gets the units consumed by the services of this object.
    /// </summary>
    /// <returns>The units used by the services of this object,
    /// or 0 if no units are consumed.</returns>
    public int GetUnits() {
      return units;
    }

    /// <summary>
    /// Resets the units consumed by the services of this object.
    /// </summary>
    public void ResetUnits() {
      units = 0;
    }

    /// <summary>
    /// Use sandbox for both v13 and v200902 API.
    /// </summary>
    public void UseSandbox() {
      urlV13 = "https://sandbox.google.com";
      urlV200902 = "https://adwords-sandbox.google.com";
    }

    /// <summary>
    /// Loads the v13 headers from App.config
    /// </summary>
    /// <returns>A map, with key=headername and value=SoapHeader object.
    /// </returns>
    private Dictionary<string, SoapHeader> ReadV13HeadersFromConfig() {
      headers = new Dictionary<string, SoapHeader>();
      headers["emailValue"] = MakeHeader("email",
          ApplicationConfiguration.email);
      headers["passwordValue"] = MakeHeader("password",
          ApplicationConfiguration.password);
      headers["useragentValue"] = MakeHeader("useragent",
          "AWAPI DotNetLib " + DataUtilities.GetVersion() + " - " +
          ApplicationConfiguration.companyName);
      headers["developerTokenValue"] = MakeHeader("developerToken",
          ApplicationConfiguration.developerToken);
      headers["applicationTokenValue"] = MakeHeader("applicationToken",
          ApplicationConfiguration.applicationToken);
      headers["clientEmailValue"] = MakeHeader("clientEmail",
          ApplicationConfiguration.clientEmail);
      headers["clientCustomerIdValue"] = MakeHeader("clientCustomerId",
          ApplicationConfiguration.clientCustomerId);
      return headers;
    }

    /// <summary>
    /// Creates a SoapHeader for use with v13 API.
    /// </summary>
    /// <param name="headerName">Name of the header.</param>
    /// <param name="value">String value for the header.</param>
    /// <returns>The SoapHeader object.</returns>
    private SoapHeader MakeHeader(string headerName, string value) {
      string typeName = "com.google.api.adwords.v13." + headerName;
      SoapHeader header = (SoapHeader) Assembly.GetExecutingAssembly().
          CreateInstance(typeName);
      PropertyInfo propInfo = header.GetType().GetProperty("Value");
      if (propInfo != null) {
        propInfo.SetValue(header, new string[] {value}, null);
      }
      return header;
    }

    /// <summary>
    /// Reads the AuthToken for v200902 services from config file.
    /// </summary>
    /// <returns>A new AuthToken object.</returns>
    private AuthToken ReadAuthTokenFromConfig() {
      return new AuthToken(ApplicationConfiguration.email, ApplicationConfiguration.password);
    }
  }
}

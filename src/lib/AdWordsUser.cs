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
using com.google.api.adwords.v200906;

namespace com.google.api.adwords.lib {
  /// <summary>
  /// Represents an AdWords API user. This class wraps features
  /// like creating the services transparently, creating the login
  /// token for v200906 and keeping track of API units used in a session.
  /// </summary>
  public class AdWordsUser {
    /// <summary>
    /// Url for v13 API.
    /// </summary>
    private string urlV13 = ApplicationConfiguration.urlV13;
    /// <summary>
    /// Url for v200906 API.
    /// </summary>
    private string urlV200906 = ApplicationConfiguration.urlV200906;

    /// <summary>
    /// The auth token to be used with v200906 services.
    /// </summary>
    private RequestHeader requestHeader = null;

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
      Initialize(ReadV200906HeadersFromConfig(), ReadV13HeadersFromConfig());
    }

    /// <summary>
    /// Public constructor. Use this version if you want to use v200906
    /// services authentication from App.config, but want to override
    /// v13 SOAP headers.
    /// </summary>
    /// <param name="headers">The custom SOAP headers to be used
    /// with v13 services.</param>
    public AdWordsUser(Dictionary<string, string> headers) {
      Initialize(MakeRequestHeaders(headers), MakeSoapHeaders(headers));
    }

    /// <summary>
    /// Creates an object of the requested type of v200906 service.
    /// </summary>
    /// <param name="v2009Service">The name of the service to be
    /// created.</param>
    /// <returns>An object of the requested type of v200906 service. The
    /// caller should cast this object to the desired type.</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public object GetService(ApiServices.v200906 v2009Service) {
      string serviceName = "v200906." + v2009Service.ToString();
      if (!objectCache.ContainsKey(serviceName)) {
        string serviceType = "com.google.api.adwords.v200906." +
            v2009Service.ToString() + "." + v2009Service.ToString();
        object service = Assembly.GetExecutingAssembly().
            CreateInstance(serviceType);
        objectCache[serviceName] = service;

        Type type = service.GetType();
        PropertyInfo propInfo = type.GetProperty("RequestHeader");
        if (propInfo != null) {
          propInfo.SetValue(service, requestHeader, null);
        }

        if (ApplicationConfiguration.proxy != null) {
          propInfo = type.GetProperty("Proxy");
          if (propInfo != null) {
            propInfo.SetValue(service, ApplicationConfiguration.proxy, null);
          }
        }
        if (!string.IsNullOrEmpty(urlV200906)) {
          string fullUrl = urlV200906 +
              "/api/adwords/cm/v200906/" + v2009Service.ToString();
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
    /// Use sandbox for both v13 and v200906 API.
    /// </summary>
    public void UseSandbox() {
      urlV13 = "https://sandbox.google.com";
      urlV200906 = "https://adwords-sandbox.google.com";
    }

    private RequestHeader ReadV200906HeadersFromConfig() {
      requestHeader = new RequestHeader();
      requestHeader.authToken =
          new AuthToken(ApplicationConfiguration.email,
              ApplicationConfiguration.password).GetToken();
      if (!string.IsNullOrEmpty(ApplicationConfiguration.clientCustomerId)) {
        requestHeader.clientCustomerId = ApplicationConfiguration.clientCustomerId;
      }
      requestHeader.clientEmail = ApplicationConfiguration.clientEmail;
      requestHeader.developerToken = ApplicationConfiguration.developerToken;
      requestHeader.applicationToken = ApplicationConfiguration.applicationToken;
      requestHeader.userAgent = "AWAPI DotNetLib " + DataUtilities.GetVersion() +
          " - " + ApplicationConfiguration.companyName;
      return requestHeader;
    }

    private RequestHeader MakeRequestHeaders(Dictionary<string, string> headers) {
      requestHeader = new RequestHeader();

      Type type = typeof(RequestHeader);
      if (!headers.ContainsKey("authToken")) {
        requestHeader.authToken = new AuthToken(headers["email"], headers["password"]).GetToken();
      }
      foreach (string key in headers.Keys) {
        PropertyInfo propInfo = type.GetProperty(key);
        if (propInfo != null) {
          propInfo.SetValue(requestHeader, headers[key], null);
        }
      }
      return requestHeader;
    }

    /// <summary>
    /// Loads the v13 headers from App.config
    /// </summary>
    /// <returns>A map, with key=headername and value=SoapHeader object.
    /// </returns>
    private Dictionary<string, SoapHeader> ReadV13HeadersFromConfig() {
      headers = new Dictionary<string, SoapHeader>();
      headers["emailValue"] = MakeSoapHeader("email",
          ApplicationConfiguration.email);
      headers["passwordValue"] = MakeSoapHeader("password",
          ApplicationConfiguration.password);
      headers["useragentValue"] = MakeSoapHeader("useragent",
          "AWAPI DotNetLib " + DataUtilities.GetVersion() + " - " +
          ApplicationConfiguration.companyName);
      headers["developerTokenValue"] = MakeSoapHeader("developerToken",
          ApplicationConfiguration.developerToken);
      headers["applicationTokenValue"] = MakeSoapHeader("applicationToken",
          ApplicationConfiguration.applicationToken);
      headers["clientEmailValue"] = MakeSoapHeader("clientEmail",
          ApplicationConfiguration.clientEmail);
      if (!string.IsNullOrEmpty(ApplicationConfiguration.clientCustomerId)) {
        headers["clientCustomerIdValue"] = MakeSoapHeader("clientCustomerId",
            ApplicationConfiguration.clientCustomerId);
      }
      return headers;
    }

    /// <summary>
    /// Convert a dictionary of string header values to SoapHeader objects.
    /// </summary>
    /// <param name="headers">The dictionary, with key as the header field name
    /// and value as the header value.</param>
    /// <returns>A dictionary, with key as header field name and value as a
    /// SoapHeader object.</returns>
    /// <remarks>This function is used by the constructors that accept header
    /// values as string rather than SoapHeader objects.</remarks>
    private Dictionary<string, SoapHeader> MakeSoapHeaders(Dictionary<string, string> headers) {
      Dictionary<string, SoapHeader> soapHeaders = new Dictionary<string, SoapHeader>();
      foreach (string key in headers.Keys) {
        soapHeaders[key + "Value"] = MakeSoapHeader(key, headers[key]);
      }
      return soapHeaders;
    }

    /// <summary>
    /// Creates a SoapHeader for use with v13 API.
    /// </summary>
    /// <param name="headerName">Name of the header.</param>
    /// <param name="value">String value for the header.</param>
    /// <returns>The SoapHeader object.</returns>
    private SoapHeader MakeSoapHeader(string headerName, string value) {
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
    /// Initializes this object.
    /// </summary>
    /// <param name="requestHeader">RequestHeader to be used with v200906 services.</param>
    /// <param name="headers">The SOAP headers to be used with v13 services.</param>
    /// <remarks>This function is used by all constructors.</remarks>
    private void Initialize(RequestHeader requestHeader, Dictionary<string, SoapHeader> headers) {
      this.requestHeader = requestHeader;
      this.headers = headers;
    }
  }
}

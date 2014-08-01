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

using Google.Api.Ads.AdWords.Headers;
using Google.Api.Ads.Common.Lib;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Google.Api.Ads.AdWords.Lib {
  /// <summary>
  /// The factory class for all AdWords API services.
  /// </summary>
  public class AdWordsServiceFactory : ServiceFactory {

    /// <summary>
    /// The request header to be used with AdWords API services.
    /// </summary>
    private RequestHeader requestHeader;

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public AdWordsServiceFactory() {
    }

    /// <summary>
    /// Create a service object.
    /// </summary>
    /// <param name="signature">Signature of the service being created.</param>
    /// <param name="user">The user for which the service is being created.
    /// <param name="serverUrl">The server to which the API calls should be
    /// made.</param>
    /// </param>
    /// <returns>An object of the desired service type.</returns>
    public override AdsClient CreateService(ServiceSignature signature, AdsUser user,
        Uri serverUrl) {
      AdWordsAppConfig awConfig = (AdWordsAppConfig) Config;
      if (serverUrl == null) {
        serverUrl = new Uri(awConfig.AdWordsApiServer);
      }

      if (user == null) {
        throw new ArgumentNullException("user");
      }

      CheckServicePreconditions(signature);

      AdWordsServiceSignature awapiSignature = signature as AdWordsServiceSignature;

      AdsClient service = (AdsClient) Activator.CreateInstance(awapiSignature.ServiceType);
      PropertyInfo propInfo = awapiSignature.ServiceType.GetProperty("RequestHeader");
      if (propInfo != null) {
        RequestHeader clonedHeader = (RequestHeader) requestHeader.Clone();
        clonedHeader.Version = awapiSignature.Version;
        clonedHeader.GroupName = awapiSignature.GroupName;
        propInfo.SetValue(service, clonedHeader, null);
      }

      if (awConfig.Proxy != null) {
        service.Proxy = awConfig.Proxy;
      }
      service.Timeout = awConfig.Timeout;
      service.Url = string.Format("{0}api/adwords/{1}/{2}/{3}",
          serverUrl.AbsoluteUri, awapiSignature.GroupName, awapiSignature.Version,
          awapiSignature.ServiceName);
      service.EnableDecompression = awConfig.EnableGzipCompression;
      service.User = user;
      service.Signature = awapiSignature;
      return service;
    }

    /// <summary>
    /// Reads the headers from App.config.
    /// </summary>
    /// <param name="config">The configuration class.</param>
    protected override void ReadHeadersFromConfig(AppConfig config) {
      AdWordsAppConfig awConfig = (AdWordsAppConfig) config;
      this.requestHeader = new RequestHeader();

      if (!string.IsNullOrEmpty(awConfig.ClientCustomerId)) {
        requestHeader.clientCustomerId = awConfig.ClientCustomerId;
      }
      requestHeader.developerToken = awConfig.DeveloperToken;
      requestHeader.userAgent = awConfig.GetUserAgent();
    }

    /// <summary>
    /// Fix the request header namespace in outgoing Soap Requests, so that
    /// cross namespace requests can work properly.
    /// </summary>
    /// <param name="signature">The service creation parameters.</param>
    /// <param name="service">The service object for which RequestHeader
    /// needs to be patched.</param>
    private static void FixRequestHeaderNameSpace(AdWordsServiceSignature signature,
        AdsClient service) {
      // Set the header namespace prefix. For all /cm services, the members
      // shouldn't have xmlns. For all other services, the members should have
      // /cm as xmlns.
      object[] attributes = service.GetType().GetCustomAttributes(false);
      foreach (object attribute in attributes) {
        if (attribute is WebServiceBindingAttribute) {
          WebServiceBindingAttribute binding = (WebServiceBindingAttribute) attribute;
          string delimiter = "/api/adwords/";
          string xmlns = String.Join("", new String[] {
              binding.Namespace.Substring(0, binding.Namespace.IndexOf(delimiter) +
                  delimiter.Length), "cm/", signature.Version});
          if (xmlns == binding.Namespace) {
            xmlns = "";
          }

          RequestHeader svcRequestHeader = null;
          PropertyInfo propInfo = service.GetType().GetProperty("RequestHeader");
          if (propInfo != null) {
            svcRequestHeader = (RequestHeader) propInfo.GetValue(service, null);

            if (svcRequestHeader != null) {
              PropertyInfo wsPropInfo = svcRequestHeader.GetType().GetProperty("TargetNamespace");
              if (wsPropInfo != null) {
                wsPropInfo.SetValue(svcRequestHeader, xmlns, null);
              }
            }
          }
        }
      }
    }

    /// <summary>
    /// Checks preconditions of the service signature and throws and exception if the service
    /// cannot be generated.
    /// </summary>
    /// <param name="signature">the service signature for generating the service</param>
    protected override void CheckServicePreconditions(ServiceSignature signature) {
      if (signature == null) {
        throw new ArgumentNullException("signature");
      }

      if (!(signature is AdWordsServiceSignature)) {
        throw new InvalidCastException(string.Format(CultureInfo.InvariantCulture,
            AdWordsErrorMessages.SignatureIsOfWrongType, typeof(AdWordsServiceSignature)));
      }
    }
  }
}

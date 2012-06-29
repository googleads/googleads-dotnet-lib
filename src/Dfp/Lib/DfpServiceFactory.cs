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
using Google.Api.Ads.Dfp.Headers;

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

namespace Google.Api.Ads.Dfp.Lib {
  /// <summary>
  /// The factory class for all DFP API services.
  /// </summary>
  public class DfpServiceFactory : ServiceFactory {
    /// <summary>
    /// The request header to be used with DFP API services.
    /// </summary>
    private RequestHeader requestHeader;

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public DfpServiceFactory() {
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
      DfpAppConfig dfpConfig = (DfpAppConfig) AppConfig;
      if (serverUrl == null) {
        serverUrl = new Uri(dfpConfig.DfpApiServer);
      }

      if (user == null) {
        throw new ArgumentNullException("user");
      }

      if (signature == null) {
        throw new ArgumentNullException("signature");
      }
      if (!(signature is DfpServiceSignature)) {
        throw new InvalidCastException(string.Format(CultureInfo.InvariantCulture,
            DfpErrorMessages.SignatureIsOfWrongType, typeof(DfpServiceSignature)));
      }

      DfpServiceSignature dfpapiSignature = signature as DfpServiceSignature;

      AdsClient service = (AdsClient) Activator.CreateInstance(dfpapiSignature.ServiceType);
      PropertyInfo propInfo = dfpapiSignature.ServiceType.GetProperty("RequestHeader");

      if (propInfo != null) {
        RequestHeader clonedHeader = (RequestHeader) requestHeader.Clone();
        clonedHeader.Version = dfpapiSignature.Version;
        propInfo.SetValue(service, clonedHeader, null);
      }

      if (dfpConfig.Proxy != null) {
        service.Proxy = dfpConfig.Proxy;
      }
      service.Timeout = dfpConfig.Timeout;
      service.Url = string.Format("{0}apis/ads/publisher/{1}/{2}",
          serverUrl, dfpapiSignature.Version, dfpapiSignature.ServiceName);
      service.UserAgent = dfpConfig.GetUserAgent();

      service.User = user;
      return service;
    }

    /// <summary>
    /// Reads the headers from App.config.
    /// </summary>
    /// <param name="config">The configuration class.</param>
    protected override void ReadHeadersFromConfig(AppConfigBase config) {
      DfpAppConfig dfpConfig = (DfpAppConfig) config;

      this.requestHeader = new RequestHeader();
      this.requestHeader.networkCode = dfpConfig.NetworkCode;
      this.requestHeader.applicationName = dfpConfig.GetUserAgent();
    }
  }
}

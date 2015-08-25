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

namespace Google.Api.Ads.Dfa.Lib {
  /// <summary>
  /// Service creation params for DFA API family of services.
  /// </summary>
  public class DfaServiceSignature : ServiceSignature {
    /// <summary>
    /// Gets the type of service.
    /// </summary>
    public override Type ServiceType {
      get {
        return Type.GetType(string.Format(CultureInfo.InvariantCulture,
            "Google.Api.Ads.Dfa.{0}.{1}", VersionNamespace, ServiceName));
      }
    }

    /// <summary>
    /// Gets the service version namespace.
    /// </summary>
    public string VersionNamespace {
      get {
        // Version takes the form of vx.yz, but since since it is not a valid
        // identifier in C#, we generate namespace as vx_yz.
        return Version.Replace(".", "_");
      }
    }

    /// <summary>
    /// Gets the service endpoint.
    /// </summary>
    public string ServiceEndpoint {
      get {
        // The generated classes have their name as XyzRemoteService, and their
        // endpoints are /xyz (i.e. lower case, and without RemoteService).
        string endpoint = ServiceName.ToLower().Replace("remoteservice", "");

        if (endpoint.CompareTo("placementstrategy") == 0) {
          return "strategy";
        } else {
          return endpoint;
        }
      }
    }

    /// <summary>
    /// Public constructor.
    /// </summary>
    /// <param name="version">Service version.</param>
    /// <param name="serviceName">Service name.</param>
    public DfaServiceSignature(string version, string serviceName) : base(version, serviceName,
        SupportedProtocols.WSE) {
    }
  }
}

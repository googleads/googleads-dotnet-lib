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
  /// Service creation params for AdWords API family of services.
  /// </summary>
  public class AdWordsServiceSignature : ServiceSignature {
    /// <summary>
    /// The group name, for instance, cm.
    /// </summary>
    private string groupName;

    /// <summary>
    /// Gets the group name.
    /// </summary>
    public string GroupName {
      get {
        return groupName;
      }
    }

    /// <summary>
    /// Gets the type of service.
    /// </summary>
    public override Type ServiceType {
      get {
        return Type.GetType(string.Format(CultureInfo.InvariantCulture,
            "Google.Api.Ads.AdWords.{0}.{1}", Version, ServiceName));
      }
    }

    /// <summary>
    /// Public constructor.
    /// </summary>
    /// <param name="version">Service version.</param>
    /// <param name="serviceSignature">Service name.</param>
    /// <param name="groupName">The group name</param>
    public AdWordsServiceSignature(string version, string serviceSignature, string groupName)
        : base(version, serviceSignature, SupportedProtocols.SOAP) {
      this.groupName = groupName;
    }
  }
}

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
using System.Reflection;
using System.Text;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Globalization;

namespace Google.Api.Ads.Dfp {
  /// <summary>
  /// Soap Request header for DFP API services.
  /// </summary>
  public class RequestHeader : SoapHeaderBase {
    /// <summary>
    /// Auth token.
    /// </summary>
    private string authTokenField;

    /// <summary>
    /// Network code.
    /// </summary>
    private string networkCodeField;

    /// <summary>
    /// Application name.
    /// </summary>
    private string applicationNameField;

    /// <summary>
    /// Gets or sets the auth token.
    /// </summary>
    public string authToken {
      get {
        return this.authTokenField;
      }
      set {
        this.authTokenField = value;
      }
    }

    /// <summary>
    /// Gets or sets the network code.
    /// </summary>
    public string networkCode {
      get {
        return this.networkCodeField;
      }
      set {
        this.networkCodeField = value;
      }
    }

    /// <summary>
    /// Gets or sets the application name.
    /// </summary>
    public string applicationName {
      get {
        return this.applicationNameField;
      }
      set {
        this.applicationNameField = value;
      }
    }
  }
}

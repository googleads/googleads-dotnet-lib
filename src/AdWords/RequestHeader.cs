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

namespace Google.Api.Ads.AdWords {
  /// <summary>
  /// Soap Request header for AdWords API services.
  /// </summary>
  public class RequestHeader : SoapHeaderBase {
    /// <summary>
    /// Application token.
    /// </summary>
    private string applicationTokenField;

    /// <summary>
    /// Auth token.
    /// </summary>
    private string authTokenField;

    /// <summary>
    /// Client customer Id.
    /// </summary>
    private string clientCustomerIdField;

    /// <summary>
    /// Client email.
    /// </summary>
    private string clientEmailField;

    /// <summary>
    /// Developer token.
    /// </summary>
    private string developerTokenField;

    /// <summary>
    /// User agent.
    /// </summary>
    private string userAgentField;

    /// <summary>
    /// Validate only header - useful if you just want to check if the call is fine.
    /// </summary>
    private bool? validateOnlyField;

    /// <summary>
    /// If true, the API will try to commit as many error free operations as
    /// possible and report the other operations' errors
    /// </summary>
    private bool? partialFailureField;

    /// <summary>
    /// Gets or sets the application token.
    /// </summary>
    public string applicationToken {
      get {
        return this.applicationTokenField;
      }
      set {
        this.applicationTokenField = value;
      }
    }

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
    /// Gets or sets the client customer id.
    /// </summary>
    public string clientCustomerId {
      get {
        return this.clientCustomerIdField;
      }
      set {
        this.clientCustomerIdField = value;
      }
    }

    /// <summary>
    /// Gets or sets the client email.
    /// </summary>
    public string clientEmail {
      get {
        return this.clientEmailField;
      }
      set {
        this.clientEmailField = value;
      }
    }

    /// <summary>
    /// Gets or sets the developer token.
    /// </summary>
    public string developerToken {
      get {
        return this.developerTokenField;
      }
      set {
        this.developerTokenField = value;
      }
    }

    /// <summary>
    /// Gets or sets the user agent.
    /// </summary>
    public string userAgent {
      get {
        return this.userAgentField;
      }
      set {
        this.userAgentField = value;
      }
    }

    /// <summary>
    /// Gets or sets the validateOnly header.
    /// </summary>
    public bool? validateOnly {
      get {
        return this.validateOnlyField;
      }
      set {
        this.validateOnlyField = value;
      }
    }

    /// <summary>
    /// Gets or sets whether the API should try to commit as many error free
    /// operations as possible and report the other operations' errors.
    /// </summary>
    public bool? partialFailure {
      get {
        return partialFailureField;
      }
      set {
        partialFailureField = value;
      }
    }
  }
}

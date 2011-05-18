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

namespace Google.Api.Ads.Dfp.Headers {
  /// <summary>
  /// Soap Request header for DFP API services.
  /// </summary>
  public class RequestHeader : DfpSoapHeader {
    /// <summary>
    /// The request header stub that this class wraps.
    /// </summary>
    RequestHeaderStub stub = new RequestHeaderStub();

    /// <summary>
    /// Gets or sets the stub that is wrapped by this object.
    /// </summary>
    public override object Stub {
      get {
        return stub;
      }
      protected set {
        stub = value as RequestHeaderStub;
      }
    }

    /// <summary>
    /// Gets or sets the authentication field.
    /// </summary>
    public Authentication authentication {
      get {
        return stub.authentication;
      }
      set {
        stub.authentication = value;
      }
    }

    /// <summary>
    /// Gets or sets the auth token.
    /// </summary>
    public string authToken {
      get {
        return stub.authToken;
      }
      set {
        stub.authToken = value;
      }
    }

    /// <summary>
    /// Gets or sets the network code.
    /// </summary>
    public string networkCode {
      get {
        return stub.networkCode;
      }
      set {
        stub.networkCode = value;
      }
    }

    /// <summary>
    /// Gets or sets the application name.
    /// </summary>
    public string applicationName {
      get {
        return stub.applicationName;
      }
      set {
        stub.applicationName = value;
      }
    }
  }
}

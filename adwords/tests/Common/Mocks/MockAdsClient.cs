// Copyright 2012, Google Inc. All Rights Reserved.
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
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Google.Api.Ads.Common.Tests.Mocks {
  /// <summary>
  /// Implements a mock version of AdsClient interface for testing purposes.
  /// </summary>
  /// <remarks>Only the properties needed for testing Ads.Common assembly are
  /// implemented, all other properties throw a NotImplementedException.
  /// </remarks>
  class MockAdsClient : AdsClient {
    /// <summary>
    /// The user who created this service.
    /// </summary>
    AdsUser user;

    /// <summary>
    /// The webrequest used for making the last API call.
    /// </summary>
    WebRequest lastRequest;

    /// <summary>
    /// The webresponse from the last API call.
    /// </summary>
    WebResponse response;

    /// <summary>
    /// Gets or sets the AdsUser object that created this
    /// service.
    /// </summary>
    public AdsUser User {
      get {
        return user;
      }
      set {
        user = value;
      }
    }


    /// <summary>
    /// Gets or sets the signature for this service.
    /// </summary>
    public ServiceSignature Signature {
      get {
        throw new NotImplementedException();
      }
      set {
        throw new NotImplementedException();
      }
    }

    /// <summary>
    /// Gets or sets the web request associated with this service's
    /// last API call.
    /// </summary>
    public WebRequest LastRequest {
      get {
        return lastRequest;
      }
      set {
        lastRequest = value;
      }
    }

    /// <summary>
    /// Gets or sets the web response associated with this service's
    /// last API call.
    /// </summary>
    public WebResponse LastResponse {
      get {
        return response;
      }
      set {
        response = value;
      }
    }

    #region Unimplemented Properties

    public string ConnectionGroupName {
      get {
        throw new NotImplementedException();
      }
      set {
        throw new NotImplementedException();
      }
    }

    public ICredentials Credentials {
      get {
        throw new NotImplementedException();
      }
      set {
        throw new NotImplementedException();
      }
    }

    public bool PreAuthenticate {
      get {
        throw new NotImplementedException();
      }
      set {
        throw new NotImplementedException();
      }
    }

    public Encoding RequestEncoding {
      get {
        throw new NotImplementedException();
      }
      set {
        throw new NotImplementedException();
      }
    }

    public int Timeout {
      get {
        throw new NotImplementedException();
      }
      set {
        throw new NotImplementedException();
      }
    }

    public string Url {
      get {
        throw new NotImplementedException();
      }
      set {
        throw new NotImplementedException();
      }
    }

    public bool UseDefaultCredentials {
      get {
        throw new NotImplementedException();
      }
      set {
        throw new NotImplementedException();
      }
    }

    public bool AllowAutoRedirect {
      get {
        throw new NotImplementedException();
      }
      set {
        throw new NotImplementedException();
      }
    }

    public X509CertificateCollection ClientCertificates {
      get { throw new NotImplementedException(); }
    }

    public CookieContainer CookieContainer {
      get {
        throw new NotImplementedException();
      }
      set {
        throw new NotImplementedException();
      }
    }

    public bool EnableDecompression {
      get {
        throw new NotImplementedException();
      }
      set {
        throw new NotImplementedException();
      }
    }

    public IWebProxy Proxy {
      get {
        throw new NotImplementedException();
      }
      set {
        throw new NotImplementedException();
      }
    }

    public bool UnsafeAuthenticatedConnectionSharing {
      get {
        throw new NotImplementedException();
      }
      set {
        throw new NotImplementedException();
      }
    }

    public string UserAgent {
      get {
        throw new NotImplementedException();
      }
      set {
        throw new NotImplementedException();
      }
    }

    #endregion
  }
}

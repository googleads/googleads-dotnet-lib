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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Google.Api.Ads.Common.Lib;

namespace Google.Api.Ads.Common.Lib {
  /// <summary>
  /// This interface defines a client protocol (SOAP, WSE, REST, etc.)
  /// supported by the library.
  /// </summary>
  public interface AdsClient {
    /// <summary>
    /// Gets or sets the name of the connection group for the request.
    /// </summary>
    string ConnectionGroupName {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets security credentials for client authentication.
    /// </summary>
    ICredentials Credentials {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets whether pre-authentication is enabled.
    /// </summary>
    bool PreAuthenticate {
      get;
      set;
    }

    /// <summary>
    /// The System.Text.Encoding used to make the client request to the service.
    /// </summary>
    Encoding RequestEncoding {
      get;
      set;
    }

    /// <summary>
    /// The timeout for the request.
    /// </summary>
    int Timeout {
      get;
      set;
    }

    /// <summary>
    /// The url endpoint for the service.
    /// </summary>
    string Url {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets whether this service should use the credentials from
    /// System.Net.CredentialCache.DefaultCredentials.
    /// </summary>
    bool UseDefaultCredentials {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets whether the client automatically follows server redirects.
    /// </summary>
    bool AllowAutoRedirect {
      get;
      set;
    }

    /// <summary>
    /// Gets the collection of client certificates.
    /// </summary>
    X509CertificateCollection ClientCertificates {
      get;
    }

    /// <summary>
    /// Gets or sets the collection of cookies.
    /// </summary>
    CookieContainer CookieContainer {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets whether gzip compression is enabled.
    /// </summary>
    bool EnableDecompression {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets proxy information for making a service request through
    /// a firewall.
    /// </summary>
    IWebProxy Proxy {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets a value that indicates whether connection sharing is
    /// enabled when the client uses NTLM authentication to connect to the Web
    /// server that hosts the web service.
    /// </summary>
    bool UnsafeAuthenticatedConnectionSharing {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the value for the user agent header that is sent with each
    /// request.
    /// </summary>
    string UserAgent {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the AdsUser object that created this
    /// service.
    /// </summary>
    AdsUser User {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the signature for this service.
    /// </summary>
    ServiceSignature Signature {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the web request associated with this service's
    /// last API call.
    /// </summary>
    WebRequest LastRequest {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the web response associated with this service's
    /// last API call.
    /// </summary>
    WebResponse LastResponse {
      get;
      set;
    }
  }
}

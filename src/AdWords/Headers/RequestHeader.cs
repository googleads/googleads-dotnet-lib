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

namespace Google.Api.Ads.AdWords.Headers {
  /// <summary>
  /// This class wraps the <see cref="RequestHeaderStub"/>, adding
  /// cross-namespace serialization capabilities.
  /// </summary>
  /// <remarks>The XmlSerializer provides two mutually exclusive ways of
  /// serialization.
  /// - Mark a class as Serializable, and allow the class to be serialized
  /// automagically. This option does object serialization correctly, but isn't
  /// cross-namespace aware.
  /// - Implement IXmlSerializable, and customize the serialization. This
  /// option allows us to customize the serialization process and add cross
  /// namespace support.
  ///
  /// However since options 1 and 2 are mutually exclusive (i.e. cannot be
  /// applied to the same object hierarchy), we cannot derive this class from
  /// <see cref="ResponseHeaderStub"/>, instead, we have to wrap it in another
  /// class.
  /// </remarks>
  public class RequestHeader : AdWordsSoapHeader {
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
    /// Gets or sets the client customer id.
    /// </summary>
    public string clientCustomerId {
      get {
        return stub.clientCustomerId;
      }
      set {
        stub.clientCustomerId = value;
      }
    }

    /// <summary>
    /// Gets or sets the developer token.
    /// </summary>
    public string developerToken {
      get {
        return stub.developerToken;
      }
      set {
        stub.developerToken = value;
      }
    }

    /// <summary>
    /// Gets or sets the user agent.
    /// </summary>
    public string userAgent {
      get {
        return stub.userAgent;
      }
      set {
        stub.userAgent = value;
      }
    }

    /// <summary>
    /// Gets or sets whether this API call is for validation only.
    /// </summary>
    public bool validateOnly {
      get {
        return stub.validateOnly;
      }
      set {
        stub.validateOnlySpecified = true;
        stub.validateOnly = value;
      }
    }

    /// <summary>
    /// Gets or sets whether <see cref="validateOnly"/> is specified.
    /// </summary>
    public bool validateOnlySpecified {
      get {
        return stub.validateOnlySpecified;
      }
      set {
        stub.validateOnlySpecified = value;
      }
    }

    /// <summary>
    /// Gets or sets whether partial failures should be returned.
    /// </summary>
    public bool partialFailure {
      get {
        return stub.partialFailure;
      }
      set {
        stub.partialFailureSpecified = true;
        stub.partialFailure = value;
      }
    }

    /// <summary>
    /// Gets or sets whether <see cref="partialFailure"/> is specified.
    /// </summary>
    public bool partialFailureSpecified {
      get {
        return stub.partialFailureSpecified;
      }
      set {
        stub.partialFailureSpecified = value;
      }
    }

    /// <summary>
    /// Gets or sets the AdWords Express business ID. This header applies only
    /// for AdWords Express API services.
    /// </summary>
    public long expressBusinessId {
      get {
        return stub.expressBusinessId;
      }
      set {
        stub.expressBusinessIdSpecified = true;
        stub.expressBusinessId = value;
      }
    }

    /// <summary>
    /// Gets or sets whether <see cref="expressBusinessId"/> is specified. This
    /// field applies only for AdWords Express API services.
    /// </summary>
    public bool expressBusinessIdSpecified {
      get {
        return stub.expressBusinessIdSpecified;
      }
      set {
        stub.expressBusinessIdSpecified = value;
      }
    }
  }
}

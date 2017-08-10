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

using System;
using System.ServiceModel.Channels;
using System.Xml;

namespace Google.Api.Ads.AdWords.Headers {

  /// <summary>
  /// This class represents an AdWords SOAP request header.
  /// </summary>
  public class RequestHeader : AdWordsSoapHeader, ICloneable {
    private bool _validateOnly;
    private bool _partialFailure;

    /// <summary>
    /// Gets the name of this header.
    /// </summary>
    public override string Name {
      get { return "RequestHeader"; }
    }

    /// <summary>
    /// Gets or sets the client customer id.
    /// </summary>
    public string clientCustomerId { get; set; }

    /// <summary>
    /// Gets or sets the developer token.
    /// </summary>
    public string developerToken { get; set; }

    /// <summary>
    /// Gets or sets the user agent.
    /// </summary>
    public string userAgent { get; set; }

    /// <summary>
    /// Gets or sets whether this API call is for validation only.
    /// </summary>
    public bool validateOnly {
      get {
        return _validateOnly;
      }
      set {
        validateOnlySpecified = true;
        _validateOnly = value;
      }
    }

    /// <summary>
    /// Gets or sets whether <see cref="validateOnly"/> is specified.
    /// </summary>
    public bool validateOnlySpecified { get; set; }

    /// <summary>
    /// Gets or sets whether partial failures should be returned.
    /// </summary>
    public bool partialFailure {
      get {
        return _partialFailure;
      }
      set {
        partialFailureSpecified = true;
        _partialFailure = value;
      }
    }

    /// <summary>
    /// Gets or sets whether <see cref="partialFailure"/> is specified.
    /// </summary>
    public bool partialFailureSpecified { get; set; }

    /// <summary>
    /// Called when the header content is serialized using the specified XML writer.
    /// </summary>
    protected override void OnWriteHeaderContents(XmlDictionaryWriter writer,
        MessageVersion messageVersion) {
      writer.WriteElementString("clientCustomerId", clientCustomerId);
      writer.WriteElementString("developerToken", developerToken);
      writer.WriteElementString("userAgent", userAgent);
      if (validateOnlySpecified) {
        writer.WriteElementString("validateOnly", validateOnly.ToString());
      }
      if (partialFailureSpecified) {
        writer.WriteElementString("partialFailure", partialFailure.ToString());
      }
    }

    /// <summary>
    /// Creates a new object that is a copy of the current instance.
    /// </summary>
    public object Clone() {
      return this.MemberwiseClone();
    }
  }
}

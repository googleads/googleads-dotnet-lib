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
using System.Reflection;
using System.Xml;

namespace Google.Api.Ads.Dfp.Headers {
  /// <summary>
  /// SOAP Response header for DFP API services.
  /// </summary>
  public class ResponseHeader : DfpSoapHeader {
    /// <summary>
    /// The request header stub that this class wraps.
    /// </summary>
    ResponseHeaderStub stub = new ResponseHeaderStub();

    /// <summary>
    /// Gets or sets the stub that is wrapped by this object.
    /// </summary>
    public override object Stub {
      get {
        return stub;
      }
      protected set {
        stub = value as ResponseHeaderStub;
      }
    }

    /// <summary>
    /// Gets or sets the request id for this API call.
    /// </summary>
    public string requestId {
      get {
        return stub.requestId;
      }
      set {
        stub.requestId = value;
      }
    }

    /// <summary>
    /// Gets or sets the response time for this API call.
    /// </summary>
    public long responseTime {
      get {
        return stub.responseTime;
      }
      set {
        stub.responseTimeSpecified = true;
        stub.responseTime = value;
      }
    }

    /// <summary>
    /// Gets or sets the response time for this API call.
    /// </summary>
    public bool responseTimeSpecified {
      get {
        return stub.responseTimeSpecified;
      }
      set {
        stub.responseTimeSpecified = value;
      }
    }

    /// <summary>
    /// Deserialize the object from xml.
    /// </summary>
    /// <param name="reader">The xml reader for reading the serialized xml.
    /// </param>
    public override void ReadXml(XmlReader reader) {
      object service = ContextStore.GetValue("SoapService");
      if (service != null) {
        PropertyInfo propInfo = service.GetType().GetProperty("RequestHeader");
        if (propInfo != null) {
          RequestHeader reqHeader = (RequestHeader)propInfo.GetValue(service, null);
          if (reqHeader != null) {
            // when deserializing, namespace is not relevant, just the version
            // is.
            this.Version = reqHeader.Version;
          }
        }
      }
      base.ReadXml(reader);
    }
  }
}

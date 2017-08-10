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

using System.Runtime.Serialization;

namespace Google.Api.Ads.AdWords.Headers {

  /// <summary>
  /// This class represents an AdWords SOAP response header.
  /// </summary>
  [DataContract(Name = "ResponseHeader", Namespace = PLACEHOLDER_NAMESPACE)]
  public class ResponseHeader {

    /// <summary>
    /// A placeholder namespace for deserializing response headers from different API versions.
    /// </summary>
    public const string PLACEHOLDER_NAMESPACE =
        "https://adwords.google.com/api/adwords/group/version";

    private long _operations;
    private long _responseTime;

    /// <summary>
    /// Gets or sets whether <see cref="operations"/> is specified.
    /// </summary>
    public bool operationsSpecified { get; set; }

    /// <summary>
    /// Gets or sets whether <see cref="responseTime"/> is specified.
    /// </summary>
    public bool responseTimeSpecified { get; set; }

    /// <summary>
    /// Gets or sets the request id for this API call.
    /// </summary>
    [DataMember(Order = 0)]
    public string requestId { get; set; }

    /// <summary>
    /// Gets or sets the name of the service that was invoked.
    /// </summary>
    [DataMember(Order = 1)]
    public string serviceName { get; set; }

    /// <summary>
    /// Gets or sets the name of the method that was invoked.
    /// </summary>
    [DataMember(Order = 2)]
    public string methodName { get; set; }

    /// <summary>
    /// Gets or sets the number of operations for this API call.
    /// </summary>
    [DataMember(Order = 3)]
    public long operations {
      get {
        return _operations;
      }
      set {
        operationsSpecified = true;
        _operations = value;
      }
    }

    /// <summary>
    /// Gets or sets the response time for this API call.
    /// </summary>
    [DataMember(Order = 4)]
    public long responseTime {
      get {
        return _responseTime;
      }
      set {
        responseTimeSpecified = true;
        _responseTime = value;
      }
    }
  }
}

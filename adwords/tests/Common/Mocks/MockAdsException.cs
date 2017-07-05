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
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Google.Api.Ads.Common.Tests.Mocks {
  /// <summary>
  /// Implements a mock version of AdsException class for testing purposes.
  /// </summary>
  [Serializable()]
  public class MockAdsException : AdsException {
    /// <summary>
    /// Mock property, for testing serialization.
    /// </summary>
    int mockProperty;

    /// <summary>
    /// Gets or sets the mock property for testing serialization.
    /// </summary>
    /// <value>
    /// The mock property.
    /// </value>
    public int MockProperty {
      get {
        return mockProperty;
      }
      set {
        mockProperty = value;
      }
    }

    /// <summary>
    /// Public constructor.
    /// </summary>
    public MockAdsException() : base() {
    }

    /// <summary>
    /// Public constructor.
    /// </summary>
    /// <param name="message">Error message for this API exception.</param>
    public MockAdsException(string message) : base(message) {
    }

    /// <summary>
    /// Public constructor.
    /// </summary>
    /// <param name="message">Error message for this API exception.</param>
    /// <param name="innerException">Inner exception, if any.</param>
    public MockAdsException(string message, Exception innerException)
        : base(message, innerException) {
    }


    /// <summary>
    /// Protected constructor. Used by serialization frameworks while
    /// deserializing an exception object.
    /// </summary>
    /// <param name="info">Info about the serialization context.</param>
    /// <param name="context">A streaming context that represents the
    /// serialization stream.</param>
    protected MockAdsException(SerializationInfo info, StreamingContext context)
        : base(info, context) {
      mockProperty = GetValue<int>(info, "MockProperty");
    }

    /// <summary>
    /// This method is called by serialization frameworks while serializing
    /// an exception object.
    /// </summary>
    /// <param name="info">Info about the serialization context.</param>
    /// <param name="context">A streaming context that represents the
    /// serialization stream.</param>
    [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
    public override void GetObjectData(SerializationInfo info, StreamingContext context) {
      base.GetObjectData(info, context);
      info.AddValue("MockProperty", mockProperty);
    }
  }
}

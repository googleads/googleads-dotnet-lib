// Copyright 2013, Google Inc. All Rights Reserved.
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
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Google.Api.Ads.Common.Lib {
  /// <summary>
  /// Exception thrown when credentials are expired.
  /// </summary>
  public class CredentialsExpiredException<T> : AdsException {
    T expiredCredential;

    /// <summary>
    /// Gets the expired credential.
    /// </summary>
    public T ExpiredCredential {
      get {
        return expiredCredential;
      }
    }

    /// <summary>
    /// Public constructor.
    /// </summary>
    /// <param name="expiredCredential">The expired credential.</param>
    public CredentialsExpiredException(T expiredCredential) : base() {
      this.expiredCredential = expiredCredential;
    }

    /// <summary>
    /// Public constructor.
    /// </summary>
    /// <param name="message">Error message for this API exception.</param>
    /// <param name="expiredCredential">The expired credential.</param>
    public CredentialsExpiredException(T expiredCredential, string message)
        : base(message) {
      this.expiredCredential = expiredCredential;
    }

    /// <summary>
    /// Public constructor.
    /// </summary>
    /// <param name="expiredCredential">The expired credential.</param>
    /// <param name="message">Error message for this API exception.</param>
    /// <param name="innerException">Inner exception, if any.</param>
    public CredentialsExpiredException(T expiredCredential, string message,
        Exception innerException) : base(message, innerException) {
      this.expiredCredential = expiredCredential;
    }

    /// <summary>
    /// Protected constructor. Used by serialization frameworks while
    /// deserializing an exception object.
    /// </summary>
    /// <param name="info">Info about the serialization context.</param>
    /// <param name="context">A streaming context that represents the
    /// serialization stream.</param>
    protected CredentialsExpiredException(SerializationInfo info, StreamingContext context)
        : base(info, context) {
      this.expiredCredential = (T) info.GetValue("ExpiredCredential", typeof(T));
    }

    /// <summary>
    /// Gets details about the exception for serialization.
    /// </summary>
    /// <param name="info">Holds the serialized object data about the exception
    /// being thrown.</param>
    /// <param name="context">Contains contextual information about the source
    /// or destination.</param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context) {
      base.GetObjectData(info, context);
      info.AddValue("ExpiredCredential", this.expiredCredential);
    }
  }
}

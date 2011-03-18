// Copyright 2010, Google Inc. All Rights Reserved.
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
using System.IO;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace com.google.api.adwords.lib {
  /// <summary>
  /// Custom exception class for wrapping AdWords API Soap exceptions.
  /// </summary>
  [Serializable]
  public class AdWordsApiException : ApplicationException {
    /// <summary>
    /// public constructor.
    /// </summary>
    public AdWordsApiException()
      : base() {
      this.apiException = null;
    }

    /// <summary>
    /// public constructor.
    /// </summary>
    public AdWordsApiException(object apiException)
      : base() {
      this.apiException = apiException;
    }

    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="message">Error message for this API exception.</param>
    public AdWordsApiException(object apiException, string message)
      : base(message) {
      this.apiException = apiException;
    }

    /// <summary>
    /// public constructor.
    /// </summary>
    /// <param name="message">Error message for this API exception.</param>
    /// <param name="innerException">Inner exception, if any.</param>
    public AdWordsApiException(object apiException, string message, Exception innerException)
      : base(message, innerException) {
      this.apiException = apiException;
    }

    /// <summary>
    /// Protected constructor, used by serialization frameworks while
    /// deserializing an exception object.
    /// </summary>
    /// <param name="info">Info about the serialization context.</param>
    /// <param name="context">A streaming context that represents the
    /// serialization stream.</param>
    protected AdWordsApiException(SerializationInfo info, StreamingContext context)
      : base(info, context) {
      string contents = (string) info.GetValue("apiException", typeof(string));
      if (contents != null) {
        Type contentType = (Type) info.GetValue("apiExceptionType", typeof(Type));

        MemoryStream memStream = new MemoryStream();
        byte[] bytes = Encoding.UTF8.GetBytes(contents);
        memStream.Write(bytes, 0, bytes.Length);
        memStream.Seek(0, SeekOrigin.Begin);

        XmlSerializer serializer = new XmlSerializer(contentType);
        apiException = serializer.Deserialize(memStream);
      }
    }

    /// <summary>
    /// This method is called by serialization frameworks while serializing
    /// an exception object.
    /// </summary>
    /// <param name="info">Info about the serialization context.</param>
    /// <param name="context">A streaming context that represents the
    /// serialization stream.</param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context) {
      base.GetObjectData(info, context);
      if (apiException != null) {
        MemoryStream memStream = new MemoryStream();
        XmlSerializer serializer = new XmlSerializer(apiException.GetType());
        serializer.Serialize(memStream, apiException);
        info.AddValue("apiException", Encoding.UTF8.GetString(memStream.ToArray()));
        info.AddValue("apiExceptionType", apiException.GetType());
      }
    }

    /// <summary>
    /// Gets the ApiException object
    /// </summary>
    public object ApiException {
      get {
        return apiException;
      }
    }

    /// <summary>
    /// The original ApiException object from AdWords API.
    /// </summary>
    private object apiException;
  }
}

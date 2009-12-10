// Copyright 2009, Google Inc. All Rights Reserved.
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

using com.google.api.adwords.lib;

using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace com.google.api.adwords {
  /// <summary>
  /// Base class for AdWords API services.
  /// </summary>
  public class AdWordsApiService : SoapServiceBase {
    /// <summary>
    /// Gets a custom exception that wraps the SOAP exception thrown
    /// by the server.
    /// </summary>
    /// <param name="ex">SOAPException that was thrown by the server.</param>
    /// <returns>A custom exception object that wraps the SOAP exception.
    /// </returns>
    protected override Exception GetCustomException(SoapException ex) {
      AdWordsApiException retVal = new AdWordsApiException(null, ex.Message, ex);

      string defaultNs = GetDefaultNamespace();
      if (!string.IsNullOrEmpty(defaultNs)) {
        // Extract the ApiExceptionFault node.
        XmlNamespaceManager xmlns = new XmlNamespaceManager(ex.Detail.OwnerDocument.NameTable);
        xmlns.AddNamespace("api", defaultNs);
        XmlElement faultNode = (XmlElement) ex.Detail.SelectSingleNode(
            "api:ApiExceptionFault", xmlns);

        if (faultNode != null) {
          // Make a stream out of the node contents.
          MemoryStream memStream = new MemoryStream();
          byte[] bytes = Encoding.UTF8.GetBytes(faultNode.OuterXml);
          memStream.Write(bytes, 0, bytes.Length);
          memStream.Seek(0, SeekOrigin.Begin);

          try {
            // Manually deserialize the stream contents into an ApiException object.
            Type ApiExceptionType = Assembly.GetExecutingAssembly().GetType(
                this.GetType().Namespace + ".ApiException");

            if (ApiExceptionType != null) {
              XmlSerializer serializer = new XmlSerializer(ApiExceptionType,
                  new XmlAttributeOverrides(), new Type[] {},
                  new XmlRootAttribute("ApiExceptionFault"), defaultNs);

              object apiException = serializer.Deserialize(memStream);
              retVal = new AdWordsApiException(apiException, ex.Message, ex);
            }
          } catch (Exception) {
            // deserialization failed, but we can safely ignore it.
          }
        }
      }
      return retVal;
    }

    /// <summary>
    /// Gets the default xml namespace, based on the type of this object.
    /// </summary>
    /// <returns>The xml namespace to which this object is serialized.</returns>
    private string GetDefaultNamespace() {
      object[] attributes = this.GetType().GetCustomAttributes(false);
      foreach (object attribute in attributes) {
        if (attribute is WebServiceBindingAttribute) {
          WebServiceBindingAttribute binding = (WebServiceBindingAttribute) attribute;
          return binding.Namespace;
        }
      }
      return "";
    }
  }
}

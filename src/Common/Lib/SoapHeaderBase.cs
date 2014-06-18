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

using Google.Api.Ads.Common.Util;

using System;
using System.Collections.Generic;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Google.Api.Ads.Common.Lib {
  /// <summary>
  /// Common base class for all SOAP Headers.
  /// </summary>
  public abstract class SoapHeaderBase : SoapHeader, ICloneable, IXmlSerializable {
    /// <summary>
    /// The placeholders that need to be replaced in the serialized xml from
    /// the stub.
    /// </summary>
    protected Dictionary<string, string> placeHolders = new Dictionary<string, string>();

    /// <summary>
    /// Gets or sets the stub that is wrapped by this object.
    /// </summary>
    public abstract object Stub {
      get;
      protected set;
    }

    /// <summary>
    /// Gets the schema for this object. Can return null if the
    /// server does not expect the schema.
    /// </summary>
    /// <returns>The xml schema associated with this class, or null
    /// if not applicable.</returns>
    public virtual XmlSchema GetSchema() {
      return null;
    }

    /// <summary>
    /// Gets the name of the XML element for serializing this object or null if
    /// no element name is available.
    /// </summary>
    protected string XmlElementName {
      get {
        foreach (Attribute attribute in Stub.GetType().GetCustomAttributes(true)) {
          if (attribute is XmlRootAttribute) {
            return (attribute as XmlRootAttribute).ElementName;
          }
        }
        return null;
      }
    }

    /// <summary>
    /// Gets the XML namespace for serializing this object or null if no
    /// namespace is available.
    /// </summary>
    protected string XmlNamespace {
      get {
        foreach (Attribute attribute in Stub.GetType().GetCustomAttributes(true)) {
          if (attribute is XmlRootAttribute) {
            return (attribute as XmlRootAttribute).Namespace;
          }
        }
        return null;
      }
    }

    /// <summary>
    /// Deserialize the object from xml.
    /// </summary>
    /// <param name="reader">The xml reader for reading the
    /// serialized xml.</param>
    public virtual void ReadXml(XmlReader reader) {
      XmlDocument doc = SerializationUtilities.LoadXml(reader.ReadOuterXml());

      XmlNameTable xmlnt = doc.NameTable;
      XmlElement root = doc.CreateElement(XmlElementName, XmlNamespace);

      XmlNodeList xmlNodes = doc.DocumentElement.SelectNodes("*");
      foreach (XmlNode node in xmlNodes) {
        root.AppendChild(node);
      }
      doc.RemoveAll();
      doc.AppendChild(root);

      string contents = doc.OuterXml;

      foreach (string key in placeHolders.Keys) {
        if (placeHolders[key] != null) {
          contents = contents.Replace(placeHolders[key], key);
        }
      }
      Stub = SerializationUtilities.DeserializeFromXmlText(contents, Stub.GetType());
    }

    /// <summary>
    /// Serialize the object into an xml.
    /// </summary>
    /// <param name="writer">The writer to which the serialized data
    /// should be written.</param>
    public virtual void WriteXml(XmlWriter writer) {
      string contents = SerializationUtilities.SerializeAsXmlText(Stub);

      foreach (string key in placeHolders.Keys) {
        if (placeHolders[key] != null) {
          contents = contents.Replace(key, placeHolders[key]);
        }
      }

      XmlDocument xDoc = SerializationUtilities.LoadXml(contents);
      writer.WriteRaw(xDoc.DocumentElement.InnerXml);
    }

    /// <summary>
    /// Clone this object.
    /// </summary>
    /// <returns>
    /// A cloned object.
    /// </returns>
    public object Clone() {
      SoapHeaderBase header = (SoapHeaderBase) Activator.CreateInstance(this.GetType());
      header.Stub = SerializationUtilities.DeserializeFromXmlText(
          SerializationUtilities.SerializeAsXmlText(this.Stub), this.Stub.GetType());
      return header;
    }
  }
}

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
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;

namespace Google.Api.Ads.Common.Lib {
  /// <summary>
  /// Common base class for all SOAP Headers.
  /// </summary>
  public abstract class SoapHeaderBase : SoapHeader, IXmlSerializable, ICloneable {
    /// <summary>
    /// The xml namespace under which the members should be serialized.
    /// </summary>
    private string targetNamespace;

    /// <summary>
    /// Gets or sets the target namespace under which the child
    /// nodes should be serialized.
    /// </summary>
    public string TargetNamespace {
      get {
        return targetNamespace;
      }
      set {
        targetNamespace = value;
      }
    }

    /// <summary>
    /// Clone this object.
    /// </summary>
    /// <returns>A cloned object.</returns>
    public object Clone() {
      SoapHeaderBase header = (SoapHeaderBase) Activator.CreateInstance(this.GetType());
      string[] allProperties = GetAllDeclaredProperties();
      foreach (string propertyName in allProperties) {
        PropertyInfo propInfo = this.GetType().GetProperty(propertyName);
        propInfo.SetValue(header, propInfo.GetValue(this, null), null);
      }
      return header;
    }

    /// <summary>
    /// Gets the schema for this object. Can return null if the
    /// server does not expect the schema.
    /// </summary>
    /// <returns>The xml schema associated with this class, or null
    /// if not applicable.</returns>
    public XmlSchema GetSchema() {
      return null;
    }

    /// <summary>
    /// Deserialize the object from xml.
    /// </summary>
    /// <param name="reader">The xml reader for reading the
    /// serialized xml.</param>
    public void ReadXml(XmlReader reader) {
      if (reader == null) {
        throw new ArgumentNullException("reader");
      }

      DeserializeProperties(reader, GetSerializableProperties());
    }

    /// <summary>
    /// Serialize the object into an xml.
    /// </summary>
    /// <param name="writer">The writer to which the serialized data
    /// should be written.</param>
    public void WriteXml(XmlWriter writer) {
      if (writer == null) {
        throw new ArgumentNullException("writer");
      }
      SerializeProperties(writer, GetSerializableProperties());
    }

    /// <summary>
    /// Gets the list of all properties declared in this class.
    /// </summary>
    /// <returns>The list of all properties.</returns>
    /// <remarks>This method won't return inherited properties.</remarks>
    protected string[] GetAllDeclaredProperties() {
      List<string> propertyNames = new List<string>();
      foreach (PropertyInfo propInfo in GetType().GetProperties(BindingFlags.DeclaredOnly |
          BindingFlags.Public | BindingFlags.Instance)) {
        propertyNames.Add(propInfo.Name);
      }
      return propertyNames.ToArray();
    }

    /// <summary>
    /// Serialize object properties as xml.
    /// </summary>
    /// <param name="writer">The writer to which properties are serialized.
    /// </param>
    /// <param name="propertyNames">The list of property names to be serialized.
    /// </param>
    protected void SerializeProperties(XmlWriter writer, string[] propertyNames) {
      foreach (string propertyName in propertyNames) {
        PropertyInfo propInfo = this.GetType().GetProperty(propertyName);
        object value = propInfo.GetValue(this, null);
        if (value != null) {
          // Convert value into its string representation.
          if (value is bool) {
            value = value.ToString().ToLower(CultureInfo.InvariantCulture);
          } else {
            value = value.ToString();
          }
          if (String.IsNullOrEmpty(targetNamespace)) {
            writer.WriteElementString(propInfo.Name, (string) value);
          } else {
            writer.WriteElementString(propInfo.Name, targetNamespace, (string) value);
          }
        }
      }
    }

    /// <summary>
    /// Deserializes object properties from xml.
    /// </summary>
    /// <param name="reader">The reader from which properties are deserialized.
    /// </param>
    /// <param name="propertyNames">The list of property names to be
    /// deserialized.</param>
    protected void DeserializeProperties(XmlReader reader, string[] propertyNames) {
      string elementXml = reader.ReadOuterXml();
      XmlDocument doc = new XmlDocument();
      doc.LoadXml(elementXml);
      foreach (XmlNode node in doc.DocumentElement.ChildNodes) {
        if (node is XmlElement) {
          if (Array.Exists<string>(propertyNames, delegate(string match) {
            return string.Compare(match, node.Name, true) == 0;
          })) {
            PropertyInfo propInfo = this.GetType().GetProperty(node.Name);
            if (propInfo.PropertyType == typeof(long?)) {
              if (!string.IsNullOrEmpty(node.InnerText)) {
                long tempVal = 0;
                long.TryParse(node.InnerText, out tempVal);
                propInfo.SetValue(this, tempVal, null);
              } else {
                propInfo.SetValue(this, null, null);
              }
            } else if (propInfo.PropertyType == typeof(bool?)) {
              bool? tempVal = null;
              if (!string.IsNullOrEmpty(node.InnerText)) {
                tempVal = node.InnerText == "true";
              }
              propInfo.SetValue(this, tempVal, null);
            } else if (propInfo.PropertyType == typeof(string)) {
              propInfo.SetValue(this, node.InnerText, null);
            } else {
              Debugger.Break();
            }
          }
        }
      }
    }

    /// <summary>
    /// Gets the list of all properties in this class that can be serialized.
    /// </summary>
    /// <returns>A list of properties to be serialized.</returns>
    protected virtual string[] GetSerializableProperties() {
      return GetAllDeclaredProperties();
    }
  }
}

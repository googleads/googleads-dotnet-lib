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
using System.IO;
using System.Xml.Serialization;
using System.Text;

namespace Google.Api.Ads.Common.Util {
  /// <summary>
  /// This class provides utility methods in serializing and deserializing an
  /// object as xml.
  /// </summary>
  public class SerializationUtilities {
    /// <summary>
    /// Deserialize an object from xml.
    /// </summary>
    /// <param name="contents">The serialized xml.</param>
    /// <param name="contentType">The type of deserialized object.</param>
    /// <param name="ns">The xml namespace of the object in serialized xml.
    /// </param>
    /// <param name="rootNode">The root node name for the object in serialized
    /// xml.</param>
    /// <returns>The deserialized object or null if deserialization fails.</returns>
    /// <remarks><paramref name="contentType"/> must be XmlSerializable.
    /// </remarks>
    public static object DeserializeFromXmlText(string contents, Type contentType, string ns,
        string rootNode) {
      if (string.IsNullOrEmpty(contents) || contentType == null || string.IsNullOrEmpty(ns) ||
          string.IsNullOrEmpty(rootNode)) {
        return null;
      }

      XmlSerializer serializer = new XmlSerializer(contentType, new XmlAttributeOverrides(),
          new Type[] {}, new XmlRootAttribute(rootNode), ns);
      return DeserializeFromXmlText(serializer, contents);
    }

    /// <summary>
    /// Deserialize an object from xml.
    /// </summary>
    /// <param name="contents">The serialized xml.</param>
    /// <param name="contentType">The type of deserialized object.</param>
    /// <returns>The deserialized object.</returns>
    /// <remarks><paramref name="contentType"/> must be XmlSerializable.
    /// </remarks>
    public static object DeserializeFromXmlText(string contents, Type contentType) {
      if (string.IsNullOrEmpty(contents) || contentType == null) {
        return null;
      }

      return DeserializeFromXmlText(new XmlSerializer(contentType), contents);
    }

    /// <summary>
    /// Deserialize an object from xml.
    /// </summary>
    /// <param name="contents">The serialized xml.</param>
    /// <param name="serializer">The serializer to be used for deserializing
    /// the objects.</param>
    /// <returns>The deserialized object.</returns>
    /// <remarks><paramref name="contentType"/> must be XmlSerializable.
    /// </remarks>
    private static object DeserializeFromXmlText(XmlSerializer serializer, string contents) {
      MemoryStream memStream = new MemoryStream();
      byte[] bytes = Encoding.UTF8.GetBytes(contents);
      memStream.Write(bytes, 0, bytes.Length);
      memStream.Seek(0, SeekOrigin.Begin);
      return serializer.Deserialize(memStream);
    }

    /// <summary>
    /// Serializes an object as xml.
    /// </summary>
    /// <param name="objToSerialize">The object to serialize.</param>
    /// <returns>The serialized xml string.</returns>
    /// <remarks><paramref name="objToSerialize"/> must me XmlSerializable.
    /// </remarks>
    public static string SerializeAsXmlText(object objToSerialize) {
      MemoryStream memStream = new MemoryStream();
      new XmlSerializer(objToSerialize.GetType()).Serialize(memStream, objToSerialize);
      return Encoding.UTF8.GetString(memStream.ToArray());
    }
  }
}

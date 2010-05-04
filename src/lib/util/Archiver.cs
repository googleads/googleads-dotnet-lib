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

// Author: api.anash@gmail.com (Anash P. Oommen)

using com.google.api.adwords.v13;
using com.google.api.adwords.v200909;

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace com.google.api.adwords.lib.util {
  /// <summary>
  /// Handles the archiving and unarchiving of a <see cref="ClientAccount"/> object
  /// to an XML document.
  /// </summary>
  class Archiver {
    /// <summary>
    /// Deserialize an account from an XML node.
    /// </summary>
    /// <param name="xAccount">The XML node that contains serialized data.</param>
    /// <returns>The deserialized ClientAccount object.</returns>
    internal ClientAccount DeSerializeAccount(XmlElement accountNode) {
      XmlNode clientAccountNode = accountNode.SelectSingleNode("ClientAccount");
      XmlDocument xmldoc = new XmlDocument();
      xmldoc.AppendChild(xmldoc.ImportNode(clientAccountNode, true));
      MemoryStream memoryStream = new MemoryStream();
      xmldoc.Save(memoryStream);
      memoryStream.Seek(0, SeekOrigin.Begin);
      XmlSerializer serializer = new XmlSerializer(typeof(ClientAccount));
      ClientAccount client = (ClientAccount) serializer.Deserialize(memoryStream);
      return client;
    }

    /// <summary>
    /// A generic serialization function to serialize an Object as XML.
    /// </summary>
    /// <param name="accountNode">The XML node to which serialization
    /// happens.</param>
    /// <param name="client">The account details to be serialized.</param>
    internal void SerializeAccount(XmlElement accountNode, ClientAccount client) {
      MemoryStream memoryStream = new MemoryStream();
      XmlSerializer serializer = new XmlSerializer(typeof(ClientAccount));
      serializer.Serialize(memoryStream, client);
      string contents = Encoding.UTF8.GetString(memoryStream.ToArray());
      XmlDocument xmldoc = new XmlDocument();
      xmldoc.LoadXml(contents);
      XmlNode importedNode = accountNode.OwnerDocument.ImportNode(xmldoc.DocumentElement, true);
      accountNode.AppendChild(importedNode);
    }
  }
}

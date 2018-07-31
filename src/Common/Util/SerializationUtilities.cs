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

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using System.Text;
using System.Xml;

namespace Google.Api.Ads.Common.Util
{
    /// <summary>
    /// This class provides utility methods in serializing and deserializing an
    /// object as xml.
    /// </summary>
    public class SerializationUtilities
    {
        /// <summary>
        /// A map to store custom xml serializers.
        /// </summary>
        /// <remarks> For background, see
        /// http://support.microsoft.com/kb/886385/en-us. The summary of the issue
        /// is that .NET runtime will leak XmlSerializer objects if the non-custom
        /// constructor (custom rootNode, namespace, etc.) is requested, and hence
        /// the serializers need to be cached locally. To replicate memory leaks,
        /// write AdWords API code that will throw a lot of SOAPExceptions. The
        /// XmlSerializers obtained for deserializing the SOAP exceptions will be
        /// leaked by the runtime.
        /// </remarks>
        private static Dictionary<string, XmlSerializer> customSerializerMaps =
            new Dictionary<string, XmlSerializer>();

        /// <summary>
        /// Deserialize an object from xml for a custom root node and xml namespace.
        /// </summary>
        /// <param name="contents">The serialized xml.</param>
        /// <param name="contentType">The type of deserialized object.</param>
        /// <param name="ns">The xml namespace of the object in serialized xml.
        /// </param>
        /// <param name="rootNode">The root node name for the object in serialized
        /// xml.</param>
        /// <returns>The deserialized object or null if deserialization fails.
        /// </returns>
        /// <remarks><paramref name="contentType"/> must be XmlSerializable.
        /// </remarks>
        public static object DeserializeFromXmlTextCustomRootNs(string contents, Type contentType,
            string ns, string rootNode)
        {
            if (string.IsNullOrEmpty(contents) || contentType == null || string.IsNullOrEmpty(ns) ||
                string.IsNullOrEmpty(rootNode))
            {
                return null;
            }

            XmlSerializer serializer = GetCustomXmlSerializer(contentType, ns, rootNode);
            return DeserializeFromXmlText(serializer, contents);
        }

        /// <summary>
        /// Gets a custom serializer for a type from a cache.
        /// </summary>
        /// <param name="contentType">The type of deserialized object.</param>
        /// <param name="ns">The xml namespace of the object in serialized xml.
        /// </param>
        /// <param name="rootNode">The root node name for the object in serialized
        /// xml.</param>
        /// <returns>The xml serializer.</returns>
        /// <remarks>See http://support.microsoft.com/kb/886385/en-us and
        /// http://code.google.com/p/google-api-adwords-dotnet/issues/detail?id=78
        /// for more details.</remarks>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private static XmlSerializer GetCustomXmlSerializer(Type contentType, string ns,
            string rootNode)
        {
            string key = string.Format("{0}_{1}_{2}", contentType.AssemblyQualifiedName, ns,
                rootNode);

            XmlSerializer serializer = CollectionUtilities.TryGetValue(customSerializerMaps, key);
            if (serializer == null)
            {
                serializer = new XmlSerializer(contentType, new XmlAttributeOverrides(), new Type[]
                {
                }, new XmlRootAttribute(rootNode), ns);
                customSerializerMaps.Add(key, serializer);
            }

            return serializer;
        }

        /// <summary>
        /// Deserialize an object from xml.
        /// </summary>
        /// <param name="contents">The serialized xml.</param>
        /// <param name="contentType">The type of deserialized object.</param>
        /// <returns>The deserialized object.</returns>
        /// <remarks><paramref name="contentType"/> must be XmlSerializable.
        /// </remarks>
        public static object DeserializeFromXmlText(string contents, Type contentType)
        {
            if (string.IsNullOrEmpty(contents) || contentType == null)
            {
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
        /// <remarks><paramref name="contents"/> must be XmlSerializable.
        /// </remarks>
        private static object DeserializeFromXmlText(XmlSerializer serializer, string contents)
        {
            object retval = null;

            using (MemoryStream memStream = new MemoryStream())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(contents);
                memStream.Write(bytes, 0, bytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                retval = serializer.Deserialize(memStream);
            }

            return retval;
        }

        /// <summary>
        /// Serializes an object as xml.
        /// </summary>
        /// <param name="objToSerialize">The object to serialize.</param>
        /// <returns>The serialized xml string.</returns>
        /// <remarks><paramref name="objToSerialize"/> must be XmlSerializable.
        /// </remarks>
        public static string SerializeAsXmlText(object objToSerialize)
        {
            string retval = "";

            using (StringWriter writer = new Utf8StringWriter())
            {
                new XmlSerializer(objToSerialize.GetType()).Serialize(writer, objToSerialize);
                retval = writer.ToString();
            }

            return retval;
        }

        /// <summary>
        /// Clones an object.
        /// </summary>
        /// <param name="objToClone">The object to clone.</param>
        /// <returns>The cloned object.</returns>
        /// <remarks><paramref name="objToClone"/> must be Serializable.
        /// </remarks>
        public static object CloneObject(object objToClone)
        {
            string serializedObject = SerializeAsXmlText(objToClone);
            return DeserializeFromXmlText(serializedObject, objToClone.GetType());
        }

        /// <summary>
        /// Used for serializing string into UTF-8 xml, instead of default Unicode.
        /// (utf-16). 
        /// </summary>
        class Utf8StringWriter : StringWriter
        {
            /// <summary>
            /// Gets the <see cref="T:System.Text.Encoding" /> in which the output is
            /// written.
            /// </summary>
            /// <returns>
            /// The Encoding in which the output is written.
            ///   </returns>
            public override Encoding Encoding
            {
                get { return new UTF8Encoding(false); }
            }
        }
    }
}

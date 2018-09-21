// Copyright 2018 Google LLC
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

using Google.Api.Ads.AdWords.Util;

using System.Collections.Generic;

namespace Google.Api.Ads.AdWords.v201806
{
    public partial class String_StringMapEntry : IMapEntry<string, string>
    {
    }

    public partial class Media_Size_DimensionsMapEntry : IMapEntry<MediaSize, Dimensions>
    {
    }

    public partial class Media_Size_StringMapEntry : IMapEntry<MediaSize, string>
    {
    }

    public partial class Type_AttributeMapEntry : IMapEntry<AttributeType, Attribute>
    {
    }

    /// <summary>
    /// A class to hold extension methods to convert MapEntry arrays into dictionary.
    /// </summary>
    public static class MapEntryExtensions
    {
        /// <summary>
        /// Converts an array of <code>IMapEntry"</code> objects into a dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="array">The array of <code>IMapEntry"</code> objects to be
        /// converted into a Dictionary.</param>
        /// <returns>The converted dictionary.</returns>
        /// <remarks>Use this method only when you can't use the equivalent extension
        /// method due to a framework limitation (e.g. Mono's VBNC compiler).</remarks>
        public static Dictionary<TKey, TValue> ToDict<TKey, TValue>(IMapEntry<TKey, TValue>[] array)
        {
            return new MapUtilities<TKey, TValue>().AddEntries(array).AsDict();
        }

        /// <summary>
        /// Converts an array of <see cref="String_StringMapEntry" /> objects into a dictionary.
        /// </summary>
        /// <param name="array">The array of <see cref="String_StringMapEntry" /> objects to be
        /// converted into a Dictionary.</param>
        /// <returns>The converted dictionary.</returns>
        public static Dictionary<string, string> ToDict(this String_StringMapEntry[] array)
        {
            return new MapUtilities<string, string>().AddEntries(array).AsDict();
        }

        /// <summary>
        /// Converts an array of <see cref="Media_Size_DimensionsMapEntry" /> objects into
        /// a dictionary.
        /// </summary>
        /// <param name="array">The array of <see cref="Media_Size_DimensionsMapEntry" /> objects
        /// to be converted into a Dictionary.</param>
        /// <returns>The converted dictionary.</returns>
        public static Dictionary<MediaSize, Dimensions> ToDict(
            this Media_Size_DimensionsMapEntry[] array)
        {
            return new MapUtilities<MediaSize, Dimensions>().AddEntries(array).AsDict();
        }

        /// <summary>
        /// Converts an array of <see cref="Media_Size_StringMapEntry" /> objects into
        /// a dictionary.
        /// </summary>
        /// <param name="array">The array of <see cref="Media_Size_StringMapEntry" /> objects
        /// to be converted into a Dictionary.</param>
        /// <returns>The converted dictionary.</returns>
        public static Dictionary<MediaSize, string> ToDict(this Media_Size_StringMapEntry[] array)
        {
            return new MapUtilities<MediaSize, string>().AddEntries(array).AsDict();
        }

        /// <summary>
        /// Converts an array of <see cref="Type_AttributeMapEntry" /> objects into
        /// a dictionary.
        /// </summary>
        /// <param name="array">The array of <see cref="Type_AttributeMapEntry" /> objects
        /// to be converted into a Dictionary.</param>
        /// <returns>The converted dictionary.</returns>
        public static Dictionary<AttributeType, Attribute> ToDict(
            this Type_AttributeMapEntry[] array)
        {
            return new MapUtilities<AttributeType, Attribute>().AddEntries(array).AsDict();
        }
    }
}

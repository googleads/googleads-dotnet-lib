// Copyright 2016, Google Inc. All Rights Reserved.
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

using System.Collections.Generic;

namespace Google.Api.Ads.AdWords.Util
{
    /// <summary>
    /// Interface to map common properties for fields of type TKey_TValueMapEntry[].
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public interface IMapEntry<TKey, TValue>
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        TKey key { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        TValue value { get; set; }
    }

    /// <summary>
    /// Provides utility functions for creating and handling fields of the form
    /// TKey_TValueMapEntry[].
    /// </summary>
    public class MapUtilities<TKey, TValue>
    {
        /// <summary>
        /// Dictionary to hold the key-value pairs.
        /// </summary>
        private Dictionary<TKey, TValue> values = new Dictionary<TKey, TValue>();

        /// <summary>
        /// Adds the specified key to the map.
        /// </summary>
        /// <param name="key">The settings key.</param>
        /// <param name="value">The settings value.</param>
        /// <returns>The maputils instance, for chaining calls.</returns>
        public MapUtilities<TKey, TValue> Add(TKey key, TValue value)
        {
            values[key] = value;
            return this;
        }

        /// <summary>
        /// Adds the specified string map entry to the map.
        /// </summary>
        /// <param name="entry">The string map entry.</param>
        /// <returns>The maputils instance, for chaining calls.</returns>
        public MapUtilities<TKey, TValue> AddEntry(IMapEntry<TKey, TValue> entry)
        {
            values[entry.key] = entry.value;
            return this;
        }

        /// <summary>
        /// Adds an array of string map entries to the map.
        /// </summary>
        /// <param name="entries">The list of string map entries.</param>
        /// <returns>The maputils instance, for chaining calls.</returns>
        public MapUtilities<TKey, TValue> AddEntries(IMapEntry<TKey, TValue>[] entries)
        {
            foreach (IMapEntry<TKey, TValue> entry in entries)
            {
                AddEntry(entry);
            }

            return this;
        }

        /// <summary>
        /// Returns the settings as a dictionary.
        /// </summary>
        /// <returns>The dictionary that contains the settings.</returns>
        public Dictionary<TKey, TValue> AsDict()
        {
            return values;
        }

        /// <summary>
        /// Returns the settings as a list of string map entries.
        /// </summary>
        /// <returns>The settings, as a list of string map entries.</returns>
        public T[] AsArray<T>() where T : IMapEntry<TKey, TValue>, new()
        {
            List<T> entries = new List<T>();
            foreach (TKey key in values.Keys)
            {
                entries.Add(new T()
                {
                    key = key,
                    value = values[key]
                });
            }

            return entries.ToArray();
        }
    }
}

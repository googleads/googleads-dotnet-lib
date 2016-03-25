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

using Google.Api.Ads.AdWords.v201603;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Util.v201603 {
  /// <summary>
  /// Provides utility functions for creating and handling
  /// String_StringMapEntry[] fields.
  /// </summary>
  public class MapUtilities {
    /// <summary>
    /// Dictionary to hold the key-value pairs.
    /// </summary>
    Dictionary<string, string> values = new Dictionary<string, string>();

    /// <summary>
    /// Adds the specified key to the map.
    /// </summary>
    /// <param name="key">The settings key.</param>
    /// <param name="value">The settings value.</param>
    /// <returns>The maputils instance, for chaining calls.</returns>
    public MapUtilities Add(string key, string value) {
      values[key] = value;
      return this;
    }

    /// <summary>
    /// Adds the specified string map entry to the map.
    /// </summary>
    /// <param name="entry">The string map entry.</param>
    /// <returns>The maputils instance, for chaining calls.</returns>
    public MapUtilities AddEntry(String_StringMapEntry entry) {
      values[entry.key] = entry.value;
      return this;
    }

    /// <summary>
    /// Adds an array of string map entries to the map.
    /// </summary>
    /// <param name="entries">The list of string map entries.</param>
    /// <returns>The maputils instance, for chaining calls.</returns>
    public MapUtilities AddEntries(String_StringMapEntry[] entries) {
      foreach (String_StringMapEntry entry in entries) {
        AddEntry(entry);
      }
      return this;
    }

    /// <summary>
    /// Returns the settings as a list of string map entries.
    /// </summary>
    /// <returns>The settings, as a list of string map entries.</returns>
    public String_StringMapEntry[] AsArray() {
      List<String_StringMapEntry> entries = new List<String_StringMapEntry>();
      foreach (string key in values.Keys) {
        String_StringMapEntry entry = new String_StringMapEntry();
        entry.key = key;
        entry.value = values[key];
        entries.Add(entry);
      }
      return entries.ToArray();
    }

    /// <summary>
    /// Returns the settings as a dictionary.
    /// </summary>
    /// <returns>The dictionary that contains the settings.</returns>
    public Dictionary<string, string> AsDict() {
      return values;
    }
  }
}

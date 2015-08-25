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
using System.Text;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace Google.Api.Ads.Common.Lib {
  /// <summary>
  /// This class provides access to the call context, and hides the differences
  /// when the library is running as a web application or a normal application.
  /// </summary>
  public class ContextStore {
    /// <summary>
    /// Adds a key-value pair to the context store.
    /// </summary>
    /// <param name="key">The key for the value being stored.</param>
    /// <param name="value">The value being stored.</param>
    public static void AddKey(string key, object value) {
      if (HttpContext.Current != null) {
        HttpContext.Current.Items[key] = value;
      } else {
        CallContext.SetData(key, value);
      }
    }

    /// <summary>
    /// Removes a stored value from the context store.
    /// </summary>
    /// <param name="key">The key for the value to be removed.</param>
    public static void RemoveKey(string key) {
      if (HttpContext.Current != null) {
        HttpContext.Current.Items.Remove(key);
      } else {
        CallContext.FreeNamedDataSlot(key);
      }
    }

    /// <summary>
    /// Gets the value of an item stored in the context store.
    /// </summary>
    /// <param name="key">The key for which value should be retrieved.</param>
    /// <returns>The object's value, or null if the key is missing.</returns>
    public static object GetValue(string key) {
      if (HttpContext.Current != null) {
        if (HttpContext.Current.Items.Contains(key)) {
          return HttpContext.Current.Items[key];
        } else {
          return null;
        }
      } else {
        return CallContext.GetData(key);
      }
    }
  }
}

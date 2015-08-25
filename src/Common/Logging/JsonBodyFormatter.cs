// Copyright 2014, Google Inc. All Rights Reserved.
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
using System.Collections.Specialized;
using System.Web.Script.Serialization;

namespace Google.Api.Ads.Common.Logging {

  /// <summary>
  /// Formats a JSON trace message by masking out sensitive fields.
  /// </summary>
  public class JsonBodyFormatter : TraceFormatter {

    /// <summary>
    /// Masks the contents of the traced message.
    /// </summary>
    /// <param name="body">The message body.</param>
    /// <param name="keysToMask">The keys for which values should be masked
    /// in the message body.</param>
    /// <returns>
    /// The formatted message body.
    /// </returns>
    public override string MaskContents(string body, ISet<string> keysToMask) {
      JavaScriptSerializer serializer = new JavaScriptSerializer();
      Dictionary<string, string> jsonDict =
          serializer.Deserialize<Dictionary<string, string>>(body);

      foreach (string key in keysToMask) {
        if (jsonDict.ContainsKey(key)) {
          jsonDict[key] = MASK_PATTERN;
        }
      }

      return serializer.Serialize(jsonDict);
    }
  }
}
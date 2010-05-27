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

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace com.google.api.adwords.lib.util {
  /// <summary>
  /// Provides utility methods for handling media resources.
  /// </summary>
  public class MediaUtilities {
    /// <summary>
    /// Retrieves an asset from the web given its url.
    /// </summary>
    /// <param name="assetUrl">The url of the asset to be retrieved.</param>
    /// <returns>Asset data, as an array of bytes.</returns>
    public static byte[] GetAssetDataFromUrl(string assetUrl) {
      int bufferSize = 2 << 20;
      WebRequest request = HttpWebRequest.Create(assetUrl);
      WebResponse response = request.GetResponse();

      MemoryStream memStream = new MemoryStream();
      using (Stream responseStream = response.GetResponseStream()) {
        byte[] strmBuffer = new byte[bufferSize];

        int bytesRead = responseStream.Read(strmBuffer, 0, bufferSize);
        while (bytesRead != 0) {
          memStream.Write(strmBuffer, 0, bytesRead);
          bytesRead = responseStream.Read(strmBuffer, 0, bufferSize);
        }
      }
      return memStream.ToArray();
    }
  }
}

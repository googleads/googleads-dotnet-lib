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
using System.IO.Compression;
using System.Net;
using System.Text;

namespace Google.Api.Ads.Common.Util {
  /// <summary>
  /// Provides utility methods for handling media resources.
  /// </summary>
  public static class MediaUtilities {
    /// <summary>
    /// Retrieves an asset from the web given its url.
    /// </summary>
    /// <param name="assetUrl">The url of the asset to be retrieved.</param>
    /// <returns>Asset data, as an array of bytes.</returns>
    /// <exception cref="ArgumentNullException">Thrown if
    /// <paramref name="assetUrl"/> is null.</exception>
    public static byte[] GetAssetDataFromUrl(Uri assetUrl) {
      if (assetUrl == null) {
        throw new ArgumentNullException("assetUrl");
      }
      WebRequest request = HttpWebRequest.Create(assetUrl);
      WebResponse response = request.GetResponse();

      MemoryStream memStream = new MemoryStream();
      using (Stream responseStream = response.GetResponseStream()) {
        CopyStream(responseStream, memStream);
      }
      return memStream.ToArray();
    }

    /// <summary>
    /// Retrieves an asset from the web given its url.
    /// </summary>
    /// <param name="assetUrl">The url of the asset to be retrieved.</param>
    /// <returns>Asset data, as an array of bytes.</returns>
    /// <exception cref="ArgumentNullException">Thrown if
    /// <paramref name="assetUrl"/> is null.</exception>
    public static byte[] GetAssetDataFromUrl(string assetUrl) {
      if (string.IsNullOrEmpty(assetUrl)) {
        throw new ArgumentNullException("assetUrl");
      }
      return GetAssetDataFromUrl(new Uri(assetUrl));
    }

    /// <summary>
    /// Deflates data compressed in gzip format.
    /// </summary>
    /// <param name="gzipData">Data to be deflated.</param>
    /// <returns>Deflated data.</returns>
    /// <exception cref="ArgumentNullException">Thrown if
    /// <paramref name="gzipData"/> is null.</exception>
    public static byte[] DeflateGZipData(byte[] gzipData) {
      if (gzipData == null) {
        throw new ArgumentNullException("gzipData");
      }
      MemoryStream memStream = new MemoryStream();
      using (GZipStream gzipStream = new GZipStream(new MemoryStream(gzipData),
          CompressionMode.Decompress)) {
        CopyStream(gzipStream, memStream);
      }
      return memStream.ToArray();
    }

    /// <summary>
    /// Copies a stream from source to destination.
    /// </summary>
    /// <param name="sourceStream">Source stream.</param>
    /// <param name="targetStream">Destination stream.</param>
    /// <exception cref="ArgumentException">Thrown if source stream is not
    /// readable, or if the target stream is not writable.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="sourceStream"/> or
    /// <paramref name="targetStream"/> is null.</exception>
    public static void CopyStream(Stream sourceStream, Stream targetStream) {
      if (sourceStream == null) {
        throw new ArgumentNullException("sourceStream");
      }

      if (targetStream == null) {
        throw new ArgumentNullException("targetStream");
      }

      if (!sourceStream.CanRead) {
        throw new System.ArgumentException(CommonErrorMessages.SourceStreamIsNotReadable);
      }

      if (!targetStream.CanWrite) {
        throw new System.ArgumentException(CommonErrorMessages.TargetStreamIsNotWritable);
      }

      int bufferSize = 2 << 20;
      byte[] buffer = new byte[bufferSize];

      int bytesRead = 0;
      while ((bytesRead = sourceStream.Read(buffer, 0, bufferSize)) != 0) {
        targetStream.Write(buffer, 0, bytesRead);
      }
    }
  }
}

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

using Google.Api.Ads.Common.Lib;

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;

namespace Google.Api.Ads.Common.Util
{
    /// <summary>
    /// Provides utility methods for handling media resources.
    /// </summary>
    public static class MediaUtilities
    {
        /// <summary>
        /// Retrieves an asset from the web given its url.
        /// </summary>
        /// <param name="assetUrl">The url of the asset to be retrieved.</param>
        /// <param name="config">The application configuration instance.</param>
        /// <returns>Asset data, as an array of bytes.</returns>
        /// <exception cref="ArgumentNullException">Thrown if
        /// <paramref name="assetUrl"/> or <paramref name="config"/>is null.
        /// </exception>
        public static byte[] GetAssetDataFromUrl(Uri assetUrl, AppConfig config)
        {
            if (assetUrl == null)
            {
                throw new ArgumentNullException("assetUrl");
            }

            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            WebRequest request = HttpUtilities.BuildRequest(assetUrl.AbsoluteUri, "GET", config);
            WebResponse response = request.GetResponse();

            MemoryStream memStream = new MemoryStream();
            using (Stream responseStream = response.GetResponseStream())
            {
                responseStream.CopyTo(memStream);
            }

            return memStream.ToArray();
        }

        /// <summary>
        /// Retrieves an asset from the web given its url.
        /// </summary>
        /// <param name="assetUrl">The url of the asset to be retrieved.</param>
        /// <param name="config">The application configuration instance.</param>
        /// <returns>Asset data, as an array of bytes.</returns>
        /// <exception cref="ArgumentNullException">Thrown if
        /// <paramref name="assetUrl"/> or <paramref name="config" /> is null.
        /// </exception>
        public static byte[] GetAssetDataFromUrl(string assetUrl, AppConfig config)
        {
            return GetAssetDataFromUrl(new Uri(assetUrl), config);
        }

        /// <summary>
        /// Deflates data compressed in gzip format.
        /// </summary>
        /// <param name="gzipData">Data to be deflated.</param>
        /// <returns>Deflated data.</returns>
        /// <exception cref="ArgumentNullException">Thrown if
        /// <paramref name="gzipData"/> is null.</exception>
        public static byte[] DeflateGZipData(byte[] gzipData)
        {
            if (gzipData == null)
            {
                throw new ArgumentNullException("gzipData");
            }

            MemoryStream memStream = new MemoryStream();
            using (GZipStream gzipStream =
                new GZipStream(new MemoryStream(gzipData), CompressionMode.Decompress))
            {
                gzipStream.CopyTo(memStream);
            }

            return memStream.ToArray();
        }

        /// <summary>
        /// Gets the stream contents as string.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>Contents of the stream, as a string.</returns>
        public static string GetStreamContentsAsString(Stream stream)
        {
            string contents = "";
            using (StreamReader reader = new StreamReader(stream))
            {
                contents = reader.ReadToEnd();
            }

            return contents;
        }
    }
}

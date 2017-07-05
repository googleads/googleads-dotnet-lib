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

using System;
using System.Text;

namespace Google.Api.Ads.Common.Lib {

  /// <summary>
  /// An OAuth2 Service Account claimset.
  /// </summary>
  public class OAuth2JwtClaimset {

    /// <summary>
    /// Gets or sets the scope for the claimset.
    /// </summary>
    public string scope { get; set; }

    /// <summary>
    /// Gets or sets the audience for the claimset.
    /// </summary>
    public string audience { get; set; }

    /// <summary>
    /// Gets or sets the service account email for the claimset.
    /// </summary>
    public string serviceAccountEmail { get; set; }

    /// <summary>
    /// Gets or sets the email of the user for the claimset.
    /// </summary>
    public string impersonationEmail { get; set; }

    /// <summary>
    /// Gets or sets the timestamp for the claimset.
    /// </summary>
    public long timestamp { get; set; }

    /// <summary>
    /// Gets or sets the expiry for the claimset.
    /// </summary>
    public long expiry { get; set; }

    /// <summary>
    /// Serializes a JWT claimset into a JSON string.
    /// </summary>
    /// <returns>A JWT claimset JSON string</returns>
    public string ToJson() {
      StringBuilder sb = new StringBuilder();
      sb.Append("{")
        .AppendFormat("\"iss\":\"{0}\"", serviceAccountEmail)
        .AppendFormat(", \"scope\":\"{0}\"", scope)
        .AppendFormat(", \"aud\":\"{0}\"", audience)
        .AppendFormat(", \"exp\":{0}", expiry)
        .AppendFormat(", \"iat\":{0}", timestamp);

      if (!string.IsNullOrEmpty(impersonationEmail)) {
        sb.AppendFormat(", \"prn\":\"{0}\"", impersonationEmail);
      }

      sb.Append("}");
      return sb.ToString();
    }
  }
}

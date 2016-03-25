// Copyright 2012, Google Inc. All Rights Reserved.
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
using System.ComponentModel;
using System.Net;

namespace Google.Api.Ads.Common.Lib {

  /// <summary>
  /// Interface for common configuration properties.
  /// </summary>
  public interface AppConfig : ICloneable {

    /// <summary>
    /// Gets whether the credentials in the log file should be masked.
    /// </summary>
    bool MaskCredentials { get; }

    /// <summary>
    /// Gets or sets the OAuth2 server URL.
    /// </summary>
    /// <remarks>This property is primarily used only for testing purposes.
    /// </remarks>
    string OAuth2ServerUrl { get; set; }

    /// <summary>
    /// Gets or sets the OAuth2 mode.
    /// </summary>
    OAuth2Flow OAuth2Mode { get; set; }

    /// <summary>
    /// Gets or sets the OAuth2 access token.
    /// </summary>
    string OAuth2AccessToken { get; set; }

    /// <summary>
    /// Gets or sets the OAuth2 client id.
    /// </summary>
    string OAuth2ClientId { get; set; }

    /// <summary>
    /// Gets or sets the OAuth2 client secret.
    /// </summary>
    string OAuth2ClientSecret { get; set; }

    /// <summary>
    /// Gets or sets the OAuth2 redirect URI.
    /// </summary>
    /// <remarks>This key is applicable only when using OAuth2 web flow.
    /// </remarks>
    string OAuth2RedirectUri { get; set; }

    /// <summary>
    /// OAuth2 refresh token.
    /// </summary>
    /// <remarks>This key is applicable only when using OAuth2 web / application
    /// flow in offline mode.</remarks>
    string OAuth2RefreshToken { get; set; }

    /// <summary>
    /// Gets or sets the OAuth2 scope.
    /// </summary>
    string OAuth2Scope { get; set; }

    /// <summary>
    /// Gets or sets the OAuth2 service account email.
    /// </summary>
    /// <remarks>This key is applicable only when using OAuth2 service accounts.
    /// </remarks>
    string OAuth2ServiceAccountEmail { get; set; }

    /// <summary>
    /// Gets or sets the OAuth2 prn email.
    /// </summary>
    /// <remarks>This key is applicable only when using OAuth2 service accounts.
    /// </remarks>
    string OAuth2PrnEmail { get; set; }

    /// <summary>
    /// Gets or sets the OAuth2 certificate path.
    /// </summary>
    /// <remarks>This key is applicable only when using OAuth2 service accounts.
    /// </remarks>
    string OAuth2CertificatePath { get; set; }

    /// <summary>
    /// Gets or sets the OAuth2 certificate password.
    /// </summary>
    /// <remarks>This key is applicable only when using OAuth2 service accounts.
    /// </remarks>
    string OAuth2CertificatePassword { get; set; }

    /// <summary>
    /// Gets or sets whether usage information of various client library
    /// features should be included in the user agent.
    /// </summary>
    bool IncludeUtilitiesInUserAgent { get; set; }

    /// <summary>
    /// Occurs when a property is changed.
    /// </summary>
    event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Gets the web proxy to be used with the services.
    /// </summary>
    IWebProxy Proxy { get; set; }

    /// <summary>
    /// Gets or sets the number of times to retry a call if an API call fails
    /// and can be retried.
    /// </summary>
    int RetryCount { get; set; }

    /// <summary>
    /// Gets the signature for this library.
    /// </summary>
    string Signature { get; }

    /// <summary>
    /// Gets or sets the timeout for Ads services in milliseconds.
    /// </summary>
    int Timeout { get; set; }

    /// <summary>
    /// Gets or sets whether gzip compression should be turned on for SOAP
    /// requests and responses.
    /// </summary>
    bool EnableGzipCompression { get; set; }

    /// <summary>
    /// Gets the number of seconds after Jan 1, 1970, 00:00:00
    /// </summary>
    long UnixTimestamp { get; }

    /// <summary>
    /// Gets or sets whether SOAP listener extensions should be turned 
    /// on for logging.
    /// </summary>
    bool EnableSoapExtension { get; set; }

    /// <summary>
    /// Gets the default OAuth2 scope.
    /// </summary>
    string GetDefaultOAuth2Scope();

    /// <summary>
    /// Gets the user agent text.
    /// </summary>
    /// <returns>The user agent.</returns>
    string GetUserAgent();
  }
}

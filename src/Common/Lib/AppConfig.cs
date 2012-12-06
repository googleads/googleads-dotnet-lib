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

// Author: api.anash@gmail.com (Anash P. Oommen)

using System;
using System.ComponentModel;
using System.Net;

namespace Google.Api.Ads.Common.Lib {
  /// <summary>
  /// Interface for common configuration properties.
  /// </summary>
  public interface AppConfig {
    /// <summary>
    /// Gets whether the SOAP logs that correspond to an error should be logged.
    /// </summary>
    bool LogErrorsOnly { get; }

    /// <summary>
    /// Gets the path to which the SOAP logs are to be saved.
    /// </summary>
    string LogPath { get; }

    /// <summary>
    /// Gets whether the SOAP logs should be written to console.
    /// </summary>
    bool LogToConsole { get; }

    /// <summary>
    /// Gets whether the SOAP logs should be written to file.
    /// </summary>
    bool LogToFile { get; }

    /// <summary>
    /// Gets whether the credentials in the log file should be masked.
    /// </summary>
    bool MaskCredentials { get; }

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
    string OAuth2RedirectUri { get; set; }

    /// <summary>
    /// OAuth2 refresh token.
    /// </summary>
    string OAuth2RefreshToken { get; set; }

    /// <summary>
    /// Gets or sets the OAuth2 scope.
    /// </summary>
    string OAuth2Scope { get; set; }

    /// <summary>
    /// Gets or sets the OAuth callback url.
    /// </summary>
    string OAuthCallbackUrl { get; set; }

    /// <summary>
    /// Gets or sets the OAuth consumer key.
    /// </summary>
    string OAuthConsumerKey { get; set; }

    /// <summary>
    /// Gets or sets the OAuth consumer secret.
    /// </summary>
    string OAuthConsumerSecret { get; set; }

    /// <summary>
    /// Gets or sets the OAuth consumer secret.
    /// </summary>
    string OAuthScope { get; set; }

    /// <summary>
    /// Occurs when a property is changed.
    /// </summary>
    event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Gets the web proxy to be used with the services.
    /// </summary>
    IWebProxy Proxy { get; }

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
    /// Gets the number of seconds after Jan 1, 1970, 00:00:00
    /// </summary>
    long UnixTimestamp { get; }
  }
}

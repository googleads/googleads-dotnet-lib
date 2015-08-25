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

using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.Dfa.Lib;

namespace Google.Api.Ads.Dfa.Util {
  /// <summary>
  /// Utility class to assist in generating login token.
  /// </summary>
  public class LoginUtil {
    /// <summary>
    /// The login token cache.
    /// </summary>
    private static LoginTokenCache tokenCache = new DefaultLoginTokenCache();

    /// <summary>
    /// Gets or sets the cache.
    /// </summary>
    public static LoginTokenCache Cache {
      get {
        return LoginUtil.tokenCache;
      }
      set {
        LoginUtil.tokenCache = value;
      }
    }

    /// <summary>
    /// Gets a login token for authenticating DFA API calls.
    /// </summary>
    /// <param name="user">The user for which token is generated.</param>
    /// <param name="serviceVersion">The service version.</param>
    /// <returns>A token which may be used for future API calls.</returns>
    public static UserToken GetAuthenticationToken(AdsUser user, string serviceVersion) {
      DfaAppConfig config = (DfaAppConfig) user.Config;

      if (string.IsNullOrEmpty(config.DfaUserName)) {
        throw new ArgumentNullException(DfaErrorMessages.UserNameCannotBeEmpty);
      }

      UserToken userToken = tokenCache.GetToken(config.DfaUserName);

      if (userToken == null) {
        lock (typeof(LoginUtil)) {
          userToken = tokenCache.GetToken(config.DfaUserName);
          if (userToken == null) {
            userToken = GenerateAuthenticationToken(user, serviceVersion);
            tokenCache.AddToken(config.DfaUserName, userToken);
          }
        }
      }
      return userToken;
    }

    /// <summary>
    /// Generates a login token for authenticating DFA API calls.
    /// </summary>
    /// <param name="user">The user for which token is generated.</param>
    /// <param name="serviceVersion">The service version.</param>
    /// <returns>A token which may be used for future API calls.</returns>
    private static UserToken GenerateAuthenticationToken(AdsUser user, string serviceVersion) {
      DfaAppConfig config = (DfaAppConfig) user.Config;
      if (!String.IsNullOrEmpty(config.DfaAuthToken)) {
        return new UserToken(config.DfaUserName, config.DfaAuthToken);
      }

      String dfaUserName = config.DfaUserName;
      String dfaPassword = config.DfaPassword;

      if (config.AuthorizationMethod == DfaAuthorizationMethod.LoginService) {
        if (string.IsNullOrEmpty(dfaUserName)) {
          throw new ArgumentNullException(DfaErrorMessages.UserNameCannotBeEmpty);
        }
        if (string.IsNullOrEmpty(dfaPassword)) {
          throw new ArgumentNullException(DfaErrorMessages.PasswordCannotBeEmpty);
        }
      } else if (config.AuthorizationMethod == DfaAuthorizationMethod.OAuth2) {
        // DFA password should not be set when using OAuth2
        dfaPassword = "";
      }

      try {
        DfaServiceSignature loginServiceSignature = new DfaServiceSignature(serviceVersion,
              "LoginRemoteService");
        AdsClient loginService = user.GetService(loginServiceSignature, config.DfaApiServer);
        object userProfile = loginService.GetType().GetMethod("authenticate").Invoke(
            loginService, new object[] {dfaUserName, dfaPassword});
        return new UserToken(
            userProfile.GetType().GetProperty("name").GetValue(userProfile, null).ToString(),
            userProfile.GetType().GetProperty("token").GetValue(userProfile, null).ToString());
      } catch (Exception e) {
        throw new DfaException("Failed to authenticate user. See inner exception for details.", e);
      }
    }
  }
}

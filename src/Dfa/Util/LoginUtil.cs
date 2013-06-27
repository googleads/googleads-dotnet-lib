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

using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.Dfa.Lib;

namespace Google.Api.Ads.Dfa.Util {
  /// <summary>
  /// Utility class to assist in generating login token.
  /// </summary>
  public class LoginUtil {
    /// <summary>
    /// Generates an auth token using login service.
    /// </summary>
    /// <param name="signature">Signature of the service being created.</param>
    /// <param name="user">The user for which the service is being created.
    /// <param name="serverUrl">The server to which the API calls should be
    /// made.</param>
    /// </param>
    /// <returns>A token which may be used for future API calls.</returns>
    public static UserToken GetAuthenticationToken(DfaAppConfig config, ServiceSignature signature,
        AdsUser user, Uri serverUrl) {
      if (!String.IsNullOrEmpty(config.DfaAuthToken)) {
        return new UserToken(config.DfaUserName, config.DfaAuthToken);
      }
      if (string.IsNullOrEmpty(config.DfaUserName)) {
        throw new ArgumentNullException(DfaErrorMessages.UserNameCannotBeEmpty);
      }
      if (string.IsNullOrEmpty(config.DfaPassword)) {
        throw new ArgumentNullException(DfaErrorMessages.PasswordCannotBeEmpty);
      }

      try {
        DfaServiceSignature loginServiceSignature = new DfaServiceSignature(signature.Version,
              "LoginRemoteService");
        AdsClient loginService = user.GetService(loginServiceSignature, config.DfaApiServer);
        object userProfile = loginService.GetType().GetMethod("authenticate").Invoke(
            loginService, new object[] {config.DfaUserName, config.DfaPassword});
        return new UserToken(
            userProfile.GetType().GetProperty("name").GetValue(userProfile, null).ToString(),
            userProfile.GetType().GetProperty("token").GetValue(userProfile, null).ToString());
      } catch (Exception ex) {
        throw new DfaException("Failed to authenticate user. See inner exception for details.", ex);
      }
    }
  }
}

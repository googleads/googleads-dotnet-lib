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

// Author: Chris Seeley (https://github.com/Narwalter)

using NUnit.Framework;

using Google.Api.Ads.Common.Lib;

using System;

namespace Google.Api.Ads.Common.Tests.Lib {
  /// <summary>
  /// Tests for OAuth2JwtClaimset class.
  /// </summary>
  public class OAuth2JwtClaimsetTests {

    /// <summary>
    /// Tests ToJson with impersonation.
    /// </summary>
    [Test]
    public void TestToJsonWithImpersonationHasPrn() {
      OAuth2JwtClaimset claimset = new OAuth2JwtClaimset();
      claimset.serviceAccountEmail = "serviceAccountEmail";
      claimset.scope = "scope";
      claimset.audience = "audience";
      claimset.expiry = 1;
      claimset.timestamp = 2;
      claimset.impersonationEmail = "impersonationEmail";

      Assert.AreEqual("{\"iss\":\"serviceAccountEmail\", \"scope\":\"scope\", " +
          "\"aud\":\"audience\", \"exp\":1, \"iat\":2, \"prn\":\"impersonationEmail\"}",
          claimset.ToJson());
    }

    /// <summary>
    /// Tests ToJson without impersonation.
    /// </summary>
    [Test]
    public void TestToJsonWithoutImpersonationDoesNotHavePrn() {
      OAuth2JwtClaimset claimset = new OAuth2JwtClaimset();
      claimset.serviceAccountEmail = "serviceAccountEmail";
      claimset.scope = "scope";
      claimset.audience = "audience";
      claimset.expiry = 1;
      claimset.timestamp = 2;

      Assert.AreEqual("{\"iss\":\"serviceAccountEmail\", \"scope\":\"scope\", " +
          "\"aud\":\"audience\", \"exp\":1, \"iat\":2}", claimset.ToJson());
    }
  }
}

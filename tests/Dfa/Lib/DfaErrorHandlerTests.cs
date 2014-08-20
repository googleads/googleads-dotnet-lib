// Copyright 2013, Google Inc. All Rights Reserved.
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

using NUnit.Framework;

using System;
using Google.Api.Ads.Dfa.Lib;
using Google.Api.Ads.Dfa.v1_20;
using Google.Api.Ads.Dfa.Util;

namespace Google.Api.Ads.Dfa.Tests.Lib {
  /// <summary>
  /// Tests for DfaErrorHandler class.
  /// </summary>
  class DfaErrorHandlerTests {
    private static readonly ErrorCode TOKEN_EXPIRED_CODE = ErrorCode.FromCode(4);
    private const string TOKEN_EXPIRED_MESSAGE = "Authentication token has expired.";

    private static readonly ErrorCode RANDOM_ERROR_CODE = ErrorCode.FromCode(20);
    private const string RANDOM_ERROR_MESSAGE = "RANDOM_ERROR_MESSAGE";

    private const string USER_NAME = "USER_NAME";
    private const string PASSWORD = "PASSWORD";

    private DfaUser user;
    private AdRemoteService adRemoteService;

    private readonly UserToken USER_TOKEN = new UserToken(USER_NAME, PASSWORD);

    /// <summary>
    /// Inits this instance.
    /// </summary>
    [SetUp]
    public void Init() {
      user = new DfaUser();
      adRemoteService = (AdRemoteService) user.GetService(DfaService.v1_20.AdRemoteService);
    }

    /// <summary>
    /// Tests if TokenExpiredError is recognized correctly.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestIsTokenExpiredError() {
      DfaException exception = null;

      exception = new DfaApiException(TOKEN_EXPIRED_CODE, TOKEN_EXPIRED_MESSAGE);
      Assert.IsTrue(DfaErrorHandler.IsTokenExpiredError(exception));

      exception = new DfaApiException(TOKEN_EXPIRED_CODE, RANDOM_ERROR_MESSAGE);
      Assert.IsFalse(DfaErrorHandler.IsTokenExpiredError(exception));

      exception = new DfaApiException(RANDOM_ERROR_CODE, TOKEN_EXPIRED_MESSAGE);
      Assert.IsFalse(DfaErrorHandler.IsTokenExpiredError(exception));
    }

    /// <summary>
    /// Tests if retry logic is working fine.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestShouldRetry() {
      DfaErrorHandler errorHandler = null;

      // Should not retry if RetryCount == 0
      errorHandler = new DfaErrorHandler(adRemoteService);
      user.Config.RetryCount = 0;
      Assert.IsFalse(errorHandler.ShouldRetry(new DfaCredentialsExpiredException(USER_TOKEN)));

      // Should not retry if exception is not DfaCredentialsExpiredException.
      errorHandler = new DfaErrorHandler(adRemoteService);
      user.Config.RetryCount = 1;
      Assert.IsFalse(errorHandler.ShouldRetry(new ArgumentNullException()));

      // Should retry if exception is DfaCredentialsExpiredException and
      // RetryCount > 0
      errorHandler = new DfaErrorHandler(adRemoteService);
      user.Config.RetryCount = 1;
      Assert.IsTrue(errorHandler.ShouldRetry(new DfaCredentialsExpiredException(USER_TOKEN)));
    }

    /// <summary>
    /// Tests if prepare for retry works as expected.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestPrepareForRetry() {
      LoginUtil.Cache.AddToken(USER_NAME, USER_TOKEN);
      (user.Config as DfaAppConfig).DfaAuthToken = PASSWORD;
      adRemoteService.Token = USER_TOKEN;
      user.Config.RetryCount = 1;

      // PrepareForRetry should reset all relevant values when presented with
      // correct values.
      DfaErrorHandler errorHandler = new DfaErrorHandler(adRemoteService);
      errorHandler.PrepareForRetry(new DfaCredentialsExpiredException(USER_TOKEN));
      Assert.IsNull(LoginUtil.Cache.GetToken(USER_NAME));
      Assert.IsNull(adRemoteService.Token);
      Assert.IsNull((user.Config as DfaAppConfig).DfaAuthToken);

      // Indirectly test retryCount. This should become 1 after
      // PrepareForRetry() and another attempt to retry should fail.
      Assert.IsFalse(errorHandler.ShouldRetry(new DfaCredentialsExpiredException(USER_TOKEN)));

      // PrepareRetry should rethrow any exception that is not
      // DfaCredentialsExpiredException
      errorHandler = new DfaErrorHandler(adRemoteService);

      Exception ex = new Exception();
      try {
        errorHandler.PrepareForRetry(ex);
        Assert.Fail("Should not prepare for retry if exception is not " +
            "DfaCredentialsExpiredException.");
      } catch (Exception e) {
        Assert.AreSame(ex, e);
      }
    }
  }
}

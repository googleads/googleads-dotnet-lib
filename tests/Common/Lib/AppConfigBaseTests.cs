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

using Google.Api.Ads.Common.Tests.Mocks;

using NUnit.Framework;

using System.Collections.Generic;
using System.Net;

namespace Google.Api.Ads.Common.Tests.Lib {

  /// <summary>
  /// Coverage tests for AppConfigBase class.
  /// </summary>
  public class AppConfigBaseTests {

    /// <summary>
    /// The dictionary to hold the test data.
    /// </summary>
    private Dictionary<string, string> dictSettings = new Dictionary<string, string>() {
      { "ProxyServer", "http://localhost/" },
      { "ProxyUser", "ProxyUser" },
      { "ProxyPassword", "ProxyPassword" },
      { "ProxyDomain", "ProxyDomain" },
      { "MaskCredentials", "false" },
      { "Timeout", "20" },
      { "RetryCount", "5" },
      { "OAuth2ClientId", "OAuth2ClientId" },
      { "OAuth2ClientSecret", "OAuth2ClientSecret" },
      { "OAuth2ServiceAccountEmail", "OAuth2ServiceAccountEmail" },
      { "OAuth2PrnEmail", "OAuth2PrnEmail" },
      { "OAuth2AccessToken", "OAuth2AccessToken" },
      { "OAuth2RefreshToken", "OAuth2RefreshToken" },
      { "OAuth2Scope", "OAuth2Scope" },
      { "OAuth2RedirectUri", "OAuth2RedirectUri" },
      { "OAuth2Mode", "SERVICE_ACCOUNT" },
      { "EnableGzipCompression", "false" },
    };

    /// <summary>
    /// Tests the overloaded constructors.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestReadSettings() {
      MockAppConfig config = new MockAppConfig();
      config.MockReadSettings(dictSettings);
      NetworkCredential credential = (NetworkCredential) config.Proxy.Credentials;
      Assert.AreEqual(dictSettings["ProxyUser"].ToString(), credential.UserName);
      Assert.AreEqual(dictSettings["ProxyPassword"].ToString(), credential.Password);
      Assert.AreEqual(dictSettings["ProxyDomain"].ToString(), credential.Domain);
      Assert.AreEqual(bool.Parse(dictSettings["MaskCredentials"].ToString()),
          config.MaskCredentials);
      Assert.AreEqual(int.Parse(dictSettings["Timeout"].ToString()), config.Timeout);
      Assert.AreEqual(int.Parse(dictSettings["RetryCount"].ToString()), config.RetryCount);

      Assert.AreEqual(dictSettings["OAuth2ClientId"].ToString(), config.OAuth2ClientId);
      Assert.AreEqual(dictSettings["OAuth2ClientSecret"].ToString(), config.OAuth2ClientSecret);
      Assert.AreEqual(dictSettings["OAuth2PrnEmail"].ToString(), config.OAuth2PrnEmail);
      Assert.AreEqual(dictSettings["OAuth2AccessToken"].ToString(), config.OAuth2AccessToken);
      Assert.AreEqual(dictSettings["OAuth2RefreshToken"].ToString(), config.OAuth2RefreshToken);
      Assert.AreEqual(dictSettings["OAuth2Scope"].ToString(), config.OAuth2Scope);
      Assert.AreEqual(dictSettings["OAuth2RedirectUri"].ToString(), config.OAuth2RedirectUri);
      Assert.AreEqual(dictSettings["OAuth2Mode"].ToString(), config.OAuth2Mode.ToString());

      Assert.AreEqual(bool.Parse(dictSettings["EnableGzipCompression"].ToString()),
          config.EnableGzipCompression);

      // Since SettingWithNonEmptyDefaultValue is missing in dictSettings, default value should be
      // retained.
      Assert.AreEqual(MockAppConfig.TEST_DEFAULT_VALUE, config.SettingWithNonEmptyDefaultValue);
    }

    /// <summary>
    /// Tests the overloaded constructors.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestReadSettingsForDefaults() {
      Assert.DoesNotThrow(delegate() {
        MockAppConfig config = new MockAppConfig();
        config.MockReadSettings(null);
      });
    }

    /// <summary>
    /// Tests the timeout property setters and getters.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestTimeout() {
      MockAppConfig config = new MockAppConfig();
      config.Timeout = 20;
      Assert.AreEqual(20, config.Timeout);
    }

    /// <summary>
    /// Tests the retry count property setters and getters.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestRetryCount() {
      MockAppConfig config = new MockAppConfig();
      config.RetryCount = 20;
      Assert.AreEqual(20, config.RetryCount);
    }

    /// <summary>
    /// Tests the signature property getter.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestSignature() {
      MockAppConfig config = new MockAppConfig();
      Assert.NotNull(config.Signature);
    }
  }
}

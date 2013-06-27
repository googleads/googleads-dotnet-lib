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
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Google.Api.Ads.Common.Lib;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Google.Api.Ads.Common.Tests.Mocks;
using System.Collections;
using System.Net;

namespace Google.Api.Ads.Common.Tests.Lib {
  /// <summary>
  /// Coverage tests for AppConfigBase class.
  /// </summary>
  public class AppConfigBaseTests {
    /// <summary>
    /// The hashtable to hold the test data.
    /// </summary>
    private Hashtable tblSettings;

    /// <summary>
    /// Inits this instance.
    /// </summary>
    [SetUp]
    [Category("Small")]
    public void Init() {
      tblSettings = new Hashtable();
      tblSettings.Add("LogPath", "Test LogPath");
      tblSettings.Add("LogToFile", "false");
      tblSettings.Add("LogErrorsOnly", "false");
      tblSettings.Add("ProxyServer", "http://localhost/");
      tblSettings.Add("ProxyUser", "ProxyUser");
      tblSettings.Add("ProxyPassword", "ProxyPassword");
      tblSettings.Add("ProxyDomain", "ProxyDomain");
      tblSettings.Add("MaskCredentials", "false");
      tblSettings.Add("Timeout", "20");
      tblSettings.Add("RetryCount", "5");

      tblSettings.Add("OAuth2ClientId", "OAuth2ClientId");
      tblSettings.Add("OAuth2ClientSecret", "OAuth2ClientSecret");
      tblSettings.Add("OAuth2ServiceAccountEmail", "OAuth2ServiceAccountEmail");
      tblSettings.Add("OAuth2PrnEmail", "OAuth2PrnEmail");
      tblSettings.Add("OAuth2JwtCertificatePath", "OAuth2JwtCertificatePath");
      tblSettings.Add("OAuth2JwtCertificatePassword", "OAuth2JwtCertificatePassword");
      tblSettings.Add("OAuth2AccessToken", "OAuth2AccessToken");
      tblSettings.Add("OAuth2RefreshToken", "OAuth2RefreshToken");
      tblSettings.Add("OAuth2Scope", "OAuth2Scope");
      tblSettings.Add("OAuth2RedirectUri", "OAuth2RedirectUri");
      tblSettings.Add("OAuth2Mode", "SERVICE_ACCOUNT");

      tblSettings.Add("Email", "Email");
      tblSettings.Add("Password", "Password");
      tblSettings.Add("AuthToken", "AuthToken");
      tblSettings.Add("EnableGzipCompression", "false");
    }

    /// <summary>
    /// Tests the overloaded constructors.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestReadSettings() {
      MockAppConfig config = new MockAppConfig();
      config.MockReadSettings(tblSettings);
      Assert.AreEqual(tblSettings["LogPath"], config.LogPath);
      Assert.AreEqual(bool.Parse(tblSettings["LogToFile"].ToString()), config.LogToFile);
      Assert.AreEqual(bool.Parse(tblSettings["LogErrorsOnly"].ToString()), config.LogErrorsOnly);
      NetworkCredential credential = (NetworkCredential) config.Proxy.Credentials;
      Assert.AreEqual(tblSettings["ProxyUser"].ToString(), credential.UserName);
      Assert.AreEqual(tblSettings["ProxyPassword"].ToString(), credential.Password);
      Assert.AreEqual(tblSettings["ProxyDomain"].ToString(), credential.Domain);
      Assert.AreEqual(bool.Parse(tblSettings["MaskCredentials"].ToString()),
          config.MaskCredentials);
      Assert.AreEqual(int.Parse(tblSettings["Timeout"].ToString()), config.Timeout);
      Assert.AreEqual(int.Parse(tblSettings["RetryCount"].ToString()), config.RetryCount);

      Assert.AreEqual(tblSettings["OAuth2ClientId"].ToString(), config.OAuth2ClientId);
      Assert.AreEqual(tblSettings["OAuth2ClientSecret"].ToString(), config.OAuth2ClientSecret);
      Assert.AreEqual(tblSettings["OAuth2ServiceAccountEmail"].ToString(),
          config.OAuth2ServiceAccountEmail);
      Assert.AreEqual(tblSettings["OAuth2PrnEmail"].ToString(), config.OAuth2PrnEmail);
      Assert.AreEqual(tblSettings["OAuth2AccessToken"].ToString(), config.OAuth2AccessToken);
      Assert.AreEqual(tblSettings["OAuth2RefreshToken"].ToString(), config.OAuth2RefreshToken);
      Assert.AreEqual(tblSettings["OAuth2Scope"].ToString(), config.OAuth2Scope);
      Assert.AreEqual(tblSettings["OAuth2RedirectUri"].ToString(), config.OAuth2RedirectUri);
      Assert.AreEqual(tblSettings["OAuth2JwtCertificatePath"].ToString(),
          config.OAuth2CertificatePath);
      Assert.AreEqual(tblSettings["OAuth2JwtCertificatePassword"].ToString(),
          config.OAuth2CertificatePassword);
      Assert.AreEqual(tblSettings["OAuth2Mode"].ToString(), config.OAuth2Mode.ToString());

      Assert.AreEqual(tblSettings["Email"].ToString(), config.Email);
      Assert.AreEqual(tblSettings["Password"].ToString(), config.Password);
      Assert.AreEqual(tblSettings["AuthToken"].ToString(), config.AuthToken);
      Assert.AreEqual(bool.Parse(tblSettings["EnableGzipCompression"].ToString()),
          config.EnableGzipCompression);
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

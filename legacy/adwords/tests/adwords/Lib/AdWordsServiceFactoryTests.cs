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

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.AdWords.Lib;
using NUnit.Framework;


namespace Google.Api.Ads.AdWords.Tests.Lib {
  /// <summary>
  /// Tests for the AdWordsServiceFactory
  /// </summary>
  [TestFixture]
  class AdWordsServiceFactoryTests {
    private const string DEPRECATED_CLIENT_LOGIN_VERSION = "v201402";
    private const string SUPPORTED_CLIENT_LOGIN_VERSION = "v201309";
    private const string TEST_SERVICE = "MediaService";
    private const string TEST_GROUP_NAME = "express";

    private AdWordsServiceFactory serviceFactory;
    private ServiceSignature serviceSignature;
    private Uri testUri;

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public AdWordsServiceFactoryTests() {

    }

    [SetUp]
    public void Init() {
      serviceFactory = new AdWordsServiceFactory();
      testUri = new Uri("https://google.com");
    }

    /// <summary>
    /// Test that creating a service using ClientLogin in a deprecated version
    /// throws an exception
    /// </summary>
    [Test]
    [ExpectedException(typeof(AdWordsException))]
    public void TestCreateServiceClientLoginDeprecated() {
      ConfigureForClientLogin();
      serviceSignature = new AdWordsServiceSignature(DEPRECATED_CLIENT_LOGIN_VERSION,
          TEST_SERVICE, TEST_GROUP_NAME);
      serviceFactory.CreateService(serviceSignature, new AdWordsUser(), testUri);
    }

    /// <summary>
    /// Test that creating a service using ClientLogin in a supported version does not
    /// throw an exception
    /// </summary>
    [Test]
    public void TestCreateServiceClientLoginSupported() {
      ConfigureForClientLogin();
      serviceSignature = new AdWordsServiceSignature(SUPPORTED_CLIENT_LOGIN_VERSION,
          TEST_SERVICE, TEST_GROUP_NAME);
      serviceFactory.CreateService(serviceSignature, new AdWordsUser(), testUri);
    }

    /// <summary>
    /// Test creating a service using OAuth2 in a version where ClientLogin is supported
    /// </summary>
    [Test]
    public void TestCreateServiceOAuth2ClientLoginSupported() {
      ConfigureForOAuth2();
      serviceSignature = new AdWordsServiceSignature(SUPPORTED_CLIENT_LOGIN_VERSION,
          TEST_SERVICE, TEST_GROUP_NAME);
      serviceFactory.CreateService(serviceSignature, new AdWordsUser(), testUri);
    }

    /// <summary>
    /// Test creating a service using OAuth2 in a version where ClientLogin is deprecated
    /// </summary>
    [Test]
    public void TestCreateServiceOAuth2ClientLoginDeprecated() {
      ConfigureForOAuth2();
      serviceSignature = new AdWordsServiceSignature(DEPRECATED_CLIENT_LOGIN_VERSION,
          TEST_SERVICE, TEST_GROUP_NAME);
      serviceFactory.CreateService(serviceSignature, new AdWordsUser(), testUri);
    }

    private void ConfigureForOAuth2() {
      AdWordsAppConfig config = new AdWordsAppConfig();
      config.AuthorizationMethod = AdWordsAuthorizationMethod.OAuth2;
      serviceFactory.Config = config;
    }

    private void ConfigureForClientLogin() {
      AdWordsAppConfig config = new AdWordsAppConfig();
      config.AuthorizationMethod = AdWordsAuthorizationMethod.ClientLogin;
      serviceFactory.Config = config;
    }
  }
}

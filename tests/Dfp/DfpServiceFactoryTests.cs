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
using Google.Api.Ads.Dfp.Lib;
using NUnit.Framework;

namespace Google.Api.Ads.Dfp.Tests {

  /// <summary>
  /// Tests for the DfpServiceFactory
  /// </summary>
  [TestFixture]
  class DfpServiceFactoryTests {

    private const string DEPRECATED_CLIENT_LOGIN_VERSION = "v201403";
    private const string SUPPORTED_CLIENT_LOGIN_VERSION = "v201311";
    private const string TEST_SERVICE = "LineItemService";

    private DfpServiceFactory serviceFactory;
    private ServiceSignature serviceSignature;

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public DfpServiceFactoryTests() {

    }

    [SetUp]
    public void Init() {
      serviceFactory = new DfpServiceFactory();
    }

    /// <summary>
    /// Test that creating a service using ClientLogin in a deprecated version
    /// throws an exception
    /// </summary>
    [Test]
    [ExpectedException(typeof(DfpException))]
    public void TestCreateServiceClientLoginDeprecated() {
      ConfigureForClientLogin();
      serviceSignature = new DfpServiceSignature(DEPRECATED_CLIENT_LOGIN_VERSION, TEST_SERVICE);
      serviceFactory.CreateService(serviceSignature, new DfpUser(), null);
    }

    /// <summary>
    /// Test that creating a service using ClientLogin in a supported version does not
    /// throw an exception
    /// </summary>
    [Test]
    public void TestCreateServiceClientLoginSupported() {
      ConfigureForClientLogin();
      serviceSignature = new DfpServiceSignature(SUPPORTED_CLIENT_LOGIN_VERSION, TEST_SERVICE);
      serviceFactory.CreateService(serviceSignature, new DfpUser(), null);
    }

    /// <summary>
    /// Test creating a service using OAuth2 in a version where ClientLogin is supported
    /// </summary>
    [Test]
    public void TestCreateServiceOAuth2ClientLoginSupported() {
      ConfigureForOAuth2();
      serviceSignature = new DfpServiceSignature(SUPPORTED_CLIENT_LOGIN_VERSION, TEST_SERVICE);
      serviceFactory.CreateService(serviceSignature, new DfpUser(), null);
    }

    /// <summary>
    /// Test creating a service using OAuth2 in a version where ClientLogin is deprecated
    /// </summary>
    [Test]
    public void TestCreateServiceOAuth2ClientLoginDeprecated() {
      ConfigureForOAuth2();
      serviceSignature = new DfpServiceSignature(DEPRECATED_CLIENT_LOGIN_VERSION, TEST_SERVICE);
      serviceFactory.CreateService(serviceSignature, new DfpUser(), null);
    }

    private void ConfigureForOAuth2() {
      DfpAppConfig config = new DfpAppConfig();
      config.AuthorizationMethod = DfpAuthorizationMethod.OAuth2;
      serviceFactory.Config = config;
    }

    private void ConfigureForClientLogin() {
      DfpAppConfig config = new DfpAppConfig();
      config.AuthorizationMethod = DfpAuthorizationMethod.ClientLogin;
      serviceFactory.Config = config;
    }
  }
}

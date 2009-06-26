// Copyright 2009, Google Inc. All Rights Reserved.
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

using com.google.api.adwords.lib;
using com.google.api.adwords.lib.util;
using com.google.api.adwords.v13;
using com.google.api.adwords.v200906;
using com.google.api.adwords.v200906.AdGroupAdService;

using NUnit.Framework;

using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Reflection;
using System.Web.Services.Protocols;
using System.Xml;

namespace com.google.api.adwords.tests {
  /// <summary>
  /// UnitTests for <see cref="AdWordsUser"/> class.
  /// </summary>
  [TestFixture]
  public class AdWordsUserTests {
    /// <summary>
    /// AdWordsUser object to be used for test cases.
    /// </summary>
    AdWordsUser user;

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      user = new AdWordsUser();
    }

    /// <summary>
    /// Test if <see cref="AdWordsUser"/> class creates a v200906 service correctly.
    /// </summary>
    [Test]
    public void TestV200906ServiceCreation() {
      AdGroupAdService service =
          (AdGroupAdService) user.GetService(ApiServices.v200906.AdGroupAdService);
      Assert.NotNull(service, "AdWordsUser could not create v200906.AdGroupAdService.");

      // Check if RequestHeaders have the correct values.
      Assert.NotNull(service.RequestHeader, "RequestHeader must not be null.");
      Assert.IsFalse(string.IsNullOrEmpty(service.RequestHeader.authToken),
          "AuthToken must not be null.");
      if (!string.IsNullOrEmpty(ApplicationConfiguration.clientEmail)) {
        Assert.AreEqual(service.RequestHeader.clientEmail.ToLower(),
            ApplicationConfiguration.clientEmail.ToLower());
      }
      if (!string.IsNullOrEmpty(ApplicationConfiguration.clientCustomerId)) {
        Assert.AreEqual(service.RequestHeader.clientCustomerId.ToLower(),
            ApplicationConfiguration.clientCustomerId.ToLower());
      }

      // Check if the service url is correct.
      Assert.That(string.Compare(ApplicationConfiguration.urlV200906 +
        "/api/adwords/cm/v200906/AdGroupAdService", service.Url, true) == 0,
        "Service url should be " + ApplicationConfiguration.urlV200906 +
        "/api/adwords/cm/v200906/AdGroupAdService");

      // Check if the service proxy is correct.
      if (ApplicationConfiguration.proxy != null) {
        Assert.AreEqual(service.Proxy, ApplicationConfiguration.proxy,
            "Service proxy should be same as ApplicationConfiguration.Proxy.");
      }
    }

    /// <summary>
    /// Test if <see cref="AdWordsUser"/> class creates a v13 service correctly.
    /// </summary>
    [Test]
    public void TestV13ServiceCreation() {
      AdService service = (AdService) user.GetService(ApiServices.v13.AdService);
      Assert.NotNull(service, "AdWordsUser could not create v13.AdService.");

      TestV13Header(ApplicationConfiguration.email, service.emailValue, "Email");
      TestV13Header(ApplicationConfiguration.password, service.passwordValue, "Password");
      TestV13Header(ApplicationConfiguration.developerToken, service.developerTokenValue,
          "Developer Token");
      TestV13Header(ApplicationConfiguration.applicationToken, service.applicationTokenValue,
          "Application Token");
      TestV13Header(ApplicationConfiguration.clientEmail, service.clientEmailValue,
          "Client Email");
      TestV13Header(ApplicationConfiguration.clientCustomerId, service.clientCustomerIdValue,
          "Client CustomerId");
      TestV13Header("AWAPI DotNetLib " + DataUtilities.GetVersion() + " - " +
          ApplicationConfiguration.companyName, service.useragentValue,
          "Useragent");

      // Check if the service url is correct.
      Assert.That(string.Compare(ApplicationConfiguration.urlV13 + "/api/adwords/v13/AdService",
          service.Url, true) == 0, "Service url should be " + ApplicationConfiguration.urlV13 +
          "/api/adwords/v13/AdService");

      // Check if the service proxy is correct.
      if (ApplicationConfiguration.proxy != null) {
        Assert.AreEqual(service.Proxy, ApplicationConfiguration.proxy,
            "Service proxy should be same as ApplicationConfiguration.Proxy.");
      }
    }

    /// <summary>
    /// Tests if AdWordsUser created and set a v13 header properly for a random v13 service.
    /// </summary>
    /// <param name="configValue">The key from app.config, which is supposed to be set for
    /// this header.</param>
    /// <param name="header">The v13 header being tested.</param>
    /// <param name="keyName">The keyname to be used in display string if the test fails.</param>
    private static void TestV13Header(string configValue, SoapHeader header, string keyName) {
      if (!string.IsNullOrEmpty(configValue)) {
        Assert.NotNull(header, keyName + " header must not be null.");

        PropertyInfo propInfo = header.GetType().GetProperty("Value");
        string[] value = (string[]) propInfo.GetValue(header, null);

        Assert.NotNull(value, keyName + " header value must not be null.");
        Assert.That(value.Length == 1, keyName + " header value should have " +
            "length = 1");
        Assert.AreEqual(value[0], configValue);
      }
    }
  }
}

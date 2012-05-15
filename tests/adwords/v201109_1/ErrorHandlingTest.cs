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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201109_1;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

using CSharpExamples = Google.Api.Ads.AdWords.Examples.CSharp.v201109_1;
using VBExamples = Google.Api.Ads.AdWords.Examples.VB.v201109_1;

namespace Google.Api.Ads.AdWords.Tests.v201109_1 {
  /// <summary>
  /// Test cases for all the code examples under v201109_1\ErrorHandling.
  /// </summary>
  class ErrorHandlingTest : ExampleBaseTests {
    /// <summary>
    /// Inits this instance.
    /// </summary>
    [SetUp]
    public void Init() {
      parameters = new Dictionary<string, string>();

      long campaignId = utils.CreateCampaign(user, new ManualCPC());
      long adGroupId = utils.CreateAdGroup(user, campaignId);
      parameters["ADGROUP_ID"] = adGroupId.ToString();
    }

    /// <summary>
    /// Tests the HandlePartialFailures VB.NET code example.
    /// </summary>
    [Test]
    public void TestHandlePartialFailuresVBExample() {
      RunExample(new VBExamples.HandlePartialFailures());
    }

    /// <summary>
    /// Tests the HandlePartialFailures C# code example.
    /// </summary>
    [Test]
    public void TestHandlePartialFailuresCSharpExample() {
      RunExample(new CSharpExamples.HandlePartialFailures());
    }

    /// <summary>
    /// Tests the HandlePolicyViolationError VB.NET code example.
    /// </summary>
    [Test]
    public void TestHandlePolicyViolationErrorVBExample() {
      RunExample(new VBExamples.HandlePolicyViolationError());
    }

    /// <summary>
    /// Tests the HandlePolicyViolationError C# code example.
    /// </summary>
    [Test]
    public void TestHandlePolicyViolationErrorCSharpExample() {
      RunExample(new CSharpExamples.HandlePolicyViolationError());
    }

    /// <summary>
    /// Tests the HandleTwoFactorAuthorizationError VB.NET code example.
    /// </summary>
    [Test]
    public void TestHandleTwoFactorAuthorizationErrorVBExample() {
      RunExample(new VBExamples.HandleTwoFactorAuthorizationError());
    }

    /// <summary>
    /// Tests the HandleTwoFactorAuthorizationError C# code example.
    /// </summary>
    [Test]
    public void TestHandleTwoFactorAuthorizationErrorCSharpExample() {
      RunExample(new CSharpExamples.HandleTwoFactorAuthorizationError());
    }
  }
}

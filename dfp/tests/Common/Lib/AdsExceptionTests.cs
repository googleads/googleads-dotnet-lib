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

using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.Common.Tests.Mocks;

using NUnit.Framework;

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Google.Api.Ads.Common.Tests.Lib {
  /// <summary>
  /// Coverage tests for AdsException class.
  /// </summary>
  public class AdsExceptionTests {
    /// <summary>
    /// Message to be used for running tests.
    /// </summary>
    private string message = "This is a test message";

    /// <summary>
    /// Inner exception to be used for running tests.
    /// </summary>
    private ApplicationException innerException = new ApplicationException();

    /// <summary>
    /// Tests the default constructor.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestContructor1() {
      Assert.DoesNotThrow(delegate() {
        AdsException exception = new MockAdsException();
      });
    }

    /// <summary>
    /// Tests the overloaded constructors.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestContructor2() {
      AdsException exception = new MockAdsException(message);
      Assert.AreEqual(message, exception.Message);
      Assert.Null(exception.InnerException);
    }

    /// <summary>
    /// Tests the overloaded constructors.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestContructor3() {
      AdsException exception = new MockAdsException(message, innerException);
      Assert.AreEqual(message, exception.Message);
      Assert.AreEqual(innerException, exception.InnerException);
    }

    /// <summary>
    /// Tests the protected serialization constructor.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestContructor4() {
      Assert.DoesNotThrow(delegate() {
        MockAdsException exception = new MockAdsException();
        exception.MockProperty = 2;
        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream memStream = new MemoryStream();
        formatter.Serialize(memStream, exception);
        memStream.Seek(0, SeekOrigin.Begin);
        MockAdsException exception1 = (MockAdsException) formatter.Deserialize(memStream);
        memStream.Dispose();
        Assert.AreEqual(2, exception1.MockProperty);
      });
    }
  }
}

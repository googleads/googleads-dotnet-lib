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

using Google.Api.Ads.Common.Util;

using NUnit.Framework;

using System;

namespace Google.Api.Ads.Common.Tests.Util {

  /// <summary>
  /// Tests for <see cref="TemporaryIdGenerator"/> class.
  /// </summary>
  [TestFixture]
  internal class TemporaryIdGeneratorTests {

    /// <summary>
    /// Tests the Next method with default values.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestNextWithDefaultValues() {
      TemporaryIdGenerator generator = new TemporaryIdGenerator();
      Assert.That(generator.Next == Int32.MinValue);
      Assert.That(generator.Next == Int32.MinValue + 1);
    }

    /// <summary>
    /// Tests the Next method when a start ID is provided.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestNextWithStartId() {
      int startId = -20;
      TemporaryIdGenerator generator = new TemporaryIdGenerator(startId);

      Assert.That(generator.Next == startId);
      Assert.That(generator.Next == startId + 1);
    }

    /// <summary>
    /// Tests that an exception is thrown when a positive start ID is provided.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestThrowsExceptionWithPositiveStartId() {
      int startId = 20;
      Assert.Throws<ArgumentException>(delegate() {
        TemporaryIdGenerator generator = new TemporaryIdGenerator(startId);
      });

      startId = 0;
      Assert.Throws<ArgumentException>(delegate() {
        TemporaryIdGenerator generator = new TemporaryIdGenerator(startId);
      });
    }

    /// <summary>
    /// Tests that an exception is thrown if a positive number gets generated.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestDoesNotGeneratePositiveId() {
      int startId = -1;
      TemporaryIdGenerator generator = new TemporaryIdGenerator(startId);
      Assert.Throws<ApplicationException>(delegate() {
        long next = generator.Next;
      });
    }
  }
}

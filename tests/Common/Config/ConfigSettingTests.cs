// Copyright 2018, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Common.Config;
using NUnit.Framework;

namespace Google.Api.Ads.Common.Tests.Config {

  /// <summary>
  /// Tests for <see cref="ConfigSetting"/> class.
  /// </summary>
  public class ConfigSettingTests {
    public const string CONFIG_NAME = "TestSetting";
    public const int DEFAULT_VALUE = 42;

    /// <summary>
    /// Tests the <see cref="ConfigSetting.TryParse(string)"/> method.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestTryParse() {
      ConfigSetting<int> configSetting = new ConfigSetting<int>(CONFIG_NAME, DEFAULT_VALUE);

      configSetting.TryParse("foo");
      Assert.AreEqual(DEFAULT_VALUE, configSetting.Value);

      configSetting.TryParse("52");
      Assert.AreEqual(52, configSetting.Value);
    }
  }
}

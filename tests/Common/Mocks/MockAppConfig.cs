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

using Google.Api.Ads.Common.Config;
using Google.Api.Ads.Common.Lib;

using System;
using System.Collections;
using System.Collections.Generic;

namespace Google.Api.Ads.Common.Tests.Mocks {
  /// <summary>
  /// Implements a mock version of AppConfigBase class for testing purposes.
  /// </summary>
  public class MockAppConfig : AppConfigBase {
    /// <summary>
    /// The short name to identify this assembly.
    /// </summary>
    private const string SHORT_NAME = "Mock-DotNet";

    private const long TEST_TIMESTAMP = 1353924951;

    private const string TEST_OAUTH2_SCOPE = "TEST_OAUTH2_SCOPE";

    public const string TEST_DEFAULT_VALUE = "TEST_DEFAULT_VALUE";

    private ConfigSetting<string> settingWithNonEmptyDefaultValue = new ConfigSetting<string>(
        "SettingWithNonEmptyDefaultValue", TEST_DEFAULT_VALUE);

    /// <summary>
    /// Default constuctor.
    /// </summary>
    public MockAppConfig() : base() {
    }

    /// <summary>
    /// Gets the number of seconds after Jan 1, 1970, 00:00:00
    /// </summary>
    public override long UnixTimestamp {
      get {
        return TEST_TIMESTAMP;
      }
    }

    public string SettingWithNonEmptyDefaultValue {
      get => settingWithNonEmptyDefaultValue.Value;
      set => SetPropertyAndNotify(settingWithNonEmptyDefaultValue, value);
    }

    /// <summary>
    /// Gets the default OAuth2 scope.
    /// </summary>
    /// <returns></returns>
    public override string GetDefaultOAuth2Scope() {
      return TEST_OAUTH2_SCOPE;
    }

    /// <summary>
    /// Allows the test cases to call ReadSettings method for testing purposes.
    /// </summary>
    /// <param name="dictSettings">The configuration settings.</param>
    /// <remarks>AppConfigBase class loads its settings from App.config, and the
    /// framework calls ReadSettings method to load the values. However, this is
    /// a protected method, so we expose ReadSettings in the mock version to
    /// allow easier configuration of AppConfig while running test cases.
    /// </remarks>
    public void MockReadSettings(Dictionary<string, string> dictSettings) {
      base.ReadSettings(dictSettings);
      ReadSetting(dictSettings, settingWithNonEmptyDefaultValue);
    }

    /// <summary>
    /// Sets the property field for tests.
    /// </summary>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="newValue">The new value.</param>
    /// <remarks>Most properties in AppConfigBase class are readonly, and are
    /// initalized at during the creation of the AppConfig instance. However,
    /// it is desirable during testing to change the value of one of these
    /// properties so that we can avoid initializing a new AppConfig instance
    /// just to test the coverage of another class that uses the config class.
    /// </remarks>
    public void SetPropertyFieldForTests(string propertyName, object newValue) {
      this.GetType().GetProperty(propertyName).SetValue(this, newValue, null);
    }
  }
}

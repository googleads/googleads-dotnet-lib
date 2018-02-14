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

using Google.Api.Ads.Common.Logging;
using Newtonsoft.Json;
using NUnit.Framework;

using System.Collections.Generic;

namespace Google.Api.Ads.Common.Tests.Util {

  /// <summary>
  /// UnitTests for <see cref="JsonBodyFormatter"/> class.
  /// </summary>
  [TestFixture]
  public class JsonBodyFormatterTests {
    private const string KEY1 = "KEY1";
    private const string KEY2 = "KEY2";
    private const string KEY3 = "KEY3";

    private const string VALUE1 = "VALUE1";
    private const string VALUE2 = "VALUE2";
    private const string VALUE3 = "VALUE3";

    /// <summary>
    /// The request body to be used for testing.
    /// </summary>
    private readonly string BODY = string.Format("{{'{0}': '{1}', '{2}': '{3}', '{4}': '{5}'}}",
        KEY1, VALUE1, KEY2, VALUE2, KEY3, VALUE3);

    /// <summary>
    /// The keys to be masked in the request.
    /// </summary>
    private ISet<string> KEYS = new HashSet<string>() { KEY1, KEY2 };

    /// <summary>
    /// Test for JsonBodyFormatter.MaskContents method.
    /// </summary>
    [Test]
    public void TestMaskContents() {
      string maskedBody = new JsonBodyFormatter().MaskContents(BODY, KEYS);
      Dictionary<string, string> jsonDict =
          JsonConvert.DeserializeObject<Dictionary<string, string>>(maskedBody);

      Assert.AreEqual(jsonDict[KEY1], TraceFormatter.MASK_PATTERN);
      Assert.AreEqual(jsonDict[KEY2], TraceFormatter.MASK_PATTERN);
      Assert.AreEqual(jsonDict[KEY3], VALUE3);
    }
  }
}
// Copyright 2016, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201603;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

using CSharpExamples = Google.Api.Ads.AdWords.Examples.CSharp.v201603;
using VBExamples = Google.Api.Ads.AdWords.Examples.VB.v201603;

namespace Google.Api.Ads.AdWords.Tests.v201603 {
  /// <summary>
  /// Test cases for all the code examples under v201603\Miscellaneous.
  /// </summary>
  class MiscellaneousTest : VersionedExampleTestsBase {
    /// <summary>
    /// Inits this instance.
    /// </summary>
    [SetUp]
    public void Init() {
    }

    /// <summary>
    /// Tests the GetAllVideosAndImages VB.NET code example.
    /// </summary>
    [Test]
    public void TestGetAllVideosAndImagesVBExample() {
      RunExample(delegate() {
        new VBExamples.GetAllVideosAndImages().Run(user);
      });
    }

    /// <summary>
    /// Tests the GetAllVideosAndImages C# code example.
    /// </summary>
    [Test]
    public void TestGetAllVideosAndImagesCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.GetAllVideosAndImages().Run(user);
      });
    }

    /// <summary>
    /// Tests the UploadImage VB.NET code example.
    /// </summary>
    [Test]
    public void TestUploadImageVBExample() {
      RunExample(delegate() {
        new VBExamples.UploadImage().Run(user);
      });
    }

    /// <summary>
    /// Tests the UploadImage C# code example.
    /// </summary>
    [Test]
    public void TestUploadImageCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.UploadImage().Run(user);
      });
    }

    /// <summary>
    /// Tests the UploadHtml5 VB.NET code example.
    /// </summary>
    [Test]
    public void TestUploadHtml5VBExample() {
      RunExample(delegate() {
        new VBExamples.UploadMediaBundle().Run(user);
      });
    }

    /// <summary>
    /// Tests the UploadHtml5 C# code example.
    /// </summary>
    [Test]
    public void TestUploadHtml5CSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.UploadMediaBundle().Run(user);
      });
    }
  }
}

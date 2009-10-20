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

// Author: api.anash@gmail.com (Anash P. Oommen)

using com.google.api.adwords.lib;
using com.google.api.adwords.lib.util;

using System;
using System.Collections.Generic;
using System.Xml;

namespace com.google.api.adwords.samples.v13 {
  /// <summary>
  /// Shows how to restore an entire sandbox account. You should run BackupSandboxDemo
  /// first to obtain the input file for this sample.
  /// </summary>
  class RestoreSandboxDemo : SampleBase{
    public override string Description {
      get {
        return "Shows how to restore an entire sandbox account. You should run BackupSandboxDemo" +
            " first to obtain the input file for this sample";
      }
    }

    /// <summary>
    /// Run the sample code.
    /// </summary>
    /// <param name="user">The AdWords user object running the sample.
    /// </param>
    public override void Run(AdWordsUser user) {
      DataUtilities.RestoreSandboxContents(user, "C:\\SandboxBackup.xml");
    }
  }
}

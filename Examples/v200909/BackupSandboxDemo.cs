// Copyright 2010, Google Inc. All Rights Reserved.
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
using System.Xml;

namespace com.google.api.adwords.samples.v200909 {
  /// <summary>
  /// This code example shows how to backup a sandbox account.
  /// </summary>
  class BackupSandboxDemo : SampleBase{
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example shows how to backup a sandbox account.";
      }
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      user.UseSandbox();
      DataUtilities.DownloadSandboxContents(user, string.Format("{0}\\SandboxBackup-{1}.xml",
          GetHomeDir(), DateTime.Now.ToString("yyyy-M-d H-m-s.ffffff")));
    }

    /// <summary>
    /// Gets the current user's home directory.
    /// </summary>
    /// <returns>The current user's home directory.</returns>
    public static String GetHomeDir() {
      return Environment.GetEnvironmentVariable("USERPROFILE");
    }
  }
}

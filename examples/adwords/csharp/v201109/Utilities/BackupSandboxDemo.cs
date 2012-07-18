// Copyright 2011, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.Util.Data;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109 {
  /// <summary>
  /// This code example shows how to backup a sandbox account.
  /// </summary>
  class BackupSandboxDemo : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      BackupSandboxDemo codeExample = new BackupSandboxDemo();
      Console.WriteLine(codeExample.Description);
      try {
        string fileName = "INSERT_FILE_NAME_HERE";
        codeExample.Run(new AdWordsUser(), fileName);
      } catch (Exception ex) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(ex));
      }
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example shows how to backup a sandbox account.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="fileName">The file to which sandbox contents are backed
    /// up.</param>
    public void Run(AdWordsUser user, string fileName) {
      // The following set of fields are not exhaustive, they are only for
      // illustration. If you need to backup more object fields, you need to
      // lookup the corresponding selector names and add them below.
      DataUtilities.DownloadSandboxContents(user, fileName,
          new string[] {"Id", "Name", "Status"},
          new string[] {"Id", "Name", "Status"},
          new string[] {"Id", "Status", "Headline", "Description1", "Description2", "DisplayUrl"},
          new string[] {"Id", "KeywordText"},
          new string[] {"Id", "CriteriaType" }
      );
    }
  }
}

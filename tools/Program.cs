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

using System;
using System.Collections.Generic;
using System.Text;

namespace com.google.api.adwords.tools {
  /// <summary>
  /// Main program for this application.
  /// </summary>
  class Program {
    /// <summary>
    /// The main method for this program.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      Dictionary<string, string> cmdLineParams = ParseCommandLine(args,
          new string[] {"--action=", "--file="});

      if (cmdLineParams["action"] == null) {
        ShowUsageAndExit();
      }

      switch (cmdLineParams["action"]) {
        case "ProcessWsdl":
          if (!new ProcessWsdl().Run(cmdLineParams)) {
            ShowUsageAndExit();
          }
          break;

        case "ProcessCode":
          if (!new ProcessCode().Run(cmdLineParams)) {
            ShowUsageAndExit();
          }
          break;
      }
    }

    /// <summary>
    /// Parse the command line arguments into a dictionary.
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    /// <param name="flags">The command line key names to be extracted.</param>
    /// <returns>A map of command line parameters, with key as the parameter
    /// name and value as the parsed argument value.</returns>
    /// <remarks>All parameters are expected in the form
    /// --option_name=option_value</remarks>
    private static Dictionary<string, string> ParseCommandLine(string[] args, string[] flags) {
      Dictionary<string, string> cmdLineParams = new Dictionary<string, string>();

      for (int i = 0; i < args.Length; i++) {
        for (int j = 0; j < flags.Length; j++) {
          if (args[i].StartsWith(flags[j])) {
            string trimmedFlag = flags[j].Trim('-', '=');
            cmdLineParams[trimmedFlag] = args[i].Substring(flags[j].Length);
          }
        }
      }
      return cmdLineParams;
    }

    /// <summary>
    /// Prints the command line usage and exits the application.
    /// </summary>
    private static void ShowUsageAndExit() {
      Console.WriteLine(
        "Usage : wsdltools.exe --action=action --file=filename\n\n" +
        "action: can be ProcessWsdl or ProcessCode.\n" +
        "file: if action is ProcessWsdl, then file is a wsdl configuration file identified" +
        " by wsdl.exe. If action is ProcessCode, then file is the C# code output generated" +
        " by wsdl.exe.");
      Environment.Exit(0);
    }
  }
}

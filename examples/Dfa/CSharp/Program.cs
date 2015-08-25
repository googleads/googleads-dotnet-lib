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

using Google.Api.Ads.Dfa.Lib;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using SamplePair = System.Collections.Generic.KeyValuePair<string,
    Google.Api.Ads.Dfa.Examples.CSharp.SampleBase>;

namespace Google.Api.Ads.Dfa.Examples.CSharp {
  /// <summary>
  /// The Main class for this application.
  /// </summary>
  class Program {
    /// <summary>
    /// A map to hold the code examples to be executed.
    /// </summary>
    static List<SamplePair> sampleMap = new List<SamplePair>();

    /// <summary>
    /// A flag to keep track of whether help message was shown earlier.
    /// </summary>
    private static bool helpShown = false;

    static void RegisterSample(string key, SampleBase value) {
      sampleMap.Add(new SamplePair(key, value));
    }
    /// <summary>
    /// Static constructor to initialize the sample map.
    /// </summary>
    static Program() {
      Type[] types = Assembly.GetExecutingAssembly().GetTypes();

      foreach (Type type in types) {
        if (type.BaseType == typeof(SampleBase)) {
          RegisterSample(type.FullName.Replace(typeof(Program).Namespace + ".", ""),
              Activator.CreateInstance(type) as SampleBase);
        }
      }
    }

    /// <summary>
    /// The main method.
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args) {
      if (args.Length == 0) {
        ShowUsage();
        return;
      }

      DfaUser user = new DfaUser();

      foreach (string cmdArgs in args) {
        SamplePair matchingPair = sampleMap.Find(delegate(SamplePair pair) {
          return string.Compare(pair.Key, cmdArgs, true) == 0;
        });

        if (matchingPair.Key != null) {
          RunASample(user, matchingPair.Value);
        } else {
          ShowUsage();
        }
      }
    }

    /// <summary>
    /// Runs a code sample.
    /// </summary>
    /// <param name="user">The user whose credentials should be used for
    /// running the sample.</param>
    /// <param name="sample">The code sample to run.</param>
    private static void RunASample(DfaUser user, SampleBase sample) {
      try {
        Console.WriteLine(sample.Description);
        sample.Run(user);
      } catch (Exception e) {
        Console.WriteLine("An exception occurred while running this code example.\n{0} at\n{1}",
            e.Message, e.StackTrace);
      } finally {
        Console.WriteLine("Press [Enter] to continue");
        Console.ReadLine();
      }
    }

    /// <summary>
    /// Prints program usage message.
    /// </summary>
    private static void ShowUsage() {
      if (helpShown) {
        return;
      } else {
        helpShown = true;
      }
      string exeName = Path.GetFileName(Assembly.GetExecutingAssembly().Location);
      Console.WriteLine("Runs DFA API code examples");
      Console.WriteLine("Usage : {0} [flags]\n", exeName);
      Console.WriteLine("Available flags\n");
      Console.WriteLine("--help\t\t : Prints this help message.", exeName);
      Console.WriteLine("examplename1 [examplename1 ...] : " +
          "Run specific code examples. Example name can be one of the following:\n", exeName);
      foreach (SamplePair pair in sampleMap) {
        Console.WriteLine("{0} : {1}", pair.Key, pair.Value.Description);
      }
      Console.WriteLine("Press [Enter] to continue");
      Console.ReadLine();
    }
  }
}

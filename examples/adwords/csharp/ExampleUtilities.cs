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

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.AdWords.Examples.CSharp {
  /// <summary>
  /// Utility class for assisting in running code examples.
  /// </summary>
  public class ExampleUtilities {
    /// <summary>
    /// Gets the current timestamp as a string.
    /// </summary>
    /// <returns>The current timestamp as a string.</returns>
    public static string GetTimeStamp() {
      return DateTime.Now.ToString("yyyy-M-d H-m-s.ffffff");
    }

    /// <summary>
    /// Gets the current user's home directory.
    /// </summary>
    /// <returns>The current user's home directory.</returns>
    public static String GetHomeDir() {
      return Environment.GetEnvironmentVariable("USERPROFILE");
    }

    /// <summary>
    /// Gets the user inputs for running a code example in command line mode.
    /// </summary>
    /// <param name="paramNames">The parameter names.</param>
    /// <returns>A dictionary, with key as parameter name and value as
    /// parameter value.</returns>
    public static Dictionary<string, string> GetUserInputs(string[] paramNames) {
      Dictionary<string, string> parameters = new Dictionary<string, string>();
      foreach (string paramName in paramNames) {
        Console.Write("Enter {0}: ", paramName);
        parameters[paramName] = Console.ReadLine();
      }
      return parameters;
    }

  }
}

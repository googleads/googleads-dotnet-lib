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

// Uncomment this key and recompile to make all the samples interactive
// instead of manually replacing the INSERT_XXX fields in samples.

#define INTERACTIVE

using Google.Api.Ads.Dfa.Lib;

using System;
using System.Threading;

namespace Google.Api.Ads.Dfa.Examples.CSharp {
  /// <summary>
  /// This abstract class represents a code example.
  /// </summary>
  abstract class SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public abstract string Description {
      get;
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">Dfa user running the code example.</param>
    public abstract void Run(DfaUser user);

    protected string _T(string prompt) {
#if INTERACTIVE
      Console.Write(prompt + " : ");
      return Console.ReadLine();
#else
      return prompt;
#endif
    }

    /// <summary>
    /// Gets the current timestamp as a string.
    /// </summary>
    /// <returns>The current timestamp as a string.</returns>
    protected string GetTimeStamp() {
      Thread.Sleep(100);
      return (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString();
    }

    /// <summary>
    /// Gets the current user's home directory.
    /// </summary>
    /// <returns>The current user's home directory.</returns>
    protected String GetHomeDir() {
      return Environment.GetEnvironmentVariable("USERPROFILE");
    }
  }
}

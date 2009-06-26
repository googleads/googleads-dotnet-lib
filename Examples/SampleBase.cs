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

using System;

using com.google.api.adwords.lib;

namespace com.google.api.adwords.samples {
  /// <summary>
  /// This abstract class represents a code sample.
  /// </summary>
  abstract class SampleBase {
    /// <summary>
    /// Returns a description about the sample code.
    /// </summary>
    public abstract string Description {
      get;
    }

    /// <summary>
    /// Run the sample code.
    /// </summary>
    /// <param name="user">AdWords user object running the sample.</param>
    public abstract void Run(AdWordsUser user);

    protected string _T(string prompt) {
#if INTERACTIVE
      Console.Write(prompt + " : ");
      return Console.ReadLine();
#else
      return prompt;
#endif
    }
  }
}

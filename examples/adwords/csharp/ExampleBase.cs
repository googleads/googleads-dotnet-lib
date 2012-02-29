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

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp {
  /// <summary>
  /// This abstract class represents a code example.
  /// </summary>
  abstract class ExampleBase {
    /// <summary>
    /// Delegate for accepting user inputs for this code example.
    /// </summary>
    /// <param name="parameterNames">The list of parameter param names.</param>
    /// <returns>A dictionary, with key as parameter name and value as parameter
    /// value.</returns>
    public delegate Dictionary<string, string> GetUserInputsMethod(string[] parameterNames);

    /// <summary>
    /// Callback for getting user inputs.
    /// </summary>
    private GetUserInputsMethod userInputsMethod = ExampleUtilities.GetUserInputs;

    /// <summary>
    /// Gets or sets the callback for getting user inputs.
    /// </summary>
    public GetUserInputsMethod UserInputsMethod {
      get {
        return userInputsMethod;
      }
      set {
        userInputsMethod = value;
      }
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public abstract string Description {
      get;
    }

    /// <summary>
    /// Gets the list of parameter names required to run this code example.
    /// </summary>
    /// <returns>A list of parameter names for this code example.</returns>
    public abstract string[] GetParameterNames();

    /// <summary>
    /// Gets the parameters required to run this code example.
    /// </summary>
    /// <returns>A dictionary, with key as parameter name and value as parameter
    /// value.</returns>
    public virtual Dictionary<string, string> GetParameters() {
      return UserInputsMethod(GetParameterNames());
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="parameters">The parameters for running the code
    /// example.</param>
    /// <param name="writer">The stream writer to which script output should be
    /// written.</param>
    public abstract void Run(AdWordsUser user, Dictionary<string, string> parameters,
        TextWriter writer);
  }
}

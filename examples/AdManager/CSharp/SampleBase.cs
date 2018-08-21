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

// Uncomment this key and recompile to make all the samples interactive
// instead of manually replacing the INSERT_XXX fields in samples.

#define INTERACTIVE

using Google.Api.Ads.AdManager.Lib;

using System;
using System.Threading;

namespace Google.Api.Ads.AdManager.Examples.CSharp
{
    /// <summary>
    /// This abstract class represents a code example.
    /// </summary>
    public abstract class SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// Retrieves a user provided sample value.
        /// </summary>
        /// <param name="prompt">
        /// A prompt to display (when in interactive mode), or the value to be returned.
        /// </param>
        /// <remarks>
        /// When called in interactive mode, presents the provided string as a prompt to the user
        /// and returns the user provided input. Otherwise, returns the provided string exactly.
        /// </remarks>
        protected static string _T(string prompt)
        {
#if INTERACTIVE
            Console.Write(prompt + " : ");
            return Console.ReadLine();
#else
      return prompt;
#endif
        }

        /// <summary>
        /// Gets the current time stamp.
        /// </summary>
        /// <returns>The current timestamp as a string.</returns>
        /// <remarks>You can use this method to generate a random string for use
        /// with various entities that need a unique name. The method adds a 100ms
        /// delay to prevent closely placed calls from generating the same
        /// timestamp.</remarks>
        protected string GetTimeStamp()
        {
            Thread.Sleep(100);
            return (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString();
        }
    }
}

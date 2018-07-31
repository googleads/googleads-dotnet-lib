// Copyright 2015, Google Inc. All Rights Reserved.
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
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.Common.Util
{
    /// <summary>
    /// Provides utility methods for checking preconditions.
    /// </summary>
    public class PreconditionUtilities
    {
        /// <summary>
        /// Utility method for null checking arguments.
        /// </summary>
        /// <param name="value">The Object to check</param>
        /// <param name="argument">The name of the argument being checked</param>
        public static void CheckArgumentNotNull(object value, string argument)
        {
            if (value == null)
            {
                throw new ArgumentNullException(argument);
            }
        }

        /// <summary>
        /// Utility method for null checking arguments.
        /// </summary>
        /// <param name="condition">The Object to check</param>
        /// <param name="message">The name of the argument being checked</param>
        public static void CheckArgument(bool condition, string message)
        {
            if (!condition)
            {
                throw new ArgumentException(message);
            }
        }

        /// <summary>
        /// Utility method for checking values.
        /// </summary>
        /// <param name="value">The Object to check</param>
        /// <param name="message">The error message if the Object is null</param>
        public static void CheckNotNull(object value, string message)
        {
            if (value == null)
            {
                throw new NullReferenceException(message);
            }
        }

        /// <summary>
        /// Utility method for checking the state of a class or method.
        /// </summary>
        /// <param name="condition">The condition to check.</param>
        /// <param name="message">The error message to use if the condition check
        /// fails.</param>
        public static void CheckState(bool condition, string message)
        {
            if (!condition)
            {
                throw new InvalidOperationException(message);
            }
        }
    }
}

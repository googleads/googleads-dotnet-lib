// Copyright 2013, Google Inc. All Rights Reserved.
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
using System.Reflection;

namespace Google.Api.Ads.Common.Util
{
    /// <summary>
    /// Utility class to display deprecation message at runtime.
    /// </summary>
    public class DeprecationUtilities
    {
        /// <summary>
        /// Writes a deprecation message to Trace stream.
        /// </summary>
        /// <param name="memberInfo">Details of the deprecated member.</param>
        /// <remarks>The member corresponding to memberInfo should be annotated with
        /// an ObsoleteAttribute.</remarks>
        public static void ShowDeprecationMessage(MemberInfo memberInfo)
        {
            if (memberInfo == null)
            {
                throw new NullReferenceException("MemberInfo cannot be null.");
            }

            TraceUtilities.WriteDeprecationWarnings(GetDeprecationMessage(memberInfo));
        }

        /// <summary>
        /// Gets the deprecation message to be displayed.
        /// </summary>
        /// <param name="memberInfo">Details of the deprecated member.</param>
        /// <returns>The deprecation message as found on the ObsoleteAttribute
        /// decoration for this member, or null otherwise.</returns>
        private static string GetDeprecationMessage(MemberInfo memberInfo)
        {
            object[] attributes = memberInfo.GetCustomAttributes(typeof(ObsoleteAttribute), false);

            if (attributes.Length > 0)
            {
                return ((ObsoleteAttribute) attributes[0]).Message;
            }

            return null;
        }
    }
}

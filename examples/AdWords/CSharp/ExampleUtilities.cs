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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Google.Api.Ads.AdWords.Examples.CSharp
{
    /// <summary>
    /// Utility class for assisting in running code examples.
    /// </summary>
    public class ExampleUtilities
    {
        /// <summary>
        /// Gets a random string. Useful for generating unique names for campaigns,
        /// ad groups, etc.
        /// </summary>
        /// <returns>The current timestamp as a string.</returns>
        public static string GetRandomString()
        {
            return string.Format("{0} - {1}", Guid.NewGuid(),
                DateTime.Now.ToString("yyyy-M-d H-m-s.ffffff"));
        }

        /// <summary>
        /// Gets a random string. Useful for generating unique names for campaigns,
        /// ad groups, etc.
        /// </summary>
        /// <returns>The current timestamp as a string.</returns>
        public static string GetShortRandomString()
        {
            return Guid.NewGuid().ToString().Substring(0, 8);
        }

        /// <summary>
        /// Formats the exception as a printable message.
        /// </summary>
        /// <param name="ex">The exception.</param>
        /// <returns>The formatted exception string.</returns>
        public static string FormatException(Exception ex)
        {
            List<string> messages = new List<string>();
            Exception rootEx = ex;
            while (rootEx != null)
            {
                messages.Add(string.Format("{0} ({1})\n\n{2}\n", rootEx.GetType().ToString(),
                    rootEx.Message, rootEx.StackTrace));
                rootEx = rootEx.InnerException;
            }

            return string.Join("\nCaused by\n\n", messages.ToArray());
        }

        /// <summary>
        /// Gets the current user's home directory.
        /// </summary>
        /// <returns>The current user's home directory.</returns>
        public static string GetHomeDir()
        {
            return Environment.GetEnvironmentVariable("USERPROFILE");
        }

        /// <summary>
        /// Determines whether the specified type is Nullable.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>True, if the type is nullable, false otherwise.</returns>
        public static bool IsNullable(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// Gets the underlying type of a given type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The type, if the given type is not nullable, the underlying
        /// type if it is nullable.</returns>
        public static Type GetUnderlyingType(Type type)
        {
            return IsNullable(type) ? type.GetGenericArguments()[0] : type;
        }

        /// <summary>
        /// Gets the parameters for running the code example.
        /// </summary>
        /// <param name="methodInfo">The method info for Run method in code
        /// example.</param>
        /// <returns>An array of parameters for running the code example.</returns>
        public static List<object> GetParameters(MethodInfo methodInfo)
        {
            List<object> retval = new List<object>();
            ParameterInfo[] paramInfos = methodInfo.GetParameters();
            for (int i = 1; i < paramInfos.Length; i++)
            {
                ParameterInfo paramInfo = paramInfos[i];

                Type underlyingType = GetUnderlyingType(paramInfo.ParameterType);
                bool isNullable = IsNullable(underlyingType);
                if (isNullable)
                {
                    Console.Write("[Optional] ");
                }

                if (underlyingType.IsArray)
                {
                    Console.Write("[Comma Separated] ");
                }

                Console.Write("Enter {0}: ", paramInfo.Name);
                string value = Console.ReadLine();
                object objValue = null;

                if (underlyingType.IsArray)
                {
                    string[] items = value.Split(',');
                    items.ToList().ForEach(x => x.Trim());
                    if (underlyingType.GetElementType() != typeof(string))
                    {
                        throw new Exception("Only string[] array parameters are supported.");
                    }
                    else
                    {
                        objValue = items;
                    }
                }
                else
                {
                    TypeConverter typeConverter = TypeDescriptor.GetConverter(underlyingType);
                    try
                    {
                        objValue = typeConverter.ConvertFromString(value);
                    }
                    catch
                    {
                        if (!isNullable)
                        {
                            throw;
                        }
                    }
                }

                retval.Add(objValue);
            }

            return retval;
        }
    }
}

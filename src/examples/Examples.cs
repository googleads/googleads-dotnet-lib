/*
* Copyright (C) 2006 Google Inc.
* 
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
* 
*      http://www.apache.org/licenses/LICENSE-2.0
* 
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System;
using System.Text;
using System.Reflection;
using com.google.api.adwords.v9;
using com.google.api.adwords.lib;
using com.google.api.adwords.examples;
using System.Web.Services.Protocols;

namespace com.google.api.adwords.examples
{
	/**
	 * Executes examples.
	 */
	class Examples
	{
		static void Main(string[] args)
		{
			// Parse arguments
			if (args.Length < 1) 
			{
				usage();
				return;
			}
			Type t = Type.GetType("com.google.api.adwords.examples." + args[0]);
			object o = Activator.CreateInstance(t);
			MethodInfo runMethod = t.GetMethod("run");
			if (null != runMethod) 
			{
				try 
				{
					runMethod.Invoke(o, null);
				}
				catch (SoapException e) 
				{
					Console.WriteLine("SOAP Fault code: {0} Message {1}", new object[] {e.Code, e.Detail});
					Console.ReadLine();
				}
				catch (Exception e) 
				{
					Console.WriteLine("Exception: {0}", e.InnerException.Message);
					Console.ReadLine();
				}
			}
		}
		static void usage() 
		{
			Console.WriteLine("Usage: google-api-adwords-dotnet.exe exampleClassName");
		}
	}
}
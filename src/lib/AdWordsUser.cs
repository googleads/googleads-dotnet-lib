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

namespace com.google.api.adwords.lib 
{
	using com.google.api.adwords.v5;
	using System;
	using System.Web.Services.Protocols;
	using System.Text.RegularExpressions;
	using System.Reflection;
	using System.Collections;
	using System.Configuration;

	public class Util
	{
	}

	public class AdWordsUser 
	{
		const String LAST_VERSION = "v5";
		const String PACKAGE_PREFIX = "com.google.api.adwords.";
		const String LIB_VERSION_PREFIX = "google C# lib 0.1 ";

		public static String[] HEADERS = {"email",
									 "clientEmail",
									 "password",
									 "applicationToken",
									 "developerToken",
									 "useragent"};

		public email emailValue;
        
		public clientEmail clientEmailValue;
        
		public password passwordValue;
        
		public useragent useragentValue;
        
		public applicationToken applicationTokenValue;
        
		public developerToken developerTokenValue;

		public token tokenValue;

		public Hashtable headers;

		public Hashtable services;

		public String version;

		public String alternateUrl;

		public static Hashtable units = new Hashtable();
 
		public AdWordsUser():this(LAST_VERSION)
		{
		}

		public AdWordsUser(String version) 
		{
			//reads headers for app.config file
			this.headers = (Hashtable)System.Configuration.ConfigurationSettings.GetConfig("adwordsHeaders");

			this.version = version;
			this.services = new Hashtable(9);

			//all this could be refactored using reflection
			this.useragentValue = new useragent();
			this.useragentValue.Text = new String[] { LIB_VERSION_PREFIX + (String)this.headers["useragent"] };
			this.headers["useragent"] = this.useragentValue;

			this.emailValue = new email();
			this.emailValue.Text = new String[] { (String)this.headers["email"] };
			this.headers["email"] = this.emailValue;

			this.passwordValue = new password();
			this.passwordValue.Text = new String[] { (String)this.headers["password"] };
			this.headers["password"] = this.passwordValue;

			this.developerTokenValue = new developerToken();
			this.developerTokenValue.Text = new String[] { (String)this.headers["developerToken"] };
			this.headers["developerToken"] = this.developerTokenValue;

			this.applicationTokenValue = new applicationToken();
			this.applicationTokenValue.Text = new String[] { (String)this.headers["applicationToken"] };
			this.headers["applicationToken"] = this.applicationTokenValue;

			if (this.headers["clientEmail"] != null) 
			{
				this.clientEmailValue = new clientEmail();
				this.clientEmailValue.Text = new String[] { (String)this.headers["clientEmail"] };
				this.headers["clientEmail"] = this.clientEmailValue;
			}
			if (null != this.headers["alternateUrl"]) 
			{
				this.alternateUrl = (String)this.headers["alternateUrl"];
			}

		}

		public void useSandbox() 
		{
			this.alternateUrl = "https://sandbox.google.com/";
		}

		public object getService(String name) 
		{
			object o = services[name];
			if (null != o) 
			{
				return o;
			}
			Type t = Type.GetType(PACKAGE_PREFIX + version + "." + name);
			o = Activator.CreateInstance(t);
			foreach(String headerName in HEADERS)
			{
				FieldInfo f = t.GetField(headerName + "Value");
				if ((null != f) && (null != this.headers[headerName]))
				{
					f.SetValue(o, this.headers[headerName]);
				}
			}
			if (null != alternateUrl) 
			{
				setUrlPrefix((SoapHttpClientProtocol)o, alternateUrl);
			}
			services.Add(name, o);
			return o;
		}

		public static void setUrlPrefix(SoapHttpClientProtocol client, String url) 
		{
			client.Url = Regex.Replace(client.Url, @"https://adwords.google.com/", url);
		}

		public static void addUnits(String email, int i) 
		{
			lock(units)
			{
				if (!units.Contains(email))
				{
					units[email] = 0;
				}
				units[email] = (int)units[email] + i; 
			}
		}

		public int getUnits() 
		{
			if (AdWordsUser.units.Contains(this.emailValue.Text[0]))
			{
				return (int)AdWordsUser.units[this.emailValue.Text[0]];
			}
			return 0;
		}
	}
}
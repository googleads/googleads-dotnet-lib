//
// Copyright (C) 2006 Google Inc.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

namespace com.google.api.adwords.lib
{
	using System;
	using System.Collections;
	using System.Configuration;
	using System.Reflection;
	using System.Text.RegularExpressions;
	using System.Web.Services.Protocols;

	// If you want to use earlier/later version, change this import
	using com.google.api.adwords.v9;

	public class Util
	{
	}

	public class AdWordsUser
	{
		// If you want to use earlier/later version, change LAST_VERSION
		const String LAST_VERSION = "v9";
		// Change MAX_WEB_SERVICES to the number of available web sevices for 
		// this API version, see http://www.google.com/apis/adwords/developer/adwords_api_services.html
		const int MAX_WEB_SERVICES = 9;
		const String PACKAGE_PREFIX = "com.google.api.adwords.";
		const String LIB_VERSION_PREFIX = "Google C# Lib 0.10.0: ";

		public static String[] HEADERS = {
			"email",
			"clientEmail",
			"clientCustomerId",
			"password",
			"applicationToken",
			"developerToken",
			"useragent"
		};

		public email emailValue;
		public clientEmail clientEmailValue;
		public clientCustomerId clientCustomerIdValue;        
		public password passwordValue;
		public useragent useragentValue;        
		public applicationToken applicationTokenValue;
		public developerToken developerTokenValue;
		public String alternateUrl;

		public Hashtable headers;

		public Hashtable services;
		
		public String version;

		public static Hashtable units = new Hashtable();
 
		public AdWordsUser():this(LAST_VERSION)
		{
		}

		public AdWordsUser(String version)
		{
			// Reads headers from App.config file
			this.headers = (Hashtable) System.Configuration.
				ConfigurationSettings.GetConfig("adwordsHeaders");

			this.version = version;
			this.services = new Hashtable(MAX_WEB_SERVICES);

			// All this could be refactored using reflection.
			// Always check to see if we should reconstruct our list of 
			// headers.
			if (!this.headers.ContainsKey("email") || 
				this.headers["email"].GetType() != typeof(email))
			{
				this.emailValue = new email();
				this.emailValue.Text = 
					new String[] {(String) this.headers["email"]};
				this.headers["email"] = this.emailValue;
			}

			if (this.headers["clientEmail"] != null)
			{
				if (!this.headers.ContainsKey("clientEmail") || 
					this.headers["clientEmail"].GetType() != 
					typeof(clientEmail))
				{
					this.clientEmailValue = new clientEmail();
					this.clientEmailValue.Text = 
						new String[] {(String) this.headers["clientEmail"]};
					this.headers["clientEmail"] = this.clientEmailValue;
				}
			}

			if (this.headers["clientCustomerId"] != null)
			{
				if (!this.headers.ContainsKey("clientCustomerId") || 
					this.headers["clientCustomerId"].GetType() != 
					typeof(clientCustomerId))
				{
					this.clientCustomerIdValue = new clientCustomerId();
					this.clientCustomerIdValue.Text = 
						new String[] {(String) this.headers["clientCustomerId"]};
					this.headers["clientCustomerId"] = this.clientCustomerIdValue;
				}
			}

			// If both are specified, defaults to clientEmail
			if (this.headers["clientEmail"] != null &&
				this.headers["clientCustomerId"] != null)
			{
				this.headers["clientCustomerId"] = null;
				this.clientCustomerIdValue = null;
			}

			if (!this.headers.ContainsKey("password") || 
				this.headers["password"].GetType() != typeof(password))
			{
				this.passwordValue = new password();
				this.passwordValue.Text = 
					new String[] {(String) this.headers["password"]};
				this.headers["password"] = this.passwordValue;
			}

			if (!this.headers.ContainsKey("useragent") || 
				this.headers["useragent"].GetType() != typeof(useragent))
			{
				this.useragentValue = new useragent();
				this.useragentValue.Text = 
					new String[] {LIB_VERSION_PREFIX 
					+ (String) this.headers["useragent"]};
				this.headers["useragent"] = this.useragentValue;
			}

			if (!this.headers.ContainsKey("developerToken") || 
				this.headers["developerToken"].GetType() != typeof(
				developerToken))
			{
				this.developerTokenValue = new developerToken();
				this.developerTokenValue.Text = 
					new String[] {(String) this.headers["developerToken"]};
				this.headers["developerToken"] = this.developerTokenValue;
			}

			if (!this.headers.ContainsKey("applicationToken") || 
				this.headers["applicationToken" ].GetType() != typeof( 
				applicationToken))
			{
				this.applicationTokenValue = new applicationToken();
				this.applicationTokenValue.Text = 
					new String[] {(String) this.headers["applicationToken"]};
				this.headers["applicationToken"] = this.applicationTokenValue;
			}

			if (this.headers["alternateUrl"] != null)
			{
				this.alternateUrl = (String) this.headers["alternateUrl"];
				this.headers["alternateUrl"] = this.alternateUrl;
			}
		}

		public AdWordsUser(Hashtable headers)
		{
			if (headers != null)
			{
				this.headers = new Hashtable();

				this.version = LAST_VERSION;
				this.services = new Hashtable(MAX_WEB_SERVICES);

				if (!headers.ContainsKey("email") ||
					headers["email"].GetType() != typeof(email))
				{
					this.emailValue = new email();
					this.emailValue.Text = 
						new String[] {(String) headers["email"]};
					this.headers["email"] = this.emailValue;
				}

				if (!headers.ContainsKey("clientEmail") || 
					headers["clientEmail"].GetType() != 
					typeof(clientEmail))
				{
					this.clientEmailValue = new clientEmail();
					this.clientEmailValue.Text = 
						new String[] {(String) headers["clientEmail"]};
					this.headers["clientEmail"] = this.clientEmailValue;
				}

				if (!headers.ContainsKey("clientCustomerId") || 
					headers["clientCustomerId"].GetType() != 
					typeof(clientCustomerId))
				{
					this.clientCustomerIdValue = new clientCustomerId();
					this.clientCustomerIdValue.Text = 
						new String[] {(String) headers["clientCustomerId"]};
					this.headers["clientCustomerId"] = this.clientCustomerIdValue;
				}

				// If both are specified, defaults to clientEmail
				if (headers.ContainsKey("clientEmail") &&
					headers.ContainsKey("clientCustomerId"))
				{
					this.headers["clientCustomerId"] = null;
					this.clientCustomerIdValue = null;
				}

				if (!headers.ContainsKey("password") ||
					headers["password"].GetType() != typeof(password))
				{
					this.passwordValue = new password();
					this.passwordValue.Text = 
						new String[] {(String) headers["password"]};
					this.headers["password"] = this.passwordValue;
				}

				if (!headers.ContainsKey("useragent") ||
					headers["useragent"].GetType() != typeof(useragent))
				{
					this.useragentValue = new useragent();
					this.useragentValue.Text = 
						new String[] {LIB_VERSION_PREFIX 
						+ (String) headers["useragent"]};
					this.headers["useragent"] = this.useragentValue;
				}

				if (!headers.ContainsKey("developerToken") ||
					headers["developerToken"].GetType() != typeof(
					developerToken))
				{
					this.developerTokenValue = new developerToken();
					this.developerTokenValue.Text = 
						new String[] {(String) headers["developerToken"]};
					this.headers["developerToken"] = this.developerTokenValue;
				}

				if (!headers.ContainsKey("applicationToken") ||
					headers["applicationToken"].GetType() != typeof(
					applicationToken))
				{
					this.applicationTokenValue = new applicationToken();
					this.applicationTokenValue.Text = 
						new String[] {(String) headers["applicationToken"]};
					this.headers["applicationToken"] = 
						this.applicationTokenValue;
				}

				if (headers["alternateUrl"] != null)
				{
					this.alternateUrl = (String) headers["alternateUrl"];
					this.headers["alternateUrl"] = this.alternateUrl;
				}
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
				if ((f != null) && (this.headers[headerName] != null))
				{
					f.SetValue(o, this.headers[headerName]);
				}
			}
			if (alternateUrl != null)
			{
				setUrlPrefix((SoapHttpClientProtocol) o, alternateUrl);
			}
			services.Add(name, o);
			return o;
		}

		public static void setUrlPrefix(
			SoapHttpClientProtocol client, String url)
		{
			client.Url = Regex.Replace(
				client.Url, @"https://adwords.google.com/", url);
		}

		public static void addUnits(String token, int i)
		{
			if (token != null)
			{
				lock(units)
				{
					if (!units.Contains(token))
					{
						units[token] = 0;
					}
					units[token] = (int) units[token] + i; 
				}
			}
		}

		public int getUnits()
		{
			if (AdWordsUser.units.Contains(this.developerTokenValue.Text[0]))
			{
				return (int) AdWordsUser.units[
					this.developerTokenValue.Text[0]];
			}
			return 0;
		}
	}
}
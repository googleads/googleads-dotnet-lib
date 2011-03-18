// Copyright 2010, Google Inc. All Rights Reserved.
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Xml;

namespace com.google.api.adwords.lib.util {
  /// <summary>
  /// Defines data utility functions for the client library.
  /// </summary>
  public class DataUtilities {
    /// <summary>
    /// Gets all the methods available in AdWords API.
    /// </summary>
    /// <returns>A list of ApiMethod objects.</returns>
    public static List<ApiMethod> GetAllMethods() {
      List<ApiMethod> retVal = new List<ApiMethod>();

      Type[] childTypes = typeof(AdWordsService).GetNestedTypes();

      foreach (Type childType in childTypes) {
        FieldInfo[] fieldInfos = childType.GetFields(BindingFlags.Static | BindingFlags.Public);
        foreach (FieldInfo fieldInfo in fieldInfos) {
          if (fieldInfo.FieldType != typeof(ServiceSignature)) {
            continue;
          }
          ServiceSignature signature = (ServiceSignature) fieldInfo.GetValue(null);
          string version = "";
          if (signature is AdWordsApiServiceSignature) {
            version = (signature as AdWordsApiServiceSignature).version;
          } else if (signature is LegacyAdwordsApiServiceSignature) {
            version = (signature as LegacyAdwordsApiServiceSignature).version;
          }

          if(string.IsNullOrEmpty(version)) {
            continue;
          }

          Type serviceType = Type.GetType("com.google.api.adwords." + version + "." +
              signature.serviceName);
          MethodInfo[] methodInfos = serviceType.GetMethods(BindingFlags.Public |
              BindingFlags.Instance | BindingFlags.DeclaredOnly);
          foreach (MethodInfo methodInfo in methodInfos) {
            if (!methodInfo.IsSpecialName) {
              ApiMethod method = new ApiMethod();
              method.version = version;
              method.serviceName = signature.serviceName;
              method.methodName = methodInfo.Name;
              retVal.Add(method);
            }
          }
        }
      }
      return retVal;
    }

    /// <summary>
    /// Gets a version string for this assembly.
    /// </summary>
    /// <returns>The version string in format Major.Minor.Revision.</returns>
    public static string GetVersion() {
      Version version = Assembly.GetExecutingAssembly().GetName().Version;
      return string.Format("{0}.{1}.{2}", version.Major, version.Minor, version.Revision);
    }

    /// <summary>
    /// Load an embedded csv into memory.
    /// </summary>
    /// <param name="csvName">The csv filename to be loaded.</param>
    /// <returns>The contents of the resource file as a string, or null if the
    /// resource cannot be found.</returns>
    private static string GetCsv(string csvName) {
      Stream resourceStream = null;
      StreamReader reader = null;
      string contents = null;
      try {
        resourceStream = Assembly.GetExecutingAssembly().
          GetManifestResourceStream("com.google.api.adwords.data." + csvName);
        if (resourceStream != null) {
          reader = new StreamReader(resourceStream);
          contents = reader.ReadToEnd();
          reader.Close();
          resourceStream.Close();
          return contents;
        }
      } catch (Exception ex) {
        throw new ApplicationException("Could not load resource '" + csvName + "'. See inner " +
            "exception for details.", ex);
      } finally {
        if (resourceStream != null) {
          resourceStream.Close();
        }
        if (reader != null) {
          reader.Close();
        }
      }
      return contents;
    }

    /// <summary>
    /// Saves the contents of a sandbox account into an XML.
    /// </summary>
    /// <param name="user">The AdWordsUser to be used for downloading sandbox contents.</param>
    /// <param name="fileName">The XML file to which the dump is to be
    /// saved.</param>
    public static void DownloadSandboxContents(AdWordsUser user, string fileName) {
      AccountManager manager = new AccountManager(user);
      ClientAccount[] allClients = manager.DownloadAllAccounts();

      XmlDocument xDoc = null;

      xDoc = new XmlDocument();
      xDoc.LoadXml("<Accounts/>");

      Archiver archiver = new Archiver();

      foreach (ClientAccount account in allClients) {
        XmlElement xClient = xDoc.CreateElement("Account");
        archiver.SerializeAccount(xClient, account);
        xDoc.DocumentElement.AppendChild(xClient);
      }
      xDoc.Save(fileName);
    }

    /// <summary>
    /// Restores the contents of a sandbox account from an XML.
    /// </summary>
    /// <param name="fileName">The XML file containing a sandbox dump.</param>
    /// <param name="user">The AdWordsUser to be used for uploading file contents to the sandbox.
    /// </param>
    public static void RestoreSandboxContents(AdWordsUser user, string fileName) {
      XmlDocument xDoc = new XmlDocument();
      xDoc.Load(fileName);

      Archiver archiver = new Archiver();
      List<ClientAccount> allClients = new List<ClientAccount>();

      XmlNodeList xClients = xDoc.SelectNodes("Accounts/Account");

      foreach (XmlElement xClient in xClients) {
        allClients.Add(archiver.DeSerializeAccount(xClient));
      }

      AccountManager manager = new AccountManager(user);
      manager.UploadAllAccounts(allClients.ToArray());
    }
  }
}

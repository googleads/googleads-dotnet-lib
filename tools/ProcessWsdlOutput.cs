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

// Author: api.anash@gmail.com (Anash P. Oommen)

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace com.google.api.adwords.tools {
  /// <summary>
  /// Main program of this application.
  /// </summary>
  class Program {
    static void Main(string[] args) {
      Dictionary<string, string> cmdLineParams = ParseCommandLine(args, new string[] {"--file="});

      if (cmdLineParams.Count != 1) {
        ShowUsageAndExit();
      }
      ProcessWsdlOutput(cmdLineParams["file"]);
    }

    /// <summary>
    /// Process the C# wrapper file that wsdl.exe generates from AdWords API
    /// web services.
    /// </summary>
    /// <param name="outputFile">The C# file to be processed. This file will be
    /// overwritten by the application.</param>
    private static void ProcessWsdlOutput(string outputFile) {
      string[] searches = {
          "private SoapHeader", "private SoapResponseHeader",
          "public SoapHeader", "public SoapResponseHeader",
          "OperationResult[][]", "Type_AttributeMapEntry[][]",
          "public partial class SoapHeader",
          "public partial class SoapResponseHeader",
          "System.Web.Services.Protocols.SoapHttpClientProtocol"};

      string[] replaces = {
          "private RequestHeader", "private ResponseHeader",
          "public RequestHeader", "public ResponseHeader",
          "OperationResult[]", "Type_AttributeMapEntry[]",
          "public partial class __SoapHeader",
          "public partial class __SoapResponseHeader",
          "AdWordsApiService"};

      string[] operationPatches = {
          "AdGroupAdOperation", "AdGroupCriterionOperation", "AdGroupOperation", "JobOperation",
          "CampaignOperation", "CampaignCriterionOperation", "CampaignAdExtensionOperation",
          "CampaignTargetOperation"
      };

      string[] lines = {};
      using (StreamReader reader = new StreamReader(outputFile)) {
        lines = reader.ReadToEnd().Split(new char[] {'\r', '\n'},
            StringSplitOptions.RemoveEmptyEntries);
      }
      if (lines.Length > 0) {
        using (StreamWriter writer = new StreamWriter(outputFile)) {
          for (int i = 0; i < lines.Length; i++) {
            string line = lines[i];

            // Search and replaces.
            for (int j = 0; j < searches.Length; j++) {
              line = line.Replace(searches[j], replaces[j]);
            }

            // Patch the url.
            string temp = line.Trim();
            if (temp.StartsWith("this.Url = ")) {
              line = "this.Url = \"http://localhost\";";
              if (temp.EndsWith("+")) {
                while (!lines[i].Trim().EndsWith(";")) {
                  i++;
                }
              }
            }

            // Add missing XmlIncludes.
            if (temp.StartsWith("public abstract partial class Operation")) {
              foreach(string operationClass in operationPatches) {
                writer.WriteLine(string.Format(
                    "[System.Xml.Serialization.XmlIncludeAttribute(typeof({0}))]",
                    operationClass));
              }
            }

            // Remove /// <remarks/>
            if (temp == "/// <remarks/>") {
              continue;
            }

            // Patch the way TargetingIdeaPage::entries is deserialized.

            if (temp == "[System.Xml.Serialization.XmlArrayItemAttribute(\"data\", " +
                "typeof(Type_AttributeMapEntry), IsNullable=false)]") {
              continue;
            }


            if (temp == "public Type_AttributeMapEntry[] entries {") {
              string[] targetingIdeaPatch = {
                  "public partial class TargetingIdea {",
                  "  public Type_AttributeMapEntry[] data;",
                  "}",
                  "private System.Collections.Generic.List<TargetingIdea> ideasField =",
                  "    new System.Collections.Generic.List<TargetingIdea>();",
                  "public TargetingIdea[] ideas {",
                  "  get {",
                  "    return ideasField.ToArray();",
                  "  }",
                  "}",
                  "[System.Xml.Serialization.XmlArrayItemAttribute(\"data\", " +
                  "typeof(Type_AttributeMapEntry), IsNullable = false)]",
                  "public Type_AttributeMapEntry[] entries {",
                  "  get {",
                  "    throw new NotSupportedException(\"Use ideas field instead.\");",
                  "  }",
                  "  set {",
                  "    TargetingIdea idea = new TargetingIdea();",
                  "    idea.data = value;",
                  "    ideasField.Add(idea);",
                  "  }",
                  "}"
              };
              foreach (string patchLine in targetingIdeaPatch) {
                writer.WriteLine(patchLine);
              }
              int numCurly = 1;
              while (numCurly != 0) {
                i++;
                if (lines[i].Trim().EndsWith("{")) {
                  numCurly++;
                } else if (lines[i].Trim().EndsWith("}")) {
                  numCurly--;
                }
              }
              continue;
            }

            writer.WriteLine(line);
          }
        }
      }
    }

    /// <summary>
    /// Parse the command line arguments into a dictionary.
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    /// <param name="flags">The command line key names to be extracted.</param>
    /// <returns>A map of command line parameters, with key as the parameter
    /// name and value as the parsed argument value.</returns>
    /// <remarks>All parameters are expected in the form
    /// --option_name=option_value</remarks>
    private static Dictionary<string, string> ParseCommandLine(string[] args, string[] flags) {
      Dictionary<string, string> cmdLineParams = new Dictionary<string, string>();

      for (int i = 0; i < args.Length; i++) {
        for (int j = 0; j < flags.Length; j++) {
          if (args[i].StartsWith(flags[j])) {
            string trimmedFlag = flags[j].Trim('-', '=');
            cmdLineParams[trimmedFlag] = args[i].Substring(flags[j].Length);
          }
        }
      }
      return cmdLineParams;
    }

    /// <summary>
    /// Prints the command line usage and exits the application.
    /// </summary>
    private static void ShowUsageAndExit() {
      Console.WriteLine(
        "Usage : ProcessWsdlOutput.exe --file=C# source file\n\n" +
        "C# source file: The full path of the C# source file to be processed. " +
        "This is often the output file generated by wsdl.exe.");
      Environment.Exit(0);
    }
  }
}

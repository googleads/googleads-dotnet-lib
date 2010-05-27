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
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;

namespace com.google.api.adwords.tools {
  class ProcessWsdl {
    /// <summary>
    /// Main entry point for this class.
    /// </summary>
    /// <param name="cmdLineParams">The command line parameters for this
    /// program.</param>
    /// <returns>True, if the command line parameters were properly
    /// given.</returns>
    public bool Run(Dictionary<string, string> cmdLineParams) {
      if (cmdLineParams.ContainsKey("file")) {
        ProcessWsdlConfig(cmdLineParams["file"]);
        return true;
      } else {
        return false;
      }
    }

    /// <summary>
    /// Processes the wsdl_config.xml, downloads the wsdl files, patch them
    /// and creates a new wsdl_config.xml.
    /// </summary>
    /// <param name="wsdlConfigFile">The path of wsdl_config.xml.</param>
    private void ProcessWsdlConfig(string wsdlConfigFile) {
      XmlDocument doc = new XmlDocument();
      doc.Load(wsdlConfigFile);
      XmlNamespaceManager xmlns = new XmlNamespaceManager(doc.NameTable);
      xmlns.AddNamespace("root", "http://microsoft.com/webReference/");
      XmlNodeList wsdlUrlNodes =
          doc.SelectNodes("/root:wsdlParameters/root:documents/root:document/text()", xmlns);

      string rootDir = Directory.GetCurrentDirectory();
      foreach (XmlNode wsdlUrlNode in wsdlUrlNodes) {
        Uri wsdlUri = new Uri(wsdlUrlNode.Value);
        string serviceName = wsdlUri.Segments[wsdlUri.Segments.Length - 1];
        string localPath = rootDir + "\\" + serviceName + ".wsdl";
        DownloadWsdl(wsdlUri.AbsoluteUri, localPath);
        Uri localUri = new Uri(localPath);
        wsdlUrlNode.Value = localUri.AbsoluteUri;
        if (serviceName.EndsWith(".wsdl")) {
          serviceName = serviceName.Substring(0, serviceName.Length - 5);
        }

        switch (serviceName) {
          case "TargetingIdeaService":
            PatchWithDummy(localPath, "TargetingIdea");
            break;

          case "GeoLocationService":
            PatchWithDummy(localPath, "GeoLocationSelector");
            break;

          case "CampaignTargetService":
            PatchWithDummy(localPath, "CampaignTargetSelector");
            break;

          case "BulkMutateJobService":
            PatchWithDummy(localPath, "OperationStreamResult");
            PatchWithDummy(localPath, "BulkMutateJobPolicy");
            break;
        }
        PatchSoapHeaders(localPath);
      }
      doc.Save("wsdl_config_new.xml");
    }

    /// <summary>
    /// Patch RequestHeader and ResponseHeader soap headers.
    /// </summary>
    /// <param name="localPath">The path of local wsdl file.</param>
    private void PatchSoapHeaders(string localPath) {
      string contents = "";
      using (StreamReader reader = new StreamReader(localPath)) {
        contents = reader.ReadToEnd();
      }

      contents = contents.Replace("\"RequestHeader\"", "\"RequestHeaderTemp\"");
      contents = contents.Replace("\"tns:RequestHeader\"", "\"tns:RequestHeaderTemp\"");
      contents = contents.Replace("\"SoapHeader\"", "\"RequestHeader\"");
      contents = contents.Replace("\"tns:SoapHeader\"", "\"tns:RequestHeader\"");
      contents = contents.Replace("\"cm:SoapHeader\"", "\"cm:RequestHeader\"");

      contents = contents.Replace("\"ResponseHeader\"", "\"ResponseHeaderTemp\"");
      contents = contents.Replace("\"tns:ResponseHeader\"", "\"tns:ResponseHeaderTemp\"");
      contents = contents.Replace("\"SoapResponseHeader\"", "\"ResponseHeader\"");
      contents = contents.Replace("\"tns:SoapResponseHeader\"", "\"tns:ResponseHeader\"");
      contents = contents.Replace("\"cm:SoapResponseHeader\"", "\"cm:ResponseHeader\"");

      using (StreamWriter writer = new StreamWriter(localPath)) {
        writer.Write(contents);
      }
      return;
    }

    /// <summary>
    /// Patch a wsdl complex type by adding a dummy string member
    /// variable to it's definition.
    /// </summary>
    /// <param name="localPath">The path of local wsdl file.</param>
    /// <param name="typeName">The type that should be patched.</param>
    private void PatchWithDummy(string localPath, string typeName) {
      XmlDocument doc = new XmlDocument();
      doc.Load(localPath);
      XmlNamespaceManager xmlns = new XmlNamespaceManager(doc.NameTable);
      xmlns.AddNamespace("root", "http://www.w3.org/2001/XMLSchema");
      xmlns.AddNamespace("wsdl", "http://schemas.xmlsoap.org/wsdl/");
      XmlNode typeNode = doc.SelectSingleNode("/wsdl:definitions/wsdl:types/root:schema/" +
        "root:complexType[@name='" + typeName + "']", xmlns);
      if (typeNode != null) {
        XmlDocumentFragment fragment = doc.CreateDocumentFragment();
        fragment.InnerXml = "<element maxOccurs='1' minOccurs='1' name='dummy' type='xsd:string'" +
           " xmlns='http://www.w3.org/2001/XMLSchema'/>";
        typeNode.SelectSingleNode("root:sequence", xmlns).AppendChild(fragment);
      }
      doc.Save(localPath);
    }

    /// <summary>
    /// Download the wsdl file from server.
    /// </summary>
    /// <param name="url">The wsdl url.</param>
    /// <param name="fileName">The local path to which wsdl should
    /// be saved.</param>
    private void DownloadWsdl(string url, string fileName) {
      WebRequest request = HttpWebRequest.Create(url);
      WebResponse response = request.GetResponse();

      string contents = "";

      using (StreamReader reader = new StreamReader(response.GetResponseStream())) {
        contents = reader.ReadToEnd();
      }
      using (StreamWriter writer = new StreamWriter(fileName)) {
        writer.Write(contents);
      }
      return;
    }
  }
}

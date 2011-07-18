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

// Author: api.anash@gmail.com (Anash P. Oommen)

using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Google.Api.Ads.Common.Lib {
  /// <summary>
  /// Listens to SOAP messages sent and received by this library.
  /// </summary>
  public abstract class TraceListener : SoapListener {
    /// <summary>
    /// The filename to which we log the SOAP messages.
    /// </summary>
    protected string soapFileName;

    /// <summary>
    /// The filename to which we log the request info.
    /// </summary>
    protected string requestInfoFileName;

    /// <summary>
    /// Should we log the SOAP messages to file?
    /// </summary>
    private bool logToFile;

    /// <summary>
    /// Should we log the SOAP messages to console?
    /// </summary>
    private bool logToConsole;

    /// <summary>
    /// Should we only log SOAP messages corresponding to an error?
    /// </summary>
    private bool logErrorsOnly;

    /// <summary>
    /// Maximum number of attempts to write to log file if it is locked.
    /// </summary>
    private const int MAX_ATTEMPTS = 3;

    /// <summary>
    /// Protected constructor.
    /// </summary>
    /// <param name="config">The config class.</param>
    protected TraceListener(AppConfigBase config) : base(config) {
      string logPath = "";
      if (config.LogToFile) {
        logToFile = config.LogToFile;
        logPath = config.LogPath.TrimEnd('\\', '/') + Path.DirectorySeparatorChar;
        if (!Directory.Exists(logPath)) {
          Directory.CreateDirectory(logPath);
        }
        soapFileName = logPath + "soap_xml.log";
        requestInfoFileName = logPath + "request_info.log";
      } else {
        logPath = "";  // default location for SOAP logs
      }

      // should we log to console as well?
      logToConsole = config.LogToConsole;
      logErrorsOnly = config.LogErrorsOnly;
    }

    /// <summary>
    /// Handles the SOAP message.
    /// </summary>
    /// <param name="soapMessage">The SOAP message.</param>
    /// <param name="service">The SOAP service.</param>
    /// <param name="direction">The direction of message.</param>
    public override void HandleMessage(XmlDocument soapMessage, AdsClient service,
        Direction direction) {
      if (direction == Direction.OUT) {
        ContextStore.AddKey("SoapRequest", soapMessage.OuterXml);
      } else {
        ContextStore.AddKey("SoapResponse", soapMessage.OuterXml);
      }
      if (direction == Direction.IN) {
        PerformLogging(service, (string) ContextStore.GetValue("SoapRequest"),
            (string) ContextStore.GetValue("SoapResponse"));
      }
    }

    /// <summary>
    /// Cleans up any resources after an API call.
    /// </summary>
    public override void CleanupAfterCall() {
      base.CleanupAfterCall();
      ContextStore.RemoveKey("SoapRequest");
      ContextStore.RemoveKey("SoapResponse");
    }

    /// <summary>
    /// Performs the SOAP and HTTP logging.
    /// </summary>
    /// <param name="service">The SOAP service.</param>
    /// <param name="soapResponse">The SOAP response xml.</param>
    /// <param name="soapRequest">The SOAP request xml.</param>
    private void PerformLogging(AdsClient service, string soapRequest, string soapResponse) {
      if (service == null || service.User == null || soapRequest == null || soapResponse == null) {
        return;
      }

      if (config.MaskCredentials) {
        XmlDocument xDoc = new XmlDocument();
        xDoc.LoadXml(soapRequest);
        MaskCredentialsInLogs(xDoc, GetFieldsToMask());
        soapRequest = xDoc.OuterXml;
      }

      string formattedSoapRequest = FormatSoapRequest(service.LastRequest, soapRequest);
      string formattedSoapResponse = FormatSoapResponse(service.LastResponse, soapResponse);
      string formattedHttpRequest = FormatHttpRequest(soapRequest);
      string formattedHttpResponse = FormatHttpResponse(soapResponse);

      string soapLog = formattedSoapRequest + formattedSoapResponse;
      string requestLog = string.Format(CultureInfo.InvariantCulture, "host={0},url={1},{2},{3}",
          service.LastRequest.RequestUri.Host, service.LastRequest.RequestUri.AbsolutePath,
          formattedHttpRequest, formattedHttpResponse);

      bool isError = service.LastResponse != null && service.LastResponse is HttpWebResponse &&
          (service.LastResponse as HttpWebResponse).StatusCode ==
              HttpStatusCode.InternalServerError;

      if (!logErrorsOnly || logErrorsOnly && isError) {
        if (logToFile) {
          WriteToFile(soapFileName, soapLog);
          WriteToFile(requestInfoFileName, requestLog);
        }
        if (logToConsole) {
          WriteToStream(Console.Out, soapLog);
          WriteToStream(Console.Out, requestLog);
        }
      }
    }

    /// <summary>
    /// Gets a list of fields to be masked in xml logs.
    /// </summary>
    /// <returns>The list of fields to be masked.</returns>
    protected abstract string[] GetFieldsToMask();

    /// <summary>
    /// Writes a log string into a specified log file.
    /// </summary>
    /// <param name="fileName">The file to which the log text should be written.
    /// </param>
    /// <param name="logText">The log text to be written to the file.</param>
    private static void WriteToFile(string fileName, string logText) {
      for (int i = 0; i < MAX_ATTEMPTS; i++) {
        try {
          StreamWriter writer = new StreamWriter(fileName, true);
          writer.WriteLine(logText);
          writer.Close();
          break;
        } catch (Exception) {
          Thread.Sleep(100 + new Random().Next(1000));
        }
      }
    }

    /// <summary>
    /// Writes a log string into a specified stream.
    /// </summary>
    /// <param name="fileName">The text writer to which the log text should
    /// be written.</param>
    /// <param name="logText">The log text to be written to the stream.</param>
    private void WriteToStream(TextWriter writer, string logText) {
      for (int i = 0; i < MAX_ATTEMPTS; i++) {
        try {
          writer.WriteLine(logText);
          break;
        } catch (Exception) {
          Thread.Sleep(100 + new Random().Next(1000));
        }
      }
    }

    /// <summary>
    /// Creates a formatted http request text, to be written into HTTP logs.
    /// </summary>
    /// <param name="soapRequest">The request xml for this SOAP call.</param>
    /// <returns>A formatted string that represents the HTTP request.</returns>
    protected virtual string FormatHttpRequest(string soapRequest) {
      return "";
    }

    /// <summary>
    /// Creates a formatted http response text, to be written into HTTP logs.
    /// </summary>
    /// <param name="soapResponse">The response xml for this SOAP call.</param>
    /// <returns>A formatted string that represents the HTTP response.</returns>
    protected virtual string FormatHttpResponse(string soapResponse) {
      return "";
    }

    /// <summary>
    /// Creates a formatted soap request text, to be written into SOAP logs.
    /// </summary>
    /// <param name="webRequest">The web request for this SOAP call.</param>
    /// <param name="soapRequest">The request xml for this SOAP call.</param>
    /// <returns>A formatted string that represents the SOAP request.</returns>
    protected virtual string FormatSoapRequest(WebRequest webRequest, string soapRequest) {
      StringBuilder builder = new StringBuilder();
      builder.AppendFormat("-----------------BEGIN API CALL---------------------\r\n");
      builder.AppendFormat("\r\nRequest\r\n");
      builder.AppendFormat("-------\r\n\r\n");

      StringBuilder headerBuilder = new StringBuilder();

      headerBuilder.AppendFormat("{0} {1}\r\n", webRequest.Method,
          webRequest.RequestUri.AbsolutePath);
      foreach (string key in webRequest.Headers) {
        headerBuilder.AppendFormat("{0}: {1}\r\n", key, webRequest.Headers[key]);
      }
      headerBuilder.AppendFormat("TimeStamp: {0}\r\n", DateTime.Now.ToString("R"));
      builder.AppendFormat("\r\n{0}\r\n", AppendHeadersToSoapXml(soapRequest,
          headerBuilder.ToString()));
      return builder.ToString();
    }

    /// <summary>
    /// Creates a formatted soap response text, to be written into SOAP logs.
    /// </summary>
    /// <param name="webResponse">The web response for this SOAP call.</param>
    /// <param name="soapResponse">The response xml for this SOAP call.</param>
    /// <returns>A formatted string that represents the SOAP response.</returns>
    protected virtual string FormatSoapResponse(WebResponse webResponse, string soapResponse) {
      StringBuilder builder = new StringBuilder();
      builder.AppendFormat("\r\nResponse\r\n");
      builder.AppendFormat("--------\r\n\r\n");

      StringBuilder headerBuilder = new StringBuilder();
      foreach (string key in webResponse.Headers) {
        headerBuilder.AppendFormat("{0}: {1}\r\n", key, webResponse.Headers[key]);
      }
      headerBuilder.AppendFormat("TimeStamp: {0}\r\n", DateTime.Now.ToString("R"));
      builder.AppendFormat("\r\n{0}\r\n", AppendHeadersToSoapXml(soapResponse,
          headerBuilder.ToString()));

      builder.AppendFormat("-----------------END API CALL-----------------------\r\n");
      return builder.ToString();
    }

    /// <summary>
    /// Appends the HTTP headers to SOAP xml.
    /// </summary>
    /// <param name="soapRequest">The SOAP request.</param>
    /// <param name="headers">The HTTP headers.</param>
    /// <returns>The modified SOAP xml for appending to the logs.</returns>
    protected string AppendHeadersToSoapXml(string soapRequest, string headers) {
      try {
        XmlDocument xDoc = new XmlDocument();
        xDoc.LoadXml(soapRequest);
        XmlComment comment = xDoc.CreateComment(headers);
        xDoc.DocumentElement.InsertBefore(comment, xDoc.DocumentElement.FirstChild);
        return xDoc.OuterXml;
      } catch {
        return string.Format("{0}\r\n{1}\r\n", headers, soapRequest);
      }
    }

    /// <summary>
    /// Masks a list of fields in the SOAP message before logging it.
    /// </summary>
    /// <param name="soapMessageXml">The SOAP message, loaded as an XmlDocument.
    /// </param>
    /// <param name="fieldNames">The list of field names to be masked.</param>
    protected void MaskCredentialsInLogs(XmlDocument soapMessageXml, string[] fieldNames) {
      XmlNamespaceManager xmlns = new XmlNamespaceManager(soapMessageXml.NameTable);
      xmlns.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
      XmlNodeList nodes =
          soapMessageXml.SelectNodes("soap:Envelope/soap:Header/descendant::*", xmlns);

      foreach (XmlElement node in nodes) {
        if (Array.Exists<string>(fieldNames, delegate(string match) {
          return string.Compare(match, node.Name, true) == 0;
        })) {
          node.InnerText = "******";
        }
      }
    }
  }
}

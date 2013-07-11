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

using Google.Api.Ads.Common.Util;

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
    /// The config class to be used with this class.
    /// </summary>
    private AppConfig config;

    /// <summary>
    /// The writer for writing trace logs.
    /// </summary>
    private TraceWriter writer;

    /// <summary>
    /// Gets the config class to be used with this class.
    /// </summary>
    public AppConfig Config {
      get {
        return config;
      }
    }

    /// <summary>
    /// Gets or sets the writer for writing trace logs.
    /// </summary>
    public TraceWriter Writer {
      get {
        return writer;
      }
      set {
        writer = value;
      }
    }

    /// <summary>
    /// Protected constructor.
    /// </summary>
    /// <param name="config">The config class.</param>
    protected TraceListener(AppConfig config) {
      this.config = config;
      if (this.config.LogToFile) {
        this.writer = new DefaultTraceWriter(config);
      }
    }

    /// <summary>
    /// Initializes the listener for handling an API call.
    /// </summary>
    public void InitForCall() {
    }

    /// <summary>
    /// Handles the SOAP message.
    /// </summary>
    /// <param name="soapMessage">The SOAP message.</param>
    /// <param name="service">The SOAP service.</param>
    /// <param name="direction">The direction of message.</param>
    public void HandleMessage(XmlDocument soapMessage, AdsClient service,
        SoapMessageDirection direction) {
      if (direction == SoapMessageDirection.OUT) {
        ContextStore.AddKey("SoapRequest", soapMessage.OuterXml);
      } else {
        ContextStore.AddKey("SoapResponse", soapMessage.OuterXml);
      }
      if (direction == SoapMessageDirection.IN) {
        PerformLogging(service, (string) ContextStore.GetValue("SoapRequest"),
            (string) ContextStore.GetValue("SoapResponse"));
      }
    }

    /// <summary>
    /// Cleans up any resources after an API call.
    /// </summary>
    public void CleanupAfterCall() {
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
        XmlDocument xDoc = SerializationUtilities.LoadXml(soapRequest);
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

      if (!config.LogErrorsOnly || config.LogErrorsOnly && isError) {
        if (config.LogToFile && this.writer != null) {
          writer.Write(soapLog, requestLog);
        }
      }
    }

    /// <summary>
    /// Gets a list of fields to be masked in xml logs.
    /// </summary>
    /// <returns>The list of fields to be masked.</returns>
    protected abstract string[] GetFieldsToMask();

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
      headerBuilder.AppendFormat("TimeStamp: {0}\r\n", this.GetTimeStamp());
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
      headerBuilder.AppendFormat("TimeStamp: {0}\r\n", this.GetTimeStamp());
      builder.AppendFormat("\r\n{0}\r\n", AppendHeadersToSoapXml(soapResponse,
          headerBuilder.ToString()));

      builder.AppendFormat("-----------------END API CALL-----------------------\r\n");
      return builder.ToString();
    }

    /// <summary>
    /// Gets the current timestamp as a formatted string.
    /// </summary>
    /// <returns>The current timestamp.</returns>
    protected virtual string GetTimeStamp() {
      return DateTime.Now.ToString("R");
    }

    /// <summary>
    /// Appends the HTTP headers to SOAP xml.
    /// </summary>
    /// <param name="soapRequest">The SOAP request.</param>
    /// <param name="headers">The HTTP headers.</param>
    /// <returns>The modified SOAP xml for appending to the logs.</returns>
    protected string AppendHeadersToSoapXml(string soapRequest, string headers) {
      try {
        XmlDocument xDoc = SerializationUtilities.LoadXml(soapRequest);
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
          return string.Compare(match, node.LocalName, true) == 0;
        })) {
          node.InnerText = "******";
        }
      }
    }
  }
}

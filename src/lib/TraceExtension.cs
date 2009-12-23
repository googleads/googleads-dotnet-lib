// This file is based on the code sample available at http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpguide/html/cpconAlteringSOAPMessageUsingSOAPExtensions.asp

using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;

namespace com.google.api.adwords.lib {
  /// <summary>
  /// Define a SOAP Extension that traces the SOAP request and SOAP response
  /// for the XML Web service method the SOAP extension is applied to.
  /// </summary>
  public class TraceExtension : SoapExtension {
    /// <summary>
    /// The old stream, which we replaced in <see cref="ChainStream"/>.
    /// </summary>
    private Stream oldStream;

    /// <summary>
    /// The new stream, which we substituted for, in <see cref="ChainStream"/>.
    /// </summary>
    private Stream newStream;

    /// <summary>
    /// The filename to which we log the SOAP messages.
    /// </summary>
    private string soapFileName;

    /// <summary>
    /// The filename to which we log the HTTP requests.
    /// </summary>
    private string httpFileName;

    /// <summary>
    /// Should we log the SOAP messages to file?
    /// </summary>
    private bool logToFile;

    /// <summary>
    /// Should we log the SOAP messages to console?
    /// </summary>
    private bool logToConsole;

    /// <summary>
    /// Save the Stream representing the SOAP request or SOAP response into
    /// a local memory buffer.
    /// </summary>
    /// <param name="stream">The original stream</param>
    /// <returns>The new stream.</returns>
    public override Stream ChainStream(Stream stream) {
      oldStream = stream;
      newStream = new MemoryStream();
      return newStream;
    }

    /// <summary>
    /// When the SOAP extension is accessed for the first time, the XML Web
    /// service method it is applied to is accessed to store the file
    /// name passed in, using the corresponding SoapExtensionAttribute.
    /// </summary>
    /// <param name="methodInfo">The method being called.</param>
    /// <param name="attribute">Decorating attribute for the method.</param>
    /// <returns>An initializer object.</returns>
    public override object GetInitializer(LogicalMethodInfo methodInfo,
        SoapExtensionAttribute attribute) {
      return methodInfo.DeclaringType;
    }

    /// <summary>
    /// The SOAP extension was configured to run using a configuration file
    /// instead of an attribute applied to a specific XML Web service
    /// method.
    /// </summary>
    /// <param name="WebServiceType">The type of the webservice being
    /// used.</param>
    /// <returns>An initializer object.</returns>
    public override object GetInitializer(Type WebServiceType) {
      return WebServiceType;
    }

    /// <summary>
    /// Receive the file name stored by GetInitializer and store it in a
    /// member variable for this specific instance.
    /// </summary>
    /// <param name="initializer">Initializer object, passed on from
    /// GetInitializer().</param>
    public override void Initialize(object initializer) {
      Type WebServiceType = (Type) initializer;

      string logPath = "";
      if (ApplicationConfiguration.logToFile) {
        logToFile = ApplicationConfiguration.logToFile;
        logPath = ApplicationConfiguration.logPath;
        if (!Directory.Exists(logPath)) {
          Directory.CreateDirectory(logPath);
        }
        soapFileName = logPath + "soap_xml.log";
        httpFileName = logPath + "http_logs.log";
      } else {
        logPath = "";  // default location for SOAP logs
      }

      // should we log to console as well?

      logToConsole = ApplicationConfiguration.logToConsole;
    }

    /// <summary>
    /// Process the messages passing in and out of the SOAP services.
    /// </summary>
    /// <param name="message">The current SOAP message.</param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public override void ProcessMessage(SoapMessage message) {
      switch (message.Stage) {
        case SoapMessageStage.BeforeSerialize:
          break;
        case SoapMessageStage.AfterSerialize:
          CopyContentsToOldStream();
          SaveStreamContents(true);
          break;
        case SoapMessageStage.BeforeDeserialize:
          CopyContentsFromOldStream();
          SaveStreamContents(false);
          break;
        case SoapMessageStage.AfterDeserialize:
          PerformLogging();
          break;
        default: throw new Exception("Invalid stage");
      }
    }

    /// <summary>
    /// Perform the SOAP and HTTP logging.
    /// </summary>
    private void PerformLogging() {
      AdWordsUser user = null;
      SoapServiceBase service = null;
      String soapRequest = null;
      String soapResponse = null;

      if (HttpContext.Current != null) {
        user = (AdWordsUser) HttpContext.Current.Items["AdWordsParent"];
        service = (SoapServiceBase) HttpContext.Current.Items["SoapService"];
        soapRequest = (string) HttpContext.Current.Items["SoapRequest"];
        soapResponse = (string) HttpContext.Current.Items["SoapResponse"];
      } else {
        user = (AdWordsUser) CallContext.GetData("AdWordsParent");
        service = (SoapServiceBase) CallContext.GetData("SoapService");
        soapRequest = (string) CallContext.GetData("SoapRequest");
        soapResponse = (string) CallContext.GetData("SoapResponse");
      }

      if (user == null || service == null || soapRequest == null || soapResponse == null) {
        return;
      }

      if (ApplicationConfiguration.maskCredentials) {
        XmlDocument xDoc = new XmlDocument();
        xDoc.LoadXml(soapRequest);
        MaskCredentialsInLogs(xDoc);
        soapRequest = xDoc.OuterXml;
      }

      string formattedSoapRequest = FormatSoapRequest(service.Request, soapRequest);
      string formattedSoapResponse = FormatSoapResponse(service.Response, soapResponse);
      string formattedHttpRequest = FormatHttpRequest(soapRequest);
      string formattedHttpResponse = FormatHttpResponse(soapResponse);

      string soapLog = formattedSoapRequest + formattedSoapResponse;
      string httpLog = string.Format("host={0},url={1},{2},{3}", service.Request.RequestUri.Host,
          service.Request.RequestUri.AbsolutePath, formattedHttpRequest, formattedHttpResponse);

      if (logToFile) {
        WriteToFile(soapFileName, soapLog);
        WriteToFile(httpFileName, httpLog);
      }
    }

    /// <summary>
    /// Writes a log string into a specified log file.
    /// </summary>
    /// <param name="fileName">The file to which the log text should be written.</param>
    /// <param name="logText">The log text to be written to the file.</param>
    private void WriteToFile(string fileName, string logText) {
      for (int i = 0; i < 3; i++) {
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
    /// Create a formatted http request text, to be written into HTTP logs.
    /// </summary>
    /// <param name="soapRequest">The request xml for this SOAP call.</param>
    /// <returns>A formatted string that represents the HTTP request.</returns>
    private string FormatHttpRequest(string soapRequest) {
      XmlDocument xDoc = new XmlDocument();
      xDoc.LoadXml(soapRequest);
      XmlNamespaceManager xmlns = new XmlNamespaceManager(xDoc.NameTable);
      xmlns.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
      XmlNode methodNode =
          xDoc.SelectSingleNode("soap:Envelope/soap:Body/*", xmlns);
      string operators = "None";
      if (methodNode.Name == "mutate") {
        operators = "";
        foreach (XmlNode child in methodNode.ChildNodes) {
          if (child.Name == "operations") {
            foreach (XmlNode grandChild in child.ChildNodes) {
              if (grandChild.Name == "operator") {
                operators += grandChild.InnerText + "|";
              }
            }
          }
        }
        operators = operators.TrimEnd('|');
      }
      return string.Format("method={0},operator={1}", methodNode.Name, operators);
    }

    /// <summary>
    /// Create a formatted http response text, to be written into HTTP logs.
    /// </summary>
    /// <param name="soapResponse">The response xml for this SOAP call.</param>
    /// <returns>A formatted string that represents the HTTP response.</returns>
    private string FormatHttpResponse(string soapResponse) {
      XmlDocument xDoc = new XmlDocument();
      xDoc.LoadXml(soapResponse);
      XmlNamespaceManager xmlns = new XmlNamespaceManager(xDoc.NameTable);
      xmlns.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
      XmlNodeList childNodes =
          xDoc.SelectNodes("soap:Envelope/soap:Header/*", xmlns);
      if (childNodes.Count == 1 && childNodes[0].Name == "ResponseHeader") {
        childNodes = childNodes[0].ChildNodes;
      }
      StringBuilder responseText = new StringBuilder();
      foreach (XmlNode childNode in childNodes) {
        if (childNode is XmlElement) {
          responseText.AppendFormat("{0}={1},", childNode.Name, childNode.InnerText);
        }
      }
      return responseText.ToString().TrimEnd(',');
    }

    /// <summary>
    /// Create a formatted soap request text, to be written into SOAP logs.
    /// </summary>
    /// <param name="webRequest">The web request for this SOAP call.</param>
    /// <param name="soapRequest">The request xml for this SOAP call.</param>
    /// <returns>A formatted string that represents the SOAP request.</returns>
    private string FormatSoapRequest(WebRequest webRequest, string soapRequest) {
      StringBuilder builder = new StringBuilder();
      builder.AppendFormat("-----------------BEGIN API CALL---------------------\r\n");
      builder.AppendFormat("\r\nRequest\r\n");
      builder.AppendFormat("-------\r\n\r\n");
      builder.AppendFormat("{0} {1}\r\n", webRequest.Method, webRequest.RequestUri.AbsolutePath);
      foreach (string key in webRequest.Headers) {
        builder.AppendFormat("{0}: {1}\r\n", key, webRequest.Headers[key]);
      }
      builder.AppendFormat("\r\n{0}\r\n", soapRequest);
      return builder.ToString();
    }

    /// <summary>
    /// Create a formatted soap response text, to be written into SOAP logs.
    /// </summary>
    /// <param name="webResponse">The web response for this SOAP call.</param>
    /// <param name="soapResponse">The response xml for this SOAP call.</param>
    /// <returns>A formatted string that represents the SOAP response.</returns>
    private string FormatSoapResponse(WebResponse webResponse, string soapResponse) {
      StringBuilder builder = new StringBuilder();
      builder.AppendFormat("\r\nResponse\r\n");
      builder.AppendFormat("--------\r\n\r\n");

      foreach (string key in webResponse.Headers) {
        builder.AppendFormat("{0}: {1}\r\n", key, webResponse.Headers[key]);
      }
      builder.AppendFormat("\r\n{0}\r\n", soapResponse);
      builder.AppendFormat("-----------------END API CALL-----------------------\r\n");
      return builder.ToString();
    }

    /// <summary>
    /// Mask password, authToken, applicationToken and developerToken from the
    /// soap message before logging it.
    /// </summary>
    /// <param name="soapMessageXml">The SOAP message, loaded as an XmlDocument.</param>
    private static void MaskCredentialsInLogs(XmlDocument soapMessageXml) {
      XmlNamespaceManager xmlns = new XmlNamespaceManager(soapMessageXml.NameTable);
      xmlns.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
      XmlNodeList headerNodes =
          soapMessageXml.SelectNodes("soap:Envelope/soap:Header/*", xmlns);
      foreach (XmlElement headerNode in headerNodes) {
        switch (headerNode.Name) {
          case "password":
            headerNode.InnerText = "********";
            break;

          case "developerToken":
          case "applicationToken":
            if (headerNode.InnerText.Length > 4) {
              headerNode.InnerText =
                  "********" + headerNode.InnerText.Substring(headerNode.InnerText.Length - 4);
            }
            break;

          case "RequestHeader":
            XmlNodeList childNodes = headerNode.SelectNodes("*");
            foreach (XmlElement childNode in childNodes) {
              switch (childNode.Name) {
                case "authToken":
                  childNode.InnerText = "********";
                  break;

                case "developerToken":
                case "applicationToken":
                  if (childNode.InnerText.Length > 4) {
                    childNode.InnerText =
                        "********" + childNode.InnerText.Substring(childNode.InnerText.Length - 4);
                  }
                  break;
              }
            }
            break;
        }
      }
    }

    /// <summary>
    /// Save the contents of the Soap stream into CallContext.
    /// </summary>
    /// <param name="isRequest">True, if the Soap stream contains the http
    /// request.</param>
    /// <remarks>We store the stream contents to callcontext rather than dumping
    /// it immediately to file, to ensure that request and response xmls appear
    /// as a pair when making multithreaded calls.</remarks>
    private void SaveStreamContents(bool isRequest) {
      bool isAdWordsCall = false;

      if (HttpContext.Current != null) {
        isAdWordsCall = HttpContext.Current.Items.Contains("AdWordsParent");
      } else {
        isAdWordsCall = CallContext.GetData("AdWordsParent") != null;
      }

      if (!isAdWordsCall) {
        return;
      }

      MemoryStream memStream = (MemoryStream) newStream;
      string key = isRequest ? "SoapRequest" : "SoapResponse";
      string value = Encoding.UTF8.GetString(memStream.ToArray());

      if (HttpContext.Current != null) {
        HttpContext.Current.Items.Add(key, value);
      } else {
        CallContext.SetData(key, value);
      }
    }

    /// <summary>
    /// Copy the contents from new stream to old stream.
    /// </summary>
    private void CopyContentsToOldStream() {
      newStream.Position = 0;
      Copy(newStream, oldStream);
    }

    /// <summary>
    /// Copy the contents from old stream to new stream.
    /// </summary>
    private void CopyContentsFromOldStream() {
      Copy(oldStream, newStream);
      newStream.Position = 0;
    }

    /// <summary>
    /// Copy contents from one stream to another.
    /// </summary>
    /// <param name="from">Stream from which copying is done.</param>
    /// <param name="to">Stream to which copying is done.</param>
    private void Copy(Stream from, Stream to) {
      TextReader reader = new StreamReader(from);
      TextWriter writer = new StreamWriter(to);
      writer.WriteLine(reader.ReadToEnd());
      writer.Flush();
    }
  }

  /// <summary>
  /// SoapExtensionAttribute for the SOAP Extension that can be
  /// applied to an XML Web service method to enable tracing.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method)]
  public class TraceExtensionAttribute : SoapExtensionAttribute {
    /// <summary>
    /// Return the type of Extension.
    /// </summary>
    public override Type ExtensionType {
      get {return typeof(TraceExtension);}
    }

    /// <summary>
    /// Return the priority of this attribute.
    /// </summary>
    public override int Priority {
      get {
        return priority;
      }
      set {
        priority = value;
      }
    }

    /// <summary>
    /// stores the priority for this trace attribute.
    /// </summary>
    private int priority;
  }
}

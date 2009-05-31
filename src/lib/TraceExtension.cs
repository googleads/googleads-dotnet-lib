// This file is based on the code sample available at http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpguide/html/cpconAlteringSOAPMessageUsingSOAPExtensions.asp

using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;

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
    private string fileName;

    /// <summary>
    /// Should we log the SOAP messages to file?
    /// </summary>
    private bool logToFile;

    /// <summary>
    /// Should we log the SOAP messages to console?
    /// </summary>
    private bool logToConsole;

    /// <summary>
    /// Content type of input SOAP messages.
    /// </summary>
    string inputContentType;

    /// <summary>
    /// Content type of output SOAP messages.
    /// </summary>
    string outputContentType;

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
        fileName = logPath + WebServiceType.FullName + ".log";
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
          outputContentType = message.ContentType;
          break;
        case SoapMessageStage.AfterSerialize:
          if (logToFile && !string.IsNullOrEmpty(fileName)) {
            StreamWriter writer = new StreamWriter(fileName, true);
            WriteOutput(writer, message);
            writer.Close();
          }
          if (logToConsole == true) {
            WriteOutput(Console.Out, message);
          }
          CopyContentsToOldStream();
          outputContentType = "";
          break;
        case SoapMessageStage.BeforeDeserialize:
          inputContentType = message.ContentType;
          CopyContentsFromOldStream();
          if (logToFile && !string.IsNullOrEmpty(fileName)) {
            StreamWriter writer = new StreamWriter(fileName, true);
            WriteInput(writer, message);
            writer.Close();
          }
          if (logToConsole == true) {
            WriteInput(Console.Out, message);
          }
          inputContentType = "";
          break;
        case SoapMessageStage.AfterDeserialize:
          break;
        default: throw new Exception("Invalid stage");
      }
    }

    /// <summary>
    /// Writes an outgoing SOAP message to a textwriter.
    /// </summary>
    /// <param name="textWriter">The textwriter to which the message is
    /// to be written.</param>
    /// <param name="message">The message to be written.</param>
    private void WriteOutput(TextWriter textWriter, SoapMessage message) {
      newStream.Position = 0;
      string soapString =
          (message is SoapServerMessage) ? "SoapResponse" : "SoapRequest";

      string soapHeaderString = "";

      if (message is SoapClientMessage) {
        SoapClientMessage clientMessage = (SoapClientMessage)message;
        HttpWebClientProtocol hwcp =
            (HttpWebClientProtocol)clientMessage.Client;

        // Gather the header information
        System.Uri uri = new Uri(message.Url);
        String host = uri.Host;
        String path = uri.LocalPath;
        String userAgent = hwcp.UserAgent;
        String contentLength = clientMessage.Stream.Length.ToString();
        String soapAction = clientMessage.MethodInfo.BeginMethodInfo.Name;

        soapHeaderString += "POST " + path + " HTTP/1.0\n";
        soapHeaderString += "Host: " + host + "\n";
        soapHeaderString += "User-agent: " + userAgent + "\n";
        soapHeaderString += "Content-type: " + outputContentType + "\n";
        soapHeaderString += "Content-length: " + contentLength + "\n";
        soapHeaderString += "SOAPAction: \"" + soapAction + "\"";
      }

      MemoryStream cleanStream = new MemoryStream();
      CopyAndCleanXmlStream(newStream, cleanStream);

      MemoryStream w = new MemoryStream();
      StreamWriter writer = new StreamWriter(w);
      writer.WriteLine("-----" + soapString + " at " + DateTime.Now + "-----");
      writer.WriteLine(soapHeaderString);
      writer.WriteLine("--------------------------------------------------");
      writer.Flush();
      Copy(cleanStream, w);
      writer.WriteLine("--------------------------------------------------");
      writer.Flush();
      writer.Close();
      cleanStream.Close();
      textWriter.WriteLine(Encoding.UTF8.GetString(w.ToArray()));
      newStream.Position = 0;
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
    /// Writes an outgoing SOAP message to a textwriter.
    /// </summary>
    /// <param name="textWriter">The textwriter to which the message is
    /// to be written.</param>
    /// <param name="message">The message to be written.</param>
    private void WriteInput(TextWriter textWriter, SoapMessage message) {
      string soapHeaderString = "";

      if (message is SoapClientMessage) {
        // Gather the header information
        SoapClientMessage clientMessage = (SoapClientMessage)message;

        String contentLength = newStream.Length.ToString();
        String soapAction = clientMessage.MethodInfo.BeginMethodInfo.Name;

        soapHeaderString += "Content-type: " + inputContentType + "\n";
        soapHeaderString += "Content-length: " + contentLength + "\n";
        soapHeaderString += "SOAPAction: \"" + soapAction + "\"";
      }

      string soapString =
          (message is SoapServerMessage) ? "SoapRequest" : "SoapResponse";

      MemoryStream cleanStream = new MemoryStream();
      CopyAndCleanXmlStream(newStream, cleanStream);
      MemoryStream w = new MemoryStream();
      StreamWriter writer = new StreamWriter(w);

      writer.WriteLine("-----" + soapString +  " at " + DateTime.Now + "-----");
      writer.WriteLine(soapHeaderString);
      writer.WriteLine("--------------------------------------------------");
      writer.Flush();
      Copy(cleanStream, w);
      writer.WriteLine("--------------------------------------------------");
      writer.Flush();
      writer.Close();
      cleanStream.Close();
      textWriter.WriteLine(Encoding.UTF8.GetString(w.ToArray()));
    }

    /// <summary>
    /// Load the XML from oldStream and save a human readable version
    /// in cleanedUpStream.
    /// </summary>
    /// <param name="oldStream">The old stream from which the contents
    /// are to be copied.</param>
    /// <param name="cleanedUpStream">The new stream, which holds the
    /// formatted xml contents.</param>
    private void CopyAndCleanXmlStream(Stream oldStream,
        Stream cleanedUpStream) {
      // Save the position of oldStream before reading it
      long oldPosition = oldStream.Position;
      oldStream.Position = 0;

      // Load the XML writer
      XmlTextWriter xmlWriter = new XmlTextWriter(cleanedUpStream,
          System.Text.Encoding.UTF8);
      xmlWriter.Indentation = 2;
      xmlWriter.IndentChar = ' ';
      xmlWriter.Formatting = Formatting.Indented;

      // Load the XML from oldStream and write to cleanedUpStream
      XmlTextReader xmlReader = new XmlTextReader(oldStream);
      XmlDocument xml = new XmlDocument();
      xml.Load(xmlReader);
      xml.WriteTo(xmlWriter);
      xmlWriter.Flush();
      cleanedUpStream.Flush();

      cleanedUpStream.Position = 0;
      oldStream.Position = oldPosition;
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

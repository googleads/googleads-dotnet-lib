// Source: http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpguide/html/cpconAlteringSOAPMessageUsingSOAPExtensions.asp

namespace com.google.api.adwords.lib {
  using System;
  using System.Collections;
  using System.IO;
  using System.Net;
  using System.Web;
  using System.Web.Services;
  using System.Web.Services.Protocols;
  using System.Xml;

  // Define a SOAP Extension that traces the SOAP request and SOAP response     
  // for the XML Web service method the SOAP extension is applied to.
  public class TraceExtension : SoapExtension {
    Stream oldStream;
    Stream newStream;
    string fileName;
    string inputContentType;
    string outputContentType;

    // Save the Stream representing the SOAP request or SOAP response into
    // a local memory buffer.
    public override Stream ChainStream(Stream stream) {
      oldStream = stream;
      newStream = new MemoryStream();
      return newStream;
    }

    // When the SOAP extension is accessed for the first time, the XML Web
    // service method it is applied to is accessed to store the file
    // name passed in, using the corresponding SoapExtensionAttribute.
    public override object GetInitializer(LogicalMethodInfo methodInfo,
        SoapExtensionAttribute attribute) {
      return ((TraceExtensionAttribute) attribute).Filename;
    }

    // The SOAP extension was configured to run using a configuration file
    // instead of an attribute applied to a specific XML Web service
    // method.
    public override object GetInitializer(Type WebServiceType) {
      // Gets SOAP logs path from App.config file
      Hashtable headers = (Hashtable) System.Configuration.
          ConfigurationSettings.GetConfig("adwordsHeaders");
      String logPath;
      if (headers["logPath"] != null) {
        logPath = (String) headers["logPath"] + "\\";
        if (!Directory.Exists(logPath)) {
          Directory.CreateDirectory(logPath);
        }
      }
      else {
        logPath = "C:\\";  // default location for SOAP logs
      }

      // Return a file name to log the trace information to, based on the
      // type.
      return logPath + WebServiceType.FullName + ".log";
    }

    // Receive the file name stored by GetInitializer and store it in a
    // member variable for this specific instance.
    public override void Initialize(object initializer) {
      fileName = (string) initializer;
    }

    //  If the SoapMessageStage is such that the SoapRequest or
    //  SoapResponse is still in the SOAP format to be sent or received,
    //  save it out to a file.
    public override void ProcessMessage(SoapMessage message) {
      switch (message.Stage) {
        case SoapMessageStage.BeforeSerialize:
          // Capture content-type before serialization.
          // In the after serialization stage, content-type cannot be accessed.
          outputContentType = message.ContentType;
          break;
        case SoapMessageStage.AfterSerialize:
          WriteOutput(message);
          outputContentType = "";
          break;
        case SoapMessageStage.BeforeDeserialize:
          inputContentType = message.ContentType;
          WriteInput(message);
          inputContentType = "";
          break;
        case SoapMessageStage.AfterDeserialize:
          break;
        default: throw new Exception("Invalid stage");
      }
    }

    public void WriteOutput(SoapMessage message) {
      newStream.Position = 0;
      FileStream fs =
          new FileStream(fileName, FileMode.Append, FileAccess.Write);
      StreamWriter w = new StreamWriter(fs);

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
      CopyAndCleanXMLStream(newStream, cleanStream);

      w.WriteLine("-----" + soapString +  " at " + DateTime.Now + "-----");
      w.WriteLine(soapHeaderString);
      w.WriteLine("--------------------------------------------------------");
      w.Flush();
      Copy(cleanStream, fs);
      w.WriteLine("--------------------------------------------------------\n");
      w.Flush();
      w.Close();
      cleanStream.Close();

      newStream.Position = 0;
      Copy(newStream, oldStream);
    }

    public void WriteInput(SoapMessage message) {
      Copy(oldStream, newStream);
      FileStream fs =
          new FileStream(fileName, FileMode.Append, FileAccess.Write);
      StreamWriter w = new StreamWriter(fs);

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
      CopyAndCleanXMLStream(newStream, cleanStream);

      w.WriteLine("-----" + soapString +  " at " + DateTime.Now + "-----");
      w.WriteLine(soapHeaderString);
      w.WriteLine("--------------------------------------------------------");
      w.Flush();
      Copy(cleanStream, fs);
      w.WriteLine("--------------------------------------------------------\n");
      w.Flush();
      w.Close();
      cleanStream.Close();

      newStream.Position = 0;
    }
    
    // Load the XML from oldStream and save a human readable version
    // in cleanedUpStream.
    void CopyAndCleanXMLStream(Stream oldStream, Stream cleanedUpStream) {
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

    void Copy(Stream from, Stream to) {
      TextReader reader = new StreamReader(from);
      TextWriter writer = new StreamWriter(to);
      writer.WriteLine(reader.ReadToEnd());
      writer.Flush();
    }
  }

  // Create a SoapExtensionAttribute for the SOAP Extension that can be
  // applied to an XML Web service method.
  [AttributeUsage(AttributeTargets.Method)]
  public class TraceExtensionAttribute : SoapExtensionAttribute {
    private string fileName = "c:\\log.txt";
    private int priority;

    public override Type ExtensionType {
      get { return typeof(TraceExtension); }
    }

    public override int Priority {
      get { return priority; }
      set { priority = value; }
    }

    public string Filename {
      get { return fileName; }
      set { fileName = value; }
    }
  }
}

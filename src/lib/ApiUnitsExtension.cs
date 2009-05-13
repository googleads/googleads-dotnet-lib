/// This file is based on the code sample available at http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpguide/html/cpconAlteringSOAPMessageUsingSOAPExtensions.asp

using System;
using System.IO;
using System.Net;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web;
using System.Runtime.Remoting.Messaging;
using System.Runtime.CompilerServices;

namespace com.google.api.adwords.lib {
  /// <summary>
  /// Define a SOAP Extension that tracks API units from the
  /// SOAP request and response messages.
  /// </summary>
  public class ApiUnitsExtension : SoapExtension {
    /// <summary>
    /// This method is called when the extension is initialized as a
    /// result of an attribute being applied to a method. The object
    /// initialization can be done here.
    /// </summary>
    /// <param name="methodInfo">Information about the method to which
    /// the attribute was applied.</param>
    /// <param name="attribute">The attribute that was applied to the method.
    /// </param>
    /// <returns>An object that may be used for initialization.</returns>
    public override object GetInitializer(LogicalMethodInfo methodInfo,
        SoapExtensionAttribute attribute) {
      return null;
    }

    /// <summary>
    /// This method is called when the extension is initialized as a
    /// hook to an entire web service. This happens when you initialize
    /// the extension using app.config key. The object initialization can
    /// be done here.
    /// </summary>
    /// <param name="WebServiceType">Type of the webservice for which this
    /// extension is being initialized.</param>
    /// <returns>An object that may be used for initialization.</returns>
    public override object GetInitializer(Type WebServiceType) {
      return null;
    }

    /// <summary>
    /// Perform your class initialization steps here.
    /// </summary>
    /// <param name="initializer">The initalizer object passed from
    /// GetInitializer.</param>
    public override void Initialize(object initializer) {
    }

    /// <summary>
    /// Allows you to replace the SOAP stream with your own stream.
    /// </summary>
    /// <param name="stream">The current stream.</param>
    /// <returns>The new stream.</returns>
    public override Stream ChainStream(Stream stream) {
      return stream;
    }

    /// <summary>
    /// This method allows you to hook into and process a SOAP message
    /// at various stages of its lifetime.
    /// </summary>
    /// <param name="message">The message being processed.</param>
    public override void ProcessMessage(SoapMessage message) {
      switch (message.Stage) {
        case SoapMessageStage.BeforeSerialize:
          break;
        case SoapMessageStage.AfterSerialize:
          break;
        case SoapMessageStage.BeforeDeserialize:
          break;
        case SoapMessageStage.AfterDeserialize:
          int units = 0;
          foreach (SoapHeader header in message.Headers) {
            if (header.GetType() == Type.GetType(
                "com.google.api.adwords.v13.units")) {
              units = Int32.Parse(((
                  com.google.api.adwords.v13.units)header).Value[0]);
            }
          }
          AdWordsUser parent = null;
          if (HttpContext.Current != null) {
            parent = HttpContext.Current.Items["AdWordsParent"] as AdWordsUser;
          } else {
            parent = CallContext.GetData("AdWordsParent") as AdWordsUser;
          }
          if (parent != null) {
            parent.AddUnits(units);
          }
          break;
        default:
          throw new Exception("Invalid stage");
      }
    }
  }

  /// <summary>
  /// Create a SoapExtensionAttribute for the SOAP Extension that can be
  /// applied to an XML Web service method.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method)]
  public class QuotaExtensionAttribute : SoapExtensionAttribute {
    /// <summary>
    /// Priority to be associated with this extension.
    /// </summary>
    private int priority;

    /// <summary>
    /// Returns the type of the extension.
    /// </summary>
    public override Type ExtensionType {
      get {return typeof(ApiUnitsExtension);}
    }

    /// <summary>
    /// Priority to be associated with this extension.
    /// </summary>
    public override int Priority {
      get {return priority;}
      set {priority = value;}
    }
  }
}

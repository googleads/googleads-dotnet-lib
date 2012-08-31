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
using System.IO;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.Services.Configuration;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Google.Api.Ads.Common.Lib {
  /// <summary>
  /// Base class for all SOAP services supported by this library.
  /// </summary>
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  public abstract class AdsSoapClient : SoapHttpClientProtocol, AdsClient {
    /// <summary>
    /// The user that created this service instance.
    /// </summary>
    private AdsUser user;

    /// <summary>
    /// The signature for this service.
    /// </summary>
    private ServiceSignature signature;

    /// <summary>
    /// The WebRequest that was used by the last API call from this service.
    /// </summary>
    private WebRequest lastRequest;

    /// <summary>
    /// The WebRessponse for the last API call from this service.
    /// </summary>
    private WebResponse lastResponse;

    /// <summary>
    /// An internal delegate to the method that makes the SOAP API call.
    /// </summary>
    /// <param name="methodName">The name of the SOAP API method.</param>
    /// <param name="parameters">The list of parameters for the SOAP API
    /// method.</param>
    /// <returns>The results from calling the SOAP API method.</returns>
    private delegate object[] CallMethod(string methodName, object[] parameters);

    /// <summary>
    /// Gets or sets the AdsUser object that created this
    /// service.
    /// </summary>
    public AdsUser User {
      get {
        return user;
      }
      set {
        user = value;
      }
    }

    /// <summary>
    /// Gets or sets the signature for this service.
    /// </summary>
    public ServiceSignature Signature {
      get {
        return signature;
      }
      set {
        signature = value;
      }
    }

    /// <summary>
    /// Gets or sets the web request associated with this service's
    /// last API call.
    /// </summary>
    public WebRequest LastRequest {
      get {
        return lastRequest;
      }
      set {
        lastRequest = value;
      }
    }

    /// <summary>
    /// Gets or sets the web response associated with this service's
    /// last API call.
    /// </summary>
    public WebResponse LastResponse {
      get {
        return lastResponse;
      }
      set {
        lastResponse = value;
      }
    }

    /// <summary>
    /// Invokes a SOAP service method synchronously using SOAP.
    /// </summary>
    /// <param name="methodName">The name of the SOAP service method
    /// in the derived class that is invoking BeginInvoke. </param>
    /// <param name="parameters">An array of objects containing the
    /// parameters to pass to the SOAP service. The order of the
    /// values in the array correspond to the order of the parameters
    /// in the calling method of the derived class.</param>
    /// <returns>An array of objects containing the return value and any
    /// by reference or out parameters of the derived class method.</returns>
    protected new object[] Invoke(string methodName, object[] parameters) {
      return MakeApiCall(methodName, parameters);
    }

    /// <summary>
    /// Starts an asynchronous invocation of a SOAP service method
    /// using SOAP.
    /// </summary>
    /// <param name="methodName">The name of the SOAP service method
    /// in the derived class that is invoking BeginInvoke. </param>
    /// <param name="parameters">An array of objects containing the
    /// parameters to pass to the SOAP service. The order of the
    /// values in the array correspond to the order of the parameters
    /// in the calling method of the derived class.</param>
    /// <param name="callback">The delegate to call when the asynchronous
    /// invoke is complete.</param>
    /// <param name="asyncState">Extra information supplied by the caller.
    /// </param>
    /// <returns>An IAsyncResult which is passed to EndInvoke to obtain
    /// the return values from the remote method call.</returns>
    protected new IAsyncResult BeginInvoke(string methodName, object[] parameters,
        AsyncCallback callback, object asyncState) {
      CallMethod apiFunction = new CallMethod(MakeApiCall);
      return apiFunction.BeginInvoke(methodName, parameters, callback, apiFunction);
    }

    /// <summary>
    /// Ends an asynchronous invocation of a SOAP service method using
    /// SOAP.
    /// </summary>
    /// <param name="asyncResult">The IAsyncResult returned from BeginInvoke.
    /// </param>
    /// <returns>An array of objects containing the return value and any
    /// by-reference or out parameters of the derived class method.</returns>
    /// <exception cref="ArgumentNullException">Thrown if
    /// <paramref name="asyncResult"/> is null.</exception>
    protected new object[] EndInvoke(IAsyncResult asyncResult) {
      if (asyncResult == null) {
        throw new ArgumentNullException("asyncResult");
      }
      CallMethod apiFunction = (CallMethod) asyncResult.AsyncState;
      return apiFunction.EndInvoke(asyncResult);
    }

    /// <summary>
    /// Initializes the service before MakeApiCall.
    /// </summary>
    /// <param name="methodName">Name of the method.</param>
    /// <param name="parameters">The method parameters.</param>
    protected virtual void InitForCall(string methodName, object[] parameters) {
      if (!IsSoapListenerLoaded()) {
        throw new ApplicationException(CommonErrorMessages.SoapListenerExtensionNotLoaded);
      }
      ContextStore.AddKey("SoapService", this);
      ContextStore.AddKey("SoapMethod", methodName);
      this.user.InitListeners();
    }

    /// <summary>
    /// Cleans up the service after MakeApiCall.
    /// </summary>
    /// <param name="methodName">Name of the method.</param>
    /// <param name="parameters">The method parameters.</param>
    protected virtual void CleanupAfterCall(string methodName, object[] parameters) {
      this.user.CleanupListeners();
      ContextStore.RemoveKey("SoapService");
      ContextStore.RemoveKey("SoapMethod");
      this.lastRequest = null;
      this.lastResponse = null;
    }

    /// <summary>
    /// This method makes the actual SOAP API call. It is a thin wrapper
    /// over SOAPHttpClientProtocol:Invoke, and provide things like
    /// protection from race condition.
    /// </summary>
    /// <param name="methodName">The name of the SOAP API method.</param>
    /// <param name="parameters">The list of parameters for the SOAP API
    /// method.</param>
    /// <returns>The results from calling the SOAP API method.</returns>
    protected virtual object[] MakeApiCall(string methodName, object[] parameters) {
      int retryCount = this.user.Config.RetryCount;
      while (retryCount >= 0) {
        try {
          InitForCall(methodName, parameters);
          return base.Invoke(methodName, parameters);
        } catch (SoapException ex) {
          Exception customException = GetCustomException(ex);
          if (retryCount > 0 && ShouldRetry(customException)) {
            try {
              PrepareForRetry(customException);
              retryCount--;
              continue;
            } catch (Exception e) {
              // We threw an exception while trying to recover from another
              // exception. The second exception may contain additional details
              // (e.g. exact reason why OAuth token refresh failed.), so we
              // raise an ApplicationException with the message from the second
              // exception and the first exception as inner exception. Ideally,
              // we'd like to return both the exception objects, but this is
              // a reasonable tradeoff.
              string msg = string.Format("An error occured while retrying a failed API call : " +
                  "{0}. See inner exception for more details.", e.Message);
              throw new ApplicationException(msg, customException);
            }
          } else {
            throw customException;
          }
        } finally {
          CleanupAfterCall(methodName, parameters);
        }
      }
      throw new ArgumentOutOfRangeException("Retry count cannot be negative.");
    }

    /// <summary>
    /// Determines whether SOAP listener extension is loaded.
    /// </summary>
    /// <returns>True, if SoapListenerExtension is loaded as a SOAP extension.
    /// </returns>
    private static bool IsSoapListenerLoaded() {
      foreach (SoapExtensionTypeElement extensionElement in
          WebServicesSection.Current.SoapExtensionTypes) {
        if (extensionElement.Type == typeof(SoapListenerExtension)) {
          return true;
        }
      }
      return false;
    }

    /// <summary>
    /// Gets a custom exception that wraps the SOAP exception thrown
    /// by the server.
    /// </summary>
    /// <param name="ex">SOAPException that was thrown by the server.</param>
    /// <returns>A custom exception object that wraps the SOAP exception.
    /// </returns>
    /// <remarks>Any service that wishes to provide a custom exception
    /// should override this method.</remarks>
    protected virtual Exception GetCustomException(SoapException ex) {
      return ex;
    }

    /// <summary>
    /// Prepares for retrying the API call.
    /// </summary>
    /// <param name="ex">The exception thrown from the previous call.</param>
    protected virtual void PrepareForRetry(Exception ex) {
      return;
    }

    /// <summary>
    /// Whether the current API call should be retried or not.
    /// </summary>
    /// <param name="ex">The exception thrown from the previous call.</param>
    /// <returns>True, if the current API call should be retried.</returns>
    protected virtual bool ShouldRetry(Exception ex) {
      return false;
    }

    /// <summary>
    /// Creates a WebRequest instance for the specified url.
    /// </summary>
    /// <param name="uri">The Uri to use when creating the WebRequest.</param>
    /// <returns>The WebRequest instance.</returns>
    protected override WebRequest GetWebRequest(Uri uri) {
      // Store the base WebRequest in the member variable for future access.
      this.lastRequest = base.GetWebRequest(uri);
      (this.lastRequest as HttpWebRequest).ServicePoint.Expect100Continue = false;
      return this.lastRequest;
    }

    /// <summary>
    /// Returns a response from a synchronous request to an XML Web
    /// service method.
    /// </summary>
    /// <param name="request">The System.Net.WebRequest from which
    /// to get the response.</param>
    /// <returns>The web response.</returns>
    protected override WebResponse GetWebResponse(WebRequest request) {
      // Store the base WebResponse in the member variable for future access.
      this.lastResponse = base.GetWebResponse(request);
      return this.lastResponse;
    }

    /// <summary>
    /// Returns a response from an asynchronous request to a SOAP service
    /// method.
    /// </summary>
    /// <param name="request">The System.Net.WebRequest from which to get the
    /// response.</param>
    /// <param name="result">The System.IAsyncResult to pass to System.Net.
    /// HttpWebRequest.EndGetResponse(System.IAsyncResult) when the response
    /// has completed.</param>
    /// <returns>The web response.</returns>
    protected override WebResponse GetWebResponse(WebRequest request, IAsyncResult result) {
      // Store the base WebResponse in the member variable for future access.
      lastResponse = base.GetWebResponse(request, result);
      return lastResponse;
    }

    /// <summary>
    /// Gets the default XML namespace, based on the type of this object.
    /// </summary>
    /// <returns>The XML namespace to which this object is serialized, or an
    /// empty string if the method fails to retrieve the default namespace.
    /// </returns>
    /// <remarks>
    /// All the services making use of the XML Serialization framework
    /// (including ones generated by wsdl.exe and xsd.exe) will have
    /// a WebServiceBindingAttribute decoration, something like:
    ///
    /// [System.Web.Services.WebServiceBindingAttribute(
    ///     Name = "SomeServiceSoapBinding",
    ///     Namespace = "https://the_xml_namespace_for_serializing")]
    /// public partial class SomeService : SoapHttpClientProtocol {
    ///  ...
    /// }
    ///
    /// The only exception to this rule is when we choose to write our own
    /// serialization framework, by implementing IXmlSerializable on
    /// AdsSoapClient. If and when someone does that, and someone were to
    /// call this method, then he/she will get an empty string from this
    /// method.
    /// </remarks>
    protected string GetDefaultNamespace() {
      object[] attributes = this.GetType().GetCustomAttributes(false);
      foreach (object attribute in attributes) {
        if (attribute is WebServiceBindingAttribute) {
          return ((WebServiceBindingAttribute) attribute).Namespace;
        }
      }
      return String.Empty;
    }

    /// <summary>
    /// Extracts the fault xml node from soap exception.
    /// </summary>
    /// <param name="exception">The SOAP exception corresponding to the SOAP
    /// fault.</param>
    /// <param name="ns">The xml namespace for the fault node.</param>
    /// <param name="nodeName">The root node name for fault node.</param>
    /// <returns>The fault node.</returns>
    protected static XmlElement GetFaultNode(SoapException exception, string ns,
        string nodeName) {
      // Issue 1: Exception.Detail could be null. Can happen if SoapException
      // is a SoapHeaderException.
      if (exception.Detail == null) {
        return null;
      } else {
        XmlNamespaceManager xmlns =
            new XmlNamespaceManager(exception.Detail.OwnerDocument.NameTable);
        xmlns.AddNamespace("api", ns);
        return (XmlElement) exception.Detail.SelectSingleNode("api:" + nodeName, xmlns);
      }
    }
  }
}

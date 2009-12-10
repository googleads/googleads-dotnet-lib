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
using System.IO;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Web;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace com.google.api.adwords.lib {
  /// <summary>
  /// Base class for all SOAP services supported by this library.
  /// </summary>
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  public class SoapServiceBase  : SoapHttpClientProtocol {
    delegate object[] CallMethod(string methodName, object[] parameters);

    /// <summary>
    /// Gets or sets the AdWordsUser parent object that created this
    /// service.
    /// </summary>
    public AdWordsUser Parent {
      get {
        return parent;
      }
      set {
        parent = value;
      }
    }

    /// <summary>
    /// Gets or sets the web request associated with this service's
    /// last Api call.
    /// </summary>
    public WebRequest Request {
      get {
        return request;
      }
      set {
        request = value;
      }
    }

    /// <summary>
    /// Gets or sets the web response associated with this service's
    /// last Api call.
    /// </summary>
    public WebResponse Response {
      get {
        return response;
      }
      set {
        response = value;
      }
    }

    /// <summary>
    /// Invokes an XML Web service method synchronously using SOAP.
    /// </summary>
    /// <param name="methodName">The name of the XML Web service method
    /// in the derived class that is invoking BeginInvoke. </param>
    /// <param name="parameters">An array of objects containing the
    /// parameters to pass to the XML Web service. The order of the
    /// values in the array correspond to the order of the parameters
    /// in the calling method of the derived class.</param>
    /// <returns>An array of objects containing the return value and any
    /// by reference or out parameters of the derived class method.</returns>
    protected new object[] Invoke(string methodName, object[] parameters) {
      return Call(methodName, parameters);
    }

    /// <summary>
    /// Starts an asynchronous invocation of an XML Web service method
    /// using SOAP.
    /// </summary>
    /// <param name="methodName">The name of the XML Web service method
    /// in the derived class that is invoking BeginInvoke. </param>
    /// <param name="parameters">An array of objects containing the
    /// parameters to pass to the XML Web service. The order of the
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
      CallMethod apiFunction = new CallMethod(Call);
      return apiFunction.BeginInvoke(methodName, parameters, callback, apiFunction);
    }

    /// <summary>
    /// Ends an asynchronous invocation of an XML Web service method using
    /// SOAP.
    /// </summary>
    /// <param name="asyncResult">The IAsyncResult returned from BeginInvoke.
    /// </param>
    /// <returns>An array of objects containing the return value and any
    /// by-reference or out parameters of the derived class method.</returns>
    protected new object[] EndInvoke(IAsyncResult asyncResult) {
      CallMethod apiFunction = (CallMethod) asyncResult.AsyncState;
      return apiFunction.EndInvoke(asyncResult);
    }

    private object[] Call(string methodName, object[] parameters) {
      try {
        if (HttpContext.Current != null) {
          HttpContext.Current.Items.Add("AdWordsParent", this.Parent);
          HttpContext.Current.Items.Add("SoapService", this);
          HttpContext.Current.Items.Add("SoapMethod", methodName);
        } else {
          CallContext.SetData("AdWordsParent", this.Parent);
          CallContext.SetData("SoapService", this);
          CallContext.SetData("SoapMethod", methodName);
        }
        return base.Invoke(methodName, parameters);
      } catch (SoapException ex) {
        throw GetCustomException(ex);
      } finally {
        if (HttpContext.Current != null) {
          HttpContext.Current.Items.Remove("AdWordsParent");
          HttpContext.Current.Items.Remove("SoapService");
          HttpContext.Current.Items.Remove("SoapMethod");

        } else {
          CallContext.FreeNamedDataSlot("AdWordsParent");
          CallContext.FreeNamedDataSlot("SoapService");
          CallContext.FreeNamedDataSlot("SoapMethod");
        }
      }
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
    /// Creates a WebRequest instance for the specified url.
    /// </summary>
    /// <param name="uri">The Uri to use when creating the WebRequest.</param>
    /// <returns>The WebRequest instance.</returns>
    protected override WebRequest GetWebRequest(Uri uri) {
      request = base.GetWebRequest(uri);
      return request;
    }

    /// <summary>
    /// Returns a response from a synchronous request to an XML Web
    /// service method.
    /// </summary>
    /// <param name="request">The System.Net.WebRequest from which
    /// to get the response.</param>
    /// <returns>The web response.</returns>
    protected override WebResponse GetWebResponse(WebRequest request) {
      response = base.GetWebResponse(request);
      return response;
    }

    /// <summary>
    /// Returns a response from an asynchronous request to an XML Web service
    /// method.
    /// </summary>
    /// <param name="request">The System.Net.WebRequest from which to get the
    /// response.</param>
    /// <param name="result">The System.IAsyncResult to pass to System.Net.
    /// HttpWebRequest.EndGetResponse(System.IAsyncResult) when the response
    /// has completed.</param>
    /// <returns>The web response.</returns>
    protected override WebResponse GetWebResponse(WebRequest request, IAsyncResult result) {
      response = base.GetWebResponse(request, result);
      return response;
    }

    private AdWordsUser parent = null;

    private WebRequest request = null;

    private WebResponse response = null;
  }
}

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
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Services.Protocols;
using System.Xml;

namespace Google.Api.Ads.Common.Lib {
  /// <summary>
  /// Define a SOAP Extension that traces the SOAP request and SOAP response
  /// for the XML Web service method the SOAP extension is applied to.
  /// </summary>
  public class SoapListenerExtension : SoapExtension {
    /// <summary>
    /// The old stream, which we replaced in <see cref="ChainStream"/>.
    /// </summary>
    private Stream oldStream;

    /// <summary>
    /// The new stream, which we substituted for, in <see cref="ChainStream"/>.
    /// </summary>
    private MemoryStream newStream;

    /// <summary>
    /// Initializes a new instance of the <see cref="SoapListenerExtension"/>
    /// class.
    /// </summary>
    public SoapListenerExtension() {}

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
    /// <exception cref="ArgumentNullException">Thrown if
    /// <paramref name="methodInfo"/> is null.</exception>
    public override object GetInitializer(LogicalMethodInfo methodInfo,
        SoapExtensionAttribute attribute) {
      if (methodInfo == null) {
        throw new ArgumentNullException("methodInfo");
      }
      return methodInfo.DeclaringType;
    }

    /// <summary>
    /// The SOAP extension was configured to run using a configuration file
    /// instead of an attribute applied to a specific XML Web service
    /// method.
    /// </summary>
    /// <param name="serviceType">The type of the webservice being
    /// used.</param>
    /// <returns>An initializer object.</returns>
    public override object GetInitializer(Type serviceType) {
      return serviceType;
    }

    /// <summary>
    /// Process the messages passing in and out of the SOAP services.
    /// </summary>
    /// <param name="message">The current SOAP message.</param>
    /// <exception cref="ArgumentNullException">Thrown if
    /// <paramref name="message"/> is null.</exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public override void ProcessMessage(SoapMessage message) {
      if (message == null) {
        throw new ArgumentNullException("message");
      }
      switch (message.Stage) {
        case SoapMessageStage.BeforeSerialize:
          break;
        case SoapMessageStage.AfterSerialize:
          CallListeners(SoapMessageDirection.OUT);
          CopyContentsToOldStream();
          break;
        case SoapMessageStage.BeforeDeserialize:
          CopyContentsFromOldStream();
          CallListeners(SoapMessageDirection.IN);
          break;
        case SoapMessageStage.AfterDeserialize:
          break;
        default:
          throw new ArgumentException(CommonErrorMessages.InvalidStageForSoapMessage);
      }
    }

    /// <summary>
    /// Calls the listeners.
    /// </summary>
    /// <param name="direction">The direction of SOAP message.</param>
    private void CallListeners(SoapMessageDirection direction) {
      XmlDocument document = SerializationUtilities.LoadXml(Encoding.UTF8.GetString(
          newStream.ToArray()));

      AdsClient service = (AdsClient) ContextStore.GetValue("SoapService");
      if (service != null) {
        service.User.CallListeners(document, service, direction);
      }
      byte[] bytes = Encoding.UTF8.GetBytes(document.OuterXml);
      newStream.SetLength(0);
      newStream.Write(bytes, 0, bytes.Length);
      newStream.Seek(0, SeekOrigin.Begin);
    }

    /// <summary>
    /// Copy the contents from new stream to old stream.
    /// </summary>
    private void CopyContentsToOldStream() {
      newStream.Position = 0;
      MediaUtilities.CopyStream(newStream, oldStream);
    }

    /// <summary>
    /// Copy the contents from old stream to new stream.
    /// </summary>
    private void CopyContentsFromOldStream() {
      MediaUtilities.CopyStream(oldStream, newStream);
      newStream.Position = 0;
    }

    /// <summary>
    /// When overridden in a derived class, allows a SOAP extension to
    /// initialize itself using the data cached in the GetInitializer method.
    /// </summary>
    /// <param name="initializer">The object returned from GetInitializer
    /// </param>
    public override void Initialize(object initializer) {
    }
  }
}

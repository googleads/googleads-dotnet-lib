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
using System.IO;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Google.Api.Ads.Common.Lib {
  /// <summary>
  /// Listens to SOAP messages sent and received by this library.
  /// </summary>
  public abstract class SoapListener {
    /// <summary>
    /// The config class to be used with this class.
    /// </summary>
    protected AppConfigBase config;

    /// <summary>
    /// Direction of SOAP message.
    /// </summary>
    public enum Direction {
      /// <summary>
      /// Response from the server.
      /// </summary>
      IN,
      /// <summary>
      /// Request to the server.
      /// </summary>
      OUT
    }

    /// <summary>
    /// Protected constructor.
    /// </summary>
    /// <param name="config">The config class to be used with this class.
    /// </param>
    protected SoapListener(AppConfigBase config) {
      this.config = config;
    }

    /// <summary>
    /// Initializes the class for an API call.
    /// </summary>
    public virtual void InitForCall() { }

    /// <summary>
    /// Handles the SOAP message.
    /// </summary>
    /// <param name="soapMessage">The SOAP message.</param>
    /// <param name="service">The SOAP service.</param>
    /// <param name="direction">The direction of message.</param>
    public abstract void HandleMessage(XmlDocument soapMessage, AdsClient service,
        Direction direction);

    /// <summary>
    /// Cleans up any resources after an API call.
    /// </summary>
    public virtual void CleanupAfterCall() { }
  }
}

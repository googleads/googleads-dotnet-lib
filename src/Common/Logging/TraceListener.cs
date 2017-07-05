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

using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.Common.Util;

using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Xml;

namespace Google.Api.Ads.Common.Logging {

  /// <summary>
  /// Listens to SOAP messages sent and received by this library.
  /// </summary>
  public abstract class TraceListener : SoapListener {

    /// <summary>
    /// The config class to be used with this class.
    /// </summary>
    private AppConfig config;

    /// <summary>
    /// The date and time provider.
    /// </summary>
    private DateTimeProvider dateTimeProvider;

    /// <summary>
    /// Gets or sets the date and time provider.
    /// </summary>
    public DateTimeProvider DateTimeProvider {
      get {
        return dateTimeProvider;
      }
      set {
        dateTimeProvider = value;
      }
    }

    /// <summary>
    /// Gets the config class to be used with this class.
    /// </summary>
    public AppConfig Config {
      get {
        return config;
      }
    }

    /// <summary>
    /// Protected constructor.
    /// </summary>
    /// <param name="config">The config class.</param>
    protected TraceListener(AppConfig config) {
      this.config = config;
      this.dateTimeProvider = new DefaultDateTimeProvider();
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
      ContextStore.RemoveKey("FormattedSoapLog");
      ContextStore.RemoveKey("FormattedRequestLog");
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

      bool isFailure = service.LastResponse != null && service.LastResponse is HttpWebResponse &&
          (service.LastResponse as HttpWebResponse).StatusCode ==
                HttpStatusCode.InternalServerError;

      LogEntry logEntry = new LogEntry(config, dateTimeProvider);
      RequestInfo requestInfo = new RequestInfo(service.LastRequest, soapRequest);
      logEntry.LogRequestDetails(requestInfo, GetFieldsToMask(), new SoapTraceFormatter());
      logEntry.LogResponseDetails(new ResponseInfo(service.LastResponse, soapResponse),
          new HashSet<string>(), new DefaultBodyFormatter());
      logEntry.LogRequestSummary(requestInfo, GetSummaryRequestLogs(soapRequest));
      logEntry.LogResponseSummary(isFailure, GetSummaryResponseLogs(soapResponse));
      logEntry.Flush();

      ContextStore.AddKey("FormattedSoapLog", logEntry.DetailedLog);
      ContextStore.AddKey("FormattedRequestLog", logEntry.SummaryLog);
    }

    /// <summary>
    /// Gets a list of fields to be masked in xml logs.
    /// </summary>
    /// <returns>The list of fields to be masked.</returns>
    protected abstract ISet<string> GetFieldsToMask();

    /// <summary>
    /// Gets the summary request logs.
    /// </summary>
    /// <param name="soapRequest">The request xml for this SOAP call.</param>
    /// <returns>The summary request logs.</returns>
    protected virtual string GetSummaryRequestLogs(string soapRequest) {
      return "";
    }

    /// <summary>
    /// Gets the summary response logs.
    /// </summary>
    /// <param name="soapResponse">The response xml for this SOAP call.</param>
    /// <returns>The summary response logs.</returns>
    protected virtual string GetSummaryResponseLogs(string soapResponse) {
      return "";
    }
  }
}

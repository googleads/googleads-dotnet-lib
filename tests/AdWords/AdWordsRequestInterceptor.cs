// Copyright 2012, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.Common.Tests;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;

namespace Google.Api.Ads.AdWords.Tests {
  /// <summary>
  /// Intercepts SOAP messages from AdWords API client library for testing
  /// purposes.
  /// </summary>
  public class AdWordsRequestInterceptor : WebRequestInterceptor {
    /// <summary>
    /// The queue that holds the messages being mocked.
    /// </summary>
    static Queue<HttpMessage> messageQueue = new Queue<HttpMessage>();

    /// <summary>
    /// Content type for SOAP messages.
    /// </summary>
    public const string SOAP_RESPONSE_TYPE = "text/xml; charset=UTF-8";

    /// <summary>
    /// Singleton instance.
    /// </summary>
    private static WebRequestInterceptor instance;


    /// <summary>
    /// Gets the only instance.
    /// </summary>
    public static WebRequestInterceptor Instance {
      get {
        return instance;
      }
    }

    /// <summary>
    /// Initializes the <see cref="AdWordsRequestInterceptor"/> class.
    /// </summary>
    static AdWordsRequestInterceptor() {
      AdWordsAppConfig config = new AdWordsAppConfig();
      instance = new AdWordsRequestInterceptor();
      WebRequest.RegisterPrefix(config.AdWordsApiServer, instance);
    }

    /// <summary>
    /// Loads the messages for mock sequence.
    /// </summary>
    /// <param name="messages">The messages for mock purposes.</param>
    /// <param name="callback">The callbac to be called before sending response.
    /// </param>
    public void LoadMessages(HttpMessage[] messages, OnBeforeSendResponse callback) {
      messageQueue.Clear();
      foreach (HttpMessage message in messages) {
        messageQueue.Enqueue(message);
      }
      BeforeSendResponse += callback;
    }

    public override HttpMessage GetNextMessage() {
      if (messageQueue.Count != 0) {
        return messageQueue.Dequeue();
      } else {
        throw new WebException("Mock queue is empty.", WebExceptionStatus.SendFailure);
      }
    }
  }
}

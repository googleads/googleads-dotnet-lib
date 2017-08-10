// Copyright 2017, Google Inc. All Rights Reserved.
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

using System.ServiceModel;
using System.ServiceModel.Channels;
using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.Common.Tests.Mocks;
using NUnit.Framework;

namespace Google.Api.Ads.Common.Tests.Lib {

  /// <summary>
  /// UnitTests for <see cref="GzipHeaderInspector"/> class.
  /// </summary>
  [TestFixture]
  public class GzipHeaderInspectorTests {
    /// <summary>
    /// The service to test applying headers to.
    /// </summary>
    IClientChannel channel;

    /// <summary>
    /// The request Message for testing.
    /// </summary>
    private Message request;

    /// <summary>
    /// The test message version.
    /// </summary>
    readonly MessageVersion TestMessageVersion =
        MessageVersion.CreateVersion(EnvelopeVersion.Soap11);

    /// <summary>
    /// Initialize this test class instance.
    /// </summary>
    [SetUp]
    public void Init() {
      EndpointAddress endpoint = new EndpointAddress("http://www.google.com");
      BasicHttpBinding binding = new BasicHttpBinding();
      this.channel = new MockAdsService(binding, endpoint).InnerChannel;
      this.request = Message.CreateMessage(TestMessageVersion, "", "request body");
    }

    /// <summary>
    /// Test that the message state is valid and can be read
    /// after the inspector is applied.
    /// </summary>
    [Test]
    public void TestMessageIsValidState() {
      GzipHeaderInspector inspector = new GzipHeaderInspector();
      inspector.BeforeSendRequest(ref request, channel);
      inspector.AfterReceiveReply(ref request, channel);
      Assert.AreEqual(MessageState.Created, request.State);
    }

    /// <summary>
    /// Test that an appropriate Accept-Encoding HTTP header is added.
    /// </summary>
    [Test]
    public void TestAcceptEncodingHeaderApplied() {
      GzipHeaderInspector inspector = new GzipHeaderInspector();
      inspector.BeforeSendRequest(ref request, channel);

      object properties;
      request.Properties.TryGetValue(HttpRequestMessageProperty.Name, out properties);
      HttpRequestMessageProperty httpProps = (HttpRequestMessageProperty)properties;

      Assert.AreEqual(1, httpProps.Headers.Count);
      Assert.AreEqual("Accept-Encoding", httpProps.Headers.GetKey(0));
      Assert.AreEqual("gzip, deflate", httpProps.Headers.Get(0));
    }
  }
}
// copyright 2017, google inc. all rights reserved.
//
// licensed under the apache license, version 2.0 (the "license");
// you may not use this file except in compliance with the license.
// you may obtain a copy of the license at
//
//     http://www.apache.org/licenses/license-2.0
//
// unless required by applicable law or agreed to in writing, software
// distributed under the license is distributed on an "as is" basis,
// without warranties or conditions of any kind, either express or implied.
// see the license for the specific language governing permissions and
// limitations under the license.

using System.ServiceModel;
using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.Common.Tests.Mocks;
using NUnit.Framework;

namespace Google.Api.Ads.Common.Tests.Lib {

  /// <summary>
  /// UnitTests for <see cref="AdsSoapClient{TChannel}"/> class.
  /// </summary>
  [TestFixture]
  public class AdsSoapClientTests {
    /// <summary>
    /// The service to test.
    /// </summary>
    AdsSoapClient<IMockAdsService> service;

    /// <summary>
    /// The request Message for testing.
    /// </summary>
    private AdsServiceInspectorBehavior behavior;

    /// <summary>
    /// Initialize this test class instance.
    /// </summary>
    [SetUp]
    public void Init() {
      EndpointAddress endpoint = new EndpointAddress("http://www.google.com");
      BasicHttpBinding binding = new BasicHttpBinding();
      service = new MockAdsSoapClient<IMockAdsService>(binding, endpoint);
      behavior = new AdsServiceInspectorBehavior();
#if NET452
      service.Endpoint.Behaviors.Add(behavior);
#else
      service.Endpoint.EndpointBehaviors.Add(behavior);
#endif
    }

    /// <summary>
    /// Test that setting EnableDecompression adds a GzipHeaderInspector.
    /// </summary>
    [Test]
    public void TestEnableDecompression() {
      Assert.IsNull(behavior.GetInspector<GzipHeaderInspector>());
      service.EnableDecompression = true;
      Assert.NotNull(behavior.GetInspector<GzipHeaderInspector>());
    }

    /// <summary>
    /// Test that setting EnableDecompression removed a GzipHeaderInspector. 
    /// </summary>
    [Test]
    public void TestDisableDecompression() {
      GzipHeaderInspector inspector = new GzipHeaderInspector();
      behavior.Add(inspector);
      
      Assert.NotNull(behavior.GetInspector<GzipHeaderInspector>());
      service.EnableDecompression = false;
      Assert.Null(behavior.GetInspector<GzipHeaderInspector>());
    }

    [Test]
    public void TestGetEnableDecompression() {
      Assert.False(service.EnableDecompression);
      behavior.Add(new GzipHeaderInspector());
      Assert.True(service.EnableDecompression);
      behavior.Remove<GzipHeaderInspector>();
      Assert.False(service.EnableDecompression);
    }
  }
}

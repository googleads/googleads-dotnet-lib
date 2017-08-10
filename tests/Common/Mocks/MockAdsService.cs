
using System.ServiceModel;

namespace Google.Api.Ads.Common.Tests.Mocks {

  /// <summary>
  /// A mock ads service for testing
  /// </summary>
  public class MockAdsService : ClientBase<IMockAdsService> {
    public MockAdsService(BasicHttpBinding binding, EndpointAddress endpoint)
      : base(binding, endpoint) {
    }
  }
}
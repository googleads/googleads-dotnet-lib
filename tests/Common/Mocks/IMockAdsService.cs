using System.ServiceModel;

namespace Google.Api.Ads.Common.Tests.Mocks {

  /// <summary>
  /// A mock service interface for testing.
  /// </summary>
  [System.ServiceModel.ServiceContractAttribute(Namespace = "https://www.google.com/", ConfigurationName = "IMockAdsService")]
  public interface IMockAdsService {

    [System.ServiceModel.OperationContractAttribute(Action = "GetMockData")]
    void GetMockData();
  }
}
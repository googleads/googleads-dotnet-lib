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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.Common.Lib;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Tests {
  /// <summary>
  /// Utility to help mock a service.
  /// </summary>
  /// <example>
  /// To use this class,
  /// 1. Create a mock class for the service you want to mock and override
  ///    the method you want to mock.
  ///
  /// public class MockCampaignService : CampaignService {
  ///   public override CampaignPage get(Selector serviceSelector) {
  ///     // Do your stuff here
  ///     return null;
  ///   }
  /// }
  ///
  /// 2. Inject the mocked class into the user instance.
  ///
  /// MockUtilities.RegisterMockService(user,
  ///    AdWordsService.v201109.CampaignService, typeof(MockCampaignService));
  ///
  /// 3. Call your code as usual.
  ///
  /// CampaignService service = (CampaignService) user.GetService(
  ///     AdWordsService.v201109.CampaignService);
  ///
  /// 4. If you need to call the real campaign service as part of your
  /// tests, then you also need to decorate the overridden methods with the
  /// appropriate attributes.
  ///
  /// public class MockCampaignService : CampaignService {
  ///   [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
  ///   [System.Web.Services.Protocols.SoapHeaderAttribute("ResponseHeader",
  ///       Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
  ///   [System.Web.Services.Protocols.SoapDocumentMethodAttribute("",
  ///       RequestNamespace = "https://adwords.google.com/api/adwords/cm/v201109",
  ///       ResponseNamespace = "https://adwords.google.com/api/adwords/cm/v201109",
  ///       Use = System.Web.Services.Description.SoapBindingUse.Literal,
  ///       ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
  ///   [return: System.Xml.Serialization.XmlElementAttribute("rval")]
  ///   public override CampaignPage get(Selector serviceSelector) {
  ///     // Do your stuff here
  ///     return null;
  ///   }
  /// }
  ///
  /// You can copy the attributes from the stub code for the service you are
  /// mocking.
  /// </example>
  public class MockUtilities {
    /// <summary>
    /// Gets the signature.
    /// </summary>
    /// <param name="user">The user for which service should be mocked.</param>
    /// <param name="signature">The service signature to be mocked.</param>
    /// <param name="mockType">Type of the mock service.</param>
    public static void RegisterMockService(AdsUser user, ServiceSignature signature,
        Type mockType) {
      MockAdWordsServiceSignature mockSignature = MockAdWordsServiceSignature.FromSignature(
          signature, mockType);
      user.RegisterService(signature.Id, user.GetServiceFactory(mockSignature.Id));
    }
  }

  /// <summary>
  /// Service signature for mock purposes.
  /// </summary>
  public class MockAdWordsServiceSignature : AdWordsServiceSignature {
    /// <summary>
    /// Type of the mock service.
    /// </summary>
    Type serviceType;

    /// <summary>
    /// Private constructor that prevents a default instance of the
    /// <see cref="MockAdWordsServiceSignature"/> class from being created.
    /// </summary>
    /// <param name="version">Service version.</param>
    /// <param name="serviceSignature">Service name.</param>
    /// <param name="groupName">The group name</param>
    private MockAdWordsServiceSignature(string version, string serviceSignature,
        string groupName)
      : base(version, serviceSignature, groupName) {
    }

    /// <summary>
    /// Create a mock service signature from an existing service signature.
    /// </summary>
    /// <param name="signature">The service signature.</param>
    /// <param name="serviceType">Type of the mock service.</param>
    /// <returns></returns>
    public static MockAdWordsServiceSignature FromSignature(ServiceSignature signature,
        Type serviceType) {
      AdWordsServiceSignature awSignature = (AdWordsServiceSignature) signature;
      MockAdWordsServiceSignature retval = new MockAdWordsServiceSignature(
          awSignature.Version, awSignature.ServiceName, awSignature.GroupName);
      retval.serviceType = serviceType;
      return retval;
    }

    /// <summary>
    /// Gets the type of service.
    /// </summary>
    public override Type ServiceType {
      get {
        return serviceType;
      }
    }
  }
}

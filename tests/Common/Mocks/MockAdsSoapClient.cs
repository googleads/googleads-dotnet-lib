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

using Google.Api.Ads.Common.Lib;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Google.Api.Ads.Common.Tests.Mocks {
  public class MockAdsSoapClient<TChannel> : AdsSoapClient<TChannel> where TChannel : class {
    /// <summary>
    /// Initializes a new instance of the MockAdsSoapClient class.
    /// </summary>
    /// <param name="binding">The binding with which to make calls to the service.</param>
    /// <param name="remoteAddress">Remote address of the service.</param>
    public MockAdsSoapClient(Binding binding, EndpointAddress remoteAddress)
      : base(binding, remoteAddress) {
    } 
  }
}
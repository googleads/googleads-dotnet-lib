// Copyright 2016, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201609;

using System;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201609 {

  /// <summary>
  /// This code example accepts a pending invitation to link your AdWords
  /// account to a Google Merchant Center account.
  /// </summary>
  public class AcceptServiceLink : ExampleBase {

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AcceptServiceLink codeExample = new AcceptServiceLink();
      Console.WriteLine(codeExample.Description);
      try {
        long serviceLinkId = long.Parse("INSERT_SERVICE_LINK_ID_HERE");
        codeExample.Run(new AdWordsUser(), serviceLinkId);
      } catch (Exception e) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(e));
      }
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example accepts a pending invitation to link your AdWords account " +
            "to a Google Merchant Center account.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="serviceLinkId">The service link ID to accept.</param>
    public void Run(AdWordsUser user, long serviceLinkId) {
      // [START acceptServiceLink] MOE:strip_line
      // Get the CustomerService.
      CustomerService customerService = (CustomerService) user.GetService(
          AdWordsService.v201609.CustomerService);

      // Create the operation to set the status to ACTIVE.
      ServiceLinkOperation op = new ServiceLinkOperation();
      op.@operator = Operator.SET;
      ServiceLink serviceLink = new ServiceLink();
      serviceLink.serviceLinkId = serviceLinkId;
      serviceLink.serviceType = ServiceType.MERCHANT_CENTER;
      serviceLink.linkStatus = ServiceLinkLinkStatus.ACTIVE;
      op.operand = serviceLink;

      try {
        // Update the service link.
        ServiceLink[] mutatedServiceLinks =
            customerService.mutateServiceLinks(new ServiceLinkOperation[] { op });

        // Display the results.
        foreach (ServiceLink mutatedServiceLink in mutatedServiceLinks) {
          Console.WriteLine("Service link with service link ID {0}, type '{1}' updated to " +
              "status: {2}.", mutatedServiceLink.serviceLinkId, mutatedServiceLink.serviceType,
              mutatedServiceLink.linkStatus);
        }
        // [END acceptServiceLink] MOE:strip_line
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to update service link.", e);
      }
    }
  }
}

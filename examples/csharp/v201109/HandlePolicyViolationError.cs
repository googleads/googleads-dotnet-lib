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
using Google.Api.Ads.AdWords.Util;
using Google.Api.Ads.AdWords.v201109;

using System;
using System.IO;
using System.Net;
using System.Collections.Generic;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109 {
  /// <summary>
  /// This code example adds a text ad, and shows how to handle a policy
  /// violation.
  ///
  /// Tags: AdGroupAdService.mutate
  /// </summary>
  class HandlePolicyViolationError : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example adds a text ad, and shows how to handle a policy violation.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new HandlePolicyViolationError();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the AdGroupAdService.
      AdGroupAdService service =
          (AdGroupAdService) user.GetService(AdWordsService.v201109.AdGroupAdService);

      long adGroupId = long.Parse(_T("INSERT_ADGROUP_ID_HERE"));

      // Create your text ad.
      TextAd textAd = new TextAd();
      textAd.headline = "Luxury Cruise to Mars";
      textAd.description1 = "Visit the Red Planet in style.";
      textAd.description2 = "Low-gravity fun for everyone!!";
      textAd.displayUrl = "www.example.com";
      textAd.url = "http://www.example.com";

      AdGroupAd textadGroupAd = new AdGroupAd();
      textadGroupAd.adGroupId = adGroupId;
      textadGroupAd.ad = textAd;

      AdGroupAdOperation textAdOperation = new AdGroupAdOperation();
      textAdOperation.@operator = Operator.ADD;
      textAdOperation.operand = textadGroupAd;

      try {
        AdGroupAdReturnValue retVal = null;

        List<AdGroupAdOperation> allOperations = new List<AdGroupAdOperation>();
        List<AdGroupAdOperation> operationsToBeRemoved = new List<AdGroupAdOperation>();

        allOperations.Add(textAdOperation);

        try {
          // Call the service in validateOnly mode.
          service.RequestHeader.validateOnly = true;
          retVal = service.mutate(allOperations.ToArray());
        } catch (AdWordsApiException ex) {
          ApiException innerException = ex.ApiException as ApiException;
          if (innerException != null) {
            foreach (ApiError error in innerException.errors) {
              int index = ErrorUtilities.GetOperationIndex(error.fieldPath);
              if (index == -1) {
                // This API error is not associated with an operand.
                throw;
              }

              if (error is PolicyViolationError) {
                PolicyViolationError policyError = (PolicyViolationError) error;

                if (policyError.isExemptable) {
                  List<ExemptionRequest> exemptionRequests = new List<ExemptionRequest>();
                  if (allOperations[index].exemptionRequests != null) {
                    exemptionRequests.AddRange(allOperations[index].exemptionRequests);
                  }

                  ExemptionRequest exemptionRequest = new ExemptionRequest();
                  exemptionRequest.key = policyError.key;
                  exemptionRequests.Add(exemptionRequest);
                  allOperations[index].exemptionRequests = exemptionRequests.ToArray();
                } else {
                  operationsToBeRemoved.Add(allOperations[index]);
                }
              } else {
                operationsToBeRemoved.Add(allOperations[index]);
              }
            }
            // Remove all operations that aren't exemptable.
            foreach (AdGroupAdOperation operation in operationsToBeRemoved) {
              allOperations.Remove(operation);
            }
          } else {
            throw new Exception("Failed to retrieve ApiError. See inner exception for more " +
                "details.", ex);
          }
        }

        if (allOperations.Count > 0) {
          // Set valiateOnly to false.
          service.RequestHeader.validateOnly = false;
          retVal = service.mutate(allOperations.ToArray());

          if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
            foreach (AdGroupAd tempAdGroupAd in retVal.value) {
              Console.WriteLine("New ad with id = \"{0}\" and displayUrl = \"{1}\" was created.",
                  tempAdGroupAd.ad.id, tempAdGroupAd.ad.displayUrl);
            }
          } else {
            Console.WriteLine("No ads were created.");
          }
        } else {
          Console.WriteLine("No ads were created.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to create Ad(s). Exception says \"{0}\"", ex.Message);
      }
    }
  }
}

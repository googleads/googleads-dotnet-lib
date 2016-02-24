// Copyright 2015, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.Util.v201602;
using Google.Api.Ads.Dfp.v201602;

using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example creates new line item creative associations (LICAs) for
  /// an existing line item and a set of creative ids. For small business
  /// networks, the creative ids must represent new or copied creatives as
  /// creatives cannot be used for more than one line item. For premium
  /// solution networks, the creative ids can represent any creative. To copy
  /// creatives, run CopyImageCreatives.cs. To determine which LICAs exist, run
  /// GetAllLicasExample.cs.
  /// </summary>
  class CreateLicas : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates new line item creative associations (LICAs) for an " +
            "existing line item and a set of creative ids. For small business networks, the " +
            "creative ids must represent new or copied creatives as creatives cannot be used " +
            "for more than one line item. For premium solution networks, the creative ids can " +
            "represent any creative. To copy creatives, run CopyImageCreatives.cs. To determine " +
            "which LICAs exist, run GetAllLicasExample.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateLicas();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the LineItemCreativeAssociationService.
      LineItemCreativeAssociationService licaService =
          (LineItemCreativeAssociationService) user.GetService(
              DfpService.v201602.LineItemCreativeAssociationService);

      // Set the line item ID and creative IDs to associate.
      long lineItemId = long.Parse(_T("INSERT_LINE_ITEM_ID_HERE"));
      long[] creativeIds = new long[] {long.Parse(_T("INSERT_CREATIVE_ID_HERE"))};

      // Create an array to store local LICA objects.
      LineItemCreativeAssociation[] licas = new LineItemCreativeAssociation[creativeIds.Length];

      // For each line item, associate it with the given creative.
      int i = 0;
      foreach (long creativeId in creativeIds) {
        LineItemCreativeAssociation lica = new LineItemCreativeAssociation();
        lica.creativeId = creativeId;
        lica.lineItemId = lineItemId;
        licas[i++] = lica;
      }

      try {
        // Create the LICAs on the server.
        licas = licaService.createLineItemCreativeAssociations(licas);

        if (licas != null) {
          foreach (LineItemCreativeAssociation lica in licas) {
            Console.WriteLine("A LICA with line item ID \"{0}\", creative ID \"{1}\", and status " +
                "\"{2}\" was created.", lica.lineItemId, lica.creativeId, lica.status);
          }
        } else {
          Console.WriteLine("No LICAs created.");
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to associate creative with line item. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}

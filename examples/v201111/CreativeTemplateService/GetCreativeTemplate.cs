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

using Google.Api.Ads.Common.Util;
using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.Util.v201111;
using Google.Api.Ads.Dfp.v201111;

using System;

namespace Google.Api.Ads.Dfp.Examples.v201111 {
  /// <summary>
  /// This code example gets a creative by its ID. To determine which creative
  /// templates exist, run GetAllCreativeTemplates.cs.
  ///
  /// Tags: CreativeTemplateService.getCreativeTemplate
  /// </summary>
  class GetCreativeTemplate : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets a creative by its ID. To determine which creative " +
            "templates exist, run GetAllCreativeTemplates.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetCreativeTemplate();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the CreativeTemplateService.
      CreativeTemplateService creativeTemplateService =
          (CreativeTemplateService) user.GetService(DfpService.v201111.CreativeTemplateService);

      // Set the ID of the creative template to get.
      long creativeTemplateId = long.Parse(_T("INSERT_CREATIVE_TEMPLATE_ID_HERE"));

      try {
        // Get the creative template.
        CreativeTemplate creativeTemplate = creativeTemplateService.getCreativeTemplate(
            creativeTemplateId);

        if (creativeTemplate != null) {
          Console.WriteLine("Creative template with ID \"{0}\", name \"{1}\", and type \"{2}\" " +
              "was found.", creativeTemplate.id, creativeTemplate.name, creativeTemplate.type);
          if (creativeTemplate.variables != null) {
            foreach (CreativeTemplateVariable variable in creativeTemplate.variables) {
              Console.WriteLine("Variable with name \"{0}\" is {1}.", variable.uniqueName,
                  variable.isRequired ? "required." : "optional.");
            }
          }
        } else {
          Console.WriteLine("No creative template found for this ID.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to get creative templates. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}

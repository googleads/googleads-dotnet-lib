// Copyright 2010, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Dfa.Lib;
using Google.Api.Ads.Dfa.v112;

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.v112 {
  /// <summary>
  /// This code example creates a content category with the given name and
  /// description.
  /// </summary>
  class CreateContentCategory : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates a content category with the given name and description.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateContentCategory();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfaUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The Dfa user object running the code example.
    /// </param>
    public override void Run(DfaUser user) {
      // Create ContentCategoryRemoteService instance.
      ContentCategoryRemoteService service = (ContentCategoryRemoteService) user.GetService(
          DfaService.v112.ContentCategoryRemoteService);

      string contentCategoryName = _T("INSERT_CONTENT_CATEGORY_NAME_HERE");
      string contentCategoryDescription = _T("INSERT_CONTENT_CATEGORY_DESCRIPTION_HERE");

      // Create content category structure.
      ContentCategory contentCategory = new ContentCategory();
      contentCategory.id = 0;
      contentCategory.name = contentCategoryName;
      contentCategory.description = contentCategoryDescription;

      try {
        // Create content category.
        ContentCategorySaveResult contentCategorySaveResult =
            service.saveContentCategory(contentCategory);

        // Display content category Id.
        Console.WriteLine("Content category with Id \"{0}\" was created.",
            contentCategorySaveResult.id);
      } catch (Exception ex) {
        Console.WriteLine("Failed to add content category. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}

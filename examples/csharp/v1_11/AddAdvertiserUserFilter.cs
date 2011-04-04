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

using Google.Api.Ads.Dfa.Lib;
using Google.Api.Ads.Dfa.v1_11;

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.CSharp.v1_11 {
  /// <summary>
  /// This code example locates a user profile by id number and adds a filter
  /// to it, limiting the user profile's access to certain advertisers. To get
  /// user ids, run GetUsers.cs. To get advertiser ids, run GetAdvertisers.cs.
  ///
  /// A similar pattern can be applied to set filters limiting site, user role,
  /// and/or campaign access for any user. To get the Filter Type ids and Filter
  /// Criteria Type ids, run GetUserFilterTypes.cs.
  /// </summary>
  class AddAdvertiserUserFilter : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example locates a user profile by id number and adds a filter to it," +
            " limiting the user profile's access to certain advertisers. To get user ids, " +
            "run GetUsers.cs. To get advertiser ids, run GetAdvertisers.cs.\n\nA similar " +
            "pattern can be applied to set filters limiting site, user role, and/or campaign " +
            "access for any user. To get the Filter Type ids and Filter Criteria Type ids, " +
            "run GetUserFilterTypes.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new AddAdvertiserUserFilter();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfaUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The Dfa user object running the code example.
    /// </param>
    public override void Run(DfaUser user) {
      // Create UserRemoteService instance.
      UserRemoteService service = (UserRemoteService) user.GetService(
          DfaService.v1_11.UserRemoteService);

      long userId = long.Parse(_T("INSERT_USER_ID_HERE"));
      long advertiserId = long.Parse(_T("INSERT_ADVERTISER_ID_HERE"));

      // Create and configure a user filter.
      UserFilter filterToAdd = new UserFilter();

      // The following two fields have been filled in to make an advertiser
      // filter that allows a user to access only the assigned advertisers.
      // These values were determined using GetUserFilterTypes.cs.
      filterToAdd.userFilterTypeId = 3;
      filterToAdd.userFilterCriteriaId = 2;

      // Because this filter used the criteria type "Assigned" it is necessary
      // to specify what advertisers this user has access to. This next step
      // would be skipped for the criteria types "All" and "None".

      // Create an object filter to represent each object the user has access
      // to. Since this is an advertiser filter, an object filter represents an
      // advertiser. The total of object filter objects will need to match the
      // total of advertisers the user is assigned.
      ObjectFilter allowedObject = new ObjectFilter();

      // Insert the advertiser id of an advertiser assigned to this user.
      allowedObject.id = advertiserId;

      // Create any additional object filters that are needed, then add these
      // settings to the user filter
      filterToAdd.objectFilters = new ObjectFilter[] {allowedObject};

      try {
        // Retrieve the user who is to be modified.
        User userToModify = service.getUser(userId);

        // Add the filter to the user. The following method is specific to
        // advertiser filters. See the User class documentation for the names of
        // methods for other filters.
        userToModify.advertiserUserFilter = filterToAdd;

        // Save the changes made and display a success message.
        UserSaveResult userSaveResult = service.saveUser(userToModify);
        Console.WriteLine("User with id \"{0}\" was modified.", userSaveResult.id);
      } catch (Exception ex) {
        Console.WriteLine("Failed to add advertiser user filter. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}

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
using Google.Api.Ads.Dfa.Lib;
using Google.Api.Ads.Dfa.v1_20;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Google.Api.Ads.Dfa.Examples.Wcf {
  /// <summary>
  /// A class that makes calls to DFA API.
  /// </summary>
  public class DfaClient {
    /// <summary>
    /// The dfa user making calls.
    /// </summary>
    DfaUser user = new DfaUser();

    /// <summary>
    /// Gets the ad types.
    /// </summary>
    /// <returns></returns>
    internal AdType[] GetAdTypes() {
       // Create AdRemoteService instance.
      AdRemoteService service = (AdRemoteService) user.GetService(
          DfaService.v1_20.AdRemoteService);

      // Get ad types.
      return service.getAdTypes();
    }
  }
}

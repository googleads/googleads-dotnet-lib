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

using Google.Api.Ads.Common.Lib;

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Google.Api.Ads.AdWords.Lib {
  /// <summary>
  /// Lists all the services available through this library.
  /// </summary>
  public partial class AdWordsService {
    /// <summary>
    /// All the services availble in v13.
    /// </summary>
    public class v13 {
      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/developer/AccountService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AccountService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/developer/ReportService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ReportService;

      /// <summary>
      /// Factory type for v13 services.
      /// </summary>
      public static readonly Type factoryType = typeof(LegacyAdWordsServiceFactory);

      /// <summary>
      /// Static constructor to initialize the service constants.
      /// </summary>
      static v13() {
        AccountService = AdWordsService.MakeLegacyServiceSignature("v13", "AccountService");
        ReportService = AdWordsService.MakeLegacyServiceSignature("v13", "ReportService");
      }
    }
  }
}

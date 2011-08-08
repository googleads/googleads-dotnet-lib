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

using Google.Api.Ads.Common.OAuth.Lib;

using Microsoft.Practices.ServiceLocation;

using System;

namespace Google.Api.Ads.AdWords.Examples.OAuth {
  /// <summary>
  /// Handles all the startup tasks for the web application.
  /// </summary>
  public class Global : System.Web.HttpApplication {
    /// <summary>
    /// The service factory to be used with the framework.
    /// </summary>
    IServiceLocator injector = new AdsServiceLocator();

    /// <summary>
    /// Executes custom initialization code after all event handler modules
    /// have been added.
    /// </summary>
    public override void Init() {
      base.Init();
      ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider(delegate() {
        return injector;
      }));
    }
  }
}

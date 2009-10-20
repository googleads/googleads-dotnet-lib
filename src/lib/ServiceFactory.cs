// Copyright 2009, Google Inc. All Rights Reserved.
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

using System;
using System.Collections.Generic;
using System.Text;

namespace com.google.api.adwords.lib {
  /// <summary>
  /// Interface to a factory which can create a particular group of services.
  /// For every new service supported, you need an implementation of this
  /// interface. See <see cref="LegacyAdWordsApiServiceFactory"/> for a
  /// reference implementation.
  /// </summary>
  public abstract class ServiceFactory {
    /// <summary>
    /// Create a service object.
    /// </summary>
    /// <param name="signature">Signature of the service being created.</param>
    /// <param name="user">The user for which the service is being created.
    /// </param>
    /// <returns>An object of the desired service type.</returns>
    public abstract object CreateService(ServiceSignature signature, AdWordsUser user);

    /// <summary>
    /// Switch all the services created by this factory to sandbox mode.
    /// </summary>
    public abstract void UseSandbox();

    /// <summary>
    /// Gets a useragent string that can be used with the library.
    /// </summary>
    protected static string Useragent {
      get {
        return "AWAPI DotNetLib " + ApplicationConfiguration.version +
             " - " + ApplicationConfiguration.companyName;
      }
    }
  }
}

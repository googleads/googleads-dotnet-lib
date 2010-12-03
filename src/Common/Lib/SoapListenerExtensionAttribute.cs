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

using System;
using System.Web.Services.Protocols;

namespace Google.Api.Ads.Common.Lib {
  /// <summary>
  /// SoapExtensionAttribute for the SOAP Extension that can be
  /// applied to an XML Web service method to enable tracing.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method)]
  public sealed class SoapListenerExtensionAttribute : SoapExtensionAttribute {
    /// <summary>
    /// Stores the priority for this trace attribute.
    /// </summary>
    private int priority;

    /// <summary>
    /// Return the type of Extension.
    /// </summary>
    public override Type ExtensionType {
      get {
        return typeof(SoapListenerExtension);
      }
    }

    /// <summary>
    /// Return the priority of this attribute.
    /// </summary>
    public override int Priority {
      get {
        return priority;
      }
      set {
        priority = value;
      }
    }
  }
}

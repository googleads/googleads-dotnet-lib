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

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.Common.OAuth.Lib {
  /// <summary>
  /// This class stores the definition of a service component. This class is
  /// used by AdsServiceLocator class for storing type definitions.
  /// </summary>
  public class AdsServiceComponent {
    /// <summary>
    /// The component id.
    /// </summary>
    string id;

    /// <summary>
    /// Type of the service.
    /// </summary>
    Type serviceType;

    /// <summary>
    /// The underlying type of the service.
    /// </summary>
    Type instanceType;

    /// <summary>
    /// True, if this is the default instance of serviceType.
    /// </summary>
    bool isDefault;

    /// <summary>
    /// Default public constructor.
    /// </summary>
    /// <param name="id">The component id.</param>
    /// <param name="serviceType">Type of the service.</param>
    /// <param name="instanceType">The underlying type of the service.</param>
    public AdsServiceComponent(string id, Type serviceType, Type instanceType)
      : this(id, serviceType, instanceType, true) {
    }

    /// <summary>
    /// Overloaded public constructor.
    /// </summary>
    /// <param name="id">The component id.</param>
    /// <param name="serviceType">Type of the service.</param>
    /// <param name="instanceType">The underlying type of the service.</param>
    /// <param name="isDefault">True, if this is the default instance of
    /// serviceType.</param>
    public AdsServiceComponent(string id, Type serviceType, Type instanceType, bool isDefault) {
      this.id = id;
      this.serviceType = serviceType;
      this.instanceType = instanceType;
      this.isDefault = isDefault;
    }

    /// <summary>
    /// Gets the component id.
    /// </summary>
    public string Id {
      get {
        return id;
      }
    }

    /// <summary>
    /// Gets the type of the service.
    /// </summary>
    public Type ServiceType {
      get {
        return serviceType;
      }
    }

    /// <summary>
    /// Gets the underlying type of the service.
    /// </summary>
    public Type InstanceType {
      get {
        return instanceType;
      }
    }

    /// <summary>
    /// Gets a value indicating whether this instance is the default instance of
    /// ServiceType.
    /// </summary>
    public bool IsDefault {
      get {
        return isDefault;
      }
      set {
        isDefault = value;
      }
    }
  }
}

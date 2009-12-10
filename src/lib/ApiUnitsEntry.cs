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

namespace com.google.api.adwords.lib {
  /// <summary>
  /// Represents the Api units from one method call, as logged by
  /// ApiUnitsExtension.
  /// </summary>
  public partial class ApiUnitsEntry {
    /// <summary>
    /// Gets or sets the SOAP service to which this call belongs.
    /// </summary>
    public SoapServiceBase SoapService {
      get {
        return soapService;
      }
      set {
        soapService = value;
      }
    }

    /// <summary>
    /// Gets or sets the name of the SOAP method that was called.
    /// </summary>
    public string SoapMethod {
      get {
        return soapMethod;
      }
      set {
        soapMethod = value;
      }
    }

    /// <summary>
    /// Gets or sets the API units for this call.
    /// </summary>
    public int Units {
      get {
        return units;
      }
      set {
        units = value;
      }
    }

    /// <summary>
    /// The SOAP service to which this call belongs.
    /// </summary>
    SoapServiceBase soapService;

    /// <summary>
    /// Name of the SOAP method that was called.
    /// </summary>
    string soapMethod;

    /// <summary>
    /// The API units for this call.
    /// </summary>
    int units;
  }
}

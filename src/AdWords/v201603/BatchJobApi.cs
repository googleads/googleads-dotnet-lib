// Copyright 2016, Google Inc. All Rights Reserved.
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

using System.ComponentModel;

namespace Google.Api.Ads.AdWords.v201603 {

  /// <summary>
  /// Represents a request that wraps the operations sent to a batch job.
  /// </summary>
  [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://adwords.google.com/api/adwords/cm/v201603", TypeName = "mutate")]
  public class BatchJobMutateRequest {
    private Operation[] operationsField;

    /// <summary>
    /// Gets or sets the operations.
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute("operations")]
    public Operation[] operations {
      get { return operationsField; }
      set { operationsField = value; }
    }
  }

  /// <summary>
  /// Represents the SOAP envelope node that wraps a mutate response from a
  /// batch job.
  /// </summary>
  [System.Xml.Serialization.XmlTypeAttribute(TypeName = "root")]
  public class BatchJobMutateResponseEnvelope {
    BatchJobMutateResponse mutateResponseField;

    /// <summary>
    /// Gets or sets the mutate response.
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "https://adwords.google.com/api/adwords/cm/v201603")]
    public BatchJobMutateResponse mutateResponse {
      get { return mutateResponseField; }
      set { mutateResponseField = value; }
    }
  }

  /// <summary>
  /// Represents a mutate response from a batch job.
  /// </summary>
  public class BatchJobMutateResponse {
    private MutateResult[] rvalField;

    [System.Xml.Serialization.XmlElementAttribute("rval")]
    public MutateResult[] rval {
      get { return rvalField; }
      set { rvalField = value; }
    }
  }

  /// <summary>
  /// The list of API errors.
  /// </summary>
  public class ErrorList {
    private ApiError[] errorsField;

    /// <summary>
    /// Gets or sets the errors.
    /// </summary>
    [System.Xml.Serialization.XmlElementAttribute("errors")]
    public ApiError[] errors {
      get { return errorsField; }
      set { errorsField = value; }
    }
  }
}

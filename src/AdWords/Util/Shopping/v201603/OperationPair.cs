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

using Google.Api.Ads.AdWords.v201603;

using System;

namespace Google.Api.Ads.AdWords.Util.Shopping.v201603 {

  /// <summary>
  /// An OperationPair associates a <see cref="ProductPartitionNode"/>  with an
  /// <see cref="AdGroupCriterionOperation"/> that mutates the node.
  /// </summary>
  internal class OperationPair : Tuple<ProductPartitionNode, AdGroupCriterionOperation> {

    /// <summary>
    /// Gets the product partition node.
    /// </summary>
    public ProductPartitionNode Node {
      get {
        return this.Item1;
      }
    }

    /// <summary>
    /// Gets the operation associated with this node.
    /// </summary>
    public AdGroupCriterionOperation Operation {
      get {
        return this.Item2;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationPair"/> class.
    /// </summary>
    /// <param name="node">The product partition node.</param>
    /// <param name="operation">Gets the product partition node.</param>
    public OperationPair(ProductPartitionNode node, AdGroupCriterionOperation operation)
      : base(node, operation) {
    }
  }
}

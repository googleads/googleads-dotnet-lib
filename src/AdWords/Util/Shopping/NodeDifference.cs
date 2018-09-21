// Copyright 2015, Google Inc. All Rights Reserved.
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

namespace Google.Api.Ads.AdWords.Util.Shopping
{
    /// <summary>
    /// Enumeration of changes to a node.
    /// </summary>
    public enum NodeDifference
    {
        /// <summary>
        /// No differences.
        /// </summary>
        NONE,

        /// <summary>
        /// New node was added.
        /// </summary>
        NEW_NODE,

        /// <summary>
        /// Original node was removed.
        /// </summary>
        REMOVED_NODE,

        /// <summary>
        /// The product partition type differs between the two nodes.
        /// </summary>
        PARTITION_TYPE_CHANGE,

        /// <summary>
        /// The isExcludedUnit attribute differs between the two nodes - both nodes
        /// are unit nodes.
        /// </summary>
        EXCLUDED_UNIT_CHANGE,

        /// <summary>
        /// The bid differs between the two nodes - both nodes are non-excluded
        /// unit nodes.
        /// </summary>
        BID_CHANGE
    }
}

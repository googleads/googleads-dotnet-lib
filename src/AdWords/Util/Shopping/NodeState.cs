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

using System;

namespace Google.Api.Ads.AdWords.Util.Shopping
{
    /// <summary>
    /// The state of a node. This encapsulates the node type and behavior for
    /// setting/getting bids, as well as transitions from one node type to
    /// another.
    /// </summary>
    internal abstract class NodeState
    {
        /// <summary>
        /// Gets the NodeType for this state.
        /// </summary>
        internal abstract NodeType NodeType { get; }

        /// <summary>
        /// Gets or sets a value indicating whether bid in micros is specified or
        /// not.
        /// </summary>
        internal virtual bool BidInMicrosSpecified
        {
            get { return false; }
            set
            {
                throw new InvalidOperationException(
                    string.Format(ShoppingMessages.CannotSetBidOnNode, this.NodeType));
            }
        }

        /// <summary>
        /// Gets or sets the bid in micros.
        /// </summary>
        internal virtual long BidInMicros
        {
            get { return 0; }
            set
            {
                throw new InvalidOperationException(
                    string.Format(ShoppingMessages.CannotSetBidOnNode, this.NodeType));
            }
        }

        /// <summary>
        /// Transitions this NodeState to a NodeState for the specified
        /// <paramref name="nodeType"/>.
        /// </summary>
        /// <param name="nodeType">Type of the node.</param>
        /// <returns>a NodeState for the specified NodeType. Will be the current
        /// object if the NodeType matches this state's NodeType.</returns>
        internal NodeState TransitionTo(NodeType nodeType)
        {
            if (this.NodeType == nodeType)
            {
                return this;
            }

            switch (nodeType)
            {
                case NodeType.BIDDABLE_UNIT:
                    return new BiddableUnitState();

                case NodeType.EXCLUDED_UNIT:
                    return new ExcludedUnitState();

                case NodeType.SUBDIVISION:
                    return new SubdivisionState();

                default:
                    throw new ArgumentException(string.Format(ShoppingMessages.UnknownNodeType,
                        nodeType));
            }
        }
    }
}

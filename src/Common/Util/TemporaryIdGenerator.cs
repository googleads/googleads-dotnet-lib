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
using System.Collections.Generic;
using System.Linq;

namespace Google.Api.Ads.Common.Util
{
    /// <summary>
    /// Generates a sequence of temporary negative IDs.
    /// </summary>
    public class TemporaryIdGenerator
    {
        /// <summary>
        /// The number generation sequence.
        /// </summary>
        private IEnumerator<int> sequence;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemporaryIdGenerator"/> class.
        /// </summary>
        public TemporaryIdGenerator() : this(Int32.MinValue)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemporaryIdGenerator"/> class.
        /// </summary>
        /// <param name="startId">The ID to start generating the sequence from.</param>
        /// <exception cref="ArgumentException">If startId is a positive value.</exception>
        /// <remarks>The IDs are generated in the <i>increasing</i> order from the
        /// <paramref name="startId"/>.</remarks>
        public TemporaryIdGenerator(int startId)
        {
            if (startId >= 0)
            {
                throw new ArgumentException("ID cannot be positive.");
            }

            int count = -1 - startId;
            sequence = Enumerable.Range(startId, count).GetEnumerator();
        }

        /// <summary>
        /// Returns the next ID in the list.
        /// </summary>
        /// <returns></returns>
        public long Next
        {
            get
            {
                if (sequence.MoveNext())
                {
                    return sequence.Current;
                }
                else
                {
                    throw new ApplicationException("No more IDs to generate.");
                }
            }
        }

        /// <summary>
        /// Gets the sequence of numbers.
        /// </summary>
        public IEnumerator<int> Sequence
        {
            get { return sequence; }
        }
    }
}

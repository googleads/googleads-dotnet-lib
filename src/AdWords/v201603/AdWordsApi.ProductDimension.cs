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

// This file overrides the ToString() method for all ProductDimensions supported
// by ProductPartitionTree class.

namespace Google.Api.Ads.AdWords.v201603 {

  public partial class ProductType : ProductDimension {

    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String" /> that represents this instance.
    /// </returns>
    public override string ToString() {
      return string.Format("Type: {0}, Value: {1}", this.type, this.value);
    }
  }

  public partial class ProductOfferId : ProductDimension {

    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String" /> that represents this instance.
    /// </returns>
    public override string ToString() {
      return string.Format("Value: {0}", this.value);
    }
  }

  public partial class ProductCustomAttribute : ProductDimension {

    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String" /> that represents this instance.
    /// </returns>
    public override string ToString() {
      return string.Format("Type: {0}, Value: {1}", this.type, this.value);
    }
  }

  public partial class ProductChannelExclusivity : ProductDimension {

    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String" /> that represents this instance.
    /// </returns>
    public override string ToString() {
      return string.Format("Channel Exclusivity: {0}", this.channelExclusivity);
    }
  }

  public partial class ProductChannel : ProductDimension {

    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String" /> that represents this instance.
    /// </returns>
    public override string ToString() {
      return string.Format("Channel: {0}", this.channel);
    }
  }

  public partial class ProductCanonicalCondition : ProductDimension {

    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String" /> that represents this instance.
    /// </returns>
    public override string ToString() {
      return string.Format("Condition: {0}", this.condition);
    }
  }

  public partial class ProductBrand : ProductDimension {

    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String" /> that represents this instance.
    /// </returns>
    public override string ToString() {
      return string.Format("Value: {0}", this.value);
    }
  }

  public partial class ProductBiddingCategory : ProductDimension {

    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String" /> that represents this instance.
    /// </returns>
    public override string ToString() {
      return string.Format("Type: {0}, Value: {1}", this.type, this.value);
    }
  }
}

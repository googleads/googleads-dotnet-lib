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

namespace Google.Api.Ads.AdWords.v201506 {

  /// <summary>
  /// Specifies the page of results to return in the response. A page is
  /// specified by the result position to start at and the maximum number of
  /// results to return.
  /// </summary>
  public partial class Paging {

    /// <summary>
    /// The default page size.
    /// </summary>
    public const int DEFAULT_PAGE_SIZE = 500;

    /// <summary>
    /// Gets the default instance.
    /// </summary>
    public static Paging Default {
      get {
        return new Paging() {
          startIndex = 0,
          numberResults = DEFAULT_PAGE_SIZE
        };
      }
    }

    /// <summary>
    /// Increases the offset by a given value.
    /// </summary>
    /// <param name="pageSize">Size of the page.</param>
    public void IncreaseOffsetBy(int pageSize) {
      this.startIndex += pageSize;
    }

    /// <summary>
    /// Increases the offset by <see cref="numberResults"/>.
    /// </summary>
    public void IncreaseOffset() {
      this.startIndex += this.numberResults;
    }
  }
}

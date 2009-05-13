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

using System;

namespace com.google.api.adwords.samples {
  /// <summary>
  ///  This is a support class to accept various inputs from the user.
  /// </summary>
  class InputUtils {
    /// <summary>
    /// Accepts a long value from the console. Typically used to get an
    /// AdGroup ID, Ad ID or a Campaign ID.
    /// </summary>
    /// <param name="message">The message prompt to be displayed.</param>
    /// <returns></returns>
    public static long AcceptLong(string message) {
      bool isValidInput = false;
      long adId = 0;
      while (!isValidInput) {
        Console.Write(message);
        isValidInput = long.TryParse(Console.ReadLine().Trim(), out adId);
      }
      return adId;
    }
  }
}

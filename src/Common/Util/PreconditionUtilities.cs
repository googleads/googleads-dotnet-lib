using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.Dfp.Util {

  public class PreconditionUtilities {

    /// <summary>
    /// Utility method for null checking arguments.
    /// </summary>
    /// <param name="obj">The Object to check</param>
    /// <param name="argument">The name of the argument being checked</param>
    public static void CheckArgumentNotNull(object value, string argument) {
      if (value == null) {
        throw new ArgumentNullException(argument);
      }
    }
  }
}

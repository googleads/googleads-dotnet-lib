// Copyright 2011, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.Util.v201103;
using Google.Api.Ads.Dfp.v201103;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.Dfp.Examples.v201103 {
  /// <summary>
  /// This code example updates a line item to add custom criteria targeting. To
  /// determine which line items exist, run GetAllLineItems.cs. To determine
  /// which custom targeting keys and values exist, run
  /// GetAllCustomTargetingKeysAndValues.cs.
  ///
  /// Tags: LineItemService.getLineItem, LineItemService.updateLineItem
  /// </summary>
  class TargetCustomCriteria : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates a line item to add custom criteria targeting. To " +
            "determine which line items exist, run GetAllLineItems.cs. To determine which custom " +
            "targeting keys and values exist, run GetAllCustomTargetingKeysAndValues.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new TargetCustomCriteria();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the LineItemService.
      LineItemService lineItemService =
          (LineItemService) user.GetService(DfpService.v201103.LineItemService);

      long lineItemId = long.Parse(_T("INSERT_LINE_ITEM_ID_HERE"));
      long freeFormCustomTargetingKeyId1 =
          long.Parse(_T("INSERT_FREE_FORM_CUSTOM_TARGETING_KEY_ID_HERE"));
      long freeFormCustomTargetingKeyId2 =
          long.Parse(_T("INSERT_FREE_FORM_CUSTOM_TARGETING_KEY_ID_HERE"));

      // Create the free-form custom criteria for targeting "toyota".
      CustomTargetingValue toyotaFreeFormCustomTargetingValue = new CustomTargetingValue();
      toyotaFreeFormCustomTargetingValue.name = "toyota";
      toyotaFreeFormCustomTargetingValue.matchType = CustomTargetingValueMatchType.EXACT;

      FreeFormCustomCriteria toyotaFreeFormCustomCriteria = new FreeFormCustomCriteria();
      toyotaFreeFormCustomCriteria.keyId = freeFormCustomTargetingKeyId1;
      toyotaFreeFormCustomCriteria.values =
          new CustomTargetingValue[] {toyotaFreeFormCustomTargetingValue};
      toyotaFreeFormCustomCriteria.@operator = CustomCriteriaComparisonOperator.IS;

      // Create the free-form custom criteria for targeting "honda".
      CustomTargetingValue hondaFreeFormCustomTargetingValue = new CustomTargetingValue();
      hondaFreeFormCustomTargetingValue.name = "honda";
      hondaFreeFormCustomTargetingValue.matchType = CustomTargetingValueMatchType.EXACT;

      FreeFormCustomCriteria hondaFreeFormCustomCriteria = new FreeFormCustomCriteria();
      hondaFreeFormCustomCriteria.keyId = freeFormCustomTargetingKeyId1;
      hondaFreeFormCustomCriteria.values =
          new CustomTargetingValue[] {hondaFreeFormCustomTargetingValue};
      hondaFreeFormCustomCriteria.@operator = CustomCriteriaComparisonOperator.IS_NOT;

      // Create the free-form custom criteria for targeting "ford".
      CustomTargetingValue fordFreeFormCustomTargetingValue = new CustomTargetingValue();
      fordFreeFormCustomTargetingValue.name = "ford";
      fordFreeFormCustomTargetingValue.matchType = CustomTargetingValueMatchType.EXACT;

      FreeFormCustomCriteria fordFreeFormCustomCriteria = new FreeFormCustomCriteria();
      fordFreeFormCustomCriteria.keyId = freeFormCustomTargetingKeyId2;
      fordFreeFormCustomCriteria.values =
          new CustomTargetingValue[] {fordFreeFormCustomTargetingValue};
      fordFreeFormCustomCriteria.@operator = CustomCriteriaComparisonOperator.IS;

      // Create the custom criteria set that will resemble:
      //
      // (freeFormCustomTargetingKeyId1 == toyota OR
      //     (freeFormCustomTargetingKeyId1 != honda AND
      //         freeFormCustomTargetingKeyId2 == ford))
      CustomCriteriaSet topCustomCriteriaSet = new CustomCriteriaSet();
      topCustomCriteriaSet.logicalOperator = CustomCriteriaSetLogicalOperator.OR;

      CustomCriteriaSet subCustomCriteriaSet = new CustomCriteriaSet();
      subCustomCriteriaSet.logicalOperator = CustomCriteriaSetLogicalOperator.AND;
      subCustomCriteriaSet.children =
         new CustomCriteriaNode[] {hondaFreeFormCustomCriteria, fordFreeFormCustomCriteria};
      topCustomCriteriaSet.children =
         new CustomCriteriaNode[] {toyotaFreeFormCustomCriteria, subCustomCriteriaSet};

      try {
        // Set the custom criteria targeting on the line item.
        LineItem lineItem = lineItemService.getLineItem(lineItemId);
        lineItem.targeting.customTargeting = topCustomCriteriaSet;

        // Update the line items on the server.
        lineItem = lineItemService.updateLineItem(lineItem);

        // Display the updated line item.
        Console.WriteLine("Line item with ID {0} updated with custom criteria targeting \n{1}\n",
            lineItem.id, getCustomCriteriaSetString(lineItem.targeting.customTargeting, 0));
      } catch (Exception ex) {
        Console.WriteLine("Failed to add custom target criteria. Exception says \"{0}\"",
            ex.Message);
      }
    }

    /// <summary>
    /// Gets a string representation of the custom criteria node. If it has
    /// children, each child will be appended to the string recursively.
    /// </summary>
    /// <param name="root">The root custom criteria node.</param>
    /// <param name="level">The level of the custom criteria tree.</param>
    /// <returns>
    /// A string representation of the custom criteria node and its
    /// children
    /// </returns>
    private static string getCustomCriteriaSetString(CustomCriteriaNode root, int level) {
      StringBuilder sb = new StringBuilder();
      for (int i = 0; i < level; i++) {
        sb.Append("\t");
      }

      if (root is CustomCriteria) {
        if (root is PredefinedCustomCriteria) {
          PredefinedCustomCriteria predefinedCustomCriteria = (PredefinedCustomCriteria) root;

          List<string> valueIds = new List<string>();
          for (int i = 0; i < predefinedCustomCriteria.valueIds.Length; i++) {
            valueIds.Add(predefinedCustomCriteria.valueIds.ToString());
          }

          sb.AppendFormat("Predefined: operator: [{0}] key: [{1}] values: [{1}]\n",
              predefinedCustomCriteria.@operator, predefinedCustomCriteria.keyId,
              string.Join(",", valueIds.ToArray()));
          return sb.ToString();
        } else if (root is FreeFormCustomCriteria) {
          FreeFormCustomCriteria freeFormCustomCriteria = (FreeFormCustomCriteria) root;
          sb.AppendFormat("Free-form: operator: [{0}] key: [{1}] values: [{2}]\n",
              freeFormCustomCriteria.@operator, freeFormCustomCriteria.keyId,
              getCustomTargetValuesString(freeFormCustomCriteria.values));
          return sb.ToString();
        } else {
          throw new Exception("Unexpected node: " + root.CustomCriteriaNodeType);
        }
      } else if (root is CustomCriteriaSet) {
        CustomCriteriaSet customCriteriaSet = (CustomCriteriaSet) root;
        sb.AppendFormat("CustomCriteriaSet: operator: [{0}] children: \n",
            customCriteriaSet.logicalOperator);
        foreach (CustomCriteriaNode node in customCriteriaSet.children) {
          sb.Append(getCustomCriteriaSetString(node, level + 1));
        }
        return sb.Append("\n").ToString();
      } else {
        throw new Exception("Unexpected node: " + root.CustomCriteriaNodeType);
      }
    }

    /// <summary>
    /// Gets a string representation of an array of custom targeting values.
    /// </summary>
    /// <param name="values">The array of custom targeting values.</param>
    /// <returns>A string representation in the form of (match type : name),...
    /// </returns>
    private static String getCustomTargetValuesString(CustomTargetingValue[] values) {
      StringBuilder sb = new StringBuilder();
      foreach (CustomTargetingValue customTargetingValue in values) {
        sb.AppendFormat("({0} : {1})", customTargetingValue.matchType,
            customTargetingValue.name);
      }
      return sb.ToString();
    }
  }
}

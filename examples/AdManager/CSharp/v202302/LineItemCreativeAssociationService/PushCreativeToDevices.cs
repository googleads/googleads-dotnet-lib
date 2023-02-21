// Copyright 2021 Google LLC
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

using Google.Api.Ads.AdManager.Lib;
using Google.Api.Ads.AdManager.Util.v202302;
using Google.Api.Ads.AdManager.v202302;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v202302
{
    /// <summary>
    /// This code example pushes a LICA to a linked device for preview. To determine
    /// which linked devices exist, use the PublisherQueryLanguageService linked_device table.
    /// </summary>
    public class PushCreativeToDevices : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example pushes a LICA to a linked device for preview. To " +
                    "determine which linked devices exist, use the PublisherQueryLanguageService " +
                    "linked_device table.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            PushCreativeToDevices codeExample = new PushCreativeToDevices();
            Console.WriteLine(codeExample.Description);
            // Set the line item to push to the linked device.
            long lineItemId = long.Parse(_T("INSERT_LINE_ITEM_ID_HERE"));
            // Set the creative to push to the linked device.
            long creativeId = long.Parse(_T("INSERT_CREATIVE_ID_HERE"));
            // Set the linked device to push the LICA to.
            long linkedDeviceId = long.Parse(_T("INSERT_LINKED_DEVICE_ID_HERE"));
            codeExample.Run(new AdManagerUser(), lineItemId, creativeId, linkedDeviceId);
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user, long lineItemId, long creativeId, long linkedDeviceId)
        {
            using (LineItemCreativeAssociationService licaService =
                user.GetService<LineItemCreativeAssociationService>())
            {

                // Create a statement to page through active LICAs.
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Where("id = :linkedDeviceId")
                    .AddValue("linkedDeviceId", linkedDeviceId);

                try
                {
                    // Perform action.
                    UpdateResult result = licaService.pushCreativeToDevices(
                        statementBuilder.ToStatement(),
                        new CreativePushOptions() { lineItemId = lineItemId,
                                                    creativeId = creativeId });
                    // Display results.
                    Console.WriteLine("Creative pushed to {0} device(s)", result.numChanges);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to push creative. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}

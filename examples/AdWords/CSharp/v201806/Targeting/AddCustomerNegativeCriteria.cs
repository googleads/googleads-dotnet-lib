// Copyright 2018 Google LLC
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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201806;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201806
{
    /// <summary>
    /// This code example adds various types of negative criteria to a customer. These criteria
    /// will be applied to all campaigns for the customer.
    /// </summary>
    public class AddCustomerNegativeCriteria : ExampleBase
    {
        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            AddCustomerNegativeCriteria codeExample = new AddCustomerNegativeCriteria();
            Console.WriteLine(codeExample.Description);
            try
            {
                codeExample.Run(new AdWordsUser());
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception occurred while running this code example. {0}",
                    ExampleUtilities.FormatException(e));
            }
        }

        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example adds various types of negative criteria to a customer. " +
                    "These criteria will be applied to all campaigns for the customer.";
            }
        }

        /// <summary>
        /// Runs the code example.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        public void Run(AdWordsUser user)
        {
            using (CustomerNegativeCriterionService customerNegativeCriterionService =
                (CustomerNegativeCriterionService) user.GetService(AdWordsService.v201806
                    .CustomerNegativeCriterionService))
            {
                List<Criterion> criteria = new List<Criterion>();

                // Exclude tragedy & conflict content.
                ContentLabel tragedyContentLabel = new ContentLabel
                {
                    contentLabelType = ContentLabelType.TRAGEDY
                };
                criteria.Add(tragedyContentLabel);

                // Exclude a specific placement.
                Placement placement = new Placement
                {
                    url = "http://www.example.com"
                };
                criteria.Add(placement);

                // Additional criteria types are available for this service. See the types listed
                // under Criterion here:
                // https://developers.google.com/adwords/api/docs/reference/latest/CustomerNegativeCriterionService.Criterion

                // Create operations to add each of the criteria above.
                List<CustomerNegativeCriterionOperation> operations =
                    new List<CustomerNegativeCriterionOperation>();
                foreach (Criterion criterion in criteria)
                {
                    CustomerNegativeCriterion negativeCriterion = new CustomerNegativeCriterion
                    {
                        criterion = criterion
                    };
                    CustomerNegativeCriterionOperation operation =
                        new CustomerNegativeCriterionOperation
                        {
                            @operator = Operator.ADD,
                            operand = negativeCriterion
                        };
                    operations.Add(operation);
                }

                try
                {
                    // Send the request to add the criteria.
                    CustomerNegativeCriterionReturnValue result =
                        customerNegativeCriterionService.mutate(operations.ToArray());

                    // Display the results.
                    foreach (CustomerNegativeCriterion negativeCriterion in result.value)
                    {
                        Console.WriteLine(
                            "Customer negative criterion with criterion ID {0} and type '{1}' " +
                            "was added.", negativeCriterion.criterion.id,
                            negativeCriterion.criterion.type);
                    }
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException(
                        "Failed to set customer negative criteria.", e);
                }
            }

        }
    }
}

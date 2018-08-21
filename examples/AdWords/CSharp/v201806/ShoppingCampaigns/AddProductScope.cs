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

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201806
{
    /// <summary>
    /// This code example restricts the products that will be included in the
    /// campaign by setting a ProductScope.
    /// </summary>
    public class AddProductScope : ExampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example restricts the products that will be included in the " +
                    "campaign by setting a ProductScope.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            AddProductScope codeExample = new AddProductScope();
            Console.WriteLine(codeExample.Description);
            try
            {
                long campaignId = long.Parse("INSERT_CAMPAIGN_ID_HERE");
                codeExample.Run(new AdWordsUser(), campaignId);
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception occurred while running this code example. {0}",
                    ExampleUtilities.FormatException(e));
            }
        }

        /// <summary>
        /// Runs the code example.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="campaignId">The campaign id to add product scope.</param>
        public void Run(AdWordsUser user, long campaignId)
        {
            using (CampaignCriterionService campaignCriterionService =
                (CampaignCriterionService) user.GetService(AdWordsService.v201806
                    .CampaignCriterionService))
            {
                ProductScope productScope = new ProductScope();
                // This set of dimensions is for demonstration purposes only. It would be
                // extremely unlikely that you want to include so many dimensions in your
                // product scope.
                ProductBrand nexusBrand = new ProductBrand
                {
                    value = "Nexus"
                };

                ProductCanonicalCondition newProducts = new ProductCanonicalCondition
                {
                    condition = ProductCanonicalConditionCondition.NEW
                };

                ProductCustomAttribute customAttribute = new ProductCustomAttribute
                {
                    type = ProductDimensionType.CUSTOM_ATTRIBUTE_0,
                    value = "my attribute value"
                };

                ProductOfferId bookOffer = new ProductOfferId
                {
                    value = "book1"
                };

                ProductType mediaProducts = new ProductType
                {
                    type = ProductDimensionType.PRODUCT_TYPE_L1,
                    value = "Media"
                };

                ProductType bookProducts = new ProductType
                {
                    type = ProductDimensionType.PRODUCT_TYPE_L2,
                    value = "Books"
                };

                // The value for the bidding category is a fixed ID for the
                // 'Luggage & Bags' category. You can retrieve IDs for categories from
                // the ConstantDataService. See the 'GetProductCategoryTaxonomy' example
                // for more details.
                ProductBiddingCategory luggageBiddingCategory = new ProductBiddingCategory
                {
                    type = ProductDimensionType.BIDDING_CATEGORY_L1,
                    value = -5914235892932915235
                };

                productScope.dimensions = new ProductDimension[]
                {
                    nexusBrand,
                    newProducts,
                    bookOffer,
                    mediaProducts,
                    luggageBiddingCategory
                };

                CampaignCriterion campaignCriterion = new CampaignCriterion
                {
                    campaignId = campaignId,
                    criterion = productScope
                };

                // Create operation.
                CampaignCriterionOperation operation = new CampaignCriterionOperation
                {
                    operand = campaignCriterion,
                    @operator = Operator.ADD
                };

                try
                {
                    // Make the mutate request.
                    CampaignCriterionReturnValue result = campaignCriterionService.mutate(
                        new CampaignCriterionOperation[]
                        {
                            operation
                        });

                    Console.WriteLine("Created a ProductScope criterion with ID '{0}'",
                        result.value[0].criterion.id);
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to set shopping product scope.",
                        e);
                }
            }
        }
    }
}

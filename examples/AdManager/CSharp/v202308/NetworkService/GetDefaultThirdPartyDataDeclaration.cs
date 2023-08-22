// Copyright 2020 Google LLC
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
using Google.Api.Ads.AdManager.Util.v202308;
using Google.Api.Ads.AdManager.v202308;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v202308
{
    /// <summary>
    /// This code example gets the current network's default third party data declaration.
    /// </summary>
    public class  GetDefaultThirdPartyDataDeclaration : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example gets the current network's defaut third party data " +
                    "declaration";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            GetDefaultThirdPartyDataDeclaration codeExample =
                new GetDefaultThirdPartyDataDeclaration();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (NetworkService networkService = user.GetService<NetworkService>())
            using (PublisherQueryLanguageService pqlService =
                user.GetService<PublisherQueryLanguageService>())
            {
                try
                {
                    // Get the default third party data declarations.
                    ThirdPartyDataDeclaration declaration =
                          networkService.getDefaultThirdPartyDataDeclaration();

                    if (declaration == null)
                    {
                      Console.WriteLine(
                          "No default ad technology partners have been set on this network.");
                    }
                    else if (declaration.declarationType == DeclarationType.NONE
                        || declaration.thirdPartyCompanyIds.Length == 0)
                    {
                        Console.WriteLine("This network has specified that there are no " +
                            "ad technology providers  associated with its reservation creatives " +
                            "by default.");
                    }
                    else
                    {
                        Console.WriteLine("This network has specified {0} ad technology " +
                            "provider(s) associated with its reservation creatives by default:",
                            declaration.thirdPartyCompanyIds.Length);
                        ResultSet resultSet = pqlService.select(new StatementBuilder()
                            .Select("name, id")
                            .From("rich_media_ad_company")
                            .Where("id in (:ids)")
                            .AddValue("ids", string.Join(",", declaration.thirdPartyCompanyIds))
                            .ToStatement());
                        Console.WriteLine(PqlUtilities.ResultSetToString(resultSet));

                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to get the default third party data declaration. " +
                        "Exception says \"{0}\"", e.Message);
                }
            }
        }
    }
}

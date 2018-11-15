// Copyright 2017, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdManager.v201811;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201811
{
    /// <summary>
    /// This code example creates new CDN configurations.
    /// </summary>
    public class CreateCdnConfigurations : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get { return "This code example creates new CDN configurations."; }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            CreateCdnConfigurations codeExample = new CreateCdnConfigurations();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (CdnConfigurationService cdnConfigurationService =
                user.GetService<CdnConfigurationService>())
            {
                // Make CND configuration objects.
                // Only LIVE_STREAM_SOURCE_CONTENT is currently supported by the API.
                // Basic example with no security policies.
                SecurityPolicySettings noSecurityPolicy = new SecurityPolicySettings()
                {
                    securityPolicyType = SecurityPolicyType.NONE
                };
                CdnConfiguration cdnConfigurationWithoutSecurityPolicy = new CdnConfiguration()
                {
                    name = "ApiConfig1",
                    cdnConfigurationType = CdnConfigurationType.LIVE_STREAM_SOURCE_CONTENT,
                    sourceContentConfiguration = new SourceContentConfiguration()
                    {
                        ingestSettings = new MediaLocationSettings()
                        {
                            urlPrefix = "example.google.com",
                            securityPolicy = noSecurityPolicy
                        },
                        defaultDeliverySettings = new MediaLocationSettings()
                        {
                            urlPrefix = "example.google.com",
                            securityPolicy = noSecurityPolicy
                        }
                    }
                };

                // Complex example with security policies.
                CdnConfiguration cdnConfigurationWithSecurityPolicy = new CdnConfiguration()
                {
                    name = "ApiConfig2",
                    cdnConfigurationType = CdnConfigurationType.LIVE_STREAM_SOURCE_CONTENT,
                    sourceContentConfiguration = new SourceContentConfiguration()
                    {
                        ingestSettings = new MediaLocationSettings()
                        {
                            urlPrefix = "example.google.com",
                            securityPolicy = new SecurityPolicySettings()
                            {
                                securityPolicyType = SecurityPolicyType.AKAMAI,
                                disableServerSideUrlSigning = false,
                                tokenAuthenticationKey = "abc123"
                            }
                        },
                        defaultDeliverySettings = new MediaLocationSettings()
                        {
                            urlPrefix = "example.google.com",
                            securityPolicy = new SecurityPolicySettings()
                            {
                                securityPolicyType = SecurityPolicyType.AKAMAI,
                                disableServerSideUrlSigning = false,
                                originForwardingType = OriginForwardingType.CONVENTIONAL,
                                originPathPrefix = "/path/to/my/origin"
                            }
                        }
                    }
                };

                try
                {
                    // Create the CDN configurations on the server.
                    CdnConfiguration[] cdnConfigurations =
                        cdnConfigurationService.createCdnConfigurations(new CdnConfiguration[]
                        {
                            cdnConfigurationWithoutSecurityPolicy,
                            cdnConfigurationWithSecurityPolicy
                        });

                    foreach (CdnConfiguration createdCdnConfiguration in cdnConfigurations)
                    {
                        Console.WriteLine(
                            "A CDN configuration with ID \"{0}\" and name \"{1}\" was created.",
                            createdCdnConfiguration.id, createdCdnConfiguration.name);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to create company. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}

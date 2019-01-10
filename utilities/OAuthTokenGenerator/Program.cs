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

using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Google.Api.Ads.Common.Utilities.OAuthTokenGenerator
{
    /// <summary>
    /// Entry point for the application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The AdWords API scope.
        /// </summary>
        private const string ADWORDS_API_SCOPE = "https://www.googleapis.com/auth/adwords";

        /// <summary>
        /// The Ad Manager API scope.
        /// </summary>
        private const string AD_MANAGER_API_SCOPE = "https://www.googleapis.com/auth/dfp";

        /// <summary>
        /// The Application configuration patch to be displayed to the user.
        /// </summary>
        private const string APP_CONFIG_PATCH = @"
<add key='AuthorizationMethod' value='OAuth2' />
<add key='OAuth2ClientId' value='{0}' />
<add key='OAuth2ClientSecret' value='{1}' />
<add key='OAuth2RefreshToken' value='{2}' />
";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main(string[] args)
        {
            Console.WriteLine("This application generates an OAuth2 refresh token for use with " +
                "the Google Ads API .NET client library. To use this application\n" +
                "1) Follow the instructions on https://developers.google.com/adwords/api/docs/" +
                "guides/authentication#create_a_client_id_and_client_secret to generate a new " +
                "client ID and secret.\n2) Enter the client ID and client Secret when prompted.\n" +
                "3) Once the output is generated, copy its contents into your " +
                "App.config file.\n\n");

            Console.WriteLine("Important note: The client ID you use with the example should be " +
                "of type 'Other'. If you are using a Web application client, you should add " +
                "'http://127.0.0.1/authorize' and 'http://localhost/authorize' to the list of " +
                "Authorized redirect URIs in your Google Developer Console to avoid getting a " +
                "redirect_uri_mismatch error.\n\n");

            // Accept the client ID from user.
            Console.Write("Enter the client ID: ");
            string clientId = Console.ReadLine();

            // Accept the client ID from user.
            Console.Write("Enter the client secret: ");
            string clientSecret = Console.ReadLine();

            // Should API scopes include AdWords API?
            string useAdWordsApiScope = AcceptInputWithLimitedOptions(
                "Authenticate for AdWords API?", new string[]
                {
                    "yes",
                    "no"
                });

            // Should API scopes include AdWords API?
            string useAdManagerApiScope =
                AcceptInputWithLimitedOptions("Authenticate for Ad Manager API?",
                    new string[]
                    {
                        "yes",
                        "no"
                    });

            // Accept any additional scopes.
            Console.Write("Enter additional OAuth2 scopes to authenticate for (space separated): ");
            string additionalScopes = Console.ReadLine();

            List<string> scopes = new List<string>();

            if (useAdWordsApiScope.ToLower().Trim() == "yes")
            {
                scopes.Add(ADWORDS_API_SCOPE);
            }

            if (useAdManagerApiScope.ToLower().Trim() == "yes")
            {
                scopes.Add(AD_MANAGER_API_SCOPE);
            }

            scopes.AddRange(additionalScopes.Split(' ').Select(s => s.Trim())
                .Where(s => !string.IsNullOrEmpty(s)));

            // Load the JSON secrets.
            ClientSecrets secrets = new ClientSecrets()
            {
                ClientId = clientId,
                ClientSecret = clientSecret
            };

            try
            {
                // Authorize the user using installed application flow.
                Task<UserCredential> task = GoogleWebAuthorizationBroker.AuthorizeAsync(secrets,
                    scopes, String.Empty, CancellationToken.None, new NullDataStore());
                task.Wait();
                UserCredential credential = task.Result;

                Console.WriteLine("\nCopy the following content into your App.config file.\n\n" +
                    $"<add key = 'OAuth2Mode' value = 'APPLICATION' />\n" +
                    $"<add key = 'OAuth2ClientId' value = '{clientId}' />\n" +
                    $"<add key = 'OAuth2ClientSecret' value = '{clientSecret}' />\n" +
                    $"<add key = 'OAuth2RefreshToken' " + 
                    $"value = '{credential.Token.RefreshToken}' />\n");

                Console.WriteLine("Press <Enter> to continue...");
                Console.ReadLine();
            }
            catch (AggregateException)
            {
                Console.WriteLine("An error occured while authorizing the user.");
            }
        }

        /// <summary>
        /// Accepts the input with limited options.
        /// </summary>
        /// <param name="prompt">The user prompt.</param>
        /// <param name="options">The acceptable options.</param>
        /// <returns>The user response.</returns>
        /// <remarks>The options and user responses are converted to lower case.</remarks>
        private static string AcceptInputWithLimitedOptions(string prompt,
            IEnumerable<string> options)
        {
            List<string> sanitizedOptions = new List<string>(options)
                .Select(delegate(string item) { return item.ToLower(); }).ToList();

            string allowedOptionsPrompt = string.Join(" / ", sanitizedOptions);
            bool foundMatch = false;
            string response = "";

            while (!foundMatch)
            {
                Console.Write($"{prompt} ({allowedOptionsPrompt}): ");
                response = Console.ReadLine().Trim().ToLower();
                if (sanitizedOptions.Contains(response))
                {
                    foundMatch = true;
                }
                else
                {
                    foundMatch = false;
                    Console.WriteLine($"Invalid input: please enter {allowedOptionsPrompt}.");
                }
            }

            return response;
        }
    }
}

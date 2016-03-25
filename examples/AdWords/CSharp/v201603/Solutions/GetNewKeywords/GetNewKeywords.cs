// Copyright 2016, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.Util;
using Google.Api.Ads.AdWords.Util.Reports;
using Google.Api.Ads.AdWords.v201603;
using Google.Api.Ads.Common.Util;
using Google.Api.Ads.Common.Util.Reports;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {

  /// <summary>
  /// This code example shows how to generate keyword ideas for an existing
  /// campaign.
  /// </summary>
  public class GetNewKeywords : ExampleBase {

    /// <summary>
    /// Class to hold various settings when generating keyword ideas and
    /// estimates.
    /// </summary>
    /// <remarks>See https://developers.google.com/adwords/api/docs/appendix/limits
    /// for various limits related to request size.</remarks>
    public class Settings {

      /// <summary>
      /// Specifies how many seed keywords should be sent to
      /// TargetingIdeaService in a single request when getting keyword ideas.
      /// </summary>
      public const int TIS_KEYWORDS_LIST_SIZE = 200;

      /// <summary>
      /// Specifies how many keywords should be sent to TrafficEstimatorService
      /// in a single request when estimating traffic.
      /// </summary>
      public const int TES_KEYWORDS_LIST_SIZE = 2500;

      /// <summary>
      /// Specifies how many keywords should be sent to AdGroupCriterionService
      /// in a single request when checking policy violations
      /// </summary>
      public const int AGCS_KEYWORDS_LIST_SIZE = 2000;

      /// <summary>
      /// Specifies how many items to pick from Search Query performance report
      /// when picking seed keywords.
      /// </summary>
      public const int SQR_MAX_RESULTS = 10;

      /// <summary>
      /// Specifies how many items to pick from Keywords Performance report when
      /// picking seed keywords.
      /// </summary>
      public const int KPR_MAX_RESULTS = 10;

      /// <summary>
      /// The accuracy for float results.
      /// </summary>
      public const int ACCURACY = 3;

      /// <summary>
      /// Specifies what locations should be used when generating keyword ideas
      /// and estimates.
      /// </summary>
      public static readonly int[] LOCATIONS = new int[] {
        2840 // USA
      };

      /// <summary>
      /// Specifies what languages should be used when generating keyword ideas
      /// and estimates.
      /// </summary>
      public static readonly int[] LANGUAGES = new int[] {
        1000 // ENGLISH
      };

      /// <summary>
      /// Full path of the file from which user input seed keywords are picked.
      /// </summary>
      public static readonly string USER_KEYWORDS_FILE_PATH = ExampleUtilities.GetHomeDir() +
          Path.DirectorySeparatorChar + "user.csv";

      /// <summary>
      /// Full path of the file from which user negative terms are picked.
      /// </summary>
      public static readonly string USER_NEGATIVE_TERMS_FILE_PATH = ExampleUtilities.GetHomeDir() +
          Path.DirectorySeparatorChar + "negative.csv";
    }

    /// <summary>
    /// Source of the seed keyword.
    /// </summary>
    private enum Source {

      /// <summary>
      /// SQR report from the client's account.
      /// </summary>
      SQR,

      /// <summary>
      /// Top performing keywords from the client's account.
      /// </summary>
      ACCOUNT,

      /// <summary>
      /// User-specified list.
      /// </summary>
      USER
    }

    /// <summary>
    /// Class to hold keyword details.
    /// </summary>
    private class LocalKeyword {

      /// <summary>
      /// Gets or sets the keyword text.
      /// </summary>
      public string Text { get; set; }

      /// <summary>
      /// Gets or sets the keyword match type.
      /// </summary>
      public KeywordMatchType MatchType { get; set; }
    }

    /// <summary>
    /// Class to hold the keyword stats.
    /// </summary>
    private class Stat {

      /// <summary>
      /// Gets or sets the clicks received by a keyword.
      /// </summary>
      public long Clicks { get; set; }

      /// <summary>
      /// Gets or sets the impressions received by a keyword.
      /// </summary>
      public long Impressions { get; set; }
    }

    /// <summary>
    /// A seed keyword used for generating keyword ideas and estimates.
    /// </summary>
    private class SeedKeyword : IComparable<SeedKeyword> {

      /// <summary>
      /// Gets or sets the source of seed keyword.
      /// </summary>
      public Source Source { get; set; }

      /// <summary>
      /// Gets or sets the seed keyword.
      /// </summary>
      public LocalKeyword Keyword { get; set; }

      /// <summary>
      /// Gets or sets the seed keyword stat, if available.
      /// </summary>
      public Stat Stat { get; set; }

      /// <summary>
      /// Compares the current object with another object of the same type.
      /// </summary>
      /// <param name="other">An object to compare with this object.</param>
      /// <returns>
      /// A value that indicates the relative order of the objects being
      /// compared.
      /// </returns>
      public int CompareTo(SeedKeyword other) {
        if (this.Stat == null || other.Stat == null) {
          return 0;
        }
        return (int) (other.Stat.Clicks - this.Stat.Clicks);
      }
    }

    /// <summary>
    /// A keyword idea, based on a seed keyword.
    /// </summary>
    private class KeywordIdea {

      /// <summary>
      /// Gets or sets the keyword text.
      /// </summary>
      public string KeywordText { get; set; }

      /// <summary>
      /// Gets or sets the average searches for this keyword.
      /// </summary>
      public long AverageSearches { get; set; }

      /// <summary>
      /// Gets or sets the competition for this keyword.
      /// </summary>
      public double Competition { get; set; }

      /// <summary>
      /// Gets or sets the average CPC for this keyword.
      /// </summary>
      public long AverageCpc { get; set; }
    }

    /// <summary>
    /// Traffic estimate for a keyword.
    /// </summary>
    private class TrafficEstimate {
      private List<PolicyViolationError> errors = new List<PolicyViolationError>();

      /// <summary>
      /// Gets or sets the keyword text.
      /// </summary>
      public KeywordIdea Keyword { get; set; }

      /// <summary>
      /// Gets or sets the keyword matchtype.
      /// </summary>
      public KeywordMatchType MatchType { get; set; }

      /// <summary>
      /// Gets or sets the estimated clicks for this keyword.
      /// </summary>
      public float Clicks { get; set; }

      /// <summary>
      /// Gets or sets the estimated impressions estimated for this keyword.
      /// </summary>
      public float Impressions { get; set; }

      /// <summary>
      /// Gets or sets the estimated cost for this keyword.
      /// </summary>
      public long Cost { get; set; }

      /// <summary>
      /// Gets or sets the estimated average CPC for this keyword.
      /// </summary>
      public long AverageCpc { get; set; }

      /// <summary>
      /// Gets or sets the average position for this keyword.
      /// </summary>
      public double AveragePosition { get; set; }

      /// <summary>
      /// Gets the policy violation errors for this keyword.
      /// </summary>
      public List<PolicyViolationError> Errors {
        get {
          return errors;
        }
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      GetNewKeywords codeExample = new GetNewKeywords();
      Console.WriteLine(codeExample.Description);
      try {
        long campaignId = long.Parse("INSERT_CAMPAIGN_ID_HERE");
        long maxCpcInMicros = long.Parse("INSERT_MAX_CPC_HERE");
        string outputPath = "INSERT_OUTPUT_PATH_HERE";

        codeExample.Run(new AdWordsUser(), campaignId, maxCpcInMicros, outputPath);
      } catch (Exception e) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(e));
      }
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example shows how to generate keyword ideas for an existing campaign.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="campaignId">The campaign ID.</param>
    /// <param name="maxCpcInMicros">The max CPC in micros.</param>
    /// <param name="outputFile">The output file.</param>
    public void Run(AdWordsUser user, long campaignId, long maxCpcInMicros, string outputFile) {
      List<SeedKeyword> sqrKeywords = GetSeedKeywordsFromQueryReport(user, campaignId);
      List<SeedKeyword> accountKeywords = GetSeedKeywordsFromTopKeywords(user, campaignId);
      List<SeedKeyword> userKeywords = GetSeedKeywordsFromUserInput(
          Settings.USER_KEYWORDS_FILE_PATH);
      List<SeedKeyword> negativeTerms = GetNegativeTermsFromUserInput(
          Settings.USER_NEGATIVE_TERMS_FILE_PATH);

      HashSet<string> keywords = new HashSet<string>();
      keywords.UnionWith(GetKeyword(sqrKeywords));
      keywords.UnionWith(GetKeyword(accountKeywords));
      keywords.UnionWith(GetKeyword(userKeywords));

      List<KeywordIdea> ideas = GetKeywordIdeas(user, new List<string>(keywords),
          negativeTerms.ToArray());

      KeywordMatchType[] matchTypesToSearch = new KeywordMatchType[] {
        KeywordMatchType.BROAD,
        KeywordMatchType.EXACT,
        KeywordMatchType.PHRASE
      };

      List<TrafficEstimate> trafficEstimates = new List<TrafficEstimate>();
      foreach (KeywordMatchType matchType in matchTypesToSearch) {
        List<TrafficEstimate> trafficEstimate = GetTrafficEstimates(
            user,
            ideas,
            matchType,
            maxCpcInMicros,
            campaignId
        );
        trafficEstimates.AddRange(trafficEstimate);
      }

      long adGroupId = GetAdGroupForPolicyChecks(user, campaignId);
      CheckPolicyViolations(user, trafficEstimates, adGroupId);

      SaveEstimates(trafficEstimates, outputFile);
    }

    /// <summary>
    /// Saves the estimates as a CSV file.
    /// </summary>
    /// <param name="trafficEstimates">The traffic estimates.</param>
    /// <param name="outputFile">The output file.</param>
    private void SaveEstimates(List<TrafficEstimate> trafficEstimates, string outputFile) {
      CsvFile csvFile = new CsvFile();
      csvFile.Headers.AddRange(new string[] {"KeywordText", "MatchType", "Estimated Clicks",
        "Estimated Impressions", "Estimated Cost", "Estimated Avg. CPC", "Estimated Avg. Position",
        "Average Searches", "Competition", "Average CPC", "Policy Errors"});

      foreach (TrafficEstimate estimate in trafficEstimates) {
        csvFile.Records.Add(new string[] {
          estimate.Keyword.KeywordText,
          estimate.MatchType.ToString(),
          estimate.Clicks.ToString(),
          estimate.Impressions.ToString(),
          estimate.Cost.ToString(),
          estimate.AverageCpc.ToString(),
          estimate.AveragePosition.ToString(),
          estimate.Keyword.AverageSearches.ToString(),
          estimate.Keyword.Competition.ToString(),
          estimate.Keyword.AverageCpc.ToString(),
          GetPolicyErrorText(estimate.Errors)
        });
      }
      csvFile.Write(outputFile);
    }

    /// <summary>
    /// Gets the policy violation errors as a delimited string.
    /// </summary>
    /// <param name="errors">The list of policy violation errors.</param>
    /// <returns>The formatted policy violation errors.</returns>
    private string GetPolicyErrorText(List<PolicyViolationError> errors) {
      List<string> policyTexts = new List<string>();

      foreach (PolicyViolationError error in errors) {
        policyTexts.Add(string.Format("{0}|{1}|{2}", error.errorString, error.fieldPath,
            error.isExemptable));
      }
      return String.Join(";", policyTexts);
    }

    /// <summary>
    /// Checks a list of keywords for policy violations, and add the errors to
    /// a list of traffic estimates.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="trafficEstimates">The list of keywords and their traffic
    /// estimates.</param>
    /// <param name="adGroupId">The ad group ID.</param>
    /// <remarks>Users can use the policy violation details to decide whether
    /// to pick a keyword and submit an exemption request, or skip the
    /// violating keyword and scout for other keywords that are policy
    /// compliant.</remarks>
    private void CheckPolicyViolations(AdWordsUser user, List<TrafficEstimate> trafficEstimates,
        long adGroupId) {
      // Get the AdGroupCriterionService.
      AdGroupCriterionService adGroupCriterionService =
          (AdGroupCriterionService) user.GetService(
              AdWordsService.v201603.AdGroupCriterionService);

      adGroupCriterionService.RequestHeader.validateOnly = true;

      for (int i = 0; i < trafficEstimates.Count; i += Settings.AGCS_KEYWORDS_LIST_SIZE) {
        List<AdGroupCriterionOperation> operations = new List<AdGroupCriterionOperation>();

        for (int j = i; j < i + Settings.AGCS_KEYWORDS_LIST_SIZE &&
            j < trafficEstimates.Count; j++) {
          AdGroupCriterionOperation operation = new AdGroupCriterionOperation() {
            @operator = Operator.ADD,
            operand = new BiddableAdGroupCriterion() {
              adGroupId = adGroupId,
              criterion = new Keyword() {
                text = trafficEstimates[i].Keyword.KeywordText,
                matchType = trafficEstimates[i].MatchType,
              },
              userStatus = UserStatus.ENABLED
            }
          };

          operations.Add(operation);
        }

        try {
          AdGroupCriterionReturnValue retVal = adGroupCriterionService.mutate(
              operations.ToArray());
        } catch (AdWordsApiException e) {
          ApiException innerException = e.ApiException as ApiException;
          if (innerException == null) {
            throw new Exception("Failed to retrieve ApiError. See inner exception for more " +
                "details.", e);
          }

          // Examine each ApiError received from the server.
          foreach (ApiError apiError in innerException.errors) {
            int index = ErrorUtilities.GetOperationIndex(apiError.fieldPath);
            if (index == -1) {
              // This API error is not associated with an operand, so we cannot
              // recover from this error by removing one or more operations.
              // Rethrow the exception for manual inspection.
              throw;
            }

            // Handle policy violation errors.
            if (apiError is PolicyViolationError) {
              PolicyViolationError policyError = (PolicyViolationError) apiError;
              trafficEstimates[i + index].Errors.Add(policyError);
            }
          }
        }
      }
      return;
    }

    /// <summary>
    /// Gets the ad group for policy checks.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="campaignId">The campaign identifier.</param>
    /// <returns></returns>
    /// <exception cref="System.ApplicationException">Failed to retrieve ad
    /// group for policy checks.</exception>
    private long GetAdGroupForPolicyChecks(AdWordsUser user, long campaignId) {
      AdGroupService adGroupService = (AdGroupService) user.GetService(
          AdWordsService.v201603.AdGroupService);

      string query = string.Format("Select AdGroupId where CampaignId = {0}", campaignId);

      try {
        AdGroupPage page = adGroupService.query(query);
        return page.entries[0].id;
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to retrieve ad group for policy checks.", e);
      }
    }

    /// <summary>
    /// Gets the seed keywords from a Search Query Report.
    /// </summary>
    /// <param name="user">The user for which reports are run.</param>
    /// <param name="campaignId">ID of the campaign for which we are generating
    ///  keyword ideas.</param>
    /// <param name="maxResults">The maximum results to pick from SQR.</param>
    /// <param name="comparer">A comparer, to decide how to sort the report
    /// rows.</param>
    /// <returns>A list of seed keywords from SQR, to be used for getting
    /// further keyword ideas.</returns>
    private List<SeedKeyword> GetSeedKeywordsFromQueryReport(AdWordsUser user, long campaignId) {
      string query = string.Format("Select Query, MatchTypeWithVariant, Clicks, Impressions " +
          "from SEARCH_QUERY_PERFORMANCE_REPORT where CampaignId = {0} during LAST_MONTH",
          campaignId);

      AdWordsAppConfig config = (AdWordsAppConfig) user.Config;
      config.SkipReportHeader = true;
      config.SkipReportSummary = true;

      ReportUtilities utilities = new ReportUtilities(user, query, DownloadFormat.CSV.ToString());
      ReportResponse response = utilities.GetResponse();

      List<SeedKeyword> retval = new List<SeedKeyword>();

      using (response) {
        byte[] data = response.Download();
        string report = Encoding.UTF8.GetString(data);

        CsvFile csvFile = new CsvFile();
        csvFile.ReadFromString(report, true);

        foreach (string[] row in csvFile.Records) {
          row[1] = row[1].Replace("(close variant)", "").Trim();
          SeedKeyword sqrKeyword = new SeedKeyword() {
            Keyword = new LocalKeyword() {
              Text = row[0],
              MatchType = (KeywordMatchType) Enum.Parse(typeof(KeywordMatchType), row[1], true)
            },
            Stat = new Stat() {
              Clicks = long.Parse(row[2]),
              Impressions = long.Parse(row[3])
            },
            Source = GetNewKeywords.Source.SQR
          };
          retval.Add(sqrKeyword);
        }
      }

      LimitResults(retval, Settings.SQR_MAX_RESULTS);
      return retval;
    }

    /// <summary>
    /// Gets the seed keywords from top keywords in a campaign.
    /// </summary>
    /// <param name="user">The user for which reports are run.</param>
    /// <param name="campaignId">ID of the campaign for which we are generating
    ///  keyword ideas.</param>
    /// <param name="maxResults">The maximum results to pick from SQR.</param>
    /// <param name="comparer">A comparer, to decide how to sort the report
    /// rows.</param>
    /// <returns>A list of top performing keywords in a campaign, that can be
    /// used for generating new keywords.</returns>
    private List<SeedKeyword> GetSeedKeywordsFromTopKeywords(AdWordsUser user, long campaignId) {
      string query = string.Format("Select KeywordText, KeywordMatchType, Clicks, Impressions " +
          "from KEYWORDS_PERFORMANCE_REPORT where CampaignId={0} during LAST_MONTH",
          campaignId);

      AdWordsAppConfig config = (AdWordsAppConfig) user.Config;
      config.SkipReportHeader = true;
      config.SkipReportSummary = true;

      ReportUtilities utilities = new ReportUtilities(user, query, DownloadFormat.CSV.ToString());
      ReportResponse response = utilities.GetResponse();

      List<SeedKeyword> retval = new List<SeedKeyword>();

      using (response) {
        byte[] data = response.Download();
        string report = Encoding.UTF8.GetString(data);

        CsvFile csvFile = new CsvFile();
        csvFile.ReadFromString(report, true);

        foreach (string[] row in csvFile.Records) {
          row[1] = row[1].Replace("(close variant)", "").Trim();
          SeedKeyword accountKeyword = new SeedKeyword() {
            Keyword = new LocalKeyword() {
              Text = row[0],
              MatchType = (KeywordMatchType) Enum.Parse(typeof(KeywordMatchType), row[1], true)
            },
            Stat = new Stat() {
              Clicks = long.Parse(row[2]),
              Impressions = long.Parse(row[3])
            },
            Source = GetNewKeywords.Source.ACCOUNT
          };
          retval.Add(accountKeyword);
        }
      }
      LimitResults(retval, Settings.KPR_MAX_RESULTS);
      return retval;
    }

    /// <summary>
    /// Gets a list of the seed keywords from user input.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <returns>A list of user-provided keywords that will be used for
    /// generating keyword ideas.</returns>
    private List<SeedKeyword> GetSeedKeywordsFromUserInput(string filePath) {
      CsvFile csvFile = new CsvFile();
      csvFile.Read(filePath, true);

      List<SeedKeyword> retval = new List<SeedKeyword>();

      foreach (string[] row in csvFile.Records) {
        SeedKeyword userKeyword = new SeedKeyword() {
          Keyword = new LocalKeyword() {
            Text = row[0],
            MatchType = (KeywordMatchType) Enum.Parse(typeof(KeywordMatchType), row[1], true)
          },
          Source = GetNewKeywords.Source.USER
        };
        retval.Add(userKeyword);
      }
      return retval;
    }

    /// <summary>
    /// Gets a list of the negative terms from user input.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <returns>A list of user-provided terms that should be excluded when
    /// generating keyword ideas.</returns>
    private List<SeedKeyword> GetNegativeTermsFromUserInput(string filePath) {
      return GetSeedKeywordsFromUserInput(filePath);
    }

    /// <summary>
    /// Gets keyword ideas for the list of Seed keywords.
    /// </summary>
    /// <param name="user">The user for which keyword ideas are generated.
    /// </param>
    /// <param name="seedKeywords">The seed keywords for generating ideas.
    /// </param>
    /// <param name="searchParameters">The search parameters to be used when
    /// generating keyword ideas.</param>
    /// <returns>A list of keyword ideas.</returns>
    private List<KeywordIdea> GetKeywordIdeas(AdWordsUser user, List<string> seedKeywords,
        SeedKeyword[] negativeTerms) {
      // Get the TargetingIdeaService.
      TargetingIdeaService targetingIdeaService =
          (TargetingIdeaService) user.GetService(AdWordsService.v201603.TargetingIdeaService);

      IdeaTextFilterSearchParameter excludedTerms = null;
      if (negativeTerms.Length > 0) {
        excludedTerms = GetExcludedKeywordSearchParams(negativeTerms);
      }

      LanguageSearchParameter languageParams = GetLanguageSearchParams();
      LocationSearchParameter locationParams = GetLocationSearchParams();

      List<KeywordIdea> retval = new List<KeywordIdea>();

      for (int i = 0; i < seedKeywords.Count; i += Settings.TIS_KEYWORDS_LIST_SIZE) {
        List<string> keywordsToSearch = new List<string>();
        for (int j = i; j < i + Settings.TIS_KEYWORDS_LIST_SIZE && j < seedKeywords.Count; j++) {
          keywordsToSearch.Add(seedKeywords[j]);
        }

        // Create selector.
        TargetingIdeaSelector selector = new TargetingIdeaSelector();
        selector.requestType = RequestType.IDEAS;
        selector.ideaType = IdeaType.KEYWORD;
        selector.requestedAttributeTypes = new AttributeType[] {
          AttributeType.KEYWORD_TEXT,
          AttributeType.SEARCH_VOLUME,
          AttributeType.AVERAGE_CPC,
          AttributeType.COMPETITION
        };

        List<SearchParameter> paramList = new List<SearchParameter>();
        paramList.Add(new RelatedToQuerySearchParameter() {
          queries = keywordsToSearch.ToArray(),
        });

        if (excludedTerms != null) {
          paramList.Add(excludedTerms);
        }
        paramList.Add(locationParams);
        paramList.Add(languageParams);

        selector.searchParameters = paramList.ToArray();

        // Set selector paging (required for targeting idea service).
        Paging paging = Paging.Default;
        TargetingIdeaPage page = new TargetingIdeaPage();

        try {
          do {
            // Get related keywords.
            page = targetingIdeaService.get(selector);

            // Display related keywords.
            if (page.entries != null && page.entries.Length > 0) {
              foreach (TargetingIdea targetingIdea in page.entries) {
                string keyword = null;
                long averageMonthlySearches = 0;
                long averageCpc = 0;
                double competition = 0f;

                foreach (Type_AttributeMapEntry entry in targetingIdea.data) {
                  if (entry.key == AttributeType.KEYWORD_TEXT) {
                    StringAttribute temp = (entry.value as StringAttribute);
                    if (temp != null) {
                      keyword = temp.value;
                    }
                  }

                  if (entry.key == AttributeType.SEARCH_VOLUME) {
                    LongAttribute temp = (entry.value as LongAttribute);
                    if (temp != null) {
                      averageMonthlySearches = temp.value;
                    }
                  }

                  if (entry.key == AttributeType.AVERAGE_CPC) {
                    MoneyAttribute temp = (entry.value as MoneyAttribute);
                    if (temp != null && temp.value != null) {
                      averageCpc = temp.value.microAmount;
                    }
                  }

                  if (entry.key == AttributeType.COMPETITION) {
                    DoubleAttribute temp = (entry.value as DoubleAttribute);
                    if (temp != null) {
                      competition = temp.value;
                    }
                  }
                }

                KeywordIdea keywordIdea = new KeywordIdea() {
                  KeywordText = keyword,
                  AverageSearches = averageMonthlySearches,
                  AverageCpc = averageCpc,
                  Competition = Math.Round(competition, Settings.ACCURACY,
                      MidpointRounding.AwayFromZero)
                };
                retval.Add(keywordIdea);
              }
            }
            selector.paging.IncreaseOffset();
          } while (selector.paging.startIndex < page.totalNumEntries);
        } catch (Exception e) {
          throw new System.ApplicationException("Failed to retrieve related keywords.", e);
        }
      }
      return retval;
    }

    /// <summary>
    /// Gets the traffic estimates for a list of keywords.
    /// </summary>
    /// <param name="user">The user for which keyword ideas are generated.</param>
    /// <param name="keywords">The keywords for which traffic estimates are
    ///  done.</param>
    /// <param name="matchType">Type of the keyword match to apply when making
    /// traffic estimates.</param>
    /// <param name="maxCpc">The maximum CPC to consider for the traffic.</param>
    /// <param name="campaignId">The campaign ID whose settings should be used
    /// for traffic estimation.</param>
    /// <returns>A list of traffic estimates for the keywords.</returns>
    private List<TrafficEstimate> GetTrafficEstimates(AdWordsUser user,
        List<KeywordIdea> keywords, KeywordMatchType matchType, long maxCpc, long campaignId) {
      // Get the TrafficEstimatorService.
      TrafficEstimatorService trafficEstimatorService = (TrafficEstimatorService) user.GetService(
          AdWordsService.v201603.TrafficEstimatorService);

      List<Criterion> trafficCriteria = GetTrafficEstimateCriteria();
      List<TrafficEstimate> retval = new List<TrafficEstimate>();

      // Eliminate duplicate keywords in the keyword suggestion list so that
      // TrafficEstimatorService doesn't complain about them.
      Dictionary<string, KeywordIdea> uniqueEntries = new Dictionary<string, KeywordIdea>();

      for (int i = 0; i < keywords.Count; i++) {
        if (!uniqueEntries.ContainsKey(keywords[i].KeywordText)) {
          uniqueEntries[keywords[i].KeywordText] = keywords[i];
        }
      }

      keywords = new List<KeywordIdea>(uniqueEntries.Values);

      for (int i = 0; i < keywords.Count; i += Settings.TES_KEYWORDS_LIST_SIZE) {
        List<KeywordEstimateRequest> keywordEstimateRequests = new List<KeywordEstimateRequest>();

        for (int j = i; j < i + Settings.TES_KEYWORDS_LIST_SIZE && j < keywords.Count; j++) {
          KeywordEstimateRequest keywordEstimateRequest = new KeywordEstimateRequest() {
            keyword = new Keyword() {
              text = keywords[j].KeywordText,
              matchType = matchType
            }
          };
          keywordEstimateRequests.Add(keywordEstimateRequest);
        }

        // Create campaign estimate requests.
        CampaignEstimateRequest campaignEstimateRequest = new CampaignEstimateRequest() {
          adGroupEstimateRequests = new AdGroupEstimateRequest[] {
            new AdGroupEstimateRequest() {
              keywordEstimateRequests = keywordEstimateRequests.ToArray(),
              maxCpc = new Money() {
                microAmount = maxCpc
              }
            }
          }
        };

        campaignEstimateRequest.criteria = trafficCriteria.ToArray();
        campaignEstimateRequest.campaignId = campaignId;

        // Create the selector.
        TrafficEstimatorSelector selector = new TrafficEstimatorSelector();
        selector.campaignEstimateRequests = new CampaignEstimateRequest[] {
          campaignEstimateRequest
        };

        try {
          // Get traffic estimates.
          TrafficEstimatorResult result = trafficEstimatorService.get(selector);

          // Display traffic estimates.
          if (result != null && result.campaignEstimates != null &&
              result.campaignEstimates.Length > 0) {
            CampaignEstimate campaignEstimate = result.campaignEstimates[0];
            if (campaignEstimate.adGroupEstimates != null &&
                campaignEstimate.adGroupEstimates.Length > 0) {
              AdGroupEstimate adGroupEstimate = campaignEstimate.adGroupEstimates[0];

              if (adGroupEstimate.keywordEstimates != null) {
                for (int k = 0; k < adGroupEstimate.keywordEstimates.Length; k++) {
                  Keyword keyword = keywordEstimateRequests[k].keyword;
                  KeywordEstimate keywordEstimate = adGroupEstimate.keywordEstimates[k];

                  if (keywordEstimateRequests[k].isNegative) {
                    continue;
                  }

                  // Find the mean of the min and max values.
                  long meanAverageCpc = 0;
                  double meanAveragePosition = 0;
                  long meanClicks = 0;
                  long meanImpressions = 0;
                  long meanTotalCost = 0;

                  if (keywordEstimate.min != null && keywordEstimate.max != null) {
                    if (keywordEstimate.min.averageCpc != null &&
                        keywordEstimate.max.averageCpc != null) {
                      meanAverageCpc = (long) Math.Round(
                          (double) (keywordEstimate.min.averageCpc.microAmount
                           + keywordEstimate.max.averageCpc.microAmount) / 2, Settings.ACCURACY,
                           MidpointRounding.AwayFromZero);
                    }
                    meanAveragePosition = Math.Round((keywordEstimate.min.averagePosition
                          + keywordEstimate.max.averagePosition) / 2, Settings.ACCURACY,
                          MidpointRounding.AwayFromZero);

                    meanClicks = (long) Math.Round((keywordEstimate.min.clicksPerDay +
                        keywordEstimate.max.clicksPerDay) / 2, MidpointRounding.AwayFromZero);

                    meanImpressions = (long) Math.Round((keywordEstimate.min.impressionsPerDay
                        + keywordEstimate.max.impressionsPerDay) / 2,
                        MidpointRounding.AwayFromZero);

                    if (keywordEstimate.min.totalCost != null &&
                        keywordEstimate.max.totalCost != null) {
                      meanTotalCost = (keywordEstimate.min.totalCost.microAmount
                     + keywordEstimate.max.totalCost.microAmount) / 2;
                    }
                  }

                  TrafficEstimate trafficEstimate = new TrafficEstimate() {
                    Keyword = keywords[i + k],
                    MatchType = keyword.matchType,
                    Clicks = meanClicks,
                    Impressions = meanImpressions,
                    Cost = meanTotalCost,
                    AverageCpc = meanAverageCpc,
                    AveragePosition = meanAveragePosition
                  };
                  retval.Add(trafficEstimate);
                }
              }
            }
          }
        } catch (Exception e) {
          throw new System.ApplicationException("Failed to retrieve traffic estimates.", e);
        }
      }

      return retval;
    }

    /// <summary>
    /// Gets the location search parameters.
    /// </summary>
    /// <returns>The location search parameters.</returns>
    private static LocationSearchParameter GetLocationSearchParams() {
      LocationSearchParameter locationParams = new LocationSearchParameter();
      List<Location> locations = new List<Location>();

      foreach (int locationCode in Settings.LOCATIONS) {
        locations.Add(new Location() {
          id = locationCode
        });
      }
      locationParams.locations = locations.ToArray();
      return locationParams;
    }

    /// <summary>
    /// Gets the language search parameters.
    /// </summary>
    /// <returns>The language search parameters.</returns>
    private static LanguageSearchParameter GetLanguageSearchParams() {
      LanguageSearchParameter languageParams = new LanguageSearchParameter();
      List<Language> languages = new List<Language>();

      foreach (int languageCode in Settings.LANGUAGES) {
        languages.Add(new Language() {
          id = languageCode
        });
      }
      languageParams.languages = languages.ToArray();
      return languageParams;
    }

    /// <summary>
    /// Gets the excluded keyword search parameters.
    /// </summary>
    /// <param name="negativeTerms">The negative terms.</param>
    /// <returns>The excluded search parameters.</returns>
    private static IdeaTextFilterSearchParameter GetExcludedKeywordSearchParams(
        SeedKeyword[] negativeTerms) {
      IdeaTextFilterSearchParameter excludedKeywords = new IdeaTextFilterSearchParameter();
      List<string> keywords = new List<string>();

      foreach (SeedKeyword negativeTerm in negativeTerms) {
        keywords.Add(negativeTerm.Keyword.Text);
      }

      excludedKeywords.excluded = keywords.ToArray();
      return excludedKeywords;
    }

    /// <summary>
    /// Gets the traffic estimate criteria.
    /// </summary>
    /// <returns>The traffic estimate criteria.</returns>
    private List<Criterion> GetTrafficEstimateCriteria() {
      List<Criterion> trafficCriteria = new List<Criterion>();

      foreach (long locationId in Settings.LOCATIONS) {
        trafficCriteria.Add(new Location() {
          id = locationId
        });
      }

      foreach (long languageId in Settings.LANGUAGES) {
        trafficCriteria.Add(new Language() {
          id = languageId
        });
      }
      return trafficCriteria;
    }

    /// <summary>
    /// Gets the keywords from a list of seed keywords.
    /// </summary>
    /// <param name="seedKeywords">The seed keywords.</param>
    /// <returns>The list of keywords.</returns>
    private IEnumerable<string> GetKeyword(List<SeedKeyword> seedKeywords) {
      List<string> keywords = new List<string>();
      foreach (SeedKeyword seedKeyword in seedKeywords) {
        keywords.Add(seedKeyword.Keyword.Text);
      }
      return keywords;
    }

    /// <summary>
    /// Limits the results.
    /// </summary>
    /// <param name="allResults">All results.</param>
    /// <param name="maxResults">The maximum results.</param>
    private static void LimitResults(List<SeedKeyword> allResults, int maxResults) {
      allResults.Sort();

      if (allResults.Count > maxResults) {
        allResults.RemoveRange(maxResults, allResults.Count - maxResults);
      }
    }
  }
}

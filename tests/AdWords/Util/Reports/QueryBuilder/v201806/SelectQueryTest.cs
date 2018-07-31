// Copyright 2018, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdWords.Util.Reports.v201806;
using Google.Api.Ads.AdWords.v201806;
using NUnit.Framework;

namespace Google.Api.Ads.AdWords.Tests.Util.Reports.QueryBuilder.v201806 {

  /// <summary>
  /// Unit tests for the <see cref="SelectQuery"/> class.
  /// </summary>
  internal class SelectQueryTest {

    /// <summary>
    /// Tests for <see cref="SelectQuery.HasNextPage(Page)"/>.
    /// </summary>
    [Test]
    public void TestHasNextPage() {
      SelectQueryBuilder queryBuilder = null;
      SelectQuery query = null;

      queryBuilder = new SelectQueryBuilder();
      query = queryBuilder
          .Select("CampaignId", "Status", "Clicks", "Impressions")
          .Where("Clicks").Equals(20)
          .OrderByAscending("CampaignId")
          .OrderByDescending("Status")
          .Build();

      // Tests for regular pages.
      CampaignPage campaignPage = new CampaignPage();

      // Start index is less than total number of entries.
      query.Limit(1, 20);
      campaignPage.totalNumEntries = 15;
      Assert.IsTrue(query.HasNextPage(campaignPage));

      // Start index is greater than total number of entries.
      query.Limit(18, 20);
      campaignPage.totalNumEntries = 15;
      Assert.IsFalse(query.HasNextPage(campaignPage));

      // Tests for AdGroupBidLandscapePage.
      AdGroupBidLandscapePage adGroupBidLandscapePage = new AdGroupBidLandscapePage();
      adGroupBidLandscapePage.entries = new AdGroupBidLandscape[] {
        new AdGroupBidLandscape() {
          campaignId = 123,
          adGroupId = 456,
          landscapePoints = new BidLandscapeLandscapePoint[] {
            new BidLandscapeLandscapePoint() {
              clicks = 10,
              impressions = 200
            },
            new BidLandscapeLandscapePoint() {
              clicks = 12,
              impressions = 232
            },
          }
        },
        new AdGroupBidLandscape() {
          campaignId = 125,
          adGroupId = 454,
          landscapePoints = new BidLandscapeLandscapePoint[] {
            new BidLandscapeLandscapePoint() {
              clicks = 10,
              impressions = 200
            },
            new BidLandscapeLandscapePoint() {
              clicks = 5,
              impressions = 232
            },
            new BidLandscapeLandscapePoint() {
              clicks = 66,
              impressions = 550
            },
          }
        }
      };

      // Start index is less than total number of landscape points (5).
      query.Limit(1, 20);
      Assert.IsTrue(query.HasNextPage(adGroupBidLandscapePage));

      adGroupBidLandscapePage.entries = new AdGroupBidLandscape[] {
        new AdGroupBidLandscape() {
          campaignId = 123,
          adGroupId = 456,
          landscapePoints = new BidLandscapeLandscapePoint[] {}
        },
        new AdGroupBidLandscape() {
          campaignId = 125,
          adGroupId = 454,
          landscapePoints = null
        }
      };

      // Start index is less than total number of landscape points (0).
      query.Limit(1, 20);
      Assert.IsFalse(query.HasNextPage(adGroupBidLandscapePage));

      // Tests for CriterionBidLandscapePage.
      CriterionBidLandscapePage criterionBidLandscapePage = new CriterionBidLandscapePage() {
        entries = new CriterionBidLandscape[] {
          new CriterionBidLandscape() {
            campaignId = 125,
            adGroupId = 454,
            landscapePoints = new BidLandscapeLandscapePoint[] {
              new BidLandscapeLandscapePoint() {
                clicks = 10,
                impressions = 200
              },
              new BidLandscapeLandscapePoint() {
                clicks = 5,
                impressions = 232
              },
              new BidLandscapeLandscapePoint() {
                clicks = 66,
                impressions = 550
              },
            }
          }, new CriterionBidLandscape() {
            campaignId = 125,
            adGroupId = 454,
            landscapePoints = new BidLandscapeLandscapePoint[] {
              new BidLandscapeLandscapePoint() {
                clicks = 10,
                impressions = 200
              },
              new BidLandscapeLandscapePoint() {
                clicks = 5,
                impressions = 232
              },
              new BidLandscapeLandscapePoint() {
                clicks = 66,
                impressions = 550
              },
            }
          }
        }
      };

      // Start index is less than total number of landscape points (5).
      query.Limit(1, 20);
      Assert.IsTrue(query.HasNextPage(criterionBidLandscapePage));

      // Start index is less than total number of landscape points (0).
      criterionBidLandscapePage.entries = new CriterionBidLandscape[] {
        new CriterionBidLandscape() {
          campaignId = 123,
          adGroupId = 456,
          landscapePoints = new BidLandscapeLandscapePoint[] {}
        },
        new CriterionBidLandscape() {
          campaignId = 125,
          adGroupId = 454,
          landscapePoints = null
        }
      };
      query.Limit(1, 20);
      Assert.IsFalse(query.HasNextPage(criterionBidLandscapePage));
    }

    /// <summary>
    /// Tests for <see cref="SelectQuery.NextPage(Page)"/>.
    /// </summary>
    [Test]
    public void TestNextPage() {
      SelectQueryBuilder queryBuilder = null;
      SelectQuery query = null;

      queryBuilder = new SelectQueryBuilder();
      query = queryBuilder
          .Select("CampaignId", "Status", "Clicks", "Impressions")
          .Where("Clicks").Equals(20)
          .OrderByAscending("CampaignId")
          .OrderByDescending("Status")
          .Build();

      // Test for regular pages.
      CampaignPage campaignPage = new CampaignPage();

      // Query limits should increment by numberResults (20).
      query.Limit(1, 20);
      query.NextPage(campaignPage);
      Assert.IsTrue(query.ToString().Contains("LIMIT 21, 20"));

      // Test for AdGroupBidLandscapePage.
      AdGroupBidLandscapePage adGroupBidLandscapePage = new AdGroupBidLandscapePage();
      adGroupBidLandscapePage.entries = new AdGroupBidLandscape[] {
        new AdGroupBidLandscape() {
          campaignId = 123,
          adGroupId = 456,
          landscapePoints = new BidLandscapeLandscapePoint[] {
            new BidLandscapeLandscapePoint() {
              clicks = 10,
              impressions = 200
            },
            new BidLandscapeLandscapePoint() {
              clicks = 12,
              impressions = 232
            },
          }
        },
        new AdGroupBidLandscape() {
          campaignId = 125,
          adGroupId = 454,
          landscapePoints = new BidLandscapeLandscapePoint[] {
            new BidLandscapeLandscapePoint() {
              clicks = 10,
              impressions = 200
            },
            new BidLandscapeLandscapePoint() {
              clicks = 5,
              impressions = 232
            },
            new BidLandscapeLandscapePoint() {
              clicks = 66,
              impressions = 550
            },
          }
        }
      };

      // Query limits should increment by total landscapePoints (5).
      query.Limit(1, 20);
      query.NextPage(adGroupBidLandscapePage);
      Assert.IsTrue(query.ToString().Contains("LIMIT 6, 20"));

      // Query limits should increment by total landscapePoints (0).
      adGroupBidLandscapePage.entries = new AdGroupBidLandscape[] {
        new AdGroupBidLandscape() {
          campaignId = 123,
          adGroupId = 456,
          landscapePoints = new BidLandscapeLandscapePoint[] {}
        },
        new AdGroupBidLandscape() {
          campaignId = 125,
          adGroupId = 454,
          landscapePoints = null
        }
      };

      query.Limit(1, 20);
      query.NextPage(adGroupBidLandscapePage);
      Assert.IsTrue(query.ToString().Contains("LIMIT 1, 20"));

      CriterionBidLandscapePage criterionBidLandscapePage = new CriterionBidLandscapePage() {
        entries = new CriterionBidLandscape[] {
          new CriterionBidLandscape() {
            campaignId = 125,
            adGroupId = 454,
            landscapePoints = new BidLandscapeLandscapePoint[] {
              new BidLandscapeLandscapePoint() {
                clicks = 10,
                impressions = 200
              },
              new BidLandscapeLandscapePoint() {
                clicks = 5,
                impressions = 232
              },
              new BidLandscapeLandscapePoint() {
                clicks = 66,
                impressions = 550
              },
            }
          }, new CriterionBidLandscape() {
            campaignId = 125,
            adGroupId = 454,
            landscapePoints = new BidLandscapeLandscapePoint[] {
              new BidLandscapeLandscapePoint() {
                clicks = 10,
                impressions = 200
              },
              new BidLandscapeLandscapePoint() {
                clicks = 5,
                impressions = 232
              },
              new BidLandscapeLandscapePoint() {
                clicks = 66,
                impressions = 550
              },
            }
          }
        }
      };

      // Query limits should increment by total landscapePoints (6).
      query.Limit(1, 20);
      query.NextPage(criterionBidLandscapePage);
      Assert.IsTrue(query.ToString().Contains("LIMIT 7, 20"));

      // Query limits should increment by total landscapePoints (0).
      criterionBidLandscapePage.entries = new CriterionBidLandscape[] {
        new CriterionBidLandscape() {
          campaignId = 123,
          adGroupId = 456,
          landscapePoints = new BidLandscapeLandscapePoint[] {}
        },
        new CriterionBidLandscape() {
          campaignId = 125,
          adGroupId = 454,
          landscapePoints = null
        }
      };

      query.Limit(1, 20);
      query.NextPage(criterionBidLandscapePage);
      Assert.IsTrue(query.ToString().Contains("LIMIT 1, 20"));
    }
  }
}

// Copyright 2010, Google Inc. All Rights Reserved.
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

using com.google.api.adwords.lib;
using com.google.api.adwords.v200909;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace com.google.api.adwords.tests.v200909 {
  /// <summary>
  /// UnitTests for <see cref="BulkMutateJobService"/> class.
  /// </summary>
  [TestFixture]
  class BulkMutateJobServiceTests : BaseTests {
    /// <summary>
    /// AdGroupAdService object to be used in this test.
    /// </summary>
    BulkMutateJobService bulkMutateJobService;

    /// <summary>
    /// The AdWords user to be used for tests.
    /// </summary>
    AdWordsUser user = new AdWordsUser();

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public BulkMutateJobServiceTests()
      : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      bulkMutateJobService = (BulkMutateJobService) user.GetService(
          AdWordsService.v200909.BulkMutateJobService);
    }

    /// <summary>
    /// Test whether we can fetch all jobs currently in the queue.
    /// </summary>
    [Test]
    public void TestGetAllJobs() {
      Assert.That(bulkMutateJobService.get(new BulkMutateJobSelector()) is BulkMutateJob[]);
    }

    /// <summary>
    /// Test whether we can fetch all COMPLETED jobs.
    /// </summary>
    [Test]
    public void TestGetAllCompletedJobs() {
      BulkMutateJobSelector selector = new BulkMutateJobSelector();
      selector.jobStatuses = new BasicJobStatus[] {BasicJobStatus.COMPLETED};

      Assert.That(bulkMutateJobService.get(selector) is BulkMutateJob[]);
    }

    /// <summary>
    /// Test whether we can set campaign targets using single part job with
    /// single stream and multiple operations.
    /// </summary>
    [Test]
    public void TestSinglePartSingleStreamMultipleOperations() {
      TestUtils utils = new TestUtils();
      long campaignId = utils.CreateCampaign(user, true);

      TargetList[] campaignTargets = CreateCampaignTargets(campaignId);
      Operation[] targetOperations = new Operation[campaignTargets.Length];

      for (int i = 0; i < campaignTargets.Length; i++) {
        CampaignTargetOperation operation = new CampaignTargetOperation();
        operation.operatorSpecified = true;
        operation.@operator = Operator.SET;
        operation.operand = campaignTargets[i];
        targetOperations[i] = operation;
      }

      OperationStream targetOpStream = new OperationStream();

      targetOpStream.scopingEntityId = new EntityId();
      targetOpStream.scopingEntityId.type = EntityIdType.CAMPAIGN_ID;
      targetOpStream.scopingEntityId.typeSpecified = true;
      targetOpStream.scopingEntityId.value = campaignId;
      targetOpStream.scopingEntityId.valueSpecified = true;

      targetOpStream.operations = targetOperations;

      BulkMutateJob bulkJob = null;

      bulkJob = new BulkMutateJob();
      bulkJob.numRequestParts = 1;
      bulkJob.numRequestPartsSpecified = true;

      // b. Create a part of the job.

      BulkMutateRequest bulkRequest1 = new BulkMutateRequest();
      bulkRequest1.partIndex = 0;
      bulkRequest1.partIndexSpecified = true;
      bulkRequest1.operationStreams = new OperationStream[] {targetOpStream};
      bulkJob.request = bulkRequest1;

      // c. Create job operation.
      JobOperation jobOperation1 = new JobOperation();
      jobOperation1.operatorSpecified = true;
      jobOperation1.@operator = Operator.ADD;
      jobOperation1.operand = bulkJob;

      bulkJob = bulkMutateJobService.mutate(jobOperation1);

      bool completed = false;

      while (completed == false) {
        Thread.Sleep(2000);

        BulkMutateJobSelector selector = new BulkMutateJobSelector();
        selector.jobIds = new long[] {bulkJob.id};

        BulkMutateJob[] allJobs = bulkMutateJobService.get(selector);
        if (allJobs != null && allJobs.Length > 0) {
          if (allJobs[0].status == BasicJobStatus.COMPLETED ||
              allJobs[0].status == BasicJobStatus.FAILED) {
            completed = true;
            bulkJob = allJobs[0];
            Assert.Pass();
            break;
          }
        }
      }
    }

    /// <summary>
    /// Test whether we can set campaign targets using single part job with
    /// single stream and multiple operations.
    /// </summary>
    [Test]
    public void TestSinglePartMultipleStreamsSingleOperation() {
      TestUtils utils = new TestUtils();
      long campaignId = utils.CreateCampaign(user, true);

      TargetList[] campaignTargets = CreateCampaignTargets(campaignId);
      OperationStream[] operationStreams = new OperationStream[campaignTargets.Length];

      for (int i = 0; i < campaignTargets.Length; i++) {
        CampaignTargetOperation operation = new CampaignTargetOperation();
        operation.operatorSpecified = true;
        operation.@operator = Operator.SET;
        operation.operand = campaignTargets[i];

        OperationStream operationStream = new OperationStream();

        operationStream.scopingEntityId = new EntityId();
        operationStream.scopingEntityId.type = EntityIdType.CAMPAIGN_ID;
        operationStream.scopingEntityId.typeSpecified = true;
        operationStream.scopingEntityId.value = campaignId;
        operationStream.scopingEntityId.valueSpecified = true;

        operationStream.operations = new Operation[] {operation};

        operationStreams[i] = operationStream;
      }

      BulkMutateJob bulkJob = null;

      bulkJob = new BulkMutateJob();
      bulkJob.numRequestParts = 1;
      bulkJob.numRequestPartsSpecified = true;

      // b. Create a part of the job.

      BulkMutateRequest bulkRequest1 = new BulkMutateRequest();
      bulkRequest1.partIndex = 0;
      bulkRequest1.partIndexSpecified = true;
      bulkRequest1.operationStreams = operationStreams;
      bulkJob.request = bulkRequest1;

      // c. Create job operation.
      JobOperation jobOperation1 = new JobOperation();
      jobOperation1.operatorSpecified = true;
      jobOperation1.@operator = Operator.ADD;
      jobOperation1.operand = bulkJob;

      bulkJob = bulkMutateJobService.mutate(jobOperation1);

      bool completed = false;

      while (completed == false) {
        Thread.Sleep(2000);

        BulkMutateJobSelector selector = new BulkMutateJobSelector();
        selector.jobIds = new long[] {bulkJob.id};

        BulkMutateJob[] allJobs = bulkMutateJobService.get(selector);
        if (allJobs != null && allJobs.Length > 0) {
          if (allJobs[0].status == BasicJobStatus.COMPLETED ||
              allJobs[0].status == BasicJobStatus.FAILED) {
            completed = true;
            bulkJob = allJobs[0];
            Assert.Pass();
            break;
          }
        }
      }
    }

    /// <summary>
    /// Create a language, country and network target for a given campaign.
    /// </summary>
    /// <param name="campaignId">Id of the campaign for which the targets should be created.</param>
    /// <returns>A list of targets.</returns>
    private TargetList[] CreateCampaignTargets(long campaignId) {
      // Create a language target - for English language.
      LanguageTarget languageTarget = new LanguageTarget();
      languageTarget.languageCode = "en";
      LanguageTargetList languageTargetList = new LanguageTargetList();
      languageTargetList.targets = new LanguageTarget[] {languageTarget};
      languageTargetList.campaignId = campaignId;
      languageTargetList.campaignIdSpecified = true;

      // Create a country target - include US, exclude metrocode 743.
      CountryTarget countryTarget = new CountryTarget();
      countryTarget.countryCode = "US";
      countryTarget.excludedSpecified = true;
      countryTarget.excluded = false;
      MetroTarget metroTarget = new MetroTarget();
      metroTarget.excludedSpecified = true;
      metroTarget.excluded = true;
      metroTarget.metroCode = "743";

      GeoTargetList geoTargetList = new GeoTargetList();
      geoTargetList.targets = new GeoTarget[] {countryTarget, metroTarget};
      geoTargetList.campaignId = campaignId;
      geoTargetList.campaignIdSpecified = true;

      // Create a network target - Google Search.
      NetworkTarget networkTarget1 = new NetworkTarget();
      networkTarget1.networkCoverageTypeSpecified = true;
      networkTarget1.networkCoverageType = NetworkCoverageType.GOOGLE_SEARCH;
      NetworkTarget networkTarget2 = new NetworkTarget();
      networkTarget2.networkCoverageTypeSpecified = true;
      networkTarget2.networkCoverageType = NetworkCoverageType.SEARCH_NETWORK;

      NetworkTargetList networkTargetList = new NetworkTargetList();
      networkTargetList.targets = new NetworkTarget[] {networkTarget1, networkTarget2};
      networkTargetList.campaignId = campaignId;
      networkTargetList.campaignIdSpecified = true;

      return new TargetList[] {languageTargetList, geoTargetList, networkTargetList};
    }
  }
}

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
using Google.Api.Ads.AdWords.Util.Reports;
using Google.Api.Ads.AdWords.v201601;
using Google.Api.Ads.Common.Util.Reports;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201601 {

  /// <summary>
  /// This code example runs a report for every advertiser account under a
  /// given manager account, using multiple parallel threads. This code example
  /// needs to be run against an AdWords manager account.
  /// </summary>
  public class ParallelReportDownload : ExampleBase {

    /// <summary>
    /// The maximum number of reports to download in parallel. This number should
    /// be less than or equal to <see cref="MAX_NUMBER_OF_THREADS"/>.
    /// </summary>
    private const int MAX_REPORT_DOWNLOADS_IN_PARALLEL = 3;

    /// <summary>
    /// The maximum number of threads to initialize for report downloads.
    /// Normally, you would set this to <see cref="MAX_REPORT_DOWNLOADS_IN_PARALLEL"/>.
    /// However, a more dynamic strategy involves changing
    /// MAX_REPORT_DOWNLOADS_IN_PARALLEL at runtime depending on the AdWords
    /// API server loads.
    /// </summary>
    private const int MAX_NUMBER_OF_THREADS = 10;

    /// <summary>
    /// Represents a report that was successfully downloaded.
    /// </summary>
    public class SuccessfulReportDownload {

      /// <summary>
      /// Gets or sets the customer ID for the report.
      /// </summary>
      public long CustomerId {
        get;
        set;
      }

      /// <summary>
      /// Gets or sets the path to which report was downloaded.
      /// </summary>
      public string Path {
        get;
        set;
      }
    }

    /// <summary>
    /// Represents a report download that failed.
    /// </summary>
    public class FailedReportDownload {

      /// <summary>
      /// Gets or sets the customer ID for the report.
      /// </summary>
      public long CustomerId {
        get;
        set;
      }

      /// <summary>
      /// Gets or sets the exception that was thrown..
      /// </summary>
      public AdWordsReportsException Exception {
        get;
        set;
      }
    }

    /// <summary>
    /// A data structure to hold data specific for a particular report download
    /// thread.
    /// </summary>
    public class ReportDownloadData {

      /// <summary>
      /// Gets or sets the application configuration.
      /// </summary>
      public AdWordsAppConfig Config {
        get;
        set;
      }

      /// <summary>
      /// Gets or sets the index of the thread that identifies it.
      /// </summary>
      public int ThreadIndex {
        get;
        set;
      }

      /// <summary>
      /// Gets or sets the folder to which reports are downloaded.
      /// </summary>
      public string DownloadFolder {
        get;
        set;
      }

      /// <summary>
      /// Gets or sets the event that signals the main thread that this thread
      /// is finished with its job.
      /// </summary>
      public ManualResetEvent SignalEvent {
        get;
        set;
      }

      /// <summary>
      /// Gets or sets the queue that holds the list of all customerIDs to be
      /// processed.
      /// </summary>
      public IProducerConsumerCollection<long> CustomerIdQueue {
        get;
        set;
      }

      /// <summary>
      /// Gets or sets the queue that holds the list of successful report
      /// downloads.
      /// </summary>
      public IProducerConsumerCollection<SuccessfulReportDownload> SuccessfulReports {
        get;
        set;
      }

      /// <summary>
      /// Gets or sets the queue that holds the list of failed report downloads.
      /// </summary>
      public IProducerConsumerCollection<FailedReportDownload> FailedReports {
        get;
        set;
      }

      /// <summary>
      /// Gets or sets the lock that ensures only a fixed number of report
      /// downloads happen simultaneously.
      /// </summary>
      public Semaphore QuotaLock {
        get;
        set;
      }

      /// <summary>
      /// The callback method for the report download thread.
      /// </summary>
      public void ThreadCallback(object arg) {
        string query = (string) arg;

        AdWordsUser user = new AdWordsUser(this.Config);

        while (true) {
          // Wait to acquire a lock on the quota lock.
          QuotaLock.WaitOne();

          // Try to get a customer ID from the queue.
          long customerId = 0;
          bool hasMoreCustomers = CustomerIdQueue.TryTake(out customerId);

          if (!hasMoreCustomers) {
            // Nothing more to do, break the loop.
            QuotaLock.Release();
            break;
          }
          try {
            ProcessCustomer(user, customerId, query);
          } finally {
            // Release the quota lock once we have downloaded the report for the
            // customer ID.
            QuotaLock.Release();
          }
        }
        // Mark the download as finished.
        this.SignalEvent.Set();
      }

      /// <summary>
      /// Processes the customer.
      /// </summary>
      /// <param name="user">The AdWords user.</param>
      /// <param name="customerId">The customer ID.</param>
      /// <param name="query">The report query.</param>
      private void ProcessCustomer(AdWordsUser user, long customerId, string query) {
        // Set the customer ID to the current customer.
        this.Config.ClientCustomerId = customerId.ToString();

        string downloadFile = string.Format("{0}{1}adgroup_{2:D10}.gz", this.DownloadFolder,
            Path.DirectorySeparatorChar, customerId);

        // Download the report.
        Console.WriteLine("[Thread #{0}]: Downloading report for customer: {1} into {2}...",
            this.ThreadIndex, customerId, downloadFile);

        try {
          ReportUtilities utilities = new ReportUtilities(user, "v201601", query,
              DownloadFormat.GZIPPED_CSV.ToString());
          using (ReportResponse response = utilities.GetResponse()) {
            response.Save(downloadFile);
          }

          // Mark this report download as success.
          SuccessfulReportDownload success = new SuccessfulReportDownload();
          success.CustomerId = customerId;
          success.Path = downloadFile;
          SuccessfulReports.TryAdd(success);

          Console.WriteLine("Report was downloaded to '{0}'.", downloadFile);
        } catch (AdWordsReportsException e) {
          // Mark this report download as failure.
          FailedReportDownload failure = new FailedReportDownload();
          failure.CustomerId = customerId;
          failure.Exception = e;
          FailedReports.TryAdd(failure);

          Console.WriteLine("Failed to download report for customer: {0}. Exception says {1}",
              customerId, e.Message);
        }
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      ParallelReportDownload codeExample = new ParallelReportDownload();
      Console.WriteLine(codeExample.Description);
      try {
        string fileName = "INSERT_FOLDER_NAME_HERE";
        codeExample.Run(new AdWordsUser(), fileName);
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
        return "This code example runs a report for every advertiser account under a " +
            "given manager account, using multiple parallel threads. This code example " +
            "needs to be run against an AdWords manager account.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="downloadFolder">The file to which the report is downloaded.
    /// </param>
    public void Run(AdWordsUser user, string downloadFolder) {
      // Increase the number of HTTP connections we can do in parallel.
      System.Net.ServicePointManager.DefaultConnectionLimit = 100;

      try {
        // Start the rate limiter with an initial value of zero, so that all
        // threads block immediately.
        Semaphore rateLimiter = new Semaphore(0, MAX_REPORT_DOWNLOADS_IN_PARALLEL);

        // Get all the advertiser accounts under this manager account.
        List<long> allCustomerIds = GetDescendantAdvertiserAccounts(user);

        // Create a concurrent queue of customers so that all threads can work
        // on the collection in parallel.
        ConcurrentQueue<long> customerQueue = new ConcurrentQueue<long>(allCustomerIds);

        // Create queues to keep track of successful and failed report downloads.
        ConcurrentQueue<SuccessfulReportDownload> reportsSucceeeded =
            new ConcurrentQueue<SuccessfulReportDownload>();
        ConcurrentQueue<FailedReportDownload> reportsFailed =
            new ConcurrentQueue<FailedReportDownload>();

        // Keep an array of events. This is used by the main thread to wait for
        // all worker threads to join.
        ManualResetEvent[] doneEvents = new ManualResetEvent[MAX_NUMBER_OF_THREADS];

        // The list of threads to download reports.
        Thread[] threads = new Thread[MAX_NUMBER_OF_THREADS];

        // The data for each thread.
        ReportDownloadData[] threadData = new ReportDownloadData[MAX_NUMBER_OF_THREADS];

        // The query to be run on each account.
        string query = "SELECT CampaignId, AdGroupId, Impressions, Clicks, Cost from " +
            "ADGROUP_PERFORMANCE_REPORT where AdGroupStatus IN [ENABLED, PAUSED] " +
            "DURING LAST_7_DAYS";

        // Initialize the threads and their data.
        for (int i = 0; i < MAX_NUMBER_OF_THREADS; i++) {
          doneEvents[i] = new ManualResetEvent(false);
          threadData[i] = new ReportDownloadData() {
            Config = (AdWordsAppConfig) (user.Config.Clone()),
            DownloadFolder = downloadFolder,
            SignalEvent = doneEvents[i],
            ThreadIndex = i,
            QuotaLock = rateLimiter,
            CustomerIdQueue = customerQueue,
            SuccessfulReports = reportsSucceeeded,
            FailedReports = reportsFailed
          };

          threads[i] = new Thread(threadData[i].ThreadCallback);
        }

        // Start the threads. Since the initial value of rate limiter is zero,
        // all threads will block immediately.
        for (int i = 0; i < threads.Length; i++) {
          threads[i].Start(query);
        }

        // Now reset the rate limiter so all threads can start downloading reports.
        rateLimiter.Release(MAX_REPORT_DOWNLOADS_IN_PARALLEL);

        // Wait for all threads in pool to complete.
        WaitHandle.WaitAll(doneEvents);
        Console.WriteLine("Download completed, results:");

        Console.WriteLine("Successful reports:");
        while (!reportsSucceeeded.IsEmpty) {
          SuccessfulReportDownload success = null;
          if (reportsSucceeeded.TryDequeue(out success)) {
            Console.WriteLine("Client ID: {0}, Path: {1}", success.CustomerId, success.Path);
          }
        }

        Console.WriteLine("Failed reports:");
        while (!reportsFailed.IsEmpty) {
          FailedReportDownload failure = null;
          if (reportsFailed.TryDequeue(out failure)) {
            Console.WriteLine("Client ID: {0}, Cause: {1}", failure.CustomerId,
                failure.Exception.Message);
          }
        }

        Console.WriteLine("All reports are downloaded.");
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to download reports.", e);
      }
    }

    /// <summary>
    /// Gets the list of all descendant advertiser accounts under the manager
    /// account.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <returns>A list of customer IDs for descendant advertiser accounts.</returns>
    public static List<long> GetDescendantAdvertiserAccounts(AdWordsUser user) {
      List<long> retval = new List<long>();

      // Get the ManagedCustomerService.
      ManagedCustomerService managedCustomerService = (ManagedCustomerService) user.GetService(
          AdWordsService.v201601.ManagedCustomerService);

      // Create selector.
      Selector selector = new Selector() {
        fields = new String[] {
            ManagedCustomer.Fields.CustomerId
        },
        predicates = new Predicate[] {
          // Select only advertiser accounts.
          Predicate.Equals(ManagedCustomer.Fields.CanManageClients, false.ToString())
        },
        paging = Paging.Default
      };

      ManagedCustomerPage page = null;
      try {
        do {
          page = managedCustomerService.get(selector);

          if (page.entries != null) {
            foreach (ManagedCustomer customer in page.entries) {
              retval.Add(customer.customerId);
            }
          }
          selector.paging.IncreaseOffset();
        } while (selector.paging.startIndex < page.totalNumEntries);
      } catch (Exception e) {
        Console.WriteLine("Failed to retrieve advertiser accounts under the manager account.");
        throw;
      }
      return retval;
    }
  }
}

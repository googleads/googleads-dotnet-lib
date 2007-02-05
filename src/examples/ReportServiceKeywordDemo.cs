/*
* Copyright (C) 2006 Google Inc.
* 
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
* 
*      http://www.apache.org/licenses/LICENSE-2.0
* 
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/
using System;
using System.Text;
using com.google.api.adwords.v8;
using com.google.api.adwords.lib;
using System.Threading;

namespace com.google.api.adwords.examples
{
	/**
	 * Schedules keyword report and retrieves report download url.
	 */
	class ReportServiceKeywordDemo
	{
		public static void run()
		{
			// Create a user (reads headers from app.config file)
			AdWordsUser user = new AdWordsUser();
			// Use sandbox
			user.useSandbox();
			// Get the services
			ReportService rs = (ReportService)user.getService("ReportService");

			// Create the report job we're going to send
			KeywordReportJob myReportJob = new KeywordReportJob();

			// Create an array of Campaign ids and put them in the report job.
			// If you don't set any campaign ids, the report includes all active keywords
			// in all your campaigns.
			// Uncomment if you want to limit the report to contain keywords in a
			// specific campaign
			// int [] campaignIds = {myCampaignId };
			// myReportJob.setCampaigns(campaignIds);

			// In this case, use the default settings for a keyword report.
			// The report will contain keywords that
			// -- have any matching type
			// -- can be shown in all situations (content pages and search results)
			// -- have any status

			// Set the aggregation period
			myReportJob.aggregationType = AggregationType.Daily;

			// Set the start and end date for the report
			myReportJob.endDay = DateTime.Today; // defaults to today
			myReportJob.startDay = new DateTime(2006, 1, 1);
			myReportJob.name = "Report1";

			// Submit the request for the report
			long myJobId = rs.scheduleReportJob(myReportJob);

			// Wait until the report has been generated
			ReportJobStatus mystatus = rs.getReportJobStatus(myJobId);

			while (mystatus != ReportJobStatus.Completed && mystatus != ReportJobStatus.Failed) 
			{
				Thread.Sleep(30000);
				mystatus = rs.getReportJobStatus(myJobId);
				Console.WriteLine("Report job status is " + mystatus);
			}

			if (mystatus == ReportJobStatus.Failed) 
			{
				Console.WriteLine("Job failed!");
			} 
			else 
			{
				// Report is ready; download it
				Console.WriteLine("The report is ready!");

				// Download the report
				String url = rs.getReportDownloadUrl(myJobId);
				Console.WriteLine("Download it at url {0}", url);

			}
			Console.ReadLine();
		}
	}
}
//
// Copyright (C) 2006 Google Inc.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

// Requires SharpZipLib library from http://www.icsharpcode.net/OpenSource/SharpZipLib/
using ICSharpCode.SharpZipLib.GZip;

using com.google.api.adwords.lib;
using com.google.api.adwords.v9;

namespace com.google.api.adwords.examples
{
	// Schedules and retrieves custom report.
	class ReportServiceCustomDemo
	{
		public static void run()
		{
			// Create a user (reads headers from App.config file).
			AdWordsUser user = new AdWordsUser();
			user.useSandbox();	// use sandbox

			// Get the service.
			ReportService service = 
				(ReportService) user.getService("ReportService");

			// Create the report job.
			CustomReportJob reportJob = new CustomReportJob();

			// Create an array of campaign ids and put them in the report 
			// job.  If you don't set any campaign ids, the report includes 
			// all active keywords in all of your campaigns.
			// int[] campaignIds = {myCampaignId};
			// myReportJob.setCampaigns(campaignIds);

			// In this case, use the default settings for a custom 
			// report.  The report will contain keywords that
			// - have any matching type
			// - can be shown in all situations (content pages and search 
			//   results)
			// - have any status

			// Set the aggregation period.
			reportJob.aggregationType = AggregationType.Daily;

			// Set the start and end dates for this report.
			reportJob.endDay = DateTime.Today;	// defaults to today
			reportJob.startDay = new DateTime(2007, 1, 1);
			
			// Custom reports lets you specify exactly what you want to 
			// retrive in the report.
			CustomReportOption[] customOptions = {
				CustomReportOption.AccountName,
				CustomReportOption.Campaign,
				CustomReportOption.CampaignId,
				CustomReportOption.AdGroup,
				CustomReportOption.AdGroupId,
				CustomReportOption.Keyword,
				CustomReportOption.KeywordId,
				CustomReportOption.Cpc,
				CustomReportOption.Ctr};
			reportJob.customOptions = customOptions;
			
			// Name this report job.
			reportJob.name = "Custom Report";

			// Schedule this report.
			long jobId = service.scheduleReportJob(reportJob);

			// Wait until the report has been generated.
			ReportJobStatus status = service.getReportJobStatus(jobId);
			while (
				status != ReportJobStatus.Completed && 
				status != ReportJobStatus.Failed)
			{
				Thread.Sleep(30000);
				status = service.getReportJobStatus(jobId);
				Console.WriteLine("Report job status is " + status);
			}

			if (status == ReportJobStatus.Failed) 
			{
				Console.WriteLine("Job failed!");
			} 
			else 
			{
				// Report is ready.
				Console.WriteLine("The report is ready!");

				// Download the report.
				String url = service.getGzipReportDownloadUrl(jobId);
				Console.WriteLine("Download it at url {0}", url);

				String fileName = "C:\\custom_report.xml";
				DownloadGZipReport(url, fileName);
				Console.WriteLine("Report is available at {0}", fileName);
			}

			Console.ReadLine();
		}

		// Downloads GZip report from a given url and saves it into a given
		// location.
		public static void DownloadGZipReport(String url, String location) 
		{
			Uri uri = new Uri(url);
			HttpWebRequest request = 
				(HttpWebRequest) WebRequest.CreateDefault(uri);
			request.KeepAlive = false;
			request.Method = "GET";
			request.Timeout = 60000;
			
			HttpWebResponse response = (HttpWebResponse) request.GetResponse();
			Stream receiveStream = response.GetResponseStream();

			Stream stream = new GZipInputStream(receiveStream);
			try 
			{
				FileStream fileStream = File.Create(location);
				try 
				{
					int size = 2048;
					byte[] writeData = new byte[2048];
					while (true)
					{
						size = stream.Read(writeData, 0, size);
						if (size > 0)
						{
							fileStream.Write(writeData, 0, size);
						}
						else
						{
							break;
						}
					}
				}
				finally
				{
					fileStream.Close();
				}
			}
			finally
			{
				stream.Close();
			}
		}
	}
}
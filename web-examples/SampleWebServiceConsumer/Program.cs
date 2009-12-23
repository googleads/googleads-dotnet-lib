// Copyright 2009, Google Inc. All Rights Reserved.
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

using com.google.api.adwords.websamples.remoteservice;

using System;
using System.Threading;

namespace com.google.api.adwords.websamples {
  /// <summary>
  /// This program shows how to consume a webservice that uses
  /// the AdWords API .NET client library to make calls to AdWords API.
  /// </summary>
  class Program {
    /// <summary>
    /// The main method of this class.
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    static void Main(string[] args) {
      Console.WriteLine("Creating campaigns synchronously...");
      CreateCampaignsSynchronously();
      Console.WriteLine("Creating campaigns asynchronously...");
      CreateCampaignsAsynchronously();
    }

    /// <summary>
    /// Shows how to create campaigns asynchronously.
    /// </summary>
    private static void CreateCampaignsAsynchronously() {
      Thread[] threads = new Thread[3];
      for (int i = 0; i < 3; i++) {
        threads[i] = new Thread(new ThreadStart(CreateCampaignCallback));
        threads[i].Start();
      }
      foreach (Thread thread in threads) {
        thread.Join();
      }
      return;
    }

    /// <summary>
    /// The thread callback method.
    /// </summary>
    static void CreateCampaignCallback() {
      SampleWebService service = new SampleWebService();
      Console.WriteLine("Created campaign - {0}", service.CreateCampaignRemotely());
    }

    /// <summary>
    /// Shows how to create campaigns synchronously.
    /// </summary>
    private static void CreateCampaignsSynchronously() {
      SampleWebService service = new SampleWebService();
      for (int i = 0; i < 3; i++) {
        Console.WriteLine("Created campaign - {0}", service.CreateCampaignRemotely());
      }
    }
  }
}

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

using Google.Api.Ads.Dfp.Lib;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using SamplePair = System.Collections.Generic.KeyValuePair<string,
    Google.Api.Ads.Dfp.Examples.SampleBase>;

namespace Google.Api.Ads.Dfp.Examples {
  /// <summary>
  /// The Main class for this application.
  /// </summary>
  class Program {
    /// <summary>
    /// A map to hold the code examples to be executed.
    /// </summary>
    static List<SamplePair> sampleMap = new List<SamplePair>();

    /// <summary>
    /// A flag to keep track of whether help message was shown earlier.
    /// </summary>
    private static bool helpShown = false;

    static void RegisterSample(string key, SampleBase value) {
      sampleMap.Add(new SamplePair(key, value));
    }
    /// <summary>
    /// Static constructor to initialize the code example map.
    /// </summary>
    static Program() {
      // Add v201004 code examples.
      RegisterSample("v201004.CreateCompanies", new v201004.CreateCompanies());
      RegisterSample("v201004.GetAllCompanies", new v201004.GetAllCompanies());
      RegisterSample("v201004.GetCompaniesByStatement", new v201004.GetCompaniesByStatement());
      RegisterSample("v201004.GetCompany", new v201004.GetCompany());
      RegisterSample("v201004.UpdateCompanies", new v201004.UpdateCompanies());
      RegisterSample("v201004.CreateCreatives", new v201004.CreateCreatives());
      RegisterSample("v201004.CopyImageCreatives", new v201004.CopyImageCreatives());
      RegisterSample("v201004.GetAllCreatives", new v201004.GetAllCreatives());
      RegisterSample("v201004.GetCreative", new v201004.GetCreative());
      RegisterSample("v201004.GetCreativesByStatement", new v201004.GetCreativesByStatement());
      RegisterSample("v201004.UpdateCreatives", new v201004.UpdateCreatives());
      RegisterSample("v201004.CreateAdUnits", new v201004.CreateAdUnits());
      RegisterSample("v201004.DeActivateAdUnits", new v201004.DeActivateAdUnits());
      RegisterSample("v201004.GetAdUnit", new v201004.GetAdUnit());
      RegisterSample("v201004.GetAdUnitsByStatement", new v201004.GetAdUnitsByStatement());
      RegisterSample("v201004.GetAllAdUnits", new v201004.GetAllAdUnits());
      RegisterSample("v201004.GetInventoryTree", new v201004.GetInventoryTree());
      RegisterSample("v201004.UpdateAdUnits", new v201004.UpdateAdUnits());
      RegisterSample("v201004.CreateLicas", new v201004.CreateLicas());
      RegisterSample("v201004.DeactivateLicas", new v201004.DeactivateLicas());
      RegisterSample("v201004.GetAllLicas", new v201004.GetAllLicas());
      RegisterSample("v201004.GetLica", new v201004.GetLica());
      RegisterSample("v201004.GetLicasByStatement", new v201004.GetLicasByStatement());
      RegisterSample("v201004.UpdateLicas", new v201004.UpdateLicas());
      RegisterSample("v201004.ActivateLineItem", new v201004.ActivateLineItem());
      RegisterSample("v201004.CreateLineItems", new v201004.CreateLineItems());
      RegisterSample("v201004.GetAllLineItems", new v201004.GetAllLineItems());
      RegisterSample("v201004.GetLineItem", new v201004.GetLineItem());
      RegisterSample("v201004.GetLineItemsByStatement", new v201004.GetLineItemsByStatement());
      RegisterSample("v201004.UpdateLineItems", new v201004.UpdateLineItems());
      RegisterSample("v201004.ApproveOrder", new v201004.ApproveOrder());
      RegisterSample("v201004.CreateOrders", new v201004.CreateOrders());
      RegisterSample("v201004.GetAllOrders", new v201004.GetAllOrders());
      RegisterSample("v201004.GetOrder", new v201004.GetOrder());
      RegisterSample("v201004.GetOrdersByStatement", new v201004.GetOrdersByStatement());
      RegisterSample("v201004.UpdateOrders", new v201004.UpdateOrders());
      RegisterSample("v201004.CreatePlacements", new v201004.CreatePlacements());
      RegisterSample("v201004.DeactivatePlacement", new v201004.DeactivatePlacement());
      RegisterSample("v201004.GetAllPlacements", new v201004.GetAllPlacements());
      RegisterSample("v201004.GetPlacement", new v201004.GetPlacement());
      RegisterSample("v201004.GetPlacementsByStatement", new v201004.GetPlacementsByStatement());
      RegisterSample("v201004.UpdatePlacements", new v201004.UpdatePlacements());
      RegisterSample("v201004.CreateUsers", new v201004.CreateUsers());
      RegisterSample("v201004.DeactivateUser", new v201004.DeactivateUser());
      RegisterSample("v201004.GetAllRoles", new v201004.GetAllRoles());
      RegisterSample("v201004.GetAllUsers", new v201004.GetAllUsers());
      RegisterSample("v201004.GetUser", new v201004.GetUser());
      RegisterSample("v201004.GetUsersByStatement", new v201004.GetUsersByStatement());
      RegisterSample("v201004.UpdateUsers", new v201004.UpdateUsers());
      RegisterSample("v201004.RunInventoryReport", new v201004.RunInventoryReport());
      RegisterSample("v201004.RunDeliveryReport", new v201004.RunDeliveryReport());
      RegisterSample("v201004.RunSalesReport", new v201004.RunSalesReport());
      RegisterSample("v201004.RunSalesReport", new v201004.DownloadReport());
      RegisterSample("v201004.GetForecast", new v201004.GetForecast());
      RegisterSample("v201004.GetForecastById", new v201004.GetForecastById());
      RegisterSample("v201004.GetAllNetworks", new v201004.GetAllNetworks());
      RegisterSample("v201004.GetCurrentNetwork", new v201004.GetCurrentNetwork());

      // Add v201010 code examples.
      RegisterSample("v201010.CreateCompanies", new v201010.CreateCompanies());
      RegisterSample("v201010.GetAllCompanies", new v201010.GetAllCompanies());
      RegisterSample("v201010.GetCompaniesByStatement", new v201010.GetCompaniesByStatement());
      RegisterSample("v201010.GetCompany", new v201010.GetCompany());
      RegisterSample("v201010.UpdateCompanies", new v201010.UpdateCompanies());
      RegisterSample("v201010.CreateCreatives", new v201010.CreateCreatives());
      RegisterSample("v201010.CopyImageCreatives", new v201010.CopyImageCreatives());
      RegisterSample("v201010.GetAllCreatives", new v201010.GetAllCreatives());
      RegisterSample("v201010.GetCreative", new v201010.GetCreative());
      RegisterSample("v201010.GetCreativesByStatement", new v201010.GetCreativesByStatement());
      RegisterSample("v201010.UpdateCreatives", new v201010.UpdateCreatives());
      RegisterSample("v201010.CreateAdUnits", new v201010.CreateAdUnits());
      RegisterSample("v201010.DeActivateAdUnits", new v201010.DeActivateAdUnits());
      RegisterSample("v201010.GetAdUnit", new v201010.GetAdUnit());
      RegisterSample("v201010.GetAdUnitsByStatement", new v201010.GetAdUnitsByStatement());
      RegisterSample("v201010.GetAllAdUnits", new v201010.GetAllAdUnits());
      RegisterSample("v201010.GetInventoryTree", new v201010.GetInventoryTree());
      RegisterSample("v201010.UpdateAdUnits", new v201010.UpdateAdUnits());
      RegisterSample("v201010.CreateLicas", new v201010.CreateLicas());
      RegisterSample("v201010.DeactivateLicas", new v201010.DeactivateLicas());
      RegisterSample("v201010.GetAllLicas", new v201010.GetAllLicas());
      RegisterSample("v201010.GetLica", new v201010.GetLica());
      RegisterSample("v201010.GetLicasByStatement", new v201010.GetLicasByStatement());
      RegisterSample("v201010.UpdateLicas", new v201010.UpdateLicas());
      RegisterSample("v201010.ActivateLineItem", new v201010.ActivateLineItem());
      RegisterSample("v201010.CreateLineItems", new v201010.CreateLineItems());
      RegisterSample("v201010.GetAllLineItems", new v201010.GetAllLineItems());
      RegisterSample("v201010.GetLineItem", new v201010.GetLineItem());
      RegisterSample("v201010.GetLineItemsByStatement", new v201010.GetLineItemsByStatement());
      RegisterSample("v201010.UpdateLineItems", new v201010.UpdateLineItems());
      RegisterSample("v201010.ApproveOrder", new v201010.ApproveOrder());
      RegisterSample("v201010.CreateOrders", new v201010.CreateOrders());
      RegisterSample("v201010.GetAllOrders", new v201010.GetAllOrders());
      RegisterSample("v201010.GetOrder", new v201010.GetOrder());
      RegisterSample("v201010.GetOrdersByStatement", new v201010.GetOrdersByStatement());
      RegisterSample("v201010.UpdateOrders", new v201010.UpdateOrders());
      RegisterSample("v201010.CreatePlacements", new v201010.CreatePlacements());
      RegisterSample("v201010.DeactivatePlacement", new v201010.DeactivatePlacement());
      RegisterSample("v201010.GetAllPlacements", new v201010.GetAllPlacements());
      RegisterSample("v201010.GetPlacement", new v201010.GetPlacement());
      RegisterSample("v201010.GetPlacementsByStatement", new v201010.GetPlacementsByStatement());
      RegisterSample("v201010.UpdatePlacements", new v201010.UpdatePlacements());
      RegisterSample("v201010.CreateUsers", new v201010.CreateUsers());
      RegisterSample("v201010.DeactivateUser", new v201010.DeactivateUser());
      RegisterSample("v201010.GetAllRoles", new v201010.GetAllRoles());
      RegisterSample("v201010.GetAllUsers", new v201010.GetAllUsers());
      RegisterSample("v201010.GetUser", new v201010.GetUser());
      RegisterSample("v201010.GetUsersByStatement", new v201010.GetUsersByStatement());
      RegisterSample("v201010.UpdateUsers", new v201010.UpdateUsers());
      RegisterSample("v201010.RunInventoryReport", new v201010.RunInventoryReport());
      RegisterSample("v201010.RunDeliveryReport", new v201010.RunDeliveryReport());
      RegisterSample("v201010.RunSalesReport", new v201010.RunSalesReport());
      RegisterSample("v201010.RunSalesReport", new v201010.DownloadReport());
      RegisterSample("v201010.GetForecast", new v201010.GetForecast());
      RegisterSample("v201010.GetForecastById", new v201010.GetForecastById());
      RegisterSample("v201010.GetAllNetworks", new v201010.GetAllNetworks());
      RegisterSample("v201010.GetCurrentNetwork", new v201010.GetCurrentNetwork());
    }

    /// <summary>
    /// The main method.
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args) {
      DfpUser user = new DfpUser();

      if (string.Compare(args[0], "--all", true) == 0) {
        foreach (SamplePair pair in sampleMap) {
          SampleBase sample = pair.Value;
          Console.WriteLine(sample.Description);
          sample.Run(user);
          Console.WriteLine("Press [Enter] to continue");
          Console.ReadLine();
        }
      } else if (string.Compare(args[0], "--v201004all", true) == 0) {
        List<SamplePair> matchingItems = sampleMap.FindAll(delegate(SamplePair pair) {
          return pair.Key.StartsWith("v201004");
        });
        foreach (SamplePair matchingItem in matchingItems) {
          SampleBase sample = matchingItem.Value;
          Console.WriteLine(sample.Description);
          sample.Run(user);
          Console.WriteLine("Press [Enter] to continue");
          Console.ReadLine();
        }
      } else if (string.Compare(args[0], "--v201010all", true) == 0) {
        List<SamplePair> matchingItems = sampleMap.FindAll(delegate(SamplePair pair) {
          return pair.Key.StartsWith("v201010");
        });
        foreach (SamplePair matchingItem in matchingItems) {
          SampleBase sample = matchingItem.Value;
          Console.WriteLine(sample.Description);
          sample.Run(user);
          Console.WriteLine("Press [Enter] to continue");
          Console.ReadLine();
        }
      } else {
        foreach (string cmdArgs in args) {
          SamplePair matchingPair = sampleMap.Find(delegate(SamplePair pair) {
            return string.Compare(pair.Key, cmdArgs, true) == 0;
          });

          if (matchingPair.Key != null) {
            SampleBase sample = matchingPair.Value;
            Console.WriteLine(sample.Description);
            sample.Run(user);
            Console.WriteLine("Press [Enter] to continue");
            Console.ReadLine();
          } else {
            ShowUsage();
          }
        }
      }
    }
    /// <summary>
    /// Prints program usage message.
    /// </summary>
    private static void ShowUsage() {
      if (helpShown) {
        return;
      } else {
        helpShown = true;
      }
      string exeName = Path.GetFileName(Assembly.GetExecutingAssembly().Location);
      Console.WriteLine("Runs Dfp API code examples");
      Console.WriteLine("Usage : {0} [flags]\n", exeName);
      Console.WriteLine("Available flags\n");
      Console.WriteLine("--help\t\t : Prints this help message.", exeName);
      Console.WriteLine("--all\t\t : Run all code examples.", exeName);
      Console.WriteLine("name1 [name2 ...] : " +
          "Run specific code examples. Names can be one of the following:\n", exeName);
      foreach (SamplePair pair in sampleMap) {
        Console.WriteLine("{0} : {1}", pair.Key, pair.Value.Description);
      }
      Console.WriteLine("Press [Enter] to continue");
      Console.ReadLine();
    }
  }
}

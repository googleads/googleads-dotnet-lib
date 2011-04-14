// Copyright 2011, Google Inc. All Rights Reserved.
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
      RegisterSample("v201004.ActivateLicas", new v201004.ActivateLicas());
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
      RegisterSample("v201010.ActivateLicas", new v201010.ActivateLicas());
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

      // Add v201101 code examples.
      RegisterSample("v201101.CreateCompanies", new v201101.CreateCompanies());
      RegisterSample("v201101.GetAllCompanies", new v201101.GetAllCompanies());
      RegisterSample("v201101.GetCompaniesByStatement", new v201101.GetCompaniesByStatement());
      RegisterSample("v201101.GetCompany", new v201101.GetCompany());
      RegisterSample("v201101.UpdateCompanies", new v201101.UpdateCompanies());
      RegisterSample("v201101.CreateCreatives", new v201101.CreateCreatives());
      RegisterSample("v201101.CopyImageCreatives", new v201101.CopyImageCreatives());
      RegisterSample("v201101.GetAllCreatives", new v201101.GetAllCreatives());
      RegisterSample("v201101.GetCreative", new v201101.GetCreative());
      RegisterSample("v201101.GetCreativesByStatement", new v201101.GetCreativesByStatement());
      RegisterSample("v201101.UpdateCreatives", new v201101.UpdateCreatives());
      RegisterSample("v201101.CreateAdUnits", new v201101.CreateAdUnits());
      RegisterSample("v201101.DeActivateAdUnits", new v201101.DeActivateAdUnits());
      RegisterSample("v201101.GetAdUnit", new v201101.GetAdUnit());
      RegisterSample("v201101.GetAdUnitsByStatement", new v201101.GetAdUnitsByStatement());
      RegisterSample("v201101.GetAllAdUnits", new v201101.GetAllAdUnits());
      RegisterSample("v201101.GetInventoryTree", new v201101.GetInventoryTree());
      RegisterSample("v201101.UpdateAdUnits", new v201101.UpdateAdUnits());
      RegisterSample("v201101.CreateLicas", new v201101.CreateLicas());
      RegisterSample("v201101.ActivateLicas", new v201101.ActivateLicas());
      RegisterSample("v201101.DeactivateLicas", new v201101.DeactivateLicas());
      RegisterSample("v201101.GetAllLicas", new v201101.GetAllLicas());
      RegisterSample("v201101.GetLica", new v201101.GetLica());
      RegisterSample("v201101.GetLicasByStatement", new v201101.GetLicasByStatement());
      RegisterSample("v201101.UpdateLicas", new v201101.UpdateLicas());
      RegisterSample("v201101.ActivateLineItem", new v201101.ActivateLineItem());
      RegisterSample("v201101.CreateLineItems", new v201101.CreateLineItems());
      RegisterSample("v201101.GetAllLineItems", new v201101.GetAllLineItems());
      RegisterSample("v201101.GetLineItem", new v201101.GetLineItem());
      RegisterSample("v201101.GetLineItemsByStatement", new v201101.GetLineItemsByStatement());
      RegisterSample("v201101.UpdateLineItems", new v201101.UpdateLineItems());
      RegisterSample("v201101.ApproveOrder", new v201101.ApproveOrder());
      RegisterSample("v201101.CreateOrders", new v201101.CreateOrders());
      RegisterSample("v201101.GetAllOrders", new v201101.GetAllOrders());
      RegisterSample("v201101.GetOrder", new v201101.GetOrder());
      RegisterSample("v201101.GetOrdersByStatement", new v201101.GetOrdersByStatement());
      RegisterSample("v201101.UpdateOrders", new v201101.UpdateOrders());
      RegisterSample("v201101.CreatePlacements", new v201101.CreatePlacements());
      RegisterSample("v201101.DeactivatePlacement", new v201101.DeactivatePlacement());
      RegisterSample("v201101.GetAllPlacements", new v201101.GetAllPlacements());
      RegisterSample("v201101.GetPlacement", new v201101.GetPlacement());
      RegisterSample("v201101.GetPlacementsByStatement", new v201101.GetPlacementsByStatement());
      RegisterSample("v201101.UpdatePlacements", new v201101.UpdatePlacements());
      RegisterSample("v201101.CreateUsers", new v201101.CreateUsers());
      RegisterSample("v201101.DeactivateUser", new v201101.DeactivateUser());
      RegisterSample("v201101.GetAllRoles", new v201101.GetAllRoles());
      RegisterSample("v201101.GetAllUsers", new v201101.GetAllUsers());
      RegisterSample("v201101.GetUser", new v201101.GetUser());
      RegisterSample("v201101.GetUsersByStatement", new v201101.GetUsersByStatement());
      RegisterSample("v201101.UpdateUsers", new v201101.UpdateUsers());
      RegisterSample("v201101.RunInventoryReport", new v201101.RunInventoryReport());
      RegisterSample("v201101.RunDeliveryReport", new v201101.RunDeliveryReport());
      RegisterSample("v201101.RunSalesReport", new v201101.RunSalesReport());
      RegisterSample("v201101.RunSalesReport", new v201101.DownloadReport());
      RegisterSample("v201101.GetForecast", new v201101.GetForecast());
      RegisterSample("v201101.GetForecastById", new v201101.GetForecastById());
      RegisterSample("v201101.GetAllNetworks", new v201101.GetAllNetworks());
      RegisterSample("v201101.GetCurrentNetwork", new v201101.GetCurrentNetwork());

      RegisterSample("v201101.CreateCustomTargetingKeysAndValues",
          new v201101.CreateCustomTargetingKeysAndValues());
      RegisterSample("v201101.GetAllCustomTargetingKeysAndValues",
          new v201101.GetAllCustomTargetingKeysAndValues());
      RegisterSample("v201101.GetCustomTargetingKeysByStatement",
          new v201101.GetCustomTargetingKeysByStatement());
      RegisterSample("v201101.GetCustomTargetingValuesByStatement",
          new v201101.GetCustomTargetingValuesByStatement());
      RegisterSample("v201101.UpdateCustomTargetingKeys", new v201101.UpdateCustomTargetingKeys());
      RegisterSample("v201101.UpdateCustomTargetingValues",
          new v201101.UpdateCustomTargetingValues());
      RegisterSample("v201101.DeleteCustomTargetingValues",
          new v201101.DeleteCustomTargetingValues());
      RegisterSample("v201101.DeleteCustomTargetingKeys", new v201101.DeleteCustomTargetingKeys());
      RegisterSample("v201101.TargetCustomCriteria", new v201101.TargetCustomCriteria());

      RegisterSample("v201101.GetAllCities", new v201101.GetAllCities());
      RegisterSample("v201101.GetAllMetros", new v201101.GetAllMetros());
      RegisterSample("v201101.GetAllRegions", new v201101.GetAllRegions());
      RegisterSample("v201101.GetAllCountries", new v201101.GetAllCountries());

      // Add v201103 code examples.
      RegisterSample("v201103.CreateCompanies", new v201103.CreateCompanies());
      RegisterSample("v201103.GetAllCompanies", new v201103.GetAllCompanies());
      RegisterSample("v201103.GetCompaniesByStatement", new v201103.GetCompaniesByStatement());
      RegisterSample("v201103.GetCompany", new v201103.GetCompany());
      RegisterSample("v201103.UpdateCompanies", new v201103.UpdateCompanies());
      RegisterSample("v201103.CreateCreatives", new v201103.CreateCreatives());
      RegisterSample("v201103.CopyImageCreatives", new v201103.CopyImageCreatives());
      RegisterSample("v201103.GetAllCreatives", new v201103.GetAllCreatives());
      RegisterSample("v201103.GetCreative", new v201103.GetCreative());
      RegisterSample("v201103.GetCreativesByStatement", new v201103.GetCreativesByStatement());
      RegisterSample("v201103.UpdateCreatives", new v201103.UpdateCreatives());
      RegisterSample("v201103.CreateAdUnits", new v201103.CreateAdUnits());
      RegisterSample("v201103.DeActivateAdUnits", new v201103.DeActivateAdUnits());
      RegisterSample("v201103.GetAdUnit", new v201103.GetAdUnit());
      RegisterSample("v201103.GetAdUnitsByStatement", new v201103.GetAdUnitsByStatement());
      RegisterSample("v201103.GetAllAdUnits", new v201103.GetAllAdUnits());
      RegisterSample("v201103.GetInventoryTree", new v201103.GetInventoryTree());
      RegisterSample("v201103.UpdateAdUnits", new v201103.UpdateAdUnits());
      RegisterSample("v201103.CreateLicas", new v201103.CreateLicas());
      RegisterSample("v201103.ActivateLicas", new v201103.ActivateLicas());
      RegisterSample("v201103.DeactivateLicas", new v201103.DeactivateLicas());
      RegisterSample("v201103.GetAllLicas", new v201103.GetAllLicas());
      RegisterSample("v201103.GetLica", new v201103.GetLica());
      RegisterSample("v201103.GetLicasByStatement", new v201103.GetLicasByStatement());
      RegisterSample("v201103.UpdateLicas", new v201103.UpdateLicas());
      RegisterSample("v201103.ActivateLineItem", new v201103.ActivateLineItem());
      RegisterSample("v201103.CreateLineItems", new v201103.CreateLineItems());
      RegisterSample("v201103.GetAllLineItems", new v201103.GetAllLineItems());
      RegisterSample("v201103.GetLineItem", new v201103.GetLineItem());
      RegisterSample("v201103.GetLineItemsByStatement", new v201103.GetLineItemsByStatement());
      RegisterSample("v201103.UpdateLineItems", new v201103.UpdateLineItems());
      RegisterSample("v201103.ApproveOrder", new v201103.ApproveOrder());
      RegisterSample("v201103.CreateOrders", new v201103.CreateOrders());
      RegisterSample("v201103.GetAllOrders", new v201103.GetAllOrders());
      RegisterSample("v201103.GetOrder", new v201103.GetOrder());
      RegisterSample("v201103.GetOrdersByStatement", new v201103.GetOrdersByStatement());
      RegisterSample("v201103.UpdateOrders", new v201103.UpdateOrders());
      RegisterSample("v201103.CreatePlacements", new v201103.CreatePlacements());
      RegisterSample("v201103.DeactivatePlacement", new v201103.DeactivatePlacement());
      RegisterSample("v201103.GetAllPlacements", new v201103.GetAllPlacements());
      RegisterSample("v201103.GetPlacement", new v201103.GetPlacement());
      RegisterSample("v201103.GetPlacementsByStatement", new v201103.GetPlacementsByStatement());
      RegisterSample("v201103.UpdatePlacements", new v201103.UpdatePlacements());
      RegisterSample("v201103.CreateUsers", new v201103.CreateUsers());
      RegisterSample("v201103.DeactivateUser", new v201103.DeactivateUser());
      RegisterSample("v201103.GetAllRoles", new v201103.GetAllRoles());
      RegisterSample("v201103.GetAllUsers", new v201103.GetAllUsers());
      RegisterSample("v201103.GetUser", new v201103.GetUser());
      RegisterSample("v201103.GetUsersByStatement", new v201103.GetUsersByStatement());
      RegisterSample("v201103.UpdateUsers", new v201103.UpdateUsers());
      RegisterSample("v201103.RunInventoryReport", new v201103.RunInventoryReport());
      RegisterSample("v201103.RunDeliveryReport", new v201103.RunDeliveryReport());
      RegisterSample("v201103.RunSalesReport", new v201103.RunSalesReport());
      RegisterSample("v201103.RunSalesReport", new v201103.DownloadReport());
      RegisterSample("v201103.GetForecast", new v201103.GetForecast());
      RegisterSample("v201103.GetForecastById", new v201103.GetForecastById());
      RegisterSample("v201103.GetAllNetworks", new v201103.GetAllNetworks());
      RegisterSample("v201103.GetCurrentNetwork", new v201103.GetCurrentNetwork());

      RegisterSample("v201103.CreateCustomTargetingKeysAndValues",
          new v201103.CreateCustomTargetingKeysAndValues());
      RegisterSample("v201103.GetAllCustomTargetingKeysAndValues",
          new v201103.GetAllCustomTargetingKeysAndValues());
      RegisterSample("v201103.GetCustomTargetingKeysByStatement",
          new v201103.GetCustomTargetingKeysByStatement());
      RegisterSample("v201103.GetCustomTargetingValuesByStatement",
          new v201103.GetCustomTargetingValuesByStatement());
      RegisterSample("v201103.UpdateCustomTargetingKeys", new v201103.UpdateCustomTargetingKeys());
      RegisterSample("v201103.UpdateCustomTargetingValues",
          new v201103.UpdateCustomTargetingValues());
      RegisterSample("v201103.TargetCustomCriteria", new v201103.TargetCustomCriteria());

      RegisterSample("v201103.GetAllCities", new v201103.GetAllCities());
      RegisterSample("v201103.GetAllMetros", new v201103.GetAllMetros());
      RegisterSample("v201103.GetAllRegions", new v201103.GetAllRegions());
      RegisterSample("v201103.GetAllCountries", new v201103.GetAllCountries());
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
      } else if (string.Compare(args[0], "--v201101all", true) == 0) {
        List<SamplePair> matchingItems = sampleMap.FindAll(delegate(SamplePair pair) {
          return pair.Key.StartsWith("v201101");
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

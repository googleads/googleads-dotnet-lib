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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.AdWords.Util.ApiCodes;
using Google.Api.Ads.AdWords.v13;
using Google.Api.Ads.AdWords.v200909;

using System;
using System.Collections;
using System.Collections.Generic;

namespace Google.Api.Ads.AdWords.Util.Units {
  /// <summary>
  /// Defines utility functions for the client library that use InfoService.
  /// </summary>
  public static class UnitsUtilities {
    /// <summary>
    /// Gets the quota usage of an account in units, broken down by
    /// method name.
    /// </summary>
    /// <param name="user">The AdWordsUser object for which the quota usage
    /// should be retrieved.</param>
    /// <param name="startDate">Start date for the date range for which
    /// results are to be retrieved.</param>
    /// <param name="endDate">End date for the date range for which results
    /// are to be retrieved.</param>
    /// <returns>A list of MethodQuotaUsage objects, with one entry for each
    /// method.</returns>
    public static List<MethodQuotaUsage> GetMethodQuotaUsage(AdWordsUser user, DateTime startDate,
        DateTime endDate) {
      List<MethodQuotaUsage> methodQuotaUsageList = new List<MethodQuotaUsage>();
      SortedList<string, List<string>> serviceToMethodsMap = GetAllMethods();
      InfoService service = (InfoService) user.GetService(AdWordsService.v200909.InfoService);

      foreach (string serviceName in serviceToMethodsMap.Keys) {
        List<string> methods = serviceToMethodsMap[serviceName];

        foreach (string methodName in methods) {
          InfoSelector selector = new InfoSelector();
          selector.apiUsageTypeSpecified = true;
          selector.apiUsageType = ApiUsageType.UNIT_COUNT;
          selector.dateRange = new DateRange();
          selector.dateRange.min = startDate.ToString("YYYYMMDD");
          selector.dateRange.max = endDate.ToString("YYYYMMDD");
          selector.serviceName = serviceName;
          if (methodName.Contains(".")) {
            string[] splits = methodName.Split('.');
            selector.methodName = splits[0];
            selector.operatorSpecified = true;
            selector.@operator = (Operator) Enum.Parse(typeof(Operator), splits[1]);
          } else {
            selector.methodName = methodName;
          }

          methodQuotaUsageList.Add(new MethodQuotaUsage(serviceName, methodName,
              service.get(selector).cost));
        }
      }
      return methodQuotaUsageList;
    }

    /// <summary>
    /// Gets the quota usage for client accounts.
    /// </summary>
    /// <param name="user">The AdWordsUser object for which the quota usage
    /// should be retrieved.</param>
    /// <param name="startDate">Start date for the date range for which
    /// results are to be retrieved.</param>
    /// <param name="endDate">End date for the date range for which results are
    /// to be retrieved.</param>
    /// <returns>A ClientQuotaUsage object that stores the API usage breakup.
    /// </returns>
    public static ClientQuotaUsage GetClientQuotaUsage(AdWordsUser user, DateTime startDate,
        DateTime endDate) {
      ClientQuotaUsage retVal = new ClientQuotaUsage();
      AccountService accountService =
          (AccountService) user.GetService(AdWordsService.v13.AccountService);
      AdWordsAccount rootUser = new AdWordsAccount();
      rootUser.Email = accountService.emailValue.Value[0];
      rootUser.IsManager = true;
      Hashtable allUsers = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
      BuildUserGraph(accountService, rootUser, allUsers);

      InfoService infoService = (InfoService) user.GetService(AdWordsService.v200909.InfoService);
      FetchUnitUsages(infoService, rootUser, startDate, endDate);

      foreach (string email in allUsers.Keys) {
        retVal.UsageMap[email] = GetUnits((AdWordsAccount)allUsers[email], allUsers);
      }

      InfoSelector selector = new InfoSelector();
      selector.apiUsageTypeSpecified = true;
      selector.apiUsageType = ApiUsageType.UNIT_COUNT;
      selector.dateRange = new DateRange();
      selector.dateRange.min = startDate.ToString("YYYYMMDD");
      selector.dateRange.max = endDate.ToString("YYYYMMDD");

      retVal.TotalUnits = infoService.get(selector).cost;
      retVal.DiffUnits = retVal.TotalUnits - retVal.UsageMap[rootUser.Email];
      return retVal;
    }

    /// <summary>
    /// Gets a list of all methods supported by AdWords API v13.
    /// </summary>
    /// <returns>A sorted map, with key as service name and value as an
    /// array of method names.</returns>
    private static SortedList<string, List<string>> GetAllMethods() {
      SortedList<string, List<string>> serviceToMethodsMap = new SortedList<string, List<string>>();

      foreach (ApiMethod method in CodeUtilities.GetAllMethods()) {
        if (!serviceToMethodsMap.ContainsKey(method.ServiceName)) {
          serviceToMethodsMap.Add(method.ServiceName, new List<string>());
        }
        serviceToMethodsMap[method.ServiceName].Add(method.MethodName);
      }
      return serviceToMethodsMap;
    }

    /// <summary>
    /// Gets the API units for an account.
    /// </summary>
    /// <param name="account">The account whose units to retrieve units for.</param>
    /// <param name="allUsers">The list of all user accounts visited so far.</param>
    /// <returns>The number of API units for an account.</returns>
    private static long GetUnits(AdWordsAccount account, Hashtable allUsers) {
      foreach (AdWordsAccount user in allUsers.Values) {
        user.Visited = false;
      }
      return RollupUnits(account);
    }

    /// <summary>
    /// Rolls up the API unit usage of an account hierarchy to a
    /// particular account.
    /// </summary>
    /// <param name="account">The account to which the rollup should be done.
    /// </param>
    /// <returns>The rolled up API units.</returns>
    private static long RollupUnits(AdWordsAccount account) {
      long retVal = 0;
      if (account.Visited == false) {
        retVal = account.Units;
        if (account.IsManager == true) {
          foreach (AdWordsAccount child in account.Children) {
            retVal += RollupUnits(child);
          }
        }
        account.Visited = true;
      }
      return retVal;
    }

    /// <summary>
    /// Builds a graph of user accounts, and returns the account root.
    /// </summary>
    /// <param name="accountService">An instance of AccountService to be
    /// used by this method.</param>
    /// <param name="account">The account to be traversed.</param>
    /// <param name="allUsers">A table of all accounts traversed so far.
    /// </param>
    /// <returns>The root account, with parent and children lists populated
    /// as per account account hierarchy.</returns>
    private static AdWordsAccount BuildUserGraph(AccountService accountService,
        AdWordsAccount account, Hashtable allUsers) {
      clientEmail oldClientEmail = accountService.clientEmailValue;

      accountService.clientEmailValue = new clientEmail();
      accountService.clientEmailValue.Value = new string[] {account.Email};

      if (allUsers.ContainsKey(account.Email) == false) {
        allUsers.Add(account.Email, account);
      }

      ClientAccountInfo[] clients = accountService.getClientAccountInfos();

      if (clients != null) {
        Array.Sort<ClientAccountInfo>(clients, delegate(ClientAccountInfo x, ClientAccountInfo y) {
          if (x.isCustomerManager == y.isCustomerManager) {
            return 0;
          }
          return (x.isCustomerManager == false && y.isCustomerManager == true) ? 1 : -1;
        });

        foreach (ClientAccountInfo client in clients) {
          AdWordsAccount child = null;
          if (allUsers.ContainsKey(client.emailAddress) == true) {
            child = (AdWordsAccount) allUsers[client.emailAddress];
          } else {
            child = new AdWordsAccount();
            child.Email = client.emailAddress;
            child.IsManager = client.isCustomerManager;
          }
          child.Parents.Add(account);
          account.Children.Add(child);

          BuildUserGraph(accountService, child, allUsers);
        }
      }
      accountService.clientEmailValue = oldClientEmail;
      return account;
    }

    /// <summary>
    /// Recursively walks the account graph hierarchy and fetches the unit
    /// usages for each account.
    /// </summary>
    /// <param name="infoService">InfoService instance to be used while making
    /// calls.</param>
    /// <param name="account">The account to be traversed recursively.</param>
    /// <param name="startDate">Start date for fetching API usage.</param>
    /// <param name="endDate">End date for fetching API usage.</param>
    private static void FetchUnitUsages(InfoService infoService, AdWordsAccount account,
        DateTime startDate, DateTime endDate) {
      string oldClientEmail = infoService.RequestHeader.clientEmail;
      startDate = new DateTime(2009, 1, 1);
      infoService.RequestHeader.clientEmail = account.Email;

      InfoSelector selector = new InfoSelector();
      selector.apiUsageTypeSpecified = true;
      selector.apiUsageType = ApiUsageType.UNIT_COUNT_FOR_CLIENTS;
      selector.dateRange = new DateRange();
      selector.dateRange.min = startDate.ToString("yyyyMMdd");
      selector.dateRange.max = endDate.ToString("yyyyMMdd");

      ApiUsageInfo usageInfo = infoService.get(selector);

      foreach (AdWordsAccount child in account.Children) {
        if (usageInfo.apiUsageRecords != null) {
          foreach (ApiUsageRecord usageRecord in usageInfo.apiUsageRecords) {
            if (child.Email == usageRecord.clientEmail) {
              child.Units = usageRecord.cost;
              break;
            }
          }
        }

        if (child.IsManager) {
          FetchUnitUsages(infoService, child, startDate, endDate);
        }
      }
      infoService.RequestHeader.clientEmail = oldClientEmail;
    }
  }
}

// Copyright 2014, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201406;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201406 {
  /// <summary>
  /// This code example illustrates how to retrieve the account hierarchy under
  /// an account. This code example won't work with Test Accounts. See
  /// https://developers.google.com/adwords/api/docs/test-accounts
  ///
  /// Tags: ManagedCustomerService.get
  /// </summary>
  public class GetAccountHierarchy : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      GetAccountHierarchy codeExample = new GetAccountHierarchy();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new AdWordsUser());
      } catch (Exception ex) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(ex));
      }
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example illustrates how to retrieve the account hierarchy under" +
            " an account. This code example won't work with Test Accounts. See " +
            "https://developers.google.com/adwords/api/docs/test-accounts";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    public void Run(AdWordsUser user) {
      // Get the ManagedCustomerService.
      ManagedCustomerService managedCustomerService = (ManagedCustomerService) user.GetService(
          AdWordsService.v201406.ManagedCustomerService);
      managedCustomerService.RequestHeader.clientCustomerId = null;

      // Create selector.
      Selector selector = new Selector();
      selector.fields = new String[] {"Login", "CustomerId", "Name"};

      try {
        // Get results.
        ManagedCustomerPage page = managedCustomerService.get(selector);

        // Display serviced account graph.
        if (page.entries != null) {
          // Create map from customerId to customer node.
          Dictionary<long, ManagedCustomerTreeNode> customerIdToCustomerNode =
              new Dictionary<long, ManagedCustomerTreeNode>();

          // Create account tree nodes for each customer.
          foreach (ManagedCustomer customer in page.entries) {
            ManagedCustomerTreeNode node = new ManagedCustomerTreeNode();
            node.Account = customer;
            customerIdToCustomerNode.Add(customer.customerId, node);
          }

          // For each link, connect nodes in tree.
          if (page.links != null) {
            foreach (ManagedCustomerLink link in page.links) {
              ManagedCustomerTreeNode managerNode =
                  customerIdToCustomerNode[link.managerCustomerId];
              ManagedCustomerTreeNode childNode = customerIdToCustomerNode[link.clientCustomerId];
              childNode.ParentNode = managerNode;
              if (managerNode != null) {
                managerNode.ChildAccounts.Add(childNode);
              }
            }
          }

          // Find the root account node in the tree.
          ManagedCustomerTreeNode rootNode = null;
          foreach (ManagedCustomer account in page.entries) {
            if (customerIdToCustomerNode[account.customerId].ParentNode == null) {
              rootNode = customerIdToCustomerNode[account.customerId];
              break;
            }
          }

          // Display account tree.
          Console.WriteLine("Login, CustomerId, Name");
          Console.WriteLine(rootNode.ToTreeString(0, new StringBuilder()));
        } else {
          Console.WriteLine("No serviced accounts were found.");
        }
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to create ad groups.", ex);
      }
    }

    /**
     * Example implementation of a node that would exist in an account tree.
     */
    class ManagedCustomerTreeNode {
      /// <summary>
      /// The parent node.
      /// </summary>
      private ManagedCustomerTreeNode parentNode;

      /// <summary>
      /// The account associated with this node.
      /// </summary>
      private ManagedCustomer account;

      /// <summary>
      /// The list of child accounts.
      /// </summary>
      private List<ManagedCustomerTreeNode> childAccounts = new List<ManagedCustomerTreeNode>();

      /// <summary>
      /// Gets or sets the parent node.
      /// </summary>
      public ManagedCustomerTreeNode ParentNode {
        get { return parentNode; }
        set { parentNode = value; }
      }

      /// <summary>
      /// Gets or sets the account.
      /// </summary>
      public ManagedCustomer Account {
        get { return account; }
        set { account = value; }
      }

      /// <summary>
      /// Gets the child accounts.
      /// </summary>
      public List<ManagedCustomerTreeNode> ChildAccounts {
        get { return childAccounts; }
      }

      /// <summary>
      /// Returns a <see cref="System.String"/> that represents this instance.
      /// </summary>
      /// <returns>
      /// A <see cref="System.String"/> that represents this instance.
      /// </returns>
      public override String ToString() {
        String login = String.IsNullOrEmpty(account.login) ? "(no login)" : account.login;
        return String.Format("{0}, {1}, {2}", login, account.customerId, account.name);
      }

      /// <summary>
      /// Returns a string representation of the current level of the tree and
      /// recursively returns the string representation of the levels below it.
      /// </summary>
      /// <param name="depth">The depth of the node.</param>
      /// <param name="sb">The String Builder containing the tree
      /// representation.</param>
      /// <returns>The tree string representation.</returns>
      public StringBuilder ToTreeString(int depth, StringBuilder sb) {
        sb.Append(new String('-', depth * 2));
        sb.Append(this);
        sb.Append("\n");
        foreach (ManagedCustomerTreeNode childAccount in childAccounts) {
          childAccount.ToTreeString(depth + 1, sb);
        }
        return sb;
      }
    }
  }
}
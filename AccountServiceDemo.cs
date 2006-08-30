// Demonstration of calling the AdWords API via .Net using C#
// Copyright 2006, Google Inc. All rights reserved.

/**
 * Demonstrates how customers can use the stubs generated when a web
 * reference is created pointing at the AccountService.
 *
 * Create the web reference by providing the AccountService WSDL file:
 * http://adwords.google.com/api/adwords/v5/AccountService?wsdl
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace AccountServiceDemo
{
    /**
     * Displays some of the fields in the Account's Info. 
     */
    class AccountServiceDemo
    {
        static void Main(string[] args)
        {
            // instantiate an Account Service client
            AccountService.AccountServiceService accountService = new AccountService.AccountServiceService();

            // set headers 
            String myUseragent = "YOUR COMPANY -- C# Get AdGroup Test";
            String myEmail = "youremail@yourcompany.com";
            String myPassword = "secret";
            String myToken = "ABcdeFGH93KL-NOPQ_STUv";

            accountService.useragentValue = new AccountService.useragent();
            accountService.useragentValue.Text = new String[] { myUseragent };

            accountService.emailValue = new AccountService.email();
            accountService.emailValue.Text = new String[] { myEmail };

            accountService.passwordValue = new AccountService.password();
            accountService.passwordValue.Text = new String[] { myPassword };

            accountService.tokenValue = new AccountService.token();
            accountService.tokenValue.Text = new String[] { myToken };

            // get the account info
            AccountService.AccountInfo acctInfo = accountService.getAccountInfo();
            Console.WriteLine("----- Account Info -----");
            Console.WriteLine("Customer ID: " + acctInfo.customerId);
            Console.WriteLine("Descriptive Name: " + acctInfo.descriptiveName);
            Console.WriteLine("Billing information");
            Console.WriteLine("   Company Name: " + acctInfo.billingAddress.companyName);
            Console.WriteLine("   Address Line 1: " + acctInfo.billingAddress.addressLine1);
            Console.WriteLine("   Address Line 2: " + acctInfo.billingAddress.addressLine2);
            Console.WriteLine("   City: " + acctInfo.billingAddress.city);
            Console.WriteLine("   State: " + acctInfo.billingAddress.state);
            Console.WriteLine("   Postal Code: " + acctInfo.billingAddress.postalCode);
            Console.WriteLine("   Country Code: " + acctInfo.billingAddress.countryCode);
            Console.WriteLine("Time Zone ID: " + acctInfo.timeZoneId);
            Console.WriteLine("------------------------");
            

            Console.ReadKey();
        }
    }
}

// Demonstration of calling the AdWords API via .Net using C#
// Copyright 2006, Google Inc. All rights reserved.

/**
 * Demonstrates how customers can use the stubs generated when a web
 * reference is created pointing at the InfoService.
 *
 * Create the web reference by providing the InfoService WSDL file:
 * http://adwords.google.com/api/adwords/v5/InfoService?wsdl
 */

using System;
using System.Collections.Generic;
using System.Text;

/**
 * Display the usage quota for the current month, the unit count between 
 * July 1, 2006 and today, the operation count between July 1, 2006
 * and today, and the unit count spent on AccountService.getAccountInto
 * between July 1, 2006 and today.
 */
namespace InfoServiceDemo
{
    class InfoServiceDemo
    {
        static void Main(string[] args)
        {
            // instantiate an Info Service client
            InfoService.InfoService infoService = 
                                 new InfoService.InfoService();

            // set headers 
            String myUseragent = "YOUR COMPANY -- C# Get AdGroup Test";
            String myEmail = "youremail@yourcompany.com";
            String myPassword = "secret";
            String myApplicationToken = "ABcdeFGH93KL-NOPQ_STUv";
            String myDeveloperToken = "zYxwVutS11AB-cdEF_g99H";

            infoService.useragentValue = new InfoService.useragent();
            infoService.useragentValue.Text = new String[] { myUseragent };

            infoService.emailValue = new InfoService.email();
            infoService.emailValue.Text = new String[] { myEmail };

            infoService.passwordValue = 
                                 new InfoService.password();
            infoService.passwordValue.Text = new String[] { myPassword };

            infoService.developerTokenValue = 
                                 new InfoService.developerToken();
            infoService.developerTokenValue.Text = 
                                 new String[] { myDeveloperToken };

            infoService.applicationTokenValue = new InfoService.applicationToken();
            infoService.applicationTokenValue.Text = new String[] { myApplicationToken };

            // get the quota for this month
            long usageQuota = infoService.getUsageQuotaThisMonth();
            Console.WriteLine("Usage quota for this month: " + usageQuota);
            //Console.ReadKey();

            //get the quota used between July 1, 2006 and today
            long unitCount = infoService.getUnitCount(
                                            new DateTime(2006, 7, 1, 0, 0, 0),
                                            DateTime.Today);
            Console.WriteLine("Unit count for the past day month: " 
                               + unitCount);
            //Console.ReadKey();

            //get the operation count used between July 1, 2006 and today
            long operationCount = infoService.getOperationCount(
                                            new DateTime(2006, 7, 1, 0, 0, 0),
                                            DateTime.Today);
            Console.WriteLine("Operation count for the past day month: " 
                               + operationCount);


            long methodUnitCount = infoService.getUnitCountForMethod(
                                            "AccountService", 
                                            "getAccountInfo", 
                                            new DateTime(2006, 7, 1, 0, 0, 0),
                                            DateTime.Today);
            Console.WriteLine("Method unit count for AccountService" 
                               + ".getAccountInfo between July 1, 2006 and"
                               + " today: " + methodUnitCount);

            Console.ReadKey();
        }
    }
}

Ad Manager - End-to-end ASP.NET example
====================================

Google's Ad Manager API lets developers design computer programs that interact
 directly with Google Ad Manager. With these applications, advertisers and third
 parties can more efficiently -- and creatively -- manage their large or complex
 Ad Manager accounts and campaigns.

The Ad Manager end-to-end ASP.NET sample application demonstrates how to access the
 Ad Manager API from within an ASP.NET web application. It is based on the
 [Google Ad Manager .NET client library](https://github.com/googleads/googleads-dotnet-lib).

If you are new to ASP.NET you can find more information on the
[official site](http://www.asp.net/).

The application demonstrates the following:

 - Authorization against Ad Manager with OAuth schema and credentials re-use.
 - Simple service request (UserService.getUsersByStatement) and displaying the
 results.

We are sharing this code as open source to provide a starting point for new
 developers and to demonstrate some of the core functionality in the API.

How do I get started?
---------------------

1. Make sure you have .NET 4.0 (or higher) and Visual Studio installed:

    - [.NET Framework 4.0 (or above)](http://msdn2.microsoft.com/en-us/netframework/default.aspx):
    - [Microsoft Visual Studio] (http://msdn2.microsoft.com/en-us/vstudio/default.aspx)

2. Download the application from
 [GitHub](https://github.com/googleads/googleads-dotnet-lib/releases/latest).

3. Unpack the contents

4. Open `AdManager.sln` with Visual Studio.

5. Set `AdManager.Examples.CSharp.OAuth.csproj` as active project. See
 [MSDN](https://msdn.microsoft.com/en-us/library/aa232376(v=vs.60).aspx) for instructions.

6. Edit the `Web.config` file and set appropriate values for the OAuth2
 configuration keys:

        <add key="OAuth2ClientId" value="INSERT_OAUTH2_CLIENT_ID_HERE" />
        <add key="OAuth2ClientSecret" value="INSERT_OAUTH2_CLIENT_SECRET_HERE" />

  See our [wiki guide](https://github.com/googleads/googleads-dotnet-lib/wiki/How-to-create-OAuth2-client-id-and-secret#2-web-application)
   for steps on how to obtain a OAuth2 client ID and secret for a web project.

  Also, comment out the following setting:

        <add key="OAuth2RefreshToken" value="INSERT_OAUTH2_REFRESH_TOKEN_HERE" />

  This tells the web application to generate its own credentials at runtime
   instead of reusing existing credentials from the config file.

7. Edit the `Web.config` file and set appropriate values for your network code
 and your application name:

    <add key="ApplicationName" value="INSERT_YOUR_APPLICATION_NAME_HERE"/>
    <add key="NetworkCode" value="INSERT_YOUR_NETWORK_CODE_HERE"/>

8. [Optional] Set a debug port. See
 [MSDN](https://msdn.microsoft.com/en-us/library/ms178109(v=vs.140).aspx)
 for instructions.

9. Start the project. See
 [MSDN](https://msdn.microsoft.com/en-us/library/y740d9d3.aspx#BKMK_Start_debugging_a_VS_project)
 for instructions.

This will start the project, and open a browser window that navigates you to
 the main page (something like http://localhost:5000/Default.aspx). The port
 number is randomly assigned by Visual Studio, unless you pick a specific port
 as explained in the previous step.

Using the application
---------------------

In order to access Ad Manager data the application needs to be granted access by a
 logged in user. Start by clicking the "Authorize User" button. You will be
 automatically redirected to a page with login prompt when not yet authorized.

To grant access, click the 'Proceed' link. Make sure you are on the Google
 login page, log in with your Ad Manager account credentials and select 'Grant access'.

Note: Granting access to the application will only allow access to the Ad
Manager data for the account. Other services will not be accessible.

Once logged in, click the following button to make the API call:

  * 'Get users: Lists all the users in your network on a
 [GridView](https://msdn.microsoft.com/en-us/library/system.web.ui.webcontrols.gridview(v=vs.110).aspx).

Configuring the Ad Manager API .NET library
----------------------------------------

To be able to use the Ad Manager API there are a few parameters that need to be
specified. These parameters are read from the application’s
 `App.config / Web.config` file at the runtime. For details regarding
 configuration directive please refer to the
 [App.config wiki](https://github.com/googleads/googleads-dotnet-lib/wiki/Understanding-App.config).

Miscellaneous
-------------

See the project’s
 [README file](https://github.com/googleads/googleads-dotnet-lib#miscellaneous)
 for details on how to file bugs, feature requests, contact maintainers, etc.

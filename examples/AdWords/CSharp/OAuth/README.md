AdWords - End-to-end ASP.NET example
====================================

Google's AdWords API service lets developers design computer programs that
 interact directly with the AdWords platform. With these applications,
 advertisers and third parties can more efficiently -- and creatively --
 manage their large or complex AdWords accounts and campaigns.

The AdWords end-to-end ASP.NET sample application demonstrates how to access the
 AdWords API from within an ASP.NET web application. It is based on the
 [Google AdWords .NET client library](https://github.com/googleads/googleads-dotnet-lib).

If you are new to ASP.NET you can find more information on the
[official site](http://www.asp.net/).

The application demonstrates the following:

 - Authorization against AdWords with OAuth schema and credentials re-use.
 - Simple service request (CampaignService.get) and displaying the results.
 - Basic AdHoc reporting functionality with downloads support.

We are sharing this code as open source to provide a starting point for new
 developers and to demonstrate some of the core functionality in the API.

How do I get started?
---------------------

1. Make sure you have .NET 4.0 (or higher) and Visual Studio installed:
  - [.NET Framework 4.0 (or above)](http://msdn2.microsoft.com/en-us/netframework/default.aspx):
  - [Microsoft Visual Studio] (http://msdn2.microsoft.com/en-us/vstudio/default.aspx)

2. Download the application from [GitHub](https://github.com/googleads/googleads-dotnet-lib/releases/latest).

3. Unpack the contents

4. Open AdWords.sln with Visual Studio.

5. Set AdWords.Examples.CSharp.OAuth.csproj as active project. See
 [MSDN](https://msdn.microsoft.com/en-us/library/aa232376(v=vs.60).aspx)
 for instructions.

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

7. Edit the `Web.config` file and set appropriate values for the developer token:

    <add key="DeveloperToken" value="INSERT_YOUR_DEVELOPER_TOKEN_HERE"/>

  If you don’t have one yet, you can get one by following the instructions on our
   [signup guide](https://developers.google.com/adwords/api/docs/signingup).


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

In order to access AdWords data the application needs to be granted access by
 a logged in user. Start by clicking the 'Authorize User' button. You will be
 automatically redirected to a page with login prompt when not yet authorized.

To grant access, click the 'Proceed' link. Make sure you are on the Google login
 page, log in with your AdWords account credentials and select 'Grant access'.

Note: Granting access to the application will only allow access to the AdWords
 data for the account. Other services will not be accessible.

Once logged in you can enter the customer ID of the account that you want to run
 make calls against. Then click the following buttons to make the API calls:

  * 'Get campaigns': Lists all the campaigns in your account on a
 [GridView](https://msdn.microsoft.com/en-us/library/system.web.ui.webcontrols.gridview(v=vs.110).aspx).
  * 'Download Criteria Report': Runs a criteria report and downloads it in CSV
 format.

Configuring the AdWords API .NET library
----------------------------------------

To be able to use the AdWords API there are a few parameters that need to be
specified. These parameters are read from the application’s
 `App.config / Web.config` file at the runtime. For details regarding
  configuration directive please refer to the
  [App.config wiki](https://github.com/googleads/googleads-dotnet-lib/wiki/Understanding-App.config).

Miscellaneous
-------------

See the project’s [README file](https://github.com/googleads/googleads-dotnet-lib#miscellaneous)
 for details on how to file bugs, feature requests, contact maintainers, etc.

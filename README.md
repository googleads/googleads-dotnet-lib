#Google Ads API .NET Client Library

This project hosts the .NET client library for the various SOAP-Based Ads APIs at Google -
 [AdWords API](//developers.google.com/adwords/api) and
 [Google's DoubleClick for Publishers (DFP) API](//developers.google.com/doubleclick-publishers).

##Features
- Distributed via Nuget
- Stub classes for all the supported API versions and services.
- Helpful utilities
- SOAP messages are logged, for easier debugging purposes
- Automatic handling of SOAP headers
- Easy management of credentials, authentication, and session information
- Docs available in HTML and XML format.

##Requirements

- .NET Framework 4.0 (or above): http://msdn2.microsoft.com/en-us/netframework/default.aspx
- Microsoft Visual Studio: http://msdn2.microsoft.com/en-us/vstudio/default.aspx
- An appropriate Google Ads account.

##Announcements and updates

For API and client library updates and news, please follow our Google+ Ads Developers page:
https://plus.google.com/+GoogleAdsDevelopers/posts and our Google Ads Developers blog:
http://googleadsdeveloper.blogspot.com/.

##How do I start?

We provide source, binary and nuget distributions for the client library. You may continue reading
the the appropriate section below, depending on the distribution you are using.

##Nuget distribution

This is the recommended way to get the client library. The nuget distributions of the client library
 are listed below:
###AdWords API

- [Google.AdWords](https://www.nuget.org/packages/Google.AdWords/): AdWords and DoubleClick Ad
 Exchange Buyer API DotNet Client Library
- [Google.AdWords.Examples.CSharp](https://www.nuget.org/packages/Google.AdWords.Examples.CSharp/):
 C# Code examples for AdWords API
- [Google.AdWords.Examples.VB](https://www.nuget.org/packages/Google.AdWords.Examples.VB/):
 VB.NET Code examples for AdWords API

###DFP API

- [Google.Dfp](https://www.nuget.org/packages/Google.Dfp/): DFP API DotNet Client Library
- [Google.Dfp.Examples.CSharp](https://www.nuget.org/packages/Google.Dfp.Examples.CSharp/):
 C# Code examples for DFP API

To use the library, you can install the appropriate nuget packages and add reference to your Visual
 Studio project. You can learn more about the nuget package manager at http://www.nuget.org.
##Binary distribution

The binary distribution of the Ads API .NET library consists of a precompiled version of the
 library as a .NET assembly, code examples for using the library, and library documentation.
 If you are interested in just using the library and not in its internals, and you don't use
 nuget package manager in your development environment, then you should download this distribution.
 There are separate downloads for AdWords and DFP APIs.

The contents of this distribution are as follows:
```
  \
    \lib
      - Precompiled assemblies.
      - Documentation xmls for the assemblies.
    \examples
       Code examples, in C# and VB.NET (when available).
    README
    ChangeLog
    COPYING
    Visual Studio solution file
```
To run the code examples:

- Open Visual Studio solution file in the root folder of the binary distribution in Microsoft
 Visual Studio
- Open `App.config` for the examples project and follow the instructions in the file to enter
 required configuration values.
- Save and close `App.config`.
- Open the Properties dialog for the Examples project (Right click the Examples project of your
 choice in the Solution Explorer and select the Properties option from the context menu.).
- Navigate to the Debug Tab and enter the command line options. The command line options are of
 the form `version.examplename`. For instance, `v201603.AddCampaign` is the command line option to
 run `AddCampaign` example in `v201603` version of the AdWords API.
- Compile and run the Examples project.

Alternatively, each code example has a `main()` method, so you can set the appropriate code example
 as the Startup object (Select the Application tab on the Examples Properties dialog and pick
 the desired class from the "Startup object" dropdown.).

To use the library in a new project:

- Create a new Visual Studio project of your choice (for instance, a C# Windows application).
- Copy the lib folder from the binary distribution to your project folder. Add references to all
 the assemblies in this folder in your project.
- Add a reference to System.Web.Services in your project.
- Copy `examples\App.config` to your project directory and add it to your project.
- Edit the required keys in `App.config`. If your application has its own `App.config`, then you
 need to merge its contents with the contents of `examples\App.config`.
- Make a call to the library, e.g.:

```
// Create an appropriate AdsUser instance.
AdWordsUser user = new AdWordsUser();

// Create the required service.
CampaignService service = (CampaignService) user.GetService(
     AdWordsService.v201603.CampaignService);

// make more calls to service class.
```
You can refer to [this wiki article](//github.com/googleads/googleads-dotnet-lib/wiki/Getting-Started) for
 more details.

##Source distribution

The source distribution of the Ads API .NET Client Library consists of individual Visual Studio
 projects for the client library, code examples and test cases, documentation, and support
 libraries and tools for compiling and running the library. If you are interested in the
 library internals you should download this distribution. There are separate downloads for
 AdWords and DFP APIs.

```
The contents of this distribution are as follows:

  \
    \examples
      \adwords
        - Adwords API code examples, in C# and VB.NET.
    \lib
      - External client libraries referenced by various projects in the source
        distribution.
    \src
      - Client library source, provided as a Visual Studio project.
    \tests
      - NAnt test cases for the library, provided as a Visual Studio project
    README
    Visual Studio solution
    ChangeLog
    COPYING
```

To compile and run the project:

- Open the Visual Studio solution in Microsoft Visual Studio.
- Open `App.config` for the examples project of your choice and follow the instructions in the
 file to enter required configuration values.
- Save and close `App.config`.
- Open the Properties dialog for the Examples project (Right click the Examples project in the
 Solution Explorer and select the Properties option from the context menu.).
- Navigate to the Debug tab and enter the command line options. The command line options are of
 the form `version.examplename`. For instance, `v201603.AddCampaign` is the command line option
 to run the `AddCampaign` example for the `v201603` version of the AdWords API.
- Set the Examples project as the Startup project.
- Compile and run the Examples project.

Alternatively, each code example has a `main()` method, so you can set the appropriate code
 example as the Startup object (Select the Application tab on the Examples Properties dialog and
 pick the desired class from the "Startup object" dropdown.).

To run the test cases, you need to download and install the latest version of NUnit from
 http://www.nunit.org/. Once you have installed NUnit, you can compile and run the test cases
 as follows:

- Right click the appropriate Tests project and choose References from the context menu.
- From the references dialog, add a reference to the `nunit.framework` assembly.
- Right click theTests project and choose Properties. Navigate to the Debug tab and choose
 "Start external program". Browse to the path for `nunit.exe` and pick it as the Startup
 application.
- Open `App.config` for the test project and follow the instructions in the file to enter
 required configuration values.
- Set the `AdWords.Tests` project as the startup project and run the project.

It is recommended that you run the test cases against a test environment. Refer to the
 appropriate API documentation for details on how this may be done.

##Working with OAuth2

See https://github.com/googleads/googleads-dotnet-lib/wiki#oauth2 for details.

##How to enable logging

See https://github.com/googleads/googleads-dotnet-lib/wiki#logging for details. 
 
##Miscellaneous

###Wiki
- https://github.com/googleads/googleads-dotnet-lib/wiki

###Issue tracker
- https://github.com/googleads/googleads-dotnet-lib/issues

###API Documentation:
- AdWords API: https://developers.google.com/adwords/api/
- DFP API: https://developers.google.com/doubleclick-publishers/

###Support forum
- AdWords API: https://developers.google.com/adwords/api/community/
- DFP API: https://developers.google.com/doubleclick-publishers/community

###Authors
- https://github.com/AnashOommen
- https://github.com/ChristopherSeeley
- https://github.com/jimper

# Google Ads API .NET Client Library

This project hosts the .NET client library for the various SOAP-Based Ads APIs at Google -
 [AdWords API](//developers.google.com/adwords/api) and
 [Google Ad Manager API](//developers.google.com/ad-manager).

## Features
- Distributed via Nuget
- Stub classes for all the supported API versions and services.
- Helpful utilities
- SOAP messages are logged, for easier debugging purposes
- Automatic handling of SOAP headers
- Easy management of credentials, authentication, and session information
- Docs available in HTML and XML format.

## Supported Frameworks

- .NET Framework 4.5.2+
- .NET Standard 2.0 ([limited support](https://github.com/googleads/googleads-dotnet-lib/wiki/Cross-Platform-Development))

## Announcements and updates

For API and client library updates and news, please follow our Google Ads Developers blog:
http://googleadsdeveloper.blogspot.com/.

## Getting started

1. Install your library of choice. We recommend using the following Nuget distributions:
  #### AdWords API

  - [Google.AdWords](https://www.nuget.org/packages/Google.AdWords/): AdWords API DotNet Client
  Library

  #### Ad Manager API

  - [Google.Dfp](https://www.nuget.org/packages/Google.Dfp/): Ad Manager API DotNet Client Library

  You can learn more about the nuget package manager at http://www.nuget.org. For other distribution
  options, see the alternative distribution options below.

1. Setup your OAuth2 credentials.

  The AdWords and Ad Manager APIs use
[OAuth2](http://oauth.net/2/) as the authentication mechanism. Follow the appropriate guide below
 based on your use case.

  **If you're accessing an API using your own credentials...**

  * [Using AdWords API](https://github.com/googleads/googleads-dotnet-lib/wiki/API-access-using-own-credentials-(installed-application-flow))
  * [Using Ad Manager API](https://github.com/googleads/googleads-dotnet-lib/wiki/API-access-using-own-credentials-(server-to-server-flow))

  **If you're accessing an API on behalf of clients...**

  * [Developing a web application (AdWords API or Ad Manager API)](https://github.com/googleads/googleads-dotnet-lib/wiki/API-access-on-behalf-of-your-clients-(web-flow))

## Alternative distribution options

### Binary distribution

The binary distribution of the Ads API .NET library consists of a precompiled version of the
 library as a .NET assembly, code examples for using the library, and library documentation.
 If you are interested in just using the library and not in its internals, and you don't use
 nuget package manager in your development environment, then you should download this distribution.
 There are separate downloads for AdWords and Ad Manager APIs.

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
 the form `version.examplename`. For instance, `v201809.AddCampaign` is the command line option to
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
using (CampaignService service = (CampaignService) user.GetService(
     AdWordsService.v201809.CampaignService))
{
    // make more calls to service class.
}

```
You can refer to [this wiki article](//github.com/googleads/googleads-dotnet-lib/wiki/Getting-Started) for
 more details.

## How to enable logging

See https://github.com/googleads/googleads-dotnet-lib/wiki#logging for details.

## Miscellaneous

### Wiki
- https://github.com/googleads/googleads-dotnet-lib/wiki

### Issue tracker
- https://github.com/googleads/googleads-dotnet-lib/issues

### API Documentation:
- AdWords API: https://developers.google.com/adwords/api/
- Ad Manager API: https://developers.google.com/ad-manager/

### Support forum
- AdWords API: https://developers.google.com/adwords/api/community/
- Ad Manager API: https://developers.google.com/doubleclick-publishers/community

### Authors
- https://github.com/AnashOommen
- https://github.com/ChristopherSeeley
- https://github.com/jimper

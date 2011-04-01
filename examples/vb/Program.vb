' Copyright 2011, Google Inc. All Rights Reserved.
'
' Licensed under the Apache License, Version 2.0 (the "License");
' you may not use this file except in compliance with the License.
' You may obtain a copy of the License at
'
'     http://www.apache.org/licenses/LICENSE-2.0
'
' Unless required by applicable law or agreed to in writing, software
' distributed under the License is distributed on an "AS IS" BASIS,
' WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
' See the License for the specific language governing permissions and
' limitations under the License.

' Author: api.anash@gmail.com (Anash P. Oommen)

Imports Google.Api.Ads.AdWords.Examples.VB.Both
Imports Google.Api.Ads.AdWords.Examples.VB.v13
Imports Google.Api.Ads.AdWords.Examples.VB.v201008

Imports Google.Api.Ads.AdWords.Lib

Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Net
Imports System.Reflection

Namespace Google.Api.Ads.AdWords.Examples.VB
  ''' <summary>
  ''' The Main class for this application.
  ''' </summary>
  Class Program
    ''' <summary>
    ''' A flag to keep track of whether help message was shown earlier.
    ''' </summary>
    Private Shared helpShown As Boolean = False

    ''' <summary>
    ''' A map to hold the code examples to be executed.
    ''' </summary>
    Private Shared sampleMap As List(Of KeyValuePair(Of String, SampleBase)) = _
        New List(Of KeyValuePair(Of String, SampleBase))

    ''' <summary>
    ''' Registers the code example.
    ''' </summary>
    ''' <param name="key">The code example name.</param>
    ''' <param name="value">The code example instance.</param>
    Private Shared Sub RegisterSample(ByVal key As String, ByVal value As SampleBase)
      sampleMap.Add(New KeyValuePair(Of String, SampleBase)(key, value))
    End Sub

    ''' <summary>
    ''' Static constructor to initialize the sample map.
    ''' </summary>
    Shared Sub New()
      ' Add v13 samples.
      RegisterSample("v13.AccountServiceDemo", New v13.AccountServiceDemo)
      RegisterSample("v13.AccountServiceNoConfigDemo", New v13.AccountServiceNoConfigDemo)
      RegisterSample("v13.KeywordEstimateDemo", New v13.KeywordEstimateDemo)
      RegisterSample("v13.CheckKeywordTrafficDemo", New v13.CheckKeywordTrafficDemo)
      RegisterSample("v13.ReportServiceKeywordDemo", New v13.ReportServiceKeywordDemo)
      RegisterSample("v13.ReportServiceStructureDemo", New v13.ReportServiceStructureDemo)
      RegisterSample("v13.DownloadReportAsCsvDemo", New v13.DownloadReportAsCsvDemo)
      RegisterSample("v13.DownloadReportAsXmlDemo", New v13.DownloadReportAsXmlDemo)

      ' Add v201008 code examples.
      RegisterSample("v201008.AddCampaign", New v201008.AddCampaign)
      RegisterSample("v201008.UpdateCampaign", New v201008.UpdateCampaign)
      RegisterSample("v201008.GetAllCampaigns", New v201008.GetAllCampaigns)
      RegisterSample("v201008.GetCampaign", New v201008.GetCampaign)
      RegisterSample("v201008.GetAllPausedCampaigns", New v201008.GetAllPausedCampaigns)
      RegisterSample("v201008.CheckCampaigns", New v201008.CheckCampaigns)
      RegisterSample("v201008.DeleteCampaign", New v201008.DeleteCampaign)

      RegisterSample("v201008.SetCampaignTargets", New v201008.SetCampaignTargets)
      RegisterSample("v201008.GetAllCampaignTargets", New v201008.GetAllCampaignTargets)

      RegisterSample("v201008.AddAdGroup", New v201008.AddAdGroup)
      RegisterSample("v201008.UpdateAdGroup", New v201008.UpdateAdGroup)
      RegisterSample("v201008.GetAllAdGroups", New v201008.GetAllAdGroups)
      RegisterSample("v201008.DeleteAdGroup", New v201008.DeleteAdGroup)

      RegisterSample("v201008.AddAds", New v201008.AddAds)
      RegisterSample("v201008.HandlePolicyViolationError", New v201008.HandlePolicyViolationError)
      RegisterSample("v201008.UpdateAd", New v201008.UpdateAd)
      RegisterSample("v201008.AddMobileImageAd", New v201008.AddMobileImageAd)
      RegisterSample("v201008.GetAllAds", New v201008.GetAllAds)
      RegisterSample("v201008.GetAllDisapprovedAds", New v201008.GetAllDisapprovedAds)
      RegisterSample("v201008.DeleteAd", New v201008.DeleteAd)

      RegisterSample("v201008.AddCampaignAdExtension", New v201008.AddCampaignAdExtension)

      RegisterSample("v201008.PerformBulkMutateJob", New v201008.PerformBulkMutateJob)
      RegisterSample("v201008.GetAllBulkMutateJobs", New v201008.GetAllBulkMutateJobs)
      RegisterSample("v201008.DeleteBulkMutateJob", New v201008.DeleteBulkMutateJob)

      RegisterSample("v201008.GetAllCampaignAdExtensions", New v201008.GetAllCampaignAdExtensions)

      RegisterSample("v201008.AddAdGroupCriteria", New v201008.AddAdGroupCriteria)
      RegisterSample("v201008.UpdateAdGroupCriterion", New v201008.UpdateAdGroupCriterion)
      RegisterSample("v201008.GetAllAdGroupCriteria", New v201008.GetAllAdGroupCriteria)
      RegisterSample("v201008.GetAllActiveAdGroupCriteria", New v201008.GetAllActiveAdGroupCriteria)
      RegisterSample("v201008.DeleteAdGroupCriterion", New v201008.DeleteAdGroupCriterion)

      RegisterSample("v201008.AddNegativeCampaignCriterion", _
          New v201008.AddNegativeCampaignCriterion)
      RegisterSample("v201008.GetAllNegativeCampaignCriteria", _
          New v201008.GetAllNegativeCampaignCriteria)

      RegisterSample("v201008.GetRelatedKeywords", New v201008.GetRelatedKeywords)
      RegisterSample("v201008.GetRelatedPlacements", New v201008.GetRelatedPlacements)

      RegisterSample("v201008.AddAdExtensionOverride", New v201008.AddAdExtensionOverride)
      RegisterSample("v201008.GetAllAdExtensionOverrides", New v201008.GetAllAdExtensionOverrides)

      RegisterSample("v201008.SetAdParams", New v201008.SetAdParams)

      RegisterSample("v201008.GetGeoLocationInfo", New v201008.GetGeoLocationInfo)

      RegisterSample("v201008.GetConversionOptimizerEligibility", _
          New v201008.GetConversionOptimizerEligibility)

      RegisterSample("v201008.GetTotalUsageUnitsPerMonth", New v201008.GetTotalUsageUnitsPerMonth)
      RegisterSample("v201008.GetOperationCount", New v201008.GetOperationCount)
      RegisterSample("v201008.GetUnitCount", New v201008.GetUnitCount)
      RegisterSample("v201008.GetMethodCost", New v201008.GetMethodCost)
      RegisterSample("v201008.MethodApiUnitsUsageDemo", New v201008.MethodApiUnitsUsageDemo)

      RegisterSample("v201008.BackupSandboxDemo", New v201008.BackupSandboxDemo)
      RegisterSample("v201008.RestoreSandboxDemo", New v201008.RestoreSandboxDemo)

      RegisterSample("v201008.GetCriterionBidLandscape", New v201008.GetCriterionBidLandscape)

      RegisterSample("v201008.GetAllReportDefinitions", New v201008.GetAllReportDefinitions)
      RegisterSample("v201008.GetReportFields", New v201008.GetReportFields)
      RegisterSample("v201008.DownloadReport", New v201008.DownloadReport)
      RegisterSample("v201008.AddKeywordsPerformanceReportDefinition", _
          New v201008.AddKeywordsPerformanceReportDefinition)

      RegisterSample("v201008.UploadImage", New v201008.UploadImage)
      RegisterSample("v201008.GetAllImages", New v201008.GetAllImages)

      RegisterSample("v201008.GetAllVideos", New v201008.GetAllVideos)

      RegisterSample("v201008.AddSiteLinks", New v201008.AddSiteLinks)
      RegisterSample("v201008.DeleteSitelinks", New v201008.DeleteSitelinks)

      RegisterSample("v201008.AddExperiment", New v201008.AddExperiment)
      RegisterSample("v201008.GetAllExperiments", New v201008.GetAllExperiments)
      RegisterSample("v201008.PromoteExperiment", New v201008.PromoteExperiment)
      RegisterSample("v201008.DeleteExperiment", New v201008.DeleteExperiment)

      RegisterSample("v201008.GetTrafficEstimates", New v201008.GetTrafficEstimates)

      RegisterSample("v201008.GetCampaignAlerts", New v201008.GetCampaignAlerts)

      RegisterSample("v201008.GetAccountHierarchy", New v201008.GetAccountHierarchy)

      RegisterSample("v201008.AddUserList", New v201008.AddUserList)
      RegisterSample("v201008.AddLogicalUserList", New v201008.AddLogicalUserList)
      RegisterSample("v201008.DeleteUserList", New v201008.DeleteUserList)
      RegisterSample("v201008.GetAllUserLists", New v201008.GetAllUserLists)
      RegisterSample("v201008.UpdateUserList", New v201008.UpdateUserList)

      RegisterSample("v201008.GetAllAccountChanges", New v201008.GetAllAccountChanges)
      RegisterSample("v201008.HandlePartialFailures", New v201008.HandlePartialFailures)

      ' Add v201101 code examples.
      RegisterSample("v201101.AddCampaign", New v201101.AddCampaign)
      RegisterSample("v201101.UpdateCampaign", New v201101.UpdateCampaign)
      RegisterSample("v201101.GetAllCampaigns", New v201101.GetAllCampaigns)
      RegisterSample("v201101.GetCampaign", New v201101.GetCampaign)
      RegisterSample("v201101.GetAllPausedCampaigns", New v201101.GetAllPausedCampaigns)
      RegisterSample("v201101.CheckCampaigns", New v201101.CheckCampaigns)
      RegisterSample("v201101.DeleteCampaign", New v201101.DeleteCampaign)

      RegisterSample("v201101.SetCampaignTargets", New v201101.SetCampaignTargets)
      RegisterSample("v201101.GetAllCampaignTargets", New v201101.GetAllCampaignTargets)

      RegisterSample("v201101.AddAdGroup", New v201101.AddAdGroup)
      RegisterSample("v201101.UpdateAdGroup", New v201101.UpdateAdGroup)
      RegisterSample("v201101.GetAllAdGroups", New v201101.GetAllAdGroups)
      RegisterSample("v201101.DeleteAdGroup", New v201101.DeleteAdGroup)

      RegisterSample("v201101.AddAds", New v201101.AddAds)
      RegisterSample("v201101.HandlePolicyViolationError", New v201101.HandlePolicyViolationError)
      RegisterSample("v201101.UpdateAd", New v201101.UpdateAd)
      RegisterSample("v201101.AddMobileImageAd", New v201101.AddMobileImageAd)
      RegisterSample("v201101.GetAllAds", New v201101.GetAllAds)
      RegisterSample("v201101.GetAllDisapprovedAds", New v201101.GetAllDisapprovedAds)
      RegisterSample("v201101.DeleteAd", New v201101.DeleteAd)

      RegisterSample("v201101.AddCampaignAdExtension", New v201101.AddCampaignAdExtension)

      RegisterSample("v201101.PerformBulkMutateJob", New v201101.PerformBulkMutateJob)
      RegisterSample("v201101.GetBulkMutateJob", New v201101.GetBulkMutateJob)
      RegisterSample("v201101.DeleteBulkMutateJob", New v201101.DeleteBulkMutateJob)

      RegisterSample("v201101.GetAllCampaignAdExtensions", New v201101.GetAllCampaignAdExtensions)

      RegisterSample("v201101.AddAdGroupCriteria", New v201101.AddAdGroupCriteria)
      RegisterSample("v201101.UpdateAdGroupCriterion", New v201101.UpdateAdGroupCriterion)
      RegisterSample("v201101.GetAllAdGroupCriteria", New v201101.GetAllAdGroupCriteria)
      RegisterSample("v201101.GetAllActiveAdGroupCriteria", New v201101.GetAllActiveAdGroupCriteria)
      RegisterSample("v201101.DeleteAdGroupCriterion", New v201101.DeleteAdGroupCriterion)

      RegisterSample("v201101.AddNegativeCampaignCriterion", _
          New v201101.AddNegativeCampaignCriterion)
      RegisterSample("v201101.GetAllNegativeCampaignCriteria", _
          New v201101.GetAllNegativeCampaignCriteria)

      RegisterSample("v201101.GetRelatedKeywords", New v201101.GetRelatedKeywords)
      RegisterSample("v201101.GetRelatedPlacements", New v201101.GetRelatedPlacements)

      RegisterSample("v201101.AddAdExtensionOverride", New v201101.AddAdExtensionOverride)
      RegisterSample("v201101.GetAllAdExtensionOverrides", New v201101.GetAllAdExtensionOverrides)

      RegisterSample("v201101.SetAdParams", New v201101.SetAdParams)

      RegisterSample("v201101.GetGeoLocationInfo", New v201101.GetGeoLocationInfo)

      RegisterSample("v201101.GetConversionOptimizerEligibility", _
          New v201101.GetConversionOptimizerEligibility)

      RegisterSample("v201101.GetTotalUsageUnitsPerMonth", New v201101.GetTotalUsageUnitsPerMonth)
      RegisterSample("v201101.GetOperationCount", New v201101.GetOperationCount)
      RegisterSample("v201101.GetUnitCount", New v201101.GetUnitCount)
      RegisterSample("v201101.GetMethodCost", New v201101.GetMethodCost)
      RegisterSample("v201101.MethodApiUnitsUsageDemo", New v201101.MethodApiUnitsUsageDemo)

      RegisterSample("v201101.BackupSandboxDemo", New v201101.BackupSandboxDemo)
      RegisterSample("v201101.RestoreSandboxDemo", New v201101.RestoreSandboxDemo)

      RegisterSample("v201101.GetCriterionBidLandscape", New v201101.GetCriterionBidLandscape)
      RegisterSample("v201101.GetAdGroupBidLandscape", New v201101.GetAdGroupBidLandScape)

      RegisterSample("v201101.GetAllReportDefinitions", New v201101.GetAllReportDefinitions)
      RegisterSample("v201101.GetReportFields", New v201101.GetReportFields)
      RegisterSample("v201101.DownloadReport", New v201101.DownloadReport)
      RegisterSample("v201101.AddKeywordsPerformanceReportDefinition", _
          New v201101.AddKeywordsPerformanceReportDefinition)

      RegisterSample("v201101.UploadImage", New v201101.UploadImage)
      RegisterSample("v201101.GetAllImages", New v201101.GetAllImages)

      RegisterSample("v201101.GetAllVideos", New v201101.GetAllVideos)

      RegisterSample("v201101.AddSiteLinks", New v201101.AddSiteLinks)
      RegisterSample("v201101.DeleteSitelinks", New v201101.DeleteSitelinks)

      RegisterSample("v201101.AddExperiment", New v201101.AddExperiment)
      RegisterSample("v201101.GetAllExperiments", New v201101.GetAllExperiments)
      RegisterSample("v201101.PromoteExperiment", New v201101.PromoteExperiment)
      RegisterSample("v201101.DeleteExperiment", New v201101.DeleteExperiment)

      RegisterSample("v201101.GetTrafficEstimates", New v201101.GetTrafficEstimates)

      RegisterSample("v201101.GetCampaignAlerts", New v201101.GetCampaignAlerts)

      RegisterSample("v201101.GetAccountHierarchy", New v201101.GetAccountHierarchy)

      RegisterSample("v201101.AddUserList", New v201101.AddUserList)
      RegisterSample("v201101.AddLogicalUserList", New v201101.AddLogicalUserList)
      RegisterSample("v201101.DeleteUserList", New v201101.DeleteUserList)
      RegisterSample("v201101.GetAllUserLists", New v201101.GetAllUserLists)
      RegisterSample("v201101.UpdateUserList", New v201101.UpdateUserList)

      RegisterSample("v201101.GetAllAccountChanges", New v201101.GetAllAccountChanges)
      RegisterSample("v201101.HandlePartialFailures", New v201101.HandlePartialFailures)
      RegisterSample("v201101.GetKeywordOpportunities", New v201101.GetKeywordOpportunities())
      RegisterSample("v201101.AddConversion", New v201101.AddConversionTracker())
      RegisterSample("v201101.GetAllConversions", New v201101.GetAllConversionTrackers())
      RegisterSample("v201101.UpdateConversion", New v201101.UpdateConversionTracker())

      ' Add combined examples.
      RegisterSample("Both.UsingTrafficEstimatorDemo", New Both.UsingTrafficEstimatorDemo)
    End Sub

    ''' <summary>
    ''' The main method.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      If (args.Length = 0) Then
        ShowUsage()
      Else
        Dim user As New AdWordsUser
        If (String.Compare(args(0), "--all", True) = 0) Then
          For Each pair As KeyValuePair(Of String, SampleBase) In sampleMap
            RunASample(user, pair.Value)
          Next
        Else
          If (String.Compare(args(0), "--v13all", True) = 0) Then
            For Each pair As KeyValuePair(Of String, SampleBase) In sampleMap
              If pair.Key.StartsWith("v13") Then
                RunASample(user, pair.Value)
              End If
            Next
          ElseIf (String.Compare(args(0), "--v201008all", True) = 0) Then
            For Each pair As KeyValuePair(Of String, SampleBase) In sampleMap
              If pair.Key.StartsWith("v201008") Then
                RunASample(user, pair.Value)
              End If
            Next
          Else
            For Each cmdArgs As String In args
              Dim found As Boolean = False
              For Each pair As KeyValuePair(Of String, SampleBase) In sampleMap
                If String.Compare(pair.Key, cmdArgs, True) = 0 Then
                  found = True
                  RunASample(user, pair.Value)
                  Exit For
                End If
              Next
              If (Not found) Then
                ShowUsage()
              End If
            Next
          End If
        End If
      End If
    End Sub

    ''' <summary>
    ''' Runs a code example.
    ''' </summary>
    ''' <param name="user">The user whose credentials should be used for
    ''' running the code example.</param>
    ''' <param name="sample">The code example to run.</param>
    Private Shared Sub RunASample(ByVal user As AdWordsUser, ByVal sample As SampleBase)
      Try
        Console.WriteLine(sample.Description)
        sample.Run(user)
      Catch ex As Exception
        Console.WriteLine("An exception occurred while running this code example.\n{0} at\n{1}", _
            ex.Message, ex.StackTrace)
      Finally
        Console.WriteLine("Press [Enter] to continue")
        Console.ReadLine()
      End Try
    End Sub

    ''' <summary>
    ''' Prints program usage message.
    ''' </summary>
    Private Shared Sub ShowUsage()
      If Not helpShown Then
        helpShown = True
        Dim exeName As String = Path.GetFileName(Assembly.GetExecutingAssembly.Location)

        Console.WriteLine("Runs AdWords API code examples")
        Console.WriteLine("Usage : {0} [flags]\n", exeName)
        Console.WriteLine("Available flags\n")
        Console.WriteLine("--help\t\t : Prints this help message.", exeName)
        Console.WriteLine("--all\t\t : Run all code examples.", exeName)
        Console.WriteLine("--v13all: Runs all v13 code examples.")
        Console.WriteLine("--v201008all: Runs all v201008 code examples.")

        Console.WriteLine("\nexamplename1 [examplename1 ...] : Run specific code examples. " & _
            "Example name can be one of the following:", exeName)

        For Each pair As KeyValuePair(Of String, SampleBase) In sampleMap
          Console.WriteLine("{0} : {1}", pair.Key, pair.Value.Description)
        Next

        Console.WriteLine("Press [Enter] to continue")
        Console.ReadLine()
      End If
    End Sub
  End Class
End Namespace

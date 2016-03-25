' Copyright 2016, Google Inc. All Rights Reserved.
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

Imports Google.Api.Ads.AdWords.Lib
Imports Google.Api.Ads.AdWords.v201603

Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Text

Namespace Google.Api.Ads.AdWords.Examples.VB.v201603
  ''' <summary>
  ''' This code example gets the changes in the account during the last 24
  ''' hours.
  ''' </summary>
  Public Class GetAccountChanges
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New GetAccountChanges
      Console.WriteLine(codeExample.Description)
      Try
        codeExample.Run(New AdWordsUser)
      Catch e As Exception
        Console.WriteLine("An exception occurred while running this code example. {0}", _
            ExampleUtilities.FormatException(e))
      End Try
    End Sub

    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example gets the changes in the account during the last 24 hours."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    Public Sub Run(ByVal user As AdWordsUser)
      ' Get the CustomerSyncService.
      Dim customerSyncService As CustomerSyncService = CType(user.GetService( _
          AdWordsService.v201603.CustomerSyncService), CustomerSyncService)

      ' The date time string should be of the form  yyyyMMdd HHmmss zzz.
      Dim minDateTime As String = (DateTime.Now.AddDays(-1).ToUniversalTime.ToString( _
          "yyyyMMdd HHmmss") & " UTC")
      Dim maxDateTime As String = (DateTime.Now.ToUniversalTime.ToString( _
                                   "yyyyMMdd HHmmss") & " UTC")

      ' Create date time range.
      Dim dateTimeRange As New DateTimeRange
      dateTimeRange.min = minDateTime
      dateTimeRange.max = maxDateTime

      Try
        ' Create the selector.
        Dim selector As New CustomerSyncSelector
        selector.dateTimeRange = dateTimeRange
        selector.campaignIds = GetAllCampaignIds(user)

        ' Get all account changes for campaign.
        Dim accountChanges As CustomerChangeData = customerSyncService.get(selector)

        ' Display the changes.
        If ((Not accountChanges Is Nothing) AndAlso _
            (Not accountChanges.changedCampaigns Is Nothing)) Then
          Console.WriteLine("Displaying changes up to: {0}", accountChanges.lastChangeTimestamp)

          For Each campaignChanges As CampaignChangeData In accountChanges.changedCampaigns
            Console.WriteLine("Campaign with id ""{0}"" was changed:", campaignChanges.campaignId)
            Console.WriteLine("  Campaign changed status: {0}", _
                campaignChanges.campaignChangeStatus)
            If (campaignChanges.campaignChangeStatus <> ChangeStatus.NEW) Then

              Console.WriteLine("  Added campaign criteria: {0}", _
                  GetFormattedList(campaignChanges.addedCampaignCriteria))

              Console.WriteLine("  Removed campaign criteria: {0}", _
                  GetFormattedList(campaignChanges.removedCampaignCriteria))

              If (Not campaignChanges.changedAdGroups Is Nothing) Then
                For Each adGroupChanges As AdGroupChangeData In campaignChanges.changedAdGroups
                  Console.WriteLine("  Ad group with id ""{0}"" was changed:", _
                      adGroupChanges.adGroupId)
                  Console.WriteLine("    Ad group changed status: {0}", _
                      adGroupChanges.adGroupChangeStatus)
                  If (adGroupChanges.adGroupChangeStatus <> ChangeStatus.NEW) Then
                    Console.WriteLine("    Ads changed: {0}", _
                        GetFormattedList(adGroupChanges.changedAds))
                    Console.WriteLine("    Criteria changed: {0}", _
                        GetFormattedList(adGroupChanges.changedCriteria))
                    Console.WriteLine("    Criteria removed: {0}", _
                        GetFormattedList(adGroupChanges.removedCriteria))
                  End If
                Next
              End If
            End If
            Console.WriteLine()
          Next
        Else
          Console.WriteLine("No account changes were found.")
        End If
      Catch e As Exception
        Throw New System.ApplicationException("Failed to get account changes.", e)
      End Try
    End Sub

    ''' <summary>
    ''' Formats a list of ids as a comma separated string.
    ''' </summary>
    ''' <param name="ids">The list of ids.</param>
    ''' <returns>The comma separed formatted string, enclosed in square braces.
    ''' </returns>
    Private Function GetFormattedList(ByVal ids As Long()) As String
      Dim builder As New StringBuilder
      If (Not ids Is Nothing) Then
        For Each id As Long In ids
          builder.AppendFormat("{0}, ", id)
        Next
      End If
      Return ("[" & builder.ToString.TrimEnd(New Char() {","c, " "c}) & "]")
    End Function

    ''' <summary>
    ''' Gets all campaign ids in the account.
    ''' </summary>
    ''' <param name="user">The user for which campaigns are retrieved.</param>
    ''' <returns>The list of campaign ids.</returns>
    Private Function GetAllCampaignIds(ByVal user As AdWordsUser) As Long()
      ' Get the CampaignService.
      Dim campaignService As CampaignService = CType(user.GetService( _
          AdWordsService.v201603.CampaignService), CampaignService)

      Dim allCampaigns As New List(Of Long)

      ' Create the selector.
      Dim selector As New Selector
      selector.fields = New String() {Campaign.Fields.Id}

      ' Get all campaigns.
      Dim page As CampaignPage = campaignService.get(selector)

      ' Return the results.
      If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
        For Each campaign As Campaign In page.entries
          allCampaigns.Add(campaign.id)
        Next
      End If
      Return allCampaigns.ToArray
    End Function
  End Class
End Namespace

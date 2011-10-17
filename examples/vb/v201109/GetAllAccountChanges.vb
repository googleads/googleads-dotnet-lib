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

Imports Google.Api.Ads.AdWords.Lib
Imports Google.Api.Ads.AdWords.v201109

Imports System
Imports System.Collections.Generic
Imports System.Text

Namespace Google.Api.Ads.AdWords.Examples.VB.v201109
  ''' <summary>
  ''' This code example gets all account changes between the two dates
  ''' specified, for all campaigns.
  '''
  ''' Tags: CustomerSyncService.get
  ''' </summary>
  Class GetAllAccountChanges
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example gets all account changes between the two dates specified, " & _
            "for all campaigns."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New GetAllAccountChanges
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      Dim customerSyncService As CustomerSyncService = user.GetService( _
          AdWordsService.v201109.CustomerSyncService)

      ' The date time string should be of the form  yyyyMMdd HHmmss zzz
      Dim minDateTime As String = _T("INSERT_START_DATE_TIME_HERE")
      Dim maxDateTime As String = _T("INSERT_END_DATE_TIME_HERE")

      ' Create date time range.
      Dim dateTimeRange As New DateTimeRange
      dateTimeRange.min = minDateTime
      dateTimeRange.max = maxDateTime

      Try
        ' Create selector.
        Dim selector As New CustomerSyncSelector
        selector.dateTimeRange = dateTimeRange
        selector.campaignIds = GetAllCampaignIds(user)

        ' Get all account changes for campaign.
        Dim accountChanges As CustomerChangeData = customerSyncService.get(selector)

        ' Display changes.
        If ((Not accountChanges Is Nothing) AndAlso _
            (Not accountChanges.changedCampaigns Is Nothing)) Then
          Console.WriteLine("Displaying changes up to: {0}", accountChanges.lastChangeTimestamp)

          For Each campaignChanges As CampaignChangeData In accountChanges.changedCampaigns
            Console.WriteLine("Campaign with id ""{0}"" was changed:", campaignChanges.campaignId)
            Console.WriteLine("  Campaign changed status: {0}", _
                campaignChanges.campaignChangeStatus)
            If (campaignChanges.campaignChangeStatus <> ChangeStatus.NEW) Then
              Console.WriteLine("  Added ad extensions: {0}", _
                  GetFormattedList(campaignChanges.addedAdExtensions))
              Console.WriteLine("  Added campaign criteria: {0}", _
                  GetFormattedList(campaignChanges.addedCampaignCriteria))
              If (campaignChanges.campaignTargetingChanged) Then
                Console.WriteLine("  Added campaign targeting: yes")
              Else
                Console.WriteLine("  Added campaign targeting: no")
              End If

              Console.WriteLine("  Deleted ad extensions: {0}", _
                  GetFormattedList(campaignChanges.deletedAdExtensions))
              Console.WriteLine("  Deleted campaign criteria: {0}", _
                  GetFormattedList(campaignChanges.deletedCampaignCriteria))

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
                    Console.WriteLine("    Criteria deleted: {0}", _
                        GetFormattedList(adGroupChanges.deletedCriteria))
                  End If
                Next
              End If
            End If
            Console.WriteLine()
          Next
        Else
          Console.WriteLine("No account changes were found.")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to get account changes. Exception says ""{0}""", ex.Message)
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
        Dim id As Long
        For Each id In ids
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
      Dim campaignService As CampaignService = user.GetService( _
          AdWordsService.v201109.CampaignService)
      Dim allCampaigns As New List(Of Long)

      ' Create the selector.
      Dim selector As New Selector
      selector.fields = New String() {"Id"}

      Dim page As CampaignPage = campaignService.get(selector)

      If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing) AndAlso _
          (page.entries.Length > 0)) Then
        For Each campaign As Campaign In page.entries
          allCampaigns.Add(campaign.id)
        Next
      End If
      Return allCampaigns.ToArray
    End Function
  End Class
End Namespace

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
Imports System.IO
Imports System.Text

Namespace Google.Api.Ads.AdWords.Examples.VB.v201109
  ''' <summary>
  ''' This code example gets the changes in the account during the last 24
  ''' hours.
  '''
  ''' Tags: CustomerSyncService.get
  ''' </summary>
  Public Class GetAccountChanges
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As ExampleBase = New GetAccountChanges
      Console.WriteLine(codeExample.Description)
      Try
        codeExample.Run(New AdWordsUser, codeExample.GetParameters, Console.Out)
      Catch ex As Exception
        Console.WriteLine("An exception occurred while running this code example. {0}", _
            ExampleUtilities.FormatException(ex))
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
    ''' Gets the list of parameter names required to run this code example.
    ''' </summary>
    ''' <returns>
    ''' A list of parameter names for this code example.
    ''' </returns>
    Public Overrides Function GetParameterNames() As String()
      Return New String() {}
    End Function

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="parameters">The parameters for running the code
    ''' example.</param>
    ''' <param name="writer">The stream writer to which script output should be
    ''' written.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser, ByVal parameters As  _
        Dictionary(Of String, String), ByVal writer As TextWriter)
      ' Get the CustomerSyncService.
      Dim customerSyncService As CustomerSyncService = user.GetService( _
          AdWordsService.v201109.CustomerSyncService)

      ' The date time string should be of the form  yyyyMMdd HHmmss zzz.
      Dim minDateTime As String = (DateTime.Now.AddDays(-1).ToUniversalTime.ToString( _
          "yyyyMMdd HHmmss") & " UTC")
      Dim maxDateTime As String = (DateTime.Now.ToUniversalTime.ToString("yyyyMMdd HHmmss") & _
                                   " UTC")

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
          writer.WriteLine("Displaying changes up to: {0}", accountChanges.lastChangeTimestamp)

          For Each campaignChanges As CampaignChangeData In accountChanges.changedCampaigns
            writer.WriteLine("Campaign with id ""{0}"" was changed:", campaignChanges.campaignId)
            writer.WriteLine("  Campaign changed status: {0}", _
                campaignChanges.campaignChangeStatus)
            If (campaignChanges.campaignChangeStatus <> ChangeStatus.NEW) Then
              writer.WriteLine("  Added ad extensions: {0}", _
                  GetFormattedList(campaignChanges.addedAdExtensions))
              writer.WriteLine("  Added campaign criteria: {0}", _
                  GetFormattedList(campaignChanges.addedCampaignCriteria))
              If (campaignChanges.campaignTargetingChanged) Then
                writer.WriteLine("  Added campaign targeting: yes")
              Else
                writer.WriteLine("  Added campaign targeting: no")
              End If

              writer.WriteLine("  Deleted ad extensions: {0}", _
                  GetFormattedList(campaignChanges.deletedAdExtensions))
              writer.WriteLine("  Deleted campaign criteria: {0}", _
                  GetFormattedList(campaignChanges.deletedCampaignCriteria))

              If (Not campaignChanges.changedAdGroups Is Nothing) Then
                For Each adGroupChanges As AdGroupChangeData In campaignChanges.changedAdGroups
                  writer.WriteLine("  Ad group with id ""{0}"" was changed:", _
                      adGroupChanges.adGroupId)
                  writer.WriteLine("    Ad group changed status: {0}", _
                      adGroupChanges.adGroupChangeStatus)
                  If (adGroupChanges.adGroupChangeStatus <> ChangeStatus.NEW) Then
                    writer.WriteLine("    Ads changed: {0}", _
                        GetFormattedList(adGroupChanges.changedAds))
                    writer.WriteLine("    Criteria changed: {0}", _
                        GetFormattedList(adGroupChanges.changedCriteria))
                    writer.WriteLine("    Criteria deleted: {0}", _
                        GetFormattedList(adGroupChanges.deletedCriteria))
                  End If
                Next
              End If
            End If
            writer.WriteLine()
          Next
        Else
          writer.WriteLine("No account changes were found.")
        End If
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to get account changes.", ex)
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
      Dim campaignService As CampaignService = user.GetService( _
          AdWordsService.v201109.CampaignService)

      Dim allCampaigns As New List(Of Long)

      ' Create the selector.
      Dim selector As New Selector
      selector.fields = New String() {"Id"}

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

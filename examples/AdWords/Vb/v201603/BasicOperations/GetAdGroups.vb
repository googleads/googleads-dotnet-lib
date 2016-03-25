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

Namespace Google.Api.Ads.AdWords.Examples.VB.v201603
  ''' <summary>
  ''' This code example illustrates how to retrieve all the ad groups for a
  ''' campaign. To create an ad group, run AddAdGroup.vb.
  ''' </summary>
  Public Class GetAdGroups
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New GetAdGroups
      Console.WriteLine(codeExample.Description)
      Try
        Dim campaignId As Long = Long.Parse("INSERT_CAMPAIGN_ID_HERE")
        codeExample.Run(New AdWordsUser, campaignId)
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
        Return "This code example illustrates how to retrieve all the ad groups for a " & _
            "campaign. To create an ad group, run AddAdGroup.vb."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="campaignId">Id of the campaign for which ad groups are
    ''' retrieved.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal campaignId As Long)
      ' Get the AdGroupService.
      Dim adGroupService As AdGroupService = CType(user.GetService( _
          AdWordsService.v201603.AdGroupService), AdGroupService)

      ' Create the selector.
      Dim selector As New Selector
      selector.fields = New String() {
        AdGroup.Fields.Id, AdGroup.Fields.Name
      }

      selector.predicates = New Predicate() {
        Predicate.Equals(AdGroup.Fields.CampaignId, campaignId)
      }
      selector.ordering = New OrderBy() {OrderBy.Asc(AdGroup.Fields.Name)}
      selector.paging = Paging.Default

      Dim page As New AdGroupPage

      Try
        Do
          ' Get the ad groups.
          page = adGroupService.get(selector)

          ' Display the results.
          If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
            Dim i As Integer = selector.paging.startIndex
            For Each adGroup As AdGroup In page.entries
              Console.WriteLine("{0}) Ad group name is ""{1}"" and id is ""{2}"".", i + 1, _
                  adGroup.name, adGroup.id)
              i += 1
            Next
          End If
          selector.paging.IncreaseOffset()
        Loop While (selector.paging.startIndex < page.totalNumEntries)
        Console.WriteLine("Number of ad groups found: {0}", page.totalNumEntries)
      Catch e As Exception
        Throw New System.ApplicationException("Failed to retrieve ad group(s).", e)
      End Try
    End Sub
  End Class
End Namespace

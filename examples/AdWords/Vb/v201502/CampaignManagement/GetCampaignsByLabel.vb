' Copyright 2015, Google Inc. All Rights Reserved.
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
Imports Google.Api.Ads.AdWords.v201502

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201502
  ''' <summary>
  ''' This code example gets all campaigns with a specific label. To add a
  ''' label to campaigns, run AddCampaignLabels.vb.
  '''
  ''' Tags: CampaignService.get
  ''' </summary>
  Public Class GetCampaignsByLabel
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New GetCampaignsByLabel
      Console.WriteLine(codeExample.Description)
      Dim labelId As Long = Long.Parse("INSERT_LABEL_ID_HERE")

      Try
        codeExample.Run(New AdWordsUser, labelId)
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
        Return "This code example gets all campaigns with a specific label. To add a" & _
            " label to campaigns, run AddCampaignLabels.vb."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="labelId">ID of the label.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal labelId As Long)
      ' Get the CampaignService.
      Dim campaignService As CampaignService = CType(user.GetService( _
          AdWordsService.v201502.CampaignService), AdWords.v201502.CampaignService)

      ' Create the selector.
      Dim selector As New Selector
      selector.fields = New String() {"Id", "Name", "Labels"}

      ' Labels filtering is performed by ID. You can use CONTAINS_ANY to
      ' select campaigns with any of the label IDs, CONTAINS_ALL to select
      ' campaigns with all of the label IDs, or CONTAINS_NONE to select
      ' campaigns with none of the label IDs.
      Dim predicate As New Predicate
      predicate.operator = PredicateOperator.CONTAINS_ANY
      predicate.field = "Labels"
      predicate.values = New String() {labelId.ToString()}
      selector.predicates = New Predicate() {predicate}

      ' Set the selector paging.
      selector.paging = New Paging

      Dim offset As Integer = 0
      Dim pageSize As Integer = 500

      Dim page As New CampaignPage

      Try
        Do
          selector.paging.startIndex = offset
          selector.paging.numberResults = pageSize

          ' Get the campaigns.
          page = campaignService.get(selector)

          ' Display the results.
          If Not (page Is Nothing) AndAlso Not (page.entries Is Nothing) Then
            Dim i As Integer = offset
            For Each campaign As Campaign In page.entries
              Dim labelNames As New List(Of String)
              For Each label As Label In campaign.labels
                labelNames.Add(label.name)
              Next

              Console.WriteLine("{0}) Campaign with id = '{1}', name = '{2}' and labels = " & _
                                "'{3}' was found.", i + 1, campaign.id, campaign.name, _
                  String.Join(", ", labelNames.ToArray()))
              i = i + 1
            Next
          End If
          offset += pageSize
        Loop While offset < page.totalNumEntries
        Console.WriteLine("Number of campaigns found: {0}", page.totalNumEntries)
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to retrieve campaigns by label.", ex)
      End Try
    End Sub
  End Class
End Namespace

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
  ''' This code example removes a campaign by setting the status to 'REMOVED'.
  ''' To get campaigns, run GetCampaigns.vb.
  ''' </summary>
  Public Class RemoveCampaign
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New RemoveCampaign
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
        Return "This code example removes a campaign by setting the status to 'REMOVED'. To " & _
            "get campaigns, run GetCampaigns.vb."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="campaignId">Id of the campaign to be removed.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal campaignId As Long)
      ' Get the CampaignService.
      Dim campaignService As CampaignService = CType(user.GetService( _
          AdWordsService.v201603.CampaignService), CampaignService)

      ' Create campaign with REMOVED status.
      Dim campaign As New Campaign
      campaign.id = campaignId
      campaign.status = CampaignStatus.REMOVED

      ' Create the operation.
      Dim operation As New CampaignOperation
      operation.operand = campaign
      operation.operator = [Operator].SET

      Try
        ' Remove the campaign.
        Dim retVal As CampaignReturnValue = campaignService.mutate( _
            New CampaignOperation() {operation})

        ' Display the results.
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing) AndAlso _
            (retVal.value.Length > 0)) Then
          Dim removedCampaign As Campaign = retVal.value(0)
          Console.WriteLine("Campaign with id = ""{0}"" was renamed to ""{1}"" and removed.", _
              removedCampaign.id, removedCampaign.name)
        Else
          Console.WriteLine("No campaigns were removed.")
        End If
      Catch e As Exception
        Throw New System.ApplicationException("Failed to remove campaigns.", e)
      End Try
    End Sub
  End Class
End Namespace

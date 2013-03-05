' Copyright 2013, Google Inc. All Rights Reserved.
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
Imports Google.Api.Ads.AdWords.v201302

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201302
  ''' <summary>
  ''' This code example sets the enhanced bit in a given campaign. To get campaigns, run
  ''' GetCampaigns.vb.
  '''
  ''' Tags: CampaignService.mutate
  ''' </summary>
  Public Class SetCampaignEnhanced
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New SetCampaignEnhanced
      Console.WriteLine(codeExample.Description)
      Try
        Dim campaignId As Long = Long.Parse("INSERT_CAMPAIGN_ID_HERE")
        codeExample.Run(New AdWordsUser, campaignId)
      Catch ex As Exception
        Console.WriteLine("An exception occurred while running this code example. {0}", _
            ExampleUtilities.FormatException(ex))
      End Try
    End Sub

    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    '''
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example sets the enhanced bit in a given campaign using the forward " & _
            "compatibility map attribute. To get campaigns, run GetCampaigns.vb."
      End Get
    End Property

    ''' <summary>
    ''' Runs the specified user.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="campaignId">Id of the campaign that should be enhanced.
    ''' </param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal campaignId As Long)
      ' Get the CampaignService.
      Dim campaignService As CampaignService = user.GetService( _
          AdWordsService.v201302.CampaignService)

      ' Campaign to be updated with the enhanced value.
      ' Note: After setting the enhanced value to true, setting it back to false
      ' will generate an ApiError.
      Dim campaign As New Campaign()
      campaign.id = campaignId
      campaign.enhanced = True

      ' Create operation.
      Dim operation As New CampaignOperation()
      operation.operator = [Operator].SET
      operation.operand = campaign

      Try
        ' Change campaign.
        Dim result As CampaignReturnValue = campaignService.mutate( _
            New CampaignOperation() {operation})
        ' Display changed campaign.
        If Not result.value Is Nothing Then
          Dim updatedCampaign As Campaign = result.value(0)

          Console.WriteLine("Campaign with ID {0} has been set enhanced to '{1}'.", _
              updatedCampaign.id, updatedCampaign.enhanced)

        Else
          Console.WriteLine("No campaigns were enhanced.")
        End If
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to enhance campaign.", ex)
      End Try
    End Sub
  End Class
End Namespace
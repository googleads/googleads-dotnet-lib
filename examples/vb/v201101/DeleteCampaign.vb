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
Imports Google.Api.Ads.AdWords.v201101

Imports System

Namespace Google.Api.Ads.AdWords.Examples.VB.v201101
  ''' <summary>
  ''' This code example deletes a campaign by setting the status to 'DELETED'.
  ''' To get campaigns, run GetAllCampaigns.vb.
  '''
  ''' Tags: CampaignService.mutate
  ''' </summary>
  Class DeleteCampaign
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example deletes a campaign by setting the status to 'DELETED'. To " & _
            "get campaigns, run GetAllCampaigns.vb."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New DeleteCampaign
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the CampaignService.
      Dim campaignService As CampaignService = user.GetService( _
          AdWordsService.v201101.CampaignService)

      Dim campaignId As Long = Long.Parse(_T("INSERT_CAMPAIGN_ID_HERE"))

      ' Create campaign with DELETED status.
      Dim campaign As New Campaign
      campaign.id = campaignId
      campaign.status = CampaignStatus.DELETED

      ' Create operations.
      Dim operation As New CampaignOperation
      operation.operand = campaign
      operation.operator = [Operator].SET

      Try
        ' Delete campaign.
        Dim retVal As CampaignReturnValue = campaignService.mutate( _
            New CampaignOperation() {operation})

        ' Display campaigns.
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing) AndAlso _
            (retVal.value.Length > 0)) Then
          For Each temp As Campaign In retVal.value
            Console.WriteLine("Campaign with name = ""{0}"" and id = ""{1}"" was deleted.", _
                temp.name, temp.id)
          Next
        Else
          Console.WriteLine("No campaigns were deleted.")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to delete campaigns. Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace

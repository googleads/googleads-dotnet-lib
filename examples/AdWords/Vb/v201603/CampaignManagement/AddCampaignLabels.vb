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
  ''' This code example adds a label to multiple campaigns.
  ''' </summary>
  Public Class AddCampaignLabels
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New AddCampaignLabels
      Console.WriteLine(codeExample.Description)
      Try
        Dim campaignId1 As Long = Long.Parse("INSERT_CAMPAIGN_ID1_HERE")
        Dim campaignId2 As Long = Long.Parse("INSERT_CAMPAIGN_ID2_HERE")
        Dim labelId As Long = Long.Parse("INSERT_LABEL_ID_HERE")

        codeExample.Run(New AdWordsUser, campaignId1, campaignId2, labelId)
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
        Return "This code example adds a label to multiple campaigns."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="campaignId1">Id of the campaign to which labels are
    ''' added.</param>
    ''' <param name="campaignId2">Id of the ad group to which labels are
    ''' added.</param>
    ''' <param name="labelId">ID of the label to apply.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal campaignId1 As Long, _
                   ByVal campaignId2 As Long, ByVal labelId As Long)
      Try
        ' Get the CampaignService.
        Dim campaignService As CampaignService = CType(user.GetService( _
            AdWordsService.v201603.CampaignService), CampaignService)

        ' Create label operations.
        Dim operations As New List(Of CampaignLabelOperation)

        For Each campaignId As Long In New Long() {campaignId1, campaignId2}
          Dim campaignLabel As New CampaignLabel
          campaignLabel.campaignId = campaignId
          campaignLabel.labelId = labelId

          Dim operation As New CampaignLabelOperation
          operation.operand = campaignLabel
          operation.operator = [Operator].ADD

          operations.Add(operation)
        Next

        Dim retval As CampaignLabelReturnValue = campaignService.mutateLabel( _
                operations.ToArray())

        ' Display campaign labels.
        If Not (retval Is Nothing) AndAlso Not (retval.value Is Nothing) Then
          For Each newCampaignLabel As CampaignLabel In retval.value
            Console.WriteLine("Campaign label for campaign ID {0} and label ID {1} was added.\n", _
                newCampaignLabel.campaignId, newCampaignLabel.labelId)
          Next
        Else
          Console.WriteLine("No campaign labels were added.")
        End If
      Catch e As Exception
        Throw New System.ApplicationException("Failed to add campaign labels.", e)
      End Try
    End Sub
  End Class
End Namespace

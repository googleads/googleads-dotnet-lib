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
  ''' This code example gets all campaign targets. To set a campaign target,
  ''' run SetCampaignTargets.vb.
  '''
  ''' Tags: CampaignTargetService.get
  ''' </summary>
  Class GetAllCampaignTargets
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example gets all campaign targets. To set a campaign target, " & _
            "run SetCampaignTargets.vb."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New GetAllCampaignTargets
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the CampaignTargetService.
      Dim campaignTargetService As CampaignTargetService = user.GetService( _
          AdWordsService.v201101.CampaignTargetService)
      Try
        ' Get all campaign targets.
        Dim page As CampaignTargetPage = campaignTargetService.get(New CampaignTargetSelector)

        ' Display campaign targets.
        If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
          For Each targetList As TargetList In page.entries
            Console.WriteLine("Campaign target of type '{0}' was found for campaign with " & _
                "id = '{1}'.", targetList.TargetListType, targetList.campaignId)
          Next
        Else
          Console.WriteLine("No campaign targets were found.")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to get Campaign target(s). Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace

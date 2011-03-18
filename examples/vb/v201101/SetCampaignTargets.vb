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
  ''' This code example adds geo and language targeting to an existing
  ''' campaign. To get a campaign, run GetAllCampaigns.vb.
  '''
  ''' Tags: CampaignTargetService.mutate
  ''' </summary>
  Class SetCampaignTargets
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example adds geo and language targeting to an existing campaign. To " & _
            "get a campaign, run GetAllCampaigns.vb."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New SetCampaignTargets
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

      Dim campaignId As Long = Long.Parse(_T("INSERT_CAMPAIGN_ID_HERE"))

      ' Create language targets.
      Dim langTargetList As New LanguageTargetList
      langTargetList.campaignId = campaignId

      Dim langTarget1 As New LanguageTarget
      langTarget1.languageCode = "fr"

      Dim langTarget2 As New LanguageTarget
      langTarget2.languageCode = "ja"
      langTargetList.targets = New LanguageTarget() {langTarget1, langTarget2}

      ' Create language target set operation.
      Dim langTargetOperation As New CampaignTargetOperation
      langTargetOperation.operator = [Operator].SET
      langTargetOperation.operand = langTargetList

      ' Create geo targets.
      Dim geoTargetList As New GeoTargetList
      geoTargetList.campaignId = campaignId

      Dim geoTarget1 As New CountryTarget
      geoTarget1.countryCode = "US"

      Dim geoTarget2 As New CountryTarget
      geoTarget2.countryCode = "JP"

      ' Create geo target set operation.
      Dim geoTargetOperation As New CampaignTargetOperation
      geoTargetOperation.operator = [Operator].SET
      geoTargetOperation.operand = geoTargetList

      Try
        ' Set campaign targets.
        Dim retVal As CampaignTargetReturnValue = campaignTargetService.mutate( _
            New CampaignTargetOperation() {geoTargetOperation, langTargetOperation})

        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing)) Then
          ' Display campaign targets.
          For Each targetList As TargetList In retVal.value
            Console.WriteLine("Campaign target of type '{0}' was set to Campaign with id " & _
                "= '{1}'.", targetList.TargetListType, targetList.campaignId)
          Next
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to set Campaign target(s). Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace

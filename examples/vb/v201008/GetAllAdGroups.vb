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
Imports Google.Api.Ads.AdWords.v201008

Imports System

Namespace Google.Api.Ads.AdWords.Examples.VB.v201008
  ''' <summary>
  ''' This code example illustrates how to retrieve all the ad groups for a
  ''' campaign. To create an ad group, run AddAdGroup.vb.
  '''
  ''' Tags: AdGroupService.get
  ''' </summary>
  Class GetAllAdGroups
    Inherits SampleBase
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
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New GetAllAdGroups
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the AdGroupService.
      Dim adGroupService As AdGroupService = user.GetService( _
          AdWordsService.v201008.AdGroupService)

      Dim campaignId As Long = Long.Parse(_T("INSERT_CAMPAIGN_ID_HERE"))

      Dim adGroupSelector As New AdGroupSelector
      adGroupSelector.campaignIds = New Long() {campaignId}

      Try
        Dim page As AdGroupPage = adGroupService.get(adGroupSelector)
        If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
          Console.WriteLine("Campaign #{0} has {1} ad group(s).", campaignId, page.entries.Length)

          For Each adGroup As AdGroup In page.entries
            Console.WriteLine("  Ad group name is '{0} and id is {1}.", adGroup.name, adGroup.id)
          Next
        Else
          Console.WriteLine("No ad groups found for campaign #{0}.", campaignId)
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to retrieve ad group(s). Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace

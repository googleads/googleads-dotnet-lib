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
  ''' This code example illustrates how to retrieve all the ad extension
  ''' overrides for an existing campaign. To create an ad extension override
  ''' run AddAdExtensionOverride.vb.
  '''
  ''' Tags: AdExtensionOverrideService.get
  ''' </summary>
  Class GetAllAdExtensionOverrides
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example illustrates how to retrieve all the ad extension overrides " & _
            "for an existing campaign. To create an ad extension override run " & _
            "AddAdExtensionOverride.vb."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New GetAllAdExtensionOverrides
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the AdExtensionOverrideService.
      Dim adExtensionOverrideService As AdExtensionOverrideService = user.GetService( _
          AdWordsService.v201008.AdExtensionOverrideService)

      Dim campaignId As Long = Long.Parse(_T("INSERT_CAMPAIGN_ID_HERE"))

      Dim selector As New AdExtensionOverrideSelector
      selector.campaignIds = New Long() {campaignId}

      Try
        Dim result As AdExtensionOverridePage = adExtensionOverrideService.get(selector)
        If ((Not result Is Nothing) AndAlso (Not result.entries Is Nothing)) Then
          Console.WriteLine("Campaign id '{0}' has {1} ad extension override(s).", _
              campaignId, result.entries.Length)

          For Each adExtension As AdExtensionOverride In result.entries
            Console.WriteLine("  Ad extension override has id = '{0}' and is for ad id = '{1}'.", _
                adExtension.adId, adExtension.adExtension.id)
          Next
        Else
          Console.WriteLine("No ad extension overrides found for campaign id = '{0}'.", campaignId)
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to retrieve ad extension override(s). Exception says ""{0}""", _
            ex.Message)
      End Try
    End Sub
  End Class
End Namespace

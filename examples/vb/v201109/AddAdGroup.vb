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
Imports Google.Api.Ads.AdWords.v201109

Imports System

Namespace Google.Api.Ads.AdWords.Examples.VB.v201109
  ''' <summary>
  ''' This code example illustrates how to create an ad group. To create a
  ''' campaign, run AddCampaign.vb.
  '''
  ''' Tags: AdGroupService.mutate
  ''' </summary>
  Class AddAdGroup
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example illustrates how to create an ad group. To create a campaign," & _
            " run AddCampaign.vb"
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New AddAdGroup
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
          AdWordsService.v201109.AdGroupService)

      Dim campaignId As Long = Long.Parse(_T("INSERT_CAMPAIGN_ID_HERE"))

      Dim adGroup As New AdGroup
      adGroup.name = String.Format("Earth to Mars Cruises #{0}", GetTimeStamp)
      adGroup.status = AdGroupStatus.ENABLED
      adGroup.campaignId = campaignId

      Dim bids As New ManualCPCAdGroupBids

      Dim keywordMaxCpc As New Bid
      keywordMaxCpc.amount = New Money
      keywordMaxCpc.amount.microAmount = 10000000
      bids.keywordMaxCpc = keywordMaxCpc

      adGroup.bids = bids

      Dim operation As New AdGroupOperation
      operation.operator = [Operator].ADD
      operation.operand = adGroup

      Try
        Dim retVal As AdGroupReturnValue = adGroupService.mutate( _
            New AdGroupOperation() {operation})
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing)) Then
          Dim adGroupValue As AdGroup
          For Each adGroupValue In retVal.value
            Console.WriteLine("Ad group with id = '{0}' and name = '{1}' was created.", _
                adGroupValue.id, adGroupValue.name)
          Next
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to create ad group(s). Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace

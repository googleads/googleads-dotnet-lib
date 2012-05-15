' Copyright 2012, Google Inc. All Rights Reserved.
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
Imports Google.Api.Ads.AdWords.v201109_1

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201109_1
  ''' <summary>
  ''' This code example illustrates how to create ad groups. To create
  ''' campaigns, run AddCampaigns.vb.
  '''
  ''' Tags: AdGroupService.mutate
  ''' </summary>
  Public Class AddAdGroups
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As ExampleBase = New AddAdGroups
      Console.WriteLine(codeExample.Description)
      Try
        codeExample.Run(New AdWordsUser, codeExample.GetParameters, Console.Out)
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
        Return "This code example illustrates how to create ad groups. To create campaigns," & _
            " run AddCampaigns.vb"
      End Get
    End Property

    ''' <summary>
    ''' Gets the list of parameter names required to run this code example.
    ''' </summary>
    ''' <returns>
    ''' A list of parameter names for this code example.
    ''' </returns>
    Public Overrides Function GetParameterNames() As String()
      Return New String() {"CAMPAIGN_ID"}
    End Function

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="parameters">The parameters for running the code
    ''' example.</param>
    ''' <param name="writer">The stream writer to which script output should be
    ''' written.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser, ByVal parameters As  _
        Dictionary(Of String, String), ByVal writer As TextWriter)
      ' Get the AdGroupService.
      Dim adGroupService As AdGroupService = user.GetService( _
          AdWordsService.v201109_1.AdGroupService)

      Dim campaignId As Long = Long.Parse(parameters("CAMPAIGN_ID"))

      ' Create the ad group.
      Dim adGroup1 As New AdGroup
      adGroup1.name = String.Format("Earth to Mars Cruises #{0}", ExampleUtilities.GetTimeStamp)
      adGroup1.status = AdGroupStatus.ENABLED
      adGroup1.campaignId = campaignId

      ' Set the ad group bids.
      Dim bid1 As New ManualCPCAdGroupBids

      Dim keywordMaxCpc1 As New Bid
      keywordMaxCpc1.amount = New Money
      keywordMaxCpc1.amount.microAmount = 10000000
      bid1.keywordMaxCpc = keywordMaxCpc1

      adGroup1.bids = bid1

      ' Optional: Set the keywordContentMaxCpc
      Dim keywordContentMaxCpc1 As New Bid
      keywordContentMaxCpc1.amount = New Money()
      keywordContentMaxCpc1.amount.microAmount = 15000000
      bid1.keywordContentMaxCpc = keywordContentMaxCpc1

      ' Create the operation.
      Dim operation1 As New AdGroupOperation
      operation1.operator = [Operator].ADD
      operation1.operand = adGroup1

      ' Create the ad group.
      Dim adGroup2 As New AdGroup
      adGroup2.name = String.Format("Earth to Venus Cruises #{0}", ExampleUtilities.GetTimeStamp)
      adGroup2.status = AdGroupStatus.ENABLED
      adGroup2.campaignId = campaignId

      ' Set the ad group bids.
      Dim bid2 As New ManualCPCAdGroupBids

      Dim keywordMaxCpc2 As New Bid
      keywordMaxCpc2.amount = New Money
      keywordMaxCpc2.amount.microAmount = 20000000
      bid2.keywordMaxCpc = keywordMaxCpc2

      adGroup2.bids = bid2

      ' Optional: Set the keywordContentMaxCpc
      Dim keywordContentMaxCpc2 As New Bid
      keywordContentMaxCpc2.amount = New Money()
      keywordContentMaxCpc2.amount.microAmount = 25000000
      bid2.keywordContentMaxCpc = keywordContentMaxCpc2

      ' Create the operation.
      Dim operation2 As New AdGroupOperation
      operation2.operator = [Operator].ADD
      operation2.operand = adGroup2

      Try
        ' Create the ad group.
        Dim retVal As AdGroupReturnValue = adGroupService.mutate( _
            New AdGroupOperation() {operation1, operation2})

        ' Display the results.
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing) AndAlso _
            (retVal.value.Length > 0)) Then
          For Each newAdGroup As AdGroup In retVal.value
            writer.WriteLine("Ad group with id = '{0}' and name = '{1}' was created.", _
                newAdGroup.id, newAdGroup.name)
          Next
        Else
          writer.WriteLine("No ad groups were created.")
        End If
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to create ad groups.", ex)
      End Try
    End Sub
  End Class
End Namespace

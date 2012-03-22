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
Imports Google.Api.Ads.AdWords.v201109

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201109
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
      codeExample.Run(New AdWordsUser(), codeExample.GetParameters(), Console.Out)
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
          AdWordsService.v201109.AdGroupService)

      Dim campaignId As Long = Long.Parse(parameters("CAMPAIGN_ID"))

      ' Create the ad group.
      Dim adGroup1 As New AdGroup
      adGroup1.name = String.Format("Earth to Mars Cruises #{0}", ExampleUtilities.GetTimeStamp)
      adGroup1.status = AdGroupStatus.ENABLED
      adGroup1.campaignId = campaignId

      ' Set the ad group bids.
      Dim bid1 As New ManualCPMAdGroupBids()

      Dim maxCpm1 As New Bid()
      maxCpm1.amount = New Money()
      maxCpm1.amount.microAmount = 10000000
      bid1.maxCpm = maxCpm1

      adGroup1.bids = bid1

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
      Dim bid2 As New ManualCPMAdGroupBids()

      Dim maxCpm2 As New Bid()
      maxCpm2.amount = New Money()
      maxCpm2.amount.microAmount = 20000000
      bid2.maxCpm = maxCpm2

      adGroup2.bids = bid2

      ' Create the operation.
      Dim operation2 As New AdGroupOperation
      operation2.operator = [Operator].ADD
      operation2.operand = adGroup2

      Try
        ' Create the ad group.
        Dim retVal As AdGroupReturnValue = adGroupService.mutate( _
            New AdGroupOperation() {operation1})

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
        writer.WriteLine("Failed to create ad groups. Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace

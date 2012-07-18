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
    ''' Number of items being added / updated in this code example.
    ''' </summary>
    Const NUM_ITEMS As Integer = 5

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New AddAdGroups
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
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example illustrates how to create ad groups. To create campaigns," & _
            " run AddCampaigns.vb"
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="campaignId">Id of the campaign to which ad groups are
    ''' added.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal campaignId As Long)
      ' Get the AdGroupService.
      Dim adGroupService As AdGroupService = user.GetService( _
          AdWordsService.v201109.AdGroupService)

      Dim operations As New List(Of AdGroupOperation)

      For i As Integer = 1 To NUM_ITEMS
        ' Create the ad group.
        Dim adGroup1 As New AdGroup
        adGroup1.name = String.Format("Earth to Mars Cruises #{0}", ExampleUtilities.GetRandomString)
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

        operations.Add(operation1)
      Next

      Try
        ' Create the ad group.
        Dim retVal As AdGroupReturnValue = adGroupService.mutate(operations.ToArray)

        ' Display the results.
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing) AndAlso _
            (retVal.value.Length > 0)) Then
          For Each newAdGroup As AdGroup In retVal.value
            Console.WriteLine("Ad group with id = '{0}' and name = '{1}' was created.", _
                newAdGroup.id, newAdGroup.name)
          Next
        Else
          Console.WriteLine("No ad groups were created.")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to create ad groups. Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace

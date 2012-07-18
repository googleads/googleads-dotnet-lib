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
Imports Google.Api.Ads.AdWords.v201206

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201206
  ''' <summary>
  ''' This code example updates the bid of a placement. To get placement, run
  ''' GetPlacements.vb.
  '''
  ''' Tags: AdGroupCriterionService.mutate
  ''' </summary>
  Public Class UpdatePlacement
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New UpdatePlacement
      Console.WriteLine(codeExample.Description)
      Try
        Dim adGroupId As Long = Long.Parse("INSERT_ADGROUP_ID_HERE")
        Dim placementId As Long = Long.Parse("INSERT_PLACEMENT_ID_HERE")
        codeExample.Run(New AdWordsUser, adGroupId, placementId)
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
        Return "This code example updates the bid of a placement. To get placement, run " & _
            "GetPlacements.vb."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="adGroupId">Id of the ad group that contains the keyword.
    ''' </param>
    ''' <param name="placementId">Id of the placement to be updated.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long, ByVal placementId As Long)
      ' Get the AdGroupCriterionService.
      Dim adGroupCriterionService As AdGroupCriterionService = user.GetService( _
          AdWordsService.v201206.AdGroupCriterionService)

      ' Since we are not updating any placement-specific fields, it is enough to
      ' create a criterion object.
      Dim criterion As New Criterion
      criterion.id = placementId

      ' Create ad group criterion.
      Dim biddableAdGroupCriterion As New BiddableAdGroupCriterion
      biddableAdGroupCriterion.adGroupId = adGroupId
      biddableAdGroupCriterion.criterion = criterion

      ' Create the bids.
      Dim bids As New ManualCPMAdGroupCriterionBids
      bids.maxCpm = New Bid
      bids.maxCpm.amount = New Money
      bids.maxCpm.amount.microAmount = 1000000

      biddableAdGroupCriterion.bids = bids

      ' Create the operation.
      Dim operation As New AdGroupCriterionOperation
      operation.operator = [Operator].SET
      operation.operand = biddableAdGroupCriterion

      Try
        ' Update the placement.
        Dim retVal As AdGroupCriterionReturnValue = adGroupCriterionService.mutate( _
            New AdGroupCriterionOperation() {operation})

        ' Display the results.
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing) AndAlso _
            (retVal.value.Length > 0)) Then
          Dim adGroupCriterion As AdGroupCriterion = retVal.value(0)
          Dim bidAmount As Long = 0
          If TypeOf adGroupCriterion Is BiddableAdGroupCriterion Then
            bidAmount = TryCast(TryCast(adGroupCriterion, BiddableAdGroupCriterion).bids,  _
                ManualCPCAdGroupCriterionBids).maxCpc.amount.microAmount
          End If
          Console.WriteLine("Placement with ad group id = '{0}', id = '{1}' was updated with " & _
              "bid amount = '{2}' micros.", adGroupCriterion.adGroupId, _
              adGroupCriterion.criterion.id, bidAmount)
        Else
          Console.WriteLine("No placements were updated.")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to update placement. Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace

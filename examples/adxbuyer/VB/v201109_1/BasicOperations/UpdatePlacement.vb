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
      Dim codeExample As ExampleBase = New UpdatePlacement
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser(), codeExample.GetParameters(), Console.Out)
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
    ''' Gets the list of parameter names required to run this code example.
    ''' </summary>
    ''' <returns>
    ''' A list of parameter names for this code example.
    ''' </returns>
    Public Overrides Function GetParameterNames() As String()
      Return New String() {"ADGROUP_ID", "PLACEMENT_ID"}
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
      ' Get the AdGroupCriterionService.
      Dim adGroupCriterionService As AdGroupCriterionService = user.GetService( _
          AdWordsService.v201109_1.AdGroupCriterionService)

      Dim adGroupId As Long = Long.Parse(parameters("ADGROUP_ID"))
      Dim placementId As Long = Long.Parse(parameters("PLACEMENT_ID"))

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
          writer.WriteLine("Placement with ad group id = '{0}', id = '{1}' was updated with " & _
              "bid amount = '{2}' micros.", adGroupCriterion.adGroupId, _
              adGroupCriterion.criterion.id, bidAmount)
        Else
          writer.WriteLine("No placements were updated.")
        End If
      Catch ex As Exception
        writer.WriteLine("Failed to update placement. Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace

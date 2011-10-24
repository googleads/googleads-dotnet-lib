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
  ''' This code example updates the bid of an ad group criterion. To get
  ''' ad group criteria, run GetAllAdGroupCriteria.vb.
  '''
  ''' Tags: AdGroupCriterionService.mutate
  ''' </summary>
  Class UpdateAdGroupCriterion
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example updates the bid of an ad group criterion. To get ad group " & _
            "criteria, run GetAllAdGroupCriteria.vb."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New UpdateAdGroupCriterion
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the AdGroupCriterionService.
      Dim adGroupCriterionService As AdGroupCriterionService = user.GetService( _
          AdWordsService.v201109.AdGroupCriterionService)

      Dim adGroupId As Long = Long.Parse(_T("INSERT_AD_GROUP_ID_HERE"))
      Dim criterionId As Long = Long.Parse(_T("INSERT_CRITERION_ID_HERE"))

      Dim criterion As New Criterion
      criterion.id = criterionId

      ' Create ad group criterion.
      Dim biddableAdGroupCriterion As New BiddableAdGroupCriterion
      biddableAdGroupCriterion.adGroupId = adGroupId
      biddableAdGroupCriterion.criterion = criterion

      ' Create bids.
      Dim bids As New ManualCPCAdGroupCriterionBids
      bids.maxCpc = New Bid
      bids.maxCpc.amount = New Money
      bids.maxCpc.amount.microAmount = 10000

      biddableAdGroupCriterion.bids = bids

      ' Create operations.
      Dim operation As New AdGroupCriterionOperation
      operation.operator = [Operator].SET
      operation.operand = biddableAdGroupCriterion

      Try
        ' Update ad group criteria.
        Dim retVal As AdGroupCriterionReturnValue = adGroupCriterionService.mutate( _
            New AdGroupCriterionOperation() {operation})

        ' Display ad group criteria.
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing)) Then
          Dim adGroupCriterion As AdGroupCriterion
          For Each adGroupCriterion In retVal.value
            Dim bidAmount As Long = 0
            If TypeOf adGroupCriterion Is BiddableAdGroupCriterion Then
              bidAmount = TryCast(TryCast(adGroupCriterion, BiddableAdGroupCriterion).bids,  _
                  ManualCPCAdGroupCriterionBids).maxCpc.amount.microAmount
            End If
            Console.WriteLine("Ad group criterion with ad group id = '{0}', criterion id = " & _
                "'{1}' and type = '{2}' was updated with bid amount = '{3}' micros.", _
                adGroupCriterion.adGroupId, adGroupCriterion.criterion.id, _
                adGroupCriterion.criterion.CriterionType, bidAmount)
          Next
        Else
          Console.WriteLine("No ad group criteria were updated.")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to update ad group criteria. Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace

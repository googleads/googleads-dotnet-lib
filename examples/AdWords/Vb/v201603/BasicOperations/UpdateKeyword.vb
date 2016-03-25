' Copyright 2016, Google Inc. All Rights Reserved.
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

Imports Google.Api.Ads.AdWords.Lib
Imports Google.Api.Ads.AdWords.v201603

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201603
  ''' <summary>
  ''' This code example updates the bid of a keyword. To get keyword, run
  ''' GetKeywords.vb.
  ''' </summary>
  Public Class UpdateKeyword
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New UpdateKeyword
      Console.WriteLine(codeExample.Description)
      Try
        Dim adGroupId As Long = Long.Parse("INSERT_ADGROUP_ID_HERE")
        Dim keywordId As Long = Long.Parse("INSERT_KEYWORD_ID_HERE")
        codeExample.Run(New AdWordsUser, adGroupId, keywordId)
      Catch e As Exception
        Console.WriteLine("An exception occurred while running this code example. {0}", _
            ExampleUtilities.FormatException(e))
      End Try
    End Sub

    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example updates the bid of a keyword. To get keyword, run GetKeywords.vb."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="adGroupId">Id of the ad group that contains the keyword.
    ''' </param>
    ''' <param name="keywordId">Id of the keyword to be updated.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long, ByVal keywordId As Long)
      ' Get the AdGroupCriterionService.
      Dim adGroupCriterionService As AdGroupCriterionService = CType(user.GetService( _
          AdWordsService.v201603.AdGroupCriterionService), AdGroupCriterionService)

      ' Since we are not updating any keyword-specific fields, it is enough to
      ' create a criterion object.
      Dim criterion As New Criterion
      criterion.id = keywordId

      ' Create ad group criterion.
      Dim biddableAdGroupCriterion As New BiddableAdGroupCriterion
      biddableAdGroupCriterion.adGroupId = adGroupId
      biddableAdGroupCriterion.criterion = criterion

      ' Create the bids.
      Dim biddingConfig As New BiddingStrategyConfiguration()
      Dim cpcBid As New CpcBid()
      cpcBid.bid = New Money()
      cpcBid.bid.microAmount = 1000000
      biddingConfig.bids = New Bids() {cpcBid}

      biddableAdGroupCriterion.biddingStrategyConfiguration = biddingConfig

      ' Create the operation.
      Dim operation As New AdGroupCriterionOperation
      operation.operator = [Operator].SET
      operation.operand = biddableAdGroupCriterion

      Try
        ' Update the keyword.
        Dim retVal As AdGroupCriterionReturnValue = adGroupCriterionService.mutate( _
            New AdGroupCriterionOperation() {operation})

        ' Display the results.
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing) AndAlso _
            (retVal.value.Length > 0)) Then
          Dim adGroupCriterion As BiddableAdGroupCriterion = _
              CType(retVal.value(0), BiddableAdGroupCriterion)
          Dim bidAmount As Long = 0L
          For Each bids As Bids In adGroupCriterion.biddingStrategyConfiguration.bids
            If TypeOf bids Is CpcBid Then
              bidAmount = TryCast(bids, CpcBid).bid.microAmount
            End If
          Next

          Console.WriteLine("Keyword with ad group id = '{0}', id = '{1}' was updated with " & _
              "bid amount = '{2}' micros.", adGroupCriterion.adGroupId, _
              adGroupCriterion.criterion.id, bidAmount)
        Else
          Console.WriteLine("No keyword was updated.")
        End If
      Catch e As Exception
        Throw New System.ApplicationException("Failed to update keyword.", e)
      End Try
    End Sub
  End Class
End Namespace

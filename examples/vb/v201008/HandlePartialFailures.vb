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
Imports Google.Api.Ads.AdWords.Util
Imports Google.Api.Ads.AdWords.v201008

Imports System
Imports System.Collections.Generic

Namespace Google.Api.Ads.AdWords.Examples.VB.v201008
  ''' <summary>
  ''' This code example demonstrates how to handle partial failures.
  '''
  ''' Tags: AdGroupCriterionService.mutate
  ''' </summary>
  Class HandlePartialFailures
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example demonstrates how to handle partial failures."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New HandlePartialFailures
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
          AdWordsService.v201008.AdGroupCriterionService)

      ' Set partial failure mode for the service.
      adGroupCriterionService.RequestHeader.partialFailure = True

      Dim adGroupId As Long = Long.Parse(_T("INSERT_ADGROUP_ID_HERE"))

      Dim operations As New List(Of AdGroupCriterionOperation)

      ' Create keywords.
      Dim keywords As String() = New String() {"mars cruise", "inv@lid cruise", "venus cruise", _
          "b(a)d keyword cruise"}

      For Each keywordText As String In keywords
        ' Create keyword
        Dim keyword As New Keyword
        keyword.text = keywordText
        keyword.matchType = KeywordMatchType.BROAD

        ' Create biddable ad group criterion.
        Dim keywordBiddableAdGroupCriterion As New BiddableAdGroupCriterion
        keywordBiddableAdGroupCriterion.adGroupId = adGroupId
        keywordBiddableAdGroupCriterion.criterion = keyword

        ' Create operation.
        Dim keywordAdGroupCriterionOperation As New AdGroupCriterionOperation
        keywordAdGroupCriterionOperation.operand = keywordBiddableAdGroupCriterion
        keywordAdGroupCriterionOperation.operator = [Operator].ADD
        operations.Add(keywordAdGroupCriterionOperation)
      Next
      Try
        ' Add ad group criteria.
        Dim result As AdGroupCriterionReturnValue = adGroupCriterionService.mutate( _
            operations.ToArray)

        ' Display results.
        If ((Not result Is Nothing) AndAlso (Not result.value Is Nothing)) Then
          For Each adGroupCriterionResult As AdGroupCriterion In result.value
            If (Not adGroupCriterionResult.criterion Is Nothing) Then
              Console.WriteLine("Ad group criterion with ad group id '{0}', and criterion id " & _
                  "'{1}', and keyword '{2}' was added.\n", adGroupCriterionResult.adGroupId, _
                  adGroupCriterionResult.criterion.id, _
                  DirectCast(adGroupCriterionResult.criterion, Keyword).text)
            End If
          Next
        Else
          Console.WriteLine("No ad group criteria were added.")
        End If

        If ((Not result Is Nothing) AndAlso (Not result.partialFailureErrors Is Nothing)) Then
          For Each apiError As ApiError In result.partialFailureErrors
            Dim operationIndex As Integer = ErrorUtilities.GetOperationIndex(apiError.fieldPath)
            If (operationIndex <> -1) Then
              Dim adGroupCriterion As AdGroupCriterion = operations(operationIndex).operand
              Console.WriteLine("Ad group criterion with ad group id '{0}' and keyword '{1}' " & _
                  "triggered a failure for the following reason: '{2}'.\n", _
                  adGroupCriterion.adGroupId, _
                  DirectCast(adGroupCriterion.criterion, Keyword).text, _
                  apiError.errorString)
            Else
              Console.WriteLine("A failure for the following reason: '{0}' has occurred.\n", _
                  apiError.errorString)
            End If
          Next
        End If
      Catch e As Exception
        Console.WriteLine("Failed to add keyword(s) in partial failure mode. Exception " & _
            "says '{0}'", e.Message)
      End Try
    End Sub
  End Class
End Namespace

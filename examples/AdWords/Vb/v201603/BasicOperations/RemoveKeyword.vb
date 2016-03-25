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
  ''' This code example removes a keyword using the 'REMOVE' operator. To get
  ''' keywords, run GetKeywords.vb.
  ''' </summary>
  Public Class RemoveKeyword
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New RemoveKeyword
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
        Return "This code example removes a keyword using the 'REMOVE' operator. To get " & _
            "keywords, run GetKeywords.vb."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="adGroupId">Id of the ad group that contains the keyword.
    ''' </param>
    ''' <param name="keywordId">Id of the keyword to be removed.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long, ByVal keywordId As Long)
      ' Get the AdGroupCriterionService.
      Dim adGroupCriterionService As AdGroupCriterionService = CType(user.GetService( _
          AdWordsService.v201603.AdGroupCriterionService), AdGroupCriterionService)

      ' Create base class criterion to avoid setting keyword-specific
      ' fields.
      Dim criterion As New Criterion
      criterion.id = keywordId

      ' Create the ad group criterion.
      Dim adGroupCriterion As New BiddableAdGroupCriterion
      adGroupCriterion.adGroupId = adGroupId
      adGroupCriterion.criterion = criterion

      ' Create the operation.
      Dim operation As New AdGroupCriterionOperation
      operation.operand = adGroupCriterion
      operation.operator = [Operator].REMOVE

      Try
        ' Remove the keyword.
        Dim retVal As AdGroupCriterionReturnValue = adGroupCriterionService.mutate( _
            New AdGroupCriterionOperation() {operation})

        ' Display the results.
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing) AndAlso _
            (retVal.value.Length > 0)) Then
          Dim removedKeyword As AdGroupCriterion = retVal.value(0)
          Console.WriteLine("Keyword with ad group id = ""{0}"" and id = ""{1}"" was " & _
                "removed.", removedKeyword.adGroupId, removedKeyword.criterion.id)
        Else
          Console.WriteLine("No keywords were removed.")
        End If
      Catch e As Exception
        Throw New System.ApplicationException("Failed to remove keywords.", e)
      End Try
    End Sub
  End Class
End Namespace

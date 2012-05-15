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
  ''' This code example gets all keywords in an account. To add keywords, run
  ''' AddKeywords.vb.
  '''
  ''' Tags: AdGroupCriterionService.get
  ''' </summary>
  Public Class GetKeywords
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As ExampleBase = New GetKeywords
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
        Return "This code example gets all keywords in an account. To add keywords, run " & _
            "AddKeywords.vb."
      End Get
    End Property

    ''' <summary>
    ''' Gets the list of parameter names required to run this code example.
    ''' </summary>
    ''' <returns>
    ''' A list of parameter names for this code example.
    ''' </returns>
    Public Overrides Function GetParameterNames() As String()
      Return New String() {}
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

      ' Create a selector.
      Dim selector As New Selector
      selector.fields = New String() {"Id", "AdGroupId", "KeywordText"}

      ' Select only keywords.
      Dim predicate As New Predicate
      predicate.field = "CriteriaType"
      predicate.operator = PredicateOperator.EQUALS
      predicate.values = New String() {"KEYWORD"}
      selector.predicates = New Predicate() {predicate}

      ' Set the selector paging.
      selector.paging = New Paging

      Dim offset As Integer = 0
      Dim pageSize As Integer = 500

      Dim page As New AdGroupCriterionPage

      Try
        Do
          selector.paging.startIndex = offset
          selector.paging.numberResults = pageSize

          ' Get the keywords.
          page = adGroupCriterionService.get(selector)

          ' Display the results.
          If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
            Dim i As Integer = offset

            For Each adGroupCriterion As AdGroupCriterion In page.entries
              Dim isNegative As Boolean = TypeOf adGroupCriterion Is NegativeAdGroupCriterion

              ' If you are retrieving multiple type of criteria, then you may
              ' need to check for
              '
              ' if (adGroupCriterion is Keyword) { ... }
              '
              ' to identify the criterion type.
              Dim keyword As Keyword = adGroupCriterion.criterion
              If isNegative Then
                writer.WriteLine("{0}) Negative keyword with ad group ID = '{1}', keyword ID " & _
                    "= '{2}', and text = '{3}' was found.", i, adGroupCriterion.adGroupId, _
                    keyword.id, keyword.text)
              Else
                writer.WriteLine("{0}) Keyword with ad group ID = '{1}', keyword ID = '{2}', " & _
                    "text = '{3}' and matchType = '{4} was found.", i, adGroupCriterion.adGroupId, _
                    keyword.id, keyword.text, keyword.matchType)
              End If
              i += 1
            Next
          End If
          offset = offset + pageSize
        Loop While (offset < page.totalNumEntries)
        writer.WriteLine("Number of keywords found: {0}", page.totalNumEntries)
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to retrieve keywords.", ex)
      End Try
    End Sub
  End Class
End Namespace

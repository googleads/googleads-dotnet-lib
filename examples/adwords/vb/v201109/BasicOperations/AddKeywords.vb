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
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201109
  ''' <summary>
  ''' This code example adds a keyword to an ad group. To get ad groups, run
  ''' GetAdGroups.vb.
  '''
  ''' Tags: AdGroupCriterionService.mutate
  ''' </summary>
  Public Class AddKeywords
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As ExampleBase = New AddKeywords
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
        Return "This code example adds a keyword to an ad group. To get ad groups, run " & _
            "GetAdGroups.vb."
      End Get
    End Property

    ''' <summary>
    ''' Gets the list of parameter names required to run this code example.
    ''' </summary>
    ''' <returns>
    ''' A list of parameter names for this code example.
    ''' </returns>
    Public Overrides Function GetParameterNames() As String()
      Return New String() {"ADGROUP_ID"}
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
          AdWordsService.v201109.AdGroupCriterionService)

      Dim adGroupId As Long = Long.Parse(parameters("ADGROUP_ID"))

      ' Create the keyword.
      Dim keyword1 As New Keyword
      keyword1.text = "mars cruise"
      keyword1.matchType = KeywordMatchType.BROAD

      ' Create the biddable ad group criterion.
      Dim keywordCriterion1 As New BiddableAdGroupCriterion
      keywordCriterion1.adGroupId = adGroupId
      keywordCriterion1.criterion = keyword1

      ' Optional: Set the user status.
      keywordCriterion1.userStatus = UserStatus.PAUSED

      ' Optional: Set the keyword destination url.
      keywordCriterion1.destinationUrl = "http://example.com/mars/cruise"

      ' Create the keyword.
      Dim keyword2 As New Keyword
      keyword2.text = "mars chocolate"
      keyword2.matchType = KeywordMatchType.EXACT

      ' Create the biddable ad group criterion.
      Dim keywordCriterion2 As New NegativeAdGroupCriterion
      keywordCriterion2.adGroupId = adGroupId
      keywordCriterion2.criterion = keyword2

      ' Create the operations.
      Dim keywordOperation1 As New AdGroupCriterionOperation
      keywordOperation1.operator = [Operator].ADD
      keywordOperation1.operand = keywordCriterion1

      Dim keywordOperation2 As New AdGroupCriterionOperation
      keywordOperation2.operator = [Operator].ADD
      keywordOperation2.operand = keywordCriterion2

      Try
        ' Create the keywords.
        Dim retVal As AdGroupCriterionReturnValue = adGroupCriterionService.mutate( _
            New AdGroupCriterionOperation() {keywordOperation1, keywordOperation2})

        ' Display the results.
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing)) Then
          For Each adGroupCriterion As AdGroupCriterion In retVal.value
            ' If you are adding multiple type of criteria, then you may need to
            ' check for
            '
            ' if (adGroupCriterion is Keyword) { ... }
            '
            ' to identify the criterion type.
            writer.WriteLine("Keyword with ad group id = '{0}, keyword id = '{1}, text = " & _
                "'{2}' and match type = '{3}' was created.", adGroupCriterion.adGroupId, _
                adGroupCriterion.criterion.id, TryCast(adGroupCriterion.criterion, Keyword).text, _
                TryCast(adGroupCriterion.criterion, Keyword).matchType)
          Next
        Else
          writer.WriteLine("No keywords were added.")
        End If
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to create keywords.", ex)
      End Try
    End Sub
  End Class
End Namespace

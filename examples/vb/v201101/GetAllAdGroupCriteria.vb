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
Imports Google.Api.Ads.AdWords.v201101

Imports System

Namespace Google.Api.Ads.AdWords.Examples.VB.v201101
  ''' <summary>
  ''' This code example gets all ad group criteria in an account. To add ad
  ''' group criteria, run AddAdGroupCriteria.vb.
  '''
  ''' Tags: AdGroupCriterionService.get
  ''' </summary>
  Class GetAllAdGroupCriteria
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example gets all ad group criteria in an account. To add ad group " & _
            "criteria, run AddAdGroupCriteria.vb."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New GetAllAdGroupCriteria
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      Dim adGroupCriterionService As AdGroupCriterionService = user.GetService( _
          AdWordsService.v201101.AdGroupCriterionService)

      ' Create a selector.
      Dim selector As New Selector
      selector.fields = New String() {"Id", "AdGroupId", "KeywordText", "PlacementUrl"}

      Try
        Dim adGroupCriterionPage As AdGroupCriterionPage = adGroupCriterionService.get(selector)
        If ((Not adGroupCriterionPage Is Nothing) AndAlso _
            (Not adGroupCriterionPage.entries Is Nothing)) Then
          For Each adGroupCriterion As AdGroupCriterion In adGroupCriterionPage.entries
            Dim isNegative As Boolean = TypeOf adGroupCriterion Is NegativeAdGroupCriterion

            If TypeOf adGroupCriterion.criterion Is Keyword Then
              Dim keyword As Keyword = adGroupCriterion.criterion
              If isNegative Then
                Console.WriteLine("Negative keyword ad group criterion with ad group ID = " & _
                    "'{0}', criterion ID = '{1}', and text = '{2}' was found.", _
                    adGroupCriterion.adGroupId, keyword.id, keyword.text)
              Else
                Console.WriteLine("Keyword ad group criterion with ad group ID = '{0}', " & _
                    "criterion ID = '{1}', text = '{2}' and matchType = '{3} was found.", _
                    adGroupCriterion.adGroupId, keyword.id, keyword.text, keyword.matchType)
              End If
            ElseIf TypeOf adGroupCriterion.criterion Is Placement Then
              Dim placement As Placement = adGroupCriterion.criterion
              If isNegative Then
                Console.WriteLine("Negative placement ad group criterion with ad group ID = " & _
                    "'{0}', criterion ID = '{1}' and url = '{2}' was found.", _
                    adGroupCriterion.adGroupId, placement.id, placement.url)
              Else
                Console.WriteLine("Placement ad group criterion with ad group ID = '{0}', " & _
                    "criterion ID = '{1}' and url = '{2}' was found.", _
                    adGroupCriterion.adGroupId, placement.id, placement.url)
              End If
            End If
          Next
        Else
          Console.WriteLine("No ad group criteria found.")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to retrieve criteria. Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace

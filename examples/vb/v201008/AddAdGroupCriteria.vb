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
Imports Google.Api.Ads.AdWords.v201008

Imports System

Namespace Google.Api.Ads.AdWords.Examples.VB.v201008
  ''' <summary>
  ''' This code example adds a keyword and a placement to an ad group. To get
  ''' ad groups, run GetAllAdGroups.vb.
  '''
  ''' Tags: AdGroupCriterionService.mutate
  ''' </summary>
  Class AddAdGroupCriteria
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example adds a keyword and a placement to an ad group. To get ad " & _
            "groups, run GetAllAdGroups.vb."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New AddAdGroupCriteria
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

      Dim adGroupId As Long = Long.Parse(_T("INSERT_AD_GROUP_ID_HERE"))

      ' Create keyword.
      Dim keyword As New Keyword
      keyword.text = "mars cruise"
      keyword.matchType = KeywordMatchType.BROAD

      ' Create biddable ad group criterion.
      Dim keywordCriterion As AdGroupCriterion = New BiddableAdGroupCriterion
      keywordCriterion.adGroupId = adGroupId
      keywordCriterion.criterion = keyword

      ' Create placement.
      Dim placement As New Placement
      placement.url = "http://mars.google.com"

      ' Create biddable ad group criterion.
      Dim placementCriterion As AdGroupCriterion = New BiddableAdGroupCriterion
      placementCriterion.adGroupId = adGroupId
      placementCriterion.criterion = placement

      ' Create operations.
      Dim keywordOperation As New AdGroupCriterionOperation
      keywordOperation.operator = [Operator].ADD
      keywordOperation.operand = keywordCriterion

      Dim placementOperation As New AdGroupCriterionOperation
      placementOperation.operator = [Operator].ADD
      placementOperation.operand = placementCriterion

      Try
        Dim retVal As AdGroupCriterionReturnValue = adGroupCriterionService.mutate( _
            New AdGroupCriterionOperation() {keywordOperation, placementOperation})

        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing)) Then
          Dim adGroupCriterion As AdGroupCriterion
          For Each adGroupCriterion In retVal.value
            Console.WriteLine("Ad group criterion with ad group id = '{0}, criterion id = " & _
                "'{1}' and type = '{2}' was created.", adGroupCriterion.adGroupId, _
                adGroupCriterion.criterion.id, adGroupCriterion.criterion.CriterionType)
          Next
        Else
          Console.WriteLine("No ad group criteria were added.\n")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to create ad group criteria. Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace

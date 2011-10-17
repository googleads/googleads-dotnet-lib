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
  ''' This code example gets all ad parameters in an ad group. To set ad params,
  ''' run SetAdParams.vb.
  '''
  ''' Tags: AdParamService.get
  ''' </summary>
  Class GetAllAdParams
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example gets all ad parameters in an ad group. To set ad params," & _
            " run SetAdParams.vb."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New GetAllAdParams
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the AdParamService.
      Dim adParamService As AdParamService = user.GetService(AdWordsService.v201109.AdParamService)

      Dim adGroupId As Long = Long.Parse(_T("INSERT_ADGROUP_ID_HERE"))

      ' Create a selector.
      Dim selector As New Selector
      selector.fields = New String() {"AdGroupId", "CriterionId", "InsertionText", "ParamIndex"}

      ' Set a filter condition.
      Dim predicate As New Predicate
      predicate.field = "AdGroupId"
      predicate.operator = PredicateOperator.EQUALS
      predicate.values = New String() {adGroupId.ToString}
      selector.predicates = New Predicate() {predicate}

      Try
        Dim page As AdParamPage = adParamService.get(selector)
        If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
          Dim adParam As AdParam
          For Each adParam In page.entries
            Console.WriteLine("Ad param with text '{0}' was found for criterion with id '{1}' " & _
                "and ad group id '{2}'.", adParam.insertionText, adParam.criterionId, _
                adParam.adGroupId)
          Next
        Else
          Console.WriteLine("No ad parameters found for adgroup #{0}.", adGroupId)
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to retrieve ad parameters(s). Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace

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
  ''' This code example retrieves all third party redirect ads given an existing
  ''' ad group. To add ads to an existing ad group, run
  ''' AddThirdPartyRedirectAd.vb.
  '''
  ''' Tags: AdGroupAdService.get
  ''' </summary>
  Public Class GetThirdPartyRedirectAds
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As ExampleBase = New GetThirdPartyRedirectAds
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser(), codeExample.GetParameters(), Console.Out)
    End Sub

    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example retrieves all third party redirect ads given an existing ad " & _
            "group. To add third party redirect ads to an existing ad group, run " & _
            "AddThirdPartyRedirectAd.vb."
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
      ' Get the AdGroupAdService.
      Dim service As AdGroupAdService = user.GetService(AdWordsService.v201109_1.AdGroupAdService)

      Dim adGroupId As Long = Long.Parse(parameters("ADGROUP_ID"))

      ' Create a selector.
      Dim selector As New Selector
      selector.fields = New String() {"Id", "Status", "Url", "DisplayUrl", "RichMediaAdSnippet"}

      ' Set the sort order.
      Dim orderBy As New OrderBy
      orderBy.field = "Id"
      orderBy.sortOrder = SortOrder.ASCENDING
      selector.ordering = New OrderBy() {orderBy}

      ' Restrict the fetch to only the selected ad group id.
      Dim adGroupPredicate As New Predicate
      adGroupPredicate.field = "AdGroupId"
      adGroupPredicate.operator = PredicateOperator.EQUALS
      adGroupPredicate.values = New String() {adGroupId.ToString}

      ' Retrieve only third party redirect ads.
      Dim typePredicate As New Predicate
      typePredicate.field = "AdType"
      typePredicate.operator = PredicateOperator.EQUALS
      typePredicate.values = New String() {"THIRD_PARTY_REDIRECT_AD"}

      ' By default disabled ads aren't returned by the selector. To return them
      ' include the DISABLED status in the statuses field.
      Dim statusPredicate As New Predicate
      statusPredicate.field = "Status"
      statusPredicate.operator = PredicateOperator.IN

      statusPredicate.values = New String() {AdGroupAdStatus.ENABLED.ToString, _
          AdGroupAdStatus.PAUSED.ToString, AdGroupAdStatus.DISABLED.ToString}

      selector.predicates = New Predicate() {adGroupPredicate, statusPredicate, typePredicate}

      ' Select the selector paging.
      selector.paging = New Paging

      Dim offset As Integer = 0
      Dim pageSize As Integer = 500

      Dim page As New AdGroupAdPage

      Try
        Do
          selector.paging.startIndex = offset
          selector.paging.numberResults = pageSize

          ' Get the third party redirect ads.
          page = service.get(selector)

          ' Display the results.
          If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
            Dim i As Integer = offset

            For Each adGroupAd As AdGroupAd In page.entries
              Dim thirdPartyRedirectAd As ThirdPartyRedirectAd = adGroupAd.ad
              writer.WriteLine("{0}) Ad id is {1} and status is {2}", i, thirdPartyRedirectAd.id, _
                  adGroupAd.status)
              writer.WriteLine("  Url: {0}\n  Display Url: {1}\n  Snippet:{2}", _
                  thirdPartyRedirectAd.url, thirdPartyRedirectAd.displayUrl, _
                  thirdPartyRedirectAd.snippet)
            Next
            i += 1
          End If
          offset = offset + pageSize
        Loop While (offset < page.totalNumEntries)
        writer.WriteLine("Number of third party redirect ads found: {0}", page.totalNumEntries)
      Catch ex As Exception
        writer.WriteLine("Failed to get third party redirect ads. Exception says ""{0}""", _
            ex.Message)
      End Try
    End Sub
  End Class
End Namespace

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
Imports System.Web

Namespace Google.Api.Ads.AdWords.Examples.VB.v201603
  ''' <summary>
  ''' This code example adds keywords to an ad group. To get ad groups, run
  ''' GetAdGroups.vb.
  ''' </summary>
  Public Class AddKeywords
    Inherits ExampleBase
    ''' <summary>
    ''' Items being added in this code example.
    ''' </summary>
    ReadOnly KEYWORDS As String() = New String() {"mars cruise", "space hotel"}

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New AddKeywords
      Console.WriteLine(codeExample.Description)
      Try
        Dim adGroupId As Long = Long.Parse("INSERT_ADGROUP_ID_HERE")
        codeExample.Run(New AdWordsUser, adGroupId)
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
        Return "This code example adds keywords to an ad group. To get ad groups, run " & _
            "GetAdGroups.vb."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="adGroupId">Id of the ad group to which keywords are added.
    ''' </param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long)
      ' Get the AdGroupCriterionService.
      Dim adGroupCriterionService As AdGroupCriterionService = CType(user.GetService( _
          AdWordsService.v201603.AdGroupCriterionService), AdGroupCriterionService)

      Dim operations As New List(Of AdGroupCriterionOperation)

      For Each keywordText As String In KEYWORDS
        ' Create the keyword.
        Dim keyword As New Keyword
        keyword.text = keywordText
        keyword.matchType = KeywordMatchType.BROAD

        ' Create the biddable ad group criterion.
        Dim keywordCriterion As New BiddableAdGroupCriterion
        keywordCriterion.adGroupId = adGroupId
        keywordCriterion.criterion = keyword

        ' Optional: Set the user status.
        keywordCriterion.userStatus = UserStatus.PAUSED

        ' Optional: Set the keyword destination url.
        keywordCriterion.finalUrls = New UrlList()
        keywordCriterion.finalUrls.urls = New String() {"http://example.com/mars/cruise/?kw=" & _
             HttpUtility.UrlEncode(keywordText)}

        ' Create the operations.
        Dim operation As New AdGroupCriterionOperation
        operation.operator = [Operator].ADD
        operation.operand = keywordCriterion

        operations.Add(operation)
      Next

      Try
        ' Create the keywords.
        Dim retVal As AdGroupCriterionReturnValue = adGroupCriterionService.mutate( _
            operations.ToArray())

        ' Display the results.
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing)) Then
          For Each adGroupCriterion As AdGroupCriterion In retVal.value
            ' If you are adding multiple type of criteria, then you may need to
            ' check for
            '
            ' if (adGroupCriterion is Keyword) { ... }
            '
            ' to identify the criterion type.
            Console.WriteLine("Keyword with ad group id = '{0}, keyword id = '{1}, text = " & _
                "'{2}' and match type = '{3}' was created.", adGroupCriterion.adGroupId, _
                adGroupCriterion.criterion.id, TryCast(adGroupCriterion.criterion, Keyword).text, _
                TryCast(adGroupCriterion.criterion, Keyword).matchType)
          Next
        Else
          Console.WriteLine("No keywords were added.")
        End If
      Catch e As Exception
        Throw New System.ApplicationException("Failed to create keywords.", e)
      End Try
    End Sub
  End Class
End Namespace

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
  ''' This code example illustrates how to retrieve all the ad groups for a
  ''' campaign. To create an ad group, run AddAdGroup.vb.
  '''
  ''' Tags: AdGroupService.get
  ''' </summary>
  Public Class GetAdGroups
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As ExampleBase = New GetAdGroups
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
        Return "This code example illustrates how to retrieve all the ad groups for a " & _
            "campaign. To create an ad group, run AddAdGroup.vb."
      End Get
    End Property

    ''' <summary>
    ''' Gets the list of parameter names required to run this code example.
    ''' </summary>
    ''' <returns>
    ''' A list of parameter names for this code example.
    ''' </returns>
    Public Overrides Function GetParameterNames() As String()
      Return New String() {"CAMPAIGN_ID"}
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
      ' Get the AdGroupService.
      Dim adGroupService As AdGroupService = user.GetService( _
          AdWordsService.v201109_1.AdGroupService)

      Dim campaignId As Long = Long.Parse(parameters("CAMPAIGN_ID"))

      ' Create the selector.
      Dim selector As New Selector
      selector.fields = New String() {"Id", "Name"}

      ' Create the filters.
      Dim predicate As New Predicate
      predicate.field = "CampaignId"
      predicate.operator = PredicateOperator.EQUALS
      predicate.values = New String() {campaignId.ToString}
      selector.predicates = New Predicate() {predicate}

      ' Set the selector paging.
      selector.paging = New Paging

      Dim offset As Integer = 0
      Dim pageSize As Integer = 500

      Dim page As New AdGroupPage

      Try
        Do
          selector.paging.startIndex = offset
          selector.paging.numberResults = pageSize

          ' Get the ad groups.
          page = adGroupService.get(selector)

          ' Display the results.
          If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
            Dim i As Integer = offset
            For Each adGroup As AdGroup In page.entries
              writer.WriteLine("{0}) Ad group name is ""{1}"" and id is ""{2}"".", i, _
                  adGroup.name, adGroup.id)
              i += 1
            Next
          End If
          offset = offset + pageSize
        Loop While (offset < page.totalNumEntries)
        writer.WriteLine("Number of ad groups found: {0}", page.totalNumEntries)
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to retrieve ad group(s).", ex)
      End Try
    End Sub
  End Class
End Namespace

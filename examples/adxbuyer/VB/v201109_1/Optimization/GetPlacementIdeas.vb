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
  ''' This code example retrieves urls that have content keywords related to a
  ''' given website.
  '''
  ''' Tags: TargetingIdeaService.get
  ''' </summary>
  Public Class GetPlacementIdeas
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New GetPlacementIdeas
      Console.WriteLine(codeExample.Description)
      Try
        codeExample.Run(New AdWordsUser)
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
        Return "This code example retrieves urls that have content keywords related to a " & _
            "given website."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    Public Sub Run(ByVal user As AdWordsUser)
      ' Get the TargetingIdeaService.
      Dim targetingIdeaService As TargetingIdeaService = user.GetService( _
          AdWordsService.v201109_1.TargetingIdeaService)

      ' Create seed url.
      Dim url As String = "mars.google.com"

      ' Create the selector.
      Dim selector As New TargetingIdeaSelector
      selector.requestType = RequestType.IDEAS
      selector.ideaType = IdeaType.PLACEMENT
      selector.requestedAttributeTypes = New AttributeType() {AttributeType.CRITERION, _
          AttributeType.PLACEMENT_TYPE}

      ' Create related to url search parameter.
      Dim relatedToUrlSearchParameter As New RelatedToUrlSearchParameter
      relatedToUrlSearchParameter.urls = New String() {url}
      relatedToUrlSearchParameter.includeSubUrls = False
      selector.searchParameters = New SearchParameter() {relatedToUrlSearchParameter}

      ' Select selector paging.
      selector.paging = New Paging

      Dim offset As Integer = 0
      Dim pageSize As Integer = 500

      Dim page As New TargetingIdeaPage

      Try
        Do
          selector.paging.startIndex = offset
          selector.paging.numberResults = pageSize

          ' Get placement ideas.
          page = targetingIdeaService.get(selector)

          ' Display the results.
          If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
            Dim i As Integer = offset

            For Each idea As TargetingIdea In page.entries
              For Each entry As Type_AttributeMapEntry In idea.data
                If (entry.key = AttributeType.CRITERION) Then
                  Dim placementAttribute As CriterionAttribute = entry.value
                  Dim placement As Placement = placementAttribute.value
                  Console.WriteLine("{0}) Related placement urls were found at '{1}'.", _
                      i, placement.url)
                End If
              Next
            Next
            i += 1
          End If
          offset = offset + pageSize
        Loop While (offset < page.totalNumEntries)
        Console.WriteLine("Number of related placements found: {0}", page.totalNumEntries)
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to retrieve related placements.", ex)
      End Try
    End Sub
  End Class
End Namespace

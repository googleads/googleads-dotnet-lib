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
  ''' This code example gets all conversion trackers in the account. To add
  ''' conversions, run AddConversionTracker.vb.
  '''
  ''' Tags: ConversionTrackerService.get
  ''' </summary>
  Friend Class GetAllConversionTrackers
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    '''
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example gets all conversion trackers in the account. To add " & _
            "conversion tracker, run AddConversionTracker.vb."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New GetAllConversionTrackers
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the ConversionTrackerService.
      Dim conversionTrackerService As ConversionTrackerService = user.GetService( _
          AdWordsService.v201101.ConversionTrackerService)

      Dim selector As New Selector
      selector.fields = New String() {"Id", "Name", "Status", "Category"}

      Dim orderBy As New OrderBy
      orderBy.field = "Name"
      orderBy.sortOrder = SortOrder.ASCENDING

      selector.ordering = New OrderBy() {orderBy}
      Try
        ' Get the conversions.
        Dim page As ConversionTrackerPage = conversionTrackerService.get(selector)
        If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing) AndAlso _
            page.entries.Length > 0) Then
          For Each conversionTracker As AdWordsConversionTracker In page.entries
            Console.WriteLine("Conversion tracker with id '{0}', name '{1}', status '{2}', " & _
                "category '{3}' was found. With code snippet: \n{4}\n", conversionTracker.id, _
                conversionTracker.name, conversionTracker.status, conversionTracker.category, _
                conversionTracker.snippet)
          Next
        Else
          Console.WriteLine("No conversion trackers were found.")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to get all conversion trackers. Exception says ""{0}""", _
            ex.Message)
      End Try
    End Sub
  End Class
End Namespace

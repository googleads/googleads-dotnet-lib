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
  ''' This code example gets location criteria by name.
  '''
  ''' Tags: LocationCriterionService.get
  ''' </summary>
  Class LookupLocation
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As ExampleBase = New LookupLocation
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser(), codeExample.GetParameters(), Console.Out)
    End Sub

    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example gets location criteria by name."
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
      ' Get the LocationCriterionService.
      Dim locationCriterionService As LocationCriterionService = user.GetService( _
          AdWordsService.v201109.LocationCriterionService)

      Dim locationNames As String() = New String() {"Paris", "Quebec", "Spain", "Deutschland"}

      Dim selector As New Selector
      selector.fields = New String() {"Id", "LocationName", "CanonicalName", "DisplayType", _
          "ParentLocations", "Reach"}

      ' Location names must match exactly, only EQUALS and IN are supported.
      Dim predicate1 As New Predicate
      predicate1.field = "LocationName"
      predicate1.operator = PredicateOperator.IN
      predicate1.values = locationNames

      ' Set the locale of the returned location names.
      Dim predicate2 As New Predicate
      predicate2.field = "Locale"
      predicate2.operator = PredicateOperator.EQUALS
      predicate2.values = New String() {"en"}

      selector.predicates = New Predicate() {predicate1, predicate2}

      Try
        ' Make the get request.
        Dim locationCriteria As LocationCriterion() = locationCriterionService.get(selector)

        ' Display the resulting location criteria.
        For Each locationCriterion As LocationCriterion In locationCriteria
          Dim parentLocations As String = ""
          If ((Not locationCriterion.location Is Nothing) AndAlso ( _
              Not locationCriterion.location.parentLocations Is Nothing)) Then
            For Each location As Location In locationCriterion.location.parentLocations
              parentLocations = (parentLocations & Me.GetLocationString(location) & ", ")
            Next
            parentLocations.TrimEnd(New Char() {","c, " "c})
          Else
            parentLocations = "N/A"
          End If
          writer.WriteLine("The search term '{0}' returned the location '{1}' of type '{2}' " & _
              "with parent locations '{3}' and reach '{4}'.", locationCriterion.searchTerm, _
              locationCriterion.location.locationName, locationCriterion.location.displayType, _
              parentLocations, locationCriterion.reach)
        Next
      Catch ex As Exception
        writer.WriteLine("Failed to get location criteria. Exception says ""{0}""", ex.Message)
      End Try
    End Sub

    ''' <summary>
    ''' Gets a string representation for a location.
    ''' </summary>
    ''' <param name="location">The location</param>
    ''' <returns></returns>
    Public Function GetLocationString(ByVal location As Location) As String
      Return String.Format("{0} ({1})", location.locationName, location.displayType)
    End Function
  End Class
End Namespace

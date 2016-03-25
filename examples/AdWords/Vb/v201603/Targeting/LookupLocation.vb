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

Namespace Google.Api.Ads.AdWords.Examples.VB.v201603
  ''' <summary>
  ''' This code example gets location criteria by name.
  ''' </summary>
  Public Class LookupLocation
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New LookupLocation
      Console.WriteLine(codeExample.Description)
      Try
        codeExample.Run(New AdWordsUser)
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
        Return "This code example gets location criteria by name."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    Public Sub Run(ByVal user As AdWordsUser)
      ' Get the LocationCriterionService.
      Dim locationCriterionService As LocationCriterionService = CType(user.GetService( _
          AdWordsService.v201603.LocationCriterionService),  _
          LocationCriterionService)

      Dim locationNames As String() = New String() {"Paris", "Quebec", "Spain", "Deutschland"}

      Dim selector As New Selector
      selector.fields = New String() {
        Location.Fields.Id, Location.Fields.LocationName, LocationCriterion.Fields.CanonicalName,
        Location.Fields.DisplayType, Location.Fields.ParentLocations,
        LocationCriterion.Fields.Reach, Location.Fields.TargetingStatus
      }

      selector.predicates = New Predicate() {
        Predicate.In(Location.Fields.LocationName, locationNames),
        Predicate.Equals(LocationCriterion.Fields.Locale, "en")
      }

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
          Console.WriteLine("The search term '{0}' returned the location '{1}' of type '{2}' " & _
              "with parent locations '{3}',  reach '{4}' and targeting status '{5}.", _
              locationCriterion.searchTerm, locationCriterion.location.locationName, _
              locationCriterion.location.displayType, parentLocations, locationCriterion.reach, _
              locationCriterion.location.targetingStatus)
        Next
      Catch e As Exception
        Throw New System.ApplicationException("Failed to get location criteria.", e)
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

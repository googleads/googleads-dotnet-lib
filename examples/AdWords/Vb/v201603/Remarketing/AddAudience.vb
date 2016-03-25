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
  ''' This code example illustrates how to create a user list a.k.a. audience.
  ''' </summary>
  Public Class AddAudience
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New AddAudience
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
        Return "This code example illustrates how to create a user list a.k.a. audience."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    Public Sub Run(ByVal user As AdWordsUser)
      ' Get the UserListService.
      Dim userListService As AdwordsUserListService = CType(user.GetService( _
          AdWordsService.v201603.AdwordsUserListService), AdwordsUserListService)

      ' Get the ConversionTrackerService.
      Dim conversionTrackerService As ConversionTrackerService = CType(user.GetService( _
          AdWordsService.v201603.ConversionTrackerService),  _
          ConversionTrackerService)

      Dim userList As New BasicUserList

      userList.name = ("Mars cruise customers #" & ExampleUtilities.GetRandomString)
      userList.description = "A list of mars cruise customers in the last year."
      userList.status = UserListMembershipStatus.OPEN
      userList.membershipLifeSpan = 365

      Dim conversionType As New UserListConversionType
      conversionType.name = userList.name
      userList.conversionTypes = New UserListConversionType() {conversionType}

      ' Optional: Set the user list status.
      userList.status = UserListMembershipStatus.OPEN

      ' Create the operation.
      Dim operation As New UserListOperation
      operation.operand = userList
      operation.operator = [Operator].ADD

      Try
        ' Add the user list.
        Dim retval As UserListReturnValue = userListService.mutate(New UserListOperation() _
            {operation})

        Dim userLists As UserList() = Nothing
        If ((Not retval Is Nothing) AndAlso (Not retval.value Is Nothing)) Then
          userLists = retval.value
          ' Get all conversion snippets.
          Dim conversionIds As New List(Of String)
          For Each newUserList As BasicUserList In userLists
            If (Not newUserList.conversionTypes Is Nothing) Then
              For Each newConversionType As UserListConversionType In _
                  newUserList.conversionTypes
                conversionIds.Add(newConversionType.id.ToString)
              Next
            End If
          Next

          Dim conversionsMap As New Dictionary(Of Long, ConversionTracker)

          If (conversionIds.Count > 0) Then
            ' Create the selector.
            Dim selector As New Selector
            selector.fields = New String() {ConversionTracker.Fields.Id}

            selector.predicates = New Predicate() {
              Predicate.In(ConversionTracker.Fields.Id, conversionIds)
            }

            ' Get all conversion trackers.
            Dim page As ConversionTrackerPage = conversionTrackerService.get(selector)

            If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
              For Each tracker As ConversionTracker In page.entries
                conversionsMap.Item(tracker.id) = tracker
              Next
            End If
          End If

          ' Display the results.
          For Each newUserList As BasicUserList In userLists
            Console.WriteLine("User list with name '{0}' and id '{1}' was added.", _
                newUserList.name, newUserList.id)

            ' Display user list associated conversion code snippets.
            If (Not newUserList.conversionTypes Is Nothing) Then
              For Each newConversionType As UserListConversionType In newUserList.conversionTypes
                Dim conversionTracker As AdWordsConversionTracker = _
                    DirectCast(conversionsMap.Item(newConversionType.id),  _
                        AdWordsConversionTracker)
                Console.WriteLine("Conversion type code snippet associated to the list:\n{0}", _
                    conversionTracker.snippet)
              Next
            End If
          Next
        Else
          Console.WriteLine("No user lists (a.k.a. audiences) were added.")
        End If
      Catch e As Exception
        Throw New System.ApplicationException("Failed to add user lists (a.k.a. audiences).", e)
      End Try
    End Sub
  End Class
End Namespace

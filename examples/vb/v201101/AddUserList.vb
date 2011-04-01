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
Imports System.Collections.Generic

Namespace Google.Api.Ads.AdWords.Examples.VB.v201101
  ''' <summary>
  ''' This code example illustrates how to create a user list.
  '''
  ''' Tags: UserListService.mutate
  ''' </summary>
  Class AddUserList
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example illustrates how to create a user list."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New AddUserList
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the UserListService.
      Dim userListService As UserListService = user.GetService( _
          AdWordsService.v201101.UserListService)
      
      ' Get the ConversionTrackerService.
      Dim conversionTrackerService As ConversionTrackerService = user.GetService( _
          AdWordsService.v201101.ConversionTrackerService)

      Dim userList As New RemarketingUserList
      userList.name = ("Mars cruise customers #" & GetTimeStamp)
      userList.description = "A list of mars cruise customers in the last year."
      userList.status = UserListMembershipStatus.OPEN
      userList.membershipLifeSpan = 365

      Dim conversionType As New UserListConversionType
      conversionType.name = userList.name
      userList.conversionTypes = New UserListConversionType() {conversionType}

      Dim operation As New UserListOperation
      operation.operand = userList
      operation.operator = [Operator].ADD

      Try
        ' Add user list.
        Dim retval As UserListReturnValue = userListService.mutate(New UserListOperation() _
            {operation})
        Dim userLists As UserList() = Nothing

        If ((Not retval Is Nothing) AndAlso (Not retval.value Is Nothing) And _
            retval.value.Length > 0) Then
          userLists = retval.value
          Dim conversionIds As New List(Of String)
          For Each tempUserList As RemarketingUserList In userLists
            If (Not tempUserList.conversionTypes Is Nothing) Then
              For Each tempConversionType As UserListConversionType In userList.conversionTypes
                conversionIds.Add(tempConversionType.id.ToString)
              Next
            End If
          Next
          Dim conversionsMap As New Dictionary(Of Long, ConversionTracker)

          If (conversionIds.Count > 0) Then
            Dim conversionTypePredicate As New Predicate
            conversionTypePredicate.field = "Id"
            conversionTypePredicate.operator = PredicateOperator.IN
            conversionTypePredicate.values = conversionIds.ToArray

            Dim selector As New Selector
            selector.fields = New String() {"Id"}
            selector.predicates = New Predicate() {conversionTypePredicate}

            Dim page As ConversionTrackerPage = conversionTrackerService.get(selector)
            If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
              For Each tracker As ConversionTracker In page.entries
                conversionsMap.Item(tracker.id) = tracker
              Next
            End If
          End If

          For Each tempUserList As RemarketingUserList In userLists
            Console.WriteLine("User list with name '{0}' and id '{1}' was added.", _
                tempUserList.name, tempUserList.id)

            If (Not tempUserList.conversionTypes Is Nothing) Then
              For Each tempConversionType As UserListConversionType In tempUserList.conversionTypes
                Dim conversionTracker As AdWordsConversionTracker = _
                    DirectCast(conversionsMap.Item(tempConversionType.id),  _
                        AdWordsConversionTracker)
                Console.WriteLine("Conversion type code snippet associated to the list:\n{0}\n", _
                    conversionTracker.snippet)
              Next
            End If
          Next
        Else
          Console.WriteLine("No user lists were added.")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to add user lists. Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace

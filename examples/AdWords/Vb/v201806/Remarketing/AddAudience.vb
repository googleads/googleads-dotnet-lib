' Copyright 2018 Google LLC
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
Imports Google.Api.Ads.AdWords.v201806

Namespace Google.Api.Ads.AdWords.Examples.VB.v201806
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
                Console.WriteLine("An exception occurred while running this code example. {0}",
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
            Using userListService As AdwordsUserListService = CType(
                user.GetService(
                    AdWordsService.v201806.AdwordsUserListService),
                AdwordsUserListService)

                Using conversionTrackerService As ConversionTrackerService = CType(
                    user.GetService(
                        AdWordsService.v201806.ConversionTrackerService),
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
                        Dim retval As UserListReturnValue = userListService.mutate(
                            New UserListOperation() {operation})

                        Dim newUserList As UserList = retval.value(0)

                        Console.WriteLine("User list with name '{0}' and id '{1}' was added.",
                                          newUserList.name, newUserList.id)

                        Dim conversionIds As New List(Of String)()
                        For Each item As UserListConversionType In userList.conversionTypes
                            conversionIds.Add(item.id.ToString())
                        Next

                        If (conversionIds.Count > 0) Then
                            ' Create the selector.
                            Dim selector As New Selector
                            selector.fields = New String() { _
                                                               ConversionTracker.Fields.Id,
                                                               ConversionTracker.Fields.
                                                                   GoogleGlobalSiteTag,
                                                               ConversionTracker.Fields.
                                                                   GoogleEventSnippet
                                                           }

                            selector.predicates = New Predicate() { _
                                                                      Predicate.In(
                                                                          ConversionTracker.Fields.
                                                                                      Id,
                                                                          conversionIds)
                                                                  }

                            ' Get all conversion trackers.
                            Dim page As ConversionTrackerPage =
                                    conversionTrackerService.get(selector)

                            If (Not page Is Nothing) AndAlso (Not page.entries Is Nothing) Then
                                For Each tracker As ConversionTracker In page.entries
                                    Console.WriteLine(
                                        "Google global site tag:\n{0}\nGoogle event snippet:\n{1}",
                                        tracker.googleGlobalSiteTag, tracker.googleGlobalSiteTag)
                                Next
                            End If
                        End If
                    Catch e As Exception
                        Throw New System.ApplicationException("Failed to add user lists (a.k.a. " +
                                                              "audiences).", e)
                    End Try
                End Using
            End Using
        End Sub
    End Class
End Namespace

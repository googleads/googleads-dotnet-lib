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
Imports Google.Api.Ads.AdWords.v201008

Imports System

Namespace Google.Api.Ads.AdWords.Examples.VB.v201008
  ''' <summary>
  ''' This code example deletes a user list by setting the status to 'CLOSED'.
  ''' To get user lists, run GetAllUserLists.vb.
  '''
  ''' Tags: UserListService.mutate
  ''' </summary>
  Class DeleteUserList
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example deletes a user list by setting the status to 'CLOSED'. To " & _
            "get user lists, run GetAllUserLists.vb."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New DeleteUserList
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the UserListService.
      Dim userListService As UserListService = user.GetService( _
          AdWordsService.v201008.UserListService)

      Dim userListId As Long = Integer.Parse(_T("INSERT_USER_LIST_ID_HERE"))

      ' Prepare for deleting remarketing user list. Bear in mind that you must
      ' create an object of the appropriate type in order to perform the
      ' update. If you are unsure which type a user list is, you should perform
      ' a 'get' on it first.
      Dim userList As New RemarketingUserList
      userList.id = userListId
      userList.status = UserListMembershipStatus.CLOSED

      Dim operation As New UserListOperation
      operation.operand = userList
      operation.operator = [Operator].SET

      Try
        ' Delete user list.
        Dim retval As UserListReturnValue = userListService.mutate( _
            New UserListOperation() {operation})
        If ((Not retval Is Nothing) AndAlso (Not retval.value Is Nothing) AndAlso _
            (retval.value.Length > 0)) Then
          Dim tempUserList As UserList = retval.value(0)
          Console.WriteLine("User list with name ""{0}"" and id {1} was deleted (closed).", _
              tempUserList.name, tempUserList.id)
        Else
          Console.WriteLine("No user lists were deleted (closed).")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to delete (close) user lists. Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace

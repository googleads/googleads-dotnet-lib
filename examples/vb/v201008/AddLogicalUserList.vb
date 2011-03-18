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
  ''' This code example illustrates how to create a user list.
  '''
  ''' Tags: UserListService.mutate
  ''' </summary>
  Class AddLogicalUserList
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
          AdWordsService.v201008.UserListService)

      Dim userList As New LogicalUserList
      userList.name = ("Mars cruise customers #" & GetTimeStamp)
      userList.description = "A list of mars cruise customers in the last year."
      userList.status = UserListMembershipStatus.OPEN
      userList.membershipLifeSpan = 365

      ' Make an UserInterest group for Travel > Cruises & Charters. See
      ' http://code.google.com/apis/adwords/docs/appendix/verticals.html for
      ' various verticals and their ids.
      Dim interest As New UserInterest
      interest.name = "Mars cruise interest group"
      interest.sizeRange = SizeRange.FIFTY_THOUSAND_TO_ONE_HUNDRED_THOUSAND
      interest.id = 206

      Dim userListOperand As New LogicalUserListOperand
      userListOperand.Item = interest

      Dim rule As New UserListLogicalRule
      rule.operator = UserListLogicalRuleOperator.NONE
      rule.ruleOperands = New LogicalUserListOperand() {userListOperand}
      userList.rules = New UserListLogicalRule() {rule}

      Dim operation As New UserListOperation
      operation.operand = userList
      operation.operator = [Operator].ADD

      Try
        ' Add user list.
        Dim retval As UserListReturnValue = userListService.mutate( _
            New UserListOperation() {operation})
        If ((Not retval Is Nothing) And (Not retval.value Is Nothing) And _
            (retval.value.Length > 0)) Then
          Dim tempUserList As UserList = retval.value(0)
          Console.WriteLine("User list with name ""{0}"" and id {1} was added.", _
              tempUserList.name, tempUserList.id)
        Else
          Console.WriteLine("No user lists were added.")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to add user lists. Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace

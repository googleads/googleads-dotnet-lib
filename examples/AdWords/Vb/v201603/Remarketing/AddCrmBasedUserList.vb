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
Imports System.Security.Cryptography
Imports System.Text

Namespace Google.Api.Ads.AdWords.Examples.VB.v201603
  ''' <summary>
  ''' This code example adds a remarketing user list (a.k.a. audience) and
  ''' uploads hashed email addresses to populate the list.
  ''' 
  ''' <p>
  ''' <em>Note:</em> It may take up to several hours for the list to be
  ''' populated with members. Email addresses must be associated with a Google
  ''' account. For privacy purposes, the user list size will show as zero until
  ''' the list has at least 1000 members. After that, the size will be rounded
  ''' to the two most significant digits. 
  ''' </p>
  ''' </summary>
  Public Class AddCrmBasedUserList
    Inherits ExampleBase

    Private Shared ReadOnly EMAILS As String() = New String() {
      "customer1@example.com", "customer2@example.com", " Customer3@example.com "
    }

    Private Shared ReadOnly hashProvider As HashAlgorithm = SHA256Managed.Create()

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New AddCrmBasedUserList
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
        Return "This code example adds a remarketing user list (a.k.a. audience) and uploads " & _
            "hashed email addresses to populate the list."
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

      ' Create remarketing user list.
      Dim userList As New CrmBasedUserList
      userList.name = "Customer relationship management list #" & _
          ExampleUtilities.GetRandomString()
      userList.description = "A list of customers that originated from email addresses"

      ' CRM Userlist has a maximum membership lifespan of 180 days. See
      ' https://support.google.com/adwords/answer/6276125 for details.
      userList.membershipLifeSpan = 180L

      ' Create operation.
      Dim operation As New UserListOperation
      operation.operand = userList
      operation.operator = [Operator].ADD

      Try
        ' Add user list.
        Dim result As UserListReturnValue = userListService.mutate( _
            New UserListOperation() {operation})

        Console.WriteLine("Created new user list with name = '{0}' and id = '{1}'.", _
            result.value(0).name, result.value(0).id)

        ' Get user list ID.
        Dim userListId As Long = result.value(0).id

        ' Create operation to add members to the user list based on email
        ' addresses.
        Dim mutateMembersOperation As New MutateMembersOperation
        mutateMembersOperation.operand = New MutateMembersOperand()
        mutateMembersOperation.operand.userListId = userListId
        mutateMembersOperation.operand.dataType = MutateMembersOperandDataType.EMAIL_SHA256
        mutateMembersOperation.operator = [Operator].ADD

        ' Hash normalized email addresses based on SHA-256 hashing algorithm.
        Dim emailHashes(EMAILS.Length) As String
        For i As Integer = 0 To EMAILS.Length - 1
          Dim normalizedEmail As String = ToNormalizedEmail(EMAILS(i))
          emailHashes(i) = ToSha256String(hashProvider, normalizedEmail)
        Next

        ' Add email address hashes.
        mutateMembersOperation.operand.members = emailHashes

        ' Add members to the user list based on email addresses.
        Dim mutateMembersResult As MutateMembersReturnValue =
            userListService.mutateMembers(New MutateMembersOperation() {mutateMembersOperation})

        ' Display results.
        ' Reminder: it may take several hours for the list to be populated with
        ' members.
        For Each userListResult As UserList In mutateMembersResult.userLists
          Console.WriteLine("Email addresses were added to user list with name '{0}' and " & _
              "id '{1}'.", userListResult.name, userListResult.id)
        Next
      Catch e As Exception
        Throw New System.ApplicationException("Failed to add user lists (a.k.a. audiences)." & _
            "and upload email addresses.", e)
      End Try
    End Sub

    ''' <summary>
    ''' Hash email address using SHA-256 hashing algorithm.
    ''' </summary>
    ''' <param name="hashProvider">Provides the algorithm for SHA-256.</param>
    ''' <param name="email">The email address to hash.</param>
    ''' <returns>Hash email address using SHA-256 hashing algorithm.</returns>
    Private Shared Function ToSha256String(ByVal hashProvider As HashAlgorithm, _
                                           ByVal email As String) As String
      Dim hash As Byte() = hashProvider.ComputeHash(Encoding.UTF8.GetBytes(email))
      Return BitConverter.ToString(hash).Replace("-", String.Empty)
    End Function

    ''' <summary>
    ''' Removes leading and trailing whitespace and converts all characters to
    ''' lower case.
    ''' </summary>
    ''' <param name="email">The email address to normalize.</param>
    ''' <returns>A normalized copy of the string.</returns>
    Private Shared Function ToNormalizedEmail(ByVal email As String) As String
      Return email.Trim().ToLower()
    End Function
  End Class
End Namespace

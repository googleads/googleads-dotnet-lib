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

Imports System.Security.Cryptography
Imports System.Text
Imports Google.Api.Ads.AdWords.Lib
Imports Google.Api.Ads.AdWords.v201809

Namespace Google.Api.Ads.AdWords.Examples.VB.v201809
    ''' <summary>
    ''' This code example adds a user list (a.k.a. audience) and uploads hashed
    ''' email addresses to populate the list.
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

        Private Shared ReadOnly EMAILS As String() = New String() { _
                                                                      "customer1@example.com",
                                                                      "customer2@example.com",
                                                                      " Customer3@example.com "
                                                                  }

        Private Const FIRST_NAME As String = "John"
        Private Const LAST_NAME As String = "Doe"
        Private Const COUNTRY_CODE As String = "US"
        Private Const ZIP_CODE As String = "10001"

        Private Shared digest As SHA256 = SHA256.Create()

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
                Console.WriteLine("An exception occurred while running this code " &
                                  "example. {0}", ExampleUtilities.FormatException(e))
            End Try
        End Sub

        ''' <summary>
        ''' Returns a description about the code example.
        ''' </summary>
        Public Overrides ReadOnly Property Description() As String
            Get
                Return "This code example adds a user list (a.k.a. audience) and " &
                       "uploads hashed email addresses to populate the list."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        Public Sub Run(ByVal user As AdWordsUser)
            Using userListService As AdwordsUserListService = CType(
                user.GetService(
                    AdWordsService.v201809.AdwordsUserListService),
                AdwordsUserListService)

                ' Create a user list.
                Dim userList As New CrmBasedUserList
                userList.name = "Customer relationship management list #" &
                                ExampleUtilities.GetRandomString()
                userList.description = "A list of customers that originated from email addresses"

                ' CRM-based user lists can use a membershipLifeSpan of 10000 to indicate
                ' unlimited; otherwise normal values apply.
                userList.membershipLifeSpan = 30L
                userList.uploadKeyType = CustomerMatchUploadKeyType.CONTACT_INFO

                ' Create operation.
                Dim operation As New UserListOperation
                operation.operand = userList
                operation.operator = [Operator].ADD

                Try
                    ' Add user list.
                    Dim result As UserListReturnValue = userListService.mutate(
                        New UserListOperation() {operation})

                    Console.WriteLine("Created new user list with name = '{0}' and " &
                                      "id = '{1}'.", result.value(0).name, result.value(0).id)

                    ' Get user list ID.
                    Dim userListId As Long = result.value(0).id

                    ' Prepare the emails for upload.
                    Dim memberList As New List(Of Member)()

                    ' Hash normalized email addresses based on SHA-256 hashing algorithm.
                    For i As Integer = 0 To EMAILS.Length - 1
                        Dim member As New Member()
                        member.hashedEmail = ToSha256String(digest, ToNormalizedEmail(EMAILS(i)))
                        memberList.Add(member)
                    Next

                    ' Add a user by first and last name.
                    Dim addressInfo As New AddressInfo()
                    ' First and last name must be normalized and hashed.

                    addressInfo.hashedFirstName = ToSha256String(digest, FIRST_NAME)
                    addressInfo.hashedLastName = ToSha256String(digest, LAST_NAME)
                    ' Country code and zip code are sent in plaintext.

                    addressInfo.zipCode = ZIP_CODE
                    addressInfo.countryCode = COUNTRY_CODE

                    Dim memberByAddress As New Member()
                    memberByAddress.addressInfo = addressInfo
                    memberList.Add(memberByAddress)

                    ' Create operation to add members to the user list based on email
                    ' addresses.
                    Dim mutateMembersOperation As New MutateMembersOperation
                    mutateMembersOperation.operand = New MutateMembersOperand()
                    mutateMembersOperation.operand.userListId = userListId
                    mutateMembersOperation.operand.membersList = memberList.ToArray()
                    mutateMembersOperation.operator = [Operator].ADD

                    ' Add members to the user list based on email addresses.
                    Dim mutateMembersResult As MutateMembersReturnValue =
                            userListService.mutateMembers(
                                New MutateMembersOperation() {mutateMembersOperation})

                    ' Display results.
                    ' Reminder: it may take several hours for the list to be populated with
                    ' members.
                    For Each userListResult As UserList In mutateMembersResult.userLists
                        Console.WriteLine("Email addresses were added to user list with " &
                                          "name '{0}' and id '{1}'.",
                                          userListResult.name, userListResult.id)
                    Next
                Catch e As Exception
                    Throw New System.ApplicationException("Failed to add user lists " &
                                                          "(a.k.a. audiences) and upload email " &
                                                          "addresses.",
                                                          e)
                End Try
            End Using
        End Sub

        ''' <summary>
        ''' Hash email address using SHA-256 hashing algorithm.
        ''' </summary>
        ''' <param name="digest">Provides the algorithm for SHA-256.</param>
        ''' <param name="email">The email address to hash.</param>
        ''' <returns>Hash email address using SHA-256 hashing algorithm.</returns>
        Private Shared Function ToSha256String(ByVal digest As SHA256,
                                               ByVal email As String) As String
            Dim digestBytes As Byte() = digest.ComputeHash(Encoding.UTF8.GetBytes(email))
            ' Convert the byte array into an unhyphenated hexadecimal string.
            Return BitConverter.ToString(digestBytes).Replace("-", String.Empty)
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

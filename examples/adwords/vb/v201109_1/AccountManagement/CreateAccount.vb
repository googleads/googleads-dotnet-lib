' Copyright 2012, Google Inc. All Rights Reserved.
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
Imports Google.Api.Ads.AdWords.v201109_1

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201109_1
  ''' <summary>
  ''' This code example illustrates how to create an account. Note by default,
  ''' this account will only be accessible via parent MCC.
  '''
  ''' Tags: CreateAccountService.mutate
  ''' </summary>
  Public Class CreateAccount
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New CreateAccount
      Console.WriteLine(codeExample.Description)
      Try
        codeExample.Run(New AdWordsUser)
      Catch ex As Exception
        Console.WriteLine("An exception occurred while running this code example. {0}", _
            ExampleUtilities.FormatException(ex))
      End Try
    End Sub

    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example illustrates how to create an account. Note by default," & _
            " this account will only be accessible via parent MCC."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    Public Sub Run(ByVal user As AdWordsUser)
      ' Get the CreateAccountService.
      Dim createAccountService As CreateAccountService = user.GetService( _
          AdWordsService.v201109_1.CreateAccountService)

      createAccountService.RequestHeader.clientCustomerId = Nothing

      Dim account As New Account()
      account.currencyCode = "EUR"
      account.dateTimeZone = "Europe/London"

      ' Create the operation.
      Dim operation As New CreateAccountOperation()
      operation.operator = [Operator].ADD
      operation.operand = account
      operation.descriptiveName = "Account created with CreateAccountService"

      Try
        ' Create the account. It is possible to create multiple accounts with
        ' one request by sending an array of operations.
        Dim accounts As Account() = createAccountService.mutate( _
            New CreateAccountOperation() {operation})

        ' Display the results.
        If (Not accounts Is Nothing AndAlso accounts.Length > 0) Then
          Dim newAccount As Account = accounts(0)
          Console.WriteLine("Account with customer ID '{0:###-###-####}' was successfully " & _
              "created.", newAccount.customerId)
        Else
          Console.WriteLine("No accounts were created.")
        End If
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to create accounts.", ex)
      End Try
    End Sub
  End Class
End Namespace

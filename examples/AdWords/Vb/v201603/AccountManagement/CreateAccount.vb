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
Imports System.Text

Namespace Google.Api.Ads.AdWords.Examples.VB.v201603
  ''' <summary>
  ''' This code example illustrates how to create an account. Note by default,
  ''' this account will only be accessible via its parent AdWords manager
  ''' account.
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
        Return "This code example illustrates how to create an account. Note by default," & _
            " this account will only be accessible via its parent AdWords manager account."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    Public Sub Run(ByVal user As AdWordsUser)
      ' Get the ManagedCustomerService.
      Dim managedCustomerService As ManagedCustomerService = CType(user.GetService( _
          AdWordsService.v201603.ManagedCustomerService), ManagedCustomerService)

      ' Create account.
      Dim customer As New ManagedCustomer()
      customer.name = "Customer created with ManagedCustomerService on " & _
          New DateTime().ToString()
      customer.currencyCode = "EUR"
      customer.dateTimeZone = "Europe/London"

      ' Create operations.
      Dim operation As New ManagedCustomerOperation()
      operation.operand = customer
      operation.operator = [Operator].ADD
      Try
        Dim operations As ManagedCustomerOperation() = New ManagedCustomerOperation() {operation}
        ' Add account.
        Dim result As ManagedCustomerReturnValue = managedCustomerService.mutate(operations)

        ' Display accounts.
        If (Not result.value Is Nothing) AndAlso (result.value.Length > 0) Then
          Dim customerResult As ManagedCustomer = result.value(0)
          Console.WriteLine("Account with customer ID '{0}' was created.", _
              customerResult.customerId)
        Else
          Console.WriteLine("No accounts were created.")
        End If
      Catch e As Exception
        Throw New System.ApplicationException("Failed to create accounts.", e)
      End Try
    End Sub
  End Class
End Namespace

' Copyright 2015, Google Inc. All Rights Reserved.
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
Imports Google.Api.Ads.AdWords.v201502

Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Text

Namespace Google.Api.Ads.AdWords.Examples.VB.v201502
  ''' <summary>
  ''' This code example illustrates how to create an account. Note by default,
  ''' this account will only be accessible via its parent MCC.
  '''
  ''' Tags: ManagedCustomerService.mutate
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
            " this account will only be accessible via its parent MCC."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    Public Sub Run(ByVal user As AdWordsUser)
      ' Get the ManagedCustomerService.
      Dim managedCustomerService As ManagedCustomerService = CType(user.GetService( _
          AdWordsService.v201502.ManagedCustomerService), AdWords.v201502.ManagedCustomerService)

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
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to create accounts.", ex)
      End Try
    End Sub
  End Class


  ''' <summary>
  '''Example implementation of a node that would exist in an account tree.
  ''' </summary>
  Class ManagedCustomerTreeNode
    ''' <summary>
    ''' The parent node.
    ''' </summary>
    Private _parentNode As ManagedCustomerTreeNode

    ''' <summary>
    ''' The account associated with this node.
    ''' </summary>
    Private _account As ManagedCustomer

    ''' <summary>
    ''' The list of child accounts.
    ''' </summary>
    Private _childAccounts As New List(Of ManagedCustomerTreeNode)

    ''' <summary>
    ''' Gets or sets the parent node.
    ''' </summary>
    Public Property ParentNode() As ManagedCustomerTreeNode
      Get
        Return _parentNode
      End Get
      Set(ByVal value As ManagedCustomerTreeNode)
        _parentNode = value
      End Set
    End Property


    ''' <summary>
    ''' Gets or sets the account.
    ''' </summary>
    Public Property Account() As ManagedCustomer
      Get
        Return _account
      End Get
      Set(ByVal value As ManagedCustomer)
        _account = value
      End Set
    End Property

    ''' <summary>
    ''' Gets or sets the child accounts.
    ''' </summary>
    Public Property ChildAccounts() As List(Of ManagedCustomerTreeNode)
      Get
        Return _childAccounts
      End Get
      Set(ByVal value As List(Of ManagedCustomerTreeNode))
        _childAccounts = value
      End Set
    End Property

    ''' <summary>
    ''' Returns a <see cref="System.String"/> that represents this instance.
    ''' </summary>
    ''' <returns>
    ''' A <see cref="System.String"/> that represents this instance.
    ''' </returns>
    Public Overrides Function ToString() As String
      Return String.Format("{0}, {1}", _account.customerId, _account.name)
    End Function

    ''' <summary>
    ''' Returns a string representation of the current level of the tree and
    ''' recursively returns the string representation of the levels below it.
    ''' </summary>
    ''' <param name="depth">The depth of the node.</param>
    ''' <param name="sb">The String Builder containing the tree
    ''' representation.</param>
    ''' <returns>The tree string representation.</returns>
    Public Function ToTreeString(ByVal depth As Integer, ByVal sb As StringBuilder) As StringBuilder
      For i As Integer = 0 To depth * 2
        sb.Append("-")
      Next
      sb.Append(Me)
      sb.Append("\n")
      For Each childAccount As ManagedCustomerTreeNode In _childAccounts
        childAccount.ToTreeString(depth + 1, sb)
      Next
      Return sb
    End Function
  End Class
End Namespace

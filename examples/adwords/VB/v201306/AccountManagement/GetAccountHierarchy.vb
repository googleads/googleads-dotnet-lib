' Copyright 2013, Google Inc. All Rights Reserved.
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
Imports Google.Api.Ads.AdWords.v201306

Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Text

Namespace Google.Api.Ads.AdWords.Examples.VB.v201306
  ''' <summary>
  ''' This code example illustrates how to retrieve the account hierarchy under
  ''' an account. This code example won't work with Test Accounts. See
  ''' https://developers.google.com/adwords/api/docs/test-accounts
  '''
  ''' Tags: ServicedAccountService.get
  ''' </summary>
  Public Class GetAccountHierarchy
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New GetAccountHierarchy
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
        Return "This code example illustrates how to retrieve the account hierarchy under " & _
            "an account. This code example won't work with Test Accounts. See " & _
            "https://developers.google.com/adwords/api/docs/test-accounts"
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    Public Sub Run(ByVal user As AdWordsUser)
      ' Get the ManagedCustomerService.
      Dim mcService As ManagedCustomerService = user.GetService( _
          AdWordsService.v201306.ManagedCustomerService)
      mcService.RequestHeader.clientCustomerId = Nothing

      ' Create selector.
      Dim selector As New Selector()
      selector.fields = New String() {"Login", "CustomerId", "Name"}

      Try
        ' Get results.
        Dim page As ManagedCustomerPage = mcService.get(selector)

        ' Display serviced account graph.
        If Not page.entries Is Nothing Then
          ' Create map from customerId to customer node.
          Dim customerIdToCustomerNode As Dictionary(Of Long, ManagedCustomerTreeNode) = _
              New Dictionary(Of Long, ManagedCustomerTreeNode)()

          ' Create account tree nodes for each customer.
          For Each customer As ManagedCustomer In page.entries
            Dim node As New ManagedCustomerTreeNode()
            node.Account = customer
            customerIdToCustomerNode.Add(customer.customerId, node)
          Next

          ' For each link, connect nodes in tree.
          If (Not page.links Is Nothing) Then
            For Each link As ManagedCustomerLink In page.links
              Dim managerNode As ManagedCustomerTreeNode = _
                  customerIdToCustomerNode(link.managerCustomerId)
              Dim childNode As ManagedCustomerTreeNode = _
                  customerIdToCustomerNode(link.clientCustomerId)
              childNode.ParentNode = managerNode
              If (Not managerNode Is Nothing) Then
                managerNode.ChildAccounts.Add(childNode)
              End If
            Next
          End If

          ' Find the root account node in the tree.
          Dim rootNode As ManagedCustomerTreeNode = Nothing
          For Each account As ManagedCustomer In page.entries
            If (customerIdToCustomerNode(account.customerId).ParentNode Is Nothing) Then
              rootNode = customerIdToCustomerNode(account.customerId)
              Exit For
            End If
          Next

          ' Display account tree.
          Console.WriteLine("Login, CustomerId, Name")
          Console.WriteLine(rootNode.ToTreeString(0, New StringBuilder()))
        Else
          Console.WriteLine("No accounts were found.")
        End If
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to get accounts.", ex)
      End Try
    End Sub
  End Class
End Namespace

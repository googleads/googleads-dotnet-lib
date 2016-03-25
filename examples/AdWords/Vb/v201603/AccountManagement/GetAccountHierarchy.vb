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
  ''' This code example illustrates how to retrieve the account hierarchy under
  ''' an account. This code example won't work with Test Accounts. See
  ''' https://developers.google.com/adwords/api/docs/test-accounts
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
      Dim mcService As ManagedCustomerService = CType(user.GetService( _
          AdWordsService.v201603.ManagedCustomerService), ManagedCustomerService)

      ' Create selector.
      Dim selector As New Selector()
      selector.fields = New String() {
        ManagedCustomer.Fields.CustomerId, ManagedCustomer.Fields.Name
      }
      selector.paging = Paging.Default

      ' Map from customerId to customer node.
      Dim customerIdToCustomerNode As Dictionary(Of Long, ManagedCustomerTreeNode) = _
          New Dictionary(Of Long, ManagedCustomerTreeNode)()

      ' Temporary cache to save links.
      Dim allLinks As New List(Of ManagedCustomerLink)

      Dim page As ManagedCustomerPage = Nothing
      Try
        Do
          page = mcService.get(selector)

          ' Display serviced account graph.
          If Not page.entries Is Nothing Then
            ' Create account tree nodes for each customer.
            For Each customer As ManagedCustomer In page.entries
              Dim node As New ManagedCustomerTreeNode()
              node.Account = customer
              customerIdToCustomerNode.Add(customer.customerId, node)
            Next

            If Not page.links Is Nothing Then
              allLinks.AddRange(page.links)
            End If
          End If

          selector.paging.IncreaseOffset()
        Loop While (selector.paging.startIndex < page.totalNumEntries)

        ' For each link, connect nodes in tree.
        For Each link As ManagedCustomerLink In allLinks
          Dim managerNode As ManagedCustomerTreeNode = _
              customerIdToCustomerNode(link.managerCustomerId)
          Dim childNode As ManagedCustomerTreeNode = _
              customerIdToCustomerNode(link.clientCustomerId)
          childNode.ParentNode = managerNode
          If (Not managerNode Is Nothing) Then
            managerNode.ChildAccounts.Add(childNode)
          End If
        Next

        ' Find the root account node in the tree.
        Dim rootNode As ManagedCustomerTreeNode = Nothing
        For Each node As ManagedCustomerTreeNode In customerIdToCustomerNode.Values
          If node.ParentNode Is Nothing Then
            rootNode = node
            Exit For
          End If
        Next

        ' Display account tree.
        Console.WriteLine("CustomerId, Name")
        Console.WriteLine(rootNode.ToTreeString(0, New StringBuilder()))
      Catch e As Exception
        Throw New System.ApplicationException("Failed to get accounts.", e)
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
      sb.Append("-"c, depth * 2)
      sb.Append(Me)
      sb.AppendLine()
      For Each childAccount As ManagedCustomerTreeNode In _childAccounts
        childAccount.ToTreeString(depth + 1, sb)
      Next
      Return sb
    End Function
  End Class
End Namespace

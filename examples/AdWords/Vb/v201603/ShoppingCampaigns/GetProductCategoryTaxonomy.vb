' Copyright 2016, Google Inc. All Rights Reserved.
'
' Licensed under the Apache License, Version 2.0 (the "License")
' you may not use this file except in compliance with the License.
' You may obtain a copy of the License at
'
'     http:'www.apache.org/licenses/LICENSE-2.0
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

Namespace Google.Api.Ads.AdWords.Examples.VB.v201603
  ''' <summary>
  ''' This code example fetches the set of valid ProductBiddingCategories.
  ''' </summary>
  Public Class GetProductCategoryTaxonomy
    Inherits ExampleBase

    ''' <summary>
    ''' Stores details about a product category and its hierarchy.
    ''' </summary>
    Class ProductCategory
      ''' <summary>
      ''' The product category id.
      ''' </summary>
      Private idField As Long

      ''' <summary>
      ''' The product category name.
      ''' </summary>
      Private nameField As String

      ''' <summary>
      ''' The product category children.
      ''' </summary>
      Private childrenField As New List(Of ProductCategory)

      ''' <summary>
      ''' Gets or sets the product category id.
      ''' </summary>
      Public Property Id() As Long
        Get
          Return idField
        End Get
        Set(ByVal value As Long)
          idField = value
        End Set
      End Property

          ''' <summary>
          ''' Gets or sets the product category name.
          ''' </summary>
      Public Property Name() As String
        Get
          Return nameField
        End Get
        Set(ByVal value As String)
          nameField = value
        End Set
      End Property

          ''' <summary>
          ''' Gets or sets the product category children.
          ''' </summary>
      Public ReadOnly Property Children() As List(Of ProductCategory)
        Get
          Return childrenField
        End Get
      End Property
    End Class

    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example fetches the set of valid ProductBiddingCategories."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New GetProductCategoryTaxonomy
      Console.WriteLine(codeExample.Description)
      Try
        codeExample.Run(New AdWordsUser)
      Catch e As Exception
        Console.WriteLine("An exception occurred while running this code example. {0}", _
            ExampleUtilities.FormatException(e))
      End Try
    End Sub

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    Public Sub Run(ByVal user As AdWordsUser)
      ' Get the ConstantDataService.
      Dim constantDataService As ConstantDataService = CType(user.GetService( _
          AdWordsService.v201603.ConstantDataService), ConstantDataService)

      Dim selector As New Selector()
      selector.predicates = New Predicate() {
        Predicate.In(ProductBiddingCategoryData.Fields.Country, New String() {"US"})
      }

      Try
        Dim results As ProductBiddingCategoryData() = _
            constantDataService.getProductBiddingCategoryData(selector)

        Dim biddingCategories As New Dictionary(Of Long, ProductCategory)()
        Dim rootCategories As New List(Of ProductCategory)()

        For Each productBiddingCategory As ProductBiddingCategoryData In results
          Dim id As Long = productBiddingCategory.dimensionValue.value
          Dim parentId As Long = 0
          Dim name As String = productBiddingCategory.displayValue(0).value

          If Not (productBiddingCategory.parentDimensionValue Is Nothing) Then
            parentId = productBiddingCategory.parentDimensionValue.value
          End If

          If Not biddingCategories.ContainsKey(id) Then
            biddingCategories.Add(id, New ProductCategory())
          End If

          Dim category As ProductCategory = biddingCategories(id)

          If (parentId <> 0) Then
            If Not biddingCategories.ContainsKey(parentId) Then
              biddingCategories.Add(parentId, New ProductCategory())
            End If
            Dim parent As ProductCategory = biddingCategories(parentId)
            parent.Children.Add(category)
          Else
            rootCategories.Add(category)
          End If

          category.Id = id
          category.Name = name
        Next

        DisplayProductCategories(rootCategories, "")
      Catch e As Exception
        Throw New System.ApplicationException("Failed to create shopping campaign.", e)
      End Try
    End Sub

    ''' <summary>
    ''' Displays the product categories.
    ''' </summary>
    ''' <param name="categories">The product categories.</param>
    ''' <param name="prefix">The prefix for display purposes.</param>
    Sub DisplayProductCategories(ByVal categories As List(Of ProductCategory), _
                                 ByVal prefix As String)
      For Each category As ProductCategory In categories
        Console.WriteLine("{0}{1} [{2}]", prefix, category.Name, category.Id)
        DisplayProductCategories(category.Children, String.Format("{0}{1} > ", _
            prefix, category.Name))
      Next
    End Sub
  End Class

End Namespace

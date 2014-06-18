' Copyright 2014, Google Inc. All Rights Reserved.
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

' Author: api.anash@gmail.com (Anash P. Oommen)

Imports Google.Api.Ads.AdWords.Lib
Imports Google.Api.Ads.AdWords.v201402

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201402
  ''' <summary>
  ''' This code example creates a ProductPartition tree.
  '''
  ''' Tags: AdGroupCriterionService.mutate
  ''' </summary>
  Public Class AddProductPartitionTree
    Inherits ExampleBase

    ''' <summary>
    ''' A helper class for creating ProductPartition trees.
    ''' </summary>
    Private Class ProductPartitionHelper

      ''' <summary>
      ''' The ID of the AdGroup that we wish to attach the partition tree to.
      ''' </summary>
      Private adGroupId As Long

      ''' <summary>
      ''' The next temporary critertion ID to be used.
      '''
      ''' When creating our tree we need to specify the parent-child
      ''' relationships between nodes. However, until a criterion has been
      ''' created on the server we do not have a criterionId with which to
      ''' refer to it.
      '''
      ''' Instead we can specify temporary IDs that are specific to a single
      ''' mutate request. Once the criteria have been created they are assigned
      ''' an ID as normal and the temporary ID will no longer refer to it.
      '''
      ''' A valid temporary ID is any negative integer.
      ''' </summary>
      Private nextId As Long = -1

      ''' <summary>
      ''' The set of mutate operations needed to create the current tree.
      ''' </summary>
      Private operationsField As New List(Of AdGroupCriterionOperation)

      ''' <summary>
      ''' Overloaded constructor.
      ''' </summary>
      ''' <param name="adGroupId">The ID of the AdGroup that we wish to attach
      ''' the partition tree to.</param>
      Public Sub New(ByVal adGroupId As Long)
        Me.adGroupId = adGroupId
      End Sub

      Public ReadOnly Property Operations() As AdGroupCriterionOperation()
        Get
          Return operationsField.ToArray
        End Get
      End Property

      ''' <summary>
      ''' Creates a subdivision node.
      ''' </summary>
      ''' <param name="parent">The node that should be this node's parent.
      ''' </param>
      ''' <param name="value">The value being paritioned on.</param>
      ''' <returns>A new subdivision node.</returns>
      Public Function CreateSubdivision(ByVal parent As ProductPartition, _
                                        ByVal value As ProductDimension) As ProductPartition
        Dim division As New ProductPartition()
        division.partitionType = ProductPartitionType.SUBDIVISION
        division.id = Me.nextId
        Me.nextId = Me.nextId - 1

        ' The root node has neither a parent nor a value.
        If Not (parent Is Nothing) Then
          division.parentCriterionId = parent.id
          division.caseValue = value
        End If

        Dim criterion As New BiddableAdGroupCriterion()
        criterion.adGroupId = Me.adGroupId
        criterion.criterion = division

        Me.CreateAddOperation(criterion)
        Return division
      End Function

      ''' <summary>
      ''' Creates the unit.
      ''' </summary>
      ''' <param name="parent">The node that should be this node's parent.
      ''' </param>
      ''' <param name="value">The value being paritioned on.</param>
      ''' <param name="bidAmount">The amount to bid for matching products,
      ''' in micros.</param>
      ''' <param name="isNegative">True, if this is negative criterion, false
      ''' otherwise.</param>
      ''' <returns>A new unit node.</returns>
      Public Function CreateUnit(ByVal parent As ProductPartition, _
                                 ByVal value As ProductDimension, _
                                 ByVal bidAmount As Long, _
                                 ByVal isNegative As Boolean) As ProductPartition
        Dim unit As New ProductPartition()
        unit.partitionType = ProductPartitionType.UNIT

        ' The root node has neither a parent nor a value.
        If Not (parent Is Nothing) Then
          unit.parentCriterionId = parent.id
          unit.caseValue = value
        End If

        Dim criterion As AdGroupCriterion = Nothing

        If (isNegative) Then
          Dim biddingStrategyConfiguration As New BiddingStrategyConfiguration()

          Dim cpcBid As New CpcBid()
          cpcBid.bid = New Money()
          cpcBid.bid.microAmount = bidAmount
          biddingStrategyConfiguration.bids = New Bids() {cpcBid}

          criterion = New BiddableAdGroupCriterion()
          DirectCast(criterion, BiddableAdGroupCriterion).biddingStrategyConfiguration = _
              biddingStrategyConfiguration
        Else
          criterion = New NegativeAdGroupCriterion()
        End If

        criterion.adGroupId = Me.adGroupId
        criterion.criterion = unit

        Me.CreateAddOperation(criterion)

        Return unit
      End Function

      ''' <summary>
      ''' Creates an AdGroupCriterionOperation for the given criterion
      ''' </summary>
      ''' <param name="criterion">The criterion we want to add</param>
      Private Sub CreateAddOperation(ByVal criterion As AdGroupCriterion)
        Dim operation As New AdGroupCriterionOperation()
        operation.operand = criterion
        operation.operator = [Operator].ADD
        Me.operationsField.Add(operation)
      End Sub
    End Class

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New AddProductPartitionTree
      Console.WriteLine(codeExample.Description)
      Try
        Dim adGroupId As Long = Long.Parse("INSERT_ADGROUP_ID_HERE")
        codeExample.Run(New AdWordsUser, adGroupId)
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
        Return "This code example creates a ProductPartition tree."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="adGroupId">The ad group to which product partition is
    ''' added.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long)
      ' Get the AdGroupCriterionService.
      Dim adGroupCriterionService As AdGroupCriterionService = user.GetService( _
          AdWordsService.v201402.AdGroupCriterionService)

      Dim helper As New ProductPartitionHelper(adGroupId)

      ' The most trivial partition tree has only a unit node as the root:
      '   helper.createUnit(null, null, 100000);

      Dim root As ProductPartition = helper.CreateSubdivision(Nothing, Nothing)

      Dim newCondition As New ProductCanonicalCondition()
      newCondition.condition = ProductCanonicalConditionCondition.NEW
      Dim newPartition As ProductPartition = helper.CreateUnit(root, newCondition, 200000, False)

      Dim usedCondition As New ProductCanonicalCondition()
      usedCondition.condition = ProductCanonicalConditionCondition.USED
      Dim usedPartition As ProductPartition = helper.CreateUnit(root, usedCondition, 100000, False)

      Dim otherCondition As ProductPartition = helper.CreateSubdivision(root, _
          New ProductCanonicalCondition())

      Dim coolBrand As New ProductBrand()
      coolBrand.value = "CoolBrand"
      helper.CreateUnit(otherCondition, coolBrand, 900000, False)

      Dim cheapBrand = New ProductBrand()
      cheapBrand.value = "CheapBrand"
      helper.CreateUnit(otherCondition, cheapBrand, 10000, False)

      Dim otherBrand = helper.CreateSubdivision(otherCondition, New ProductBrand())

      ' The value for the bidding category is a fixed ID for the
      ' Luggage & Bags' category. You can retrieve IDs for categories from the
      ' ConstantDataService. See the 'GetProductCategoryTaxonomy' example for
      ' more details.

      Dim luggageAndBags As New ProductBiddingCategory()
      luggageAndBags.type = ProductDimensionType.BIDDING_CATEGORY_L1
      luggageAndBags.value = -5914235892932915235
      helper.CreateUnit(otherBrand, luggageAndBags, 750000, False)

      Dim everythingElse As New ProductBiddingCategory()
      everythingElse.type = ProductDimensionType.BIDDING_CATEGORY_L1

      helper.CreateUnit(otherBrand, everythingElse, 110000, False)

      Try
        ' Make the mutate request.
        Dim retval As AdGroupCriterionReturnValue = adGroupCriterionService.mutate( _
            helper.Operations)

        Dim children As New Dictionary(Of Long, List(Of ProductPartition))
        Dim rootNode As ProductPartition = Nothing
        ' For each criterion, make an array containing each of its children
        ' We always create the parent before the child, so we can rely on that
        ' here.
        For Each adGroupCriterion As AdGroupCriterion In retval.value
          Dim newCriterion As ProductPartition = adGroupCriterion.criterion
          children(newCriterion.id) = New List(Of ProductPartition)

          If (newCriterion.parentCriterionIdSpecified) Then
            children(newCriterion.parentCriterionId).Add(newCriterion)
          Else
            rootNode = DirectCast(adGroupCriterion.criterion, ProductPartition)
          End If
        Next

        Dim writer As New StringWriter()
        ' Show the tree
        DisplayTree(rootNode, children, 0, writer)
        Console.WriteLine(writer.ToString())
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to add bid modifiers to adgroup.", ex)
      End Try
    End Sub

    ''' <summary>
    ''' Displays the product partition tree.
    ''' </summary>
    ''' <param name="node">The root node.</param>
    ''' <param name="children">The child node.</param>
    ''' <param name="level">The tree level.</param>
    ''' <param name="writer">The stream to write output to.</param>
    Private Sub DisplayTree(ByVal node As ProductPartition, _
                            ByVal children As Dictionary(Of Long, List(Of ProductPartition)), _
                            ByVal level As Integer, ByVal writer As StringWriter)
      ' Recursively display a node and each of its children
      Dim value As Object = Nothing
      Dim type As String = ""

      If Not (node.caseValue Is Nothing) Then
        type = node.caseValue.ProductDimensionType
        Select Case (type)
          Case "ProductCanonicalCondition"
            value = DirectCast(node.caseValue, ProductCanonicalCondition).condition.ToString()
            Exit Select

          Case "ProductBiddingCategory"
            value = DirectCast(node.caseValue, ProductBiddingCategory).type.ToString() & _
                "(" & DirectCast(node.caseValue, ProductBiddingCategory).value & ")"
            Exit Select

          Case Else
            value = node.caseValue.GetType().GetProperty("value").GetValue(node.caseValue, Nothing)
            Exit Select
        End Select
      End If

      Console.WriteLine("{0}id: {1}, type: {2}, value: {3}", "".PadLeft(level, " "), _
                        node.id, type, value)
      For Each childNode As ProductPartition In children(node.id)
        DisplayTree(childNode, children, level + 1, writer)
      Next
    End Sub
  End Class

End Namespace

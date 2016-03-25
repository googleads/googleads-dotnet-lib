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
Imports Google.Api.Ads.AdWords.Util.Shopping.v201603

Namespace Google.Api.Ads.AdWords.Examples.VB.v201603
  ''' <summary>
  ''' This code example creates a ProductPartition tree.
  ''' </summary>
  Public Class AddProductPartitionTree
    Inherits ExampleBase

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
      Dim adGroupCriterionService As AdGroupCriterionService = CType(user.GetService( _
          AdWordsService.v201603.AdGroupCriterionService), AdGroupCriterionService)

      ' Build a new ProductPartitionTree using the ad group's current set of criteria.
      Dim partitionTree As ProductPartitionTree = _
          ProductPartitionTree.DownloadAdGroupTree(user, adGroupId)

      Console.WriteLine("Original tree: {0}", partitionTree)

      ' Clear out any existing criteria.
      Dim rootNode As ProductPartitionNode = partitionTree.Root.RemoveAllChildren()

      ' Make the root node a subdivision.
      rootNode = rootNode.AsSubdivision()

      ' Add a unit node for condition = NEW.
      Dim newConditionNode As ProductPartitionNode = rootNode.AddChild(
          ProductDimensions.CreateCanonicalCondition(ProductCanonicalConditionCondition.NEW))
      newConditionNode.AsBiddableUnit().CpcBid = 200000

      Dim usedConditionNode As ProductPartitionNode = rootNode.AddChild(
          ProductDimensions.CreateCanonicalCondition(ProductCanonicalConditionCondition.USED))
      usedConditionNode.AsBiddableUnit().CpcBid = 100000

      ' Add a subdivision node for condition = null (everything else).
      Dim otherConditionNode As ProductPartitionNode =
          rootNode.AddChild(ProductDimensions.CreateCanonicalCondition()).AsSubdivision()

      ' Add a unit node under condition = null for brand = "CoolBrand".
      Dim coolBrandNode As ProductPartitionNode = otherConditionNode.AddChild(
          ProductDimensions.CreateBrand("CoolBrand"))
      coolBrandNode.AsBiddableUnit().CpcBid = 900000L

      ' Add a unit node under condition = null for brand = "CheapBrand".
      Dim cheapBrandNode As ProductPartitionNode = otherConditionNode.AddChild(
          ProductDimensions.CreateBrand("CheapBrand"))
      cheapBrandNode.AsBiddableUnit().CpcBid = 10000L

      ' Add a subdivision node under condition = null for brand = null (everything else).
      Dim otherBrandNode As ProductPartitionNode = otherConditionNode.AddChild(
          ProductDimensions.CreateBrand()).AsSubdivision()

      ' Add unit nodes under condition = null/brand = null.
      ' The value for each bidding category is a fixed ID for a specific
      ' category. You can retrieve IDs for categories from the ConstantDataService.
      ' See the 'GetProductCategoryTaxonomy' example for more details.

      ' Add a unit node under condition = null/brand = null for product type
      ' level 1 = 'Luggage & Bags'.
      Dim luggageAndBagNode As ProductPartitionNode = otherBrandNode.AddChild(
          ProductDimensions.CreateBiddingCategory(ProductDimensionType.BIDDING_CATEGORY_L1,
          -5914235892932915235L))
      luggageAndBagNode.AsBiddableUnit().CpcBid = 750000L

      ' Add a unit node under condition = null/brand = null for product type
      ' level 1 = null (everything else).
      Dim everythingElseNode As ProductPartitionNode = otherBrandNode.AddChild(
          ProductDimensions.CreateBiddingCategory(ProductDimensionType.BIDDING_CATEGORY_L1))
      everythingElseNode.AsBiddableUnit().CpcBid = 110000L

      Try
        ' Make the mutate request, using the operations returned by the ProductPartitionTree.
        Dim mutateOperations As AdGroupCriterionOperation() = partitionTree.GetMutateOperations()

        If mutateOperations.Length = 0 Then
          Console.WriteLine("Skipping the mutate call because the original tree and the " & _
              "current tree are logically identical.")
        Else
          adGroupCriterionService.mutate(mutateOperations)
        End If

        ' The request was successful, so create a new ProductPartitionTree based on the updated
        ' state of the ad group.
        partitionTree = ProductPartitionTree.DownloadAdGroupTree(user, adGroupId)

        Console.WriteLine("Final tree: {0}", partitionTree)
      Catch e As Exception
        Throw New System.ApplicationException("Failed to add bid modifiers to adgroup.", e)
      End Try
    End Sub
  End Class
End Namespace

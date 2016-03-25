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
  ''' This code example restricts the products that will be included in the
  ''' campaign by setting a ProductScope.
  ''' </summary>
  Public Class AddProductScope
    Inherits ExampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example restricts the products that will be included in the " & _
            "campaign by setting a ProductScope."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New AddProductScope
      Console.WriteLine(codeExample.Description)
      Try
        Dim campaignId As Long = Long.Parse("INSERT_CAMPAIGN_ID_HERE")
        codeExample.Run(New AdWordsUser, campaignId)
      Catch e As Exception
        Console.WriteLine("An exception occurred while running this code example. {0}", _
            ExampleUtilities.FormatException(e))
      End Try
    End Sub

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="campaignId">The campaign id to add product scope.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal campaignId As Long)
      ' Get the CampaignCriterionService.
      Dim campaignCriterionService As CampaignCriterionService = CType(user.GetService( _
          AdWordsService.v201603.CampaignCriterionService),  _
          CampaignCriterionService)

      Dim productScope As New ProductScope()
      ' This set of dimensions is for demonstration purposes only. It would be
      ' extremely unlikely that you want to include so many dimensions in your
      ' product scope.
      Dim nexusBrand As New ProductBrand()
      nexusBrand.value = "Nexus"

      Dim newProducts As New ProductCanonicalCondition()
      newProducts.condition = ProductCanonicalConditionCondition.NEW

      Dim customAttribute As New ProductCustomAttribute()
      customAttribute.type = ProductDimensionType.CUSTOM_ATTRIBUTE_0
      customAttribute.value = "my attribute value"

      Dim bookOffer As New ProductOfferId()
      bookOffer.value = "book1"

      Dim mediaProducts As New ProductType()
      mediaProducts.type = ProductDimensionType.PRODUCT_TYPE_L1
      mediaProducts.value = "Media"

      Dim bookProducts As New ProductType()
      bookProducts.type = ProductDimensionType.PRODUCT_TYPE_L2
      bookProducts.value = "Books"

      ' The value for the bidding category is a fixed ID for the
      ' 'Luggage & Bags' category. You can retrieve IDs for categories from
      ' the ConstantDataService. See the 'GetProductCategoryTaxonomy' example
      ' for more details.
      Dim luggageBiddingCategory As New ProductBiddingCategory()
      luggageBiddingCategory.type = ProductDimensionType.BIDDING_CATEGORY_L1
      luggageBiddingCategory.value = -5914235892932915235

      productScope.dimensions = New ProductDimension() {nexusBrand, newProducts, bookOffer, _
          mediaProducts, luggageBiddingCategory}

      Dim campaignCriterion As New CampaignCriterion()
      campaignCriterion.campaignId = campaignId
      campaignCriterion.criterion = productScope

      ' Create operation.
      Dim operation As New CampaignCriterionOperation()
      operation.operand = campaignCriterion
      operation.operator = [Operator].ADD

      Try
        ' Make the mutate request.
        Dim result As CampaignCriterionReturnValue = campaignCriterionService.mutate( _
            New CampaignCriterionOperation() {operation})

        Console.WriteLine("Created a ProductScope criterion with ID '{0}'", _
              result.value(0).criterion.id)
      Catch e As Exception
        Throw New System.ApplicationException("Failed to set shopping product scope.", e)
      End Try
    End Sub
  End Class

End Namespace

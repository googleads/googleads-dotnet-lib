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

Namespace Google.Api.Ads.AdWords.Examples.VB.v201603
  ''' <summary>
  ''' This code example adds demographic target criteria to an ad group. To get
  ''' ad groups, run AddAdGroup.vb.
  ''' </summary>
  Public Class AddAdGroupDemographicCriteria
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New AddAdGroupDemographicCriteria
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
        Return "This code example adds demographic target criteria to an ad group. To get ad " & _
            "groups, run AddAdGroup.vb."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="adGroupId">Id of the ad group to which criteria are
    ''' added.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long)
      ' Get the AdGroupCriterionService.
      Dim adGroupCriterionService As AdGroupCriterionService = CType(user.GetService( _
          AdWordsService.v201603.AdGroupCriterionService), AdGroupCriterionService)

      ' Create biddable ad group criterion for gender
      Dim genderTarget As New Gender()
      ' Criterion Id for male. The IDs can be found here
      ' https://developers.google.com/adwords/api/docs/appendix/genders
      genderTarget.id = 10

      Dim genderBiddableAdGroupCriterion As New BiddableAdGroupCriterion()
      genderBiddableAdGroupCriterion.adGroupId = adGroupId
      genderBiddableAdGroupCriterion.criterion = genderTarget

      ' Create negative ad group criterion for age range
      Dim ageRangeNegative As New AgeRange()
      ' Criterion Id for age 18 to 24. The IDs can be found here
      ' https://developers.google.com/adwords/api/docs/appendix/ages

      ageRangeNegative.id = 503001
      Dim ageRangeNegativeAdGroupCriterion As New NegativeAdGroupCriterion()
      ageRangeNegativeAdGroupCriterion.adGroupId = adGroupId
      ageRangeNegativeAdGroupCriterion.criterion = ageRangeNegative

      ' Create operations.
      Dim genderBiddableAdGroupCriterionOperation As New AdGroupCriterionOperation()
      genderBiddableAdGroupCriterionOperation.operand = genderBiddableAdGroupCriterion
      genderBiddableAdGroupCriterionOperation.operator = [Operator].ADD

      Dim ageRangeNegativeAdGroupCriterionOperation As New AdGroupCriterionOperation()
      ageRangeNegativeAdGroupCriterionOperation.operand = ageRangeNegativeAdGroupCriterion
      ageRangeNegativeAdGroupCriterionOperation.operator = [Operator].ADD

      Dim operations As New List(Of Operation)
      operations.Add(genderBiddableAdGroupCriterionOperation)
      operations.Add(ageRangeNegativeAdGroupCriterionOperation)

      Try
        ' Add ad group criteria.
        Dim result As AdGroupCriterionReturnValue = adGroupCriterionService.mutate( _
            CType(operations.ToArray, AdGroupCriterionOperation()))

        ' Display ad group criteria.
        If (Not result Is Nothing) AndAlso Not (result.value Is Nothing) Then
          For Each adGroupCriterionResult As AdGroupCriterion In result.value
            Console.WriteLine("Ad group criterion with ad group id '{0}', criterion id " & _
                "'{1}', and type '{2}' was added.", adGroupCriterionResult.adGroupId, _
                adGroupCriterionResult.criterion.id, _
                adGroupCriterionResult.criterion.CriterionType)
          Next
        Else
          Console.WriteLine("No ad group criteria were added.")
        End If
      Catch e As Exception
        Throw New System.ApplicationException("Failed to create ad group criteria.", e)
      End Try
    End Sub
  End Class
End Namespace

' Copyright 2011, Google Inc. All Rights Reserved.
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
Imports Google.Api.Ads.AdWords.v201101

Imports System

Namespace Google.Api.Ads.AdWords.Examples.VB.v201101
  ''' <summary>
  ''' This code example deletes a campaign by setting the status to 'DELETED'.
  ''' To get campaigns, run GetAllCampaigns.vb.
  '''
  ''' Tags: AdGroupCriterionService.mutate
  ''' </summary>
  Class DeleteAdGroupCriterion
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example deletes a campaign by setting the status to 'DELETED'. " & _
            "To get campaigns, run GetAllCampaigns.vb."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New DeleteAdGroupCriterion
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the AdGroupCriterionService.
      Dim adGroupCriterionService As AdGroupCriterionService = user.GetService( _
          AdWordsService.v201101.AdGroupCriterionService)

      Dim adGroupId As Long = Long.Parse(_T("INSERT_AD_GROUP_ID_HERE"))
      Dim criterionId As Long = Long.Parse(_T("INSERT_CRITERION_ID_HERE"))

      ' Create base class criterion to avoid setting keyword and placement specific
      ' fields.
      Dim criterion As New Criterion
      criterion.id = criterionId

      ' Create ad group criterion.
      Dim adGroupCriterion As New BiddableAdGroupCriterion
      adGroupCriterion.adGroupId = adGroupId
      adGroupCriterion.criterion = criterion

      ' Create operations.
      Dim operation As New AdGroupCriterionOperation
      operation.operand = adGroupCriterion
      operation.operator = [Operator].REMOVE

      Try
        ' Delete ad group criteria.
        Dim retVal As AdGroupCriterionReturnValue = adGroupCriterionService.mutate( _
            New AdGroupCriterionOperation() {operation})
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing) AndAlso _
            (retVal.value.Length > 0)) Then
          For Each temp As AdGroupCriterion In retVal.value
            Console.WriteLine("Ad group criterion with ad group id = ""{0}"", criterion id = " & _
                """{1}"" and type = ""{2}"" was deleted.", temp.adGroupId, temp.criterion.id, _
                temp.criterion.CriterionType)
          Next
        Else
          Console.WriteLine("No ad group criteria were deleted.")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to delete ad group criteria. Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace

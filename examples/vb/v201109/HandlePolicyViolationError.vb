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
Imports Google.Api.Ads.AdWords.Util
Imports Google.Api.Ads.AdWords.v201109

Imports System
Imports System.Collections.Generic

Namespace Google.Api.Ads.AdWords.Examples.VB.v201109
  ''' <summary>
  ''' This code example adds a text ad, and shows how to handle a policy
  ''' violation.
  '''
  ''' Tags: AdGroupAdService.mutate
  ''' </summary>
  Class HandlePolicyViolationError
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example adds a text ad, and shows how to handle a policy violation."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New HandlePolicyViolationError
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the AdGroupAdService.
      Dim service As AdGroupAdService = user.GetService( _
          AdWordsService.v201109.AdGroupAdService)

      Dim adGroupId As Long = Long.Parse(_T("INSERT_ADGROUP_ID_HERE"))

      ' Create your text ad.
      Dim textAd As New TextAd
      textAd.headline = "Luxury Cruise to Mars"
      textAd.description1 = "Visit the Red Planet in style."
      textAd.description2 = "Low-gravity fun for everyone!!"
      textAd.displayUrl = "www.example.com"
      textAd.url = "http://www.example.com"

      Dim textadGroupAd As New AdGroupAd
      textadGroupAd.adGroupId = adGroupId
      textadGroupAd.ad = textAd

      Dim textAdOperation As New AdGroupAdOperation
      textAdOperation.operator = [Operator].ADD
      textAdOperation.operand = textadGroupAd

      Try
        Dim retVal As AdGroupAdReturnValue = Nothing

        Dim allOperations As New List(Of AdGroupAdOperation)
        Dim operationsToBeRemoved As New List(Of AdGroupAdOperation)

        allOperations.Add(textAdOperation)

        Try
          ' Call the service in validateOnly mode.
          service.RequestHeader.validateOnly = True
          retVal = service.mutate(allOperations.ToArray)
        Catch ex As AdWordsApiException
          Dim innerException As ApiException = TryCast(ex.ApiException, ApiException)
          If (innerException Is Nothing) Then
            Throw New Exception("Failed to retrieve ApiError. See inner exception for more " & _
                "details.", ex)
          End If

          For Each apiError As ApiError In innerException.errors
            Dim index As Integer = ErrorUtilities.GetOperationIndex(apiError.fieldPath)
            If (index = -1) Then
              ' This API error is not associated with an operand.
              Throw
            End If

            If TypeOf apiError Is PolicyViolationError Then
              Dim policyError As PolicyViolationError = apiError

              If policyError.isExemptable Then
                Dim exemptionRequests As New List(Of ExemptionRequest)
                If (Not allOperations.Item(index).exemptionRequests Is Nothing) Then
                  exemptionRequests.AddRange(allOperations.Item(index).exemptionRequests)
                End If

                Dim exemptionRequest As New ExemptionRequest
                exemptionRequest.key = policyError.key
                exemptionRequests.Add(exemptionRequest)
                allOperations.Item(index).exemptionRequests = exemptionRequests.ToArray
              Else
                operationsToBeRemoved.Add(allOperations.Item(index))
              End If
            Else
              operationsToBeRemoved.Add(allOperations.Item(index))
            End If
          Next

          ' Remove all operations that aren't exemptable.
          For Each operation As AdGroupAdOperation In operationsToBeRemoved
            allOperations.Remove(operation)
          Next
        End Try
        If (allOperations.Count > 0) Then
          ' Set valiateOnly to false.
          service.RequestHeader.validateOnly = False
          retVal = service.mutate(allOperations.ToArray)

          If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing) _
              AndAlso (retVal.value.Length > 0)) Then
            For Each tempAdGroupAd As AdGroupAd In retVal.value
              Console.WriteLine("New ad with id = ""{0}"" and displayUrl = ""{1}"" was created.", _
                  tempAdGroupAd.ad.id, tempAdGroupAd.ad.displayUrl)
            Next
          Else
            Console.WriteLine("No ads were created.")
          End If
        Else
          Console.WriteLine("No ads were created.")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to create Ad(s). Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace

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
Imports Google.Api.Ads.AdWords.Util
Imports Google.Api.Ads.AdWords.v201603

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201603
  ''' <summary>
  ''' This code example adds a text ad, and shows how to handle a policy
  ''' violation.
  ''' </summary>
  Public Class HandlePolicyViolationError
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New HandlePolicyViolationError
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
        Return "This code example adds a text ad, and shows how to handle a policy violation."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="adGroupId">Id of the ad group to which ads are added.
    ''' </param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long)
      ' Get the AdGroupAdService.
      Dim service As AdGroupAdService = CType(user.GetService( _
          AdWordsService.v201603.AdGroupAdService), AdGroupAdService)

      ' Create the text ad.
      Dim textAd As New TextAd
      textAd.headline = "Luxury Cruise to Mars"
      textAd.description1 = "Visit the Red Planet in style."
      textAd.description2 = "Low-gravity fun for everyone!!"
      textAd.displayUrl = "www.example.com"
      textAd.url = "http://www.example.com"

      Dim textadGroupAd As New AdGroupAd
      textadGroupAd.adGroupId = adGroupId
      textadGroupAd.ad = textAd

      ' Create the operations.
      Dim textAdOperation As New AdGroupAdOperation
      textAdOperation.operator = [Operator].ADD
      textAdOperation.operand = textadGroupAd

      Try
        Dim retVal As AdGroupAdReturnValue = Nothing

        ' Setup two arrays, one to hold the list of all operations to be
        ' validated, and another to hold the list of operations that cannot be
        ' fixed after validation.
        Dim allOperations As New List(Of AdGroupAdOperation)
        Dim operationsToBeRemoved As New List(Of AdGroupAdOperation)

        allOperations.Add(textAdOperation)

        Try
          ' Validate the operations.
          service.RequestHeader.validateOnly = True
          retVal = service.mutate(allOperations.ToArray)
        Catch e As AdWordsApiException
          Dim innerException As ApiException = TryCast(e.ApiException, ApiException)
          If (innerException Is Nothing) Then
            Throw New Exception("Failed to retrieve ApiError. See inner exception for more " & _
                "details.", e)
          End If

          ' Examine each ApiError received from the server.
          For Each apiError As ApiError In innerException.errors
            Dim index As Integer = ErrorUtilities.GetOperationIndex(apiError.fieldPath)
            If (index = -1) Then
              ' This API error is not associated with an operand, so we cannot
              ' recover from this error by removing one or more operations.
              ' Rethrow the exception for manual inspection.
              Throw
            End If

            ' Handle policy violation errors.
            If TypeOf apiError Is PolicyViolationError Then
              Dim policyError As PolicyViolationError = CType(apiError, PolicyViolationError)

              If policyError.isExemptable Then
                ' If the policy violation error is exemptable, add an exemption
                ' request.
                Dim exemptionRequests As New List(Of ExemptionRequest)
                If (Not allOperations.Item(index).exemptionRequests Is Nothing) Then
                  exemptionRequests.AddRange(allOperations.Item(index).exemptionRequests)
                End If

                Dim exemptionRequest As New ExemptionRequest
                exemptionRequest.key = policyError.key
                exemptionRequests.Add(exemptionRequest)
                allOperations.Item(index).exemptionRequests = exemptionRequests.ToArray
              Else
                ' Policy violation error is not exemptable, remove this
                ' operation from the list of operations.
                operationsToBeRemoved.Add(allOperations.Item(index))
              End If
            Else
              ' This is not a policy violation error, remove this operation
              ' from the list of operations.
              operationsToBeRemoved.Add(allOperations.Item(index))
            End If
          Next

          ' Remove all operations that aren't exemptable.
          For Each operation As AdGroupAdOperation In operationsToBeRemoved
            allOperations.Remove(operation)
          Next
        End Try
        If (allOperations.Count > 0) Then
          ' Perform the operations exemptible of a policy violation.
          service.RequestHeader.validateOnly = False
          retVal = service.mutate(allOperations.ToArray)

          ' Display the results.
          If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing) _
              AndAlso (retVal.value.Length > 0)) Then
            For Each newAdGroupAd As AdGroupAd In retVal.value
              Console.WriteLine("New ad with id = ""{0}"" and displayUrl = ""{1}"" was created.", _
                  newAdGroupAd.ad.id, newAdGroupAd.ad.displayUrl)
            Next
          Else
            Console.WriteLine("No ads were created.")
          End If
        Else
          Console.WriteLine("There are no ads to create after policy violation checks.")
        End If
      Catch e As Exception
        Throw New System.ApplicationException("Failed to create Ad(s).", e)
      End Try
    End Sub
  End Class
End Namespace

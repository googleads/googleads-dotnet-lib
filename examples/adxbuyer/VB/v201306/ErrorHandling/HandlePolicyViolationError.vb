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
Imports Google.Api.Ads.AdWords.Util
Imports Google.Api.Ads.AdWords.v201306

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201306
  ''' <summary>
  ''' This code example adds a text ad, and shows how to handle a policy
  ''' violation.
  '''
  ''' Tags: AdGroupAdService.mutate
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
      Dim service As AdGroupAdService = user.GetService( _
          AdWordsService.v201306.AdGroupAdService)

      ' Create a third party redirect ad that violates a policy.
      ' Create the third party redirect ad.
      Dim redirectAd As New ThirdPartyRedirectAd
      redirectAd.name = String.Format("Policy violation demo ad ", ExampleUtilities.GetRandomString)
      redirectAd.url = "gopher://gopher.google.com"

      redirectAd.dimensions = New Dimensions
      redirectAd.dimensions.height = 250
      redirectAd.dimensions.width = 300

      redirectAd.snippet = "<img src=""https://sandbox.google.com/sandboximages/image.jpg""/>"
      redirectAd.impressionBeaconUrl = "http://www.examples.com/beacon"
      redirectAd.certifiedVendorFormatId = 119
      redirectAd.isCookieTargeted = False
      redirectAd.isUserInterestTargeted = False
      redirectAd.isTagged = False

      Dim redirectAdGroupAd As New AdGroupAd
      redirectAdGroupAd.adGroupId = adGroupId
      redirectAdGroupAd.ad = redirectAd

      ' Create the operations.
      Dim redirectAdOperation As New AdGroupAdOperation
      redirectAdOperation.operator = [Operator].ADD
      redirectAdOperation.operand = redirectAdGroupAd

      Try
        Dim retVal As AdGroupAdReturnValue = Nothing

        ' Setup two arrays, one to hold the list of all operations to be
        ' validated, and another to hold the list of operations that cannot be
        ' fixed after validation.
        Dim allOperations As New List(Of AdGroupAdOperation)
        Dim operationsToBeRemoved As New List(Of AdGroupAdOperation)

        allOperations.Add(redirectAdOperation)

        Try
          ' Validate the operations.
          service.RequestHeader.validateOnly = True
          retVal = service.mutate(allOperations.ToArray)
        Catch ex As AdWordsApiException
          Dim innerException As ApiException = TryCast(ex.ApiException, ApiException)
          If (innerException Is Nothing) Then
            Throw New Exception("Failed to retrieve ApiError. See inner exception for more " & _
                "details.", ex)
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
              Dim policyError As PolicyViolationError = apiError

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
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to create Ad(s).", ex)
      End Try
    End Sub
  End Class
End Namespace

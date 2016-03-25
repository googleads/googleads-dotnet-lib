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
Imports System.Net
Imports System.Threading

Namespace Google.Api.Ads.AdWords.Examples.VB.v201603
  ''' <summary>
  ''' This code example shows how to handle RateExceededError in your
  ''' application. To trigger the rate exceeded error, this code example runs
  ''' 100 threads in parallel, each thread attempting to validate 100 keywords
  ''' in a single request. Note that spawning 100 parallel threads is for
  ''' illustrative purposes only, you shouldn't do this in your application.
  ''' </summary>
  Public Class HandleRateExceededError
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New HandleRateExceededError
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
        Return "This code example shows how to handle RateExceededError in your application. " & _
            "To trigger the rate exceeded error, this code example runs 100 threads in " & _
            "parallel, each thread attempting to validate 100 keywords in a single request. " & _
            "Note that spawning 100 parallel threads is for illustrative purposes only, you " & _
            "shouldn't do this in your application."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="adGroupId">Id of the ad group to which keywords are added.
    ''' </param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long)
      Const NUM_THREADS As Integer = 100

      ' Increase the maximum number of parallel HTTP connections that .NET
      ' framework allows. By default, this is set to 2 by the .NET framework.
      ServicePointManager.DefaultConnectionLimit = NUM_THREADS

      Dim threads As New List(Of Thread)

      For i As Integer = 0 To NUM_THREADS
        Dim thread As New Thread(AddressOf New KeywordThread(user, i, adGroupId).Run)
        threads.Add(thread)
      Next i

      For i As Integer = 0 To threads.Count - 1
        threads.Item(i).Start(i)
      Next i

      For i As Integer = 0 To threads.Count - 1
        threads.Item(i).Join()
      Next i
    End Sub

    ''' <summary>
    ''' Thread class for validating keywords.
    ''' </summary>
    Public Class KeywordThread
      ''' <summary>
      ''' Index of this thread, for identifying and debugging.
      ''' </summary>
      Dim threadIndex As Integer

      ''' <summary>
      ''' The ad group id to which keywords are added.
      ''' </summary>
      Dim adGroupId As Long

      ''' <summary>
      ''' The AdWords user who owns this ad group.
      ''' </summary>
      Dim user As AdWordsUser

      ''' <summary>
      ''' Number of keywords to be validated in each API call.
      ''' </summary>
      Const NUM_KEYWORDS As Integer = 100

      ''' <summary>
      ''' Initializes a new instance of the <see cref="KeywordThread" /> class.
      ''' </summary>
      ''' <param name="threadIndex">Index of the thread.</param>
      ''' <param name="adGroupId">The ad group id.</param>
      ''' <param name="user">The AdWords user who owns the ad group.</param>
      Public Sub New(ByVal user As AdWordsUser, ByVal threadIndex As Integer, _
          ByVal adGroupId As Long)
        Me.user = user
        Me.threadIndex = threadIndex
        Me.adGroupId = adGroupId
      End Sub

      ''' <summary>
      ''' Main method for the thread.
      ''' </summary>
      ''' <param name="obj">The thread parameter.</param>
      Public Sub Run(ByVal obj As Object)
        ' Create the operations.
        Dim operations As New List(Of AdGroupCriterionOperation)
        For j As Integer = 0 To NUM_KEYWORDS
          ' Create the keyword.
          Dim keyword As New Keyword
          keyword.text = "mars cruise thread " & threadIndex.ToString & " seed " & j.ToString
          keyword.matchType = KeywordMatchType.BROAD

          ' Create the biddable ad group criterion.
          Dim keywordCriterion As AdGroupCriterion = New BiddableAdGroupCriterion
          keywordCriterion.adGroupId = adGroupId
          keywordCriterion.criterion = keyword

          ' Create the operations.
          Dim keywordOperation As New AdGroupCriterionOperation
          keywordOperation.operator = [Operator].ADD
          keywordOperation.operand = keywordCriterion

          operations.Add(keywordOperation)
        Next j

        ' Get the AdGroupCriterionService. This should be done within the
        ' thread, since a service can only handle one outgoing HTTP request
        ' at a time.
        Dim service As AdGroupCriterionService = CType(user.GetService( _
            AdWordsService.v201603.AdGroupCriterionService), AdGroupCriterionService)
        service.RequestHeader.validateOnly = True
        Dim retryCount As Integer = 0
        Const NUM_RETRIES As Integer = 3
        Try
          While (retryCount < NUM_RETRIES)
            Try
              ' Validate the keywords.
              Dim retval As AdGroupCriterionReturnValue = service.mutate(operations.ToArray)
              Exit While
            Catch e As AdWordsApiException
              ' Handle API errors.
              Dim innerException As ApiException = TryCast(e.ApiException, ApiException)
              If (innerException Is Nothing) Then
                Throw New Exception("Failed to retrieve ApiError. See inner exception for more " & _
                    "details.", e)
              End If
              For Each apiError As ApiError In innerException.errors
                If Not TypeOf apiError Is RateExceededError Then
                  ' Rethrow any errors other than RateExceededError.
                  Throw
                End If
                ' Handle rate exceeded errors.
                Dim rateExceededError As RateExceededError = DirectCast(apiError, RateExceededError)
                Console.WriteLine("Got Rate exceeded error - rate name = '{0}', scope = '{1}', " & _
                    "retry After {2} seconds.", rateExceededError.rateScope, _
                    rateExceededError.rateName, rateExceededError.retryAfterSeconds)
                Thread.Sleep(rateExceededError.retryAfterSeconds)
                retryCount = retryCount + 1
              Next
            Finally
              If (retryCount = NUM_RETRIES) Then
                Throw New Exception(String.Format("Could not recover after making {0} attempts.", _
                    retryCount))
              End If
            End Try
          End While
        Catch e As Exception
          Throw New System.ApplicationException("Failed to validate keywords.", e)
        End Try
      End Sub
    End Class
  End Class
End Namespace

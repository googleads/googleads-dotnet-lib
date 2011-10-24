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
Imports Google.Api.Ads.AdWords.v201109

Imports System
Imports System.Collections.Generic
Imports System.Text.RegularExpressions
Imports System.Threading

Namespace Google.Api.Ads.AdWords.Examples.VB.v201109
  ''' <summary>
  ''' This code example shows how to add ads and keywords using the
  ''' MutateJobService.
  '''
  ''' Tags: MutateJobService.mutate
  ''' </summary>
  Class PerformMutateJob
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example shows how to add ads and keywords using the " & _
            "MutateJobService."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New PerformMutateJob
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      Dim mutateJobService As MutateJobService = user.GetService( _
          AdWordsService.v201109.MutateJobService)

      Const RETRY_INTERVAL As Integer = 30
      Const RETRIES_COUNT As Integer = 30
      Const KEYWORD_NUMBER As Integer = 100
      Const INDEX_REGEX As String = "operations\[(\d+)\].operand"

      Dim adGroupId As Long = Long.Parse(_T("INSERT_ADGROUP_ID_HERE"))

      Dim operations As New List(Of Operation)

      ' Create an AdGroupAdOperation to add a text ad.
      Dim adGroupAdOperation As New AdGroupAdOperation
      adGroupAdOperation.operator = [Operator].ADD

      Dim textAd As New TextAd
      textAd.headline = "Luxury Cruise to Mars"
      textAd.description1 = "Visit the Red Planet in style."
      textAd.description2 = "Low-gravity fun for everyone!"
      textAd.displayUrl = "www.example.com"
      textAd.url = "http://www.example.com"

      Dim adGroupAd As New AdGroupAd
      adGroupAd.adGroupId = adGroupId
      adGroupAd.ad = textAd
      adGroupAdOperation.operand = adGroupAd
      operations.Add(adGroupAdOperation)

      Dim i As Integer

      For i = 0 To KEYWORD_NUMBER
        Dim keyword As New Keyword
        keyword.text = String.Format("mars cruise {0}", i)
        keyword.matchType = KeywordMatchType.BROAD

        Dim criterion As New BiddableAdGroupCriterion
        criterion.adGroupId = adGroupId
        criterion.criterion = keyword

        Dim adGroupCriterionOperation As New AdGroupCriterionOperation
        adGroupCriterionOperation.operator = [Operator].ADD
        adGroupCriterionOperation.operand = criterion
        operations.Add(adGroupCriterionOperation)
      Next i

      Dim policy As New BulkMutateJobPolicy
      ' You can specify up to 3 job IDs that must successfully complete before
      ' this job can be processed.
      policy.prerequisiteJobIds = New Long() {}

      Dim job As SimpleMutateJob = mutateJobService.mutate(operations.ToArray, policy)

      Dim completed As Boolean = False
      Dim retryCount As Integer = 0
      Console.WriteLine("Retrieving job status...")

      Dim selector As BulkMutateJobSelector
      Do While Not completed
        selector = New BulkMutateJobSelector
        selector.jobIds = New Long() {job.id}
        Try
          Dim allJobs As Job() = mutateJobService.get(Selector)
          If ((Not allJobs Is Nothing) AndAlso (allJobs.Length > 0)) Then
            job = allJobs(0)
            If ((job.status = BasicJobStatus.COMPLETED) OrElse _
                (job.status = BasicJobStatus.FAILED)) Then
              completed = True
              Exit Do
            Else
              Console.WriteLine("{0}: Current status is {1}, waiting {2} seconds to retry...", _
                  retryCount, job.status, RETRY_INTERVAL)
              Thread.Sleep(RETRY_INTERVAL * 1000)
              retryCount = retryCount + 1
            End If
          End If
        Catch ex As Exception
          Console.WriteLine("Failed to fetch bulk mutate job with id = {0}. Exception " & _
              "says ""{1}"".", job.id, ex.Message)
          Return
        End Try
      Loop
      If (job.status = BasicJobStatus.COMPLETED) Then
        selector = New BulkMutateJobSelector
        selector.jobIds = New Long() {job.id}
        Dim jobResult As JobResult = mutateJobService.getResult(selector)
        If Not jobResult Is Nothing Then
          Dim results As SimpleMutateResult = jobResult.Item
          If Not results Is Nothing Then
            If Not results.results Is Nothing Then
              For i = 0 To results.results.Length - 1
                Dim operand As Operand = results.results(i)
                Dim status As String
                If TypeOf operand.Item Is PlaceHolder Then
                  status = "FAILED"
                Else
                  status = "SUCCEEDED"
                End If
                Console.WriteLine("Operation {0} - {1}", i, status)
              Next i
            End If

            If Not results.errors Is Nothing Then
              For Each apiError As ApiError In results.errors
                Dim match As Match = Regex.Match(apiError.fieldPath, INDEX_REGEX, _
                    RegexOptions.IgnoreCase)
                Dim index As String = "???"
                If (match.Success) Then
                  index = match.Groups(1).Value
                End If
                Console.WriteLine("Operation index {0} failed due to reason: '{1}', " & _
                    "trigger: '{2}'", index, apiError.errorString, apiError.trigger)
              Next
            End If
          End If
        End If
        Console.WriteLine("Job completed successfully!")
      ElseIf (job.status = BasicJobStatus.FAILED) Then
        Console.WriteLine("Job failed with reason: " & job.failureReason.ToString())
      ElseIf (job.status = BasicJobStatus.PROCESSING OrElse job.status = BasicJobStatus.PENDING) _
          Then
        Console.WriteLine("Job did not complete in {0} secconds.", RETRY_INTERVAL * RETRIES_COUNT)
      End If

    End Sub
  End Class
End Namespace

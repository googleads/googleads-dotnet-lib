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
Imports Google.Api.Ads.AdWords.v201008

Imports System
Imports System.Threading

Namespace Google.Api.Ads.AdWords.Examples.VB.v201008
  ''' <summary>
  ''' This code example shows how to add ads and keywords using the
  ''' BulkMutateJobService.
  '''
  ''' Tags: BulkMutateJobService.mutate
  ''' </summary>
  Class PerformBulkMutateJob
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example shows how to add ads and keywords using the BulkMutateJobService."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New PerformBulkMutateJob
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the BulkMutateJobService.
      Dim bmjService As BulkMutateJobService = user.GetService( _
          AdWordsService.v201008.BulkMutateJobService)

      Dim campaignId As Long = Long.Parse(_T("INSERT_CAMPAIGN_ID_HERE"))
      Dim adGroupId As Long = Long.Parse(_T("INSERT_ADGROUP_ID_HERE"))

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

      ' Add that operation into the first stream.
      Dim adOpStream As New OperationStream
      adOpStream.scopingEntityId = New EntityId
      adOpStream.scopingEntityId.type = EntityIdType.CAMPAIGN_ID
      adOpStream.scopingEntityId.value = campaignId

      adOpStream.operations = New Operation() {adGroupAdOperation}

      ' Create AdGroupCriterionOperations to add keywords.
      Dim adGroupCriterionOperations As AdGroupCriterionOperation() = _
          New AdGroupCriterionOperation(100) {}

      For i As Integer = 0 To 100
        Dim keyword As New Keyword
        keyword.text = String.Format("mars cruise {0}", i)
        keyword.matchType = KeywordMatchType.BROAD

        Dim criterion As New BiddableAdGroupCriterion
        criterion.adGroupId = adGroupId
        criterion.criterion = keyword

        Dim adGroupCriterionOperation As New AdGroupCriterionOperation
        adGroupCriterionOperation.operator = [Operator].ADD

        adGroupCriterionOperation.operand = criterion
        adGroupCriterionOperations(i) = adGroupCriterionOperation
      Next i

      ' Add those operation into the second stream.
      Dim keywordOpStream As New OperationStream

      keywordOpStream.scopingEntityId = New EntityId
      keywordOpStream.scopingEntityId.type = EntityIdType.CAMPAIGN_ID
      keywordOpStream.scopingEntityId.value = campaignId

      keywordOpStream.operations = adGroupCriterionOperations

      ' Create a job.

      ' a. Create a bulk job object.
      Dim bulkJobId As Long = 0
      Dim bulkJob As BulkMutateJob = Nothing

      bulkJob = New BulkMutateJob
      bulkJob.numRequestParts = 2

      ' b. Create a part of the job.
      Dim bulkRequest1 As New BulkMutateRequest
      bulkRequest1.partIndex = 0
      bulkRequest1.operationStreams = New OperationStream() {adOpStream}
      bulkJob.request = bulkRequest1

      ' c. Create job operation.
      Dim jobOperation1 As New JobOperation
      jobOperation1.operator = [Operator].ADD
      jobOperation1.operand = bulkJob

      ' d. Call mutate().
      Try
        bulkJobId = bmjService.mutate(jobOperation1).id
      Catch ex As Exception
        Console.WriteLine("Failed to create bulk mutate job. Exception says ""{0}""", ex.Message)
        Return
      End Try

      ' Similarly, create the next part of the job.

      ' Note: since we already created a job earlier, this time we modify it.
      bulkJob = New BulkMutateJob
      bulkJob.id = bulkJobId

      Dim bulkRequest2 As New BulkMutateRequest
      bulkRequest2.partIndex = 1
      bulkRequest2.operationStreams = New OperationStream() {keywordOpStream}
      bulkJob.request = bulkRequest2

      Dim jobOperation2 As New JobOperation
      jobOperation2.operator = [Operator].SET
      jobOperation2.operand = bulkJob

      Try
        bulkJob = bmjService.mutate(jobOperation2)
        bulkJobId = bulkJob.id
      Catch ex As Exception
        Console.WriteLine("Failed to modify bulk mutate job with id = {0}. Exception says " & _
            """{1}""", bulkJobId, ex.Message)
        Return
      End Try

      ' Wait for the job to complete.
      Dim completed As Boolean = False

      Do While Not completed
        Thread.Sleep(2000)

        Dim selector As New BulkMutateJobSelector
        selector.jobIds = New Long() {bulkJobId}

        Try
          Dim allJobs As BulkMutateJob() = bmjService.get(selector)
          If ((Not allJobs Is Nothing) AndAlso (allJobs.Length > 0)) Then
            If ((allJobs(0).status = BasicJobStatus.COMPLETED) OrElse _
                (allJobs(0).status = BasicJobStatus.FAILED)) Then
              completed = True
              bulkJob = allJobs(0)
              Exit Do
            End If
          End If
        Catch ex As Exception
          Console.WriteLine("Failed to fetch bulk mutate job with id = {0}. Exception says " & _
              """{1}""", bulkJobId, ex.Message)
          Return
        End Try
      Loop

      If (bulkJob.status = BasicJobStatus.COMPLETED) Then
        For i As Integer = 0 To bulkJob.numRequestParts - 1
          Dim selector As New BulkMutateJobSelector
          selector.jobIds = New Long() {bulkJobId}
          selector.resultPartIndex = i

          Dim allJobParts As BulkMutateJob() = bmjService.get(selector)
          For Each jobPart As BulkMutateJob In allJobParts
            Console.WriteLine("Part {0}/{1} of job '{2}' has successfully completed.", _
                jobPart.result.partIndex + 1, bulkJob.numRequestParts, jobPart.id)
          Next
        Next i
        Console.WriteLine("Job completed successfully!")
      Else
        Console.WriteLine("Job could not be completed.")
      End If
    End Sub
  End Class
End Namespace

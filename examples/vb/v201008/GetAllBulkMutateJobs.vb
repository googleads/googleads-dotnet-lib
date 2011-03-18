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

Namespace Google.Api.Ads.AdWords.Examples.VB.v201008
  ''' <summary>
  ''' This code example gets all bulk mutate jobs in the account. To add a
  ''' bulk mutate job, run PerformBulkMutateJob.vb.
  '''
  ''' Tags: BulkMutateJobService.get
  ''' </summary>
  Class GetAllBulkMutateJobs
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example gets all bulk mutate jobs in the account. To add a bulk " & _
            "mutate job, run PerformBulkMutateJob.vb."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New GetAllBulkMutateJobs
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the BulkMutateJobService.
      Dim bulkMutateJobService As BulkMutateJobService = user.GetService( _
          AdWordsService.v201008.BulkMutateJobService)

      ' Create selector.
      Dim selector As New BulkMutateJobSelector
      selector.includeStats = True

      Try
        ' Get all bulk mutate jobs.
        Dim bulkMutateJobs As BulkMutateJob() = bulkMutateJobService.get(selector)
        If (Not bulkMutateJobs Is Nothing) Then
          ' Display bulk mutate jobs.
          For Each bulkMutateJob As BulkMutateJob In bulkMutateJobs
            Console.WriteLine("Bulk mutate job with id '{0}' and status '{1}' was found.", _
                bulkMutateJob.id, bulkMutateJob.status)
            Select Case bulkMutateJob.status
              Case BasicJobStatus.COMPLETED
                Dim bulkMutateJobStats as BulkMutateJobStats = bulkMutateJob.stats
                Console.WriteLine("  Total operations: {0}, failed: {1}, unprocessed: {2}.", _
                    bulkMutateJobStats.numOperations, bulkMutateJobStats.numFailedOperations, _
                    bulkMutateJobStats.numUnprocessedOperations)
                Exit Select
              Case BasicJobStatus.PROCESSING
                Console.WriteLine("  Percent complete: {0}.", bulkMutateJob.stats.progressPercent)
                Exit Select
              Case BasicJobStatus.FAILED
                Console.WriteLine("  Failure reason: %s.", bulkMutateJob.failureReason.Item)
                Exit Select
              Case BasicJobStatus.PENDING
                Console.WriteLine("  Total parts: {0}, parts received: {1}.", _
                    bulkMutateJob.numRequestParts, bulkMutateJob.numRequestPartsReceived)
                Exit Select
            End Select
          Next
        Else
          Console.WriteLine("No bulk mutate jobs were found.\n")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to get all bulk mutate jobs. Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace

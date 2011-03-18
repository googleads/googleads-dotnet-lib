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
  ''' This code example deletes a bulk mutate job using the 'REMOVE' operator.
  ''' Jobs may only deleted if they are in the 'PENDING' state and have not
  ''' yet receieved all of their request parts. To get bulk mutate jobs,
  ''' run GetAllBulkMutateJobs.vb.
  '''
  ''' Tags: BulkMutateJobService.mutate
  ''' </summary>
  Class DeleteBulkMutateJob
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example deletes a bulk mutate job using the 'REMOVE' operator. Jobs " & _
            "may only deleted if they are in the 'PENDING' state and have not yet receieved " & _
            "all of their request parts. To get bulk mutate jobs, run GetAllBulkMutateJobs.vb."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New DeleteBulkMutateJob
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

      Dim bulkMutateJobId As Long = Long.Parse(_T("INSERT_BULK_MUTATE_JOB_ID_HERE"))

      ' Create BulkMutateJob.
      Dim bulkMutateJob As New BulkMutateJob
      bulkMutateJob.id = bulkMutateJobId

      ' Create operation.
      Dim operation As New JobOperation
      operation.operand = bulkMutateJob
      operation.operator = [Operator].REMOVE

      Try
        ' Delete bulk mutate job.
        bulkMutateJob = bulkMutateJobService.mutate(operation)

        ' Display bulk mutate jobs.
        If (Not bulkMutateJob Is Nothing) Then
          Console.WriteLine("Bulk mutate job with id '{0}' was deleted.", bulkMutateJob.id)
        Else
          Console.WriteLine("No bulk mutate jobs were deleted.")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to delete bulk mutate jobs. Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace

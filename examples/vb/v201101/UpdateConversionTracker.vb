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
  ''' This code example updates a conversion tracker by setting its status to
  ''' 'DISABLED'. To get conversion trackers, run GetAllConversionTrackers.vb.
  '''
  ''' Tags: ConversionTrackerService.mutate
  ''' </summary>
  Friend Class UpdateConversionTracker
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    '''
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example updates a conversion tracker by setting its status to " & _
            "'DISABLED'. To get conversion trackers, run GetAllConversionTrackers.vb."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New UpdateConversionTracker
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the ConversionTrackerService.
      Dim conversionTrackerService As ConversionTrackerService = user.GetService( _
          AdWordsService.v201101.ConversionTrackerService)

      Dim conversionTrackerId As Long = Long.Parse(_T("INSERT_CONVERSION_TRACKER_ID_HERE"))

      ' Create conversion tracker with updated status.
      Dim conversionTracker As New AdWordsConversionTracker
      conversionTracker.id = conversionTrackerId
      conversionTracker.status = ConversionTrackerStatus.DISABLED

      ' Create operations.
      Dim operation As New ConversionTrackerOperation
      operation.operand = conversionTracker
      operation.operator = [Operator].SET

      Try
        ' Update conversion.
        Dim retval As ConversionTrackerReturnValue = conversionTrackerService.mutate( _
            New ConversionTrackerOperation() {operation})

        ' Display conversions.
        If (Not retval Is Nothing AndAlso (Not retval.value Is Nothing) AndAlso _
            retval.value.Length > 0) Then
          For Each tempConversionTracker As ConversionTracker In retval.value
            Console.WriteLine("Conversion tracker with id '{0}' , name '{1}', status '{2}', " & _
                "category '{3}' was disabled.", tempConversionTracker.id, _
                tempConversionTracker.name, tempConversionTracker.status, _
                tempConversionTracker.category)
          Next
        Else
          Console.WriteLine("No conversion trackers were disabled.")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to update conversion tracker. Exception says ""{0}""", _
            ex.Message)
      End Try
    End Sub
  End Class
End Namespace

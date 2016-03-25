' Copyright 2016, Google Inc. All Rights Reserved.
'
' Licensed under the Apache License, Version 2.0 (the "License")
' you may not use this file except in compliance with the License.
' You may obtain a copy of the License at
'
'     http:'www.apache.org/licenses/LICENSE-2.0
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

Namespace Google.Api.Ads.AdWords.Examples.VB.v201603
  ''' <summary>
  ''' This code example imports offline conversion values for specific clicks to
  ''' your account. To get Google Click ID for a click, run
  ''' CLICK_PERFORMANCE_REPORT. To set up a conversion tracker, run the
  ''' AddConversionTracker.vb example.
  ''' </summary>
  Public Class UploadOfflineConversions
    Inherits ExampleBase

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim conversionName As String = "INSERT_CONVERSION_NAME_HERE"
      ' GCLID needs to be newer than 30 days.
      Dim gClId As String = "INSERT_GOOGLE_CLICK_ID_HERE"
      '  The conversion time should be higher than the click time.
      Dim conversionTime As String = "INSERT_CONVERSION_TIME_HERE"
      Dim conversionValue As Double = Double.Parse("INSERT_CONVERSION_VALUE_HERE")

      Dim codeExample As New UploadOfflineConversions
      Console.WriteLine(codeExample.Description)
      Try
        codeExample.Run(New AdWordsUser, conversionName, gClId, conversionTime, conversionValue)
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
        Return "This code example imports offline conversion values for specific clicks to " & _
            "your account. To get Google Click ID for a click, run CLICK_PERFORMANCE_REPORT. " & _
            "To set up a conversion tracker, run the AddConversionTracker.vb example."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="conversionName">The name of the upload conversion to be
    ''' created.</param>
    ''' <param name="gClid">The Google Click ID of the click for which offline
    ''' conversions are uploaded.</param>
    ''' <param name="conversionValue">The conversion value to be uploaded.
    ''' </param>
    ''' <param name="conversionTime">The conversion time, in yyyymmdd hhmmss
    ''' format.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal conversionName As String, _
        ByVal gClid As String, ByVal conversionTime As String, ByVal conversionValue As Double)
      ' Get the OfflineConversionFeedService.
      Dim offlineConversionFeedService As OfflineConversionFeedService = CType(user.GetService( _
              AdWordsService.v201603.OfflineConversionFeedService),  _
              OfflineConversionFeedService)

      Try
        ' Associate offline conversions with the existing named conversion tracker. If
        ' this tracker was newly created, it may be a few hours before it can accept
        ' conversions.
        Dim feed As New OfflineConversionFeed()
        feed.conversionName = conversionName
        feed.conversionTime = conversionTime
        feed.conversionValue = conversionValue
        feed.googleClickId = gClid

        Dim offlineConversionOperation As New OfflineConversionFeedOperation()
        offlineConversionOperation.operator = [Operator].ADD
        offlineConversionOperation.operand = feed

        Dim offlineConversionRetval As OfflineConversionFeedReturnValue = _
            offlineConversionFeedService.mutate( _
                New OfflineConversionFeedOperation() {offlineConversionOperation})

        Dim newFeed As OfflineConversionFeed = offlineConversionRetval.value(0)

        Console.WriteLine("Uploaded offline conversion value of {0} for Google Click ID = " & _
            "'{1}' to '{2}'.", newFeed.conversionValue, newFeed.googleClickId, _
            newFeed.conversionName)
      Catch e As Exception
        Throw New System.ApplicationException("Failed to upload offline conversions.", e)
      End Try
    End Sub
  End Class
End Namespace

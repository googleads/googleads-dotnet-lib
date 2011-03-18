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
  ''' This code example updates an ad's status given an existing ad group
  ''' and ad.
  '''
  ''' Tags: AdGroupAdService.mutate
  ''' </summary>
  Class UpdateAd
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example updates an ad's status given an existing ad group and ad."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New UpdateAd
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
          AdWordsService.v201101.AdGroupAdService)

      Dim adGroupId As Long = Long.Parse(_T("INSERT_AD_GROUP_ID_HERE"))
      Dim adId As Long = Long.Parse(_T("INSERT_AD_ID_HERE"))
      Dim status As AdGroupAdStatus = System.Enum.Parse(GetType(AdGroupAdStatus), _
          _T("INSERT_AD_GROUP_AD_STATUS_HERE"))

      ' Update your Ad.
      Dim adGroupAd As New AdGroupAd
      adGroupAd.status = status
      adGroupAd.adGroupId = adGroupId

      adGroupAd.ad = New Ad
      adGroupAd.ad.id = adId

      Dim adGroupAdOperation As New AdGroupAdOperation
      adGroupAdOperation.operator = [Operator].SET
      adGroupAdOperation.operand = adGroupAd

      Try
        Dim retVal As AdGroupAdReturnValue = service.mutate( _
            New AdGroupAdOperation() {adGroupAdOperation})
        If ((Not retVal.value Is Nothing) AndAlso (retVal.value.Length > 0)) Then
          Dim tempAdGroupAd As AdGroupAd = retVal.value(0)
          Console.WriteLine("Status of ad with id ""{0}"" was set to ""{1}""", _
              tempAdGroupAd.ad.id, tempAdGroupAd.status)
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to update Ad. Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace

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
Imports Google.Api.Ads.AdWords.v13

Imports System

Namespace Google.Api.Ads.AdWords.Examples.VB.v13
  ''' <summary>
  ''' This code example displays some of the client account's info.
  ''' </summary>
  Class AccountServiceDemo
    Inherits SampleBase
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example displays some of the client account's info."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New AccountServiceDemo
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the service.
      Dim service As AccountService = user.GetService(AdWordsService.v13.AccountService)
      service.clientEmailValue = Nothing

      ' Gets account's info.
      Dim acctInfo As AccountInfo = service.getAccountInfo
      Console.WriteLine("----- Account Info -----\nCustomer Id: {0}\nDescriptive Name: {1}", _
          acctInfo.customerId, acctInfo.descriptiveName)
      If (Not acctInfo.billingAddress Is Nothing) Then
        Console.WriteLine( _
            "Billing information\n" & _
            "   Company Name: {0}\n   Address Line 1: {1}\n" & _
            "   Address Line 2: {2}\n" & _
            "   City: {3}\n" & _
            "   State: {4}\n" & _
            "   Postal Code: {5}\n" & _
            "   Country Code: {6}", _
            acctInfo.billingAddress.companyName, acctInfo.billingAddress.addressLine1, _
            acctInfo.billingAddress.addressLine2, acctInfo.billingAddress.city, _
            acctInfo.billingAddress.state, acctInfo.billingAddress.postalCode, _
            acctInfo.billingAddress.countryCode)
      End If
      Console.WriteLine("Time Zone ID: {0}------------------------", acctInfo.timeZoneId)
    End Sub
  End Class
End Namespace

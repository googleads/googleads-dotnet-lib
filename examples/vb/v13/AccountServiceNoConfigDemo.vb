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
Imports System.Collections.Generic

Namespace Google.Api.Ads.AdWords.Examples.VB.v13
  ''' <summary>
  ''' This code example displays some of the account's info. It also
  ''' demonstrates how to override the settings from App.config.
  ''' </summary>
  Class AccountServiceNoConfigDemo
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example displays some of the account's info. It also demonstrates " & _
            "how to override the settings from App.config."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New AccountServiceNoConfigDemo
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Declare the headers.
      Dim headers As New Dictionary(Of String, String)

      headers.Add("email", "ENTER_YOUR_EMAIL_HERE")
      headers.Add("password", "ENTER_YOUR_PASSWORD_HERE")
      headers.Add("useragent", "ENTER_YOUR_COMPANY_NAME_HERE")
      headers.Add("developerToken", "ENTER_YOUR_DEVELOPER_TOKEN_HERE")
      headers.Add("applicationToken", "ENTER_YOUR_APPLICATION_TOKEN_HERE")
      headers.Add("clientEmail", "ENTER_YOUR_CLIENT_EMAIL_HERE")

      ' Create a custom AdWordsUser.
      user = New AdWordsUser(headers)

      ' Get the service.
      Dim service As AccountService = user.GetService(AdWordsService.v13.AccountService)

      ' Gets account's info.
      Dim acctInfo As AccountInfo = service.getAccountInfo

      Console.WriteLine("----- Account Info -----\nCustomer Id: {0}\nDescriptive Name: {1}", _
          acctInfo.customerId, acctInfo.descriptiveName)

      If (Not Nothing Is acctInfo.billingAddress) Then
        Console.WriteLine( _
            "Billing information\n" & _
            "   Company Name: {0}\n" & _
            "   Address Line 1: {1}\n" & _
            "   Address Line 2: {2}\n" & _
            "   City: {3}\n" & _
            "   State: {4}\n" & _
            "   Postal Code: {5}" & _
            "   Country Code: {6}", _
            acctInfo.billingAddress.companyName, acctInfo.billingAddress.addressLine1, _
            acctInfo.billingAddress.addressLine2, acctInfo.billingAddress.city, _
            acctInfo.billingAddress.state, acctInfo.billingAddress.postalCode, _
            acctInfo.billingAddress.countryCode)
      End If

      Console.WriteLine("Time Zone ID: {0}\n------------------------", acctInfo.timeZoneId)
    End Sub
  End Class
End Namespace

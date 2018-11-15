' Copyright 2018 Google LLC
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
Imports Google.Api.Ads.AdWords.v201809
Imports Google.Api.Ads.Common.Util

Namespace Google.Api.Ads.AdWords.Examples.VB.v201809
    ''' <summary>
    ''' This code example uploads an image asset. To get images, run GetAllImageAssets.vb.
    ''' </summary>
    Public Class UploadImageAsset
        Inherits ExampleBase

        ''' <summary>
        ''' Main method, to run this code example as a standalone application.
        ''' </summary>
        ''' <param name="args">The command line arguments.</param>
        Public Shared Sub Main(ByVal args As String())
            Dim codeExample As New UploadImageAsset
            Console.WriteLine(codeExample.Description)
            Try
                codeExample.Run(New AdWordsUser)
            Catch e As Exception
                Console.WriteLine("An exception occurred while running this code example. {0}",
                                  ExampleUtilities.FormatException(e))
            End Try
        End Sub

        ''' <summary>
        ''' Returns a description about the code example.
        ''' </summary>
        Public Overrides ReadOnly Property Description() As String
            Get
                Return "This code example uploads an image asset. To get images, run " +
                       "GetAllImageAssets.vb."
            End Get
        End Property

        ''' <summary>
        ''' Runs the code example.
        ''' </summary>
        ''' <param name="user">The AdWords user.</param>
        Public Sub Run(ByVal user As AdWordsUser)
            ' [START upload_image_asset] MOE:strip_line
            Using assetService As AssetService = CType(
                user.GetService(
                    AdWordsService.v201809.AssetService),
                AssetService)

                ' Create the image asset.
                Dim imageAsset As New ImageAsset()
                ' Optional: Provide a unique friendly name to identify your asset. If you specify
                ' the assetName field, then both the asset name and the image being uploaded should be
                ' unique, and should not match another ACTIVE asset in this customer account.
                ' imageAsset.assetName = "Jupiter Trip " + ExampleUtilities.GetRandomString()
                imageAsset.imageData = MediaUtilities.GetAssetDataFromUrl("https://goo.gl/3b9Wfh",
                                                                          user.Config)

                ' Create the operation.
                Dim operation As New AssetOperation()
                operation.operator = [Operator].ADD
                operation.operand = imageAsset

                Try
                    ' Create the asset.
                    Dim result As AssetReturnValue = assetService.mutate(
                        New AssetOperation() _
                                                                            {operation})
                    ' [END upload_image_asset] MOE:strip_line

                    ' Display the results.
                    If Not (result Is Nothing) AndAlso Not (result.value Is Nothing) _
                       AndAlso result.value.Length > 0 Then
                        Dim newAsset As Asset = result.value(0)

                        Console.WriteLine("Image asset with id = '{0}' and name = {1} was created.",
                                          newAsset.assetId, newAsset.assetName)
                    Else
                        Console.WriteLine("No image asset was created.")
                    End If
                Catch e As Exception
                    Throw New System.ApplicationException("Failed to upload image assets.", e)
                End Try
            End Using
        End Sub
    End Class
End Namespace

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs"
  Inherits="Google.Api.Ads.AdWords.Examples.CSharp.OAuth.Default" %>
<!--
  Copyright 2011, Google Inc. All Rights Reserved.

  Licensed under the Apache License, Version 2.0 (the "License");
  you may not use this file except in compliance with the License.
  You may obtain a copy of the License at

      http://www.apache.org/licenses/LICENSE-2.0

  Unless required by applicable law or agreed to in writing, software
  distributed under the License is distributed on an "AS IS" BASIS,
  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
  See the License for the specific language governing permissions and
  limitations under the License.
-->

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <link rel="stylesheet" type="text/css" href="css/main.css" />
  <title>AdWords API - OAuth demo</title>
</head>
<body>
  <form id="form1" runat="server">
  <h1>AdWords API OAuth demo</h1>
  <p>
    This code example uses OAuth to retrieve the list all campaigns in your account.
  </p>
  <asp:Button ID="btnAuthorize" runat="server" Text="Authorize User"
     onclick="OnAuthorizeButtonClick" />
  <asp:Button ID="btnLogout" runat="server" Text="Logout"
     onclick="OnLogoutButtonClick" /><br /><br />
  <asp:Label ID="lblCustomerId" runat="server" Text="Enter Customer ID:" Height="18px"></asp:Label>
  <asp:TextBox ID="txtCustomerId" runat="server" Width="232px"></asp:TextBox>
  <asp:Button ID="btnGetCampaigns" runat="server" Text="Get campaigns"
     onclick="OnGetCampaignsButtonClick" />
  <asp:Button ID="btnDownloadReport" runat="server" Text="Download Criteria Report"
     onclick="OnDownloadReportButtonClick" />
  <br />
  <br />
  <asp:GridView ID="CampaignGrid" runat="server"
      Width="100%" BorderColor="#CCCCCC"
      onrowdatabound="CampaignGrid_RowDataBound">
    <HeaderStyle BackColor="#6699FF" ForeColor="White" />
    <AlternatingRowStyle BackColor="#CCFFFF" BorderColor="#CCCCCC" />
  </asp:GridView>
  </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sites.aspx.cs" Inherits="CRMWebClient.Sites" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 544px;
        }
        .auto-style2 {
            height: 132px;
        }
        .auto-style3 {
            height: 132px;
            width: 544px;
        }
        .auto-style4 {
            height: 132px;
            width: 189px;
        }
        .auto-style5 {
            width: 189px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%; height: 423px;">
            <tr>
                <td class="auto-style4">

                    &nbsp;</td>
                <td class="auto-style3">

                    Serial Number:
                    <asp:TextBox ID="serialNumberTextBox" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Zip:
                    <asp:TextBox ID="zipTextBox" runat="server"></asp:TextBox>
                    <br />
                    Store Name:
                    <asp:TextBox ID="storeNameTextBox" runat="server" Width="337px"></asp:TextBox>
                    <br />
                    City:
                    <asp:TextBox ID="cityTextBox" runat="server"></asp:TextBox>
                    <br />

                    Site ID:
                    <asp:TextBox ID="filterSiteID" runat="server"></asp:TextBox>
                    <asp:Button ID="searchButton" runat="server" OnClick="Button1_Click" Text="Search" />

                </td>
                <td class="auto-style2"></td>
            </tr>
            <tr>
                <td class="auto-style5">
                    &nbsp;</td>
                <td class="auto-style1">
                    <asp:GridView ID="sitesGridView" runat="server" AutoGenerateSelectButton="True" OnSelectedIndexChanged="sitesGridView_SelectedIndexChanged">
                    </asp:GridView>
                </td>
                <td>
                    <asp:Label ID="FullSNLabel" runat="server" Text="Full SN:"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="SNLabel" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="Label3" runat="server" Text="Version:"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="VersionLabel" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="Label5" runat="server" Text="Organization"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="organizationLabel" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="label7" runat="server" Text="Next Due"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="nextDueLabel" runat="server"></asp:Label>
                    <br />
                    Contact:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="contactLabel" runat="server"></asp:Label>
                    <br />
                    Address:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="addressLabel" runat="server"></asp:Label>
                    <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <br />
                    <asp:Label ID="Label8" runat="server" Text="Phone"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="phoneLabel" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="Label9" runat="server" Text="Email:"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="emailLabel" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="Label11" runat="server" Text="Notes:"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">
                    &nbsp;</td>
                <td class="auto-style1">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>

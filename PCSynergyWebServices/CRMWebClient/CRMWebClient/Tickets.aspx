<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tickets.aspx.cs" Inherits="CRMWebClient._Default" Async="true" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    </asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <table style="width:100%;" border="1">
        <tr>
            <td class="auto-style2">
                <asp:Button ID="Button1" runat="server" Text="Quick Ticket" /><br />
                <asp:Button ID="Button2" runat="server" Text="Refresh" />
            </td>
            <td class="auto-style3">
                <asp:Label ID="Label1" runat="server" Text="Store Name:"></asp:Label>
                <asp:TextBox ID="filterSiteName" runat="server" Width="186px"></asp:TextBox>
                    <br />

                <asp:Label ID="Label2" runat="server" Text="Serial #:"></asp:Label>
                <asp:TextBox ID="filterSerial" runat="server" Width="319px"></asp:TextBox>    
                    <br />

                <asp:Label ID="Label3" runat="server" Text="Phone:"></asp:Label>
                <asp:TextBox ID="filterPhone" runat="server"></asp:TextBox>


                <asp:Label ID="Label4" runat="server" Text="Zip: "></asp:Label>
                <asp:TextBox ID="filterZip" runat="server"></asp:TextBox>


            </td>
            <td class="auto-style4">
                Ticket #: <asp:TextBox ID="filterTicket" runat="server"></asp:TextBox>
                Status: 
                <asp:DropDownList ID="filterStatus" runat="server">
                    <asp:ListItem Value="1">Open</asp:ListItem>
                    <asp:ListItem Value="0">Closed (Last 7 days)</asp:ListItem>
                </asp:DropDownList> <br />


                Priority: <asp:DropDownList ID="filterPriority" runat="server">
                    <asp:ListItem Value="any">0 - Any Priority</asp:ListItem>
                    <asp:ListItem Value="1">1 - High</asp:ListItem>
                    <asp:ListItem Value="2">2 - Medium</asp:ListItem>
                    <asp:ListItem Value="3">3 - Low</asp:ListItem>
                </asp:DropDownList> <br />
                Medium: 
                <asp:DropDownList ID="filterMedium" runat="server">
                    <asp:ListItem Value="anyMed">Any Medium</asp:ListItem>
                    <asp:ListItem Value="na">N/A</asp:ListItem>
                    <asp:ListItem Value="liveCall">Live Call</asp:ListItem>
                    <asp:ListItem Value="voiceMail">Voice Mail</asp:ListItem>
                    <asp:ListItem Value="fax">Fax</asp:ListItem>
                    <asp:ListItem Value="email">E-mail</asp:ListItem>
                    <asp:ListItem Value="returnCall">Return Call</asp:ListItem>
                    <asp:ListItem Value="corporateRequest">Corporate Request</asp:ListItem>
                    <asp:ListItem Value="apptCall">Appointment Call</asp:ListItem>
                    <asp:ListItem Value="beta">Beta</asp:ListItem>
                    <asp:ListItem Value="discussionGroup">Discussion Group</asp:ListItem>
                    <asp:ListItem Value="internalRequest">Internal Request</asp:ListItem>
                    <asp:ListItem Value="mail">Mail</asp:ListItem>
                    <asp:ListItem Value="followUp">Follow Up</asp:ListItem>
                    <asp:ListItem Value="billing">Billing</asp:ListItem>
                    <asp:ListItem Value="training">Training</asp:ListItem>
                </asp:DropDownList>
            </td>

            <td>

                <asp:Button ID="searchButton" runat="server" Text="Find" OnClick="searchButton_Click" /> <br />
                <asp:Button ID="Button4" runat="server" Text="Clear" />

            </td>
            
        </tr>
    </table>
    <table style="width:100%;" border="1">
        <tr>
            <td class="auto-style6">Open tickets, etc.</td>
            
            <td class="auto-style5" id="listPane">
                <br />
                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                <br />
                <asp:GridView ID="ticketsGridView" runat="server" AutoGenerateSelectButton="True" OnSelectedIndexChanged="ticketsGridView_SelectedIndexChanged">
                </asp:GridView>
                <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="Hello world" />
            </td>

            <td class="auto-style1">
                <br />
                Site: <asp:Label ID="detailsSiteName" runat="server" Text=""></asp:Label>
                Ticket: <asp:Label ID="detailsTicketID" runat="server" Text=""></asp:Label>
                <br />

                Contact: <asp:Label ID="detailsContact" runat="server" Text=""></asp:Label>
                Queue #: <asp:Label ID="detailsQueue" runat="server" Text=""></asp:Label>
                <br />

                Address: <asp:Label ID="detailsAddress" runat="server" Text=""></asp:Label>
                <br />

                Phone: <asp:Label ID="detailsPhone" runat="server" Text=""></asp:Label>
                <br />

                Email: <asp:Label ID="detailsEmail" runat="server" Text=""></asp:Label>
                <br />


                Assigned to: <asp:Label ID="detailsAssignedTo" runat="server" Text=""></asp:Label>

                Priority: <asp:Label ID="detailsPriority" runat="server" Text=""></asp:Label>
                <br />

                Entered by: <asp:Label ID="detailsReportedBy" runat="server" Text=""></asp:Label>

                Reported on: <asp:Label ID="detailsReportedOn" runat="server" Text=""></asp:Label>
                <br />

                Contact media: <asp:Label ID="detailsMedia" runat="server" Text=""></asp:Label>

                Issue Type: <asp:Label ID="detailsIssueType" runat="server" Text=""></asp:Label>
                <br />

                Appointment: <asp:Label ID="detailsAppointment" runat="server" Text=""></asp:Label>
                <br />

                Issue: <asp:Label ID="detailsIssue" runat="server" Text=""></asp:Label>

            </td>
        </tr>
        <tr>
            <td class="auto-style6">&nbsp;</td>
            
            <td class="auto-style5" id="listPane">
                &nbsp;</td>

            <td class="auto-style1">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }
        .auto-style2 {
            width: 64px;
        }
        .auto-style3 {
            width: 410px;
        }
        .auto-style4 {
            width: 380px;
        }
        .auto-style5 {
            height: 23px;
            width: 609px;
        }
        .auto-style6 {
            height: 23px;
            width: 103px;
        }
    </style>
</asp:Content>


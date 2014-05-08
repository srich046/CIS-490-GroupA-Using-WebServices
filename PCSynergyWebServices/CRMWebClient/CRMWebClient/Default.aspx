<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CRMWebClient._Default" Async="true" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    </asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <table style="width:100%;" border="1">
        <tr>
            <td class="auto-style2">
                <asp:Button ID="Button1" runat="server" Text="Quick Ticket" BackColor="#0066FF" 
                    Font-Bold="True" ForeColor="White" /><br />
                <asp:Button ID="Button2" runat="server" Text="Refresh" BackColor="#0066FF" 
                    Font-Bold="True" ForeColor="White" />
            </td>
            <td class="auto-style1">
                <table>
                    <tr>
                        <td><asp:Label ID="Label1" runat="server" Text="Store Name:" Font-Bold="True" 
                    ForeColor="#666666"></asp:Label></td>
                        <td><asp:TextBox ID="filterSiteName" runat="server" Width="186px"></asp:TextBox></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="Label2" runat="server" Text="Serial #:" Font-Bold="True" 
                    ForeColor="#666666"></asp:Label></td>
                        <td><asp:TextBox ID="filterSerial" runat="server" Width="231px"></asp:TextBox></td>    
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="Label3" runat="server" Text="Phone:" Font-Bold="True" 
                    ForeColor="#666666"></asp:Label></td>
                        <td><asp:TextBox ID="filterPhone" runat="server"></asp:TextBox></td>


                        <td><asp:Label ID="Label4" runat="server" Text="Zip: " Font-Bold="True" 
                    ForeColor="#666666"></asp:Label></td>
                        <td><asp:TextBox ID="filterZip" runat="server"></asp:TextBox></td>
                    </tr>
                </table>
            </td>
            <td class="style3">
                <span class="style2"><strong>Ticket #: </strong></span> <asp:TextBox ID="filterTicket" runat="server"></asp:TextBox>
                <span class="style2"><strong>Status:</strong></span> 
                <asp:DropDownList ID="filterStatus" runat="server">
                    <asp:ListItem Value="1">Open</asp:ListItem>
                    <asp:ListItem Value="0">Closed (Last 7 days)</asp:ListItem>
                </asp:DropDownList> <br />


                <span class="style2"><strong>Priority: </strong></span> <asp:DropDownList ID="filterPriority" runat="server">
                    <asp:ListItem Value="any">0 - Any Priority</asp:ListItem>
                    <asp:ListItem Value="1">1 - High</asp:ListItem>
                    <asp:ListItem Value="2">2 - Medium</asp:ListItem>
                    <asp:ListItem Value="3">3 - Low</asp:ListItem>
                </asp:DropDownList> <br />
                <span class="style2"><strong>Medium:</strong></span> 
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

                <asp:Button ID="searchButton" runat="server" Text="Find" 
                    OnClick="searchButton_Click" BackColor="#0066FF" Font-Bold="True" 
                    ForeColor="White" /> <br />
                <asp:Button ID="Button4" runat="server" Text="Clear" BackColor="#0066FF" 
                    Font-Bold="True" ForeColor="White" />

            </td>
            
        </tr>
    </table>
    <table style="width:100%;" border="1">
        <tr>
            <td class="auto-style6"><span class="style2">Open tickets, etc</span>.</td>
            
            <td class="style4" id="listPane">
                <br />
                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                <br />
                <asp:GridView ID="ticketsGridView" runat="server" 
                    AutoGenerateSelectButton="True" 
                    OnSelectedIndexChanged="ticketsGridView_SelectedIndexChanged" 
                    ForeColor="#666666">
                </asp:GridView>
            </td>

            <td class="auto-style1">
                <br />
                <span class="style2"><strong>Site:</strong></span> 
                <asp:Label ID="detailsSiteName" runat="server" Text="" CssClass="style2"></asp:Label>
                <span class="style2">Ticket: </span> <asp:Label ID="detailsTicketID" 
                    runat="server" Text="" CssClass="style2"></asp:Label>
                <br class="style2" />

                <span class="style2"><strong>Contact: </strong></span> 
                <asp:Label ID="detailsContact" runat="server" Text="" CssClass="style2"></asp:Label>
                <span class="style2">Queue #: </span> <asp:Label ID="detailsQueue" 
                    runat="server" Text="" CssClass="style2"></asp:Label>
                <br class="style2" />

                <span class="style2"><strong>Address:</strong></span> 
                <asp:Label ID="detailsAddress" runat="server" Text="" CssClass="style2"></asp:Label>
                <br class="style2" />

                <span class="style2"><strong>Phone: </strong></span> 
                <asp:Label ID="detailsPhone" runat="server" Text="" CssClass="style2"></asp:Label>
                <br class="style2" />

                <span class="style2"><strong>Email:</strong></span> 
                <asp:Label ID="detailsEmail" runat="server" Text="" CssClass="style2"></asp:Label>
                <br class="style2" />


                <span class="style2"><strong>Assigned to: </strong></span> 
                <asp:Label ID="detailsAssignedTo" runat="server" Text="" CssClass="style2"></asp:Label>

                <span class="style2"><strong>Priority:</strong></span> 
                <asp:Label ID="detailsPriority" runat="server" Text="" CssClass="style2"></asp:Label>
                <br class="style2" />

                <span class="style2"><strong>Entered by:</strong></span> 
                <asp:Label ID="detailsReportedBy" runat="server" Text="" CssClass="style2"></asp:Label>

                <span class="style2"><strong>Reported on:</strong></span> 
                <asp:Label ID="detailsReportedOn" runat="server" Text="" CssClass="style2"></asp:Label>
                <br class="style2" />

                <span class="style2"><strong>Contact media:</strong></span> 
                <asp:Label ID="detailsMedia" runat="server" Text="" CssClass="style2"></asp:Label>

                <span class="style2"><strong>Issue Type: </strong></span> 
                <asp:Label ID="detailsIssueType" runat="server" Text="" CssClass="style2"></asp:Label>
                <br class="style2" />

                <span class="style2"><strong>Appointment:</strong></span> 
                <asp:Label ID="detailsAppointment" runat="server" Text="" CssClass="style2"></asp:Label>
                <br class="style2" />

                <span class="style2"><strong>Issue: </strong></span> 
                <asp:Label ID="detailsIssue" runat="server" Text="" CssClass="style2"></asp:Label>

            </td>
        </tr>
        <tr>
            <td class="auto-style6">&nbsp;</td>
            
            <td class="style4" id="listPane">
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
        .style2
        {
            color: #666666;
        }
        .style3
        {
            width: 433px;
        }
        .style4
        {
            height: 23px;
            width: 610px;
            color: #666666;
        }
    </style>
</asp:Content>


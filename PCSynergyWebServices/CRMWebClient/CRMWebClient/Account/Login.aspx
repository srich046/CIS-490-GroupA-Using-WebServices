<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CRMWebClient.Account.Login" %>
<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
    </hgroup>
    <section id="loginForm">
        <h2 class="style2">Use a local account to log in.</h2>
        <asp:Login runat="server" ViewStateMode="Disabled" RenderOuterTable="false" 
        style="color: #666666">
            <LayoutTemplate>
                <p class="validation-summary-errors">
                    <asp:Literal runat="server" ID="FailureText" />
                </p>
                <fieldset>
                    <legend class="validation-summary-errors">Log in Form</legend>
                    <ol>
                        <li>
                            <asp:Label runat="server" AssociatedControlID="UserName" ForeColor="#666666">User name</asp:Label>
                            <asp:TextBox runat="server" ID="UserName" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName" 
                                CssClass="field-validation-error" 
                                ErrorMessage="The user name field is required." ForeColor="#666666" />
                        </li>
                        <li>
                            <asp:Label runat="server" AssociatedControlID="Password" ForeColor="#666666">Password</asp:Label>
                            <asp:TextBox runat="server" ID="Password" TextMode="Password" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" 
                                CssClass="field-validation-error" 
                                ErrorMessage="The password field is required." ForeColor="#666666" />
                        </li>
                        <li>
                            <asp:CheckBox runat="server" ID="RememberMe" ForeColor="#666666" />
                            <asp:Label runat="server" AssociatedControlID="RememberMe" CssClass="checkbox" 
                                ForeColor="#666666">Remember me?</asp:Label>
                        </li>
                    </ol>
                    <asp:Button runat="server" CommandName="Login" Text="Log in" 
                        BackColor="#0066FF" Font-Bold="True" ForeColor="White" />
                </fieldset>
            </LayoutTemplate>
        </asp:Login>
        <p>
            <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled" 
                CssClass="validation-summary-errors">Register</asp:HyperLink>
            <span class="validation-summary-errors">if you don't have an account.
        </span>
        </p>
    </section>

    <section id="socialLoginForm">
        <h2 class="validation-summary-errors">Use another service to log in.</h2>
        <uc:OpenAuthProviders runat="server" ID="OpenAuthLogin" />
    </section>
</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .style2
        {
            color: #666666;
        }
        .validation-summary-errors
        {
            color: #666666;
        }
    </style>
</asp:Content>


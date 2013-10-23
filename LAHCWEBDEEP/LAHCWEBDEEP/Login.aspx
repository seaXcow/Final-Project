<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Login.aspx.vb" Inherits="LAHCWEBDEEP.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width:100%;">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Username"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tbUsername" runat="server"></asp:TextBox>
                </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="Please Enter your correct Username" 
                    ControlToValidate="tbUsername"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
                </td>
            <td>
                <asp:TextBox ID="tbPassword" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ErrorMessage="Please Enter your correct Username" 
                    ControlToValidate="tbPassword"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
        <td colspan="2" align="center">
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
        </td></tr>
        <tr>
            <td colspan="3" align="center">
                <asp:Button ID="btnLogin" runat="server" Text="Login" />
            </td>

        </tr>
    </table>
</asp:Content>

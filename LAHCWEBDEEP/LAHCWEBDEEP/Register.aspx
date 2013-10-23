<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Register.aspx.vb" Inherits="LAHCWEBDEEP.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 100%;">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="FirstName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tbFirstName" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="Please enter your First Name" ControlToValidate="tbFirstName"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                 <asp:Label ID="Label2" runat="server" Text="LastName"></asp:Label>
            </td>
            <td>
               <asp:TextBox ID="tbLastName" runat="server"></asp:TextBox>
            </td>
            <td>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ErrorMessage="Please enter your Last Name" ControlToValidate="tbLastName"></asp:RequiredFieldValidator>

            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Username"></asp:Label>
            </td>
            <td>
               <asp:TextBox ID="tbUsername" runat="server"></asp:TextBox>
            </td>
            <td>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ErrorMessage="Please Enter a Username" ControlToValidate="tbUsername"></asp:RequiredFieldValidator>

            </td>
        </tr>
        <tr>
        <td>
        <asp:Label ID="Label4" runat="server" Text="Password"></asp:Label>
        </td>
        <td>
        <asp:TextBox ID="tbPassword" runat="server" TextMode="Password"></asp:TextBox>
        </td>
        <td>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                  ErrorMessage="Please entere a password" ControlToValidate="tbPassword"></asp:RequiredFieldValidator>
        
        </td>
        </tr>
           <tr>
        <td>
        <asp:Label ID="Label5" runat="server" Text="E-mail"></asp:Label>
        </td>
        <td>
        <asp:TextBox ID="tbemail" runat="server"></asp:TextBox>
        </td>
        <td>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                  ErrorMessage="Please enter your email." ControlToValidate="tbemail"></asp:RequiredFieldValidator>
        
        </td>
        </tr>
        <tr>
        <td colspan="3" align="center">
        <asp:Button ID="btnRegister" runat="server" Text="Register!" />
        </td>
        </tr>
    </table>
    
</asp:Content>

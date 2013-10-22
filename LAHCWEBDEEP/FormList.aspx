<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FormList.aspx.vb" Inherits="LAHCWEBDEEP.FormList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="StyleSheet1.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-color:Black;">
    <form id="form1" runat="server">
    <asp:Repeater ID="rpthread" runat="server">
    <HeaderTemplate>
    <table cellspacing="0" align="center" width="80%">
    </HeaderTemplate>
        <ItemTemplate>  
        <tr>
        <td width="50%">
        <asp:HyperLink ID="HyperLink1" NavigateUrl="Login.aspx" runat="server" Text='<%#Eval("Summary")%>' /></td>
         <td width="15%"><%#Eval("Username")%> </td>
         <td width="15%"><%#Eval("postedtime")%> </td>
        </tr>
        </ItemTemplate>
        <FooterTemplate>
        </table> 
        </FooterTemplate>
    </asp:Repeater>
    </form>
</body>
</html>

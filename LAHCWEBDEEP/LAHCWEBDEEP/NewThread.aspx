<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="NewThread.aspx.vb" Inherits="LAHCWEBDEEP.NewThread" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="height: 850px">
    <form id="form1" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Summary : "></asp:Label>


    <asp:TextBox ID="tbSummary" runat="server" Width="716px"></asp:TextBox>




        <p>
        <asp:Label ID="Label2" runat="server" Text="Body  "></asp:Label>
    </p>




    <p>




    <asp:TextBox ID="tbBody" runat="server" Height="644px" TextMode="MultiLine" 
        Width="767px" MaxLength="5" style="margin-left: 56px"></asp:TextBox>




    </p>
    <asp:Button ID="btnSubmit" runat="server" Text="Post" />




    </form>
</body>
</html>

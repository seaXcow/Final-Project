Imports LAHCWEBDEEP.BusinessObjects
Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim m As New Member

        m = m.Login(tbUsername.Text, tbPassword.Text)

        If m.IsNew = False Then
            Session.Add("Member", m)
            Response.Redirect("NewThread.aspx")
        Else
            lblError.Text = "Login unsuccesfull"
        End If
    End Sub


End Class
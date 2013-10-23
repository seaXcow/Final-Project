Imports LAHCWEBDEEP.BusinessObjects

Public Class Register
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        Dim M As New Member
        M.FirstName = tbFirstName.Text
        M.LastName = tbLastName.Text
        M.username = tbUsername.Text
        M.Password = tbPassword.Text
        M.Email = tbemail.Text

        If M.IsSavable Then
            M = M.Save
        End If

        Response.Redirect("Login.aspx")
    End Sub
End Class
Imports LAHCWEBDEEP.BusinessObjects
Public Class NewThread
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click

        Dim M As Member = Nothing

        If Session("Member") IsNot Nothing Then
            M = CType(Session("Member"), Member)
        Else
            Response.Redirect("Login.aspx")
        End If

        Dim T As New Thread
        T.Summary = tbSummary.Text
        T.body = tbBody.Text
        T.memberID = M.ID

        If T.IsSavable Then
            T = T.Save
            Response.Redirect("FormList.aspx")
        End If
    End Sub


End Class
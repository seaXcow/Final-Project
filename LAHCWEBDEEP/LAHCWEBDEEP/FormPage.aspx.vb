Imports LAHCWEBDEEP.BusinessObjects
Public Class FormPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ThreadID As New Guid(Request.QueryString("ThreadID"))


        Response.Write(ThreadID)

    End Sub

End Class
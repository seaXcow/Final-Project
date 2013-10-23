Imports LAHCWEBDEEP.BusinessObjects
Public Class FormList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim TL As New ThreadList

        TL = TL.Search

        Me.rpthread.DataSource = TL.List

        Me.rpthread.DataBind()
    End Sub


    Public Sub rpthread_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rpthread.ItemDataBound
        If e.Item.ItemType = ListItemType.Item And e.Item.ItemType = ListItemType.AlternatingItem Then

            If e.Item.ItemType = ListItemType.Item And e.Item.ItemType = ListItemType.AlternatingItem Then




            End If


        End If
    End Sub



End Class
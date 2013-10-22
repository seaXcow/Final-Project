Imports System.ComponentModel
Imports LAHCWEBDEEP.DatabaseHelper
Imports LAHCWEBDEEP.SQLHelper
Namespace BusinessObjects
    Public Class ThreadList

#Region " Private Members "
        Private WithEvents _List As New BindingList(Of Thread)
        Private _Criteria As New Criteria


#End Region

#Region " Public Properties "
        Public ReadOnly Property List As BindingList(Of Thread)
            Get
                Return _List
            End Get
        End Property


        Public WriteOnly Property summary As String
            Set(value As String)
                If value.Trim <> String.Empty Then
                    _Criteria.Fields.Add("Summary")
                    _Criteria.Values.Add(value)
                    _Criteria.Types.Add(DataTypeHelper.Types.DataType.String_Contains)
                End If
            End Set
        End Property

        Public WriteOnly Property body As String
            Set(value As String)
                If value.Trim <> String.Empty Then
                    _Criteria.Fields.Add("body")
                    _Criteria.Values.Add(value)
                    _Criteria.Types.Add(DataTypeHelper.Types.DataType.String_Contains)
                End If
            End Set
        End Property



#End Region

#Region " Private Methods "

#End Region

#Region " Public Methods "
        Public Function Save() As ThreadList
            Dim result As Boolean = True
            For Each t As Thread In _List
                If t.IsSavable = True Then
                    t = t.Save
                    If t.IsNew = True Then
                        result = False
                        Exit For

                    End If
                End If
            Next
            Return Me
        End Function

        Public Function IsSavable() As Boolean
            Dim result As Boolean = True

            For Each t As Thread In _List
                If t.IsSavable = True Then
                    result = True
                    Exit For
                End If
            Next

            Return result
        End Function

        Public Function Search() As ThreadList
            Dim database As New Database()
            Dim ds As DataSet

            database.ConnectionName = System.Web.Configuration.WebConfigurationManager.AppSettings("ConnectionName")
            database.Command.CommandType = CommandType.Text
            database.Command.CommandText = SQLHelper.Builder.Build(_Criteria)

            ds = database.ExecuteQuery

            For Each dr As DataRow In ds.Tables(0).Rows
                Dim t As New Thread
                t.Initialize(dr)
                t.InitializeBusinessData(dr)
                t.IsNew = False
                t.IsDirty = False
                AddHandler t.evtIsSavable, AddressOf ThreadList_evtIsSavable
                _List.Add(t)
            Next
            Return Me

        End Function

#End Region

#Region " Public Events "

        Public Delegate Sub eIssavable(ByVal savable As Boolean)
        Public Event evtIsSavable As eIssavable
#End Region

#Region " Public Event Handlers "
        Private Sub ThreadList_evtIsSavable(savable As Boolean)
            RaiseEvent evtIsSavable(savable)
        End Sub

        Private Sub _List_AddingNew(Sender As Object, e As System.ComponentModel.AddingNewEventArgs) Handles _List.AddingNew
            e.NewObject = New Thread
            AddHandler CType(e.NewObject, Thread).evtIsSavable, AddressOf ThreadList_evtIsSavable

        End Sub
#End Region

#Region " Construction "
        Public Sub New()
            _Criteria = New Criteria
            _Criteria.TableName = "thread"
        End Sub
#End Region



    End Class
End Namespace
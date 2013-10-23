Imports LAHCWEBDEEP.SQLHelper
Imports LAHCWEBDEEP.DatabaseHelper
Imports System.ComponentModel
Namespace BusinessObjects
    Public Class MemberList


#Region " Private Members "
        Private WithEvents _List As New BindingList(Of Member)
        Private _Criteria As New Criteria

#End Region

#Region " Public Properties "
        Public ReadOnly Property List As BindingList(Of Member)
            Get
                Return _List
            End Get
        End Property

        Public WriteOnly Property firstname As String
            Set(value As String)
                If value.Trim <> String.Empty Then
                    _Criteria.Fields.Add("firstname")
                    _Criteria.Values.Add(value)
                    _Criteria.Types.Add(DataTypeHelper.Types.DataType.String_Contains)
                End If
            End Set
        End Property

        Public WriteOnly Property Laststname As String
            Set(value As String)
                If value.Trim <> String.Empty Then
                    _Criteria.Fields.Add("Lastname")
                    _Criteria.Values.Add(value)
                    _Criteria.Types.Add(DataTypeHelper.Types.DataType.String_Contains)
                End If
            End Set
        End Property


        Public WriteOnly Property Username As String
            Set(value As String)
                If value.Trim <> String.Empty Then
                    _Criteria.Fields.Add("username")
                    _Criteria.Values.Add(value)
                    _Criteria.Types.Add(DataTypeHelper.Types.DataType.String_Contains)
                End If
            End Set
        End Property

        Public WriteOnly Property Password As String
            Set(value As String)
                If value.Trim <> String.Empty Then
                    _Criteria.Fields.Add("Password")
                    _Criteria.Fields.Add(value)
                    _Criteria.Types.Add(DataTypeHelper.Types.DataType.String_Contains)

                End If
            End Set
        End Property


        Public WriteOnly Property Email As String
            Set(value As String)
                If value.Trim <> String.Empty Then
                    _Criteria.Fields.Add("Email")
                    _Criteria.Values.Add(value)
                    _Criteria.Types.Add(DataTypeHelper.Types.DataType.String_Contains)
                End If
            End Set
        End Property

#End Region

#Region " Private Methods "

#End Region

#Region " Public Methods "
        Public Function Save() As MemberList
            Dim result As Boolean = True
            For Each m As Member In _List
                If m.IsSavable = True Then
                    m = m.Save
                    If m.IsNew = True Then
                        result = False
                        Exit For
                    End If
                End If
            Next

            Return Me
        End Function

        Public Function IsSavable() As Boolean
            Dim result As Boolean = True

            For Each m As Member In _List
                If m.IsSavable = True Then
                    result = True
                    Exit For
                End If
            Next

            Return result
        End Function

        Public Function Search() As MemberList
            Dim database As New Database()
            Dim ds As DataSet

            database.ConnectionName = System.Web.Configuration.WebConfigurationManager.AppSettings("LouisianaHC")
            database.Command.CommandType = CommandType.Text
            database.Command.CommandText = SQLHelper.Builder.Build(_Criteria)

            ds = database.ExecuteQuery

            For Each dr As DataRow In ds.Tables(0).Rows
                Dim m As New Member
                m.Initialize(dr)
                m.InitializeBusinessData(dr)
                m.IsNew = False
                m.IsDirty = False
                AddHandler m.evtIsSavable, AddressOf MemberList_evtIsSavable
                _List.Add(m)
            Next
            Return Me
        End Function
#End Region

#Region " Public Events "
        Public Delegate Sub eIssavable(ByVal savable As Boolean)
        Public Event evtIsSavable As eIssavable
#End Region

#Region " Public Event Handlers "
        Private Sub MemberList_evtIsSavable(savable As Boolean)
            RaiseEvent evtIsSavable(savable)
        End Sub

        Private Sub _List_AddingNew(sender As Object, e As System.ComponentModel.AddingNewEventArgs) Handles _List.AddingNew
            e.NewObject = New Member
            AddHandler CType(e.NewObject, Member).evtIsSavable, AddressOf MemberList_evtIsSavable
        End Sub
#End Region

#Region " Construction "
        Public Sub New()
            _Criteria = New Criteria
            _Criteria.TableName = "members"
        End Sub
#End Region



    End Class
End Namespace
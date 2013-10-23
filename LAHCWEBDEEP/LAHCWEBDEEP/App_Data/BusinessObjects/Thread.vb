Imports System.Data.SqlClient
Imports LAHCWEBDEEP.DatabaseHelper
Imports System.ComponentModel

Namespace BusinessObjects
    Public Class Thread
        Inherits HeaderData

#Region " Private Members "
        Private _Summary As String = String.Empty
        Private _body As String = String.Empty
        Private _MemberID As Guid = Guid.Empty
        Private _postedtime As DateTime = DateTime.MaxValue


#End Region

#Region " Public Properties "

        Public Property Summary As String
            Get
                Return _Summary

            End Get
            Set(ByVal value As String)
                If value <> _Summary Then
                    _Summary = value
                    MyBase.IsDirty = True
                    'Raise an Event here to notify if the object is savable
                    RaiseEvent evtIsSavable(IsSavable)
                End If
            End Set
        End Property


        Public Property body As String
            Get
                Return _body

            End Get
            Set(ByVal value As String)
                If value <> _body Then
                    _body = value
                    MyBase.IsDirty = True
                    'Raise an Event here to notify if the object is savable
                    RaiseEvent evtIsSavable(IsSavable)
                End If
            End Set
        End Property

        Public Property memberID As Guid
            Get
                Return memberID

            End Get
            Set(ByVal value As Guid)
                If value <> _MemberID Then
                    _MemberID = value
                    MyBase.IsDirty = True
                    'Raise an Event here to notify if the object is savable
                    RaiseEvent evtIsSavable(IsSavable)
                End If
            End Set
        End Property

        Public ReadOnly Property Username As String
            Get
                Dim m As New Member

                m = m.GetByID(_MemberID)

                Return m.username
            End Get
        End Property

        Public Property postedtime As DateTime
            Get
                Return _postedtime
            End Get
            Set(value As DateTime)
                _postedtime = value
            End Set
        End Property

#End Region

#Region " Private Methods "

        Private Function Insert(database As DatabaseHelper.Database) As Boolean
            Try
                'setting up the command object
                database.Command.Parameters.Clear()
                database.Command.CommandType = CommandType.StoredProcedure
                database.Command.CommandText = "thread_Insert"
                'Add the parameters for the header data
                MyBase.Initialize(database, Guid.Empty)
                'Add the parameter
                database.Command.Parameters.Add("@summary", SqlDbType.VarChar).Value = _Summary
                database.Command.Parameters.Add("@body", SqlDbType.VarChar).Value = _body
                database.Command.Parameters.Add("@memberID", SqlDbType.UniqueIdentifier).Value = _MemberID
                database.ExecuteNonQueryWithTransaction()
                '
                'Change execute non query with transaction
                '
                'retrieve the header data values from the command object
                MyBase.Initialize(database.Command)
                Return True

            Catch ex As Exception
                Return False
            End Try
        End Function


        Private Function Update(database As DatabaseHelper.Database) As Boolean
            Try
                'setting up the command object
                database.Command.Parameters.Clear()
                database.Command.CommandType = CommandType.StoredProcedure
                database.Command.CommandText = "thread_Update"
                'Add the parameters for the header data
                MyBase.Initialize(database, Guid.Empty)
                'Add the parameter
                database.Command.Parameters.Add("@sumamary", SqlDbType.VarChar).Value = _Summary
                database.Command.Parameters.Add("@body", SqlDbType.VarChar).Value = _body
                database.Command.Parameters.Add("@memberID", SqlDbType.UniqueIdentifier).Value = _memberID
                'Execute The non query
                database.ExecuteNonQueryWithTransaction()
                '
                'Change execute non query with transaction
                '
                'retrieve the header data values from the command object
                MyBase.Initialize(database.Command)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Private Function Delete(database As DatabaseHelper.Database) As Boolean
            Try
                'setting up the command object
                database.Command.Parameters.Clear()
                database.Command.CommandType = CommandType.StoredProcedure
                database.Command.CommandText = "thread_Delete"
                'Add the parameters for the header data
                MyBase.Initialize(database, Guid.Empty)
                'Execute The non query
                database.ExecuteNonQueryWithTransaction()
                '
                'Change execute non query with transaction
                '
                'retrieve the header data values from the command object
                MyBase.Initialize(database.Command)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Private Function IsValid() As Boolean
            'These are the business rules
            'assume true unless a rule is broken
            Dim result As Boolean = True
            If _body = String.Empty Then
                result = False
            End If

            If _Summary = String.Empty Then
                result = False
            End If


            If _Summary.Length > 100 Then
                result = False
            End If


            Return result
        End Function

#End Region

#Region " Public Methods "


        Public Function Save() As Thread
            Dim db As New Database()
            db.BeginTransaction(System.Web.Configuration.WebConfigurationManager.AppSettings("ConnectionName"))
            Dim result As Boolean = True

            If MyBase.IsNew = True AndAlso MyBase.IsDirty() = True AndAlso IsValid() = True Then
                result = Insert(db)
            ElseIf MyBase.Deleted = True AndAlso MyBase.IsDirty = True Then
                result = Delete(db)
            ElseIf MyBase.IsNew = False AndAlso MyBase.IsDirty = True AndAlso IsValid() = True Then
                result = Update(db)
            End If

            If result = True Then
                MyBase.IsDirty = False
                MyBase.IsNew = False
                MyBase.Status = MyBase.Success
            Else
                MyBase.Status = MyBase.Fail
            End If
            '
            'Handle the transaction here
            '
            If result = True Then
                db.EndTransaction()
            Else
                db.Rollback()
            End If
            Return Me

        End Function

        Public Function IsSavable() As Boolean

            If MyBase.IsDirty = True AndAlso IsValid() = True Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function GetByID(ID As Guid) As Thread
            Dim db As New Database(System.Web.Configuration.WebConfigurationManager.AppSettings("ConnectionName"))
            Dim ds As DataSet = Nothing
            db.Command.CommandType = CommandType.StoredProcedure
            db.Command.CommandText = "Thread_GetByID"
            db.Command.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = ID
            ds = db.ExecuteQuery

            If ds.Tables(0).Rows.Count = 1 Then
                'Get the first and only row in the dataset
                Dim dr As DataRow = ds.Tables(0).Rows(0)
                MyBase.Initialize(dr)
                InitializeBusinessData(dr)
                MyBase.IsNew = False
                MyBase.IsDirty = False
                Return Me

            ElseIf ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception(String.Format("Member {0} was not found", ID))
            Else
                Throw New Exception(String.Format("Member {0} found multiple records", ID))

            End If


        End Function


        Public Sub InitializeBusinessData(dr As DataRow)
            _body = dr("body")
            _Summary = dr("summary")
            _MemberID = dr("memberID")
            _postedtime = dr("posted_time")


        End Sub
#End Region

#Region " Public Events "

        Public Delegate Sub IsSavableArgs(savable As Boolean)
        Public Event evtIsSavable As IsSavableArgs

#End Region

#Region " Public Event Handlers "

#End Region

#Region " Construction "

#End Region

    End Class

End Namespace
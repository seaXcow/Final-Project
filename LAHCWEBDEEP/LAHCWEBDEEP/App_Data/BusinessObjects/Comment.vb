
Imports System.Data.SqlClient
Imports LAHCWEBDEEP.DatabaseHelper
Imports System.ComponentModel
Namespace BusinessObjects
    Public Class Comment
        Inherits HeaderData

#Region " Private Members "
        Private _body As String = String.Empty
        Private _threadID As Guid = Guid.Empty


#End Region

#Region " Public Properties "

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

        Public Property threadID As Guid
            Get
                Return threadID

            End Get
            Set(ByVal value As Guid)
                If value <> _threadID Then
                    _threadID = value
                    MyBase.IsDirty = True
                    'Raise an Event here to notify if the object is savable
                    RaiseEvent evtIsSavable(IsSavable)
                End If
            End Set
        End Property

#End Region

#Region " Private Methods "

        Private Function Insert(database As DatabaseHelper.Database) As Boolean
            Try
                'setting up the command object
                database.Command.Parameters.Clear()
                database.Command.CommandType = CommandType.StoredProcedure
                database.Command.CommandText = "comment_Insert"
                'Add the parameters for the header data
                MyBase.Initialize(database, Guid.Empty)
                'Add the parameter

                database.Command.Parameters.Add("@body", SqlDbType.VarChar).Value = _body
                database.Command.Parameters.Add("@threadID", SqlDbType.UniqueIdentifier).Value = _threadID
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
                database.Command.CommandText = "comment_Update"
                'Add the parameters for the header data
                MyBase.Initialize(database, Guid.Empty)
                'Add the parameter

                database.Command.Parameters.Add("@body", SqlDbType.VarChar).Value = _body
                database.Command.Parameters.Add("@threadID", SqlDbType.UniqueIdentifier).Value = _threadID
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
                database.Command.CommandText = "comment_Delete"
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

            Return result
        End Function

#End Region

#Region " Public Methods "


        Public Function Save() As Comment
            Dim db As New Database()
            db.BeginTransaction(System.Web.Configuration.WebConfigurationManager.AppSettings("LouisianaHC"))
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

        Public Function GetByID(ID As Guid) As Comment
            Dim db As New Database(System.Web.Configuration.WebConfigurationManager.AppSettings("LouisianaHC"))
            Dim ds As DataSet = Nothing
            db.Command.CommandType = CommandType.StoredProcedure
            db.Command.CommandText = "comment_GetByID"
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
            _threadID = dr("threadID")



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
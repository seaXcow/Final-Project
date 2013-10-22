Imports System.Data.SqlClient
Imports LAHCWEBDEEP.DatabaseHelper
Imports System.ComponentModel

Namespace BusinessObjects
    Public Class Member
        Inherits HeaderData

#Region " Private Members "
        Private _FirstName As String = String.Empty
        Private _LastName As String = String.Empty
        Private _Username As String = String.Empty
        Private _password As String = String.Empty
        Private _email As String = String.Empty

#End Region

#Region " Public Properties "

        Public Property FirstName As String
            Get
                Return _FirstName

            End Get
            Set(ByVal value As String)
                If value <> _FirstName Then
                    _FirstName = value
                    MyBase.IsDirty = True
                    'Raise an Event here to notify if the object is savable
                    RaiseEvent evtIsSavable(IsSavable)
                End If
            End Set
        End Property


        Public Property LastName As String
            Get
                Return _LastName

            End Get
            Set(ByVal value As String)
                If value <> _LastName Then
                    _LastName = value
                    MyBase.IsDirty = True
                    'Raise an Event here to notify if the object is savable
                    RaiseEvent evtIsSavable(IsSavable)
                End If
            End Set
        End Property

        Public Property username As String
            Get
                Return _Username

            End Get
            Set(ByVal value As String)
                If value <> _Username Then
                    _Username = value
                    MyBase.IsDirty = True
                    'Raise an Event here to notify if the object is savable
                    RaiseEvent evtIsSavable(IsSavable)
                End If
            End Set
        End Property

        Public Property Password As String
            Get
                Return _password

            End Get
            Set(ByVal value As String)
                If value <> _password Then
                    _password = value
                    MyBase.IsDirty = True
                    'Raise an Event here to notify if the object is savable
                    RaiseEvent evtIsSavable(IsSavable)
                End If
            End Set
        End Property


        Public Property Email As String
            Get
                Return _email

            End Get
            Set(ByVal value As String)
                If value <> _email Then
                    _email = value
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
                database.Command.CommandText = "members_Insert"
                'Add the parameters for the header data
                MyBase.Initialize(database, Guid.Empty)
                'Add the parameter
                database.Command.Parameters.Add("@First_Name", SqlDbType.VarChar).Value = _FirstName
                database.Command.Parameters.Add("@Last_Name", SqlDbType.VarChar).Value = _LastName
                database.Command.Parameters.Add("@username", SqlDbType.VarChar).Value = _Username
                database.Command.Parameters.Add("@password", SqlDbType.VarChar).Value = _password
                database.Command.Parameters.Add("@email", SqlDbType.VarChar).Value = _email
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


        Private Function Update(database As DatabaseHelper.Database) As Boolean
            Try
                'setting up the command object
                database.Command.Parameters.Clear()
                database.Command.CommandType = CommandType.StoredProcedure
                database.Command.CommandText = "members_Update"
                'Add the parameters for the header data
                MyBase.Initialize(database, Guid.Empty)
                'Add the parameter
                database.Command.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = _FirstName
                database.Command.Parameters.Add("@LasttName", SqlDbType.VarChar).Value = _LastName
                database.Command.Parameters.Add("@username", SqlDbType.VarChar).Value = _Username
                database.Command.Parameters.Add("@password", SqlDbType.VarChar).Value = _password
                database.Command.Parameters.Add("@email", SqlDbType.VarChar).Value = _email
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
                database.Command.CommandText = "members_Delete"
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
            If _FirstName = String.Empty Then
                result = False
            End If

            If _FirstName.Length > 20 Then
                result = False
            End If

            If _LastName = String.Empty Then
                result = False
            End If


            If _LastName.Length > 30 Then
                result = False
            End If

            If _Username = String.Empty Then
                result = False
            End If

            If _Username.Length > 20 Then
                result = False
            End If

            If _password = String.Empty Then
                result = False
            End If

            If _password.Length > 30 Then
                result = False
            End If

            If _email = String.Empty Then
                result = False
            End If

            If _email.Length > 50 Then
                result = False
            End If


            Return result
        End Function

#End Region

#Region " Public Methods "


        Public Function Save() As Member
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
            '
            '
            If MyBase.IsDirty = True AndAlso IsValid() = True Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function GetByID(ID As Guid) As Member
            Dim db As New Database(System.Web.Configuration.WebConfigurationManager.AppSettings("ConnectionName"))
            Dim ds As DataSet = Nothing
            db.Command.CommandType = CommandType.StoredProcedure
            db.Command.CommandText = "members_GetByID"
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
        Public Function Login(Username As String, Password As String) As Member
            Dim db As New Database(System.Web.Configuration.WebConfigurationManager.AppSettings("ConnectionName"))
            Dim ds As DataSet = Nothing
            db.Command.CommandType = CommandType.StoredProcedure
            db.Command.CommandText = "member_Login"
            db.Command.Parameters.Add("@Username", SqlDbType.VarChar).Value = Username
            db.Command.Parameters.Add("@Password", SqlDbType.VarChar).Value = Password

            ds = db.ExecuteQuery

            If ds.Tables(0).Rows.Count = 1 Then
                Dim dr As DataRow = ds.Tables(0).Rows(0)
                MyBase.Initialize(dr)
                InitializeBusinessData(dr)
                MyBase.IsNew = False
                MyBase.IsDirty = False
            
            End If

            Return Me
        End Function

        Public Sub InitializeBusinessData(dr As DataRow)
            _FirstName = dr("First_Name")
            _LastName = dr("Last_Name")
            _Username = dr("username")
            _password = dr("password")
            _email = dr("email")

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
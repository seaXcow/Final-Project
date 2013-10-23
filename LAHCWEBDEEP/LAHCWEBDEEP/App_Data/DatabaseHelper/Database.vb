Imports System.Data.SqlClient
Namespace DatabaseHelper
    Public Class Database
#Region " Private Members "
        Private _cn As SqlConnection
        Private _cmd As SqlCommand
        Private _da As SqlDataAdapter
        Private _ds As DataSet
        Private _transaction As SqlTransaction

        Private _ConnectionName As String = String.Empty

#End Region

#Region " Public Properties "
        Public Property ConnectionName() As String
            Get
                Return _ConnectionName
            End Get
            Set(ByVal value As String)
                _ConnectionName = value
            End Set
        End Property

        Public ReadOnly Property Command As SqlCommand
            Get
                Return _cmd
            End Get
        End Property

#End Region

#Region " Public Methods "
        'Insert 3 single quotes and then this will pop up
        ''' <summary>
        '''  Allows you to add or update the database
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>An SQLCommand object which holds the header data values</remarks>
        Public Function ExecuteNonQuery() As SqlCommand 'HeaderData is returned to the user with the SqlCommand Object
            'go get the connection string from the configuration file
            _cn.ConnectionString = ConfigurationHelper.Configuration.GetConnectionString(_ConnectionName)
            'Open Sazame
            _cn.Open()
            'Tell the command object about the connection
            _cmd.Connection = _cn
            'Execute the command object
            'This assumes that the command text and parameters have been setup
            _cmd.ExecuteNonQuery()
            'close the connection
            _cn.Close()

            'return the command object which will have values output from th estored procedure (ie. header data values)
            Return _cmd

        End Function

        ''' <summary>
        ''' Retrieves data from database
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ExecuteQuery() As DataSet
            'Get the connection string from the configuration file
            _cn.ConnectionString = ConfigurationHelper.Configuration.GetConnectionString(ConnectionName)
            'Open up the connection
            _cn.Open()
            'Tell the command object about the connection
            _cmd.Connection = _cn
            'Tell the data adapter about the command object
            _da.SelectCommand = _cmd
            'fill up the dataset with the data adapter
            _da.Fill(_ds)
            'close the connection
            _cn.Close()
            'return the dataset to the caller
            Return _ds

        End Function

        Public Sub BeginTransaction(connectionName As String)
            'SETUP THE CONNECTION STRING
            _cn.ConnectionString = ConfigurationHelper.Configuration.GetConnectionString(connectionName)

            'OPEN THE CONNECTION
            _cn.Open()

            'BEGIN THE TRANSACTION
            _transaction = _cn.BeginTransaction
            'TELL THE COMMAND OBJECT ABOUT THE CONNECTION
            _cmd.Connection = _cn
            'TELL THE COMMANDS TRANSACTION ABOUT THE TRANSACTION
            _cmd.Transaction = _transaction
        End Sub

        Public Sub EndTransaction()
            'COMMIT TRANSACTION
            _transaction.Commit()
            'CLOSE TRANSACTION
            _cn.Close()
        End Sub

        Public Sub Rollback()
            'ROLLBACK THE TRANSACTION
            _transaction.Rollback()
            'CLOSE THE TRANSACTION
            _cn.Close()
        End Sub

        Public Function ExecuteNonQueryWithTransaction() As SqlCommand
            'ASSUME THE CONNECTION HAS BEEN OPENED
            'SETUP THE COMMAND OBJECT
            'CALL THE EXECUTENONQUERY METHOD OF THE COMMAND OBJECT
            _cmd.ExecuteNonQuery()
            'RETURN THE COMMAND OBJECT TO THE CALLER
            Return _cmd
        End Function

#End Region

#Region " Private Methods "

#End Region

#Region " Public Events "

#End Region

#Region " Public Event Handlers "

#End Region

#Region " Construction "
        'Property without the connection 'name' is the default constructor
        ''' <summary>
        ''' This method excepts the name of the Connection String.
        ''' </summary>
        ''' <param name="name"></param>
        ''' <remarks></remarks>
        Public Sub New(name As String)
            _cn = New SqlConnection
            _cmd = New SqlCommand
            _da = New SqlDataAdapter
            _ds = New DataSet
            _ConnectionName = name
        End Sub

        Public Sub New()
            _cn = New SqlConnection
            _cmd = New SqlCommand
            _da = New SqlDataAdapter
            _ds = New DataSet

        End Sub
#End Region



    End Class
End Namespace

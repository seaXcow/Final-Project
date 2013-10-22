Imports System.Collections.Generic
Imports System.Linq
Imports System.Text


Namespace SQLHelper
    Public Class Criteria
#Region "Private Members"
        Private _TableName As String = [String].Empty
        Private _Fields As New List(Of String)()
        Private _Values As New List(Of String)()
        Private _Types As New List(Of DataTypeHelper.Types.DataType)()
#End Region

#Region "Public Properties"

        Public Property TableName() As String
            Get
                Return _TableName
            End Get
            Set(value As String)
                _TableName = value
            End Set
        End Property
        Public ReadOnly Property Fields() As List(Of String)
            Get
                Return _Fields
            End Get
        End Property
        Public ReadOnly Property Values() As List(Of String)
            Get
                Return _Values
            End Get
        End Property
        Public ReadOnly Property Types() As List(Of DataTypeHelper.Types.DataType)
            Get
                Return _Types
            End Get
        End Property

#End Region


    End Class
End Namespace

Imports System.Configuration.ConfigurationManager
Namespace ConfigurationHelper

    Public Class Configuration
        Public Shared Function GetConnectionString(name As String) As String

            Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings(name).ConnectionString

            Return connectionString

        End Function
    End Class
End Namespace
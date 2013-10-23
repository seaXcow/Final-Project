Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace SQLHelper
    Public Class Builder
        Public Shared Function Build(crit As Criteria) As String
            Dim sql As String = " SELECT * FROM " & Convert.ToString(crit.TableName)

            If crit.Fields.Count = 0 Then
                sql += " WHERE [Deleted] = 'false' "
            Else
                sql += " WHERE "
            End If

            For i As Integer = 0 To crit.Fields.Count - 1
                If crit.Types(i) = DataTypeHelper.Types.DataType.String_Contains Then
                    sql += ("[" + crit.Fields(i) & "] LIKE '%") + crit.Values(i) & "%'"
                ElseIf crit.Types(i) = DataTypeHelper.Types.DataType.String_Starts_With Then
                    sql += ("[" + crit.Fields(i) & "] LIKE '") + crit.Values(i) & "%'"
                ElseIf crit.Types(i) = DataTypeHelper.Types.DataType.String_Ends_with Then
                    sql += ("[" + crit.Fields(i) & "] LIKE '%") + crit.Values(i) & "'"
                End If
                If i < crit.Fields.Count - 1 Then
                    sql += " AND "
                Else
                    sql += "AND [Deleted] = 'false'"


                End If
            Next
            Return sql




        End Function
        'END BUILD
        Public Shared Function BuildList(crit As Criteria) As String
            Dim sql As String = String.Empty
            If crit.TableName <> [String].Empty Then
                sql = "SELECT DISTINCT PersonID FROM" & Convert.ToString(crit.TableName)
                'add the where clause to select fields from table
                'if ther are no fields entered, give the whole table
                If crit.Fields.Count = 0 Then
                    sql += sql & " WHERE [DELETED] = 'false'"
                Else
                    sql = sql & " WHERE "
                End If
                'end if
                For i As Integer = 0 To crit.Fields.Count - 1
                    If crit.Types(i) = DataTypeHelper.Types.DataType.String_Contains Then
                        sql = ((sql & "[") + crit.Fields(i) & "]" & " LIKE '%") + crit.Values(i) & "%"
                    ElseIf crit.Types(i) = DataTypeHelper.Types.DataType.String_Starts_With Then
                        sql += ((sql & "[") + crit.Fields(i) & "]" & " LIKE '%") + crit.Values(i) & "%'"
                    ElseIf crit.Types(i) = DataTypeHelper.Types.DataType.String_Ends_with Then
                        sql = ((sql & "[") + crit.Fields(i) & "]" & " LIKE '%") + crit.Values(i) & "'"
                    End If
                    If i < crit.Fields.Count - 1 Then
                        sql += sql & " AND "
                    Else
                        sql = sql & " AND DELETED = 'false'"


                    End If

                Next
            End If
            Return sql
        End Function
        'END CLASS
    End Class
End Namespace
'END NAMESPACE
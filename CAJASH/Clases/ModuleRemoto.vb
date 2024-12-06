Imports System.Data.Odbc
Imports System.Threading.Tasks

Module ModuleRemoto
    Private conn2 As OdbcConnection

    'Public Sub ConectarRemoto()
    '    conn2 = New OdbcConnection("dsn=AguaRemoto")
    '    If conn2.State = ConnectionState.Closed Then
    '        conn2.Open()
    '    End If
    'End Sub

    Public Sub DesconectarRemoto()
        If conn2 IsNot Nothing AndAlso conn2.State = ConnectionState.Open Then
            conn2.Close()
        End If
    End Sub

    Public Async Function EjecutarConsultaRemotaAsync(ByVal sqlQuery As String) As Task

    End Function
End Module

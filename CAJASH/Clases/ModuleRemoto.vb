Imports System.Data.Odbc
Imports System.Threading.Tasks

Module ModuleRemoto
    Private conn2 As OdbcConnection

    Public Sub ConectarRemoto()
        conn2 = New OdbcConnection("dsn=AguaRemoto")
        If conn2.State = ConnectionState.Closed Then
            conn2.Open()
        End If
    End Sub

    Public Sub DesconectarRemoto()
        If conn2 IsNot Nothing AndAlso conn2.State = ConnectionState.Open Then
            conn2.Close()
        End If
    End Sub

    Public Async Function EjecutarConsultaRemotaAsync(ByVal sqlQuery As String) As Task
        If conn2 Is Nothing OrElse conn2.State <> ConnectionState.Open Then
            Try
                conn2.Open()
            Catch ex As Exception

            End Try

        End If

        Try
            Dim cmd As New OdbcCommand(sqlQuery, conn2)
            Await cmd.ExecuteNonQueryAsync()
        Catch ex As Exception
            Dim arch As New clsDocumentoTXT
            arch.guardartxt(sqlQuery, "c:\cajamovil\Remoto" & My.Settings.caja & My.Settings.serie & Now.ToString("yyyyMMdd") & ".SQL")
        End Try
    End Function
End Module

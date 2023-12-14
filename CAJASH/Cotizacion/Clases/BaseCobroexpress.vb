Imports System.Data
Public Class BaseCobroexpress
    Public conexion As New MySql.Data.MySqlClient.MySqlConnection(My.Settings.ConexionCobroexpress)
    Public Sub New()
        conectar()


    End Sub

    Public Sub conectar()
        If conexion.State = ConnectionState.Open Then
            Return
        End If
        Try
            conexion.Open()
        Catch ex As Exception

        End Try


    End Sub

    Public Sub desconectar()
        conexion.Close()
    End Sub

    Public Sub ejecutar(ByVal txtsql As String)
        conexion.Close()
        'Try
        conexion.Open()

        Dim comando As New MySql.Data.MySqlClient.MySqlCommand
        comando.CommandText = txtsql
        comando.Connection = conexion
        comando.ExecuteNonQuery()




        '2:
        'Catch ex As Exception

        'End Try
        If My.Settings.escajamovil.ToLower = "si" Then
            Dim arch As New clsDocumentoTXT
            arch.guardartxt(txtsql, "c:\cajamovil\" & My.Settings.caja & My.Settings.serie & Now.ToString("yyyyMMdd") & ".SQL")
        End If

    End Sub

    Public Sub ejecutarSIMPLE(ByVal txtsql As String)
        conectar()
        Dim comando As New MySql.Data.MySqlClient.MySqlCommand
        comando.CommandText = txtsql
        comando.Connection = conexion
        Try
            comando.ExecuteNonQuery()
        Catch ex As Exception

        End Try

        '2:
        'Catch ex As Exception

        'End Try


    End Sub

    Public Function consultasql(ByVal txtsql As String) As Data.Common.DbDataReader

        Dim rs As Data.Common.DbDataReader = Nothing
        Try
            conectar()

            Dim comando2 As New MySql.Data.MySqlClient.MySqlCommand
            comando2.Connection = conexion
            comando2.CommandText = txtsql
            rs = comando2.ExecuteReader
            Return rs

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return rs
        End Try
        Return rs
    End Function
    'Public Function obtenerCampo(ByVal sql As String, ByVal campo As String) As String
    '    Dim comDatos As Data.Common.DbCommand = New Data.Common.DbCommand
    '    comDatos.Connection = conexion
    '    comDatos.CommandText = sql
    '    comDatos.CommandType = CommandType.Text
    '    Try
    '        Dim dr As System.Data.IDataReader
    '        dr = comDatos.ExecuteReader()
    '        Application.DoEvents()
    '        dr.Read()

    '        obtenerCampo = dr(campo).ToString
    '    Catch ex As Exception
    '        Return "0"
    '    End Try

    '    If obtenerCampo <> "" Then
    '        Return obtenerCampo
    '    Else
    '        Return "0"
    '    End If
    'End Function

   
End Class

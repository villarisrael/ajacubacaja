Imports System.Data.Odbc
Imports System.Data

Public Class base
    Public conexion As New OdbcConnection(My.Settings.baseaguaConnectionString)

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
    'Desconecta de la base de datos
    Public Sub desconectar()
        conexion.Close()
    End Sub

    Public Sub ejecutar(txtsql As String)
        ' Dim conexion As New OdbcConnection(My.Settings.baseaguaConnectionString)
        'Try
        '   conexion.Open()
        'txtsql = txtsql.ToLower()

        Dim comando As New OdbcCommand
        comando.CommandText = txtsql
        comando.Connection = conexion
        comando.ExecuteNonQuery()

        'conexion.Dispose()


        '2:
        'Catch ex As Exception

        'End Try
        If My.Settings.escajamovil.ToLower = "si" Then
            Dim arch As New clsDocumentoTXT
            arch.guardartxt(txtsql, "c:\cajamovil\" & My.Settings.caja & My.Settings.serie & Now.ToString("yyyyMMdd") & ".SQL")
        End If

    End Sub

    Public Sub ejecutarSIMPLE(ByVal txtsql As String)

        Dim conexion As New OdbcConnection(My.Settings.baseaguaConnectionString)

        conexion.Open()
        Dim comando As New ODBCCommand
        comando.CommandText = txtsql
        comando.Connection = conexion
        Try
            comando.ExecuteNonQuery()
            conexion.Dispose()
        Catch ex As Exception

        End Try

        '2:
        'Catch ex As Exception

        'End Try


    End Sub

    Public Function consultasql(ByVal txtsql As String) As OdbcDataReader
        txtsql = txtsql.ToLower()
        Dim rs As OdbcDataReader = Nothing
        Try
            conectar()

            Dim comando2 As New OdbcCommand
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


    Public Sub CrearXml(ByVal txtsql As String, ByVal NombreArch As String, Optional ByVal ruta As String = "c:\", Optional ByVal nombre As String = "row")
        Dim bs As New BindingSource
        Dim sql1 As ODBCCommand = New ODBCCommand
        Dim da As New OdbcDataAdapter(sql1)
        Dim sqlCommandBuilder As New ODBCCommandBuilder(da)
        Dim ds As New System.Data.DataSet
        sql1.Connection = conexion
        sql1.CommandText = txtsql
        sql1.CommandType = CommandType.Text
        Application.DoEvents()
        da.Fill(ds, nombre)
        ds.Tables(0).WriteXml(ruta & NombreArch & ".Xml")
    End Sub

    Public Function llenaGrid(ByVal grid As DataGridView, ByVal txtSql As String) As Collection
        txtSql = txtSql.ToLower()
        llenaGrid = New Collection
        Dim bs As New BindingSource
        Dim sql1 As ODBCCommand = New ODBCCommand
        Dim da As New OdbcDataAdapter(sql1)
        Dim sqlCommandBuilder As New ODBCCommandBuilder(da)
        Dim ds As New System.Data.DataSet

        sql1.Connection = conexion
        sql1.CommandText = txtSql
        sql1.CommandType = CommandType.Text
        da.Fill(ds)
        Application.DoEvents()
        bs.DataSource = ds.Tables(0)

        grid.DataSource = bs 'ds.Tables(0)
        grid.Refresh()
        Try
            If grid.Rows.Count <> 0 Then
                grid.ClearSelection()
                grid.CurrentCell = grid(0, grid.RowCount - 1)
            End If
        Catch ex As Exception
            If grid.Rows.Count <> 0 Then
                Try
                    grid.ClearSelection()
                    grid.CurrentCell = grid(1, grid.RowCount - 1)
                Catch ex1 As Exception
                    Exit Try
                End Try
            End If
            'Throw New ArgumentOutOfRangeException("Salir")
        End Try
        llenaGrid.Add(bs)
        llenaGrid.Add(da)
    End Function


    Public Sub llenarCombo(ByVal combo As ComboBox, ByVal txtSql As String)
        txtSql = txtSql.ToLower()
        Dim da As New OdbcDataAdapter(txtSql, conexion)

        Dim dt As New DataTable
        Try
            da.Fill(dt)
            Application.DoEvents()
            combo.DataSource = dt
            combo.ValueMember = dt.Columns(0).ToString
            combo.DisplayMember = dt.Columns(1).ToString
            combo.SelectedIndex = -1

        Catch ex As Exception
            MessageBox.Show(ex.Message())
            'MessageBoxEx.Show("Posible perdida de conexión")
        End Try
    End Sub


    Public Sub llenarCombo(ByVal combo As ComboBox, ByVal campo As String, ByVal txtSql As String)
        txtSql = txtSql.ToLower()
        Dim sql As OdbcCommand = New OdbcCommand
        Dim da As New OdbcDataAdapter(sql)
        Dim ds As New DataSet
        sql.Connection = conexion
        sql.CommandText = txtSql
        sql.CommandType = CommandType.Text

        Dim dr As System.Data.IDataReader
        dr = sql.ExecuteReader()
        combo.Items.Clear()
        While dr.Read()
            Application.DoEvents()
            combo.Items.Add(dr(campo))
        End While
    End Sub

    Public Function obtenerCampo(ByVal sql As String, ByVal campo As String) As String
        sql = sql.ToLower()
        Dim comDatos As OdbcCommand = New OdbcCommand
        comDatos.Connection = conexion
        comDatos.CommandText = sql
        comDatos.CommandType = CommandType.Text
        Try
            Dim dr As System.Data.IDataReader
            dr = comDatos.ExecuteReader()
            Application.DoEvents()
            dr.Read()

            obtenerCampo = dr(campo).ToString
        Catch ex As Exception
            Return "0"
        End Try

        If obtenerCampo <> "" Then
            Return obtenerCampo
        Else
            Return "0"
        End If
    End Function


    Public Sub MATACONECCIONES()
        'Dim CONEX As OdbcDataReader
        'CONEX = consultasql("SHOW PROCESSLIST")
        'Dim COLE As New Collection
        'Try
        '    While CONEX.Read
        '        If CONEX("COMMAND") = "Sleep" And CONEX("TIME") > 3600 Then
        '            COLE.Add(CONEX("ID").ToString)
        '        End If
        '    End While
        '    Dim CADENA As String = ""
        '    For I = COLE.Count - 1 To 1 Step -1
        '        CADENA += "KILL " & COLE(I) & ";"
        '    Next
        '    ejecutarSIMPLE(CADENA)
        'Catch ex As Exception

        'End Try
      
    End Sub
    Public Sub MATACONECCIONES(ByVal tiempo As Integer)
        'Dim CONEX As OdbcDataReader
        'CONEX = consultasql("SHOW PROCESSLIST")
        'Dim COLE As New Collection
        'Try
        '    While CONEX.Read
        '        If CONEX("COMMAND") = "Sleep" And CONEX("TIME") > tiempo Then
        '            COLE.Add(CONEX("ID").ToString)
        '        End If
        '    End While
        '    Dim CADENA As String = ""
        '    For I = COLE.Count - 1 To 1 Step -1
        '        CADENA += "KILL " & COLE(I) & ";"
        '    Next
        '    ejecutarSIMPLE(CADENA)
        'Catch ex As Exception

        'End Try

    End Sub
End Class

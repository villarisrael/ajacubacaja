Public Class frmBuscaruser

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.Text = "Realizar búsqueda                                             " & Today & "   " & TimeOfDay
    End Sub
    Private Sub FrmBuscarxnombre_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Timer1.Start()
        Me.DTGbusqueda.BackgroundColor = Color.White
    End Sub

    Private Sub btncancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancelar.Click
        Me.Hide()
    End Sub

    Private Sub txtcuenta_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcuenta.TextChanged
        Try
            Dim x As base = New base()
            x.llenaGrid(DTGbusqueda, "   SELECT CUENTA, cuentaAnterior, NOMBRE, DIRECCION, COLONIA,DESCRIPCION_CUOTA FROM vusuario WHERE CUENTA =" & txtcuenta.Text)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtnombre.TextChanged
        If txtnombre.Text.Length > 3 Then
            Dim x As base = New base()
            x.llenaGrid(DTGbusqueda, "   SELECT CUENTA, cuentaAnterior, NOMBRE, DIRECCION, COLONIA,DESCRIPCION_CUOTA, manzana, lote FROM vusuario WHERE NOMBRE LIKE '%" & txtnombre.Text & "%'")
            ajustacolumnas()
        End If
    End Sub

    Private Sub txtdireccion_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdireccion.TextChanged
        If txtdireccion.Text.Length > 3 Then
            Dim x As base = New base()
            x.llenaGrid(DTGbusqueda, "   SELECT CUENTA, cuentaAnterior, NOMBRE, DIRECCION, COLONIA,DESCRIPCION_CUOTA FROM vusuario WHERE DIRECCION LIKE '%" & txtdireccion.Text & "%'")
            ajustacolumnas()
        End If
    End Sub

    Private Sub txtcolonia_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcolonia.TextChanged
        If txtcolonia.Text.Length > 3 Then
            Dim x As base = New base()
            x.llenaGrid(DTGbusqueda, "SELECT CUENTA, cuentaAnterior, NOMBRE, DIRECCION, COLONIA,DESCRIPCION_CUOTA FROM vusuario WHERE COLONIA LIKE '%" & txtcolonia.Text & "%'")
            ajustacolumnas()

        End If
    End Sub

    Private Sub DTGbusqueda_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DTGbusqueda.CellMouseDoubleClick
        Try
            Dim cuenta As Long = DTGbusqueda.SelectedRows.Item(0).Cells(0).Value
            Caja.txtCuentaCliente.Text = cuenta
            Caja.usuario = 1
            Caja.RadButton10_Click(sender, e)
            Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ajustacolumnas()
        DTGbusqueda.Columns.Item(0).Width = 30
        DTGbusqueda.Columns.Item(1).Width = 60

        DTGbusqueda.Columns.Item(2).Width = 60
        DTGbusqueda.Columns.Item(3).Width = 60
        DTGbusqueda.Columns.Item(4).Width = 60
        Try
            DTGbusqueda.Columns.Item(5).Width = 30
            DTGbusqueda.Columns.Item(6).Width = 30
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnaceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnaceptar.Click
        Try
            Dim cuenta As Long = DTGbusqueda.SelectedRows.Item(0).Cells(0).Value
            Caja.txtCuentaCliente.Text = cuenta
            Caja.usuario = 1
            Caja.RadButton10_Click(sender, e)
            Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtcuentaAnterior_TextChanged(sender As Object, e As EventArgs) Handles txtcuentaAnterior.TextChanged

        Try
            Dim x As base = New base()
            x.llenaGrid(DTGbusqueda, "SELECT CUENTA, cuentaAnterior, NOMBRE, DIRECCION, COLONIA,DESCRIPCION_CUOTA FROM vusuario WHERE cuentaAnterior ='" & txtcuentaAnterior.Text & "'")
        Catch ex As Exception
        End Try

    End Sub

    Private Sub txtcuentaAnterior_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcuentaAnterior.KeyDown
        If e.KeyCode = 13 Then
            Try
                Dim x As base = New base()
                x.llenaGrid(DTGbusqueda, "   SELECT CUENTA, cuentaAnterior, NOMBRE, DIRECCION, COLONIA,DESCRIPCION_CUOTA FROM vusuario WHERE cuentaAnterior ='" & txtcuentaAnterior.Text & "'")
            Catch ex As Exception
            End Try
        End If
    End Sub


    Private Sub txtUbicacion__KeyDown(sender As Object, e As KeyEventArgs) Handles txtUbicacion.KeyDown
        If e.KeyCode = 13 Then
            Try
                Dim x As base = New base()
                x.llenaGrid(DTGbusqueda, "   SELECT CUENTA, cuentaAnterior, NOMBRE, DIRECCION, COLONIA,DESCRIPCION_CUOTA FROM vusuario WHERE ubicacion ='" & txtUbicacion.Text & "'")
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub txtUbicacion_TextChanged(sender As Object, e As EventArgs) Handles txtUbicacion.TextChanged

        Try
                Dim x As base = New base()
            x.llenaGrid(DTGbusqueda, "   SELECT CUENTA, cuentaAnterior, NOMBRE, DIRECCION, COLONIA,DESCRIPCION_CUOTA FROM vusuario WHERE ubicacion ='" & txtUbicacion.Text & "'")
        Catch ex As Exception
            End Try
    End Sub
End Class

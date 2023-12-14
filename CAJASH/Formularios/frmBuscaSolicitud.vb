Public Class FrmBuscaSolicitud

    Private Sub FrmBuscaSolicitud_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub txtNumero_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNumero.TextChanged
        If txtNumero.Text.Length > 0 Then
            Dim x As base = New base()
            x.llenaGrid(DTGbusqueda, "SELECT S.NUMERO, S.NOMBRE, S.DOMICILIO, C.COLONIA, COM.COMUNIDAD FROM SOLICITUD S INNER JOIN COLONIA C ON (C.ID_COLONIA = S.ID_COLONIA) INNER JOIN COMUNIDADES COM ON (COM.Id_comunidad = S.Id_comunidad) WHERE NUMERO LIKE '%" & txtNumero.Text & "%' and Estadosolicitud='PENDIENTE'")
        End If
    End Sub

    Private Sub txtNombre_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNombre.TextChanged
        If txtNombre.Text.Length > 3 Then
            Dim x As base = New base()
            x.llenaGrid(DTGbusqueda, "SELECT S.NUMERO, S.NOMBRE, S.DOMICILIO, C.COLONIA, COM.COMUNIDAD FROM SOLICITUD S INNER JOIN COLONIA C ON (C.ID_COLONIA = S.ID_COLONIA) INNER JOIN COMUNIDADES COM ON (COM.Id_comunidad = S.Id_comunidad) WHERE NOMBRE LIKE '%" & txtNombre.Text & "%' and Estadosolicitud='PENDIENTE'")
        End If
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.Hide()
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Try
            Dim cuenta As Long = DTGbusqueda.SelectedRows.Item(0).Cells(0).Value
            Caja.txtCuentaCliente.Text = cuenta
            Caja.usuario = 3
            Caja.RadButton10_Click(sender, e)
            Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DTGbusqueda_CellMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DTGbusqueda.CellMouseDoubleClick
        Try
            Dim cuenta As Long = DTGbusqueda.SelectedRows.Item(0).Cells(0).Value
            Caja.txtCuentaCliente.Text = cuenta
            Caja.usuario = 3
            Caja.RadButton10_Click(sender, e)
            Close()
        Catch ex As Exception

        End Try
    End Sub
End Class

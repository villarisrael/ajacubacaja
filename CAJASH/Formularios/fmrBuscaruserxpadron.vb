Public Class fmrBuscaruserxpadron

    Private Sub btncancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancelar.Click
        Me.Hide()
    End Sub

    Private Sub txtcuenta_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcuenta.TextChanged

        Try
            Dim x As base = New base()
            'x.llenaGrid(DGVContenido, "   SELECT CUENTA , NOMBRE, DIRECCION, COLONIA, RFC FROM consesionarios WHERE CUENTA =" & txtcuenta.Text)
            'x.llenaGrid(DGVContenido, "   SELECT CUENTA , NOMBRE, DIRECCION, COLONIA, RFC FROM consesionarios INNER JOIN colonia ON (COLONIA.ID_COLONIA= consesionarios.ncolonia) WHERE CUENTA=" & txtcuenta.Text)
            x.llenaGrid(DGVContenido, "   SELECT  CLAVE, NOMBRE, COMUNIDAD, COLONIA, DIRECCION, RFC FROM nousuarios WHERE CLAVE=" & txtcuenta.Text)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub txtnombre_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtnombre.TextChanged
        If txtnombre.Text.Length > 3 Then
            Dim x As base = New base()
            'x.llenaGrid(DGVContenido, "   SELECT CUENTA , NOMBRE,DIRECCION, COMUNIDAD FROM consesionarios WHERE NOMBRE LIKE '%" & txtnombre.Text & "%'")
            'x.llenaGrid(DGVContenido, "  SELECT CUENTA, NOMBRE, DIRECCION, Comunidad FROM consesionarios INNER JOIN comunidades ON (comunidades.Id_comunidad= consesionarios.NUMCOMU) WHERE NOMBRE LIKE '%" & txtnombre.Text & "%'")
            x.llenaGrid(DGVContenido, "   SELECT  CLAVE, NOMBRE, COMUNIDAD, COLONIA, DIRECCION, RFC FROM nousuarios WHERE NOMBRE LIKE '%" & txtnombre.Text & "%'")
        End If
    End Sub

    Private Sub txtdireccion_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdireccion.TextChanged
        If txtdireccion.Text.Length > 3 Then
            Dim x As base = New base()
            'x.llenaGrid(DGVContenido, "SELECT CUENTA , NOMBRE, DIRECCION, COMUNIDAD FROM consesionarios WHERE DIRECCION LIKE '%" & txtdireccion.Text & "%'")
            'x.llenaGrid(DGVContenido, "SELECT CUENTA, NOMBRE, DIRECCION, Comunidad FROM consesionarios INNER JOIN comunidades ON (comunidades.Id_comunidad= consesionarios.NUMCOMU) WHERE DIRECCION LIKE '%" & txtdireccion.Text & "%'")
            x.llenaGrid(DGVContenido, "   SELECT  CLAVE, NOMBRE, COMUNIDAD, COLONIA, DIRECCION, RFC FROM nousuarios WHERE DIRECCION LIKE '%" & txtdireccion.Text & "%'")
        End If
    End Sub

    Private Sub txtcolonia_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcolonia.TextChanged
        If txtcolonia.Text.Length > 3 Then
            Dim x As base = New base()
            'x.llenaGrid(DGVContenido, "SELECT CUENTA , NOMBRE, DIRECCION, COLONIA FROM consesionarios WHERE COLONIA LIKE '%" & txtcolonia.Text & "%'")
            'x.llenaGrid(DGVContenido, " SELECT CUENTA , NOMBRE, DIRECCION, COLONIA FROM consesionarios INNER JOIN colonia ON (COLONIA.ID_COLONIA= consesionarios.ncolonia) WHERE COLONIA LIKE '%" & txtcolonia.Text & "%'")
            x.llenaGrid(DGVContenido, "   SELECT  CLAVE, NOMBRE, COMUNIDAD, COLONIA, DIRECCION, RFC FROM nousuarios WHERE COLONIA LIKE '%" & txtcolonia.Text & "%'")
        End If
    End Sub

    Private Sub btnagregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnagregar.Click
        Try
            Dim busca As Long = DGVContenido.SelectedRows.Item(0).Cells(0).Value

            Caja.txtCuentaCliente.Text = busca
            Caja.usuario = 2
            Caja.RadButton10_Click(sender, e)
            Close()
        Catch ex As Exception

        End Try


    End Sub

    Private Sub DGVContenido_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVContenido.CellDoubleClick
        Try
            Dim cliente As Long = DGVContenido.SelectedRows.Item(0).Cells(0).Value
            Caja.txtCuentaCliente.Text = cliente
            Caja.usuario = 2
            Caja.RadButton10_Click(sender, e)
            Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub fmrBuscaruserxpadron_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.DGVContenido.BackgroundColor = Color.White
    End Sub

    Private Sub DGVContenido_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVContenido.CellContentClick

    End Sub
End Class
Public Class Frmabono
    Public concepto As Clsconcepto
    Public control As Clscontrolpago
    Public numerodeconcepto As Integer

    Private Sub btnsalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsalir.Click
        Close()

    End Sub

    Private Sub btnaceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaceptar.Click
        If concepto.CLAVEMOV > 0 Then
            If diAbono.Value > concepto.importe + concepto.IVA Then
                MessageBox.Show("El abono no puede ser mayor al importe original")
                Exit Sub
            End If
        End If
        Dim nuevoimporte As Double = 0
        Dim iva As Double = 0
        If concepto.Concepto.Contains("RECARGO") Then
            concepto.llevaiva = 0
        End If
        If concepto.llevaiva Then
            nuevoimporte = Math.Round(diAbono.Value / (1 + (variable_iva / 100)), 2)
            iva = Math.Round(nuevoimporte * (variable_iva / 100), 2)
        Else
            nuevoimporte = diAbono.Value
            iva = 0
        End If
        concepto.importe = nuevoimporte
        concepto.IVA = iva
        concepto.Preciounitario = Math.Round(concepto.importe / concepto.Cantidad, 2)
        control.Listadeconceptos.Remove(numerodeconcepto)
        control.Listadeconceptos.Add(concepto)

        Try
            If concepto.Clave = My.Settings.Clavederecargo Then
                If My.Settings.esAdministrativa.ToUpper() = "DIRECTOR" Then
                    control.totaldescuentorecargo = control.totaldeudarecargos - diAbono.Value
                    Dim resultfacturas As DialogResult = MessageBox.Show("¿ Desea Grabar el descuento para caja ?", "Alerta", MessageBoxButtons.OKCancel)

                    If resultfacturas = DialogResult.OK Then

                        Ejecucion("DELETE FROM descuentorecargo where cuenta=" & control.cuenta & " and estado='Pendiente'")
                        Dim cadena As String
                        cadena = "INSERT INTO descuentorecargo (cuenta,descuentorecargo,descontartodo, descuentoporc,estado,idusuario,fecha,hora,equipo,numeromeses, fuepor) values ( "
                        cadena += control.cuenta & "," & Math.Abs(control.totaldescuentorecargo) & ",0,0,'Pendiente', " & NumUser & ",curdate(),curtime(),'" & getMacAddress() & "',0,'ABONO')"
                        Ejecucion(cadena)
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try



        Close()

    End Sub

    Private Sub Frmabono_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        concepto = TryCast(control.Listadeconceptos(numerodeconcepto), Clsconcepto)
        lblclaveconcepto.Text = "Monto del concepto con IVA =" & (concepto.importe + concepto.IVA)
    End Sub
End Class

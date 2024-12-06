Imports System.Net.NetworkInformation

Public Class FrmDescuentos
    Public controldescu As New Clscontrolpago
    Public maximopermitido As Double
    Private Sub btncancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancelar.Click
        Close()
    End Sub

    Private Sub btnaceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaceptar.Click


        Try



            controldescu.descontartodoslosperiodosdeconsumo = chkconsumo.Checked
            controldescu.descontartodoslosperiodosdealcantarillado = chkalcantarillado.Checked
            controldescu.descontartodoslosperiodosdesaneamiento = chksaneamiento.Checked
            controldescu.descontartodoslosperiodosderecargo = chkrecargo.Checked
            controldescu.descontartodoslosperiodosderezago = chkrezago.Checked

            controldescu.periodoscondescuentodeconsumo = IImesesdecconsumo.Value
            controldescu.periodoscondescuentodealcantarillado = IImesesdescalcanta.Value
            controldescu.periodoscondescuentodesaneamiento = IImesesdescsaneamiento.Value
            controldescu.periodoscondescuentoderecargo = IImesesderecargo.Value
            controldescu.periodoscondescuentoderezago = IImesesrezago.Value



            'If diporcconsumo.Value <= maximopermitido Then
            controldescu.descuentoaconsumo = diporcconsumo.Value
            'Else
            '    MessageBox.Show("Tu no tienes permitido ese nivel de descuento")
            'Exit sub
            'End If


            'If diporcalcantarillado.Value <= maximopermitido Then
            controldescu.descuentoaalcantarillado = diporcalcantarillado.Value
            'Else
            '    MessageBox.Show("Tu no tienes permitido ese nivel de descuento")
            '   exit sub
            'End If


            'If diporcsaneamiento.Value <= maximopermitido Then
            controldescu.descuentoasaneamiento = diporcsanemiento.Value
            'Else
            '    MessageBox.Show("Tu no tienes permitido ese nivel de descuento")
            '   exit sub
            'End If

            'If diporcsaneamiento.Value <= maximopermitido Then
            controldescu.descuentoasaneamiento = diporcsanemiento.Value
            'Else
            '    MessageBox.Show("Tu no tienes permitido ese nivel de descuento")
            '   exit sub
            'End If

            'If diporcsaneamiento.Value <= maximopermitido Then
            controldescu.descuentoarecargo = diporcrecargo.Value
            'Else
            '    MessageBox.Show("Tu no tienes permitido ese nivel de descuento")
            '   exit sub
            'End If

            'If diporcsaneamiento.Value <= maximopermitido Then
            controldescu.descuentoarezago = DiporcRezago.Value
            'Else
            '    MessageBox.Show("Tu no tienes permitido ese nivel de descuento")
            '   exit sub
            'End If

            chkalcantarillado.Checked = False
            chkconsumo.Checked = False
            chkrecargo.Checked = False
            chkrezago.Checked = False
            chksaneamiento.Checked = False


            IImesesdecconsumo.Value = 0
            IImesesderecargo.Value = 0
            IImesesdescalcanta.Value = 0
            IImesesdescsaneamiento.Value = 0
            IImesesrezago.Value = 0

            diporcconsumo.Value = 0
            diporcalcantarillado.Value = 0
            diporcrecargo.Value = 0
            DiporcRezago.Value = 0
            diporcsanemiento.Value = 0



            If My.Settings.esAdministrativa.ToUpper = "DIRECTOR" Then

                Dim resultfacturas As DialogResult = MessageBox.Show("¿ Desea Grabar el descuento para caja ?", "Alerta", MessageBoxButtons.OKCancel)

                If resultfacturas = DialogResult.OK Then

                    Ejecucion("DELETE FROM descuentorecargo where cuenta=" & controldescu.cuenta & " and estado='Pendiente'")
                    Dim cadena As String
                    cadena = "INSERT INTO descuentorecargo  (cuenta,descuentorecargo,descontartodo, descuentoporc,estado,idusuario,fecha,hora,equipo,numeromeses, fuepor) values  ("
                    cadena += controldescu.cuenta & ",0," & controldescu.descontartodoslosperiodosderecargo & "," & controldescu.descuentoarecargo & ",'Pendiente', " & NumUser & ",curdate(),curtime(),'" & getMacAddress() & "'," & controldescu.periodoscondescuentoderecargo & ",'DESCUENTO')"
                    Ejecucion(cadena)
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try

        Close()
    End Sub

    Private Sub FrmDescuentos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub


End Class

Imports System.Data.Odbc
Public Class FrmCancelarecibo
   
    Dim fechaoriginal As Date
    Dim cuenta As Long
    Public control As New Clscontrolpago
    Public recibo As New clsrecibo
    Private Sub FrmCancelarecibo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Hide()
        End If
    End Sub

    Private Sub FrmCancelarecibo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtrecibo.Focus()
        txtserie.Text = My.Settings.serie
        lblcaja.Text = My.Settings.caja
        btnaceptar.Enabled = False
    End Sub

    Private Sub txtrecibo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtrecibo.KeyDown
        If e.KeyCode = 13 Then
            lblestado.Text = "ACTIVO"
            lblestado.BackColor = Color.Green
            btnaceptar.Enabled = True
            Try
                Dim dato As New base
                Dim consulta As odbcDatareader
                consulta = dato.consultasql("SELECT * FROM pagos WHERE RECIBO = '" & txtrecibo.Text & "'")
                If consulta.Read() Then
                    cuenta = consulta!cuenta
                    fechaoriginal = consulta!fecha_deuda
                    lbltarifa.Text = consulta!TARIFA
                    lblperiodo.Text = consulta!PERIODO
                    lblnombre.Text = consulta!NOMBRE
                    '   lblestado.Text = consulta!CANCELADO
                    lblsubtotal.Text = consulta!TOTAL
                    lbliva.Text = consulta!IVA
                    lbltotal.Text = consulta!TOTAL
                    Try
                        txtmotivos.Text = consulta!MOTIVOS
                    Catch ex As Exception

                    End Try
                    If consulta!CANCELADO = "A" Then
                        lblestado.Text = "ACTIVO"
                        lblestado.BackColor = Color.Green
                    Else
                        ' lblestado.Text = "C"
                        lblestado.Text = "CANCELADO"
                        lblestado.BackColor = Color.Red
                    End If
                Else
                    MsgBox("Recibo no existente.", MsgBoxStyle.Exclamation, "Advertencia...!!!")
                End If
            Catch ex As Exception

            End Try


        End If

    End Sub

    Private Sub btnaceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaceptar.Click
        If txtrecibo.Text = "" Then
            MessageBox.Show("No has seleccionado un recibo")
            Exit Sub
        End If
        Dim ask As MsgBoxResult
        ask = MsgBox("Realmente desea realizar la acción?", MsgBoxStyle.OkCancel, "Advertencia...!")
        If ask = MsgBoxResult.Ok Then
            Try


                Ejecucion("UPDATE pagos set Recibo = '" & txtrecibo.Text & "', Cancelado = 'C', Motivos= '" & txtmotivos.Text & "' where Recibo= " & txtrecibo.Text & "")
                Ejecucion("UPDATE usuario SET DEUDAFEC ='" & fechaoriginal.ToString("yyyy-MM-dd") & "' where cuenta=" & cuenta)
                Ejecucion(" UPDATE otrosconeptos, pagotros SET PAGADO=0 , otrosconceptos.RESTA= otrosconceptos.RESTA + pagotros.MONTO WHERE otrosconceptos.CLAVE=pagotros.CLAVEMOV AND pagotros.recibo=" & txtrecibo.Text)
                '''''''''''''''''''
                'Actualiza el recibo maestro de Cobroexpress



                ''''''''''''''''''''''
                Try
                    Dim dato As New base
                    Dim consulta As OdbcDataReader
                    consulta = dato.consultasql("SELECT * FROM pago_mes WHERE RECIBO = " & txtrecibo.Text & " and CONCEPTO='CONSUMO'")
                    While consulta.Read
                        Dim mesquecancelo As String
                        Dim periodo As String
                        'mesquecancelo = consulta!periodo
                        mesquecancelo = consulta("mes")
                        periodo = consulta("ano")
                        Dim cuenta As String = consulta!cuenta
                        Ejecucion("UPDATE lecturas SET PAGADO=0 WHERE CUENTA=" & cuenta & " AND MES='" & mesquecancelo & "' AND AN_PER='" & periodo & "'")
                    End While
                    dato.conexion.Dispose()
                Catch ex As Exception

                End Try
                '''''''''''cancelar otrosconceptos'''''''''''''''''

                '''''''''''''''''''''''''''''''''''''''''''''''''''
                MsgBox("Recibo cancelado, correctamente", MsgBoxStyle.Information, "Correcto...!!!")
                Close()
            Catch ex As Exception
                MsgBox("Hubo un error al actualizar", MsgBoxStyle.Exclamation, AcceptButton)
            End Try
        Else
            If ask = MsgBoxResult.Cancel Then

            End If
        End If
    End Sub

    Private Sub btnsalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsalir.Click
        Close()
    End Sub

    Private Sub txtrecibo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtrecibo.TextChanged

    End Sub
End Class

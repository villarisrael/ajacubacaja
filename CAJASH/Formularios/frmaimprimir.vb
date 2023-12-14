Imports System.Data.Odbc
Public Class Frmaimprimir

    Dim fechaoriginal As Date
    Dim cuenta As Long
    Public control As New Clscontrolpago
    Public recibo As New clsrecibo
    Public imprime As New clsimprimeformato
    Private Sub Frmaimprimir_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtrecibo.Focus()
        txtserie.Text = My.Settings.serie
        lblcaja.Text = My.Settings.caja

    End Sub

    Private Sub txtrecibo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtrecibo.KeyDown
        If e.KeyCode = 13 Then
            lblestado.Text = "ACTIVO"
            lblestado.BackColor = Color.Green
            Try
                Dim dato As New base
                Dim consulta As OdbcDataReader
                consulta = dato.consultasql("SELECT * FROM pagos WHERE RECIBO = " & txtrecibo.Text & " and serie ='" & txtserie.Text & "'")
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

    Private Sub txtrecibo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtrecibo.TextChanged

    End Sub

    Private Sub btnaceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaceptar.Click
        If txtrecibo.Text = "" Then
            MessageBox.Show("No has seleccionado un recibo")
            Exit Sub
        End If
        Dim ask As MsgBoxResult
        ask = MsgBox("Realmente desea realizar la acción?", MsgBoxStyle.OkCancel, "Advertencia...!")
        If ask = MsgBoxResult.Ok Then

            Dim tic As New Ticket
            tic.imprime_ticket58mm(txtserie.Text, txtrecibo.Text, True)



        Else


            If ask = MsgBoxResult.Cancel Then

            End If


        End If
        Close()
    End Sub


    Private Sub btnsalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsalir.Click
        Close()
    End Sub
End Class

Imports System.Data.Odbc

Public Class frmRestriccionDesc

    Private Sub btningresar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btningresar.Click
        Dim dato As New base
        Dim usuario, pass As String
        Dim obtener As odbcDatareader
        Dim encript As New clsclave

        encript.palabra = txtusuario.Text

        usuario = encript.encriptado

        encript.palabra = txtcontrasena.Text

        pass = encript.encriptado

        obtener = dato.consultasql("select nombre, letra from letras WHERE Nombre = '" & usuario & "'" & "AND letra = '" & pass & "'")
        If obtener.Read() Then
            Me.Hide()
            FrmDescuentos.ShowDialog()
            Close()
        Else
            MsgBox("No tienes permitido realizar descuentos", MsgBoxStyle.Exclamation, "Acceso denegado...!!!")
            Exit Sub
        End If
    End Sub

    Private Sub txtdescuento_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtdescuento.KeyPress
        If e.KeyChar = vbBack Then
            Exit Sub
        End If
        If Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnsalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsalir.Click
        Me.Hide()
    End Sub

    Private Sub frmRestriccionDesc_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtusuario.Focus()
    End Sub
End Class

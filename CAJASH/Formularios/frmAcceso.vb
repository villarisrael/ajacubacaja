Imports System.Data.Odbc
Public Class FrmAcceso

    Private Sub FrmAcceso_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ''''''''''''''Mostrar campos desde el seting
        txtclave.Text = My.Settings.folio
        lblcaja.Text = My.Settings.caja
        lblserie.Text = My.Settings.serie
        ''''''''''''''
        txtclave.ReadOnly = True
        If My.Settings.esAdministrativa.ToLower = "si" Then
            txtclave.Visible = False
            lblfolio.Visible = False
        End If

    End Sub
    Private Sub btningresar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btningresar.Click
        conectar() ' Dim dato As New base
        '''''''''''''''''''
        Dim usuarios, pass As String
        Dim obtener As odbcDatareader
        Dim encript As New Encriptar
        encript.palabra = txtusuario.Text

        usuarios = encript.Encriptada
        encript.palabra = txtcontrasena.Text

        pass = encript.Encriptada
        variable_iva = obtenerCampo("select PorcIVA FROM EMPRESA LIMIT 1", "PorcIVA")
        obtener = ConsultaSql("select * from letras WHERE User = '" & usuarios & "'" & "AND " & My.Settings.Pass & "  = '" & pass & "'").ExecuteReader
        Try
            If obtener.Read() Then
                'My.Settings.folio = txtclave.Text
                'My.MySettings.Default.folio = txtclave.Text
                encript.palabra = obtener("nombre").ToString()
                usuariodelsistema = encript.palabra
                Double.TryParse(obtener("Des").ToString(), niveldedescuento)
                CAJAS.My.Settings.folio = txtclave.Text
                My.Settings.Save()
                Caja.lblfolio.Text = My.Settings.folio
                NumUser = obtener("ccodusuario")
                '     dato.conexion.Dispose()
                Caja.Show()
                Hide()

            Else
                MsgBox("No eres un usuario registrado", MsgBoxStyle.Exclamation, "Acceso denegado...!!!")
                ' dato.conexion.Dispose()
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message())
        End Try

        '''''''''''''''''''
        '  dato.conexion.Dispose()
    End Sub
    Private Sub btnsalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsalir.Click
        ''''''''''''''''''''
        Dim ask As MsgBoxResult
        ask = MsgBox("Realmente desea salir del sistema?", MsgBoxStyle.OkCancel, "Saliendo...!")
        If ask = MsgBoxResult.Ok Then

            Application.Exit()
        Else
            If ask = MsgBoxResult.Cancel Then

            End If
        End If
        ''''''''''''''''''''
    End Sub


    Private Sub txtcontrasena_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcontrasena.KeyDown
        If e.KeyCode = 13 Then
            btningresar.Focus()
        End If
    End Sub

    Private Sub txtusuario_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtusuario.KeyDown
        If e.KeyCode = 13 Then
            txtcontrasena.Focus()
        End If
    End Sub

    Private Sub txtclave_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtclave.KeyDown
        If e.KeyCode = 13 Then
            txtusuario.Focus()
        End If
    End Sub

End Class

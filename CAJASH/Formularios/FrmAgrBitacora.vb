Imports DevComponents.DotNetBar

Public Class FrmAgrBitacora
    Public Cuenta As Double, Comunidad As String

    Private Sub FrmAgrBitacora_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Dim keyascii As Keys = e.KeyData

        Select Case keyascii
            Case Keys.Enter
                SendKeys.Send("{TAB}")
        End Select
    End Sub

    Private Sub BtnAceptar_Click(sender As Object, e As EventArgs) Handles BtnAceptar.Click
        If txtconcepto.Text = "" Then
            MessageBoxEx.Show("No ha introducido la descripcion del evento", "Nombre", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If txtmotivo.Text = "" Then
            MessageBoxEx.Show("No ha introducido el motivo del evento", "Nombre", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If


        Dim nombre_Host As String = Net.Dns.GetHostName()
        Dim este_Host As Net.IPHostEntry = Net.Dns.GetHostEntry(nombre_Host)
        Dim direccion_Ip As String = este_Host.AddressList(0).ToString

        Try
            Ejecucion("insert into bitacora(fecha,hora,evento,cuenta,usuario,concepto,maquina,motivo) values(" & UnixDateFormat(Now.Date, True, False) & ",'" & Now.ToShortTimeString() & "','Bitacora'," & Cuenta & "," & NumUser & ",'" & txtconcepto.Text & "','" & direccion_Ip & "','" & txtmotivo.Text & "')")

            MessageBoxEx.Show("Registro realizado satisfactoriamente", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'ButtonX1.Enabled = True

            'frmBusUsuario.filtro(frmBusUsuario._sqlgeneral)
            BtnAceptar.Enabled = False
        Catch ex As Exception

        End Try

    End Sub

    Private Sub BtnCancelar_Click(sender As Object, e As EventArgs) Handles BtnCancelar.Click
        Close()
    End Sub

    Private Sub FrmAgrBitacora_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cons As IDataReader = ConsultaSql("Select * from vusuario where cuenta = " & Cuenta & " ").ExecuteReader
        cons.Read()
        LblCuenta.Text = Cuenta
        LblComunidad.Text = Comunidad
        LblNombre.Text = cons("nombre")
        LblEstadoActual.Text = UCase(cons("estado"))
        LblDireccion.Text = cons("Direccion")
        LblGiro.Text = cons("giro")
        LblTarifa.Text = cons("descripcion_cuota")
    End Sub
End Class
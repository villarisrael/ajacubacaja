Public Class FRmconfigcaja

    Private Sub FRmconfigcaja_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim x As New base
        x.llenarCombo(cmbformato, "select distinct idformato,idformato from formatorecibo")
        x.llenarCombo(cmbbanco, "SELECT RFC,Nombre from c_banco order by Nombre ")

        cmbproduccionmultifacturas.Items.Clear()
        cmbproduccionmultifacturas.Items.Add(New DevComponents.DotNetBar.ComboBoxItem("SI", "SI"))
        cmbproduccionmultifacturas.Items.Add(New DevComponents.DotNetBar.ComboBoxItem("NO", "NO"))

        x.conexion.Dispose()
        txtserie.Text = My.Settings.serie
        cmbformato.Text = My.Settings.formatorecibo
        cmbcajamovil.Text = My.Settings.escajamovil
        cmbadministrativa.Text = My.Settings.esAdministrativa
        cmbtipocaja.Text = My.Settings.tipocaja
        txtalcantarillado.Text = My.Settings.Clavedealcantarillado
        txtclaveconsumo.Text = My.Settings.Clavedeconsumo
        txtsaneamiento.Text = My.Settings.clavedesaneamiento
        txtclaverecargo.Text = My.Settings.Clavederecargo
        txtclaverezago.Text = My.Settings.ClavedeRezago
        iifolio.Value = My.Settings.folio
        txtcaja.Text = My.Settings.caja


        txtservidorereportes.Text = My.Settings.servidorreporte
        txtusernamereporte.Text = My.Settings.usuarioreporte
        txtpassreporte.Text = My.Settings.passreporte
        txtbasededatos.Text = My.Settings.basereporte

        txtusuariomultifacturas.Text = My.Settings.UsuarioMultifacturas
        txtpassmultifacturas.Text = My.Settings.PassFacturaMultifacturas
        txtdireccioncermultifalturas.Text = My.Settings.CER
        txtkeymultifacturas.Text = My.Settings.KEY
        txtpasskey.Text = My.Settings.KeyContrasena
        Try
            cmbbanco.SelectedValue = My.Settings.rfcbancodestino

        Catch ex As Exception
            txtcuentabancaria.Text = My.Settings.cuentabancariadestino
        End Try


        cmbproduccionmultifacturas.SelectedText = My.Settings.TimbrarPrueba

        txtCorreo.Text = My.MySettings.Default.CorreoFacturas
        Password.Text = My.MySettings.Default.Passwordcorreo
        txtcontenidomensaje.Text = My.MySettings.Default.MENSAJECORREO
        txtasunto.Text = My.MySettings.Default.Asuntocorreo
        txtnombrecorreo.Text = My.Settings.Correodefault
        txtnocopias.Text = My.Settings.copiasderecibo

    End Sub

    

    Private Sub btnaceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaceptar.Click
        My.Settings.serie = txtserie.Text
        My.Settings.formatorecibo = cmbformato.Text
        My.Settings.escajamovil = cmbcajamovil.Text
        My.Settings.esAdministrativa = cmbadministrativa.Text
        My.Settings.tipocaja = cmbtipocaja.Text
        My.Settings.Clavedealcantarillado = txtalcantarillado.Text
        My.Settings.Clavedeconsumo = txtclaveconsumo.Text
        My.Settings.clavedesaneamiento = txtsaneamiento.Text
        My.Settings.Clavederecargo = txtclaverecargo.Text
        My.Settings.ClavedeRezago = txtclaverezago.Text
        My.Settings.folio = iifolio.Value
        My.Settings.caja = txtcaja.Text
        My.Settings.copiasderecibo = txtnocopias.Text


        My.Settings.servidorreporte = txtservidorereportes.Text
        My.Settings.usuarioreporte = txtusernamereporte.Text
        My.Settings.passreporte = txtpassreporte.Text
        My.Settings.basereporte = txtbasededatos.Text

        My.Settings.UsuarioMultifacturas = txtusuariomultifacturas.Text
        My.Settings.PassFacturaMultifacturas = txtpassmultifacturas.Text
        My.Settings.CER = txtdireccioncermultifalturas.Text
        My.Settings.KEY = txtkeymultifacturas.Text
        My.Settings.KeyContrasena = txtpasskey.Text

        My.Settings.rfcbancodestino = cmbbanco.SelectedValue
        My.Settings.cuentabancariadestino = txtcuentabancaria.Text

        My.Settings.TimbrarPrueba = cmbproduccionmultifacturas.Text


        My.MySettings.Default.CorreoFacturas = txtCorreo.Text
        My.MySettings.Default.Passwordcorreo = Password.Text
        My.MySettings.Default.MENSAJECORREO = txtcontenidomensaje.Text
        My.MySettings.Default.Asuntocorreo = txtasunto.Text
        My.Settings.Correodefault = txtnombrecorreo.Text



        My.Settings.Save()
    End Sub

    Private Sub btnsalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsalir.Click
        Close()

    End Sub
End Class

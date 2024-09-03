Imports System.Data
Imports System.Data.Odbc
Imports Telerik.WinControls.UI
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports iTextSharp.text.pdf
Imports iTextSharp.text

Public Class Caja
    '
    'Dim dts2 As New DtSConse
    'Dim usu2 As New DtSConseTableAdapters.consesionariosTableAdapter
    ''
    'Dim usu As New dtsDatosTableAdapters.usuarioTableAdapter
    ' Dim BD As New base()
    Dim imprime As New Imprimirnet
    Dim sql As String
    Dim cn As OdbcConnection
    Dim cm As OdbcCommand
    Dim da As OdbcDataAdapter
    Dim ds As DataSet
    Dim rs As OdbcDataReader
    Dim cargado As Boolean = False
    Dim control As New Clscontrolpago
    Public mesesdescconsumo As Integer
    Public mesesdescalcantarillado As Integer
    Public recibo As New clsrecibo
    Public usuario As Integer = 1
    Dim SubTotal As Double = 0
    Dim IVA As Double = 0
    Dim Total As Double = 0
    Dim concepto As New ClsRegistrolectura
    Private dts As OdbcDataReader
    Dim DtsDatos As Object
    Dim lblMedidor As New Label
    Public fechaVenConvenios As Date = Now
    Public conConvenio As Boolean = False
    Private Property OtroscobrosTableAdapter As Object
    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscarUserRegistrado.Click
        usuario = 1
        Dim bu As New frmBuscaruser
        bu.ShowDialog()
    End Sub
    Private Sub txtCuenta_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCuentaCliente.KeyDown

        If e.KeyCode = 13 Then
            If txtCuentaCliente.Text = "" Then
                MessageBox.Show("Debes ingresar el número de cuenta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            'convenio()
            If usuario = 1 Then
                chkcobrarconsumo.Checked = True
                cargardatos()
                llenabitacora()
                If control.EsMEdido Then
                    llenagridanticipo()
                    'llenagridlecturas()
                    calculotempAdelantado()
                End If
                dtpFechaPagar.Value = DateAdd(DateInterval.Month, -1, Now)
                dtpFechaPagar.Focus()
            ElseIf usuario = 2 Then
                chkcobrarconsumo.Checked = False
                cargadatosnousuario()
                'control.Listadeconceptos.Clear()
                llenagrid()
            ElseIf usuario = 3 Then
                chkcobrarconsumo.Checked = False
                'control.Listadeconceptos.Clear()
                control.desgloseconsumo.Clear()
                control.desgloserezago.Clear()
                control.desglosealcantarillado.Clear()
                control.desglosesaneamiento.Clear()
                control.desgloserecargo.Clear()
                cargasolicitud()
                llenagrid()
            End If
            dtpFechaPagar.Value = DateAdd(DateInterval.Month, -1, Now)
            dtpFechaPagar.Focus()
        End If
    End Sub

    Private Sub Caja_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Application.Exit()
    End Sub
    Private Sub Caja_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Seleccion del Texbox por Focus
        limpiacontroldedescuento()
        txtCuentaCliente.Focus()
        conectar()
        llenarCombo(ddlconsumo, "SELECT id_concepto,DESCRIPCION FROM conceptoscxc where obsoleto=0 order by descripcion")
        '  Timer1.Start()
        txtCuentaCliente.Focus()
        dtpFechaPagar.Value = DateAdd(DateInterval.Month, -1, Now)
        cargado = True
        txtnombre.ReadOnly = True
        txtcolonia.ReadOnly = True
        txtcomunidad.ReadOnly = True
        txttarifa.ReadOnly = True
        txtult_pago.ReadOnly = False
        txtdireccion.ReadOnly = True
        txtrfc.ReadOnly = True
        lblusername.Text = usuariodelsistema
        If My.Settings.esAdministrativa.ToLower = "si" Or My.Settings.tipocaja.ToLower() = "consulta" Then
            btnpagar.Visible = False
            btncancelar.Visible = False
            btnfactura.Visible = False
            BTNTARIFA.Enabled = True
            btnreportexrubro.Enabled = True
            BtnPorRubros.Enabled = True

        End If
        If My.Settings.tipocaja.ToLower() = "cobro" Then
            btnpagar.Visible = True
            btncancelar.Visible = True
            btnfactura.Visible = True
            BTNTARIFA.Enabled = False
            btnreportexrubro.Enabled = True
            BtnPorRubros.Enabled = True
            BtnPorRubros.Visible = True
        End If
        lblimpresora.Text = imprime.prtSettings.PrinterName
        lblimpresora.ForeColor = Color.Red

    End Sub
    Private Sub Caja_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        MessageBox.Show(e.KeyCode.ToString())
        'Select Case e.KeyCode
        '    Case Keys.F1
        '        MessageBox.Show(e.KeyCode.ToString())
        '    Case Keys.F2
        '        MessageBox.Show(e.KeyCode.ToString())
        '    Case Keys.F3
        '        MessageBox.Show(e.KeyCode.ToString())
        '    Case Keys.F4
        '        MessageBox.Show(e.KeyCode.ToString())
        '    Case Keys.F5
        '        MessageBox.Show(e.KeyCode.ToString())
        '    Case Keys.F6
        '        MessageBox.Show(e.KeyCode.ToString())
        '    Case Keys.F7
        '        MessageBox.Show(e.KeyCode.ToString())
        '    Case Keys.F8
        '        MessageBox.Show(e.KeyCode.ToString())
        '    Case Keys.F9
        '        MessageBox.Show(e.KeyCode.ToString())
        '    Case Keys.F11
        '        MessageBox.Show(e.KeyCode.ToString())
        '    Case Keys.F12
        '        MessageBox.Show(e.KeyCode.ToString())
        '    Case Else
        'End Select
    End Sub

#Region "Cargadatos"
    Public Sub cargardatos(Optional ByVal permitiradelanto As Boolean = False) ' pueden o no estableces si quieren pagar efectivo por defecto es no
        lblestado.Text = ""
        lblestado.BackColor = Color.White
        'Dim comopagar As New clspagofijo
        'Dim COMORECARGO As New Clsrecargo
        'control = New Clscontrolpago()
        Try
            Dim MESANTERIOR As Integer = Now.Month - 1
            Dim ANANTERIOR As Integer = Now.Year
            If MESANTERIOR = 0 Then MESANTERIOR = 12
            If MESANTERIOR = 12 Then ANANTERIOR = Now.Year - 1
            Dim taritem As String = obtenerCampo("select * from usuario where cuenta=" & txtCuentaCliente.Text, "tarifa")

            'CAMBIO DE DATOS Y MODIFICACIÓN
            'SERVICO admnistrativo

            '        If taritem = "4" Then
            '       Ejecucion("Update lecturas Set  monto = consumomedidos(consumo,'1', AN_PER )  where cuenta=" & txtCuentaCliente.Text & " and pagado=0 and valornummes(mes,an_per) < valornummes('" & NOMBREDEMES3CAR(MESANTERIOR) & "'," & ANANTERIOR & ") ")
            '      Ejecucion("Update lecturas Set  monto = consumomedidos(consumo,'4', AN_PER)  where cuenta=" & txtCuentaCliente.Text & " and pagado=0 and mes ='" & NOMBREDEMES3CAR(MESANTERIOR) & "' and  an_per=" & ANANTERIOR & "")


            '      End If

        Catch ex As Exception
            MessageBox.Show(ex.Message())
        End Try



        Dim concepto As New Clsconcepto
        Dim SubTotal As Double = 0
        Dim IVA As Double = 0
        Dim Total As Double = 0
        Dim basem As New base
        'dts = basem.consultasql("select nombre,direccion,comunidad,ubicacion,colonia,deudafec,descripcion_cuota, " & My.Settings.campousuariotarifa & " from usuario,cuotas where cuenta=" & txtCuentaCliente.Text & " and usuario." & My.Settings.campousuariotarifa & "=cuotas.numero") /*and calles.Id_comunidad= usuario.Id_comunidad)*
        If My.Settings.manejacatalogocalles = "NO" Then
            dts = basem.consultasql("select cuenta,usuario.NOMBRE, usuario.domicilio AS `Direccion`, Comunidad, ubicacion, Colonia, deudafec, descripcion_cuota,  tarifa, idcuotavalvulista, observacion, nodemedidor ,usuario.idDescuento as id_Descuento, descuentos.nPctDsct as Porcentaje, descuentos.xDescrip, usuario.alcantarillado, usuario.Saneamiento, usuario.credito, cuotas.medido, usuario.ubicacion, ultimalectura(" & txtCuentaCliente.Text & ") as ultimalectura, estadotoma.mostrar,estadotoma.descripcion as estado from usuario as usuario inner join comunidades ON (comunidades.Id_comunidad = usuario.id_comunidad) inner join colonia ON (colonia.ID_COLONIA = usuario.ID_COLONIA) inner join cuotas ON (cuotas.id_tarifa = usuario.tarifa) inner join descuentos ON (usuario.idDescuento = descuentos.idDescuento) inner join estadotoma on (usuario.estado=estadotoma.clave)  where cuenta =" & txtCuentaCliente.Text & "  and cuotas.id_tarifa = usuario.tarifa ;")
        Else
            dts = basem.consultasql("select cuenta.usuario.NOMBRE, CONCAT(CALLES.NOMBRE, "" "", usuario.numext,"" "" ,usuario.numint) AS `Direccion`, Comunidad, ubicacion, Colonia, deudafec, descripcion_cuota,  tarifa, idcuotavalvulista, observacion, nodemedidor ,usuario.idDescuento as id_Descuento, descuentos.nPctDsct as Porcentaje, descuentos.xDescrip, usuario.alcantarillado, usuario.Saneamiento, usuario.credito, usuario.ubicacion, cuotas.medido, ultimalectura(" & txtCuentaCliente.Text & ") as ultimalectura, estadotoma.mostrar,estadotoma.descripcion as estado from usuario as usuario inner join comunidades ON (comunidades.Id_comunidad = usuario.id_comunidad) inner join colonia ON (colonia.ID_COLONIA = usuario.ID_COLONIA) inner join cuotas ON (cuotas.id_tarifa = usuario.tarifa) inner join descuentos ON (usuario.idDescuento = descuentos.idDescuento) inner join estadotoma on (usuario.estado=estadotoma.clave) inner join calles on (usuario.id_Calle= calles.id_Calle) where cuenta =" & txtCuentaCliente.Text & "  and cuotas.id_tarifa = usuario.tarifa ;")
        End If
        dts.Read()



        If dts.RecordsAffected = 0 Then
            MessageBox.Show("El usuario no se encuentra dentro del Padron", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
            basem.conexion.Dispose()
            txtCuentaCliente.Clear()
            txtnombre.Clear()
            txtdireccion.Clear()
            txtcolonia.Clear()
            txtcomunidad.Clear()
            txtTipDesc.Clear()
            txtrfc.Clear()
            txttarifa.Clear()
            txtult_pago.Clear()
            txtperiodosadeudados.Clear()
            txtobservaciones.Clear()
            txtCuentaCliente.Focus()
            lblestado.Text = ""
            lblestado.BackColor = Color.White
            Exit Sub
        End If
        Try
            txtnombre.Text = dts("NOMBRE")
        Catch ex As Exception
        End Try
        Try
            txtdireccion.Text = dts("DIRECCION")
        Catch ex As Exception
        End Try
        Try
            txtcomunidad.Text = dts("COMUNIDAD")
        Catch ex As Exception
        End Try
        Try
            txtcolonia.Text = dts("colonia")
        Catch ex As Exception
        End Try
        Try
            txtult_pago.Text = dts("DEUDAFEC")
        Catch ex As Exception
        End Try
        Try
            txttarifa.Text = dts("Descripcion_Cuota")
        Catch ex As Exception
        End Try
        Try
            txtobservaciones.Text = dts("OBSERVACION")
        Catch ex As Exception
        End Try
        Try
            txtUbicacion.Text = dts("ubicacion")
        Catch ex As Exception
        End Try
        Try
            txtTipDesc.Text = dts("xDescrip")
        Catch ex As Exception
        End Try
        Try
            lblMedidor.Text = dts("nodemedidor")
        Catch ex As Exception
        End Try
        Try
            control.Tarifa = dts(My.Settings.campousuariotarifa)
        Catch ex As Exception
        End Try
        Try
            control.Fechainicio = dts("DEUDAFEC")
        Catch ex As Exception
        End Try
        Try
            control.Fechafinal = dtpFechaPagar.Value
        Catch ex As Exception
        End Try
        Try
            control.cuenta = Val(txtCuentaCliente.Text)
        Catch ex As Exception
        End Try
        Try
            control.valvulista = dts("idcuotavalvulista")
        Catch ex As Exception
            control.valvulista = 0
        End Try
        Try
            control.alcantarillado = dts("alcantarillado")
        Catch ex As Exception
        End Try
        Try
            control.saneamiento = dts("Saneamiento")
        Catch ex As Exception
        End Try
        Try
            'lblestado.Text = dts("estado")
            'lblestado.BackColor = Color.Green
        Catch ex As Exception
        End Try
        Try
            control.credito = dts("Credito")
            txtCredito.Text = dts("Credito")
        Catch ex As Exception
        End Try
        Try
            control.EsMEdido = dts("medido")
        Catch ex As Exception
        End Try
        If dts("id_Descuento") > 0 Then
            ''------ Descuentos

            Try
                ' control.descontartodoslosperiodosdeconsumo = True
            Catch ex As Exception
            End Try
            Try
                control.periodoscondescuentodeconsumo = 0
            Catch ex As Exception
            End Try
            Try
                control.descuentoaconsumo = dts("Porcentaje")
            Catch ex As Exception
            End Try
            ''------
        End If
        '------------------------------------------------------descuentos 

        If dts("id_Descuento") = 0 Then
            '------ Descuentos
            Try
                control.descontartodoslosperiodosdeconsumo = False
            Catch ex As Exception
            End Try
            Try
                control.periodoscondescuentodeconsumo = 0
            Catch ex As Exception
            End Try
            Try
                control.descuentoaconsumo = 0
            Catch ex As Exception
            End Try
            ''------
        End If


        If dts("id_Descuento") = 0 And control.descuentoaconsumo > 0 Then
            ' ''------ Descuentos
            'Try
            '    control.descontartodoslosperiodosdeconsumo = False
            'Catch ex As Exception
            'End Try
            Try
                ' control.periodoscondescuentodeconsumo = Month(dtpFechaPagar.Value)
            Catch ex As Exception
            End Try
            Try
                control.descuentoaconsumo = control.descuentoaconsumo
            Catch ex As Exception
            End Try
            ''------
        End If

        '------------------------------------------------- decuento recargo pregrabado

        Try

            'Dim fecha = (Now.Day - 1) * -1
            'MessageBox.Show(fecha)

            'Dim fechaCom = Now.AddDays((Now.Day - 1) * -1)
            'MessageBox.Show(fechaCom)


Line1:      Dim datosdescuento As IDataReader = ConsultaSql("select * from  descuentorecargo where cuenta=" & control.cuenta & " and estado= 'Pendiente' limit 1").ExecuteReader()
            If datosdescuento.Read() Then

                'Dim Dato = datosdescuento("fecha")
                'MessageBox.Show(Dato)

                If datosdescuento("fecha") < Now.AddDays((Now.Day - 1) * -1).ToString("dd-MM-yyyy") Then
                    Ejecucion("update descuentorecargo set estado='Cancelado' where iddescuentorecargo=" & datosdescuento("iddescuentorecargo"))
                    GoTo Line1
                End If


                If datosdescuento("fuepor").ToString.ToUpper() = "DESCUENTO" Then
                    If datosdescuento("descontartodo") = 1 Then
                        control.descontartodoslosperiodosderecargo = True
                        control.descuentoarecargo = datosdescuento("descuentoporc")

                    End If
                    If datosdescuento("numeromeses") > 0 Then
                        control.periodoscondescuentoderecargo = datosdescuento("numeromeses")
                        control.descuentoarecargo = datosdescuento("descuentoporc")
                    End If
                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try



        If dts("mostrar") = 0 Then
            RadGroupBox1.BackColor = Color.Red
            MessageBox.Show("la cuenta tiene un estado que no puede cobrarse")
            limpiar()
            RadGroupBox1.BackColor = Color.White
            basem.conexion.Dispose()
            Exit Sub
        End If
        Try
            llenagrid()
        Catch ex As Exception
        End Try
        If control.EsMEdido = True Then
            Try
                llenagridanticipo()
                'llenagridlecturas()
            Catch ex As Exception
                ' MessageBox.Show(ex.Message)
                control.desgloseAnticipo.Clear()
                control.desgloselecturas.Clear()
            End Try
        End If
        control.periodo = ""
        Try



            control.calcula(permitiradelanto, conConvenio, fechaVenConvenios)

            '----------------------------------- en caso de que fue por cantidad 

            Try
                Dim datosdescuento As IDataReader = ConsultaSql("select * from descuentorecargo where cuenta=" & control.cuenta & " and estado= 'Pendiente' and fuepor='ABONO' limit 1").ExecuteReader()
                If datosdescuento.Read() Then

                    Dim nuevoimporte As Double = control.totaldeudarecargos - datosdescuento("descuentorecargo")

                    Dim conceptoderecargo As New Clsconcepto
                    concepto = control.Listadeconceptos("recargo")
                    concepto.importe = nuevoimporte
                    concepto.Preciounitario = nuevoimporte
                    control.Listadeconceptos.Remove("recargo")
                    If concepto.importe > 0 Then
                        control.Listadeconceptos.Add(concepto)
                    End If



                End If
            Catch ex As Exception

            End Try

        Catch ex As Exception

        End Try


        Dim observalectura As String = ""
        Try
            If control.desgloseAnticipo.Count > 0 Then
                Dim contadorinicial As Integer = 0
                Try
                    contadorinicial = dts("ultimalectura")
                Catch ex As Exception
                End Try
                'observalectura = "EL ANTICIPO CUBRE LAS LECTURAS MINIMO DE " & contadorinicial
                'For i = 1 To control.desgloseAnticipo.Count
                '    Dim x As New ClsRegistrolectura
                '    x = TryCast(control.desgloseAnticipo.Item(i), ClsRegistrolectura)
                '    contadorinicial = contadorinicial + x.Consumocobrado
                'Next
                observalectura += "EL ANTICIPO CUBRE MAXIMO DE " & contadorinicial & "M3 EN EL PERIODO, " & vbCrLf & "CUALQUIER  SOBREPASO TENDRA UN COBRO ADICIONAL" & vbCrLf
                txtobservaciones.Text = observalectura '& txtobservaciones.Text
            End If
        Catch ex As Exception
        End Try
        Try
        Catch ex As Exception
        End Try
        Try
            ' basem.conexion.Dispose()
            Try
                llenagrid()
            Catch ex As Exception
            End Try
            txtperiodosadeudados.Text = control.periodosadeudados
        Catch ex As Exception
            '  MessageBox.Show(ex.Message.ToString())
            '   MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Advertencia...!!!")
        End Try
        basem.conexion.Dispose()
    End Sub
#End Region
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        cargardatos()
    End Sub
    Private Sub btncalcular_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncalcular.Click
        control.totaldeudaiva = 0
        If chkcobrarconsumo.Checked = True Then
            cargardatos(True)
            If control.EsMEdido Then
                llenagridanticipo()
                'llenagridlecturas()
                calculotempAdelantado()
            End If
        Else
            quitarconceptos()
            control.desgloseconsumo.Clear()
            control.desglosealcantarillado.Clear()
            control.desgloserezago.Clear()
            control.desglosesaneamiento.Clear()
            control.desgloserecargo.Clear()
        End If
        btnpagar.Focus()
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Text = "Comisión de agua potable                                                                                                                                " & "   " & Today & "   " & TimeOfDay
    End Sub
    Private Sub btnsalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsalir.Click
        Dim ask As MsgBoxResult
        ask = MsgBox("Realmente desea salir del sistema?", MsgBoxStyle.OkCancel, "Saliendo...!")
        If ask = MsgBoxResult.Ok Then
            Me.Hide()
        Else
            If ask = MsgBoxResult.Cancel Then
            End If
        End If
    End Sub
    'Private Sub chkcobrarconsumo_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkcobrarconsumo.ToggleStateChanged
    '    'If chkcobrarconsumo.Checked = True Then
    '    cargardatos()
    '    ' Else
    '    ' quitarconceptos()
    '    ' End If
    'End Sub
    Public Sub convenio()
        Try
            Dim cuentaCliente As String = txtCuentaCliente.Text
            Dim ncon As Integer
            Dim Pagares As ConveniosPagos = New ConveniosPagos
            Dim convenio As String = "Select nombre, convenio from usuario where cuenta=" & cuentaCliente
            Dim nombre As String
            Dim dra As IDataReader = ConsultaSql(convenio).ExecuteReader()
            dra.Read()
            If Not IsDBNull(dra("convenio")) Then
                ncon = dra("convenio")
            Else
                ncon = Nothing
            End If
            nombre = dra("nombre")
            If ncon = 1 Then
                conConvenio = True
                Pagares.DatosCon(cuentaCliente)
                'Dim h As New ConveniosPagos
                'h.cuenta = Val(txtCuentaCliente.Text)
                Pagares.Text = "Detalle de Convenios " & txtCuentaCliente.Text & " " & nombre
                Pagares.dataConv.Refresh()
                Pagares.ShowDialog()
                fechaVenConvenios = Pagares.DateTimePicker1.Value
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub RadButton10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbuscacliente.Click
        'convenio()
        Dim basem As New base
        '  basem.MATACONECCIONES(1000)
        '  txtCuentaCliente.Clear()

        txtnombre.Clear()
        txtdireccion.Clear()
        txtcolonia.Clear()
        txtcomunidad.Clear()
        txtTipDesc.Clear()
        txtrfc.Clear()
        txttarifa.Clear()
        txtult_pago.Clear()
        txtperiodosadeudados.Clear()
        txtobservaciones.Clear()
        If txtCuentaCliente.Text = "" Then
            MessageBox.Show("Debes ingresar el número de cuenta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
            basem.conexion.Dispose()
            'txtCuentaCliente.Clear()
            'txtnombre.Clear()
            'txtdireccion.Clear()
            'txtcolonia.Clear()
            'txtcomunidad.Clear()
            'txtTipDesc.Clear()
            'txtrfc.Clear()
            'txttarifa.Clear()
            'txtult_pago.Clear()
            'txtperiodosadeudados.Clear()
            'txtobservaciones.Clear()
            txtCuentaCliente.Focus()
            Exit Sub
        End If
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '''
        If usuario = 1 Or usuario = 2 Then


            Dim cadena As String
            Dim estado As Integer
            cadena = "Select total from usuario where cuenta =" & txtCuentaCliente.Text
            Dim dr1 As IDataReader = ConsultaSql(cadena).ExecuteReader()
            dr1.Read()
            estado = dr1("total")

            Dim total As Decimal

            total = Convert.ToDecimal(estado)

            'If total > 1000 Then

            '    MessageBox.Show("ADEUDO")
            '    'MessageBox.Equals()
            '    'RadGroupBox1.BackColor = Color.White

            'End If

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            Try
                limpiacontroldedescuento()
                control.Listadeconceptos.Clear()
                'chkcobrarconsumo.Checked = True
                dtpFechaPagar.Value = DateAdd(DateInterval.Month, -1, Now)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If

        If usuario = 1 Then
            Try



                chkcobrarconsumo.Checked = True
                cargardatos()
                llenabitacora()
                If control.EsMEdido Then
                    llenagridanticipo()
                    'llenagridlecturas()
                    calculotempAdelantado()
                End If
                dtpFechaPagar.Focus()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try



        ElseIf usuario = 2 Then


            chkcobrarconsumo.Checked = False
            cargadatosnousuario()
            'control.Listadeconceptos.Clear()
            llenagrid()


        ElseIf usuario = 3 Then


            chkcobrarconsumo.Checked = False
            'control.Listadeconceptos.Clear()
            control.desgloseconsumo.Clear()
            control.desgloserezago.Clear()
            control.desglosealcantarillado.Clear()
            control.desglosesaneamiento.Clear()
            control.desgloserecargo.Clear()

            cargasolicitud()
            llenagrid()


        End If
        '    Else
        '        MsgBox("Cuenta congelada")
        '        Try
        '            dtgConceptos.Rows.Clear()
        '            dtgConceptos.DataSource = Nothing
        '        Catch ex As Exception
        '        End Try
        '    End If
        'Catch ex As Exception
        'End Try
        basem.conexion.Dispose()
    End Sub
    Public Sub quitarconceptos()
        Try
            control.Listadeconceptos.Remove("Consumo")
        Catch ex As Exception
        End Try
        Try
            control.Listadeconceptos.Remove("Alcantarillado")
        Catch ex As Exception
        End Try
        Try
            control.Listadeconceptos.Remove("Saneamiento")
        Catch ex As Exception
        End Try
        Try
            control.Listadeconceptos.Remove("Recargo")
        Catch ex As Exception
        End Try
        Try
            control.Listadeconceptos.Remove("Rezago")
        Catch ex As Exception
        End Try
        Try
            control.Listadeconceptos.Remove("PAGO VALVULISTA")
        Catch ex As Exception
        End Try
        llenagrid()
    End Sub
    Private Sub btnagrcon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnagrcon.Click
        Try
            Dim x As New base
            Dim dato As OdbcDataReader
            dato = x.consultasql("Select * from CONCEPTOSCXC where ID_CONCEPTO='" & ddlconsumo.SelectedValue & "'")
            If dato.Read Then
                Dim concepto As New Clsconcepto
                concepto.Clave = dato("ID_CONCEPTO")
                concepto.Cantidad = 1
                concepto.importe = dato("monto_INICIAL")
                concepto.Concepto = dato("Descripcion")
                concepto.unidadsat = dato("unidadsat")
                concepto.clavesat = dato("clavesat")
                If CBool(dato("APLICAIVA")) Then
                    concepto.IVA = concepto.importe * (variable_iva / 100)
                Else
                    concepto.IVA = 0
                End If
                concepto.Preciounitario = concepto.importe
                concepto.Clave = ddlconsumo.SelectedValue
                control.Listadeconceptos.Add(concepto, "Renglon" & concepto.Clave)
            End If
            llenagrid()
        Catch ex As Exception
        End Try
    End Sub

    Public Sub llenagrid()
        Total = 0
        SubTotal = 0
        IVA = 0
        Try
            Dim concepto As New Clsconcepto
            dtgConceptos.Rows.Clear()
            For i = 1 To control.Listadeconceptos.Count
                concepto = control.Listadeconceptos.Item(i)
                dtgConceptos.Rows.Add(concepto.Cantidad, concepto.Concepto, concepto.Preciounitario, concepto.importe, "", "", concepto.Clave, concepto.CLAVEMOV)
                SubTotal = SubTotal + concepto.importe
                IVA = IVA + Math.Round(concepto.IVA, 2)
            Next
        Catch ex As Exception
        End Try



        Total = IVA + SubTotal
        lblSubtotal.Text = SubTotal.ToString("0.00")
        lblIVA.Text = IVA.ToString("0.00")
        lblTotal.Text = Total.ToString("0.00")
    End Sub



    Private Sub dtgConceptos_CellClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dtgConceptos.CellClick
        If (e.ColumnIndex = 4) Then  ' columna condonar
            Select Case Mid(dtgConceptos.CurrentRow.Cells(1).Value, 1, 7)
                Case "CONSUMO", "REZAGO "
                    MessageBox.Show("No puedes borrar el consumo")
                Case "ALCANTA"
                    control.Listadeconceptos.Remove("Alcantarillado")
                Case "SANEAMI"
                    control.Listadeconceptos.Remove("Saneamiento")
                Case "RECARGO"
                    control.Listadeconceptos.Remove("Recargo")
                Case "PAGO VA"
                    control.Listadeconceptos.Remove("PAGO VALVULISTA")
                Case Else
                    If dtgConceptos.CurrentRow.Cells(7).Value > 0 Then
                        Dim nombre As String = "Renglon" & dtgConceptos.CurrentRow.Cells(7).Value
                        control.Listadeconceptos.Remove(nombre)
                    Else
                        Dim nombre As String = "Renglon" & dtgConceptos.CurrentRow.Cells(6).Value
                        control.Listadeconceptos.Remove(nombre)
                    End If
            End Select
            control.rectificaiva()
        End If
        If (e.ColumnIndex = 5) Then ' columna desglozar
            Dim vista As New frmdesglose
            vista.control = control
            vista.Text = "Desglose de la cuenta " & txtCuentaCliente.Text & " " & txtnombre.Text
            Select Case Mid(dtgConceptos.CurrentRow.Cells(1).Value, 1, 7)

                Case "CONSUMO"
                    vista.deque = "CONSUMO"
                    vista.ShowDialog()
                Case "ALCANTA"
                    vista.deque = "ALCANTARILLADO"
                    vista.ShowDialog()
                Case "SANEAMI"
                    vista.deque = "SANEAMIENTO"
                    vista.ShowDialog()
                Case "RECARGO"
                    vista.deque = "RECARGO"
                    vista.ShowDialog()
                Case "REZAGO "
                    vista.deque = "REZAGO"
                    vista.ShowDialog()
                Case Else

            End Select
        End If
        If (e.ColumnIndex = 8) Then ' columna abono
            If My.Settings.tipocaja.ToUpper() = "CONSULTA" Then

                Select Case Mid(dtgConceptos.CurrentRow.Cells(1).Value, 1, 7).ToUpper

                    Case "CONSUMO", "ALCANTA", "SANEAMI", "REZAGO "
                        MessageBox.Show("No puedes modificar las cantidades de estos conceptos, utiliza la fecha o descuentos")
                    Case Else
                        Dim abono As New Frmabono
                        abono.control = control
                        abono.numerodeconcepto = e.RowIndex + 1
                        abono.ShowDialog()
                        control = abono.control
                        llenagrid()
                End Select
            End If
            If My.Settings.tipocaja.ToUpper() = "COBRO" Then

                Select Case Mid(dtgConceptos.CurrentRow.Cells(1).Value, 1, 7).ToUpper

                    Case "CONSUMO", "ALCANTA", "SANEAMI", "REZAGO "
                        MessageBox.Show("No puedes modificar las cantidades de estos conceptos, utiliza la fecha o descuentos")
                    Case Else
                        Dim abono As New Frmabono
                        abono.control = control
                        abono.numerodeconcepto = e.RowIndex + 1
                        abono.ShowDialog()
                        control = abono.control
                        llenagrid()
                End Select
            End If
        End If

        llenagrid()
    End Sub


    Public Sub llenagridanticipo()
        Try
            Dim concepto As New ClsRegistrolectura
            Try
                dtganticipo.Rows.Clear()

                dtganticipo.Refresh()
            Catch ex As Exception

            End Try

            dtganticipo.Refresh()

            For i = 1 To control.desgloseAnticipo.Count
                concepto = control.desgloseAnticipo.Item(i)
                Try
                    dtganticipo.Rows.Add(concepto.Mes, concepto.Periodo, concepto.Lectura_Anterior, concepto.Lectura_Actual, concepto.Consumocobrado, concepto.Totalcondescuento)
                Catch ex As Exception

                End Try


            Next


            For i = 1 To control.desgloseAnticipo.Count
                Dim nodo As New DevComponents.AdvTree.Node
                Dim registro As ClsRegistrolectura
                registro = control.desgloseAnticipo(i)
                nodo.Cells(0).Text = registro.Mes
                Dim celda As New DevComponents.AdvTree.Cell
                celda.Text = registro.Periodo
                nodo.Cells.Add(celda)
                'cmbtperiodos.AdvTree.Nodes.Add(nodo)
            Next
        Catch ex As Exception

        End Try



    End Sub

    'Public Sub llenagridlecturas()
    '    Try
    '        Try
    '            dtglecturasgeneradas.Rows.Clear()
    '        Catch ex As Exception

    '        End Try


    '        For i = 1 To control.desgloselecturas.Count
    '            concepto = control.desgloselecturas.Item(i)
    '            Try
    '                dtglecturasgeneradas.Rows.Add(concepto.Mes, concepto.Periodo, concepto.Lectura_Anterior, concepto.Lectura_Actual, concepto.Consumocobrado, concepto.Totalcondescuento)
    '            Catch ex As Exception

    '            End Try


    '        Next
    '    Catch ex As Exception

    '    End Try
    '    'Dim concepto As New ClsRegistrolectura


    'End Sub

    Public Sub calculotempAdelantado()
        Dim x As base = New base

        Dim totalConsu As Double = 0
        Dim totalAlcan As Double = 0
        Dim totalSanea As Double = 0
        Dim totalAnticipo As Double = 0


        For i = 0 To control.desgloseAnticipo.Count - 1
            concepto = control.desgloseAnticipo.Item(i + 1)

            dts = x.consultasql("select cuenta, mes, an_per from lecturas where cuenta='" + txtCuentaCliente.Text + "' and mes='" + concepto.Mes + "' and an_per='" + concepto.Periodo + "'")
            If dts.Read = True Then
                MessageBox.Show("La Lectura del Periodo: " & concepto.Mes & "-" & concepto.Periodo & " ya Existe")
            Else

                Dim montolectura As Double = 0

                ' Dim consumo As Double = x.obtenerCampo("SELECT ConsumoMedidosSin(" & concepto.Consumo & ",'" & control.Tarifa & "') as Consumo  ", "Consumo")
                Dim consumo As Double = concepto.Totalcondescuento
                Dim alcantarillado As Double = 0
                For cicloalcanta = 1 To control.desglosealcantarillado.Count
                    Dim conceptoalcan As ClsRegistrolectura
                    conceptoalcan = control.desglosealcantarillado(cicloalcanta)
                    If concepto.Mes = conceptoalcan.Mes And concepto.Periodo = conceptoalcan.Periodo Then
                        alcantarillado = conceptoalcan.Totalcondescuento
                    End If
                Next


                Dim saneamiento As Double = 0
                For ciclosanea = 1 To control.desglosesaneamiento.Count
                    Dim conceptoalcan As ClsRegistrolectura
                    conceptoalcan = control.desglosesaneamiento(ciclosanea)
                    If concepto.Mes = conceptoalcan.Mes And concepto.Periodo = conceptoalcan.Periodo Then
                        saneamiento = conceptoalcan.Totalcondescuento
                    End If
                Next

                'Dim alcantarillado As Double =  x.obtenerCampo("SELECT CalcAlca(" & concepto.Consumo & ",'" & control.Tarifa & "') as Alcan  ", "Alcan")
                'Dim saneamiento As Double = 0 ' x.obtenerCampo("SELECT Calcsanea(" & concepto.Consumo & ",'" & control.Tarifa & "') as Sanea  ", "Sanea")



                montolectura = consumo + alcantarillado + saneamiento

                totalConsu = totalConsu + consumo
                totalAlcan = totalAlcan + alcantarillado
                totalSanea = totalSanea + saneamiento

            End If
        Next


        totalAnticipo = totalConsu + totalAlcan + totalSanea

        txtAConsumo.Text = totalConsu
        txtAAlcantarillado.Text = totalAlcan
        txtASaneamiento.Text = totalSanea
        txtTotalAnticipos.Text = totalAnticipo

        x.conexion.Dispose()
    End Sub





    Private Sub btnpagar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpagar.Click
        If txtCuentaCliente.Text = "" Then
            MessageBox.Show("No has seleccionado una cuenta")
            Exit Sub
        End If
        If control.Listadeconceptos.Count = 0 Then
            MessageBox.Show("No hay nada que pagar")
            Exit Sub
        End If
        crearecibo()
        Try
            Dim frmpa As New frmPagoefectivo()

            If chkcobrarconsumo.Checked = True Then
                frmpa.actualizarfecha = True 'actualiza la fecha de pago al usuario cuando paga consumo
            Else
                frmpa.actualizarfecha = False
            End If
            recibo.subtotal = SubTotal
            recibo.descuento = control.descuentoaconsumo
            recibo.observacion = txtobservaciones.Text
            recibo.Totaldescuentoenpesos = Math.Round(control.totaldescuentopesos, 2)
            frmpa.recibo = recibo
            frmpa.control = control
            frmpa.totalapagar = (Total - Val(txtCredito.Text))
            frmpa.lbltotal_pagar.Text = Total.ToString("C")
            frmpa.vale = Val(txtCredito.Text)
            frmpa.total = Total
            frmpa.ShowDialog()
            recibo.ccodpago = frmpa.lblFormadepago.Text
            ''''
            lblfolio.Text = My.Settings.folio

        Catch ex As Exception

        End Try


        'Try
        '    dtganticipo.DataSource = ""
        '    dtglecturasgeneradas.DataSource = ""
        'Catch ex As Exception

        'End Try'

        Try
            control.desgloseAnticipo.Clear()
            control.desgloselecturas.Clear()
        Catch ex As Exception

        End Try




        usuario = 1
        txtCuentaCliente.Focus()
    End Sub

    Private Sub crearecibo()
        Try
            recibo.descuento = 0
            recibo.Totaldescuentoenpesos = 0
            Dim concepto As New Clsconcepto

            Try
                recibo.Fecha_Act = Now.ToString("yyyy-MM-dd") ''''dtFecha1.tostring("yyyy-mm-dd"),
            Catch ex As Exception

            End Try
            Try
                recibo.periodo = control.periodo

            Catch ex As Exception
                recibo.periodo = ""
            End Try
            Try
                recibo.descuento = control.descuentoaconsumo
            Catch ex As Exception
                recibo.descuento = 0
            End Try

            Try
                recibo.Totaldescuentoenpesos = control.totaldescuentopesos
            Catch ex As Exception
                recibo.Totaldescuentoenpesos = 0
            End Try

            Try
                recibo.fecha_deuda = Date.Parse(txtult_pago.Text).ToString("yyyy-MM-dd")
            Catch ex As Exception
                recibo.fecha_deuda = Now.ToString("yyyy-MM-dd")
            End Try
            'FormatDateTime(txtult_pago.SelectedText, "yyyy-MM-dd") '''' txt5.Text += DateTime.Now.ToString("dd/MM/yyyy");FormatDateTime('yyyy-mm-dd',cfec);
            Try
                recibo.fechafin = dtpFechaPagar.Value.ToString("yyyy-MM-dd")
            Catch ex As Exception
                recibo.fechafin = Now.ToString("yyyy-MM-dd")
            End Try

            recibo.iva = control.totaldeudaiva
            recibo.total = lblTotal.Text
            recibo.nombre = txtnombre.Text
            recibo.cancelado = "A"
            recibo.cuenta = txtCuentaCliente.Text
            recibo.comunidad = txtcomunidad.Text
            recibo.colonia = txtcolonia.Text
            Dim longi As Int16 = usuariodelsistema.Length
            If longi >= 18 Then
                longi = 18
            Else
                longi = usuariodelsistema.Length
            End If
            recibo.usuarios = usuariodelsistema.Substring(0, longi)
            recibo.observacion = txtobservaciones.Text
            Try
                recibo.medidor = lblMedidor.Text
            Catch ex As Exception
                recibo.medidor = 0
            End Try

            Try
                recibo.ubicacion = dts("Ubicacion")
            Catch ex As Exception
                recibo.ubicacion = ""
            End Try
            Try
                If usuario = 1 Then
                    recibo.tarifa = obtenerCampo("select tarifa from usuario where cuenta=" + txtCuentaCliente.Text, "tarifa")
                End If
            Catch ex As Exception
                recibo.tarifa = 0
            End Try

            recibo.esusuario = usuario
            recibo.subtotal = SubTotal
            recibo.iva = IVA

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub bynbuscaruserpadron_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bynbuscaruserpadron.Click
        usuario = 1
        Dim x As New fmrBuscaruserxpadron
        x.ShowDialog()
    End Sub



    Private Sub btnhistorial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnhistorial.Click
        If usuario = 1 Then
            Dim h As New FrmHistorial
            h.cuenta = Val(txtCuentaCliente.Text)
            h.tipoUsuario = usuario
            h.Text = "Historial del usuario " & txtCuentaCliente.Text & " " & txtnombre.Text
            h.ShowDialog()
        Else
        End If
    End Sub


    Public Sub cargadatosnousuario()
        Try
            Dim basem As New base
            Dim usu2 As OdbcDataReader
            usu2 = basem.consultasql("select * from nousuarios where clave =" & txtCuentaCliente.Text)
            usu2.Read()
            txtnombre.Text = usu2("NOMBRE")
            txtdireccion.Text = usu2("DIRECCION")
            txtcomunidad.Text = usu2("COMUNIDAD")
            txtrfc.Text = usu2("rfc")
            txtcolonia.Text = usu2("colonia")
            txttarifa.Text = ""
            txtobservaciones.Clear()
            txtult_pago.Text = Date.Now.ToString("dd/mm/yyyy")
            txtperiodosadeudados.Text = ""
            dtgConceptos.Rows.Clear()
            control.Listadeconceptos.Clear()
            '  dtgConceptos.AllowAddNewRow = True

            Dim x As New base
            Dim dato As OdbcDataReader
            dato = x.consultasql("select * from conceptosnousuario where nousuario=" & txtCuentaCliente.Text)

            While dato.Read()
                Dim concepto As New Clsconcepto
                concepto.Cantidad = 1
                concepto.importe = dato("Subtotal")
                concepto.Concepto = dato("Concepto")
                concepto.Preciounitario = concepto.importe
                If dato("iva") > 0 Then
                    concepto.llevaiva = 1
                Else
                    concepto.llevaiva = 0
                End If
                concepto.IVA = Math.Round(concepto.importe * (variable_iva / 100) * concepto.llevaiva, 2)
                concepto.Clave = dato("id_concepto")



                control.Listadeconceptos.Add(concepto, "Renglon" & concepto.Clave)
                control.totaldeudaiva += Math.Round(concepto.IVA * concepto.llevaiva, 2)
            End While

            llenagrid()

        Catch ex As Exception
            ''''MsgBox("Dato no existente")
        End Try
    End Sub

    Public Sub cargasolicitud()
        Try
            Dim basem As New base
            Dim usu2 As OdbcDataReader

            usu2 = basem.consultasql("SELECT S.NUMERO, S.NOMBRE, CONCAT(CO.NOMBRE,"" "",S.NUMEXT,"" "",S.NUMINT) AS DOMICILIO, C.COLONIA, COM.COMUNIDAD, S.rfc FROM (SOLICITUD S INNER JOIN COLONIA C ON (C.ID_COLONIA = S.ID_COLONIA) INNER JOIN COMUNIDADES COM ON (COM.Id_comunidad = S.Id_comunidad)) inner join calles CO on S.id_Calle=CO.id_Calle  where numero =" & txtCuentaCliente.Text)
            usu2.Read()
            txtnombre.Text = usu2("NOMBRE")
            txtdireccion.Text = usu2("Domicilio")
            txtcomunidad.Text = usu2("COMUNIDAD")
            txtrfc.Text = usu2("rfc")
            txtcolonia.Text = usu2("colonia")
            txttarifa.Text = ""
            txtobservaciones.Clear()
            txtult_pago.Text = Date.Now.ToString("dd/mm/yyyy")
            txtperiodosadeudados.Text = ""
            dtgConceptos.Rows.Clear()
            control.Listadeconceptos.Clear()
            '  dtgConceptos.AllowAddNewRow = True

            Dim x As New base
            Dim dato As OdbcDataReader
            dato = x.consultasql("select * from conceptoscxc where id_concepto= 'ELPR'")
            If dato.Read Then
                Dim concepto As New Clsconcepto
                concepto.Cantidad = 1
                concepto.importe = dato("Monto_inicial")
                concepto.Concepto = dato("Descripcion")
                concepto.Preciounitario = concepto.importe
                concepto.IVA = concepto.importe * (variable_iva / 100)
                concepto.Clave = dato("id_concepto")
                control.Listadeconceptos.Add(concepto, "Renglon" & concepto.Clave)
            End If
            ' llenagrid()

        Catch ex As Exception
            ''''MsgBox("Dato no existente")
        End Try
    End Sub


    Private Sub lblTotal_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblTotal.TextChanged
        If lblTotal.Text = "0.0" Then
            lbltotalconletras.Text = ""
            Exit Sub
        End If
        Dim valor As Double = Val(lblTotal.Text)
        lbltotalconletras.Text = ConvertCurrencyToSpanish(valor, "pesos")
    End Sub

    Private Sub lblTotal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblTotal.Click

    End Sub

    Private Sub btnfactura_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnfactura.Click
        btnfactura.Enabled = False
        If txtCuentaCliente.Text = "" Then
            MessageBox.Show("No has seleccionado una cuenta")
            btnfactura.Enabled = True
            Exit Sub
        End If
        If control.Listadeconceptos.Count = 0 Then
            MessageBox.Show("No hay nada que pagar")
            btnfactura.Enabled = True
            Exit Sub
        End If


        crearecibo()
        Dim FACTURA As New Frmvalidafactura
        If usuario = 1 Then
            FACTURA.TIPO = "USUARIO"
        ElseIf usuario = 2 Then
            FACTURA.TIPO = "NO USUARIO"
        ElseIf usuario = 3 Then
            FACTURA.TIPO = "SOLICITUD"
        End If
        FACTURA.recibo = recibo
        '  frmPagoefectivo.control = control


        FACTURA.control = control
        FACTURA.cuenta = Val(txtCuentaCliente.Text)
        FACTURA.subtotal = Math.Round(recibo.subtotal, 2)
        FACTURA.iva = Math.Round(recibo.iva, 2)
        FACTURA.total = Math.Round(recibo.total, 2)
        FACTURA.ShowDialog()

        If My.Settings.ProvedorFacturacion = "FACTUPRONTO" Then

            Dim frmpa As New frmPagoefectivo


            If chkcobrarconsumo.Checked = True Then
                frmpa.actualizarfecha = True 'actualiza la fecha de pago al usuario cuando paga consumo
            Else
                frmpa.actualizarfecha = False
            End If

            frmpa.recibo = recibo
            frmpa.control = control
            frmpa.lblFormadepago.Text = FACTURA.formafacturado
            frmpa.FORMAFACTURADO = FACTURA.formafacturado
            'Dim x As New base
            frmpa.lbltotal_pagar.Text = lblTotal.Text
            frmpa.FACTURADO = FACTURA.facturado

            frmpa.Label2.Visible = False
            frmpa.ShowDialog()

            'recibo.ccodpago = frmpa.lblFormadepago.Text
            ''''
        End If

        lblfolio.Text = My.Settings.folio
        btnfactura.Enabled = True
    End Sub

    Private Sub btndescuento_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndescuento.Click
        Dim r As New FrmDescuentos
        r.controldescu = control
        r.chkconsumo.Checked = control.descontartodoslosperiodosdeconsumo
        r.chkalcantarillado.Checked = control.descontartodoslosperiodosdealcantarillado
        r.chksaneamiento.Checked = control.descontartodoslosperiodosdesaneamiento
        r.chkrecargo.Checked = control.descontartodoslosperiodosderecargo
        r.diporcconsumo.Value = control.descuentoaconsumo
        r.diporcalcantarillado.Value = control.descuentoaalcantarillado
        r.diporcsanemiento.Value = control.descuentoasaneamiento
        r.diporcrecargo.Value = control.descuentoarecargo

        r.IImesesdecconsumo.Value = control.periodoscondescuentodeconsumo
        r.IImesesdescalcanta.Value = control.periodoscondescuentodealcantarillado
        r.IImesesdescsaneamiento.Value = control.periodoscondescuentodesaneamiento
        r.IImesesderecargo.Value = control.periodoscondescuentoderecargo

        r.ShowDialog()
        control = r.controldescu



    End Sub

    Public Sub limpiacontroldedescuento()
        control.descontartodoslosperiodosderecargo = False
        control.descontartodoslosperiodosdeconsumo = False
        control.descontartodoslosperiodosdesaneamiento = False
        control.descontartodoslosperiodosdealcantarillado = False
        control.descuentoaalcantarillado = 0
        control.descuentoaconsumo = 0
        control.descuentoarecargo = 0
        control.descuentoasaneamiento = 0
        control.periodoscondescuentodealcantarillado = 0
        control.periodoscondescuentodeconsumo = 0
        control.periodoscondescuentodesaneamiento = 0
        control.periodoscondescuentoderecargo = 0
    End Sub

    Public Sub limpiar()
        txtcolonia.Text = ""
        txtcomunidad.Text = ""
        txtCuentaCliente.Text = ""
        txtdireccion.Text = ""
        txtnombre.Text = ""
        txtobservaciones.Text = ""
        txtrfc.Text = ""
        txttarifa.Text = ""
        txtult_pago.Text = ""

        dtgConceptos.Rows.Clear()

        lblDescuento.Text = "0.0"
        lblIVA.Text = "0.0"
        lblSubtotal.Text = "0.0"
        lblTotal.Text = "0.0"

        SubTotal = 0
        IVA = 0
        Total = 0

        txtCuentaCliente.Focus()
    End Sub

    Private Sub CambiarFolioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CambiarFolioToolStripMenuItem.Click
        Dim xf As New frmfolio
        xf.ShowDialog()
        lblfolio.Text = My.Settings.folio
    End Sub


    Private Sub configcaja_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles configcaja.Click
        Dim configc As New FRmconfigcaja
        configc.ShowDialog()
    End Sub

    Private Sub dtpFechaPagar_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtpFechaPagar.KeyPress
        If e.KeyChar = Chr(13) Then
            btncalcular.Focus()
        End If
    End Sub

    Private Sub dtpFechaPagar_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpFechaPagar.LostFocus
        btncalcular.Focus()
    End Sub

    Private Sub FacturaPublicoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FacturaPublicoToolStripMenuItem.Click
        Dim VFP As New Frmvalidafacturapublico
        VFP.ShowDialog()
    End Sub

    Private Sub ReimprimirFolioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReimprimirFolioToolStripMenuItem.Click
        Dim x As New Frmaimprimir
        x.Show()

    End Sub
    Private Sub btnBorrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBorrar.Click
        txtobservaciones.Clear()
    End Sub

    Private Sub btnSolicitud_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSolicitud.Click
        usuario = 3
        Dim frSol As New FrmBuscaSolicitud
        frSol.ShowDialog()
    End Sub

    Private Sub ListadoDeRecibosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListadoDeRecibosToolStripMenuItem.Click
        Dim x As New frmListadoRecibos
        x.ShowDialog()
    End Sub
    Private Sub btncancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancelar.Click
        Dim ar As New Frmarqueo
        ar.Show()
    End Sub

    Private Sub ListadoDeFacturasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListadoDeFacturasToolStripMenuItem.Click
        Dim x As New Frmlistadofacturas
        x.ShowDialog()
    End Sub

    Private Sub btncotización_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncotización.Click


        If txtCuentaCliente.Text = "" Then
            Exit Sub
        End If
        Dim dts As DataSet = New DataSet()
        Dim dtsub As DataSet = New DataSet()
        Dim usuario As New DatosRecibo.UsuarioDataTable
        Dim usuariotableadapter As New DatosReciboTableAdapters.UsuarioTableAdapter
        usuariotableadapter.Fillbycuenta(usuario, Val(txtCuentaCliente.Text))

        dts.Tables.Add(usuario)
        Dim conceptos As New DataTable

        Try
            conceptos = ConvertToDataTable(control.Listadeconceptos)
            conceptos.TableName = "Conceptos"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        dts.Tables.Add(conceptos)


        'Dim reporte As New ReportDocument()



        ''Reemplazar por un reporte impreso en ItextSharp


        'Try
        '    reporte.Load(AppPath() & ".\Reportes\cotizacion.rpt")

        '    'Dim servidorreporte As String = My.Settings.servidorreporte
        '    'Dim usuarioreporte As String = My.Settings.usuarioreporte
        '    'Dim passreporte As String = My.Settings.passreporte
        '    'Dim basereporte As String = My.Settings.basereporte

        '    'reporte.DataSourceConnections.Item(1).SetConnection(servidorreporte, basereporte, False)
        '    'reporte.DataSourceConnections.Item(1).SetLogon(usuarioreporte, passreporte)

        '    reporte.SetDataSource(dts)



        '    Dim basecot As New base

        '    reporte.SetParameterValue("total", lblTotal.Text)
        '    reporte.SetParameterValue("iva", lblIVA.Text)
        '    reporte.SetParameterValue("subtotal", lblSubtotal.Text)
        '    reporte.SetParameterValue("totalconletras", lbltotalconletras.Text)

        Dim basecot As New base

        Dim promedio As Integer = 0
        Dim ultimalectura As Integer = 0


        rs = basecot.consultasql("select ultimalectura(" & control.cuenta & ") as ultimalectura, promedio(" & control.cuenta & ",3) as promedio")


        Try
            rs.Read()
            Try
                ultimalectura = rs("ultimalectura")
            Catch ex As Exception

            End Try
            Try
                promedio = rs("promedio")
            Catch ex As Exception

            End Try
            'reporte.SetParameterValue("ultimalectura", ultimalectura)
            'reporte.SetParameterValue("promedio", promedio)


        Catch excoti1 As Exception
            MessageBox.Show(excoti1.Message)
        End Try


        basecot.conexion.Dispose()
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try


        'Dim x As New frmReporte()
        'x.CrystalReportViewer3.ReportSource = reporte

        'x.Show()



        'Invocar Método CotizaciónPDF

        Dim CNOMBRE As String = obtenerCampo("select * from empresa limit 1 ", "CNOMBRE")



        'Crear la carpeta deonde se almacenara el PDF


        Dim dirCotizacion = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\Cotizaciones\" & Now.Year & "\"
        If Not Directory.Exists(dirCotizacion) Then
            Directory.CreateDirectory(dirCotizacion)
        End If


        Dim cadenafolder As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\Cotizaciones\" & Now.Year & "\"

        'Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)


        Dim pdfDoc As New Document(iTextSharp.text.PageSize.LETTER, 15.0F, 15.0F, 30.0F, 30.0F)


        'Dim pdfWrite As PdfWriter = PdfWriter.GetInstance(pdfDoc, New System.IO.FileStream(cadenafolder & "\factura" & serie & factura & ".pdf", FileMode.Create))
        Dim pdfWrite As PdfWriter = PdfWriter.GetInstance(pdfDoc, New System.IO.FileStream(dirCotizacion & "Cotizacion_" & txtnombre.Text & ".pdf", FileMode.Create))
        'Formtos para distintos tamaños de letras

        'Formato Letras


        Dim Font As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 6, iTextSharp.text.Font.NORMAL))
        Dim FontWhi As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.WHITE))
        Dim FontB8 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))
        Dim Font8N As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.BOLD))
        Dim Font88 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 15, iTextSharp.text.Font.BOLD))
        Dim Font12 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 12, iTextSharp.text.Font.BOLD))
        Dim Font9 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 9, iTextSharp.text.Font.NORMAL))
        Dim Fontp As New Font(FontFactory.GetFont(FontFactory.COURIER, 7, iTextSharp.text.Font.BOLD))
        Dim CVacio As PdfPCell = New PdfPCell(New Phrase(""))
        CVacio.Border = 0


        'abrimos el pdf para comenzar a escribir en el, al terminar cerramos
        pdfDoc.Open()

        'Actopan

        'Tabla Vacia
        Dim TableVacia As PdfPTable = New PdfPTable(1)
        TableVacia.WidthPercentage = 100
        Dim widthsvacio As Single() = New Single() {1000.0F}
        TableVacia.SetWidths(widthsvacio)

        Dim Colvacio = New PdfPCell(New Phrase(" ", Font8N))
        Colvacio.Border = 0
        Colvacio.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        TableVacia.AddCell(Colvacio)



        'Crear la tabla de Encabezado

        Dim TableEncabezado As PdfPTable = New PdfPTable(2)
        TableEncabezado.DefaultCell.Border = BorderStyle.None
        Dim Col1 As PdfPCell
        'Dim ILine As Integer
        'Dim iFila As Integer
        TableEncabezado.WidthPercentage = 100

        Dim widths As Single() = New Single() {100.0F, 600.0F}
        TableEncabezado.SetWidths(widths)

        'Encabezado

        Dim imagenBMP As iTextSharp.text.Image
        imagenBMP = iTextSharp.text.Image.GetInstance(LOGOBYTE)
        imagenBMP.ScaleToFit(80.0F, 70.0F)
        imagenBMP.Border = 0
        TableEncabezado.AddCell(imagenBMP)



        Dim TableDireccion As PdfPTable = New PdfPTable(1)
        TableDireccion.WidthPercentage = 100
        Dim widthsTDir As Single() = New Single() {1000.0F}
        TableDireccion.SetWidths(widthsTDir)

        Dim ColInfoEnc = New PdfPCell(New Phrase(CNOMBRE, Font8N))
        ColInfoEnc.Border = 0
        ColInfoEnc.HorizontalAlignment = PdfPCell.ALIGN_CENTER

        Colvacio = New PdfPCell(New Phrase(" ", Font12))
        Colvacio.Border = 0
        Colvacio.HorizontalAlignment = PdfPCell.ALIGN_CENTER

        Dim ColCot = New PdfPCell(New Phrase("COTIZACIÓN", Font12))
        ColCot.Border = 0
        ColCot.HorizontalAlignment = PdfPCell.ALIGN_CENTER

        Colvacio = New PdfPCell(New Phrase(" ", Font12))
        Colvacio.Border = 0
        Colvacio.HorizontalAlignment = PdfPCell.ALIGN_CENTER

        TableDireccion.AddCell(ColInfoEnc)
        TableDireccion.AddCell(Colvacio)
        TableDireccion.AddCell(ColCot)
        TableDireccion.AddCell(Colvacio)

        TableEncabezado.AddCell(TableDireccion)


        'Tabla Texto
        Dim TableInfoEnc As PdfPTable = New PdfPTable(1)
        TableInfoEnc.WidthPercentage = 100
        Dim widthsTInfoEnc As Single() = New Single() {1000.0F}
        TableInfoEnc.SetWidths(widthsTInfoEnc)

        Dim ColInfo = New PdfPCell(New Phrase("Esta comisión realiza esfuerzos para que la información presentada sea apegada a lo que usted pagará al momento de caja, sin embargo está cantidad puede variar de acuerdo al día del mes y aclaraciones realizadas por el usuario", Fontp))
        ColInfo.Border = 0
        ColInfo.HorizontalAlignment = PdfPCell.ALIGN_LEFT

        TableInfoEnc.AddCell(ColInfo)


        'Tabla Información Usuario


        Dim Region As String
        Dim Ruta As String
        Dim Lote As String
        Dim NoMedidor As String

        Dim cadena2 As String = "select * from usuario where cuenta = '" & txtCuentaCliente.Text & "'"
        Dim drusu As IDataReader = ConsultaSql(cadena2).ExecuteReader()
        drusu.Read()

        Region = drusu("Region")
        Ruta = drusu("Ruta")
        Lote = drusu("Lote")
        NoMedidor = drusu("nodemedidor")


        Dim TableGeneralUsuario As PdfPTable = New PdfPTable(3)
        TableGeneralUsuario.WidthPercentage = 100
        TableGeneralUsuario.DefaultCell.Border = BorderStyle.None
        Dim widthsInfUsua As Single() = New Single() {350.0F, 350.0F, 300.0F}
        TableGeneralUsuario.SetWidths(widthsInfUsua)

        'Tabla Info Usuario 1
        Dim TableUsua1 As PdfPTable = New PdfPTable(1)
        TableUsua1.WidthPercentage = 100
        Dim widthsInfUsua1 As Single() = New Single() {1000.0F}
        TableUsua1.SetWidths(widthsInfUsua1)

        Dim Contrato As String = txtCuentaCliente.Text
        Dim UsuarioCot As String = txtnombre.Text
        'Dim IDCValvu As String = dts("idcuotavalvulista")

        Dim ColInfoUsu1 = New PdfPCell(New Phrase("Contrato: " & Contrato & ", " & UsuarioCot, Font)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
        TableUsua1.AddCell(ColInfoUsu1)

        ColInfoUsu1 = New PdfPCell(New Phrase(txtcolonia.Text & "," & txtdireccion.Text, Font)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
        TableUsua1.AddCell(ColInfoUsu1)

        ColInfoUsu1 = New PdfPCell(New Phrase("Estado de la toma: " & lblestado.Text, Font)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
        TableUsua1.AddCell(ColInfoUsu1)

        ColInfoUsu1 = New PdfPCell(New Phrase("Region: " & Region, Font)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
        TableUsua1.AddCell(ColInfoUsu1)

        'ColInfoUsu1 = New PdfPCell(New Phrase("Cuota Valvulista: ", Font)) With {
        '    .Border = 0,
        '    .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        '}
        'TableUsua1.AddCell(ColInfoUsu1)



        TableGeneralUsuario.AddCell(TableUsua1)


        'Tabla Info Usuario 2
        Dim TableUsua2 As PdfPTable = New PdfPTable(1)
        TableUsua2.WidthPercentage = 100
        Dim widthsInfUsua2 As Single() = New Single() {1000.0F}
        TableUsua1.SetWidths(widthsInfUsua2)

        Dim ColInfoUsu2 = New PdfPCell(New Phrase(" ", Font)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
        TableUsua2.AddCell(ColInfoUsu2)

        ColInfoUsu2 = New PdfPCell(New Phrase(" ", Font)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
        TableUsua2.AddCell(ColInfoUsu2)


        ColInfoUsu2 = New PdfPCell(New Phrase("Tarifa: " & txttarifa.Text, Font)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
        TableUsua2.AddCell(ColInfoUsu2)

        ColInfoUsu2 = New PdfPCell(New Phrase("Ruta: " & Ruta, Font)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
        TableUsua2.AddCell(ColInfoUsu2)

        ColInfoUsu2 = New PdfPCell(New Phrase("Periodos adeudos: " & txtperiodosadeudados.Text, Font)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
        TableUsua2.AddCell(ColInfoUsu2)

        TableGeneralUsuario.AddCell(TableUsua2)

        'Tabla Info Usuario 3
        Dim TableUsua3 As PdfPTable = New PdfPTable(1)
        TableUsua3.WidthPercentage = 100
        Dim widthsInfUsua3 As Single() = New Single() {1000.0F}
        TableUsua3.SetWidths(widthsInfUsua3)


        Dim FechaAct As String = DateTime.Now.ToString("dd/MM/yyyy")

        Dim ColInfoUsu3 = New PdfPCell(New Phrase(FechaAct, Font8N)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
        TableUsua3.AddCell(ColInfoUsu3)

        ColInfoUsu3 = New PdfPCell(New Phrase(" ", Font8N)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
        TableUsua3.AddCell(ColInfoUsu3)

        ColInfoUsu3 = New PdfPCell(New Phrase("Orden de Ruta:  " & Lote, Font)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
        TableUsua3.AddCell(ColInfoUsu3)

        TableGeneralUsuario.AddCell(TableUsua3)



        'Tabla Información Lecturas

        Dim TableGeneralLecturas As PdfPTable = New PdfPTable(2)
        TableGeneralLecturas.WidthPercentage = 100
        TableGeneralLecturas.DefaultCell.Border = BorderStyle.None
        Dim widthsInfLectu As Single() = New Single() {400.0F, 400.0F}
        TableGeneralLecturas.SetWidths(widthsInfLectu)

        'Tabla Info Lectura 1
        Dim TableLect1 As PdfPTable = New PdfPTable(1)
        TableLect1.WidthPercentage = 100
        Dim widthsInfLect1 As Single() = New Single() {1000.0F}
        TableLect1.SetWidths(widthsInfLect1)

        'Obtener datos de lecturas
        'Dim UltLect As String

        'Dim cadena3 As String = "select ultimalectura(" & txtCuentaCliente.Text & ") as ultimalectura"
        'Dim drlec As IDataReader = ConsultaSql(cadena3).ExecuteReader()
        ''drlec.Read()

        'UltLect = ("ultimalectura")


        Dim ColInfoLect1 = New PdfPCell(New Phrase("No de Medidor: " & NoMedidor, Font)) With {
            .Border = 1,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
        TableLect1.AddCell(ColInfoLect1)

        ColInfoLect1 = New PdfPCell(New Phrase("Marca de Medidor: ", Font)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
        TableLect1.AddCell(ColInfoLect1)

        ColInfoLect1 = New PdfPCell(New Phrase("Diametro: ", Font)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
        TableLect1.AddCell(ColInfoLect1)

        TableGeneralLecturas.AddCell(TableLect1)

        'Tabla Info Lectura 1
        Dim TableLect2 As PdfPTable = New PdfPTable(1)
        TableLect2.WidthPercentage = 100
        Dim widthsInfLect2 As Single() = New Single() {1000.0F}
        TableLect2.SetWidths(widthsInfLect2)


        Dim ColInfoLect2 = New PdfPCell(New Phrase("Ultima Lectura M3: " & ultimalectura, Font)) With {
            .Border = 1,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
        TableLect2.AddCell(ColInfoLect2)

        ColInfoLect2 = New PdfPCell(New Phrase("Promedio de Consumo M3: " & promedio, Font)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
        TableLect2.AddCell(ColInfoLect2)

        TableGeneralLecturas.AddCell(TableLect2)



        'Tabla Información Conceptos

        Dim TableGeneralConceptos As PdfPTable = New PdfPTable(5)
        TableGeneralConceptos.WidthPercentage = 100
        TableGeneralConceptos.DefaultCell.Border = BorderStyle.None
        Dim widthsInfConc As Single() = New Single() {100.0F, 350.0F, 100.0F, 100.0F, 60.0F}
        TableGeneralConceptos.SetWidths(widthsInfConc)


        Dim ColInfoConceptos = New PdfPCell(New Phrase("Cantidad", FontWhi)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT,
            .BackgroundColor = New iTextSharp.text.BaseColor(0, 0, 0)
        }
        TableGeneralConceptos.AddCell(ColInfoConceptos)

        ColInfoConceptos = New PdfPCell(New Phrase("Concepto", FontWhi)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT,
            .BackgroundColor = New iTextSharp.text.BaseColor(0, 0, 0)
        }
        TableGeneralConceptos.AddCell(ColInfoConceptos)

        ColInfoConceptos = New PdfPCell(New Phrase("Precio Unitario", FontWhi)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT,
            .BackgroundColor = New iTextSharp.text.BaseColor(0, 0, 0)
        }
        TableGeneralConceptos.AddCell(ColInfoConceptos)

        ColInfoConceptos = New PdfPCell(New Phrase("Importe", FontWhi)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT,
            .BackgroundColor = New iTextSharp.text.BaseColor(0, 0, 0)
        }
        TableGeneralConceptos.AddCell(ColInfoConceptos)

        ColInfoConceptos = New PdfPCell(New Phrase("IVA", FontWhi)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT,
            .BackgroundColor = New iTextSharp.text.BaseColor(0, 0, 0)
        }
        TableGeneralConceptos.AddCell(ColInfoConceptos)


        For Each concepto As Clsconcepto In control.Listadeconceptos

            ColInfoConceptos = New PdfPCell(New Phrase(concepto.Cantidad, FontB8)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
            }
            TableGeneralConceptos.AddCell(ColInfoConceptos)

            ColInfoConceptos = New PdfPCell(New Phrase(concepto.Concepto, FontB8)) With {
                .Border = 0,
                .HorizontalAlignment = PdfPCell.ALIGN_LEFT
            }
            TableGeneralConceptos.AddCell(ColInfoConceptos)

            ColInfoConceptos = New PdfPCell(New Phrase(concepto.Preciounitario, FontB8)) With {
                .Border = 0,
                .HorizontalAlignment = PdfPCell.ALIGN_LEFT
            }
            TableGeneralConceptos.AddCell(ColInfoConceptos)

            ColInfoConceptos = New PdfPCell(New Phrase(concepto.importe, FontB8)) With {
                .Border = 0,
                .HorizontalAlignment = PdfPCell.ALIGN_LEFT
            }
            TableGeneralConceptos.AddCell(ColInfoConceptos)

            ColInfoConceptos = New PdfPCell(New Phrase(concepto.IVA, FontB8)) With {
                .Border = 0,
                .HorizontalAlignment = PdfPCell.ALIGN_LEFT
            }
            TableGeneralConceptos.AddCell(ColInfoConceptos)

        Next


        'Tabla Información Conceptos

        Dim TableGeneralTotales As PdfPTable = New PdfPTable(2)
        TableGeneralTotales.WidthPercentage = 100
        TableGeneralTotales.DefaultCell.Border = BorderStyle.None
        Dim widthsTotales As Single() = New Single() {450.0F, 450.0F}
        TableGeneralTotales.SetWidths(widthsTotales)

        'Tabla Totales 1
        Dim TableTotal1 As PdfPTable = New PdfPTable(2)
        TableTotal1.WidthPercentage = 100
        TableTotal1.DefaultCell.Border = BorderStyle.None
        Dim widthsTotal1 As Single() = New Single() {450.0F, 450.0F}
        TableTotal1.SetWidths(widthsTotal1)

        Dim ColTotales = New PdfPCell(New Phrase("Subtotal", FontB8)) With {
            .Border = 1,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
        TableTotal1.AddCell(ColTotales)

        ColTotales = New PdfPCell(New Phrase(SubTotal.ToString("C"), FontB8)) With {
            .Border = 1,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
        TableTotal1.AddCell(ColTotales)

        ColTotales = New PdfPCell(New Phrase("IVA", FontB8)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
        TableTotal1.AddCell(ColTotales)

        ColTotales = New PdfPCell(New Phrase(IVA.ToString("C"), FontB8)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }

        TableTotal1.AddCell(ColTotales)

        TableGeneralTotales.AddCell(TableTotal1)




        'Tabla Totales 2 


        Dim TableTotal2 As PdfPTable = New PdfPTable(2)
        TableTotal2.WidthPercentage = 100
        TableTotal2.DefaultCell.Border = BorderStyle.None
        Dim widthsTotal2 As Single() = New Single() {450.0F, 450.0F}
        TableTotal2.SetWidths(widthsTotal2)

        Dim ColTotales2 = New PdfPCell(New Phrase("Total", Font12)) With {
            .Border = 1,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
        TableTotal2.AddCell(ColTotales2)

        ColTotales2 = New PdfPCell(New Phrase(Total.ToString("C"), Font12)) With {
            .Border = 1,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
        TableTotal2.AddCell(ColTotales2)



        TableGeneralTotales.AddCell(TableTotal2)


        Dim TableTotalLetra As PdfPTable = New PdfPTable(1)
        TableTotalLetra.WidthPercentage = 100
        TableTotalLetra.DefaultCell.Border = BorderStyle.None
        Dim widthsTotalLet As Single() = New Single() {900.0F}
        TableTotalLetra.SetWidths(widthsTotalLet)

        Dim ColTotLetra = New PdfPCell(New Phrase(ConvertCurrencyToSpanish(Total, "Pesos"), Font9))
        ColTotLetra.Border = 1
        ColTotLetra.HorizontalAlignment = PdfPCell.ALIGN_LEFT
        TableTotalLetra.AddCell(ColTotLetra)


        Dim TablePiePagina As PdfPTable = New PdfPTable(1)
        TablePiePagina.WidthPercentage = 100
        TablePiePagina.DefaultCell.Border = BorderStyle.None
        Dim widthsPPagina As Single() = New Single() {900.0F}
        TablePiePagina.SetWidths(widthsPPagina)


        Dim ColPPagina = New PdfPCell(New Phrase("Esta es una cotización de recibo por lo que no es válido como recibo oficial de pago", FontB8)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }

        TablePiePagina.AddCell(ColPPagina)



        'TableGeneralUsuario.CompleteRow()
        'Agregar las tablas al documento
        pdfDoc.Add(TableEncabezado)
        pdfDoc.Add(TableInfoEnc)
        pdfDoc.Add(TableVacia)
        pdfDoc.Add(TableVacia)
        pdfDoc.Add(TableGeneralUsuario)
        pdfDoc.Add(TableGeneralLecturas)
        pdfDoc.Add(TableVacia)
        pdfDoc.Add(TableGeneralConceptos)
        pdfDoc.Add(TableVacia)
        pdfDoc.Add(TableVacia)
        pdfDoc.Add(TableVacia)
        pdfDoc.Add(TableVacia)
        pdfDoc.Add(TableVacia)
        pdfDoc.Add(TableVacia)
        pdfDoc.Add(TableVacia)
        pdfDoc.Add(TableVacia)
        pdfDoc.Add(TableVacia)
        pdfDoc.Add(TableVacia)
        pdfDoc.Add(TableGeneralTotales)
        pdfDoc.Add(TableVacia)
        pdfDoc.Add(TableTotalLetra)
        pdfDoc.Add(TableVacia)
        pdfDoc.Add(TableVacia)
        pdfDoc.Add(TableVacia)
        pdfDoc.Add(TableVacia)
        pdfDoc.Add(TableVacia)
        pdfDoc.Add(TableVacia)
        pdfDoc.Add(TableVacia)
        pdfDoc.Add(TableVacia)
        pdfDoc.Add(TablePiePagina)






        'Cerrar el documento
        pdfDoc.Close()


        'Visualizar PDF

        Try
            Dim psi As New ProcessStartInfo(cadenafolder & "\" & "Cotizacion_" & txtnombre.Text & ".pdf")
            'psi.WorkingDirectory = cadenafolder & "\factura\" + nombresespacios

            psi.WindowStyle = ProcessWindowStyle.Hidden
            Dim p As Process = Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show("error al visualizar el pdf" & ex.Message)
        End Try

    End Sub



    Private Sub ToolStripSplitButton1_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripSplitButton1.ButtonClick

    End Sub

    Private Sub ListadoDeAuditoriaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim xcorte As New FrmReportexrubros()
        xcorte.tipo = "Auditoria"
        xcorte.Show()
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        Dim xcorte As New FrmReportexrubros()
        xcorte.tipo = "RESUMEN"
        xcorte.Show()
    End Sub

    Private Sub btncortedeldia_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncortedeldia.Click
        Dim xcorte As New FrmReportexrubros()
        xcorte.tipo = "CAJA"
        xcorte.Show()
    End Sub

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        Dim xcorte As New FrmReportexrubros()
        xcorte.tipo = "CAJADESGLOSADO"
        xcorte.Show()
    End Sub
    Private Sub btnreportexrubro_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreportexrubro.Click
        Dim xcorte As New FrmReportexrubros()
        xcorte.tipo = "CONCEPTOS"
        xcorte.Show()
    End Sub

    Private Sub BtnPorRubros_Click(sender As Object, e As EventArgs) Handles BtnPorRubros.Click
        Dim xcorte As New FrmReportexrubros()
        xcorte.tipo = "RUBROS"
        xcorte.Show()
    End Sub

    Private Sub BTNTARIFA_Click(sender As Object, e As EventArgs) Handles BTNTARIFA.Click
        Dim xcorte As New FrmReportexrubros()
        xcorte.tipo = "TARIFA"
        xcorte.Show()
    End Sub

    Private Sub ButtonX1_Click(sender As Object, e As EventArgs) Handles ButtonX1.Click

        Ejecucion("update usuario set deudafec='" + txtult_pago.Text + "' where cuenta=" + txtCuentaCliente.Text)
    End Sub

    Private Sub RadialMenu1_ItemClick(sender As Object, e As EventArgs)
        Try

            Dim elemento As DevComponents.DotNetBar.RadialMenuItem = sender
            If elemento.Name = "RmTicket" Then
                Dim xf As New Formatos
                xf.Show()

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FormatosDeReciboToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dim frmftos As New Formatos
        frmftos.ShowDialog()
    End Sub

    Private Sub btnagrbitacora_Click(sender As Object, e As EventArgs)
        Try

            Dim agrbit As New FrmAgrBitacora
            agrbit.Cuenta = txtCuentaCliente.Text
            agrbit.ShowDialog()
            llenabitacora()
        Catch ex As Exception
            MessageBox.Show("No has introducido una cuenta")
        End Try
    End Sub

    Public Sub llenabitacora()
        If txtCuentaCliente.Text = "" Then Exit Sub
        Dim cons2 As IDataReader
        Try
            cons2 = ConsultaSql("Select * from Bitacora where cuenta = " & txtCuentaCliente.Text & " order by fecha desc").ExecuteReader
        Catch ex As Exception
            Exit Sub
        End Try

        AdvBitacora.Nodes.Clear()

        While cons2.Read
            Dim node As New DevComponents.AdvTree.Node
            node.Cells.Item(0).Text = cons2("Fecha")
            Dim celda As New DevComponents.AdvTree.Cell
            celda.Text = cons2("Hora")
            node.Cells.Add(celda)
            Dim celdaconcepto As New DevComponents.AdvTree.Cell

            Dim control As New DevComponents.DotNetBar.Controls.TextBoxX

            control.Multiline = True
            control.Text = cons2("concepto")
            celdaconcepto.HostedControl = control
            node.Cells.Add(celdaconcepto)

            Dim celdamotivo As New DevComponents.AdvTree.Cell
            celdamotivo.Text = cons2("Motivo")
            node.Cells.Add(celdamotivo)

            Dim usuario As String = obtenerCampo(" select * from letras where ccodusuario=" & cons2("usuario"), "Nombre")

            Dim celdausuario As New DevComponents.AdvTree.Cell
            celdausuario.Text = usuario
            node.Cells.Add(celdausuario)

            AdvBitacora.Nodes.Add(node)
        End While

    End Sub

    Private Sub btnagrbitacora_Click_1(sender As Object, e As EventArgs) Handles btnagrbitacora.Click
        Try

            Dim agrbit As New FrmAgrBitacora
            agrbit.Cuenta = txtCuentaCliente.Text
            agrbit.ShowDialog()
            llenabitacora()
        Catch ex As Exception
            MessageBox.Show("No has introducido una cuenta")
        End Try
    End Sub


End Class


Imports System.Data.Odbc
Imports System.Data
Imports System.Text


Public Class frmPagoefectivo
    Public recibo As New clsrecibo
    Public control As New Clscontrolpago
    Public FACTURADO As Long = 0
    Public FORMAFACTURADO = ""
    Public SerieValidar As String = String.Empty
    Dim imprime As New clsimprimeformato()
    Public actualizarfecha As Boolean = True
    Dim Dat As DataSet
    Public total As Decimal = 0
    Public vale As Decimal = 0
    Public totalapagar As Decimal = 0
    Public cambio As Decimal = 0
    Public nuevovale As Decimal = 0
    Private dts As OdbcDataReader


#Region "varibles pagomixto"
    Dim formadepagodominante As String = "01"
    Dim efectivo As Decimal = totalapagar
    Dim cheque As Decimal = 0
    Dim transferencia As Decimal = 0
    Dim monedero As Decimal = 0
    Dim tcredito As Decimal = 0
    Dim tdebito As Decimal = 0
    Dim obsefectivo As String
    Dim obscheque As String
    Dim obstransferencia As String
    Dim obsmonedero As String
    Dim obstcredito As String
    Dim obstdebito As String
    Public mixto As Boolean = False
#End Region


    Private Sub frmPagoefectivo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        ''''''''''

        If e.KeyCode = 113 Then        ' F2
            Dim fr As New FrmPago(Me)
            fr.totalapagar = totalapagar
            Try

            Catch ex As Exception

            End Try

            fr.ShowDialog()
            recibo.ccodpago = fr.cmbforma.SelectedValue
            efectivo = fr.efectivo
            cheque = fr.cheque
            transferencia = fr.transferencia
            tcredito = fr.credito
            tdebito = fr.debito
            obsefectivo = fr.efectivo
            obscheque = fr.cheque
            obstransferencia = fr.transferencia
            obstcredito = fr.credito
            obstdebito = fr.debito

        End If
        If e.KeyCode = 114 Then        ' F3
            imprime.imp.seleccionarimpresora()
        End If
        '''''''''''
        If e.KeyCode = Keys.Escape Then
            Me.Hide()
        End If
        '''''''''''
    End Sub
    Private Sub FrmPagar_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles MyBase.Load
        ' Timer1.Start()


        recibo.ccodpago = "01"

        Dim basem As New base
        If FORMAFACTURADO = "" Then ' NO VIENE DE LA FACTURACION ELECTRONICA
            Dim dato As Odbc.OdbcDataReader = basem.consultasql("select * from fpago limit 1")
            If dato.Read Then
                lblFormadepago.Text = dato("ccodpago")


            End If
            dato = basem.consultasql("select * from pagos where serie='" & My.Settings.serie & "' and recibo=" & My.Settings.folio + 1 & " limit 1")
            If dato.Read Then
                MessageBox.Show("El folio que vas imprimir ya existe")
                basem.conexion.Dispose()
                Close()
            End If
            basem.conexion.Dispose()

        Else ' VIENE DE LA FACTURACION

            lblFormadepago.Text = FORMAFACTURADO
            '  Dim dato As Odbc.OdbcDataReader
            If FORMAFACTURADO = "x" Then
                ' MessageBox.Show("El folio que vas imprimir ya existe")
                basem.conexion.Dispose()
                Close()

            End If
            Try
                txtefectivo_recibido.Text = "0"
                cambio = 0
                lblcambio.Text = "0"

                basem.conexion.Dispose()
            Catch ex As Exception

            End Try
        End If
        basem.conexion.Dispose()

    End Sub


    Private Sub txtefectivo_recibido_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtefectivo_recibido.KeyPress
        If e.KeyChar = Chr(13) Then
            btncobrar_imprimir.Focus()
        End If
    End Sub

    Private Sub txtefectivo_recibido_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtefectivo_recibido.LostFocus
        btncobrar_imprimir.Focus()
    End Sub

    Private Sub txtefectivo_recibido_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtefectivo_recibido.TextChanged

        If Val(txtefectivo_recibido.Text) > totalapagar Then
            btnabonar.Enabled = True
        End If
        Try
            cambio = Val(txtefectivo_recibido.Text) - totalapagar
            lblcambio.Text = (txtefectivo_recibido.Text) - (lbltotal_pagar.Text)

        Catch ex As Exception
            MsgBox("Ingrese la cantidad recibida", MsgBoxStyle.Exclamation, "Advertencia....!!!")
        End Try
    End Sub

    Private Sub btncobrar_imprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncobrar_imprimir.Click
        btncobrar_imprimir.Enabled = False
        '  Dim DES As New FrmDescuentos

        If control.EsMEdido And control.desgloselecturas.Count > 0 Then
            GuardarLecturasAD()
        End If

        grabareimprimir()
        imprime.desconectarhilosdeimpresora()

        'Actualizar los saldos 8/11/2021

        Try
            Dim datos As Odbc.OdbcDataReader
            Dim total As Double
            datos = ConsultaSql("select * from usuario USU inner join descuentos DES on(USU.idDescuento=DES.idDescuento) where cuenta=" & recibo.cuenta & "").ExecuteReader
            datos.Read()
            Dim pago As New Clscontrolpago
            pago.cuenta = recibo.cuenta
            pago.Tarifa = datos("Tarifa").ToString()
            pago.Fechafinal = Now.AddMonths(-1)
            pago.Fechainicio = datos("deudafec")
            'pasar los parametros si contiene descuento, alcantarrillado y saneamiento

            pago.saneamiento = datos("Saneamiento")
            pago.alcantarillado = datos("alcantarillado")
            pago.valvulista = datos("idCuotaValvulista")

            If datos("idDescuento") > 0 Then

                ''------ Descuentos
                Try
                    pago.descontartodoslosperiodosdeconsumo = True
                Catch ex As Exception

                End Try

                Try
                    pago.periodoscondescuentodeconsumo = Month(Now)
                Catch ex As Exception

                End Try

                Try
                    pago.descuentoaconsumo = datos("nPctDsct")
                Catch ex As Exception

                End Try
                ''------
            Else
                Try
                    pago.descontartodoslosperiodosdeconsumo = False
                Catch ex As Exception

                End Try

                Try
                    pago.periodoscondescuentodeconsumo = 0
                Catch ex As Exception

                End Try

                Try
                    pago.descuentoaconsumo = 0
                Catch ex As Exception

                End Try
            End If

            'Try
            '    If datos("id_Tarifa") = "4" Then
            '        Ejecucion("Update lecturas Set  monto = consumomedidos(consumo,'1', " & Year(Now) & ")  where cuenta=" & datos("cuenta") & " and pagado=0 and an_per=" & Year(Now) & " and not ( mes= '" & NOMBREDEMES3CAR(Month(Now)) & "' and an_per=" & Year(Now) & ");")

            '    End If

            'Catch ex As Exception

            'End Try

            pago.calcula(False, False, Now)


            'Dim concepto As New Clsconcepto
            'Try

            '    concepto = pago.Listadeconceptos.Item("PAGO VALVULISTA")
            'Catch ex As Exception

            'End Try

            ' Rectifica el iva en los conceptos
            Dim acumiva As Double = 0

            For index = 1 To pago.Listadeconceptos.Count
                Dim conce As New Clsconcepto
                conce = pago.Listadeconceptos(index)
                If conce.IVA > 0 Then
                    conce.IVA = Math.Round((conce.Cantidad * conce.Preciounitario) * (variable_iva / 100), 2)
                End If
                acumiva += conce.IVA
            Next

            total = pago.totaldeudaconsumo + pago.totaldeudaalcantarillado + pago.totaldeudasaneamiento + acumiva + pago.totaldeudarecargos + pago.totaldeudaotros '+ concepto.importe

            Ejecucion("update usuario set deuda=" & pago.totaldeudaconsumo & " , deualcant=" & pago.totaldeudaalcantarillado & ", deudasanea=" & pago.totaldeudasaneamiento & ", iva=" & acumiva & ", recargos =" & pago.totaldeudarecargos & ", deudaotros=" & pago.totaldeudaotros & ", total=" & total & ", LecturaAnt=UltimaLecturaActualizar('" & recibo.cuenta & "'), PeriodosAdeudo=" & pago.desgloseconsumo.Count + pago.desgloserezago.Count & ",periodo='" & pago.periodo & "'  where cuenta=" & recibo.cuenta)
            datos.Close()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub GuardarLecturasAD()
        '  Dim x As base = New base

        Dim montolectura As Double = 0
        Dim alcantarillado As Double = 0
        Dim saneamiento As Double = 0
        Dim consumo As Double = 0
        Dim totalConsu As Double = 0
        Dim totalAlcan As Double = 0
        Dim totalSanea As Double = 0
        Dim totalAnticipo As Double = 0
        Dim concepto As New ClsRegistrolectura



        Try
            'For i = 0 To control.desgloseAnticipo.Count - 1
            'concepto = control.desgloseAnticipo.Item(i + 1)
            For i = 0 To control.desgloselecturas.Count - 1
                concepto = control.desgloselecturas.Item(i + 1)
                Try
                    dts = ConsultaSql("select cuenta, mes, an_per from lecturas where cuenta='" & control.cuenta & "' and mes='" & concepto.Mes & "' and an_per='" & concepto.Periodo & "'").ExecuteReader
                    If dts.Read = True Then
                        MessageBox.Show("La Lectura del Periodo: " & concepto.Mes & "-" & concepto.Periodo & " ya Existe")
                    Else

                        ' Dim consumo As Double = x.obtenerCampo("SELECT ConsumoMedidosSin(" & concepto.Consumo & ",'" & control.Tarifa & "') as Consumo  ", "Consumo")
                        consumo = concepto.Totalcondescuento
                        For cicloalcanta = 1 To control.desglosealcantarillado.Count
                            Dim conceptoalcan As ClsRegistrolectura
                            conceptoalcan = control.desglosealcantarillado(cicloalcanta)
                            If concepto.Mes = conceptoalcan.Mes And concepto.Periodo = conceptoalcan.Periodo Then
                                alcantarillado = conceptoalcan.Totalcondescuento
                            End If
                        Next



                        For ciclosanea = 1 To control.desglosesaneamiento.Count
                            Dim conceptoalcan As ClsRegistrolectura
                            conceptoalcan = control.desglosesaneamiento(ciclosanea)
                            If concepto.Mes = conceptoalcan.Mes And concepto.Periodo = conceptoalcan.Periodo Then
                                saneamiento = conceptoalcan.Totalcondescuento
                            End If
                        Next

                        montolectura = consumo + alcantarillado + saneamiento
                        Try
                            Ejecucion("insert into lecturas set cuenta='" & control.cuenta & "'," &
                                       "mes= '" & concepto.Mes & "', an_per= " & concepto.Periodo & ", consumo=" & concepto.Consumo & "," &
                                       "montocobrado=" & montolectura & " , adelant= 0 , AConsumo=" & consumo & ", AAlcantarillado= " & alcantarillado &
                                       ", pagado=1, ASaneamiento= " & saneamiento & ",lectant=ultimalectura(" & recibo.cuenta & "),lectura=ultimalectura(" & recibo.cuenta & ")")

                        Catch ex As Exception
                            MessageBox.Show(ex.Message)
                        End Try

                    End If

                Catch ex As Exception

                End Try
                totalConsu = totalConsu + consumo
                totalAlcan = totalAlcan + alcantarillado
                totalSanea = totalSanea + saneamiento
            Next

            For i = 0 To control.desgloseAnticipo.Count - 1
                concepto = control.desgloseAnticipo.Item(i + 1)
                Try
                    Ejecucion("UPDATE lecturas SET adelant=1 WHERE CUENTA='" & control.cuenta & "'AND MES= '" & concepto.Mes & "' AND an_per=" & concepto.Periodo & ";")
                Catch ex As Exception
                    guardatxt("c:\errrpagos", "errorpagos.txt", ex.Message)
                End Try

            Next



            totalAnticipo = totalConsu + totalAlcan + totalSanea

            'txtAConsumo.Text = totalConsu
            'txtAAlcantarillado.Text = totalAlcan
            'txtASaneamiento.Text = totalSanea
            'txtTotalAnticipos.Text = totalAnticipo
            Dim cadenainanti As New StringBuilder
            cadenainanti.Append("INSERT INTO anticipos set Fecha='" & Now.Year & "-" & Now.Month & "-" & Now.Day & "',")
            cadenainanti.Append("Cuenta=" & control.cuenta & ",")
            cadenainanti.Append("idMov='" & My.Settings.serie & (My.Settings.folio + 1) & "',")
            cadenainanti.Append("Status='A',")
            cadenainanti.Append("Monto=" & totalAnticipo & ",")
            cadenainanti.Append("Recibo =" & My.Settings.folio + 1 & ",")
            cadenainanti.Append("Serie = '" & My.Settings.serie & "',")
            cadenainanti.Append("meses= " & control.desgloseAnticipo.Count & ",")
            cadenainanti.Append("Consumo=" & totalConsu & ",")
            cadenainanti.Append("Alcantarillado=" & totalAlcan & ",")
            cadenainanti.Append("Saneamiento=" & totalSanea & ";")


            Ejecucion(cadenainanti.ToString())
            'Ejecucion("UPDATE usuario SET credito=Credito + " & totalAnticipo & "  WHERE CUENTA='" & control.cuenta & "'")


        Catch ex As Exception

        End Try

    End Sub
    Public Sub imprimerecibo(ByVal folio As Integer, ByVal serie As String)
        Dim que As New base
        Dim imphist As Boolean = False
        'actualiza la fecha de pago al usuario cuando paga consumo
        If actualizarfecha = True Then
            imphist = True

        End If
        Try

        Catch ex As Exception

        End Try
        Dim quees As String = obtenerCampo("select esusuario from pagos where recibo=" & folio & " and serie='" & serie & "'", "esusuario")
        If quees = "1" Then
            Try
                If My.Settings.manejacatalogocalles.ToUpper() = "SI" Then
                    imprime.imprime("select pagos.recibo,pagos.serie,usuario.nombre as nombre, concat(calles.nombre,"" "", usuario.numext, "" "", numint) as Direccion, Col.Colonia, Com.Comunidad, pagos.total as total, pagos.fecha_Act as fecha_act, usuario.rfc as rfc, usuario.nodemedidor as Medidor, pagos.pagos as subtotal, pagos.iva as iva,Cuo.Descripcion_Cuota AS TARIFA, pagos.CCODPAGO AS CCODPAGO,pagos.cuenta as CUENTA,pagos.observacion as observacion, pagos.Descuento, pagos.DescuentoPesos,pagos.CAJA, PAGOS.USUARIO,usuario.LecturaAct, usuario.LecturaAnt from (pagos,usuario inner join colonia Col on (Col.id_colonia= usuario.id_colonia) inner join comunidades Com on (Com.Id_comunidad= usuario.Id_comunidad) inner join cuotas Cuo on(usuario.TARIFA=Cuo.id_tarifa)) inner join calles on usuario.id_Calle = calles.id_calle  where recibo=" & folio & " and serie='" & serie & "' and usuario.cuenta=pagos.cuenta", My.Settings.formatorecibo, "select * from pagotros where recibo=" & folio & " and serie='" & serie & "'", False, imphist)
                Else
                    imprime.imprime("select pagos.recibo,pagos.serie,usuario.nombre as nombre, domicilio as Direccion, Col.Colonia, Com.Comunidad, pagos.total as total, pagos.fecha_Act as fecha_act, usuario.rfc as rfc, usuario.nodemedidor as Medidor, pagos.pagos as subtotal, pagos.iva as iva,Cuo.Descripcion_Cuota AS TARIFA, pagos.CCODPAGO AS CCODPAGO,pagos.cuenta as CUENTA,pagos.observacion as observacion, pagos.Descuento, pagos.DescuentoPesos,pagos.CAJA,PAGOS.USUARIO,usuario.LecturaAct, usuario.LecturaAnt from (pagos,usuario inner join colonia Col on (Col.id_colonia= usuario.id_colonia) inner join comunidades Com on (Com.Id_comunidad= usuario.Id_comunidad) inner join cuotas Cuo on(usuario.TARIFA=Cuo.id_tarifa))   where recibo=" & folio & " and serie='" & serie & "' and usuario.cuenta=pagos.cuenta", My.Settings.formatorecibo, "select * from pagotros where recibo=" & folio & " and serie='" & serie & "'", False, imphist)
                End If

            Catch ex As Exception

            End Try

            'imprime.imprime("select pagos.recibo,pagos.serie,usuario.nombre as nombre,usuario.cuentaAnterior as CuentaAnterior, concat(calles.nombre,"" "", usuario.numext, "" "", numint) as Direccion, Col.Colonia, Com.Comunidad, pagos.total as total, pagos.fecha_Act as fecha_act, usuario.rfc as rfc, usuario.nodemedidor as Medidor, pagos.pagos as subtotal, pagos.iva as iva,Cuo.Descripcion_Cuota AS TARIFA, pagos.CCODPAGO AS CCODPAGO,pagos.cuenta as cuenta,pagos.observacion as observacion, pagos.Descuento from (pagos,usuario inner join colonia Col on (Col.id_colonia= usuario.id_colonia) inner join comunidades Com on (Com.Id_comunidad= usuario.Id_comunidad) inner join cuotas Cuo on(usuario.TARIFA=Cuo.id_tarifa)) inner join calles on usuario.id_Calle = calles.id_calle  where recibo=" & folio & " and serie='" & serie & "' and usuario.cuenta=pagos.cuenta", My.Settings.formatorecibo, "select * from pagotros where recibo=" & folio & " and serie='" & serie & "'", True, imphist)


        ElseIf quees = "2" Then
            Try
                imprime.imprime("select pagos.recibo,pagos.serie,nousuarios.nombre as nombre,"""" as CuentaAnterior, nousuarios.direccion, nousuarios.colonia AS COLONIA, nousuarios.comunidad AS COMUNIDAD, pagos.total as total, pagos.fecha_Act as fecha_act, nousuarios.rfc as rfc, pagos.pagos as subtotal, pagos.iva as iva,0 AS TARIFA, pagos.CCODPAGO AS CCODPAGO,pagos.cuenta as cuenta,pagos.observacion as observacion, pagos.descuento, pagos.DescuentoPesos,PAGOS.USUARIO,PAGOS.CAJA from pagos,nousuarios where recibo=" & folio & " and serie='" & serie & "' and nousuarios.clave=pagos.cuenta", My.Settings.formatorecibo, "select * from pagotros where recibo=" & folio & " and serie='" & serie & "'", False, False)
            Catch ex As Exception

            End Try


            'esto para las solicitudes
        ElseIf quees = "3" Then
            Try
                imprime.imprime("select pagos.recibo, pagos.serie, s.nombre as nombre, """" as cuentaAnterior, concat(calles.nombre, "" "", s.numext,"" "", s.numint) as DIRECCION, pagos.colonia AS COLONIA, pagos.comunidad AS comunidad, pagos.total as total, 0 as medidor, pagos.fecha_Act as fecha_act, S.rfc as rfc, pagos.pagos as subtotal, pagos.iva as iva, 0 AS TARIFA, pagos.CCODPAGO AS CCODPAGO, pagos.cuenta as cuenta, pagos.observacion as observacion, pagos.descuento, pagos.DescuentoPesos,PAGOS.CAJA,PAGOS.USUARIO from ( (pagos inner join solicitud s on s.Numero = pagos.cuenta) inner join calles on s.id_calle=calles.id_Calle) where recibo =" & folio & " and serie = '" & serie & "'", My.Settings.formatorecibo, "select * from pagotros where recibo=" & folio & " and serie='" & serie & "'", False, False)
            Catch ex As Exception

            End Try



        End If
        que.conexion.Dispose()
    End Sub

    Public Sub grabareimprimir()
        ' Dim save As New base
        'Dim GuardaCobroexpress As New BaseCobroexpress
        'esto es para saber si existe el folio
        'Try
        '    ConectarRemoto()
        'Catch ex As Exception

        'End Try

        Dim REC As String

        Dim consumoLectura As Integer
        Dim tipoServicio As String
        Dim tipoUso As String
        Dim formaPago As String = ""

        REC = obtenerCampo("select RECIBO FROM pagos WHERE serie='" & My.Settings.serie & " and  RECIBO='" & My.Settings.folio + 1 & "'", "RECIBO").ToString()

        If REC <> 0 Then
            MessageBox.Show("EL FOLIO YA EXISTE, CAMBIARLO POR UNO NUEVO", "INFORMACION")
            Close()
        Else


            Try

                If recibo.ccodpago = "" Then
                    recibo.ccodpago = "01"
                End If


                Dim tipotarifa As String
                If control.EsFijo Then
                    tipotarifa = "FIJO"
                Else
                    tipotarifa = "MEDIDO"
                End If


                tipoUso = obtenerCampo($"select * from usuario where cuenta = {recibo.cuenta}", "ID_TIPO_USUARIO")

                If tipoUso = "1" Then
                    tipoUso = "DOMESTICO"
                Else
                    tipoUso = "NO DOMESTICO"
                End If
                recibo.ccodpago = lblFormadepago.Text

                formaPago = recibo.ccodpago


                If Not control.Listadeconceptos.Contains("Consumo") Then ' SI NO ELGIERON EL CONSUMO NO TIENE CASO GRABAR EL DESCUETO EN PESOS DEL CONSUMO
                    recibo.Totaldescuentoenpesos = 0

                End If

                Dim restavales As Decimal = nuevovale - vale

                If recibo.Totaldescuentoenpesos > 0 And recibo.descuento = 0 Then
                    recibo.Totaldescuentoenpesos = 0
                End If


                Dim cadenainsertapagos As String = "INSERT INTO pagos (FECHA_ACT,
                    PERIODO, PAGOS, FECHA_DEUDA, IVA, TOTAL, NOMBRE, RECIBO, CANCELADO,
                    CUENTA, COMUNIDAD, COLONIA, SERIE, USUARIO, CAJA, UBICACION, TARIFA, CCODPAGO, ESFIJO, FACTURADO, esusuario,observacion,
                    Descuento,Descuentopesos,Vale) VALUES ('" & recibo.Fecha_Act & "', '" & recibo.periodo & "'," & recibo.subtotal & ", '" & recibo.fecha_deuda & "', '" & recibo.iva & "', '" & recibo.subtotal + recibo.iva + restavales & "', '" & recibo.nombre & "', '" & My.Settings.folio + 1 & "', '" & recibo.cancelado & "', '" & recibo.cuenta & "', '" & recibo.comunidad & "', '" & recibo.colonia & "', '" & My.Settings.serie & "', '" & recibo.usuarios & "', '" & My.Settings.caja & "', '" & recibo.ubicacion & "', '" & recibo.tarifa & "', '" & recibo.ccodpago & "'," & control.EsFijo & "," & FACTURADO & "," & recibo.esusuario & ",'" & recibo.observacion & "', " & recibo.descuento & "," & recibo.Totaldescuentoenpesos & "," & restavales & " )"


                Ejecucion(cadenainsertapagos)
                'Dim unused2 = EjecutarConsultaRemotaAsync(cadenainsertapagos)


                ''''''''''''''''''''''''''''' grabando lo de los descuentos '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                '''
                ''' 
                '''''''''''''''''''''''''''''

                If control.descuentoaconsumo > 0 And control.totaldescuentopesos > 0 Then
                    Dim CADENAZUrich = "insert into descuentospagos (serie,recibo, concepto, porcentaje, Monto) values ( '" & My.Settings.serie & "'," & My.Settings.folio + 1 & ",'CONSUMO'," & control.descuentoaconsumo & "," & control.totaldescuentopesos & ")"
                    Ejecucion(CADENAZUrich)

                End If
                If control.descuentoaalcantarillado > 0 Then

                End If
                If control.descuentoasaneamiento > 0 Then

                End If
                If control.descuentoarecargo > 0 And control.totaldescuentorecargo > 0 Then
                    Dim CADENAZUrich = "insert into descuentospagos (serie,recibo, concepto, porcentaje, Monto) values ( '" & My.Settings.serie & "'," & My.Settings.folio + 1 & ",'RECARGO'," & control.descuentoarecargo & "," & control.totaldescuentorecargo & ")"
                    Ejecucion(CADENAZUrich)

                End If





                If mixto Then

                    If efectivo > 0 Then
                        Dim cadxr As String = "insert into pagomixto (serie,folio, monto,id_forma,observacion) values ('" & My.Settings.serie & "'," & My.Settings.folio + 1 & "," & efectivo & ",'01','" & obsefectivo & "')"
                        Ejecucion(cadxr)


                        'Try
                        '    ''Dim unused = EjecutarConsultaRemotaAsync(cadxr)
                        'Catch ex As Exception

                        'End Try

                    End If

                    If cheque > 0 Then
                        Dim cadxr As String = "insert into pagomixto (serie,folio, monto,id_forma,observacion) values ('" & My.Settings.serie & "'," & My.Settings.folio + 1 & "," & cheque & ",'02','" & obscheque & "')"
                        Ejecucion(cadxr)


                        'Try
                        '    'Dim unused = EjecutarConsultaRemotaAsync(cadxr)
                        'Catch ex As Exception

                        'End Try

                    End If
                    If transferencia > 0 Then
                        Dim cadxr As String = "insert into pagomixto (serie,folio, monto,id_forma,observacion) values ('" & My.Settings.serie & "'," & My.Settings.folio + 1 & "," & transferencia & ",'03','" & obstransferencia & "')"
                        Ejecucion(cadxr)


                        'Try
                        '    Dim unused = EjecutarConsultaRemotaAsync(cadxr)
                        'Catch ex As Exception

                        'End Try

                    End If
                    If tcredito > 0 Then
                        Dim cadxr As String = "insert into pagomixto (serie,folio, monto,id_forma,observacion) values ('" & My.Settings.serie & "'," & My.Settings.folio + 1 & "," & tcredito & ",'04','" & obstcredito & "')"
                        Ejecucion(cadxr)

                        'Try
                        '    Dim unused = EjecutarConsultaRemotaAsync(cadxr)
                        'Catch ex As Exception

                        'End Try

                    End If
                    If tdebito > 0 Then
                        Dim cadxr As String = "insert into pagomixto (serie,folio, monto,id_forma,observacion) values ('" & My.Settings.serie & "'," & My.Settings.folio + 1 & "," & tdebito & ",'28','" & obstdebito & "')"
                        Ejecucion(cadxr)


                        'Try
                        '    Dim unused = EjecutarConsultaRemotaAsync(cadxr)
                        'Catch ex As Exception

                        'End Try

                    End If

                    If vale > 0 Then
                        Dim cadxr As String = "insert into pagomixto (serie,folio, monto,id_forma,observacion) values ('" & My.Settings.serie & "'," & My.Settings.folio + 1 & "," & vale & ",'05','Viene de abono')"
                        Ejecucion(cadxr)

                        'Try
                        '    Dim unused = EjecutarConsultaRemotaAsync(cadxr)
                        'Catch ex As Exception

                        'End Try

                    End If
                End If




                'Dim cadenaremota As String = "iNSERT INTO `recibomaestro` ( `Serie`, `recibo`, `fecha`, `nombre`, `subtotal`, `iva`, `total`,"
                'cadenaremota += " `descuento_pesos`, `descuento_porcentaje`, `tipodescuento`, `id_caja`, `id_forma_pago`, `esusuario`,"
                'cadenaremota += "`observacion`, `Estado`, `cuenta`, `fechaini`, `fechafinal`, `id_factura`,seriefactura,factura, `usuariosistema`, `basegravable`, `Tarifa`, "
                'cadenaremota += "`Tipousuario`, `TipoTarifa`, `comunidad`, `colonia`, `direccion`, `periodo`,vale) values ('"
                'cadenaremota += My.Settings.serie & "'," & My.Settings.folio + 1 & ",'" & recibo.Fecha_Act & "','" & recibo.nombre & "'," & recibo.subtotal & "," & recibo.iva & "," & recibo.total & "," & recibo.Totaldescuentoenpesos & "," & recibo.descuento & ",'" & TIPODESCUENTO & "'," & My.Settings.caja & ",'" & recibo.ccodpago & "'," & recibo.esusuario & ",'','" & recibo.cancelado & "'," & recibo.cuenta & ",'" & recibo.fecha_deuda & "','" & Year(control.Fechafinal) & "-" & Month(control.Fechafinal) & "-01',0,'" & serieregresada & "', " & FACTURADO & ",'" & recibo.usuarios & "',0,'" & recibo.tarifa & "','" & tipoUso & "','" & tipotarifa & "','" & recibo.comunidad & "','" & recibo.colonia & "','','" & recibo.periodo & "'," & nuevovale & ")"


                'Try
                '    ConectarRemoto()
                '    Dim unused = EjecutarConsultaRemotaAsync(cadenaremota)
                '    DesconectarRemoto()
                'Catch ex As Exception
                '    DesconectarRemoto()
                'End Try





                If FACTURADO > 0 Then

                    Dim cadenafactura As String = "insert into encfac SET FECHA=CONCAT(CURDATE(), ' ' ,curtime()), SERIE=' ', numero =" & FACTURADO & ",NOMBRE='" & recibo.nombre & "',  SUBTOTAL=" & recibo.subtotal & ",IVA=" & recibo.iva & ",TOTAL=" & recibo.total & ",TIPO='" & recibo.esusuario & "', ESTADO='A', CAJA='" & My.Settings.caja & "', USUARIO='" & usuariodelsistema & "', motivocancelacion=''"
                    Ejecucion(cadenafactura)

                    'Dim usused4 = EjecutarConsultaRemotaAsync(cadenafactura)



                End If



                Try
                    Dim cadenadescuentorec As String = "update descuentorecargo set descuentorecargo=" & control.totaldescuentorecargo & ",ESTADO='Aplicado' where cuenta=" & control.cuenta & " and estado='Pendiente'"
                    Ejecucion(cadenadescuentorec)

                    'Dim unsed5 = EjecutarConsultaRemotaAsync(cadenadescuentorec)

                Catch ex As Exception

                End Try

                For i = 1 To control.Listadeconceptos.Count
                    Dim concepto As New Clsconcepto
                    concepto = control.Listadeconceptos(i)
                    Dim coniva As Boolean = False
                    If concepto.IVA > 0 Then
                        coniva = True
                    End If


                    'Dim cadena As String
                    'Dim idconvenio As Integer
                    'cadena = "select idEncConvenio from EncConvenio where idcuenta =" & recibo.cuenta
                    'Dim dr As IDataReader = ConsultaSql(cadena).ExecuteReader()
                    'If dr.Read() Then
                    '    idconvenio = dr("idEncConvenio")
                    'End If


                    'Try
                    '    If concepto.Clave = My.Settings.claveConvenio Then

                    '        Dim folioencconvenio As Integer = idconvenio



                    '        Ejecucion("update encconvenio set estado='Con pagos' where idencconvenio=" & folioencconvenio)

                    '    End If

                    'Catch ex As Exception

                    'End Try


                    Dim cadenapagotros As String = "INSERT INTO pagotros (RECIBO, CUENTA, SERIE, USUARIO, FECHA, concepto, Caja, CANCELADO, CCODPAGO, IMPORTE, CANTIDAD, MONTO, NUMCONCEPTO, CLAVEMOV, IVA, montoiva) 
                    VALUES ('" & My.Settings.folio + 1 & "', '" & recibo.cuenta & "', '" & My.Settings.serie & "', '" & recibo.usuarios & "', '" & recibo.Fecha_Act & "', '" & concepto.Concepto & "', '" & My.Settings.caja & "', '" & recibo.cancelado & "', 
                    '" & recibo.ccodpago & "', " & concepto.importe & ", " & concepto.Cantidad & "," & concepto.Preciounitario & ",'" & concepto.Clave & "'," & concepto.CLAVEMOV & "," & coniva & "," & concepto.IVA & ")"

                    Ejecucion(cadenapagotros)





                    If FACTURADO > 0 Then

                        Dim detafac As String = "INSERT INTO detfac SET SERIE=' ', NUMERO=" & FACTURADO & ",CANTIDAD=" & concepto.Cantidad & ", DESCRIPCION='" & concepto.Concepto & "' , PRECIOUNITARIO=" & concepto.Preciounitario & ", IMPORTE=" & concepto.importe & ", CONIVA=" & coniva & ", IVA=" & concepto.IVA & ""
                        Ejecucion(detafac)

                        'Dim unsed6 = EjecutarConsultaRemotaAsync(detafac)

                    End If


                Next i


            Catch ex As Exception

                MessageBox.Show(ex.Message)

            End Try



            Try

                If actualizarfecha = True Then

                    Dim efectivoRecibido As Double = Double.Parse(recibo.total)
                    Try
                        Porcentajes(recibo.cuenta, efectivoRecibido)

                    Catch ex As Exception

                    End Try


                    If control.EsFijo And (control.desgloserezago.Count + control.desgloseconsumo.Count) > 0 Then
                        Dim fechafinalfijo As String = control.Fechafinal
                        If control.desgloseconsumo.Count > 0 Then
                            Dim renglon As clsunidadmes
                            renglon = control.desgloseconsumo(control.desgloseconsumo.Count)

                            fechafinalfijo = renglon.periodo & "-" & CadenaNumeroMes(renglon.mes) & "-01"
                        Else
                            If control.desgloserezago.Count > 0 Then
                                Dim renglon As clsunidadmes
                                renglon = control.desgloserezago(control.desgloserezago.Count)

                                fechafinalfijo = renglon.periodo & "-" & CadenaNumeroMes(renglon.mes) & "-01"
                            End If
                        End If

                        Dim cadenafechafin As String = "UPDATE usuario SET deudafec='" & fechafinalfijo & "',credito=" & nuevovale & " WHERE CUENTA=" & recibo.cuenta

                        Ejecucion(cadenafechafin)



                        'Dim unsed7 = EjecutarConsultaRemotaAsync(cadenafechafin)

                    End If


                    If control.EsMEdido And (control.desgloseconsumo.Count + control.desgloserezago.Count) > 0 Then
                        Dim fechafinalmedido As String = control.Fechafinal
                        If control.desgloseconsumo.Count > 0 Then
                            Dim renglon As ClsRegistrolectura
                            renglon = control.desgloseconsumo(control.desgloseconsumo.Count)

                            fechafinalmedido = renglon.Periodo & "-" & CadenaNumeroMes(renglon.Mes) & "-01"
                        Else
                            If control.desgloserezago.Count > 0 Then
                                Dim renglon As ClsRegistrolectura
                                renglon = control.desgloserezago(control.desgloserezago.Count)

                                fechafinalmedido = renglon.Periodo & "-" & CadenaNumeroMes(renglon.Mes) & "-01"
                            End If
                        End If



                        Dim cadenafechafin As String = "UPDATE usuario Set deudafec='" & fechafinalmedido & "',credito=" & nuevovale & " WHERE CUENTA=" & recibo.cuenta

                        Ejecucion(cadenafechafin)



                        'Dim unsed7 = EjecutarConsultaRemotaAsync(cadenafechafin)

                    End If
                End If



            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            Try


                For i = 1 To control.desgloseconsumo.Count
                    If control.EsFijo = True Then

                        Dim objeto As clsunidadmes

                        objeto = control.desgloseconsumo.Item(i)

                        Try



                            consumoLectura = obtenerCampo($"select * from lecturas where cuenta = {recibo.cuenta} and an_per = {objeto.periodo} and mes = '{objeto.mes}'", "consumo")

                            tipoServicio = obtenerCampo($"select * from vusuario where cuenta = {recibo.cuenta}", "MEDIDO")

                            If tipoServicio = 1 Then
                                tipoServicio = "MEDIDO"
                            Else
                                tipoServicio = "FIJO"
                            End If

                            tipoUso = obtenerCampo($"select * from usuario where cuenta = {recibo.cuenta}", "ID_TIPO_USUARIO")

                            If tipoUso = 1 Then
                                tipoUso = "DOMESTICO"
                            Else
                                tipoUso = "NO DOMESTICO"
                            End If

                            Dim cadenapagomes As String = "INSERT INTO pago_mes (PERIODO, MES, ANO, CONCEPTO, FECHA, recibo, Caja, SERIE, CUENTA, monto, DESCUENTO, MONTOPAGADO, TIPO, CONSUMO, TIPOUSO) VALUES ('" & CadenaNumeroMes(objeto.mes) & Mid(objeto.periodo, 3, 2) & "','" & objeto.mes & "'," & objeto.periodo & ",'CONSUMO ','" & recibo.Fecha_Act & "'," & My.Settings.folio + 1 & ",'" & My.Settings.caja & "', '" & My.Settings.serie & "'," & recibo.cuenta & "," & objeto.totalcondescuento & "," & objeto.total - objeto.totalcondescuento & "," & objeto.totalcondescuento & ",'" & tipoServicio & "'," & consumoLectura & ",'" & tipoUso & "')"
                            Ejecucion(cadenapagomes)

                            'Dim usued8 = EjecutarConsultaRemotaAsync(cadenapagomes)

                        Catch ex As Exception
                            MessageBox.Show($"Ocurrio un error al insertar a tabla Pago_mes {ex.ToString()}")
                        End Try
                    Else

                        Dim objeto As ClsRegistrolectura
                        objeto = control.desgloseconsumo.Item(i)

                        Try



                            consumoLectura = obtenerCampo($"select * from lecturas where cuenta = {recibo.cuenta} and an_per = {objeto.Periodo} and mes = '{objeto.Mes}'", "consumo")

                            tipoServicio = obtenerCampo($"select * from vusuario where cuenta = {recibo.cuenta}", "MEDIDO")

                            If tipoServicio = 1 Then
                                tipoServicio = "MEDIDO"
                            Else
                                tipoServicio = "FIJO"
                            End If

                            tipoUso = obtenerCampo($"select * from usuario where cuenta = {recibo.cuenta}", "ID_TIPO_USUARIO")

                            If tipoUso = 1 Then
                                tipoUso = "DOMESTICO"
                            Else
                                tipoUso = "NO DOMESTICO"
                            End If

                            Dim cadenapagomes As String = "INSERT INTO pago_mes (PERIODO, MES, ANO, CONCEPTO, FECHA, RECIBO, CAJA,SERIE, CUENTA,MONTO, DESCUENTO,MONTOPAGADO, TIPO, CONSUMO, TIPOUSO) VALUES ('" & CadenaNumeroMes(objeto.Mes) & Mid(objeto.Periodo, 3, 2) & "','" & objeto.Mes & "'," & objeto.Periodo & ",'CONSUMO','" & recibo.Fecha_Act & "'," & My.Settings.folio + 1 & ",'" & My.Settings.caja & "', '" & My.Settings.serie & "'," & recibo.cuenta & "," & objeto.Totalcondescuento & "," & objeto.Total - objeto.Totalcondescuento & "," & objeto.Totalcondescuento & ",'" & tipoServicio & "'," & consumoLectura & ",'" & tipoUso & "')"
                            Ejecucion(cadenapagomes)

                            'Dim usued8 = EjecutarConsultaRemotaAsync(cadenapagomes)



                        Catch ex As Exception
                            MessageBox.Show($"Ocurrio un error al insertar a tabla Pago_mes {ex.ToString()}")
                        End Try

                    End If
                Next


            Catch ex As Exception

                MessageBox.Show("Ocurrio un erro al grabar el desglose de consumo")

            End Try


            Try



                For i = 1 To control.desgloserezago.Count
                    If control.EsFijo = True Then

                        Dim objeto As clsunidadmes

                        objeto = control.desgloserezago.Item(i)

                        Try


                            consumoLectura = obtenerCampo($"select * from lecturas where cuenta = {recibo.cuenta} and an_per = {objeto.periodo} and mes = '{objeto.mes}'", "consumo")

                            tipoServicio = obtenerCampo($"select * from vusuario where cuenta = {recibo.cuenta}", "MEDIDO")

                            If tipoServicio = 1 Then
                                tipoServicio = "MEDIDO"
                            Else
                                tipoServicio = "FIJO"
                            End If

                            tipoUso = obtenerCampo($"select * from usuario where cuenta = {recibo.cuenta}", "ID_TIPO_USUARIO")

                            If tipoUso = 1 Then
                                tipoUso = "DOMESTICO"
                            Else
                                tipoUso = "NO DOMESTICO"
                            End If


                            Dim cadenapagomes As String = "INSERT INTO pago_mes (PERIODO, MES, ANO, CONCEPTO, FECHA, RECIBO, CAJA,SERIE, CUENTA,MONTO,DESCUENTO,MONTOPAGADO, TIPO, CONSUMO, TIPOUSO) VALUES ('" & CadenaNumeroMes(objeto.mes) & Mid(objeto.periodo, 3, 2) & "','" & objeto.mes & "'," & objeto.periodo & ",'CONSUMO ','" & recibo.Fecha_Act & "'," & My.Settings.folio + 1 & ",'" & My.Settings.caja & "', '" & My.Settings.serie & "'," & recibo.cuenta & "," & objeto.totalcondescuento & "," & objeto.total - objeto.totalcondescuento & "," & objeto.totalcondescuento & ",'" & tipoServicio & "'," & consumoLectura & ",'" & tipoUso & "')"
                            Ejecucion(cadenapagomes)

                            'Dim usued8 = EjecutarConsultaRemotaAsync(cadenapagomes)

                        Catch ex As Exception
                            MessageBox.Show($"Ocurrio un error al insertar a tabla Pago_mes {ex.ToString()}")
                        End Try

                    Else

                        Dim objeto As ClsRegistrolectura

                        objeto = control.desgloserezago.Item(i)

                        Try



                            consumoLectura = obtenerCampo($"select * from lecturas where cuenta = {recibo.cuenta} and an_per = {objeto.Periodo} and mes = '{objeto.Mes}'", "consumo")

                            tipoServicio = obtenerCampo($"select * from vusuario where cuenta = {recibo.cuenta}", "MEDIDO")

                            If tipoServicio = 1 Then
                                tipoServicio = "MEDIDO"
                            Else
                                tipoServicio = "FIJO"
                            End If

                            tipoUso = obtenerCampo($"select * from usuario where cuenta = {recibo.cuenta}", "ID_TIPO_USUARIO")

                            If tipoUso = 1 Then
                                tipoUso = "DOMESTICO"
                            Else
                                tipoUso = "NO DOMESTICO"
                            End If

                            Dim cadenapagomes As String = "INSERT INTO pago_mes (PERIODO, MES, ANO, CONCEPTO, FECHA, RECIBO, CAJA,SERIE, CUENTA,MONTO, DESCUENTO,MONTOPAGADO, TIPO, CONSUMO, TIPOUSO) VALUES ('" & CadenaNumeroMes(objeto.Mes) & Mid(objeto.Periodo, 3, 2) & "','" & objeto.Mes & "'," & objeto.Periodo & ",'CONSUMO','" & recibo.Fecha_Act & "'," & My.Settings.folio + 1 & ",'" & My.Settings.caja & "', '" & My.Settings.serie & "'," & recibo.cuenta & "," & objeto.Totalcondescuento & "," & objeto.Total - objeto.Totalcondescuento & "," & objeto.Totalcondescuento & ",'" & tipoServicio & "'," & consumoLectura & ",'" & tipoUso & "')"
                            Ejecucion(cadenapagomes)

                            'Dim usued8 = EjecutarConsultaRemotaAsync(cadenapagomes)



                        Catch ex As Exception
                            MessageBox.Show($"Ocurrio un error al insertar a tabla Pago_mes {ex.ToString()}")
                        End Try

                    End If

                Next


            Catch ex As Exception

                MessageBox.Show("Ocurrio un error al grabar el desloze de rezago")

            End Try


            Try



                For i = 1 To control.desglosealcantarillado.Count

                    If control.EsFijo = True Then
                        Dim objeto As clsunidadmes
                        objeto = control.desglosealcantarillado.Item(i)


                        consumoLectura = obtenerCampo($"select * from lecturas where cuenta = {recibo.cuenta} and an_per = {objeto.periodo} and mes = '{objeto.mes}'", "consumo")

                        tipoServicio = obtenerCampo($"select * from vusuario where cuenta = {recibo.cuenta}", "MEDIDO")

                        If tipoServicio = 1 Then
                            tipoServicio = "MEDIDO"
                        Else
                            tipoServicio = "FIJO"
                        End If

                        tipoUso = obtenerCampo($"select * from usuario where cuenta = {recibo.cuenta}", "ID_TIPO_USUARIO")

                        If tipoUso = 1 Then
                            tipoUso = "DOMESTICO"
                        Else
                            tipoUso = "NO DOMESTICO"
                        End If

                        Dim cadenapagomes As String = "INSERT INTO pago_mes (PERIODO, MES, ANO, CONCEPTO, FECHA, RECIBO, CAJA,SERIE, CUENTA,MONTO,DESCUENTO,MONTOPAGADO, TIPO, CONSUMO, TIPOUSO) VALUES ('" & CadenaNumeroMes(objeto.mes) & Mid(objeto.periodo, 3, 2) & "','" & objeto.mes & "'," & objeto.periodo & ",'ALCANTARILLADO ','" & recibo.Fecha_Act & "'," & My.Settings.folio + 1 & ",'" & My.Settings.caja & "', '" & My.Settings.serie & "'," & recibo.cuenta & "," & objeto.totalcondescuento & "," & objeto.total - objeto.totalcondescuento & "," & objeto.totalcondescuento & ",'" & tipoServicio & "'," & consumoLectura & ",'" & tipoUso & "')"
                        Ejecucion(cadenapagomes)

                        'Dim usued8 = EjecutarConsultaRemotaAsync(cadenapagomes)



                    Else


                        Dim objeto As ClsRegistrolectura
                        objeto = control.desglosealcantarillado.Item(i)


                        consumoLectura = obtenerCampo($"select * from lecturas where cuenta = {recibo.cuenta} and an_per = {objeto.Periodo} and mes = '{objeto.Mes}'", "consumo")

                        tipoServicio = obtenerCampo($"select * from vusuario where cuenta = {recibo.cuenta}", "MEDIDO")

                        If tipoServicio = 1 Then
                            tipoServicio = "MEDIDO"
                        Else
                            tipoServicio = "FIJO"
                        End If

                        tipoUso = obtenerCampo($"select * from usuario where cuenta = {recibo.cuenta}", "ID_TIPO_USUARIO")

                        If tipoUso = 1 Then
                            tipoUso = "DOMESTICO"
                        Else
                            tipoUso = "NO DOMESTICO"
                        End If

                        Dim cadenapagomes As String = "INSERT INTO pago_mes (PERIODO, MES, ANO, CONCEPTO, FECHA, RECIBO, CAJA,SERIE, CUENTA,MONTO, DESCUENTO,MONTOPAGADO, TIPO, CONSUMO, TIPOUSO) VALUES ('" & CadenaNumeroMes(objeto.Mes) & Mid(objeto.Periodo, 3, 2) & "','" & objeto.Mes & "'," & objeto.Periodo & ",'ALCANTARILLADO','" & recibo.Fecha_Act & "'," & My.Settings.folio + 1 & ",'" & My.Settings.caja & "', '" & My.Settings.serie & "'," & recibo.cuenta & "," & objeto.Totalcondescuento & "," & objeto.Total - objeto.Totalcondescuento & "," & objeto.Totalcondescuento & ",'" & tipoServicio & "'," & consumoLectura & ",'" & tipoUso & "')"
                        Ejecucion(cadenapagomes)

                        'Dim usued8 = EjecutarConsultaRemotaAsync(cadenapagomes)



                    End If
                Next


            Catch ex As Exception

                MessageBox.Show("Ocurrio un error al grabar el desglose de alcantarillado")

            End Try


            Try



                For i = 1 To control.desglosesaneamiento.Count



                    If control.EsFijo = True Then

                        Dim objeto As clsunidadmes
                        objeto = control.desglosesaneamiento.Item(i)

                        consumoLectura = obtenerCampo($"select * from lecturas where cuenta = {recibo.cuenta} and an_per = {objeto.periodo} and mes = '{objeto.mes}'", "consumo")

                        tipoServicio = obtenerCampo($"select * from vusuario where cuenta = {recibo.cuenta}", "MEDIDO")

                        If tipoServicio = 1 Then
                            tipoServicio = "MEDIDO"
                        Else
                            tipoServicio = "FIJO"
                        End If

                        tipoUso = obtenerCampo($"select * from usuario where cuenta = {recibo.cuenta}", "ID_TIPO_USUARIO")

                        If tipoUso = 1 Then
                            tipoUso = "DOMESTICO"
                        Else
                            tipoUso = "NO DOMESTICO"
                        End If

                        Dim cadenapagomes As String = "INSERT INTO pago_mes (PERIODO, MES, ANO, CONCEPTO, FECHA, RECIBO, CAJA,SERIE, CUENTA,MONTO,DESCUENTO,MONTOPAGADO, TIPO, CONSUMO, TIPOUSO) VALUES ('" & CadenaNumeroMes(objeto.mes) & Mid(objeto.periodo, 3, 2) & "','" & objeto.mes & "'," & objeto.periodo & ",'SANEAMIENTO ','" & recibo.Fecha_Act & "'," & My.Settings.folio + 1 & ",'" & My.Settings.caja & "', '" & My.Settings.serie & "'," & recibo.cuenta & "," & objeto.totalcondescuento & "," & objeto.total - objeto.totalcondescuento & "," & objeto.totalcondescuento & ",'" & tipoServicio & "'," & consumoLectura & ",'" & tipoUso & "')"
                        Ejecucion(cadenapagomes)

                        'Dim usued8 = EjecutarConsultaRemotaAsync(cadenapagomes)



                    Else


                        Dim objeto As ClsRegistrolectura
                        objeto = control.desglosesaneamiento.Item(i)

                        consumoLectura = obtenerCampo($"select * from lecturas where cuenta = {recibo.cuenta} and an_per = {objeto.Periodo} and mes = '{objeto.Mes}'", "consumo")

                        tipoServicio = obtenerCampo($"select * from vusuario where cuenta = {recibo.cuenta}", "MEDIDO")

                        If tipoServicio = 1 Then
                            tipoServicio = "MEDIDO"
                        Else
                            tipoServicio = "FIJO"
                        End If

                        tipoUso = obtenerCampo($"select * from usuario where cuenta = {recibo.cuenta}", "ID_TIPO_USUARIO")

                        If tipoUso = 1 Then
                            tipoUso = "DOMESTICO"
                        Else
                            tipoUso = "NO DOMESTICO"
                        End If


                        Dim cadenapagomes As String = "INSERT INTO pago_mes (PERIODO, MES, ANO, CONCEPTO, FECHA, RECIBO, CAJA,SERIE, CUENTA,MONTO,DESCUENTO,MONTOPAGADO, TIPO, CONSUMO, TIPOUSO) VALUES ('" & CadenaNumeroMes(objeto.Mes) & Mid(objeto.Periodo, 3, 2) & "','" & objeto.Mes & "'," & objeto.Periodo & ",'SANEAMIENTO ','" & recibo.Fecha_Act & "'," & My.Settings.folio + 1 & ",'" & My.Settings.caja & "', '" & My.Settings.serie & "'," & recibo.cuenta & "," & objeto.Totalcondescuento & "," & objeto.Total - objeto.Totalcondescuento & "," & objeto.Totalcondescuento & ",'" & tipoServicio & "'," & consumoLectura & ",'" & tipoUso & "')"

                        Ejecucion(cadenapagomes)

                        'Dim usued8 = EjecutarConsultaRemotaAsync(cadenapagomes)




                    End If

                Next


            Catch ex As Exception

                MessageBox.Show("Ocurrio un errro al grabar el desglose de saneamiento")

            End Try



            Try

                For i = 1 To control.desgloserecargo.Count
                    'If control.EsFijo = True Then
                    Dim objeto As clsunidadmes
                    objeto = control.desgloserecargo.Item(i)

                    consumoLectura = obtenerCampo($"select * from lecturas where cuenta = {recibo.cuenta} and an_per = {objeto.periodo} and mes = '{objeto.mes}'", "consumo")

                    tipoServicio = obtenerCampo($"select * from vusuario where cuenta = {recibo.cuenta}", "MEDIDO")

                    If tipoServicio = 1 Then
                        tipoServicio = "MEDIDO"
                    Else
                        tipoServicio = "FIJO"
                    End If

                    tipoUso = obtenerCampo($"select * from usuario where cuenta = {recibo.cuenta}", "ID_TIPO_USUARIO")

                    If tipoUso = 1 Then
                        tipoUso = "DOMESTICO"
                    Else
                        tipoUso = "NO DOMESTICO"
                    End If


                    Dim cadenapagomes As String = "INSERT INTO pago_mes (PERIODO, MES, ANO, CONCEPTO, FECHA, RECIBO, CAJA,SERIE, CUENTA,MONTO,DESCUENTO,MONTOPAGADO, TIPO, CONSUMO, TIPOUSO) VALUES ('" & CadenaNumeroMes(objeto.mes) & Mid(objeto.periodo, 3, 2) & "','" & objeto.mes & "'," & objeto.periodo & ",'RECARGO ','" & recibo.Fecha_Act & "'," & My.Settings.folio + 1 & ",'" & My.Settings.caja & "', '" & My.Settings.serie & "'," & recibo.cuenta & "," & objeto.totalcondescuento & "," & objeto.total - objeto.totalcondescuento & "," & objeto.totalcondescuento & ",'" & tipoServicio & "'," & consumoLectura & ",'" & tipoUso & "')"
                    Ejecucion(cadenapagomes)


                Next


            Catch ex As Exception

                MessageBox.Show("Ocurrio un error al grabar el desglose de recargos")

            End Try




            Try
                If actualizarfecha Then
                    Ejecucion("update usuario set requeri=0 where cuenta=" & recibo.cuenta)
                Else
                    ' no hubo pago por concepto de consumo
                End If
            Catch ex As Exception

            End Try



            Try
                If recibo.esusuario = 1 And control.EsMEdido Then
                    For i = 1 To control.desgloseconsumo.Count
                        Dim r As New ClsRegistrolectura
                        r = control.desgloseconsumo(i)
                        ' Dim cadenaperiodo As String = CadenaNumeroMes(r.Mes) & Mid(r.Periodo, 3, 2)
                        Dim mes As String = r.Mes
                        Dim periodo As String = r.Periodo
                        'ejecucion("UPDATE LECTURAS SET PAGADO=1 WHERE MES= '" & cadenaperiodo & "' AND CUENTA= " & recibo.cuenta)
                        Try
                            Dim cadenalect As String = "UPDATE lecturas SET PAGADO=1 WHERE MES= '" & mes & "' AND an_per=" & periodo & " AND CUENTA= " & recibo.cuenta
                            Ejecucion(cadenalect)
                            'Dim unsued9 = EjecutarConsultaRemotaAsync(cadenalect)
                        Catch ex As Exception
                            guardatxt("c:\errrpagos", "errorpagos.txt", ex.Message)
                        End Try

                    Next
                    For i = 1 To control.desgloserezago.Count
                        Dim r As New ClsRegistrolectura
                        r = control.desgloserezago(i)
                        'Dim cadenaperiodo As String = CadenaNumeroMes(r.Mes) & Mid(r.Periodo, 3, 2)
                        'ejecucion("UPDATE LECTURAS SET PAGADO=1 WHERE MES= '" & cadenaperiodo & "' AND CUENTA= " & recibo.cuenta)
                        Dim mes As String = r.Mes
                        Dim periodo As String = r.Periodo
                        Try
                            Dim cadenalect As String = "UPDATE lecturas SET PAGADO=1 WHERE MES= '" & mes & "' AND an_per=" & periodo & " AND CUENTA= " & recibo.cuenta
                            Ejecucion(cadenalect)
                            'Dim unsued9 = EjecutarConsultaRemotaAsync(cadenalect)

                        Catch ex As Exception
                            guardatxt("c:\errrpagos", "errorpagos.txt", ex.Message)
                        End Try

                    Next

                End If
            Catch ex As Exception

            End Try


            Dim conceptootro As New Clsconcepto
            For i = 1 To control.Listadeconceptos.Count
                conceptootro = control.Listadeconceptos.Item(i)
                If conceptootro.CLAVEMOV > 0 Then
                    ' Caja.dtgConceptos.Rows.Add(concepto.Concepto)
                    Dim BASEm As New base
                    Dim RESTA, SUBTOTRESTA, IVA As Double
                    Dim estado As String = "ABONADO"
                    Double.TryParse(BASEm.obtenerCampo("select resta from otrosconceptos where clave=" & conceptootro.CLAVEMOV, "RESTA"), RESTA)
                    Double.TryParse(BASEm.obtenerCampo("select subtotresta as RESTA from otrosconceptos where clave=" & conceptootro.CLAVEMOV, "RESTA"), SUBTOTRESTA)
                    Dim PAGADO As Integer = 0
                    IVA = 0
                    RESTA = Math.Round(RESTA - (conceptootro.importe + conceptootro.IVA), 2)
                    SUBTOTRESTA = Math.Round(SUBTOTRESTA - conceptootro.importe, 2)
                    If conceptootro.IVA > 0 Then
                        IVA = Math.Round(SUBTOTRESTA * (variable_iva / 100), 2)
                    End If
                    If RESTA <= 0 Then
                        RESTA = 0
                        PAGADO = 1
                        estado = "APLICADO"
                    End If

                    'Ejecutar actualización de resta de notificaciones, 08/11/2021


                    Dim cadenaotrosconceptos = "UPDATE otrosconceptos SET resta=" & RESTA & ",subtotresta=" & SUBTOTRESTA & ",ivaresta=" & IVA & " , pagado=" & PAGADO & ", estado='" & estado & "'  WHERE Clave=" & conceptootro.CLAVEMOV & ";"

                    Ejecucion(cadenaotrosconceptos)
                    'Dim unsed10 = EjecutarConsultaRemotaAsync(cadenaotrosconceptos)

                    Dim datopag As Decimal

                    Dim cadena As String
                    Dim idconvenio As Integer
                    cadena = "select idEncConvenio from EncConvenio where idcuenta =" & recibo.cuenta
                    Dim dr As IDataReader = ConsultaSql(cadena).ExecuteReader()
                    If dr.Read() Then
                        idconvenio = dr("idEncConvenio")
                    End If

                    Try
                        datopag = obtenerCampo("select sum(resta) as resta from otrosconceptos where folio=" & idconvenio, "Resta")

                    Catch ex As Exception

                    End Try


                    Try
                        If datopag = 0 Then

                            Dim cadenaconv1 As String = "update encconvenio set estado='Finalizado' where idencconvenio= " & idconvenio
                            Dim cadenaconv2 As String = "update usuario set convenio = 0 where cuenta =" & recibo.cuenta
                            Ejecucion(cadenaconv1)
                            Ejecucion(cadenaconv2)
                            'Dim unused11 = EjecutarConsultaRemotaAsync(cadenaconv1)
                            'Dim unused12 = EjecutarConsultaRemotaAsync(cadenaconv2)
                        End If


                    Catch ex As Exception

                    End Try



                    BASEm.conexion.Dispose()
                End If

            Next

            ''''' aqui se imprime ''''''''''''''''''''''''''''''''

            Try
                If FACTURADO = 0 Then
                    '  imprime = New clsimprimeformato()
                    '  imprimerecibo(My.Settings.folio + 1, My.Settings.serie)
                    '' Dim TIC As New Ticket
                    ''TIC.imprime_ticket58mm(My.Settings.serie, My.Settings.folio + 1, False, cambio, vale) 'false directo a la impresora o true a la ventana

                    Dim recibo As New reciboaimprimir
                    recibo.ReciboHojaCarta(My.Settings.serie, My.Settings.folio + 1, True, cambio, formaPago, vale)
                    'recizurich.imprime(My.Settings.serie, My.Settings.folio + 1, False, cambio, vale)
                    ''TIC.imprime_ticket58mm(My.Settings.serie, My.Settings.folio + 1, False, cambio, vale) 'false directo a la impresora o true a la ventana
                End If
            Catch ex As Exception
                MessageBox.Show("Fallas en la impresion")
            End Try





            My.Settings.folio = My.Settings.folio + 1
            My.Settings.Save()

            My.Settings.Reload()

            If recibo.esusuario Then

                Try
                    Dim datos As Odbc.OdbcDataReader
                    Dim total As Double
                    datos = ConsultaSql("select * from usuario USU inner join descuentos DES on(USU.idDescuento=DES.idDescuento) where cuenta=" & recibo.cuenta & "").ExecuteReader
                    datos.Read()
                    Dim pago As New Clscontrolpago
                    pago.cuenta = recibo.cuenta
                    pago.Tarifa = datos("Tarifa").ToString()
                    pago.Fechafinal = Now.AddMonths(-1)
                    pago.Fechainicio = datos("deudafec")
                    'pasar los parametros si contiene descuento, alcantarrillado y saneamiento

                    pago.saneamiento = datos("Saneamiento")
                    pago.alcantarillado = datos("alcantarillado")
                    pago.valvulista = datos("idCuotaValvulista")

                    If datos("idDescuento") > 0 Then

                        ''------ Descuentos
                        Try
                            pago.descontartodoslosperiodosdeconsumo = True
                        Catch ex As Exception

                        End Try

                        Try
                            pago.periodoscondescuentodeconsumo = Month(Now)
                        Catch ex As Exception

                        End Try

                        Try
                            pago.descuentoaconsumo = datos("nPctDsct")
                        Catch ex As Exception

                        End Try
                        ''------
                    Else
                        Try
                            pago.descontartodoslosperiodosdeconsumo = False
                        Catch ex As Exception

                        End Try

                        Try
                            pago.periodoscondescuentodeconsumo = 0
                        Catch ex As Exception

                        End Try

                        Try
                            pago.descuentoaconsumo = 0
                        Catch ex As Exception

                        End Try
                    End If



                    pago.calcula(False, False, Now)


                    'Dim concepto As New Clsconcepto
                    'Try

                    '    concepto = pago.Listadeconceptos.Item("PAGO VALVULISTA")
                    'Catch ex As Exception

                    'End Try

                    ' Rectifica el iva en los conceptos
                    Dim acumiva As Double = 0

                    For index = 1 To pago.Listadeconceptos.Count
                        Dim conce As New Clsconcepto
                        conce = pago.Listadeconceptos(index)
                        If conce.IVA > 0 Then
                            conce.IVA = Math.Round((conce.Cantidad * conce.Preciounitario) * (variable_iva / 100), 2)
                        End If
                        acumiva += conce.IVA
                    Next

                    Dim rezagoagua As Decimal = 0 ' esto es esclusivo  par santa rosalia
                    Dim consumo As Decimal = 0
                    Dim alcaactual As Decimal = 0
                    Dim rezagoalca As Decimal = 0
                    Try

                        Try
                            Dim objeto As Object = pago.desgloseconsumo.Item(1)
                            consumo = objeto.total
                            rezagoagua = pago.totaldeudaconsumo - objeto.total
                        Catch ex As Exception
                            If pago.totaldeudaconsumo > 0 Then
                                rezagoagua = pago.totaldeudaconsumo
                            End If
                        End Try



                        Try
                            Dim objetoalca As Object = pago.desglosealcantarillado.Item(pago.desglosealcantarillado.Count)
                            alcaactual = objetoalca.total
                            rezagoalca = pago.totaldeudaalcantarillado - objetoalca.total
                        Catch ex As Exception
                            alcaactual = 0
                            rezagoalca = 0
                        End Try

                    Catch ex As Exception
                        'MessageBox.Show(ex.Message)
                        rezagoagua = 0
                        consumo = 0

                    End Try '' hasta aqui es ara santa rosalia


                    total = Math.Round(pago.totaldeudaconsumo, 2) + pago.totaldeudaalcantarillado + pago.totaldeudasaneamiento + acumiva + pago.totaldeudarecargos + pago.totaldeudaotros

                    Dim cadenasaldo As String = "update usuario set consumo=" & consumo & ", deuda=" & rezagoagua & " ,alcaconsumo=" & alcaactual & ", deualcant=" & rezagoalca & ", deudasanea=" & pago.totaldeudasaneamiento & ", iva=" & acumiva & ", recargos =" & pago.totaldeudarecargos & ", deudaotros=" & pago.totaldeudaotros & ", total=" & total & ", LecturaAct=UltimaLectura(" & recibo.cuenta & "), LecturaAnt=PenUltimaLectura(" & recibo.cuenta & "), PeriodosAdeudo=" & pago.desgloseconsumo.Count + pago.desgloserezago.Count & ",periodo='" & pago.periodo & "' where cuenta=" & recibo.cuenta & ""
                    Ejecucion(cadenasaldo)

                    'Dim UNSED14 = EjecutarConsultaRemotaAsync(cadenasaldo)
                    ' Ejecucion("update usuario set deuda=" & pago.totaldeudaconsumo & " , deualcant=" & pago.totaldeudaalcantarillado & ", deudasanea=" & pago.totaldeudasaneamiento & ", iva=" & acumiva & ", recargos =" & pago.totaldeudarecargos & ", deudaotros=" & pago.totaldeudaotros & ", total=" & total & ", LecturaAnt=UltimaLecturaActualizar('" & recibo.cuenta & "'), PeriodosAdeudo=" & pago.desgloseconsumo.Count + pago.desgloserezago.Count & ",periodo='" & pago.periodo & "'  where cuenta=" & recibo.cuenta)
                    datos.Close()
                Catch ex As Exception

                End Try
            End If

            Caja.limpiar()
            ReiniciarDescuentos()


            Close()

        End If


    End Sub

    Public Function DatosRecibo() As DataSet
        Try

            Dim Conex As New OdbcConnection(My.Settings.baseaguaConnectionString)

            Conex.Open()
            Dim query As String = "SELECT vusuario.NOMBRE, vusuario.CALLE, vusuario.COLONIA, vusuario.COMUNIDAD, vusuario.numext, vusuario.cp, pagos.periodo, pagos.total, pagos.recibo, pagotros.concepto, pagotros.importe, pagotros.cantidad FROM vusuario inner join pagos on (pagos.cuenta= vusuario.cuenta) inner join pagotros on(pagotros.cuenta=vusuario.cuenta and pagotros.recibo= pagos.recibo) where pagos.Recibo=" & My.Settings.folio + 1
            Dim comando As New OdbcCommand(query, Conex)
            Dim Adaptador As New OdbcDataAdapter(comando)
            Dim commandBuilder As New OdbcCommandBuilder(Adaptador)
            Dat = New DataSet()
            Adaptador = New OdbcDataAdapter(comando)
            Adaptador.Fill(Dat, "Datos")

            Conex.Close()


            Return Dat

        Catch err As Exception

        End Try
        Return New DataSet
    End Function

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        imprime.desconectarhilosdeimpresora()
        Close()
    End Sub

    Private Sub frmPagoefectivo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim basem As New base
        If FORMAFACTURADO = "" Then ' NO VIENE DE LA FACTURACION ELECTRONICA
            Dim dato As Odbc.OdbcDataReader = basem.consultasql("select * from fpago limit 1")
            If dato.Read Then
                lblFormadepago.Text = dato("ccodpago")
                lbltotal_pagar.Text = totalapagar.ToString("C")
                lblvale.Text = vale.ToString("C")
                lbltotal.Text = total.ToString("C")


            End If
            dato = basem.consultasql("select * from pagos where serie='" & My.Settings.serie & "' and recibo=" & My.Settings.folio + 1 & " limit 1")
            If dato.Read Then
                MessageBox.Show("El folio que vas imprimir ya existe")
                basem.conexion.Dispose()
                Close()
            End If
            basem.conexion.Dispose()
        Else ' VIENE DE LA FACTURACION

            lblFormadepago.Text = FORMAFACTURADO
            '  Dim dato As Odbc.OdbcDataReader
            If FORMAFACTURADO = "x" Then
                ' MessageBox.Show("El folio que vas imprimir ya existe")
                basem.conexion.Dispose()
                Close()

            End If
            Try
                txtefectivo_recibido.Text = "0"
                lblcambio.Text = ""

                basem.conexion.Dispose()
            Catch ex As Exception

            End Try
        End If
        basem.conexion.Dispose()
        If FACTURADO > 0 Then
            btncobrar_imprimir_Click(sender, e)
        End If
    End Sub



    Public Function describirforma(id As String) As String
        If id = "01" Then
            Return "EFECTIVO"
        End If
        If id = "02" Then
            Return "CHEQUE"
        End If
        If id = "03" Then
            Return "TRASFERENCIA"
        End If
        If id = "04" Then
            Return "TARJETA DE CREDITO"
        End If
        If id = "28" Then
            Return "TARJETA DE DEBITO"
        End If
        Return "EFECTIVO"
    End Function

    Private Sub lblFormadepago_TextChanged(sender As Object, e As EventArgs) Handles lblFormadepago.TextChanged
        lblformadescrita.Text = describirforma(lblFormadepago.Text)
    End Sub

    Private Sub btnabonar_Click(sender As Object, e As EventArgs) Handles btnabonar.Click
        nuevovale = cambio
        cambio = 0
        lblcambio.Text = cambio.ToString("C")
        lbltotalvale.Text = nuevovale.ToString("C")
    End Sub

    ' Método para reiniciar descuentos
    Public Sub ReiniciarDescuentos()


        ' Reiniciar valores numero de periodos

        control.descuentoaconsumo = 0
        control.descontartodoslosperiodosderecargo = 0
        control.descontartodoslosperiodosdeconsumo = 0
        control.descontartodoslosperiodosdealcantarillado = 0
        control.descontartodoslosperiodosdesaneamiento = 0
        control.descontartodoslosperiodosderezago = 0
        control.periodoscondescuentodeconsumo = 0
        control.periodoscondescuentodealcantarillado = 0
        control.periodoscondescuentodesaneamiento = 0
        control.periodoscondescuentoderecargo = 0
        control.periodoscondescuentoderezago = 0



        ' Reiniciar valores de porcentaje

        control.descuentoaconsumo = 0
        control.descuentoaalcantarillado = 0
        control.descuentoasaneamiento = 0
        control.descuentoasaneamiento = 0
        control.descuentoarecargo = 0
        control.descuentoarezago = 0

    End Sub

End Class



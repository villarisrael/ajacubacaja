Imports System.Data.Odbc
Imports System.IO

Imports System.Net
Imports System.Net.Mail

Imports System.Text
Imports System.Text.RegularExpressions


Imports MultiFacturasSDK
Imports CAJAS
Imports System.Xml
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class Frmvalidafactura
    Public recibo As New clsrecibo
    Public control As New Clscontrolpago
    Dim sdkresp As SDKRespuesta

    Private archivotexto As String
    Private archivopdf As String
    Private strStreamW As Stream = Nothing
    Private strStreamWriter As StreamWriter = Nothing

    Public subtotal As Decimal
    Public DescRecargo As Decimal
    Public iva As Decimal
    Public total As Decimal

    Public cuenta As Integer
    Public TIPO As String

    Public quehacer As String = "INSERTAR"
    Public facturado As Long = 0 ' para factupronto
    Public formafacturado As String

    Public actualizarfecha As Boolean = True
    Public seriefactura As String = "" ' para multifacturas 
    Public foliofactura As Long = 0 ' para multifacturas

    Public Numerodecaja As Int16 = 1
    'Public Reciboqueseestafacturando As Int16 = 0
    Public Reciboqueseestafacturando As Long = 0
    Public seriedelreciboqueseestafacturando As String

    Private dts As OdbcDataReader
    Private imprime As clsimprimeformato
    Public vienede As String = "CAJA"
    Public esusua As Integer
    Dim tipousuario As Int16


    Private Sub Frmvalidafactura_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        conectar()

        Dim dato As OdbcDataReader
        ' dato = basem.consultasql("select * from pagos where serie='" & My.Settings.serie & "' and recibo=" & My.Settings.folio + 1 & " limit 1")
        dato = ConsultaSql("select * from pagos where serie='" & My.Settings.serie & "' and recibo=" & My.Settings.folio + 1 & " limit 1").ExecuteReader
        If dato.Read Then
            MessageBox.Show("El folio que vas imprimir ya existe")

            formafacturado = "x"
            Close()

        Else

            llenarCombo(cmbbanco, "SELECT RFC,Nombre from c_banco order by Nombre ")

            llenarCombo2(cmbRegimen, "SELECT ClaveRF,Descripcion FROM regimenfiscal")
            llenarCombo(cmbformapago, "SELECT CODFISCAL,CONcAT(CODFISCAL,' | ',CDESPAGO) FROM fpago ")
            cmbformapago.SelectedIndex = 1
            llenarCombo(cmbuso, "SELECT c_UsoCFDI,CONCAT(c_UsoCFDI,' | ',Descripcion) fROM c_usocfdi")
            llenarCombo(cmbmetodo, "SELECT c_MetodoPago, Descripcion FROM c_metodopago")
            cmbmetodo.SelectedValue = "PUE"
            cmbuso.SelectedValue = "S01"

            cmbRegimen.SelectedValue = "616"



            '    Select Case TIPO.ToUpper
            '        Case TIPO.ToUpper = "usuario"
            '            tipousuario = 1
            '' The following is the only Case clause that evaluates to True.
            '        Case TIPO.ToUpper = "no usuario" Or TIPO = "cliente"
            '            tipousuario = 2
            '        Case TIPO.ToUpper = "solicitud"
            '            tipousuario = 3

            '    End Select
            TIPO = TIPO.ToUpper

            If TIPO.ToUpper = "USUARIO" Then
                tipousuario = 1
            ElseIf TIPO.ToUpper = "NO USUARIO" Or TIPO = "CLIENTE" Then

                tipousuario = 2
            ElseIf TIPO.ToUpper = "FACTIBILIDAD" Then


                tipousuario = 3

            End If




        End If


        Dim datosf As OdbcDataReader = ConsultaSql("SELECT * FROM datosfiscales WHERE CUENTA=" & cuenta & " AND TIPO='" & tipousuario & "'").ExecuteReader

        If datosf.Read Then
            quehacer = "ACTUALIZAR"
            Try
                txtnombre.Text = datosf("NOMBRE")
                txtcalle.Text = datosf("CALLE")
                txtnumext.Text = datosf("NUMEXT")
                txtnuminterior.Text = datosf("NUMINT")
                txtcolonia.Text = datosf("COLONIA")
                txtdelegacion.Text = datosf("DELEGACION")
                txtpoblacion.Text = datosf("POBLACION")
                txtestado.Text = datosf("ESTADO")
                txtPais.Text = datosf("PAIS")
                txtcp.Text = datosf("CP")
                txtrfc.Text = datosf("RFC")
                txtmail.Text = datosf("MAILDEENVIO")
                txtcuentabancaria.Text = datosf("cuentabancaria")
                cmbbanco.SelectedValue = datosf("rfcbanco")
                cmbRegimen.SelectedValue = datosf("RegimenFiscal")
                'txtObservaciones.Text = "Cuenta: " & datosU("cuenta") & " Tarifa: " & datosU("descripcion_cuota") & " Medidor: " & datosU("nodemedidor")
            Catch ex As Exception


            End Try
            TIPO = TIPO.ToUpper
            'MessageBox.Show(TIPO)
            If TIPO = "USUARIO" Then
                Dim datosU As OdbcDataReader = ConsultaSql("SELECT * FROM vusuario WHERE CUENTA=" & cuenta).ExecuteReader
                datosU.Read()


                txtObservaciones.Text = " Tarifa: " & datosU("descripcion_cuota") & " Medidor: " & datosU("nodemedidor")

                Dim datosl As OdbcDataReader = ConsultaSql("SELECT ultimoconsumo(" & cuenta & ") AS CONSUMO").ExecuteReader
                datosl.Read()
                lbltipodeusuario.Text = "1"
                txtObservaciones.Text += "  ULT CON: " & datosl("CONSUMO") & " M3"
                datosU.Close()
            ElseIf TIPO = "NO USUARIO" Or TIPO = "CLIENTE" Then
                Dim datosU As OdbcDataReader = ConsultaSql("SELECT * FROM nousuarios WHERE clave=" & cuenta).ExecuteReader
                datosU.Read()
                lbltipodeusuario.Text = "2"
                txtObservaciones.Text = "CLIENTE FUERA DEL PADRON NO: " & datosU("CLAVE")
                datosU.Close()
            ElseIf TIPO = "SOLICITUD" Then
                Dim datosU As OdbcDataReader = ConsultaSql("SELECT * FROM solicitud WHERE numero=" & cuenta).ExecuteReader
                datosU.Read()
                lbltipodeusuario.Text = "3"
                txtObservaciones.Text = "Solicitud: " & datosU("NUMERO")
            End If

        Else
            TIPO = TIPO.ToUpper

            If TIPO = "USUARIO" Then
                Dim datosU As OdbcDataReader = ConsultaSql("SELECT * FROM vusuario WHERE CUENTA=" & cuenta).ExecuteReader
                datosU.Read()
                txtnombre.Text = datosU("NOMBRE")
                txtcalle.Text = datosU("DIRECCION")
                txtnumext.Text = ""
                txtnuminterior.Text = ""
                txtcolonia.Text = datosU("COLONIA")
                txtdelegacion.Text = "" 'datosf("DELEGACION")
                txtpoblacion.Text = datosU("COMUNIDAD")
                txtestado.Text = "HIDALGO" '
                txtPais.Text = "MEXICO" 'datosf("PAIS")
                txtcp.Text = "42500" ' datosf("CP")
                If IsDBNull(datosU("cp")) Then

                    txtcp.Text = "23920"
                ElseIf datosU("cp") = "" Or datosU("cp") = "0" Then
                    txtcp.Text = "23920"
                End If
                Try
                    If IsDBNull(datosU("RFC")) Or datosU("RFC") = "NULL" Then
                        txtrfc.Text = "XAXX010101000"
                    ElseIf datosU("rfc") = "" Then
                        txtrfc.Text = "XAXX010101000"
                    Else
                        txtrfc.Text = "" & datosU("RFC")
                    End If
                Catch ex As Exception

                End Try

                txtObservaciones.Text = ""
                'If IsDBNull(datosU("MAILDEENVIO")) Then

                Dim datosl As OdbcDataReader = ConsultaSql("SELECT ultimoconsumo(" & cuenta & ") AS CONSUMO").ExecuteReader
                datosl.Read()

                txtObservaciones.Text += "  ULT CON: " & datosl("CONSUMO") & " M3"

                txtmail.Text = ""
                'End If
            ElseIf TIPO = "NO USUARIO" Or TIPO = "CLIENTE" Then
                Dim datosU As OdbcDataReader = ConsultaSql("SELECT * FROM nousuarios WHERE clave=" & cuenta).ExecuteReader
                datosU.Read()
                txtnombre.Text = datosU("NOMBRE")
                txtcalle.Text = datosU("DIRECCION")
                txtnumext.Text = datosU("NUMEXT")
                txtnuminterior.Text = ""
                txtcolonia.Text = datosU("COLONIA")
                txtdelegacion.Text = "" 'datosf("DELEGACION")
                txtpoblacion.Text = datosU("COMUNIDAD")
                txtestado.Text = "HIDALGO" '
                txtPais.Text = "MEXICO" 'datosf("PAIS")
                txtcp.Text = "CP" ' datosf("CP")
                Try
                    If IsDBNull(datosU("RFC")) Or datosU("RFC") = "NULL" Then
                        txtrfc.Text = "XAXX010101000"
                    ElseIf datosU("rfc") = "" Then
                        txtrfc.Text = "XAXX010101000"
                    Else
                        txtrfc.Text = "" & datosU("RFC")
                    End If
                Catch ex As Exception

                End Try


                txtmail.Text = ""


                If IsDBNull(datosU("cp")) Then

                    txtcp.Text = "23920"
                ElseIf datosU("cp") = "" Or datosU("cp") = "0" Then
                    txtcp.Text = "23920"
                End If
                txtObservaciones.Text = "CLIENTE FUERA DEL PADRON NO: " & datosU("CLAVE")
                datosU.Close()
            ElseIf TIPO = "SOLICITUD" Then
                Dim datosU As OdbcDataReader = ConsultaSql("SELECT * FROM solicitud WHERE numero=" & cuenta).ExecuteReader
                datosU.Read()
                txtnombre.Text = datosU("NOMBRE")
                txtcalle.Text = datosU("DOMICILIO")
                txtnumext.Text = datosU("NUMEXT")
                txtnuminterior.Text = datosU("NUMINT")
                txtcolonia.Text = "" 'datosU("COLONIA")
                txtdelegacion.Text = "" 'datosf("DELEGACION")
                txtpoblacion.Text = "" 'datosU("COMUNIDAD")
                txtestado.Text = "HIDALGO" '
                txtPais.Text = "MEXICO" 'datosf("PAIS")
                txtcp.Text = "" ' datosf("CP")
                If IsDBNull(datosU("cp")) Then
                    txtcp.Text = "23920"
                End If
                txtrfc.Text = "XAXX010101000" 'datosU("RFC")
                txtmail.Text = "" 'datosf("MAILDEENVIO")
                txtObservaciones.Text = "Solicitud: " & datosU("NUMERO")
                datosU.Close()
            End If
        End If


        If txtrfc.Text <> "XAXX010101000" Then
            chkmandarmail.Checked = True
        End If
        'basem.conexion.Dispose()
    End Sub


    Private Sub btnaceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaceptar.Click


        btnaceptar.Enabled = False
        BTNGUARDAR_Click(sender, e)


        Dim cadena As String = ""

        If txtnombre.Text = "" Then
            cadena = "Nombre "
        End If


        If txtcalle.Text = "" Then
            cadena = cadena & "Calle" & vbCrLf
        End If


        If txtcolonia.Text = "" Then
            cadena = cadena & "COLONIA" & vbCrLf
        End If


        If txtpoblacion.Text = "" Then
            cadena = cadena & "POBLACION" & vbCrLf
        End If


        If txtestado.Text = "" Then
            cadena = cadena & "ESTADO" & vbCrLf
        End If


        If txtPais.Text = "" Then
            txtPais.Text = "MEXICO" & vbCrLf
        End If


        If txtcp.Text = "" Then
            cadena = cadena & "CP" & vbCrLf
        End If


        If txtrfc.Text = "" Then
            cadena = cadena & "RFC" & vbCrLf
        End If


        If txtmail.Text.Trim = "" Then
            cadena = cadena & "MAIL" & vbCrLf
        End If


        If cadena.Length > 0 Then
            MessageBox.Show("LOS CAMPOS " & cadena & " NO PUEDEN IR VACIOS")
            btnaceptar.Enabled = True
            Exit Sub
        End If

        'Try
        '    If My.Computer.Network.Ping("www.google.com", 1000) Then



        '    Else
        '        MessageBox.Show("No dispones de conexión a internet para poder timbrar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '        btnaceptar.Enabled = True
        '        Exit Sub
        '    End If
        'Catch ex As Exception

        'End Try

        Dim formaPago As String = cmbformapago.SelectedValue


        If vienede = "CAJA" Then

            If chksinrecibo.Checked = False Then

                grabareimprimir(formaPago, False)

            End If

        End If



        Try


            If chkmandarmail.Checked Then

                Timbrar(True)

            Else

                Timbrar(False)

            End If

        Catch ex As Exception

            MessageBox.Show($"{ex.ToString()}")

        End Try


        btnaceptar.Enabled = True

    End Sub


    Private Sub BTNGUARDAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNGUARDAR.Click
        Dim CADENA As String = ""

        If validarEmail(txtmail.Text) = True Then

            Select Case quehacer
                Case "INSERTAR"
                    CADENA = "INSERT INTO datosfiscales SET TIPO='" & tipousuario & "', CUENTA=" & cuenta & ","
                Case "ACTUALIZAR"
                    CADENA = "UPDATE datosfiscales SET "
            End Select
            CADENA = CADENA & " NOMBRE ='" & txtnombre.Text.Replace("'", "''").Trim() & "', CALLE='" & txtcalle.Text.Trim() & "',NUMEXT='" & txtnumext.Text.Trim() & "', NUMINT ='" & txtnuminterior.Text.Trim() & "', COLONIA='" & txtcolonia.Text.Trim() & "', POBLACION='" & txtpoblacion.Text.Trim() & "' , DELEGACION='" & txtdelegacion.Text.Trim() & "', ESTADO='" & txtestado.Text.Trim() & "', PAIS='" & txtPais.Text.Trim() & "', CP='" & txtcp.Text.Trim() & "',RFC='" & txtrfc.Text.Trim() & "', MAILdeenvio='" & txtmail.Text.Trim() & "', RFCBANCO='" & cmbbanco.SelectedValue & "', CUENTABANCARIA='" & txtcuentabancaria.Text.Trim() & "', REGIMENFISCAL='" & cmbRegimen.SelectedValue & "'"

            If quehacer = "ACTUALIZAR" Then
                CADENA = CADENA & " WHERE CUENTA=" & cuenta & " AND TIPO ='" & TIPO & "'"
            End If

            Ejecucion(CADENA)

        Else
            MsgBox("El e-mail no es correcto, favor de validarlo o ingresarlo nuevamente.")
        End If


    End Sub

    Private Sub btncancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancelar.Click
        Close()
    End Sub

#Region "FACTUPRONTO"
    'Private Sub MakeFileToSend() ' PARA FACTUPRONTO
    '    Dim xFactura As String = String.Empty

    '    Dim UserCfdi As String, PwdCfdi As String, CadenaUser As String = String.Empty, CadenaPwd As String = String.Empty

    '    Dim basem As New base
    '    Try

    '        Dim consultasicofi As OdbcDataReader
    '        consultasicofi = basem.consultasql("select * from sicofi")

    '        consultasicofi.Read()

    '        UserCfdi = consultasicofi("usuario")
    '        PwdCfdi = consultasicofi("password")
    '        CadenaUser = "" & UserCfdi.ToString.Trim
    '        CadenaPwd = "" & PwdCfdi.ToString.Trim

    '        'escribimos en el archivo
    '        xFactura = xFactura & "[CFD]" & vbCrLf
    '        xFactura = xFactura & "SERIE;A" & vbCrLf
    '        xFactura = xFactura & "HORA;" & Format(Date.Now, "HH:mm:ss") & vbCrLf
    '        xFactura = xFactura & "TIPODECOMPROBANTE;ingreso" & vbCrLf
    '        xFactura = xFactura & "TIPODECAMBIO;1" & vbCrLf
    '        xFactura = xFactura & "OBSERVACIONES;" & txtObservaciones.Text & vbCrLf
    '        xFactura = xFactura & "FORMADEPAGO;PAGO EN UNA SOLA EXHIBICION" & vbCrLf
    '        xFactura = xFactura & "CONDICIONESDEPAGO;CONTADO" & vbCrLf
    '        xFactura = xFactura & "METODODEPAGO;" & cmbformapago.Text & vbCrLf
    '        xFactura = xFactura & "LUGARDEEXPEDICION;" & obtenerCampo("SELECT CPOBLACION FROM EMPRESA", "CPOBLACION") & vbCrLf
    '        xFactura = xFactura & "SUBTOTAL;" & subtotal & vbCrLf
    '        xFactura = xFactura & "DESCUENTO;0" & vbCrLf
    '        xFactura = xFactura & "TOTAL;" & total & vbCrLf
    '        xFactura = xFactura & "MONEDA;MXP" & vbCrLf
    '        xFactura = xFactura & "[RECEPTOR]" & vbCrLf
    '        xFactura = xFactura & "RFC;" & txtrfc.Text.ToString.Trim & vbCrLf
    '        xFactura = xFactura & "RAZONSOCIAL;" & txtnombre.Text.ToString.Trim & vbCrLf
    '        xFactura = xFactura & "CALLE;" & txtcalle.Text.ToString.Trim & vbCrLf
    '        xFactura = xFactura & "NUMEXT;" & txtnumext.Text & vbCrLf
    '        xFactura = xFactura & "NUMINT;" & txtnuminterior.Text & vbCrLf
    '        xFactura = xFactura & "COLONIA;" & txtcolonia.Text.ToString.Trim & vbCrLf
    '        xFactura = xFactura & "CIUDAD;" & txtpoblacion.Text.ToString.Trim & vbCrLf
    '        xFactura = xFactura & "DELEG;" & txtdelegacion.Text.ToString.Trim & vbCrLf
    '        xFactura = xFactura & "CP;" & txtcp.Text & vbCrLf
    '        xFactura = xFactura & "ESTADO;" & txtestado.Text & vbCrLf
    '        xFactura = xFactura & "PAIS;" & txtPais.Text & vbCrLf
    '        xFactura = xFactura & "EMAIL1;" & txtmail.Text & vbCrLf
    '        xFactura = xFactura & "EMAIL2;" & vbCrLf
    '        For i = 1 To control.Listadeconceptos.Count
    '            Dim concepto As Clsconcepto = control.Listadeconceptos.Item(i)
    '            Dim sIva As String = IIf(concepto.IVA = 0, "0.00", variable_iva.ToString())
    '            Dim sPSIva As String = "$" & Math.Round(concepto.Preciounitario, 2)
    '            Dim sPrecio As String
    '            Dim sImporte As String
    '            If concepto.IVA = 0 Then
    '                sPrecio = "$" & Math.Round((concepto.Preciounitario), 2)
    '                sImporte = "$" & Math.Round(concepto.importe, 2)
    '            Else
    '                sPrecio = "$" & Math.Round((concepto.Preciounitario + (concepto.Preciounitario * (variable_iva / 100))), 2)
    '                sImporte = "$" & Math.Round((concepto.importe + (concepto.importe * (variable_iva / 100))), 2)
    '            End If


    '            xFactura = xFactura & "[CONCEPTO]" & vbCrLf
    '            xFactura = xFactura & "NOIDENTIFICACION;" & i & vbCrLf
    '            xFactura = xFactura & "CANTIDAD;" & concepto.Cantidad & vbCrLf
    '            xFactura = xFactura & "UNIDAD;SERV" & vbCrLf
    '            xFactura = xFactura & "DESCRIPCION;" & concepto.Concepto & vbCrLf
    '            xFactura = xFactura & "PRECIO;" & sPrecio & vbCrLf
    '            xFactura = xFactura & "PRECIOSIVA;" & sPSIva & vbCrLf
    '            xFactura = xFactura & "IMPORTE;" & sImporte & vbCrLf
    '            xFactura = xFactura & "IVA;" & sIva & vbCrLf
    '            xFactura = xFactura & "DESCUENTO;0" & vbCrLf
    '        Next
    '        xFactura = xFactura & "[IMPUESTO_RETENIDO]" & vbCrLf
    '        xFactura = xFactura & "TIPO;" & vbCrLf
    '        xFactura = xFactura & "IMPORTE;0.00" & vbCrLf
    '        xFactura = xFactura & "[IMPUESTO_TRASLADADO];" & vbCrLf
    '        xFactura = xFactura & "TIPO;IVA" & vbCrLf
    '        xFactura = xFactura & "TASA;16.00" & vbCrLf
    '        xFactura = xFactura & "IMPORTE;" & iva & vbCrLf
    '    Catch ex As Exception
    '        MessageBox.Show("problema antes de enviar factura el codigo de factura " & vbCrLf & ex.Message)
    '    End Try
    '    Try
    '        facturado = WebServiceToInvoice(xFactura, CadenaUser, CadenaPwd, txtnombre.Text)
    '        formafacturado = cmbformapago.SelectedValue
    '    Catch ex As Exception
    '        MessageBox.Show("problema al enviar factura  " & vbCrLf & ex.Message)
    '    End Try

    '    basem.conexion.Dispose()



    'End Sub ' para factupronto

    Public Sub grabarfactura()
        If facturado > 0 Then
            Ejecucion("insert into encfac SET FECHA=CONCAT('" & Now.Year & "-" & Now.Month & "-" & Now.Day & "', ' ' ,curtime()), SERIE='" & My.Settings.serie & "', numero =" & facturado & ",NOMBRE='" & recibo.nombre & "',  SUBTOTAL=" & recibo.subtotal & ",IVA=" & recibo.iva & ",TOTAL=" & recibo.total & ",TIPO='" & recibo.esusuario & "', ESTADO='A', CAJA='" & My.Settings.caja & "', USUARIO='" & usuariodelsistema & "', motivocancelacion=''")
            For i = 1 To control.Listadeconceptos.Count
                Dim concepto As New Clsconcepto
                concepto = control.Listadeconceptos(i)
                Dim coniva As Boolean = False
                If concepto.IVA > 0 Then
                    coniva = True
                End If

                If facturado > 0 Then
                    Ejecucion("INSERT INTO detfac SET SERIE='" & My.Settings.serie & "', NUMERO=" & facturado & ",CANTIDAD=" & concepto.Cantidad & ", DESCRIPCION='" & concepto.Concepto & "' , PRECIOUNITARIO=" & concepto.Preciounitario & ", IMPORTE=" & concepto.importe & ", CONIVA=" & coniva & ", IVA=" & concepto.IVA & "")
                End If
                'If My.Settings.GuardaCobroexpress = "SI" Then
                '    GuardaCobroexpress.ejecutar("INSERT INTO reciboesclavo(clave,Concepto,Importe,IVA,Cantidad,recibo,serie,caja) VALUES(" & My.Settings.ClaveAgua & ",'" & concepto.Concepto & "'," & concepto.importe & "," & concepto.IVA & "," & concepto.Cantidad & "," & My.Settings.folio & ",'" & My.Settings.serie & "','" & My.Settings.caja & "')")
                'End If

            Next i
        End If

    End Sub

#End Region

    ' graba el recibo 25/10/2016
    Public Sub grabareimprimir(ByVal formaPagoReciboFactura As String, ByVal imprimirRecibo As Boolean)

        Dim save As New base

        Dim consumoLectura As Integer
        Dim tipoServicio As String
        Dim tipoUso As String

        Try
            If recibo.fechaoriginaldedeuda = String.Empty Then
                recibo.fechaoriginaldedeuda = Now.ToShortDateString()
            End If

            Dim tipou As Integer = 1
            If TIPO = "USUARIO" Then
                tipou = 1
            ElseIf TIPO = "NO USUARIO" Or TIPO = "CLIENTE" Then
                tipou = 2
            ElseIf TIPO = "SOLICITUD" Then
                tipou = 3
            End If

            'save.ejecutar("INSERT INTO pagos (FECHA_ACT, PERIODO, PAGOS, FECHA_DEUDA, IVA, TOTAL, NOMBRE, RECIBO, CANCELADO, CUENTA, COMUNIDAD, COLONIA, SERIE, USUARIO, CAJA, UBICACION, TARIFA, CCODPAGO, ESFIJO, FACTURADO, esusuario,observacion, Descuento, Descuentopesos) VALUES ('" & recibo.Fecha_Act & "', '" & recibo.periodo & "'," & recibo.subtotal & ", '" & recibo.fecha_deuda & "', '" & recibo.iva & "', '" & recibo.total & "', '" & recibo.nombre & "', '" & My.Settings.folio + 1 & "', '" & recibo.cancelado & "', '" & recibo.cuenta & "', '" & recibo.comunidad & "', '" & recibo.colonia & "', '" & My.Settings.serie & "', '" & recibo.usuarios & "', '" & My.Settings.caja & "', '" & recibo.ubicacion & "', '" & recibo.tarifa & "', '" & recibo.ccodpago & "','" & control.EsFijo & "," & facturado & "," & recibo.esusuario & ",''," & recibo.descuento & "," & recibo.Totaldescuentoenpesos & ")")

            save.ejecutar("INSERT INTO pagos (FECHA_ACT, PERIODO, PAGOS, FECHA_DEUDA, IVA, TOTAL, NOMBRE, RECIBO, CANCELADO, CUENTA, COMUNIDAD, COLONIA, SERIE, USUARIO, CAJA, UBICACION, TARIFA, CCODPAGO, ESFIJO, FACTURADO, esusuario,observacion, Descuento,Descuentopesos) VALUES ('" & recibo.Fecha_Act & "', '" & recibo.periodo & "'," & recibo.subtotal & ", '" & recibo.fecha_deuda & "', '" & recibo.iva & "', '" & recibo.total & "', '" & recibo.nombre & "', '" & My.Settings.folio + 1 & "', '" & recibo.cancelado & "', '" & recibo.cuenta & "', '" & recibo.comunidad & "', '" & recibo.colonia & "', '" & My.Settings.serie & "', '" & recibo.usuarios & "', '" & My.Settings.caja & "', '" & recibo.ubicacion & "', '" & recibo.tarifa & "', '" & formaPagoReciboFactura & "'," & control.EsFijo & "," & facturado & "," & recibo.esusuario & ",'', " & recibo.descuento & "," & recibo.Totaldescuentoenpesos & " )")

            Try
                save.ejecutar("update descuentorecargo set descuentorecargo=" & control.totaldescuentorecargo & ",ESTADO='Aplicado' where cuenta=" & control.cuenta & " and estado='Pendiente'")

            Catch ex As Exception

            End Try


            For i = 1 To control.Listadeconceptos.Count
                Dim concepto As New Clsconcepto
                concepto = control.Listadeconceptos(i)
                Dim coniva As Boolean = False
                If concepto.IVA > 0 Then
                    coniva = True
                End If
                Dim montoiva As Decimal = 0
                If coniva Then
                    montoiva = Math.Round(concepto.importe * (variable_iva / 100), 2)
                Else
                    montoiva = 0
                End If
                save.ejecutar("INSERT INTO pagotros (RECIBO, CUENTA, SERIE, USUARIO, FECHA, CONCEPTO, CAJA, CANCELADO, CCODPAGO, IMPORTE, CANTIDAD,MONTO,NUMCONCEPTO,CLAVEMOV,IVA,MONTOIVA) VALUES ('" & My.Settings.folio + 1 & "', '" & recibo.cuenta & "', '" & My.Settings.serie & "', '" & recibo.usuarios & "', '" & recibo.Fecha_Act & "', '" & concepto.Concepto & "', '" & My.Settings.caja & "', '" & recibo.cancelado & "', '" & recibo.ccodpago & "', " & concepto.importe & ", " & concepto.Cantidad & "," & concepto.Preciounitario & ",'" & concepto.Clave & "'," & concepto.CLAVEMOV & "," & coniva & "," & montoiva & ")")


            Next i
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Try
            If actualizarfecha = True Then
                save.ejecutar("UPDATE USUARIO SET DEUDAFEC='" & recibo.fechafin & "' WHERE CUENTA=" & recibo.cuenta)
            End If
        Catch ex As Exception

        End Try





        Try
            For i = 1 To control.desgloseconsumo.Count
                Dim objeto As Object
                If TypeName(control.desgloseconsumo.Item(i)) = "clsunidadmes" Then
                    objeto = New clsunidadmes
                Else
                    objeto = New ClsRegistrolectura
                End If

                Try




                    objeto = control.desgloseconsumo.Item(i)

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

                    save.ejecutar("INSERT INTO pago_mes (PERIODO, MES, ANO, CONCEPTO, FECHA, RECIBO, CAJA,SERIE, CUENTA,MONTO, DESCUENTO,MONTOPAGADO, TIPO, CONSUMO, TIPOUSO) VALUES ('" & CadenaNumeroMes(objeto.Mes) & Mid(objeto.Periodo, 3, 2) & "','" & objeto.Mes & "'," & objeto.Periodo & ",'CONSUMO','" & recibo.Fecha_Act & "'," & My.Settings.folio + 1 & ",'" & My.Settings.caja & "', '" & My.Settings.serie & "'," & recibo.cuenta & "," & objeto.Totalcondescuento & "," & objeto.Total - objeto.Totalcondescuento & "," & objeto.Totalcondescuento & ",'" & tipoServicio & "'," & consumoLectura & ",'" & tipoUso & "')")

                Catch ex As Exception
                    MessageBox.Show(ex.ToString)
                End Try

            Next





            For i = 1 To control.desgloserezago.Count
                Dim objeto As Object
                If TypeName(control.desgloserezago.Item(i)) = "clsunidadmes" Then
                    objeto = New clsunidadmes
                Else
                    objeto = New ClsRegistrolectura
                End If

                Try




                    objeto = control.desgloserezago.Item(i)

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

                    save.ejecutar("INSERT INTO pago_mes (PERIODO, MES, ANO, CONCEPTO, FECHA, RECIBO, CAJA,SERIE, CUENTA,MONTO, DESCUENTO,MONTOPAGADO, TIPO, CONSUMO, TIPOUSO) VALUES ('" & CadenaNumeroMes(objeto.Mes) & Mid(objeto.Periodo, 3, 2) & "','" & objeto.Mes & "'," & objeto.Periodo & ",'CONSUMO','" & recibo.Fecha_Act & "'," & My.Settings.folio + 1 & ",'" & My.Settings.caja & "', '" & My.Settings.serie & "'," & recibo.cuenta & "," & objeto.Totalcondescuento & "," & objeto.Total - objeto.Totalcondescuento & "," & objeto.Totalcondescuento & ",'" & tipoServicio & "'," & consumoLectura & ",'" & tipoUso & "')")

                Catch ex As Exception
                    MessageBox.Show(ex.ToString)
                End Try

            Next





            For i = 1 To control.desglosealcantarillado.Count

                Dim objeto As Object
                If TypeName(control.desglosealcantarillado.Item(i)) = "clsunidadmes" Then
                    objeto = New clsunidadmes
                Else
                    objeto = New ClsRegistrolectura
                End If


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


                objeto = control.desglosealcantarillado.Item(i)
                save.ejecutar("INSERT INTO pago_mes (PERIODO, MES, ANO, CONCEPTO, FECHA, RECIBO, CAJA,SERIE, CUENTA,MONTO, DESCUENTO,MONTOPAGADO, TIPO, CONSUMO, TIPOUSO) VALUES ('" & CadenaNumeroMes(objeto.Mes) & Mid(objeto.Periodo, 3, 2) & "','" & objeto.Mes & "'," & objeto.Periodo & ",'ALCANTARILLADO','" & recibo.Fecha_Act & "'," & My.Settings.folio + 1 & ",'" & My.Settings.caja & "', '" & My.Settings.serie & "'," & recibo.cuenta & "," & objeto.Totalcondescuento & "," & objeto.Total - objeto.Totalcondescuento & "," & objeto.Totalcondescuento & ",'" & tipoServicio & "'," & consumoLectura & ",'" & tipoUso & "')")

            Next
            For i = 1 To control.desglosesaneamiento.Count

                Dim objeto As Object
                If TypeName(control.desglosesaneamiento.Item(i)) = "clsunidadmes" Then
                    objeto = New clsunidadmes
                Else
                    objeto = New ClsRegistrolectura
                End If

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

                objeto = control.desglosesaneamiento.Item(i)
                save.ejecutar("INSERT INTO pago_mes (PERIODO, MES, ANO, CONCEPTO, FECHA, RECIBO, CAJA,SERIE, CUENTA,MONTO,DESCUENTO,MONTOPAGADO, TIPO, CONSUMO, TIPOUSO) VALUES ('" & CadenaNumeroMes(objeto.mes) & Mid(objeto.periodo, 3, 2) & "','" & objeto.mes & "'," & objeto.periodo & ",'SANEAMIENTO ','" & recibo.Fecha_Act & "'," & My.Settings.folio + 1 & ",'" & My.Settings.caja & "', '" & My.Settings.serie & "'," & recibo.cuenta & "," & objeto.totalcondescuento & "," & objeto.total - objeto.totalcondescuento & "," & objeto.totalcondescuento & ",'" & tipoServicio & "'," & consumoLectura & ",'" & tipoUso & "')")


            Next
            For i = 1 To control.desgloserecargo.Count
                'If control.EsFijo = True Then
                Dim objeto As Object
                If TypeName(control.desgloserecargo.Item(i)) = "clsunidadmes" Then
                    objeto = New clsunidadmes
                Else
                    objeto = New ClsRegistrolectura
                End If
                objeto = control.desgloserecargo.Item(i)


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

                save.ejecutar("INSERT INTO pago_mes (PERIODO, MES, ANO, CONCEPTO, FECHA, RECIBO, CAJA,SERIE, CUENTA,MONTO,DESCUENTO,MONTOPAGADO, TIPO, CONSUMO, TIPOUSO) VALUES ('" & CadenaNumeroMes(objeto.mes) & Mid(objeto.periodo, 3, 2) & "','" & objeto.mes & "'," & objeto.periodo & ",'RECARGO ','" & recibo.Fecha_Act & "'," & My.Settings.folio + 1 & ",'" & My.Settings.caja & "', '" & My.Settings.serie & "'," & recibo.cuenta & "," & objeto.totalcondescuento & "," & objeto.total - objeto.totalcondescuento & "," & objeto.totalcondescuento & ",'" & tipoServicio & "'," & consumoLectura & ",'" & tipoUso & "')")

            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Try
            If actualizarfecha Then
                save.ejecutar("update usuario set requeri=0 where cuenta=" & recibo.cuenta)
            Else
                ' no hubo pago por concepto de consumo
            End If
        Catch ex As Exception

        End Try

        If recibo.esusuario And control.EsMEdido Then
            For i = 1 To control.desgloseconsumo.Count

                Dim r As Object

                If TypeName(control.desgloseconsumo(i)) = "ClsRegistrolectura" Then
                    r = New ClsRegistrolectura
                Else
                    r = New clsunidadmes
                End If

                r = control.desgloseconsumo(i)
                ' Dim cadenaperiodo As String = CadenaNumeroMes(r.Mes) & Mid(r.Periodo, 3, 2)
                Dim mes As String = r.Mes
                Dim periodo As String = r.Periodo
                'save.ejecutar("UPDATE LECTURAS SET PAGADO=1 WHERE MES= '" & cadenaperiodo & "' AND CUENTA= " & recibo.cuenta)
                save.ejecutar("UPDATE LECTURAS SET PAGADO=1 WHERE MES= '" & mes & "' AND '" & periodo & "' AND CUENTA= " & recibo.cuenta)
            Next
            For i = 1 To control.desgloserezago.Count
                Dim r As Object

                If TypeName(control.desgloserezago(i)) = "ClsRegistrolectura" Then
                    r = New ClsRegistrolectura
                Else
                    r = New clsunidadmes
                End If

                r = control.desgloserezago(i)
                'Dim cadenaperiodo As String = CadenaNumeroMes(r.Mes) & Mid(r.Periodo, 3, 2)
                'save.ejecutar("UPDATE LECTURAS SET PAGADO=1 WHERE MES= '" & cadenaperiodo & "' AND CUENTA= " & recibo.cuenta)
                Dim mes As String = r.Mes
                Dim periodo As String = r.Periodo
                save.ejecutar("UPDATE LECTURAS SET PAGADO=1 WHERE MES= '" & mes & "' AND an_per='" & periodo & "' AND CUENTA= " & recibo.cuenta)
            Next

        End If

        ''''''''''''''''''Código para dar como pagado en la tabla otrosconceptos'''''''''''''''''''

        '''''''''''''''''
        Dim conceptootro As New Clsconcepto
        For i = 1 To control.Listadeconceptos.Count
            conceptootro = control.Listadeconceptos.Item(i)
            If conceptootro.CLAVEMOV > 0 Then
                ' Caja.dtgConceptos.Rows.Add(concepto.Concepto)
                Dim BASEm As New base
                Dim RESTA As Decimal
                Dim estado As String = "ABONADO"
                Decimal.TryParse(BASEm.obtenerCampo("select resta from otrosconceptos where clave=" & conceptootro.CLAVEMOV, "RESTA"), RESTA)
                Dim PAGADO = 0
                RESTA = Math.Round(RESTA - (conceptootro.importe + conceptootro.IVA), 2)
                If RESTA <= 0 Then
                    RESTA = 0
                    PAGADO = 1
                    estado = "APLICADO"
                End If

                save.ejecutar("UPDATE otrosconceptos SET `resta`=" & RESTA & ", pagado='" & PAGADO & "', estado='" & estado & "'  WHERE `Clave`='" & conceptootro.CLAVEMOV & "';")
            End If

        Next

        'Try

        '    Dim recibo As New reciboaimprimir
        '    recibo.ReciboHojaCarta(My.Settings.serie, My.Settings.folio + 1, True, 0.00, recibo.ccodpago, 0.00)

        'Catch ex As Exception
        '    MessageBox.Show("Fallas en la impresion")
        'End Try

        My.Settings.folio = My.Settings.folio + 1
        My.Settings.Save()

        My.Settings.Reload()


        ' actualizando saldo para mantener actualizado el saldo en la base de datos y evitar calcular saldo en la medida de los posible
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


            Dim concepto As New Clsconcepto
            Try

                concepto = pago.Listadeconceptos.Item("PAGO VALVULISTA")
            Catch ex As Exception

            End Try

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

            total = pago.totaldeudaconsumo + pago.totaldeudaalcantarillado + pago.totaldeudasaneamiento + acumiva + pago.totaldeudarecargos + pago.totaldeudaotros + concepto.importe

            Ejecucion("update usuario set deuda=" & pago.totaldeudaconsumo & " , deualcant=" & pago.totaldeudaalcantarillado & ", deudasanea=" & pago.totaldeudasaneamiento & ", iva=" & acumiva & ", recargos =" & pago.totaldeudarecargos & ", deudaotros=" & pago.totaldeudaotros + concepto.importe & ", total=" & total & ", LecturaAnt=UltimaLecturaActualizar('" & recibo.cuenta & "'), PeriodosAdeudo=" & pago.desgloseconsumo.Count + pago.desgloserezago.Count & ",periodo='" & pago.periodo & "'  where cuenta=" & recibo.cuenta)
            datos.Close()
        Catch ex As Exception

        End Try





        ''''''''''''''''''


        Caja.limpiar()
        ReiniciarDescuentos()
        save.conexion.Dispose()
        Close()


    End Sub

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

    Public Sub imprimerecibo(ByVal folio As Integer, ByVal serie As String)
        Dim que As New base
        Dim imphist As Boolean = False
        'actualiza la fecha de pago al usuario cuando paga consumo
        If actualizarfecha = True Then
            imphist = True

        End If
        If que.obtenerCampo("select esusuario from pagos where recibo=" & folio & " and serie='" & serie & "'", "esusuario") = "1" Then
            imprime.imprime("select pagos.recibo,pagos.serie,usuario.nombre as nombre, Domicilio as Direccion, Col.Colonia, Com.Comunidad, pagos.total as total, pagos.fecha_Act as fecha_act, usuario.rfc as rfc, pagos.pagos as subtotal, pagos.iva as iva,Cuo.Descripcion_Cuota AS TARIFA, PAGOS.CCODPAGO AS CCODPAGO,pagos.cuenta as cuenta,pagos.observacion as observacion, pagos.Descuento, PAGOS.caja as caja, pagos.usuario as usuario, usuario.LecturaAnt, usuario.LecturaAct  from pagos,usuario inner join colonia Col on (Col.id_colonia= usuario.id_colonia) inner join comunidades Com on (Com.Id_comunidad= usuario.Id_comunidad) inner join cuotas Cuo on(usuario.TARIFA=Cuo.id_tarifa) where recibo=" & folio & " and serie='" & serie & "' and usuario.cuenta=pagos.cuenta", My.Settings.formatorecibo, "select * from pagotros where recibo=" & folio & " and serie='" & serie & "'", False, imphist)


        ElseIf que.obtenerCampo("select esusuario from pagos where recibo=" & folio & " and serie='" & serie & "'", "esusuario") = "2" Then
            imprime.imprime("select pagos.recibo,pagos.serie,pagos.nombre as nombre,  nousuarios.direccion, nousuarios.colonia AS COLONIA, nousuarios.comunidad AS COMUNIDAD, pagos.total as total, pagos.fecha_Act as fecha_act, nousuarios.rfc as rfc, pagos.pagos as subtotal, pagos.iva as iva,0 AS TARIFA, PAGOS.CCODPAGO AS CCODPAGO,pagos.cuenta as cuenta,pagos.observacion as observacion, 0 from pagos,nousuarios where recibo=" & folio & " and serie='" & serie & "' and nousuarios.clave=pagos.cuenta", My.Settings.formatorecibo, "select * from pagotros where recibo=" & folio & " and serie='" & serie & "'", False, False)

            'esto para las solicitudes
        ElseIf que.obtenerCampo("select esusuario from pagos where recibo=" & folio & " and serie='" & serie & "'", "esusuario") = "3" Then
            imprime.imprime("select pagos.recibo, pagos.serie, S.nombre as nombre,   S.Domicilio as DIRECCION, pagos.colonia AS COLONIA, pagos.comunidad AS COMUNIDAD, pagos.total as total, pagos.fecha_Act as fecha_act, S.rfc as rfc, pagos.pagos as subtotal, pagos.iva as iva, 0 AS TARIFA, PAGOS.CCODPAGO AS CCODPAGO, pagos.cuenta as cuenta, pagos.observacion as observacion, pagos.descuento from pagos inner join solicitud S on(S.Numero = pagos.cuenta) where recibo =" & folio & " and serie = '" & serie & "'", My.Settings.formatorecibo, "select * from pagotros where recibo=" & folio & " and serie='" & serie & "'", False, False)
        End If
        que.conexion.Dispose()
    End Sub







    'Public Sub Timbrar()
    '    Caja.limpiar()

    '    'Dim folio As Integer
    '    'Dim serie As String

    '    Dim sdkresp As SDKRespuesta


    '    'Dim directorio As String = ""
    '    'Dim directorio2 As String = ""


    '    Dim nombre, nombresespacios As String

    '    nombre = txtnombre.Text
    '    nombresespacios = nombre.Replace(" ", "").Replace(",", "")

    '    'Se crean los direcctorios para guardar las facturas      

    '    If Not My.Computer.FileSystem.DirectoryExists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\facturas\" & nombresespacios) Then

    '        My.Computer.FileSystem.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\facturas\" & nombresespacios)
    '    End If


    '    'directorio = Application.StartupPath & "\facturas\" & nombresespacios
    '    'directorio2 = Application.StartupPath & "\facturas" & Year(Now) & acompletacero(Month(Now), 2) & "\"


    '    Try



    '        Dim mf As MFSDK = New MFSDK()


    '        ' Se crea la factura


    '        foliofactura = Val(obtenerCampo("select * from empresa limit 1 ", "foliofactura")) + 1

    '        seriefactura = obtenerCampo("select * from empresa limit 1 ", "seriefactura")

    '        Dim nombreEM As String = obtenerCampo("select * from empresa", "CNOMBRE")
    '        iva = Math.Round(total - subtotal, 2)

    '        Dim _uso As String = cmbuso.SelectedValue
    '        Dim _metodo As String = cmbmetodo.SelectedValue

    '        mf = MakeFileToSendMultifacturas(control, iva, subtotal, total, seriefactura, foliofactura, cmbformapago.SelectedValue, _metodo, _uso, txtnombre.Text, txtrfc.Text, nombreEM)


    '        sdkresp = mf.Timbrar("C:\sdk2\timbrar32.bat", "C:\sdk2\timbrados\", "factura", False)



    '        If Not sdkresp.Codigo_MF_Texto.Contains("OK") Then
    '            MessageBox.Show(sdkresp.Codigo_MF_Texto)
    '            'sdkresp = sdk.Timbrar(cfdi, "c:\facturas\", "XmlFactura" & seriefactura & foliofactura)
    '            Try
    '                guardatxt("C:\facturas\errores", "C:\facturas\errores\dia" & Now.Year & Now.Month & Now.Day & ".txt", "Factura " & seriefactura & foliofactura & "->" & sdkresp.Codigo_MF_Texto)
    '            Catch ex2 As Exception

    '            End Try

    '            Return
    '        End If

    '        Dim directorioxml1 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\facturas\" & nombresespacios
    '        Dim directorioxml2 = (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\facturas\" & Year(Now) & acompletacero(Month(Now).ToString(), 2)).Trim
    '        Dim directorioxml3 = (Application.StartupPath & "\facturas\" & Year(Now) & acompletacero(Month(Now).ToString(), 2)).Trim
    '        If Not Directory.Exists(directorioxml1) Then
    '            Directory.CreateDirectory(directorioxml1)
    '        End If

    '        If Not Directory.Exists(directorioxml2) Then
    '            Directory.CreateDirectory(directorioxml2)
    '        End If

    '        If Not Directory.Exists(directorioxml3) Then
    '            Directory.CreateDirectory(directorioxml3)
    '        End If


    '        Dim cadenaxml = (directorioxml1 & "\xmlFactura" & seriefactura & foliofactura & ".xml").Trim
    '        Dim cadenaxml2 = (directorioxml2 & "\xmlFactura" & seriefactura & foliofactura & ".xml").Trim
    '        Dim cadenaxml3 = (directorioxml3 & "\xmlFactura" & seriefactura & foliofactura & ".xml").Trim

    '        Dim fs1 As FileStream = File.Create(cadenaxml)

    '        ' Add text to the file.
    '        Dim info1 As Byte() = New UTF8Encoding(True).GetBytes(sdkresp.CFDI.ToString().TrimStart().TrimEnd())
    '        fs1.Write(info1, 0, info1.Length)
    '        fs1.Close()


    '        Dim fs As FileStream = File.Create(cadenaxml3)

    '        ' Add text to the file.
    '        Dim info As Byte() = New UTF8Encoding(True).GetBytes(sdkresp.CFDI.ToString().TrimStart().TrimEnd())
    '        fs.Write(info, 0, info.Length)
    '        fs.Close()


    '        Dim fs2 As FileStream = File.Create(cadenaxml2)

    '        ' Add text to the file.
    '        Dim info2 As Byte() = New UTF8Encoding(True).GetBytes(sdkresp.CFDI.ToString().TrimStart().TrimEnd())
    '        fs2.Write(info2, 0, info2.Length)
    '        fs2.Close()


    '        'Try ' haccemos al server en caso de que timbro localmente el Xml para el expediente general
    '        '    ''    If Not File.Exists("\\192.168.1.80\facturas\xmlFactura" & seriefactura & foliofactura & ".xml") Then
    '        '    ''        System.IO.File.Copy("c:\facturas\XmlFactura" & seriefactura & foliofactura, "\\192.168.1.80\facturas\xmlFactura" & seriefactura & foliofactura & ".xml", True)
    '        '    ''    End If
    '        '    'If Not File.Exists(Application.StartupPath & "\facturas\" + nombresespacios & "\xmlFactura" & seriefactura & foliofactura & ".xml") Then
    '        '    '    System.IO.File.Copy("c:\facturas\" + nombresespacios & "\XmlFactura" & seriefactura & foliofactura, Application.StartupPath & "\facturas\" + nombresespacios & "\xmlFactura" & seriefactura & foliofactura & ".xml", True)
    '        '    'End If
    '        'Catch ex As Exception

    '        'End Try

    '        Try '' grabando el datamatrix en la tabla cajas para que de ahi lo tome el crystal reports
    '            '  Dim codigo As Image = Image.FromFile(sdkresp.RutaPNG)
    '            ' convierto a bytes


    '            Dim image As System.Drawing.Image = qr(cadenaxml)

    '            Dim imageConverter As New ImageConverter()
    '            Dim pngs As Byte() = DirectCast(imageConverter.ConvertTo(image, GetType(Byte())), Byte())


    '            Dim dts As New DatosReciboTableAdapters.cajasTableAdapter

    '            dts.UpdateQueryimagen(pngs, My.Settings.caja)


    '        Catch ex As Exception
    '            MessageBox.Show("error en codigo qr " + ex.Message)
    '        End Try
    '        Dim filtro As String
    '        If vienede = "CAJA" Then
    '            Dim tipo As Integer = recibo.esusuario
    '            If chksinrecibo.Checked Then ' se genera la factura sin recibo y sin quitar el saldo
    '                ' por lo tanto tiene que tomar el xml
    '                'tipo = 4
    '                tipo = 1



    '                recibo.NUMERO = 0
    '                recibo.SERIE = " "
    '            Else
    '                recibo.NUMERO = My.Settings.folio
    '                recibo.SERIE = My.Settings.serie
    '            End If
    '            Try
    '                Ejecucion("insert into encfac SET FECHA=CONCAT('" & Now.Year & "-" & Now.Month & "-" & Now.Day & "', ' ' ,curtime()) " &
    '                 ",NOMBRE='" & txtnombre.Text &
    '                 "',  SUBTOTAL=" & recibo.subtotal & ",IVA=" & recibo.iva &
    '                 ",TOTAL=" & recibo.total & ",TIPO='" & tipo &
    '                 "', ESTADO='A', CAJA='" & My.Settings.caja &
    '                 "', USUARIO='" & usuariodelsistema &
    '                 "', motivocancelacion='', Advertencia ='" & sdkresp.Advertencia &
    '                  "', cadena='" & sdkresp.Cadena &
    '                  "', certificadoSAT= '" & sdkresp.CertificadoSAT &
    '                  "', cfdi =' " & sdkresp.CFDI &
    '                  "', codigo_mf_texto=' " & sdkresp.Codigo_MF_Texto &
    '                  "', ejecucion ='" & sdkresp.Ejecucion &
    '                  "', fechatimbrado ='" & sdkresp.FechaTimbrado &
    '                  "', idpac ='" & sdkresp.IdPac &
    '                  "', mensajeoriginalpacjson ='" &
    '                  "', nodecertificado ='" & sdkresp.NoCertificado &
    '                  "',pac='" & sdkresp.Pac &
    '                  "',png64='" & sdkresp.PNG64 &
    '                  "',produccion='" & sdkresp.Produccion &
    '                  "', respuestaoriginal = '" &
    '                  "', rutapng ='" & sdkresp.RutaPNG.Replace("\", "\\") &
    '                  "', rutaxml ='" & sdkresp.RutaXML.Replace("\", "\\") &
    '                  "', sello ='" & sdkresp.Sello & "', sellosat='" & sdkresp.SelloSAT & "' " &
    '                  ", servidor='" & sdkresp.Servidor & "', uuid='" & sdkresp.UUID &
    '                  "', serie='" & seriefactura & "', numero=" & foliofactura &
    '                  ", version='3.3'" &
    '                  ", recibo=" & recibo.NUMERO & ", serierecibo='" & recibo.SERIE & "'")
    '            Catch ex As Exception
    '                MsgBox(ex.Message)
    '            End Try

    '        End If
    '        If vienede = "RECIBO" Then


    '            Ejecucion("insert into encfac SET FECHA=CONCAT('" & Now.Year & "-" & Now.Month & "-" & Now.Day & "', ' ' ,curtime()) " &
    '                  ",NOMBRE='" & txtnombre.Text &
    '                  "',  SUBTOTAL=" & recibo.subtotal & ",IVA=" & recibo.iva &
    '                  ",TOTAL=" & recibo.total &
    '                  ",TIPO='" & recibo.esusuario &
    '                  "', ESTADO='A', CAJA='" & My.Settings.caja &
    '                  "', USUARIO='" & usuariodelsistema &
    '                  "', motivocancelacion='', Advertencia ='" & sdkresp.Advertencia &
    '                   "', cadena='" & sdkresp.Cadena &
    '                   "', certificadoSAT= '" & sdkresp.CertificadoSAT &
    '                   "', cfdi =' " & sdkresp.CFDI &
    '                   "', codigo_mf_texto=' " & sdkresp.Codigo_MF_Texto &
    '                   "', ejecucion ='" & sdkresp.Ejecucion &
    '                   "', fechatimbrado ='" & sdkresp.FechaTimbrado &
    '                   "', idpac ='" & sdkresp.IdPac &
    '                   "', mensajeoriginalpacjson ='" &
    '                   "', nodecertificado ='" & sdkresp.NoCertificado &
    '                   "',pac='" & sdkresp.Pac &
    '                   "',png64='" & sdkresp.PNG64 &
    '                   "',produccion='" & sdkresp.Produccion &
    '                   "', respuestaoriginal = '" &
    '                   "', rutapng ='" & sdkresp.RutaPNG.Replace("\", "\\") &
    '                   "', rutaxml ='" & sdkresp.RutaXML.Replace("\", "\\") &
    '                   "', sello ='" & sdkresp.Sello & "', sellosat='" & sdkresp.SelloSAT & "' " &
    '                   ", servidor='" & sdkresp.Servidor & "', uuid='" & sdkresp.UUID &
    '                   "', serie='" & seriefactura & "', numero=" & foliofactura &
    '                   ", version='3.3', observacion='" & txtObservaciones.Text & "'" &
    '                   ", recibo=" & recibo.NUMERO & ", serierecibo='" & recibo.SERIE & "'")
    '        End If

    '        ' Ejecucion("update cajas set folio=" & foliofactura & " WHERE id_caja='" & My.Settings.caja & "'")
    '        Dim reporte As New ReportDocument()
    '        If Not chksinrecibo.Checked Then


    '            If vienede = "CAJA" Then
    '                Ejecucion("update pagos set factura=" & foliofactura & ",facturado=" & foliofactura & ", seriefactura='" & seriefactura & "' where recibo=" & My.Settings.folio & " and serie='" & My.Settings.serie & "'")
    '                filtro = "{cajas1.id_caja}='" & My.Settings.caja & "' and {pagos1.recibo}=" & My.Settings.folio & " and {pagos1.serie}='" & My.Settings.serie & "'"
    '            Else
    '                Ejecucion("update pagos set factura=" & foliofactura & ",facturado=" & foliofactura & ", seriefactura='" & seriefactura & "' where recibo=" & recibo.NUMERO & " and serie='" & recibo.SERIE & "'")
    '                filtro = "{cajas1.id_caja}='" & My.Settings.caja & "' and {pagos1.recibo}=" & recibo.NUMERO & " and {pagos1.serie}='" & recibo.SERIE & "'"
    '            End If



    '            Try


    '                reporte.Load(AppPath() & ".\Reportes\FACTURA.rpt")
    '                Dim servidorreporte As String = My.Settings.servidorreporte
    '                Dim usuarioreporte As String = My.Settings.usuarioreporte
    '                Dim passreporte As String = My.Settings.passreporte
    '                Dim basereporte As String = My.Settings.basereporte

    '                reporte.DataSourceConnections.Item(0).SetConnection(servidorreporte, basereporte, False)
    '                reporte.DataSourceConnections.Item(0).SetLogon(usuarioreporte, passreporte)
    '                reporte.RecordSelectionFormula = filtro

    '            Catch ex As Exception
    '                MessageBox.Show(ex.Message)
    '            End Try






    '            Try
    '                Dim varXmlFile As XmlDocument = New XmlDocument()

    '                Dim varXmlNsMngr As XmlNamespaceManager = New XmlNamespaceManager(varXmlFile.NameTable)


    '                varXmlFile.LoadXml((sdkresp.CFDI))

    '                varXmlNsMngr.AddNamespace("cfdi", "http://www.sat.gob.mx/cfd/3")
    '                varXmlNsMngr.AddNamespace("tfd", "http://www.sat.gob.mx/TimbreFiscalDigital")




    '                Dim varTotal As String = varXmlFile.ChildNodes.Item(1).Attributes.Item(16).Value '  varXmlFile.SelectSingleNode("/cfdi:Comprobante/@total", varXmlNsMngr).InnerText
    '                Dim VARNODECERTIFICADO As String = varXmlFile.ChildNodes.Item(1).Attributes.Item(18).Value 'varXmlFile.SelectSingleNode("/cfdi:Comprobante/@NoCertificado", varXmlNsMngr).InnerText
    '                Dim varformapago As String = varXmlFile.ChildNodes.Item(1).Attributes.Item(8).Value 'varXmlFile.SelectSingleNode("/cfdi:Comprobante/@formapago", varXmlNsMngr).InnerText
    '                Dim varUUID As String = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@UUID", varXmlNsMngr).InnerText
    '                Dim varcertificado As String = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@NoCertificadoSAT", varXmlNsMngr).InnerText
    '                Dim VARSELLOSAT As String = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@SelloSAT", varXmlNsMngr).InnerText
    '                Dim VARSELLOCFD As String = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@SelloCFD", varXmlNsMngr).InnerText

    '                Dim VARRFCEMISOR As String = String.Empty

    '                Dim varuso As String = "G03"
    '                Dim metodo As String = "PUE"

    '                Dim LISTAComprobante As XmlNodeList = varXmlFile.GetElementsByTagName("cfdi:Comprobante")
    '                For Each xAtt In LISTAComprobante
    '                    metodo = VarXml(xAtt, "MetodoPago")

    '                Next





    '                Dim LISTANODOSEMISOR As XmlNodeList = varXmlFile.GetElementsByTagName("cfdi:Emisor")


    '                Dim strEmisorNombre As String = String.Empty
    '                For Each xAtt In LISTANODOSEMISOR
    '                    VARRFCEMISOR = VarXml(xAtt, "Rfc")
    '                    strEmisorNombre = VarXml(xAtt, "Nombre")
    '                Next


    '                Dim LISTANODORECEPTOR As XmlNodeList = varXmlFile.GetElementsByTagName("cfdi:Receptor")
    '                Dim varRFCRECEPTOR As String = String.Empty
    '                Dim strReceptorNombre As String = String.Empty


    '                For Each xAtt In LISTANODORECEPTOR
    '                    varRFCRECEPTOR = VarXml(xAtt, "Rfc")
    '                    strReceptorNombre = VarXml(xAtt, "Nombre")
    '                    varuso = VarXml(xAtt, "UsoCFDI")
    '                Next

    '                'reporte.SetParameterValue("nombre", txtnombre.Text)
    '                'reporte.SetParameterValue("Direccion", txtcalle.Text & " " & txtnumext.Text & " " & txtnuminterior.Text)
    '                'reporte.DataDefinition.FormulaFields("colonia").Text = "'" & txtcolonia.Text & "'"
    '                'reporte.DataDefinition.FormulaFields("ciudad").Text = "'" & txtpoblacion.Text & " " & txtdelegacion.Text.Trim & " " & txtestado.Text & " CP " & txtcp.Text & "'"
    '                'reporte.SetParameterValue("fechatimbrado", sdkresp.FechaTimbrado)
    '                'reporte.SetParameterValue("certificado", sdkresp.CertificadoSAT)
    '                'reporte.SetParameterValue("cantidadconletra", ConvertCurrencyToSpanish(recibo.total, "Pesos"))
    '                'reporte.SetParameterValue("formadepago", cmbformapago.SelectedValue.ToString())
    '                'reporte.SetParameterValue("Cadenaoriginal", sdkresp.Cadena)
    '                'reporte.SetParameterValue("foliofiscal", seriefactura & foliofactura)
    '                'reporte.SetParameterValue("RFCCLIENTE", txtrfc.Text)
    '                'reporte.SetParameterValue("CERTIFICADOSAT", sdkresp.CertificadoSAT)
    '                'reporte.SetParameterValue("nota", txtObservaciones.Text)
    '                'reporte.SetParameterValue("SerieCertificado", sdkresp.NoCertificado)

    '                'reporte.SetParameterValue("Sellodigital", sdkresp.SelloSAT)
    '                'reporte.SetParameterValue("SelloCFDI", sdkresp.Sello)
    '                'reporte.SetParameterValue("UUID", sdkresp.UUID)
    '                'reporte.SetParameterValue("Medidor", "")
    '                'reporte.SetParameterValue("Promedio", "")
    '                reporte.SetParameterValue("nombre", strReceptorNombre)
    '                reporte.SetParameterValue("Direccion", txtcalle.Text & " " & txtnumext.Text & " " & txtnuminterior.Text)
    '                reporte.DataDefinition.FormulaFields("colonia").Text = "'" & txtcolonia.Text & "'"
    '                reporte.DataDefinition.FormulaFields("ciudad").Text = "'" & txtpoblacion.Text & " " & txtdelegacion.Text.Trim & " " & txtestado.Text & " CP " & txtcp.Text & "'"
    '                reporte.SetParameterValue("fechatimbrado", sdkresp.FechaTimbrado.ToString())
    '                reporte.SetParameterValue("certificado", sdkresp.NoCertificado)
    '                reporte.SetParameterValue("cantidadconletra", ConvertCurrencyToSpanish(recibo.total, "Pesos"))
    '                reporte.SetParameterValue("formadepago", varformapago)
    '                reporte.SetParameterValue("MetodoPago", metodo)
    '                reporte.SetParameterValue("UsoCfdi", varuso)
    '                reporte.SetParameterValue("Cadenaoriginal", sdkresp.Cadena)
    '                reporte.SetParameterValue("foliofiscal", seriefactura & foliofactura)
    '                reporte.SetParameterValue("RFCCLIENTE", varRFCRECEPTOR)
    '                reporte.SetParameterValue("CERTIFICADOSAT", sdkresp.CertificadoSAT)
    '                reporte.SetParameterValue("nota", txtObservaciones.Text)
    '                reporte.SetParameterValue("SerieCertificado", VARNODECERTIFICADO)

    '                reporte.SetParameterValue("Sellodigital", VARSELLOSAT)
    '                reporte.SetParameterValue("SelloCFDI", VARSELLOCFD)
    '                reporte.SetParameterValue("UUID", varUUID)
    '                reporte.SetParameterValue("Medidor", "")
    '                reporte.SetParameterValue("Promedio", "")

    '            Catch ex As Exception
    '                MessageBox.Show(ex.Message)
    '            End Try
    '        End If

    '        Ejecucion("update empresa set foliofactura=" & foliofactura & "")

    '        If chksinrecibo.Checked Then
    '            reporte.Load(AppPath() & "\REPORTES\FACTURADIA33.RPT")
    '            Dim dataSet As DataSet = New DataSet

    '            dataSet.ReadXml(cadenaxml, XmlReadMode.Auto)
    '            reporte.SetDataSource(dataSet)
    '            Try


    '                reporte.SetParameterValue("cantidadconletra", ConvertCurrencyToSpanish(total, "Pesos"))

    '                reporte.SetParameterValue("nota", txtObservaciones.Text)
    '                reporte.SetParameterValue("CADENAORIGINAL", sdkresp.Cadena)

    '                reporte.RecordSelectionFormula = "{cajas1.ID_CAJA}='" & My.Settings.caja & "'"


    '            Catch ex As Exception
    '                MessageBox.Show(ex.Message)
    '            End Try
    '        End If
    '        ' CREA EL REPORTE EN PDF 
    '        Dim cadenapdf As String = ""
    '        Try
    '            'cadenapdf = Application.StartupPath & "\facturas\" + nombresespacios & "\PdfFactura" & seriefactura & foliofactura & ".pdf"
    '            cadenapdf = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\facturas\" + nombresespacios & "\Factura" & seriefactura & foliofactura & ".pdf"
    '            ExportToDisk(cadenapdf, reporte)
    '            'Try
    '            '    If Not File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\facturas\" + nombresespacios & "\Factura" & seriefactura & foliofactura & ".pdf") Then
    '            '        System.IO.File.Copy(cadenapdf, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\facturas\" + nombresespacios & "\Factura" & seriefactura & foliofactura & ".pdf", True)
    '            '    End If
    '            'Catch ex As Exception

    '            'End Try
    '        Catch ex As Exception
    '            MessageBox.Show("error al crear pdf " & ex.Message)
    '        End Try




    '        Process.Start(cadenapdf)

    '        'imprimirpdf(cadenapdf)
    '        'imprimirpdf(cadenapdf)

    '        For i = 0 To reporte.Database.Tables.Count - 1
    '            Dim tabla As Table = reporte.Database.Tables.Item(i)
    '            tabla.Dispose()
    '        Next
    '        'reporte.Database.Dispose()
    '        reporte.Dispose()


    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try


    '    Close()
    'End Sub ' para multifacturas




    ''Rutina ItextSharp







    Public Sub Timbrar(ByVal enviarFacturaMail As Boolean)



        Dim nombre, nombresespacios As String

        nombre = txtnombre.Text
        nombresespacios = nombre.Replace(" ", "")


        Try



            Dim mf As MFSDK = New MFSDK()



            foliofactura = Val(obtenerCampo("select * from empresa limit 1 ", "foliofactura")) + 1

            seriefactura = obtenerCampo("select * from empresa limit 1 ", "seriefactura")



            Dim nombreEM As String = obtenerCampo("select * from empresa", "CNOMBRE")
            Dim _uso As String = cmbuso.SelectedValue
            Dim _metodo As String = cmbmetodo.SelectedValue


            iva = Math.Round(total - subtotal, 2)
            mf = MakeFileToSendMultifacturas_v4(control, iva, subtotal, total, seriefactura, foliofactura, cmbformapago.SelectedValue, _metodo, _uso, txtnombre.Text, txtrfc.Text, nombreEM, cmbRegimen.SelectedValue, txtcp.Text)





            Try
                sdkresp = mf.Timbrar("C:\sdk2\timbrar32.bat", "C:\sdk2\timbrados\", "factura", False)
            Catch ex As Exception
                MessageBox.Show(sdkresp.Codigo_MF_Texto.ToString())
                MessageBox.Show($"Ocurrio en el timbrado: {ex.ToString()}")
            End Try






            Dim ultimoUUIDTimbrado As String = obtenerCampo("select UUID from ENCFAC where caja = " & My.Settings.caja & " order by fecha desc limit 1", "UUID")

            If sdkresp.Codigo_MF_Texto = "NO SE PUDO LEER CERTIFICADO" Then
                MessageBox.Show("NO SE PUDO LEER CERTIFICADO")
                Return
            End If
            If sdkresp.UUID = "" Then
                MessageBox.Show("No timbro, vuelve a intentar")
                Return
            End If

            ''Validar que el UUID sea de un nuevo timbrado exitosamente

            If ultimoUUIDTimbrado = sdkresp.UUID Then

                MessageBox.Show("Este UUID ya fue generado anteriormente, intenta facturar nuevamente desde el listado de recibos")
                Return

            Else


            End If



            If Not sdkresp.Codigo_MF_Texto.Contains("OK") Then



                MessageBox.Show(sdkresp.Codigo_MF_Texto)
                'sdkresp = sdk.Timbrar(cfdi, "c:\facturas\", "XmlFactura" & seriefactura & foliofactura)
                Try
                    guardatxt("C:\facturas\errores", "C:\facturas\errores\dia" & Now.Year & Now.Month & Now.Day & ".txt", "Factura " & seriefactura & foliofactura & "->" & sdkresp.Codigo_MF_Texto)
                Catch ex2 As Exception

                End Try

                Return

            End If




        Catch ex As Exception

        End Try


        Dim filtro As String = String.Empty
        Try


            If vienede = "CAJA" Then
                Reciboqueseestafacturando = My.Settings.folio
                seriedelreciboqueseestafacturando = My.Settings.serie

                Ejecucion("insert into encfac SET FECHA=CONCAT('" & Now.Year & "-" & Now.Month & "-" & Now.Day & "', ' ' ,curtime()) " &
                  ",NOMBRE='" & recibo.nombre.Replace("'", "''").Trim() &
                  "',  SUBTOTAL=" & recibo.subtotal & ",IVA=" & recibo.iva &
                  ",TOTAL=" & recibo.total & ",TIPO='" & recibo.esusuario &
                  "', ESTADO='A', CAJA='" & My.Settings.caja &
                  "', USUARIO='" & usuariodelsistema &
                  "', motivocancelacion='', Advertencia ='" & sdkresp.Advertencia &
                   "', cadena='" & sdkresp.Cadena &
                   "', certificadoSAT= '" & sdkresp.CertificadoSAT &
                   "', cfdi =' " & sdkresp.CFDI.Replace("'", "''") &
                   "', codigo_mf_texto=' " & sdkresp.Codigo_MF_Texto &
                   "', ejecucion ='" & sdkresp.Ejecucion &
                   "', fechatimbrado ='" & sdkresp.FechaTimbrado &
                   "', idpac ='" & sdkresp.IdPac &
                   "', mensajeoriginalpacjson ='" &
                   "', nodecertificado ='" & sdkresp.NoCertificado &
                   "',pac='" & sdkresp.Pac &
                   "',png64='" & sdkresp.PNG64 &
                   "',produccion='" & sdkresp.Produccion &
                   "', respuestaoriginal = '" &
                   "', rutapng ='" & sdkresp.RutaPNG.Replace("\", "\\") &
                   "', rutaxml ='" & sdkresp.RutaXML.Replace("\", "\\") &
                   "', sello ='" & sdkresp.Sello & "', sellosat='" & sdkresp.SelloSAT & "' " &
                   ", servidor='" & sdkresp.Servidor & "', uuid='" & sdkresp.UUID &
                   "', serie='" & seriefactura & "', numero=" & foliofactura &
                   ", version='4.0'" &
                   ", recibo=" & My.Settings.folio & ", serierecibo='" & My.Settings.serie & "'" &
                   ", CuentaDatosFiscales='" & cuenta & "'")
            End If
            If vienede = "RECIBO" Then
                Ejecucion("insert into encfac SET FECHA=CONCAT('" & Now.Year & "-" & Now.Month & "-" & Now.Day & "', ' ' ,curtime()) " &
                 ",NOMBRE='" & recibo.nombre.Replace("'", "''").Trim() &
                 "',  SUBTOTAL=" & recibo.subtotal & ",IVA=" & recibo.iva &
                 ",TOTAL=" & recibo.total & ",TIPO='" & recibo.esusuario &
                 "', ESTADO='A', CAJA='" & My.Settings.caja &
                 "', USUARIO='" & usuariodelsistema &
                 "', motivocancelacion='', Advertencia ='" & sdkresp.Advertencia &
                  "', cadena='" & sdkresp.Cadena &
                  "', certificadoSAT= '" & sdkresp.CertificadoSAT &
                  "', cfdi =' " & sdkresp.CFDI.Replace("'", "''") &
                  "', codigo_mf_texto=' " & sdkresp.Codigo_MF_Texto &
                  "', ejecucion ='" & sdkresp.Ejecucion &
                  "', fechatimbrado ='" & sdkresp.FechaTimbrado &
                  "', idpac ='" & sdkresp.IdPac &
                  "', mensajeoriginalpacjson ='" &
                  "', nodecertificado ='" & sdkresp.NoCertificado &
                  "',pac='" & sdkresp.Pac &
                  "',png64='" & sdkresp.PNG64 &
                  "',produccion='" & sdkresp.Produccion &
                  "', respuestaoriginal = '" &
                  "', rutapng ='" & sdkresp.RutaPNG.Replace("\", "\\") &
                  "', rutaxml ='" & sdkresp.RutaXML.Replace("\", "\\") &
                  "', sello ='" & sdkresp.Sello & "', sellosat='" & sdkresp.SelloSAT & "' " &
                  ", servidor='" & sdkresp.Servidor & "', uuid='" & sdkresp.UUID &
                  "', serie='" & seriefactura & "', numero=" & foliofactura &
                  ", version='4.0'" &
                  ", recibo=" & My.Settings.folio & ", serierecibo='" & My.Settings.serie & "'" &
                   ", CuentaDatosFiscales='" & cuenta & "'")
            End If

            Ejecucion("update cajas set folio=" & foliofactura & " WHERE id_caja='" & My.Settings.caja & "'")
            Ejecucion("update empresa set foliofactura=" & foliofactura & "")


            If vienede = "CAJA" Then
                Ejecucion("update pagos set factura=" & foliofactura & ",facturado=" & foliofactura & ", seriefactura='" & seriefactura & "' where recibo=" & My.Settings.folio & " and serie='" & My.Settings.serie & "'")
                filtro = "{cajas1.id_caja}='" & My.Settings.caja & "' and {pagos1.recibo}=" & My.Settings.folio & " and {pagos1.serie}='" & My.Settings.serie & "'"
            End If


            If vienede = "RECIBO" Then
                Ejecucion("update pagos set factura=" & foliofactura & ",facturado=" & foliofactura & ", seriefactura='" & seriefactura & "' where recibo=" & Reciboqueseestafacturando & " and serie='" & seriedelreciboqueseestafacturando & "'")
                filtro = "{cajas1.id_caja}='" & My.Settings.caja & "' and {pagos1.recibo}=" & Reciboqueseestafacturando & " and {pagos1.serie}='" & seriedelreciboqueseestafacturando & "'"
            End If



        Catch ex As Exception
            MessageBox.Show($"LA FACTURA FUE TIMBRADA PERO HUBO UN ERROR INESPERADO AL REGISTRARLA EN EL SISTEMA: {ex.ToString()}")
        End Try


        Try

            'Dim tipoUsuario As Short = ConvertirTipoUsuario()

            Dim objFactura As New FormatoFactura_40()
            objFactura.GenerarPDFFactura_CFDI4(seriefactura, foliofactura, cuenta, False, recibo.esusuario, enviarFacturaMail)

        Catch ex As Exception

            MessageBox.Show($"Ocurrio un error al generar el PDF de la factura: {ex.ToString()}")

        End Try

        Me.Close()


    End Sub ' para multifacturas ITextSharp

    Public Function validarEmail(ByVal correo) As Boolean
        ' Dim email As New Regex("^(?<user>[^@]+)@(?<host>.+)$")
        Dim email As New Regex("([\w-+]+(?:\.[\w-+]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7})")
        If email.IsMatch(correo) Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub RadGroupBox2_Click(sender As Object, e As EventArgs) Handles RadGroupBox2.Click

    End Sub

    'convierte una imagen en byte para poderlo poner en crystal report datamatrix

    'Public Sub GuardarLecturasAD()
    '    '  Dim x As base = New base

    '    Dim montolectura As Double = 0
    '    Dim alcantarillado As Double = 0
    '    Dim saneamiento As Double = 0
    '    Dim consumo As Double = 0
    '    Dim totalConsu As Double = 0
    '    Dim totalAlcan As Double = 0
    '    Dim totalSanea As Double = 0
    '    Dim totalAnticipo As Double = 0
    '    Dim concepto As New ClsRegistrolectura

    '    ''''' rutina para el pago de lecturas adelantadas

    '    Try
    '        'For i = 0 To control.desgloseAnticipo.Count - 1
    '        'concepto = control.desgloseAnticipo.Item(i + 1)
    '        For i = 0 To control.desgloselecturas.Count - 1
    '            concepto = control.desgloselecturas.Item(i + 1)
    '            Try
    '                dts = ConsultaSql("select cuenta, mes, an_per from lecturas where cuenta='" & control.cuenta & "' and mes='" & concepto.Mes & "' and an_per='" & concepto.Periodo & "'").ExecuteReader
    '                If dts.Read = True Then
    '                    MessageBox.Show("La Lectura del Periodo: " & concepto.Mes & "-" & concepto.Periodo & " ya Existe")
    '                Else

    '                    ' Dim consumo As Double = x.obtenerCampo("SELECT ConsumoMedidosSin(" & concepto.Consumo & ",'" & control.Tarifa & "') as Consumo  ", "Consumo")
    '                    consumo = concepto.Totalcondescuento
    '                    For cicloalcanta = 1 To control.desglosealcantarillado.Count
    '                        Dim conceptoalcan As ClsRegistrolectura
    '                        conceptoalcan = control.desglosealcantarillado(cicloalcanta)
    '                        If concepto.Mes = conceptoalcan.Mes And concepto.Periodo = conceptoalcan.Periodo Then
    '                            alcantarillado = conceptoalcan.Totalcondescuento
    '                        End If
    '                    Next



    '                    For ciclosanea = 1 To control.desglosesaneamiento.Count
    '                        Dim conceptoalcan As ClsRegistrolectura
    '                        conceptoalcan = control.desglosesaneamiento(ciclosanea)
    '                        If concepto.Mes = conceptoalcan.Mes And concepto.Periodo = conceptoalcan.Periodo Then
    '                            saneamiento = conceptoalcan.Totalcondescuento
    '                        End If
    '                    Next

    '                    montolectura = consumo + alcantarillado + saneamiento
    '                    Try
    '                        Ejecucion("insert into lecturas set cuenta='" & control.cuenta & "'," & _
    '                                   "mes= '" & concepto.Mes & "', an_per= " & concepto.Periodo & ", consumo=" & concepto.Consumo & "," & _
    '                                   "montocobrado=" & montolectura & " , adelant= 0 , AConsumo=" & consumo & ", AAlcantarillado= " & alcantarillado & _
    '                                   ", pagado=1, ASaneamiento= " & saneamiento & ",lectant=ultimalectura(" & recibo.cuenta & "),lectura=ultimalectura(" & recibo.cuenta & ");")

    '                    Catch ex As Exception
    '                        MessageBox.Show(ex.Message)
    '                    End Try

    '                End If

    '            Catch ex As Exception

    '            End Try
    '            totalConsu = totalConsu + consumo
    '            totalAlcan = totalAlcan + alcantarillado
    '            totalSanea = totalSanea + saneamiento
    '        Next

    '        For i = 0 To control.desgloseAnticipo.Count - 1
    '            concepto = control.desgloseAnticipo.Item(i + 1)
    '            Try

    '            Catch ex As Exception
    '                guardatxt("c:\errorpagos", "errorpagos.txt", ex.Message)
    '            End Try

    '        Next



    '        totalAnticipo = totalConsu + totalAlcan + totalSanea

    '        'txtAConsumo.Text = totalConsu
    '        'txtAAlcantarillado.Text = totalAlcan
    '        'txtASaneamiento.Text = totalSanea
    '        'txtTotalAnticipos.Text = totalAnticipo
    '        Dim cadenainanti As New StringBuilder
    '        cadenainanti.Append("INSERT INTO anticipos set Fecha='" & Now.Year & "-" & Now.Month & "-" & Now.Day & "',")
    '        cadenainanti.Append("Cuenta=" & control.cuenta & ",")
    '        cadenainanti.Append("idMov='" & My.Settings.serie & (My.Settings.folio + 1) & "',")
    '        cadenainanti.Append("Status='A',")
    '        cadenainanti.Append("Monto=" & totalAnticipo & ",")
    '        cadenainanti.Append("Recibo =" & My.Settings.folio + 1 & ",")
    '        cadenainanti.Append("Serie = '" & My.Settings.serie & "',")
    '        cadenainanti.Append("meses= " & control.desgloseAnticipo.Count & ",")
    '        cadenainanti.Append("Consumo=" & totalConsu & ",")
    '        cadenainanti.Append("Alcantarillado=" & totalAlcan & ",")
    '        cadenainanti.Append("Saneamiento=" & totalSanea & "")


    '        Ejecucion(cadenainanti.ToString())
    '        ejecucion("UPDATE usuario SET credito=Credito + " & totalAnticipo & "  WHERE CUENTA='" & control.cuenta & "'")


    '    Catch ex As Exception

    '    End Try

    'End Sub

End Class

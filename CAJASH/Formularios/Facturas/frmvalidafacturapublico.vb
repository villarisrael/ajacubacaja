Imports System.Data.Odbc
Imports System.IO
Imports System.Text
Imports System.Xml
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports MultiFacturasSDK

Public Class Frmvalidafacturapublico
    Public recibo As New clsrecibo
    Public control As New Clscontrolpago

    Private archivotexto As String
    Private archivopdf As String
    Private strStreamW As Stream = Nothing
    Private strStreamWriter As StreamWriter = Nothing

    Public subtotal As Double
    Public iva As Double
    Public total As Double

    Public cuenta As Integer
    Public TIPO As String

    Public quehacer As String = "ACTUALIZAR"
    Dim basemy As New base
    Dim fechaincial As String
    Dim fechafinal As String
    Dim foliofactura As Long = 0
    Dim seriefactura As String = ""


    Private Sub Frmvalidafactura_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim basem As New base

        Dim datosf As OdbcDataReader = basem.consultasql("SELECT * FROM empresa limit 1")

        If datosf.Read Then
            quehacer = "ACTUALIZAR"
            Try

                ' txtnombre.Text = datosf("CNOMBRE")
                txtcalle.Text = datosf("CDOMICILIO")
                txtnumext.Text = datosf("NUMEXT")
                txtnuminterior.Text = datosf("NUMINT")
                txtcolonia.Text = datosf("CCOLONIA")
                txtdelegacion.Text = datosf("CDELEGACION")
                txtpoblacion.Text = datosf("CPOBLACION")
                txtestado.Text = datosf("ESTADO")
                txtPais.Text = datosf("PAIS")
                txtcp.Text = datosf("CCODPOS")
                txtrfc.Text = datosf("RFCPUBLICO")
                txtmail.Text = datosf("CEMAIL")
            Catch ex As Exception


            End Try

        End If


    End Sub

    Private Sub btnaceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaceptar.Click

        conectar()

        If total = 0 Then
            MessageBox.Show("No hay nada que facturar")
            Exit Sub
        End If
        Try
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
            'If txtmail.Text = "" Then
            '    cadena = cadena & "MAIL" & vbCrLf
            'End If
            If cadena.Length > 0 Then
                MessageBox.Show("LOS CAMPOS " & cadena & " NO PUEDEN IR VACIOS")
                Exit Sub
            End If
        Catch ex As Exception

        End Try

        Try
            timbrar(False)
            Close()
        Catch ex As Exception

        End Try



    End Sub


    Private Sub BTNGUARDAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNGUARDAR.Click
        Dim CADENA As String = ""
        Select Case quehacer
            Case "INSERTAR"
                ' CADENA = "INSERT INTO DATOSFISCALES SET TIPO='" & TIPO & "', CUENTA=" & cuenta & ","
            Case "ACTUALIZAR"
                CADENA = "UPDATE EMPRESA SET "
        End Select
        CADENA = CADENA & " CNOMBRE ='" & txtnombre.Text & "', CDOMICILIO='" & txtcalle.Text & "',NUMEXT='" & txtnumext.Text & "', NUMINT ='" & txtnuminterior.Text & "', CCOLONIA='" & txtcolonia.Text & "', CPOBLACION='" & txtpoblacion.Text & "' , CDELEGACION='" & txtdelegacion.Text & "', ESTADO='" & txtestado.Text & "', PAIS='" & txtPais.Text & "', CCODPOS='" & txtcp.Text & "', RFCPUBLICO='" & txtrfc.Text & "' ,CEMAIL='" & txtmail.Text & "'"

        If quehacer = "ACTUALIZAR" Then
            CADENA = CADENA & " WHERE CODEMP=1"
        End If
        Dim basem As New base
        basem.ejecutar(CADENA)
    End Sub

    Private Sub btncancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancelar.Click
        Close()
    End Sub


    Private Sub btncargar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncargar.Click


        control = New Clscontrolpago

        basemy.conectar()
        fechaincial = DTPincio.Value.Year & "-" & DTPincio.Value.Month & "-" & DTPincio.Value.Day
        fechafinal = DtPfin.Value.Year & "-" & DtPfin.Value.Month & "-" & DtPfin.Value.Day

        If txtcaja.Text.Trim() = "" Then

            basemy.ejecutarSIMPLE("truncate table temfacpub")
            'basemy.ejecutarSIMPLE("INSERT INTO temfacpub (CONCEPTO,CANTIDAD,IMPORTE, IVA, MONTO) SELECT otroscobros.descripcion as concepto, 1 , sum(PAGOTROS.IMPORTE) AS IMPORTE , sum(pagotros.monto) AS MONTO, PAGOTROS.IVA  FROM PAGOS,PAGOTROS,otroscobros WHERE PAGOS.SERIE=PAGOTROS.SERIE AND PAGOS.RECIBO=PAGOTROS.RECIBO AND  FECHA_ACT>='" & fechaincial & "' and fecha_act<='" & fechafinal & "' and facturado=0 and PAGOTROS.NUMCONCEPTO=OTROSCOBROS.CLAVE GROUP BY CONCEPTO,IVA")
            ' basemy.ejecutarSIMPLE("INSERT INTO temfacpub (CONCEPTO,CANTIDAD,IMPORTE,preciouni, IVA, MONTO) SELECT otroscobros.descripcion as concepto, 1 , sum(PAGOTROS.IMPORTE), sum(PAGOTROS.IMPORTE)  , PAGOTROS.IVA as IVA, sum(pagotros.monto) AS MONTO FROM PAGOS,PAGOTROS,otroscobros WHERE PAGOS.SERIE=PAGOTROS.SERIE AND PAGOS.CANCELADO='A' AND PAGOS.RECIBO=PAGOTROS.RECIBO AND  FECHA_ACT>='" & fechaincial & "' and fecha_act<='" & fechafinal & "' and facturado=0 and PAGOTROS.NUMCONCEPTO=OTROSCOBROS.CLAVE GROUP BY CONCEPTO,IVA")

            'basemy.ejecutarSIMPLE("INSERT INTO temfacpub (CONCEPTO,CANTIDAD,PRECIOUNI,IMPORTE, IVA,MONTO, CONCEPTOSAT,UNIDADSAT) SELECT if (pagotros.IVA=0,conceptoscxc.descripcion, CONCAT(conceptoscxc.descripcion,' GRABADO'))  as concepto, 1 , sum(PAGOTROS.IMPORTE) AS IMPORTE ,round( sum(PAGOTROS.IMPORTE),2) , PAGOTROS.IVA, round( sum(PAGOTROS.IMPORTE),2),conceptoscxc.clavesat , conceptoscxc.unidadsat  FROM PAGOS,PAGOTROS,conceptoscxc WHERE PAGOS.SERIE=PAGOTROS.SERIE AND PAGOS.RECIBO=PAGOTROS.RECIBO AND  FECHA_ACT>='" & fechaincial & "' and fecha_act<='" & fechafinal & "' and facturado=0 and Pagos.Cancelado='A' and PAGOTROS.NUMCONCEPTO=conceptoscxc.id_concepto GROUP BY CONCEPTO,IVA")

            Try



                basemy.ejecutarSIMPLE($"INSERT INTO temfacpub (CONCEPTO, CANTIDAD, PRECIOUNI, IMPORTE, IVA, MONTO, CONCEPTOSAT, UNIDADSAT) SELECT IF(pagotros.IVA = 0, conceptoscxc.descripcion, CONCAT(conceptoscxc.descripcion, ' GRABADO')) AS concepto, 1 AS CANTIDAD, ROUND(SUM(pagotros.IMPORTE), 2) AS PRECIOUNI, 
            ROUND(SUM(pagotros.IMPORTE), 2) AS IMPORTE, pagotros.IVA, ROUND(SUM(pagotros.IMPORTE), 2) AS MONTO, conceptoscxc.clavesat AS CONCEPTOSAT, conceptoscxc.unidadsat AS UNIDADSAT FROM PAGOS LEFT JOIN PAGOTROS ON PAGOS.SERIE = PAGOTROS.SERIE AND PAGOS.RECIBO = PAGOTROS.RECIBO 
            LEFT JOIN conceptoscxc ON PAGOTROS.NUMCONCEPTO = conceptoscxc.id_concepto WHERE FECHA_ACT BETWEEN '{fechaincial}' AND '{fechafinal}' AND facturado = 0 AND Pagos.Cancelado = 'A' GROUP BY concepto, pagotros.IVA")

            Catch ex As Exception

                MessageBox.Show($"OCURRIO UN ERROR AL CARGAR LOS DATOS: {ex.ToString()}")

            End Try

        ElseIf txtcaja.Text <> "" Then



            basemy.ejecutarSIMPLE("truncate table temfacpub")
            'basemy.ejecutarSIMPLE("INSERT INTO temfacpub (CONCEPTO,CANTIDAD,IMPORTE, IVA, MONTO) SELECT otroscobros.descripcion as concepto, 1 , sum(PAGOTROS.IMPORTE) AS IMPORTE , sum(pagotros.monto) AS MONTO, PAGOTROS.IVA  FROM PAGOS,PAGOTROS,otroscobros WHERE PAGOS.SERIE=PAGOTROS.SERIE AND PAGOS.RECIBO=PAGOTROS.RECIBO AND  FECHA_ACT>='" & fechaincial & "' and fecha_act<='" & fechafinal & "' and facturado=0 and PAGOTROS.NUMCONCEPTO=OTROSCOBROS.CLAVE GROUP BY CONCEPTO,IVA")
            ' basemy.ejecutarSIMPLE("INSERT INTO temfacpub (CONCEPTO,CANTIDAD,IMPORTE,preciouni, IVA, MONTO) SELECT otroscobros.descripcion as concepto, 1 , sum(PAGOTROS.IMPORTE), sum(PAGOTROS.IMPORTE)  , PAGOTROS.IVA as IVA, sum(pagotros.monto) AS MONTO FROM PAGOS,PAGOTROS,otroscobros WHERE PAGOS.SERIE=PAGOTROS.SERIE AND PAGOS.CANCELADO='A' AND PAGOS.RECIBO=PAGOTROS.RECIBO AND  FECHA_ACT>='" & fechaincial & "' and fecha_act<='" & fechafinal & "' and facturado=0 and PAGOTROS.NUMCONCEPTO=OTROSCOBROS.CLAVE GROUP BY CONCEPTO,IVA")

            'basemy.ejecutarSIMPLE("INSERT INTO temfacpub (CONCEPTO,CANTIDAD,PRECIOUNI,IMPORTE, IVA,MONTO, CONCEPTOSAT,UNIDADSAT) SELECT if (pagotros.IVA=0,conceptoscxc.descripcion, CONCAT(conceptoscxc.descripcion,' GRABADO'))  as concepto, 1 , sum(PAGOTROS.IMPORTE) AS IMPORTE ,round( sum(PAGOTROS.IMPORTE),2) , PAGOTROS.IVA, round( sum(PAGOTROS.IMPORTE),2),conceptoscxc.clavesat , conceptoscxc.unidadsat  FROM PAGOS,PAGOTROS,conceptoscxc WHERE PAGOS.SERIE=PAGOTROS.SERIE AND PAGOS.RECIBO=PAGOTROS.RECIBO AND  FECHA_ACT>='" & fechaincial & "' and fecha_act<='" & fechafinal & "' and facturado=0 and Pagos.Cancelado='A' and PAGOTROS.NUMCONCEPTO=conceptoscxc.id_concepto GROUP BY CONCEPTO,IVA")

            Try



                basemy.ejecutarSIMPLE($"INSERT INTO temfacpub (CONCEPTO, CANTIDAD, PRECIOUNI, IMPORTE, IVA, MONTO, CONCEPTOSAT, UNIDADSAT) SELECT IF(pagotros.IVA = 0, conceptoscxc.descripcion, CONCAT(conceptoscxc.descripcion, ' GRABADO')) AS concepto, 1 AS CANTIDAD, SUM(pagotros.IMPORTE) AS PRECIOUNI, 
            ROUND(SUM(pagotros.IMPORTE), 2) AS IMPORTE, pagotros.IVA, ROUND(SUM(pagotros.IMPORTE), 2) AS MONTO, conceptoscxc.clavesat AS CONCEPTOSAT, conceptoscxc.unidadsat AS UNIDADSAT FROM PAGOS LEFT JOIN PAGOTROS ON PAGOS.SERIE = PAGOTROS.SERIE AND PAGOS.RECIBO = PAGOTROS.RECIBO 
            LEFT JOIN conceptoscxc ON PAGOTROS.NUMCONCEPTO = conceptoscxc.id_concepto WHERE FECHA_ACT BETWEEN '{fechaincial}' AND '{fechafinal}' AND facturado = 0 AND Pagos.Cancelado = 'A' and Pagos.CAJA = {txtcaja.Text.Trim()} GROUP BY concepto, pagotros.IVA")


            Catch ex As Exception

                MessageBox.Show($"OCURRIO UN ERROR AL CARGAR LOS DATOS: {ex.ToString()}")

            End Try

        End If

        '  basemy.ejecutarSIMPLE("drop table if exists temfacpuB ; CREATE TABLE temfacpub (  `id` INT NOT NULL AUTO_INCREMENT,  `CONCEPTO` VARCHAR(100) NULL,  `CANTIDAD` INT NOT NULL DEFAULT 1,  `PRECIOUNI` DECIMAL(18,2) NOT NULL DEFAULT 1,  `IMPORTE` VARCHAR(45) NOT NULL DEFAULT '1',  `IVA` TINYINT NOT NULL DEFAULT 1,  PRIMARY KEY (`id`))")



        basemy.llenaGrid(DtgConceptos, "SELECT id, CONCEPTO, CANTIDAD, PRECIOUNI, IMPORTE, IVA, CONCEPTOSAT,unidadsat FROM TEMFACPUB")

        conectar()
        Dim DATOS2 As OdbcDataReader = ConsultaSql("SELECT * FROM TEMFACPUB").ExecuteReader

        Dim contador As Int16 = 1

        Try



            While DATOS2.Read

                Dim con2 As New Clsconcepto
                con2.Clave = contador
                con2.Cantidad = 1
                con2.clavesat = DATOS2("conceptosat")
                con2.unidadsat = DATOS2("unidadsat")
                con2.Concepto = DATOS2("concepto")
                con2.Preciounitario = DATOS2("preciouni")
                con2.importe = DATOS2("importe")
                con2.IVA = DATOS2("iva")
                control.Listadeconceptos.Add(con2)
                contador = contador + 1

            End While



            basemy.conectar()


            'Dim DATOS As OdbcDataReader = basemy.consultasql("SELECT SUM(MONTO) AS SUBTOTAL , SUM(MONTO * temfacpub.IVA*(EMPRESA.PorcIVA/100)) AS IVA FROM TEMFACPUB,EMPRESA WHERE EMPRESA.CODEMP=1")
            Dim DATOS As OdbcDataReader = basemy.consultasql("SELECT SUM(MONTO) AS SUBTOTAL , SUM(MONTO * temfacpub.IVA*(EMPRESA.PorcIVA/100)) AS IVA FROM TEMFACPUB,EMPRESA ")


            subtotal = 0
            iva = 0
            total = 0


            If DATOS.Read Then
                Try
                    subtotal = DATOS("subtotal")
                Catch ex As Exception
                    subtotal = 0
                End Try
                Try
                    iva = Math.Round(DATOS("IVA"), 2)
                Catch ex As Exception
                    iva = 0
                End Try

                total = Math.Round(subtotal + iva, 2)
                lblsutotal.Text = monedatext(subtotal).Text
                lblIva.Text = monedatext(iva).Text
                Lbltotal.Text = monedatext(total).Text
            Else
                lblsutotal.Text = subtotal
                lblIva.Text = iva
                Lbltotal.Text = total
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


        If txtcaja.Text = "" Then

            txtobservaciones.Text = "FACTURA CORRESPONDIENTE AL PERIODO DEL DIA " & DTPincio.Value.ToShortDateString & " AL DIA " & DtPfin.Value.ToShortDateString

        ElseIf txtcaja.Text <> "" Then

            txtobservaciones.Text = $"FACTURA CORRESPONDIENTE AL PERIODO DEL DIA {DTPincio.Value.ToShortDateString} AL DIA {DtPfin.Value.ToShortDateString} | CAJA: {txtcaja.Text}"

        End If


    End Sub




    Public Sub timbrar(ByVal enviarFacturaMail As Boolean)
        conectar()

        If total = 0 Then
            MessageBox.Show("No hay nada que facturar")
            Exit Sub
        End If
        Try
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
            'If txtmail.Text = "" Then
            '    cadena = cadena & "MAIL" & vbCrLf
            'End If
            If cadena.Length > 0 Then
                MessageBox.Show("LOS CAMPOS " & cadena & " NO PUEDEN IR VACIOS")
                Exit Sub
            End If
        Catch ex As Exception

        End Try

        If My.Computer.Network.IsAvailable Then



            'Dim directorio As String = ""
            'Dim directorio2 As String = ""



            'directorio = Application.StartupPath & "\facturas\" & nombresespacios
            'directorio2 = Application.StartupPath & "\facturas" & Year(Now) & acompletacero(Month(Now), 2) & "\"


            Try

                Dim sdkresp As SDKRespuesta

                Dim mf As MFSDK = New MFSDK()


                Try


                    foliofactura = Val(obtenerCampo("select * from empresa", "foliofactura")) + 1
                    seriefactura = obtenerCampo("select * from empresa", "seriefactura")

                Catch ex As Exception

                    MessageBox.Show($"Ocurrio un error al obtener el folio o serie de factura correspondiente")

                End Try


                Dim nombreEM As String = obtenerCampo("select * from empresa", "CNOMBRE")





                Try



                    mf = MakeFileToSendMultifacturas_v4(control, iva, subtotal, total, seriefactura, foliofactura, "01", "PUE", "S01", txtnombre.Text, txtrfc.Text, nombreEM, "616", txtcp.Text, True)

                    sdkresp = mf.Timbrar("C:\sdk2\timbrar32.bat", "C:\sdk2\timbrados\", "factura", False)

                Catch ex As Exception

                    MessageBox.Show($"Ocurrio un error: {ex.ToString()}")

                End Try




                'Validar que este generando un nuevo UUID, si no, no esta haciendo un correcto timbrado

                Dim ultimoUUIDTimbrado As String = obtenerCampo("select UUID from ENCFAC where caja = " & My.Settings.caja & " order by fecha desc limit 1", "UUID")

                If ultimoUUIDTimbrado = sdkresp.UUID Then

                    MessageBox.Show("El UUID ya fue generado anteriormente, vuelve a intentar facturar")
                    Return

                End If




                If Not sdkresp.Codigo_MF_Texto.Contains("OK") Then
                    MessageBox.Show(sdkresp.Codigo_MF_Texto)

                    Return
                End If




                Try


                    Ejecucion("insert into encfac SET FECHA=CONCAT(CURDATE(), ' ' ,curtime()) " &
                          ",NOMBRE='PUBLICO EN GENERAL" &
                          "',  SUBTOTAL=" & subtotal & ",IVA=" & iva &
                          ",TOTAL=" & total & ",TIPO='4" &
                          "', ESTADO='A', CAJA='" & My.Settings.caja &
                          "', USUARIO='" & usuariodelsistema &
                          "', motivocancelacion='', Advertencia ='" & sdkresp.Advertencia &
                           "', cadena='" & sdkresp.Cadena &
                           "', certificadoSAT= '" & sdkresp.CertificadoSAT &
                           "', cfdi =' " & sdkresp.CFDI &
                           "', codigo_mf_texto=' " & sdkresp.Codigo_MF_Texto &
                           "', ejecucion ='" & sdkresp.Ejecucion &
                           "', fechatimbrado ='" & sdkresp.FechaTimbrado &
                           "', idpac ='" & sdkresp.IdPac &
                           "', mensajeoriginalpacjson ='" & sdkresp.MensajeOriginalPacJSON &
                           "', nodecertificado ='" & sdkresp.NoCertificado &
                           "',pac='" & sdkresp.Pac &
                           "',png64='" & sdkresp.PNG64 &
                           "',produccion='" & sdkresp.Produccion &
                           "', respuestaoriginal = '" & sdkresp.RespuestaOriginalSDK &
                           "', rutapng ='" & sdkresp.RutaPNG.Replace("\", "\\") &
                           "', rutaxml ='" & sdkresp.RutaXML.Replace("\", "\\") &
                           "', sello ='" & sdkresp.Sello & "', sellosat='" & sdkresp.SelloSAT & "' " &
                           ", servidor='" & sdkresp.Servidor & "', uuid='" & sdkresp.UUID & "'" &
                           ", OBSERVACION=' " & txtobservaciones.Text &
                           "', serie='" & seriefactura & "', numero=" & foliofactura &
                           ", recibo=0, serierecibo='" & My.Settings.serie & "'")

                Catch ex As Exception

                    MessageBox.Show($"Ocurrio un error al grabar la factura en el sistema: {ex.ToString()}")

                End Try



                Try

                    Ejecucion("update empresa set foliofactura=" & foliofactura & " WHERE CODEMP='1'")

                Catch ex As Exception

                    MessageBox.Show($"Ocurrio un error ala ctualizar el folio de la factura en el sistema: {ex.ToString()}")

                End Try



                fechaincial = DTPincio.Value.Year & "/" & DTPincio.Value.Month & "/" & DTPincio.Value.Day
                fechafinal = DtPfin.Value.Year & "/" & DtPfin.Value.Month & "/" & DtPfin.Value.Day

                Try


                    If txtcaja.Text = "" Then
                        Ejecucion("update pagos SET facturado=" & foliofactura & ", seriefactura='" & seriefactura & "' where fecha_act>='" & fechaincial & "' and fecha_act<='" & fechafinal & "' and cancelado='A' and facturado=0")
                    Else
                        Ejecucion("update pagos SET facturado=" & foliofactura & ", seriefactura='" & seriefactura & "' where fecha_act>='" & fechaincial & "' and fecha_act<='" & fechafinal & "' and caja=" & txtcaja.Text & " and cancelado='A' and facturado=0")
                    End If

                Catch ex As Exception

                    MessageBox.Show($"Ocurrio un error: {ex.ToString()}")

                End Try




            Catch err As Exception

                MessageBox.Show($"Ocurrio un error al timbrar la factura: {err.ToString()}")

            End Try


            Try

                Dim tipoFactura As Short = ConvertirTipoUsuario("PERIODO")

                Dim objFactura As New FormatoFactura_40()
                objFactura.GenerarPDFFactura_CFDI4(seriefactura, foliofactura, cuenta, False, tipoFactura, enviarFacturaMail)

            Catch ex As Exception

                MessageBox.Show($"Ocurrio un error: {ex.ToString()}")

            End Try




        End If




        ' si tiene red





    End Sub


End Class

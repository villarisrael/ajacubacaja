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
            timbrar()
            Close()
        Catch ex As Exception

        End Try

        'If My.Computer.Network.IsAvailable Then



        '    'Dim directorio As String = ""
        '    'Dim directorio2 As String = ""


        '    Dim nombre, nombresespacios As String

        '    nombre = txtnombre.Text
        '    nombresespacios = nombre.Replace(" ", "")

        '    'Se crean los direcctorios para guardar las facturas      

        '    If Not My.Computer.FileSystem.DirectoryExists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\facturas\" & nombresespacios) Then

        '        My.Computer.FileSystem.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\facturas\" & nombresespacios)
        '    End If

        '    If Not My.Computer.FileSystem.DirectoryExists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\facturas\" & Year(Now) & acompletacero(Month(Now), 2)) Then

        '        My.Computer.FileSystem.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\facturas\" & Year(Now) & acompletacero(Month(Now), 2))
        '    End If



        '    Try
        '        If Not My.Computer.FileSystem.DirectoryExists(Application.StartupPath & "\facturas" & Year(Now) & acompletacero(Month(Now), 2) & "\") Then

        '            My.Computer.FileSystem.CreateDirectory(Application.StartupPath & "\facturas" & Year(Now) & acompletacero(Month(Now), 2) & "\")
        '        End If

        '    Catch ex As Exception

        '    End Try

        '    'directorio = Application.StartupPath & "\facturas\" & nombresespacios
        '    'directorio2 = Application.StartupPath & "\facturas" & Year(Now) & acompletacero(Month(Now), 2) & "\"
        '    Dim folio As Integer = 0
        '    Dim serie As String = ""
        '    Dim sdkresp As SDKRespuesta
        '    Try

        '        ' Se indica la informacion del PAC
        '        ' pac = New PAC(My.Settings.usuarioMultifacturas, My.Settings.PassFacturaMultifacturas, "SI")


        '        ' Se crea el objeto SDK
        '        Dim mf As MFSDK = New MFSDK()


        '        ' Se crea la factura


        '        folio = Val(obtenerCampo("select * from empresa", "foliofactura")) + 1
        '        serie = obtenerCampo("select * from empresa", "seriefactura")
        '        Dim nombreEM As String = obtenerCampo("select * from empresa", "CNOMBRE")
        '        mf = MakeFileToSendMultifacturas(control, iva, subtotal, total, serie, folio, "01", "PUE", "G03", txtnombre.Text, txtrfc.Text, nombreEM)

        '        sdkresp = mf.Timbrar("C:\sdk2\timbrar32.bat", "C:\sdk2\timbrados\", "factura", False)



        '        If Not sdkresp.Codigo_MF_Texto.Contains("OK") Then
        '            MessageBox.Show(sdkresp.Codigo_MF_Texto)

        '            Return
        '        End If
        '        Dim fs1 As FileStream = File.Create((Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\facturas\" & nombresespacios & "\FACTURA" & serie & folio & ".XML").Trim)

        '        ' Add text to the file.
        '        Dim info As Byte() = New UTF8Encoding(True).GetBytes(sdkresp.CFDI.ToString().TrimStart())
        '        fs1.Write(info, 0, info.Length)
        '        fs1.Close()



        '        Dim fs As FileStream = File.Create((Application.StartupPath & "\FACTURA" & serie & folio & ".XML").Trim)

        '        ' Add text to the file.

        '        fs.Write(info, 0, info.Length)
        '        fs.Close()



        '        Try '' grabando el datamatrix en la tabla cajas para que de ahi lo tome el crystal reports
        '            '  Dim codigo As Image = Image.FromFile(sdkresp.RutaPNG)
        '            ' convierto a bytes


        '            Dim imagecod As Byte() = ImageToByte(sdkresp.RutaPNG)

        '            ' lo pasa a la tabla cajas para grabar en la base la imagen de manera temporal 
        '            ' y asi logre poner el codigo de barras en el reporte
        '            ' me costo mucho trabajo y mucha experimentaccion
        '            ' 01/11/2016


        '            Dim dts As New DatosReciboTableAdapters.cajasTableAdapter
        '            dts.UpdateQueryimagen(imagecod, My.Settings.caja)


        '        Catch ex As Exception

        '        End Try
        '        Dim filtro As String

        '        Ejecucion("insert into encfac SET FECHA=CONCAT(CURDATE(), ' ' ,curtime()) " &
        '                  ",NOMBRE='PUBLICO EN GENERAL" &
        '                  "',  SUBTOTAL=" & subtotal & ",IVA=" & iva &
        '                  ",TOTAL=" & total & ",TIPO='4" &
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
        '                   "', mensajeoriginalpacjson ='" & sdkresp.MensajeOriginalPacJSON &
        '                   "', nodecertificado ='" & sdkresp.NoCertificado &
        '                   "',pac='" & sdkresp.Pac &
        '                   "',png64='" & sdkresp.PNG64 &
        '                   "',produccion='" & sdkresp.Produccion &
        '                   "', respuestaoriginal = '" & sdkresp.RespuestaOriginalSDK &
        '                   "', rutapng ='" & sdkresp.RutaPNG.Replace("\", "\\") &
        '                   "', rutaxml ='" & sdkresp.RutaXML.Replace("\", "\\") &
        '                   "', sello ='" & sdkresp.Sello & "', sellosat='" & sdkresp.SelloSAT & "' " &
        '                   ", servidor='" & sdkresp.Servidor & "', uuid='" & sdkresp.UUID & "'" &
        '                   ", OBSERVACION=' " & txtobservaciones.Text &
        '                   "', serie='" & serie & "', numero=" & folio &
        '                   ", recibo=0, serierecibo='" & My.Settings.serie & "'")

        '        Ejecucion("update empresa set foliofactura=" & folio & " WHERE CODEMP='1'")

        '        fechaincial = DTPincio.Value.Year & "/" & DTPincio.Value.Month & "/" & DTPincio.Value.Day
        '        fechafinal = DtPfin.Value.Year & "/" & DtPfin.Value.Month & "/" & DtPfin.Value.Day

        '        Ejecucion("update pagos SET facturado=" & folio & ", seriefactura='" & serie & "' where fecha_act>='" & fechaincial & "' and fecha_act<='" & fechafinal & "' and cancelado='A' and facturado=0")

        '        Dim reporte As New ReportDocument()

        '        Try
        '            reporte.Load(AppPath() & ".\Reportes\FACTURAdia33.rpt")
        '            ' reporte.RecordSelectionFormula = "{cajas1.ID_CAJA}=" & My.Settings.caja & ""

        '            Dim servidorreporte As String = My.Settings.servidorreporte
        '            Dim usuarioreporte As String = My.Settings.usuarioreporte
        '            Dim passreporte As String = My.Settings.passreporte
        '            Dim basereporte As String = My.Settings.basereporte

        '            reporte.DataSourceConnections.Item(1).SetConnection(servidorreporte, basereporte, False)
        '            reporte.DataSourceConnections.Item(1).SetLogon(usuarioreporte, passreporte)



        '            Dim dataSet As DataSet = New DataSet

        '            dataSet.ReadXml((Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\facturas\" & nombresespacios & "\FACTURA" & serie & folio & ".XML").Trim, XmlReadMode.Auto)
        '            reporte.SetDataSource(dataSet)


        '        Catch ex As Exception
        '            MessageBox.Show(ex.Message)
        '        End Try


        '        Try


        '            reporte.SetParameterValue("cantidadconletra", ConvertCurrencyToSpanish(total, "Pesos"))

        '            reporte.SetParameterValue("nota", txtobservaciones.Text)
        '            reporte.SetParameterValue("CADENAORIGINAL", sdkresp.Cadena)

        '            reporte.RecordSelectionFormula = "{cajas1.ID_CAJA}='" & My.Settings.caja & "'"


        '        Catch ex As Exception
        '            MessageBox.Show(ex.Message)
        '        End Try

        '        ' CREA EL REPORTE EN PDF 
        '        Dim cadenapdf As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\facturas\" + nombresespacios & "\Factura" & serie & folio & ".pdf"
        '        Try
        '            'cadenapdf = Application.StartupPath & "\facturas\" + nombresespacios & "\PdfFactura" & seriefactura & foliofactura & ".pdf"

        '            ExportToDisk(cadenapdf, reporte)

        '        Catch ex As Exception
        '            MessageBox.Show("error al crear pdf " & ex.Message)
        '        End Try



        '        ' VISUALIZA EL PDF EN PANTALLA
        '        ' If chkvisualizar.Checked Then
        '        Try
        '            Dim psi As New ProcessStartInfo("Factura" & serie & folio & ".pdf")
        '            psi.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\facturas\" + nombresespacios

        '            psi.WindowStyle = ProcessWindowStyle.Hidden
        '            Dim p As Process = Process.Start(psi)
        '        Catch ex As Exception
        '            MessageBox.Show("error al visualizar el pdf" & ex.Message)
        '        End Try



        '        'End If

        '        'imprimir automaticamente
        '        'imprimirpdf(cadenapdf)
        '        'imprimirpdf(cadenapdf)

        '        For i = 0 To reporte.Database.Tables.Count - 1
        '            Dim tabla As Table = reporte.Database.Tables.Item(i)
        '            tabla.Dispose()
        '        Next
        '        reporte.Database.Dispose()
        '        reporte.Dispose()




        '    Catch ex As Exception
        '        'sdk.CreaINI(cfdi, "c:\facturas\XmlFactura" & seriefactura & foliofactura)
        '        'sdkresp = sdk.Timbrar(cfdi, "c:\facturas\", "XmlFactura" & seriefactura & foliofactura)
        '        Try
        '            guardatxt("C:\facturas\errores", "C:\facturas\errores\dia" & Now.Year & Now.Month & Now.Day & ".txt", "Factura " & serie & folio & "->" & sdkresp.Codigo_MF_Texto)
        '        Catch ex2 As Exception

        '        End Try


        '    End Try



        'End If
        'Close()

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
        Try
            control = New Clscontrolpago
            basemy.conectar()
            fechaincial = DTPincio.Value.Year & "/" & DTPincio.Value.Month & "/" & DTPincio.Value.Day
            fechafinal = DtPfin.Value.Year & "/" & DtPfin.Value.Month & "/" & DtPfin.Value.Day
            '  basemy.ejecutarSIMPLE("drop table if exists temfacpuB ; CREATE TABLE temfacpub (  `id` INT NOT NULL AUTO_INCREMENT,  `CONCEPTO` VARCHAR(100) NULL,  `CANTIDAD` INT NOT NULL DEFAULT 1,  `PRECIOUNI` DECIMAL(18,2) NOT NULL DEFAULT 1,  `IMPORTE` VARCHAR(45) NOT NULL DEFAULT '1',  `IVA` TINYINT NOT NULL DEFAULT 1,  PRIMARY KEY (`id`))")
            basemy.ejecutarSIMPLE("truncate table temfacpub")
            'basemy.ejecutarSIMPLE("INSERT INTO temfacpub (CONCEPTO,CANTIDAD,IMPORTE, IVA, MONTO) SELECT otroscobros.descripcion as concepto, 1 , sum(PAGOTROS.IMPORTE) AS IMPORTE , sum(pagotros.monto) AS MONTO, PAGOTROS.IVA  FROM PAGOS,PAGOTROS,otroscobros WHERE PAGOS.SERIE=PAGOTROS.SERIE AND PAGOS.RECIBO=PAGOTROS.RECIBO AND  FECHA_ACT>='" & fechaincial & "' and fecha_act<='" & fechafinal & "' and facturado=0 and PAGOTROS.NUMCONCEPTO=OTROSCOBROS.CLAVE GROUP BY CONCEPTO,IVA")
            ' basemy.ejecutarSIMPLE("INSERT INTO temfacpub (CONCEPTO,CANTIDAD,IMPORTE,preciouni, IVA, MONTO) SELECT otroscobros.descripcion as concepto, 1 , sum(PAGOTROS.IMPORTE), sum(PAGOTROS.IMPORTE)  , PAGOTROS.IVA as IVA, sum(pagotros.monto) AS MONTO FROM PAGOS,PAGOTROS,otroscobros WHERE PAGOS.SERIE=PAGOTROS.SERIE AND PAGOS.CANCELADO='A' AND PAGOS.RECIBO=PAGOTROS.RECIBO AND  FECHA_ACT>='" & fechaincial & "' and fecha_act<='" & fechafinal & "' and facturado=0 and PAGOTROS.NUMCONCEPTO=OTROSCOBROS.CLAVE GROUP BY CONCEPTO,IVA")

            If txtcaja.Text <> "" Then
                Try
                    basemy.ejecutarSIMPLE("INSERT INTO temfacpub (CONCEPTO,CANTIDAD,PRECIOUNI,IMPORTE, IVA,MONTO,CONCEPTOSAT,UNIDADSAT) SELECT if (pagotros.IVA=0,conceptoscxc.descripcion, CONCAT(conceptoscxc.descripcion,' GRABADO'))  as concepto, 1 , sum(PAGOTROS.IMPORTE) AS IMPORTE ,round( sum(PAGOTROS.IMPORTE),2) , PAGOTROS.IVA, round( sum(PAGOTROS.IMPORTE),2),conceptoscxc.clavesat , conceptoscxc.unidadsat  FROM PAGOS,PAGOTROS,conceptoscxc WHERE PAGOS.SERIE=PAGOTROS.SERIE AND PAGOS.RECIBO=PAGOTROS.RECIBO AND PAGOS.CAJA = " & txtcaja.Text & " AND FECHA_ACT>='" & fechaincial & "' and fecha_act<='" & fechafinal & "' and facturado=0 and Pagos.Cancelado='A' and PAGOTROS.NUMCONCEPTO=conceptoscxc.id_concepto GROUP BY CONCEPTO,IVA")
                Catch ex As Exception

                End Try


            Else
                Try
                    basemy.ejecutarSIMPLE("INSERT INTO temfacpub (CONCEPTO,CANTIDAD,PRECIOUNI,IMPORTE, IVA,MONTO,CONCEPTOSAT,UNIDADSAT) SELECT if (pagotros.IVA=0,conceptoscxc.descripcion, CONCAT(conceptoscxc.descripcion,' GRABADO'))  as concepto, 1 , sum(PAGOTROS.IMPORTE) AS IMPORTE ,round( sum(PAGOTROS.IMPORTE),2) , PAGOTROS.IVA, round( sum(PAGOTROS.IMPORTE),2),conceptoscxc.clavesat , conceptoscxc.unidadsat  FROM PAGOS,PAGOTROS,conceptoscxc WHERE PAGOS.SERIE=PAGOTROS.SERIE AND PAGOS.RECIBO=PAGOTROS.RECIBO AND FECHA_ACT>='" & fechaincial & "' and fecha_act<='" & fechafinal & "' and facturado=0 and Pagos.Cancelado='A' and PAGOTROS.NUMCONCEPTO=conceptoscxc.id_concepto GROUP BY CONCEPTO,IVA")

                Catch ex As Exception

                End Try
            End If


            basemy.llenaGrid(DtgConceptos, "SELECT id, CONCEPTO, CANTIDAD, PRECIOUNI, IMPORTE,  IVA,CONCEPTOSAT,unidadsat FROM TEMFACPUB")


            conectar()
            Dim DATOS2 As OdbcDataReader = ConsultaSql("SELECT * FROM TEMFACPUB").ExecuteReader

            Dim contador As Int16 = 1
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

        txtobservaciones.Text = "FACTURA CORRESPONDIENTE AL PERIODO DEL DIA " & DTPincio.Value.ToShortDateString & " AL DIA " & DtPfin.Value.ToShortDateString

    End Sub




    Public Sub timbrar()
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


            Dim nombre, nombresespacios As String

            nombre = txtnombre.Text
            nombresespacios = nombre.Replace(" ", "")

            'Se crean los direcctorios para guardar las facturas      

            If Not My.Computer.FileSystem.DirectoryExists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\facturas\" & nombresespacios) Then

                My.Computer.FileSystem.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\facturas\" & nombresespacios)
            End If

            If Not My.Computer.FileSystem.DirectoryExists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\facturas\" & Year(Now) & acompletacero(Month(Now), 2)) Then

                My.Computer.FileSystem.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\facturas\" & Year(Now) & acompletacero(Month(Now), 2))
            End If



            Try
                If Not My.Computer.FileSystem.DirectoryExists(Application.StartupPath & "\facturas" & Year(Now) & acompletacero(Month(Now), 2) & "\") Then

                    My.Computer.FileSystem.CreateDirectory(Application.StartupPath & "\facturas" & Year(Now) & acompletacero(Month(Now), 2) & "\")
                End If

            Catch ex As Exception

            End Try

            'directorio = Application.StartupPath & "\facturas\" & nombresespacios
            'directorio2 = Application.StartupPath & "\facturas" & Year(Now) & acompletacero(Month(Now), 2) & "\"

            Dim sdkresp As SDKRespuesta
            Try

                ' Se indica la informacion del PAC
                ' pac = New PAC(My.Settings.usuarioMultifacturas, My.Settings.PassFacturaMultifacturas, "SI")


                ' Se crea el objeto SDK
                Dim mf As MFSDK = New MFSDK()


                ' Se crea la factura


                foliofactura = Val(obtenerCampo("select * from empresa", "foliofactura")) + 1
                seriefactura = obtenerCampo("select * from empresa", "seriefactura")
                Dim nombreEM As String = obtenerCampo("select * from empresa", "CNOMBRE")


                'Dim ivacumulado As Double = 0
                'For i = 1 To DtgConceptos.RowCount

                '    If DtgConceptos.Rows(i - 1).Cells(3).Value > 0 Then
                '        Dim concepto As New Clsconcepto

                '        concepto.Cantidad = 1
                '        concepto.Concepto = DtgConceptos.Rows(i - 1).Cells(1).Value
                '        concepto.Preciounitario = DtgConceptos.Rows(i - 1).Cells(3).Value
                '        concepto.importe = DtgConceptos.Rows(i - 1).Cells(4).Value
                '        concepto.clavesat = DtgConceptos.Rows(i - 1).Cells(6).Value
                '        concepto.unidadsat = DtgConceptos.Rows(i - 1).Cells(7).Value
                '        concepto.IVA = Math.Round(concepto.importe * DtgConceptos.Rows(i - 1).Cells(5).Value * (variable_iva / 100), 2)
                '        ivacumulado += concepto.IVA
                '        control.Listadeconceptos.Add(concepto, i.ToString())
                '    End If
                'Next



                'Error de importes





                mf = MakeFileToSendMultifacturas_v4(control, iva, subtotal, total, seriefactura, foliofactura, "01", "PUE", "S01", txtnombre.Text, txtrfc.Text, nombreEM, "616", txtcp.Text, True)

                sdkresp = mf.Timbrar("C:\sdk24\timbrar32.bat", "C:\sdk24\timbrados\", "factura", False)


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


                Dim directoriofDiaxml = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\facturasPeriodoActopan\" & Now.Year & "\" & nombresespacios & "\"



                If Not Directory.Exists(directoriofDiaxml) Then
                    Directory.CreateDirectory(directoriofDiaxml)
                End If



                Dim directorioxmlRespaldo = (Application.StartupPath & "\facturasPeriodo\" & nombresespacios).Trim

                If Not Directory.Exists(directorioxmlRespaldo) Then
                    Directory.CreateDirectory(directorioxmlRespaldo)
                End If





                Dim fs1 As FileStream = File.Create((Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\facturasPeriodoActopan\" & Now.Year & "\" & nombresespacios & "\FACTURA" & seriefactura & foliofactura & ".XML").Trim)

                ' Add text to the file.
                Dim info As Byte() = New UTF8Encoding(True).GetBytes(sdkresp.CFDI.ToString().TrimStart())
                fs1.Write(info, 0, info.Length)
                fs1.Close()


                ' 6079, ENERO - MARZO

                Dim cadenaxml11 = (directorioxmlRespaldo & "\xmlFactura" & seriefactura & foliofactura & ".xml").Trim
                Dim fsRespaldo As FileStream = File.Create(cadenaxml11)

                ' Add text to the file.
                Dim info1 As Byte() = New UTF8Encoding(True).GetBytes(sdkresp.CFDI.ToString().TrimStart().TrimEnd())
                fsRespaldo.Write(info1, 0, info1.Length)
                fsRespaldo.Close()





                Try '' grabando el datamatrix en la tabla cajas para que de ahi lo tome el crystal reports
                        '  Dim codigo As Image = Image.FromFile(sdkresp.RutaPNG)
                        ' convierto a bytes


                        Dim imagecod As Byte() = ImageToByte(sdkresp.RutaPNG)

                        ' lo pasa a la tabla cajas para grabar en la base la imagen de manera temporal 
                        ' y asi logre poner el codigo de barras en el reporte
                        ' me costo mucho trabajo y mucha experimentaccion
                        ' 01/11/2016


                        Dim dts As New DatosReciboTableAdapters.cajasTableAdapter
                        dts.UpdateQueryimagen(imagecod, My.Settings.caja)


                    Catch ex As Exception

                    End Try
                    Dim filtro As String

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

                Ejecucion("update empresa set foliofactura=" & foliofactura & " WHERE CODEMP='1'")

                fechaincial = DTPincio.Value.Year & "/" & DTPincio.Value.Month & "/" & DTPincio.Value.Day
                    fechafinal = DtPfin.Value.Year & "/" & DtPfin.Value.Month & "/" & DtPfin.Value.Day

                    If txtcaja.Text = "" Then
                        Ejecucion("update pagos SET facturado=" & foliofactura & ", seriefactura='" & seriefactura & "' where fecha_act>='" & fechaincial & "' and fecha_act<='" & fechafinal & "' and cancelado='A' and facturado=0")
                    Else
                        Ejecucion("update pagos SET facturado=" & foliofactura & ", seriefactura='" & seriefactura & "' where fecha_act>='" & fechaincial & "' and fecha_act<='" & fechafinal & "' and caja=" & txtcaja.Text & " and cancelado='A' and facturado=0")
                    End If







                    ' Se crea la factura






                    iva = Math.Round(total - subtotal, 2)


                    Dim con = control



            Catch err As Exception

            End Try



            Dim varXmlFile As XmlDocument = New XmlDocument()

            Dim varXmlNsMngr As XmlNamespaceManager = New XmlNamespaceManager(varXmlFile.NameTable)


            Try
                If Not My.Computer.FileSystem.DirectoryExists(Application.StartupPath & "\facturasperiodo" & Now.Year & "\") Then

                    My.Computer.FileSystem.CreateDirectory(Application.StartupPath & "\facturasperiodo" & Now.Year & "\")
                End If
            Catch ex As Exception

            End Try




            Dim cadenafolder As String = Application.StartupPath & "\facturasperiodo" & Now.Year & "\"


            varXmlFile.LoadXml(sdkresp.CFDI.Trim())

            varXmlNsMngr.AddNamespace("cfdi", "http://www.sat.gob.mx/cfd/3")
            varXmlNsMngr.AddNamespace("tfd", "http://www.sat.gob.mx/TimbreFiscalDigital")




            Dim varTotal As String = varXmlFile.ChildNodes.Item(1).Attributes.GetNamedItem("Total").Value '  varXmlFile.SelectSingleNode("/cfdi:Comprobante/@total", varXmlNsMngr).InnerText
            Dim VARNODECERTIFICADO As String = varXmlFile.ChildNodes.Item(1).Attributes.GetNamedItem("NoCertificado").Value 'varXmlFile.SelectSingleNode("/cfdi:Comprobante/@NoCertificado", varXmlNsMngr).InnerText
            Dim varformapago As String = varXmlFile.ChildNodes.Item(1).Attributes.GetNamedItem("FormaPago").Value 'varXmlFile.SelectSingleNode("/cfdi:Comprobante/@formapago", varXmlNsMngr).InnerText


            Dim LISTANODOComplemento As XmlNodeList = varXmlFile.GetElementsByTagName("tfd:TimbreFiscalDigital")

            For Each xAtt In LISTANODOComplemento
                Dim varUUID As String = VarXml(xAtt, "UUID")
                Dim varcertificado As String = VarXml(xAtt, "NoCertificadoSAT")
                Dim VARSELLOSAT As String = VarXml(xAtt, "SelloSAT")
                Dim VARSELLOCFD As String = VarXml(xAtt, "SelloCFD")
                'strEmisorNombre = VarXml(xAtt, "nombre")
            Next


            'Dim varUUID As String = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@UUID", varXmlNsMngr).InnerText
            'Dim varcertificado As String = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@NoCertificadoSAT", varXmlNsMngr).InnerText
            'Dim VARSELLOSAT As String = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@SelloSAT", varXmlNsMngr).InnerText
            'Dim VARSELLOCFD As String = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@SelloCFD", varXmlNsMngr).InnerText
            Dim LISTANODOSEMISOR As XmlNodeList = varXmlFile.GetElementsByTagName("cfdi:Emisor")
            Dim VARRFCEMISOR As String = String.Empty
            Dim varRFCRECEPTOR As String = String.Empty
            For Each xAtt In LISTANODOSEMISOR
                VARRFCEMISOR = VarXml(xAtt, "Rfc")
                ' strEmisorNombre = VarXml(xAtt, "nombre")
            Next
            Dim LISTANODORECEPTOR As XmlNodeList = varXmlFile.GetElementsByTagName("cfdi:Receptor")
            For Each xAtt In LISTANODORECEPTOR
                varRFCRECEPTOR = VarXml(xAtt, "Rfc")
                ' strEmisorNombre = VarXml(xAtt, "nombre")
            Next



            '''''''''''''''''''''''''''Factura''''''''''''''''''''''''''''

            'Dim foliofactura As Int32 = Val(obtenerCampo("select * from cajas where id_caja='" & My.Settings.caja & "'", "folio")) + 1

            'Dim seriefactura As String = obtenerCampo("select * from cajas where id_caja='" & My.Settings.caja & "'", "serie")

            Dim directoriofDia = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\facturasPeriodoActopan\" & Now.Year & "\" & nombresespacios & "\"

            If Not Directory.Exists(directoriofDia) Then
                Directory.CreateDirectory(directoriofDia)
            End If
            'Dar propiedades al Documento
            Dim pdfDoc As New Document(iTextSharp.text.PageSize.LETTER, 15.0F, 15.0F, 30.0F, 30.0F)


            'Dim pdfWrite As PdfWriter = PdfWriter.GetInstance(pdfDoc, New System.IO.FileStream(cadenafolder & "\factura" & serie & factura & ".pdf", FileMode.Create))
            Dim pdfWrite As PdfWriter = PdfWriter.GetInstance(pdfDoc, New System.IO.FileStream(directoriofDia & "\factura" & seriefactura & foliofactura & ".pdf", FileMode.Create))
            'Formtos para distintos tamaños de letras

            'Formato Letras




            Dim Font As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.NORMAL))
            Dim Font8N As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.BOLD))
            Dim Font88 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 15, iTextSharp.text.Font.BOLD))
            Dim Font12 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 12, iTextSharp.text.Font.BOLD))
            Dim Font9 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 9, iTextSharp.text.Font.NORMAL))
            Dim Fontp As New Font(FontFactory.GetFont(FontFactory.COURIER, 7, iTextSharp.text.Font.BOLD))
            Dim CVacio As PdfPCell = New PdfPCell(New Phrase(""))
            CVacio.Border = 0








            'abrimos el pdf para comenzar a escribir en el, al terminar cerramos
            pdfDoc.Open()




            ' comenzamos con un cuadro

            Dim _cb As PdfContentByte

            Dim colordefinido = New Clscolorreporte()
            colordefinido.ClsColoresReporte(My.Settings.colorfactura)

            _cb = pdfWrite.DirectContentUnder
            _cb.SetColorStroke(colordefinido.color) '/Color de la linea
            _cb.SetColorFill(colordefinido.color) '/ Color del relleno
            _cb.SetLineWidth(3.5) ''Tamano de la linea
            _cb.Rectangle(360, 680, 10, 100)
            _cb.FillStroke()

            '''


            Dim Table1 As PdfPTable = New PdfPTable(3)
            Table1.DefaultCell.Border = BorderStyle.None
            Dim Col1 As PdfPCell
            Dim ILine As Integer
            Dim iFila As Integer
            Table1.WidthPercentage = 100

            Dim widths As Single() = New Single() {100.0F, 300, 280.0F}
            Table1.SetWidths(widths)

            'Encabezado

            '   Dim imagenURL As String = "C:\Users\User\Desktop\huichapan\CAJAS Huichapan 3.3\LogoHuichapan.jpg"




            Dim imagenBMP As iTextSharp.text.Image
            imagenBMP = iTextSharp.text.Image.GetInstance(LOGOBYTE)
            imagenBMP.ScaleToFit(80.0F, 70.0F)
            'imagenBMP.SpacingBefore = 100.0F
            'imagenBMP.SpacingAfter = 1000.0F

            imagenBMP.Border = 0


            Table1.AddCell(imagenBMP)

            'Sustituir por los valores reales cuando se pase a frmvalidafactura,vb



            Dim Tabledireccion As PdfPTable = New PdfPTable(1)
            Col1 = New PdfPCell(New Phrase(Empresa, Font8N))
            Col1.Border = 0
            Col1.HorizontalAlignment = PdfPCell.ALIGN_CENTER

            Dim DIRECCIONE As String = Direccion & " " & coloniaEMPRESA & " " & poblacionEMPRESA & " " & Estadoempresa
            Dim Col1d = New PdfPCell(New Phrase(DIRECCIONE, Font8N))
            Col1d.Border = 0
            Col1d.HorizontalAlignment = PdfPCell.ALIGN_CENTER

            RFCORGANISMO = obtenerCampo("select * from empresa", "CNIF")
            Dim Col1rfe = New PdfPCell(New Phrase(RFCORGANISMO, Font8N))
            Col1rfe.Border = 0
            Col1rfe.HorizontalAlignment = PdfPCell.ALIGN_CENTER

            Tabledireccion.AddCell(Col1)
            Tabledireccion.AddCell(Col1d)
            Tabledireccion.AddCell(Col1rfe)
            Table1.AddCell(Tabledireccion)

            Dim Table2 As PdfPTable = New PdfPTable(2)
            Dim Col10 As PdfPCell
            Dim Col11 As PdfPCell
            Dim Col12 As PdfPCell
            Dim Col13 As PdfPCell
            Dim Col14 As PdfPCell
            Dim Col15 As PdfPCell
            Dim Col16 As PdfPCell

            Table2.WidthPercentage = 100
            Dim widthsT2 As Single() = New Single() {100, 180.0F}
            Table2.SetWidths(widthsT2)

            Col10 = New PdfPCell(New Phrase("Serie", Font88))
            Col10.Border = 0
            Col10.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table2.AddCell(Col10)


            Col11 = New PdfPCell(New Phrase(seriefactura, Font88))
            Col11.Border = 0
            Col11.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table2.AddCell(Col11)

            Dim Col10f = New PdfPCell(New Phrase("Factura", Font88))
            Col10f.Border = 0
            Col10f.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table2.AddCell(Col10f)


            Col12 = New PdfPCell(New Phrase(foliofactura, Font88))
            Col12.Border = 0
            Col12.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table2.AddCell(Col12)

            Col13 = New PdfPCell(New Phrase("Fecha de inicio:", Font))
            Col13.Border = 0
            Col13.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table2.AddCell(Col13)

            Col14 = New PdfPCell(New Phrase(fechaincial, Font))
            Col14.Border = 0
            Col14.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table2.AddCell(Col14)

            Col15 = New PdfPCell(New Phrase("Fecha final:", Font))
            Col15.Border = 0
            Col15.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table2.AddCell(Col15)

            Col16 = New PdfPCell(New Phrase(fechafinal, Font))
            Col16.Border = 0
            Col16.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table2.AddCell(Col16)


            ''''''''''''''''''

            Dim ColDC1 = New PdfPCell(New Phrase("UUID", Font))
            ColDC1.Border = 0
            ColDC1.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table2.AddCell(ColDC1)


            Dim ColDC2 = New PdfPCell(New Phrase(sdkresp.UUID, Font))
            ColDC2.Border = 0
            ColDC2.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table2.AddCell(ColDC2)

            Dim ColDC3 = New PdfPCell(New Phrase("Certificado Emisor", Font))
            ColDC3.Border = 0
            ColDC3.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table2.AddCell(ColDC3)

            Dim ColDC4 = New PdfPCell(New Phrase(sdkresp.NoCertificado, Font))
            ColDC4.Border = 0
            ColDC4.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table2.AddCell(ColDC4)


            Dim ColDC7 = New PdfPCell(New Phrase("Certificado Sat ", Font))
            ColDC7.Border = 0
            ColDC7.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table2.AddCell(ColDC7)

            Dim ColDC8 = New PdfPCell(New Phrase(sdkresp.CertificadoSAT, Font))
            ColDC8.Border = 0
            ColDC8.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table2.AddCell(ColDC8)



            Dim ColDC11 = New PdfPCell(New Phrase(" ", Font))
            ColDC11.Border = 0
            ColDC11.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table2.AddCell(ColDC11)

            Dim ColDC12 = New PdfPCell(New Phrase(" ", Font))
            ColDC12.Border = 0
            ColDC12.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table2.AddCell(ColDC12)

            Table1.AddCell(Table2)


            Table1.CompleteRow()

            ''''''''''''''''''''''''

            'Table Datos Cliente
            Dim Table4 As PdfPTable = New PdfPTable(2)
            Dim ColDN As PdfPCell
            Dim ColDN1 As PdfPCell
            Dim ColDN2 As PdfPCell
            Dim ColDN3 As PdfPCell
            Dim ColDN4 As PdfPCell
            Dim ColDN5 As PdfPCell
            Dim ColDN6 As PdfPCell
            Dim ColDN7 As PdfPCell
            Dim ColDN8 As PdfPCell
            Dim ColDN9 As PdfPCell
            Table4.WidthPercentage = 100
            Dim widthsT4 As Single() = New Single() {600.0F, 200.0F}

            Table4.SetWidths(widthsT4)

            ColDN = New PdfPCell(New Phrase(recibo.nombre, Font9))
            ColDN.Border = 0
            ColDN.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table4.AddCell(ColDN)

            ColDN1 = New PdfPCell(New Phrase("RFC: " & txtrfc.Text, Font9))
            ColDN1.Border = 0
            ColDN1.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table4.AddCell(ColDN1)

            ColDN2 = New PdfPCell(New Phrase(txtcalle.Text & " " & txtnumext.Text & " " & txtnuminterior.Text, Font9))
            ColDN2.Border = 0
            ColDN2.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table4.AddCell(ColDN2)

            ColDN3 = New PdfPCell(New Phrase(" ", Font))
            ColDN3.Border = 0
            ColDN3.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table4.AddCell(ColDN3)

            ColDN4 = New PdfPCell(New Phrase(txtcolonia.Text, Font9))
            ColDN4.Border = 0
            ColDN4.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table4.AddCell(ColDN4)

            ColDN5 = New PdfPCell(New Phrase("Tipo: Factura de Periodo", Font))
            ColDN5.Border = 0
            ColDN5.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table4.AddCell(ColDN5)

            ColDN6 = New PdfPCell(New Phrase(txtpoblacion.Text & " " & txtdelegacion.Text & " " & txtestado.Text & " CP " & txtcp.Text, Font9))
            ColDN6.Border = 0
            ColDN6.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table4.AddCell(ColDN6)

            ColDN7 = New PdfPCell(New Phrase(" ", Font))
            ColDN7.Border = 0
            ColDN7.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table4.AddCell(ColDN7)

            ColDN8 = New PdfPCell(New Phrase(" ", Font9))
            ColDN8.Border = 0
            ColDN8.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table4.AddCell(ColDN8)

            ColDN9 = New PdfPCell(New Phrase(" ", Font))
            ColDN9.Border = 0
            ColDN9.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table4.AddCell(ColDN9)




            Dim Table5 As PdfPTable = New PdfPTable(1)
            Dim Col51 As PdfPCell
            Dim Col52 As PdfPCell

            Table5.WidthPercentage = 100
            Dim widthsT5 As Single() = New Single() {1000.0F}

            Table5.SetWidths(widthsT5)

            Col51 = New PdfPCell(New Phrase(" ", Font))
            Col51.Border = 0
            Col51.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table5.AddCell(Col51)

            Col52 = New PdfPCell(New Phrase(txtobservaciones.Text, Font))
            Col52.Border = 0
            Col52.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table5.AddCell(Col52)






            'Encabezado consulta tabla

            Dim Table6 As PdfPTable = New PdfPTable(4)
            Dim Col61 As PdfPCell
            Dim Col62 As PdfPCell
            Dim Col63 As PdfPCell
            Dim Col64 As PdfPCell

            Table6.WidthPercentage = 100
            Dim widthsT6 As Single() = New Single() {50.0F, 290.0F, 100.0F, 100.0F}
            Table6.SetWidths(widthsT6)

            Col61 = New PdfPCell(New Phrase("Cantidad", Font9))
            Col61.Border = 7
            Col61.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Col61.BackgroundColor = colordefinido.color
            Table6.AddCell(Col61)

            Col62 = New PdfPCell(New Phrase("Concepto", Font9))
            Col62.Border = 3
            Col62.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            Col62.BackgroundColor = colordefinido.color
            Table6.AddCell(Col62)

            Col63 = New PdfPCell(New Phrase("Monto", Font9))
            Col63.Border = 3
            Col63.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Col63.BackgroundColor = colordefinido.color
            Table6.AddCell(Col63)

            'Dim ColMi = New PdfPCell(New Phrase("IVA", Font9))
            'ColMi.Border = 3
            'ColMi.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            'ColMi.BackgroundColor = colordefinido.color
            'Table6.AddCell(ColMi)

            'Dim ColMi = New PdfPCell(New Phrase(" ", Font9))
            'ColMi.Border = 3
            'ColMi.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            'ColMi.BackgroundColor = colordefinido.color
            'Table6.AddCell(ColMi)


            Col64 = New PdfPCell(New Phrase("Importe", Font9))
            Col64.Border = 11
            Col64.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Col64.BackgroundColor = colordefinido.color
            Table6.AddCell(Col64)



            For Each concepto As Clsconcepto In control.Listadeconceptos
                Col61 = New PdfPCell(New Phrase(concepto.Cantidad, Font9))
                Col61.Border = 0
                Col61.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                Table6.AddCell(Col61)

                Col62 = New PdfPCell(New Phrase(concepto.Concepto, Font9))
                Col62.Border = 0
                Col62.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                Table6.AddCell(Col62)
                Dim montox As String = Decimal.Parse(concepto.Preciounitario).ToString("C")
                Col63 = New PdfPCell(New Phrase(montox, Font9))
                Col63.Border = 0
                Col63.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                Table6.AddCell(Col63)
                'Dim ivax As String = Decimal.Parse(concepto.IVA).ToString("C")
                'Dim ColMiv = New PdfPCell(New Phrase(ivax, Font9))
                'ColMiv.Border = 0
                'ColMiv.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                'Table6.AddCell(ColMiv)

                'Dim ivax As String = Decimal.Parse(" ")
                'Dim ColMiv = New PdfPCell(New Phrase(ivax, Font9))
                'ColMiv.Border = 0
                'ColMiv.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                'Table6.AddCell(ColMiv)

                Dim importex As String = Decimal.Parse(concepto.importe).ToString("C")
                Col64 = New PdfPCell(New Phrase(importex, Font9))
                Col64.Border = 0
                Col64.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                Table6.AddCell(Col64)



            Next

            Dim TableEspacio As PdfPTable = New PdfPTable(1)
            Dim ColEsp As PdfPCell
            TableEspacio.WidthPercentage = 100
            Dim widthsTE As Single() = New Single() {1000.0F}
            TableEspacio.SetWidths(widthsTE)

            ColEsp = New PdfPCell(New Phrase(" ", Font))
            ColEsp.Border = 0
            ColEsp.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            TableEspacio.AddCell(ColEsp)

            Dim Table7 As PdfPTable = New PdfPTable(4)
            Dim Col71 As PdfPCell
            Dim Col72 As PdfPCell
            Dim Col73 As PdfPCell
            Dim Col74 As PdfPCell
            Dim Col75 As PdfPCell
            Dim Col76 As PdfPCell
            Dim Col77 As PdfPCell
            Dim Col78 As PdfPCell

            Table7.WidthPercentage = 100
            Dim widthsT7 As Single() = New Single() {200.0F, 500.0F, 150.0F, 150.0F}
            Table7.SetWidths(widthsT7)

            Col71 = New PdfPCell(New Phrase(" ", Font))
            Col71.Border = 0
            Col71.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table7.AddCell(Col71)

            Col72 = New PdfPCell(New Phrase("", Font))
            Col72.Border = 0
            Col72.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            Table7.AddCell(Col72)

            Col73 = New PdfPCell(New Phrase("Subtotal: ", Font))
            Col73.Border = 0
            Col73.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table7.AddCell(Col73)


            subtotal = Decimal.Parse(subtotal).ToString("C")
            Col74 = New PdfPCell(New Phrase(subtotal.ToString("C"), Font))
            Col74.Border = 1
            Col74.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table7.AddCell(Col74)
            Table7.CompleteRow()

            Col71 = New PdfPCell(New Phrase(" ", Font))
            Col71.Border = 0
            Col71.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table7.AddCell(Col71)

            Col72 = New PdfPCell(New Phrase("", Font))
            Col72.Border = 0
            Col72.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table7.AddCell(Col72)

            Col73 = New PdfPCell(New Phrase("IVA: ", Font))
            Col73.Border = 0
            Col73.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table7.AddCell(Col73)


            iva = Decimal.Parse(iva).ToString("C")
            Col74 = New PdfPCell(New Phrase(iva.ToString("C"), Font))
            Col74.Border = 0
            Col74.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table7.AddCell(Col74)

            Table7.CompleteRow()

            Col71 = New PdfPCell(New Phrase(" ", Font))
            Col71.Border = 0
            Col71.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table7.AddCell(Col71)

            Col72 = New PdfPCell(New Phrase("", Font))
            Col72.Border = 0
            Col72.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table7.AddCell(Col72)

            Col73 = New PdfPCell(New Phrase("TOTAL: ", Font))
            Col73.Border = 0
            Col73.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table7.AddCell(Col73)


            'Coversión
            Dim totalfacturax As String = Decimal.Parse(total).ToString("C")
            Col74 = New PdfPCell(New Phrase(totalfacturax, Font))
            Col74.Border = 1
            Col74.HorizontalAlignment = PdfPCell.ALIGN_RIGHT



            Table7.AddCell(Col74)

            Col75 = New PdfPCell(New Phrase(" ", Font))
            Col75.Border = 0
            Col75.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            'Table7.
            Table7.AddCell(Col75)

            Col76 = New PdfPCell(New Phrase(" ", Font))
            Col76.Border = 0
            Col76.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            'Table7.
            Table7.AddCell(Col76)

            Col77 = New PdfPCell(New Phrase(" ", Font))
            Col77.Border = 0
            Col77.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            'Table7.
            Table7.AddCell(Col77)

            Col78 = New PdfPCell(New Phrase(" ", Font))
            Col78.Border = 0
            Col78.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            'Table7.
            Table7.AddCell(Col78)


            Dim Table8 As PdfPTable = New PdfPTable(1)
            Dim Col81 As PdfPCell
            Dim Col82 As PdfPCell
            Dim Col83 As PdfPCell


            Table8.WidthPercentage = 100
            Dim widthsT8 As Single() = New Single() {1000.0F}
            Table8.SetWidths(widthsT8)

            Col81 = New PdfPCell(New Phrase(ConvertCurrencyToSpanish(total, "Pesos"), Font9))
            Col81.Border = 0
            Col81.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table8.AddCell(Col81)


            Col83 = New PdfPCell(New Phrase(" ", Font))
            Col83.Border = 0
            Col83.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            Table8.AddCell(Col83)

            Table8.CompleteRow()

            Dim Table9 As PdfPTable = New PdfPTable(2)
            Dim Col91 As PdfPCell
            Dim Col92 As PdfPCell


            Table9.WidthPercentage = 100
            Dim widthsT9 As Single() = New Single() {800.0F, 200.0F}
            Table9.SetWidths(widthsT9)


            Dim TableSellos As PdfPTable = New PdfPTable(2)


            TableSellos.WidthPercentage = 100
            Dim widthsTIT2 As Single() = New Single() {200.0F, 500.0F}
            TableSellos.SetWidths(widthsTIT2)


            Dim ColSell1 As PdfPCell
            Dim ColSell2 As PdfPCell
            Dim ColSell3 As PdfPCell
            Dim ColSell4 As PdfPCell
            Dim ColSell5 As PdfPCell
            Dim ColSell6 As PdfPCell




            ColSell1 = New PdfPCell(New Phrase("Sello CFDI   ", Font9))
            ColSell1.Border = 0
            ColSell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            TableSellos.AddCell(ColSell1)

            ColSell2 = New PdfPCell(New Phrase(sdkresp.Sello, Fontp))
            ColSell2.Border = 0
            ColSell2.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            TableSellos.AddCell(ColSell2)

            ColSell3 = New PdfPCell(New Phrase("Sello SAT   ", Font9))
            ColSell3.Border = 0
            ColSell3.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            TableSellos.AddCell(ColSell3)

            ColSell4 = New PdfPCell(New Phrase(sdkresp.SelloSAT, Fontp))
            ColSell4.Border = 0
            ColSell4.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            TableSellos.AddCell(ColSell4)

            ColSell5 = New PdfPCell(New Phrase("Cadena Original   ", Font9))
            ColSell5.Border = 0
            ColSell5.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            TableSellos.AddCell(ColSell5)

            ColSell6 = New PdfPCell(New Phrase(sdkresp.Cadena, Fontp))
            ColSell6.Border = 0
            ColSell6.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            TableSellos.AddCell(ColSell6)

            TableSellos.DefaultCell.Border = BorderStyle.None
            Table9.DefaultCell.Border = BorderStyle.None

            Col91 = New PdfPCell(TableSellos)
            Col91.Border = 0
            Col91.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table9.AddCell(Col91)




            ''' aqui el qr
            ''' 

            'Agregamos el codigo QR al documento
            Dim codigoQR = New StringBuilder()
            codigoQR.Append("https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx?id=" & sdkresp.UUID)
            codigoQR.Append("&re=" & VARRFCEMISOR) 'RFC del Emisor
            codigoQR.Append("&rr=" & txtrfc.Text) 'RFC del receptor
            codigoQR.Append("&tt=" & total) ' Total del comprobante 10 enteros y 6 decimales
            codigoQR.Append("&fe=" & sdkresp.SelloSAT.Substring(sdkresp.SelloSAT.Length - 8, 8)) 'UUID del comprobante
            Dim pdfCodigoQR = New BarcodeQRCode(codigoQR.ToString(), 1, 1, New Dictionary(Of iTextSharp.text.pdf.qrcode.EncodeHintType, System.Object))
            Dim img As Image = pdfCodigoQR.GetImage()
            img.SpacingAfter = 0.0F
            img.SpacingBefore = 0.0F
            img.BorderWidth = 1.0F
            img.HasAbsolutePosition()

            Table9.AddCell(img)

            Table9.CompleteRow()

            pdfDoc.Add(Table1)


            pdfDoc.Add(Table4)
            pdfDoc.Add(Table5)
            pdfDoc.Add(TableEspacio)
            pdfDoc.Add(TableEspacio)
            pdfDoc.Add(Table6)
            pdfDoc.Add(Table7)
            pdfDoc.Add(Table8)
            pdfDoc.Add(TableEspacio)
            pdfDoc.Add(TableEspacio)
            pdfDoc.Add(TableEspacio)
            pdfDoc.Add(Table9)
            pdfDoc.Add(TableEspacio)
            pdfDoc.Add(TableEspacio)
            pdfDoc.Add(TableEspacio)
            pdfDoc.Add(TableEspacio)


            pdfDoc.Close()


            Ejecucion("update empresa set foliofactura=" & foliofactura & " WHERE CODEMP='1'")

            Try
                Dim psi As New ProcessStartInfo(directoriofDia & "\factura" & seriefactura & foliofactura & ".pdf")
                'psi.WorkingDirectory = cadenapdf & "\facturas\" + nombresespacios

                psi.WindowStyle = ProcessWindowStyle.Hidden
                Dim p As Process = Process.Start(psi)
            Catch ex As Exception
                MessageBox.Show("Error al visualizar el pdf" & ex.Message)
            End Try



        End If




        ' si tiene red





    End Sub


End Class

Imports System.Data.Odbc
Imports System.IO
Imports System.Net.Mail
Imports System.Net
Imports System.Text



Imports ThoughtWorks.QRCode
Imports ThoughtWorks.QRCode.Codec
Imports ThoughtWorks.QRCode.Codec.Data

Imports System.Xml
Imports System.Xml.Xsl
Imports MultiFacturasSDK

Module FuncFacturas
    Public usuariodelsistema As String
    Public niveldedescuento As Decimal
    Public usuariotem As String
    Public niveldedescuentotem As Decimal
    Public variable_iva As Decimal = 16
    Public serieregresada As String = ""




    Public Function WebServiceToInvoice(ByVal Documento As String, ByVal cliente As String, ByVal passcliente As String, ByVal nombre As String, Optional ByVal maildeusuario As String = "enviofacturas@caposa.gob.mx") As Long
        ' Create a request using a URL that can receive a post.
        Dim request As WebRequest

        Dim Data = New StringBuilder()

        Dim byt As Byte() = System.Text.Encoding.UTF8.GetBytes(Documento)
        Dim Documento2 As String = Convert.ToBase64String(byt)

        If My.MySettings.Default.TimbrarPrueba = "SI" Then
            request = WebRequest.Create(CAJAS.My.Settings.direcciondepruebas)
            Data.Append("usuario=" & My.Settings.usuariopruebasfactupronto)
            Data.Append("&pass=" & My.Settings.passwordpruebasfactupronto)
            Data.Append("&txt=" & Documento2)

        Else
            'Dim direccion As String = obtenerCampo("select * from empresa limit 1 ", "direccionwebservice")
            'MessageBox.Show(direccion)
            request = WebRequest.Create("http://www.factupronto.mx/prometheus/txt_timbrado_factupronto.php")
            Data.Append("usuario=" & cliente)
            Data.Append("&pass=" & passcliente)
            Data.Append("&txt=" & Documento2)
        End If
        ' Set the Method property of the request to POST.
        request.Method = "POST"
        'request.Credentials = New System.Net.NetworkCredential("eduardovime@outlook.com", "Kelcay74@")
        'request.UseDefaultCredentials = False




        Dim encodedString As Byte() = Encoding.UTF8.GetBytes(Data.ToString())

        'Dim PosData As String = "usuario=eduardovime@outlook.com&Pass=Kelcay74@&txt=''" ' & Documento
        Dim byteArray As Byte() = UTF8Encoding.UTF8.GetBytes(Data.ToString())
        request.ContentType = "application/x-www-form-urlencoded"
        request.ContentLength = byteArray.Length
        Dim dataStream As Stream = request.GetRequestStream()
        dataStream.Write(byteArray, 0, byteArray.Length)
        dataStream.Close()

        ' Get the response.
        Dim response As WebResponse = request.GetResponse()
        'Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)
        dataStream = response.GetResponseStream()
        Dim reader As New StreamReader(dataStream)
        Dim responseFromServer As String = reader.ReadToEnd()


        Dim ContenidoWeb() As String = responseFromServer.ToString.Split("|")
        Dim xmlfile As String, pdffile As String = String.Empty, xmlFactura As String, folioregresado As String = ""
        Dim directorio As String = ""
        Dim directorio2 As String = ""

        '   If ContenidoWeb(0) = 1 Then



        If ContenidoWeb(1).Contains("501") Then
            MessageBox.Show("El usuario y contraseña posiblemente han cambiado")
            Return 0
        End If
        If ContenidoWeb(1).Contains("503") Then
            MessageBox.Show("No hay timbres disponibles")
            Return 0
        End If
        If ContenidoWeb(1).Contains("507") Then

            MessageBox.Show("La serie no es correcta")
            Return 0
        End If
        If ContenidoWeb(1).Contains("200") Then
        End If
        If Not ContenidoWeb(1).Contains("200") Then
            MessageBox.Show("Error en el servidor  y/o servicio factuprontro")
            Return 0
        End If

        xmlfile = ContenidoWeb(2)

        Dim b As Byte() = Convert.FromBase64String(xmlfile)
        xmlFactura = System.Text.Encoding.UTF8.GetString(b)

        'Dim xmltexto As String
        'xmltexto = ' Convert.FromBase64CharArray(xmlfile, 0, xmlfile.Length)
        ''Dim xmltexto As String = ConvertUTF8IntPtrtoString(xmlfile)

        pdffile = ContenidoWeb(3)
        Dim FacturaFaltam As Integer = ContenidoWeb(4) - 1

        serieregresada = ContenidoWeb(5)
        folioregresado = ContenidoWeb(6)

        If Not My.Computer.FileSystem.DirectoryExists(Application.StartupPath & "\facturas\" & nombre) Then

            My.Computer.FileSystem.CreateDirectory(Application.StartupPath & "\facturas\" & nombre)
        End If

        If Not My.Computer.FileSystem.DirectoryExists(Application.StartupPath & "\facturas" & Year(Now) & acompletacero(Month(Now), 2) & "\") Then

            My.Computer.FileSystem.CreateDirectory(Application.StartupPath & "\facturas" & Year(Now) & acompletacero(Month(Now), 2) & "\")
        End If

        directorio = Application.StartupPath & "\facturas\" & nombre
        directorio2 = Application.StartupPath & "\facturas" & Year(Now) & acompletacero(Month(Now), 2) & "\"

        Try
            '  DownloadFile(, directorio & "\XmlFactura" & serieregresada & folioregresado & ".xml")
            Dim arch As New clsDocumentoTXT
            arch.guardartxtfactura(xmlFactura, directorio & "\XmlFactura" & serieregresada & folioregresado & ".xml")
            arch.guardartxtfactura(xmlFactura, directorio2 & "\XmlFactura" & serieregresada & folioregresado & ".xml")

            DownloadFile(pdffile, directorio & "\PdfFactura" & serieregresada & folioregresado & ".pdf")
            DownloadFile(pdffile, directorio2 & "\PdfFactura" & serieregresada & folioregresado & ".pdf")

        Catch ex As Exception
            MessageBox.Show("Error" & ex.Message)
        End Try

        Dim timbres As String
        timbres = ContenidoWeb(4)
        If timbres < 1500 Then
            MessageBox.Show("Te quedan " & timbres & " Timbres")
        End If

        Dim folio As Long = 0
        Long.TryParse(folioregresado, folio)
        'reader.Close()
        'dataStream.Close()
        'response.Close()
        ' ***** cambia a visualizar el proceso

        'Dim x As New FrmVisor()
        'x.MaximizeBox = True
        Dim cadenapdf = directorio & "\PdfFactura" & serieregresada & folioregresado & ".pdf"


        'x.pdfvisor.Tag = cadenapdf

        ' FrmVisor.pdfvisor
        '   FrmVisor.MdiParent = Me
        'x.pdfvisor.printAll()
        'x.pdfvisor.printAll()

        imprimirpdf(cadenapdf)
        imprimirpdf(cadenapdf)


        Dim mail As New MailMessage
        Dim autenticar As New NetworkCredential
        Dim send As New SmtpClient


        Try
            mail.To.Clear()
            mail.Body = My.MySettings.Default.MENSAJECORREO

            ' mail.Body = My.MySettings.Default.
            mail.Subject = My.MySettings.Default.Asuntocorreo
            mail.IsBodyHtml = False
            mail.To.Add(Trim(maildeusuario))
            mail.Attachments.Add(New Attachment(directorio & "\XmlFactura" & serieregresada & folioregresado & ".xml"))
            mail.Attachments.Add(New Attachment(directorio & "\PdfFactura" & serieregresada & folioregresado & ".pdf"))
            mail.From = New MailAddress(My.MySettings.Default.CorreoFacturas, "CAPOSA HUICHAPAN")
            send.Credentials = New NetworkCredential(My.MySettings.Default.Usuariocorreo, My.MySettings.Default.Passwordcorreo)
            'If cboMail.SelectedIndex = 1 Then
            send.Host = "smtp.gmail.com"
            send.Port = 587
            send.EnableSsl = True
            send.DeliveryMethod = SmtpDeliveryMethod.Network
            '        End If
            'If cboMail.SelectedIndex = 2 Then
            '    send.Host = "smtp.live.com"
            '    send.Port = 587
            '    send.EnableSsl = False
            'End If
            send.Send(mail)
            ' Me.Cursor = Cursors.Default
            '  Me.Text = "Enviar Mensaje"
            MsgBox("Un correo fue enviado correctamente", MsgBoxStyle.Information, "Mensaje")

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Mensajeria", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return folio
        'Else
        ' MessageBox.Show("Error Del Contenido " & ContenidoWeb(1).ToString)

        ' Return 0
        ' End If

        Console.WriteLine(responseFromServer)

        Return 0
    End Function



    Public Function DownloadFile(ByVal remoteFilename As [String], ByVal localFilename As [String]) As Integer
        ' Function will return the number of bytes processed
        ' to the caller. Initialize to 0 here.


        Dim bytesProcessed As Integer = 0

        ' Assign values to these objects here so that they can
        ' be referenced in the finally block
        Dim remoteStream As Stream = Nothing
        Dim localStream As Stream = Nothing
        Dim response As WebResponse = Nothing

        ' Use a try/catch/finally block as both the WebRequest and Stream
        ' classes throw exceptions upon error
        Try
            ' Create a request for the specified remote file name
            Dim request As WebRequest = WebRequest.Create(remoteFilename)
            If request IsNot Nothing Then
                ' Send the request to the server and retrieve the
                ' WebResponse object
                response = request.GetResponse()
                If response IsNot Nothing Then
                    ' Once the WebResponse object has been retrieved,
                    ' get the stream object associated with the response's data
                    remoteStream = response.GetResponseStream()

                    ' Create the local file
                    localStream = File.Create(localFilename)

                    ' Allocate a 1k buffer
                    Dim buffer As Byte() = New Byte(1023) {}
                    Dim bytesRead As Integer

                    ' Simple do/while loop to read from stream until
                    ' no bytes are returned
                    Do
                        ' Read data (up to 1k) from the stream
                        bytesRead = remoteStream.Read(buffer, 0, buffer.Length)

                        ' Write the data to the local file
                        localStream.Write(buffer, 0, bytesRead)

                        ' Increment total bytes processed
                        bytesProcessed += bytesRead
                    Loop While bytesRead > 0
                End If
            End If
        Catch e As Exception
            Console.WriteLine(e.Message)
        Finally
            ' Close the response and streams objects here
            ' to make sure they're closed even if an exception
            ' is thrown at some point
            If response IsNot Nothing Then
                response.Close()
            End If
            If remoteStream IsNot Nothing Then
                remoteStream.Close()
            End If
            If localStream IsNot Nothing Then
                localStream.Close()
            End If
        End Try

        ' Return total bytes processed to caller.
        Return bytesProcessed

    End Function


    Public Function ToBase64(ByVal data() As Byte) As String
        If data Is Nothing Then Throw New ArgumentNullException("data")
        Return Convert.ToBase64String(data)
    End Function

    Public Function ConvertFromBase64(ByVal Input As String)
        Dim B As Byte() = System.Convert.FromBase64String(Input)
        Return System.Text.Encoding.UTF8.GetString(B)
    End Function


    Public Sub imprimirpdf(ByVal _pdf As String)
        Dim psi As New ProcessStartInfo

        psi.UseShellExecute = True

        psi.Verb = "print"

        psi.WindowStyle = ProcessWindowStyle.Hidden

        'psi.Arguments = PrintDialog1.PrinterSettings.PrinterName.ToString()

        psi.FileName = _pdf

        Process.Start(psi)
    End Sub

    Public Function qr(ByVal PDF As String) As Image
        Dim varXmlFile As XmlDocument = New XmlDocument()

        Dim varXmlNsMngr As XmlNamespaceManager = New XmlNamespaceManager(varXmlFile.NameTable)


        varXmlFile.Load(PDF)

        varXmlNsMngr.AddNamespace("cfdi", "http://www.sat.gob.mx/cfd/3")
        varXmlNsMngr.AddNamespace("tfd", "http://www.sat.gob.mx/TimbreFiscalDigital")


        Dim varUUID As String = ""
        Dim varTotal As Decimal
        Dim VARRFCEMISOR As String = ""
        Dim varRFCRECEPTOR As String = ""
        Dim varcertificado As String

        'varTotal = varXmlFile.SelectSingleNode("/cfdi:Comprobante/@total", varXmlNsMngr).InnerText
        varUUID = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@UUID", varXmlNsMngr).InnerText
        'varcertificado = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@noCertificadoSAT", varXmlNsMngr).InnerText
        Dim LISTANODOSEMISOR As XmlNodeList = varXmlFile.GetElementsByTagName("cfdi:Emisor")
        For Each xAtt In LISTANODOSEMISOR
            VARRFCEMISOR = VarXml(xAtt, "rfc")
            ' strEmisorNombre = VarXml(xAtt, "nombre")
        Next
        Dim LISTANODORECEPTOR As XmlNodeList = varXmlFile.GetElementsByTagName("cfdi:Receptor")
        For Each xAtt In LISTANODORECEPTOR
            varRFCRECEPTOR = VarXml(xAtt, "rfc")
            ' strEmisorNombre = VarXml(xAtt, "nombre")
        Next



        ' CONSTRUIMOS LA CADENA PARA EL QR 10/08/2017


        Dim texto As String = "https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx?&id="
        texto += varUUID
        texto += "&re="
        texto += VARRFCEMISOR
        texto += "&rr="
        texto += varRFCRECEPTOR
        'texto += "&tt="
        'texto += varTotal.ToString
        'texto += "&fe="
        'texto += varcertificado.Substring(12)




        Dim colorFondoQR As Integer =
       Color.FromArgb(255, 255, 255, 255).ToArgb
        Dim colorQR As Integer =
            Color.FromArgb(255, 0, 0, 0).ToArgb


        Dim escala As Integer = 1
        Dim niveldecorreccion As String = "Bajo (7%)"

        Dim generarCodigoQR As QRCodeEncoder = New QRCodeEncoder
        generarCodigoQR.QRCodeEncodeMode =
            Codec.QRCodeEncoder.ENCODE_MODE.BYTE
        generarCodigoQR.QRCodeScale = escala

        Select Case niveldecorreccion
            Case "Bajo (7%)"
                generarCodigoQR.QRCodeErrorCorrect =
                    Codec.QRCodeEncoder.ERROR_CORRECTION.L
            Case "Medio (15%)"
                generarCodigoQR.QRCodeErrorCorrect =
                    Codec.QRCodeEncoder.ERROR_CORRECTION.M
            Case "Alto (25%)"
                generarCodigoQR.QRCodeErrorCorrect =
                    Codec.QRCodeEncoder.ERROR_CORRECTION.Q
            Case "Muy alto (30%)"
                generarCodigoQR.QRCodeErrorCorrect =
                    Codec.QRCodeEncoder.ERROR_CORRECTION.H
        End Select

        'La versión "0" calcula automáticamente el tamaño
        generarCodigoQR.QRCodeVersion = 0

        '' --------- Forzar una determinada version -----------
        ''En caso de querer forzar una determinada version 
        '(tamaño) el siguiente código devuelve la
        ''versión mínima para el texto que se quiere códificar:
        'Dim iVersion As Integer = 
        '    AdjustQRVersion(TextBox1.Text, QRCodeEncoder.QRCodeErrorCorrect)
        'If iVersion = -1 Then
        '    MessageBox.Show("El texto es demasiado grande o el " +
        '        "Correction Level (ERROR_CORRECTION) no es el apropiado")
        '    Exit Sub
        'Else
        '    qrCodeEncoder.QRCodeVersion = iVersion
        'End If
        '' -----------------------------------------------------

        generarCodigoQR.QRCodeBackgroundColor =
            System.Drawing.Color.FromArgb(colorFondoQR)
        generarCodigoQR.QRCodeForegroundColor =
            System.Drawing.Color.FromArgb(colorQR)
        Return generarCodigoQR.Encode(texto)

    End Function

    Public Function qrdatos(ByVal varUUID As String, VARTOTAL As Decimal, VARRFCEMISOR As String, varRFCRECEPTOR As String, varcertificado As String) As Image



        ' CONSTRUIMOS LA CADENA PARA EL QR 10/08/2017


        Dim texto As String = "https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx?&id="
        texto += varUUID
        texto += "&re="
        texto += VARRFCEMISOR
        texto += "&rr="
        texto += varRFCRECEPTOR
        'texto += "&tt="
        'texto += varTotal.ToString
        'texto += "&fe="
        'texto += varcertificado.Substring(12)

        'Dim texto As String = ""

        'texto += VARRFCEMISOR & " "
        'texto += Chr(13)
        'texto += varRFCRECEPTOR
        'texto += Chr(13)
        'texto += varTotal.ToString()
        'texto += Chr(13)
        ''  texto += Chr(13)
        'texto += varUUID
        'texto += Chr(13)
        ''  texto += varcertificado.Substring(12)


        Dim colorFondoQR As Integer =
       Color.FromArgb(255, 255, 255, 255).ToArgb
        Dim colorQR As Integer =
            Color.FromArgb(255, 0, 0, 0).ToArgb


        Dim escala As Integer = 1
        Dim niveldecorreccion As String = "Bajo (7%)"

        Dim generarCodigoQR As QRCodeEncoder = New QRCodeEncoder
        generarCodigoQR.QRCodeEncodeMode =
            Codec.QRCodeEncoder.ENCODE_MODE.BYTE
        generarCodigoQR.QRCodeScale = escala

        Select Case niveldecorreccion
            Case "Bajo (7%)"
                generarCodigoQR.QRCodeErrorCorrect =
                    Codec.QRCodeEncoder.ERROR_CORRECTION.L
            Case "Medio (15%)"
                generarCodigoQR.QRCodeErrorCorrect =
                    Codec.QRCodeEncoder.ERROR_CORRECTION.M
            Case "Alto (25%)"
                generarCodigoQR.QRCodeErrorCorrect =
                    Codec.QRCodeEncoder.ERROR_CORRECTION.Q
            Case "Muy alto (30%)"
                generarCodigoQR.QRCodeErrorCorrect =
                    Codec.QRCodeEncoder.ERROR_CORRECTION.H
        End Select

        'La versión "0" calcula automáticamente el tamaño
        generarCodigoQR.QRCodeVersion = 0

        '' --------- Forzar una determinada version -----------
        ''En caso de querer forzar una determinada version 
        '(tamaño) el siguiente código devuelve la
        ''versión mínima para el texto que se quiere códificar:
        'Dim iVersion As Integer = 
        '    AdjustQRVersion(TextBox1.Text, QRCodeEncoder.QRCodeErrorCorrect)
        'If iVersion = -1 Then
        '    MessageBox.Show("El texto es demasiado grande o el " +
        '        "Correction Level (ERROR_CORRECTION) no es el apropiado")
        '    Exit Sub
        'Else
        '    qrCodeEncoder.QRCodeVersion = iVersion
        'End If
        '' -----------------------------------------------------

        generarCodigoQR.QRCodeBackgroundColor =
            System.Drawing.Color.FromArgb(colorFondoQR)
        generarCodigoQR.QRCodeForegroundColor =
            System.Drawing.Color.FromArgb(colorQR)
        Return generarCodigoQR.Encode(texto)

    End Function
    Public Function VarXml(ByRef xAtt As XmlElement, ByVal strVar As String) As String
        VarXml = xAtt.GetAttribute(strVar)
        If VarXml = Nothing Then VarXml = ""
    End Function


    Function CONSTRUIRCADENACFDI(ByVal XML As String) As String

        Dim xslt_settings As New XsltSettings

        Dim lector As StreamReader = New StreamReader(XML)
        Dim xpathdocumento As XPath.XPathDocument = New XPath.XPathDocument(lector)

        Dim mitransforma As Xml.Xsl.XslCompiledTransform = New Xml.Xsl.XslCompiledTransform()
        mitransforma.Load("C:\multifacturas_sdk\xslt\cadenaoriginal_3_2.xslt")

        Dim str As StringWriter = New StringWriter
        Dim miwriter As Xml.XmlTextWriter = New XmlTextWriter(str)

        mitransforma.Transform(xpathdocumento, miwriter)

        Dim resultado As String
        resultado = str.ToString()
        Return resultado
    End Function



    'Error aritmetico
    Public Function MakeFileToSendMultifacturas(ByVal control As Clscontrolpago, ByVal _IVA As Decimal, ByVal _subtotal As Decimal, ByVal _total As Decimal, ByVal _serie As String, ByVal _folio As Integer, _FORMAFACTURADO As String, metodopago As String, uso As String, Optional _NOMBRERECEPTOR As String = "PUBLICO EN GENERAL", Optional _rfcreceptor As String = "XAXX010101000", Optional _EMPRESA As String = "COMISION DE AGUA POTABLA, ALCANTARILLADO DE SANTIAGO TULANTEPEC HGO") As MFSDK


        'Dim basem As New base
        'Dim empresa As OdbcDataReader
        'empresa = ConsultaSql("select * from empresa;").ExecuteReader
        ' empresa.Read()
        Dim sdk As MFSDK = New MFSDK()

        sdk = New MFSDK()
        sdk.Iniciales.Add("version_cfdi", "3.3")
        sdk.Iniciales.Add("MODOINI", "DIVISOR")

        sdk.Iniciales.Add("cfdi", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\facturas\cfdi_ejemplo_factura.xml")
        sdk.Iniciales.Add("xml_debug", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\facturas\sin_timbrar_factura.xml")
        sdk.Iniciales.Add("remueve_acentos", "NO")
        sdk.Iniciales.Add("RESPUESTA_UTF8", "SI")
        sdk.Iniciales.Add("html_a_txt", "NO")





        Dim emi As MFObject = New MFObject("emisor")
        If My.Settings.TimbrarPrueba.ToLower() = "si" Then
            'emi("rfc") = "lan7008173r5".ToUpper()
            'emi("nombre") = "General"
            'emi("RegimenFiscal") = "601"

            'emi("rfc") = "lan7008173r5".ToUpper()
            'emi("nombre") = "CINDEMEX SA DE CV"
            'emi("RegimenFiscal") = "601"

            'emi("rfc") = "CAA981221KEA".ToUpper()
            'emi("nombre") = "COMISION DE AGUA Y ALCANTARILLADO DEL MUNICIPIO DE PROGRESO ALVA"
            'emi("RegimenFiscal") = "601"

            emi("rfc") = "KIJ0906199R1".ToUpper()
            emi("nombre") = "KERNEL INDUSTIA JUGUETERA SA DE CV"
            emi("RegimenFiscal") = "603"
        Else
            emi("rfc") = obtenerCampo("select * from empresa ", "cnif")
            emi("nombre") = _EMPRESA
            emi("RegimenFiscal") = My.Settings.Regimen
        End If

        Dim recept As MFObject = New MFObject("receptor")
        recept("rfc") = _rfcreceptor.Replace("Ñ", "%ntilde;").Replace("&", "&amp;").Replace("'", "&apos;")
        recept("nombre") = _NOMBRERECEPTOR.Replace("Ñ", "%ntilde;").Replace("&", "&amp;").Replace("'", "&apos;")
        recept("UsoCFDI") = uso

        ' Concepto Normal


        Dim conceptos As MFObject = New MFObject("conceptos")

        Dim acuiva As Decimal = 0
        conceptos.Subnodos.Clear()

        For i = 1 To control.Listadeconceptos.Count
            Dim concepto As Clsconcepto = control.Listadeconceptos.Item(i)

            If concepto.Preciounitario > 0 Then
                Dim concepto0 As MFObject = New MFObject(i.ToString())
                concepto0("ClaveProdServ") = concepto.clavesat
                concepto0("NoIdentificacion") = "COD" & i.ToString()
                concepto0("Cantidad") = concepto.Cantidad
                concepto0("ClaveUnidad") = concepto.unidadsat
                concepto0("Descripcion") = concepto.Concepto
                concepto0("ValorUnitario") = concepto.Preciounitario
                concepto0("Importe") = concepto.importe
                ' Impuestos de conceptos
                Dim impuestos As MFObject = New MFObject("Impuestos")
                ' Traslados
                Dim itras As MFObject = New MFObject("Traslados")
                Dim itra0 As MFObject = New MFObject(i.ToString())
                itra0("Base") = concepto.importe
                itra0("Impuesto") = "002"

                If concepto.IVA > 0 Then
                    itra0("Importe") = Math.Round(concepto.importe * 0.16, 2)
                    acuiva = acuiva + Math.Round(concepto.importe * 0.16, 2)
                    itra0("TasaOCuota") = "0.160000"
                    itra0("TipoFactor") = "Tasa"
                Else
                    itra0("TipoFactor") = "Exento"
                End If

                itras.AgregaSubnodo(itra0)
                impuestos.AgregaSubnodo(itras)
                concepto0.AgregaSubnodo(impuestos)

                conceptos.AgregaSubnodo(concepto0)
            End If
        Next

        Dim fact As MFObject = New MFObject("factura")
        fact("serie") = _serie
        fact("folio") = _folio
        fact("fecha_expedicion") = DateTime.Now.ToString("s")
        fact("metodo_pago") = metodopago ' NUEVA FORMA DE PAGO 3.3
        fact("forma_pago") = _FORMAFACTURADO
        fact("condicionesDePago") = "condiciones"
        fact("tipocomprobante") = "I"
        fact("moneda") = "MXN"
        fact("tipocambio") = "1"
        fact("LugarExpedicion") = obtenerCampo("select * from empresa ", "ccodpos")
        fact("RegimenFiscal") = My.Settings.Regimen
        fact("subtotal") = _subtotal '
        _total = _subtotal + acuiva
        '  fact("descuento") = "0.00"
        fact("total") = _total ' 100.0



        ' Impuestos
        Dim impuestos3 As MFObject = New MFObject("impuestos")
        _IVA = Math.Round(acuiva, 2)

        If _IVA > 0 Then
            impuestos3("TotalImpuestosTrasladados") = _IVA
            ' Traslados
            Dim itraslados As MFObject = New MFObject("translados")
            Dim itraslado0 As MFObject = New MFObject("0")
            itraslado0("Impuesto") = "002"
            itraslado0("Importe") = _IVA
            itraslado0("TasaOCuota") = "0.160000"
            itraslado0("TipoFactor") = "Tasa"
            itraslados.AgregaSubnodo(itraslado0)
            impuestos3.AgregaSubnodo(itraslados)
        End If


        sdk.AgregaObjeto(PAC())
        sdk.AgregaObjeto(Conf())
        sdk.AgregaObjeto(fact)

        sdk.AgregaObjeto(emi)
        sdk.AgregaObjeto(recept)
        sdk.AgregaObjeto(conceptos)
        sdk.AgregaObjeto(impuestos3)


        Return sdk


    End Function 'para multifacturas

    Public Function PAC() As MFObject
        Dim p As MFObject = New MFObject("PAC")
        If My.Settings.TimbrarPrueba.ToLower = "si" Then

            p("usuario") = "DEMO700101XXX"
            p("pass") = "DEMO700101XXX"
            p("produccion") = "NO"
        Else

            p("usuario") = My.Settings.UsuarioMultifacturas
            p("pass") = My.Settings.PassFacturaMultifacturas
            p("produccion") = "SI"
        End If

        Return p
    End Function
    Public Function PAC2() As MFObject
        Dim p As MFObject = New MFObject("PAC")
        If My.Settings.TimbrarPrueba.ToLower = "si" Then

            p("usuario") = "DEMO700101XXX"
            p("pass") = "DEMO700101XXX"

        Else

            p("usuario") = My.Settings.UsuarioMultifacturas
            p("pass") = My.Settings.PassFacturaMultifacturas

        End If
        Return p
    End Function

    Public Function Conf() As MFObject
        Dim con As MFObject = New MFObject("conf")

        If My.Settings.TimbrarPrueba.ToLower = "si" Then

            con("cer") = "C:\\SDK2\\certificados\\lan7008173r5.cer.pem"
            con("key") = "C:\\SDK2\\certificados\\lan7008173r5.key.pem"
            con("pass") = "12345678a"
        Else

            con("cer") = My.Settings.CER
            con("key") = My.Settings.KEY
            con("pass") = My.Settings.KeyContrasena
        End If
        ' Se indica la ruta y contraseña de los archivos CSD (Archivos CSD de pruebas)


        Return con
    End Function

    Public Function consulta_saldo() As String
        Dim sdk As MFSDK = New MFSDK()
        sdk.Iniciales.Add("SALDO", "SI")
        sdk.AgregaObjeto(PAC2())
        ' Muestras la estructura
        Dim sdkresp As SDKRespuesta
        sdkresp = sdk.Timbrar("C:\sdk2\timbrar32.bat", "C:\sdk2\timbrados\", "factura", False)
        Dim cadenas As String()
        cadenas = sdkresp.Codigo_MF_Numero.Split("=")

        Dim saldo As String = cadenas(2)

        Return saldo

    End Function

    'Public Function cancela_factura(UUID As String, RFC As String) As MFSDK
    '    Dim sdk As MFSDK = New MFSDK()

    '    sdk.Iniciales.Add("modulo", "cancelacion2018")
    '    sdk.Iniciales.Add("accion", "cancelar")

    '    If (My.Settings.TimbrarPrueba.ToUpper = "SI") Then

    '        sdk.Iniciales.Add("b64Cer", "C:\SDK2\certificados\demo.cer.pem")
    '        sdk.Iniciales.Add("b64Key", "C:\SDK2\certificados\demo.key.pem")
    '        sdk.Iniciales.Add("password", "12345678a")

    '    Else

    '        sdk.Iniciales.Add("b64Cer", "C:\sdk2\certificados\CAA981221KEA.cer.pem")
    '        sdk.Iniciales.Add("b64Key", "C:\sdk2\certificados\CAA981221KEA.key.pem")
    '        sdk.Iniciales.Add("password", My.Settings.KeyContrasena)

    '    End If

    '    sdk.Iniciales.Add("uuid", UUID)
    '    sdk.Iniciales.Add("rfc", RFC)



    '    ''sdk.Iniciales.Add("cfdi", FXML)
    '    ''sdk.Iniciales.Add("cancelar", "SI")
    '    sdk.AgregaObjeto(PAC())
    '    ''sdk.AgregaObjeto(Conf())
    '    ' Muestras la estructura
    '    Return sdk
    'End Function


    Public Function cancela_factura(FXML As String) As MFSDK
        Dim sdk As MFSDK = New MFSDK()
        sdk.Iniciales.Add("cfdi", FXML)
        sdk.Iniciales.Add("cancelar", "SI")

        sdk.AgregaObjeto(PAC())
        sdk.AgregaObjeto(Conf())
        ' Muestras la estructura
        Return sdk
    End Function





    'Public String Cancela40(String uuid, String motivo, int IDFactura, int UserID, String viene)
    '    {
    '        //MotivoCancelacionContext db = New MotivoCancelacionContext();


    '        //Certificados de prueba
    '        String usuario = @"C:\sdk2\certificados\lan7008173r5.cer.pem";
    '        String pass = @"C:\sdk2\certificados\lan7008173r5.key.pem";
    '        String rfc = "LAN7008173R5";

    '        String cer = Convert.ToBase64String(File.ReadAllBytes(@"C:\sdk2\certificados\lan7008173r5.cer.pem"));
    '        String key = Convert.ToBase64String(File.ReadAllBytes(@"C:\sdk2\certificados\lan7008173r5.key.pem"));
    '        String passcer = "12345678a";


    '        If (Settings.Default.TimbrarPrueba.ToUpper() == "NO")
    '        {
    '            String SQLEmpresa = "select * from Empresa";
    '            Empresa datosNomList = New CobroDefault().Database.SqlQuery < Empresa > (SQLEmpresa).FirstOrDefault();


    '            //usuario y pass los proporcionara maestra Gaby
    '            usuario = Settings.Default.UsuarioMultifacturas;
    '            pass = Settings.Default.PasswordMultifacturas;

    '            //Obtener los certificados de Actopan desde Settings o hacer una tabla donde se carguen los certificados y obtenerlos
    '            cer = Convert.ToBase64String(File.ReadAllBytes(Settings.Default.CertificadoCliente));
    '            key = Convert.ToBase64String(File.ReadAllBytes(Settings.Default.keycliente));
    '            //passcer = Convert.ToBase64String(Encoding.UTF8.GetBytes(certificado.PassCertificado));
    '            passcer = Settings.Default.passsCliente;
    '            rfc = datosNomList.RFC; //emisor
    '        }



    '        WSCancela.Cancelarcfdi40SAT cliente = New WSCancela.Cancelarcfdi40SAT();
    '        WSCancela.datos datos = New WSCancela.datos();

    '        datos.accion = "cancelar";
    '        datos.b64Cer = cer;
    '        datos.b64Key = key;
    '        datos.motivo = motivo;
    '        datos.pass = pass;
    '        datos.password = passcer;
    '        datos.produccion = "SI";
    '        datos.usuario = usuario;
    '        datos.uuid = uuid;
    '        //datos.folioSustitucion = uuidsusti;
    '        datos.rfc = rfc;



    '        Try
    '        {

    '            WSCancela.respuesta respuesta = cliente.cancelarCfdi(datos);

    '            If (respuesta.codigo_mf_texto.Contains("OK"))
    '            {


    '                //if (viene == "Factura")
    '                //{
    '                //    string insert = "insert into FacturasCanceladasSAT (Estado, IDFactura,Fecha,Usuario) values " +
    '                //   "('C'," + IDFactura + ",sysdatetime()," + "UsuarioDelSistema" + ")";
    '                //    //MessageBox.Show("Hacer procedimiento");
    '                //    New CobroDefault().Database.ExecuteSqlCommand(insert);
    '                //}
    '                //else
    '                //{
    '                //    string insert = "insert into FacturasCanceladasSAT (Estado, IDFactura,Fecha,Usuario) values " +
    '                //   "('C'," + IDFactura + ",sysdatetime()," + "UsuarioDelSistema" + ")";
    '                //    //MessageBox.Show("Hacer procedimiento");
    '                //    New CobroDefault().Database.ExecuteSqlCommand(insert);
    '                //}
    '                //  MessageBox.Show("La factura fue cancelada ante el sat");
    '                MessageBox.Show("UUID CANCELADO CORRECTAMENTE");
    '                Return respuesta.acuse;
    '            }
    '            Else
    '            {
    '                MessageBox.Show("OCURRIO UN ERROR AL CANCELAR EL UUID");
    '                Return "";
    '            }
    '        }
    '        Catch (Exception err)
    '        {
    '            MessageBox.Show("OCURRIO UN ERROR AL CANCELAR EL UUID \n" + err);
    '            Return "";
    '        }
    '    }

    Public Function Cancela40()


    End Function


End Module

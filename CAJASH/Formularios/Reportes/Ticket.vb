Imports System.Drawing.Printing
Imports System.IO
Imports System.Text
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.qrcode
Imports ZXing

Public Class Ticket

    Public Sub imprime_ticket58mm(Serie As String, folio As Integer, preview As Boolean)

        Dim DATOS As IDataReader
        Dim CONTE As IDataReader
        Try
            DATOS = ConsultaSql("SELECT * FROM PAGOS WHERE SERIE='" & Serie & "' AND recibo=" & folio).ExecuteReader
            DATOS.Read()
            CONTE = ConsultaSql("SELECT * FROM PAGOtros WHERE SERIE='" & Serie & "' AND recibo=" & folio).ExecuteReader

        Catch ex As Exception

            Exit Sub
        End Try
        Dim UBICACION As String = String.Empty

        Try
            If DATOS("ESUSUARIO") = 1 Then
                UBICACION = obtenerCampo("SELECT UBICACION FROM USUARIO WHERE CUENTA=" & DATOS("CUENTA"), "UBICACION")
            End If

        Catch ex As Exception
            UBICACION = String.Empty
        End Try

        Try

            'Crear el directorio en donde se van a almacenar los PDF
            If Not My.Computer.FileSystem.DirectoryExists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\ReporteCaja\" & Year(Now) & acompletacero(Month(Now).ToString(), 2).Trim) Then

                My.Computer.FileSystem.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\ReporteCaja\" & Year(Now) & acompletacero(Month(Now).ToString(), 2).Trim)
            End If

            Dim cadenafolder As String = (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\ReporteCaja\" & Year(Now) & acompletacero(Month(Now).ToString(), 2)).Trim

            'Dar propiedades al Documento
            Dim ticket As Rectangle = New Rectangle(150.63F, 500.0F)
            Dim pdfDoc As New Document(ticket, 10.0F, 0.0F, 0.0F, 0.0F)

            'Obtener la ruta donde se va a crear el pdf
            Dim pdfWrite As PdfWriter = PdfWriter.GetInstance(pdfDoc, New System.IO.FileStream(cadenafolder & "\ticket_" & Serie & folio & ".pdf", FileMode.Create))

            'Formato de letras
            Dim Font8 As New Font(FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.NORMAL))
            Dim Font5 As New Font(FontFactory.GetFont(FontFactory.TIMES_ROMAN, 5, iTextSharp.text.Font.NORMAL))
            Dim Font6 As New Font(FontFactory.GetFont(FontFactory.TIMES_ROMAN, 6, iTextSharp.text.Font.NORMAL))
            Dim Font6n As New Font(FontFactory.GetFont(FontFactory.TIMES_ROMAN, 6, iTextSharp.text.Font.BOLD))
            Dim Font7 As New Font(FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, iTextSharp.text.Font.NORMAL))
            Dim Font4 As New Font(FontFactory.GetFont(FontFactory.TIMES_ROMAN, 4, iTextSharp.text.Font.NORMAL))
            Dim Font8N As New Font(FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.BOLD))
            Dim Font13N As New Font(FontFactory.GetFont(FontFactory.TIMES_ROMAN, 13, iTextSharp.text.Font.BOLD))
            Dim Font10N As New Font(FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.BOLD))
            Dim Font12N As New Font(FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.BOLD))
            Dim Font9 As New Font(FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL))
            Dim Font7White As New Font(FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, iTextSharp.text.Font.BOLD, BaseColor.WHITE))
            Dim Font8White As New Font(FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.BOLD, BaseColor.WHITE))
            Dim Font5White As New Font(FontFactory.GetFont(FontFactory.TIMES_ROMAN, 5, iTextSharp.text.Font.BOLD, BaseColor.WHITE))
            Dim Font7courierN As New Font(FontFactory.GetFont(FontFactory.TIMES_ROMAN, 6, iTextSharp.text.Font.BOLD))

            For I As Integer = 1 To My.Settings.copiasderecibo
                Dim imagenBMP As iTextSharp.text.Image
                imagenBMP = iTextSharp.text.Image.GetInstance(LOGOBYTE)
                imagenBMP.ScaleToFit(60.0F, 40.0F)

                imagenBMP.Border = 0
                'Abrimos el pdf para comenzar a escribir en el


                Dim TableVacio As PdfPTable = New PdfPTable(1)
                TableVacio.DefaultCell.Border = BorderStyle.None
                TableVacio.WidthPercentage = 100
                Dim widthsVacio As Single() = New Single() {80.0F}
                TableVacio.SetWidths(widthsVacio)

                Dim ColVacio = New PdfPCell(New Phrase(" ", Font5White))
                ColVacio.Border = 0
                ColVacio.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                TableVacio.AddCell(ColVacio)


                pdfDoc.Open()

                Dim TableEncGeneral As PdfPTable = New PdfPTable(1)
                TableEncGeneral.DefaultCell.Border = BorderStyle.None
                TableEncGeneral.WidthPercentage = 100
                Dim widthsEncGen2 As Single() = New Single() {80.0F}
                TableEncGeneral.SetWidths(widthsEncGen2)


                Dim Colimagen = New PdfPCell(imagenBMP)
                Colimagen.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                Colimagen.Border = 0

                TableEncGeneral.AddCell(Colimagen)


                Dim colmensaje = New PdfPCell(New Phrase("O O M S A P A S", Font8))
                colmensaje.Border = 0
                colmensaje.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                TableEncGeneral.AddCell(colmensaje)

                colmensaje = New PdfPCell(New Phrase("DE MULEGÉ", Font8))
                colmensaje.Border = 0
                colmensaje.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                TableEncGeneral.AddCell(colmensaje)

                colmensaje = New PdfPCell(New Phrase("AV. CONSTITUCION Y CALLE 3", Font6))
                colmensaje.Border = 0
                colmensaje.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                TableEncGeneral.AddCell(colmensaje)

                colmensaje = New PdfPCell(New Phrase("COL. CENTRO, MULEGE, BCS.", Font6))
                colmensaje.Border = 0
                colmensaje.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                TableEncGeneral.AddCell(colmensaje)

                colmensaje = New PdfPCell(New Phrase("CP 23920, TEL. 615 1522252", Font6))
                colmensaje.Border = 0
                colmensaje.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                TableEncGeneral.AddCell(colmensaje)

                pdfDoc.Add(TableEncGeneral)

                pdfDoc.Add(TableVacio)

                Dim TableDatos As PdfPTable = New PdfPTable(2)
                TableDatos.DefaultCell.Border = BorderStyle.None
                TableDatos.HorizontalAlignment = HorizontalAlignment.Left
                '  TableDatos.WidthPercentage = 100
                Dim widthsdatos2 As Single() = New Single() {50.0F, 90.0F}
                TableDatos.SetWidths(widthsdatos2)

                colmensaje = New PdfPCell(New Phrase("FECHA", Font6))
                colmensaje.Border = 0
                colmensaje.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                TableDatos.AddCell(colmensaje)




                Dim fecha As String = DATOS("fecha_Act")
                colmensaje = New PdfPCell(New Phrase(fecha, Font6n))
                colmensaje.Border = 0
                colmensaje.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                TableDatos.AddCell(colmensaje)

                colmensaje = New PdfPCell(New Phrase("RECIBO", Font6))
                colmensaje.Border = 0
                colmensaje.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                TableDatos.AddCell(colmensaje)


                colmensaje = New PdfPCell(New Phrase(DATOS("SERIE") & " " & DATOS("RECIBO") & "    CAJA: " & DATOS("CAJA"), Font6n))
                colmensaje.Border = 0
                colmensaje.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                TableDatos.AddCell(colmensaje)



                colmensaje = New PdfPCell(New Phrase("CUENTA", Font6))
                colmensaje.Border = 0
                colmensaje.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                TableDatos.AddCell(colmensaje)


                colmensaje = New PdfPCell(New Phrase(DATOS("CUENTA"), Font6n))
                colmensaje.Border = 0
                colmensaje.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                TableDatos.AddCell(colmensaje)


                colmensaje = New PdfPCell(New Phrase("UBICACION", Font6))
                colmensaje.Border = 0
                colmensaje.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                TableDatos.AddCell(colmensaje)


                colmensaje = New PdfPCell(New Phrase(UBICACION, Font6n))
                colmensaje.Border = 0
                colmensaje.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                TableDatos.AddCell(colmensaje)


                colmensaje = New PdfPCell(New Phrase("NOMBRE", Font6))
                colmensaje.Border = 0
                colmensaje.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                TableDatos.AddCell(colmensaje)


                colmensaje = New PdfPCell(New Phrase(DATOS("NOMBRE"), Font6n))
                colmensaje.Border = 0
                colmensaje.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                TableDatos.AddCell(colmensaje)
                Dim usuario As IDataReader
                If DATOS("esusuario") = "1" Then
                    usuario = ConsultaSql("select * from vusuario where cuenta=" & DATOS("cuenta")).ExecuteReader
                End If

                If DATOS("esusuario") = "2" Then
                    usuario = ConsultaSql("select * from nousuarios where clave=" & DATOS("cuenta")).ExecuteReader
                End If
                If DATOS("esusuario") = "3" Then
                    usuario = ConsultaSql("select * from Vsolicitud where numero=" & DATOS("cuenta")).ExecuteReader
                End If
                usuario.Read()


                Dim colonia As String = usuario("COLONIA")
                Dim comunidad As String = usuario("COMUNIDAD")
                Dim direccion As String = usuario("DIRECCION")


                colmensaje = New PdfPCell(New Phrase("DIRECCION", Font6))
                colmensaje.Border = 0
                colmensaje.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                TableDatos.AddCell(colmensaje)


                colmensaje = New PdfPCell(New Phrase(direccion, Font6n))
                colmensaje.Border = 0
                colmensaje.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                TableDatos.AddCell(colmensaje)

                colmensaje = New PdfPCell(New Phrase("COLONIA", Font6))
                colmensaje.Border = 0
                colmensaje.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                TableDatos.AddCell(colmensaje)


                colmensaje = New PdfPCell(New Phrase(colonia, Font6n))
                colmensaje.Border = 0
                colmensaje.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                TableDatos.AddCell(colmensaje)
                colmensaje = New PdfPCell(New Phrase("COMUNIDAD", Font6))
                colmensaje.Border = 0
                colmensaje.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                TableDatos.AddCell(colmensaje)


                colmensaje = New PdfPCell(New Phrase(comunidad, Font6n))
                colmensaje.Border = 0
                colmensaje.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                TableDatos.AddCell(colmensaje)

                colmensaje = New PdfPCell(New Phrase("TARIFA", Font6))
                colmensaje.Border = 0
                colmensaje.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                TableDatos.AddCell(colmensaje)



                Dim TARIFA As String = obtenerCampo("SELECT * FROM cuotas where id_tarifa ='" & DATOS("TARIFA") & "'", "descripcion_cuota")
                If TARIFA = "0" Then
                    TARIFA = ""
                End If

                colmensaje = New PdfPCell(New Phrase(TARIFA, Font6n))
                colmensaje.Border = 0
                colmensaje.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                TableDatos.AddCell(colmensaje)





                pdfDoc.Add(TableDatos)

                pdfDoc.Add(TableVacio)

                Dim Tableenca3 As PdfPTable = New PdfPTable(1)
                Tableenca3.DefaultCell.Border = BorderStyle.None
                Tableenca3.WidthPercentage = 100

                Tableenca3.SetWidths(widthsVacio)

                Dim linea = New PdfPCell(New Phrase("_____________________________", Font8N))
                linea.Border = 0
                linea.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                Tableenca3.AddCell(linea)

                linea = New PdfPCell(New Phrase("CONCEPTO                   IMPORTE", Font8N))
                linea.Border = 0
                linea.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                Tableenca3.AddCell(linea)

                linea = New PdfPCell(New Phrase("_____________________________", Font8N))
                linea.Border = 0
                linea.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                Tableenca3.AddCell(linea)

                pdfDoc.Add(Tableenca3)



                Dim TableDatos2 As PdfPTable = New PdfPTable(2)
                TableDatos2.DefaultCell.Border = BorderStyle.None
                '   TableDatos2.WidthPercentage = 100
                TableDatos2.HorizontalAlignment = HorizontalAlignment.Left
                Dim widthsdatos21 As Single() = New Single() {85.0F, 50.0F}
                TableDatos2.SetWidths(widthsdatos21)


                Do While CONTE.Read
                    Dim colmensajec As PdfPCell = New PdfPCell(New Phrase(CONTE("CONCEPTO"), Font6))
                    colmensajec.Border = 0
                    colmensajec.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    TableDatos2.AddCell(colmensajec)

                    Dim IMPO As Decimal = Decimal.Parse(CONTE("IMPORTE").ToString())

                    colmensajec = New PdfPCell(New Phrase(IMPO.ToString("C"), Font6))
                    colmensajec.Border = 0
                    colmensajec.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    TableDatos2.AddCell(colmensajec)
                Loop
                colmensaje = New PdfPCell(New Phrase("          ", Font6))
                colmensaje.Border = 0
                colmensaje.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                TableDatos2.AddCell(colmensaje)

                colmensaje = New PdfPCell(New Phrase("___________", Font6))
                colmensaje.Border = 0
                colmensaje.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                TableDatos2.AddCell(colmensaje)

                colmensaje = New PdfPCell(New Phrase("SUBTOTAL", Font6))
                colmensaje.Border = 0
                colmensaje.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                TableDatos2.AddCell(colmensaje)

                Dim subtotal As Decimal = Decimal.Parse(DATOS("pagos").ToString())
                Dim ivaA As Decimal = Decimal.Parse(DATOS("IVA").ToString())


                colmensaje = New PdfPCell(New Phrase(subtotal, Font6))
                colmensaje.Border = 0
                colmensaje.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                TableDatos2.AddCell(colmensaje)
                colmensaje = New PdfPCell(New Phrase("IVA", Font6))
                colmensaje.Border = 0
                colmensaje.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                TableDatos2.AddCell(colmensaje)

                colmensaje = New PdfPCell(New Phrase(ivaA.ToString("C"), Font6))
                colmensaje.Border = 0
                colmensaje.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                TableDatos2.AddCell(colmensaje)
                colmensaje = New PdfPCell(New Phrase("TOTAL", Font6))
                colmensaje.Border = 0
                colmensaje.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                TableDatos2.AddCell(colmensaje)

                Dim TOTAL As Decimal = Decimal.Parse(DATOS("TOTAL").ToString())

                colmensaje = New PdfPCell(New Phrase(TOTAL.ToString("C"), Font6))
                colmensaje.Border = 0
                colmensaje.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                TableDatos2.AddCell(colmensaje)

                pdfDoc.Add(TableDatos2)


                Dim Tableenca4 As PdfPTable = New PdfPTable(1)
                Tableenca4.DefaultCell.Border = BorderStyle.None
                Tableenca4.WidthPercentage = 100
                Dim widthsEncGen4 As Single() = New Single() {160.0F}
                Tableenca4.SetWidths(widthsEncGen4)

                linea = New PdfPCell(New Phrase(ConvertCurrencyToSpanish(TOTAL, "pesos"), Font6))
                linea.Border = 0
                linea.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                Tableenca4.AddCell(linea)

                Dim formadepago As String = DATOS("CCODPAGO").ToString()

                Dim forma As String = obtenerCampo("select * from fpago where ccodpago='" & formadepago & "'", "CDESPAGO")
                linea = New PdfPCell(New Phrase("", Font10N))
                linea.Border = 0
                linea.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                Tableenca4.AddCell(linea)
                linea = New PdfPCell(New Phrase("FORMA DE PAGO " & forma, Font8N))
                linea.Border = 0
                linea.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                Tableenca4.AddCell(linea)


                linea = New PdfPCell(New Phrase("", Font10N))
                linea.Border = 0
                linea.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                Tableenca4.AddCell(linea)


                Dim codigoQR = New StringBuilder()
                codigoQR.Append("RECIBO: " & Serie & folio & ", FECHA:" & Date.Now.ToString() & " , TOTAL " & TOTAL)

                Dim pdfCodigoQR = New BarcodeQRCode(codigoQR.ToString(), 0, 0, New Dictionary(Of iTextSharp.text.pdf.qrcode.EncodeHintType, System.Object))
                Dim img As Image = pdfCodigoQR.GetImage()
                img.SpacingAfter = 0.0F
                img.SpacingBefore = 0.0F
                img.BorderWidth = 1.0F
                img.HasAbsolutePosition()
                'img.ScalePercent(100, 78)
                Tableenca4.AddCell(img)

                linea = New PdfPCell(New Phrase("*** GRACIAS POR TU PAGO ***", Font8N))
                linea.Border = 0
                linea.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                Tableenca4.AddCell(linea)

                pdfDoc.Add(Tableenca4)

                pdfDoc.Add(TableVacio)
                ' Crear un objeto QRCode
                'Agregamos el codigo QR al documento
                pdfDoc.Add(TableVacio)
                pdfDoc.Add(TableVacio)

                pdfDoc.Add(TableVacio)
                pdfDoc.Add(TableVacio)

            Next
            pdfDoc.Close()

            If preview = True Then
                Try
                    Dim psi As New ProcessStartInfo(cadenafolder & "\ticket_" & Serie & folio & ".pdf")
                    'psi.WorkingDirectory = cadenafolder & "\factura\" + nombresespacios

                    psi.WindowStyle = ProcessWindowStyle.Hidden
                    Dim p As Process = Process.Start(psi)
                Catch ex As Exception
                    MessageBox.Show("Error al visualizar el pdf, posiblemente el archivo este en uso, cierrelo antes de generar un nuevo reporte" & ex.Message)
                End Try
            Else
                Dim gsProcessInfo As ProcessStartInfo
                Dim gsProcess As Process
                gsProcessInfo = New ProcessStartInfo()
                gsProcessInfo.Verb = "Print"
                gsProcessInfo.WindowStyle = ProcessWindowStyle.Hidden
                gsProcessInfo.FileName = cadenafolder & "\ticket_" & Serie & folio & ".pdf"
                ' gsProcessInfo.Arguments = """" & nombreImpresora & """"
                gsProcess = Process.Start(gsProcessInfo)
                gsProcess.WaitForInputIdle(2200)
                gsProcess.Close()
                '  gsProcess.Kill()

                ' gsProcess.EnableRaisingEvents = True


            End If

        Catch ex As Exception
            MessageBox.Show("Error crear el pdf " & ex.Message)
        End Try
        DATOS.Close()
        CONTE.Close()

    End Sub



End Class

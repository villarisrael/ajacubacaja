Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf


Public Class reporteResumenFecha

    Dim fechaActual As String
    Public Sub CreaResumen(ByVal sql As String, ByVal filtro As String, ByVal Caja As String, sqldescuento As String, sqlDescuentosRecargosP As String, sqlformapago As String)

        fechaActual = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper()

        Try

            'Crear el directorio en donde se van a almacenar los PDF
            If Not My.Computer.FileSystem.DirectoryExists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\ReporteCaja\" & Year(Now) & acompletacero(Month(Now).ToString(), 2).Trim) Then

                My.Computer.FileSystem.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\ReporteCaja\" & Year(Now) & acompletacero(Month(Now).ToString(), 2).Trim)
            End If

            Dim cadenafolder As String = (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\ReporteCaja\" & Year(Now) & acompletacero(Month(Now).ToString(), 2)).Trim

            'Dar propiedades al Documento
            Dim pdfDoc As New Document(iTextSharp.text.PageSize.LETTER, 15.0F, 15.0F, 5.0F, 3.0F)

            'Obtener la ruta donde se va a crear el pdf
            Dim pdfWrite As PdfWriter = PdfWriter.GetInstance(pdfDoc, New System.IO.FileStream(cadenafolder & "\CorteCaja_Resumen" & filtro & ".pdf", FileMode.Create))

            'Formato de letras
            Dim Font8 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.NORMAL))
            Dim Font5 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 5, iTextSharp.text.Font.NORMAL))
            Dim Font7 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 7, iTextSharp.text.Font.NORMAL))
            Dim Font4 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 4, iTextSharp.text.Font.NORMAL))
            Dim Font8N As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.BOLD))
            Dim Font13N As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 13, iTextSharp.text.Font.BOLD))
            Dim Font10N As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.BOLD))
            Dim Font12N As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.BOLD))
            Dim Font9 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 9, iTextSharp.text.Font.NORMAL))
            Dim Font7White As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.WHITE))
            Dim Font8White As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.WHITE))
            Dim Font5White As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 5, iTextSharp.text.Font.BOLD, BaseColor.WHITE))
            Dim Font7courierN As New Font(FontFactory.GetFont(FontFactory.COURIER, 6, iTextSharp.text.Font.BOLD))

            'Abrimos el pdf para comenzar a escribir en el
            pdfDoc.Open()


#Region ""

            Dim imagenBMP As iTextSharp.text.Image
            imagenBMP = iTextSharp.text.Image.GetInstance(LOGOBYTE)
            imagenBMP.ScaleToFit(60.0F, 40.0F)

            imagenBMP.Border = 0

            Dim widthsRec As Single() = New Single() {50.0F, 50.0, 55.0F, 110.0F, 70.0F, 70.0F, 80.0F, 40.0F, 70.0F}
            Dim ColRecibo = New PdfPCell(New Phrase("", Font7White))


#End Region
            Dim TableVacio As PdfPTable = New PdfPTable(1)
            TableVacio.DefaultCell.Border = BorderStyle.None
            TableVacio.WidthPercentage = 100
            Dim widthsVacio As Single() = New Single() {1000.0F}
            TableVacio.SetWidths(widthsVacio)

            Dim widthsEncGen2 As Single() = New Single() {602.0F}


            Try

                Dim ColVacio = New PdfPCell(New Phrase(" ", Font5White))
                ColVacio.Border = 0
                ColVacio.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                TableVacio.AddCell(ColVacio)


                Dim TableEncGeneral As PdfPTable = New PdfPTable(1)
                TableEncGeneral.DefaultCell.Border = BorderStyle.None
                TableEncGeneral.WidthPercentage = 100

                TableEncGeneral.SetWidths(widthsEncGen2)


                Dim ColPiePag1 = New PdfPCell(New Phrase("", Font5White))
                ColPiePag1.Border = 0
                ColPiePag1.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableEncGeneral.AddCell(ColPiePag1)


                Dim TableEnc2 As PdfPTable = New PdfPTable(2)
                TableEnc2.DefaultCell.Border = BorderStyle.None
                TableEnc2.WidthPercentage = 100
                Dim widthsEnc2 As Single() = New Single() {60.0F, 900.0F}
                TableEnc2.SetWidths(widthsEnc2)

                Dim Colimagen = New PdfPCell(imagenBMP)
                Colimagen.Border = 0
                Colimagen.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                TableEnc2.AddCell(Colimagen)


                ColPiePag1 = New PdfPCell(New Phrase(Empresa, Font10N))
                ColPiePag1.Border = 0
                ColPiePag1.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableEnc2.AddCell(ColPiePag1)



                ColPiePag1 = New PdfPCell(New Phrase("REPORTE DE INGRESOS POR RECIBO DESGLOZADO", Font10N))
                ColPiePag1.Border = 0
                ColPiePag1.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)



                TableEncGeneral.AddCell(TableEnc2)

                TableEncGeneral.AddCell(ColPiePag1)

                Dim TableEncFechas As PdfPTable = New PdfPTable(2)
                TableEncFechas.DefaultCell.Border = BorderStyle.None
                TableEncFechas.WidthPercentage = 100
                Dim widthsEncFec As Single() = New Single() {650.0F, 400.0F}
                TableEncFechas.SetWidths(widthsEncFec)

                If Caja = "" Then
                    Caja = "CORTE GENERAL"
                End If

                Dim ColEncFec = New PdfPCell(New Phrase("REPORTE DE INGRESOS DEL " & filtro & "            CAJA: " & Caja, Font8))
                ColEncFec.Border = 0
                ColEncFec.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableEncFechas.AddCell(ColEncFec)


                fechaActual = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper()

                ColEncFec = New PdfPCell(New Phrase("FECHA DE EMISIÓN: " & fechaActual, Font8))
                ColEncFec.Border = 0
                ColEncFec.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableEncFechas.AddCell(ColEncFec)



                Dim TableRecibos As PdfPTable = New PdfPTable(5)
                TableRecibos.DefaultCell.Border = BorderStyle.None
                TableRecibos.WidthPercentage = 100
                Dim widthsRec2 As Single() = New Single() {70.0F, 110.0F, 70.0, 70.0F, 110.0F}
                TableRecibos.SetWidths(widthsRec2)

                ColRecibo = New PdfPCell(New Phrase("FECHA", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase("SUBTOTAL ", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase("IVA", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase("TOTAL", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)


                ColRecibo = New PdfPCell(New Phrase("DESCUENTO", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)
                Dim DATA As IDataReader = ConsultaSql(sql).ExecuteReader()


                Dim sumaSubtotal As Decimal = 0
                Dim sumiva As Decimal = 0
                Dim sumTotal As Decimal = 0
                Dim sumDescuento As Decimal = 0




                Do While DATA.Read
                    ColRecibo = New PdfPCell(New Phrase(DATA("FECHA").ToString().Substring(0, 8), Font8))
                    ColRecibo.Border = 0
                    ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    '    ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    TableRecibos.AddCell(ColRecibo)

                    sumaSubtotal += DATA(1)

                    ColRecibo = New PdfPCell(New Phrase(DATA(1).ToString(), Font8))
                    ColRecibo.Border = 0
                    ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    ' ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    TableRecibos.AddCell(ColRecibo)


                    sumiva += DATA(2)
                    ColRecibo = New PdfPCell(New Phrase(DATA(2).ToString(), Font8))
                    ColRecibo.Border = 0
                    ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    '  ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    TableRecibos.AddCell(ColRecibo)


                    sumTotal += DATA(3)

                    ColRecibo = New PdfPCell(New Phrase(DATA(3).ToString(), Font8))
                    ColRecibo.Border = 0
                    ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    '  ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    TableRecibos.AddCell(ColRecibo)

                    ColRecibo = New PdfPCell(New Phrase(DATA(4).ToString(), Font8))
                    ColRecibo.Border = 0
                    ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    '  ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    TableRecibos.AddCell(ColRecibo)

                    sumDescuento += DATA(4)
                Loop


                ColRecibo = New PdfPCell(New Phrase("TOTAL", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase(sumaSubtotal.ToString("C"), Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase(sumiva.ToString("C"), Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase(sumTotal.ToString("C"), Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)


                ColRecibo = New PdfPCell(New Phrase(sumDescuento.ToString("C"), Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)


                'Actopan
                '30,3,4,5


                Dim Tabletitdescuentos As PdfPTable = New PdfPTable(1)
                Tabletitdescuentos.DefaultCell.Border = BorderStyle.None
                TableEncGeneral.WidthPercentage = 100

                Tabletitdescuentos.SetWidths(widthsEncGen2)


                Dim ColPiePag1d = New PdfPCell(New Phrase("AGRUPAMIENTO POR DESCUENTO", Font10N))
                ColPiePag1d.Border = 0
                ColPiePag1d.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)



                Tabletitdescuentos.AddCell(ColPiePag1d)



                Dim executeSQL As IDataReader


                executeSQL = ConsultaSql(sqldescuento).ExecuteReader
                Dim TableDescuentos As PdfPTable = New PdfPTable(4)
                TableDescuentos.DefaultCell.Border = BorderStyle.None
                TableDescuentos.WidthPercentage = 100
                Dim widthsDesc As Single() = New Single() {250.0F, 250.0F, 250.0F, 250.0F}
                TableDescuentos.SetWidths(widthsDesc)

                Do While executeSQL.Read

                    Dim cuantos As Decimal = Decimal.Parse(executeSQL("cuantos"))
                    Dim virtu As Decimal = Decimal.Parse(executeSQL("virtuales"))
                    Dim descu As String = executeSQL("descuento").ToString()

                    If descu = "5" Or descu = "10" Or descu = "50" Or descu = "100" Then ' A petición de la directora Yoseline, solo mostrar descuentos del 5, 10, 50 y 100% 14/09/2022


                        If virtu > 0 Then

                            Dim ColDesc = New PdfPCell(New Phrase(cuantos & " descuentos del: ", Font8))
                            ColDesc.Border = 0
                            ColDesc.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                            TableDescuentos.AddCell(ColDesc)

                            '  TotaCDesc = SumaTotal + SumaDescuento

                            ColDesc = New PdfPCell(New Phrase(descu & "%", Font8))
                            ColDesc.Border = 0
                            ColDesc.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                            TableDescuentos.AddCell(ColDesc)

                            ColDesc = New PdfPCell(New Phrase("Total : ", Font8))
                            ColDesc.Border = 0
                            ColDesc.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                            TableDescuentos.AddCell(ColDesc)

                            ' TotaCDesc = TotaCDesc - SumaDescuento
                            ColDesc = New PdfPCell(New Phrase(virtu.ToString("C"), Font8))
                            ColDesc.Border = 0
                            ColDesc.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                            TableDescuentos.AddCell(ColDesc)
                        End If

                    End If

                Loop


                Dim TabletitFROMAPAGO As PdfPTable = New PdfPTable(1)
                TabletitFROMAPAGO.DefaultCell.Border = BorderStyle.None
                TabletitFROMAPAGO.WidthPercentage = 100

                TabletitFROMAPAGO.SetWidths(widthsEncGen2)


                Dim ColPiePag1FP = New PdfPCell(New Phrase("AGRUPAMIENTO POR FORMA DE PAGO", Font10N))
                ColPiePag1FP.Border = 0
                ColPiePag1FP.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)



                TabletitFROMAPAGO.AddCell(ColPiePag1FP)






                executeSQL = ConsultaSql(sqlformapago).ExecuteReader

                Dim Tableformapago As PdfPTable = New PdfPTable(4)
                Tableformapago.DefaultCell.Border = BorderStyle.None
                Tableformapago.WidthPercentage = 100
                Dim widthsfp As Single() = New Single() {250.0F, 250.0F, 250.0F, 250.0F}
                Tableformapago.SetWidths(widthsDesc)

                Do While executeSQL.Read

                    Dim cuantos As Decimal = Decimal.Parse(executeSQL("cuantos"))
                    Dim ccodpago As String = executeSQL("ccodpago")
                    Dim descu As Decimal = executeSQL("Total").ToString()



                    Dim fp As String = obtenerCampo("select * from fpago where ccodpago='" & ccodpago & "'", "CDESPAGO")

                    Dim ColDesc = New PdfPCell(New Phrase(cuantos & " pagos de " & fp & " ", Font8))
                    ColDesc.Border = 0
                    ColDesc.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    Tableformapago.AddCell(ColDesc)

                    '   TotaCDesc = SumaTotal + SumaDescuento

                    ColDesc = New PdfPCell(New Phrase(descu, Font8))
                    ColDesc.Border = 0
                    ColDesc.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    Tableformapago.AddCell(ColDesc)

                    ColDesc = New PdfPCell(New Phrase("Total : ", Font8))
                    ColDesc.Border = 0
                    ColDesc.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    Tableformapago.AddCell(ColDesc)

                    '  TotaCDesc = TotaCDesc - SumaDescuento
                    ColDesc = New PdfPCell(New Phrase(descu.ToString("C"), Font8))
                    ColDesc.Border = 0
                    ColDesc.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    Tableformapago.AddCell(ColDesc)




                Loop


                pdfDoc.Add(TableEncGeneral)
                pdfDoc.Add(TableVacio)
                pdfDoc.Add(TableVacio)
                pdfDoc.Add(TableVacio)
                pdfDoc.Add(TableEncFechas)
                pdfDoc.Add(TableRecibos)
                '    pdfDoc.Add(TableTotales)
                pdfDoc.Add(TableVacio)
                pdfDoc.Add(TableVacio)
                pdfDoc.Add(TableVacio)

                pdfDoc.Add(TableVacio)
                pdfDoc.Add(TableVacio)
                pdfDoc.Add(TableVacio)

                pdfDoc.Add(TableVacio)
                pdfDoc.Add(Tabletitdescuentos)
                pdfDoc.Add(TableVacio)
                pdfDoc.Add(TableDescuentos)

                pdfDoc.Add(TableVacio)
                pdfDoc.Add(TableVacio)
                pdfDoc.Add(TabletitFROMAPAGO)
                pdfDoc.Add(TableVacio)
                pdfDoc.Add(Tableformapago)

                pdfDoc.Close()

            Catch ex As Exception
                MessageBox.Show(ex.Message)

            End Try
            'Ejecucion("update empresa set foliofactura = " & foliofactura & "")

            Try
                    Dim psi As New ProcessStartInfo(cadenafolder & "\CorteCaja_Resumen" & filtro & ".pdf")
                    'psi.WorkingDirectory = cadenafolder & "\factura\" + nombresespacios

                    psi.WindowStyle = ProcessWindowStyle.Hidden
                    Dim p As Process = Process.Start(psi)
                Catch ex As Exception
                    MessageBox.Show("Error al visualizar el pdf, posiblemente el archivo este en uso, cierrelo antes de generar un nuevo reporte" & ex.Message)
                End Try

            Catch ex As Exception
                MessageBox.Show("Error al generar el reporte: " & ex.Message)
            End Try
        End Sub
End Class

﻿Imports System.IO
Imports iTextSharp.text
    Imports iTextSharp.text.pdf

    Public Class RptCajaDesglozado

        Dim Factura As Int64
        Dim Cuenta As Int64
        Dim Nombre As String
        Dim Subtotal As Decimal
        Dim IVA As Decimal
        Dim Total As Decimal
        Dim Status As String
        Dim Virtual As Decimal
        Dim DescPorcentaje As Double
        Dim DescPorcentaje50 As Double
    Dim DescPorcentaje100 As Double
    Dim Abono As Decimal

    Dim executeSQL As IDataReader

        Dim SumaSubTotal As Decimal
        Dim SumaIVA As Decimal
        Dim SumaTotal As Decimal
    Dim SumaDescuento As Decimal
    Dim SumaVale As Decimal
    Dim Cont As Int64 = 0
        Dim ContCanc As Int64 = 0
        Dim Recibo As Int64


        Dim totalGeneral As Decimal
        Dim fechaActual As String

        Dim TotaCDesc As Decimal

    Public Sub CorteDiario(ByVal sql As String, ByVal filtro As String, ByVal Caja As String, sqldescuento As String, sqlDescuentosRecargosP As String, sqlformapago As String, sqlmixto As String)

        Ejecucion("delete from tempcortecaja")

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
            Dim pdfWrite As PdfWriter = PdfWriter.GetInstance(pdfDoc, New System.IO.FileStream(cadenafolder & "\CorteCajadesglosado_" & filtro & ".pdf", FileMode.Create))

            'Formato de letras
            Dim Font8 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.NORMAL))
            Dim Font5 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 5, iTextSharp.text.Font.NORMAL))
            Dim Font7A As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLUE))
            Dim Font7 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 7, iTextSharp.text.Font.NORMAL))
            Dim Font4 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 4, iTextSharp.text.Font.NORMAL))
            Dim Font8N As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.BOLD))
            Dim Font13N As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 13, iTextSharp.text.Font.BOLD))
            Dim Font10N As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.BOLD))
            Dim Font12N As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 12, iTextSharp.text.Font.BOLD))
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

            Dim widthsRec As Single() = New Single() {50.0F, 50.0, 55.0F, 110.0F, 70.0F, 70.0F, 40.0F, 40.0F, 70.0F, 40.0F, 60.0F}
            Dim ColRecibo = New PdfPCell(New Phrase("", Font7White))


#End Region
            Dim TableVacio As PdfPTable = New PdfPTable(1)
            TableVacio.DefaultCell.Border = BorderStyle.None
            TableVacio.WidthPercentage = 100
            Dim widthsVacio As Single() = New Single() {1000.0F}
            TableVacio.SetWidths(widthsVacio)

            Dim widthsEncGen2 As Single() = New Single() {602.0F}
#Region "Encabezado"

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

                ColEncFec = New PdfPCell(New Phrase("FECHA DE EMISIÓN: " & fechaActual, Font8))
                ColEncFec.Border = 0
                ColEncFec.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableEncFechas.AddCell(ColEncFec)





#End Region




#Region "Tabla Recibos"


                Dim TableRecibosencabezado As PdfPTable = New PdfPTable(11)
                TableRecibosencabezado.DefaultCell.Border = BorderStyle.None

                TableRecibosencabezado.WidthPercentage = 100

                TableRecibosencabezado.SetWidths(widthsRec)

                ColRecibo = New PdfPCell(New Phrase("FACTURA", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibosencabezado.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase("RECIBO ", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibosencabezado.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase("CUENTA", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibosencabezado.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase("NOMBRE", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibosencabezado.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase("SUBTOTAL", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibosencabezado.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase("IVA", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibosencabezado.AddCell(ColRecibo)
                ColRecibo = New PdfPCell(New Phrase("ABONO", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibosencabezado.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase("ABONO APLICADO", Font5White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibosencabezado.AddCell(ColRecibo)


                ColRecibo = New PdfPCell(New Phrase("TOTAL", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibosencabezado.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase("STATUS", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibosencabezado.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase("VIRTUAL", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibosencabezado.AddCell(ColRecibo)




                pdfDoc.Add(TableEncGeneral)
                pdfDoc.Add(TableVacio)
                pdfDoc.Add(TableVacio)
                pdfDoc.Add(TableVacio)
                pdfDoc.Add(TableEncFechas)
                pdfDoc.Add(TableRecibosencabezado)
            Catch ex As Exception
                MessageBox.Show("EN:" & ex.Message)
            End Try


            'CONSULTA

            executeSQL = ConsultaSql(sql).ExecuteReader



            Dim sumavaleentregado As Decimal = 0

            Dim sumavaleaplicado As Decimal = 0



            While executeSQL.Read()

                Dim TableRecibos As PdfPTable = New PdfPTable(11)
                TableRecibos.DefaultCell.Border = BorderStyle.None
                TableRecibos.WidthPercentage = 100

                Try
                    TableRecibos.SetWidths(widthsRec)
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try


                Factura = executeSQL("facturado")
                Recibo = executeSQL("recibo")
                Cuenta = executeSQL("cuenta")
                Nombre = executeSQL("Nombre")
                Subtotal = executeSQL("pagos")
                IVA = executeSQL("IVA")
                Total = executeSQL("total")
                Status = executeSQL("Cancelado")
                Virtual = 0
                DescPorcentaje = executeSQL("descuento")
                Abono = executeSQL("Vale")


                ColRecibo = New PdfPCell(New Phrase(Factura, Font7))
                ColRecibo.Border = 1
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase(executeSQL("serie") & Recibo, Font7))
                ColRecibo.Border = 1
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase(Cuenta, Font7))
                ColRecibo.Border = 1
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase(Nombre, Font7))
                ColRecibo.Border = 1
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase(Subtotal.ToString("C"), Font7))
                ColRecibo.Border = 1
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase(IVA.ToString("C"), Font7))
                ColRecibo.Border = 1
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                If Abono > 0 Then
                    ColRecibo = New PdfPCell(New Phrase(Abono.ToString("C"), Font7))
                    ColRecibo.Border = 1
                    ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    TableRecibos.AddCell(ColRecibo)
                    If Status = "A" Then
                        sumavaleentregado += Abono
                    End If
                    ColRecibo = New PdfPCell(New Phrase(0.ToString("C"), Font7))
                    ColRecibo.Border = 1
                    ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    TableRecibos.AddCell(ColRecibo)

                End If

                If Abono < 0 Then
                    ColRecibo = New PdfPCell(New Phrase(0.ToString("C"), Font7))
                    ColRecibo.Border = 1
                    ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    TableRecibos.AddCell(ColRecibo)

                    ColRecibo = New PdfPCell(New Phrase(Abono.ToString("C"), Font7))
                    ColRecibo.Border = 1
                    ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    TableRecibos.AddCell(ColRecibo)
                    If Status = "A" Then
                        sumavaleaplicado += Abono
                    End If

                End If

                If Abono = 0 Then
                    ColRecibo = New PdfPCell(New Phrase(0.ToString("C"), Font7))
                    ColRecibo.Border = 1
                    ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    TableRecibos.AddCell(ColRecibo)
                    TableRecibos.AddCell(ColRecibo)

                End If



                ColRecibo = New PdfPCell(New Phrase(Total.ToString("C"), Font7))
                ColRecibo.Border = 1
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase(Status, Font7))
                ColRecibo.Border = 1
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                If Status = "A" Then
                    Dim descuentosdelrecibovane = ConsultaSql("select * From  descuentospagos where serie='" & executeSQL("Serie") & "' and recibo=" & Recibo).ExecuteReader
                    Virtual = 0

                    Do While descuentosdelrecibovane.Read
                        Virtual += Decimal.Parse(descuentosdelrecibovane("monto"))
                        Ejecucion("insert into tempcortecaja (porcentaje,concepto,monto,recibo) values (" & descuentosdelrecibovane("porcentaje") & ",'" & descuentosdelrecibovane("concepto") & "'," & descuentosdelrecibovane("monto") & "," & descuentosdelrecibovane("recibo") & ")")

                    Loop


                    ColRecibo = New PdfPCell(New Phrase(Virtual.ToString("C"), Font7))
                    ColRecibo.Border = 1
                    ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                    'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    TableRecibos.AddCell(ColRecibo)





                Else
                    ColRecibo = New PdfPCell(New Phrase("$0.00", Font7))
                    ColRecibo.Border = 1
                    ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                    'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    TableRecibos.AddCell(ColRecibo)

                End If

                If Status = "A" Then
                    SumaSubTotal += Subtotal
                    SumaIVA += IVA
                    SumaTotal += Total
                    SumaVale += Abono
                End If




                Cont = Cont + 1

                If Status = "C" Then
                    ContCanc = ContCanc + 1
                End If

                pdfDoc.Add(TableRecibos)


                Try
                    Dim Tabledesgloze As PdfPTable = New PdfPTable(5)
                    Tabledesgloze.DefaultCell.Border = BorderStyle.None
                    Tabledesgloze.WidthPercentage = 80
                    Dim widthdesglozado As Single() = New Single() {60.0F, 200.0, 85.0F, 85.0F, 50.0F}
                    Tabledesgloze.SetWidths(widthdesglozado)

                    Dim datosrecibo = ConsultaSql("select * from pagotros where serie='" & executeSQL("serie") & "' and recibo=" & Recibo).ExecuteReader


                    Dim colcantidad = New PdfPCell(New Phrase("CANT,", Font7A))
                    colcantidad.Border = 0
                    colcantidad.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                    'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    Tabledesgloze.AddCell(colcantidad)

                    Dim colconcepto = New PdfPCell(New Phrase("CONCEPTO", Font7A))
                    colconcepto.Border = 0
                    colconcepto.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    Tabledesgloze.AddCell(colconcepto)

                    Dim colpc = New PdfPCell(New Phrase("P.U.", Font7A))
                    colpc.Border = 0
                    colpc.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    Tabledesgloze.AddCell(colpc)

                    Dim colimporte = New PdfPCell(New Phrase("IMPORTE", Font7A))
                    colimporte.Border = 0
                    colimporte.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    Tabledesgloze.AddCell(colimporte)

                    Dim coliva = New PdfPCell(New Phrase("IVA", Font7A))
                    coliva.Border = 0
                    coliva.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    Tabledesgloze.AddCell(coliva)


                    While datosrecibo.Read
                        colcantidad = New PdfPCell(New Phrase(datosrecibo("cantidad"), Font7A))
                        colcantidad.Border = 0
                        colcantidad.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                        'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                        Tabledesgloze.AddCell(colcantidad)

                        colconcepto = New PdfPCell(New Phrase(datosrecibo("concepto"), Font7A))
                        colconcepto.Border = 0
                        colconcepto.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                        Tabledesgloze.AddCell(colconcepto)

                        colpc = New PdfPCell(New Phrase(datosrecibo("Monto"), Font7A))
                        colpc.Border = 0
                        colpc.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                        Tabledesgloze.AddCell(colpc)

                        colimporte = New PdfPCell(New Phrase(datosrecibo("Importe"), Font7A))
                        colimporte.Border = 0
                        colimporte.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                        Tabledesgloze.AddCell(colimporte)

                        coliva = New PdfPCell(New Phrase(datosrecibo("MontoIVa"), Font7A))
                        coliva.Border = 0
                        coliva.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                        Tabledesgloze.AddCell(coliva)


                    End While
                    Tabledesgloze.CompleteRow()
                    pdfDoc.Add(Tabledesgloze)
                Catch ex As Exception
                    MessageBox.Show("IR: " & ex.Message)
                End Try




            End While



            Dim TableTotales As PdfPTable = New PdfPTable(11)
            TableTotales.DefaultCell.Border = BorderStyle.None
            TableTotales.WidthPercentage = 100
            Dim widthsRecs2 As Single() = New Single() {70.0F, 65.0, 55.0F, 100.0F, 70.0F, 50.0F, 50.0F, 50.0F, 80.0F, 30.0F, 70.0F}
            TableTotales.SetWidths(widthsRecs2)

            Dim ColTotal = New PdfPCell(New Phrase("", Font12N))
            ColTotal.Border = 1
            ColTotal.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableTotales.AddCell(ColTotal)

            ColTotal = New PdfPCell(New Phrase("", Font12N))
            ColTotal.Border = 1
            ColTotal.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableTotales.AddCell(ColTotal)

            ColTotal = New PdfPCell(New Phrase("", Font12N))
            ColTotal.Border = 1
            ColTotal.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableTotales.AddCell(ColTotal)

            ColTotal = New PdfPCell(New Phrase("TOTAL GENERAL ", Font10N))
            ColTotal.Border = 1
            ColTotal.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableTotales.AddCell(ColTotal)

            ColTotal = New PdfPCell(New Phrase(SumaSubTotal.ToString("C"), Font10N))
            ColTotal.Border = 1
            ColTotal.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableTotales.AddCell(ColTotal)

            ColTotal = New PdfPCell(New Phrase(SumaIVA.ToString("C"), Font10N))
            ColTotal.Border = 1
            ColTotal.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableTotales.AddCell(ColTotal)

            ColTotal = New PdfPCell(New Phrase(sumavaleentregado.ToString("C"), Font8N))
            ColTotal.Border = 1
            ColTotal.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableTotales.AddCell(ColTotal)

            ColTotal = New PdfPCell(New Phrase(sumavaleaplicado.ToString("C"), Font8N))
            ColTotal.Border = 1
            ColTotal.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableTotales.AddCell(ColTotal)


            ColTotal = New PdfPCell(New Phrase(SumaTotal.ToString("C"), Font10N))
            ColTotal.Border = 1
            ColTotal.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableTotales.AddCell(ColTotal)

            ColTotal = New PdfPCell(New Phrase("", Font12N))
            ColTotal.Border = 1
            ColTotal.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableTotales.AddCell(ColTotal)

            ColTotal = New PdfPCell(New Phrase(SumaDescuento.ToString("C"), Font10N))
            ColTotal.Border = 1
            ColTotal.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableTotales.AddCell(ColTotal)


            totalGeneral = SumaTotal - SumaDescuento


            Dim TableRecibos2 As PdfPTable = New PdfPTable(11)
            TableRecibos2.DefaultCell.Border = BorderStyle.None
            TableRecibos2.WidthPercentage = 100

            Try
                TableRecibos2.SetWidths(widthsRec)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try



            ColRecibo = New PdfPCell(New Phrase("", Font7))
            ColRecibo.Border = 1
            ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableRecibos2.AddCell(ColRecibo)

            ColRecibo = New PdfPCell(New Phrase("", Font7))
            ColRecibo.Border = 1
            ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableRecibos2.AddCell(ColRecibo)

            ColRecibo = New PdfPCell(New Phrase("", Font7))
            ColRecibo.Border = 1
            ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableRecibos2.AddCell(ColRecibo)

            ColRecibo = New PdfPCell(New Phrase(SumaSubTotal.ToString("C"), Font7))
            ColRecibo.Border = 1
            ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableRecibos2.AddCell(ColRecibo)

            ColRecibo = New PdfPCell(New Phrase(SumaIVA.ToString("C"), Font7))
            ColRecibo.Border = 1
            ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableRecibos2.AddCell(ColRecibo)

            ColRecibo = New PdfPCell(New Phrase(SumaTotal.ToString("C"), Font7))
            ColRecibo.Border = 1
            ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableRecibos2.AddCell(ColRecibo)

            ColRecibo = New PdfPCell(New Phrase("", Font7))
            ColRecibo.Border = 1
            ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableRecibos2.AddCell(ColRecibo)

            ColRecibo = New PdfPCell(New Phrase("", Font7))
            ColRecibo.Border = 1
            ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableRecibos2.AddCell(ColRecibo)

#End Region




#Region "Table Totales y firmas"

            Dim nombreDirectorCaja As String = obtenerCampo("select * from empresa where codemp='1'", "Admin_Cajas")

            Dim TableRecibosTotal As PdfPTable = New PdfPTable(4)
            TableRecibosTotal.DefaultCell.Border = BorderStyle.None
            TableRecibosTotal.WidthPercentage = 100
            Dim widthsTot As Single() = New Single() {70.0F, 70.0F, 70.0F, 70.0F}
            TableRecibosTotal.SetWidths(widthsTot)

            Dim ColTotxRec = New PdfPCell(New Phrase("Recibos Totales: ", Font8))
            ColTotxRec.Border = 0
            ColTotxRec.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableRecibosTotal.AddCell(ColTotxRec)

            ColTotxRec = New PdfPCell(New Phrase(Cont, Font8))
            ColTotxRec.Border = 0
            ColTotxRec.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableRecibosTotal.AddCell(ColTotxRec)

            ColTotxRec = New PdfPCell(New Phrase("Recibos Cancelados: ", Font8))
            ColTotxRec.Border = 0
            ColTotxRec.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableRecibosTotal.AddCell(ColTotxRec)

            ColTotxRec = New PdfPCell(New Phrase(ContCanc, Font8))
            ColTotxRec.Border = 0
            ColTotxRec.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableRecibosTotal.AddCell(ColTotxRec)



            Dim TableFirmas As PdfPTable = New PdfPTable(5)
            TableFirmas.DefaultCell.Border = BorderStyle.None
            TableFirmas.WidthPercentage = 100
            Dim widthsTotxRec As Single() = New Single() {70.0F, 400.0F, 20.0F, 400.0F, 70.0F}
            TableFirmas.SetWidths(widthsTotxRec)

            Dim ColFirm = New PdfPCell(New Phrase(" ", Font5White))
            ColFirm.Border = 0
            ColFirm.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableFirmas.AddCell(ColFirm)

            ColFirm = New PdfPCell(New Phrase(usuariodelsistema, Font8))
            ColFirm.Border = 1
            ColFirm.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableFirmas.AddCell(ColFirm)

            ColFirm = New PdfPCell(New Phrase(" ", Font5White))
            ColFirm.Border = 0
            ColFirm.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableFirmas.AddCell(ColFirm)

            ColFirm = New PdfPCell(New Phrase(nombreDirectorCaja, Font8))
            ColFirm.Border = 1
            ColFirm.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableFirmas.AddCell(ColFirm)

            ColFirm = New PdfPCell(New Phrase(" ", Font5White))
            ColFirm.Border = 0
            ColFirm.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableFirmas.AddCell(ColFirm)






            'Actopan
            '30,3,4,5


            Dim Tabletitdescuentos As PdfPTable = New PdfPTable(1)
            Tabletitdescuentos.DefaultCell.Border = BorderStyle.None
            Tabletitdescuentos.WidthPercentage = 100

            Tabletitdescuentos.SetWidths(widthsEncGen2)


            Dim ColPiePag1d = New PdfPCell(New Phrase("AGRUPAMIENTO POR DESCUENTO", Font10N))
            ColPiePag1d.Border = 0
            ColPiePag1d.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)



            Tabletitdescuentos.AddCell(ColPiePag1d)





            Dim TableDescuentos As PdfPTable = New PdfPTable(4)
            TableDescuentos.DefaultCell.Border = BorderStyle.None
            TableDescuentos.WidthPercentage = 100
            Dim widthsDesc As Single() = New Single() {250.0F, 250.0F, 250.0F, 250.0F}
            TableDescuentos.SetWidths(widthsDesc)

            Dim agrupadesc As IDataReader = ConsultaSql("select concepto, porcentaje, sum(monto) as monto, count(idtemocortecaja) as cuantos from tempcortecaja group by concepto,porcentaje ").ExecuteReader

            Do While agrupadesc.Read

                Dim cuantos As Decimal = Decimal.Parse(agrupadesc("cuantos"))
                Dim virtu As Decimal = Decimal.Parse(agrupadesc("monto").ToString())
                Dim concepto As String = agrupadesc("concepto").ToString()
                Dim porcentaje As String = agrupadesc("porcentaje").ToString()



                If virtu > 0 Then

                    Dim ColDesc = New PdfPCell(New Phrase(cuantos & " descuentos de:  " + concepto, Font8))
                    ColDesc.Border = 0
                    ColDesc.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    TableDescuentos.AddCell(ColDesc)

                    TotaCDesc = SumaTotal + SumaDescuento

                    ColDesc = New PdfPCell(New Phrase(porcentaje & "%", Font8))
                    ColDesc.Border = 0
                    ColDesc.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    TableDescuentos.AddCell(ColDesc)

                    ColDesc = New PdfPCell(New Phrase("Total : ", Font8))
                    ColDesc.Border = 0
                    ColDesc.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    TableDescuentos.AddCell(ColDesc)

                    TotaCDesc = TotaCDesc - SumaDescuento
                    ColDesc = New PdfPCell(New Phrase(virtu.ToString("C"), Font8))
                    ColDesc.Border = 0
                    ColDesc.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    TableDescuentos.AddCell(ColDesc)
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






            Dim Tableformapago As PdfPTable = New PdfPTable(4)
            Tableformapago.DefaultCell.Border = BorderStyle.None
            Tableformapago.WidthPercentage = 100
            Dim widthsfp As Single() = New Single() {250.0F, 250.0F, 250.0F, 250.0F}
            Tableformapago.SetWidths(widthsDesc)


            Dim tabla As DataTable = agrupa(sqlformapago, sqlmixto)

            For Each filaMixto As DataRow In tabla.Rows


                Dim cuantos As Integer = filaMixto.Field(Of Integer)("Conteo")
                Dim ccodpago As String = filaMixto.Field(Of String)("Descripcion")
                Dim descu As Decimal = filaMixto.Field(Of Decimal)("Monto")




                Dim fp As String = obtenerCampo("select * from fpago where ccodpago='" & ccodpago & "'", "CDESPAGO")

                Dim ColDesc = New PdfPCell(New Phrase(cuantos & " pagos de " & fp & " ", Font8))
                ColDesc.Border = 0
                ColDesc.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                Tableformapago.AddCell(ColDesc)

                TotaCDesc = SumaTotal + SumaDescuento

                ColDesc = New PdfPCell(New Phrase("", Font8))
                ColDesc.Border = 0
                ColDesc.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                Tableformapago.AddCell(ColDesc)

                ColDesc = New PdfPCell(New Phrase("Total : ", Font8))
                ColDesc.Border = 0
                ColDesc.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                Tableformapago.AddCell(ColDesc)

                TotaCDesc = TotaCDesc - SumaDescuento
                ColDesc = New PdfPCell(New Phrase(descu.ToString("C"), Font8))
                ColDesc.Border = 0
                ColDesc.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                Tableformapago.AddCell(ColDesc)




            Next






#End Region



            pdfDoc.Add(TableTotales)
            pdfDoc.Add(TableVacio)
            pdfDoc.Add(TableVacio)
            pdfDoc.Add(TableVacio)
            pdfDoc.Add(TableRecibosTotal)
            pdfDoc.Add(TableVacio)
            pdfDoc.Add(TableVacio)
            pdfDoc.Add(TableVacio)
            pdfDoc.Add(TableVacio)
            pdfDoc.Add(TableVacio)
            pdfDoc.Add(TableVacio)
            pdfDoc.Add(TableVacio)
            pdfDoc.Add(TableFirmas)
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
            'pdfDoc.Add(TableDescuentosRecargos)

            pdfDoc.Close()

            'Ejecucion("update empresa set foliofactura = " & foliofactura & "")

            Try
                Dim psi As New ProcessStartInfo(cadenafolder & "\CorteCajadesglosado_" & filtro & ".pdf")
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


    Public Function agrupa(sqlformapago As String, sqlmixto As String) As DataTable
        Dim miDataTable As New DataTable("Tablepagos")

        ' Definir las columnas de la DataTable
        miDataTable.Columns.Add("Conteo", GetType(Integer))
        miDataTable.Columns.Add("Descripcion", GetType(String))
        miDataTable.Columns.Add("Monto", GetType(Decimal))



        executeSQL = ConsultaSql(sqlformapago).ExecuteReader

        Do While executeSQL.Read
            Dim cuantos As Integer = Integer.Parse(executeSQL("cuantos"))
            Dim ccodpago As String = executeSQL("ccodpago")
            Dim descu As Decimal = Decimal.Parse(executeSQL("Total").ToString())

            miDataTable.Rows.Add(cuantos, ccodpago, descu)
        Loop

        Dim executeSQLmixto As IDataReader = ConsultaSql(sqlmixto).ExecuteReader

        Dim miDataTablemixto As New DataTable("Mixto")

        ' Definir las columnas de la DataTable
        miDataTablemixto.Columns.Add("FormaOri", GetType(String))
        miDataTablemixto.Columns.Add("Formareal", GetType(String))
        miDataTablemixto.Columns.Add("Monto", GetType(Decimal))

        Do While executeSQLmixto.Read
            Dim formaori As String = executeSQLmixto("formaori").ToString()
            Dim formareal As String = executeSQLmixto("formareal").ToString()
            Dim Monto As Decimal = Decimal.Parse(executeSQLmixto("Monto").ToString())

            miDataTablemixto.Rows.Add(formaori, formareal, Monto)
        Loop

        ' Restamos todo lo que tenga formaori=Descripcion

        For Each filaTablaPagos As DataRow In miDataTable.Rows
            Dim descripcion As String = filaTablaPagos.Field(Of String)("Descripcion")

            Dim filasFiltradas = From row In miDataTablemixto.AsEnumerable()
                                 Where row.Field(Of String)("FormaOri") = descripcion
                                 Select row

            Dim montoAcumuladoResta As Decimal = filasFiltradas.Sum(Function(row) row.Field(Of Decimal)("Monto"))
            Dim montoOriginal As Decimal = filaTablaPagos.Field(Of Decimal)("Monto")
            Dim nuevoMontoResta As Decimal = montoOriginal - montoAcumuladoResta

            ' Mostrar y actualizar el resultado de la resta
            Console.WriteLine($"Monto original: {montoOriginal}, Monto acumulado resta: {montoAcumuladoResta}, Nuevo monto: {nuevoMontoResta}")
            filaTablaPagos.SetField("Monto", nuevoMontoResta)

            ' Sumamos todo lo que tenga formaReal=Descripcion
            Dim filasFiltradas2 = From row In miDataTablemixto.AsEnumerable()
                                  Where row.Field(Of String)("Formareal") = descripcion
                                  Select row

            Dim montoAcumuladoSuma As Decimal = filasFiltradas2.Sum(Function(row) row.Field(Of Decimal)("Monto"))
            Dim nuevoMontoSuma As Decimal = nuevoMontoResta + montoAcumuladoSuma

            ' Mostrar y actualizar el resultado de la suma
            Console.WriteLine($"Monto acumulado suma: {montoAcumuladoSuma}, Nuevo monto suma: {nuevoMontoSuma}")
            filaTablaPagos.SetField("Monto", nuevoMontoSuma)

        Next

        ' Agregar las filas de miDataTablemixto que no coinciden con ninguna descripción en miDataTable
        For Each filaMixto As DataRow In miDataTablemixto.Rows
            Dim formaReal As String = filaMixto.Field(Of String)("Formareal")

            ' Verificar si la formaReal no coincide con ninguna Descripcion en miDataTable
            If Not miDataTable.AsEnumerable().Any(Function(row) row.Field(Of String)("Descripcion") = formaReal) Then
                ' Calcular el monto acumulado de las filas que tienen formaReal igual a la formaReal actual
                Dim filasFiltradas3 = From row In miDataTablemixto.AsEnumerable()
                                      Where row.Field(Of String)("Formareal") = formaReal
                                      Select row

                Dim montoAcumulado3 As Decimal = filasFiltradas3.Sum(Function(row) row.Field(Of Decimal)("Monto"))

                ' Agregar una nueva fila a miDataTablefinal
                miDataTable.Rows.Add(filasFiltradas3.Count, formaReal, montoAcumulado3)
            End If
        Next

        Return miDataTable


    End Function

End Class



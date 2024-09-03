Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class CorteCajaITSharp

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
    Dim Vale As Decimal

    Dim executeSQL As IDataReader

    Dim SumaSubTotal As Decimal
    Dim SumaIVA As Decimal
    Dim SumaTotal As Decimal
    Dim SumaVale As Decimal = 0
    Dim SumaDescuento As Decimal
    Dim Cont As Int64 = 0
    Dim ContCanc As Int64 = 0
    Dim Recibo As Int64


    Dim totalGeneral As Decimal
    Dim fechaActual As String

    Dim TotaCDesc As Decimal

    Public Sub CorteDiario(ByVal sql As String, ByVal filtro As String, ByVal Caja As String, sqldescuento As String, sqlDescuentosRecargosP As String, sqlformapago As String, sqlmixto As String)

        fechaActual = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper()

        Try

            'Crear el directorio en donde se van a almacenar los PDF
            If Not My.Computer.FileSystem.DirectoryExists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\ReporteCaja\" & Year(Now) & acompletacero(Month(Now).ToString(), 2).Trim) Then

                My.Computer.FileSystem.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\ReporteCaja\" & Year(Now) & acompletacero(Month(Now).ToString(), 2).Trim)
            End If

            Dim cadenafolder As String = (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\ReporteCaja\" & Year(Now) & acompletacero(Month(Now).ToString(), 2)).Trim

            'Dar propiedades al Documento
            Dim pdfDoc As New Document(iTextSharp.text.PageSize.LETTER, 15.0F, 15.0F, 5.0F, 10.0F)

            'Obtener la ruta donde se va a crear el pdf
            Dim pdfWrite As PdfWriter = PdfWriter.GetInstance(pdfDoc, New System.IO.FileStream(cadenafolder & "\CorteCaja_" & filtro & ".pdf", FileMode.Create))

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




#End Region

#Region "Encabezado"


            Dim TableVacio As PdfPTable = New PdfPTable(1)
            TableVacio.DefaultCell.Border = BorderStyle.None
            TableVacio.WidthPercentage = 100
            Dim widthsVacio As Single() = New Single() {1000.0F}
            TableVacio.SetWidths(widthsVacio)

            Dim ColVacio = New PdfPCell(New Phrase(" ", Font5White))
            ColVacio.Border = 0
            ColVacio.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            TableVacio.AddCell(ColVacio)


            Dim TableEncGeneral As PdfPTable = New PdfPTable(1)
            TableEncGeneral.DefaultCell.Border = BorderStyle.None
            TableEncGeneral.WidthPercentage = 100
            Dim widthsEncGen2 As Single() = New Single() {1000.0F}
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



            ColPiePag1 = New PdfPCell(New Phrase("REPORTE DE INGRESOS POR RECIBO", Font10N))
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


            Dim TableRecibos As PdfPTable = New PdfPTable(12)
            TableRecibos.DefaultCell.Border = BorderStyle.None
            TableRecibos.WidthPercentage = 100
            Dim widthsRec As Single() = New Single() {30.0F, 35.0, 40.0F, 45.0F, 135.0F, 70.0F, 70.0F, 50.0F, 50.0F, 80.0F, 30.0F, 60.0F}
            TableRecibos.SetWidths(widthsRec)

            Dim ColRecibo = New PdfPCell(New Phrase("FACT", Font7White))
            ColRecibo.Border = 0
            ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableRecibos.AddCell(ColRecibo)

            ColRecibo = New PdfPCell(New Phrase("RECIBO ", Font7White))
            ColRecibo.Border = 0
            ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableRecibos.AddCell(ColRecibo)

            ColRecibo = New PdfPCell(New Phrase("CUENTA", Font7White))
            ColRecibo.Border = 0
            ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableRecibos.AddCell(ColRecibo)
            ColRecibo = New PdfPCell(New Phrase("UBICA", Font7White))
            ColRecibo.Border = 0
            ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableRecibos.AddCell(ColRecibo)

            ColRecibo = New PdfPCell(New Phrase("NOMBRE", Font7White))
            ColRecibo.Border = 0
            ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableRecibos.AddCell(ColRecibo)

            ColRecibo = New PdfPCell(New Phrase("SUBTOTAL", Font7White))
            ColRecibo.Border = 0
            ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableRecibos.AddCell(ColRecibo)

            ColRecibo = New PdfPCell(New Phrase("IVA", Font7White))
            ColRecibo.Border = 0
            ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableRecibos.AddCell(ColRecibo)

            ColRecibo = New PdfPCell(New Phrase("ABONO ENTREGA", Font5White))
            ColRecibo.Border = 0
            ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableRecibos.AddCell(ColRecibo)

            ColRecibo = New PdfPCell(New Phrase("ABONO APLICACION", Font5White))
            ColRecibo.Border = 0
            ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableRecibos.AddCell(ColRecibo)

            ColRecibo = New PdfPCell(New Phrase("TOTAL", Font7White))
            ColRecibo.Border = 0
            ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableRecibos.AddCell(ColRecibo)

            ColRecibo = New PdfPCell(New Phrase("STATUS", Font7White))
            ColRecibo.Border = 0
            ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableRecibos.AddCell(ColRecibo)

            ColRecibo = New PdfPCell(New Phrase("VIRTUAL", Font7White))
            ColRecibo.Border = 0
            ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableRecibos.AddCell(ColRecibo)



            'CONSULTA

            executeSQL = ConsultaSql(sql).ExecuteReader

            Dim sumavaleentregado As Decimal = 0

            Dim sumavaleaplicado As Decimal = 0

            While executeSQL.Read()

                Factura = executeSQL("facturado")
                Recibo = executeSQL("recibo")
                Cuenta = executeSQL("cuenta")
                Nombre = executeSQL("Nombre")
                Subtotal = executeSQL("pagos")

                IVA = executeSQL("IVA")
                Vale = executeSQL("Vale")
                Total = executeSQL("total")
                Status = executeSQL("Cancelado")
                Virtual = executeSQL("DescuentoPesos")
                DescPorcentaje = executeSQL("descuento")



                ColRecibo = New PdfPCell(New Phrase(Factura, Font7))
                ColRecibo.Border = 1
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase(Recibo, Font7))
                ColRecibo.Border = 1
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                Dim ubi As String = obtenerCampo("select ubicacion from usuario where cuenta=" & Cuenta, "ubicacion")


                ColRecibo = New PdfPCell(New Phrase(Cuenta, Font7))
                ColRecibo.Border = 1
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase(ubi, Font8N))
                ColRecibo.Border = 1

                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
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


                If Vale > 0 Then
                    ColRecibo = New PdfPCell(New Phrase(Vale.ToString("C"), Font7))
                    ColRecibo.Border = 1
                    ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    TableRecibos.AddCell(ColRecibo)
                    If Status = "A" Then
                        sumavaleentregado += Vale
                    End If
                    ColRecibo = New PdfPCell(New Phrase(0.ToString("C"), Font7))
                    ColRecibo.Border = 1
                    ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    TableRecibos.AddCell(ColRecibo)

                End If

                If Vale < 0 Then
                    ColRecibo = New PdfPCell(New Phrase(0.ToString("C"), Font7))
                    ColRecibo.Border = 1
                    ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    TableRecibos.AddCell(ColRecibo)

                    ColRecibo = New PdfPCell(New Phrase(Vale.ToString("C"), Font7))
                    ColRecibo.Border = 1
                    ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    TableRecibos.AddCell(ColRecibo)
                    If Status = "A" Then
                        sumavaleaplicado += Vale
                    End If

                End If

                If Vale = 0 Then
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
                    ColRecibo = New PdfPCell(New Phrase(Virtual.ToString("C"), Font7))
                    ColRecibo.Border = 1
                    ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                    'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    TableRecibos.AddCell(ColRecibo)



                    SumaDescuento += Virtual


                    If DescPorcentaje = 50 Then
                        DescPorcentaje50 += Virtual
                    End If

                    If DescPorcentaje = 100 Then
                        DescPorcentaje100 += Virtual
                    End If

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

                End If




                Cont = Cont + 1

                If Status = "C" Then
                    ContCanc = ContCanc + 1
                End If

            End While


            Dim TableTotales As PdfPTable = New PdfPTable(11)
            TableTotales.DefaultCell.Border = BorderStyle.None
            TableTotales.WidthPercentage = 100
            Dim widthsRecs As Single() = New Single() {70.0F, 65.0, 55.0F, 110.0F, 70.0F, 70.0F, 50.0F, 50.0F, 80.0F, 30.0F, 60.0F}
            TableTotales.SetWidths(widthsRecs)

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

            ColTotal = New PdfPCell(New Phrase("TOTAL GENERAL ", Font12N))
            ColTotal.Border = 1
            ColTotal.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableTotales.AddCell(ColTotal)

            ColTotal = New PdfPCell(New Phrase(SumaSubTotal.ToString("C"), Font12N))
            ColTotal.Border = 1
            ColTotal.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableTotales.AddCell(ColTotal)





            ColTotal = New PdfPCell(New Phrase(SumaIVA.ToString("C"), Font12N))
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

            ColTotal = New PdfPCell(New Phrase(SumaTotal.ToString("C"), Font12N))
            ColTotal.Border = 1
            ColTotal.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableTotales.AddCell(ColTotal)

            ColTotal = New PdfPCell(New Phrase("", Font12N))
            ColTotal.Border = 1
            ColTotal.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableTotales.AddCell(ColTotal)

            ColTotal = New PdfPCell(New Phrase(SumaDescuento.ToString("C"), Font12N))
            ColTotal.Border = 1
            ColTotal.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableTotales.AddCell(ColTotal)


            totalGeneral = SumaTotal - SumaDescuento


            ColRecibo = New PdfPCell(New Phrase("", Font7))
            ColRecibo.Border = 1
            ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableRecibos.AddCell(ColRecibo)

            ColRecibo = New PdfPCell(New Phrase("", Font7))
            ColRecibo.Border = 1
            ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableRecibos.AddCell(ColRecibo)

            ColRecibo = New PdfPCell(New Phrase("", Font7))
            ColRecibo.Border = 1
            ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableRecibos.AddCell(ColRecibo)

            ColRecibo = New PdfPCell(New Phrase(SumaSubTotal.ToString("C"), Font7))
            ColRecibo.Border = 1
            ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableRecibos.AddCell(ColRecibo)

            ColRecibo = New PdfPCell(New Phrase(SumaIVA.ToString("C"), Font7))
            ColRecibo.Border = 1
            ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableRecibos.AddCell(ColRecibo)

            ColRecibo = New PdfPCell(New Phrase(SumaTotal.ToString("C"), Font7))
            ColRecibo.Border = 1
            ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableRecibos.AddCell(ColRecibo)

            ColRecibo = New PdfPCell(New Phrase("", Font7))
            ColRecibo.Border = 1
            ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableRecibos.AddCell(ColRecibo)

            ColRecibo = New PdfPCell(New Phrase("", Font7))
            ColRecibo.Border = 1
            ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableRecibos.AddCell(ColRecibo)

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
            TableEncGeneral.WidthPercentage = 100

            Tabletitdescuentos.SetWidths(widthsEncGen2)


            Dim ColPiePag1d = New PdfPCell(New Phrase("AGRUPAMIENTO POR DESCUENTO", Font10N))
            ColPiePag1d.Border = 0
            ColPiePag1d.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)



            Tabletitdescuentos.AddCell(ColPiePag1d)






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

                        TotaCDesc = SumaTotal + SumaDescuento

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

                        TotaCDesc = TotaCDesc - SumaDescuento
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


            pdfDoc.Add(TableEncGeneral)
            pdfDoc.Add(TableVacio)
            pdfDoc.Add(TableVacio)
            pdfDoc.Add(TableVacio)
            pdfDoc.Add(TableEncFechas)
            pdfDoc.Add(TableRecibos)
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
                Dim psi As New ProcessStartInfo(cadenafolder & "\CorteCaja_" & filtro & ".pdf")
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

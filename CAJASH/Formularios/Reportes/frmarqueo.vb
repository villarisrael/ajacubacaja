Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class Frmarqueo


    Private Sub Frmarqueo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtfecha.Text = Now.ToLongDateString
    End Sub

    Public Sub modifica(ByVal control As DevComponents.Editors.IntegerInput, ByVal Monto As Double, ByVal controltotal As DevComponents.Editors.DoubleInput)
        controltotal.Value = control.Value * Monto
    End Sub

    Public Sub modificatodos()
        modifica(bill1000, 1000, totb1000)
        modifica(bill500, 500, totb500)
        modifica(bill200, 200, totb200)
        modifica(bill100, 100, totb100)
        modifica(bill50, 50, totb50)
        modifica(bill20, 20, totb20)
        modifica(coin20, 20, totcoin20)
        modifica(coin10, 10, totcoin10)
        modifica(coin5, 5, totcoin5)
        modifica(coin2, 2, totcoin2)
        modifica(coin1, 1, totcoin1)
        modifica(coin50c, 0.5, totcoin50c)
        modifica(coin20c, 0.2, totcoin20c)

        suma()
    End Sub

    Public Sub suma()
        Dim acumula As Double = 0
        acumula = acumula + totb1000.Value
        acumula = acumula + totb500.Value
        acumula = acumula + totb200.Value
        acumula = acumula + totb100.Value
        acumula = acumula + totb50.Value
        acumula = acumula + totb20.Value
        acumula = acumula + totcoin20.Value
        acumula = acumula + totcoin10.Value
        acumula = acumula + totcoin5.Value
        acumula = acumula + totcoin2.Value
        acumula = acumula + totcoin1.Value
        acumula = acumula + totcoin50c.Value
        acumula = acumula + totcoin20c.Value

        dbtotal.Value = acumula - DbRetiros.Value + ditotaldocumentos.Value
    End Sub

    Private Sub bill1000_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bill1000.ValueChanged
        modificatodos()
    End Sub

    Private Sub bill500_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bill500.ValueChanged
        modificatodos()
    End Sub

    Private Sub bill200_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bill200.ValueChanged
        modificatodos()
    End Sub

    Private Sub bill100_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bill100.ValueChanged
        modificatodos()
    End Sub

    Private Sub bill50_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bill50.ValueChanged
        modificatodos()
    End Sub

    Private Sub bill20_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bill20.ValueChanged
        modificatodos()
    End Sub

    Private Sub coin20_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles coin20.ValueChanged
        modificatodos()
    End Sub

    Private Sub coin10_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles coin10.ValueChanged
        modificatodos()
    End Sub

    Private Sub coin5_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles coin5.ValueChanged
        modificatodos()
    End Sub

    Private Sub coin2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles coin2.ValueChanged
        modificatodos()
    End Sub

    Private Sub coin1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles coin1.ValueChanged
        modificatodos()
    End Sub

    Private Sub coin50c_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles coin50c.ValueChanged
        modificatodos()
    End Sub

    Private Sub coin20c_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles coin20c.ValueChanged
        modificatodos()
    End Sub



    Private Sub DbRetiros_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DbRetiros.ValueChanged
        modificatodos()
    End Sub

    Private Sub btnimprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimprimir.Click

        btnimprimir.Enabled = False
        CreaResumen()
        btnimprimir.Enabled = True


    End Sub

    Public Sub CreaResumen()

        Dim fechaActual As String = txtfecha.Text

        Try

            'Crear el directorio en donde se van a almacenar los PDF
            If Not My.Computer.FileSystem.DirectoryExists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\ReporteCaja\" & Year(Now) & acompletacero(Month(Now).ToString(), 2).Trim) Then

                My.Computer.FileSystem.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\ReporteCaja\" & Year(Now) & acompletacero(Month(Now).ToString(), 2).Trim)
            End If

            Dim cadenafolder As String = (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\ReporteCaja\" & Year(Now) & acompletacero(Month(Now).ToString(), 2)).Trim

            'Dar propiedades al Documento
            Dim pdfDoc As New Document(iTextSharp.text.PageSize.LETTER, 15.0F, 15.0F, 5.0F, 3.0F)

            'Obtener la ruta donde se va a crear el pdf
            Dim pdfWrite As PdfWriter = PdfWriter.GetInstance(pdfDoc, New System.IO.FileStream(cadenafolder & "\Arqueo" & fechaActual & ".pdf", FileMode.Create))

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



                ColPiePag1 = New PdfPCell(New Phrase("ARQUEO DE CAJA", Font10N))
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



                Dim ColEncFec = New PdfPCell(New Phrase(" CAJA: " & My.Settings.caja, Font8))
                ColEncFec.Border = 0
                ColEncFec.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableEncFechas.AddCell(ColEncFec)


                'fechaActual = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper()

                ColEncFec = New PdfPCell(New Phrase("FECHA DE EMISIÓN: " & fechaActual, Font8))
                ColEncFec.Border = 0
                ColEncFec.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableEncFechas.AddCell(ColEncFec)



                Dim TableRecibos As PdfPTable = New PdfPTable(3)
                TableRecibos.DefaultCell.Border = BorderStyle.None
                TableRecibos.WidthPercentage = 100
                Dim widthsRec2 As Single() = New Single() {400.0F, 70, 110.0F}
                TableRecibos.SetWidths(widthsRec2)

                ColRecibo = New PdfPCell(New Phrase("BILLETE /MONEDA", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase("CANTIDAD ", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase("MONTO", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)




                Dim sumaSubtotal As Decimal = 0
                Dim sumiva As Decimal = 0
                Dim sumTotal As Decimal = 0
                Dim sumDescuento As Decimal = 0










                ColRecibo = New PdfPCell(New Phrase("BILLETES DE $ 1,000.00 ", Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                '    ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)



                ColRecibo = New PdfPCell(New Phrase(bill1000.Value, Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ' ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                sumTotal += totb1000.Value

                ColRecibo = New PdfPCell(New Phrase(totb1000.Value.ToString("0.00"), Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                '  ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase("BILLETES DE $ 500.00 ", Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                '    ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)



                ColRecibo = New PdfPCell(New Phrase(bill500.Value, Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ' ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                sumTotal += totb500.Value

                ColRecibo = New PdfPCell(New Phrase(totb500.Value.ToString("0.00"), Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                '  ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)


                ColRecibo = New PdfPCell(New Phrase("BILLETES DE $ 200.00 ", Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                '    ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)



                ColRecibo = New PdfPCell(New Phrase(bill200.Value, Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ' ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                sumTotal += totb200.Value

                ColRecibo = New PdfPCell(New Phrase(totb200.Value.ToString("0.00"), Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                '  ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase("BILLETES DE $ 100.00 ", Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                '    ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)



                ColRecibo = New PdfPCell(New Phrase(bill100.Value, Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ' ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                sumTotal += totb100.Value

                ColRecibo = New PdfPCell(New Phrase(totb100.Value.ToString("0.00"), Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                '  ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase("BILLETES DE $ 50.00 ", Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                '    ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)



                ColRecibo = New PdfPCell(New Phrase(bill50.Value, Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ' ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                sumTotal += totb50.Value

                ColRecibo = New PdfPCell(New Phrase(totb50.Value.ToString("0.00"), Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                '  ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)



                ColRecibo = New PdfPCell(New Phrase("BILLETES DE $ 20.00 ", Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                '    ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)



                ColRecibo = New PdfPCell(New Phrase(bill20.Value, Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ' ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                sumTotal += totb20.Value

                ColRecibo = New PdfPCell(New Phrase(totb20.Value.ToString("0.00"), Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                '  ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)



                ColRecibo = New PdfPCell(New Phrase("  ", Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                '    ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)



                ColRecibo = New PdfPCell(New Phrase(" ", Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ' ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)



                ColRecibo = New PdfPCell(New Phrase("", Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                '  ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)



                ColRecibo = New PdfPCell(New Phrase("MONEDAS DE $ 20.00 ", Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                '    ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)



                ColRecibo = New PdfPCell(New Phrase(coin20.Value, Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ' ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                sumTotal += totcoin20.Value

                ColRecibo = New PdfPCell(New Phrase(totcoin20.Value.ToString("0.00"), Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                '  ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase("MONEDAS DE $ 10.00 ", Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                '    ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)



                ColRecibo = New PdfPCell(New Phrase(coin10.Value, Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ' ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                sumTotal += totcoin10.Value

                ColRecibo = New PdfPCell(New Phrase(totcoin10.Value.ToString("0.00"), Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                '  ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)


                ColRecibo = New PdfPCell(New Phrase("MONEDAS DE $ 5.00 ", Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                '    ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)



                ColRecibo = New PdfPCell(New Phrase(coin5.Value, Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ' ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                sumTotal += totcoin5.Value

                ColRecibo = New PdfPCell(New Phrase(totcoin5.Value.ToString("0.00"), Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                '  ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase("MONEDAS DE $ 2.00 ", Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                '    ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)



                ColRecibo = New PdfPCell(New Phrase(coin2.Value, Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ' ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                sumTotal += totcoin2.Value

                ColRecibo = New PdfPCell(New Phrase(totcoin2.Value.ToString("0.00"), Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                '  ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)


                ColRecibo = New PdfPCell(New Phrase("MONEDAS DE $ 1.00 ", Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                '    ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)



                ColRecibo = New PdfPCell(New Phrase(coin1.Value, Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ' ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                sumTotal += totcoin1.Value

                ColRecibo = New PdfPCell(New Phrase(totcoin1.Value.ToString("0.00"), Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                '  ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)


                ColRecibo = New PdfPCell(New Phrase("MONEDAS DE 50.0 C ", Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                '    ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)



                ColRecibo = New PdfPCell(New Phrase(coin50c.Value, Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ' ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                sumTotal += totcoin50c.Value

                ColRecibo = New PdfPCell(New Phrase(totcoin50c.Value.ToString("0.00"), Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                '  ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)





                ColRecibo = New PdfPCell(New Phrase("MONEDAS DE 20.0 C ", Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                ' ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase(coin20c.Value, Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ' ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                sumTotal += totcoin20c.Value

                ColRecibo = New PdfPCell(New Phrase(totcoin20c.Value.ToString("0.00"), Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                '  ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase("", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                '  ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase("", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ' ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)


                ColRecibo = New PdfPCell(New Phrase("", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase("TOTAL EN EFECTIVO", Font10N))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                ' ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase("", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                '  ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase(sumTotal.ToString("C"), Font10N))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                'ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)



                ColRecibo = New PdfPCell(New Phrase("RETIRO O VALES DE CAJA", Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                'ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase("", Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                '   ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)


                ColRecibo = New PdfPCell(New Phrase(DbRetiros.Value.ToString("0.00"), Font8))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                '  ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase("", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase("", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)


                ColRecibo = New PdfPCell(New Phrase("", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)


                ColRecibo = New PdfPCell(New Phrase("", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase("DOCUMENTOS", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)


                ColRecibo = New PdfPCell(New Phrase("", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)


                ColRecibo = New PdfPCell(New Phrase("", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)


                ColRecibo = New PdfPCell(New Phrase("", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)


                ColRecibo = New PdfPCell(New Phrase("", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)



                Dim totaldocumentos As Decimal = 0
                For i = 0 To Dtdocumentos.Rows.Count - 2

                    Dim CONTENIDO = Dtdocumentos.Rows(i).Cells.Item(0).Value.ToString() & " " & Dtdocumentos.Rows(i).Cells.Item(1).Value.ToString
                    Dim docomento = Decimal.Parse(Dtdocumentos.Rows(i).Cells.Item(3).Value)

                    totaldocumentos += docomento
                    ColRecibo = New PdfPCell(New Phrase(CONTENIDO, Font8))
                    ColRecibo.Border = 0
                    ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    ' ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    TableRecibos.AddCell(ColRecibo)


                    ColRecibo = New PdfPCell(New Phrase(Dtdocumentos.Rows(i).Cells.Item(2).Value.ToString(), Font8))
                    ColRecibo.Border = 0
                    ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    ' ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    TableRecibos.AddCell(ColRecibo)


                    ColRecibo = New PdfPCell(New Phrase(docomento.ToString("0.00"), Font8))
                    ColRecibo.Border = 0
                    ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    'ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    TableRecibos.AddCell(ColRecibo)

                Next


                ColRecibo = New PdfPCell(New Phrase("", Font10N))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                '   ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)


                ColRecibo = New PdfPCell(New Phrase("", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                '  ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)


                ColRecibo = New PdfPCell(New Phrase("", Font10N))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                ' ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)




                ColRecibo = New PdfPCell(New Phrase("TOTAL DOCUMENTOS", Font10N))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                ' ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase("", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                'ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                sumTotal -= DbRetiros.Value

                ColRecibo = New PdfPCell(New Phrase(ditotaldocumentos.Value.ToString("C"), Font10N))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ' ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)


                ColRecibo = New PdfPCell(New Phrase("", Font10N))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                '   ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)


                ColRecibo = New PdfPCell(New Phrase("", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                '  ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)


                ColRecibo = New PdfPCell(New Phrase("", Font10N))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                ' ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

                ColRecibo = New PdfPCell(New Phrase("GRAN TOTAL", Font10N))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                '   ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)


                ColRecibo = New PdfPCell(New Phrase("", Font7White))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                '  ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)


                ColRecibo = New PdfPCell(New Phrase(dbtotal.Value.ToString("C"), Font10N))
                ColRecibo.Border = 0
                ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                ' ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableRecibos.AddCell(ColRecibo)

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

                ColFirm = New PdfPCell(New Phrase("", Font8))
                ColFirm.Border = 1
                ColFirm.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableFirmas.AddCell(ColFirm)

                ColFirm = New PdfPCell(New Phrase(" ", Font5White))
                ColFirm.Border = 0
                ColFirm.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                TableFirmas.AddCell(ColFirm)



                pdfDoc.Add(TableEncGeneral)
                pdfDoc.Add(TableVacio)
                pdfDoc.Add(TableVacio)
                pdfDoc.Add(TableVacio)
                pdfDoc.Add(TableEncFechas)
                pdfDoc.Add(TableRecibos)

                pdfDoc.Add(TableVacio)
                pdfDoc.Add(TableVacio)
                pdfDoc.Add(TableVacio)
                pdfDoc.Add(TableFirmas)



                pdfDoc.Close()

            Catch ex As Exception
                MessageBox.Show(ex.Message)

            End Try
            'Ejecucion("update empresa set foliofactura = " & foliofactura & "")

            Try
                Dim psi As New ProcessStartInfo(cadenafolder & "\Arqueo" & fechaActual & ".pdf")
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


    Function sumarmonedas() As Double
        Return totcoin20.Value + totcoin10.Value + totcoin5.Value + totcoin2.Value + totcoin1.Value + totcoin50c.Value + totcoin20c.Value

    End Function

    Function sumarbilletes() As Double
        Return totb1000.Value + totb500.Value + totb200.Value + totb100.Value + totb50.Value + totb20.Value
    End Function



    Private Sub Dtdocumentos_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dtdocumentos.CellEndEdit
        If e.ColumnIndex = 3 Then
            sumar()
        End If
    End Sub


    Public Sub sumar()

        Dim acu As Double = 0
        Try
            Dim i As Int16 = 0
            For Each linea In Dtdocumentos.Rows
                acu = acu + Dtdocumentos.Rows(i).Cells(3).Value
                i = i + 1
            Next
        Catch ex As Exception

        End Try

        ditotaldocumentos.Value = acu
        suma()
    End Sub

    Private Sub Dtdocumentos_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dtdocumentos.RowsAdded
        sumar()
    End Sub

    Private Sub Dtdocumentos_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles Dtdocumentos.RowsRemoved
        sumar()
    End Sub

    Private Sub btnsalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsalir.Click
        Close()
    End Sub
End Class

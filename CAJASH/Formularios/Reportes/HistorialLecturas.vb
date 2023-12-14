Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class HistorialLecturas

    Dim RFCEmisor As String

    Public Sub GenerarReporteHistorialLecturas(ByVal FiltroSQLP As String, ByVal ContratoP As Long)

        Dim datosContrato As Odbc.OdbcDataReader
        datosContrato = ConsultaSql("select * from vusuario where cuenta= " & ContratoP & "").ExecuteReader

        Dim datosLecturas As Odbc.OdbcDataReader
        datosLecturas = ConsultaSql(FiltroSQLP).ExecuteReader

        RFCEmisor = obtenerCampo("select CNIF from empresa where CODEMP = 1", "CNIF")

        Dim directorioReporteLecturas = (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\HistorialLecturas\Reportes_" & Year(Now) & acompletacero(Month(Now).ToString(), 2)).Trim

        If Not Directory.Exists(directorioReporteLecturas) Then
            Directory.CreateDirectory(directorioReporteLecturas)
        End If


#Region "Propiedades del documento"



        'Dar propiedades al Documento
        Dim pdfDoc As New Document(iTextSharp.text.PageSize.LETTER, 15.0F, 15.0F, 30.0F, 30.0F)


        Dim pdfWrite As PdfWriter = PdfWriter.GetInstance(pdfDoc, New System.IO.FileStream(directorioReporteLecturas & "\HistorialLecturas_Contrato_" & ContratoP & ".pdf", FileMode.Create))


        'Formato Letras


        Dim Font As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 6, iTextSharp.text.Font.NORMAL))
        Dim Font8N As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 6, iTextSharp.text.Font.BOLD))
        Dim Font88 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 13, iTextSharp.text.Font.BOLD))
        Dim Font8 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.BOLD))
        Dim Font12 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.BOLD))
        Dim Font9 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 7, iTextSharp.text.Font.NORMAL))
        Dim Fontp As New Font(FontFactory.GetFont(FontFactory.COURIER, 7, iTextSharp.text.Font.BOLD))
        Dim CVacio As PdfPCell = New PdfPCell(New Phrase(""))
        CVacio.Border = 0

        'abrimos el pdf para comenzar a escribir en el, al terminar cerramos
        pdfDoc.Open()



        Dim colordefinido = New Clscolorreporte()
        colordefinido.ClsColoresReporte(My.Settings.colorfactura)

#End Region

#Region "Tabla Vacia"


        Dim TableEspacio As PdfPTable = New PdfPTable(1)
        Dim ColEsp As PdfPCell
        TableEspacio.WidthPercentage = 100
        Dim widthsTE As Single() = New Single() {1000.0F}
        TableEspacio.SetWidths(widthsTE)

        ColEsp = New PdfPCell(New Phrase(" ", Font))
        ColEsp.Border = 0
        ColEsp.HorizontalAlignment = PdfPCell.ALIGN_LEFT
        TableEspacio.AddCell(ColEsp)

#End Region

#Region "Encabezado"



        Dim Table1 As PdfPTable = New PdfPTable(2)
        Table1.DefaultCell.Border = BorderStyle.None
        Dim Col1 As PdfPCell
        'Dim ILine As Integer
        'Dim iFila As Integer
        Table1.WidthPercentage = 100

        Dim widths As Single() = New Single() {150.0F, 400.0F}
        Table1.SetWidths(widths)

        'Encabezado

        Dim imagenBMP As iTextSharp.text.Image
        imagenBMP = iTextSharp.text.Image.GetInstance(LOGOBYTE)
        imagenBMP.ScaleToFit(80.0F, 70.0F)

        imagenBMP.Border = 0

        Table1.AddCell(imagenBMP)

        Dim Tabledireccion As PdfPTable = New PdfPTable(1)
        Col1 = New PdfPCell(New Phrase(Empresa, Font12))
        Col1.Border = 0
        Col1.HorizontalAlignment = PdfPCell.ALIGN_CENTER

        Dim DIRECCIONE As String = Direccion & " " & coloniaEMPRESA & " " & poblacionEMPRESA & " " & Estadoempresa
        Dim Col1d = New PdfPCell(New Phrase(DIRECCIONE, Font8))
        Col1d.Border = 0
        Col1d.HorizontalAlignment = PdfPCell.ALIGN_CENTER

        Dim Col1rfe = New PdfPCell(New Phrase(RFCEmisor, Font9))
        Col1rfe.Border = 0
        Col1rfe.HorizontalAlignment = PdfPCell.ALIGN_CENTER


        Tabledireccion.AddCell(Col1)
        Tabledireccion.AddCell(Col1d)
        Tabledireccion.AddCell(Col1rfe)
        Table1.AddCell(Tabledireccion)

#End Region

#Region "Datos Contrato"



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
        Dim widthsT4 As Single() = New Single() {600.0F, 300.0F}

        Table4.SetWidths(widthsT4)

        Try

            datosContrato.Read()

            ColDN = New PdfPCell(New Phrase("NOMBRE: " & datosContrato("Nombre"), Font9))
            ColDN.Border = 0
            ColDN.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table4.AddCell(ColDN)

            ColDN1 = New PdfPCell(New Phrase("CONTRATO: " & datosContrato("Cuenta"), Font9))
            ColDN1.Border = 0
            ColDN1.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table4.AddCell(ColDN1)

            ColDN2 = New PdfPCell(New Phrase("CALLE: " & datosContrato("Domicilio") & " COLONIA: " & datosContrato("Colonia"), Font9))
            ColDN2.Border = 0
            ColDN2.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table4.AddCell(ColDN2)

            ColDN3 = New PdfPCell(New Phrase("TARIFA: " & datosContrato("descripcion_cuota").ToString().ToUpper(), Font9))
            ColDN3.Border = 0
            ColDN3.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table4.AddCell(ColDN3)

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try



#End Region

#Region "Tabla Lecturas"

        'Encabezado consulta tabla

        Dim Table6 As PdfPTable = New PdfPTable(10)


        Table6.WidthPercentage = 100
        Dim widthsT6 As Single() = New Single() {40.0F, 40.0F, 50.0F, 50.0F, 40.0F, 80.0F, 60.0F, 50.0F, 50.0F, 70.0F}
        Table6.SetWidths(widthsT6)

        Dim Col61 = New PdfPCell(New Phrase("AÑO", Font9))
        Col61.Border = 7
        Col61.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        Col61.BackgroundColor = colordefinido.color
        Table6.AddCell(Col61)

        Dim Col62 = New PdfPCell(New Phrase("MES", Font9))
        Col62.Border = 3
        Col62.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        Col62.BackgroundColor = colordefinido.color
        Table6.AddCell(Col62)

        Dim Col63 = New PdfPCell(New Phrase("LECTURA ACTUAL", Font9))
        Col63.Border = 3
        Col63.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        Col63.BackgroundColor = colordefinido.color
        Table6.AddCell(Col63)

        Dim Col64 = New PdfPCell(New Phrase("LECTURA ANTERIOR", Font9))
        Col64.Border = 3
        Col64.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        Col64.BackgroundColor = colordefinido.color
        Table6.AddCell(Col64)


        Dim Col65 = New PdfPCell(New Phrase("CONSUMO", Font9))
        Col65.Border = 3
        Col65.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        Col65.BackgroundColor = colordefinido.color
        Table6.AddCell(Col65)

        Dim Col66 = New PdfPCell(New Phrase("SITUACION", Font9))
        Col66.Border = 3
        Col66.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        Col66.BackgroundColor = colordefinido.color
        Table6.AddCell(Col66)

        Dim Col67 = New PdfPCell(New Phrase("TOTAL", Font9))
        Col67.Border = 3
        Col67.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        Col67.BackgroundColor = colordefinido.color
        Table6.AddCell(Col67)

        Dim Col68 = New PdfPCell(New Phrase("LEC. ADEL.", Font9))
        Col68.Border = 3
        Col68.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        Col68.BackgroundColor = colordefinido.color
        Table6.AddCell(Col68)

        Dim Col69 = New PdfPCell(New Phrase("PAGADO", Font9))
        Col69.Border = 3
        Col69.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        Col69.BackgroundColor = colordefinido.color
        Table6.AddCell(Col69)

        Dim Col70 = New PdfPCell(New Phrase("OBSERVACIÓN", Font9))
        Col70.Border = 11
        Col70.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        Col70.BackgroundColor = colordefinido.color
        Table6.AddCell(Col70)

        While datosLecturas.Read()
            Col61 = New PdfPCell(New Phrase(datosLecturas("PERIODO"), Font9))
            Col61.Border = 7
            Col61.HorizontalAlignment = PdfPCell.ALIGN_CENTER

            Table6.AddCell(Col61)

            Col62 = New PdfPCell(New Phrase(datosLecturas("MES"), Font9))
            Col62.Border = 3
            Col62.HorizontalAlignment = PdfPCell.ALIGN_CENTER

            Table6.AddCell(Col62)

            Col63 = New PdfPCell(New Phrase(datosLecturas("LECTURA"), Font9))
            Col63.Border = 3
            Col63.HorizontalAlignment = PdfPCell.ALIGN_CENTER

            Table6.AddCell(Col63)

            Col64 = New PdfPCell(New Phrase(datosLecturas("ANTERIOR"), Font9))
            Col64.Border = 3
            Col64.HorizontalAlignment = PdfPCell.ALIGN_CENTER

            Table6.AddCell(Col64)


            Col65 = New PdfPCell(New Phrase(datosLecturas("CONSUMO"), Font9))
            Col65.Border = 3
            Col65.HorizontalAlignment = PdfPCell.ALIGN_CENTER

            Table6.AddCell(Col65)

            Try
                Col66 = New PdfPCell(New Phrase(datosLecturas("SITUACION"), Font9))
                Col66.Border = 3
                Col66.HorizontalAlignment = PdfPCell.ALIGN_CENTER

                Table6.AddCell(Col66)

            Catch ex As Exception

                'ColEsp = New PdfPCell(New Phrase(" ", Font))

                Col66 = New PdfPCell(New Phrase(" ", Font9))
                Col66.Border = 3
                Col66.HorizontalAlignment = PdfPCell.ALIGN_CENTER

                Table6.AddCell(Col66)
            End Try


            Col67 = New PdfPCell(New Phrase("$" & datosLecturas("TOTAL"), Font9))
            Col67.Border = 3
            Col67.HorizontalAlignment = PdfPCell.ALIGN_CENTER

            Table6.AddCell(Col67)

            Col68 = New PdfPCell(New Phrase(datosLecturas("L. ADELANTADA"), Font9))
            Col68.Border = 3
            Col68.HorizontalAlignment = PdfPCell.ALIGN_CENTER

            Table6.AddCell(Col68)

            Col69 = New PdfPCell(New Phrase(datosLecturas("PAGADO"), Font9))
            Col69.Border = 3
            Col69.HorizontalAlignment = PdfPCell.ALIGN_CENTER

            Table6.AddCell(Col69)

            Try


                'System.InvalidCastException: 'La conversión del tipo 'DBNull' en el tipo 'String' no es válida.'
                Col70 = New PdfPCell(New Phrase(datosLecturas("OBSERVA"), Font9))
                Col70.Border = 11
                Col70.HorizontalAlignment = PdfPCell.ALIGN_CENTER

                Table6.AddCell(Col70)

            Catch ex As Exception

                Col70 = New PdfPCell(New Phrase(" ", Font9))
                Col70.Border = 11
                Col70.HorizontalAlignment = PdfPCell.ALIGN_CENTER

                Table6.AddCell(Col70)

            End Try


        End While



#End Region

#Region "Pie Pagina"

        Dim Table10 As PdfPTable = New PdfPTable(1)
        Dim Col101 As PdfPCell


        Table10.WidthPercentage = 100
        Dim widthsT10 As Single() = New Single() {1000.0F}
        Table10.SetWidths(widthsT10)

        Col101 = New PdfPCell(New Phrase("ESTE DOCUMENTO ES UNA REPRESENTACION IMPRESA DE LECTURAS, LAS LECTURAS PUEDEN LLEGAR A CAMBIAR DEBIDO A CORRECCIONES EN EL CONTRATO", Font8))
        Col101.Border = 0
        Col101.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        Table10.AddCell(Col101)

#End Region


        pdfDoc.Add(Table1)
        pdfDoc.Add(TableEspacio)
        pdfDoc.Add(TableEspacio)
        pdfDoc.Add(Table4)
        pdfDoc.Add(TableEspacio)
        pdfDoc.Add(TableEspacio)
        pdfDoc.Add(Table6)
        pdfDoc.Add(TableEspacio)
        pdfDoc.Add(TableEspacio)
        pdfDoc.Add(Table10)
        pdfDoc.Close()

        Try
            Dim psi As New ProcessStartInfo(directorioReporteLecturas & "\HistorialLecturas_Contrato_" & ContratoP & ".pdf")
            'psi.WorkingDirectory = cadenafolder & "\factura\" + nombresespacios

            psi.WindowStyle = ProcessWindowStyle.Hidden
            Dim p As Process = Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show("Error al Visualizar el Reporte de Historial de Lecturas" & ex.Message)
        End Try

    End Sub





End Class

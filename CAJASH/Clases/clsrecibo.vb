Imports System.IO
Imports System.Text
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class clsrecibo

    Public Fecha_Act As String
    Public fechafin As String
    Public periodo As String
    Public fecha_deuda As String
    Public subtotal As Double
    Public iva As Double
    Public total As Double
    Public nombre As String
    Public cancelado As String
    Public cuenta As Double
    Public comunidad As String
    Public colonia As String
    Public usuarios As String
    Public ubicacion As String
    Public tarifa As String
    Public ccodpago As String
    Public esusuario As String
    Public esmedido As Boolean = False
    Public observacion As String = ""
    Public fechaoriginaldedeuda As String
    Public medidor As String
    '''''
    Public concepto As String
    Public Cantidad As Integer
    Public importe As Double
    Public descuento As Double
    Public Totaldescuentoenpesos As Double
    '''''

    Public NUMERO As Int32 = 0
    Public SERIE As String = "H"
End Class

Public Class reciboaimprimir

    Public Sub imprime(Serie As String, folio As Integer, preview As Boolean, cambio As Decimal, Optional valeanterior As Decimal = 0)

        Dim DATOS As IDataReader
        Dim CONTE As IDataReader
        Try
            DATOS = ConsultaSql("SELECT * FROM PAGOS WHERE SERIE='" & Serie & "' AND recibo=" & folio).ExecuteReader
            DATOS.Read()
            CONTE = ConsultaSql("SELECT * FROM PAGOtros WHERE SERIE='" & Serie & "' AND recibo=" & folio).ExecuteReader

        Catch ex As Exception

            Exit Sub
        End Try

        Dim Nombre As String = String.Empty
        Dim direccion As String = String.Empty
        Dim colonia As String = String.Empty
        Dim municipio As String = String.Empty
        Dim entidad As String = String.Empty
        Dim tarifa As String = String.Empty
        Dim nodemedidor As String = String.Empty


        Nombre = DATOS("Nombre")

        Dim DATOSUSUARIO As IDataReader

        If DATOS("ESUSUARIO") = 1 Then
            DATOSUSUARIO = ConsultaSql("SELECT * FROM VUSUARIO WHERE CUENTA=" & DATOS("CUENTA")).ExecuteReader
            DATOSUSUARIO.Read()
            direccion = DATOSUSUARIO("Direccion")
            colonia = DATOSUSUARIO("colonia")
            municipio = DATOSUSUARIO("municipio")
            tarifa = DATOS("tarifa")
            nodemedidor = DATOSUSUARIO("nodemedidor")

        End If
        If DATOS("ESUSUARIO") = 3 Then
            DATOSUSUARIO = ConsultaSql("SELECT * FROM vSOLICITUD WHERE NUMERO=" & DATOS("CUENTA")).ExecuteReader
            DATOSUSUARIO.Read()
            direccion = DATOSUSUARIO("domicilio") + DATOSUSUARIO("numext") + DATOSUSUARIO("numint")
            colonia = DATOSUSUARIO("colonia")
            municipio = DATOSUSUARIO("municipio")

        End If

        If DATOS("ESUSUARIO") = 2 Then
            DATOSUSUARIO = ConsultaSql("SELECT * FROM NOUSUARIO WHERE CLAVE=" & DATOS("CUENTA")).ExecuteReader
            DATOSUSUARIO.Read()
            direccion = DATOSUSUARIO("direccion") + DATOSUSUARIO("numext") + DATOSUSUARIO("numint")
            colonia = DATOSUSUARIO("colonia")
            municipio = DATOSUSUARIO("municipio")
        End If

        Dim UBICACION As String = String.Empty

        Try
            If DATOS("ESUSUARIO") = 1 Then
                UBICACION = DATOS("UBICACION")
            End If

        Catch ex As Exception
            UBICACION = String.Empty
        End Try

        'Crear el directorio en donde se van a almacenar los PDF
        If Not My.Computer.FileSystem.DirectoryExists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\ReporteCaja\" & Year(Now) & acompletacero(Month(Now).ToString(), 2).Trim) Then

            My.Computer.FileSystem.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\ReporteCaja\" & Year(Now) & acompletacero(Month(Now).ToString(), 2).Trim)
        End If

        Dim cadenafolder As String = (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\ReporteCaja\" & Year(Now) & acompletacero(Month(Now).ToString(), 2)).Trim

        'Dar propiedades al Documento

        Dim pdfDoc As New Document(PageSize.LETTER, 10.0F, 20.0F, 10.0F, 20.0F)

        'Obtener la ruta donde se va a crear el pdf
        Dim pdfWrite As PdfWriter = PdfWriter.GetInstance(pdfDoc, New System.IO.FileStream(cadenafolder & "\recibo_" & Serie & folio & ".pdf", FileMode.Create))

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
        Dim Font12 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.BOLD))

        Dim Fontp As New Font(FontFactory.GetFont(FontFactory.COURIER, 7, iTextSharp.text.Font.BOLD))
        Dim CVacio As PdfPCell = New PdfPCell(New Phrase(""))

        pdfDoc.Open()

        Try

            For i = 1 To 2



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



                Dim Table1 As PdfPTable = New PdfPTable(3)
                Table1.DefaultCell.Border = BorderStyle.None
                Table1.PaddingTop = 40
                Dim Col1 As PdfPCell
                'Dim ILine As Integer
                'Dim iFila As Integer
                Table1.WidthPercentage = 100

                Dim widths As Single() = New Single() {150.0F, 350, 100.0F}
                Table1.SetWidths(widths)

                'Encabezado

                Table1.AddCell(imagenBMP)

                Dim Tabledireccion As PdfPTable = New PdfPTable(1)
                Col1 = New PdfPCell(New Phrase(Empresa, Font12))
                Col1.Border = 0
                Col1.HorizontalAlignment = PdfPCell.ALIGN_CENTER

                Dim DIRECCIONE As String = direccion & " " & coloniaEMPRESA & " " & poblacionEMPRESA & " " & Estadoempresa
                Dim Col1d = New PdfPCell(New Phrase(DIRECCIONE, Font8))
                Col1d.Border = 0
                Col1d.HorizontalAlignment = PdfPCell.ALIGN_CENTER



                Dim Col1rfe = New PdfPCell(New Phrase(RFCORGANISMO, Font9))
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
                Table2.WidthPercentage = 100
                Dim widthsT2 As Single() = New Single() {60, 60.0F}
                Table2.SetWidths(widthsT2)

                Col10 = New PdfPCell(New Phrase("Serie", Font8))
                Col10.Border = 0
                Col10.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                Table2.AddCell(Col10)


                Col11 = New PdfPCell(New Phrase(Serie, Font8N))
                Col11.Border = 0
                Col11.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                Table2.AddCell(Col11)

                Dim Col10f = New PdfPCell(New Phrase("Recibo", Font8))
                Col10f.Border = 0
                Col10f.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                Table2.AddCell(Col10f)


                Col12 = New PdfPCell(New Phrase(folio, Font8N))
                Col12.Border = 0
                Col12.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                Table2.AddCell(Col12)

                Col13 = New PdfPCell(New Phrase("Fecha :", Font8))
                Col13.Border = 0
                Col13.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                Table2.AddCell(Col13)

                Col14 = New PdfPCell(New Phrase(DateTime.Now.ToShortDateString(), Font8))
                Col14.Border = 0
                Col14.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                Table2.AddCell(Col14)

                Table1.AddCell(Table2)

                pdfDoc.Add(Table1)


                'Tabla para el Encabezado
                Dim Tablecontenido = New PdfPTable(1)
                'Tablecontenido.DefaultCell.BorderWidthRight = 50
                'Tablecontenido.
                Tablecontenido.WidthPercentage = 100
                Dim widthstc As Single() = New Single() {600.0F} ' fijamos dos columnas por renglon
                Tablecontenido.SetWidths(widthstc)

                Dim CELDATITULO = New PdfPCell(New Phrase("RECIBO DE PAGO"))
                CELDATITULO.Border = 0
                CELDATITULO.PaddingTop = 10
                CELDATITULO.PaddingBottom = 10
                CELDATITULO.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                Tablecontenido.AddCell(CELDATITULO)


                pdfDoc.Add(Tablecontenido)




                'Tabala datos del usuario encabezado
                Dim tabladatosEncUusario = New PdfPTable(4)
                tabladatosEncUusario.PaddingTop = 20
                tabladatosEncUusario.DefaultCell.Border = BorderStyle.None
                tabladatosEncUusario.WidthPercentage = 95
                Dim anchodatos As Single() = New Single() {90.0F, 360.0F, 80.0F, 50.0F}
                tabladatosEncUusario.SetWidths(anchodatos)

                Dim ColdatosEncUsuario1 = New PdfPCell(New Phrase("NOMBRE:", Font8White))
                ColdatosEncUsuario1.Border = 0
                ColdatosEncUsuario1.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ColdatosEncUsuario1.BackgroundColor = New iTextSharp.text.BaseColor(23, 162, 184)
                tabladatosEncUusario.AddCell(ColdatosEncUsuario1)

                Dim ColdatosEncUsuario2 = New PdfPCell(New Phrase(Nombre.ToString().ToUpper(), Font12))
                ColdatosEncUsuario2.Border = 0
                ColdatosEncUsuario2.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                tabladatosEncUusario.AddCell(ColdatosEncUsuario2)

                Dim ColdatosEncUsuario3 = New PdfPCell(New Phrase("CUENTA:", Font8White))
                ColdatosEncUsuario3.Border = 0
                ColdatosEncUsuario3.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ColdatosEncUsuario3.BackgroundColor = New iTextSharp.text.BaseColor(23, 162, 184)
                tabladatosEncUusario.AddCell(ColdatosEncUsuario3)



                Dim ColdatosEncUsuario4 = New PdfPCell(New Phrase(DATOS("CUENTA"), Font8))
                ColdatosEncUsuario4.Border = 0
                ColdatosEncUsuario4.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                tabladatosEncUusario.AddCell(ColdatosEncUsuario4)



                ColdatosEncUsuario1 = New PdfPCell(New Phrase("DIRECCIÓN:", Font8White))
                ColdatosEncUsuario1.Border = 0
                ColdatosEncUsuario1.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ColdatosEncUsuario1.BackgroundColor = New iTextSharp.text.BaseColor(23, 162, 184)
                tabladatosEncUusario.AddCell(ColdatosEncUsuario1)

                ColdatosEncUsuario2 = New PdfPCell(New Phrase(direccion + " " + colonia + " " + municipio, Font8))
                ColdatosEncUsuario2.Border = 0
                ColdatosEncUsuario2.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                tabladatosEncUusario.AddCell(ColdatosEncUsuario2)

                ColdatosEncUsuario3 = New PdfPCell(New Phrase("TARIFA:", Font8White))
                ColdatosEncUsuario3.Border = 0
                ColdatosEncUsuario3.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ColdatosEncUsuario3.BackgroundColor = New iTextSharp.text.BaseColor(23, 162, 184)
                tabladatosEncUusario.AddCell(ColdatosEncUsuario3)

                ColdatosEncUsuario4 = New PdfPCell(New Phrase(tarifa.ToString().ToUpper(), Font8))
                ColdatosEncUsuario4.Border = 0
                ColdatosEncUsuario4.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                tabladatosEncUusario.AddCell(ColdatosEncUsuario4)




                ColdatosEncUsuario1 = New PdfPCell(New Phrase(" ", Font12))
                ColdatosEncUsuario1.Border = 0
                ColdatosEncUsuario1.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                'ColdatosEncUsuario1.BackgroundColor = New iTextSharp.text.BaseColor(23,162,184)
                tabladatosEncUusario.AddCell(ColdatosEncUsuario1)

                ColdatosEncUsuario2 = New PdfPCell(New Phrase(" ", Font5))
                ColdatosEncUsuario2.Border = 0
                ColdatosEncUsuario2.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                tabladatosEncUusario.AddCell(ColdatosEncUsuario2)

                ColdatosEncUsuario3 = New PdfPCell(New Phrase("NO. MED:", Font8White))
                ColdatosEncUsuario3.Border = 0
                ColdatosEncUsuario3.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ColdatosEncUsuario3.BackgroundColor = New iTextSharp.text.BaseColor(23, 162, 184)
                tabladatosEncUusario.AddCell(ColdatosEncUsuario3)
                Try
                    ColdatosEncUsuario4 = New PdfPCell(New Phrase(nodemedidor, Font12))
                    ColdatosEncUsuario4.Border = 0
                    ColdatosEncUsuario4.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    tabladatosEncUusario.AddCell(ColdatosEncUsuario4)
                Catch ex As Exception
                    ColdatosEncUsuario4 = New PdfPCell(New Phrase(DATOS(""), Font12))
                    ColdatosEncUsuario4.Border = 0
                    ColdatosEncUsuario4.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    tabladatosEncUusario.AddCell(ColdatosEncUsuario4)
                End Try



                pdfDoc.Add(tabladatosEncUusario)

                pdfDoc.Add(TableVacio)
                pdfDoc.Add(TableVacio)

                Dim TableGeneralConceptos As PdfPTable = New PdfPTable(5)
                TableGeneralConceptos.WidthPercentage = 100
                TableGeneralConceptos.PaddingTop = 30
                TableGeneralConceptos.DefaultCell.Border = BorderStyle.FixedSingle

                Dim widthsInfConc As Single() = New Single() {50.0F, 350.0F, 100.0F, 100.0F, 60.0F}
                TableGeneralConceptos.SetWidths(widthsInfConc)


                Dim ColInfoConceptos = New PdfPCell(New Phrase("Cantidad", Font8White)) With {
                .Border = 0,
                .HorizontalAlignment = PdfPCell.ALIGN_LEFT,
                .BackgroundColor = New iTextSharp.text.BaseColor(23, 162, 184)
                }

                TableGeneralConceptos.AddCell(ColInfoConceptos)

                ColInfoConceptos = New PdfPCell(New Phrase("Concepto", Font8White)) With {
                .Border = 0,
                .HorizontalAlignment = PdfPCell.ALIGN_LEFT,
                .BackgroundColor = New iTextSharp.text.BaseColor(23, 162, 184)
            }
                TableGeneralConceptos.AddCell(ColInfoConceptos)

                ColInfoConceptos = New PdfPCell(New Phrase("Precio Unitario", Font8White)) With {
                .Border = 0,
                .HorizontalAlignment = PdfPCell.ALIGN_LEFT,
                .BackgroundColor = New iTextSharp.text.BaseColor(23, 162, 184)
            }
                TableGeneralConceptos.AddCell(ColInfoConceptos)

                ColInfoConceptos = New PdfPCell(New Phrase("Importe", Font8White)) With {
                .Border = 0,
                .HorizontalAlignment = PdfPCell.ALIGN_LEFT,
                .BackgroundColor = New iTextSharp.text.BaseColor(23, 162, 184)
            }
                TableGeneralConceptos.AddCell(ColInfoConceptos)

                ColInfoConceptos = New PdfPCell(New Phrase("IVA", Font8White)) With {
                .Border = 0,
                .HorizontalAlignment = PdfPCell.ALIGN_LEFT,
                .BackgroundColor = New iTextSharp.text.BaseColor(23, 162, 184)
                }

                TableGeneralConceptos.AddCell(ColInfoConceptos)

                Dim contenido As String = String.Empty

                CONTE = ConsultaSql("SELECT * FROM PAGOtros WHERE SERIE='" & Serie & "' AND recibo=" & folio).ExecuteReader
                While CONTE.Read()

                    contenido = contenido & CONTE("Cantidad") & "|"

                    ColInfoConceptos = New PdfPCell(New Phrase(CONTE("Cantidad"), Font12)) With {
                .Border = 0,
                .HorizontalAlignment = PdfPCell.ALIGN_LEFT
                }
                    contenido = contenido & CONTE("CONCEPTO") & "|"
                    TableGeneralConceptos.AddCell(ColInfoConceptos)

                    ColInfoConceptos = New PdfPCell(New Phrase(CONTE("Concepto"), Font12)) With {
                    .Border = 0,
                    .HorizontalAlignment = PdfPCell.ALIGN_LEFT
                }

                    contenido = contenido & CONTE("importe") & "|"
                    TableGeneralConceptos.AddCell(ColInfoConceptos)

                    ColInfoConceptos = New PdfPCell(New Phrase(CONTE("monto"), Font12)) With {
                    .Border = 0,
                    .HorizontalAlignment = PdfPCell.ALIGN_LEFT
                }
                    TableGeneralConceptos.AddCell(ColInfoConceptos)

                    ColInfoConceptos = New PdfPCell(New Phrase(CONTE("IMPORTE"), Font12)) With {
                    .Border = 0,
                    .HorizontalAlignment = PdfPCell.ALIGN_LEFT
                }
                    TableGeneralConceptos.AddCell(ColInfoConceptos)

                    ColInfoConceptos = New PdfPCell(New Phrase(CONTE("MontoIVa"), Font12)) With {
                    .Border = 0,
                    .HorizontalAlignment = PdfPCell.ALIGN_LEFT
                }
                    TableGeneralConceptos.AddCell(ColInfoConceptos)

                End While


                'Tabla Información Conceptos

                Dim TableGeneralTotales As PdfPTable = New PdfPTable(2)
                TableGeneralTotales.WidthPercentage = 100
                TableGeneralTotales.DefaultCell.Border = BorderStyle.None
                Dim widthsTotales As Single() = New Single() {450.0F, 450.0F}
                TableGeneralTotales.SetWidths(widthsTotales)

                'Tabla Totales 1
                Dim TableTotal1 As PdfPTable = New PdfPTable(2)
                TableTotal1.WidthPercentage = 100
                TableTotal1.DefaultCell.Border = BorderStyle.None
                Dim widthsTotal1 As Single() = New Single() {450.0F, 450.0F}
                TableTotal1.SetWidths(widthsTotal1)

                Dim ColTotales = New PdfPCell(New Phrase("Subtotal", Font12)) With {
                .Border = 1,
                .HorizontalAlignment = PdfPCell.ALIGN_LEFT
            }
                TableTotal1.AddCell(ColTotales)
                Dim subtotal As Decimal
                subtotal = DATOS("pagos")
                ColTotales = New PdfPCell(New Phrase(subtotal.ToString("C"), Font12)) With {
                .Border = 1,
                .HorizontalAlignment = PdfPCell.ALIGN_LEFT
            }
                TableTotal1.AddCell(ColTotales)



                Dim iva As Decimal
                iva = DATOS("IVA")

                ColTotales = New PdfPCell(New Phrase("IVA", Font12)) With {
                .Border = 0,
                .HorizontalAlignment = PdfPCell.ALIGN_LEFT
            }
                TableTotal1.AddCell(ColTotales)

                ColTotales = New PdfPCell(New Phrase(iva.ToString("C"), Font12)) With {
                .Border = 0,
                .HorizontalAlignment = PdfPCell.ALIGN_LEFT
            }

                TableTotal1.AddCell(ColTotales)

                TableGeneralTotales.AddCell(TableTotal1)




                'Tabla Totales 2 


                Dim TableTotal2 As PdfPTable = New PdfPTable(2)
                TableTotal2.WidthPercentage = 100
                TableTotal2.DefaultCell.Border = BorderStyle.None
                Dim widthsTotal2 As Single() = New Single() {450.0F, 450.0F}
                TableTotal2.SetWidths(widthsTotal2)

                Dim ColTotales2 = New PdfPCell(New Phrase("Total", Font12)) With {
                .Border = 1,
                .HorizontalAlignment = PdfPCell.ALIGN_LEFT
            }
                TableTotal2.AddCell(ColTotales2)

                Dim total As Decimal
                total = DATOS("total")


                ColTotales2 = New PdfPCell(New Phrase(total.ToString("C"), Font12)) With {
                .Border = 1,
                .HorizontalAlignment = PdfPCell.ALIGN_LEFT
            }


                TableTotal2.AddCell(ColTotales2)



                TableGeneralTotales.AddCell(TableTotal2)


                Dim TableTotalLetra As PdfPTable = New PdfPTable(1)
                TableTotalLetra.WidthPercentage = 100
                TableTotalLetra.DefaultCell.Border = BorderStyle.None
                Dim widthsTotalLet As Single() = New Single() {900.0F}
                TableTotalLetra.SetWidths(widthsTotalLet)

                Dim ColTotLetra = New PdfPCell(New Phrase(ConvertCurrencyToSpanish(total, "Pesos"), Font9))
                ColTotLetra.Border = 1
                ColTotLetra.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                TableTotalLetra.AddCell(ColTotLetra)





                pdfDoc.Add(TableGeneralConceptos)
                pdfDoc.Add(TableGeneralTotales)
                pdfDoc.Add(TableTotalLetra)


                Dim TableFIRMAS As PdfPTable = New PdfPTable(1)
                TableFIRMAS.WidthPercentage = 100
                TableFIRMAS.PaddingTop = 40
                TableFIRMAS.DefaultCell.Border = BorderStyle.None
                Dim widthsTotalFIR As Single() = New Single() {900.0F}
                TableFIRMAS.SetWidths(widthsTotalFIR)


                TableFIRMAS.AddCell(ColVacio)
                TableFIRMAS.AddCell(ColVacio)


                ColTotLetra = New PdfPCell(New Phrase("FIRMA DEL CAJERO", Font9))
                ColTotLetra.Border = 1
                ColTotLetra.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                TableFIRMAS.AddCell(ColTotLetra)

                TableFIRMAS.AddCell(ColVacio)
                TableFIRMAS.AddCell(ColVacio)


                pdfDoc.Add(TableFIRMAS)




                Dim Tablevalidacion As PdfPTable = New PdfPTable(2)
                Tablevalidacion.WidthPercentage = 100
                Tablevalidacion.PaddingTop = 40
                Tablevalidacion.DefaultCell.Border = BorderStyle.None
                Dim widthsTotalval As Single() = New Single() {800, 50.0F}
                Tablevalidacion.SetWidths(widthsTotalval)


                Dim cadena As String = "|1.0|" & Nombre & "|" & Serie & "|" & folio & "|" & DATOS("ESUSUARIO") & "|" & subtotal & "|" & iva & "|" & total & contenido
                ColTotLetra = New PdfPCell(New Phrase(cadena, Font9))
                ColTotLetra.Border = 1
                ColTotLetra.HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED
                Tablevalidacion.AddCell(ColTotLetra)

                Dim codigoQR = New StringBuilder()
                codigoQR.Append(cadena)

                Dim pdfCodigoQR = New BarcodeQRCode(codigoQR.ToString(), 1, 1, New Dictionary(Of iTextSharp.text.pdf.qrcode.EncodeHintType, System.Object))
                Dim img As Image = pdfCodigoQR.GetImage()
                img.SpacingAfter = 0.0F
                img.SpacingBefore = 0.0F
                img.BorderWidth = 1.0F
                img.HasAbsolutePosition()
                'img.ScalePercent(100, 78)
                Tablevalidacion.AddCell(img)

                pdfDoc.Add(Tablevalidacion)
                If i = 1 Then
                    pdfDoc.Add(TableVacio)
                    pdfDoc.Add(TableVacio)
                    pdfDoc.Add(TableVacio)
                    pdfDoc.Add(TableVacio)
                    pdfDoc.Add(TableVacio)
                End If

            Next
            pdfDoc.Close()
        Catch err As Exception
            pdfDoc.Close()
        End Try

        If preview = True Then
            Try
                Dim psi As New ProcessStartInfo(cadenafolder & "\recibo_" & Serie & folio & ".pdf")
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
            gsProcessInfo.FileName = cadenafolder & "\recibo_" & Serie & folio & ".pdf"
            ' gsProcessInfo.Arguments = """" & nombreImpresora & """"
            gsProcess = Process.Start(gsProcessInfo)
            gsProcess.WaitForInputIdle(2200)
            gsProcess.Close()
            '  gsProcess.Kill()

            ' gsProcess.EnableRaisingEvents = True


        End If


    End Sub


    Public Sub ReciboHojaCarta(Serie As String, folio As Integer, preview As Boolean, cambio As Decimal, formaPago As String, Optional valeanterior As Decimal = 0)

        Dim DATOS As IDataReader
        Dim CONTE As IDataReader


        Try

            DATOS = ConsultaSql("SELECT * FROM PAGOS WHERE SERIE='" & Serie & "' AND recibo=" & folio).ExecuteReader
            DATOS.Read()
            CONTE = ConsultaSql("SELECT * FROM PAGOtros WHERE SERIE='" & Serie & "' AND recibo=" & folio).ExecuteReader

        Catch ex As Exception

            Exit Sub

        End Try


        Dim Nombre As String = String.Empty
        Dim direccion As String = String.Empty
        Dim colonia As String = String.Empty
        Dim municipio As String = String.Empty
        Dim entidad As String = String.Empty
        Dim tarifa As String = String.Empty
        Dim nodemedidor As String = String.Empty


        Nombre = DATOS("Nombre")

        Dim DATOSUSUARIO As IDataReader

        If DATOS("ESUSUARIO") = 1 Then

            DATOSUSUARIO = ConsultaSql("SELECT * FROM VUSUARIO WHERE CUENTA=" & DATOS("CUENTA")).ExecuteReader
            DATOSUSUARIO.Read()
            direccion = DATOSUSUARIO("Direccion")
            colonia = DATOSUSUARIO("colonia")
            municipio = DATOSUSUARIO("municipio")
            tarifa = DATOS("tarifa")
            nodemedidor = DATOSUSUARIO("nodemedidor")

        End If

        If DATOS("ESUSUARIO") = 3 Then

            DATOSUSUARIO = ConsultaSql("SELECT * FROM vSOLICITUD WHERE NUMERO=" & DATOS("CUENTA")).ExecuteReader
            DATOSUSUARIO.Read()
            direccion = DATOSUSUARIO("domicilio") + DATOSUSUARIO("numext") + DATOSUSUARIO("numint")
            colonia = DATOSUSUARIO("colonia")
            municipio = DATOSUSUARIO("municipio")

        End If

        If DATOS("ESUSUARIO") = 2 Then

            DATOSUSUARIO = ConsultaSql("SELECT * FROM NOUSUARIO WHERE CLAVE=" & DATOS("CUENTA")).ExecuteReader
            DATOSUSUARIO.Read()
            direccion = DATOSUSUARIO("direccion") + DATOSUSUARIO("numext") + DATOSUSUARIO("numint")
            colonia = DATOSUSUARIO("colonia")
            municipio = DATOSUSUARIO("municipio")

        End If

        Dim UBICACION As String = String.Empty

        Try

            If DATOS("ESUSUARIO") = 1 Then
                UBICACION = DATOS("UBICACION")
            End If

        Catch ex As Exception

            UBICACION = String.Empty

        End Try

        'Crear el directorio en donde se van a almacenar los PDF
        If Not My.Computer.FileSystem.DirectoryExists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\Recibos\" & Year(Now) & acompletacero(Month(Now).ToString(), 2).Trim) Then

            My.Computer.FileSystem.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\Recibos\" & Year(Now) & acompletacero(Month(Now).ToString(), 2).Trim)

        End If

        Dim cadenafolder As String = (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\Recibos\" & Year(Now) & acompletacero(Month(Now).ToString(), 2)).Trim


        'Dar propiedades al Documento

        Dim pdfDoc As New Document(PageSize.LETTER, 15.0F, 15.0F, 30.0F, 30.0F)

        'Obtener la ruta donde se va a crear el pdf
        Dim pdfWrite As PdfWriter = PdfWriter.GetInstance(pdfDoc, New System.IO.FileStream(cadenafolder & "\recibo_" & Serie & folio & ".pdf", FileMode.Create))

        'Formato de letras
        Dim Font8 As New Font(FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.NORMAL))
        Dim Font8Bold As New Font(FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.BOLD))
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
        Dim Font9Bold As New Font(FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.BOLD))
        Dim Font7White As New Font(FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, iTextSharp.text.Font.BOLD, BaseColor.WHITE))
        Dim Font8White As New Font(FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.BOLD, BaseColor.WHITE))
        Dim Font9White As New Font(FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.BOLD, BaseColor.WHITE))
        Dim Font5White As New Font(FontFactory.GetFont(FontFactory.TIMES_ROMAN, 5, iTextSharp.text.Font.BOLD, BaseColor.WHITE))
        Dim Font7courierN As New Font(FontFactory.GetFont(FontFactory.TIMES_ROMAN, 6, iTextSharp.text.Font.BOLD))
        Dim Font12 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.BOLD))

        Dim Fontp As New Font(FontFactory.GetFont(FontFactory.COURIER, 7, iTextSharp.text.Font.BOLD))
        Dim CVacio As PdfPCell = New PdfPCell(New Phrase(""))

        pdfDoc.Open()


        Dim colordefinido = New Clscolorreporte()
        colordefinido.ClsColoresReporte(My.Settings.colorfactura)


        Dim TableVacio As PdfPTable = New PdfPTable(1)
        TableVacio.DefaultCell.Border = BorderStyle.None
        TableVacio.WidthPercentage = 100
        Dim widthsVacio As Single() = New Single() {80.0F}
        TableVacio.SetWidths(widthsVacio)

        Dim ColVacio = New PdfPCell(New Phrase(" ", Font5White))
        ColVacio.Border = 0
        ColVacio.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
        TableVacio.AddCell(ColVacio)


        Dim Table1 As PdfPTable = New PdfPTable(3)
        Table1.DefaultCell.Border = BorderStyle.None
        Dim Col1 As PdfPCell
        'Dim ILine As Integer
        'Dim iFila As Integer
        Table1.WidthPercentage = 100

        Dim widths As Single() = New Single() {200.0F, 400.0F, 250.0F}
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

        Dim DIRECCIONE As String = direccion & " " & coloniaEMPRESA & " " & poblacionEMPRESA & " " & Estadoempresa
        Dim Col1d = New PdfPCell(New Phrase(DIRECCIONE, Font8))
        Col1d.Border = 0
        Col1d.HorizontalAlignment = PdfPCell.ALIGN_CENTER


        Dim Col1rfe = New PdfPCell(New Phrase(RFCORGANISMO, Font9))
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
        Table2.WidthPercentage = 100
        Dim widthsT2 As Single() = New Single() {120, 180.0F}
        Table2.SetWidths(widthsT2)

        Col10 = New PdfPCell(New Phrase("SERIE", Font9Bold))
        Col10.Border = 0
        Col10.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
        Table2.AddCell(Col10)


        Col11 = New PdfPCell(New Phrase(Serie, Font9Bold))
        Col11.Border = 0
        Col11.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
        Table2.AddCell(Col11)

        Dim Col10f = New PdfPCell(New Phrase("FOLIO", Font9Bold))
        Col10f.Border = 0
        Col10f.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
        Table2.AddCell(Col10f)


        Col12 = New PdfPCell(New Phrase(folio, Font9Bold))
        Col12.Border = 0
        Col12.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
        Table2.AddCell(Col12)

        Col13 = New PdfPCell(New Phrase("FECHA:", Font8Bold))
        Col13.Border = 0
        Col13.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
        Table2.AddCell(Col13)

        Col14 = New PdfPCell(New Phrase(DateTime.Now.ToString(), Font8))
        Col14.Border = 0
        Col14.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
        Table2.AddCell(Col14)


        'Dim ColDC1 = New PdfPCell(New Phrase("UUID", Font))
        'ColDC1.Border = 0
        'ColDC1.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
        'Table2.AddCell(ColDC1)


        'Dim ColDC2 = New PdfPCell(New Phrase(sdkresp.UUID, Font))
        'ColDC2.Border = 0
        'ColDC2.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
        'Table2.AddCell(ColDC2)

        'Dim ColDC3 = New PdfPCell(New Phrase("Certificado Emisor", Font))
        'ColDC3.Border = 0
        'ColDC3.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
        'Table2.AddCell(ColDC3)

        'Dim ColDC4 = New PdfPCell(New Phrase(sdkresp.NoCertificado, Font))
        'ColDC4.Border = 0
        'ColDC4.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
        'Table2.AddCell(ColDC4)


        'Dim ColDC7 = New PdfPCell(New Phrase("Certificado Sat ", Font))
        'ColDC7.Border = 0
        'ColDC7.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
        'Table2.AddCell(ColDC7)

        'Dim ColDC8 = New PdfPCell(New Phrase(sdkresp.CertificadoSAT, Font))
        'ColDC8.Border = 0
        'ColDC8.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
        'Table2.AddCell(ColDC8)

        Dim ColDCFP = New PdfPCell(New Phrase("FORMA DE PAGO", Font8Bold))
        ColDCFP.Border = 0
        ColDCFP.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
        Table2.AddCell(ColDCFP)



        Dim ColDC11 = New PdfPCell(New Phrase(New decodificadorSAT().getFormapago(formaPago), Font8))
        ColDC11.Border = 0
        ColDC11.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
        Table2.AddCell(ColDC11)

        Table1.AddCell(Table2)


        'Dim ColDCMeP = New PdfPCell(New Phrase("Método de Pago ", Font))
        'ColDCMeP.Border = 0
        'ColDCMeP.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
        'Table2.AddCell(ColDCMeP)

        'Dim ColDCMeP2 = New PdfPCell(New Phrase(New decodificadorSAT().getMetodo(metodo), Font))
        'ColDCMeP2.Border = 0
        'ColDCMeP2.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
        'Table2.AddCell(ColDCMeP2)

        'Dim ColDCUsoCFDI = New PdfPCell(New Phrase("Uso CFDI ", Font))
        'ColDCUsoCFDI.Border = 0
        'ColDCUsoCFDI.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
        'Table2.AddCell(ColDCUsoCFDI)

        'Dim ColDCUsoCFDI2 = New PdfPCell(New Phrase(New decodificadorSAT().getUso(varuso), Font))
        'ColDCUsoCFDI2.Border = 0
        'ColDCUsoCFDI2.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
        'Table2.AddCell(ColDCUsoCFDI2)

        'Table1.AddCell(Table2)


        'Tabala datos del usuario encabezado
        Dim tabladatosEncUusario = New PdfPTable(2)
        tabladatosEncUusario.PaddingTop = 20
        tabladatosEncUusario.DefaultCell.Border = BorderStyle.None
        tabladatosEncUusario.WidthPercentage = 95
        Dim anchodatos As Single() = New Single() {60.0F, 360.0F}
        tabladatosEncUusario.SetWidths(anchodatos)


        Dim ColdatosEncUsuario3 = New PdfPCell(New Phrase("CUENTA:", Font8White))
        ColdatosEncUsuario3.Border = 0
        ColdatosEncUsuario3.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
        ColdatosEncUsuario3.BackgroundColor = New iTextSharp.text.BaseColor(23, 162, 184)
        tabladatosEncUusario.AddCell(ColdatosEncUsuario3)



        Dim ColdatosEncUsuario4 = New PdfPCell(New Phrase(DATOS("CUENTA"), Font12))
        ColdatosEncUsuario4.Border = 0
        ColdatosEncUsuario4.HorizontalAlignment = PdfPCell.ALIGN_LEFT
        tabladatosEncUusario.AddCell(ColdatosEncUsuario4)


        Dim ColdatosEncUsuario1 = New PdfPCell(New Phrase("NOMBRE:", Font8White))
        ColdatosEncUsuario1.Border = 0
        ColdatosEncUsuario1.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
        ColdatosEncUsuario1.BackgroundColor = New iTextSharp.text.BaseColor(23, 162, 184)
        tabladatosEncUusario.AddCell(ColdatosEncUsuario1)

        Dim ColdatosEncUsuario2 = New PdfPCell(New Phrase(Nombre.ToString().ToUpper(), Font12))
        ColdatosEncUsuario2.Border = 0
        ColdatosEncUsuario2.HorizontalAlignment = PdfPCell.ALIGN_LEFT
        tabladatosEncUusario.AddCell(ColdatosEncUsuario2)





        ColdatosEncUsuario1 = New PdfPCell(New Phrase("DIRECCIÓN:", Font8White))
        ColdatosEncUsuario1.Border = 0
        ColdatosEncUsuario1.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
        ColdatosEncUsuario1.BackgroundColor = New iTextSharp.text.BaseColor(23, 162, 184)
        tabladatosEncUusario.AddCell(ColdatosEncUsuario1)

        ColdatosEncUsuario2 = New PdfPCell(New Phrase(direccion + " " + colonia + " " + municipio, Font8))
        ColdatosEncUsuario2.Border = 0
        ColdatosEncUsuario2.HorizontalAlignment = PdfPCell.ALIGN_LEFT
        tabladatosEncUusario.AddCell(ColdatosEncUsuario2)

        ColdatosEncUsuario3 = New PdfPCell(New Phrase("TARIFA:", Font8White))
        ColdatosEncUsuario3.Border = 0
        ColdatosEncUsuario3.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
        ColdatosEncUsuario3.BackgroundColor = New iTextSharp.text.BaseColor(23, 162, 184)
        tabladatosEncUusario.AddCell(ColdatosEncUsuario3)

        ColdatosEncUsuario4 = New PdfPCell(New Phrase(tarifa.ToString().ToUpper(), Font8))
        ColdatosEncUsuario4.Border = 0
        ColdatosEncUsuario4.HorizontalAlignment = PdfPCell.ALIGN_LEFT
        tabladatosEncUusario.AddCell(ColdatosEncUsuario4)




        ColdatosEncUsuario3 = New PdfPCell(New Phrase("NO. MED:", Font8White))
        ColdatosEncUsuario3.Border = 0
        ColdatosEncUsuario3.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
        ColdatosEncUsuario3.BackgroundColor = New iTextSharp.text.BaseColor(23, 162, 184)
        tabladatosEncUusario.AddCell(ColdatosEncUsuario3)


        Try

            ColdatosEncUsuario4 = New PdfPCell(New Phrase(nodemedidor, Font12))
            ColdatosEncUsuario4.Border = 0
            ColdatosEncUsuario4.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            tabladatosEncUusario.AddCell(ColdatosEncUsuario4)

        Catch ex As Exception

            ColdatosEncUsuario4 = New PdfPCell(New Phrase(DATOS(""), Font12))
            ColdatosEncUsuario4.Border = 0
            ColdatosEncUsuario4.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            tabladatosEncUusario.AddCell(ColdatosEncUsuario4)

        End Try


        pdfDoc.Add(Table1)
        pdfDoc.Add(TableVacio)
        pdfDoc.Add(TableVacio)
        pdfDoc.Add(tabladatosEncUusario)
        pdfDoc.Add(TableVacio)
        pdfDoc.Add(TableVacio)
        pdfDoc.Add(TableVacio)
        pdfDoc.Add(TableVacio)


        Dim TableGeneralConceptos As PdfPTable = New PdfPTable(5)
        TableGeneralConceptos.WidthPercentage = 100
        TableGeneralConceptos.PaddingTop = 30
        TableGeneralConceptos.DefaultCell.Border = BorderStyle.FixedSingle

        Dim widthsInfConc As Single() = New Single() {50.0F, 350.0F, 100.0F, 100.0F, 60.0F}
        TableGeneralConceptos.SetWidths(widthsInfConc)


        Dim ColInfoConceptos = New PdfPCell(New Phrase("Cantidad", Font8White)) With {
        .Border = 0,
        .HorizontalAlignment = PdfPCell.ALIGN_LEFT,
        .BackgroundColor = New iTextSharp.text.BaseColor(23, 162, 184)
        }

        TableGeneralConceptos.AddCell(ColInfoConceptos)

        ColInfoConceptos = New PdfPCell(New Phrase("Concepto", Font9White)) With {
        .Border = 0,
        .HorizontalAlignment = PdfPCell.ALIGN_LEFT,
        .BackgroundColor = New iTextSharp.text.BaseColor(23, 162, 184)
    }
        TableGeneralConceptos.AddCell(ColInfoConceptos)

        ColInfoConceptos = New PdfPCell(New Phrase("Precio Unitario", Font9White)) With {
        .Border = 0,
        .HorizontalAlignment = PdfPCell.ALIGN_RIGHT,
        .BackgroundColor = New iTextSharp.text.BaseColor(23, 162, 184)
    }
        TableGeneralConceptos.AddCell(ColInfoConceptos)

        ColInfoConceptos = New PdfPCell(New Phrase("Importe", Font9White)) With {
        .Border = 0,
        .HorizontalAlignment = PdfPCell.ALIGN_RIGHT,
        .BackgroundColor = New iTextSharp.text.BaseColor(23, 162, 184)
    }
        TableGeneralConceptos.AddCell(ColInfoConceptos)

        ColInfoConceptos = New PdfPCell(New Phrase("IVA", Font9White)) With {
        .Border = 0,
        .HorizontalAlignment = PdfPCell.ALIGN_RIGHT,
        .BackgroundColor = New iTextSharp.text.BaseColor(23, 162, 184)
        }

        TableGeneralConceptos.AddCell(ColInfoConceptos)





        Dim contenido As String = String.Empty


        CONTE = ConsultaSql("SELECT * FROM PAGOTROS WHERE SERIE='" & Serie & "' AND recibo=" & folio).ExecuteReader


        While CONTE.Read()

            contenido = contenido & CONTE("Cantidad") & "|"

            ColInfoConceptos = New PdfPCell(New Phrase(CONTE("Cantidad"), Font12)) With {
        .Border = 0,
        .HorizontalAlignment = PdfPCell.ALIGN_RIGHT
}

            contenido = contenido & CONTE("CONCEPTO") & "|"
            TableGeneralConceptos.AddCell(ColInfoConceptos)

            ColInfoConceptos = New PdfPCell(New Phrase(CONTE("Concepto"), Font12)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }


            contenido = contenido & CONTE("importe") & "|"
            TableGeneralConceptos.AddCell(ColInfoConceptos)


            Dim monto As Decimal = CONTE("MONTO")
            Dim montoMoneda As Decimal = monto.ToString("C")
            ColInfoConceptos = New PdfPCell(New Phrase(montoMoneda, Font12)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_RIGHT
        }


            TableGeneralConceptos.AddCell(ColInfoConceptos)


            Dim importe As Decimal = CONTE("IMPORTE")
            Dim importeMoneda As Decimal = importe.ToString("C")
            ColInfoConceptos = New PdfPCell(New Phrase(importeMoneda, Font12)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_RIGHT
        }


            TableGeneralConceptos.AddCell(ColInfoConceptos)


            Dim monotoIVA As Decimal = CONTE("MontoIVA")
            Dim montoIVAMoneda As Decimal = monotoIVA.ToString("C")
            ColInfoConceptos = New PdfPCell(New Phrase(montoIVAMoneda, Font12)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_RIGHT
        }


            TableGeneralConceptos.AddCell(ColInfoConceptos)

        End While






        'Tabla Totales 1
        Dim TableTotal1 As PdfPTable = New PdfPTable(2)
        TableTotal1.WidthPercentage = 100
        TableTotal1.DefaultCell.Border = BorderStyle.None
        Dim widthsTotal1 As Single() = New Single() {450.0F, 100.0F}
        TableTotal1.SetWidths(widthsTotal1)




        Dim ColTotales = New PdfPCell(New Phrase("SUBTOTAL", Font12)) With {
        .Border = 1,
        .HorizontalAlignment = PdfPCell.ALIGN_RIGHT
    }
        TableTotal1.AddCell(ColTotales)



        Dim subtotal As Decimal
        subtotal = DATOS("pagos")
        ColTotales = New PdfPCell(New Phrase(subtotal.ToString("C"), Font12)) With {
        .Border = 1,
        .HorizontalAlignment = PdfPCell.ALIGN_RIGHT
    }
        TableTotal1.AddCell(ColTotales)



        Dim iva As Decimal
        iva = DATOS("IVA")
        ColTotales = New PdfPCell(New Phrase("IVA", Font12)) With {
        .Border = 0,
        .HorizontalAlignment = PdfPCell.ALIGN_RIGHT
    }
        TableTotal1.AddCell(ColTotales)



        ColTotales = New PdfPCell(New Phrase(iva.ToString("C"), Font12)) With {
        .Border = 0,
        .HorizontalAlignment = PdfPCell.ALIGN_RIGHT
    }
        TableTotal1.AddCell(ColTotales)


        Dim ColTotales2 = New PdfPCell(New Phrase("TOTAL", Font12)) With {
        .Border = 1,
        .HorizontalAlignment = PdfPCell.ALIGN_RIGHT
    }
        TableTotal1.AddCell(ColTotales2)



        Dim total As Decimal
        total = DATOS("total")
        ColTotales2 = New PdfPCell(New Phrase(total.ToString("C"), Font12)) With {
        .Border = 1,
        .HorizontalAlignment = PdfPCell.ALIGN_RIGHT
    }
        TableTotal1.AddCell(ColTotales2)



        TableTotal1.AddCell(ColTotales)








        Dim TableTotalLetra As PdfPTable = New PdfPTable(1)
        TableTotalLetra.WidthPercentage = 100
        TableTotalLetra.DefaultCell.Border = BorderStyle.None
        Dim widthsTotalLet As Single() = New Single() {900.0F}
        TableTotalLetra.SetWidths(widthsTotalLet)



        Dim ColTotLetra = New PdfPCell(New Phrase(ConvertCurrencyToSpanish(total, "Pesos"), Font9))
        ColTotLetra.Border = 1
        ColTotLetra.HorizontalAlignment = PdfPCell.ALIGN_LEFT
        TableTotalLetra.AddCell(ColTotLetra)





        pdfDoc.Add(TableGeneralConceptos)
        pdfDoc.Add(TableTotalLetra)
        pdfDoc.Add(TableTotal1)








        Dim cadena As String = "|1.0|" & Nombre & "|" & Serie & "|" & folio & "|" & DATOS("ESUSUARIO") & "|" & subtotal & "|" & iva & "|" & total & contenido



        Dim TableCadena As PdfPTable = New PdfPTable(1)
        TableCadena.WidthPercentage = 100
        TableCadena.DefaultCell.Border = BorderStyle.None
        Dim widthsCadena As Single() = New Single() {1000.0F}
        TableCadena.SetWidths(widthsCadena)



        Dim ColCadena = New PdfPCell(New Phrase($"{cadena}", Font9))
        ColCadena.Border = 1
        ColCadena.HorizontalAlignment = PdfPCell.ALIGN_LEFT
        TableCadena.AddCell(ColCadena)





        Dim Tablevalidacion As PdfPTable = New PdfPTable(2)
        Tablevalidacion.WidthPercentage = 100
        Tablevalidacion.PaddingTop = 40
        Tablevalidacion.DefaultCell.Border = BorderStyle.None
        Dim widthsTotalval As Single() = New Single() {120.0F, 500.0F}
        Tablevalidacion.SetWidths(widthsTotalval)



        Dim codigoQR = New StringBuilder()
        codigoQR.Append(cadena)

        Dim pdfCodigoQR = New BarcodeQRCode(codigoQR.ToString(), 1, 1, New Dictionary(Of iTextSharp.text.pdf.qrcode.EncodeHintType, System.Object))
        Dim img As Image = pdfCodigoQR.GetImage()
        img.SpacingAfter = 0.0F
        img.SpacingBefore = 0.0F
        img.BorderWidth = 1.0F
        img.HasAbsolutePosition()
        'img.ScalePercent(100, 78)
        Tablevalidacion.AddCell(img)




        ColTotLetra = New PdfPCell(New Phrase($"Artículo 115.- Es obligatoria la instalación de aparatos medidores para la verificación del consumo de agua, estos deben instalarse en lugares donde el personal del prestador del servicio pueda efectuar las lecturas sin necesidad de introducirse al predio, llevar a cabo las pruebas de funcionamiento de los aparatos, realizar las suspensiones del servicio y otras actividades que sean necesarias en la prestación del servicio. Los usuarios, bajo su estricta responsabilidad, cuidarán que no se deterioren los medidores. Cuando las tomas y medidores no estén al acceso del personal, los consumos se estimarán de acuerdo con esta Ley y el servicio se suspenderá desde pozo de banqueta o red de distribución, según sea el caso, hasta que se dé cumplimiento a lo anterior", Font9))
        ColTotLetra.Border = 0
        ColTotLetra.HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED
        Tablevalidacion.AddCell(ColTotLetra)




        Dim TableFirmas As PdfPTable = New PdfPTable(3)
        TableFirmas.WidthPercentage = 100
        TableFirmas.DefaultCell.Border = BorderStyle.None
        Dim widthsFirmas As Single() = New Single() {300.0F, 300.0F, 300.0F}
        TableFirmas.SetWidths(widthsFirmas)



        Dim ColFirmas = New PdfPCell(New Phrase($" ", Font9))
        ColFirmas.Border = 0
        ColFirmas.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        TableFirmas.AddCell(ColFirmas)


        ColFirmas = New PdfPCell(New Phrase($"Firma cajero", Font9))
        ColFirmas.Border = 1
        ColFirmas.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        TableFirmas.AddCell(ColFirmas)


        ColFirmas = New PdfPCell(New Phrase($" ", Font9))
        ColFirmas.Border = 0
        ColFirmas.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        TableFirmas.AddCell(ColFirmas)





        Dim TableLeyenda As PdfPTable = New PdfPTable(1)
        TableLeyenda.WidthPercentage = 100
        TableTotalLetra.DefaultCell.Border = BorderStyle.None
        Dim widthsLeyenda As Single() = New Single() {900.0F}
        TableLeyenda.SetWidths(widthsLeyenda)



        Dim ColLeyenda = New PdfPCell(New Phrase($"¡GRACIAS POR SU PAGO!", Font9))
        ColLeyenda.Border = 0
        ColLeyenda.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        TableLeyenda.AddCell(ColLeyenda)








        pdfDoc.Add(TableVacio)
        pdfDoc.Add(TableVacio)
        pdfDoc.Add(TableCadena)

        pdfDoc.Add(TableVacio)
        pdfDoc.Add(TableVacio)

        pdfDoc.Add(Tablevalidacion)

        pdfDoc.Add(TableVacio)
        pdfDoc.Add(TableVacio)
        pdfDoc.Add(TableVacio)
        pdfDoc.Add(TableVacio)
        pdfDoc.Add(TableFirmas)
        pdfDoc.Add(TableVacio)
        pdfDoc.Add(TableVacio)
        pdfDoc.Add(TableVacio)
        pdfDoc.Add(TableLeyenda)


        pdfDoc.Close()


        If preview = True Then
            Try
                Dim psi As New ProcessStartInfo(cadenafolder & "\recibo_" & Serie & folio & ".pdf")
                'psi.WorkingDirectory = cadenafolder & "\factura\" + nombresespacios

                psi.WindowStyle = ProcessWindowStyle.Hidden
                Dim p As Process = Process.Start(psi)
            Catch ex As Exception
                MessageBox.Show("Error al visualizar el pdf, posiblemente el archivo este en uso, cierrelo antes de generarlo nuevamente" & ex.Message)
            End Try
        Else

            For index As Integer = 1 To 2



                Dim gsProcessInfo As ProcessStartInfo
                Dim gsProcess As Process
                gsProcessInfo = New ProcessStartInfo()
                gsProcessInfo.Verb = "Print"
                gsProcessInfo.WindowStyle = ProcessWindowStyle.Hidden
                gsProcessInfo.FileName = cadenafolder & "\recibo_" & Serie & folio & ".pdf"
                ' gsProcessInfo.Arguments = """" & nombreImpresora & """"
                gsProcess = Process.Start(gsProcessInfo)
                gsProcess.WaitForInputIdle(2200)
                gsProcess.Close()

            Next

        End If

    End Sub

End Class

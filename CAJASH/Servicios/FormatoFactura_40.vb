Imports System.IO
Imports System.Text
Imports System.Xml
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class FormatoFactura_40


    Public Sub GenerarPDFFactura_CFDI4(serieFacturaP As String, folioFacturaP As Integer, cuentaUsuarioP As String, reimpresionPDF As Boolean)


        Dim cadenaOriginalCertificado As String = ""
        Dim rutaPDF As String

        ' Obtener XML
        Dim archivoXMLFactura As XmlDocument = ObtenerDatosXML(serieFacturaP, folioFacturaP)


        ' Implementar clase que leera el XML
        Dim objXMLReader As New XMLReaderFactura40()
        Dim datosCFDI40_XML As NodoXMLCFDI_40 = objXMLReader.ObtenerDatosFactura40_DesdeXML(archivoXMLFactura)
        Dim datosFiscalesUsuario As New DatosFiscalesUsuario()
        Dim imgQR As Image

        ' Crear el documento PDF

        'CrearCarpetasPDF(reimpresionPDF, datosComplementoXML.Receptor.Nombre)

        ' Formato provisional, débido a  la pronta entrega del módulo

        Try




            If reimpresionPDF = True Then


                If Not My.Computer.FileSystem.DirectoryExists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\FacturasReimpresas" & Year(Now) & acompletacero(Month(Now).ToString(), 2).Trim) Then

                    My.Computer.FileSystem.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\FacturasReimpresas" & Year(Now) & acompletacero(Month(Now).ToString(), 2).Trim)

                End If


            Else


                If Not My.Computer.FileSystem.DirectoryExists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\Facturas\" & Year(Now) & acompletacero(Month(Now).ToString(), 2).Trim) Then

                    My.Computer.FileSystem.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\Facturas\" & Year(Now) & acompletacero(Month(Now).ToString(), 2).Trim)
                End If

            End If


            'Dar propiedades al Documento
            Dim pdfDoc As New Document(iTextSharp.text.PageSize.LETTER, 15.0F, 15.0F, 30.0F, 30.0F)


            Dim colordefinido = New Clscolorreporte()
            colordefinido.ClsColoresReporte(My.Settings.colorfactura)


            datosFiscalesUsuario = ObtenerDatosFiscales(datosCFDI40_XML.Receptor.Nombre, cuentaUsuarioP)



            If reimpresionPDF = True Then

                Dim cadenaFolderComplementoReimpreso As String = (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\FacturasReimpresas" & Year(Now) & acompletacero(Month(Now).ToString(), 2).Trim)

                Dim pdfWrite As PdfWriter = PdfWriter.GetInstance(pdfDoc, New System.IO.FileStream(cadenaFolderComplementoReimpreso & "\Factura_" & datosCFDI40_XML.Serie & datosCFDI40_XML.Folio & ".pdf", FileMode.Create))

                rutaPDF = $"{cadenaFolderComplementoReimpreso}\Factura_{datosCFDI40_XML.Serie}{datosCFDI40_XML.Folio}.pdf"

                objXMLReader.CrearArchivoXML(datosCFDI40_XML.Serie, datosCFDI40_XML.Folio, cadenaFolderComplementoReimpreso)

            Else

                Dim cadenafolderComplemento As String = (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\Facturas\" & Year(Now) & acompletacero(Month(Now).ToString(), 2).Trim)

                Dim pdfWrite As PdfWriter = PdfWriter.GetInstance(pdfDoc, New System.IO.FileStream(cadenafolderComplemento & "\Factura_" & datosCFDI40_XML.Serie & datosCFDI40_XML.Folio & ".pdf", FileMode.Create))

                rutaPDF = $"{cadenafolderComplemento}\Factura_{datosCFDI40_XML.Serie}{datosCFDI40_XML.Folio}.pdf"

                objXMLReader.CrearArchivoXML(datosCFDI40_XML.Serie, datosCFDI40_XML.Folio, cadenafolderComplemento)

            End If

            'Ejecutar método para crear el XML





            'Formato de letras
            Dim Font8Normal As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.NORMAL))
            Dim Font7Normal As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 7, iTextSharp.text.Font.NORMAL))
            Dim Font6Normal As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 6, iTextSharp.text.Font.NORMAL))
            Dim Font8Bold As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.BOLD))
            Dim Font13Bold As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 13, iTextSharp.text.Font.BOLD))
            Dim Font12Bold As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 12, iTextSharp.text.Font.BOLD))
            Dim Font11Bold As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 11, iTextSharp.text.Font.BOLD))
            Dim Font10Bold As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.BOLD))
            Dim Font9Bold As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 9, iTextSharp.text.Font.BOLD))
            Dim Font9 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 9, iTextSharp.text.Font.BOLD))
            Dim Font10Normal As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.NORMAL))
            Dim Font9Normal As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 9, iTextSharp.text.Font.NORMAL))
            Dim Font8WhiteBold As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.WHITE))
            Dim FontCurier7Bold As New Font(FontFactory.GetFont(FontFactory.COURIER, 7, iTextSharp.text.Font.BOLD))


            'abrimos el pdf para comenzar a escribir en el, al terminar cerramos
            pdfDoc.Open()

            'Tabla Vacia

            Dim TableVacia As PdfPTable = New PdfPTable(1)
            TableVacia.DefaultCell.Border = BorderStyle.None
            TableVacia.WidthPercentage = 1000

            Dim widthsVac As Single() = New Single() {1000.0F}
            TableVacia.SetWidths(widthsVac)

            Dim ColVacia = New PdfPCell(New Phrase(" ", Font8Normal))
            ColVacia.Border = 0
            ColVacia.HorizontalAlignment = PdfPCell.ALIGN_CENTER

            TableVacia.AddCell(ColVacia)


            'Tabla encabezado

            Dim Table1 As PdfPTable = New PdfPTable(3)
            Table1.DefaultCell.Border = BorderStyle.None
            Dim Col1 As PdfPCell
            'Dim ILine As Integer
            'Dim iFila As Integer
            Table1.WidthPercentage = 100

            Dim widths As Single() = New Single() {140.0F, 420, 300.0F}
            Table1.SetWidths(widths)

            'Encabezado

            Dim imagenBMP As iTextSharp.text.Image
            imagenBMP = iTextSharp.text.Image.GetInstance(LOGOBYTE)
            imagenBMP.ScaleToFit(80.0F, 70.0F)

            imagenBMP.Border = 0

            Table1.AddCell(imagenBMP)

            Dim Tabledireccion As PdfPTable = New PdfPTable(1)
            Col1 = New PdfPCell(New Phrase(Empresa, Font12Bold))
            Col1.Border = 0
            Col1.HorizontalAlignment = PdfPCell.ALIGN_CENTER

            Tabledireccion.AddCell(Col1)

            Dim DIRECCIONE As String = $"{Direccion}, {coloniaEMPRESA}, {poblacionEMPRESA}, {Estadoempresa} {vbCrLf} CP. {CODPOSempresa}"
            Dim Col1d = New PdfPCell(New Phrase(DIRECCIONE, Font9Normal))
            Col1d.Border = 0
            Col1d.HorizontalAlignment = PdfPCell.ALIGN_CENTER

            Tabledireccion.AddCell(Col1d)

            'RFCORGANISMO = "CAA0207082R3"

            Dim Col1rfe = New PdfPCell(New Phrase($"RFC. {datosCFDI40_XML.Emisor.Rfc}", Font8Normal))
            Col1rfe.Border = 0
            Col1rfe.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            Tabledireccion.AddCell(Col1rfe)


            Col1rfe = New PdfPCell(New Phrase(" ", Font9))
            Col1rfe.Border = 0
            Col1rfe.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            Tabledireccion.AddCell(Col1rfe)


            Col1rfe = New PdfPCell(New Phrase("FACTURA", Font9Bold))
            Col1rfe.Border = 0
            Col1rfe.HorizontalAlignment = PdfPCell.ALIGN_CENTER

            Tabledireccion.AddCell(Col1rfe)


            Table1.AddCell(Tabledireccion)


            Dim Table2 As PdfPTable = New PdfPTable(2)
            Dim Col10 As PdfPCell
            Dim Col11 As PdfPCell
            Dim Col12 As PdfPCell
            Dim Col13 As PdfPCell
            Dim Col14 As PdfPCell
            Table2.WidthPercentage = 100
            Dim widthsT2 As Single() = New Single() {100, 180.0F}
            Table2.SetWidths(widthsT2)



            Col10 = New PdfPCell(New Phrase("Serie", Font9Bold))
            Col10.Border = 0
            Col10.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table2.AddCell(Col10)


            Col11 = New PdfPCell(New Phrase(datosCFDI40_XML.Serie, Font9Bold))
            Col11.Border = 0
            Col11.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table2.AddCell(Col11)

            Dim Col10f = New PdfPCell(New Phrase("Factura", Font9Bold))
            Col10f.Border = 0
            Col10f.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table2.AddCell(Col10f)


            Col12 = New PdfPCell(New Phrase(datosCFDI40_XML.Folio, Font9Bold))
            Col12.Border = 0
            Col12.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table2.AddCell(Col12)

            Col13 = New PdfPCell(New Phrase("Fecha de comprobante", Font8Normal))
            Col13.Border = 0
            Col13.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table2.AddCell(Col13)

            Col14 = New PdfPCell(New Phrase(datosCFDI40_XML.TimbreFiscalDigital.FechaTimbrado, Font8Normal))
            Col14.Border = 0
            Col14.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table2.AddCell(Col14)


            Dim ColDC1 = New PdfPCell(New Phrase("UUID", Font8Normal))
            ColDC1.Border = 0
            ColDC1.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table2.AddCell(ColDC1)


            Dim ColDC2 = New PdfPCell(New Phrase(datosCFDI40_XML.TimbreFiscalDigital.UUID, Font6Normal))
            ColDC2.Border = 0
            ColDC2.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table2.AddCell(ColDC2)

            Dim ColDC3 = New PdfPCell(New Phrase("Certificado Emisor", Font8Normal))
            ColDC3.Border = 0
            ColDC3.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table2.AddCell(ColDC3)

            Dim ColDC4 = New PdfPCell(New Phrase(datosCFDI40_XML.TimbreFiscalDigital.NoCertificadoSAT, Font8Normal))
            ColDC4.Border = 0
            ColDC4.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table2.AddCell(ColDC4)


            Dim ColDC7 = New PdfPCell(New Phrase("Certificado Sat ", Font8Normal))
            ColDC7.Border = 0
            ColDC7.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table2.AddCell(ColDC7)

            Dim ColDC8 = New PdfPCell(New Phrase(datosCFDI40_XML.TimbreFiscalDigital.NoCertificadoSAT, Font8Normal))
            ColDC8.Border = 0
            ColDC8.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table2.AddCell(ColDC8)

            Dim ColDCFP = New PdfPCell(New Phrase("Forma de Pago ", Font8Normal))
            ColDCFP.Border = 0
            ColDCFP.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table2.AddCell(ColDCFP)


            Dim ColDC11 = New PdfPCell(New Phrase(New decodificadorSAT().getFormapago(datosCFDI40_XML.FormaPago), Font8Normal))
            ColDC11.Border = 0
            ColDC11.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table2.AddCell(ColDC11)




            Dim ColDCMeP = New PdfPCell(New Phrase("Método de Pago ", Font8Normal))
            ColDCMeP.Border = 0
            ColDCMeP.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table2.AddCell(ColDCMeP)

            Dim ColDCMeP2 = New PdfPCell(New Phrase(New decodificadorSAT().getMetodo(datosCFDI40_XML.MetodoPago), Font8Normal))
            ColDCMeP2.Border = 0
            ColDCMeP2.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table2.AddCell(ColDCMeP2)

            Dim ColDCUsoCFDI = New PdfPCell(New Phrase("Uso CFDI ", Font8Normal))
            ColDCUsoCFDI.Border = 0
            ColDCUsoCFDI.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table2.AddCell(ColDCUsoCFDI)

            Dim ColDCUsoCFDI2 = New PdfPCell(New Phrase(New decodificadorSAT().getUso(datosCFDI40_XML.Receptor.UsoCFDI), Font8Normal))
            ColDCUsoCFDI2.Border = 0
            ColDCUsoCFDI2.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table2.AddCell(ColDCUsoCFDI2)

            Table1.AddCell(Table2)



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

            ColDN = New PdfPCell(New Phrase(datosCFDI40_XML.Receptor.Nombre, Font8Bold))
            ColDN.Border = 0
            ColDN.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table4.AddCell(ColDN)

            ColDN1 = New PdfPCell(New Phrase(datosCFDI40_XML.Receptor.Rfc, Font8Bold))
            ColDN1.Border = 0
            ColDN1.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table4.AddCell(ColDN1)

            ColDN2 = New PdfPCell(New Phrase($"{datosFiscalesUsuario.calleUsuario} {datosFiscalesUsuario.numInteriorUsuario}  {datosFiscalesUsuario.numExteriorUsuario}", Font8Normal))
            ColDN2.Border = 0
            ColDN2.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table4.AddCell(ColDN2)

            ColDN3 = New PdfPCell(New Phrase("Contrato: " & cuentaUsuarioP, Font8Normal))
            ColDN3.Border = 0
            ColDN3.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table4.AddCell(ColDN3)

            ColDN4 = New PdfPCell(New Phrase($"{datosFiscalesUsuario.poblacionUsuario}, {datosFiscalesUsuario.delegacionUsuario}, {datosFiscalesUsuario.estadoUsuario}, CP {datosFiscalesUsuario.CPUsuario}", Font8Normal))
            ColDN4.Border = 0
            ColDN4.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table4.AddCell(ColDN4)

            ColDN5 = New PdfPCell(New Phrase("Tipo: " & datosFiscalesUsuario.tipoUsuario, Font8Normal))
            ColDN5.Border = 0
            ColDN5.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table4.AddCell(ColDN5)

            'esusua = recibo.esusuario

            ColDN6 = New PdfPCell(New Phrase("", Font8Normal))
            ColDN6.Border = 0
            ColDN6.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table4.AddCell(ColDN6)

            ColDN7 = New PdfPCell(New Phrase(" ", Font9))
            ColDN7.Border = 0
            ColDN7.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table4.AddCell(ColDN7)

            ColDN8 = New PdfPCell(New Phrase(" ", Font9))
            ColDN8.Border = 0
            ColDN8.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table4.AddCell(ColDN8)




            Dim Table5 As PdfPTable = New PdfPTable(2)
            Dim Col51 As PdfPCell
            Dim Col52 As PdfPCell
            Dim Col53 As PdfPCell
            Dim Col54 As PdfPCell

            Table5.WidthPercentage = 100
            Dim widthsT5 As Single() = New Single() {50.0F, 400.0F}

            Table5.SetWidths(widthsT5)

            Col51 = New PdfPCell(New Phrase("", Font8Normal))
            Col51.Border = 0
            Col51.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table5.AddCell(Col51)

            Col52 = New PdfPCell(New Phrase("", Font8Normal))
            Col52.Border = 0
            Col52.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table5.AddCell(Col52)

            Col53 = New PdfPCell(New Phrase(" ", Font8Normal))
            Col53.Border = 0
            Col53.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table5.AddCell(Col53)

            Col54 = New PdfPCell(New Phrase(" ", Font8Normal))
            Col54.Border = 0
            Col54.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table5.AddCell(Col54)

            'Encabezado consulta tabla

            Dim TableConceptos As PdfPTable = New PdfPTable(6)
            Dim ColClave As PdfPCell
            Dim ColCantidad As PdfPCell
            Dim ColDescripcionConcepto As PdfPCell
            Dim ColSubTotal As PdfPCell
            Dim ColIVA As PdfPCell
            Dim ColTotal As PdfPCell

            TableConceptos.WidthPercentage = 100
            Dim widthsT6 As Single() = New Single() {65.0F, 45.0F, 290.0F, 100.0F, 60.0F, 100.0F}
            TableConceptos.SetWidths(widthsT6)


            ColClave = New PdfPCell(New Phrase("Clave Prod.", Font8Bold))
            ColClave.Border = 7
            ColClave.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            ColClave.BackgroundColor = colordefinido.color
            TableConceptos.AddCell(ColClave)


            ColCantidad = New PdfPCell(New Phrase("Cantidad", Font8Bold))
            ColCantidad.Border = 3
            ColCantidad.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            ColCantidad.BackgroundColor = colordefinido.color
            TableConceptos.AddCell(ColCantidad)


            ColDescripcionConcepto = New PdfPCell(New Phrase("Concepto", Font8Bold))
            ColDescripcionConcepto.Border = 3
            ColDescripcionConcepto.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            ColDescripcionConcepto.BackgroundColor = colordefinido.color
            TableConceptos.AddCell(ColDescripcionConcepto)


            ColSubTotal = New PdfPCell(New Phrase("Monto", Font8Bold))
            ColSubTotal.Border = 3
            ColSubTotal.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            ColSubTotal.BackgroundColor = colordefinido.color
            TableConceptos.AddCell(ColSubTotal)

            ColIVA = New PdfPCell(New Phrase("Impuesto", Font8Bold))
            ColIVA.Border = 3
            ColIVA.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            ColIVA.BackgroundColor = colordefinido.color
            TableConceptos.AddCell(ColIVA)


            ColTotal = New PdfPCell(New Phrase("Importe", Font8Bold))
            ColTotal.Border = 11
            ColTotal.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            ColTotal.BackgroundColor = colordefinido.color
            TableConceptos.AddCell(ColTotal)



            For Each datoConcepto As Concepto In datosCFDI40_XML.Conceptos


                Dim valorUnitarioConcepto As Decimal = 0.0
                Dim importeConcepto As Decimal = 0.0
                Dim conceptoIVA As String = ""


                valorUnitarioConcepto = datoConcepto.ValorUnitario
                importeConcepto = datoConcepto.Importe
                conceptoIVA = datoConcepto.ObjetoImp

                ColClave = New PdfPCell(New Phrase($"{datoConcepto.ClaveUnidad}|{datoConcepto.ClaveProdServ}", Font8Normal))
                ColClave.Border = 0
                ColClave.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                ColClave.BackgroundColor = colordefinido.color
                TableConceptos.AddCell(ColClave)


                ColCantidad = New PdfPCell(New Phrase(datoConcepto.Cantidad, Font8Normal))
                ColCantidad.Border = 0
                ColCantidad.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ColCantidad.BackgroundColor = colordefinido.color
                TableConceptos.AddCell(ColCantidad)


                ColDescripcionConcepto = New PdfPCell(New Phrase(datoConcepto.Descripcion, Font8Normal))
                ColDescripcionConcepto.Border = 0
                ColDescripcionConcepto.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                ColDescripcionConcepto.BackgroundColor = colordefinido.color
                TableConceptos.AddCell(ColDescripcionConcepto)


                ColSubTotal = New PdfPCell(New Phrase(valorUnitarioConcepto.ToString("C"), Font8Normal))
                ColSubTotal.Border = 0
                ColSubTotal.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ColSubTotal.BackgroundColor = colordefinido.color
                TableConceptos.AddCell(ColSubTotal)


                If conceptoIVA = "01" Then

                    Dim importeImpuesto As Decimal = 0.0



                    ColIVA = New PdfPCell(New Phrase(importeImpuesto.ToString("C"), Font8Normal))
                    ColIVA.Border = 0
                    ColIVA.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    ColIVA.BackgroundColor = colordefinido.color
                    TableConceptos.AddCell(ColIVA)

                Else

                    ' ACCEDER A NODOS IMPUESTOS DEL CONCEPTO
                    Dim impuestosDetalles As String = ""
                    Dim importeImpuesto As Decimal = 0.0



                    ' Recorrer Traslados
                    For Each traslado As Impuesto In datoConcepto.Traslados

                        importeImpuesto = traslado.Importe

                        impuestosDetalles &= $"{importeImpuesto.ToString("C")}"

                    Next

                    ' Recorrer Retenciones
                    For Each retencion As Impuesto In datoConcepto.Retenciones

                        importeImpuesto = retencion.Importe

                        impuestosDetalles &= $"{importeImpuesto.ToString("C")}"

                    Next

                    ColIVA = New PdfPCell(New Phrase(impuestosDetalles, Font8Normal))
                    ColIVA.Border = 0
                    ColIVA.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    ColIVA.BackgroundColor = colordefinido.color
                    TableConceptos.AddCell(ColIVA)

                End If




                ColTotal = New PdfPCell(New Phrase(importeConcepto.ToString("C"), Font8Normal))
                ColTotal.Border = 0
                ColTotal.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                ColTotal.BackgroundColor = colordefinido.color
                TableConceptos.AddCell(ColTotal)


            Next


            Dim TableEspacio As PdfPTable = New PdfPTable(1)
            Dim ColEsp As PdfPCell
            TableEspacio.WidthPercentage = 100
            Dim widthsTE As Single() = New Single() {1000.0F}
            TableEspacio.SetWidths(widthsTE)

            ColEsp = New PdfPCell(New Phrase(" ", Font8Normal))
            ColEsp.Border = 0
            ColEsp.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            TableEspacio.AddCell(ColEsp)



            'iva = Decimal.Parse(iva).ToString("C")

            Dim impuestoFactura As Decimal = datosCFDI40_XML.Total - datosCFDI40_XML.SubTotal

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

            Col71 = New PdfPCell(New Phrase(" ", Font8Normal))
            Col71.Border = 0
            Col71.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table7.AddCell(Col71)

            Col72 = New PdfPCell(New Phrase("", Font8Normal))
            Col72.Border = 0
            Col72.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            Table7.AddCell(Col72)

            Col73 = New PdfPCell(New Phrase("SUBTOTAL: ", Font9Bold))
            Col73.Border = 0
            Col73.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table7.AddCell(Col73)


            'subtotal = Decimal.Parse(subtotal).ToString("C")
            Col74 = New PdfPCell(New Phrase(datosCFDI40_XML.SubTotal.ToString("C"), Font9))
            Col74.Border = 1
            Col74.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table7.AddCell(Col74)
            Table7.CompleteRow()

            Col71 = New PdfPCell(New Phrase(" ", Font9))
            Col71.Border = 0
            Col71.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table7.AddCell(Col71)

            Col72 = New PdfPCell(New Phrase("", Font9))
            Col72.Border = 0
            Col72.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table7.AddCell(Col72)

            Col73 = New PdfPCell(New Phrase("IVA: ", Font9))
            Col73.Border = 0
            Col73.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table7.AddCell(Col73)




            Col74 = New PdfPCell(New Phrase(impuestoFactura.ToString("C"), Font9Bold))
            Col74.Border = 0
            Col74.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table7.AddCell(Col74)

            Table7.CompleteRow()

            Col71 = New PdfPCell(New Phrase(" ", Font9))
            Col71.Border = 0
            Col71.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table7.AddCell(Col71)

            Col72 = New PdfPCell(New Phrase("", Font9))
            Col72.Border = 0
            Col72.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table7.AddCell(Col72)

            Col73 = New PdfPCell(New Phrase("TOTAL: ", Font9Bold))
            Col73.Border = 0
            Col73.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            Table7.AddCell(Col73)



            Col74 = New PdfPCell(New Phrase(datosCFDI40_XML.Total.ToString("C"), Font9))
            Col74.Border = 1
            Col74.HorizontalAlignment = PdfPCell.ALIGN_RIGHT

            Table7.AddCell(Col74)


            Col75 = New PdfPCell(New Phrase(" ", Font9))
            Col75.Border = 0
            Col75.HorizontalAlignment = PdfPCell.ALIGN_RIGHT

            Table7.AddCell(Col75)

            Col76 = New PdfPCell(New Phrase(" ", Font9))
            Col76.Border = 0
            Col76.HorizontalAlignment = PdfPCell.ALIGN_RIGHT

            Table7.AddCell(Col76)

            Col77 = New PdfPCell(New Phrase(" ", Font9))
            Col77.Border = 0
            Col77.HorizontalAlignment = PdfPCell.ALIGN_RIGHT

            Table7.AddCell(Col77)

            Col78 = New PdfPCell(New Phrase(" ", Font9))
            Col78.Border = 0
            Col78.HorizontalAlignment = PdfPCell.ALIGN_RIGHT

            Table7.AddCell(Col78)




            Dim Table8 As PdfPTable = New PdfPTable(1)
            Dim Col81 As PdfPCell
            Dim Col82 As PdfPCell
            Dim Col83 As PdfPCell

            Table8.WidthPercentage = 100
            Dim widthsT8 As Single() = New Single() {1000.0F}
            Table8.SetWidths(widthsT8)

            Col81 = New PdfPCell(New Phrase(ConvertCurrencyToSpanish(datosCFDI40_XML.Total, "Pesos"), Font9Bold))
            Col81.Border = 0
            Col81.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table8.AddCell(Col81)


            Col83 = New PdfPCell(New Phrase(" ", Font9))
            Col83.Border = 0
            Col83.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            Table8.AddCell(Col83)

            Dim Table9 As PdfPTable = New PdfPTable(2)
            Dim Col91 As PdfPCell
            Dim Col92 As PdfPCell
            Dim Col93 As PdfPCell
            Dim Col94 As PdfPCell
            Dim Col95 As PdfPCell
            Dim Col96 As PdfPCell
            Dim Col97 As PdfPCell
            Dim Col98 As PdfPCell
            Dim Col99 As PdfPCell
            Dim Col910 As PdfPCell
            Dim Col911 As PdfPCell
            Dim Col912 As PdfPCell

            Table9.WidthPercentage = 100
            Dim widthsT9 As Single() = New Single() {500.0F, 100.0F}
            Table9.SetWidths(widthsT9)



            Dim TableSellos As PdfPTable = New PdfPTable(2)


            cadenaOriginalCertificado = ""
            cadenaOriginalCertificado = obtenerCampo($"SELECT CADENA FROM ENCFAC WHERE SERIE = '{serieFacturaP}' AND NUMERO = {folioFacturaP}", "CADENA")

            TableSellos.WidthPercentage = 100
            Dim widthsTIT2 As Single() = New Single() {80.0F, 200.0F}
            TableSellos.SetWidths(widthsTIT2)

            Col92 = New PdfPCell(New Phrase("Sello CFDI   ", Font9Bold))
            Col92.Border = 0
            Col92.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            TableSellos.AddCell(Col92)

            Col93 = New PdfPCell(New Phrase(datosCFDI40_XML.TimbreFiscalDigital.SelloCFD, Font8Normal))
            Col93.Border = 0
            Col93.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            TableSellos.AddCell(Col93)

            Col94 = New PdfPCell(New Phrase(" ", Font9))
            Col94.Border = 0
            Col94.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            TableSellos.AddCell(Col94)

            Col95 = New PdfPCell(New Phrase(" ", Font9))
            Col95.Border = 0
            Col95.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            TableSellos.AddCell(Col95)

            Col96 = New PdfPCell(New Phrase("Sello SAT   ", Font9Bold))
            Col96.Border = 0
            Col96.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            TableSellos.AddCell(Col96)

            Col97 = New PdfPCell(New Phrase(datosCFDI40_XML.TimbreFiscalDigital.SelloSAT, Font8Normal))
            Col97.Border = 0
            Col97.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            TableSellos.AddCell(Col97)

            Col98 = New PdfPCell(New Phrase(" ", Font9))
            Col98.Border = 0
            Col98.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            TableSellos.AddCell(Col98)


            Col99 = New PdfPCell(New Phrase(" ", Font9))
            Col99.Border = 0
            Col99.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            TableSellos.AddCell(Col99)

            Col910 = New PdfPCell(New Phrase("Cadena Original   ", Font9Bold))
            Col910.Border = 0
            'Col910.BackgroundColor()
            Col910.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            TableSellos.AddCell(Col910)

            Col911 = New PdfPCell(New Phrase(cadenaOriginalCertificado, Font8Normal))
            Col911.Border = 0
            Col911.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            TableSellos.AddCell(Col911)

            TableSellos.DefaultCell.Border = BorderStyle.None
            Table9.DefaultCell.Border = BorderStyle.None
            Table9.AddCell(TableSellos)



            ' Generar Código QR


            imgQR = FormarCodigoQR(datosCFDI40_XML)

            'img.ScalePercent(100, 78)
            Table9.AddCell(imgQR)
            'img.Alignment = 6;
            Table9.CompleteRow()

            Dim Table10 As PdfPTable = New PdfPTable(1)
            Dim Col101 As PdfPCell


            Table10.WidthPercentage = 100
            Dim widthsT10 As Single() = New Single() {1000.0F}
            Table10.SetWidths(widthsT10)

            Col101 = New PdfPCell(New Phrase($"ESTE DOCUMENTO ES UNA REPRESENTACION IMPRESA DE UN CFDI {datosCFDI40_XML.Version} EFECTOS FISCALES AL PAGO", Font8Bold))
            Col101.Border = 0
            Col101.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            Table10.AddCell(Col101)





            Dim Table11 As PdfPTable = New PdfPTable(1)
            Dim Col111 As PdfPCell


            Table11.WidthPercentage = 100
            Dim widthsT11 As Single() = New Single() {1000.0F}
            Table11.SetWidths(widthsT11)

            Col111 = New PdfPCell(New Phrase(" ", Font9))
            Col111.Border = 0
            Col111.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            Table11.AddCell(Col111)



            pdfDoc.Add(Table1)
            pdfDoc.Add(TableEspacio)
            pdfDoc.Add(TableEspacio)
            pdfDoc.Add(Table4)
            pdfDoc.Add(Table5)
            pdfDoc.Add(TableEspacio)
            pdfDoc.Add(TableConceptos)
            pdfDoc.Add(Table7)
            pdfDoc.Add(Table8)
            pdfDoc.Add(Table9)
            pdfDoc.Add(TableEspacio)
            pdfDoc.Add(Table10)
            pdfDoc.Add(TableEspacio)
            pdfDoc.Add(TableEspacio)
            pdfDoc.Add(Table11)
            pdfDoc.Add(TableEspacio)
            pdfDoc.Add(TableEspacio)
            pdfDoc.Add(TableEspacio)
            pdfDoc.Add(TableEspacio)
            pdfDoc.Close()


            VisualizarPDF(rutaPDF)

        Catch ex As Exception

            MessageBox.Show($"OCURRIO UN ERROR: {ex.ToString()}")

        End Try

    End Sub


    Public Function FormarCadenaOriginalCertificado(datosCFDI40_XML As NodoXMLCFDI_40) As String

        Try


            Dim cadenaOriginal As New StringBuilder()
            cadenaOriginal.Append("||")
            cadenaOriginal.Append("1.1|")
            cadenaOriginal.Append(datosCFDI40_XML.TimbreFiscalDigital.UUID & "|")
            cadenaOriginal.Append(datosCFDI40_XML.Fecha.ToString() & "|")
            cadenaOriginal.Append(datosCFDI40_XML.TimbreFiscalDigital.SelloSAT & "|")
            cadenaOriginal.Append(datosCFDI40_XML.TimbreFiscalDigital.NoCertificadoSAT & "||")

            Return cadenaOriginal.ToString()

        Catch ex As Exception

            MessageBox.Show("OCURRIO UN ERROR AL FORMAR LA CADENA ORIGINAL DEL CERTIFICADO")
            Return ""

        End Try

    End Function

    Public Function FormarCodigoQR(CFDI40 As NodoXMLCFDI_40) As Image

        Try


            Dim codigoQR = New StringBuilder()
            codigoQR.Append("https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx?id=" & CFDI40.TimbreFiscalDigital.UUID)
            codigoQR.Append("&re=" & CFDI40.Emisor.Rfc) 'RFC del Emisor
            codigoQR.Append("&rr=" & CFDI40.Receptor.Rfc) 'RFC del receptor
            codigoQR.Append("&tt=" & CFDI40.Total) ' Total del comprobante 10 enteros y 6 decimales
            codigoQR.Append("&fe=" & CFDI40.TimbreFiscalDigital.SelloCFD.Substring(CFDI40.TimbreFiscalDigital.SelloCFD.Length - 8, 8))
            Dim pdfCodigoQR = New BarcodeQRCode(codigoQR.ToString(), 1, 1, New Dictionary(Of iTextSharp.text.pdf.qrcode.EncodeHintType, System.Object))
            Dim img As Image = pdfCodigoQR.GetImage()
            img.SpacingAfter = 0.0F
            img.SpacingBefore = 0.0F
            img.BorderWidth = 1.0F
            img.HasAbsolutePosition()

            Return img

        Catch ex As Exception

            MessageBox.Show($"OCURRIO UN ERROR AL FORMAR EL CÓDIGO QR: {ex.ToString()}")
            Return Nothing

        End Try

    End Function

    Public Sub ImprimirFormato(rutaArchivo As String)

        Try
            For index As Integer = 1 To 2 Step 1
                imprimirpdf(rutaArchivo)
            Next
        Catch ex As Exception
            MessageBox.Show("Error al imprimir la factura, puedes buscarla en la reimpresión de facturas. " & ex.ToString())
        End Try

    End Sub

    Public Sub VisualizarPDF(rutaPDFP As String)

        Try
            Dim psi As New ProcessStartInfo(rutaPDFP)
            'psi.WorkingDirectory = cadenafolder & "\factura\" + nombresespacios

            psi.WindowStyle = ProcessWindowStyle.Hidden
            Dim p As Process = Process.Start(psi)


        Catch ex As Exception

            MessageBox.Show("Ocurrio un error al visualizar el pdf" & ex.Message)

        End Try

    End Sub

    Public Sub imprimirpdf(ByVal _pdf As String)

        Try


            Dim psi As New ProcessStartInfo

            psi.UseShellExecute = True

            psi.Verb = "print"

            psi.WindowStyle = ProcessWindowStyle.Hidden


            'psi.Arguments = PrintDialog1.PrinterSettings.PrinterName.ToString()

            psi.FileName = _pdf

            Process.Start(psi)

        Catch ex As Exception

            MessageBox.Show($"OCURRIO UN ERROR: {ex.ToString()}")

        End Try
    End Sub

End Class
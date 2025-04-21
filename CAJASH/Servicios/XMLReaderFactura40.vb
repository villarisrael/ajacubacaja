Imports System.IO
Imports System.Text
Imports System.Xml

Public Class XMLReaderFactura40


    Public Function ObtenerDatosFactura40_DesdeXML(archivoXML As XmlDocument) As NodoXMLCFDI_40

            ' Crear una instancia de la clase datosXML
            Dim datosXML As New NodoXMLCFDI_40()

            Dim nsmgr As New XmlNamespaceManager(archivoXML.NameTable)
            nsmgr.AddNamespace("cfdi", "http://www.sat.gob.mx/cfd/4")
            nsmgr.AddNamespace("tfd", "http://www.sat.gob.mx/TimbreFiscalDigital")


            Try


                ' Obtener los nodos necesarios y asignar valores a las propiedades de datosXML
                Dim comprobanteNode As XmlNode = archivoXML.SelectSingleNode("//cfdi:Comprobante", nsmgr)
                If comprobanteNode IsNot Nothing Then
                    datosXML.Version = comprobanteNode.Attributes("Version")?.Value
                    datosXML.SubTotal = comprobanteNode.Attributes("SubTotal")?.Value
                    datosXML.Descuento = comprobanteNode.Attributes("Descuento")?.Value
                    datosXML.Total = comprobanteNode.Attributes("Total")?.Value
                    datosXML.Exportacion = comprobanteNode.Attributes("Exportacion")?.Value
                    datosXML.Folio = comprobanteNode.Attributes("Folio")?.Value
                    datosXML.FormaPago = comprobanteNode.Attributes("FormaPago")?.Value
                    datosXML.LugarExpedicion = comprobanteNode.Attributes("LugarExpedicion")?.Value
                    datosXML.MetodoPago = comprobanteNode.Attributes("MetodoPago")?.Value
                    datosXML.Fecha = comprobanteNode.Attributes("Fecha")?.Value
                    datosXML.TipoDeComprobante = comprobanteNode.Attributes("TipoDeComprobante")?.Value
                    datosXML.Moneda = comprobanteNode.Attributes("Moneda")?.Value
                    datosXML.Serie = comprobanteNode.Attributes("Serie")?.Value
                    datosXML.Certificado = comprobanteNode.Attributes("Certificado")?.Value
                    datosXML.NoCertificado = comprobanteNode.Attributes("NoCertificado")?.Value
                    datosXML.Sello = comprobanteNode.Attributes("Sello")?.Value

                    ' Leer y asignar valores a las propiedades de los nodos hijos
                    Dim emisorNode As XmlNode = comprobanteNode.SelectSingleNode("cfdi:Emisor", nsmgr)
                    If emisorNode IsNot Nothing Then
                        datosXML.Emisor.RegimenFiscal = emisorNode.Attributes("RegimenFiscal")?.Value
                        datosXML.Emisor.Rfc = emisorNode.Attributes("Rfc")?.Value
                        datosXML.Emisor.Nombre = emisorNode.Attributes("Nombre")?.Value
                    End If


                    Dim receptorNode As XmlNode = comprobanteNode.SelectSingleNode("cfdi:Receptor", nsmgr)
                    If receptorNode IsNot Nothing Then
                        datosXML.Receptor.RegimenFiscalReceptor = receptorNode.Attributes("RegimenFiscalReceptor")?.Value
                        datosXML.Receptor.DomicilioFiscalReceptor = receptorNode.Attributes("DomicilioFiscalReceptor")?.Value
                        datosXML.Receptor.UsoCFDI = receptorNode.Attributes("UsoCFDI")?.Value
                        datosXML.Receptor.Nombre = receptorNode.Attributes("Nombre")?.Value
                        datosXML.Receptor.Rfc = receptorNode.Attributes("Rfc")?.Value
                    End If


                    Dim impuestosNode As XmlNode = comprobanteNode.SelectSingleNode("cfdi:Impuestos", nsmgr)
                If impuestosNode IsNot Nothing Then

                    datosXML.Impuestos.TotalImpuestosTrasladados = impuestosNode.Attributes("TotalImpuestosTrasladados")?.Value
                    datosXML.Impuestos.TotalImpuestosRetenidos = impuestosNode.Attributes("TotalImpuestosRetenidos")?.Value

                    '' Procesar Traslados
                    'Dim trasladosNodeList As XmlNodeList = impuestosNode.SelectNodes("cfdi:Traslados/cfdi:Traslado", nsmgr)

                    'If trasladosNodeList IsNot Nothing Then

                    '    For Each trasladoNode As XmlNode In trasladosNodeList

                    '        Dim traslado As New Impuesto() With {
                    '        .TasaOCuota = trasladoNode.Attributes("TasaOCuota")?.Value,
                    '        .Importe = trasladoNode.Attributes("Importe")?.Value,
                    '        .Impuesto = trasladoNode.Attributes("Impuesto")?.Value,
                    '        .Base = trasladoNode.Attributes("Base")?.Value,
                    '        .TipoFactor = trasladoNode.Attributes("TipoFactor")?.Value
                    '    }
                    '        Concepto.Traslados.Add(traslado)
                    '    Next

                End If

                'Dim complementoNode As XmlNode = comprobanteNode.SelectSingleNode("cfdi:Complemento/pago20:Pagos", nsmgr)
                'If complementoNode IsNot Nothing Then
                '    datosXML.Pagos.Version = complementoNode.Attributes("Version")?.Value

                '    ' Obtener el nodo <pago20:Totales> dentro de <pago20:Pagos>
                '    Dim totalesNode As XmlNode = complementoNode.SelectSingleNode("pago20:Totales", nsmgr)
                '    If totalesNode IsNot Nothing Then
                '        ' Obtener los valores de los atributos MontoTotalPagos, TotalTrasladosBaseIVA16 y TotalTrasladosImpuestoIVA16
                '        datosXML.Pagos.Totales.MontoTotalPagos = totalesNode.Attributes("MontoTotalPagos")?.Value
                '        datosXML.Pagos.Totales.TotalTrasladosBaseIVA16 = totalesNode.Attributes("TotalTrasladosBaseIVA16")?.Value
                '        datosXML.Pagos.Totales.TotalTrasladosImpuestoIVA16 = totalesNode.Attributes("TotalTrasladosImpuestoIVA16")?.Value


                '    End If


                '    Dim pagoNode As XmlNode = complementoNode.SelectSingleNode("pago20:Pago", nsmgr)
                '    If pagoNode IsNot Nothing Then
                '        ' Obtener los valores de los atributos MontoTotalPagos, TotalTrasladosBaseIVA16 y TotalTrasladosImpuestoIVA16
                '        datosXML.Pagos.Pago.TipoCambioP = pagoNode.Attributes("TipoCambioP")?.Value
                '        datosXML.Pagos.Pago.FormaDePagoP = pagoNode.Attributes("FormaDePagoP")?.Value
                '        datosXML.Pagos.Pago.Monto = pagoNode.Attributes("Monto")?.Value
                '        datosXML.Pagos.Pago.FechaPago = pagoNode.Attributes("FechaPago")?.Value
                '        datosXML.Pagos.Pago.MonedaP = pagoNode.Attributes("MonedaP")?.Value


                '        ' Obtener todos los nodos <pago20:DoctoRelacionado> dentro de <pago20:Pagos>
                '        Dim doctosRelacionadosNodes As XmlNodeList = pagoNode.SelectNodes("pago20:DoctoRelacionado", nsmgr)
                '        If doctosRelacionadosNodes IsNot Nothing AndAlso doctosRelacionadosNodes.Count > 0 Then
                '            ' Iterar sobre los nodos obtenidos
                '            For Each doctoNode As XmlNode In doctosRelacionadosNodes

                '                ' Obtener los valores de los atributos de cada nodo <pago20:DoctoRelacionado>

                '                datosXML.Pagos.Pago.DoctoRelacionado.ImpSaldoInsoluto = doctoNode.Attributes("ImpSaldoInsoluto")?.Value
                '                datosXML.Pagos.Pago.DoctoRelacionado.NumParcialidad = doctoNode.Attributes("NumParcialidad")?.Value
                '                datosXML.Pagos.Pago.DoctoRelacionado.EquivalenciaDR = doctoNode.Attributes("EquivalenciaDR")?.Value
                '                datosXML.Pagos.Pago.DoctoRelacionado.ObjetoImpDR = doctoNode.Attributes("ObjetoImpDR")?.Value
                '                datosXML.Pagos.Pago.DoctoRelacionado.Folio = doctoNode.Attributes("Folio")?.Value
                '                datosXML.Pagos.Pago.DoctoRelacionado.ImpSaldoAnt = doctoNode.Attributes("ImpSaldoAnt")?.Value
                '                datosXML.Pagos.Pago.DoctoRelacionado.ImpPagado = doctoNode.Attributes("ImpPagado")?.Value
                '                datosXML.Pagos.Pago.DoctoRelacionado.IdDocumento = doctoNode.Attributes("IdDocumento")?.Value
                '                datosXML.Pagos.Pago.DoctoRelacionado.Serie = doctoNode.Attributes("Serie")?.Value
                '                datosXML.Pagos.Pago.DoctoRelacionado.MonedaDR = doctoNode.Attributes("MonedaDR")?.Value


                '            Next




                '        End If

                '        ' Obtener el nodo <pago20:ImpuestosDR>
                '        Dim impuestosDRNode As XmlNode = archivoXML.SelectSingleNode("//pago20:ImpuestosDR", nsmgr)

                '        ' Verificar si se encontró el nodo <pago20:ImpuestosDR>
                '        If impuestosDRNode IsNot Nothing Then
                '            ' Obtener el nodo hijo <pago20:TrasladosDR>
                '            Dim trasladosDRNode As XmlNode = impuestosDRNode.SelectSingleNode("pago20:TrasladosDR", nsmgr)

                '            ' Verificar si se encontró el nodo <pago20:TrasladosDR> dentro de <pago20:ImpuestosDR>
                '            If trasladosDRNode IsNot Nothing Then
                '                ' Obtener el nodo hijo <pago20:TrasladoDR>
                '                Dim trasladoDRNode As XmlNode = trasladosDRNode.SelectSingleNode("pago20:TrasladoDR", nsmgr)

                '                ' Verificar si se encontró el nodo <pago20:TrasladoDR> dentro de <pago20:TrasladosDR>
                '                If trasladoDRNode IsNot Nothing Then
                '                    ' Obtener los atributos del nodo <pago20:TrasladoDR>
                '                    datosXML.Pagos.Pago.DoctoRelacionado.ImpuestosDR.TrasladosDR.TrasladoDR.TasaOCuotaDR = trasladoDRNode.Attributes("TasaOCuotaDR")?.Value
                '                    datosXML.Pagos.Pago.DoctoRelacionado.ImpuestosDR.TrasladosDR.TrasladoDR.ImpuestoDR = trasladoDRNode.Attributes("ImpuestoDR")?.Value
                '                    datosXML.Pagos.Pago.DoctoRelacionado.ImpuestosDR.TrasladosDR.TrasladoDR.ImporteDR = trasladoDRNode.Attributes("ImporteDR")?.Value
                '                    datosXML.Pagos.Pago.DoctoRelacionado.ImpuestosDR.TrasladosDR.TrasladoDR.BaseDR = trasladoDRNode.Attributes("BaseDR")?.Value
                '                    datosXML.Pagos.Pago.DoctoRelacionado.ImpuestosDR.TrasladosDR.TrasladoDR.TipoFactorDR = trasladoDRNode.Attributes("TipoFactorDR")?.Value


                '                End If

                '            End If

                '        End If

                '        ' Obtener el nodo <pago20:ImpuestosDR>
                '        Dim impuestosPNode As XmlNode = archivoXML.SelectSingleNode("//pago20:ImpuestosP", nsmgr)

                '        ' Verificar si se encontró el nodo <pago20:ImpuestosDR>
                '        If impuestosPNode IsNot Nothing Then
                '            ' Obtener el nodo hijo <pago20:TrasladosDR>
                '            Dim impuestosTrasladosPNode As XmlNode = impuestosPNode.SelectSingleNode("pago20:TrasladosP", nsmgr)

                '            ' Verificar si se encontró el nodo <pago20:TrasladosDR> dentro de <pago20:ImpuestosDR>
                '            If impuestosTrasladosPNode IsNot Nothing Then
                '                ' Obtener el nodo hijo <pago20:TrasladoDR>
                '                Dim trasladoPNode As XmlNode = impuestosTrasladosPNode.SelectSingleNode("pago20:TrasladoP", nsmgr)

                '                ' Verificar si se encontró el nodo <pago20:TrasladoDR> dentro de <pago20:TrasladosDR>
                '                If trasladoPNode IsNot Nothing Then
                '                    ' Obtener los atributos del nodo <pago20:TrasladoDR>
                '                    datosXML.Pagos.ImpuestosP.TrasladosP.TrasladoP.TasaOCuotaP = trasladoPNode.Attributes("TasaOCuotaP")?.Value
                '                    datosXML.Pagos.ImpuestosP.TrasladosP.TrasladoP.ImpuestoP = trasladoPNode.Attributes("ImpuestoP")?.Value
                '                    'datosXML.Pagos.Pago.DoctoRelacionado.ImpuestosDR.TrasladosDR.TrasladoDR.ImporteDR = impuestosPNode.Attributes("ImporteDR")?.Value
                '                    'datosXML.Pagos.Pago.DoctoRelacionado.ImpuestosDR.TrasladosDR.TrasladoDR.BaseDR = impuestosPNode.Attributes("BaseDR")?.Value
                '                    'datosXML.Pagos.Pago.DoctoRelacionado.ImpuestosDR.TrasladosDR.TrasladoDR.TipoFactorDR = impuestosPNode.Attributes("TipoFactorDR")?.Value


                '                End If

                '            End If

                '        End If

                '    End If

                Dim timbreNode As XmlNode = comprobanteNode.SelectSingleNode("//tfd:TimbreFiscalDigital", nsmgr)

                    If timbreNode IsNot Nothing Then

                        datosXML.TimbreFiscalDigital.Version = timbreNode.Attributes("Version")?.Value
                        datosXML.TimbreFiscalDigital.UUID = timbreNode.Attributes("UUID")?.Value
                        datosXML.TimbreFiscalDigital.FechaTimbrado = timbreNode.Attributes("FechaTimbrado")?.Value
                        datosXML.TimbreFiscalDigital.SelloCFD = timbreNode.Attributes("SelloCFD")?.Value
                        datosXML.TimbreFiscalDigital.NoCertificadoSAT = timbreNode.Attributes("NoCertificadoSAT")?.Value
                        datosXML.TimbreFiscalDigital.SelloSAT = timbreNode.Attributes("SelloSAT")?.Value

                    End If



                End If



                Dim conceptosNode As XmlNodeList = comprobanteNode.SelectNodes("cfdi:Conceptos/cfdi:Concepto", nsmgr)

                If conceptosNode IsNot Nothing Then

                    datosXML.Conceptos = New List(Of Concepto)()

                    For Each conceptoNode As XmlNode In conceptosNode

                        Dim concepto As New Concepto()
                        concepto.ValorUnitario = conceptoNode.Attributes("ValorUnitario")?.Value
                        concepto.Importe = conceptoNode.Attributes("Importe")?.Value
                        concepto.DescuentoImporte = conceptoNode.Attributes("Descuento")?.Value
                        concepto.Cantidad = conceptoNode.Attributes("Cantidad")?.Value
                        concepto.ClaveProdServ = conceptoNode.Attributes("ClaveProdServ")?.Value
                        concepto.ClaveUnidad = conceptoNode.Attributes("ClaveUnidad")?.Value
                        concepto.Descripcion = conceptoNode.Attributes("Descripcion")?.Value
                        concepto.ObjetoImp = conceptoNode.Attributes("ObjetoImp")?.Value

                        ' Inicializar las listas de impuestos, aunque estén vacías
                        concepto.Traslados = New List(Of Impuesto)()
                        concepto.Retenciones = New List(Of Impuesto)()

                        ' Obtener los nodos de impuestos
                        Dim impuestosNode As XmlNode = conceptoNode.SelectSingleNode("cfdi:Impuestos", nsmgr)
                        If impuestosNode IsNot Nothing Then



                            ' Procesar Traslados
                            Dim trasladosNodeList As XmlNodeList = impuestosNode.SelectNodes("cfdi:Traslados/cfdi:Traslado", nsmgr)

                            If trasladosNodeList IsNot Nothing Then

                                For Each trasladoNode As XmlNode In trasladosNodeList

                                    Dim traslado As New Impuesto() With {
                                .TasaOCuota = trasladoNode.Attributes("TasaOCuota")?.Value,
                                .Importe = trasladoNode.Attributes("Importe")?.Value,
                                .Impuesto = trasladoNode.Attributes("Impuesto")?.Value,
                                .Base = trasladoNode.Attributes("Base")?.Value,
                                .TipoFactor = trasladoNode.Attributes("TipoFactor")?.Value
                            }
                                    concepto.Traslados.Add(traslado)
                                Next

                            End If



                            '    ' Procesar Retenciones
                            '    Dim retencionesNodeList As XmlNodeList = impuestosNode.SelectNodes("cfdi:Retenciones/cfdi:Retencion", nsmgr)
                            '    If retencionesNodeList IsNot Nothing Then
                            '        For Each retencionNode As XmlNode In retencionesNodeList
                            '            Dim retencion As New Impuesto() With {
                            '    .Impuesto = retencionNode.Attributes("Impuesto")?.Value,
                            '    .Importe = retencionNode.Attributes("Importe")?.Value
                            '}
                            '            concepto.Retenciones.Add(retencion)
                            '        Next
                            '    End If

                        End If

                        ' Agregar el concepto a la lista general
                        datosXML.Conceptos.Add(concepto)

                    Next

                End If


                Return datosXML

            Catch ex As Exception

                MessageBox.Show($"Ocurrio un error al obtener los datos del XML: {ex.ToString()}")

            End Try

        End Function


End Class




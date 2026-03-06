Imports System.Xml

Public Class NodoXMLCFDI_40
    Public Property Version As String
    Public Property SubTotal As String
    Public Property Total As String
    Public Property Descuento As String
    Public Property Exportacion As String
    Public Property Folio As String
    Public Property LugarExpedicion As String
    Public Property Fecha As String
    Public Property TipoDeComprobante As String
    Public Property Moneda As String
    Public Property Serie As String
    Public Property Certificado As String
    Public Property NoCertificado As String
    Public Property Sello As String
    Public Property FormaPago As String
    Public Property MetodoPago As String

    Public Property Emisor As Emisor
    Public Property Receptor As Receptor
    Public Property Conceptos As List(Of Concepto)
    Public Property TimbreFiscalDigital As TimbreFiscalDigital
    ' Nueva propiedad para el nodo global de Impuestos
    Public Property Impuestos As ImpuestosFactura

    Public Sub New()
        Emisor = New Emisor()
        Receptor = New Receptor()
        Conceptos = New List(Of Concepto)()
        TimbreFiscalDigital = New TimbreFiscalDigital()
        Impuestos = New ImpuestosFactura()
    End Sub

    ' Método para totalizar los importes de traslados a nivel de concepto
    Public Function TotalizarImportesTraslados() As Decimal
        Dim totalTraslados As Decimal = 0D

        For Each concepto In Conceptos
            If concepto.Traslados IsNot Nothing Then
                For Each traslado In concepto.Traslados
                    Dim importeDecimal As Decimal
                    If Decimal.TryParse(traslado.Importe, importeDecimal) Then
                        totalTraslados += importeDecimal
                    End If
                Next
            End If
        Next

        Return totalTraslados
    End Function
End Class

Public Class Emisor
    Public Property RegimenFiscal As String
    Public Property Rfc As String
    Public Property Nombre As String
End Class

Public Class Receptor
    Public Property RegimenFiscalReceptor As String
    Public Property DomicilioFiscalReceptor As String
    Public Property UsoCFDI As String
    Public Property Nombre As String
    Public Property Rfc As String
End Class

Public Class Concepto
    Public Property ValorUnitario As String
    Public Property Importe As String
    Public Property DescuentoImporte As String
    Public Property Cantidad As String
    Public Property ClaveProdServ As String
    Public Property ClaveUnidad As String
    Public Property Descripcion As String
    Public Property ObjetoImp As String

    ' Impuestos a nivel de concepto
    Public Property Traslados As List(Of Impuesto)
    Public Property Retenciones As List(Of Impuesto)

    Public Sub New()
        Traslados = New List(Of Impuesto)()
        Retenciones = New List(Of Impuesto)()
    End Sub
End Class

Public Class Impuesto
    Public Property TasaOCuota As String
    Public Property Importe As String
    Public Property TipoFactor As String
    Public Property Base As String
    Public Property Impuesto As String
End Class

Public Class TimbreFiscalDigital
    Public Property Version As String
    Public Property UUID As String
    Public Property FechaTimbrado As String
    Public Property RfcProvCertif As String
    Public Property SelloCFD As String
    Public Property NoCertificadoSAT As String
    Public Property SelloSAT As String
End Class

' Nueva clase para representar el nodo global de Impuestos (cfdi:Impuestos)
Public Class ImpuestosFactura
    ' Atributos a nivel de factura
    Public Property TotalImpuestosTrasladados As String
    Public Property TotalImpuestosRetenidos As String

    ' Listas de impuestos, en caso de que existan más de un traslado o retención
    Public Property Traslados As List(Of Impuesto)
    Public Property Retenciones As List(Of Impuesto)

    Public Sub New()
        Traslados = New List(Of Impuesto)()
        Retenciones = New List(Of Impuesto)()
    End Sub
End Class

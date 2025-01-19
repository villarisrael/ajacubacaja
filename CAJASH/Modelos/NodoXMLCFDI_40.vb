Imports System.Xml

Public Class NodoXMLCFDI_40
    Public Property Version As String
    Public Property SubTotal As Decimal
    Public Property Total As Decimal
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

    Public Sub New()
        Emisor = New Emisor()
        Receptor = New Receptor()
        Conceptos = New List(Of Concepto)()
        TimbreFiscalDigital = New TimbreFiscalDigital()
    End Sub


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
    Public Property Cantidad As String
    Public Property ClaveProdServ As String
    Public Property ClaveUnidad As String
    Public Property Descripcion As String
    Public Property ObjetoImp As String

    ' Nuevas propiedades para los impuestos
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

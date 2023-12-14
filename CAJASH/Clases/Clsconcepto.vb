Public Class Clsconcepto
    Public Cantidad As Integer
    Public Concepto As String
    Public Preciounitario As Double
    Public importe As Double
    Public IVA As Double
    Public Clave As String
    'Public Clave As Integer
    Public CLAVEMOV As Long = 0
    Public clavesat As String = "83101501"
    Public unidadsat As String = "E48"
    Public llevaiva As Integer = 1
    Public descuento As Decimal = 0
    Public descuentoenpesos As Decimal = 0
    Public totalcondescuento As Decimal = 0


    Public Sub calcula()
        Preciounitario = Math.Round(Preciounitario, 2)
        importe = Math.Round(Cantidad * Preciounitario, 2)
        IVA = Math.Round(importe * (variable_iva / 100) * llevaiva, 2)
    End Sub


End Class

Public Class Clsconcepto
    Public Cantidad As Integer
    Public Concepto As String
    Public Preciounitario As Double
    Public importe As Double
    Public IVA As Double
    Public Clave As String
    'Public Clave As Integer
    Public CLAVEMOV As Long = 0

    Public Sub calcula()
        Preciounitario = Math.Round(Preciounitario, 2)
        importe = Math.Round(Cantidad * Preciounitario, 2)
        'IVA = importe * variable_iva
    End Sub


End Class

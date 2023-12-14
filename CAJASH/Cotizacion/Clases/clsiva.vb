Public Class clsiva

    Public cantidad As Double
    Public iva As Double
    Public valordeiva As Integer = 16
    Public cantidadconiva As Double


    Public Sub calcular()

        iva = Math.Round(cantidad * (valordeiva / 100), 2)
        cantidadconiva = cantidad + iva


    End Sub


End Class

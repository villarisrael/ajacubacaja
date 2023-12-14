Public Class clsunidadmes
    Public mes As String
    Public periodo As Integer
    Public cuota As Double
    Public numerodemes As Integer
    Public recargo As Double = 0.03
    Public total As Double = 0
    Public descuento As Double
    Public totalcondescuento As Double
    Public totaliva As Double
    Public totalconiva As Double
    Public tipo As String = "CONSUMO"

    Public Sub siniva()
        totaliva = 0
        totalconiva = Math.Round(totalcondescuento, 2)
        totalconiva = totalcondescuento + totaliva
    End Sub

    Public Sub coniva()
        Try
            totaliva = totalcondescuento * (variable_iva / 100)
        Catch ex As Exception
            totaliva = 0
        End Try
        totalconiva = totalcondescuento + totaliva
    End Sub

End Class

Public Class ClsRegistrolectura
    Inherits clslecturabasica

    Public Periodo As String
    Public Mes As String

    Public Recargo As Double
    Public Consumocobrado As Integer
    Public Total As Double
    Public Montocobrado As Double
    Public Numeroperiodo As Integer
    Public Porcrecargo As Double
    Public Descuento As Double
    Public Totalcondescuento As Double
    Public situacion As String
    Public totaliva As Double
    Public totalconiva As Double
    Public Tipo As String = "Rezago"

    Public Sub siniva()
        totaliva = 0
        totalconiva = totalcondescuento
    End Sub

    Public Sub coniva()
        Try
            totaliva = Totalcondescuento * (variable_iva / 100)
        Catch ex As Exception
            totaliva = 0
        End Try
        totalconiva = totalcondescuento + totaliva
    End Sub



End Class

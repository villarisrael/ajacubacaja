
Public Class clslecturabasica
    Public Lectura_Anterior As Integer
    Public Lectura_Actual As Integer
    Public Consumo As Integer
    Public Mensaje As String

    Public Sub Calcular()
        Consumo = Lectura_Actual - Lectura_Anterior

        If Consumo < 0 Then
            Mensaje = "Lectura Negativa " & Consumo
            Consumo = 0

        End If

    End Sub

End Class

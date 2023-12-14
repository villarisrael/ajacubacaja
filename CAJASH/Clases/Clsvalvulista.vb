Public Class Clsvalvulista
    Public id As Integer = 0
    Public nmeses As Integer = 0
    Public Preciouni As Double = 0
    Public Importe As Double = 0
    Public Sub calcular()
        Dim x As New base
        Dim monto As Integer = x.obtenerCampo("select * from ccuotavalvulista where idcuotavalvulista =" & id, "nMontoValvulista")
        Preciouni = Math.Round(monto)
        Importe = Math.Round(nmeses * monto, 2)
        x.conexion.Dispose()
    End Sub
End Class

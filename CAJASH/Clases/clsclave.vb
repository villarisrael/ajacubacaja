Public Class clsclave
    Public palabra As String
    Public encriptado As String
    Public Sub password()
        encriptado = ""
        For i = 1 To Len(palabra)
            encriptado = encriptado + Chr(Asc(Mid(palabra, i, 1)) + 75)
        Next
    End Sub
    Public Function desencriptar(ByVal palabra As String) As String
        encriptado = ""
        For i = 1 To Len(palabra)
            encriptado = encriptado + Chr(Asc(Mid(palabra, i, 1)) - 75)
        Next
        Return encriptado
    End Function
End Class

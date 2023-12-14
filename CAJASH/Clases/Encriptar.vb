Public Class Encriptar
    Private _mpalabra As String
    Public palabra As String
    Public encriptado As String

    Public Sub New()

    End Sub

    Public Sub New(ByVal Mp As String)
        _mpalabra = Mp
    End Sub
    Public ReadOnly Property Encriptada() As String
        Get
            Return Encriptar()
        End Get
        'Set(ByVal value As String)
        'End Set
    End Property
    Public ReadOnly Property Desencriptada() As String
        Get
            Return Desencriptar()
        End Get
        'Set(ByVal value As String)
        'End Set
    End Property


    Private Function ConvToHex(ByVal x As Integer) As String
        If x > 9 Then ConvToHex = Chr(x + 55) Else ConvToHex = CStr(x)
    End Function

    ' función que codifica el dato
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Function Encriptar() As Object

        encriptado = ""
        For i = 1 To Len(palabra)
            encriptado = encriptado + Chr(Asc(Mid(palabra, i, 1)) + 75)
        Next
        Return encriptado
    End Function
    Private Function ConvToInt(ByVal x As String) As Integer
        Dim x1 As String, x2 As String, Temp As Integer
        x1 = Mid(x, 1, 1)
        x2 = Mid(x, 2, 1)
        If IsNumeric(x1) Then Temp = 16 * Int(x1) Else Temp = (Asc(x1) - 55) * 16
        If IsNumeric(x2) Then Temp = Temp + Int(x2) Else Temp = Temp + (Asc(x2) - 55)
        ' retorno
        ConvToInt = Temp
    End Function

    ' función que decodifica el dato
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Function Desencriptar() As Object

        encriptado = ""
        For i = 1 To Len(Palabra)
            encriptado = encriptado + Chr(Asc(Mid(Palabra, i, 1)) - 75)
        Next
        Return encriptado

    End Function

End Class
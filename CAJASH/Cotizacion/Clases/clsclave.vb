Public Class clsclave
    Private _mpalabra As String

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

    Public Property Palabra() As String
        Get
            Return _mpalabra
        End Get
        Set(ByVal Valor As String)
            _mpalabra = Valor
        End Set
    End Property

    Private Function ConvToHex(ByVal x As Integer) As String
        If x > 9 Then ConvToHex = Chr(x + 55) Else ConvToHex = CStr(x)
    End Function

    ' función que codifica el dato
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Function Encriptar() As Object
        Dim x As Long, Temp As String = "", TempNum As Integer
        Dim TempChar As String, TempChar2 As String
        For x = 1 To Len(_mpalabra)
            TempChar2 = Mid(_mpalabra, x, 1)
            TempNum = Int(Asc(TempChar2) / 16)
            If ((TempNum * 16) < Asc(TempChar2)) Then
                TempChar = ConvToHex(Asc(TempChar2) - (TempNum * 16))
                Temp = Temp & ConvToHex(TempNum) & TempChar
            Else
                Temp = Temp & ConvToHex(TempNum) & "0"
            End If
        Next x
        Encriptar = Temp
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
        Dim x As Long, Temp As String = "", HexByte As String
        For x = 1 To Len(_mpalabra) Step 2
            HexByte = Mid(_mpalabra, x, 2)
            Temp = Temp & Chr(ConvToInt(HexByte))
        Next x
        Desencriptar = Temp
    End Function
End Class

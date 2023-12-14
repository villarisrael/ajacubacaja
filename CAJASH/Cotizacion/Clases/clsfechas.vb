Public Class clsfechas

    Public Function valornummes(ByVal mes As String) As Integer
        Dim VALOR As Integer
        If mes = "ENE" Then
            VALOR = 1
        End If
        If mes = "FEB" Then
            VALOR = 2
        End If
        If mes = "MAR" Then
            VALOR = 3
        End If
        If mes = "ABR" Then
            VALOR = 4
        End If
        If mes = "MAY" Then
            VALOR = 5
        End If
        If mes = "JUN" Then
            VALOR = 6
        End If
        If mes = "JUL" Then
            VALOR = 7
        End If
        If mes = "AGO" Then
            VALOR = 8
        End If
        If mes = "SEP" Then
            VALOR = 9
        End If
        If mes = "OCT" Then
            VALOR = 10
        End If
        If mes = "NOV" Then
            VALOR = 11
        End If
        If mes = "DIC" Then
            VALOR = 12
        End If
        Return VALOR
    End Function

    Public Function valorcadenames(ByVal mes As Integer) As String


        Dim VALOR As String
        If mes = 1 Then
            VALOR = "ENE"
        End If
        If mes = 2 Then
            VALOR = "FEB"
        End If
        If mes = 3 Then
            VALOR = "MAR"
        End If
        If mes = 4 Then
            VALOR = "ABR"
        End If
        If mes = 5 Then
            VALOR = "MAY"
        End If
        If mes = 6 Then
            VALOR = "JUN"
        End If
        If mes = 7 Then
            VALOR = "JUL"
        End If
        If mes = 8 Then
            VALOR = "AGO"
        End If
        If mes = 9 Then
            VALOR = "SEP"
        End If
        If mes = 10 Then
            VALOR = "OCT"
        End If
        If mes = 11 Then
            VALOR = "NOV"
        End If
        If mes = 12 Then
            VALOR = "DIC"
        End If
        Return VALOR
    End Function


End Class

Imports Microsoft.VisualBasic.DateAndTime
Module ModNumText
    Public Const Void As String = ""
    Public Const Dot As String = "."
    Dim m_FeminineGenerous As Boolean

#Region "Numeros a Letras"

    Public Function ReplaceStringFrom(ByVal s As String, ByVal OldWrd As String, ByVal NewWrd As String, ByVal ptr As String) As String

        s = Left$(s, ptr - 1) + NewWrd + Mid$(s, Len(OldWrd) + ptr)
        ReplaceStringFrom = s
    End Function

    Public Function Valida(ByVal KeyIn As Integer, ByVal ValidateString As String, ByVal Editable As Boolean) As Integer

        Dim ValidateList As String
        Dim KeyOut As Integer

        If Editable = True Then
            ValidateList = UCase(ValidateString) & Chr(8)
        Else
            ValidateList = UCase(ValidateString)
        End If
        If InStr(1, ValidateList, UCase(Chr(KeyIn)), 1) > 0 Then
            KeyOut = KeyIn
        Else
            KeyOut = 0
            Beep()
        End If
        Valida = KeyOut

    End Function

    Public Function CToSp(ByVal Number As Double, ByVal CurrentMoney As Double, Optional ByVal FeminineGenerous As Boolean = False) As String

        Dim s As String
        Dim DecimalPlace As Long
        Dim IntPart As String
        Dim Cents As String

        FeminineGenerous = False

        m_FeminineGenerous = FeminineGenerous
        s = Format(Val(Number), "0.00")
        DecimalPlace = InStr(s, Dot)

        If DecimalPlace Then
            IntPart = Left$(s, DecimalPlace - 1)
            Cents = Left$(Mid$(s, DecimalPlace + 1, 2), 2)
        Else
            IntPart = s
            Cents = Void
        End If

        If IntPart = "0" Or IntPart = Void Then
            s = "Cero "
        ElseIf Len(IntPart) > 7 Then
            s = IntNumToSpanish(Val(Left(IntPart, Len(IntPart) - 6))) + "Millones " + IntNumToSpanish(Val(Right$(IntPart, 6)))
        Else
            s = IntNumToSpanish(Val(IntPart))
        End If

        If Right$(s, 9) = "Millones " Or Right$(s, 7) = "Millón " Then
            s = s + "de "
        End If
        Select Case s
            Case "Un ", "Una "
                s = s & Singular(CStr(CurrentMoney))
            Case Else
                s = s & CurrentMoney
        End Select

        If Val(Cents) Then
            Cents = " con " + IntNumToSpanish(Val(Cents)) + "Centavos"
        Else
            Cents = " con Cero Centavos"
        End If

        CToSp = s + Cents

    End Function

    Public Function IntNumToSpanish(ByVal numero As Long) As String

        Dim ptr
        Dim n
        Dim i
        Dim s As String
        Dim rtn As String = ""
        Dim tem As String

        s = CStr(numero)
        n = Len(s)

        tem = Void
        i = n
        Do Until i = 0
            tem = EvalPart(Val(Mid$(s, n - i + 1, 1) + construye(i - 1, "0")))
            If Not tem = "Cero" Then
                rtn = rtn + tem + " "
            End If
            i = i - 1
        Loop

        '-- //Filtros




        'filterThousands:
        rtn = ReplaceAll(rtn, " Mil Mil", " Un Mil")
        Do
            ptr = InStr(rtn, "Mil ")
            If ptr Then
                If InStr(ptr + 1, rtn, "Mil ") Then
                    rtn = ReplaceStringFrom(rtn, "Mil ", "", ptr)
                Else : Exit Do
                End If
            Else : Exit Do
            End If
        Loop

        '       Return

        'filterHundreds:
        ptr = 0
        Do
            ptr = InStr(ptr + 1, rtn, "Cien ")
            If ptr Then
                tem = Left$(Mid$(rtn, ptr + 5), 1)
                If tem = "M" Or tem = Void Then
                Else
                    rtn = ReplaceStringFrom(rtn, "Cien", "Ciento", ptr)

                End If
            End If
        Loop Until ptr = 0

        '       Return

        'filterMisc:
        rtn = ReplaceAll(rtn, "Diez Un", "Once")
        rtn = ReplaceAll(rtn, "Diez Dos", "Doce")
        rtn = ReplaceAll(rtn, "Diez Tres", "Trece")
        rtn = ReplaceAll(rtn, "Diez Cuatro", "Catorce")
        rtn = ReplaceAll(rtn, "Diez Cinco", "Quince")
        rtn = ReplaceAll(rtn, "Diez Seis", "Dieciseis")
        rtn = ReplaceAll(rtn, "Diez Siete", "Diecisiete")
        rtn = ReplaceAll(rtn, "Diez Ocho", "Dieciocho")
        rtn = ReplaceAll(rtn, "Diez Nueve", "Diecinueve")
        rtn = ReplaceAll(rtn, "Veinte Un", "Veintiun")
        rtn = ReplaceAll(rtn, "Veinte Dos", "Veintidos")
        rtn = ReplaceAll(rtn, "Veinte Tres", "Veintitres")
        rtn = ReplaceAll(rtn, "Veinte Cuatro", "Veinticuatro")
        rtn = ReplaceAll(rtn, "Veinte Cinco", "Veinticinco")
        rtn = ReplaceAll(rtn, "Veinte Seis", "Veintiseís")
        rtn = ReplaceAll(rtn, "Veinte Siete", "Veintisiete")
        rtn = ReplaceAll(rtn, "Veinte Ocho", "Veintiocho")
        rtn = ReplaceAll(rtn, "Veinte Nueve", "Veintinueve")
        'Return

        'filterOne:
        If Left$(rtn, 1) = "M" Then
            rtn = "Un " + rtn
        End If
        '-- //Un Mil...
        If Left$(rtn, 6) = "Un Mil" Then
            rtn = Mid$(rtn, 4)
        End If
        'Return


        For i = 65 To 88
            If Not i = 77 Then
                rtn = ReplaceAll(rtn, "a " + Chr(i), "* y " + Chr(i))
            End If
        Next

        rtn = ReplaceAll(rtn, "*", "a")


        IntNumToSpanish$ = rtn


    End Function

    Private Function EvalPart(ByVal X As Long) As String

        Dim rtn As String, s As String = "", i As Double

        Do
            ' GoSub SinglePart

            'SinglePart:
            Select Case X
                Case 0 : s = "Cero"
                Case 1 : s = "Un"
                Case 2 : s = "Dos"
                Case 3 : s = "Tres"
                Case 4 : s = "Cuatro"
                Case 5 : s = "Cinco"
                Case 6 : s = "Seis"
                Case 7 : s = "Siete"
                Case 8 : s = "Ocho"
                Case 9 : s = "Nueve"
                Case 10 : s = "Diez"
                Case 20 : s = "Veinte"
                Case 30 : s = "Treinta"
                Case 40 : s = "Cuarenta"
                Case 50 : s = "Cincuenta"
                Case 60 : s = "Sesenta"
                Case 70 : s = "Setenta"
                Case 80 : s = "Ochenta"
                Case 90 : s = "Noventa"
                Case 100 : s = "Cien"
                Case 200 : s = "Doscientos"
                Case 300 : s = "Trescientos"
                Case 400 : s = "Cuatrocientos"
                Case 500 : s = "Quinientos"
                Case 600 : s = "Seiscientos"
                Case 700 : s = "Setecientos"
                Case 800 : s = "Ochocientos"
                Case 900 : s = "Novecientos"
                Case 1000 : s = "Mil"
                Case 1000000 : s = "Millón"
            End Select
            If m_FeminineGenerous Then
                ReplaceAll(s, "tos", "tas")
                If s = "Un" Then s = "Una"

            End If
            '            Return

            If s = Void Then
                i = i + 1
                X = X / 1000
                If X = 0 Then i = 0

            Else
                Exit Do

            End If
        Loop Until i = 0

        rtn = s
        '  GoSub EngPart



        'EngPart:  ' //E+00...
        Select Case i
            Case 0 : s = Void
            Case 1 : s = " Mil"
            Case 2 : s = " Millones"
            Case 3 : s = " Billones"
        End Select
        '       Return

        EvalPart = rtn + s
        Exit Function

    End Function

    Public Function Singular(ByVal s As String) As String
        Singular = ""
        If Len(s) >= 2 Then
            If Right$(s, 1) = "s" Then
                If Right$(s, 2) = "es" Then
                    Singular = Left$(s, Len(s) - 2)
                Else
                    Singular = Left$(s, Len(s) - 1)
                End If
            Else
                Singular = s
            End If
        End If
    End Function

    Public Function ReplaceAll(ByVal s As String, ByVal OldWrd As String, ByVal NewWrd As String) As String

        Dim ptr

        Do
            ptr = InStr(s, OldWrd)
            If ptr Then
                s = Left$(s, ptr - 1) + NewWrd + Mid$(s, Len(OldWrd) + ptr)
            End If
        Loop Until ptr = 0
        ReplaceAll = s
    End Function

    Public Function Reemplaza(ByVal MCampo As Object, ByVal Caracter As String, ByVal Cambio As String)

        Dim i As Integer
        Dim car As String

        Reemplaza = ""
        For i = 1 To Len(MCampo)
            car = Mid(MCampo, i, 1)
            If car = Caracter Then car = Cambio

            Reemplaza = Reemplaza + car
        Next i

    End Function

    Public Function ConvertHundreds(ByVal MyNumber As Object) As String
        Dim rtn As String = ""
        ConvertHundreds = ""
        '//Exit if there is nothing to convert.
        If Val(MyNumber) = 0 Then Exit Function

        '//Append leading zeros to number.
        MyNumber = Right$("000" & MyNumber, 3)

        '//Do we have a hundreds place digit to convert?
        If Not Left$(MyNumber, 1) = "0" Then
            rtn = ConvertDigit(Left$(MyNumber, 1)) & " Hundred "
        End If

        '//Do we have a tens place digit to convert?
        If Not Mid$(MyNumber, 2, 1) = "0" Then
            rtn = rtn & ConvertTens(Mid$(MyNumber, 2))
        Else
            '//If not, then convert the ones place digit.
            rtn = rtn & ConvertDigit(Mid$(MyNumber, 3))
        End If

        ConvertHundreds = Trim$(rtn)
    End Function

    Public Function ConvertTens(ByVal MyTens As Object) As String
        Dim rtn As String = ""

        '//Is value between 10 and 19?
        If Val(Left$(MyTens, 1)) = 1 Then
            Select Case Val(MyTens)
                Case 10 : rtn = "Ten"
                Case 11 : rtn = "Eleven"
                Case 12 : rtn = "Twelve"
                Case 13 : rtn = "Thirteen"
                Case 14 : rtn = "Fourteen"
                Case 15 : rtn = "Fifteen"
                Case 16 : rtn = "Sixteen"
                Case 17 : rtn = "Seventeen"
                Case 18 : rtn = "Eighteen"
                Case 19 : rtn = "Nineteen"
                Case Else
            End Select
        Else
            '//.. otherwise it's between 20 and 99.
            Select Case Val(Left$(MyTens, 1))
                Case 2 : rtn = "Twenty "
                Case 3 : rtn = "Thirty "
                Case 4 : rtn = "Forty "
                Case 5 : rtn = "Fifty "
                Case 6 : rtn = "Sixty "
                Case 7 : rtn = "Seventy "
                Case 8 : rtn = "Eighty "
                Case 9 : rtn = "Ninety "
                Case Else
            End Select

            '//Convert ones place digit.
            rtn = rtn & ConvertDigit(Right$(MyTens, 1))
        End If

        ConvertTens = rtn
    End Function

    Public Function ConvertDigit(ByVal MyDigit As Object) As String
        Select Case Val(MyDigit)
            Case 1 : ConvertDigit = "One"
            Case 2 : ConvertDigit = "Two"
            Case 3 : ConvertDigit = "Three"
            Case 4 : ConvertDigit = "Four"
            Case 5 : ConvertDigit = "Five"
            Case 6 : ConvertDigit = "Six"
            Case 7 : ConvertDigit = "Seven"
            Case 8 : ConvertDigit = "Eight"
            Case 9 : ConvertDigit = "Nine"
            Case Else : ConvertDigit = Void
        End Select
    End Function

    Public Function ConvertCurrencyToSpanish(ByVal Number As Double, ByVal Cosa As String, Optional ByVal FeminineGenerous As Boolean = False) As String
        Dim s As String
        Dim DecimalPlace As Long
        Dim IntPart As String
        Dim Cents As String
        FeminineGenerous = False

        m_FeminineGenerous = FeminineGenerous
        s = Format(Val(Number), "0.00")
        DecimalPlace = InStr(s, Dot)

        If DecimalPlace Then
            IntPart = Left$(s, DecimalPlace - 1)
            Cents = Left$(Mid$(s, DecimalPlace + 1, 2), 2)
        Else
            IntPart = s
            Cents = Void
        End If

        If IntPart = "0" Or IntPart = Void Then
            s = "Cero "
        ElseIf Len(IntPart) > 7 Then
            s = IntNumToSpanish(Val(Left$(IntPart, Len(IntPart) - 6))) + _
                "Millones " + IntNumToSpanish(Val(Right$(IntPart, 6)))
        Else
            s = IntNumToSpanish(Val(IntPart))
        End If

        If Right$(s, 9) = "Millones " Or Right$(s, 7) = "Millón " Then
            s = s + "de "
        End If
        Select Case s
            Case "Un ", "Una "
                s = s & Singular(CStr(Cosa)) & " "
            Case Else
                s = s & Cosa & " "
        End Select

        If Val(Cents) Then
            Cents = Val(Cents) & "/100 M.N."
        Else
            Cents = " 00/100 M.N."
        End If

        ConvertCurrencyToSpanish = s + Cents
    End Function

    Public Function construye(ByVal i As Integer, ByVal cadena As String) As String
        Dim y As Integer
        construye = ""
        For y = 1 To i
            construye = construye & cadena
        Next
    End Function
#End Region

    Public Function acompletacero(ByVal cadena As String, ByVal cuantos As Integer) As String
        Dim longi As Integer
        longi = cuantos - cadena.Length
        For i = 1 To longi
            cadena = "0" & cadena
        Next
        Return cadena
    End Function

End Module

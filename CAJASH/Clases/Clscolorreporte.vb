Imports iTextSharp.text.pdf



Public Class Clscolorreporte


    Public color As CMYKColor


    Public Sub ClsColoresReporte(_color As String)

        If (_color = "Cafe") Then

            color = New CMYKColor(0, 29, 50, 70)

        End If
        If (_color = "Morado claro") Then

            color = New CMYKColor(100, 100, 0, 41)
        End If
        If (_color = "Azul") Then


            color = New CMYKColor(100, 100, 0, 0)
        End If

        If (_color = "AzulLogoCuau") Then


            color = New CMYKColor(115, 115, 0, 70)
        End If

        If (_color = "Morado") Then


            color = New CMYKColor(100, 100, 0, 50)
        End If
        If (_color = "Marron dorado") Then

            color = New CMYKColor(10, 25, 85, 0)
        End If
        If (_color = "Rojo") Then

            color = New CMYKColor(50, 100, 100, 10)
        End If
        If (_color = "Negro") Then

            color = New CMYKColor(0, 0, 0, 0)
        End If
        If (_color = "Blanco") Then

            color = New CMYKColor(255, 255, 255, 0)
        End If
        If (_color = "Verde") Then

            color = New CMYKColor(125, 0, 110, 10)
        End If
    End Sub


    Public Sub ClsColoresReporte(a As Integer, b As Integer, c As Integer, d As Integer)

        color = New CMYKColor(a, b, c, d)


    End Sub


    Public Function ConvertRgbToCmyk(r As Integer, g As Integer, b As Integer) As CMYKColor

        Dim c As Decimal
        Dim m As Decimal
        Dim y As Decimal

        Dim k As Decimal

        Dim rf As Decimal
        Dim gf As Decimal
        Dim bf As Decimal
        rf = r / 255
        gf = g / 255
        bf = b / 255
        k = ClampCmyk(1 - Math.Max(Math.Max(rf, gf), bf))
        c = ClampCmyk((1 - rf - k) / (1 - k))
        m = ClampCmyk((1 - gf - k) / (1 - k))
        y = ClampCmyk((1 - bf - k) / (1 - k))
        Return New CMYKColor(c, m, y, k)
    End Function



    Private Function ClampCmyk(value As Decimal) As Decimal

        If (value < 0) Then

            value = 0
        End If
        Return value
    End Function



End Class

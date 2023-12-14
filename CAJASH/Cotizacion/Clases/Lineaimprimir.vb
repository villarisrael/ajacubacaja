Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Public Class Lineaimprimir
    Public prtfont As Font
    Public letradefault = New Font("Sans serif Condensed", 7)

    Public coordenadaX As Integer
    Public coordenaday As Integer
    Public cadena As String = ""
    Public alinea As New Drawing.StringFormat
    Public Enum alineacion
        Izquierda
        Derecha
        Centrado
    End Enum

    Public Sub New(ByVal X As Integer, ByVal y As Integer, ByVal cad As String, ByVal f As Font, ByVal aline As alineacion)
        coordenadaX = X
        coordenaday = y
        cadena = cad
        prtfont = f

        Select Case aline
            Case alineacion.Derecha
                alinea.Alignment = StringAlignment.Far
            Case alineacion.Izquierda
                alinea.Alignment = StringAlignment.Near
            Case alineacion.Centrado
                alinea.Alignment = StringAlignment.Center
        End Select


    End Sub




End Class

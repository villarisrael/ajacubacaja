﻿Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports System.Text
Imports System.Drawing.Printing
Imports System.Drawing
Imports System.Windows.Forms
Imports System.IO
Imports System.Data.Odbc

Public Class Imprimirnet


    Public prtSettings As New PrinterSettings
    Public prtDoc As New PrintDocument
    Public prtFont As Font

    Private lienzo As Graphics

    Public lineas As New Collection(Of Lineaimprimir)
    Private lineaactual As Integer = 0

    Private prtprev As PrintPreviewDialog

    Public Enum alineacion
        Izquierda
        Derecha
        Centrado
    End Enum

    Public Sub New()
        lineas = New Collection(Of Lineaimprimir)
        prtFont = New Font("Courier New", 13)
        prtDoc = New PrintDocument
        prtSettings = New PrinterSettings

        '  lienzo.Clear(Color.White)
    End Sub

    Public Sub imprimir(ByVal x As Integer, ByVal y As Integer, ByVal cad As String, ByVal ali As alineacion, ByVal letra As String, ByVal sizeltra As Double)
        If sizeltra = 0 Then sizeltra = 8
        prtFont = New Font(letra, sizeltra)
        lineas.Add(New Lineaimprimir(x, y, cad, prtFont, ali))

    End Sub

    Public Sub mandardocumento(ByVal espreview As Boolean)
        If Not prtDoc.PrinterSettings.IsValid Then
            prtDoc.PrinterSettings = New PrinterSettings
        End If
        Try
            prtDoc.PrinterSettings.Copies = My.Settings.copiasderecibo
        Catch ex As Exception
            prtDoc.PrinterSettings.Copies = 1
        End Try

        If prtDoc.PrinterSettings.IsValid Then
            AddHandler prtDoc.PrintPage, AddressOf prtDoc_PrintPage
        End If
        lineaactual = 0
        If espreview Then
            prtprev = New PrintPreviewDialog
            prtprev.Document = prtDoc
            prtprev.Show()
        Else
            prtDoc.Print()
        End If
    End Sub

    Private Function Bytes_Imagen(ByVal Imagen As Byte()) As Image
        Try
            'si hay imagen
            If Not Imagen Is Nothing Then
                'caturar array con memorystream hacia Bin
                Dim Bin As New MemoryStream(Imagen)
                'con el método FroStream de Image obtenemos imagen
                Dim Resultado As Image = Image.FromStream(Bin)
                'y la retornamos
                Return Resultado
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function



    Public Sub seleccionarimpresora()
        Dim prtdialog As New PrintDialog
        prtdialog.AllowPrintToFile = False
        prtdialog.PrintToFile = False
        prtdialog.PrinterSettings = prtSettings

        If prtdialog.ShowDialog() = DialogResult.OK Then
            prtDoc.PrinterSettings = prtdialog.PrinterSettings
        End If

    End Sub

    Public Sub prtDoc_PrintPage(ByVal sender As Object, ByVal ev As PrintPageEventArgs)
        Dim lineHeigh As Single
        Dim yPos As Single = ev.MarginBounds.Top
        Dim leftmargin As Single = ev.MarginBounds.Left

        Dim printFont As Font

        printFont = prtFont
        lineHeigh = printFont.GetHeight(ev.Graphics)

        Try

            Dim ima As Byte() = LOGOBYTE
            Dim LOGO As Image = Bytes_Imagen(ima)

            ev.Graphics.DrawImage(LOGO, 50, 10, 180, 80)
        Catch ex As Exception

        End Try


        Try
            For i = 0 To lineas.Count
                Dim l As Lineaimprimir = lineas(i)
                ev.Graphics.DrawString(l.cadena, l.prtfont, Brushes.Black, l.coordenadaX, l.coordenaday, l.alinea)

            Next
        Catch ex As Exception

        End Try
        ev.HasMorePages = False
    End Sub
End Class
Imports DevComponents
Imports Microsoft.VisualBasic.DateAndTime
Imports System.Data.Odbc
Imports System.Data
Imports System.IO

Imports Microsoft.Office.Interop

Module funcionesbasicas
  

    Public NumCaja As String, Oficina As String

    Public rema As Double
    Public NumUser As Integer
    Public Tcaja As String, RutaImagenes As String
    Public DirDbase As String = "C:\Prueba"
    '  Public VARIABLE_IVA As Double
    Public ConcepFac As Double = 779
    Public Empresa As String, Direccion As String, Director As String
    Public LMedNuevo As String = "O"
    Public PORC_RECA_OTRO As Integer
    'Public VARIABLE_IVA As Double = My.Settings.



   

    Public Function llenarcero(ByRef var As String, ByRef cuantas As Short) As String
        If Len(var) < cuantas Then
            Do While Len(var) < cuantas
                var = "0" & Trim(var)
            Loop
            llenarcero = var
        Else
            llenarcero = var
        End If
    End Function


    Public Sub formatofecha(ByRef txt As TextBox)
        txt.MaxLength = 10
        If Len(txt.Text) = 2 Or Len(txt.Text) = 5 Then
            txt.Text = txt.Text & "/"
            txt.SelectionStart = Len(txt.Text)
        End If
        If Len(txt.Text) = 10 Then
            Dim validarFecha As Boolean = IsDate(txt.Text)
            If validarFecha = False Then
                MessageBox.Show("El formato de fecha no es correcto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txt.Text = ""
                txt.Focus()
            End If
        End If
    End Sub
    Public Sub formatofecha(ByRef txt As ToolStripTextBox)
        txt.MaxLength = 10
        If Len(txt.Text) = 2 Or Len(txt.Text) = 5 Then
            txt.Text = txt.Text & "/"
            txt.SelectionStart = Len(txt.Text)
        End If
        If Len(txt.Text) = 10 Then
            Dim validarFecha As Boolean = IsDate(txt.Text)
            If validarFecha = False Then
                MessageBox.Show("El formato de fecha no es correcto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txt.Text = ""
                txt.Focus()
            End If
        End If
    End Sub
    Public Function DiasHabiles(ByVal DateIn As DateTime, ByVal ShiftDate As Integer) As DateTime
        Dim i As Integer = 1, DateTemp As Date, Dias As Integer
        While i <= ShiftDate
            DateTemp = DateAdd(DateInterval.Day, i, DateIn)
            If Not DateTemp.DayOfWeek = DayOfWeek.Sunday Then
                Dias += 1
            Else
                Dias += 1
                ShiftDate += 1
            End If
            i += 1
        End While
        DiasHabiles = DateAdd(DateInterval.Day, Dias, DateIn)
    End Function

    Public Function Mayusculas(ByVal txt As TextBox) As String
        Mayusculas = UCase(txt.Text)
        txt.SelectionStart = Len(txt.Text)
    End Function
    Public Function Minusculas(ByVal txt As TextBox) As String
        Minusculas = LCase(txt.Text)
        txt.SelectionStart = Len(txt.Text)
    End Function

    Function SoloNumeros(ByVal Keyascii As Short) As Short
        If InStr("1234567890.", Chr(Keyascii)) = 0 Then
            SoloNumeros = 0
        Else
            SoloNumeros = Keyascii
        End If
        Select Case Keyascii
            Case 8
                SoloNumeros = Keyascii
            Case 13
                SoloNumeros = Keyascii
        End Select
    End Function

    Function SoloNumeros(ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(SoloNumeros(KeyAscii))
        If InStr("1234567890.", Chr(KeyAscii)) = 0 Then
            SoloNumeros = 0
        Else
            SoloNumeros = KeyAscii
        End If
        Select Case KeyAscii
            Case 8
                SoloNumeros = KeyAscii
            Case 13
                SoloNumeros = KeyAscii
        End Select
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Function


    'Public Sub DataTableToExcel(ByVal pDataTable As DataTable, ByVal estilo As Int16)
    '    Dim hoy As String = Day(Now) & Month(Now) & Year(Now) & Hour(Now) & Second(Now)
    '    Dim vFileName As String = "C:\Windows\Temp\tmp" & hoy & ".tmp" 'Path.GetTempFileName()
    '    FileOpen(1, vFileName, OpenMode.Output)
    '    Dim sb As String = ""
    '    Dim dc As DataColumn
    '    For Each dc In pDataTable.Columns
    '        sb &= dc.Caption & Microsoft.VisualBasic.ControlChars.Tab
    '    Next
    '    PrintLine(1, sb)

    '    Dim i As Integer = 0
    '    Dim dr As DataRow
    '    For Each dr In pDataTable.Rows
    '        i = 0 : sb = ""
    '        For Each dc In pDataTable.Columns
    '            If Not IsDBNull(dr(i)) Then
    '                sb &= CStr(dr(i)) & Microsoft.VisualBasic.ControlChars.Tab
    '            Else
    '                sb &= Microsoft.VisualBasic.ControlChars.Tab
    '            End If
    '            i += 1
    '        Next

    '        PrintLine(1, sb)
    '    Next
    '    FileClose(1)
    '    TextToExcel(vFileName, estilo)

    'End Sub

    'Public Sub TextToExcel(ByVal pFileName As String, ByVal estilo As Int16)

    '    Dim vFormato As Excel.XlRangeAutoFormat
    '    Dim hoy As String = Day(Now) & Month(Now) & Year(Now)

    '    Dim vCultura As System.Globalization.CultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture
    '    System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US")

    '    Dim Exc As Excel.Application = New Excel.Application
    '    Exc.Workbooks.OpenText(pFileName, , , , Excel.XlTextQualifier.xlTextQualifierNone, , True)

    '    Dim Wb As Excel.Workbook = Exc.ActiveWorkbook
    '    Dim Ws As Excel.Worksheet = Wb.ActiveSheet

    '    'Se le indica el formato al que queremos exportarlo
    '    Dim valor As Integer = estilo
    '    If valor > -1 Then
    '        Select Case valor
    '            Case 0 : vFormato = Excel.XlRangeAutoFormat.xlRangeAutoFormatNone
    '            Case 1 : vFormato = Excel.XlRangeAutoFormat.xlRangeAutoFormatSimple
    '            Case 2 : vFormato = Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic1
    '            Case 3 : vFormato = Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic2
    '            Case 4 : vFormato = Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic3
    '            Case 5 : vFormato = Excel.XlRangeAutoFormat.xlRangeAutoFormatAccounting1
    '            Case 6 : vFormato = Excel.XlRangeAutoFormat.xlRangeAutoFormatAccounting2
    '            Case 7 : vFormato = Excel.XlRangeAutoFormat.xlRangeAutoFormatAccounting3
    '            Case 8 : vFormato = Excel.XlRangeAutoFormat.xlRangeAutoFormatAccounting4
    '            Case 9 : vFormato = Excel.XlRangeAutoFormat.xlRangeAutoFormatColor1
    '            Case 10 : vFormato = Excel.XlRangeAutoFormat.xlRangeAutoFormatColor2
    '            Case 11 : vFormato = Excel.XlRangeAutoFormat.xlRangeAutoFormatColor3
    '            Case 12 : vFormato = Excel.XlRangeAutoFormat.xlRangeAutoFormatList1
    '            Case 13 : vFormato = Excel.XlRangeAutoFormat.xlRangeAutoFormatList2
    '            Case 14 : vFormato = Excel.XlRangeAutoFormat.xlRangeAutoFormatList3
    '            Case 15 : vFormato = Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects1
    '            Case 16 : vFormato = Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2
    '        End Select

    '        Ws.Range(Ws.Cells(1, 1), Ws.Cells(Ws.UsedRange.Rows.Count, Ws.UsedRange.Columns.Count)).AutoFormat(vFormato)

    '        'pFileName = "C:\Windows\Temp\tmp5B.tmp".Replace("tmp", "xls") 'Path.GetTempFileName.Replace("tmp", "xls")
    '        pFileName = "C:\Windows\Temp\tmp5B.xls"
    '        Dim obj As New System.Diagnostics.Process

    '        File.Delete(pFileName)
    '        'File.Delete("C:\Windows\Temp\tmp" & hoy & ".tmp")
    '        Exc.ActiveWorkbook.SaveAs(pFileName, Excel.XlTextQualifier.xlTextQualifierNone - 1)
    '    End If
    '    Exc.Quit()

    '    Ws = Nothing
    '    Wb = Nothing
    '    Exc = Nothing

    '    GC.Collect()

    '    If valor > -1 Then
    '        Dim p As System.Diagnostics.Process = New System.Diagnostics.Process
    '        p.EnableRaisingEvents = False
    '        Process.Start("Excel.exe", pFileName)
    '    End If
    '    System.Threading.Thread.CurrentThread.CurrentCulture = vCultura
    'End Sub

    Public Function AppPath() As String
        Return System.Windows.Forms.Application.StartupPath
    End Function

    Public Function mesanterior(ByVal fecha As Date) As Date
        Return DateAdd("m", -1, fecha)
    End Function

    Public Function cadenames(ByVal fecha As Date) As String
        Dim cadena As String = ""
        'cadena = NOMBREDEMES(Month(fecha)) & " " & Year(fecha)
        cadena = MonthName(fecha.Month).ToUpper & " " & fecha.Year
        Return (cadena)
    End Function

   

    'Public Function NumeroMes(ByVal Nmes As String) As Integer
    '    NumeroMes = 0
    '    Select Case Nmes.ToLower
    '        Case "enero", "ene"
    '            NumeroMes = 1
    '        Case "febrero", "feb"
    '            NumeroMes = 2
    '        Case "marzo", "mar"
    '            NumeroMes = 3
    '        Case "abril", "abr"
    '            NumeroMes = 4
    '        Case "mayo", "may"
    '            NumeroMes = 5
    '        Case "junio", "jun"
    '            NumeroMes = 6
    '        Case "julio", "jul"
    '            NumeroMes = 7
    '        Case "agosto", "ago"
    '            NumeroMes = 8
    '        Case "septiembre", "sep"
    '            NumeroMes = 9
    '        Case "octubre", "oct"
    '            NumeroMes = 10
    '        Case "noviembre", "nov"
    '            NumeroMes = 11
    '        Case "diciembre", "dic"
    '            NumeroMes = 12
    '    End Select
    '    Return NumeroMes
    'End Function

    Public Function CadenaNumeroMes(ByVal Nmes As String) As String
        Dim NumeroMes As String = "00"
        Select Case Nmes.ToLower
            Case "enero", "ene"
                NumeroMes = "01"
            Case "febrero", "feb"
                NumeroMes = "02"
            Case "marzo", "mar"
                NumeroMes = "03"
            Case "abril", "abr"
                NumeroMes = "04"
            Case "mayo", "may"
                NumeroMes = "05"
            Case "junio", "jun"
                NumeroMes = "06"
            Case "julio", "jul"
                NumeroMes = "07"
            Case "agosto", "ago"
                NumeroMes = "08"
            Case "septiembre", "sep"
                NumeroMes = "09"
            Case "octubre", "oct"
                NumeroMes = "10"
            Case "noviembre", "nov"
                NumeroMes = "11"
            Case "diciembre", "dic"
                NumeroMes = "12"
        End Select
        Return NumeroMes
    End Function

    Public Function NOMBREDEMES(ByVal MES As Integer) As String
        Dim mescad As String = ""
        Select Case MES
            Case 1
                mescad = "Enero"
            Case 2
                mescad = "Febrero"
            Case 3
                mescad = "Marzo"
            Case 4
                mescad = "Abril"
            Case 5
                mescad = "Mayo"
            Case 6
                mescad = "Junio"
            Case 7
                mescad = "Julio"
            Case 8
                mescad = "Agosto"
            Case 9
                mescad = "Septiembre"
            Case 10
                mescad = "Octubre"
            Case 11
                mescad = "Noviembre"
            Case 12
                mescad = "Diciembre"
        End Select
        Return mescad
    End Function

    'Pasarle a esta rutina la fecha que queremos averiguar  
    Public Function UltimoDia(ByVal Fecha As Date)
        'Dim Primer As Date
        Dim Ultimo As Date
        'Usamos la funcion DAteSerial para obtener el primero y el ultimo dia  
        'Primer = DateSerial(Year(Fecha), Month(Fecha) + 0, 1)
        Ultimo = DateSerial(Year(Fecha), Month(Fecha) + 1, 0)
        'MsgBox(" Primer día : " & Primer & vbNewLine & _
        '" Último día : " & Ultimo, vbInformation)
        Return Ultimo
    End Function


    Function monedatext(ByVal monto As Double) As Label
        Dim mon As New Label
        mon.Text = Format(monto, "#,###,##0.00")
        mon.TextAlign = ContentAlignment.MiddleRight
        If monto >= 0 Then mon.ForeColor = Color.Black
        If monto < 0 Then mon.ForeColor = Color.Red
        mon.Height = 15
        monedatext = mon
    End Function

    Public Function cadenames1(ByVal fecha As Date) As String
        Dim cadena As String = ""
        Dim mes As String = Now.Month - 1
        If mes = 0 Then
            mes = 12
            cadena = MonthName(mes).ToUpper
            cadena = Mid(cadena, 1, 3) & fecha.Year - 1

        Else
            cadena = MonthName(mes).ToUpper
            cadena = Mid(cadena, 1, 3) & fecha.Year
        End If
        Return (cadena)
    End Function


    Public Function NumeroMes(ByVal Nmes As String) As Integer
        NumeroMes = 0
        Select Case Nmes.ToLower
            Case "enero", "ene"
                NumeroMes = 1
            Case "febrero", "feb"
                NumeroMes = 2
            Case "marzo", "mar"
                NumeroMes = 3
            Case "abril", "abr"
                NumeroMes = 4
            Case "mayo", "may"
                NumeroMes = 5
            Case "junio", "jun"
                NumeroMes = 6
            Case "julio", "jul"
                NumeroMes = 7
            Case "agosto", "ago"
                NumeroMes = 8
            Case "septiembre", "sep"
                NumeroMes = 9
            Case "octubre", "oct"
                NumeroMes = 10
            Case "noviembre", "nov"
                NumeroMes = 11
            Case "diciembre", "dic"
                NumeroMes = 12
        End Select
        Return NumeroMes
    End Function
End Module

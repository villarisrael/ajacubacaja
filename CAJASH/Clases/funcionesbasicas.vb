Imports DevComponents
Imports Microsoft.VisualBasic.DateAndTime
Imports System.Data.Odbc
Imports System.Data
Imports System.IO
Imports System.Reflection


Imports System.Net.NetworkInformation
Imports System.Xml

Module funcionesbasicas


    Public conn As OdbcConnection

    Public dBaseConnection As System.Data.OleDb.OleDbConnection
    Public salariominimo As Double



    Public NumCaja As String, Oficina As String

    Public rema As Double
    Public NumUser As Integer
    Public Tcaja As String, RutaImagenes As String
    Public DirDbase As String = "C:\Prueba"
    '  Public VARIABLE_IVA As Double
    Public ConcepFac As Double = 779
    Public Empresa As String, Direccion As String, Director As String
    Public poblacionEMPRESA As String
    Public Estadoempresa As String
    Public CODPOSempresa As String
    Public coloniaEMPRESA As String
    Public paisEMPRESA As String
    Public LOGOBYTE() As Byte
    Public RFCORGANISMO As String
    Public LMedNuevo As String = "O"
    Public PORC_RECA_OTRO As Integer
    Public TELEMPRESA As String
    Public direccionOrganismo As String
    'Public VARIABLE_IVA As Double = My.Settings.

    Public TIPODESCUENTO = "SOBRE MINIMO" ' "SOBRE TOTAL"

    Public Function UnixDateFormat(ByVal DateString As String, Optional ByVal QuoteIt As Boolean = False, Optional ByVal IncludeTime As Boolean = False) As String
        Dim theTemp As String
        Dim mDia As Int16
        mDia = Day(CDate(DateString))
        If IsDate(DateString) Then
            theTemp = Year(CDate(DateString)) & "-" & Month(CDate(DateString)) & "-" & mDia
            If IncludeTime Then theTemp = theTemp & " " & Mid(DateString, 12, 8)
            If QuoteIt Then theTemp = Quote(theTemp)
        Else
            theTemp = " NULL "
        End If
        UnixDateFormat = theTemp
    End Function
    Public Function Quote(ByVal txt As Object, Optional ByVal IsDateValue As Boolean = False) As String
        If IsDateValue Then
            If IsDate(txt) Then
                Quote = UnixDateFormat(txt, True)
            Else
                Quote = "NULL"
            End If
        Else
            txt = Replace(txt, "'", "\'")
            Do While InStr(txt, "\\'")
                txt = Replace(txt, "\\'", "\'")
            Loop

            Do While InStr(txt, "\\")
                txt = Replace(txt, "\\", "\'")
            Loop
            txt = Replace(txt, "\\'", "\'")

            Quote = "'" & txt & "'"
        End If
    End Function
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


#Region "Operaciones MYSQL"
    Public Sub conectar()
        'Tcaja = param.ObtTipoCaja()
        Try
            conn = New OdbcConnection(My.Settings.baseaguaConnectionString)

            'conn3 = New OdbcConnection("dsn=Agua")
            Application.DoEvents()
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Application.DoEvents()
            'Cobros = New frmCobros

            'dirReport = param.obtRutaRep
            'critRez = param.obtCritRez

            Dim emp As IDataReader = ConsultaSql("select * from empresa").ExecuteReader
            emp.Read()
            RutaImagenes = emp("Rutaimagenes")
            Empresa = emp("cnombre")
            Direccion = emp("cdomicilio")
            Director = emp("cadminis")
            variable_iva = emp("PorcIva") ' /100
            LMedNuevo = emp("SITUACIONNUEVO")
            PORC_RECA_OTRO = emp("PORREC")
            salariominimo = emp("Salario")
            poblacionEMPRESA = emp("cpoblacion")
            Estadoempresa = emp("estado")
            CODPOSempresa = emp("ccodpos")
            paisEMPRESA = emp("pais")
            coloniaEMPRESA = emp("ccolonia")
            LOGOBYTE = emp("LOGO")
            RFCORGANISMO = emp("CNIF")
            TELEMPRESA = emp("CTLF")
            'Cargar a LMedNuevo el valor de la situacion de nuevo en la bd
        Catch ex As Odbc.OdbcException
            MsgBox(ex.Message.ToString)
        End Try
    End Sub

    Public Sub desconectar()
        conn.Close()
    End Sub


    Public Function llenaGrid(ByVal grid As DataGridView, ByVal txtSql As String) As Collection
        llenaGrid = New Collection
        Dim bs As New BindingSource
        Dim sql1 As OdbcCommand = New OdbcCommand
        Dim da As New OdbcDataAdapter(sql1)
        Dim sqlCommandBuilder As New OdbcCommandBuilder(da)
        Dim ds As New System.Data.DataSet
        sql1.Connection = conn
        sql1.CommandText = txtSql.ToLower()
        sql1.CommandType = CommandType.Text
        da.Fill(ds)
        '  Application.DoEvents()
        bs.DataSource = ds.Tables(0)
        grid.DataSource = bs 'ds.Tables(0)
        grid.Refresh()
        Try
            If grid.Rows.Count <> 0 Then
                grid.ClearSelection()
                grid.CurrentCell = grid(0, grid.RowCount - 1)
            End If
        Catch ex As Exception
            If grid.Rows.Count <> 0 Then
                Try
                    grid.ClearSelection()
                    grid.CurrentCell = grid(1, grid.RowCount - 1)
                Catch ex1 As Exception
                    Exit Try
                End Try
            End If
            'Throw New ArgumentOutOfRangeException("Salir")
        End Try
        llenaGrid.Add(bs)
        llenaGrid.Add(da)
    End Function

    Public Function llenarVista(ByVal txtSql As String) As DataView
        Try
            txtSql = txtSql.ToLower()
            Dim sql1 As OdbcCommand = New OdbcCommand
            Dim da As New OdbcDataAdapter(sql1)
            Dim ds As New System.Data.DataSet
            sql1.Connection = conn
            sql1.CommandText = txtSql.ToLower()
            sql1.CommandType = CommandType.Text
            da.Fill(ds)
            Application.DoEvents()
            llenarVista = New DataView(ds.Tables(0))
        Catch ex As Exception
            'MessageBoxEx.Show(ex.Message)
            Return New DataView()
        End Try
    End Function

    Public Sub llenarCombo(ByVal combo As ComboBox, ByVal txtSql As String)
        ' txtSql = txtSql.ToLower()
        Dim da As New OdbcDataAdapter(txtSql, conn)
        Dim dt As New DataTable
        Try
            da.Fill(dt)
            Application.DoEvents()
            combo.DataSource = dt
            combo.ValueMember = dt.Columns(0).ToString
            combo.DisplayMember = dt.Columns(1).ToString
            combo.SelectedIndex = -1
        Catch ex As Exception
            ' MessageBox.Show(ex.Message())
            'MessageBoxEx.Show("Posible perdida de conexión")
        End Try
    End Sub


    Public Sub llenarCombo2(ByVal combo As ComboBox, ByVal txtSql As String)
        txtSql = txtSql.ToLower()
        Dim comando As New OdbcCommand(txtSql, conn)
        Dim reader As IDataReader = comando.ExecuteReader()


        Dim dt As New DataTable
        Try
            dt.Load(reader)
            Application.DoEvents()
            combo.DataSource = dt
            combo.ValueMember = dt.Columns(0).ToString
            combo.DisplayMember = dt.Columns(1).ToString
            combo.SelectedIndex = -1
        Catch ex As Exception
            MessageBox.Show(ex.Message())
            'MessageBoxEx.Show("Posible perdida de conexión")
        End Try
    End Sub
    Public Sub llenarCombo(ByVal combo As ToolStripComboBox, ByVal txtSql As String)
        'txtSql = txtSql.ToLower()
        Dim da As New OdbcDataAdapter(txtSql, conn)
        Dim dt As New DataTable
        da.Fill(dt)
        combo.ComboBox.DataSource = dt
        combo.ComboBox.ValueMember = dt.Columns(0).ToString
        combo.ComboBox.DisplayMember = dt.Columns(1).ToString
        combo.Text = ""
    End Sub

    Public Sub llenarCombo(ByVal combo As ComboBox, ByVal campo As String, ByVal txtSql As String)
        '    txtSql = txtSql.ToLower()
        Dim sql As OdbcCommand = New OdbcCommand
        Dim da As New OdbcDataAdapter(sql)
        Dim ds As New DataSet
        sql.Connection = conn
        sql.CommandText = txtSql
        sql.CommandType = CommandType.Text
        Dim dr As System.Data.IDataReader
        dr = sql.ExecuteReader()
        combo.Items.Clear()
        While dr.Read()
            Application.DoEvents()
            combo.Items.Add(dr(campo))
        End While
    End Sub

    Public Sub llenarCombo(ByVal combo As DevComponents.DotNetBar.ComboBoxItem, ByVal campo As String, ByVal txtSql As String)
        Dim sql As OdbcCommand = New OdbcCommand
        Dim da As New OdbcDataAdapter(sql)
        Dim ds As New DataSet
        sql.Connection = conn
        sql.CommandText = txtSql
        sql.CommandType = CommandType.Text
        Dim dr As System.Data.IDataReader
        dr = sql.ExecuteReader()
        combo.Items.Clear()
        While dr.Read()
            Application.DoEvents()
            combo.Items.Add(dr(campo))
        End While
    End Sub

    Public Sub llenarCombo(ByVal combo As System.Windows.Forms.ToolStripComboBox, ByVal campo As String, ByVal txtSql As String)
        Dim sql As OdbcCommand = New OdbcCommand
        Dim da As New OdbcDataAdapter(sql)
        Dim ds As New DataSet
        sql.Connection = conn
        sql.CommandText = txtSql
        sql.CommandType = CommandType.Text
        Dim dr As System.Data.IDataReader
        dr = sql.ExecuteReader()
        combo.Items.Clear()
        While dr.Read()
            Application.DoEvents()
            combo.Items.Add(dr(campo))
        End While
    End Sub

    Public Function ConsultaSql(ByVal txtSql As String) As OdbcCommand
        ConsultaSql = New OdbcCommand
        '   Dim da As New OdbcDataAdapter(ConsultaSql)
        '  Dim ds As New DataSet
        ConsultaSql.Connection = conn
        ConsultaSql.CommandText = txtSql
        'On Error Resume Next
        'Application.DoEvents()
        ConsultaSql.CommandType = CommandType.Text
    End Function

    Public Sub llenarCombo(ByVal combo As ComboBox, ByVal campo As String, ByVal campo1 As String, ByVal txtSql As String)
        Dim sql As OdbcCommand = New OdbcCommand
        Dim da As New OdbcDataAdapter(sql)
        Dim ds As New DataSet
        Dim cadena As String
        sql.Connection = conn
        sql.CommandText = txtSql
        sql.CommandType = CommandType.Text
        Dim dr As System.Data.IDataReader
        dr = sql.ExecuteReader()
        combo.Items.Clear()
        While dr.Read()
            Application.DoEvents()
            cadena = String.Concat(dr(campo), ", ", dr(campo1))
            combo.Items.Add(cadena)
        End While
    End Sub

    Public Sub llenarCombo(ByVal combo As ComboBox, ByVal campo As String, ByVal campo1 As String, ByVal campo2 As String, ByVal txtSql As String)
        Dim sql As OdbcCommand = New OdbcCommand
        Dim da As New OdbcDataAdapter(sql)
        Dim ds As New DataSet
        Dim cadena As String
        sql.Connection = conn
        sql.CommandText = txtSql
        sql.CommandType = CommandType.Text
        Dim dr As System.Data.IDataReader
        dr = sql.ExecuteReader()
        combo.Items.Clear()
        While dr.Read()
            Application.DoEvents()
            cadena = String.Concat(dr(campo), ", ", dr(campo1), ", ", dr(campo2))
            combo.Items.Add(cadena)
        End While
    End Sub

    Public Sub llenarCombo(ByVal combo As ComboBox, ByVal campo As String, ByVal campo1 As String, ByVal campo2 As String, ByVal campo3 As String, ByVal txtSql As String, ByVal delimitador As String)
        Dim sql As OdbcCommand = New OdbcCommand
        Dim da As New OdbcDataAdapter(sql)
        Dim ds As New DataSet
        Dim cadena As String
        sql.Connection = conn
        sql.CommandText = txtSql
        sql.CommandType = CommandType.Text
        Dim dr As System.Data.IDataReader
        dr = sql.ExecuteReader()
        combo.Items.Clear()
        While dr.Read()
            cadena = String.Concat(dr(campo), delimitador, dr(campo1), delimitador, dr(campo2))
            combo.Items.Add(cadena)
        End While
    End Sub

    Public Sub llenarLista(ByVal Lista As ListBox, ByVal campo As String, ByVal txtSql As String)
        Dim sql As OdbcCommand = New OdbcCommand
        Dim da As New OdbcDataAdapter(sql)
        Dim ds As New DataSet
        sql.Connection = conn
        sql.CommandText = txtSql
        sql.CommandType = CommandType.Text
        Dim dr As System.Data.IDataReader
        dr = sql.ExecuteReader()
        While dr.Read()
            Try
                Lista.Items.Add(dr(campo))
            Catch ex As Exception
                Lista.Items.Add(dr(0))
            End Try
        End While
    End Sub

    Public Sub Ejecucion(ByVal txtSql As String, Optional ByVal Remoto As Int16 = 0)
        Dim cmd As New OdbcCommand
        cmd.Connection = conn
        cmd.CommandText = txtSql
        'Application.DoEvents()
        cmd.ExecuteNonQuery()

        'End Try
        If My.Settings.escajamovil.ToLower = "si" Then
            Dim arch As New clsDocumentoTXT
            arch.guardartxt(txtSql, "c:\cajamovil\" & My.Settings.caja & My.Settings.serie & Now.ToString("yyyyMMdd") & ".SQL")
        End If
    End Sub




    Public Function obtenerCampo(ByVal sql As String, ByVal campo As String) As String
        Dim comDatos As OdbcCommand = New OdbcCommand
        comDatos.Connection = conn
        comDatos.CommandText = sql
        comDatos.CommandType = CommandType.Text
        Try
            Dim dr As System.Data.IDataReader
            dr = comDatos.ExecuteReader()
            ' Application.DoEvents()
            dr.Read()

            obtenerCampo = dr(campo).ToString
        Catch ex As Exception
            Return "0"
        End Try

        If obtenerCampo <> "" Then
            Return obtenerCampo
        Else
            Return "0"
        End If
    End Function

#End Region
    Function ConvertToDataTable(ByVal list As Collection) As DataTable
        Dim table As New DataTable()
        Dim fields() As FieldInfo = GetType(Clsconcepto).GetFields()
        For Each field As FieldInfo In fields
            table.Columns.Add(field.Name, field.FieldType)
        Next
        For Each item As Clsconcepto In list
            Dim row As DataRow = table.NewRow()
            For Each field As FieldInfo In fields
                row(field.Name) = field.GetValue(item)
            Next
            table.Rows.Add(row)
        Next
        Return table
    End Function


    Public Sub guardatxt(ByVal directorio As String, ByVal archivo As String, ByVal frase As String)
        Dim myStreamWriter As System.IO.StreamWriter
        If Not My.Computer.FileSystem.DirectoryExists(directorio) Then
            My.Computer.FileSystem.CreateDirectory(directorio)
        End If
        Try
            myStreamWriter = System.IO.File.AppendText(archivo)

            myStreamWriter.Write(frase & ";" & Chr(13))
            myStreamWriter.Flush()
            myStreamWriter.Close()
        Catch ex As Exception

        End Try
    End Sub

    Public Function ImageToByte(ByVal pImagen As String) As Byte()

        Dim codigo As New System.IO.FileStream(pImagen, IO.FileMode.Open, IO.FileAccess.Read)
        Dim b(codigo.Length() - 1) As Byte
        codigo.Read(b, 0, b.Length)
        codigo.Close()
        Return b
    End Function


    Public Function NOMBREDEMES3CAR(ByVal MES As Integer) As String
        Dim mescad As String = ""
        Select Case MES
            Case 1
                mescad = "ENE"
            Case 2
                mescad = "FEB"
            Case 3
                mescad = "MAR"
            Case 4
                mescad = "ABR"
            Case 5
                mescad = "MAY"
            Case 6
                mescad = "JUN"
            Case 7
                mescad = "JUL"
            Case 8
                mescad = "AGO"
            Case 9
                mescad = "SEP"
            Case 10
                mescad = "OCT"
            Case 11
                mescad = "NOV"
            Case 12
                mescad = "DIC"
        End Select
        Return mescad
    End Function
    Function getMacAddress()
        Dim nics() As NetworkInterface = NetworkInterface.GetAllNetworkInterfaces()
        Return nics(1).GetPhysicalAddress.ToString
    End Function

    Public Sub RestaurarCreditosUsuario(ByVal cuentaP As Integer, ByVal serieP As String, ByVal reciboP As Integer)

        Dim creditoActualizado As Decimal = 0.0

        Dim creditoUsuario As Decimal = obtenerCampo($"select credito from usuario where cuenta = {cuentaP}", "credito")

        Dim valePago As Decimal = obtenerCampo($"select vale from pagos where serie = '{serieP}' and recibo = {reciboP}", "vale")

        creditoActualizado = creditoUsuario + (valePago * -1)

        Ejecucion($"UPDATE USUARIO SET CREDITO = {creditoActualizado} WHERE CUENTA = {cuentaP}")

        'Try
        '    Dim unsed6 = EjecutarConsultaRemotaAsync($"UPDATE USUARIO SET CREDITO = {creditoActualizado} WHERE CUENTA = {cuentaP}")
        'Catch ex As Exception

        'End Try


    End Sub


    Public Function ObtenerDatosXML(SerieP As String, FolioP As Integer) As XmlDocument

        Try


            ' Cargar XML en un objeto XmlDocument
            Dim xmlDoc As New XmlDocument()

            Dim CFDI As String = obtenerCampo($"SELECT CFDI FROM ENCFAC WHERE SERIE = '{SerieP}' AND NUMERO = {FolioP}", "CFDI")

            xmlDoc.LoadXml(CFDI.TrimStart().TrimEnd())

            Return xmlDoc

        Catch ex As Exception

            MessageBox.Show($"Ocurrio un error al obetener los datos del XML: {ex.ToString()}")

        End Try

    End Function


    Public Function ObtenerDatosFiscales(nombreReceptor As String, cuentaUsuarioP As String, tipoUsuario As Short) As DatosFiscalesUsuario
        Dim datosFiscalesUsuario As New DatosFiscalesUsuario()

        Try

            'Dim tipoUsuario As String = ConvertirTipoUsuarioInverso(tipoUsuario)

            Using datosFiscales As IDataReader = ConsultaSql($"SELECT * FROM datosfiscales WHERE datosfiscales.nombre='{nombreReceptor}' AND CUENTA = '{cuentaUsuarioP}' AND TIPO = '{tipoUsuario}'").ExecuteReader()
                If datosFiscales.Read() Then

                    datosFiscalesUsuario.RazonSocial = datosFiscales("NOMBRE").ToString().Trim()
                    datosFiscalesUsuario.cuenta = Convert.ToInt32(datosFiscales("CUENTA"))
                    datosFiscalesUsuario.calleUsuario = datosFiscales("CALLE").ToString().Trim()
                    datosFiscalesUsuario.numInteriorUsuario = datosFiscales("NUMINT").ToString().Trim()
                    datosFiscalesUsuario.numExteriorUsuario = datosFiscales("NUMEXT").ToString().Trim()
                    datosFiscalesUsuario.coloniaUsuario = datosFiscales("COLONIA").ToString().Trim()
                    datosFiscalesUsuario.tipoUsuario = datosFiscales("TIPO").ToString().Trim()
                    datosFiscalesUsuario.poblacionUsuario = datosFiscales("POBLACION").ToString().Trim()
                    datosFiscalesUsuario.delegacionUsuario = datosFiscales("DELEGACION").ToString().Trim()
                    datosFiscalesUsuario.estadoUsuario = datosFiscales("ESTADO").ToString().Trim()
                    datosFiscalesUsuario.CPUsuario = datosFiscales("CP").ToString().Trim()
                    datosFiscalesUsuario.RegimenFiscal = datosFiscales("RegimenFiscal").ToString().Trim()

                End If
            End Using

        Catch ex As Exception

            MessageBox.Show($"Ocurrio un error al obteenr la infromación del usuario: {ex.ToString()}")

        End Try

        Return datosFiscalesUsuario

    End Function

    Public Function ObtenerDatosFacturaPeriodo(nombreReceptor As String) As DatosFiscalesUsuario
        Dim datosFiscalesUsuario As New DatosFiscalesUsuario()

        Try



            'Using datosFiscales As IDataReader = ConsultaSql($"SELECT * FROM datosfiscales WHERE datosfiscales.nombre='{nombreReceptor}' AND CUENTA = '{cuentaUsuarioP}'").ExecuteReader()
            '    If datosFiscales.Read() Then

            datosFiscalesUsuario.RazonSocial = "PUBLICO EN GENERAL".Trim()
            datosFiscalesUsuario.cuenta = ""
            datosFiscalesUsuario.calleUsuario = $"{Direccion}".Trim()
            datosFiscalesUsuario.numInteriorUsuario = ""
            datosFiscalesUsuario.numExteriorUsuario = ""
            datosFiscalesUsuario.coloniaUsuario = $"{coloniaEMPRESA}".Trim()
            datosFiscalesUsuario.tipoUsuario = $"PERIODO".Trim()
            datosFiscalesUsuario.poblacionUsuario = $"{poblacionEMPRESA}".Trim()
            datosFiscalesUsuario.delegacionUsuario = $"{poblacionEMPRESA}".Trim()
            datosFiscalesUsuario.estadoUsuario = $"{Estadoempresa}".Trim()
            datosFiscalesUsuario.CPUsuario = $"{CODPOSempresa}".Trim()
            datosFiscalesUsuario.RegimenFiscal = $"".Trim()

            'End If
            'End Using

        Catch ex As Exception

            MessageBox.Show($"Ocurrio un error al obtener la información del usuario: {ex.ToString()}")

        End Try

        Return datosFiscalesUsuario

    End Function


    Public Function ConvertirTipoUsuario(tipo As String) As Integer
        Select Case tipo.ToUpper() ' Convertimos a mayúsculas para evitar errores de minúsculas
            Case "USUARIO"
                Return 1
            Case "CLIENTE"
                Return 2
            Case "NO USUARIO"
                Return 2
            Case "FACTIBILIDAD"
                Return 3
            Case "SOLICITUD"
                Return 3
            Case "PERIODO"
                Return 4
            Case Else
                Return 0 ' Devuelve 0 si el tipo no es válido
        End Select
    End Function

    Public Function ConvertirTipoUsuarioInverso(tipo As Short) As String
        Select Case tipo
            Case 1
                Return "USUARIO"
            Case 2
                Return "CLIENTE"
            Case 3
                Return "FACTIBILIDAD"
            Case 4
                Return "PERIODO"
            Case Else
                Return "DESCONOCIDO"
        End Select

    End Function


    Public Function SafeConvertToDateTime(value As Object) As DateTime
        If value IsNot DBNull.Value AndAlso value IsNot Nothing Then
            Return Convert.ToDateTime(value)
        Else
            Return DateTime.MinValue ' O cualquier fecha predeterminada
        End If
    End Function

    Public Function SafeConvertToDecimal(value As Object) As Decimal
        If value IsNot DBNull.Value AndAlso value IsNot Nothing Then
            Return Convert.ToDecimal(value)
        Else
            Return 0D ' Valor predeterminado para decimales
        End If
    End Function

    Public Function SafeConvertToString(value As Object) As String
        If value IsNot DBNull.Value AndAlso value IsNot Nothing Then
            Return value.ToString()
        Else
            Return String.Empty ' Cadena vacía predeterminada
        End If
    End Function

    Public Function SafeConvertToInt(value As Object) As Integer
        If value IsNot DBNull.Value AndAlso value IsNot Nothing Then
            Return Convert.ToUInt32(value)
        Else
            Return 0 ' Cadena vacía predeterminada
        End If
    End Function

End Module
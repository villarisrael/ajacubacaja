
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class FrmHistorial
    Public cuenta As Long = 0
    Public tipoUsuario As Int16 = 0

    Private Sub FrmHistorial_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Hide()
        End If
    End Sub
    Private Sub FrmHistorial_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '''''
        If My.Settings.esAdministrativa.ToUpper() = "DIRECTOR" And My.Settings.tipocaja.ToUpper() = "CONSULTA" Then

            btnpagado.Visible = True
            Btnhabilitar.Visible = True

        Else

            btnpagado.Visible = False
            Btnhabilitar.Visible = False

        End If


        Dim x2 As New base
        Dim x As New base
        Try
            If cuenta > 0 Then


                Dim datoshis As Odbc.OdbcDataReader = x.consultasql("SELECT FECHA_ACT AS FECHA,serie, RECIBO, PERIODO,PAGOS AS SUBTOTAL, OTROS, IVA,  TOTAL, Descuento as DESCUENTO, OBSERVACION FROM PAGOS WHERE CANCELADO='A' AND  CUENTA=" & cuenta & " and esusuario=1")

                While (datoshis.Read)
                    Dim nuevonodo As New DevComponents.AdvTree.Node
                    Try
                        nuevonodo.Cells(0).Text = datoshis!Fecha
                    Catch ex As Exception

                    End Try
                    Try
                        Dim celda As New DevComponents.AdvTree.Cell
                        celda.Text = datoshis!serie & datoshis!recibo
                        nuevonodo.Cells.Add(celda)

                    Catch ex As Exception

                    End Try
                    Try
                        Dim celdaper As New DevComponents.AdvTree.Cell
                        celdaper.Text = datoshis!periodo
                        nuevonodo.Cells.Add(celdaper)

                    Catch ex As Exception

                    End Try
                    Try
                        Dim celda2 As New DevComponents.AdvTree.Cell

                        Dim etiqueta As New DevComponents.DotNetBar.LabelX
                        celda2.HostedControl = etiqueta
                        etiqueta.Text = datoshis!subtotal
                        etiqueta.TextAlignment = StringAlignment.Far
                        nuevonodo.Cells.Add(celda2)

                    Catch ex As Exception

                    End Try
                    Try
                        Dim celda3 As New DevComponents.AdvTree.Cell
                        Dim etiqueta2 As New DevComponents.DotNetBar.LabelX
                        celda3.HostedControl = etiqueta2
                        etiqueta2.Text = datoshis!IVA
                        etiqueta2.TextAlignment = StringAlignment.Far

                        nuevonodo.Cells.Add(celda3)

                    Catch ex As Exception

                    End Try
                    Try
                        Dim celda4 As New DevComponents.AdvTree.Cell
                        Dim etiqueta3 As New DevComponents.DotNetBar.LabelX
                        celda4.HostedControl = etiqueta3
                        etiqueta3.Text = datoshis!total
                        etiqueta3.TextAlignment = StringAlignment.Far

                        nuevonodo.Cells.Add(celda4)

                    Catch ex As Exception

                    End Try
                    Try
                        Dim celda5 As New DevComponents.AdvTree.Cell
                        celda5.Text = datoshis!descuento
                        nuevonodo.Cells.Add(celda5)

                    Catch ex As Exception

                    End Try
                    Try
                        Dim celda6 As New DevComponents.AdvTree.Cell
                        celda6.Text = datoshis!observacion
                        nuevonodo.Cells.Add(celda6)

                    Catch ex As Exception

                    End Try
                    Try

                        Dim datosesclavo As Odbc.OdbcDataReader = x2.consultasql("select * from pagotros where recibo=" & datoshis!recibo & " and serie='" & datoshis!serie & "'")
                        nuevonodo.NodesColumns.Add(New DevComponents.AdvTree.ColumnHeader("Cantidad"))
                        nuevonodo.NodesColumns(0).Width.Absolute = 50
                        nuevonodo.NodesColumns.Add(New DevComponents.AdvTree.ColumnHeader("Concepto"))
                        nuevonodo.NodesColumns(1).Width.Absolute = 350
                        nuevonodo.NodesColumns.Add(New DevComponents.AdvTree.ColumnHeader("Precio Uni"))
                        nuevonodo.NodesColumns(2).Width.Absolute = 140
                        nuevonodo.NodesColumns.Add(New DevComponents.AdvTree.ColumnHeader("Importe"))
                        nuevonodo.NodesColumns(3).Width.Absolute = 140

                        nuevonodo.NodesColumns.Add(New DevComponents.AdvTree.ColumnHeader("Con Iva"))
                        nuevonodo.NodesColumns(4).Width.Absolute = 50
                        While (datosesclavo.Read)


                            Dim nuevoesclavo As New DevComponents.AdvTree.Node
                            Try
                                nuevoesclavo.Cells(0).Text = datosesclavo!Cantidad
                            Catch ex As Exception

                            End Try
                            Try
                                Dim celda As New DevComponents.AdvTree.Cell
                                celda.Text = datosesclavo!concepto
                                nuevoesclavo.Cells.Add(celda)

                            Catch ex As Exception

                            End Try
                            Try
                                Dim celda8 As New DevComponents.AdvTree.Cell

                                Dim etiqueta8 As New DevComponents.DotNetBar.LabelX
                                celda8.HostedControl = etiqueta8
                                etiqueta8.Text = datosesclavo!importe
                                etiqueta8.TextAlignment = StringAlignment.Far
                                etiqueta8.ForeColor = Color.Blue
                                nuevoesclavo.Cells.Add(celda8)

                            Catch ex As Exception

                            End Try
                            Try
                                Dim celda9 As New DevComponents.AdvTree.Cell

                                Dim etiqueta9 As New DevComponents.DotNetBar.LabelX
                                celda9.HostedControl = etiqueta9
                                etiqueta9.Text = datosesclavo!monto
                                etiqueta9.TextAlignment = StringAlignment.Far
                                etiqueta9.ForeColor = Color.Blue
                                nuevoesclavo.Cells.Add(celda9)

                            Catch ex As Exception

                            End Try
                            Try
                                Dim celdaiva As New DevComponents.AdvTree.Cell
                                celdaiva.Text = datosesclavo!iva
                                nuevoesclavo.Cells.Add(celdaiva)

                            Catch ex As Exception

                            End Try
                            nuevonodo.Nodes.Add(nuevoesclavo)
                        End While

                    Catch ex As Exception

                    End Try
                    AdvHistorial.Nodes.Add(nuevonodo)

                End While



            End If
        Catch ex As Exception

        End Try

        Try
            If cuenta > 0 Then

                'x.llenaGrid(DGlecturas, "SELECT MES AS MES,AN_PER AS AÑO, LECTURA AS LECTURA , LECTANT AS ANTERIOR, CONSUMO ,consumocobrado as 'CONSUMO COBRADO' ,concat(sit_pad, sit_med, sit_hid) as SITUACION, montocobrado as 'MONTO COBRADO', MONTO AS  TOTAL, case when pagado=1 then 'SI' else 'NO' end as PAGADO FROM LECTURAS WHERE CUENTA =" & cuenta & " order by valornummes(mes,an_per)")


                Dim SQL As String = "SELECT MES AS MES,AN_PER AS AÑO, LECTURA AS LECTURA , LECTANT AS ANTERIOR, CONSUMO ,consumocobrado as 'CONSUMO COBRADO' ,concat(sit_pad, sit_med, sit_hid) as SITUACION,  montocobrado as 'MONTO COBRADO', MONTO AS  TOTAL,  case when adelant=1 then 'SI' else 'NO' end as 'L. Adelantada', case when pagado=1 then 'SI' else 'NO' end as PAGADO, observa  FROM lecturas WHERE CUENTA =" & cuenta & " order by valornummes(mes,an_per);"
                x.llenaGrid(DGlecturas, SQL)

                DGlecturas.Columns(0).Width = 50
                DGlecturas.Columns(1).Width = 50
                DGlecturas.Columns(2).Width = 70
                DGlecturas.Columns(3).Width = 70
                DGlecturas.Columns(4).Width = 80
                DGlecturas.Columns(5).Width = 80
                DGlecturas.Columns(6).Width = 80
                DGlecturas.Columns(7).Width = 110
                DGlecturas.Columns(8).Width = 110
                DGlecturas.Columns(9).Width = 60

                DGlecturas.Columns(7).DefaultCellStyle.Format() = "C2"
                DGlecturas.Columns(8).DefaultCellStyle.Format() = "C2"
                'DGlecturas.Columns(3).DefaultCellStyle.Format() = "C2"
                'DGlecturas.Columns(4).DefaultCellStyle.Format() = "C2"
                '   DGlecturas.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                '  DGlecturas.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DGlecturas.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DGlecturas.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DGlecturas.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DGlecturas.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DGlecturas.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                DGlecturas.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                DGlecturas.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                DGlecturas.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


                For Each row As DataGridViewRow In DGlecturas.Rows
                    If row.Cells("PAGADO").Value = "NO" Then
                        row.DefaultCellStyle.ForeColor = Color.Black
                        row.DefaultCellStyle.BackColor = Color.LightGreen
                        ' row.DefaultCellStyle.Font. = True
                    End If
                Next
                'x.conexion.Dispose()
            End If

        Catch ex As Exception

        End Try

        Try

            'x.llenaGrid(DGHistorialAnticipos, "SELECT CLAVE, Ofi_Pago as 'OFICINA DE PAGO', CAJA, CONSUMO, ALCANTARILLADO, SANEAMIENTO, MONTO, status as 'STATUS' FROM ANTICIPOS WHERE CUENTA = " & cuenta & " ")
            x.llenaGrid(DGHistorialAnticipos, "SELECT CLAVE, FECHA as 'FECHA DE PAGO',  Ofi_Pago 'OFICINA DE PAGO', CAJA, CONSUMO, ALCANTARILLADO, SANEAMIENTO, MONTO, status as 'STATUS' FROM anticipos WHERE CUENTA = " & cuenta & " order by date(FECHA);")

            DGHistorialAnticipos.Columns(0).Width = 50
            DGHistorialAnticipos.Columns(1).Width = 110
            DGHistorialAnticipos.Columns(2).Width = 120

            DGHistorialAnticipos.Columns(3).Width = 40

            DGHistorialAnticipos.Columns(4).Width = 100
            DGHistorialAnticipos.Columns(5).Width = 100
            DGHistorialAnticipos.Columns(6).Width = 100
            DGHistorialAnticipos.Columns(7).Width = 100
            DGHistorialAnticipos.Columns(8).Width = 50

        Catch ex As Exception

        End Try
        x.conexion.Dispose()
        x2.conexion.Dispose()

    End Sub



    Private Sub btnimprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimprimir.Click
        Try



            GenerarReporteHistorialPago(cuenta, tipoUsuario)


        Catch ex As Exception

            MessageBox.Show("Hay un problema para generar el reporte")

        End Try
    End Sub

    Private Sub btnimprimirlecturas_Click(sender As Object, e As EventArgs) Handles btnimprimirlecturas.Click

        'Generar Reporte con ItextSharp
        Dim SQL As String = "SELECT MES AS MES,AN_PER AS PERIODO, LECTURA AS LECTURA , LECTANT AS ANTERIOR, CONSUMO ,consumocobrado as 'CONSUMO COBRADO' ,concat(sit_pad, sit_med, sit_hid) as SITUACION,  montocobrado as 'MONTO COBRADO', MONTO AS  TOTAL,  case when adelant=1 then 'SI' else 'NO' end as 'L. Adelantada', case when pagado=1 then 'SI' else 'NO' end as PAGADO, observa  FROM lecturas WHERE CUENTA =" & cuenta & " order by valornummes(mes,an_per);"

        Dim reporteLecturas As New HistorialLecturas()
        reporteLecturas.GenerarReporteHistorialLecturas(SQL, cuenta)



        'Dim reporte As New ReportDocument()

        'Try
        '    reporte.Load(AppPath() & ".\Reportes\historiallecturas.rpt")

        '    Dim servidorreporte As String = My.Settings.servidorreporte
        '    Dim usuarioreporte As String = My.Settings.usuarioreporte
        '    Dim passreporte As String = My.Settings.passreporte
        '    Dim basereporte As String = My.Settings.basereporte

        '    reporte.DataSourceConnections.Item(0).SetConnection(servidorreporte, basereporte, False)
        '    reporte.DataSourceConnections.Item(0).SetLogon(usuarioreporte, passreporte)


        '    'reporte.RecordSelectionFormula = "{vconcentradogeneral1.cuenta}=" & cuenta & " and ({vlecturas1.AN_PER}=" & Now.Year & " or {vlecturas1.AN_PER}=" & Now.Year - 1 & ")"

        '    reporte.RecordSelectionFormula = "{vlecturas1.AN_PER}=" & Now.Year & " or {vlecturas1.AN_PER}=" & Now.Year - 1

        '    Dim cadenapdf As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\Historial_lecturas" & cuenta & ".pdf"

        '    ExportToDisk(cadenapdf, reporte)

        '    Try
        '        Dim psi As New ProcessStartInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\Historial_lecturas" & cuenta & ".pdf")
        '        psi.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        '        psi.UseShellExecute = False
        '        psi.WindowStyle = ProcessWindowStyle.Maximized
        '        Dim p As Process = Process.Start(psi)
        '    Catch ex As Exception
        '        MessageBox.Show("error al visualizar el pdf, el archivo fue creado: " & cadenapdf)
        '    End Try
        'Catch ex As Exception
        '    MessageBox.Show("Eroor " & ex.Message)
        'End Try


    End Sub


    Private Sub btnpagado_Click(sender As Object, e As EventArgs) Handles btnpagado.Click
        Dim mes As String = String.Empty
        Dim ano As String = String.Empty

        Try
            mes = DGlecturas.SelectedRows.Item(0).Cells(0).Value.ToString()
            ano = DGlecturas.SelectedRows.Item(0).Cells(1).Value.ToString()

            Ejecucion("update lecturas set pagado =1 where cuenta = " & cuenta & " and mes='" & mes & "' and an_per =" & ano)

            'Registrar el movimiento en la bitacora
            Dim nombre_Host As String = Net.Dns.GetHostName()
            Dim este_Host As Net.IPHostEntry = Net.Dns.GetHostEntry(nombre_Host)
            Dim direccion_Ip As String = este_Host.AddressList(0).ToString


            Try
                Ejecucion("insert into bitacora(fecha,hora,evento,cuenta,usuario,concepto,maquina,motivo) values(" & UnixDateFormat(Now.Date, True, False) & ",'" & Now.ToShortTimeString() & "','LECTURA PAGADA'," & cuenta & "," & NumUser & ",' LECTURA DADA POR PAGADA: " & mes & "  AÑO: " & ano & "','" & direccion_Ip & "',' MODIFICACION DE LECTURA ')")

                ' MessageBoxEx.Show("Registro realizado satisfactoriamente", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'ButtonX1.Enabled = True

                'frmBusUsuario.filtro(frmBusUsuario._sqlgeneral)
                'BtnAceptar.Enabled = False
            Catch ex As Exception
                MessageBox.Show("Ocurrio un error al registar en la bitacora: " & ex.ToString())
            End Try

            FrmHistorial_Load(sender, e)
        Catch ex As Exception
            '  Dim error As String = ex.Message
        End Try




    End Sub

    Private Sub Btnhabilitar_Click(sender As Object, e As EventArgs) Handles Btnhabilitar.Click
        Dim mes As String = String.Empty
        Dim ano As String = String.Empty

        Try
            mes = DGlecturas.SelectedRows.Item(0).Cells(0).Value.ToString()
            ano = DGlecturas.SelectedRows.Item(0).Cells(1).Value.ToString()

            Ejecucion("update lecturas set pagado =0 where cuenta = " & cuenta & " and mes='" & mes & "' and an_per =" & ano)
            FrmHistorial_Load(sender, e)


            'Registrar el movimiento en la bitacora
            Dim nombre_Host As String = Net.Dns.GetHostName()
            Dim este_Host As Net.IPHostEntry = Net.Dns.GetHostEntry(nombre_Host)
            Dim direccion_Ip As String = este_Host.AddressList(0).ToString


            Try
                Ejecucion("insert into bitacora(fecha,hora,evento,cuenta,usuario,concepto,maquina,motivo) values(" & UnixDateFormat(Now.Date, True, False) & ",'" & Now.ToShortTimeString() & "','LECTURA HABILITADA PARA PAGO'," & cuenta & "," & NumUser & ",' LECTURA HABILITADA PARA PAGO: " & mes & "  AÑO: " & ano & "','" & direccion_Ip & "',' MODIFICACION DE LECTURA ')")

                ' MessageBoxEx.Show("Registro realizado satisfactoriamente", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'ButtonX1.Enabled = True

                'frmBusUsuario.filtro(frmBusUsuario._sqlgeneral)
                'BtnAceptar.Enabled = False
            Catch ex As Exception
                MessageBox.Show("Ocurrio un error al registar en la bitacora: " & ex.ToString())
            End Try


        Catch ex As Exception
            '  Dim error As String = ex.Message
        End Try

    End Sub

    Private Sub GenerarReporteHistorialPago(ByVal contratoP As Integer, ByVal tipoUsuario As Integer)




        Try



            'Crear el directorio en donde se van a almacenar los PDF
            If Not My.Computer.FileSystem.DirectoryExists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\HistorialPago\" & Year(Now) & acompletacero(Month(Now).ToString(), 2).Trim) Then

                My.Computer.FileSystem.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\HistorialPago\" & Year(Now) & acompletacero(Month(Now).ToString(), 2).Trim)
            End If

            Dim cadenafolder As String = (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\HistorialPago\" & Year(Now) & acompletacero(Month(Now).ToString(), 2)).Trim

            Dim nombrearchivo As String = cadenafolder & "\Historial_de_Pago_Cuenta_" & cuenta & ".pdf"


            'Dar propiedades al Documento
            Dim pdfDoc As New Document(iTextSharp.text.PageSize.LETTER, 15.0F, 15.0F, 20.0F, 20.0F)

            'Obtener la ruta donde se va a crear el pdf
            Dim pdfWrite As PdfWriter = PdfWriter.GetInstance(pdfDoc, New System.IO.FileStream(cadenafolder & "\Historial_de_Pago_Cuenta_" & cuenta & ".pdf", FileMode.Create))

            'Formato de letras
            Dim Font8 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.NORMAL))
            Dim Font5 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 5, iTextSharp.text.Font.NORMAL))
            Dim Font7 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 7, iTextSharp.text.Font.NORMAL))
            Dim Font6 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 6, iTextSharp.text.Font.NORMAL))
            Dim Font4 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 4, iTextSharp.text.Font.NORMAL))
            Dim Font8N As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.BOLD))
            Dim Font13N As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 13, iTextSharp.text.Font.BOLD))
            Dim Font10N As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.BOLD))
            Dim Font10 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.NORMAL))
            Dim Font9 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 9, iTextSharp.text.Font.NORMAL))
            Dim Font9N As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 9, iTextSharp.text.Font.BOLD))
            Dim Font7White As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.WHITE))
            Dim Font8White As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.WHITE))
            Dim Font5White As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 5, iTextSharp.text.Font.BOLD, BaseColor.WHITE))
            Dim Font7courierN As New Font(FontFactory.GetFont(FontFactory.COURIER, 6, iTextSharp.text.Font.BOLD))

            'Abrimos el pdf para comenzar a escribir en el
            pdfDoc.Open()

            Dim imagenBMP As iTextSharp.text.Image
            imagenBMP = iTextSharp.text.Image.GetInstance(LOGOBYTE)
            imagenBMP.ScaleAbsolute(70.0F, 50.0F)
            imagenBMP.Border = 0
            ' imagenBMP.ScalePercent(22.0) 'escala al tamaño de la imagen
            ' imagenBMP.SetAbsolutePosition(30, 700)

            ' pdfDoc.Add(imagenBMP)

            Dim TableVacio As PdfPTable = New PdfPTable(1)
            TableVacio.DefaultCell.Border = BorderStyle.None
            TableVacio.WidthPercentage = 100
            Dim widthsVacio As Single() = New Single() {1000.0F}
            TableVacio.SetWidths(widthsVacio)

            Dim ColVacio = New PdfPCell(New Phrase(" ", Font5White))
            ColVacio.Border = 0
            ColVacio.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            TableVacio.AddCell(ColVacio)


            Dim TableEncGeneral As PdfPTable = New PdfPTable(2)
            TableEncGeneral.DefaultCell.Border = BorderStyle.None
            TableEncGeneral.WidthPercentage = 100
            Dim widthsEncGen2 As Single() = New Single() {200.0F, 800.0F}
            TableEncGeneral.SetWidths(widthsEncGen2)


            Dim ColPiePag1 = New PdfPCell(imagenBMP)
            ColPiePag1.Border = 0
            ColPiePag1.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableEncGeneral.AddCell(ColPiePag1)


            Dim TableEnc2 As PdfPTable = New PdfPTable(1)
            TableEnc2.DefaultCell.Border = BorderStyle.None
            TableEnc2.WidthPercentage = 100
            Dim widthsEnc2 As Single() = New Single() {1000.0F}
            TableEnc2.SetWidths(widthsEnc2)

            ColPiePag1 = New PdfPCell(New Phrase(Empresa, Font10N))
            ColPiePag1.Border = 0
            ColPiePag1.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableEnc2.AddCell(ColPiePag1)

            Dim ColPiePag3 = New PdfPCell(New Phrase("", Font10N))
            ColPiePag3.Border = 0
            ColPiePag3.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableEnc2.AddCell(ColPiePag3)




            Dim ColPiePag4 = New PdfPCell(New Phrase("REPORTE DE HISTORIAL DE PAGOS", Font10))
            ColPiePag4.Border = 0
            ColPiePag4.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableEnc2.AddCell(ColPiePag4)

            TableEncGeneral.AddCell(TableEnc2)


            Dim TableEncFechas As PdfPTable = New PdfPTable(2)
            TableEncFechas.DefaultCell.Border = BorderStyle.None
            TableEncFechas.WidthPercentage = 100
            Dim widthsEncFec As Single() = New Single() {650.0F, 400.0F}
            TableEncFechas.SetWidths(widthsEncFec)





            Dim ColEncFec = New PdfPCell(New Phrase("REPORTE DE HISTORIAL DE PAGOS DEL CONTRATO: " & cuenta, Font8))
            ColEncFec.Border = 0
            ColEncFec.HorizontalAlignment = PdfPCell.ALIGN_LEFT
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableEncFechas.AddCell(ColEncFec)

            Dim fechaActual As String = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper()


            ColEncFec = New PdfPCell(New Phrase("FECHA DE EMISIÓN: " & fechaActual, Font8))
            ColEncFec.Border = 0
            ColEncFec.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            'ColPiePag1.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
            TableEncFechas.AddCell(ColEncFec)



            Dim Region As String
            Dim Ruta As String
            Dim Lote As String
            Dim NoMedidor As String

            Dim cadena2 As String = "select * from vusuario where cuenta = '" & cuenta & "'"
            Dim drusu As IDataReader = ConsultaSql(cadena2).ExecuteReader()
            drusu.Read()

            Region = drusu("Region")
            Ruta = drusu("Ruta")
            Lote = drusu("Lote")
            NoMedidor = drusu("nodemedidor")


            Dim TableGeneralUsuario As PdfPTable = New PdfPTable(3)
            TableGeneralUsuario.WidthPercentage = 100
            TableGeneralUsuario.DefaultCell.Border = BorderStyle.None
            Dim widthsInfUsua As Single() = New Single() {500.0F, 250.0F, 250.0F}
            TableGeneralUsuario.SetWidths(widthsInfUsua)

            'Tabla Info Usuario 1
            Dim TableUsua1 As PdfPTable = New PdfPTable(2)
            TableUsua1.WidthPercentage = 100
            Dim widthsInfUsua1 As Single() = New Single() {200.0F, 750.0F}
            TableUsua1.SetWidths(widthsInfUsua1)

            Dim Contrato As String = cuenta
            Dim UsuarioCot As String = drusu("nombre")
            'Dim IDCValvu As String = dts("idcuotavalvulista")


            Dim contenido As String = $"{cuenta} | {UsuarioCot}"


            Dim ColInfoUsu1 = New PdfPCell(New Phrase("Cuenta: ", Font9N))
            ColInfoUsu1.Border = 0
            ColInfoUsu1.HorizontalAlignment = PdfPCell.ALIGN_LEFT

            TableUsua1.AddCell(ColInfoUsu1)



            ColInfoUsu1 = New PdfPCell(New Phrase(contenido, Font9))
            ColInfoUsu1.Border = 0
            ColInfoUsu1.HorizontalAlignment = PdfPCell.ALIGN_LEFT

            TableUsua1.AddCell(ColInfoUsu1)



            Dim contenido2 As String = $"{drusu("direccion")} {drusu("Colonia")}"

            ColInfoUsu1 = New PdfPCell(New Phrase("Domicilio: ", Font9N)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
            }
            TableUsua1.AddCell(ColInfoUsu1)



            ColInfoUsu1 = New PdfPCell(New Phrase(contenido2, Font9)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
            }
            TableUsua1.AddCell(ColInfoUsu1)



            Dim estadotoma As String = drusu("Estado")
            ColInfoUsu1 = New PdfPCell(New Phrase("Estado de la toma: ", Font9N)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
            TableUsua1.AddCell(ColInfoUsu1)


            ColInfoUsu1 = New PdfPCell(New Phrase(estadotoma, Font9)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
            TableUsua1.AddCell(ColInfoUsu1)



            ColInfoUsu1 = New PdfPCell(New Phrase("Region: ", Font9N)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
            TableUsua1.AddCell(ColInfoUsu1)



            ColInfoUsu1 = New PdfPCell(New Phrase(Region, Font9)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
            TableUsua1.AddCell(ColInfoUsu1)



            'ColInfoUsu1 = New PdfPCell(New Phrase("Cuota Valvulista: ", Font)) With {
            '    .Border = 0,
            '    .HorizontalAlignment = PdfPCell.ALIGN_LEFT
            '}
            'TableUsua1.AddCell(ColInfoUsu1)



            TableGeneralUsuario.AddCell(TableUsua1)







            'Tabla Info Usuario 2
            Dim TableUsua2 As PdfPTable = New PdfPTable(2)
            TableUsua2.WidthPercentage = 100
            Dim widthsInfUsua2 As Single() = New Single() {300.0F, 650.0F}
            TableUsua1.SetWidths(widthsInfUsua2)


            Dim contenidotarifa As String = drusu("descripcion_cuota")
            Dim ColInfoUsu2 = New PdfPCell(New Phrase("Tarifa: ", Font9N)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
            TableUsua2.AddCell(ColInfoUsu2)



            ColInfoUsu2 = New PdfPCell(New Phrase(contenidotarifa, Font9)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
            TableUsua2.AddCell(ColInfoUsu2)




            ColInfoUsu2 = New PdfPCell(New Phrase("Ruta: ", Font9N)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
            TableUsua2.AddCell(ColInfoUsu2)


            ColInfoUsu2 = New PdfPCell(New Phrase(Ruta, Font9)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
            TableUsua2.AddCell(ColInfoUsu2)



            Dim nodeperiodos As String = drusu("Nodeperiodo")
            ColInfoUsu2 = New PdfPCell(New Phrase("Periodos adeudados: ", Font9N)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
            TableUsua2.AddCell(ColInfoUsu2)



            ColInfoUsu2 = New PdfPCell(New Phrase(nodeperiodos, Font9)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
            TableUsua2.AddCell(ColInfoUsu2)

            TableGeneralUsuario.AddCell(TableUsua2)



            'Tabla Info Usuario 3
            Dim TableUsua3 As PdfPTable = New PdfPTable(2)
            TableUsua3.WidthPercentage = 100
            Dim widthsInfUsua3 As Single() = New Single() {400.0F, 550.0F}
            TableUsua3.SetWidths(widthsInfUsua3)


            Dim FechaAct As String = DateTime.Now.ToString("dd/MM/yyyy")


            Dim ColInfoUsu3 = New PdfPCell(New Phrase("Orden de ruta: ", Font9N)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
            TableUsua3.AddCell(ColInfoUsu3)



            ColInfoUsu3 = New PdfPCell(New Phrase(Lote, Font9)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
            TableUsua3.AddCell(ColInfoUsu3)


            TableGeneralUsuario.AddCell(TableUsua3)







            'Tabla Información Lecturas

            Dim TableGeneralLecturas As PdfPTable = New PdfPTable(2)
            TableGeneralLecturas.WidthPercentage = 100
            TableGeneralLecturas.DefaultCell.Border = BorderStyle.None
            Dim widthsInfLectu As Single() = New Single() {400.0F, 400.0F}
            TableGeneralLecturas.SetWidths(widthsInfLectu)

            'Tabla Info Lectura 1
            Dim TableLect1 As PdfPTable = New PdfPTable(1)
            TableLect1.WidthPercentage = 100
            Dim widthsInfLect1 As Single() = New Single() {1000.0F}
            TableLect1.SetWidths(widthsInfLect1)

            'Obtener datos de lecturas
            'Dim UltLect As String

            'Dim cadena3 As String = "select ultimalectura(" & txtCuentaCliente.Text & ") as ultimalectura"
            'Dim drlec As IDataReader = ConsultaSql(cadena3).ExecuteReader()
            ''drlec.Read()

            'UltLect = ("ultimalectura")


            Dim ColInfoLect1 = New PdfPCell(New Phrase("No de Medidor: " & drusu("NodeMedidor"), Font10N)) With {
            .Border = 1,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
            TableLect1.AddCell(ColInfoLect1)

            ColInfoLect1 = New PdfPCell(New Phrase("", Font10N)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
            TableLect1.AddCell(ColInfoLect1)

            ColInfoLect1 = New PdfPCell(New Phrase(" ", Font10N)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
            TableLect1.AddCell(ColInfoLect1)

            TableGeneralLecturas.AddCell(TableLect1)

            'Tabla Info Lectura 1
            Dim TableLect2 As PdfPTable = New PdfPTable(1)
            TableLect2.WidthPercentage = 100
            Dim widthsInfLect2 As Single() = New Single() {1000.0F}
            TableLect2.SetWidths(widthsInfLect2)


            Dim ColInfoLect2 = New PdfPCell(New Phrase("", Font10N)) With {
            .Border = 1,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
            TableLect2.AddCell(ColInfoLect2)

            ColInfoLect2 = New PdfPCell(New Phrase("", Font10N)) With {
            .Border = 0,
            .HorizontalAlignment = PdfPCell.ALIGN_LEFT
        }
            TableLect2.AddCell(ColInfoLect2)

            TableGeneralLecturas.AddCell(TableLect2)





            pdfDoc.Add(TableEncGeneral)
            pdfDoc.Add(TableVacio)
            pdfDoc.Add(TableGeneralUsuario)
            pdfDoc.Add(TableGeneralLecturas)
            pdfDoc.Add(TableVacio)
            pdfDoc.Add(TableEncFechas)









            Dim acumuladorSubTotal As Decimal = 0.0
            Dim acumuladorIVA As Decimal = 0.0
            Dim acumuladorTotal As Decimal = 0.0



            Dim cadenaSQL As String = $"SELECT * FROM PAGOS WHERE CUENTA = '{cuenta}' and esusuario = {tipoUsuario} AND CANCELADO = 'A'"
            Dim drPagos As IDataReader = ConsultaSql(cadenaSQL).ExecuteReader()





            While drPagos.Read()


                Dim fechaPago As String = drPagos("FECHA_ACT")
                Dim serieReciboPago As String = $"{drPagos("SERIE")}"
                Dim folioReciboPago As String = $"{drPagos("RECIBO")}"
                Dim folioFactura As String = drPagos("FACTURADO")
                Dim subtotalRecibo As Decimal = drPagos("PAGOS")
                Dim ivaRecibo As Decimal = drPagos("IVA")
                Dim totalRecibo As Decimal = drPagos("TOTAL")
                Dim estatusRecibo As String = drPagos("CANCELADO")




                Try


                    Dim TableRecibos As PdfPTable = New PdfPTable(6)
                    TableRecibos.DefaultCell.Border = BorderStyle.None
                    TableRecibos.WidthPercentage = 100
                    Dim widthsRec As Single() = New Single() {40.0F, 80.0F, 80.0F, 80.0F, 50.0F, 100.0F}
                    TableRecibos.SetWidths(widthsRec)

                    Dim ColRecibo = New PdfPCell(New Phrase("FECHA", Font7White))
                    ColRecibo.Border = 0
                    ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    TableRecibos.AddCell(ColRecibo)

                    ColRecibo = New PdfPCell(New Phrase("RECIBO", Font7White))
                    ColRecibo.Border = 0
                    ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    TableRecibos.AddCell(ColRecibo)

                    ColRecibo = New PdfPCell(New Phrase("FACTURA", Font7White))
                    ColRecibo.Border = 0
                    ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    TableRecibos.AddCell(ColRecibo)

                    ColRecibo = New PdfPCell(New Phrase("SUBTOTAL", Font7White))
                    ColRecibo.Border = 0
                    ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    TableRecibos.AddCell(ColRecibo)

                    ColRecibo = New PdfPCell(New Phrase("IVA", Font7White))
                    ColRecibo.Border = 0
                    ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    TableRecibos.AddCell(ColRecibo)

                    ColRecibo = New PdfPCell(New Phrase("TOTAL", Font7White))
                    ColRecibo.Border = 0
                    ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    ColRecibo.BackgroundColor = New iTextSharp.text.BaseColor(21, 76, 121)
                    TableRecibos.AddCell(ColRecibo)




                    ColRecibo = New PdfPCell(New Phrase(fechaPago, Font7))
                    ColRecibo.Border = 3
                    ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    TableRecibos.AddCell(ColRecibo)


                    ColRecibo = New PdfPCell(New Phrase($"{serieReciboPago} {folioReciboPago}", Font7))
                    ColRecibo.Border = 3
                    ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    TableRecibos.AddCell(ColRecibo)


                    ColRecibo = New PdfPCell(New Phrase(folioFactura, Font7))
                    ColRecibo.Border = 3
                    ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    TableRecibos.AddCell(ColRecibo)



                    ColRecibo = New PdfPCell(New Phrase(subtotalRecibo.ToString("C"), Font7))
                    ColRecibo.Border = 3
                    ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    TableRecibos.AddCell(ColRecibo)


                    ColRecibo = New PdfPCell(New Phrase(ivaRecibo.ToString("C"), Font7))
                    ColRecibo.Border = 3
                    ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    TableRecibos.AddCell(ColRecibo)


                    ColRecibo = New PdfPCell(New Phrase(totalRecibo.ToString("C"), Font7))
                    ColRecibo.Border = 3
                    ColRecibo.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    TableRecibos.AddCell(ColRecibo)


                    pdfDoc.Add(TableRecibos)


                Catch ex As Exception
                    MessageBox.Show(ex.ToString())
                End Try





                Try


                    Dim cadenaSQL2 As String = $"SELECT * FROM PAGOTROS WHERE SERIE = '{serieReciboPago}' AND RECIBO = {folioReciboPago}"
                    Dim drPagos2 As IDataReader = ConsultaSql(cadenaSQL2).ExecuteReader()




                    While drPagos2.Read()

                        Dim cantidadPO As String = drPagos2("CANTIDAD")
                        Dim conceptoPO As String = drPagos2("CONCEPTO")
                        Dim subtotalPO As Decimal = drPagos2("MONTO")
                        Dim llevaIVA As Boolean = drPagos2("IVA")
                        Dim ivaPO As Decimal = 0.0
                        Dim totalPO As Decimal = 0.0




                        If llevaIVA = True Then

                            ivaPO = subtotalPO * (variable_iva / 100)

                        End If



                        Dim TablePagotros As PdfPTable = New PdfPTable(6)
                        TablePagotros.DefaultCell.Border = BorderStyle.None
                        TablePagotros.WidthPercentage = 100
                        Dim widthsRecPagotros As Single() = New Single() {25.0F, 22.0F, 155.0F, 80.0F, 50.0F, 100.0F}
                        TablePagotros.SetWidths(widthsRecPagotros)



                        Dim ColRecibo2 = New PdfPCell(New Phrase(" ", Font6))
                        ColRecibo2.Border = 0
                        ColRecibo2.HorizontalAlignment = PdfPCell.ALIGN_LEFT

                        TablePagotros.AddCell(ColRecibo2)


                        ColRecibo2 = New PdfPCell(New Phrase(cantidadPO, Font6))
                        ColRecibo2.Border = 0
                        ColRecibo2.HorizontalAlignment = PdfPCell.ALIGN_RIGHT

                        TablePagotros.AddCell(ColRecibo2)


                        ColRecibo2 = New PdfPCell(New Phrase(conceptoPO, Font6))
                        ColRecibo2.Border = 0
                        ColRecibo2.HorizontalAlignment = PdfPCell.ALIGN_LEFT

                        TablePagotros.AddCell(ColRecibo2)


                        ColRecibo2 = New PdfPCell(New Phrase(subtotalPO.ToString("C"), Font6))
                        ColRecibo2.Border = 0
                        ColRecibo2.HorizontalAlignment = PdfPCell.ALIGN_RIGHT

                        TablePagotros.AddCell(ColRecibo2)


                        ColRecibo2 = New PdfPCell(New Phrase(ivaPO.ToString("C"), Font6))
                        ColRecibo2.Border = 0
                        ColRecibo2.HorizontalAlignment = PdfPCell.ALIGN_RIGHT

                        TablePagotros.AddCell(ColRecibo2)


                        totalPO = subtotalPO + ivaPO
                        ColRecibo2 = New PdfPCell(New Phrase(totalPO.ToString("C"), Font6))
                        ColRecibo2.Border = 0
                        ColRecibo2.HorizontalAlignment = PdfPCell.ALIGN_RIGHT

                        TablePagotros.AddCell(ColRecibo2)

                        pdfDoc.Add(TablePagotros)

                    End While

                    pdfDoc.Add(TableVacio)

                Catch ex As Exception

                End Try


                acumuladorSubTotal = acumuladorSubTotal + subtotalRecibo
                acumuladorIVA = acumuladorIVA + ivaRecibo
                acumuladorTotal = acumuladorTotal + totalRecibo

            End While



            Dim TableTotales As PdfPTable = New PdfPTable(4)
            TableTotales.DefaultCell.Border = BorderStyle.None
            TableTotales.WidthPercentage = 100
            Dim widthsRecTot As Single() = New Single() {202.0F, 80.0F, 50.0F, 100.0F}
            TableTotales.SetWidths(widthsRecTot)



            Dim ColTotal = New PdfPCell(New Phrase("Total General", Font10N))
            ColTotal.Border = 1
            ColTotal.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            TableTotales.AddCell(ColTotal)


            ColTotal = New PdfPCell(New Phrase(acumuladorSubTotal.ToString("C"), Font10N))
            ColTotal.Border = 1
            ColTotal.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            TableTotales.AddCell(ColTotal)


            ColTotal = New PdfPCell(New Phrase(acumuladorIVA.ToString("C"), Font10N))
            ColTotal.Border = 1
            ColTotal.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            TableTotales.AddCell(ColTotal)


            ColTotal = New PdfPCell(New Phrase(acumuladorTotal.ToString("C"), Font10N))
            ColTotal.Border = 1
            ColTotal.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            TableTotales.AddCell(ColTotal)


            pdfDoc.Add(TableVacio)
            pdfDoc.Add(TableTotales)





            pdfDoc.Close()


            Try
                Dim psi As New ProcessStartInfo(nombrearchivo)
                'psi.WorkingDirectory = cadenafolder & "\factura\" + nombresespacios

                psi.WindowStyle = ProcessWindowStyle.Normal
                Dim p As Process = Process.Start(psi)
            Catch ex As Exception
                MessageBox.Show("Error al visualizar el pdf, posiblemente el archivo este en uso, cierrelo antes de generar un nuevo reporte" & ex.Message)
            End Try




        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try




    End Sub

End Class

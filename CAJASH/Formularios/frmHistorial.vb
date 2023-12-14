
Public Class FrmHistorial
    Public cuenta As Long = 0

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
        Dim filtro As String
        Dim filtromysql As String

        filtro = "CUENTA : " & cuenta & ""
        filtromysql = " cuenta =" & cuenta

        btnimprimir.Enabled = False


        btnimprimir.Enabled = True
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
End Class

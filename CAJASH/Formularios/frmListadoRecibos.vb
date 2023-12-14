Imports CAJAS.base
Imports System.Data.Odbc


Public Class frmListadoRecibos

    Dim fechaoriginal As Date
    Dim recibo As String = ""
    Dim cuenta As String = ""
    Dim serie As String = ""
    Dim result As DialogResult
    Public esusuario As String


    Private Sub frmListadoRecibos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtinicio.Value = DateTime.Now
        dtfinal.Value = DateTime.Now
        If My.Settings.esAdministrativa.ToUpper() = "SI" And My.Settings.tipocaja.ToUpper() = "CONSULTA" Then

            btncancelarrecibo.Visible = True
            toolStripButton1.Visible = True
            toolStripButton2.Enabled = True
            GroupBox1.Enabled = True
        Else
            If My.Settings.tipocaja.ToUpper() = "COBRO" Then
                'btncancelarrecibo.Enabled = False
                'toolStripButton1.Enabled = False
                'toolStripButton2.Enabled = False

                btncancelarrecibo.Visible = True
                toolStripButton1.Visible = True
                toolStripButton2.Enabled = True
                GroupBox1.Enabled = True
            End If
        End If
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncargar.Click


        Try

            Dim bas As New base
            If txtcaja.Text = "" Then
                bas.llenaGrid(dataGridView1, "select serie, recibo,cuenta, nombre, periodo, fecha_Act, total-iva as Subtotal,IVA, Total, Facturado,Cancelado, case when esusuario=1 then 'USUARIO' when esusuario=0 then 'CLIENTE' when esusuario=2 then 'CLIENTE' WHEN esusuario=3 then 'FACTIBILIDAD' end as Es, FECHA_DEUDA as Fecha_Deuda, Tarifa, ccodpago AS Forma from pagos where fecha_Act between '" & dtinicio.Value.ToString("yyyy-MM-dd") & "' and '" & dtfinal.Value.ToString("yyyy-MM-dd") & "';")
            Else
                bas.llenaGrid(dataGridView1, "select serie, recibo,cuenta, nombre, periodo, fecha_Act, total-iva as Subtotal,IVA, Total, Facturado,Cancelado, case when esusuario=1 then 'USUARIO' when esusuario=0 then 'CLIENTE' when esusuario=2 then 'CLIENTE' WHEN esusuario=3 then 'FACTIBILIDAD' end as Es, FECHA_DEUDA as Fecha_Deuda, Tarifa, CCODPAGO AS Forma from pagos where fecha_Act between '" & dtinicio.Value.ToString("yyyy-MM-dd") & "' and '" & dtfinal.Value.ToString("yyyy-MM-dd") & "' and caja='" & txtcaja.Text & "'")
            End If
            lblcuantos.Text = "Se econtraton " & dataGridView1.Rows.Count & " recibos"
            dataGridView1.Columns(0).Width = 150
            dataGridView1.Columns(1).Width = 50
            dataGridView1.Columns(2).Width = 50
            dataGridView1.Columns(3).Width = 300



            bas.conexion.Dispose()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btncancelarrecibo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancelarrecibo.Click
        'Dim recibo As String = ""
        'Dim cuenta As String = ""
        'Dim result As DialogResult

        result = MessageBox.Show("Esta Seguro que Desea Cancelar el Recibo ?", "Alerta", MessageBoxButtons.OKCancel)
        Dim x As New base
        If result = DialogResult.OK Then
            ' TEN CUIDADO NO HICE LA RUTINA UNIFICADA DE CANCELACION AQUI Y LA FACTURA ASI QUE SOLO COPIE
            ' Y PEGUE LO QUE HAGAS AQUI HAZLO EN LA CANCELACION DE FACTURA
            Try
                recibo = dataGridView1.Item("recibo", dataGridView1.CurrentRow.Index).Value.ToString()
                cuenta = dataGridView1.Item("cuenta", dataGridView1.CurrentRow.Index).Value.ToString()
                serie = dataGridView1.Item("SERIE", dataGridView1.CurrentRow.Index).Value.ToString()
                Try
                    If dataGridView1.Item("es", dataGridView1.CurrentRow.Index).Value.ToString() = "usuario" Then
                        Dim per As String = ""
                        per = dataGridView1.Item("Fecha_Deuda", dataGridView1.CurrentRow.Index).Value.ToString()
                        fechaoriginal = Date.Parse(per)
                        Ejecucion("update usuario set deudafec = '" & fechaoriginal.ToString("yyyy-MM-dd") & "' where cuenta='" & cuenta & "' ")
            End If
                Catch ex As Exception

                End Try
                Ejecucion("update pagos set Cancelado='C' where recibo=" & recibo & " and serie='" & serie & "'")
                Ejecucion("update pagotros set Cancelado='C' where recibo=" & recibo & " and serie='" & serie & "'")
                Ejecucion("update otrosconceptos, pagotros set pagado=0,estado='Pendiente', otrosconceptos.Resta=otrosconceptos.Resta + (pagotros.monto+ (pagotros.monto*pagotros.iva*" & variable_iva / 100 & ")), otrosconceptos.subtotresta=otrosconceptos.subtotresta + pagotros.monto where otrosconceptos.clave=pagotros.clavemov and pagotros.recibo=" & recibo & " and pagotros.serie='" & serie & "'")

                Dim sinusar = EjecutarConsultaRemotaAsync("update recibomaestro set Estado='C' where recibo=" & recibo & " and serie='" & serie & "'")
                Dim sinusar2 = EjecutarConsultaRemotaAsync("update reciboesclavo set Estado='C' where recibo=" & recibo & " and serie='" & serie & "'")


                Try

                    Dim dato As New base
                    Dim consulta As OdbcDataReader
                    consulta = dato.consultasql("SELECT * FROM pago_mes WHERE RECIBO = " & recibo & " AND SERIE='" & serie & "' and CONCEPTO='CONSUMO'")
                    While consulta.Read
                        Dim mesquecancelo As String
                        Dim periodo As String
                        'mesquecancelo = consulta!periodo
                        mesquecancelo = consulta("mes")
                        periodo = consulta("ano")

                        Dim dato2 As New base
                        Dim consulta2 As OdbcDataReader

                        consulta2 = dato2.consultasql("SELECT * FROM lecturas  WHERE CUENTA=" & cuenta & " AND MES='" & mesquecancelo & "' AND AN_PER='" & periodo & "'")
                        Try
                            consulta2.Read()
                            If consulta2("ADELANT") = 1 Then
                                ejecucion("DELETE FROM lecturas WHERE CUENTA=" & cuenta & " AND MES='" & mesquecancelo & "' AND AN_PER='" & periodo & "'")
                            End If
                            dato2.conexion.Dispose()
                        Catch ex As Exception

                        End Try
                        '  Dim cuenta_ As String = consulta!cuenta
                        ejecucion("UPDATE lecturas SET PAGADO=0 WHERE CUENTA=" & cuenta & " AND MES='" & mesquecancelo & "' AND AN_PER='" & periodo & "'")
                    End While
                    dato.conexion.Dispose()
                Catch ex As Exception

                End Try


                x.conexion.Dispose()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If
        btnAceptar_Click(sender, e)
    End Sub

    Private Sub toolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolStripButton1.Click
        recibo = dataGridView1.Item("recibo", dataGridView1.CurrentRow.Index).Value.ToString()
        serie = dataGridView1.Item("serie", dataGridView1.CurrentRow.Index).Value.ToString()

        result = MessageBox.Show("¿Esta seguro que desea Eliminar el Recibo: " & recibo & "?", "Cuidado!!! ", MessageBoxButtons.OKCancel)
        If result = DialogResult.OK Then

            Try
                Dim x As New base
                x.conectar()
                ejecucion("delete from pagos  where recibo=" & recibo & " and serie ='" & serie & "'")
                ejecucion("delete from pagotros  where recibo=" & recibo & " and serie ='" & serie & "'")
                ejecucion("delete from pago_mes  where recibo=" & recibo & " and serie ='" & serie & "'")
                x.conexion.Dispose()
            Catch ex As Exception

            End Try

        End If
        btnAceptar_Click(sender, e)
    End Sub

    Private Sub dataGridView1_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dataGridView1.CellDoubleClick

        recibo = dataGridView1.Item("recibo", dataGridView1.CurrentRow.Index).Value.ToString()
        serie = dataGridView1.Item("serie", dataGridView1.CurrentRow.Index).Value.ToString()
        lblreciboseleccionado.Text = "Recibo seleccionado " + serie + " " + recibo
        Dim forma As String = dataGridView1.Item("Forma", dataGridView1.CurrentRow.Index).Value.ToString()
        If forma = "01" Then
            cmbforma.Text = "01 Efectivo"
        End If
        If forma = "02" Then
            cmbforma.Text = "02 Cheque"
        End If
        If forma = "03" Then
            cmbforma.Text = "03 Transferencia"
        End If
        If forma = "04" Then
            cmbforma.Text = "04 Tarjeta de Credito"
        End If
        If forma = "28" Then
            cmbforma.Text = "28 Tarjeta de Debito"
        End If
    End Sub

    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click
        Try
            Dim x As New base

            recibo = dataGridView1.Item("recibo", dataGridView1.CurrentRow.Index).Value.ToString()
            serie = dataGridView1.Item("serie", dataGridView1.CurrentRow.Index).Value.ToString()
            esusuario = dataGridView1.Item("es", dataGridView1.CurrentRow.Index).Value.ToString()
            Dim NOfactura As String = dataGridView1.Item("facturado", dataGridView1.CurrentRow.Index).Value.ToString()
            Dim CANCELADO As String = dataGridView1.Item("CANCELADO", dataGridView1.CurrentRow.Index).Value.ToString()



            If NOfactura <> "0" Then
                MessageBox.Show("EL RECIBO YA ESTA FACTURADO")
                Exit Sub
            End If

            If CANCELADO = "C" Then
                MessageBox.Show("EL RECIBO ESTA CANCELADO")
                Exit Sub
            End If
            Dim control As New Clscontrolpago

            Dim REC As New clsrecibo




            REC.cuenta = Double.Parse(dataGridView1.Item("Cuenta", dataGridView1.CurrentRow.Index).Value.ToString)
            REC.total = Double.Parse(dataGridView1.Item("Total", dataGridView1.CurrentRow.Index).Value.ToString)
            REC.iva = Double.Parse(dataGridView1.Item("IVA", dataGridView1.CurrentRow.Index).Value.ToString)
            REC.subtotal = Double.Parse(dataGridView1.Item("subtotal", dataGridView1.CurrentRow.Index).Value.ToString)
            Dim datosrecibo As OdbcDataReader
            datosrecibo = x.consultasql("select * from pagos where recibo=" & recibo & " and serie='" & serie & "'")
            datosrecibo.Read()

            REC.cancelado = "A"


            Dim FACTURA As New Frmvalidafactura
            FACTURA.vienede = "RECIBO"
            FACTURA.Numerodecaja = datosrecibo("CAJA")
            FACTURA.total = REC.total
            FACTURA.iva = REC.iva
            FACTURA.subtotal = REC.subtotal
            REC.Fecha_Act = datosrecibo("fecha_Act")
            REC.esmedido = 1 - datosrecibo("Esfijo")
            REC.esusuario = datosrecibo("Esusuario")
            REC.ccodpago = datosrecibo("ccodpago")
            REC.nombre = datosrecibo("nombre")
            REC.NUMERO = datosrecibo("recibo")
            REC.SERIE = datosrecibo("serie")
            FACTURA.recibo = REC




            FACTURA.formafacturado = REC.ccodpago
            FACTURA.cmbformapago.SelectedValue = FACTURA.formafacturado
            FACTURA.cmbmetodo.SelectedValue = "PUE"
            FACTURA.cmbuso.SelectedValue = "G03"

            FACTURA.TIPO = dataGridView1.Item("ES", dataGridView1.CurrentRow.Index).Value.ToString()
            FACTURA.cuenta = Integer.Parse(REC.cuenta)
            FACTURA.Reciboqueseestafacturando = recibo
            FACTURA.seriedelreciboqueseestafacturando = serie

            x.conectar()





            DatosRecibo = x.consultasql("select * from pagotros where recibo=" & recibo & " and serie='" & serie & "'")
            Do While datosrecibo.Read
                Dim concepto As New Clsconcepto
                concepto.Cantidad = datosrecibo!cantidad
                concepto.Preciounitario = datosrecibo!monto
                concepto.Concepto = datosrecibo!concepto
                concepto.CLAVEMOV = datosrecibo!clavemov
                concepto.Clave = datosrecibo!NumConcepto
                concepto.calcula()
                If datosrecibo!iva = 1 Then
                    concepto.IVA = Math.Round((concepto.Cantidad * concepto.Preciounitario) * (variable_iva / 100), 2)
                Else
                    concepto.IVA = 0
                End If
                control.Listadeconceptos.Add(concepto)
            Loop
            FACTURA.vienede = "RECIBO"
            FACTURA.control = control

            FACTURA.ShowDialog()


            '    Ejecucion("UPDATE PAGOS SET FACTURADO=" & FACTURA.facturado & " where recibo=" & recibo & " and serie='" & serie & "'")


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        btnAceptar_Click(sender, e)
    End Sub

    Private Sub toolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolStripButton2.Click

        Try


            Dim x As New base

            recibo = dataGridView1.Item("recibo", dataGridView1.CurrentRow.Index).Value.ToString()
            serie = dataGridView1.Item("serie", dataGridView1.CurrentRow.Index).Value.ToString()
            esusuario = dataGridView1.Item("es", dataGridView1.CurrentRow.Index).Value.ToString()
            Dim factura As String = dataGridView1.Item("facturado", dataGridView1.CurrentRow.Index).Value.ToString()
            Dim CANCELADO As String = dataGridView1.Item("CANCELADO", dataGridView1.CurrentRow.Index).Value.ToString()



            If factura <> "0" Then
                MessageBox.Show("EL RECIBO YA ESTA FACTURADO")
                Exit Sub
            End If

            If CANCELADO = "C" Then
                MessageBox.Show("EL RECIBO ESTA CANCELADO")
                Exit Sub
            End If
            Dim myValue As String = InputBox("Escribe el numero de factura ", "Solicitud de informacion", "1")
            Try

                ejecucion("update pagos set facturado =" & myValue & "  where recibo=" & recibo & " and serie ='" & serie & "'")

                Dim sinusar = EjecutarConsultaRemotaAsync("update recibomaestro set factura=" & myValue & "  where recibo=" & recibo & " and serie ='" & serie & "'")

                x.conexion.Dispose()
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
        btnAceptar_Click(sender, e)
    End Sub

    Private Sub toolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolStripButton3.Click
        Try


            Dim x As New base

            recibo = dataGridView1.Item("recibo", dataGridView1.CurrentRow.Index).Value.ToString()
            serie = dataGridView1.Item("serie", dataGridView1.CurrentRow.Index).Value.ToString()
            esusuario = dataGridView1.Item("es", dataGridView1.CurrentRow.Index).Value.ToString()
            Dim factura As String = dataGridView1.Item("facturado", dataGridView1.CurrentRow.Index).Value.ToString()
            Dim CANCELADO As String = dataGridView1.Item("CANCELADO", dataGridView1.CurrentRow.Index).Value.ToString()



            If factura = "0" Then
                MessageBox.Show("EL RECIBO ESTA LISTO PARA FACTURAR!!!")
                Exit Sub
            End If

            If CANCELADO = "C" Then
                MessageBox.Show("EL RECIBO ESTA CANCELADO")
                Exit Sub
            End If

            Try

                ejecucion("update pagos set facturado =0  where recibo=" & recibo & " and serie ='" & serie & "'")


                Dim sinusar = EjecutarConsultaRemotaAsync("update recibomaestro set factura=0  where recibo=" & recibo & " and serie ='" & serie & "'")

                x.conexion.Dispose()
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
        btnAceptar_Click(sender, e)
    End Sub

    Private Sub superTabrecibos_SelectedTabChanged(ByVal sender As Object, ByVal e As DevComponents.DotNetBar.SuperTabStripSelectedTabChangedEventArgs) Handles superTabrecibos.SelectedTabChanged
        If e.NewValue.ToString = "Esclavo" Then
            Dim X2 As New base
            X2.conectar()
            advDetalles.ClearAndDisposeAllNodes()
            Try

                Try
                    recibo = dataGridView1.Item("recibo", dataGridView1.CurrentRow.Index).Value.ToString()
                Catch ex As Exception
                    MessageBox.Show("No has seleccionado un recibo")
                    Exit Sub
                End Try


                serie = dataGridView1.Item("serie", dataGridView1.CurrentRow.Index).Value.ToString()

                Dim datosesclavo As Odbc.OdbcDataReader = X2.consultasql("select * from pagotros where recibo=" & recibo & " and serie='" & serie & "'")
                Dim nuevonodo As New DevComponents.AdvTree.Node

                advDetalles.Columns.Add(New DevComponents.AdvTree.ColumnHeader("Cantidad"))
                advDetalles.Columns(0).Width.Absolute = 50
                advDetalles.Columns.Add(New DevComponents.AdvTree.ColumnHeader("Concepto"))
                advDetalles.Columns(1).Width.Absolute = 350
                advDetalles.Columns.Add(New DevComponents.AdvTree.ColumnHeader("Precio Uni"))
                advDetalles.Columns(2).Width.Absolute = 140
                advDetalles.Columns.Add(New DevComponents.AdvTree.ColumnHeader("Importe"))
                advDetalles.Columns(3).Width.Absolute = 140

                advDetalles.Columns.Add(New DevComponents.AdvTree.ColumnHeader("Con Iva"))
                advDetalles.Columns(4).Width.Absolute = 50
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
                    'nuevonodo.Nodes.Add(nuevoesclavo)
                    advDetalles.Nodes.Add(nuevoesclavo)
                End While


            Catch ex As Exception

            End Try
            X2.conexion.Dispose()
        End If
        If e.NewValue.ToString = "Rectifica" Then
            Dim bas As New base
            bas.llenaGrid(dataGridViewRectifica, "select pagos.serie, pagos.recibo, pagos.total-pagos.iva AS SubTotal, pagos.IVA, pagos.Total ,Round( sum(importe),2)  as Esclavo from pagos,pagotros where pagos.serie=pagotros.serie and pagos.recibo=pagotros.recibo and fecha_Act between '" & dtinicio.Value.ToString("yyyy-MM-dd") & "' and '" & dtfinal.Value.ToString("yyyy-MM-dd") & "' group by pagos.serie,pagos.recibo, pagos.total-pagos.iva, pagos.iva, pagos.total;")
            bas.conexion.Dispose()

        End If
    End Sub

    Private Sub btncerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncerrar.Click
        Close()
    End Sub

    Private Sub btnRenumerar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRenumerar.Click
        btnRenumerar.Enabled = False
        If btncargar.Text = "" Then
            MessageBox.Show("No has seleccionado una serie")
            btnRenumerar.Enabled = True
            Exit Sub
        End If
        If IIfolioinicio.Value = 0 Then
            MessageBox.Show("No has seleccionado un folio de inicio")
            btnRenumerar.Enabled = True
            Exit Sub
        End If
        If IIfoliofinal.Value = 0 Then
            MessageBox.Show("No has seleccionado un folio final")
            btnRenumerar.Enabled = True
            Exit Sub
        End If
        Try
            Dim x As New base
            x.conectar()
            Dim verificar As OdbcDataReader

            If IIsumando.Value <= -1 Then
                verificar = x.consultasql("select * from pagos where recibo between " & IIfolioinicio.Value + IIsumando.Value & " and " & IIfolioinicio.Value & " and serie='" & btncargar.Text & "'")
            End If
            If IIsumando.Value >= 1 Then
                verificar = x.consultasql("select * from pagos where recibo between " & IIfoliofinal.Value & " and " & IIfoliofinal.Value + IIsumando.Value & " and serie='" & txtserie.Text & "'")
            End If
            If verificar.Read Then
                If IIfolioinicio.Value = 0 Then
                    MessageBox.Show("No puede realizarse la operacion por que hay un folio en ese lugar")
                    Exit Sub
                End If
            End If

            If IIsumando.Value <= -1 Then
                For i = IIfolioinicio.Value To IIfoliofinal.Value
                    ejecucion("update pagos set recibo= " & i + IIsumando.Value & " where recibo = " & i & " and serie='" & txtserie.Text & "'")
                    ejecucion("update pagotros set recibo= " & i + IIsumando.Value & " where recibo = " & i & " and serie='" & txtserie.Text & "'")
                    ejecucion("update pago_mes set recibo= " & i + IIsumando.Value & " where recibo = " & i & " and serie='" & txtserie.Text & "'")
                Next
            End If
            If IIsumando.Value >= 1 Then
                For i = IIfoliofinal.Value To IIfolioinicio.Value Step -1
                    ejecucion("update pagos set recibo= " & i + IIsumando.Value & " where RECIBO = " & i & " and serie='" & txtserie.Text & "'")
                    ejecucion("update pagotros set recibo= " & i + IIsumando.Value & " where RECIBO = " & i & " and serie='" & txtserie.Text & "'")
                    ejecucion("update pago_mes set recibo= " & i + IIsumando.Value & " where RECIBO = " & i & " and serie='" & txtserie.Text & "'")
                Next
            End If
            x.conexion.Dispose()

        Catch ex As Exception
            MessageBox.Show(ex.Message())
            btnRenumerar.Enabled = True
            Exit Sub
        End Try

        btnRenumerar.Enabled = True

        btnAceptar_Click(sender, e)

    End Sub






    Private Sub dataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles dataGridView1.SelectionChanged
        Try
            recibo = dataGridView1.Item("recibo", dataGridView1.CurrentRow.Index).Value.ToString()
            serie = dataGridView1.Item("serie", dataGridView1.CurrentRow.Index).Value.ToString()

            Dim forma As String = dataGridView1.Item("Forma", dataGridView1.CurrentRow.Index).Value.ToString()
            Dim x As String
            If forma = "01" Then
                x = "01 Efectivo"
            End If
            If forma = "02" Then
                x = "02 Cheque"
            End If
            If forma = "03" Then
                x = "03 Transferencia"
            End If
            If forma = "04" Then
                x = "04 Tarjeta de Credito"
            End If
            If forma = "28" Then
                x = "28 Tarjeta de Debito"
            End If
            lblreciboseleccionado.Text = "Recibo seleccionado " + serie + " " + recibo + " " + x
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmbforma_TextChanged(sender As Object, e As EventArgs) Handles cmbforma.TextChanged
        recibo = dataGridView1.Item("recibo", dataGridView1.CurrentRow.Index).Value.ToString()
        serie = dataGridView1.Item("serie", dataGridView1.CurrentRow.Index).Value.ToString()
        Dim que As String = cmbforma.Text.Substring(0, 2)
        If que <> "00" Then

            Ejecucion("update pagos set ccodpago ='" & que & "' where serie='" & serie & "' and recibo=" & recibo)
            Dim unused = EjecutarConsultaRemotaAsync("update recibomaestro set id_forma_pago =" & que & " where serie='" & serie & "' and recibo=" & recibo)
        End If
        dataGridView1.Item("forma", dataGridView1.CurrentRow.Index).Value = que
    End Sub
End Class
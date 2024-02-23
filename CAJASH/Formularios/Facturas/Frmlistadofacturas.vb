Imports System.Data.Odbc
Imports System.Security.Cryptography
Imports System.Web.Services
Imports System.Web.Services.Discovery
Imports System.Web.Services.Configuration
Imports System.Web.Services.Description
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization
Imports System.Xml
Imports System.Management
Imports System.Text
Imports System.Xml.XPath
Imports System.IO

Imports Multifacturas.SDK
Imports MultiFacturasSDK
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.Net.Mail

Public Class Frmlistadofacturas
    Dim fechaoriginal As Date
    Dim recibo As String = ""
    Dim cuenta As String = ""
    Dim serie As String = ""
    Public iva As Double
    Dim result As DialogResult, resultfacturas As DialogResult
    Private Sub Frmlistadofacturas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtinicio.Value = DateTime.Now
        dtfinal.Value = DateTime.Now
        CheckForIllegalCrossThreadCalls = False
        Try
            lblsaldo.Text = FuncFacturas_v4.consulta_saldo.ToString
        Catch ex As Exception
        End Try
        If My.Settings.esAdministrativa.ToUpper() = "SI" And My.Settings.tipocaja.ToUpper() = "CONSULTA" Then
            btnreimprimir.Visible = True
            btncancelarrecibo.Visible = True
            ToolStripButton1.Visible = True
            ToolStripButton2.Enabled = True
        Else
            btnreimprimir.Visible = True
            btncancelarrecibo.Enabled = False
            ToolStripButton1.Visible = True
            ToolStripButton2.Enabled = False
        End If
    End Sub

    Private Sub btncerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncerrar.Click
        Close()
    End Sub

    Private Sub btncargar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncargar.Click
        Try
            Dim bas As New base
            If txtCaja.Text = "" Then
                bas.llenaGrid(dataGridView1, "select fecha, serie, numero, nombre,  Subtotal,IVA, Total, Estado, case when tipo=1 then 'USUARIO' when tipo=0 then 'CLIENTE' when tipo=2 then 'CLIENTE' WHEN tipo=3 then 'FACTIBILIDAD' WHEN tipo=4 then 'DE PERIODO'  end as Es,MOTIVOCANCELACION,recibo, serierecibo,UUID, Acuse_Cancelacion from encfac where fecha between '" & dtinicio.Value.ToString("yyyy-MM-dd") & " 00:00:01' and '" & dtfinal.Value.ToString("yyyy-MM-dd") & " 23:59:59' order by numero;")
            Else
                bas.llenaGrid(dataGridView1, "select fecha, serie, numero, nombre,  Subtotal,IVA, Total, Estado, case when tipo=1 then 'USUARIO' when tipo=0 then 'CLIENTE' when tipo=2 then 'CLIENTE' WHEN tipo=3 then 'FACTIBILIDAD'  WHEN tipo=4 then 'DE PERIODO' end as Es,MOTIVOCANCELACION,recibo, serierecibo,UUID, Acuse_Cancelacion from encfac where fecha between '" & dtinicio.Value.ToString("yyyy-MM-dd") & " 00:00:01' and '" & dtfinal.Value.ToString("yyyy-MM-dd") & " 23:59:59' and caja=" & txtCaja.Text & " order by numero;")
            End If
            lblencontradas.Text = dataGridView1.Rows.Count
            dataGridView1.Columns(0).Width = 150
            dataGridView1.Columns(1).Width = 50
            dataGridView1.Columns(2).Width = 50
            dataGridView1.Columns(3).Width = 300
            dataGridView1.Columns(4).Width = 100
            dataGridView1.Columns(5).Width = 100
            dataGridView1.Columns(7).Width = 80
            dataGridView1.Columns(12).Width = 100
            dataGridView1.Columns(13).Width = 150
            dataGridView1.Columns(5).DefaultCellStyle.Alignment = 4
            dataGridView1.Columns(4).DefaultCellStyle.Alignment = 4
            dataGridView1.Columns(6).DefaultCellStyle.Alignment = 4

            bas.conexion.Dispose()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub superTabrecibos_SelectedTabChanged(ByVal sender As System.Object, ByVal e As DevComponents.DotNetBar.SuperTabStripSelectedTabChangedEventArgs) Handles superTabrecibos.SelectedTabChanged
        Dim recibo As Integer
        Dim serie As String
        If e.NewValue.ToString = "Detalles" Then
            Dim X2 As New base
            X2.conectar()
            advDetalles.ClearAndDisposeAllNodes()
            Try
                Try
                    recibo = dataGridView1.Item("numero", dataGridView1.CurrentRow.Index).Value.ToString()
                Catch ex As Exception
                    MessageBox.Show("No has seleccionado un recibo")
                    Exit Sub
                End Try
                serie = dataGridView1.Item("serie", dataGridView1.CurrentRow.Index).Value.ToString()
                Dim datosesclavo As Odbc.OdbcDataReader = X2.consultasql("select * from detfac where numero=" & recibo & " and serie='" & serie & "'")
                Dim nuevonodo As New DevComponents.AdvTree.Node
                advDetalles.Columns.Add(New DevComponents.AdvTree.ColumnHeader("Cantidad"))
                advDetalles.Columns(0).Width.Absolute = 50
                advDetalles.Columns.Add(New DevComponents.AdvTree.ColumnHeader("Concepto"))
                advDetalles.Columns(1).Width.Absolute = 350
                advDetalles.Columns.Add(New DevComponents.AdvTree.ColumnHeader("Precio Uni"))
                advDetalles.Columns(2).Width.Absolute = 120
                advDetalles.Columns.Add(New DevComponents.AdvTree.ColumnHeader("Importe"))
                advDetalles.Columns(3).Width.Absolute = 120
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
                        celda.Text = datosesclavo!descripcion
                        nuevoesclavo.Cells.Add(celda)
                    Catch ex As Exception
                    End Try
                    Try
                        Dim celda8 As New DevComponents.AdvTree.Cell
                        Dim etiqueta8 As New DevComponents.DotNetBar.LabelX
                        celda8.HostedControl = etiqueta8
                        etiqueta8.Text = datosesclavo!preciounitario
                        etiqueta8.TextAlignment = StringAlignment.Far
                        etiqueta8.ForeColor = Color.Blue
                        nuevoesclavo.Cells.Add(celda8)
                    Catch ex As Exception
                    End Try
                    Try
                        Dim celda9 As New DevComponents.AdvTree.Cell
                        Dim etiqueta9 As New DevComponents.DotNetBar.LabelX
                        celda9.HostedControl = etiqueta9
                        etiqueta9.Text = datosesclavo!Importe
                        etiqueta9.TextAlignment = StringAlignment.Far
                        etiqueta9.ForeColor = Color.Blue
                        nuevoesclavo.Cells.Add(celda9)
                    Catch ex As Exception
                    End Try
                    Try
                        Dim celdaiva As New DevComponents.AdvTree.Cell
                        Dim etiqueta10 As New DevComponents.DotNetBar.LabelX
                        celdaiva.HostedControl = etiqueta10
                        etiqueta10.Text = datosesclavo!Iva
                        etiqueta10.TextAlignment = StringAlignment.Far
                        etiqueta10.ForeColor = Color.Blue
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
    End Sub

    Private Sub btncancelarrecibo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancelarrecibo.Click
        Dim cliente As New cancelafactupronto.wsCancelacion
        Dim bas As New base
        Dim usuario As String = ""
        Dim pass As String = ""
        Dim rfc As String = ""
        Dim UUID As String = ""
        Dim certificado As String = ""
        Dim xmlcancelacion As String = ""
        Dim fecha As String = Now.Year & "-" & Now.Month & "-" & Now.Day & "T" & Now.Hour & ":" & Now.Minute & ":" & Now.Second

        resultfacturas = MessageBox.Show("¿ Desea Cancelar la factura digital ?", "Alerta", MessageBoxButtons.OKCancel)

        If resultfacturas = DialogResult.OK Then

            Dim factura As Int32 = dataGridView1.Item("numero", dataGridView1.CurrentRow.Index).Value
            Dim serie As String = dataGridView1.Item("serie", dataGridView1.CurrentRow.Index).Value.ToString()

            Dim directorio2 As String = Application.StartupPath & "\facturas" & Year(Now) & acompletacero(Month(Now), 2) & "\"

            If My.Settings.ProvedorFacturacion = "FACTUPRONTO" Then

                Dim reader As XmlTextReader
                Dim datos As Odbc.OdbcDataReader = bas.consultasql("select * from sicofi limit 1")
                Try
                    datos.Read()
                    usuario = datos("usuario")
                    pass = datos("password")

                Catch ex As Exception

                End Try

                Try
                    reader = New XmlTextReader(directorio2 & "xmlFactura" & serie & factura & ".xml")
                    Do While reader.Read
                        reader.Read()
                        Select Case reader.NodeType
                            Case XmlNodeType.Element
                                ' If reader.Name = "cfdi:Complemento" Then
                                While reader.MoveToNextAttribute()
                                    'Mostrar valor y nombre del atributo
                                    Console.Write(" {0}='{1}'", reader.Name, reader.Value)
                                    If reader.Name = "rfc" Then
                                        rfc = reader.Value
                                    End If
                                    If reader.Name = "UUID" Then
                                        UUID = reader.Value
                                    End If
                                    If reader.Name = "certificado" Then
                                        certificado = reader.Value
                                    End If

                                End While
                                'End If
                        End Select
                    Loop


                Catch ex As Exception

                    xmlcancelacion = "<Cancelacion xmlns=""http://cancelacfd.sat.gob.mx"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" Fecha=""" & fecha & """ RfcEmisor=""" & rfc & """><Folios><UUID>" & UUID & "</UUID></Folios></Cancelacion>"

                End Try

                Try
                    Dim hashdata As Byte() = sha1(xmlcancelacion)
                    Dim digestvalue As String = Convert.ToBase64String(hashdata)
                    Dim xmlsign As String = "<SignedInfo xmlns=""http://www.w3.org/2000/09/xmldsig#"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""><CanonicalizationMethod Algorithm=""http://www.w3.org/TR/2001/REC-xml-c14n-20010315""></CanonicalizationMethod><SignatureMethod Algorithm=""http://www.w3.org/2000/09/xmldsig#rsa-sha1""></SignatureMethod><Reference URI=""""><Transforms><Transform Algorithm=""http://www.w3.org/2000/09/xmldsig#enveloped-signature""></Transform></Transforms><DigestMethod Algorithm=""http://www.w3.org/2000/09/xmldsig#sha1""></DigestMethod><DigestValue>" & digestvalue & "</DigestValue></Reference></SignedInfo>"


                Catch ex As Exception

                End Try
                Try



                    '  cliente.cancelacion(rfc, usuario, pass, UUID, certificado, xmlcancelacion, )
                Catch ex As Exception

                End Try



            ElseIf My.Settings.ProvedorFacturacion = "MULTIFACTURAS" Then
                Dim sdkresp As SDKRespuesta

                Try
                    UUID = dataGridView1.Item("UUID", dataGridView1.CurrentRow.Index).Value

                    conectar()


                    Dim datosfac As IDataReader
                    datosfac = ConsultaSql("select * from encfac where numero=" & factura & " and serie='" & serie & "'").ExecuteReader
                    datosfac.Read()

                    Dim fs As FileStream = File.Create("c:\sdk2\timbrados\FACTURACANCELAR.XML")

                    '' Add text to the file.
                    Dim info As Byte() = New UTF8Encoding(True).GetBytes(datosfac("CFDI").ToString().TrimStart())
                    fs.Write(info, 0, info.Length)
                    fs.Close()


                    'Leer XML

                    Dim cadenaxml As String = File.ReadAllText("c:\sdk2\timbrados\FACTURACANCELAR.XML")

                    Dim xm As New XmlDocument()
                    xm.LoadXml(cadenaxml)


                    rfc = My.Settings.RFC

                    'Código Cancelación

                    'Dim cancela As New CLSFACTURA()
                    'Dim salida As Boolean = cancela.cancelarbyid(UUID, rfc)
                    'Dim salida As Boolean = True

                    'If salida Then

                    Ejecucion("Update encfac set ESTADO='C' WHERE NUMERO=" + factura.ToString() & " and serie ='" & serie & "'")

                        'MessageBox.Show("FACTURA CANCELADA ANTE EL SAT CON EXITO!!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        MessageBox.Show("FACTURA CANCELADA EN EL SISTEMA CON EXITO!!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        result = MessageBox.Show("¿ Desea Cancelar tambien el Recibo ?", "Alerta", MessageBoxButtons.OKCancel)
                        Dim x As New base
                    If result = DialogResult.OK Then
                        Try
                            recibo = dataGridView1.Item("recibo", dataGridView1.CurrentRow.Index).Value.ToString()

                            serie = dataGridView1.Item("serierecibo", dataGridView1.CurrentRow.Index).Value.ToString()
                            cuenta = obtenerCampo("select cuenta from pagos where recibo=" & recibo & " and serie='" & serie & "'", "cuenta")

                            Dim estatusRecibo = obtenerCampo($"select cancelado from pagos where serie = {serie} and recibo = {recibo}", "cancelado")

                            If estatusRecibo = "A" Then


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
                                                Ejecucion("DELETE FROM lecturas WHERE CUENTA=" & cuenta & " AND MES='" & mesquecancelo & "' AND AN_PER='" & periodo & "'")
                                            End If
                                            dato2.conexion.Dispose()
                                        Catch ex As Exception

                                        End Try
                                        '  Dim cuenta_ As String = consulta!cuenta
                                        Try
                                            Ejecucion("UPDATE lecturas SET PAGADO=0 WHERE CUENTA=" & cuenta & " AND MES='" & mesquecancelo & "' AND AN_PER='" & periodo & "'")
                                        Catch ex As Exception
                                            guardatxt("c:\errorpagos", "errorpagos.txt", ex.Message)
                                        End Try


                                        Try


                                            Dim creditoUsuario = obtenerCampo($"select credito from usuario where cuenta = {cuenta}", "credito")

                                            If creditoUsuario > 0 Then

                                                RestaurarCreditosUsuario(cuenta, serie, recibo)

                                            End If


                                        Catch ex As Exception

                                            MessageBox.Show($"Ocurrio un error al actualizar el crédito del usuario: {ex.ToString()}")

                                        End Try


                                    End While
                                    dato.conexion.Dispose()
                                Catch ex As Exception
                                    MessageBox.Show(ex.Message)
                                End Try

                            Else

                                MessageBox.Show("Este recibo ya ha sido cancelado anteriormente")

                            End If

                            x.conexion.Dispose()
                        Catch ex As Exception
                            MessageBox.Show(ex.Message)
                        End Try

                        MessageBox.Show("¡¡¡LOS SALDOS SE HAN RESTAURADO EXITOSAMENTE!!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)



                    End If





                    'End If
                    'If sdkresp.Codigo_MF_Texto.Contains("previamente cancelado") Then
                    '    bas.conectar()
                    '    bas.ejecutar("Update encfac set ESTADO='C' WHERE NUMERO=" + factura.ToString() & " and serie ='" & serie & "'")
                    '    bas.desconectar()
                    '    bas.conexion.Dispose()
                    '    MessageBox.Show("La factura esta cancelada ante el SAT ")
                    'End If

                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

            End If ' fin del proveedor multifacturas
        End If ' fin de la pregunta si desea cancelar la factura
        bas.conexion.Close()
    End Sub


    Public Function sha1(ByVal str As String) As Byte()
        Dim UE As New UTF8Encoding()
        Dim hashValue As Byte()
        Dim message As Byte() = UE.GetBytes(str)
        Dim hashString As New SHA1Managed()
        hashValue = hashString.ComputeHash(message)
        Return hashValue
    End Function

    'Private Sub btnreimprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreimprimir.Click
    '    Dim factura As Int32 = dataGridView1.Item("numero", dataGridView1.CurrentRow.Index).Value
    '    Dim serie As String = dataGridView1.Item("serie", dataGridView1.CurrentRow.Index).Value.ToString()
    '    Dim tipo As String = dataGridView1.Item("ES", dataGridView1.CurrentRow.Index).Value.ToString()
    '    recibo = dataGridView1.Item("recibo", dataGridView1.CurrentRow.Index).Value.ToString()
    '    Dim serierecibo As String = dataGridView1.Item("serierecibo", dataGridView1.CurrentRow.Index).Value.ToString()
    '    '  Dim cuenta As Long = dataGridView1.Item("cuenta", dataGridView1.CurrentRow.Index).Value
    '    conectar()
    '    If Not My.Computer.FileSystem.DirectoryExists(Application.StartupPath & "\facturas\reimpresas\") Then
    '        My.Computer.FileSystem.CreateDirectory(Application.StartupPath & "\facturas\reimpresas\")
    '    End If
    '    Dim CADENAORIGINAL As String = String.Empty
    '    If tipo.ToUpper = "CLIENTE" Then tipo = "NO USUARIO"
    '    Select Case tipo.ToUpper
    '        Case "USUARIO", "NO USUARIO", "FACTIBILIDAD"
    '            Dim reporte As New ReportDocument()
    '            Dim reader As XmlTextReader
    '            Dim nombre As String = dataGridView1.Item("nombre", dataGridView1.CurrentRow.Index).Value.ToString()
    '            serierecibo = dataGridView1.Item("SERIE", dataGridView1.CurrentRow.Index).Value.ToString()
    '            'Dim es As String = dataGridView1.Item("es", dataGridView1.CurrentRow.Index).Value.ToString()
    '            Dim nombresespacios As String = nombre.Replace(" ", "")
    '            'Dim bas As New base
    '            Dim datosfac As IDataReader
    '            Dim datosrecibo As IDataReader
    '            Dim cuenta As String = obtenerCampo("select cuenta from pagos where recibo=" & recibo & " and serie='" & serierecibo & "'", "cuenta")
    '            datosrecibo = ConsultaSql("select * from datosfiscales where datosfiscales.cuenta=" & cuenta & " and datosfiscales.tipo='" & tipo & "'").ExecuteReader
    '            datosrecibo.Read()
    '            datosfac = ConsultaSql("select * from encfac where numero=" & factura & " and serie='" & serie & "'").ExecuteReader
    '            If datosfac.Read Then
    '                Dim cadenafolder As String = Application.StartupPath & "\facturas\reimpresas\"
    '                Dim varUUID As String = String.Empty
    '                Dim varTotal As Decimal
    '                Dim VARRFCEMISOR As String = String.Empty
    '                Dim varRFCRECEPTOR As String = String.Empty
    '                Dim varcertificado As String = String.Empty
    '                Dim VARSELLOSAT As String = String.Empty
    '                Dim VARSELLOCFD As String = String.Empty
    '                Dim VARNODECERTIFICADO As String = ""
    '                Dim varformapago As String = "01"
    '                Dim VARUSO As String = "G03"
    '                Dim varmetodo As String = "PUE"
    '                Try
    '                    '    If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
    '                    If serie = " " Then serie = ""
    '                    Dim fs As FileStream = File.Create((cadenafolder & "\FACTURA" & serie & factura & ".XML").Trim)
    '                    ' Add text to the file.
    '                    Dim info As Byte() = New UTF8Encoding(True).GetBytes(datosfac("CFDI").ToString().TrimStart().TrimEnd())
    '                    fs.Write(info, 0, info.Length)
    '                    fs.Close()
    '                    ' CONSTRIUIMOS LA CADENA
    '                    CADENAORIGINAL = datosfac("cadena") 'CONSTRUIRCADENACFDI((cadenafolder & "\FACTURA" & serie & factura & ".XML").Trim)
    '                    ' MANDAMOS CONSTRUIR EL QR 
    '                    ' LEEMOS EL XML
    '                    Dim image As System.Drawing.Image = qrdatos(varUUID, varTotal, VARRFCEMISOR, varRFCRECEPTOR, varcertificado)
    '                    Dim imageConverter As New ImageConverter()
    '                    Dim pngs As Byte() = DirectCast(imageConverter.ConvertTo(image, GetType(Byte())), Byte())
    '                    Dim dts As New DatosReciboTableAdapters.cajasTableAdapter
    '                    dts.UpdateQueryimagen(pngs, My.Settings.caja)
    '                    ' End If
    '                Catch ex As Exception
    '                End Try
    '                If datosfac("version") = "3.2" Then
    '                    'Dim varXmlFile As XmlDocument = New XmlDocument()
    '                    'Dim varXmlNsMngr As XmlNamespaceManager = New XmlNamespaceManager(varXmlFile.NameTable)
    '                    'varXmlFile.Load((cadenafolder & "\FACTURA" & serie & factura & ".XML").Trim)
    '                    'varXmlNsMngr.AddNamespace("cfdi", "http://www.sat.gob.mx/cfd/3")
    '                    'varXmlNsMngr.AddNamespace("tfd", "http://www.sat.gob.mx/TimbreFiscalDigital")
    '                    'VARUSO = varXmlFile.ChildNodes.Item(1).Attributes.Item(15).Value
    '                    'varTotal = varXmlFile.ChildNodes.Item(1).Attributes.Item(14).Value '  varXmlFile.SelectSingleNode("/cfdi:Comprobante/@total", varXmlNsMngr).InnerText
    '                    'VARNODECERTIFICADO = varXmlFile.ChildNodes.Item(1).Attributes.Item(11).Value 'varXmlFile.SelectSingleNode("/cfdi:Comprobante/@NoCertificado", varXmlNsMngr).InnerText
    '                    'varformapago = varXmlFile.ChildNodes.Item(1).Attributes.Item(15).Value 'varXmlFile.SelectSingleNode("/cfdi:Comprobante/@formapago", varXmlNsMngr).InnerText
    '                    'varUUID = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@UUID", varXmlNsMngr).InnerText
    '                    'varcertificado = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@noCertificadoSAT", varXmlNsMngr).InnerText
    '                    'VARSELLOSAT = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@selloSAT", varXmlNsMngr).InnerText
    '                    'VARSELLOCFD = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@selloCFD", varXmlNsMngr).InnerText
    '                    'Dim LISTANODOSEMISOR As XmlNodeList = varXmlFile.GetElementsByTagName("cfdi:Emisor")
    '                    'For Each xAtt In LISTANODOSEMISOR
    '                    '    VARRFCEMISOR = VarXml(xAtt, "rfc")
    '                    '    ' strEmisorNombre = VarXml(xAtt, "nombre")
    '                    'Next
    '                    'Dim LISTANODORECEPTOR As XmlNodeList = varXmlFile.GetElementsByTagName("cfdi:Receptor")
    '                    'For Each xAtt In LISTANODORECEPTOR
    '                    '    varRFCRECEPTOR = VarXml(xAtt, "rfc")
    '                    '    ' strEmisorNombre = VarXml(xAtt, "nombre")
    '                    'Next
    '                    'reporte.Load(AppPath() & "\REPORTES\FACTURA.RPT")
    '                    'Dim servidorreporte As String = My.Settings.servidorreporte
    '                    'Dim usuarioreporte As String = My.Settings.usuarioreporte
    '                    'Dim passreporte As String = My.Settings.passreporte
    '                    'Dim basereporte As String = My.Settings.basereporte
    '                    'reporte.DataSourceConnections.Item(0).SetConnection(servidorreporte, basereporte, False)
    '                    'reporte.DataSourceConnections.Item(0).SetLogon(usuarioreporte, passreporte)
    '                    'reporte.SetParameterValue("nombre", datosfac("NOMBRE"))
    '                    'reporte.SetParameterValue("Direccion", datosrecibo("calle") & " " & datosrecibo("numext") & " " & datosrecibo("numint"))
    '                    'reporte.DataDefinition.FormulaFields("colonia").Text = "'" & datosrecibo("colonia") & "'"
    '                    'reporte.DataDefinition.FormulaFields("ciudad").Text = "'" & datosrecibo("poblacion") & " " & datosrecibo("delegacion") & " " & datosrecibo("estado") & " CP " & datosrecibo("cp") & "'"
    '                    'reporte.SetParameterValue("fechatimbrado", datosfac("FECHA"))
    '                    'reporte.SetParameterValue("certificado", datosfac("NODECERTIFICADO"))
    '                    'reporte.SetParameterValue("cantidadconletra", ConvertCurrencyToSpanish(datosfac("TOTAL"), "Pesos"))
    '                    'reporte.SetParameterValue("formadepago", varformapago)
    '                    'reporte.SetParameterValue("Cadenaoriginal", CADENAORIGINAL)
    '                    'reporte.SetParameterValue("foliofiscal", datosfac("SERIE") & datosfac("NUMERO"))
    '                    'reporte.SetParameterValue("RFCCLIENTE", varRFCRECEPTOR)
    '                    'reporte.SetParameterValue("CERTIFICADOSAT", datosfac("CERTIFICADOSAT"))
    '                    'reporte.SetParameterValue("nota", "")
    '                    'reporte.SetParameterValue("SerieCertificado", VARNODECERTIFICADO)
    '                    'reporte.SetParameterValue("Sellodigital", VARSELLOSAT)
    '                    'reporte.SetParameterValue("SelloCFDI", VARSELLOCFD)
    '                    'reporte.SetParameterValue("UUID", datosfac("UUID"))
    '                    'reporte.SetParameterValue("Medidor", "")
    '                    'reporte.SetParameterValue("Promedio", "")
    '                    'reporte.SetParameterValue("UsoCfdi", VARUSO)
    '                    'reporte.SetParameterValue("MetodoPago", varmetodo)
    '                    'reporte.RecordSelectionFormula = "{cajas1.ID_CAJA}='" & My.Settings.caja & "' and {pagos1.recibo}=" & recibo & " and {pagos1.serie}='" & serierecibo & "'"
    '                ElseIf datosfac("version") = "3.3" Then
    '                    Dim varXmlFile As XmlDocument = New XmlDocument()
    '                    Dim varXmlNsMngr As XmlNamespaceManager = New XmlNamespaceManager(varXmlFile.NameTable)
    '                    varXmlFile.Load((cadenafolder & "\FACTURA" & serie & factura & ".XML").Trim)
    '                    varXmlNsMngr.AddNamespace("cfdi", "http://www.sat.gob.mx/cfd/3")
    '                    varXmlNsMngr.AddNamespace("tfd", "http://www.sat.gob.mx/TimbreFiscalDigital")
    '                    varTotal = varXmlFile.ChildNodes.Item(1).Attributes.GetNamedItem("Total").Value '  varXmlFile.SelectSingleNode("/cfdi:Comprobante/@total", varXmlNsMngr).InnerText
    '                    VARNODECERTIFICADO = varXmlFile.ChildNodes.Item(1).Attributes.GetNamedItem("NoCertificado").Value 'varXmlFile.SelectSingleNode("/cfdi:Comprobante/@NoCertificado", varXmlNsMngr).InnerText
    '                    varformapago = varXmlFile.ChildNodes.Item(1).Attributes.GetNamedItem("FormaPago").Value 'varXmlFile.SelectSingleNode("/cfdi:Comprobante/@formapago", varXmlNsMngr).InnerText
    '                    varUUID = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@UUID", varXmlNsMngr).InnerText
    '                    varcertificado = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@NoCertificadoSAT", varXmlNsMngr).InnerText
    '                    VARSELLOSAT = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@SelloSAT", varXmlNsMngr).InnerText
    '                    VARSELLOCFD = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@SelloCFD", varXmlNsMngr).InnerText
    '                    Dim LISTANODOSEMISOR As XmlNodeList = varXmlFile.GetElementsByTagName("cfdi:Emisor")
    '                    For Each xAtt In LISTANODOSEMISOR
    '                        VARRFCEMISOR = VarXml(xAtt, "rfc")
    '                        ' strEmisorNombre = VarXml(xAtt, "nombre")
    '                    Next
    '                    Dim LISTANODORECEPTOR As XmlNodeList = varXmlFile.GetElementsByTagName("cfdi:Receptor")
    '                    For Each xAtt In LISTANODORECEPTOR
    '                        varRFCRECEPTOR = VarXml(xAtt, "rfc")
    '                        ' strEmisorNombre = VarXml(xAtt, "nombre")
    '                    Next
    '                    reporte.Load(AppPath() & "\REPORTES\FACTURA.RPT")
    '                    Dim servidorreporte As String = My.Settings.servidorreporte
    '                    Dim usuarioreporte As String = My.Settings.usuarioreporte
    '                    Dim passreporte As String = My.Settings.passreporte
    '                    Dim basereporte As String = My.Settings.basereporte
    '                    reporte.DataSourceConnections.Item(0).SetConnection(servidorreporte, basereporte, False)
    '                    reporte.DataSourceConnections.Item(0).SetLogon(usuarioreporte, passreporte)
    '                    reporte.SetParameterValue("nombre", datosfac("NOMBRE"))
    '                    reporte.SetParameterValue("Direccion", datosrecibo("calle") & " " & datosrecibo("numext") & " " & datosrecibo("numint"))
    '                    reporte.DataDefinition.FormulaFields("colonia").Text = "'" & datosrecibo("colonia") & "'"
    '                    reporte.DataDefinition.FormulaFields("ciudad").Text = "'" & datosrecibo("poblacion") & " " & datosrecibo("delegacion") & " " & datosrecibo("estado") & " CP " & datosrecibo("cp") & "'"
    '                    reporte.SetParameterValue("fechatimbrado", datosfac("FECHA"))
    '                    reporte.SetParameterValue("certificado", datosfac("NODECERTIFICADO"))
    '                    reporte.SetParameterValue("cantidadconletra", ConvertCurrencyToSpanish(datosfac("TOTAL"), "Pesos"))
    '                    reporte.SetParameterValue("formadepago", varformapago)
    '                    reporte.SetParameterValue("Cadenaoriginal", CADENAORIGINAL)
    '                    reporte.SetParameterValue("foliofiscal", datosfac("SERIE") & datosfac("NUMERO"))
    '                    reporte.SetParameterValue("RFCCLIENTE", varRFCRECEPTOR)
    '                    reporte.SetParameterValue("CERTIFICADOSAT", datosfac("CERTIFICADOSAT"))
    '                    reporte.SetParameterValue("nota", "")
    '                    reporte.SetParameterValue("SerieCertificado", VARNODECERTIFICADO)
    '                    reporte.SetParameterValue("Sellodigital", VARSELLOSAT)
    '                    reporte.SetParameterValue("SelloCFDI", VARSELLOCFD)
    '                    reporte.SetParameterValue("UUID", datosfac("UUID"))
    '                    reporte.SetParameterValue("Medidor", "")
    '                    reporte.SetParameterValue("Promedio", "")
    '                    reporte.SetParameterValue("UsoCfdi", VARUSO)
    '                    reporte.SetParameterValue("MetodoPago", varmetodo)
    '                    'reporte.Subreports.Item(0).DataSourceConnections.Item(0).SetConnection(servidorreporte, basereporte, False)
    '                    'reporte.Subreports.Item(0).DataSourceConnections.Item(0).SetLogon(usuarioreporte, passreporte)
    '                    'reporte.Subreports.Item(1).DataSourceConnections.Item(0).SetConnection(servidorreporte, basereporte, False)
    '                    'reporte.Subreports.Item(1).DataSourceConnections.Item(0).SetLogon(usuarioreporte, passreporte)
    '                    reporte.RecordSelectionFormula = "{cajas1.ID_CAJA}='" & My.Settings.caja & "' and {pagos1.recibo}=" & recibo & " and {pagos1.serie}='" & serierecibo & "'"
    '                End If
    '            End If ' fin de si leyo las lecturas
    '            ' CREA EL REPORTE EN PDF 
    '            Dim cadenapdf As String = ""
    '            Try
    '                cadenapdf = (Application.StartupPath & "\facturas\reimpresas\FACTURA" & serie & factura & ".pdf").Trim
    '                ExportToDisk(cadenapdf, reporte)
    '                'Try
    '                '    Dim psi As New ProcessStartInfo("FACTURA" & serie & factura & ".pdf")
    '                '    psi.WorkingDirectory = cadenafolder & ""
    '                '    psi.WindowStyle = ProcessWindowStyle.Hidden
    '                '    Dim p As Process = Process.Start(psi)
    '                'Catch ex As Exception
    '                '    MessageBox.Show("error al visualizar el pdf" & ex.Message)
    '                'End Try
    '                imprimirpdf(cadenapdf)
    '            Catch ex As Exception
    '                MessageBox.Show("error " & ex.Message)
    '            End Try
    '                                    'cadenapdf = Application.StartupPath & "\facturas\" + nombresespacios & "\PdfFactura" & seriefactura & foliofactura & ".pdf"
    '        Case "DE PERIODO"
    '            Dim reporte As New ReportDocument()
    '            Dim datosfac As IDataReader
    '            datosfac = ConsultaSql("select * from encfac where numero=" & factura & " and serie='" & serie & "'").ExecuteReader
    '            If datosfac.Read Then
    '                Try
    '                    If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
    '                        ' guardatxt(FolderBrowserDialog1.SelectedPath, "factura.xml", xml)
    '                        ' Create or overwrite the file.
    '                        Dim fs As FileStream = File.Create(FolderBrowserDialog1.SelectedPath & "\FACTURA" & serie & factura & ".XML")
    '                        ' Add text to the file.
    '                        Dim info As Byte() = New UTF8Encoding(True).GetBytes(datosfac("CFDI").ToString().TrimStart())
    '                        fs.Write(info, 0, info.Length)
    '                        fs.Close()
    '                        'Dim CADENA() As String = datosfac("respuestaoriginal").ToString().TrimStart().Split("**********")
    '                        '  Dim pngs As Byte() = System.Convert.FromBase64String(Buscavariable(CADENA, "png"))
    '                        ' CONSTRIUIMOS LA CADENA
    '                        CADENAORIGINAL = datosfac("cadena") 'CONSTRUIRCADENACFDI(FolderBrowserDialog1.SelectedPath & "\FACTURA" & serie & factura & ".XML")
    '                        ' MANDAMOS CONSTRUIR EL QR 
    '                        Dim image As System.Drawing.Image = qr(FolderBrowserDialog1.SelectedPath & "\FACTURA" & serie & factura & ".XML")
    '                        Dim imageConverter As New ImageConverter()
    '                        Dim pngs As Byte() = DirectCast(imageConverter.ConvertTo(image, GetType(Byte())), Byte())
    '                        Dim dts As New DatosReciboTableAdapters.cajasTableAdapter
    '                        dts.UpdateQueryimagen(pngs, My.Settings.caja)
    '                    End If
    '                Catch ex As Exception
    '                End Try
    '                reporte.Load(AppPath() & "\REPORTES\FACTURADIA33.RPT")
    '                Dim servidorreporte As String = My.Settings.servidorreporte
    '                Dim usuarioreporte As String = My.Settings.usuarioreporte
    '                Dim passreporte As String = My.Settings.passreporte
    '                Dim basereporte As String = My.Settings.basereporte
    '                reporte.DataSourceConnections.Item(1).SetConnection(servidorreporte, basereporte, False)
    '                reporte.DataSourceConnections.Item(1).SetLogon(usuarioreporte, passreporte)
    '                Dim dataSet As DataSet = New DataSet
    '                dataSet.ReadXml(FolderBrowserDialog1.SelectedPath & "\FACTURA" & serie & factura & ".XML", XmlReadMode.Auto)
    '                reporte.SetDataSource(dataSet)
    '                Try
    '                    reporte.SetParameterValue("cantidadconletra", ConvertCurrencyToSpanish(datosfac("total"), "Pesos"))
    '                    reporte.SetParameterValue("nota", datosfac("observacion").ToString())
    '                    reporte.SetParameterValue("CADENAORIGINAL", CADENAORIGINAL)
    '                    reporte.RecordSelectionFormula = "{cajas1.ID_CAJA}='" & My.Settings.caja & "'"
    '                Catch ex As Exception
    '                    MessageBox.Show(ex.Message)
    '                End Try
    '                ' CREA EL REPORTE EN PDF 
    '                Dim cadenapdf As String = ""
    '                Try
    '                    cadenapdf = FolderBrowserDialog1.SelectedPath & "\FACTURA" & serie & factura & ".PDF"
    '                    ExportToDisk(cadenapdf, reporte)
    '                    Try
    '                        Dim psi As New ProcessStartInfo("FACTURA" & serie & factura & ".PDF")
    '                        psi.WorkingDirectory = FolderBrowserDialog1.SelectedPath & ""
    '                        psi.WindowStyle = ProcessWindowStyle.Hidden
    '                        Dim p As Process = Process.Start(psi)
    '                    Catch ex As Exception
    '                        MessageBox.Show("error al visualizar el pdf" & ex.Message)
    '                    End Try
    '                Catch ex As Exception
    '                    MessageBox.Show("" & ex.Message)
    '                End Try
    '                'cadenapdf = Application.StartupPath & "\facturas\" + nombresespacios & "\PdfFactura" & seriefactura & foliofactura & ".pdf"
    '            End If
    '    End Select
    'End Sub


    Private Sub btnreimprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreimprimir.Click
        Dim factura As Int32 = dataGridView1.Item("numero", dataGridView1.CurrentRow.Index).Value
        Dim serie As String = dataGridView1.Item("serie", dataGridView1.CurrentRow.Index).Value.ToString()
        Dim tipo As String = dataGridView1.Item("ES", dataGridView1.CurrentRow.Index).Value.ToString()
        Dim FECHAFAC As String = dataGridView1.Item("fecha", dataGridView1.CurrentRow.Index).Value.ToString()
        recibo = dataGridView1.Item("recibo", dataGridView1.CurrentRow.Index).Value.ToString()
        Dim serierecibo As String = dataGridView1.Item("serierecibo", dataGridView1.CurrentRow.Index).Value.ToString()
        Dim subtotal As String = dataGridView1.Item("Subtotal", dataGridView1.CurrentRow.Index).Value.ToString()
        Dim ivatotal As String = dataGridView1.Item("IVA", dataGridView1.CurrentRow.Index).Value.ToString()
        Dim totalfactura As String = dataGridView1.Item("Total", dataGridView1.CurrentRow.Index).Value.ToString()

        '  Dim cuenta As Long = dataGridView1.Item("cuenta", dataGridView1.CurrentRow.Index).Value
        conectar()



        If Not My.Computer.FileSystem.DirectoryExists(Application.StartupPath & "\facturas\reimpresas\") Then

            My.Computer.FileSystem.CreateDirectory(Application.StartupPath & "\facturas\reimpresas\")
        End If

        'If Not My.Computer.FileSystem.DirectoryExists(Environment.SpecialFolder.MyDocuments) & "\facturas\reimpresas\") Then

        '    My.Computer.FileSystem.CreateDirectory(Environment.SpecialFolder.MyDocuments) & "\facturas\reimpresas\")
        'End If

        Dim directorioreimpresas = (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\facturas\reimpresas").Trim

        If Not Directory.Exists(directorioreimpresas) Then
            Directory.CreateDirectory(directorioreimpresas)
        End If

        Dim CADENAORIGINAL As String = String.Empty
        If tipo.ToUpper = "CLIENTE" Then tipo = "NO USUARIO"
        Select Case tipo.ToUpper
            Case "USUARIO", "NO USUARIO", "FACTIBILIDAD"
                'Dim reporte As New ReportDocument()




                Dim nombre As String = dataGridView1.Item("nombre", dataGridView1.CurrentRow.Index).Value.ToString()

                Dim nombresespacios As String = nombre.Replace(" ", "")

                'Dim bas As New base
                Dim datosfac As IDataReader
                Dim datosrecibo As IDataReader
                Dim daatoscontenido As IDataReader

                Dim cuenta As String = obtenerCampo("select cuenta from pagos where recibo=" & recibo & " and serie='" & serierecibo & "'", "cuenta")
                datosrecibo = ConsultaSql("select * from datosfiscales where datosfiscales.cuenta=" & cuenta & " and datosfiscales.tipo='" & tipo & "'").ExecuteReader



                datosrecibo.Read()

                daatoscontenido = ConsultaSql("select * from pagotros where recibo=" & recibo & " and serie='" & serierecibo & "'").ExecuteReader

                'Dim descuentoRecargos As String = obtenerCampo("select sum(descuento) as descuento_r from pago_mes where recibo = " & recibo.NUMERO & " and serie = '" & seriedelreciboqueseestafacturando & "' and concepto = 'Recargo'", "descuento_r")

                datosfac = ConsultaSql("select * from encfac where numero=" & factura & " and serie='" & serie & "'").ExecuteReader
                If datosfac.Read Then
                    Dim cadenafolder As String = Application.StartupPath & "\facturas\reimpresas\"
                    Dim varUUID As String = String.Empty
                    Dim varTotal As Decimal
                    Dim VARRFCEMISOR As String = String.Empty
                    Dim varRFCRECEPTOR As String = String.Empty
                    Dim varcertificado As String = String.Empty
                    Dim VARSELLOSAT As String = String.Empty
                    Dim VARSELLOCFD As String = String.Empty
                    Dim VARNODECERTIFICADO As String = ""
                    Dim varformapago As String = "01"
                    Dim metodo As String = ""
                    Dim varuso As String = ""

                    Try



                        '    If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                        If serie = " " Then serie = ""

                        Dim fs As FileStream = File.Create((cadenafolder & "\FACTURA" & serie & factura & ".XML").Trim)
                        Dim fs1 As FileStream = File.Create((directorioreimpresas & "\FACTURA" & serie & factura & ".XML").Trim)
                        ' Add text to the file.
                        Dim info As Byte() = New UTF8Encoding(True).GetBytes(datosfac("CFDI").ToString().TrimStart().TrimEnd())
                        fs.Write(info, 0, info.Length)
                        fs.Close()

                        Dim info2 As Byte() = New UTF8Encoding(True).GetBytes(datosfac("CFDI").ToString().TrimStart().TrimEnd())
                        fs1.Write(info, 0, info2.Length)
                        fs1.Close()

                        ' CONSTRIUIMOS LA CADENA
                        CADENAORIGINAL = datosfac("cadena") 'CONSTRUIRCADENACFDI((cadenafolder & "\FACTURA" & serie & factura & ".XML").Trim)

                        ' MANDAMOS CONSTRUIR EL QR 

                        ' LEEMOS EL XML




                        Dim image As System.Drawing.Image = qrdatos(varUUID, varTotal, VARRFCEMISOR, varRFCRECEPTOR, varcertificado)

                        Dim imageConverter As New ImageConverter()
                        Dim pngs As Byte() = DirectCast(imageConverter.ConvertTo(image, GetType(Byte())), Byte())


                        Dim dts As New DatosReciboTableAdapters.cajasTableAdapter
                        dts.UpdateQueryimagen(pngs, My.Settings.caja)


                        ' End If
                    Catch ex As Exception

                    End Try


                    If datosfac("version") = "3.3" Then

                        Dim varXmlFile As XmlDocument = New XmlDocument()

                        Dim varXmlNsMngr As XmlNamespaceManager = New XmlNamespaceManager(varXmlFile.NameTable)


                        varXmlFile.Load((cadenafolder & "\FACTURA" & serie & factura & ".XML").Trim)

                        varXmlNsMngr.AddNamespace("cfdi", "http://www.sat.gob.mx/cfd/3")
                        varXmlNsMngr.AddNamespace("tfd", "http://www.sat.gob.mx/TimbreFiscalDigital")




                        varTotal = varXmlFile.ChildNodes.Item(1).Attributes.GetNamedItem("Total").Value '  varXmlFile.SelectSingleNode("/cfdi:Comprobante/@total", varXmlNsMngr).InnerText
                        VARNODECERTIFICADO = varXmlFile.ChildNodes.Item(1).Attributes.GetNamedItem("NoCertificado").Value 'varXmlFile.SelectSingleNode("/cfdi:Comprobante/@NoCertificado", varXmlNsMngr).InnerText
                        varformapago = varXmlFile.ChildNodes.Item(1).Attributes.GetNamedItem("FormaPago").Value 'varXmlFile.SelectSingleNode("/cfdi:Comprobante/@formapago", varXmlNsMngr).InnerText
                        varUUID = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@UUID", varXmlNsMngr).InnerText
                        varcertificado = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@NoCertificadoSAT", varXmlNsMngr).InnerText
                        VARSELLOSAT = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@SelloSAT", varXmlNsMngr).InnerText
                        VARSELLOCFD = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@SelloCFD", varXmlNsMngr).InnerText
                        Dim LISTANODOSEMISOR As XmlNodeList = varXmlFile.GetElementsByTagName("cfdi:Emisor")
                        For Each xAtt In LISTANODOSEMISOR
                            VARRFCEMISOR = VarXml(xAtt, "Rfc")
                            ' strEmisorNombre = VarXml(xAtt, "nombre")
                        Next
                        Dim LISTANODORECEPTOR As XmlNodeList = varXmlFile.GetElementsByTagName("cfdi:Receptor")


                        For Each xAtt In LISTANODORECEPTOR
                            varRFCRECEPTOR = VarXml(xAtt, "Rfc")
                            ' strEmisorNombre = VarXml(xAtt, "nombre")
                            varuso = VarXml(xAtt, "UsoCFDI")
                        Next




                        ' AQUI PONEMOS EL CODIGO

                        metodo = varXmlFile.ChildNodes.Item(1).Attributes.GetNamedItem("MetodoPago").Value

                    ElseIf datosfac("version") = "4.0" Then

                        Dim varXmlFile As XmlDocument = New XmlDocument()

                        Dim varXmlNsMngr As XmlNamespaceManager = New XmlNamespaceManager(varXmlFile.NameTable)


                        varXmlFile.Load((cadenafolder & "\FACTURA" & serie & factura & ".XML").Trim)

                        varXmlNsMngr.AddNamespace("cfdi", "http://www.sat.gob.mx/cfd/3")
                        varXmlNsMngr.AddNamespace("tfd", "http://www.sat.gob.mx/TimbreFiscalDigital")




                        varTotal = varXmlFile.ChildNodes.Item(1).Attributes.GetNamedItem("Total").Value '  varXmlFile.SelectSingleNode("/cfdi:Comprobante/@total", varXmlNsMngr).InnerText
                        VARNODECERTIFICADO = varXmlFile.ChildNodes.Item(1).Attributes.GetNamedItem("NoCertificado").Value 'varXmlFile.SelectSingleNode("/cfdi:Comprobante/@NoCertificado", varXmlNsMngr).InnerText
                        varformapago = varXmlFile.ChildNodes.Item(1).Attributes.GetNamedItem("FormaPago").Value 'varXmlFile.SelectSingleNode("/cfdi:Comprobante/@formapago", varXmlNsMngr).InnerText

                        Dim LISTANODOComplemento As XmlNodeList = varXmlFile.GetElementsByTagName("tfd:TimbreFiscalDigital")

                        For Each xAtt In LISTANODOComplemento
                            varUUID = VarXml(xAtt, "UUID")
                            varcertificado = VarXml(xAtt, "NoCertificadoSAT")
                            VARSELLOSAT = VarXml(xAtt, "SelloSAT")
                            VARSELLOCFD = VarXml(xAtt, "SelloCFD")
                            'strEmisorNombre = VarXml(xAtt, "nombre")
                        Next

                        'varUUID = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@UUID", varXmlNsMngr).InnerText
                        'varcertificado = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@NoCertificadoSAT", varXmlNsMngr).InnerText
                        'VARSELLOSAT = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@SelloSAT", varXmlNsMngr).InnerText
                        'VARSELLOCFD = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@SelloCFD", varXmlNsMngr).InnerText
                        Dim LISTANODOSEMISOR As XmlNodeList = varXmlFile.GetElementsByTagName("cfdi:Emisor")
                        For Each xAtt In LISTANODOSEMISOR
                            VARRFCEMISOR = VarXml(xAtt, "Rfc")
                            ' strEmisorNombre = VarXml(xAtt, "nombre")
                        Next
                        Dim LISTANODORECEPTOR As XmlNodeList = varXmlFile.GetElementsByTagName("cfdi:Receptor")

                        'Dim varuso As String
                        For Each xAtt In LISTANODORECEPTOR
                            varRFCRECEPTOR = VarXml(xAtt, "Rfc")
                            ' strEmisorNombre = VarXml(xAtt, "nombre")
                            varuso = VarXml(xAtt, "UsoCFDI")
                        Next




                        ' AQUI PONEMOS EL CODIGO

                        metodo = varXmlFile.ChildNodes.Item(1).Attributes.GetNamedItem("MetodoPago").Value

                    End If

#Region "Dar propiedades al documento"

                    'Dar propiedades al Documento
                    Dim pdfDoc As New Document(iTextSharp.text.PageSize.LETTER, 15.0F, 15.0F, 30.0F, 30.0F)

                        Dim pdfWrite As PdfWriter = PdfWriter.GetInstance(pdfDoc, New System.IO.FileStream(cadenafolder & "\factura" & serie & factura & ".pdf", FileMode.Create))
                        Dim pdfWrite2 As PdfWriter = PdfWriter.GetInstance(pdfDoc, New System.IO.FileStream(directorioreimpresas & "\factura" & serie & factura & ".pdf", FileMode.Create))

                        'Formtos para distintos tamaños de letras

                        'Formato Letras




                        Dim Font As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 6, iTextSharp.text.Font.NORMAL))
                        Dim Font8N As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 6, iTextSharp.text.Font.BOLD))
                        Dim Font8 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.BOLD))
                        Dim Font88 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 12, iTextSharp.text.Font.BOLD))
                        Dim Font12 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.BOLD))
                        Dim Font9 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 7, iTextSharp.text.Font.NORMAL))
                        Dim Fontp As New Font(FontFactory.GetFont(FontFactory.COURIER, 7, iTextSharp.text.Font.BOLD))
                        Dim CVacio As PdfPCell = New PdfPCell(New Phrase(""))
                        CVacio.Border = 0




#End Region

#Region "Encabezado"

                        'abrimos el pdf para comenzar a escribir en el, al terminar cerramos
                        pdfDoc.Open()




                        ' comenzamos con un cuadro

                        'Dim _cb As PdfContentByte

                        Dim colordefinido = New Clscolorreporte()
                        colordefinido.ClsColoresReporte(My.Settings.colorfactura)

                        '_cb = pdfWrite.DirectContentUnder
                        '_cb.SetColorStroke(colordefinido.color) '/Color de la linea
                        '_cb.SetColorFill(colordefinido.color) '/ Color del relleno
                        '_cb.SetLineWidth(3.5) ''Tamano de la linea
                        '_cb.Rectangle(350, 720, 20, 100)
                        '_cb.FillStroke()

                        '''


                        Dim Table1 As PdfPTable = New PdfPTable(3)
                        Table1.DefaultCell.Border = BorderStyle.None
                        Dim Col1 As PdfPCell
                        'Dim ILine As Integer
                        'Dim iFila As Integer
                        Table1.WidthPercentage = 100

                        Dim widths As Single() = New Single() {100.0F, 300, 280.0F}
                        Table1.SetWidths(widths)

                        'Encabezado

                        '   Dim imagenURL As String = "C:\Users\User\Desktop\huichapan\CAJAS Huichapan 3.3\LogoHuichapan.jpg"




                        Dim imagenBMP As iTextSharp.text.Image
                        imagenBMP = iTextSharp.text.Image.GetInstance(LOGOBYTE)
                        imagenBMP.ScaleToFit(80.0F, 70.0F)
                        'imagenBMP.SpacingBefore = 100.0F
                        'imagenBMP.SpacingAfter = 1000.0F

                        imagenBMP.Border = 0


                        Table1.AddCell(imagenBMP)

                        'Sustituir por los valores reales cuando se pase a frmvalidafactura,vb



                        Dim Tabledireccion As PdfPTable = New PdfPTable(1)
                        Col1 = New PdfPCell(New Phrase(Empresa, Font12))
                        Col1.Border = 0
                        Col1.HorizontalAlignment = PdfPCell.ALIGN_CENTER

                        Dim DIRECCIONE As String = Direccion & " " & coloniaEMPRESA & " " & poblacionEMPRESA & " " & Estadoempresa
                        Dim Col1d = New PdfPCell(New Phrase(DIRECCIONE, Font8))
                        Col1d.Border = 0
                        Col1d.HorizontalAlignment = PdfPCell.ALIGN_CENTER


                        Dim Col1rfe = New PdfPCell(New Phrase(RFCORGANISMO, Font9))
                        Col1rfe.Border = 0
                        Col1rfe.HorizontalAlignment = PdfPCell.ALIGN_CENTER

                        Tabledireccion.AddCell(Col1)
                        Tabledireccion.AddCell(Col1d)
                        Tabledireccion.AddCell(Col1rfe)
                        Table1.AddCell(Tabledireccion)

                        Dim Table2 As PdfPTable = New PdfPTable(2)
                        Dim Col10 As PdfPCell
                        Dim Col11 As PdfPCell
                        Dim Col12 As PdfPCell
                        Dim Col13 As PdfPCell
                        Dim Col14 As PdfPCell
                        Table2.WidthPercentage = 100
                        Dim widthsT2 As Single() = New Single() {100, 180.0F}
                        Table2.SetWidths(widthsT2)

                        Col10 = New PdfPCell(New Phrase("Serie", Font88))
                        Col10.Border = 0
                        Col10.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(Col10)


                        Col11 = New PdfPCell(New Phrase(serie, Font88))
                        Col11.Border = 0
                        Col11.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(Col11)

                        Dim Col10f = New PdfPCell(New Phrase("Factura", Font88))
                        Col10f.Border = 0
                        Col10f.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(Col10f)


                        Col12 = New PdfPCell(New Phrase(factura, Font88))
                        Col12.Border = 0
                        Col12.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(Col12)

                        Col13 = New PdfPCell(New Phrase("Fecha de comprobante:", Font))
                        Col13.Border = 0
                        Col13.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(Col13)

                        Col14 = New PdfPCell(New Phrase(FECHAFAC, Font))
                        Col14.Border = 0
                        Col14.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(Col14)


                        Dim ColDC1 = New PdfPCell(New Phrase("UUID", Font))
                        ColDC1.Border = 0
                        ColDC1.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(ColDC1)


                        Dim ColDC2 = New PdfPCell(New Phrase(varUUID, Font))
                        ColDC2.Border = 0
                        ColDC2.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(ColDC2)

                        Dim ColDC3 = New PdfPCell(New Phrase("Certificado Emisor", Font))
                        ColDC3.Border = 0
                        ColDC3.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(ColDC3)

                        Dim ColDC4 = New PdfPCell(New Phrase(varcertificado, Font))
                        ColDC4.Border = 0
                        ColDC4.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(ColDC4)


                        Dim ColDC7 = New PdfPCell(New Phrase("Certificado Sat ", Font))
                        ColDC7.Border = 0
                        ColDC7.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(ColDC7)

                        Dim ColDC8 = New PdfPCell(New Phrase(VARNODECERTIFICADO, Font))
                        ColDC8.Border = 0
                        ColDC8.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(ColDC8)



                        Dim ColDC11 = New PdfPCell(New Phrase("Forma de Pago ", Font))
                        ColDC11.Border = 0
                        ColDC11.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(ColDC11)

                        'Dim ColDC12 = New PdfPCell(New Phrase(varformapago, Font))
                        'ColDC12.Border = 0
                        'ColDC12.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        'Table2.AddCell(ColDC12)

                        Dim ForPago As String = varformapago.ToString()


                        Dim ColDC12 = New PdfPCell(New Phrase(New decodificadorSAT().getFormapago(ForPago), Font))
                        ColDC12.Border = 0
                        ColDC12.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(ColDC12)




                        Dim ColDCMeP = New PdfPCell(New Phrase("Método de Pago ", Font))
                        ColDCMeP.Border = 0
                        ColDCMeP.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(ColDCMeP)

                        Dim ColDCMeP2 = New PdfPCell(New Phrase(New decodificadorSAT().getMetodo(metodo), Font))
                        ColDCMeP2.Border = 0
                        ColDCMeP2.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(ColDCMeP2)

                        Dim ColDCUsoCFDI = New PdfPCell(New Phrase("Uso CFDI ", Font))
                        ColDCUsoCFDI.Border = 0
                        ColDCUsoCFDI.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(ColDCUsoCFDI)

                        Dim ColDCUsoCFDI2 = New PdfPCell(New Phrase(New decodificadorSAT().getUso(varuso), Font))
                        ColDCUsoCFDI2.Border = 0
                        ColDCUsoCFDI2.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(ColDCUsoCFDI2)

                        Table1.AddCell(Table2)

                        Table1.AddCell(Table2)


                        'Table1.CompleteRow()



                        'Table Datos Cliente
                        Dim Table4 As PdfPTable = New PdfPTable(2)
                        Dim ColDN As PdfPCell
                        Dim ColDN1 As PdfPCell
                        Dim ColDN2 As PdfPCell
                        Dim ColDN3 As PdfPCell
                        Dim ColDN4 As PdfPCell
                        Dim ColDN5 As PdfPCell
                        Dim ColDN6 As PdfPCell
                        Dim ColDN7 As PdfPCell
                        Dim ColDN8 As PdfPCell
                        Dim ColDN9 As PdfPCell
                        Table4.WidthPercentage = 100
                        Dim widthsT4 As Single() = New Single() {600.0F, 200.0F}

                        Table4.SetWidths(widthsT4)

                        ColDN = New PdfPCell(New Phrase(datosfac("NOMBRE"), Font9))
                        ColDN.Border = 0
                        ColDN.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table4.AddCell(ColDN)

                        ColDN1 = New PdfPCell(New Phrase(varRFCRECEPTOR, Font9))
                        ColDN1.Border = 0
                        ColDN1.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table4.AddCell(ColDN1)

                        ColDN2 = New PdfPCell(New Phrase(datosrecibo("calle") & " " & datosrecibo("numext") & " " & datosrecibo("numint"), Font9))
                        ColDN2.Border = 0
                        ColDN2.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table4.AddCell(ColDN2)

                        ColDN3 = New PdfPCell(New Phrase("Cuenta: " & cuenta, Font))
                        ColDN3.Border = 0
                        ColDN3.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table4.AddCell(ColDN3)

                        ColDN4 = New PdfPCell(New Phrase(datosrecibo("colonia"), Font9))
                        ColDN4.Border = 0
                        ColDN4.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table4.AddCell(ColDN4)

                        ColDN5 = New PdfPCell(New Phrase("Tipo: " & tipo, Font))
                        ColDN5.Border = 0
                        ColDN5.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table4.AddCell(ColDN5)

                        ColDN6 = New PdfPCell(New Phrase(datosrecibo("poblacion") & " " & datosrecibo("delegacion") & " " & datosrecibo("estado") & " CP " & datosrecibo("cp"), Font9))
                        ColDN6.Border = 0
                        ColDN6.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table4.AddCell(ColDN6)

                        ColDN7 = New PdfPCell(New Phrase(" ", Font))
                        ColDN7.Border = 0
                        ColDN7.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table4.AddCell(ColDN7)

                        ColDN8 = New PdfPCell(New Phrase(" ", Font9))
                        ColDN8.Border = 0
                        ColDN8.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table4.AddCell(ColDN8)

                        'Dim datos As Odbc.OdbcDataReader
                        'datos = ConsultaSql("select * from usuario USU inner join descuentos DES on(USU.idDescuento=DES.idDescuento) where cuenta=" & recibo.cuenta & "").ExecuteReader
                        'datos.Read()

                        ''''''Ticket #29
                        'DescRecargo = Decimal.Parse(descuentoRecargos).ToString("C")
                        'subtotal = Decimal.Parse(subtotal).ToString("C")
                        ColDN9 = New PdfPCell(New Phrase("", Font9))
                        ColDN9.Border = 0
                        ColDN9.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table4.AddCell(ColDN9)



                        Dim Table5 As PdfPTable = New PdfPTable(2)
                        Dim Col51 As PdfPCell
                        Dim Col52 As PdfPCell
                        Dim Col53 As PdfPCell
                        Dim Col54 As PdfPCell

                        Table5.WidthPercentage = 100
                        Dim widthsT5 As Single() = New Single() {50.0F, 400.0F}

                        Table5.SetWidths(widthsT5)

                        Col51 = New PdfPCell(New Phrase(" ", Font))
                        Col51.Border = 0
                        Col51.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table5.AddCell(Col51)

                        Col52 = New PdfPCell(New Phrase("", Font))
                        Col52.Border = 0
                        Col52.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table5.AddCell(Col52)

                        Col53 = New PdfPCell(New Phrase(" ", Font9))
                        Col53.Border = 0
                        Col53.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table5.AddCell(Col53)

                        Col54 = New PdfPCell(New Phrase(" ", Font))
                        Col54.Border = 0
                        Col54.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table5.AddCell(Col54)

                        'Encabezado consulta tabla

                        Dim Table6 As PdfPTable = New PdfPTable(5)
                        Dim Col61 As PdfPCell
                        Dim Col62 As PdfPCell
                        Dim Col63 As PdfPCell
                        Dim Col64 As PdfPCell

                        Table6.WidthPercentage = 100
                        Dim widthsT6 As Single() = New Single() {50.0F, 290.0F, 100.0F, 50.0F, 100.0F}
                        Table6.SetWidths(widthsT6)

                        Col61 = New PdfPCell(New Phrase("Cantidad", Font9))
                        Col61.Border = 7
                        Col61.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Col61.BackgroundColor = colordefinido.color
                        Table6.AddCell(Col61)

                        Col62 = New PdfPCell(New Phrase("Concepto", Font9))
                        Col62.Border = 3
                        Col62.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                        Col62.BackgroundColor = colordefinido.color
                        Table6.AddCell(Col62)

                        Col63 = New PdfPCell(New Phrase("Monto", Font9))
                        Col63.Border = 3
                        Col63.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Col63.BackgroundColor = colordefinido.color
                        Table6.AddCell(Col63)

                        Dim ColMi = New PdfPCell(New Phrase("IVA", Font9))
                        ColMi.Border = 3
                        ColMi.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        ColMi.BackgroundColor = colordefinido.color
                        Table6.AddCell(ColMi)


                        Col64 = New PdfPCell(New Phrase("Importe", Font9))
                        Col64.Border = 11
                        Col64.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Col64.BackgroundColor = colordefinido.color
                        Table6.AddCell(Col64)



                        While daatoscontenido.Read()
                            Col61 = New PdfPCell(New Phrase(daatoscontenido("Cantidad"), Font9))
                            Col61.Border = 0
                            Col61.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                            Table6.AddCell(Col61)

                            Col62 = New PdfPCell(New Phrase(daatoscontenido("Concepto"), Font9))
                            Col62.Border = 0
                            Col62.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                            Table6.AddCell(Col62)
                            Dim montox As String = Decimal.Parse(daatoscontenido("Monto")).ToString("C")
                            Col63 = New PdfPCell(New Phrase(montox, Font9))
                            Col63.Border = 0
                            Col63.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                            Table6.AddCell(Col63)
                            Dim ivax As String = Decimal.Parse(daatoscontenido("MontoIVA")).ToString("C")
                            Dim ColMiv = New PdfPCell(New Phrase(ivax, Font9))
                            ColMiv.Border = 0
                            ColMiv.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                            Table6.AddCell(ColMiv)

                            Dim importex As String = Decimal.Parse(daatoscontenido("Importe")).ToString("C")
                            Col64 = New PdfPCell(New Phrase(importex, Font9))
                            Col64.Border = 0
                            Col64.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                            Table6.AddCell(Col64)



                        End While

                        Dim TableEspacio As PdfPTable = New PdfPTable(1)
                        Dim ColEsp As PdfPCell
                        TableEspacio.WidthPercentage = 100
                        Dim widthsTE As Single() = New Single() {1000.0F}
                        TableEspacio.SetWidths(widthsTE)

                        ColEsp = New PdfPCell(New Phrase(" ", Font))
                        ColEsp.Border = 0
                        ColEsp.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        TableEspacio.AddCell(ColEsp)

                        Dim Table7 As PdfPTable = New PdfPTable(4)
                        Dim Col71 As PdfPCell
                        Dim Col72 As PdfPCell
                        Dim Col73 As PdfPCell
                        Dim Col74 As PdfPCell
                        Dim Col75 As PdfPCell
                        Dim Col76 As PdfPCell
                        Dim Col77 As PdfPCell
                        Dim Col78 As PdfPCell

                        Table7.WidthPercentage = 100
                        Dim widthsT7 As Single() = New Single() {200.0F, 500.0F, 150.0F, 150.0F}
                        Table7.SetWidths(widthsT7)

                        Col71 = New PdfPCell(New Phrase(" ", Font))
                        Col71.Border = 0
                        Col71.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table7.AddCell(Col71)

                        Col72 = New PdfPCell(New Phrase("", Font))
                        Col72.Border = 0
                        Col72.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                        Table7.AddCell(Col72)

                        Col73 = New PdfPCell(New Phrase("Subtotal: ", Font))
                        Col73.Border = 0
                        Col73.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table7.AddCell(Col73)


                        subtotal = Decimal.Parse(subtotal).ToString("C")
                        Col74 = New PdfPCell(New Phrase(subtotal, Font))
                        Col74.Border = 1
                        Col74.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table7.AddCell(Col74)
                        Table7.CompleteRow()

                        Col71 = New PdfPCell(New Phrase(" ", Font))
                        Col71.Border = 0
                        Col71.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table7.AddCell(Col71)

                        Col72 = New PdfPCell(New Phrase("", Font))
                        Col72.Border = 0
                        Col72.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table7.AddCell(Col72)

                        Col73 = New PdfPCell(New Phrase("IVA: ", Font))
                        Col73.Border = 0
                        Col73.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table7.AddCell(Col73)


                        ivatotal = Decimal.Parse(ivatotal).ToString("C")
                        Col74 = New PdfPCell(New Phrase(ivatotal, Font))
                        Col74.Border = 0
                        Col74.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table7.AddCell(Col74)

                        Table7.CompleteRow()

                        Col71 = New PdfPCell(New Phrase(" ", Font))
                        Col71.Border = 0
                        Col71.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table7.AddCell(Col71)

                        Col72 = New PdfPCell(New Phrase("", Font))
                        Col72.Border = 0
                        Col72.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table7.AddCell(Col72)

                        Col73 = New PdfPCell(New Phrase("TOTAL: ", Font))
                        Col73.Border = 0
                        Col73.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table7.AddCell(Col73)


                        'Coversión
                        Dim totalfacturax As String = Decimal.Parse(totalfactura).ToString("C")
                        Col74 = New PdfPCell(New Phrase(totalfacturax, Font))
                        Col74.Border = 1
                        Col74.HorizontalAlignment = PdfPCell.ALIGN_RIGHT



                        Table7.AddCell(Col74)

                        Col75 = New PdfPCell(New Phrase(" ", Font))
                        Col75.Border = 0
                        Col75.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        'Table7.
                        Table7.AddCell(Col75)

                        Col76 = New PdfPCell(New Phrase(" ", Font))
                        Col76.Border = 0
                        Col76.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        'Table7.
                        Table7.AddCell(Col76)

                        Col77 = New PdfPCell(New Phrase(" ", Font))
                        Col77.Border = 0
                        Col77.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        'Table7.
                        Table7.AddCell(Col77)

                        Col78 = New PdfPCell(New Phrase(" ", Font))
                        Col78.Border = 0
                        Col78.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        'Table7.
                        Table7.AddCell(Col78)


                        Dim Table8 As PdfPTable = New PdfPTable(1)
                        Dim Col81 As PdfPCell
                        Dim Col82 As PdfPCell
                        Dim Col83 As PdfPCell


                        Table8.WidthPercentage = 100
                        Dim widthsT8 As Single() = New Single() {1000.0F}
                        Table8.SetWidths(widthsT8)

                        Col81 = New PdfPCell(New Phrase(ConvertCurrencyToSpanish(datosfac("TOTAL"), "Pesos"), Font9))
                        Col81.Border = 0
                        Col81.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table8.AddCell(Col81)


                        Col83 = New PdfPCell(New Phrase(" ", Font))
                        Col83.Border = 0
                        Col83.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                        Table8.AddCell(Col83)



                        Dim Table9 As PdfPTable = New PdfPTable(2)
                        Dim Col91 As PdfPCell
                        Dim Col92 As PdfPCell
                        Dim Col93 As PdfPCell
                        Dim Col94 As PdfPCell
                        Dim Col95 As PdfPCell
                        Dim Col96 As PdfPCell
                        Dim Col97 As PdfPCell
                        Dim Col98 As PdfPCell
                        Dim Col99 As PdfPCell
                        Dim Col910 As PdfPCell
                        Dim Col911 As PdfPCell
                        Dim Col912 As PdfPCell


                        Table9.WidthPercentage = 100
                        Dim widthsT9 As Single() = New Single() {500.0F, 100.0F}
                        Table9.SetWidths(widthsT9)

                        'aqui va el codio


                        'Dim TableInT As PdfPTable = New PdfPTable(4)
                        'Dim ColI1 As PdfPCell
                        'Dim ColI2 As PdfPCell
                        'Dim ColI3 As PdfPCell
                        'Dim ColI4 As PdfPCell

                        'TableInT.WidthPercentage = 100
                        'Dim widthsTIT As Single() = New Single() {50.0F, 50.0F, 50.0F, 50.0F}
                        'TableInT.SetWidths(widthsTIT)

                        'ColI1 = New PdfPCell(New Phrase("Periodo", Fontp))
                        'ColI1.Border = 0
                        'ColI1.HorizontalAlignment = PdfPCell.ALIGN_LEFT

                        'ColI2 = New PdfPCell(New Phrase("Lect ant", Fontp))
                        'ColI2.Border = 0
                        'ColI2.HorizontalAlignment = PdfPCell.ALIGN_LEFT

                        'ColI3 = New PdfPCell(New Phrase("Lect act", Fontp))
                        'ColI3.Border = 0
                        'ColI3.HorizontalAlignment = PdfPCell.ALIGN_LEFT

                        'ColI4 = New PdfPCell(New Phrase("Consumo", Fontp))
                        'ColI4.Border = 0
                        'ColI4.HorizontalAlignment = PdfPCell.ALIGN_LEFT

                        'TableInT.AddCell(ColI1)
                        'TableInT.AddCell(ColI2)
                        'TableInT.AddCell(ColI3)
                        'TableInT.AddCell(ColI4)
                        ''Rellenar los campos restantes
                        'TableInT.CompleteRow()

                        '''''

                        'Col91 = New PdfPCell(TableInT)
                        'Col91.Border = 0
                        'Col91.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        'Table9.AddCell(Col91)




                        Dim TableSellos As PdfPTable = New PdfPTable(2)


                        TableSellos.WidthPercentage = 100
                        Dim widthsTIT2 As Single() = New Single() {80.0F, 200.0F}
                        TableSellos.SetWidths(widthsTIT2)






                        Col92 = New PdfPCell(New Phrase("Sello CFDI   ", Font9))
                        Col92.Border = 0
                        Col92.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                        TableSellos.AddCell(Col92)

                        Col93 = New PdfPCell(New Phrase(VARSELLOCFD, Fontp))
                        Col93.Border = 0
                        Col93.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                        TableSellos.AddCell(Col93)

                        Col94 = New PdfPCell(New Phrase(" ", Font))
                        Col94.Border = 0
                        Col94.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                        TableSellos.AddCell(Col94)


                        Col95 = New PdfPCell(New Phrase(" ", Font9))
                        Col95.Border = 0
                        Col95.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        TableSellos.AddCell(Col95)

                        Col96 = New PdfPCell(New Phrase("Sello SAT   ", Font9))
                        Col96.Border = 0
                        Col96.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                        TableSellos.AddCell(Col96)

                        Col97 = New PdfPCell(New Phrase(VARSELLOSAT, Fontp))
                        Col97.Border = 0
                        Col97.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                        TableSellos.AddCell(Col97)

                        Col98 = New PdfPCell(New Phrase(" ", Font))
                        Col98.Border = 0
                        Col98.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                        TableSellos.AddCell(Col98)


                        Col99 = New PdfPCell(New Phrase(" ", Font9))
                        Col99.Border = 0
                        Col99.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        TableSellos.AddCell(Col99)

                        Col910 = New PdfPCell(New Phrase("Cadena Original   ", Font9))
                        Col910.Border = 0
                        'Col910.BackgroundColor()
                        Col910.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                        TableSellos.AddCell(Col910)

                        Col911 = New PdfPCell(New Phrase(CADENAORIGINAL, Fontp))
                        Col911.Border = 0
                        Col911.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                        TableSellos.AddCell(Col911)

                        TableSellos.DefaultCell.Border = BorderStyle.None
                        Table9.DefaultCell.Border = BorderStyle.None
                        Table9.AddCell(TableSellos)




                    'Agregamos el codigo QR al documento
                    Dim codigoQR = New StringBuilder()
                    codigoQR.Append("https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx?id=" & varUUID)
                    codigoQR.Append("&re=" & VARRFCEMISOR) 'RFC del Emisor
                        codigoQR.Append("&rr=" & varRFCRECEPTOR) 'RFC del receptor
                        codigoQR.Append("&tt=" & totalfactura) ' Total del comprobante 10 enteros y 6 decimales
                    codigoQR.Append("&fe=" & VARSELLOCFD.Substring(VARSELLOCFD.Length - 8, 8)) 'UUID del comprobante
                    Dim pdfCodigoQR = New BarcodeQRCode(codigoQR.ToString(), 1, 1, New Dictionary(Of iTextSharp.text.pdf.qrcode.EncodeHintType, System.Object))
                        Dim img As Image = pdfCodigoQR.GetImage()
                        img.SpacingAfter = 0.0F
                        img.SpacingBefore = 0.0F
                        img.BorderWidth = 1.0F
                        img.HasAbsolutePosition()
                        'img.ScalePercent(100, 78)
                        Table9.AddCell(img)
                        'img.Alignment = 6;
                        Table9.CompleteRow()

                        Dim Table10 As PdfPTable = New PdfPTable(1)
                        Dim Col101 As PdfPCell


                        Table10.WidthPercentage = 100
                        Dim widthsT10 As Single() = New Single() {1000.0F}
                        Table10.SetWidths(widthsT10)

                    Col101 = New PdfPCell(New Phrase("ESTE DOCUMENTO ES UNA REPRESENTACION IMPRESA DE UN CFDI 4.0 EFECTOS FISCALES AL PAGO", Font8N))
                    Col101.Border = 0
                    Col101.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                    Table10.AddCell(Col101)


                        Dim Table11 As PdfPTable = New PdfPTable(1)
                        Dim Col111 As PdfPCell


                        Table11.WidthPercentage = 100
                        Dim widthsT11 As Single() = New Single() {1000.0F}
                        Table11.SetWidths(widthsT11)

                    Col111 = New PdfPCell(New Phrase("", Font))
                    Col111.Border = 0
                        Col111.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table11.AddCell(Col111)


                        Dim TableF As PdfPTable = New PdfPTable(3)
                        Dim ColF1 As PdfPCell
                        Dim ColF2 As PdfPCell
                        Dim ColF3 As PdfPCell
                        'Dim ColF4 As PdfPCell

                        TableF.WidthPercentage = 100
                        Dim widthsTF As Single() = New Single() {400.0F, 200.0F, 400.0F}
                        TableF.SetWidths(widthsTF)

                        ColF1 = New PdfPCell(New Phrase(" ", Font9))
                        ColF1.Border = 0
                        ColF1.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        TableF.AddCell(ColF1)

                        ColF2 = New PdfPCell(New Phrase("FIRMA DEL CLIENTE", Fontp))
                        ColF2.Border = 1
                        ColF2.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                        TableF.AddCell(ColF2)


                        'TableF.AddCell(ColF1)


                        ColF3 = New PdfPCell(New Phrase(" ", Font))
                        ColF3.Border = 0
                        ColF3.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                        TableF.AddCell(ColF3)
                        TableF.AddCell(ColF3)


                        Dim TableF2 As PdfPTable = New PdfPTable(2)
                        Dim ColF21 As PdfPCell
                        Dim ColF22 As PdfPCell
                        Dim ColF23 As PdfPCell


                        TableF2.WidthPercentage = 100
                        Dim widthsTF2 As Single() = New Single() {500.0F, 500.0F}
                        TableF2.SetWidths(widthsTF2)

                        ColF21 = New PdfPCell(New Phrase(" ", Font9))
                        ColF21.Border = 0
                        ColF21.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        TableF2.AddCell(ColF21)

                        ColF22 = New PdfPCell(New Phrase(datosfac("usuario"), Fontp))
                        ColF22.Border = 0
                        ColF22.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        TableF2.AddCell(ColF22)



                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



                        'Cargar las tablas
                        pdfDoc.Add(Table1)


                        pdfDoc.Add(Table4)
                        pdfDoc.Add(Table5)
                        pdfDoc.Add(Table6)
                        'pdfDoc.Add(TableEspacio)
                        pdfDoc.Add(Table7)
                        pdfDoc.Add(Table8)
                        pdfDoc.Add(Table9)
                        pdfDoc.Add(TableEspacio)
                        pdfDoc.Add(Table10)
                        pdfDoc.Add(TableEspacio)
                        pdfDoc.Add(TableEspacio)
                        pdfDoc.Add(TableEspacio)
                        pdfDoc.Add(Table11)
                        pdfDoc.Add(TableEspacio)
                        pdfDoc.Add(TableEspacio)
                        pdfDoc.Add(TableEspacio)
                        pdfDoc.Add(TableEspacio)
                        pdfDoc.Add(TableEspacio)
                        pdfDoc.Add(TableF)
                        pdfDoc.Add(TableEspacio)
                        pdfDoc.Add(TableF2)



#End Region

#Region "Visualizar"
                        pdfDoc.Close()
                        Try
                            Dim psi As New ProcessStartInfo(cadenafolder & "\factura" & serie & factura & ".pdf")
                            'psi.WorkingDirectory = cadenapdf & "\facturas\" + nombresespacios

                            psi.WindowStyle = ProcessWindowStyle.Hidden
                            Dim p As Process = Process.Start(psi)
                        Catch ex As Exception
                            MessageBox.Show("Error al visualizar el pdf" & ex.Message)
                        End Try

                        'End If



#End Region


















                    End If ' fin de si leyo las lecturas


                ' CREA EL REPORTE EN PDF 
                Dim cadenapdf As String = ""
                Try

                    cadenapdf = (Application.StartupPath & "\facturas\reimpresas\FACTURA" & serie & factura & ".pdf").Trim


                    ''   ExportToDisk(cadenapdf, reporte)
                    'Try
                    '    Dim psi As New ProcessStartInfo("FACTURA" & serie & factura & ".pdf")
                    '    psi.WorkingDirectory = cadenafolder & ""

                    '    psi.WindowStyle = ProcessWindowStyle.Hidden
                    '    Dim p As Process = Process.Start(psi)
                    'Catch ex As Exception
                    '    MessageBox.Show("error al visualizar el pdf" & ex.Message)
                    'End Try
                    'imprimirpdf(cadenapdf)
                    Try
                        Dim psi As New ProcessStartInfo(cadenapdf)
                        'psi.WorkingDirectory = cadenafolder & "\factura\" + nombresespacios

                        psi.WindowStyle = ProcessWindowStyle.Hidden
                        Dim p As Process = Process.Start(psi)
                    Catch ex As Exception
                        MessageBox.Show("error al visualizar el pdf" & ex.Message)
                    End Try

                Catch ex As Exception
                    MessageBox.Show("error " & ex.Message)
                End Try




            Case "DE PERIODO"








                Dim datosfac As IDataReader
                datosfac = ConsultaSql("select * from encfac where numero=" & factura & " and serie='" & serie & "'").ExecuteReader
                If datosfac.Read Then

                    Dim varUUID As String = String.Empty
                    Dim varTotal As Decimal
                    Dim VARRFCEMISOR As String = String.Empty
                    Dim varRFCRECEPTOR As String = String.Empty
                    Dim varcertificado As String = String.Empty
                    Dim VARSELLOSAT As String = String.Empty
                    Dim VARSELLOCFD As String = String.Empty
                    Dim VARNODECERTIFICADO As String = ""
                    Dim varformapago As String = "01"
                    Dim metodo As String = ""
                    Dim varuso As String = ""
                    Dim direccionEmpresa = obtenerCampo("SELECT CDOMICILIO FROM EMPRESA", "CDOMICILIO")
                    Dim coloniaEmpresa = obtenerCampo("SELECT CCOLONIA FROM EMPRESA", "CCOLONIA")
                    Dim localidadEmpresa = obtenerCampo("SELECT CPOBLACION FROM EMPRESA", "CPOBLACION")
                    Dim estadoEmpresa = obtenerCampo("SELECT CPROVINCIA FROM EMPRESA", "CPROVINCIA")
                    Dim codigoPEmpresa = obtenerCampo("SELECT CCODPOS FROM EMPRESA", "CCODPOS")

                    Try



                        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                            ' guardatxt(FolderBrowserDialog1.SelectedPath, "factura.xml", xml)


                            ' Create or overwrite the file.
                            Dim fs As FileStream = File.Create(FolderBrowserDialog1.SelectedPath & "\FACTURA" & serie & factura & ".XML")

                            ' Add text to the file.
                            Dim info As Byte() = New UTF8Encoding(True).GetBytes(datosfac("CFDI").ToString().TrimStart())
                            fs.Write(info, 0, info.Length)
                            fs.Close()



                            'Dim CADENA() As String = datosfac("respuestaoriginal").ToString().TrimStart().Split("**********")

                            '  Dim pngs As Byte() = System.Convert.FromBase64String(Buscavariable(CADENA, "png"))
                            ' CONSTRIUIMOS LA CADENA
                            CADENAORIGINAL = CONSTRUIRCADENACFDI(FolderBrowserDialog1.SelectedPath & "\FACTURA" & serie & factura & ".XML")

                            ' MANDAMOS CONSTRUIR EL QR 

                            Dim image As System.Drawing.Image = qr(FolderBrowserDialog1.SelectedPath & "\FACTURA" & serie & factura & ".XML")

                            Dim imageConverter As New ImageConverter()
                            Dim pngs As Byte() = DirectCast(imageConverter.ConvertTo(image, GetType(Byte())), Byte())



                            Dim dts As New DatosReciboTableAdapters.cajasTableAdapter
                            dts.UpdateQueryimagen(pngs, My.Settings.caja)

                        End If
                    Catch ex As Exception

                    End Try


                    '''' AQUI
                    Dim cadenafolder As String = FolderBrowserDialog1.SelectedPath

                    Dim v_conceptos As XmlNodeList

                    Dim v_descripccionxml As String

                    Dim varXmlFile As XmlDocument = New XmlDocument()

                    Dim varXmlNsMngr As XmlNamespaceManager = New XmlNamespaceManager(varXmlFile.NameTable)
                    Dim CFDI As String = datosfac("CFDI").ToString().Trim()
                    varXmlFile.LoadXml(CFDI)

                    varXmlNsMngr.AddNamespace("cfdi", "http://www.sat.gob.mx/cfd/3")
                    varXmlNsMngr.AddNamespace("tfd", "http://www.sat.gob.mx/TimbreFiscalDigital")




                    varTotal = varXmlFile.ChildNodes.Item(1).Attributes.GetNamedItem("Total").Value '  varXmlFile.SelectSingleNode("/cfdi:Comprobante/@total", varXmlNsMngr).InnerText
                    VARNODECERTIFICADO = varXmlFile.ChildNodes.Item(1).Attributes.GetNamedItem("NoCertificado").Value 'varXmlFile.SelectSingleNode("/cfdi:Comprobante/@NoCertificado", varXmlNsMngr).InnerText
                    varformapago = varXmlFile.ChildNodes.Item(1).Attributes.GetNamedItem("FormaPago").Value 'varXmlFile.SelectSingleNode("/cfdi:Comprobante/@formapago", varXmlNsMngr).InnerText

                    Dim LISTANODOComplemento As XmlNodeList = varXmlFile.GetElementsByTagName("tfd:TimbreFiscalDigital")

                    For Each xAtt In LISTANODOComplemento
                        varUUID = VarXml(xAtt, "UUID")
                        varcertificado = VarXml(xAtt, "NoCertificadoSAT")
                        VARSELLOSAT = VarXml(xAtt, "SelloSAT")
                        VARSELLOCFD = VarXml(xAtt, "SelloCFD")
                        'strEmisorNombre = VarXml(xAtt, "nombre")
                    Next

                    v_conceptos = varXmlFile.GetElementsByTagName("cfdi:Concepto")

                    Dim LISTANODOSEMISOR As XmlNodeList = varXmlFile.GetElementsByTagName("cfdi:Emisor")
                    For Each xAtt In LISTANODOSEMISOR
                        VARRFCEMISOR = VarXml(xAtt, "Rfc")
                        ' strEmisorNombre = VarXml(xAtt, "nombre")
                    Next
                    Dim LISTANODORECEPTOR As XmlNodeList = varXmlFile.GetElementsByTagName("cfdi:Receptor")
                    For Each xAtt In LISTANODORECEPTOR
                        varRFCRECEPTOR = VarXml(xAtt, "Rfc")
                        ' strEmisorNombre = VarXml(xAtt, "nombre")
                    Next

                    Dim pdfDoc As New Document(iTextSharp.text.PageSize.LETTER, 15.0F, 15.0F, 30.0F, 30.0F)


                    'Dim pdfWrite As PdfWriter = PdfWriter.GetInstance(pdfDoc, New System.IO.FileStream(cadenafolder & "\factura" & serie & factura & ".pdf", FileMode.Create))
                    Dim pdfWrite As PdfWriter = PdfWriter.GetInstance(pdfDoc, New System.IO.FileStream(cadenafolder & "\factura" & serie & factura & ".pdf", FileMode.Create))
                    'Formtos para distintos tamaños de letras

                    'Formato Letras




                    Dim Font As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.NORMAL))
                    Dim Font8N As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.BOLD))
                    Dim Font88 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 15, iTextSharp.text.Font.BOLD))
                    Dim Font12 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 12, iTextSharp.text.Font.BOLD))
                    Dim Font9 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 9, iTextSharp.text.Font.NORMAL))
                    Dim Fontp As New Font(FontFactory.GetFont(FontFactory.COURIER, 7, iTextSharp.text.Font.BOLD))
                    Dim CVacio As PdfPCell = New PdfPCell(New Phrase(""))
                    CVacio.Border = 0








                    'abrimos el pdf para comenzar a escribir en el, al terminar cerramos
                    pdfDoc.Open()




                    ' comenzamos con un cuadro

                    Dim _cb As PdfContentByte

                    Dim colordefinido = New Clscolorreporte()
                    colordefinido.ClsColoresReporte(My.Settings.colorfactura)

                    _cb = pdfWrite.DirectContentUnder
                    _cb.SetColorStroke(colordefinido.color) '/Color de la linea
                    _cb.SetColorFill(colordefinido.color) '/ Color del relleno
                    _cb.SetLineWidth(3.5) ''Tamano de la linea
                    _cb.Rectangle(360, 680, 10, 100)
                    _cb.FillStroke()

                    '''


                    Dim Table1 As PdfPTable = New PdfPTable(3)
                    Table1.DefaultCell.Border = BorderStyle.None
                    Dim Col1 As PdfPCell
                    Dim ILine As Integer
                    Dim iFila As Integer
                    Table1.WidthPercentage = 100

                    Dim widths As Single() = New Single() {100.0F, 300, 280.0F}
                    Table1.SetWidths(widths)

                    'Encabezado

                    '   Dim imagenURL As String = "C:\Users\User\Desktop\huichapan\CAJAS Huichapan 3.3\LogoHuichapan.jpg"




                    Dim imagenBMP As iTextSharp.text.Image
                    imagenBMP = iTextSharp.text.Image.GetInstance(LOGOBYTE)
                    imagenBMP.ScaleToFit(80.0F, 70.0F)
                    'imagenBMP.SpacingBefore = 100.0F
                    'imagenBMP.SpacingAfter = 1000.0F

                    imagenBMP.Border = 0


                    Table1.AddCell(imagenBMP)

                    'Sustituir por los valores reales cuando se pase a frmvalidafactura,vb



                    Dim Tabledireccion As PdfPTable = New PdfPTable(1)
                    Col1 = New PdfPCell(New Phrase(Empresa, Font8N))
                    Col1.Border = 0
                    Col1.HorizontalAlignment = PdfPCell.ALIGN_CENTER

                    Dim DIRECCIONE As String = Direccion & ", " & coloniaEMPRESA & ", " & poblacionEMPRESA & ", " & Estadoempresa
                    Dim Col1d = New PdfPCell(New Phrase(DIRECCIONE, Font8N))
                    Col1d.Border = 0
                    Col1d.HorizontalAlignment = PdfPCell.ALIGN_CENTER


                    Dim Col1rfe = New PdfPCell(New Phrase(RFCORGANISMO, Font8N))
                    Col1rfe.Border = 0
                    Col1rfe.HorizontalAlignment = PdfPCell.ALIGN_CENTER

                    Tabledireccion.AddCell(Col1)
                    Tabledireccion.AddCell(Col1d)
                    Tabledireccion.AddCell(Col1rfe)
                    Table1.AddCell(Tabledireccion)

                    Dim Table2 As PdfPTable = New PdfPTable(2)
                    Dim Col10 As PdfPCell
                    Dim Col11 As PdfPCell
                    Dim Col12 As PdfPCell
                    Dim Col13 As PdfPCell
                    Dim Col14 As PdfPCell
                    Dim Col15 As PdfPCell
                    Dim Col16 As PdfPCell

                    Table2.WidthPercentage = 100
                    Dim widthsT2 As Single() = New Single() {100, 180.0F}
                    Table2.SetWidths(widthsT2)

                    Col10 = New PdfPCell(New Phrase("Serie", Font88))
                    Col10.Border = 0
                    Col10.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    Table2.AddCell(Col10)


                    Col11 = New PdfPCell(New Phrase(serie, Font88))
                    Col11.Border = 0
                    Col11.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    Table2.AddCell(Col11)

                    Dim Col10f = New PdfPCell(New Phrase("Factura", Font88))
                    Col10f.Border = 0
                    Col10f.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    Table2.AddCell(Col10f)


                    Col12 = New PdfPCell(New Phrase(factura, Font88))
                    Col12.Border = 0
                    Col12.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    Table2.AddCell(Col12)

                    Col13 = New PdfPCell(New Phrase("Fecha de inicio:", Font))
                    Col13.Border = 0
                    Col13.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    Table2.AddCell(Col13)

                    Col14 = New PdfPCell(New Phrase(datosfac("FECHATIMBRADO"), Font))
                    Col14.Border = 0
                    Col14.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    Table2.AddCell(Col14)


                    ''''''''''''''''''

                    Dim ColDC1 = New PdfPCell(New Phrase("UUID", Font))
                    ColDC1.Border = 0
                    ColDC1.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    Table2.AddCell(ColDC1)


                    Dim ColDC2 = New PdfPCell(New Phrase(datosfac("UUID"), Font))
                    ColDC2.Border = 0
                    ColDC2.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    Table2.AddCell(ColDC2)

                    Dim ColDC3 = New PdfPCell(New Phrase("Certificado Emisor", Font))
                    ColDC3.Border = 0
                    ColDC3.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    Table2.AddCell(ColDC3)

                    Dim ColDC4 = New PdfPCell(New Phrase(datosfac("NODECERTIFICADO"), Font))
                    ColDC4.Border = 0
                    ColDC4.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    Table2.AddCell(ColDC4)


                    Dim ColDC7 = New PdfPCell(New Phrase("Certificado Sat ", Font))
                    ColDC7.Border = 0
                    ColDC7.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    Table2.AddCell(ColDC7)

                    Dim ColDC8 = New PdfPCell(New Phrase(datosfac("CERTIFICADOSAT"), Font))
                    ColDC8.Border = 0
                    ColDC8.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    Table2.AddCell(ColDC8)



                    Dim ColDC11 = New PdfPCell(New Phrase(" ", Font))
                    ColDC11.Border = 0
                    ColDC11.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    Table2.AddCell(ColDC11)

                    Dim ColDC12 = New PdfPCell(New Phrase(" ", Font))
                    ColDC12.Border = 0
                    ColDC12.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    Table2.AddCell(ColDC12)

                    Table1.AddCell(Table2)


                    Table1.CompleteRow()

                    ''''''''''''''''''''''''

                    'Table Datos Cliente
                    Dim Table4 As PdfPTable = New PdfPTable(2)
                    Dim ColDN As PdfPCell
                    Dim ColDN1 As PdfPCell
                    Dim ColDN2 As PdfPCell
                    Dim ColDN3 As PdfPCell
                    Dim ColDN4 As PdfPCell
                    Dim ColDN5 As PdfPCell
                    Dim ColDN6 As PdfPCell
                    Dim ColDN7 As PdfPCell
                    Dim ColDN8 As PdfPCell
                    Dim ColDN9 As PdfPCell
                    Table4.WidthPercentage = 100
                    Dim widthsT4 As Single() = New Single() {600.0F, 200.0F}
                    Table4.SetWidths(widthsT4)

                    ColDN = New PdfPCell(New Phrase(datosfac("NOMBRE"), Font9))
                    ColDN.Border = 0
                    ColDN.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    Table4.AddCell(ColDN)

                    ColDN1 = New PdfPCell(New Phrase("RFC: " & varRFCRECEPTOR, Font9))
                    ColDN1.Border = 0
                    ColDN1.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    Table4.AddCell(ColDN1)

                    ColDN2 = New PdfPCell(New Phrase(direccionEmpresa, Font9))
                    ColDN2.Border = 0
                    ColDN2.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    Table4.AddCell(ColDN2)

                    ColDN3 = New PdfPCell(New Phrase(" ", Font))
                    ColDN3.Border = 0
                    ColDN3.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    Table4.AddCell(ColDN3)

                    ColDN4 = New PdfPCell(New Phrase(coloniaEmpresa, Font9))
                    ColDN4.Border = 0
                    ColDN4.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    Table4.AddCell(ColDN4)

                    ColDN5 = New PdfPCell(New Phrase("Tipo: Factura de Periodo", Font))
                    ColDN5.Border = 0
                    ColDN5.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    Table4.AddCell(ColDN5)

                    ColDN6 = New PdfPCell(New Phrase($"{localidadEmpresa}, {estadoEmpresa}, {codigoPEmpresa}", Font9))
                    ColDN6.Border = 0
                    ColDN6.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    Table4.AddCell(ColDN6)

                    ColDN7 = New PdfPCell(New Phrase(" ", Font))
                    ColDN7.Border = 0
                    ColDN7.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    Table4.AddCell(ColDN7)

                    ColDN8 = New PdfPCell(New Phrase(" ", Font9))
                    ColDN8.Border = 0
                    ColDN8.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    Table4.AddCell(ColDN8)

                    ColDN9 = New PdfPCell(New Phrase(" ", Font))
                    ColDN9.Border = 0
                    ColDN9.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    Table4.AddCell(ColDN9)




                    Dim Table5 As PdfPTable = New PdfPTable(1)
                    Dim Col51 As PdfPCell
                    Dim Col52 As PdfPCell

                    Table5.WidthPercentage = 100
                    Dim widthsT5 As Single() = New Single() {1000.0F}

                    Table5.SetWidths(widthsT5)

                    Col51 = New PdfPCell(New Phrase(" ", Font))
                    Col51.Border = 0
                    Col51.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    Table5.AddCell(Col51)

                    Col52 = New PdfPCell(New Phrase(datosfac("OBSERVACION"), Font))
                    Col52.Border = 0
                    Col52.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    Table5.AddCell(Col52)






                    'Encabezado consulta tabla

                    Dim Table6 As PdfPTable = New PdfPTable(4)
                    Dim Col61 As PdfPCell
                    Dim Col62 As PdfPCell
                    Dim Col63 As PdfPCell
                    Dim Col64 As PdfPCell

                    Table6.WidthPercentage = 100
                    Dim widthsT6 As Single() = New Single() {50.0F, 340.0F, 100.0F, 100.0F}
                    Table6.SetWidths(widthsT6)

                    Col61 = New PdfPCell(New Phrase("Cantidad", Font9))
                    Col61.Border = 7
                    Col61.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    Col61.BackgroundColor = colordefinido.color
                    Table6.AddCell(Col61)

                    Col62 = New PdfPCell(New Phrase("Concepto", Font9))
                    Col62.Border = 3
                    Col62.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                    Col62.BackgroundColor = colordefinido.color
                    Table6.AddCell(Col62)

                    Col63 = New PdfPCell(New Phrase("Monto", Font9))
                    Col63.Border = 3
                    Col63.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    Col63.BackgroundColor = colordefinido.color
                    Table6.AddCell(Col63)


                    Col64 = New PdfPCell(New Phrase("Importe", Font9))
                    Col64.Border = 11
                    Col64.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    Col64.BackgroundColor = colordefinido.color
                    Table6.AddCell(Col64)



                    For Each node As XmlNode In v_conceptos
                        Dim cantidadXml As String = ""
                        Dim descripcionXml As String = ""
                        Dim valorUnitarioXml As Decimal = 0.0
                        Dim importeXml As Decimal = 0.0


                        cantidadXml = VarXml(node, "Cantidad")
                        Col61 = New PdfPCell(New Phrase(cantidadXml, Font9))
                        Col61.Border = 0
                        Col61.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table6.AddCell(Col61)

                        descripcionXml = VarXml(node, "Descripcion")
                        Col62 = New PdfPCell(New Phrase(descripcionXml, Font9))
                        Col62.Border = 0
                        Col62.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table6.AddCell(Col62)

                        valorUnitarioXml = VarXml(node, "ValorUnitario")
                        Dim montox As String = Decimal.Parse(valorUnitarioXml).ToString("C")
                        Col63 = New PdfPCell(New Phrase(montox, Font9))
                        Col63.Border = 0
                        Col63.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table6.AddCell(Col63)

                        importeXml = VarXml(node, "Importe")
                        Dim importex As String = Decimal.Parse(importeXml).ToString("C")
                        Col64 = New PdfPCell(New Phrase(importex, Font9))
                        Col64.Border = 0
                        Col64.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table6.AddCell(Col64)



                    Next


                    Dim TableEspacio As PdfPTable = New PdfPTable(1)
                    Dim ColEsp As PdfPCell
                    TableEspacio.WidthPercentage = 100
                    Dim widthsTE As Single() = New Single() {1000.0F}
                    TableEspacio.SetWidths(widthsTE)

                    ColEsp = New PdfPCell(New Phrase(" ", Font))
                    ColEsp.Border = 0
                    ColEsp.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    TableEspacio.AddCell(ColEsp)

                    Dim Table7 As PdfPTable = New PdfPTable(4)
                    Dim Col71 As PdfPCell
                    Dim Col72 As PdfPCell
                    Dim Col73 As PdfPCell
                    Dim Col74 As PdfPCell
                    Dim Col75 As PdfPCell
                    Dim Col76 As PdfPCell
                    Dim Col77 As PdfPCell
                    Dim Col78 As PdfPCell

                    Table7.WidthPercentage = 100
                    Dim widthsT7 As Single() = New Single() {200.0F, 500.0F, 150.0F, 150.0F}
                    Table7.SetWidths(widthsT7)

                    Col71 = New PdfPCell(New Phrase(" ", Font))
                    Col71.Border = 0
                    Col71.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    Table7.AddCell(Col71)

                    Col72 = New PdfPCell(New Phrase("", Font))
                    Col72.Border = 0
                    Col72.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                    Table7.AddCell(Col72)

                    Col73 = New PdfPCell(New Phrase("SUBTOTAL: ", Font))
                    Col73.Border = 0
                    Col73.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    Table7.AddCell(Col73)


                    subtotal = Decimal.Parse(datosfac("SUBTOTAL")).ToString("C")
                    Col74 = New PdfPCell(New Phrase(subtotal, Font))
                    Col74.Border = 1
                    Col74.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    Table7.AddCell(Col74)
                    Table7.CompleteRow()

                    Col71 = New PdfPCell(New Phrase(" ", Font))
                    Col71.Border = 0
                    Col71.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    Table7.AddCell(Col71)

                    Col72 = New PdfPCell(New Phrase("", Font))
                    Col72.Border = 0
                    Col72.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    Table7.AddCell(Col72)

                    Col73 = New PdfPCell(New Phrase("IVA: ", Font))
                    Col73.Border = 0
                    Col73.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    Table7.AddCell(Col73)


                    iva = Decimal.Parse(datosfac("IVA")).ToString("C")
                    Col74 = New PdfPCell(New Phrase(iva, Font))
                    Col74.Border = 0
                    Col74.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    Table7.AddCell(Col74)

                    Table7.CompleteRow()

                    Col71 = New PdfPCell(New Phrase(" ", Font))
                    Col71.Border = 0
                    Col71.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    Table7.AddCell(Col71)

                    Col72 = New PdfPCell(New Phrase("", Font))
                    Col72.Border = 0
                    Col72.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    Table7.AddCell(Col72)

                    Col73 = New PdfPCell(New Phrase("TOTAL: ", Font))
                    Col73.Border = 0
                    Col73.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    Table7.AddCell(Col73)


                    'Coversión
                    Dim totalfacturax As String = Decimal.Parse(datosfac("TOTAL")).ToString("C")
                    Col74 = New PdfPCell(New Phrase(totalfacturax, Font))
                    Col74.Border = 1
                    Col74.HorizontalAlignment = PdfPCell.ALIGN_RIGHT



                    Table7.AddCell(Col74)

                    Col75 = New PdfPCell(New Phrase(" ", Font))
                    Col75.Border = 0
                    Col75.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    'Table7.
                    Table7.AddCell(Col75)

                    Col76 = New PdfPCell(New Phrase(" ", Font))
                    Col76.Border = 0
                    Col76.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    'Table7.
                    Table7.AddCell(Col76)

                    Col77 = New PdfPCell(New Phrase(" ", Font))
                    Col77.Border = 0
                    Col77.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    'Table7.
                    Table7.AddCell(Col77)

                    Col78 = New PdfPCell(New Phrase(" ", Font))
                    Col78.Border = 0
                    Col78.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    'Table7.
                    Table7.AddCell(Col78)


                    Dim Table8 As PdfPTable = New PdfPTable(1)
                    Dim Col81 As PdfPCell
                    Dim Col82 As PdfPCell
                    Dim Col83 As PdfPCell


                    Table8.WidthPercentage = 100
                    Dim widthsT8 As Single() = New Single() {1000.0F}
                    Table8.SetWidths(widthsT8)

                    Col81 = New PdfPCell(New Phrase(ConvertCurrencyToSpanish(totalfacturax, "Pesos"), Font9))
                    Col81.Border = 0
                    Col81.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    Table8.AddCell(Col81)


                    Col83 = New PdfPCell(New Phrase(" ", Font))
                    Col83.Border = 0
                    Col83.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                    Table8.AddCell(Col83)

                    Table8.CompleteRow()

                    Dim Table9 As PdfPTable = New PdfPTable(2)
                    Dim Col91 As PdfPCell
                    Dim Col92 As PdfPCell


                    Table9.WidthPercentage = 100
                    Dim widthsT9 As Single() = New Single() {800.0F, 200.0F}
                    Table9.SetWidths(widthsT9)


                    Dim TableSellos As PdfPTable = New PdfPTable(2)


                    TableSellos.WidthPercentage = 100
                    Dim widthsTIT2 As Single() = New Single() {200.0F, 500.0F}
                    TableSellos.SetWidths(widthsTIT2)


                    Dim ColSell1 As PdfPCell
                    Dim ColSell2 As PdfPCell
                    Dim ColSell3 As PdfPCell
                    Dim ColSell4 As PdfPCell
                    Dim ColSell5 As PdfPCell
                    Dim ColSell6 As PdfPCell




                    ColSell1 = New PdfPCell(New Phrase("Sello CFDI   ", Font9))
                    ColSell1.Border = 0
                    ColSell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                    TableSellos.AddCell(ColSell1)

                    ColSell2 = New PdfPCell(New Phrase(datosfac("SELLO"), Fontp))
                    ColSell2.Border = 0
                    ColSell2.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                    TableSellos.AddCell(ColSell2)

                    ColSell3 = New PdfPCell(New Phrase("Sello SAT   ", Font9))
                    ColSell3.Border = 0
                    ColSell3.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                    TableSellos.AddCell(ColSell3)

                    ColSell4 = New PdfPCell(New Phrase(datosfac("SELLOSAT"), Fontp))
                    ColSell4.Border = 0
                    ColSell4.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                    TableSellos.AddCell(ColSell4)

                    ColSell5 = New PdfPCell(New Phrase("Cadena Original   ", Font9))
                    ColSell5.Border = 0
                    ColSell5.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                    TableSellos.AddCell(ColSell5)

                    ColSell6 = New PdfPCell(New Phrase(datosfac("CADENA"), Fontp))
                    ColSell6.Border = 0
                    ColSell6.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                    TableSellos.AddCell(ColSell6)

                    TableSellos.DefaultCell.Border = BorderStyle.None
                    Table9.DefaultCell.Border = BorderStyle.None

                    Col91 = New PdfPCell(TableSellos)
                    Col91.Border = 0
                    Col91.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    Table9.AddCell(Col91)




                    ''' aqui el qr
                    ''' 

                    'Agregamos el codigo QR al documento
                    Dim codigoQR = New StringBuilder()
                    codigoQR.Append("https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx?id=" & varUUID)
                    codigoQR.Append("&re=" & VARRFCEMISOR) 'RFC del Emisor
                    codigoQR.Append("&rr=" & varRFCRECEPTOR) 'RFC del receptor
                    codigoQR.Append("&tt=" & varTotal) ' Total del comprobante 10 enteros y 6 decimales
                    codigoQR.Append("&fe=" & datosfac("SELLO").Substring(datosfac("SELLO").Length - 8, 8)) 'UUID del comprobante
                    Dim pdfCodigoQR = New BarcodeQRCode(codigoQR.ToString(), 1, 1, New Dictionary(Of iTextSharp.text.pdf.qrcode.EncodeHintType, System.Object))
                    Dim img As Image = pdfCodigoQR.GetImage()
                    img.SpacingAfter = 0.0F
                    img.SpacingBefore = 0.0F
                    img.BorderWidth = 1.0F
                    img.HasAbsolutePosition()

                    Table9.AddCell(img)

                    Table9.CompleteRow()

                    pdfDoc.Add(Table1)


                    pdfDoc.Add(Table4)
                    pdfDoc.Add(Table5)
                    pdfDoc.Add(TableEspacio)
                    pdfDoc.Add(TableEspacio)
                    pdfDoc.Add(Table6)
                    pdfDoc.Add(Table7)
                    pdfDoc.Add(Table8)
                    pdfDoc.Add(TableEspacio)
                    pdfDoc.Add(TableEspacio)
                    pdfDoc.Add(TableEspacio)
                    pdfDoc.Add(Table9)
                    pdfDoc.Add(TableEspacio)
                    pdfDoc.Add(TableEspacio)
                    pdfDoc.Add(TableEspacio)
                    pdfDoc.Add(TableEspacio)


                    pdfDoc.Close()


                    Try
                        Dim psi As New ProcessStartInfo(FolderBrowserDialog1.SelectedPath & "\factura" & serie & factura & ".pdf")
                        'psi.WorkingDirectory = cadenapdf & "\facturas\" + nombresespacios

                        psi.WindowStyle = ProcessWindowStyle.Hidden
                        Dim p As Process = Process.Start(psi)
                    Catch ex As Exception
                        MessageBox.Show("Error al visualizar el pdf" & ex.Message)
                    End Try
                End If ' si tiene red




                ''''''HASTA ACA




        End Select
        desconectar()
    End Sub


    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            For i = 0 To dataGridView1.Rows.Count - 1
                Dim factura As Int32 = dataGridView1.Item("numero", i).Value.ToString()
                Dim serie As String = dataGridView1.Item("serie", i).Value.ToString()
                Dim tipo As String = dataGridView1.Item("ES", i).Value.ToString()
                recibo = dataGridView1.Item("recibo", i).Value.ToString()
                Dim serierecibo As String = dataGridView1.Item("serierecibo", i).Value.ToString()
                '  Dim cuenta As Long = dataGridView1.Item("cuenta", dataGridView1.CurrentRow.Index).Value
                conectar()
                Dim CADENAORIGINAL As String = String.Empty
                Select Case tipo.ToUpper
                    Case "USUARIO", "NO USUARIO", "FACTIBILIDAD"

                        Dim reader As XmlTextReader
                        Dim nombre As String = dataGridView1.Item("nombre", dataGridView1.CurrentRow.Index).Value.ToString()
                        'Dim es As String = dataGridView1.Item("es", dataGridView1.CurrentRow.Index).Value.ToString()
                        Dim nombresespacios As String = nombre.Replace(" ", "")
                        'Dim bas As New base
                        Dim datosfac As IDataReader
                        Dim datosrecibo As IDataReader
                        Dim cuenta As String = obtenerCampo("select cuenta from pagos where recibo=" & recibo & " and serie='" & serierecibo & "'", "cuenta")
                        datosrecibo = ConsultaSql("select * from datosfiscales where datosfiscales.cuenta=" & cuenta & " and datosfiscales.tipo='" & tipo & "'").ExecuteReader
                        datosrecibo.Read()
                        datosfac = ConsultaSql("select * from encfac where numero=" & factura & " and serie='" & serie & "'").ExecuteReader
                        If datosfac.Read Then
                            Dim cadenafolder As String = FolderBrowserDialog1.SelectedPath ' "\facturas\reimpresas\"
                            Dim varUUID As String = String.Empty
                            Dim varTotal As Decimal
                            Dim VARRFCEMISOR As String = String.Empty
                            Dim varRFCRECEPTOR As String = String.Empty
                            Dim varcertificado As String = String.Empty
                            Dim VARSELLOSAT As String = String.Empty
                            Dim VARSELLOCFD As String = String.Empty
                            Dim VARNODECERTIFICADO As String = String.Empty
                            Dim varformapago As String = "01"
                            Try
                                '    If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                                If serie = " " Then serie = ""
                                Dim fs As FileStream = File.Create((cadenafolder & "\FACTURA" & serie & factura & ".XML").Trim)
                                ' Add text to the file.
                                Dim info As Byte() = New UTF8Encoding(True).GetBytes(datosfac("CFDI").ToString().TrimStart())
                                fs.Write(info, 0, info.Length)
                                fs.Close()
                                ' CONSTRIUIMOS LA CADENA
                                CADENAORIGINAL = datosfac("cadena") 'CONSTRUIRCADENACFDI((cadenafolder & "\FACTURA" & serie & factura & ".XML").Trim)
                                ' MANDAMOS CONSTRUIR EL QR 
                                ' LEEMOS EL XML
                                Dim image As System.Drawing.Image = qrdatos(varUUID, varTotal, VARRFCEMISOR, varRFCRECEPTOR, varcertificado)
                                Dim imageConverter As New ImageConverter()
                                Dim pngs As Byte() = DirectCast(imageConverter.ConvertTo(image, GetType(Byte())), Byte())
                                Dim dts As New DatosReciboTableAdapters.cajasTableAdapter
                                dts.UpdateQueryimagen(pngs, My.Settings.caja)
                                ' End If
                            Catch ex As Exception
                            End Try

                            If datosfac("version") = "4.0" Then
                                Dim varXmlFile As XmlDocument = New XmlDocument()
                                Dim varXmlNsMngr As XmlNamespaceManager = New XmlNamespaceManager(varXmlFile.NameTable)
                                varXmlFile.Load((cadenafolder & "\FACTURA" & serie & factura & ".XML").Trim)
                                varXmlNsMngr.AddNamespace("cfdi", "http://www.sat.gob.mx/cfd/3")
                                varXmlNsMngr.AddNamespace("tfd", "http://www.sat.gob.mx/TimbreFiscalDigital")
                                varTotal = varXmlFile.ChildNodes.Item(1).Attributes.Item(14).Value '  varXmlFile.SelectSingleNode("/cfdi:Comprobante/@total", varXmlNsMngr).InnerText
                                VARNODECERTIFICADO = varXmlFile.ChildNodes.Item(1).Attributes.Item(11).Value 'varXmlFile.SelectSingleNode("/cfdi:Comprobante/@NoCertificado", varXmlNsMngr).InnerText
                                varformapago = varXmlFile.ChildNodes.Item(1).Attributes.Item(15).Value 'varXmlFile.SelectSingleNode("/cfdi:Comprobante/@formapago", varXmlNsMngr).InnerText

                                Dim LISTANODOComplemento As XmlNodeList = varXmlFile.GetElementsByTagName("tfd:TimbreFiscalDigital")

                                For Each xAtt In LISTANODOComplemento
                                    varUUID = VarXml(xAtt, "UUID")
                                    varcertificado = VarXml(xAtt, "NoCertificadoSAT")
                                    VARSELLOSAT = VarXml(xAtt, "SelloSAT")
                                    VARSELLOCFD = VarXml(xAtt, "SelloCFD")
                                    'strEmisorNombre = VarXml(xAtt, "nombre")
                                Next

                                'varUUID = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@UUID", varXmlNsMngr).InnerText
                                'varcertificado = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@noCertificadoSAT", varXmlNsMngr).InnerText
                                'VARSELLOSAT = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@selloSAT", varXmlNsMngr).InnerText
                                'VARSELLOCFD = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@selloCFD", varXmlNsMngr).InnerText
                                Dim LISTANODOSEMISOR As XmlNodeList = varXmlFile.GetElementsByTagName("cfdi:Emisor")
                                For Each xAtt In LISTANODOSEMISOR
                                    VARRFCEMISOR = VarXml(xAtt, "rfc")
                                    ' strEmisorNombre = VarXml(xAtt, "nombre")
                                Next
                                Dim LISTANODORECEPTOR As XmlNodeList = varXmlFile.GetElementsByTagName("cfdi:Receptor")
                                For Each xAtt In LISTANODORECEPTOR
                                    varRFCRECEPTOR = VarXml(xAtt, "rfc")
                                    ' strEmisorNombre = VarXml(xAtt, "nombre")
                                Next
                                'reporte.Load(AppPath() & "\REPORTES\FACTURA.RPT")
                                'Dim servidorreporte As String = My.Settings.servidorreporte
                                'Dim usuarioreporte As String = My.Settings.usuarioreporte
                                'Dim passreporte As String = My.Settings.passreporte
                                'Dim basereporte As String = My.Settings.basereporte
                                'reporte.DataSourceConnections.Item(0).SetConnection(servidorreporte, basereporte, False)
                                'reporte.DataSourceConnections.Item(0).SetLogon(usuarioreporte, passreporte)
                                'reporte.SetParameterValue("nombre", datosfac("NOMBRE"))
                                'reporte.SetParameterValue("Direccion", datosrecibo("calle") & " " & datosrecibo("numext") & " " & datosrecibo("numint"))
                                'reporte.DataDefinition.FormulaFields("colonia").Text = "'" & datosrecibo("colonia") & "'"
                                'reporte.DataDefinition.FormulaFields("ciudad").Text = "'" & datosrecibo("poblacion") & " " & datosrecibo("delegacion") & " " & datosrecibo("estado") & " CP " & datosrecibo("cp") & "'"
                                'reporte.SetParameterValue("fechatimbrado", datosfac("FECHA"))
                                'reporte.SetParameterValue("certificado", datosfac("NODECERTIFICADO"))
                                'reporte.SetParameterValue("cantidadconletra", ConvertCurrencyToSpanish(datosfac("TOTAL"), "Pesos"))
                                'reporte.SetParameterValue("formadepago", varformapago)
                                'reporte.SetParameterValue("Cadenaoriginal", CADENAORIGINAL)
                                'reporte.SetParameterValue("foliofiscal", datosfac("SERIE") & datosfac("NUMERO"))
                                'reporte.SetParameterValue("RFCCLIENTE", varRFCRECEPTOR)
                                'reporte.SetParameterValue("CERTIFICADOSAT", datosfac("CERTIFICADOSAT"))
                                'reporte.SetParameterValue("nota", "")
                                'reporte.SetParameterValue("SerieCertificado", VARNODECERTIFICADO)
                                'reporte.SetParameterValue("Sellodigital", VARSELLOSAT)
                                'reporte.SetParameterValue("SelloCFDI", VARSELLOCFD)
                                'reporte.SetParameterValue("UUID", datosfac("UUID"))
                                'reporte.SetParameterValue("Medidor", "")
                                'reporte.SetParameterValue("Promedio", "")
                                'reporte.RecordSelectionFormula = "{cajas1.ID_CAJA}='" & My.Settings.caja & "' and {pagos1.recibo}=" & recibo & " and {pagos1.serie}='" & serierecibo & "'"


                            End If
                        End If ' fin de si leyo las lecturas
                        ' CREA EL REPORTE EN PDF 
                        Dim cadenapdf As String = ""
                        'Try
                        '    cadenapdf = (FolderBrowserDialog1.SelectedPath & "\ factura" & serie & factura & ".pdf").Trim
                        '    ExportToDisk(cadenapdf, reporte)
                        '    'Try
                        '    '    Dim psi As New ProcessStartInfo("FACTURA" & serie & factura & ".pdf")
                        '    '    psi.WorkingDirectory = cadenafolder & ""
                        '    '    psi.WindowStyle = ProcessWindowStyle.Hidden
                        '    '    Dim p As Process = Process.Start(psi)
                        '    'Catch ex As Exception
                        '    '    MessageBox.Show("error al visualizar el pdf" & ex.Message)
                        '    'End Try
                        '    '   imprimirpdf(cadenapdf)
                        'Catch ex As Exception
                        '    MessageBox.Show("error " & ex.Message)
                        'End Try
                    Case "DE PERIODO"
                        'Dim reporte As New ReportDocument()
                        'Dim datosfac As IDataReader
                        'datosfac = ConsultaSql("select * from encfac where numero=" & factura & " and serie='" & serie & "'").ExecuteReader
                        'If datosfac.Read Then
                        '    Try
                        '        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                        '            ' guardatxt(FolderBrowserDialog1.SelectedPath, "factura.xml", xml)
                        '            ' Create or overwrite the file.
                        '            Dim fs As FileStream = File.Create(FolderBrowserDialog1.SelectedPath & "\FACTURA" & serie & factura & ".XML")
                        '            ' Add text to the file.
                        '            Dim info As Byte() = New UTF8Encoding(True).GetBytes(datosfac("CFDI").ToString().TrimStart())
                        '            fs.Write(info, 0, info.Length)
                        '            fs.Close()
                        '            'Dim CADENA() As String = datosfac("respuestaoriginal").ToString().TrimStart().Split("**********")
                        '            '  Dim pngs As Byte() = System.Convert.FromBase64String(Buscavariable(CADENA, "png"))
                        '            ' CONSTRIUIMOS LA CADENA
                        '            CADENAORIGINAL = CONSTRUIRCADENACFDI(FolderBrowserDialog1.SelectedPath & "\FACTURA" & serie & factura & ".XML")
                        '            ' MANDAMOS CONSTRUIR EL QR 
                        '            Dim image As System.Drawing.Image = qr(FolderBrowserDialog1.SelectedPath & "\FACTURA" & serie & factura & ".XML")
                        '            Dim imageConverter As New ImageConverter()
                        '            Dim pngs As Byte() = DirectCast(imageConverter.ConvertTo(image, GetType(Byte())), Byte())
                        '            Dim dts As New DatosReciboTableAdapters.cajasTableAdapter
                        '            dts.UpdateQueryimagen(pngs, My.Settings.caja)
                        '        End If
                        '    Catch ex As Exception
                        '    End Try
                        '    reporte.Load(AppPath() & "\REPORTES\FACTURADIA.RPT")
                        '    Dim servidorreporte As String = My.Settings.servidorreporte
                        '    Dim usuarioreporte As String = My.Settings.usuarioreporte
                        '    Dim passreporte As String = My.Settings.passreporte
                        '    Dim basereporte As String = My.Settings.basereporte
                        '    reporte.DataSourceConnections.Item(0).SetConnection(servidorreporte, basereporte, False)
                        '    reporte.DataSourceConnections.Item(0).SetLogon(usuarioreporte, passreporte)
                        '    Dim dataSet As DataSet = New DataSet
                        '    dataSet.ReadXml(FolderBrowserDialog1.SelectedPath & "\FACTURA" & serie & factura & ".XML", XmlReadMode.Auto)
                        '    reporte.SetDataSource(dataSet)
                        '    Try
                        '        reporte.SetParameterValue("cantidadconletra", ConvertCurrencyToSpanish(datosfac("total"), "Pesos"))
                        '        reporte.SetParameterValue("nota", datosfac("observacion").ToString())
                        '        reporte.SetParameterValue("CADENAORIGINAL", CADENAORIGINAL)
                        '        reporte.RecordSelectionFormula = "{cajas1.ID_CAJA}='" & My.Settings.caja & "'"
                        '    Catch ex As Exception
                        '        MessageBox.Show(ex.Message)
                        '    End Try
                        '    ' CREA EL REPORTE EN PDF 
                        '    Dim cadenapdf As String = ""
                        '    Try
                        '        cadenapdf = FolderBrowserDialog1.SelectedPath & "\FACTURA" & serie & factura & ".PDF"
                        '        ExportToDisk(cadenapdf, reporte)
                        '        'Try
                        '        '    Dim psi As New ProcessStartInfo("FACTURA" & serie & factura & ".PDF")
                        '        '    psi.WorkingDirectory = FolderBrowserDialog1.SelectedPath & ""
                        '        '    psi.WindowStyle = ProcessWindowStyle.Hidden
                        '        '    Dim p As Process = Process.Start(psi)
                        '        'Catch ex As Exception
                        '        '    MessageBox.Show("error al visualizar el pdf" & ex.Message)
                        '        'End Try
                        '        reporte.Dispose()
                        '    Catch ex As Exception
                        '    End Try
                        '    'cadenapdf = Application.StartupPath & "\facturas\" + nombresespacios & "\PdfFactura" & seriefactura & foliofactura & ".pdf"
                        'End If
                End Select
            Next i
            MessageBox.Show("termine")
        End If
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Dim factura As Int32 = dataGridView1.Item("numero", dataGridView1.CurrentRow.Index).Value
        Dim serie As String = dataGridView1.Item("serie", dataGridView1.CurrentRow.Index).Value.ToString()
        Dim Nombre As String = dataGridView1.Item("nombre", dataGridView1.CurrentRow.Index).Value.ToString()
        Dim total As Decimal = Decimal.Parse(dataGridView1.Item("Total", dataGridView1.CurrentRow.Index).Value.ToString())
        Dim uuid As String = dataGridView1.Item("UUID", dataGridView1.CurrentRow.Index).Value.ToString()
        Dim tipo As String = dataGridView1.Item("ES", dataGridView1.CurrentRow.Index).Value.ToString()
        Dim recibo As Integer = dataGridView1.Item("recibo", dataGridView1.CurrentRow.Index).Value.ToString()
        Dim serierecibo As String = dataGridView1.Item("serierecibo", dataGridView1.CurrentRow.Index).Value.ToString()
        '  Dim cuenta As Long = dataGridView1.Item("cuenta", dataGridView1.CurrentRow.Index).Value


    End Sub

    Private Sub toolStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles toolStrip1.ItemClicked

    End Sub

    Private Sub btnmail_Click(sender As Object, e As EventArgs) Handles btnmail.Click
        Dim factura As Int32 = dataGridView1.Item("numero", dataGridView1.CurrentRow.Index).Value
        Dim serie As String = dataGridView1.Item("serie", dataGridView1.CurrentRow.Index).Value.ToString()
        Dim tipo As String = dataGridView1.Item("ES", dataGridView1.CurrentRow.Index).Value.ToString()
        Dim FECHAFAC As String = dataGridView1.Item("fecha", dataGridView1.CurrentRow.Index).Value.ToString()
        recibo = dataGridView1.Item("recibo", dataGridView1.CurrentRow.Index).Value.ToString()
        Dim serierecibo As String = dataGridView1.Item("serierecibo", dataGridView1.CurrentRow.Index).Value.ToString()
        Dim subtotal As String = dataGridView1.Item("Subtotal", dataGridView1.CurrentRow.Index).Value.ToString()
        Dim ivatotal As String = dataGridView1.Item("IVA", dataGridView1.CurrentRow.Index).Value.ToString()
        Dim totalfactura As String = dataGridView1.Item("Total", dataGridView1.CurrentRow.Index).Value.ToString()

        '  Dim cuenta As Long = dataGridView1.Item("cuenta", dataGridView1.CurrentRow.Index).Value
        conectar()



        If Not My.Computer.FileSystem.DirectoryExists(Application.StartupPath & "\facturas\reimpresas\") Then

            My.Computer.FileSystem.CreateDirectory(Application.StartupPath & "\facturas\reimpresas\")
        End If

        'If Not My.Computer.FileSystem.DirectoryExists(Environment.SpecialFolder.MyDocuments) & "\facturas\reimpresas\") Then

        '    My.Computer.FileSystem.CreateDirectory(Environment.SpecialFolder.MyDocuments) & "\facturas\reimpresas\")
        'End If

        Dim directorioreimpresas = (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\facturas\reimpresas").Trim

        If Not Directory.Exists(directorioreimpresas) Then
            Directory.CreateDirectory(directorioreimpresas)
        End If

        Dim CADENAORIGINAL As String = String.Empty
        If tipo.ToUpper = "CLIENTE" Then tipo = "NO USUARIO"
        Select Case tipo.ToUpper()
            Case "USUARIO", "NO USUARIO", "FACTIBILIDAD"
                'Dim reporte As New ReportDocument()




                Dim nombre As String = dataGridView1.Item("nombre", dataGridView1.CurrentRow.Index).Value.ToString()

                Dim nombresespacios As String = nombre.Replace(" ", "")

                'Dim bas As New base
                Dim datosfac As IDataReader
                Dim datosrecibo As IDataReader
                Dim daatoscontenido As IDataReader

                Dim cuenta As String = obtenerCampo("select cuenta from pagos where recibo=" & recibo & " and serie='" & serierecibo & "'", "cuenta")
                datosrecibo = ConsultaSql("select * from datosfiscales where datosfiscales.cuenta=" & cuenta & " and datosfiscales.tipo='" & tipo & "'").ExecuteReader


                datosrecibo.Read()

                daatoscontenido = ConsultaSql("select * from pagotros where recibo=" & recibo & " and serie='" & serierecibo & "'").ExecuteReader

                'Dim descuentoRecargos As String = obtenerCampo("select sum(descuento) as descuento_r from pago_mes where recibo = " & recibo.NUMERO & " and serie = '" & seriedelreciboqueseestafacturando & "' and concepto = 'Recargo'", "descuento_r")

                datosfac = ConsultaSql("select * from encfac where numero=" & factura & " and serie='" & serie & "'").ExecuteReader
                If datosfac.Read Then
                    Dim cadenafolder As String = Application.StartupPath & "\facturas\reimpresas\"
                    Dim varUUID As String = String.Empty
                    Dim varTotal As Decimal
                    Dim VARRFCEMISOR As String = String.Empty
                    Dim varRFCRECEPTOR As String = String.Empty
                    Dim varcertificado As String = String.Empty
                    Dim VARSELLOSAT As String = String.Empty
                    Dim VARSELLOCFD As String = String.Empty
                    Dim VARNODECERTIFICADO As String = ""
                    Dim varformapago As String = "01"
                    Try



                        '    If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                        If serie = " " Then serie = ""

                        Dim fs As FileStream = File.Create((cadenafolder & "\FACTURA" & serie & factura & ".XML").Trim)
                        Dim fs1 As FileStream = File.Create((directorioreimpresas & "\FACTURA" & serie & factura & ".XML").Trim)
                        ' Add text to the file.
                        Dim info As Byte() = New UTF8Encoding(True).GetBytes(datosfac("CFDI").ToString().TrimStart().TrimEnd())
                        fs.Write(info, 0, info.Length)
                        fs.Close()

                        Dim info2 As Byte() = New UTF8Encoding(True).GetBytes(datosfac("CFDI").ToString().TrimStart().TrimEnd())
                        fs1.Write(info, 0, info2.Length)
                        fs1.Close()

                        ' CONSTRIUIMOS LA CADENA
                        CADENAORIGINAL = datosfac("cadena") 'CONSTRUIRCADENACFDI((cadenafolder & "\FACTURA" & serie & factura & ".XML").Trim)

                        ' MANDAMOS CONSTRUIR EL QR 

                        ' LEEMOS EL XML




                        Dim image As System.Drawing.Image = qrdatos(varUUID, varTotal, VARRFCEMISOR, varRFCRECEPTOR, varcertificado)

                        Dim imageConverter As New ImageConverter()
                        Dim pngs As Byte() = DirectCast(imageConverter.ConvertTo(image, GetType(Byte())), Byte())


                        Dim dts As New DatosReciboTableAdapters.cajasTableAdapter
                        dts.UpdateQueryimagen(pngs, My.Settings.caja)


                        ' End If
                    Catch ex As Exception

                    End Try
                    Dim varuso As String = "G03"
                    Dim metodo As String = "PUE"

                    If datosfac("version") = "3.3" Then

                        Dim varXmlFile As XmlDocument = New XmlDocument()

                        Dim varXmlNsMngr As XmlNamespaceManager = New XmlNamespaceManager(varXmlFile.NameTable)


                        varXmlFile.Load((cadenafolder & "\FACTURA" & serie & factura & ".XML").Trim)

                        varXmlNsMngr.AddNamespace("cfdi", "http://www.sat.gob.mx/cfd/3")
                        varXmlNsMngr.AddNamespace("tfd", "http://www.sat.gob.mx/TimbreFiscalDigital")




                        varTotal = varXmlFile.ChildNodes.Item(1).Attributes.GetNamedItem("Total").Value '  varXmlFile.SelectSingleNode("/cfdi:Comprobante/@total", varXmlNsMngr).InnerText
                        VARNODECERTIFICADO = varXmlFile.ChildNodes.Item(1).Attributes.GetNamedItem("NoCertificado").Value 'varXmlFile.SelectSingleNode("/cfdi:Comprobante/@NoCertificado", varXmlNsMngr).InnerText
                        varformapago = varXmlFile.ChildNodes.Item(1).Attributes.GetNamedItem("FormaPago").Value 'varXmlFile.SelectSingleNode("/cfdi:Comprobante/@formapago", varXmlNsMngr).InnerText
                        varUUID = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@UUID", varXmlNsMngr).InnerText
                        varcertificado = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@NoCertificadoSAT", varXmlNsMngr).InnerText
                        VARSELLOSAT = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@SelloSAT", varXmlNsMngr).InnerText
                        VARSELLOCFD = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@SelloCFD", varXmlNsMngr).InnerText
                        Dim LISTANODOSEMISOR As XmlNodeList = varXmlFile.GetElementsByTagName("cfdi:Emisor")
                        For Each xAtt In LISTANODOSEMISOR
                            VARRFCEMISOR = VarXml(xAtt, "Rfc")
                            ' strEmisorNombre = VarXml(xAtt, "nombre")
                        Next
                        Dim LISTANODORECEPTOR As XmlNodeList = varXmlFile.GetElementsByTagName("cfdi:Receptor")
                        For Each xAtt In LISTANODORECEPTOR
                            varRFCRECEPTOR = VarXml(xAtt, "Rfc")
                            ' strEmisorNombre = VarXml(xAtt, "nombre")
                        Next




                        ' AQUI PONEMOS EL CODIGO
                        'Dim varuso As String = "G03"
                        'Dim metodo As String = "PUE"

                    ElseIf datosfac("version") = "4.0" Then

                        Dim varXmlFile As XmlDocument = New XmlDocument()

                        Dim varXmlNsMngr As XmlNamespaceManager = New XmlNamespaceManager(varXmlFile.NameTable)


                        varXmlFile.Load((cadenafolder & "\FACTURA" & serie & factura & ".XML").Trim)

                        varXmlNsMngr.AddNamespace("cfdi", "http://www.sat.gob.mx/cfd/3")
                        varXmlNsMngr.AddNamespace("tfd", "http://www.sat.gob.mx/TimbreFiscalDigital")




                        varTotal = varXmlFile.ChildNodes.Item(1).Attributes.GetNamedItem("Total").Value '  varXmlFile.SelectSingleNode("/cfdi:Comprobante/@total", varXmlNsMngr).InnerText
                        VARNODECERTIFICADO = varXmlFile.ChildNodes.Item(1).Attributes.GetNamedItem("NoCertificado").Value 'varXmlFile.SelectSingleNode("/cfdi:Comprobante/@NoCertificado", varXmlNsMngr).InnerText
                        varformapago = varXmlFile.ChildNodes.Item(1).Attributes.GetNamedItem("FormaPago").Value
                        metodo = varXmlFile.ChildNodes.Item(1).Attributes.GetNamedItem("MetodoPago").Value 'varXmlFile.SelectSingleNode("/cfdi:Comprobante/@formapago", varXmlNsMngr).InnerText

                        Dim LISTANODOComplemento As XmlNodeList = varXmlFile.GetElementsByTagName("tfd:TimbreFiscalDigital")

                        For Each xAtt In LISTANODOComplemento
                            varUUID = VarXml(xAtt, "UUID")
                            varcertificado = VarXml(xAtt, "NoCertificadoSAT")
                            VARSELLOSAT = VarXml(xAtt, "SelloSAT")
                            VARSELLOCFD = VarXml(xAtt, "SelloCFD")
                            'strEmisorNombre = VarXml(xAtt, "nombre")
                        Next

                        'varUUID = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@UUID", varXmlNsMngr).InnerText
                        'varcertificado = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@NoCertificadoSAT", varXmlNsMngr).InnerText
                        'VARSELLOSAT = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@SelloSAT", varXmlNsMngr).InnerText
                        'VARSELLOCFD = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@SelloCFD", varXmlNsMngr).InnerText
                        Dim LISTANODOSEMISOR As XmlNodeList = varXmlFile.GetElementsByTagName("cfdi:Emisor")
                        For Each xAtt In LISTANODOSEMISOR
                            VARRFCEMISOR = VarXml(xAtt, "Rfc")
                            ' strEmisorNombre = VarXml(xAtt, "nombre")
                        Next
                        Dim LISTANODORECEPTOR As XmlNodeList = varXmlFile.GetElementsByTagName("cfdi:Receptor")
                        For Each xAtt In LISTANODORECEPTOR
                            varRFCRECEPTOR = VarXml(xAtt, "Rfc")
                            ' strEmisorNombre = VarXml(xAtt, "nombre")
                        Next




                        ' AQUI PONEMOS EL CODIGO
                        'Dim varuso As String = "G03"
                        'Dim metodo As String = "PUE"

                    End If

#Region "Dar propiedades al documento"

                    'Dar propiedades al Documento
                    Dim pdfDoc As New Document(iTextSharp.text.PageSize.LETTER, 15.0F, 15.0F, 30.0F, 30.0F)

                        Dim pdfWrite As PdfWriter = PdfWriter.GetInstance(pdfDoc, New System.IO.FileStream(cadenafolder & "\factura" & serie & factura & ".pdf", FileMode.Create))
                        Dim pdfWrite2 As PdfWriter = PdfWriter.GetInstance(pdfDoc, New System.IO.FileStream(directorioreimpresas & "\factura" & serie & factura & ".pdf", FileMode.Create))

                        'Formtos para distintos tamaños de letras

                        'Formato Letras




                        Dim Font As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 6, iTextSharp.text.Font.NORMAL))
                        Dim Font8N As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 6, iTextSharp.text.Font.BOLD))
                        Dim Font8 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.BOLD))
                        Dim Font88 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 12, iTextSharp.text.Font.BOLD))
                        Dim Font12 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.BOLD))
                        Dim Font9 As New Font(FontFactory.GetFont(FontFactory.HELVETICA, 7, iTextSharp.text.Font.NORMAL))
                        Dim Fontp As New Font(FontFactory.GetFont(FontFactory.COURIER, 7, iTextSharp.text.Font.BOLD))
                        Dim CVacio As PdfPCell = New PdfPCell(New Phrase(""))
                        CVacio.Border = 0




#End Region

#Region "Encabezado"

                        'abrimos el pdf para comenzar a escribir en el, al terminar cerramos
                        pdfDoc.Open()




                        ' comenzamos con un cuadro

                        'Dim _cb As PdfContentByte

                        Dim colordefinido = New Clscolorreporte()
                        colordefinido.ClsColoresReporte(My.Settings.colorfactura)

                        '_cb = pdfWrite.DirectContentUnder
                        '_cb.SetColorStroke(colordefinido.color) '/Color de la linea
                        '_cb.SetColorFill(colordefinido.color) '/ Color del relleno
                        '_cb.SetLineWidth(3.5) ''Tamano de la linea
                        '_cb.Rectangle(350, 720, 20, 100)
                        '_cb.FillStroke()

                        '''


                        Dim Table1 As PdfPTable = New PdfPTable(3)
                        Table1.DefaultCell.Border = BorderStyle.None
                        Dim Col1 As PdfPCell
                        'Dim ILine As Integer
                        'Dim iFila As Integer
                        Table1.WidthPercentage = 100

                        Dim widths As Single() = New Single() {100.0F, 300, 280.0F}
                        Table1.SetWidths(widths)

                        'Encabezado

                        '   Dim imagenURL As String = "C:\Users\User\Desktop\huichapan\CAJAS Huichapan 3.3\LogoHuichapan.jpg"




                        Dim imagenBMP As iTextSharp.text.Image
                        imagenBMP = iTextSharp.text.Image.GetInstance(LOGOBYTE)
                        imagenBMP.ScaleToFit(80.0F, 70.0F)
                        'imagenBMP.SpacingBefore = 100.0F
                        'imagenBMP.SpacingAfter = 1000.0F

                        imagenBMP.Border = 0


                        Table1.AddCell(imagenBMP)

                        'Sustituir por los valores reales cuando se pase a frmvalidafactura,vb



                        Dim Tabledireccion As PdfPTable = New PdfPTable(1)
                        Col1 = New PdfPCell(New Phrase(Empresa, Font12))
                        Col1.Border = 0
                        Col1.HorizontalAlignment = PdfPCell.ALIGN_CENTER

                        Dim DIRECCIONE As String = Direccion & " " & coloniaEMPRESA & " " & poblacionEMPRESA & " " & Estadoempresa
                        Dim Col1d = New PdfPCell(New Phrase(DIRECCIONE, Font8))
                        Col1d.Border = 0
                        Col1d.HorizontalAlignment = PdfPCell.ALIGN_CENTER


                        Dim Col1rfe = New PdfPCell(New Phrase(RFCORGANISMO, Font9))
                        Col1rfe.Border = 0
                        Col1rfe.HorizontalAlignment = PdfPCell.ALIGN_CENTER

                        Tabledireccion.AddCell(Col1)
                        Tabledireccion.AddCell(Col1d)
                        Tabledireccion.AddCell(Col1rfe)
                        Table1.AddCell(Tabledireccion)

                        Dim Table2 As PdfPTable = New PdfPTable(2)
                        Dim Col10 As PdfPCell
                        Dim Col11 As PdfPCell
                        Dim Col12 As PdfPCell
                        Dim Col13 As PdfPCell
                        Dim Col14 As PdfPCell
                        Table2.WidthPercentage = 100
                        Dim widthsT2 As Single() = New Single() {100, 180.0F}
                        Table2.SetWidths(widthsT2)

                        Col10 = New PdfPCell(New Phrase("Serie", Font88))
                        Col10.Border = 0
                        Col10.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(Col10)


                        Col11 = New PdfPCell(New Phrase(serie, Font88))
                        Col11.Border = 0
                        Col11.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(Col11)

                        Dim Col10f = New PdfPCell(New Phrase("Factura", Font88))
                        Col10f.Border = 0
                        Col10f.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(Col10f)


                        Col12 = New PdfPCell(New Phrase(factura, Font88))
                        Col12.Border = 0
                        Col12.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(Col12)

                        Col13 = New PdfPCell(New Phrase("Fecha de comprobante:", Font))
                        Col13.Border = 0
                        Col13.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(Col13)

                        Col14 = New PdfPCell(New Phrase(FECHAFAC, Font))
                        Col14.Border = 0
                        Col14.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(Col14)


                        Dim ColDC1 = New PdfPCell(New Phrase("UUID", Font))
                        ColDC1.Border = 0
                        ColDC1.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(ColDC1)


                        Dim ColDC2 = New PdfPCell(New Phrase(varUUID, Font))
                        ColDC2.Border = 0
                        ColDC2.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(ColDC2)

                        Dim ColDC3 = New PdfPCell(New Phrase("Certificado Emisor", Font))
                        ColDC3.Border = 0
                        ColDC3.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(ColDC3)

                        Dim ColDC4 = New PdfPCell(New Phrase(varcertificado, Font))
                        ColDC4.Border = 0
                        ColDC4.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(ColDC4)


                        Dim ColDC7 = New PdfPCell(New Phrase("Certificado Sat ", Font))
                        ColDC7.Border = 0
                        ColDC7.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(ColDC7)

                        Dim ColDC8 = New PdfPCell(New Phrase(VARNODECERTIFICADO, Font))
                        ColDC8.Border = 0
                        ColDC8.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(ColDC8)



                        Dim ColDC11 = New PdfPCell(New Phrase("Forma de Pago ", Font))
                        ColDC11.Border = 0
                        ColDC11.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(ColDC11)

                        'Dim ColDC12 = New PdfPCell(New Phrase(varformapago, Font))
                        'ColDC12.Border = 0
                        'ColDC12.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        'Table2.AddCell(ColDC12)


                        Dim ForPago As String = varformapago


                    Dim ColDC12 = New PdfPCell(New Phrase(New decodificadorSAT().getFormapago(ForPago), Font))
                    ColDC12.Border = 0
                        ColDC12.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(ColDC12)



                        Dim ColDCMeP = New PdfPCell(New Phrase("Método de Pago ", Font))
                        ColDCMeP.Border = 0
                        ColDCMeP.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(ColDCMeP)

                        Dim ColDCMeP2 = New PdfPCell(New Phrase(New decodificadorSAT().getMetodo(metodo), Font))
                        ColDCMeP2.Border = 0
                        ColDCMeP2.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(ColDCMeP2)

                        Dim ColDCUsoCFDI = New PdfPCell(New Phrase("Uso CFDI ", Font))
                        ColDCUsoCFDI.Border = 0
                        ColDCUsoCFDI.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(ColDCUsoCFDI)

                        Dim ColDCUsoCFDI2 = New PdfPCell(New Phrase(New decodificadorSAT().getUso(varuso), Font))
                        ColDCUsoCFDI2.Border = 0
                        ColDCUsoCFDI2.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table2.AddCell(ColDCUsoCFDI2)


                        Table1.AddCell(Table2)


                        'Table1.CompleteRow()



                        'Table Datos Cliente
                        Dim Table4 As PdfPTable = New PdfPTable(2)
                        Dim ColDN As PdfPCell
                        Dim ColDN1 As PdfPCell
                        Dim ColDN2 As PdfPCell
                        Dim ColDN3 As PdfPCell
                        Dim ColDN4 As PdfPCell
                        Dim ColDN5 As PdfPCell
                        Dim ColDN6 As PdfPCell
                        Dim ColDN7 As PdfPCell
                        Dim ColDN8 As PdfPCell
                        Dim ColDN9 As PdfPCell
                        Table4.WidthPercentage = 100
                        Dim widthsT4 As Single() = New Single() {600.0F, 200.0F}

                        Table4.SetWidths(widthsT4)

                        ColDN = New PdfPCell(New Phrase(datosfac("NOMBRE"), Font9))
                        ColDN.Border = 0
                        ColDN.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table4.AddCell(ColDN)

                        ColDN1 = New PdfPCell(New Phrase(varRFCRECEPTOR, Font9))
                        ColDN1.Border = 0
                        ColDN1.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table4.AddCell(ColDN1)

                        ColDN2 = New PdfPCell(New Phrase(datosrecibo("calle") & " " & datosrecibo("numext") & " " & datosrecibo("numint"), Font9))
                        ColDN2.Border = 0
                        ColDN2.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table4.AddCell(ColDN2)

                        ColDN3 = New PdfPCell(New Phrase("Cuenta: " & cuenta, Font))
                        ColDN3.Border = 0
                        ColDN3.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table4.AddCell(ColDN3)

                        ColDN4 = New PdfPCell(New Phrase(datosrecibo("colonia"), Font9))
                        ColDN4.Border = 0
                        ColDN4.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table4.AddCell(ColDN4)

                        ColDN5 = New PdfPCell(New Phrase("Tipo: " & tipo, Font))
                        ColDN5.Border = 0
                        ColDN5.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table4.AddCell(ColDN5)

                        ColDN6 = New PdfPCell(New Phrase(datosrecibo("poblacion") & " " & datosrecibo("delegacion") & " " & datosrecibo("estado") & " CP " & datosrecibo("cp"), Font9))
                        ColDN6.Border = 0
                        ColDN6.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table4.AddCell(ColDN6)

                        ColDN7 = New PdfPCell(New Phrase(" ", Font))
                        ColDN7.Border = 0
                        ColDN7.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table4.AddCell(ColDN7)

                        ColDN8 = New PdfPCell(New Phrase(" ", Font9))
                        ColDN8.Border = 0
                        ColDN8.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table4.AddCell(ColDN8)

                        'Dim datos As Odbc.OdbcDataReader
                        'datos = ConsultaSql("select * from usuario USU inner join descuentos DES on(USU.idDescuento=DES.idDescuento) where cuenta=" & recibo.cuenta & "").ExecuteReader
                        'datos.Read()

                        ''''''Ticket #29
                        'DescRecargo = Decimal.Parse(descuentoRecargos).ToString("C")
                        'subtotal = Decimal.Parse(subtotal).ToString("C")
                        ColDN9 = New PdfPCell(New Phrase("", Font9))
                        ColDN9.Border = 0
                        ColDN9.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table4.AddCell(ColDN9)



                        Dim Table5 As PdfPTable = New PdfPTable(2)
                        Dim Col51 As PdfPCell
                        Dim Col52 As PdfPCell
                        Dim Col53 As PdfPCell
                        Dim Col54 As PdfPCell

                        Table5.WidthPercentage = 100
                        Dim widthsT5 As Single() = New Single() {50.0F, 400.0F}

                        Table5.SetWidths(widthsT5)

                        Col51 = New PdfPCell(New Phrase(" ", Font))
                        Col51.Border = 0
                        Col51.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table5.AddCell(Col51)

                        Col52 = New PdfPCell(New Phrase("", Font))
                        Col52.Border = 0
                        Col52.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table5.AddCell(Col52)

                        Col53 = New PdfPCell(New Phrase(" ", Font9))
                        Col53.Border = 0
                        Col53.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table5.AddCell(Col53)

                        Col54 = New PdfPCell(New Phrase(" ", Font))
                        Col54.Border = 0
                        Col54.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table5.AddCell(Col54)

                        'Encabezado consulta tabla

                        Dim Table6 As PdfPTable = New PdfPTable(5)
                        Dim Col61 As PdfPCell
                        Dim Col62 As PdfPCell
                        Dim Col63 As PdfPCell
                        Dim Col64 As PdfPCell

                        Table6.WidthPercentage = 100
                        Dim widthsT6 As Single() = New Single() {50.0F, 290.0F, 100.0F, 50.0F, 100.0F}
                        Table6.SetWidths(widthsT6)

                        Col61 = New PdfPCell(New Phrase("Cantidad", Font9))
                        Col61.Border = 7
                        Col61.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Col61.BackgroundColor = colordefinido.color
                        Table6.AddCell(Col61)

                        Col62 = New PdfPCell(New Phrase("Concepto", Font9))
                        Col62.Border = 3
                        Col62.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                        Col62.BackgroundColor = colordefinido.color
                        Table6.AddCell(Col62)

                        Col63 = New PdfPCell(New Phrase("Monto", Font9))
                        Col63.Border = 3
                        Col63.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Col63.BackgroundColor = colordefinido.color
                        Table6.AddCell(Col63)

                        Dim ColMi = New PdfPCell(New Phrase("IVA", Font9))
                        ColMi.Border = 3
                        ColMi.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        ColMi.BackgroundColor = colordefinido.color
                        Table6.AddCell(ColMi)


                        Col64 = New PdfPCell(New Phrase("Importe", Font9))
                        Col64.Border = 11
                        Col64.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Col64.BackgroundColor = colordefinido.color
                        Table6.AddCell(Col64)



                        While daatoscontenido.Read()
                            Col61 = New PdfPCell(New Phrase(daatoscontenido("Cantidad"), Font9))
                            Col61.Border = 0
                            Col61.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                            Table6.AddCell(Col61)

                            Col62 = New PdfPCell(New Phrase(daatoscontenido("Concepto"), Font9))
                            Col62.Border = 0
                            Col62.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                            Table6.AddCell(Col62)
                            Dim montox As String = Decimal.Parse(daatoscontenido("Monto")).ToString("C")
                            Col63 = New PdfPCell(New Phrase(montox, Font9))
                            Col63.Border = 0
                            Col63.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                            Table6.AddCell(Col63)
                            Dim ivax As String = Decimal.Parse(daatoscontenido("MontoIVA")).ToString("C")
                            Dim ColMiv = New PdfPCell(New Phrase(ivax, Font9))
                            ColMiv.Border = 0
                            ColMiv.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                            Table6.AddCell(ColMiv)

                            Dim importex As String = Decimal.Parse(daatoscontenido("Importe")).ToString("C")
                            Col64 = New PdfPCell(New Phrase(importex, Font9))
                            Col64.Border = 0
                            Col64.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                            Table6.AddCell(Col64)



                        End While

                        Dim TableEspacio As PdfPTable = New PdfPTable(1)
                        Dim ColEsp As PdfPCell
                        TableEspacio.WidthPercentage = 100
                        Dim widthsTE As Single() = New Single() {1000.0F}
                        TableEspacio.SetWidths(widthsTE)

                        ColEsp = New PdfPCell(New Phrase(" ", Font))
                        ColEsp.Border = 0
                        ColEsp.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        TableEspacio.AddCell(ColEsp)

                        Dim Table7 As PdfPTable = New PdfPTable(4)
                        Dim Col71 As PdfPCell
                        Dim Col72 As PdfPCell
                        Dim Col73 As PdfPCell
                        Dim Col74 As PdfPCell
                        Dim Col75 As PdfPCell
                        Dim Col76 As PdfPCell
                        Dim Col77 As PdfPCell
                        Dim Col78 As PdfPCell

                        Table7.WidthPercentage = 100
                        Dim widthsT7 As Single() = New Single() {200.0F, 500.0F, 150.0F, 150.0F}
                        Table7.SetWidths(widthsT7)

                        Col71 = New PdfPCell(New Phrase(" ", Font))
                        Col71.Border = 0
                        Col71.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table7.AddCell(Col71)

                        Col72 = New PdfPCell(New Phrase("", Font))
                        Col72.Border = 0
                        Col72.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                        Table7.AddCell(Col72)

                        Col73 = New PdfPCell(New Phrase("Subtotal: ", Font))
                        Col73.Border = 0
                        Col73.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table7.AddCell(Col73)


                        subtotal = Decimal.Parse(subtotal).ToString("C")
                        Col74 = New PdfPCell(New Phrase(subtotal, Font))
                        Col74.Border = 1
                        Col74.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table7.AddCell(Col74)
                        Table7.CompleteRow()

                        Col71 = New PdfPCell(New Phrase(" ", Font))
                        Col71.Border = 0
                        Col71.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table7.AddCell(Col71)

                        Col72 = New PdfPCell(New Phrase("", Font))
                        Col72.Border = 0
                        Col72.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table7.AddCell(Col72)

                        Col73 = New PdfPCell(New Phrase("IVA: ", Font))
                        Col73.Border = 0
                        Col73.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table7.AddCell(Col73)


                        ivatotal = Decimal.Parse(ivatotal).ToString("C")
                        Col74 = New PdfPCell(New Phrase(ivatotal, Font))
                        Col74.Border = 0
                        Col74.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table7.AddCell(Col74)

                        Table7.CompleteRow()

                        Col71 = New PdfPCell(New Phrase(" ", Font))
                        Col71.Border = 0
                        Col71.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table7.AddCell(Col71)

                        Col72 = New PdfPCell(New Phrase("", Font))
                        Col72.Border = 0
                        Col72.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table7.AddCell(Col72)

                        Col73 = New PdfPCell(New Phrase("TOTAL: ", Font))
                        Col73.Border = 0
                        Col73.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Table7.AddCell(Col73)


                        'Coversión
                        Dim totalfacturax As String = Decimal.Parse(totalfactura).ToString("C")
                        Col74 = New PdfPCell(New Phrase(totalfacturax, Font))
                        Col74.Border = 1
                        Col74.HorizontalAlignment = PdfPCell.ALIGN_RIGHT



                        Table7.AddCell(Col74)

                        Col75 = New PdfPCell(New Phrase(" ", Font))
                        Col75.Border = 0
                        Col75.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        'Table7.
                        Table7.AddCell(Col75)

                        Col76 = New PdfPCell(New Phrase(" ", Font))
                        Col76.Border = 0
                        Col76.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        'Table7.
                        Table7.AddCell(Col76)

                        Col77 = New PdfPCell(New Phrase(" ", Font))
                        Col77.Border = 0
                        Col77.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        'Table7.
                        Table7.AddCell(Col77)

                        Col78 = New PdfPCell(New Phrase(" ", Font))
                        Col78.Border = 0
                        Col78.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        'Table7.
                        Table7.AddCell(Col78)


                        Dim Table8 As PdfPTable = New PdfPTable(1)
                        Dim Col81 As PdfPCell
                        Dim Col82 As PdfPCell
                        Dim Col83 As PdfPCell


                        Table8.WidthPercentage = 100
                        Dim widthsT8 As Single() = New Single() {1000.0F}
                        Table8.SetWidths(widthsT8)

                        Col81 = New PdfPCell(New Phrase(ConvertCurrencyToSpanish(datosfac("TOTAL"), "Pesos"), Font9))
                        Col81.Border = 0
                        Col81.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table8.AddCell(Col81)


                        Col83 = New PdfPCell(New Phrase(" ", Font))
                        Col83.Border = 0
                        Col83.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                        Table8.AddCell(Col83)



                        Dim Table9 As PdfPTable = New PdfPTable(2)
                        Dim Col91 As PdfPCell
                        Dim Col92 As PdfPCell
                        Dim Col93 As PdfPCell
                        Dim Col94 As PdfPCell
                        Dim Col95 As PdfPCell
                        Dim Col96 As PdfPCell
                        Dim Col97 As PdfPCell
                        Dim Col98 As PdfPCell
                        Dim Col99 As PdfPCell
                        Dim Col910 As PdfPCell
                        Dim Col911 As PdfPCell
                        Dim Col912 As PdfPCell


                        Table9.WidthPercentage = 100
                        Dim widthsT9 As Single() = New Single() {500.0F, 100.0F}
                        Table9.SetWidths(widthsT9)



                        Dim TableSellos As PdfPTable = New PdfPTable(2)


                        TableSellos.WidthPercentage = 100
                        Dim widthsTIT2 As Single() = New Single() {80.0F, 200.0F}
                        TableSellos.SetWidths(widthsTIT2)






                        Col92 = New PdfPCell(New Phrase("Sello CFDI   ", Font9))
                        Col92.Border = 0
                        Col92.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                        TableSellos.AddCell(Col92)

                        Col93 = New PdfPCell(New Phrase(VARSELLOCFD, Fontp))
                        Col93.Border = 0
                        Col93.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                        TableSellos.AddCell(Col93)

                        Col94 = New PdfPCell(New Phrase(" ", Font))
                        Col94.Border = 0
                        Col94.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                        TableSellos.AddCell(Col94)


                        Col95 = New PdfPCell(New Phrase(" ", Font9))
                        Col95.Border = 0
                        Col95.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        TableSellos.AddCell(Col95)

                        Col96 = New PdfPCell(New Phrase("Sello SAT   ", Font9))
                        Col96.Border = 0
                        Col96.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                        TableSellos.AddCell(Col96)

                        Col97 = New PdfPCell(New Phrase(VARSELLOSAT, Fontp))
                        Col97.Border = 0
                        Col97.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                        TableSellos.AddCell(Col97)

                        Col98 = New PdfPCell(New Phrase(" ", Font))
                        Col98.Border = 0
                        Col98.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                        TableSellos.AddCell(Col98)


                        Col99 = New PdfPCell(New Phrase(" ", Font9))
                        Col99.Border = 0
                        Col99.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        TableSellos.AddCell(Col99)

                        Col910 = New PdfPCell(New Phrase("Cadena Original   ", Font9))
                        Col910.Border = 0
                        'Col910.BackgroundColor()
                        Col910.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                        TableSellos.AddCell(Col910)

                        Col911 = New PdfPCell(New Phrase(CADENAORIGINAL, Fontp))
                        Col911.Border = 0
                        Col911.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                        TableSellos.AddCell(Col911)

                        TableSellos.DefaultCell.Border = BorderStyle.None
                        Table9.DefaultCell.Border = BorderStyle.None
                        Table9.AddCell(TableSellos)



                        Dim codigoQR = New StringBuilder()
                        codigoQR.Append("https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx?id=" + varUUID)
                        codigoQR.Append("&re=" & VARRFCEMISOR) 'RFC del Emisor
                        codigoQR.Append("&rr=" & varRFCRECEPTOR) 'RFC del receptor
                        codigoQR.Append("&tt=" & totalfactura) ' Total del comprobante 10 enteros y 6 decimales
                        codigoQR.Append("&fe=" + VARSELLOCFD.Substring(VARSELLOCFD.Length - 8, 8)) 'UUID del comprobante
                        Dim pdfCodigoQR = New BarcodeQRCode(codigoQR.ToString(), 1, 1, New Dictionary(Of iTextSharp.text.pdf.qrcode.EncodeHintType, System.Object))
                        Dim img As Image = pdfCodigoQR.GetImage()
                        img.SpacingAfter = 0.0F
                        img.SpacingBefore = 0.0F
                        img.BorderWidth = 1.0F
                        img.HasAbsolutePosition()
                        'img.ScalePercent(100, 78)
                        Table9.AddCell(img)
                        'img.Alignment = 6;
                        Table9.CompleteRow()

                        Dim Table10 As PdfPTable = New PdfPTable(1)
                        Dim Col101 As PdfPCell


                        Table10.WidthPercentage = 100
                        Dim widthsT10 As Single() = New Single() {1000.0F}
                        Table10.SetWidths(widthsT10)

                    Col101 = New PdfPCell(New Phrase("ESTE DOCUMENTO ES UNA REPRESENTACION IMPRESA DE UN CFDI 4.0 EFECTOS FISCALES AL PAGO", Font8N))
                    Col101.Border = 0
                    Col101.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                    Table10.AddCell(Col101)


                        Dim Table11 As PdfPTable = New PdfPTable(1)
                        Dim Col111 As PdfPCell


                        Table11.WidthPercentage = 100
                        Dim widthsT11 As Single() = New Single() {1000.0F}
                        Table11.SetWidths(widthsT11)

                    Col111 = New PdfPCell(New Phrase(" ", Font))
                    Col111.Border = 0
                        Col111.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        Table11.AddCell(Col111)


                        Dim TableF As PdfPTable = New PdfPTable(3)
                        Dim ColF1 As PdfPCell
                        Dim ColF2 As PdfPCell
                        Dim ColF3 As PdfPCell
                        'Dim ColF4 As PdfPCell

                        TableF.WidthPercentage = 100
                        Dim widthsTF As Single() = New Single() {400.0F, 200.0F, 400.0F}
                        TableF.SetWidths(widthsTF)

                        ColF1 = New PdfPCell(New Phrase(" ", Font9))
                        ColF1.Border = 0
                        ColF1.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        TableF.AddCell(ColF1)

                        ColF2 = New PdfPCell(New Phrase("FIRMA DEL CLIENTE", Fontp))
                        ColF2.Border = 1
                        ColF2.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                        TableF.AddCell(ColF2)


                        'TableF.AddCell(ColF1)


                        ColF3 = New PdfPCell(New Phrase(" ", Font))
                        ColF3.Border = 0
                        ColF3.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                        TableF.AddCell(ColF3)
                        TableF.AddCell(ColF3)


                        Dim TableF2 As PdfPTable = New PdfPTable(2)
                        Dim ColF21 As PdfPCell
                        Dim ColF22 As PdfPCell
                        Dim ColF23 As PdfPCell


                        TableF2.WidthPercentage = 100
                        Dim widthsTF2 As Single() = New Single() {500.0F, 500.0F}
                        TableF2.SetWidths(widthsTF2)

                        ColF21 = New PdfPCell(New Phrase(" ", Font9))
                        ColF21.Border = 0
                        ColF21.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        TableF2.AddCell(ColF21)

                        ColF22 = New PdfPCell(New Phrase(datosfac("usuario"), Fontp))
                        ColF22.Border = 0
                        ColF22.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        TableF2.AddCell(ColF22)



                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



                        'Cargar las tablas
                        pdfDoc.Add(Table1)


                        pdfDoc.Add(Table4)
                        pdfDoc.Add(Table5)
                        pdfDoc.Add(Table6)
                        'pdfDoc.Add(TableEspacio)
                        pdfDoc.Add(Table7)
                        pdfDoc.Add(Table8)
                        pdfDoc.Add(Table9)
                        pdfDoc.Add(TableEspacio)
                        pdfDoc.Add(Table10)
                        pdfDoc.Add(TableEspacio)
                        pdfDoc.Add(TableEspacio)
                        pdfDoc.Add(TableEspacio)
                        pdfDoc.Add(Table11)
                        pdfDoc.Add(TableEspacio)
                        pdfDoc.Add(TableEspacio)
                        pdfDoc.Add(TableEspacio)
                        pdfDoc.Add(TableEspacio)
                        pdfDoc.Add(TableEspacio)
                        pdfDoc.Add(TableF)
                        pdfDoc.Add(TableEspacio)
                        pdfDoc.Add(TableF2)



#End Region

                        pdfDoc.Close()










                    End If ' fin de si leyo las lecturas
                Dim mail As New MailMessage

                Dim send As New SmtpClient

                Dim titulo As String = "Información del sistema"
                Dim mensaje As String = "Introduce correo electronico"
                Dim resultado As String = ""


                Dim datosf As OdbcDataReader = ConsultaSql("SELECT * FROM datosfiscales WHERE CUENTA=" & cuenta & " AND TIPO='" & tipo & "'").ExecuteReader

                If datosf.Read Then

                    Try

                        resultado = datosf("MAILDEENVIO")

                        'txtObservaciones.Text = "Cuenta: " & datosU("cuenta") & " Tarifa: " & datosU("descripcion_cuota") & " Medidor: " & datosU("nodemedidor")
                    Catch ex As Exception


                    End Try
                End If


                resultado = InputBox(mensaje, titulo, resultado, Me.Width / 2, Me.Height / 2)



                Try
                    mail.To.Clear()
                    mail.Body = My.MySettings.Default.MENSAJECORREO

                    ' mail.Body = My.MySettings.Default.
                    mail.Subject = My.MySettings.Default.Asuntocorreo
                    mail.IsBodyHtml = False
                    mail.To.Add(Trim(resultado))
                    mail.Attachments.Add(New Attachment(directorioreimpresas & "\factura" & serie & factura & ".xml"))
                    mail.Attachments.Add(New Attachment(directorioreimpresas & "\factura" & serie & factura & ".pdf"))
                    mail.From = New MailAddress(My.MySettings.Default.CorreoFacturas, My.Settings.Correodefault)

                    'If cboMail.SelectedIndex = 1 Then
                    send.Host = "smtp.gmail.com"
                    send.Port = 587
                    send.EnableSsl = True
                    send.DeliveryMethod = SmtpDeliveryMethod.Network
                    send.UseDefaultCredentials = False
                    send.Credentials = New Net.NetworkCredential(My.Settings.CorreoFacturas, My.Settings.Passwordcorreo)
                    '        End If
                    'If cboMail.SelectedIndex = 2 Then
                    '    send.Host = "smtp.live.com"
                    '    send.Port = 587
                    '    send.EnableSsl = False
                    'End If
                    send.Send(mail)
                    ' Me.Cursor = Cursors.Default
                    '  Me.Text = "Enviar Mensaje"
                    MsgBox("Un correo fue enviado correctamente", MsgBoxStyle.Information, "Mensaje")

                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Mensajeria", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
        End Select

    End Sub

    Private Sub ToolCancelaSAT_Click(sender As Object, e As EventArgs) Handles ToolCancelaSAT.Click
        'CANCELAR FACTURA ANTES EL SAT

        'ClsFacturar factu = New ClsFacturar();

        Dim factura As Int64 = dataGridView1.Item("numero", dataGridView1.CurrentRow.Index).Value
        Dim serie As String = dataGridView1.Item("serie", dataGridView1.CurrentRow.Index).Value.ToString()
        Dim uuid As String = dataGridView1.Item("uuid", dataGridView1.CurrentRow.Index).Value.ToString()

        Dim IDFactura As String = obtenerCampo("select idENCFAC from Encfac where numero=" & factura & " and serie='" & serie & "'", "idencfac")

        'Dim objFactura As CLSFACTURA = New CLSFACTURA()

        Dim objCancelaSAT As New CancelarFactura40(uuid, IDFactura)
        'Dim objCancelaSAT As New CancelarFactura40()
        objCancelaSAT.Show()

    End Sub

    Private Sub ToolStripLabel1_Click(sender As Object, e As EventArgs) Handles ToolStripLabel1.Click

    End Sub

End Class

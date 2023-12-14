Imports System.Drawing.Text

Public Class Formatos
    Dim empezar As Boolean = False
    Dim installedFonts As New InstalledFontCollection()
    Dim x As New base

    Private Sub Formatos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        x.conectar()
        x.llenarCombo(cmbformato, "select distinct idformato,idformato from formatorecibo")
        x.llenarCombo(cmbbase, "show columns from pagos ")
        cmbbase.DisplayMember = "field"
        cmbbase.ValueMember = "field"

        cmbletras.DataSource = installedFonts.Families
        cmbletras.DisplayMember = "Name"
        cmbletras.DrawMode = DrawMode.OwnerDrawFixed

        cmbletras2.DataSource = installedFonts.Families
        cmbletras2.DisplayMember = "Name"
        cmbletras2.DrawMode = DrawMode.OwnerDrawFixed


        empezar = True
    End Sub


    Private Sub cmbformato_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbformato.SelectedIndexChanged
        If empezar = True Then

            txtidformato.Text = cmbformato.SelectedValue
            FormatoTableAdapter.Fillbyformato(DatosRecibo.Formato, txtidformato.Text)
            cargardetalle()
        End If
    End Sub

    Private Sub EnumerateInstalledFonts(ByVal e As PaintEventArgs)
        Dim families As FontFamily() = installedFonts.Families
        Dim x As Single = 0.0F
        Dim y As Single = 0.0F
        For i As Integer = 0 To installedFonts.Families.Length - 1
            If installedFonts.Families(i).IsStyleAvailable(FontStyle.Regular) Then
                e.Graphics.DrawString(installedFonts.Families(i).Name, New Font(installedFonts.Families(i), 12), _
    Brushes.Black, x, y)
                y += 20
                If y Mod 700 = 0 Then
                    x += 140
                    y = 0
                End If
            End If
        Next
    End Sub


    Private Sub btnagregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnagregar.Click
        Dim concepto As String = ""
        If rbpago.Checked And cmbbase.SelectedIndex = -1 Then
            MessageBox.Show("No indicaste un campo de la tabla pagos")
            Exit Sub
        End If
        If rbpago.Checked And cmbbase.SelectedIndex <> -1 Then
            concepto = cmbbase.Text
        End If
        If rbtexto.Checked And txt.Text = "" Then
            MessageBox.Show("No indicaste una frase a imprimir")
            Exit Sub
        End If
        If rbtexto.Checked And txt.Text <> "" Then
            concepto = txt.Text
        End If
        If cmbtipo.SelectedIndex = -1 Then
            MessageBox.Show("No indicaste un un tipo de dato a imprimir")
            Exit Sub
        End If

        If cmbletras.SelectedIndex = -1 Then
            MessageBox.Show("No indicaste un un tipo de letra")
            Exit Sub
        End If

        If txtidformato.Text = "" Then
            MessageBox.Show("Es elemental dar una identificacion a tu formato")
            Exit Sub
        End If
        Try
            x.conectar()
            x.ejecutarSIMPLE("insert into formatorecibo (  tipo,concepto,fila,columna, letra, size,alineacion,idformato) values ('" & cmbtipo.Text & "','" & concepto & "'," & iifila.Value & "," & iicolumna.Value & ",'" & cmbletras.Text & "'," & iitamano.Value & ",'" & cmbalineacion.Text & "','" & txtidformato.Text & "')")
            x.conexion.Dispose()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        FormatoTableAdapter.Fillbyformato(DatosRecibo.Formato, txtidformato.Text)

    End Sub

    Private Sub btnaceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaceptar.Click

        If txtidformato.Text = "" Then
            MessageBox.Show("Es elemental dar una identificacion a tu formato")
            Exit Sub
        End If

        If cmbletras.SelectedIndex = -1 Then
            MessageBox.Show("No indicaste un un tipo de letra para el detalle recibo")
            Exit Sub
        End If

        x.conectar()
        x.ejecutarSIMPLE("delete from formatorecibo where idformato='" & txtidformato.Text & "'")
        Try
            For i = 0 To DGFormato.Rows.Count - 1
                x.ejecutarSIMPLE("insert into formatorecibo (  tipo,concepto,fila,columna, letra, size,alineacion,idformato) values ('" & DGFormato.Rows(i).Cells(1).Value.ToString & "','" & DGFormato.Rows(i).Cells(2).Value.ToString & "'," & DGFormato.Rows(i).Cells(3).Value.ToString & "," & DGFormato.Rows(i).Cells(4).Value.ToString & ",'" & DGFormato.Rows(i).Cells(5).Value.ToString & "'," & DGFormato.Rows(i).Cells(6).Value.ToString & ",'" & DGFormato.Rows(i).Cells(7).Value.ToString & "','" & DGFormato.Rows(i).Cells(8).Value.ToString & "')")

            Next
        Catch ex As Exception

        End Try

        Dim sihay As String = x.obtenerCampo(" select linea_inicial from cuerporecibo where idformato='" & txtidformato.Text & "'", "linea_inicial")
        If sihay = "0" Then ' no hya registro del detalle
            x.ejecutarSIMPLE("INSERT INTO cuerporecibo (IDFORMATO,LINEA_INICIAL,COLUMNA_CANTIDAD,COLUMNA_CONCEPTO,COLUMNA_PRECIO,COLUMNA_IMPORTE,LETRA,TAMANO_LETRA,AVANCE) VALUES('" & txtidformato.Text & "'," & IIlininicial.Value & "," & iicantidad.Value & "," & iicolconcep.Value & "," & iiprecio.Value & "," & IIimporte.Value & ",'" & cmbletras2.Text & "'," & Ditamano.Value & "," & iiavance.Value & ")")
        Else

            x.ejecutarSIMPLE("update cuerporecibo  set LINEA_INICIAL= " & IIlininicial.Value & ",COLUMNA_CANTIDAD=" & iicantidad.Value & ",COLUMNA_CONCEPTO=" & iicolconcep.Value & ",COLUMNA_PRECIO=" & iiprecio.Value & ",COLUMNA_IMPORTE =" & IIimporte.Value & ",LETRA='" & cmbletras2.Text & "',TAMANO_LETRA=" & Ditamano.Value & " ,AVANCE = " & iiavance.Text & " where idformato='" & txtidformato.Text & "'")
        End If


        FormatoTableAdapter.Fillbyformato(DatosRecibo.Formato, txtidformato.Text)

    End Sub

    Private Sub btnsalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsalir.Click
        x.conexion.Dispose()
        Close()
    End Sub

    Private Sub btncargar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncargar.Click
        FormatoTableAdapter.Fillbyformato(DatosRecibo.Formato, txtidformato.Text)
        cargardetalle()
    End Sub

    Public Sub cargardetalle()
        CuerpoReciboTableAdapter.Fillbyformato(DatosRecibo1.CuerpoRecibo, txtidformato.Text)
        Try
            IIlininicial.Value = DatosRecibo1.CuerpoRecibo(0).LINEA_INICIAL
            cmbletras2.Text = DatosRecibo1.CuerpoRecibo(0).LETRA
            iicantidad.Value = DatosRecibo1.CuerpoRecibo(0).COLUMNA_CANTIDAD
            iicolconcep.Value = DatosRecibo1.CuerpoRecibo(0).COLUMNA_CONCEPTO
            iiprecio.Value = DatosRecibo1.CuerpoRecibo(0).COLUMNA_PRECIO
            IIimporte.Value = DatosRecibo1.CuerpoRecibo(0).COLUMNA_IMPORTE
            Ditamano.Value = DatosRecibo1.CuerpoRecibo(0).TAMANO_LETRA
            iiavance.Value = DatosRecibo1.CuerpoRecibo(0).AVANCE
        Catch ex As Exception

        End Try
    End Sub

End Class
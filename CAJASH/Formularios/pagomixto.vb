Public Class pagomixto

    Public totalapagar As Decimal = 0
    Public suma As Decimal = 0
    Public codigopredominante As String = "01"
    Private frmpagoefe As FrmPago
    Public Sub New(formulario As FrmPago)

        frmpagoefe = formulario
        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub

    Private Sub iiefectivo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles iiefectivo.KeyPress
        If e.KeyChar = Chr(13) Then
            txtobservacionefectivo.FocusHighlightColor = Color.Yellow
            txtobservacionefectivo.Select()
        End If
    End Sub

    Private Sub txtobservacionefectivo_KeyDown(sender As Object, e As KeyEventArgs) Handles txtobservacionefectivo.KeyDown
        If e.KeyCode = 13 Then
            ' IIcheque.FocusHighlightColor = Color.Yellow
            IIcheque.Select()
        End If
    End Sub

    Private Sub IIcheque_KeyPress(sender As Object, e As KeyPressEventArgs) Handles IIcheque.KeyPress
        If e.KeyChar = Chr(13) Then
            txtobservacioncheque.FocusHighlightColor = Color.Yellow
            txtobservacioncheque.Select()
        End If
    End Sub

    Private Sub txtobservacioncheque_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtobservacioncheque.KeyPress
        If e.KeyChar = Chr(13) Then
            iitransferencia.Select()
        End If
    End Sub

    Private Sub iitransferencia_KeyPress(sender As Object, e As KeyPressEventArgs) Handles iitransferencia.KeyPress
        If e.KeyChar = Chr(13) Then
            txtobservaciontransferencia.FocusHighlightColor = Color.Yellow
            txtobservaciontransferencia.Select()
        End If
    End Sub

    Private Sub txtobservaciontransferencia_KeyDown(sender As Object, e As KeyEventArgs) Handles txtobservaciontransferencia.KeyDown
        If e.KeyCode = 13 Then
            iitarjetadecredito.Select()
        End If
    End Sub

    Private Sub iitarjetadecredito_KeyPress(sender As Object, e As KeyPressEventArgs) Handles iitarjetadecredito.KeyPress
        If e.KeyChar = Chr(13) Then
            txttarjetacredito.FocusHighlightColor = Color.Yellow
            txttarjetacredito.Select()
        End If
    End Sub

    Private Sub txttarjetacredito_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txttarjetacredito.KeyPress
        If e.KeyChar = Chr(13) Then
            iitarjetadedebito.Select()
        End If
    End Sub

    Private Sub iitarjetadedebito_KeyPress(sender As Object, e As KeyPressEventArgs) Handles iitarjetadedebito.KeyPress
        If e.KeyChar = Chr(13) Then
            txttrjetadebito.FocusHighlightColor = Color.Yellow
            txttrjetadebito.Select()
        End If
    End Sub

    Private Sub txttrjetadebito_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txttrjetadebito.KeyPress
        btningresar.Select()
    End Sub

    Private Sub btningresar_Click(sender As Object, e As EventArgs) Handles btningresar.Click
        If lbltotalapagar.Text <> lblsuma.Text Then
            MessageBox.Show("Tus numeros no dan la suma esperada")
            Exit Sub
        End If
        codigopredominante = "01"
        Dim cantidadpredominante As Decimal = Val(iiefectivo.Text)
        If Val(IIcheque.Text) > cantidadpredominante Then
            codigopredominante = "02"
            cantidadpredominante = Val(IIcheque.Text)
        End If
        If Val(iitransferencia.Text) > cantidadpredominante Then
            codigopredominante = "03"
            cantidadpredominante = Val(iitransferencia.Text)
        End If
        If Val(iitarjetadecredito.Text) > cantidadpredominante Then
            codigopredominante = "04"
            cantidadpredominante = Val(iitarjetadecredito.Text)
        End If
        If Val(iitarjetadedebito.Text) > cantidadpredominante Then
            codigopredominante = "28"
            cantidadpredominante = Val(iitarjetadedebito.Text)
        End If
        Try
            frmpagoefe.cmbforma.SelectedValue = codigopredominante
            frmpagoefe.efectivo = Val(iiefectivo.Text)

            frmpagoefe.cheque = Val(IIcheque.Text)
            frmpagoefe.transferencia = Val(iitransferencia.Text)
            frmpagoefe.credito = Val(iitarjetadecredito.Text)
            frmpagoefe.debito = Val(iitarjetadedebito.Text)

            frmpagoefe.obsefectivo = txtobservacionefectivo.Text
            frmpagoefe.obscheque = txtobservacioncheque.Text
            frmpagoefe.obstransferencia = txtobservaciontransferencia.Text
            frmpagoefe.obscredito = txttarjetacredito.Text
            frmpagoefe.obsdebito = txttrjetadebito.Text
            frmpagoefe.mixto = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try



        Close()
    End Sub

    Private Sub btnsalir_Click(sender As Object, e As EventArgs) Handles btnsalir.Click
        Close()
    End Sub

    Private Sub pagomixto_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lbltotalapagar.Text = totalapagar.ToString("C")
        iiefectivo.Text = totalapagar
        sumar()
    End Sub

    Public Sub sumar()
        suma = Val(iiefectivo.Text) + Val(iitransferencia.Text) + Val(IIcheque.Text) + Val(iitarjetadecredito.Text) + Val(iitarjetadedebito.Text)
        lblsuma.Text = suma.ToString("C")
        If lblsuma.Text = lbltotalapagar.Text Then
            PictureBox1.BackColor = Color.Green
        Else
            PictureBox1.BackColor = Color.Red
        End If
    End Sub

    Private Sub iiefectivo_TextChanged(sender As Object, e As EventArgs) Handles iiefectivo.TextChanged
        sumar()
    End Sub

    Private Sub IIcheque_TextChanged(sender As Object, e As EventArgs) Handles IIcheque.TextChanged
        sumar()
    End Sub

    Private Sub iitransferencia_TextChanged(sender As Object, e As EventArgs) Handles iitransferencia.TextChanged
        sumar()
    End Sub

    Private Sub iitarjetadecredito_TextAlignChanged(sender As Object, e As EventArgs) Handles iitarjetadecredito.TextAlignChanged
        sumar()
    End Sub

    Private Sub iitarjetadedebito_TextChanged(sender As Object, e As EventArgs) Handles iitarjetadedebito.TextChanged, IIcheque.TabIndexChanged
        sumar()
    End Sub
End Class
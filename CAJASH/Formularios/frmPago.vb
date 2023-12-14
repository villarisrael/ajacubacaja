Public Class FrmPago

    Public pago As frmPagoefectivo
    Public efectivo As Decimal = 0
    Public cheque As Decimal
    Public transferencia As Decimal
    Public credito As Decimal
    Public debito As Decimal
    Public obsefectivo As String
    Public obscheque As String
    Public obstransferencia As String
    Public obscredito As String
    Public obsdebito As String
    Public totalapagar As Decimal = 0
    Public mixto As Boolean = False
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal pagefe As frmPagoefectivo)
        InitializeComponent()

        pago = pagefe
        efectivo = pago.totalapagar

    End Sub
    Private Sub btncancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Close()
    End Sub

    Private Sub FrmPago_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim basem As New base
        basem.llenarCombo(cmbforma, "select ccodpago, cdespago from fpago")
        basem.desconectar()
        cmbforma.SelectedIndex = 0
    End Sub

    Private Sub btnsalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsalir.Click
        Close()
    End Sub

    Private Sub btningresar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btningresar.Click
        pago.lblFormadepago.Text = cmbforma.SelectedValue
        pago.mixto = mixto
        Close()

    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Dim frmm As New pagomixto(Me)
        frmm.codigopredominante = cmbforma.SelectedValue
        frmm.totalapagar = totalapagar

        frmm.ShowDialog()

    End Sub
End Class

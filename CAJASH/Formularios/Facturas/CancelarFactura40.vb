Public Class CancelarFactura40

    Dim UUIDCancelar As String
    Dim IdFactura As Integer
    Dim x As New base

    Public Sub New(uuidP As String, idFac As Integer)
        ' Initialize without a course
        InitializeComponent()

        UUIDCancelar = uuidP
        IdFactura = idFac


    End Sub



    Private Sub CancelarFactura40_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        TxtFolioF.Enabled = False
        LblFolioF.Enabled = False


        x.conectar()
        x.llenarCombo(ComboBoxCancelar, "DescripcionCancelacion", "SELECT DescripcionCancelacion FROM motivos_cancelacionSAT")


    End Sub

    Private Sub btnCancelarSAT_Click(sender As Object, e As EventArgs) Handles btnCancelarSAT.Click

        Dim motivoCancela As String = obtenerCampo("select ClaveCancelacion from motivos_cancelacionsat where DescripcionCancelacion = '" & ComboBoxCancelar.Text & "' ", "ClaveCancelacion")
        Dim resultado As String

        Try

            If motivoCancela = "01" Then
                If TxtFolioF.Text = "" Then
                    MessageBox.Show("NO HAS ESCRITO EL NUEVO FOLIO FISCAL")
                    Return
                Else
                    Dim objCancelar As New CLSFACTURA
                    resultado = objCancelar.Cancela40(UUIDCancelar, TxtFolioF.Text, motivoCancela, IdFactura)

                End If

            Else

                Dim objCancelar As New CLSFACTURA
                'resultado = objCancelar.Cancela40(UUIDCancelar, TxtFolioF.Text, motivoCancela, IdFactura)

                ''''''''''''''''''''''''''''''
                resultado = objCancelar.Cancela40(UUIDCancelar, TxtFolioF.Text, motivoCancela, IdFactura)

            End If

            If resultado = "" Then
                MessageBox.Show("LA FACTURA NO SE CANCELO")
            Else
                Dim sqlAcuse As String = "update Encfac set Acuse_Cancelacion = '" & resultado & "' where idENCFAC = " & IdFactura
                Ejecucion(sqlAcuse)
                MessageBox.Show("Este es el acuse de cancelación del SAT: \n" & resultado)
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try

        Close()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Close()
    End Sub

    Private Sub ComboBoxCancelar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxCancelar.SelectedIndexChanged
        If ComboBoxCancelar.SelectedIndex = 0 Then
            TxtFolioF.Enabled = False
            LblFolioF.Enabled = False
        End If

        If ComboBoxCancelar.SelectedIndex = 1 Then
            MessageBox.Show("POR FAVOR ESCRIBE EL FOLIO FISCAL QUE SUSTITUIRA AL FOLIO FISCAL A CANCELAR")
            TxtFolioF.Enabled = True
            LblFolioF.Enabled = True
        End If

        If ComboBoxCancelar.SelectedIndex = 2 Then
            TxtFolioF.Enabled = False
            LblFolioF.Enabled = False
        End If

        If ComboBoxCancelar.SelectedIndex = 3 Then
            TxtFolioF.Enabled = False
            LblFolioF.Enabled = False
        End If

        If ComboBoxCancelar.SelectedIndex = 4 Then
            TxtFolioF.Enabled = False
            LblFolioF.Enabled = False
        End If
    End Sub
End Class
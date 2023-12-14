Public Class frmfolio

    Private Sub btnaceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaceptar.Click
        My.Settings.folio = iinuevofolio.Value
        My.Settings.Save()
        Close()

    End Sub

    Private Sub btnsalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsalir.Click
        Close()
    End Sub

    Private Sub frmfolio_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        iinuevofolio.Text = My.Settings.folio
    End Sub
End Class

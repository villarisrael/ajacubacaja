Public Class AñadeFecha


    Private _AñadeFecha As String

    Public Property AñadeFecha() As String
        Get
            Return _AñadeFecha
        End Get
        Set(ByVal value As String)
            _AñadeFecha = value
        End Set
    End Property
    Private Sub btnaceptar_Click(sender As Object, e As EventArgs) Handles btnaceptar.Click
        If fecha.Text <> "" Then
            _AñadeFecha = fecha.Text
            Me.Close()
        Else
            MessageBox.Show("Introduzca la fecha por favor", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End If
    End Sub

    Private Sub AñadeFecha_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.Close()
    End Sub
End Class
Imports DevComponents.DotNetBar

Public Class FrmShowMeses
    '  Dim BD As New base()
    Public cuenta As String
    Public tarifa As String
    Public coleccion As Collection

    



    Private Sub FrmShowMeses_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Cargar()
    End Sub


    'Public Sub Cargar()

    'End Sub



    Private Sub ButtonX3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonX3.Click

        Dim i As Integer
        For i = 0 To DataGridView1.RowCount - 1
            DataGridView1.Rows(i).Cells(2).Value = txtPromedio.Text

        Next

    End Sub

    Private Sub ButtonX1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancelar.Click
        For i = 0 To DataGridView1.RowCount - 1
            DataGridView1.Rows.RemoveAt(0)

        Next
        coleccion.Clear()
        Close()
    End Sub

    Private Sub ButtonX2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonX2.Click
        Dim cole As New Collection
        For i = 1 To DataGridView1.Rows.Count
            Dim registro As ClsRegistrolectura
            registro = coleccion(i)
            registro.Consumocobrado = DataGridView1.Rows(i - 1).Cells(2).Value
            cole.Add(registro)
        Next
        coleccion = cole
        Close()
    End Sub

    ' Private Sub DataGridView1_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGridView1.MouseEnter
    '' SuperTooltipInfo type describes Super-Tooltip
    'Dim superTooltip As New SuperTooltipInfo()
    'superTooltip.HeaderText = "ELIMINAR LECTURA ESTIMADA"
    'superTooltip.BodyText = "Para <strong>Eliminar una Lectura Estimada</strong> solo Selecione la Fila y Precione la Tecla <strong> SUPR </strong> "
    ''superTooltip.FooterText = "My footer text"

    '' Assign tooltip to a control or DotNetBar component
    'SuperTooltip1.SetSuperTooltip(DataGridView1, superTooltip)

    '' To remove tooltip from a control or component use
    ''SuperTooltip1.SetSuperTooltip(textBoxX1, Nothing)
    'End Sub
End Class

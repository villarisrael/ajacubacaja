Imports System.Data.Odbc

Public Class ConveniosPagos
    Dim idConvenio As Integer
    Public fechaaPI As Date = Now
    Public cliente As Integer
    Public Sub DatosCon(cuentaCliente As Integer)
        cliente = cuentaCliente
        Dim CONSULTA As String
        Try
            CONSULTA = "select cuenta,  concepto, vencimiento, round(total,2) as monto, resta, if (pagado=0, ""No"", ""Si"") as Pagado FROM otrosconceptos where cuenta =" & cuentaCliente & " and id_concepto= '" & My.Settings.claveConvenio & "' and resta <> 0"
            llenaGrid(dataConv, CONSULTA)
            dataConv.Columns("Cuenta").Width = 70
            dataConv.Columns("concepto").Width = 100
            dataConv.Columns("vencimiento").Width = 100
            dataConv.Columns("monto").Width = 100
            dataConv.Columns("resta").Width = 100
            dataConv.Columns("pagado").Width = 80
            dataConv.AllowUserToAddRows = False
            dataConv.Refresh()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Public totaldeudaotros As Double = 0
    Public totaldeudaiva As Double = 0
    Public Listadeconceptos As New Collection

    Public fecha As String

    Private Sub btnAplicar_Click(sender As Object, e As EventArgs) Handles btnAplicar.Click
        fechaaPI = DateTimePicker1.Value

        Dim CONSULTA As String
        Try
            CONSULTA = "select cuenta,  concepto, vencimiento, round(total,2) as monto, resta, if (pagado=0, ""No"", ""Si"") as Pagado FROM otrosconceptos where cuenta =" & cliente & " and id_concepto= '" & My.Settings.claveConvenio & "' and vencimiento <='" & UnixDateFormat(fechaaPI) & "' and resta <> 0"
            Dim x As New base
            llenaGrid(dataConv, CONSULTA)
            dataConv.Columns("Cuenta").Width = 70
            dataConv.Columns("concepto").Width = 100
            dataConv.Columns("vencimiento").Width = 100
            dataConv.Columns("monto").Width = 100
            dataConv.Columns("resta").Width = 100
            dataConv.Columns("pagado").Width = 80

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Class
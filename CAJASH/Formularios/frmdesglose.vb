Public Class frmdesglose
    Public control As Clscontrolpago
    Public deque As String

    Private Sub frmdesglose_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Hide()
        End If
    End Sub

    Private Sub frmdesglose_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '''''
        Dtgdesglose.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Me.Dtgdesglose.BackgroundColor = Color.White
        '''''
        lbldeque.Text = "<b><font size=""+6""><i>Desglose de</i><font color=""#B02B2C""> " & deque & "</font></font></b>"
        Try
        Select deque.ToUpper
                Case "CONSUMO"
                    For i = 1 To control.desgloseconsumo.Count
                        If control.EsFijo = True Then
                            Dim objeto As clsunidadmes
                            objeto = control.desgloseconsumo.Item(i)

                            Dtgdesglose.Rows.Add(objeto.mes, objeto.periodo, objeto.total, objeto.total - objeto.totalcondescuento, objeto.totalcondescuento, objeto.totaliva, objeto.totalconiva)

                        Else
                            Dim objeto As ClsRegistrolectura

                            objeto = control.desgloseconsumo.Item(i)
                            If objeto.Tipo = "CONSUMO" Or objeto.Tipo = "ANTICIPO" Then
                                Dtgdesglose.Rows.Add(objeto.Mes, objeto.Periodo, objeto.Total, objeto.Total - objeto.Totalcondescuento, objeto.Totalcondescuento, objeto.totaliva, objeto.totalconiva)
                            End If

                        End If


                    Next
                Case "ALCANTARILLADO"
                    For i = 1 To control.desglosealcantarillado.Count
                        If control.EsFijo = True Then
                            Dim objeto As clsunidadmes
                            objeto = control.desglosealcantarillado.Item(i)
                            Dtgdesglose.Rows.Add(objeto.mes, objeto.periodo, objeto.total, objeto.total - objeto.totalcondescuento, objeto.totalcondescuento, objeto.totaliva, objeto.totalconiva)
                        Else
                            Dim objeto As ClsRegistrolectura
                            objeto = control.desglosealcantarillado.Item(i)

                            Dtgdesglose.Rows.Add(objeto.Mes, objeto.Periodo, objeto.Total, objeto.Total - objeto.Totalcondescuento, objeto.Totalcondescuento, objeto.totaliva, objeto.totalconiva)
                        End If


                    Next
                Case "SANEAMIENTO"
                    For i = 1 To control.desglosesaneamiento.Count
                        If control.EsFijo = True Then
                            Dim objeto As clsunidadmes
                            objeto = control.desglosesaneamiento.Item(i)
                            Dtgdesglose.Rows.Add(objeto.mes, objeto.periodo, objeto.total, objeto.total - objeto.totalcondescuento, objeto.totalcondescuento, objeto.totaliva, objeto.totalconiva)
                        Else
                            Dim objeto As ClsRegistrolectura
                            objeto = control.desglosesaneamiento.Item(i)
                            Dtgdesglose.Rows.Add(objeto.Mes, objeto.Periodo, objeto.Total, objeto.Total - objeto.Totalcondescuento, objeto.Totalcondescuento, objeto.totaliva, objeto.totalconiva)
                        End If


                    Next
                Case "RECARGO"
                    For i = 1 To control.desgloserecargo.Count
                        'If control.EsFijo = True Then
                        Dim objeto As clsunidadmes
                        objeto = control.desgloserecargo.Item(i)
                        Dtgdesglose.Rows.Add(objeto.mes, objeto.periodo, objeto.total, objeto.total - objeto.totalcondescuento, objeto.totalcondescuento, objeto.totaliva, objeto.totalconiva)
                        'Else
                        'Dim objeto As ClsRegistrolectura
                        'objeto = control.desgloserecargo.Item(i)
                        'Dtgdesglose.Rows.Add(objeto.Mes, objeto.Periodo, objeto.Total, objeto.Total - objeto.Totalcondescuento, objeto.Totalcondescuento, objeto.totaliva, objeto.totalconiva)
                        'End If


                    Next

                Case "REZAGO"
                    For i = 1 To control.desgloserezago.Count
                        If control.EsFijo = True Then
                            Dim objeto As clsunidadmes
                            objeto = control.desgloserezago.Item(i)
                            Dtgdesglose.Rows.Add(objeto.mes, objeto.periodo, objeto.total, objeto.total - objeto.totalcondescuento, objeto.totalcondescuento, objeto.totaliva, objeto.totalconiva)
                        Else
                            Dim objeto As ClsRegistrolectura
                            objeto = control.desgloserezago.Item(i)
                            If objeto.Tipo.ToUpper = "REZAGO" Then
                                Dtgdesglose.Rows.Add(objeto.Mes, objeto.Periodo, objeto.Total, objeto.Total - objeto.Totalcondescuento, objeto.Totalcondescuento, objeto.totaliva, objeto.totalconiva)
                            End If

                        End If


                    Next

            End Select
        Catch ex As Exception

        End Try

    End Sub
End Class

Imports System.Data.Odbc

Public Class clscuota
    Public cuotas As New Hashtable
    Public Sub llena(ByVal tarifa As String)
        Try
            vaciar()
            Dim cadena As String = "select periodo as ano, pago_agua as tarifa  from cuofij where tarifa ='" & tarifa & "' order by periodo"
            Dim rs As OdbcDataReader
            Dim bas As New base

            bas.conectar()
            rs = bas.consultasql(cadena)

            Do While (rs.Read)
                cuotas.Add(rs("ano"), rs("tarifa"))
            Loop

            bas.conexion.Dispose()


        Catch ex As Exception
            '  MessageBox.Show(ex.Message)
        End Try

    End Sub
    Public Sub llena(ByVal tarifa As String, ByVal campo As String)
        vaciar()
        Dim cadena As String = "select " & campo & "," & My.Settings.campoano & " from cuofij where " & My.Settings.campotarifa & " ='" & tarifa & "' order by " & My.Settings.campoano
        Dim rs As OdbcDataReader
        Dim bas As New base

        bas.conectar()
        rs = bas.consultasql(cadena)

        Do While (rs.Read)
            cuotas.Add(rs(My.Settings.campoano), rs(campo))
        Loop
        bas.conexion.Close()
    End Sub

    Public Sub vaciar()
        cuotas.Clear()
    End Sub

End Class

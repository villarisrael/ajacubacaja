Public Class Clsrecargo

    Public recargo As Double
    Public valorderecargo As Double = My.Settings.Porcentajederecargo
    Public cantidadconrecargo As Double
    Public collection As New Collection
    Public esmedido As Boolean = True
    Public tarifa As String
    Public pordescuento As Double = 0
    Public periodoscondescuento As Integer = 0
    Public pagocondescuento As Double = 0
    Public descontartodoslosperiodos As Boolean = False
    Public llevaiva As Boolean
    Public pagodeiva As Double

    Dim cuo As New clscuota



    Public Sub calcular(ByVal consumo As Collection, alcantarillado As Collection, ByVal tipo As String)
        collection.Clear()

        Dim meses As Integer
        meses = consumo.Count
        If descontartodoslosperiodos = True Then
            periodoscondescuento = meses
        End If
        'MessageBox.Show(meses)


        Dim acumulador As Double = 0
        Dim acumuladorcondescuento As Double = 0

        For i = consumo.Count To 1 Step -1

            Dim mesperiodo As String
            Dim anoperiodo As String
            Dim montomes As Double


            Dim objeto1 As Object

            If TypeName(consumo.Item(i)) = "clsunidadmes" Then
                Dim objecto1 As clsunidadmes = DirectCast(consumo.Item(i), clsunidadmes)

            Else
                Dim objecto1 As ClsRegistrolectura = DirectCast(consumo.Item(i), ClsRegistrolectura)

            End If


            Dim montoalcan As Decimal = 0
            Try

                montoalcan = alcantarillado.Item(i).Total
            Catch ex As Exception
                montoalcan = 0
            End Try

            objeto1 = consumo.Item(i)
            mesperiodo = objeto1.Mes
            anoperiodo = CStr(objeto1.Periodo)
            montomes = objeto1.Totalcondescuento '+ montoalcan
            'End If
            Dim trabajo As New clsfechas
            '       Dim cadenafecha As String = "01/" & trabajo.valornummes(mesperiodo) & "/" & anoperiodo
            Dim cadenafecha As String = anoperiodo & "/" & trabajo.valornummes(mesperiodo) & "/01"
            Dim fecha As Date
            Date.TryParse(cadenafecha, fecha)

            Dim porcrecargo As Double
            If valorderecargo > 0 Then
                porcrecargo = valorderecargo / 100
            Else
                porcrecargo = 0
            End If
            Dim mesestranscurridos As Integer

            '    -1 si se desea que el mes pasado no paga recargos 
            '   - 0 si desea que el mes pasado genere recargos
            If esmedido Then
                mesestranscurridos = DateDiff(DateInterval.Month, fecha, Now) - 1
            Else
                mesestranscurridos = DateDiff(DateInterval.Month, fecha, Now) - 1
            End If


            '

            If mesestranscurridos <= 0 Then
                mesestranscurridos = 0
            End If
            Dim concepto As New clsunidadmes

            concepto.mes = mesperiodo
            concepto.periodo = anoperiodo
            concepto.numerodemes = mesestranscurridos
            concepto.recargo = mesestranscurridos * porcrecargo
            concepto.total = montomes * concepto.recargo


            'If (My.MySettings.Default.TopeRecargo > 0) Then

            '    'If concepto.total > montomes Then
            '    '    concepto.total = montomes
            '    'End If

            'End If


            acumulador = acumulador + concepto.total

            If i <= periodoscondescuento Then
                If pordescuento > 0 Then
                    concepto.descuento = concepto.total * (pordescuento / 100)
                    concepto.totalcondescuento = concepto.total - concepto.descuento
                End If
            Else
                concepto.descuento = 0
                concepto.totalcondescuento = concepto.total

            End If

            If concepto.totalcondescuento < 0.1 Then
                concepto.totalcondescuento = 0
            End If


            concepto.siniva()


            pagodeiva = 0

            acumuladorcondescuento = acumuladorcondescuento + concepto.totalcondescuento

            If concepto.totalcondescuento > 0 Then
                collection.Add(concepto, i)
            End If


        Next

        recargo = acumulador
        pagocondescuento = acumuladorcondescuento


    End Sub
End Class

   
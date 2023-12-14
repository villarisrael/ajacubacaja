Public Class Clsrecargo

    Public recargo As Double
    Public valorderecargo As Double = 3
    Public cantidadconrecargo As Double
    Public collection As New Collection
    'Public tarifa As Integer
    Public tarifa As String
    Public pordescuento As Double = 0
    Public periodoscondescuento As Integer = 0
    Public pagocondescuento As Double = 0
    Public descontartodoslosperiodos As Boolean = False
    Public llevaiva As Boolean
    Public pagodeiva As Double

    Dim cuo As New clscuota



    Public Sub calcular(ByVal consumo As Collection, ByVal tipo As String)
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
            If tipo = "FIJO" Then

                Dim objeto1 As New clsunidadmes

                objeto1 = consumo.Item(i)
                mesperiodo = objeto1.mes
                anoperiodo = CStr(objeto1.periodo)
                montomes = objeto1.totalcondescuento
            Else
                Dim objeto1 As New ClsRegistrolectura
                objeto1 = consumo.Item(i)
                mesperiodo = objeto1.Mes
                anoperiodo = CStr(objeto1.Periodo)
                montomes = objeto1.Totalcondescuento
            End If
            Dim trabajo As New clsfechas
            Dim cadenafecha As String = "01/" & trabajo.valornummes(mesperiodo) & "/" & anoperiodo
            Dim fecha As Date
            Date.TryParse(cadenafecha, fecha)

            Dim porcrecargo As Double
            If valorderecargo > 0 Then
                porcrecargo = valorderecargo / 100
            Else
                porcrecargo = 0
            End If

            Dim mesestranscurridos As Integer = DateDiff(DateInterval.Month, fecha, Now) - 1
            If mesestranscurridos <= 0 Then
                mesestranscurridos = 0
            End If
            Dim concepto As New clsunidadmes

            concepto.mes = mesperiodo
            concepto.periodo = anoperiodo
            concepto.numerodemes = mesestranscurridos
            concepto.recargo = mesestranscurridos * porcrecargo
            concepto.total = montomes * concepto.recargo


            If (My.MySettings.Default.TopeRecargo > 0) Then

                If concepto.total > montomes Then
                    concepto.total = montomes
                End If

            End If


            acumulador = acumulador + concepto.total

            If i < periodoscondescuento Then
                If pordescuento > 0 Then
                    concepto.descuento = concepto.total * (pordescuento / 100)
                    concepto.totalcondescuento = concepto.total - concepto.descuento
                End If
            Else
                concepto.descuento = 0
                concepto.totalcondescuento = concepto.total

            End If

            If llevaiva Then
                concepto.coniva()
            Else
                concepto.siniva()
            End If

            pagodeiva = pagodeiva + concepto.totaliva

            acumuladorcondescuento = acumuladorcondescuento + concepto.totalcondescuento


            collection.Add(concepto, i)


        Next

        recargo = acumulador
        pagocondescuento = acumuladorcondescuento


    End Sub

End Class

   
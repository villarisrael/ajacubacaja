Imports MySql.Data.MySqlClient
Public Class clsporcfijo
    Public fechainicial As Date = Now()
    Public fechafinal As Date = Now()
    Public montopago As Double = 0
    Public tarifa As Integer = 1
    Public collection As New Collection
    Public cuo As New clscuota
    Public pordescuento As Double = 0
    Public periodoscondescuento As Integer = 0
    Public pagocondescuento As Double = 0
    Public descontartodoslosperiodos As Boolean = False
    Public periodosadeudados As Integer

    Public Function restainicial() As Integer
        Dim numeromes As Integer
        numeromes = fechainicial.Month
        Return 12 - numeromes
    End Function
    Public Function restafinal() As Integer
        Return fechafinal.Month

    End Function

    Public Sub calculapago()

        If fechainicial > fechafinal Then
            montopago = 0
            Exit Sub
        End If

        Dim meses As Integer
        meses = DateDiff(DateInterval.Month, fechainicial, fechafinal)
        'MessageBox.Show(meses)
        periodosadeudados = meses

        If descontartodoslosperiodos = True Then
            periodoscondescuento = meses
        End If

        cuo.llena(tarifa)

        Dim contadormeses As Integer = Month(fechainicial)
        Dim contadorperiodos As Integer = Year(fechainicial)
        Dim trabajoconfecha As New clsfechas
        Dim acumulador As Double = 0
        Dim acumuladorcondescuento As Double = 0
        Dim funcionesf As New clsfechas


        contadormeses += 1



        For i = meses - 1 To 0 Step -1

            If contadormeses = 13 Then
                contadormeses = 1
                contadorperiodos = contadorperiodos + 1
            End If
            Dim objeto As New clsunidadmes
            objeto.numerodemes = i

            objeto.cuota = cuo.cuotas(contadorperiodos)
            objeto.mes = trabajoconfecha.valorcadenames(contadormeses)
            objeto.periodo = contadorperiodos
            '  objeto.total = cuo.cuotas(contadorperiodos)
           

                objeto.total = cuo.cuotas(contadorperiodos)
            
            acumulador = acumulador + objeto.total

            If i < periodoscondescuento Then
                If pordescuento > 0 Then
                    objeto.descuento = objeto.total * (pordescuento / 100)
                    objeto.totalcondescuento = objeto.total - objeto.descuento
                End If
            Else
                objeto.descuento = 0
                objeto.totalcondescuento = objeto.total

            End If

            acumuladorcondescuento = acumuladorcondescuento + objeto.totalcondescuento


            collection.Add(objeto, i)
            contadormeses = contadormeses + 1

        Next

        pagocondescuento = acumuladorcondescuento
        montopago = acumulador
        If montopago < 0 Then
            montopago = 0
        End If
    End Sub


End Class



Public Class Clscobrofijo


    Public Porcentajedecobro As Integer = 3
    Public Pago As Double
    Public collection As New Collection
    'Public tarifa As Integer
    Public tarifa As String
    Public pordescuento As Double = 0
    Public periodoscondescuento As Integer = 0
    Public pagocondescuento As Double = 0
    Public descontartodoslosperiodos As Boolean = False
    Public cobroaporcentaje As Boolean = False
    Public campodecobro As String = ""
    Public llevaiva As Boolean
    Public pagodeiva As Double

    Public cuenta As Integer
    Dim cuo As New clscuota



    Public Sub calcular(ByVal fechaini As Date, ByVal fechafinal As Date)

        Dim meses As Integer
        meses = DateDiff(DateInterval.Month, fechaini, fechafinal)
        If descontartodoslosperiodos = True Then
            periodoscondescuento = meses
        End If
        'MessageBox.Show(meses)
        If campodecobro <> "" Then
            cuo.llena(tarifa, campodecobro)
        Else
            cuo.llena(tarifa)
        End If

        Dim contadormeses As Integer = Month(fechaini)
        Dim contadorperiodos As Integer = Year(fechaini)
        Dim trabajoconfecha As New clsfechas
        Dim acumulador As Double = 0
        Dim acumuladorcondescuento As Double = 0


        contadormeses += 1



        For i = meses - 1 To 0 Step -1
            If contadormeses = 13 Then
                contadormeses = 1
                contadorperiodos = contadorperiodos + 1
            End If

            Dim objeto As New clsunidadmes

            If Porcentajedecobro > 0 Then
                objeto.recargo = Porcentajedecobro / 100
            Else
                objeto.recargo = 0
            End If
            objeto.numerodemes = i
            objeto.cuota = cuo.cuotas(contadorperiodos)
            objeto.mes = trabajoconfecha.valorcadenames(contadormeses)
            objeto.periodo = contadorperiodos


            objeto.total = cuo.cuotas(contadorperiodos)

            If contadorperiodos > Year(Now) Then
                Dim noseporquelorepito As IDataReader = ConsultaSql("select * from usuario inner join descuentos on usuario.cuenta=" & cuenta & " and usuario.iddescuento=descuentos. iddescuento ").ExecuteReader()
                If (noseporquelorepito.Read()) Then
                    If (noseporquelorepito("npctdsct") > 0) Then
                        objeto.descuento = objeto.total * (noseporquelorepito("npctdsct") / 100)
                        objeto.total = objeto.total - (objeto.total * (noseporquelorepito("npctdsct") / 100))
                        objeto.totalcondescuento = objeto.total
                    End If
                End If
            End If


            If contadorperiodos = Year(Now) - 1 And contadormeses = 12 And Now.Month = 1 Then
                Dim noseporquelorepito As IDataReader = ConsultaSql("select * from usuario inner join descuentos on usuario.cuenta=" & cuenta & " and usuario.iddescuento=descuentos. iddescuento ").ExecuteReader()
                If (noseporquelorepito.Read()) Then
                    If (noseporquelorepito("npctdsct") > 0) Then
                        objeto.descuento = objeto.total * (noseporquelorepito("npctdsct") / 100)
                        objeto.total = objeto.total - (objeto.total * (noseporquelorepito("npctdsct") / 100))
                        objeto.totalcondescuento = objeto.total
                    End If
                End If
            ElseIf contadorperiodos = Year(Now) And contadormeses >= Now.Month - 1 Then
                Dim noseporquelorepito As IDataReader = ConsultaSql("select * from usuario inner join descuentos on usuario.cuenta=" & cuenta & " and usuario.iddescuento=descuentos. iddescuento ").ExecuteReader()
                If (noseporquelorepito.Read()) Then
                    If (noseporquelorepito("npctdsct") > 0) Then
                        objeto.descuento = objeto.total * (noseporquelorepito("npctdsct") / 100)
                        objeto.total = objeto.total - (objeto.total * (noseporquelorepito("npctdsct") / 100))
                        objeto.totalcondescuento = objeto.total
                    End If
                End If
            End If
            'If cobroaporcentaje Then
            objeto.total = objeto.total * 0.25 'objeto.recargo
            'Else
            'objeto.total = cuo.cuotas(contadorperiodos)
            'End If

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

            If llevaiva Then
                objeto.coniva()
            Else
                objeto.siniva()
            End If

            pagodeiva = pagodeiva + Math.Round(objeto.totaliva, 2)

            acumuladorcondescuento = acumuladorcondescuento + objeto.totalcondescuento


            collection.Add(objeto, i)
            contadormeses = contadormeses + 1

        Next

        Pago = acumulador
        pagocondescuento = acumuladorcondescuento


    End Sub
End Class

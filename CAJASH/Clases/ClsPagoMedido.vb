Imports System.Data.Odbc
Imports System.Windows.Forms
Public Class ClsPagoMedido

    'Public fechainicial As String = Date.Now().ToString("MMM")
    'Public fechafinal As String = Date.Now().ToString("MMM")
    Public fechainicial As Date = Now()
    Public fechafinal As Date = Now()
    Public montopago As Double = 0
    Public tarifa As String
    'Public tarifa As Integer = 1
    Public cuenta As Integer



    Public collection As New Collection
    Public collectionconsumo As New Collection
    Public collectionrezago As New Collection



    Public lecturasgeneradas As New Collection
    Private tabladelecturas As New Hashtable
    Public pordescuento As Double = 0
    Public periodoscondescuento As Integer = 0
    Public pagocondescuento As Double = 0
    Public pagodeiva As Double = 0
    Public descontartodoslosperiodos As Boolean = False
    Public periodosadeudados As Integer
    Public periodo As String = ""
    Public Memoria As Boolean = False
    Public fechas As New clsfechas()


    Public periodoconsumo As String = ""
    Public periodorezago As String = ""


    Public montopagorezago As Double = 0
    Public pagocondescuentorezago As Double = 0
    Public totaldescuentopesos As Double = 0

    Public llevaiva As Boolean

    Public Function restainicial() As Integer
        Dim numeromes As Integer
        numeromes = fechainicial.Month - 1
        Return 12 - numeromes
    End Function
    Public Function restafinal() As Integer
        Return fechafinal.Month

    End Function

    Public Sub calculapago(Optional ByVal pagoconanticipo As Boolean = False)
        lecturasgeneradas.Clear()
        Dim mescadena As String
        Dim fecopera As New clsfechas
        Dim cadena As String
        '  mescadena = fechafinal
        mescadena = fecopera.valorcadenames(fechafinal.Month)



        mescadena = acompletacero(mescadena, 2)
        Dim meses As Integer

        If Memoria = True Then
            'cadena = "select mes,an_per,consumocobrado,lectura,lectant,consumoMedidos(consumocobrado,usuario.tarifa) AS MONTO,valornummes(mes,an_per) as ordenado,usuario.tarifa as tarifa  from lecturas,usuario where lecturas.cuenta= usuario.cuenta and usuario.cuenta =" & cuenta & " and pagado=0 order by ordenado "
            '   cadena = "select mes,an_per,consumocobrado,lectura,lectant,consumoMedidos(consumocobrado,usuario.tarifa,an_per) AS MONTO,valornummes(mes,an_per) as ordenado,usuario.tarifa as tarifa  from lecturas,usuario where lecturas.cuenta= usuario.cuenta and usuario.cuenta =" & cuenta & " and pagado=0 order by ordenado "
            cadena = "select mes,an_per,consumocobrado,lectura,lectant, MONTO,valornummes(mes,an_per) as ordenado,usuario.tarifa as tarifa  from lecturas,usuario where lecturas.cuenta= usuario.cuenta and usuario.cuenta =" & cuenta & " and pagado=0 and valornummes(mes,an_per)<= valornummes('" & mescadena & "'," & fechafinal.Year & " ) order by ordenado "
        Else
            'cadena = "select mes,an_per,consumocobrado,lectura,lectant,consumoMedidosSin(consumocobrado,usuario.tarifa) AS MONTO,valornummes(mes,an_per) as ordenado,usuario.tarifa as tarifa from lecturas,usuario where lecturas.cuenta= usuario.cuenta and usuario.cuenta =" & cuenta & " and pagado=0 order by ordenado " modificado para cobro medido por alos 17/01/2015
            cadena = "select mes,an_per,consumocobrado,lectura,lectant, MONTO,valornummes(mes,an_per) as ordenado,usuario.tarifa as tarifa from lecturas,usuario where lecturas.cuenta= usuario.cuenta and usuario.cuenta =" & cuenta & " and pagado=0 and valornummes(mes,an_per)<= valornummes('" & mescadena & "'," & fechafinal.Year & " ) order by ordenado "
        End If


        'cadena = "select mes,an_per,consumocobrado,lectura,lectant,consumoMedidos(consumocobrado,usuario.tarifa,an_per) AS MONTO,valornummes(mes,an_per) as ordenado from lecturas,usuario where lecturas.cuenta= usuario.cuenta and usuario.cuenta =" & cuenta & " and pagado=0 AND valornummes(MES,an_per)<= valornummes('" & fecopera.valorcadenames(fechainicial.Month) & "'," & fechainicial.Year & ") order by ordenado "


        Dim rs As OdbcDataReader
        Dim bas As New base

        bas.conectar()
        rs = bas.consultasql(cadena)
        Try
            Dim primero As Boolean = True

            While (rs.Read())
                If primero Then
                    fechainicial = CDate(rs("an_per") & "-" & NumeroMes(rs("mes")) & "-01")
                    primero = False
                End If
                meses = meses + 1
            End While

        Catch ex As Exception
            montopago = 0
            pagocondescuento = 0
            pagodeiva = 0
            Exit Sub
        End Try

        ''  bas.conexion.Dispose()


        bas.conectar()
        rs = bas.consultasql(cadena)

        periodosadeudados = meses

        If descontartodoslosperiodos = True Then
            periodoscondescuento = meses
        End If

        'Dim contadormeses As Integer = Month(fechainicial)
        Dim contadormeses As Integer
        contadormeses = restainicial()
        Dim contadorperiodos As Integer
        contadorperiodos = fechafinal.Month
        'Dim contadorperiodos As Integer = Year(fechainicial)
        Dim trabajoconfecha As New clsfechas
        Dim acumulador As Double = 0
        Dim acumuladorcondescuento As Double = 0
        Dim funcionesf As New clsfechas


        contadormeses += 1
        Dim i As Integer = meses
        Do While (rs.Read)
            Dim objeto As New ClsRegistrolectura
            objeto.Consumo = Int(rs("consumocobrado"))
            Try
                objeto.Lectura_Actual = Int(rs("lectura"))
            Catch ex As Exception
                objeto.Lectura_Actual = 0
            End Try
            Try
                objeto.Lectura_Anterior = Int(rs("Lectant"))
            Catch ex As Exception
                objeto.Lectura_Anterior = 0
            End Try
            Try
                objeto.Total = rs("Monto")
            Catch ex As Exception
                objeto.Total = 0
            End Try
            'objeto.Situacion = (rs("situacion"))




            If contadormeses = 13 Then
                contadormeses = 1
                contadorperiodos = contadorperiodos + 1
            End If

            objeto.Numeroperiodo = i
            i -= 1
            objeto.Mes = rs("mes")
            objeto.Periodo = rs("an_per")
            '   acumulador = acumulador + objeto.Total

            If i < periodoscondescuento Then
                If pordescuento > 0 Then

                    'Select Case TIPODESCUENTO
                    '    Case "SOBRE TOTAL"
                    objeto.Descuento = objeto.Total * (pordescuento / 100)
                    '    Case "SOBRE MINIMO"
                    '        Dim descuento As Decimal = obtenerCampo("select consumomedidos(0,""" & tarifa & """," & objeto.Periodo & ") as minimo", "minimo") * (pordescuento / 100)
                    '        objeto.Descuento = descuento
                    'End Select







                    objeto.Totalcondescuento = objeto.Total - objeto.Descuento

                End If
            Else
                objeto.Descuento = 0
                objeto.Totalcondescuento = objeto.Total

            End If


            If llevaiva Then
                objeto.coniva()
            Else
                objeto.siniva()
            End If

            '  pagodeiva = pagodeiva + objeto.totaliva
            acumuladorcondescuento = acumuladorcondescuento + objeto.Totalcondescuento
            Try
                tabladelecturas.Add(objeto.Periodo & CadenaNumeroMes(objeto.Mes), objeto)
            Catch ex As Exception

            End Try


        Loop

        Dim mesdeconsumoactual As String = Mid(cadenames(Now), 1, 3)
        Dim mesdeultimoconsumo As String = cadenames1(Now)

        Dim anoactual As Integer = Year(Now)
        Dim llaveactual As String = anoactual & CadenaNumeroMes(mesdeconsumoactual) 'mesdeconsumoactual & anoactual
        '    Dim ultimallave As String = mesdeconsumoactual & anoactual - 1
        llaveactual = llaveactual - 1
        If llaveactual = Year(Now) & "00" Then
            llaveactual = Year(Now) - 1 & "12"
        End If

        Dim fecharecorrido As Date = fechainicial

        Dim ultimalectura As Integer = 0
        Dim promedio As Integer = 0
        Try
            rs = bas.consultasql("select ultimalectura(" & cuenta & ") as ultimalectura, promedio(" & cuenta & ",3) as promedio")
            Try
                rs.Read()
                Try
                    ultimalectura = rs("ultimalectura")
                Catch ex As Exception

                End Try
                Try
                    promedio = rs("promedio")
                Catch ex As Exception

                End Try
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
        Dim diferencia As Integer = 0
        Dim coleanticipo As New Collection

        acumulador = 0
        acumuladorcondescuento = 0


        '''' ''' para controlar en que parte lo va insertar
        Dim mesdeconsumoa As String = Mid(cadenames(fecharecorrido), 1, 3)
        Dim anoa As Integer = Year(fecharecorrido)
        Dim llaveanterior As String = anoa & CadenaNumeroMes(mesdeconsumoa)

        Dim cual As Date = Date.Now.AddMonths(-1)
        Dim llaveact As String = cual.Year & CadenaNumeroMes(NOMBREDEMES(cual.Month))


        For i = 0 To (DateDiff(DateInterval.Month, fechainicial, fechafinal))
            Dim mesdeconsumo As String = Mid(cadenames(fecharecorrido), 1, 3)
            Dim ano As Integer = Year(fecharecorrido)
            Dim llave As String = ano & CadenaNumeroMes(mesdeconsumo) 'mesdeconsumo & ano

            If tabladelecturas.Contains(llave) Then   ''' la llave existia en la base
                If Val(llave) < Val(llaveact) Then

                    Dim objeto As ClsRegistrolectura = tabladelecturas(llave)
                    objeto.Tipo = "REZAGO"
                    acumulador = acumulador + objeto.Total
                    acumuladorcondescuento = acumuladorcondescuento + objeto.Totalcondescuento
                    collection.Add(objeto, llave)
                End If

                If Val(llave) >= Val(llaveact) Then ' es el pago al corrtiente
                    Dim objeto As ClsRegistrolectura = tabladelecturas(llave)
                    objeto.Tipo = "CONSUMO"
                    acumulador = acumulador + objeto.Total


                    Dim noseporquelorepito As IDataReader = ConsultaSql("select * from usuario inner join descuentos on usuario.cuenta=" & cuenta & " and usuario.iddescuento=descuentos. iddescuento ").ExecuteReader()
                    If (noseporquelorepito.Read()) Then
                        If (noseporquelorepito("npctdsct") > 0) Then
                            objeto.Descuento = objeto.Total * (noseporquelorepito("npctdsct") / 100)
                            objeto.Totalcondescuento = objeto.Total - objeto.Descuento
                        End If

                    End If



                    acumuladorcondescuento = acumuladorcondescuento + objeto.Totalcondescuento
                    collection.Add(objeto, llave)
                End If
            Else 'seran lecturas que no aparezcan en el periodo 
                Dim xconsulta As New base
                xconsulta.conectar()
                Dim xzu As OdbcDataReader
                Try
                    'xzu = xconsulta.consultasql("select * from lecturas where cuenta=" & cuenta & " and mes='" & llave.Substring(0, 3) & "' and an_per=" & llave.Substring(1, 4))
                    xzu = xconsulta.consultasql("select * from lecturas where cuenta=" & cuenta & " and mes='" & NOMBREDEMES3CAR(Val(llave.Substring(4, 2))) & "' and an_per=" & llave.Substring(0, 4))
                    If Not xzu.Read() Then
                        Dim objeto As New ClsRegistrolectura
                        objeto.Consumo = promedio
                        objeto.Consumocobrado = promedio
                        objeto.Lectura_Actual = ultimalectura
                        objeto.Lectura_Anterior = ultimalectura
                        objeto.Mes = mesdeconsumo
                        objeto.Periodo = ano
                        If Val(llave) < Val(llaveactual) Then
                            objeto.Tipo = "REZAGO"
                        End If
                        If Val(llave) = Val(llaveactual) Then
                            objeto.Tipo = "CONSUMO"
                        End If
                        If fecharecorrido.Year = Now.Year And Val(llave) > Val(llaveactual) Then
                            objeto.Tipo = "ANTICIPO"
                        End If

                        coleanticipo.Add(objeto, llave)
                        diferencia = diferencia + 1
                    End If
                    xconsulta.conexion.Dispose()
                Catch ex As Exception

                End Try

            End If
            fecharecorrido = DateAdd(DateInterval.Month, 1, fecharecorrido)  ' incrementa un mes

            llaveanterior = llave
        Next


        If pagoconanticipo Then ' si en caja pusieron calcular debe dejar cobrar el anticipo
            ' caso contrario si estan cargando la cuenta solo mostrara el saldo actual

            Dim monto As Double

            If Memoria = True Then
                'cadena = "select mes,an_per,consumocobrado,lectura,lectant,consumoMedidos(consumocobrado,usuario.tarifa) AS MONTO,valornummes(mes,an_per) as ordenado,usuario.tarifa as tarifa  from lecturas,usuario where lecturas.cuenta= usuario.cuenta and usuario.cuenta =" & cuenta & " and pagado=0 order by ordenado "
                monto = bas.obtenerCampo("select consumomedidos(" & promedio & ",'" & tarifa & "'," & now.Year &") as monto", "monto")
            Else
                'cadena = "select mes,an_per,consumocobrado,lectura,lectant,consumoMedidosSin(consumocobrado,usuario.tarifa) AS MONTO,valornummes(mes,an_per) as ordenado,usuario.tarifa as tarifa from lecturas,usuario where lecturas.cuenta= usuario.cuenta and usuario.cuenta =" & cuenta & " and pagado=0 order by ordenado " modificado para cobro medido por alos 17/01/2015
                monto = bas.obtenerCampo("select consumomedidossin(" & promedio & ",'" & tarifa & "'," & Now.Year & ") as monto", "monto")
            End If

            If diferencia > 0 Then
                Dim xc As New FrmShowMeses
                xc.txtPromedio.Text = promedio
                xc.txtUltLec.Text = ultimalectura
                xc.txtMesAct.Text = cadenames(Now)
                xc.txtNumM.Text = coleanticipo.Count
                For i = 1 To coleanticipo.Count

                    Try
                        Dim registro As ClsRegistrolectura
                        registro = TryCast(coleanticipo(i), ClsRegistrolectura)
                        xc.DataGridView1.Rows.Add(registro.Mes, registro.Periodo, registro.Consumocobrado, monto)
                    Catch ex As Exception

                    End Try




                Next
                xc.coleccion = coleanticipo
                xc.ShowDialog()


                Dim pos As Integer = 0
                For i = 1 To xc.coleccion.Count

                    Try

                        Dim registro As ClsRegistrolectura
                        registro = TryCast(xc.coleccion(i), ClsRegistrolectura)

                        registro.Consumo = 0
                        If Memoria = True Then
                            'cadena = "select mes,an_per,consumocobrado,lectura,lectant,consumoMedidos(consumocobrado,usuario.tarifa) AS MONTO,valornummes(mes,an_per) as ordenado,usuario.tarifa as tarifa  from lecturas,usuario where lecturas.cuenta= usuario.cuenta and usuario.cuenta =" & cuenta & " and pagado=0 order by ordenado "
                            registro.Montocobrado = bas.obtenerCampo("select consumomedidos(" & promedio & ",'" & tarifa & "'," & registro.Periodo & ") as monto", "monto")
                            registro.Total = registro.Montocobrado
                        Else
                            'cadena = "select mes,an_per,consumocobrado,lectura,lectant,consumoMedidosSin(consumocobrado,usuario.tarifa) AS MONTO,valornummes(mes,an_per) as ordenado,usuario.tarifa as tarifa from lecturas,usuario where lecturas.cuenta= usuario.cuenta and usuario.cuenta =" & cuenta & " and pagado=0 order by ordenado " modificado para cobro medido por alos 17/01/2015
                            registro.Montocobrado = bas.obtenerCampo("select consumomedidossin(" & promedio & ",'" & tarifa & "'," & registro.Periodo & ") as monto", "monto")

                        End If


                        '   registro.Montocobrado = bas.obtenerCampo("select consumomedidos(" & registro.Consumocobrado & ",'" & tarifa & "'," & registro.Periodo & ") as monto", "monto")
                        registro.Total = registro.Montocobrado
                        registro.Totalcondescuento = registro.Total

                        Dim noseporquelorepito As IDataReader = ConsultaSql("select * from usuario inner join descuentos on usuario.cuenta=" & cuenta & " and usuario.iddescuento=descuentos. iddescuento ").ExecuteReader()
                        If (noseporquelorepito.Read()) Then
                            If (noseporquelorepito("npctdsct") > 0) Then
                                registro.Descuento = registro.Total * (noseporquelorepito("npctdsct") / 100)
                                registro.Total = registro.Total - (registro.Total * (noseporquelorepito("npctdsct") / 100))
                                registro.Totalcondescuento = registro.Total
                            End If
                        End If



                        If llevaiva = True Then
                            registro.coniva()
                        Else
                            registro.totalconiva = registro.Totalcondescuento
                        End If


                        registro.Consumo = registro.Consumocobrado
                        registro.Montocobrado = registro.Montocobrado

                        acumulador = acumulador + registro.Total
                        acumuladorcondescuento = acumuladorcondescuento + registro.Totalcondescuento
                        lecturasgeneradas.Add(registro, registro.Periodo & CadenaNumeroMes(registro.Mes))

                        ''' aqui vamos a calcular la llave anterir
                        ''' 
                        Dim despuesdelallave As String = llaveante(registro.Periodo & CadenaNumeroMes(registro.Mes))

                        If collection.Contains(despuesdelallave) Then
                            collection.Add(registro, registro.Periodo & CadenaNumeroMes(registro.Mes),, despuesdelallave)
                        Else
                            collection.Add(registro, registro.Periodo & CadenaNumeroMes(registro.Mes))
                        End If


                    Catch ex As Exception

                    End Try


                Next

                xc.Dispose()
            End If

        End If ' fin de calculo del anticipo 


        '''''''''''''''''''''''''''''''''''''''''
        '
        '
        '
        '''''''''''''''''''''''''''''''''''''''''''''
        '
        '

        'bas.conexion.Dispose()

        'pagocondescuento = acumuladorcondescuento
        'montopago = acumulador
        'If montopago < 0 Then
        '    montopago = 0
        'End If



        ''Dim rs As OdbcDataReader
        ''Dim bas As New base

        'bas.conectar()
        'rs = bas.consultasql(cadena)
        'Try
        '    While (rs.Read())
        '        meses = meses + 1
        '    End While

        'Catch ex As Exception
        '    montopago = 0
        '    pagocondescuento = 0
        '    pagodeiva = 0
        '    Exit Sub
        'End Try




        'bas.conectar()
        'rs = bas.consultasql(cadena)

        'periodosadeudados = meses

        'If descontartodoslosperiodos = True Then
        '    periodoscondescuento = meses
        'End If

        ''Dim contadormeses As Integer = Month(fechainicial)
        ''Dim contadormeses As Integer
        ''contadormeses = fechainicial.Month
        ''Dim contadorperiodos As Integer
        ''contadorperiodos = fechafinal.Month
        '''Dim contadorperiodos As Integer = Year(fechainicial)
        ''Dim trabajoconfecha As New clsfechas
        ''Dim acumulador As Double = 0
        ''Dim acumuladorcondescuento As Double = 0
        ''Dim acumuladorrezago As Double = 0
        ''Dim acumuladorcondescuentorezago As Double = 0


        ''Dim funcionesf As New clsfechas


        'contadormeses += 1
        'Dim i As Integer = meses
        'Dim llevaconsumo As Boolean = False
        'Dim llevarezago As Boolean = False
        'Dim posicioninicialconsumo As Integer = -1
        'Dim cadenacomodin As String = ""
        'Dim elprimeroconsumo As Boolean = True
        'Dim elprimerorezago As Boolean = True

        'Do While (rs.Read)


        '    Dim objeto As New ClsRegistrolectura
        '    objeto.Consumo = Int(rs("consumocobrado"))
        '    objeto.Lectura_Actual = Int(rs("lectura"))
        '    objeto.Lectura_Anterior = Int(rs("Lectant"))
        '    'objeto.Situacion = (rs("situacion"))
        '    objeto.Total = Math.Round(rs("Monto"), 2)



        '    If contadormeses = 13 Then
        '        contadormeses = 1
        '        contadorperiodos = contadorperiodos + 1
        '    End If

        '    objeto.Numeroperiodo = i
        '    i -= 1
        '    objeto.Mes = rs("mes")
        '    objeto.Periodo = rs("an_per")





        '    acumulador = acumulador + objeto.Total

        '    If i < periodoscondescuento Then
        '        If pordescuento > 0 Then
        '            objeto.Descuento = Math.Round(objeto.Total * (pordescuento / 100), 2)
        '            objeto.Totalcondescuento = objeto.Total - objeto.Descuento
        '        End If
        '    Else
        '        objeto.Descuento = 0
        '        objeto.Totalcondescuento = objeto.Total

        '    End If


        '    If llevaiva Then
        '        objeto.coniva()
        '    Else
        '        objeto.siniva()
        '    End If

        '    pagodeiva = pagodeiva + objeto.totaliva
        '    If objeto.Periodo = Year(Now) Then
        '        objeto.Tipo = "CONSUMO"
        '        acumulador = acumulador + objeto.Total
        '        acumuladorcondescuento = acumuladorcondescuento + objeto.Totalcondescuento
        '        If elprimeroconsumo Then
        '            cadenacomodin = objeto.Mes & " " & objeto.Periodo
        '            elprimeroconsumo = False
        '        End If
        '        llevaconsumo = True

        '        collectionconsumo.Add(objeto)
        '    Else
        '        llevarezago = True

        '        objeto.Tipo = "REZAGO"
        '        acumuladorrezago += objeto.Total
        '        acumuladorcondescuentorezago += objeto.Totalcondescuento
        '        If elprimerorezago Then
        '            cadenacomodin = objeto.Mes & " " & objeto.Periodo
        '            elprimerorezago = False
        '        End If

        '        collectionrezago.Add(objeto)
        '    End If

        '    collection.Add(objeto, i)
        '    contadormeses = contadormeses + 1
        'Loop


        'If llevaconsumo Then
        '    Dim objeto As ClsRegistrolectura
        '    Dim objeto2 As ClsRegistrolectura
        '    objeto = collectionconsumo.Item(1)
        '    objeto2 = collectionconsumo.Item(collectionconsumo.Count)
        '    periodoconsumo = objeto.Mes & " " & objeto.Periodo & "-" & objeto2.Mes & " " & objeto2.Periodo
        'End If

        'If llevarezago Then
        '    Dim objeto As ClsRegistrolectura
        '    Dim objeto2 As ClsRegistrolectura
        '    objeto = collectionrezago.Item(1)
        '    objeto2 = collectionrezago.Item(collectionrezago.Count)
        '    periodorezago = objeto.Mes & " " & objeto.Periodo & "-" & objeto2.Mes & " " & objeto2.Periodo
        'End If

        bas.conexion.Dispose()
        pagocondescuento = Math.Round(acumuladorcondescuento, 2)
        totaldescuentopesos = Math.Round(acumuladorcondescuento, 2)
        montopago = Math.Round(acumulador, 2)
        '  pagocondescuentorezago = Math.Round(acumuladorcondescuentorezago, 2)
        ' montopagorezago = Math.Round(acumuladorrezago, 2)

        If montopago < 0 Then
            montopago = 0
        End If
    End Sub

    Public Function llaveante(llave As String) As String

        Dim mes, ano As Integer
        ano = Val(llave.Substring(0, 4))
        mes = Val(llave.Substring(4, 2)) - 1
        If mes = 0 Then
            mes = 12
            ano = ano - 1
        End If

        Dim llavea As String = ""
        llavea = ano.ToString()
        Dim me2 As String = ""
        me2 = acompletacero(mes.ToString(), 2)
        llavea += me2
        Return llavea
    End Function

End Class

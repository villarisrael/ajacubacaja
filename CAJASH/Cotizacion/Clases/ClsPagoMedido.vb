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


    Public llevaiva As Boolean

    Public Function restainicial() As Integer
        Dim numeromes As Integer
        numeromes = fechainicial.Month - 1
        Return 12 - numeromes
    End Function
    Public Function restafinal() As Integer
        Return fechafinal.Month

    End Function

    Public Sub calculapago(Optional ByVal _agregar As Boolean = True)
        lecturasgeneradas.Clear()
        Dim mescadena As String
        Dim fecopera As New clsfechas
        Dim cadena As String
        '  mescadena = fechafinal
        mescadena = fecopera.valorcadenames(fechafinal.Month)
        mescadena = acompletacero(mescadena, 2)
        Dim meses As Integer

        If Memoria = True Then
            cadena = "select mes,an_per,consumocobrado,lectura,lectant,consumoMedidos(consumocobrado,usuario.tarifa, an_per) AS MONTO,valornummes(mes,an_per) as ordenado,usuario.tarifa as tarifa  from lecturas,usuario where lecturas.cuenta= usuario.cuenta and usuario.cuenta =" & cuenta & " and pagado=0 order by ordenado "
        Else
            cadena = "select mes,an_per,consumocobrado,lectura,lectant,consumoMedidosSin(consumocobrado,usuario.tarifa, an_per) AS MONTO,valornummes(mes,an_per) as ordenado,usuario.tarifa as tarifa from lecturas,usuario where lecturas.cuenta= usuario.cuenta and usuario.cuenta =" & cuenta & " and pagado=0 order by ordenado "
        End If


        'Dim cadena As String = "select mes,an_per,consumocobrado,lectura,lectant,consumoMedidosSin(consumocobrado,usuario.tarifa) AS MONTO,valornummes(mes,an_per) as ordenado from lecturas,usuario where lecturas.cuenta= usuario.cuenta and usuario.cuenta =" & cuenta & " and pagado=0 AND valornummes(MES,an_per)>= valornummes('" & fecopera.valorcadenames(fechainicial.Month) & "'," & fechainicial.Year & ") order by ordenado "


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
                    objeto.Descuento = Math.Round(objeto.Total * (pordescuento / 100), 2)
                    objeto.Totalcondescuento = Math.Round(objeto.Total - objeto.Descuento, 2)
                End If
            Else
                objeto.Descuento = 0
                objeto.Totalcondescuento = Math.Round(objeto.Total, 2)

            End If


            If llevaiva Then
                objeto.coniva()
            Else
                objeto.siniva()
            End If

            '  pagodeiva = pagodeiva + objeto.totaliva
            'acumuladorcondescuento = acumuladorcondescuento + objeto.Totalcondescuento
            tabladelecturas.Add(objeto.Mes & objeto.Periodo, objeto)

        Loop

        Dim mesdeconsumoactual As String = Mid(cadenames(Now), 1, 3)
        Dim mesdeultimoconsumo As String = cadenames1(Now)

        Dim anoactual As Integer = Year(Now)
        Dim llaveactual As String = mesdeconsumoactual & anoactual
        Dim ultimallave As String = mesdeconsumoactual & anoactual - 1
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
        bas.conexion.Dispose()

        For i = 0 To (DateDiff(DateInterval.Month, fechainicial, fechafinal))
            Dim mesdeconsumo As String = Mid(cadenames(fecharecorrido), 1, 3)
            Dim ano As Integer = Year(fecharecorrido)
            Dim llave As String = mesdeconsumo & ano

            If tabladelecturas.Contains(llave) Then
                If fecharecorrido < Now And llave <> llaveactual And llave <> mesdeultimoconsumo Then ' es un rezago

                    Dim objeto As ClsRegistrolectura = tabladelecturas(llave)
                    objeto.Tipo = "Rezago"
                    acumulador = acumulador + objeto.Total
                    acumuladorcondescuento = acumuladorcondescuento + objeto.Totalcondescuento
                    collection.Add(objeto, llave)
                End If

                If llave = llaveactual Or llave = mesdeultimoconsumo Then ' es el pago al corrtiente
                    Dim objeto As ClsRegistrolectura = tabladelecturas(llave)
                    objeto.Tipo = "Consumo"
                    acumulador = acumulador + objeto.Total
                    acumuladorcondescuento = acumuladorcondescuento + objeto.Totalcondescuento
                    collection.Add(objeto, llave)
                End If
            Else 'seran lecturas que no aparezcan en el periodo
                Dim xconsulta As New base
                xconsulta.conectar()
                Dim xzu As OdbcDataReader
                Try
                    xzu = xconsulta.consultasql("select * from lecturas where cuenta=" & cuenta & " and mes='" & llave.Substring(0, 3) & "' and an_per=" & llave.Substring(3, 4))
                    If Not xzu.Read() Then
                        Dim objeto As New ClsRegistrolectura
                        objeto.Consumo = 0
                        objeto.Consumocobrado = promedio
                        objeto.Lectura_Actual = ultimalectura
                        objeto.Lectura_Anterior = ultimalectura
                        objeto.Mes = mesdeconsumo
                        objeto.Periodo = ano
                        If fecharecorrido < Now And llave <> llaveactual Then
                            objeto.Tipo = "Rezago"
                        End If
                        If llave = llaveactual Then
                            objeto.Tipo = "Consumo"
                        End If
                        If fecharecorrido > Now And llave <> llaveactual Then
                            objeto.Tipo = "Anticipo"
                        End If
                        Dim montoanti As Double
                        bas.conectar()

                        If Memoria = True Then
                            montoanti = bas.obtenerCampo("select consumomedidos(" & promedio & ",'" & tarifa & "'," & objeto.Periodo & ") as monto", "monto")
                        Else
                            montoanti = bas.obtenerCampo("select consumomedidossin(" & promedio & ",'" & tarifa & "'," & objeto.Periodo & ") as monto", "monto")
                        End If

                        objeto.Montocobrado = montoanti

                        bas.conexion.Dispose()
                        coleanticipo.Add(objeto, llave)
                        diferencia = diferencia + 1
                    End If
                    xconsulta.conexion.Dispose()
                Catch ex As Exception

                End Try

            End If
            fecharecorrido = DateAdd(DateInterval.Month, 1, fecharecorrido)  ' incrementa un mes


        Next
        Dim monto As Double


       




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
                    bas.conectar()
                    If Memoria = True Then ' calcula el monto con el que metera el anticipo
                        monto = bas.obtenerCampo("select consumomedidos(" & promedio & ",'" & tarifa & "'," & registro.Periodo & ") as monto", "monto")
                    Else
                        monto = bas.obtenerCampo("select consumomedidossin(" & promedio & ",'" & tarifa & "'," & registro.Periodo & ") as monto", "monto")
                    End If
                    xc.DataGridView1.Rows.Add(registro.Mes, registro.Periodo, registro.Consumocobrado, monto)

                Catch ex As Exception

                End Try




            Next
            bas.conexion.Dispose()
            If _agregar = True Then
                xc.coleccion = coleanticipo
                xc.ShowDialog()

            End If

          

            Dim pos As Integer = 0
            For i = 1 To xc.coleccion.Count

                Try

                    Dim registro As ClsRegistrolectura
                    registro = TryCast(xc.coleccion(i), ClsRegistrolectura)

                    registro.Consumo = 0
                    bas.conexion.Dispose()
                    bas.conectar()

                  

                    If Memoria = True Then
                        registro.Montocobrado = bas.obtenerCampo("select consumomedidos(" & registro.Consumocobrado & ",'" & tarifa & "'," & registro.Periodo & ") as monto", "monto")
                    Else
                        registro.Montocobrado = bas.obtenerCampo("select consumomedidossin(" & registro.Consumocobrado & ",'" & tarifa & "'," & registro.Periodo & ") as monto", "monto")
                    End If



                    bas.conexion.Dispose()
                   
                    registro.Total = registro.Montocobrado
                    ' aqui va el algoritmo con descuento
                    If pordescuento > 0 And registro.Periodo = Year(Now) Then
                        registro.Descuento = Math.Round(registro.Total * (pordescuento / 100), 2)
                        registro.Totalcondescuento = Math.Round(registro.Total - registro.Descuento, 2)
                    Else
                        registro.Totalcondescuento = Math.Round(registro.Total, 2)
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
                    lecturasgeneradas.Add(registro)
                    collection.Add(registro)

                Catch ex As Exception

                End Try


            Next

            xc.Dispose()
        End If



        bas.conexion.Dispose()
        pagocondescuento = Math.Round(acumuladorcondescuento, 2)
        montopago = Math.Round(acumulador, 2)
        If montopago < 0 Then
            montopago = 0
        End If
    End Sub

    
End Class

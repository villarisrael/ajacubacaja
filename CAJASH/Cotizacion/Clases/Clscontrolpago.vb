Imports System.Data.Odbc
Imports System



Public Class Clscontrolpago

    Public Tarifa As String
    'Public Tarifa As Integer
    Public Fechainicio As Date
    Public Fechafinal As Date
    Public Listadeconceptos As New Collection
    Public EsFijo As Boolean
    Public EsMEdido As Boolean
    Public descuentoaconsumo As Double = 0
    Public descuentoaalcantarillado As Double = 0
    Public descuentoasaneamiento As Double = 0
    Public descuentoarecargo As Double = 0
    Public descuentoarezago As Double = 0

    Public periodoscondescuentodeconsumo As Integer = 0
    Public periodoscondescuentodealcantarillado As Integer = 0
    Public periodoscondescuentodesaneamiento As Integer = 0
    Public periodoscondescuentoderecargo As Integer = 0
    Public periodoscondescuentoderezago As Integer = 0

    Public descontartodoslosperiodosdeconsumo As Boolean = False
    Public descontartodoslosperiodosdealcantarillado As Boolean = False
    Public descontartodoslosperiodosdesaneamiento As Boolean = False
    Public descontartodoslosperiodosderecargo As Boolean = False
    Public descontartodoslosperiodosderezago As Boolean = False

    Public periodosadeudados As Integer
    Public Tarifaconiva As Boolean = False
    Public cuenta As Long
    Public periodo As String = ""
    Public periodoconsumo As String = ""
    Public periodorezago As String = ""
    Public desgloseconsumo As New Collection
    Public desglosealcantarillado As New Collection
    Public desglosesaneamiento As New Collection
    Public desgloserecargo As New Collection
    Public desgloserezago As New Collection
    Public desgloseAnticipo As New Collection
    Public desgloselecturas As New Collection

    Public llevaivaconsumo As Boolean = False
    Public llevaivaalcantarillado As Boolean = False
    Public llevaivasaneamiento As Boolean = False
    Public llevaivarecargo As Boolean = False

    Public ConMemoria As Boolean = False


    Public totaldeudaconsumo As Double = 0
    Public totaldeudaalcantarillado As Double = 0
    Public totaldeudasaneamiento As Double = 0
    Public totaldeudaiva As Double = 0
    Public totaldeudarecargos As Double = 0
    Public totaldeudaotros As Double = 0

    Public totaldescuento As Double = 0
    Public Ttotaldescuento As Double = 0

    Public pagosatrasados As Integer = 0

    Public fechas As New clsfechas()
    Public FI As String = ""
    Public FIF As String = ""

    Public valvulista As Integer = 0
    Public saneamiento As Integer = 0
    Public alcantarillado As Integer = 0

    Public credito As Decimal = 0.0
    Private agregar As Boolean = True

    Public Sub calcula(Optional ByVal _agregar As Boolean = True)
        agregar = _agregar
        periodoconsumo = ""
        periodorezago = ""
        totaldeudaconsumo = 0
        totaldeudaalcantarillado = 0
        totaldeudasaneamiento = 0
        totaldeudaiva = 0
        totaldeudarecargos = 0
        totaldeudaotros = 0
        ' LIMPIA LOS DESGOSES
        desgloseconsumo.Clear()
        desglosealcantarillado.Clear()
        desglosesaneamiento.Clear()
        desgloserecargo.Clear()
        desgloserezago.Clear()
        desgloseAnticipo.Clear()
        desgloselecturas.Clear()


        Try
            Listadeconceptos.Clear()
        Catch ex As Exception

        End Try

        Dim cadena As String = "select * from " & My.Settings.tablacuotas & " where id_tarifa ='" & Tarifa & "'"
        Dim rs As OdbcDataReader
        Dim bas As New base

        bas.conectar()
        rs = bas.consultasql(cadena)

        Try
            rs.Read()
            If rs(My.Settings.booleanmedido) = 0 Then
                EsFijo = True
                EsMEdido = False
            Else
                EsFijo = False
                EsMEdido = True
            End If


            If rs("ivaacon") = 0 Then
                llevaivaconsumo = False
            Else
                llevaivaconsumo = True
            End If

            If rs("ivaalca") = 0 Then
                llevaivaalcantarillado = False
            Else
                llevaivaalcantarillado = True
            End If

            If rs("ivasane") = 0 Then
                llevaivasaneamiento = False
            Else
                llevaivasaneamiento = True
            End If

            If rs("ivareca") = 0 Then
                llevaivarecargo = False
            Else
                llevaivarecargo = True
            End If

            If rs("Memoria") = 0 Then
                ConMemoria = False
            Else
                ConMemoria = True
            End If

        Catch ex As Exception

        End Try
        Try
            If rs(My.Settings.booleanIVA) <> 0 Then
                Tarifaconiva = True
            End If
        Catch ex As Exception

        End Try
        pagosatrasados = 0
        If EsFijo Then
            calculafijo(rs)

        End If  ' fin de si es fijo en consumo

        If EsMEdido Then
            calculamedido(rs)

        End If  ' fin de si es fijo en consumo

        If periodoconsumo = "" And periodorezago <> "" Then
            periodo = periodorezago
        End If
        If periodorezago = "" And periodoconsumo <> "" Then
            periodo = periodoconsumo
        End If
        If periodoconsumo <> "" And periodorezago <> "" Then
            periodo = Mid(periodorezago, 1, 9) & Mid(periodoconsumo, 10, 10)
        End If

        If My.Settings.cobrarvalvulista = "SI" Then
            calculavalvulista()
        End If
        bas.conexion.Dispose()
        llenaotros()
        rectificaiva()
    End Sub

    Public Sub rectificaiva()
        ' Rectifica el iva en los conceptos
        Dim acumiva As Double = 0

        For index = 1 To Listadeconceptos.Count
            Dim conce As New Clsconcepto
            conce = Listadeconceptos(index)
            If conce.IVA > 0 Then
                conce.IVA = Math.Round((conce.Cantidad * Math.Round(conce.Preciounitario, 2)) * (variable_iva / 100), 2)
            End If
            acumiva += conce.IVA
        Next
        totaldeudaiva = acumiva
    End Sub

    Public Sub calculafijo(ByVal rs As odbcDatareader)


        Dim fechahoy As Date
        Date.TryParse("28/" & Now.Month & "/" & Now.Year, fechahoy)
        fechahoy = fechahoy.AddMonths(-2)     ' -1 si el mes que paso se cobra recargo y se toma como atraso -2 si el mes que paso esta atiempo



        Dim Cadenadeperiodo As String = ""
        Dim consumo As New clspagofijo


        Try
            If Not (fechahoy < Fechainicio) Then
                consumo.fechainicial = fechahoy
            Else
                consumo.fechainicial = Fechainicio
            End If


            If Tarifaconiva Then
                consumo.llevaiva = llevaivaconsumo
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


        consumo.fechafinal = Fechafinal
        consumo.tarifa = Tarifa
        consumo.pordescuento = descuentoaconsumo
        consumo.periodoscondescuento = periodoscondescuentodeconsumo
        consumo.descontartodoslosperiodos = descontartodoslosperiodosdeconsumo
        consumo.calculapago()

        desgloseconsumo = consumo.collection '' hace una copia de cada mes en desglose como una coleccion
        periodosadeudados = consumo.periodosadeudados

        If consumo.montopago > 0 Then
            Dim concepto As New Clsconcepto
            concepto.Clave = My.Settings.Clavedeconsumo
            concepto.Cantidad = 1
            concepto.Concepto = "CONSUMO DE AGUA DEL PERIODO "
            Dim objeto, objeto2 As New clsunidadmes

            Try

                objeto = consumo.collection.Item(1)
                objeto2 = consumo.collection.Item(consumo.collection.Count)
                Cadenadeperiodo = objeto.mes & " " & objeto.periodo & " - " & objeto2.mes & " " & objeto2.periodo
                periodoconsumo = Cadenadeperiodo
                concepto.Concepto += Cadenadeperiodo
            Catch ex As Exception

            End Try
            concepto.Preciounitario = consumo.pagocondescuento
            concepto.calcula()
            '
            totaldescuento = totaldescuento + (consumo.montopago - consumo.pagocondescuento)
            '
            concepto.IVA = consumo.pagodeiva
            concepto.importe = consumo.pagocondescuento
            totaldeudaconsumo += consumo.pagocondescuento ' acumulando el consumo para el evento cierre de mes
            totaldeudaiva += concepto.IVA   ' acumula el iva al cierre
            concepto.Clave = My.Settings.Clavedeconsumo
            pagosatrasados += consumo.collection.Count

            Listadeconceptos.Add(concepto, "Consumo") ' añade el concepto de consumo
        End If ' fin de consumo


        Dim Cadenadeperiodorezago As String = ""
        Dim Rezagoconsumo As New clspagofijo

        If Tarifaconiva Then
            Rezagoconsumo.llevaiva = llevaivaconsumo
        End If



        Rezagoconsumo.fechainicial = Fechainicio
        If Fechafinal > fechahoy Then
            Rezagoconsumo.fechafinal = fechahoy
        Else
            Rezagoconsumo.fechafinal = Fechafinal
        End If
        Rezagoconsumo.tarifa = Tarifa
        Rezagoconsumo.pordescuento = descuentoaconsumo
        Rezagoconsumo.periodoscondescuento = periodoscondescuentodeconsumo
        Rezagoconsumo.descontartodoslosperiodos = descontartodoslosperiodosdeconsumo
        Rezagoconsumo.calculapago()

        desgloserezago = Rezagoconsumo.collection '' hace una copia de cada mes en desglose como una coleccion
        periodosadeudados = Rezagoconsumo.periodosadeudados

        If Rezagoconsumo.montopago > 0 Then

            totaldescuento = totaldescuento + (Rezagoconsumo.montopago - Rezagoconsumo.pagocondescuento)
            Dim concepto As New Clsconcepto
            concepto.Clave = My.Settings.ClavedeRezago
            concepto.Cantidad = 1
            concepto.Concepto = "REZAGO DE AGUA DEL PERIODO "
            Dim objeto, objeto2 As New clsunidadmes

            Try

                objeto = Rezagoconsumo.collection.Item(1)
                objeto2 = Rezagoconsumo.collection.Item(Rezagoconsumo.collection.Count)
                Cadenadeperiodorezago = objeto.mes & " " & objeto.periodo & " - " & objeto2.mes & " " & objeto2.periodo
                periodorezago = Cadenadeperiodorezago
                concepto.Concepto += Cadenadeperiodorezago
            Catch ex As Exception

            End Try
            concepto.Preciounitario = Rezagoconsumo.pagocondescuento
            concepto.calcula()
            '      '
            concepto.IVA = Rezagoconsumo.pagodeiva

            totaldeudaconsumo += Rezagoconsumo.pagocondescuento ' acumulando el consumo para el evento cierre de mes
            totaldeudaiva += concepto.IVA
            concepto.Clave = My.Settings.ClavedeRezago

            pagosatrasados += Rezagoconsumo.collection.Count

            Listadeconceptos.Add(concepto, "Rezago") ' añade el concepto de consumo
        End If '



        Try
            If alcantarillado = 1 Then
                If rs(My.Settings.booleanalcantarillado) <> 0 Then
                    Dim alcantarillado As New Clscobrofijo


                    If Tarifaconiva Then
                        alcantarillado.llevaiva = llevaivaalcantarillado
                    End If

                    If rs(My.Settings.booleancobrarfijoalcantarillado) <> 0 Then ' si va cobrar fijo alcantarillado

                        alcantarillado.tarifa = Tarifa
                        alcantarillado.cobroaporcentaje = False
                        alcantarillado.campodecobro = "pago_alcant"

                        alcantarillado.descontartodoslosperiodos = descontartodoslosperiodosdealcantarillado
                        alcantarillado.pordescuento = descuentoaalcantarillado
                        alcantarillado.periodoscondescuento = periodoscondescuentodealcantarillado

                        alcantarillado.calcular(Fechainicio, Fechafinal)
                    Else

                        alcantarillado.tarifa = Tarifa
                        alcantarillado.cobroaporcentaje = True
                        alcantarillado.Porcentajedecobro = rs(My.Settings.Porcentajedealcantarillado)

                        alcantarillado.descontartodoslosperiodos = descontartodoslosperiodosdealcantarillado
                        alcantarillado.pordescuento = descuentoaalcantarillado
                        alcantarillado.periodoscondescuento = periodoscondescuentodealcantarillado
                        alcantarillado.descontartodoslosperiodos = descontartodoslosperiodosdealcantarillado
                        alcantarillado.calcular(Fechainicio, Fechafinal)

                    End If
                    If alcantarillado.Pago > 0 Then
                        desglosealcantarillado = alcantarillado.collection ' obtiene el desglosado de alcantarillado por ms

                        Dim conceptoalcan As New Clsconcepto
                        conceptoalcan.Clave = My.Settings.Clavedealcantarillado
                        conceptoalcan.Cantidad = 1
                        conceptoalcan.Concepto = "ALCANTARILLADO"
                        conceptoalcan.Preciounitario = alcantarillado.pagocondescuento
                        conceptoalcan.calcula()
                        '

                        '
                        conceptoalcan.IVA = alcantarillado.pagodeiva

                        totaldeudaalcantarillado = conceptoalcan.importe ' acumulado para el cierre
                        totaldeudaiva += conceptoalcan.IVA

                        conceptoalcan.Clave = My.Settings.Clavedealcantarillado
                        Listadeconceptos.Add(conceptoalcan, "Alcantarillado") ' añade el concepto de alcantarillado
                    End If

                End If ' fin de si se cobro alcantarillado
            End If

        Catch ex As Exception

        End Try

        Try
            If saneamiento = 1 Then
                If rs(My.Settings.booleansaneamiento) <> 0 Then
                    Dim saneamiento As New Clscobrofijo

                    If Tarifaconiva Then
                        saneamiento.llevaiva = llevaivasaneamiento
                    End If

                    If rs(My.Settings.booleancobrarfijoalcantarillado) <> 0 Then ' si va cobrar fijo alcantarillado

                        saneamiento.tarifa = Tarifa
                        saneamiento.cobroaporcentaje = False
                        saneamiento.campodecobro = "Saneamiento"

                        saneamiento.descontartodoslosperiodos = descontartodoslosperiodosdesaneamiento
                        saneamiento.pordescuento = descuentoasaneamiento
                        saneamiento.periodoscondescuento = periodoscondescuentodesaneamiento
                        saneamiento.descontartodoslosperiodos = descontartodoslosperiodosdesaneamiento

                    Else

                        saneamiento.tarifa = Tarifa
                        saneamiento.cobroaporcentaje = True
                        saneamiento.Porcentajedecobro = rs(My.Settings.Porcentajedesaneamiento)

                        saneamiento.descontartodoslosperiodos = descontartodoslosperiodosdesaneamiento
                        saneamiento.pordescuento = descuentoasaneamiento
                        saneamiento.periodoscondescuento = periodoscondescuentodesaneamiento
                        saneamiento.descontartodoslosperiodos = descontartodoslosperiodosdesaneamiento


                    End If
                    saneamiento.calcular(Fechainicio, Fechafinal)
                    If saneamiento.Pago > 0 Then
                        desglosesaneamiento = saneamiento.collection
                        Dim conceptosan As New Clsconcepto
                        conceptosan.Clave = My.Settings.clavedesaneamiento
                        conceptosan.Cantidad = 1
                        conceptosan.Concepto = "SANEAMIENTO"
                        conceptosan.Preciounitario = saneamiento.pagocondescuento
                        conceptosan.calcula()
                        '
             
                        conceptosan.IVA = saneamiento.pagodeiva

                        totaldeudasaneamiento = conceptosan.importe ' acumulado para el cierre
                        totaldeudaiva += conceptosan.IVA
                        conceptosan.Clave = My.Settings.clavedesaneamiento

                        Listadeconceptos.Add(conceptosan, "SANEAMIENTO") ' añade el concepto de saneamiento
                    End If

                End If ' fin de si se cobro alcantarillado
            End If

        Catch ex As Exception

        End Try
        Try

            If rs(My.Settings.booleanrecargos) <> 0 Then
                Dim recargos As New Clsrecargo

                If Tarifaconiva Then
                    recargos.llevaiva = llevaivarecargo
                End If

                recargos.descontartodoslosperiodos = descontartodoslosperiodosderecargo
                recargos.periodoscondescuento = periodoscondescuentoderecargo
                recargos.pordescuento = descuentoarecargo

                recargos.calcular(Rezagoconsumo.collection, "FIJO")
                If recargos.recargo > 0 Then
                    desgloserecargo = recargos.collection
                    Dim conceptoreca As New Clsconcepto
                    conceptoreca.Clave = My.Settings.Clavederecargo
                    conceptoreca.Cantidad = 1
                    conceptoreca.Concepto = "RECARGO "
                    conceptoreca.Preciounitario = recargos.pagocondescuento
                    conceptoreca.calcula()
                    '

                    '
                    conceptoreca.IVA = recargos.pagodeiva
                    conceptoreca.Clave = My.Settings.Clavederecargo


                    Listadeconceptos.Add(conceptoreca, "RECARGO") ' añade el concepto de RECARGO


                    totaldeudarecargos = conceptoreca.importe ' acumulado para el cierre
                    totaldeudaiva += conceptoreca.IVA


                End If

            End If ' fin 

        Catch ex As Exception

        End Try
        Ttotaldescuento = totaldescuento
    End Sub

    Public Sub calculamedido(ByVal rs As OdbcDataReader)

        Dim consumo As New ClsPagoMedido
        Dim fechahoy As Date
        Date.TryParse("28/" & Now.Month & "/" & Now.Year, fechahoy)
        'fechahoy = fechahoy.AddMonths(0)
        'fechahoy = fechahoy.AddMonths(-1)

        Dim Cadenadeperiodo As String = ""
        'Dim consumo As New clspagofijo

        Try
            If Tarifaconiva Then
                consumo.llevaiva = llevaivaconsumo
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


        'If (fechahoy.Month >= Fechafinal.Month And fechahoy.Year >= Fechafinal.Year) Or (fechahoy.Month >= Fechafinal.Month And fechahoy.Year >= Fechafinal.Year) Then
        ' If (fechahoy.Month >= Fechafinal.Month And fechahoy.Year >= Fechafinal.Year) Then

        consumo.cuenta = cuenta


        consumo.fechainicial = DateAdd(DateInterval.Month, 0, Fechainicio)
        'consumo.fechafinal = fechahoy 'Fechafinal
        consumo.fechafinal = Fechafinal
        consumo.Memoria = ConMemoria

        consumo.tarifa = Tarifa

        consumo.pordescuento = descuentoaconsumo
        consumo.periodoscondescuento = periodoscondescuentodeconsumo
        consumo.descontartodoslosperiodos = descontartodoslosperiodosdeconsumo
        consumo.calculapago(agregar)

        desgloselecturas = consumo.lecturasgeneradas

        periodosadeudados = consumo.periodosadeudados

        Dim acuconsumo As Double = 0
        Dim acurezago As Double = 0
        Dim posicionconsumo As Integer = 0
        Dim contador As Integer = 1
        Dim acuivaconsumo As Double = 0
        Dim acuivarezago As Double = 0


        totaldescuento = consumo.montopago - consumo.pagocondescuento

        For Each elemento In consumo.collection
            Dim registro As ClsRegistrolectura = elemento

            If registro.Tipo = "Consumo" Or registro.Tipo = "Anticipo" Then
                If posicionconsumo = 0 Then
                    posicionconsumo = contador

                End If
                acuivaconsumo = acuivaconsumo + registro.totaliva
                desgloseconsumo.Add(registro)
                If registro.Tipo = "Anticipo" Then
                    desgloseAnticipo.Add(registro)
                End If
                acuconsumo += registro.Totalcondescuento
            End If
            If registro.Tipo = "Rezago" Then
                acurezago += registro.Totalcondescuento
                desgloserezago.Add(registro)
                acuivarezago += registro.totaliva
            End If
            contador += 1
        Next

        'If consumo.montopago > 0 Then
        '  desgloseconsumo = consumo.collection '' hace una copia de cada mes en desglose como una coleccion
        Dim objeto, objeto2 As New ClsRegistrolectura
        If acuconsumo > 0 Then
            Dim concepto As New Clsconcepto
            concepto.Clave = My.Settings.Clavedeconsumo
            concepto.Cantidad = 1
            concepto.Concepto = "CONSUMO DE AGUA PERIODO "
          
            Try

                objeto = consumo.collection.Item(posicionconsumo)
                objeto2 = consumo.collection.Item(consumo.collection.Count)
                'Cadenadeperiodo = objeto2.Mes & " " & objeto2.Periodo & " - " & objeto.Mes & " " & objeto.Periodo
                Cadenadeperiodo = objeto.Mes & " " & objeto.Periodo & " - " & objeto2.Mes & " " & objeto2.Periodo
                periodoconsumo = Cadenadeperiodo
                concepto.Concepto += Cadenadeperiodo
            Catch ex As Exception

            End Try
            concepto.Preciounitario = acuconsumo
            concepto.importe = acuconsumo
            concepto.IVA = acuivaconsumo

            '            concepto.calcula()
            '           concepto.IVA = consumo.pagodeiva


            totaldeudaconsumo += concepto.importe ' acumulado para el cierre
            totaldeudaiva += concepto.IVA
            concepto.Clave = My.Settings.Clavedeconsumo
            Listadeconceptos.Add(concepto, "Consumo") ' añade el concepto de consumo

        End If
        ' aqui empieza el rezago

        If acurezago > 0 Then
            Dim conceptorez As New Clsconcepto
            conceptorez.Clave = My.Settings.ClavedeRezago
            conceptorez.Cantidad = 1
            conceptorez.Concepto = "REZAGO DEL PERIODO "
            conceptorez.IVA = acuivarezago
            Try

                objeto = consumo.collection.Item(1)
                If posicionconsumo <= 0 Then
                    objeto2 = consumo.collection.Item(consumo.collection.Count)
                Else
                    If posicionconsumo > 1 Then
                        objeto2 = consumo.collection.Item(posicionconsumo - 1)
                    End If
                    End If

                    'Cadenadeperiodo = objeto2.Mes & " " & objeto2.Periodo & " - " & objeto.Mes & " " & objeto.Periodo
                    Cadenadeperiodo = objeto.Mes & " " & objeto.Periodo & " - " & objeto2.Mes & " " & objeto2.Periodo
                    periodorezago = Cadenadeperiodo
                    conceptorez.Concepto += Cadenadeperiodo
            Catch ex As Exception

            End Try
            conceptorez.Preciounitario = acurezago
            conceptorez.importe = acurezago
            '          'conceptorez.calcula()

            'REVISAR POR QUE ??
            'conceptorez.IVA = consumo.pagodeiva


            totaldeudaconsumo += conceptorez.importe ' acumulado para el cierre
            totaldeudaiva += conceptorez.IVA

            Listadeconceptos.Add(conceptorez, "Rezago") ' 

        End If
        ' alcantarillador
        Try
            If alcantarillado = 1 Then
                If rs(My.Settings.booleanalcantarillado) <> 0 Then
                    Dim alcantarillado As New Clscobrofijo

                    If Tarifaconiva Then
                        alcantarillado.llevaiva = llevaivaalcantarillado
                    End If

                    If rs(My.Settings.booleancobrarfijoalcantarillado) <> 0 Then ' si va cobrar fijo alcantarillado

                        alcantarillado.tarifa = Tarifa
                        alcantarillado.cobroaporcentaje = False


                        alcantarillado.descontartodoslosperiodos = descontartodoslosperiodosdealcantarillado
                        alcantarillado.pordescuento = descuentoaalcantarillado
                        alcantarillado.periodoscondescuento = periodoscondescuentodealcantarillado
                        alcantarillado.descontartodoslosperiodos = descontartodoslosperiodosdealcantarillado


                        For i = 1 To consumo.collection.Count
                            Dim objetoalca As New ClsRegistrolectura
                            Dim objetlectura As New ClsRegistrolectura
                            objetlectura = consumo.collection.Item(i)
                            objetoalca.Mes = objetlectura.Mes
                            objetoalca.Periodo = objetlectura.Periodo
                            objetoalca.Numeroperiodo = objetlectura.Numeroperiodo
                            objetoalca.Total = rs(My.Settings.campoalcantarillo)
                            '''' aqui meter algoritmo de descuento

                            objetoalca.Totalcondescuento = rs(My.Settings.campoalcantarillo)
                            If alcantarillado.llevaiva Then
                                objetoalca.totaliva = Math.Round(objetoalca.Totalcondescuento * (variable_iva / 100), 2)
                                alcantarillado.pagodeiva += objetoalca.totaliva
                                objetoalca.totalconiva = objetoalca.Totalcondescuento + objetoalca.totaliva
                            End If
                            alcantarillado.collection.Add(objetoalca)
                            alcantarillado.Pago = alcantarillado.Pago + objetoalca.Totalcondescuento
                        Next



                    Else

                        alcantarillado.tarifa = Tarifa
                        alcantarillado.cobroaporcentaje = False

                        alcantarillado.descontartodoslosperiodos = descontartodoslosperiodosdealcantarillado
                        alcantarillado.pordescuento = descuentoaalcantarillado
                        alcantarillado.periodoscondescuento = periodoscondescuentodealcantarillado
                        alcantarillado.descontartodoslosperiodos = descontartodoslosperiodosdealcantarillado


                        For i = 1 To consumo.collection.Count
                            Dim objetoalca As New ClsRegistrolectura
                            Dim objetlectura As New ClsRegistrolectura
                            objetlectura = consumo.collection.Item(i)
                            objetoalca.Mes = objetlectura.Mes
                            objetoalca.Periodo = objetlectura.Periodo
                            objetoalca.Numeroperiodo = objetlectura.Numeroperiodo
                            objetoalca.Total = Math.Round((rs(My.Settings.Porcentajedealcantarillado) / 100) * objetlectura.Totalcondescuento, 2)
                            '''' aqui meter algoritmo de descuento
                            objetoalca.Descuento = Math.Round(objetoalca.Total * (descuentoaalcantarillado / 100), 2)

                            objetoalca.Totalcondescuento = Math.Round(objetoalca.Total - objetoalca.Descuento, 2)


                            If alcantarillado.llevaiva Then
                                objetoalca.totaliva = Math.Round(objetoalca.Totalcondescuento * (variable_iva / 100), 2)
                                alcantarillado.pagodeiva += objetoalca.totaliva
                                objetoalca.totalconiva = objetoalca.Totalcondescuento + objetoalca.totaliva
                            End If
                            alcantarillado.collection.Add(objetoalca)
                            alcantarillado.Pago = alcantarillado.Pago + objetoalca.Totalcondescuento
                        Next




                    End If
                    If alcantarillado.Pago > 0 Then
                        desglosealcantarillado = alcantarillado.collection ' obtiene el desglosado de alcantarillado por ms

                        Dim conceptoalcan As New Clsconcepto
                        conceptoalcan.Clave = My.Settings.Clavedealcantarillado
                        conceptoalcan.Cantidad = 1
                        conceptoalcan.Concepto = "ALCANTARILLADO"
                        conceptoalcan.Preciounitario = Math.Round(alcantarillado.Pago, 2)
                        conceptoalcan.calcula()
                        conceptoalcan.IVA = Math.Round(alcantarillado.pagodeiva, 2)

                        totaldeudaalcantarillado = conceptoalcan.importe ' acumulado para el cierre
                        totaldeudaiva += conceptoalcan.IVA

                        conceptoalcan.Clave = My.Settings.Clavedealcantarillado

                        Listadeconceptos.Add(conceptoalcan, "Alcantarillado") ' añade el concepto de alcantarillado
                    End If

                End If ' fin de si se cobro alcantarillado
            End If

        Catch ex As Exception

        End Try
        ' aqui comienza el saneamiento

        Try
            If saneamiento = 1 Then
                If rs(My.Settings.booleansaneamiento) <> 0 Then
                    Dim saneamiento As New Clscobrofijo

                    If Tarifaconiva Then
                        saneamiento.llevaiva = llevaivasaneamiento
                    End If


                    If rs(My.Settings.booleancobrarfijosaneamiento) <> 0 Then ' si va cobrar fijo alcantarillado

                        saneamiento.tarifa = Tarifa
                        saneamiento.cobroaporcentaje = False

                        saneamiento.descontartodoslosperiodos = descontartodoslosperiodosdealcantarillado
                        saneamiento.pordescuento = descuentoaalcantarillado
                        saneamiento.periodoscondescuento = periodoscondescuentodealcantarillado
                        saneamiento.descontartodoslosperiodos = descontartodoslosperiodosdealcantarillado
                        saneamiento.calcular(Fechainicio, Fechafinal)


                        For i = 1 To consumo.collection.Count
                            Dim objetoalca As New ClsRegistrolectura
                            Dim objetlectura As New ClsRegistrolectura
                            objetlectura = consumo.collection.Item(i)
                            objetoalca.Mes = objetlectura.Mes
                            objetoalca.Periodo = objetlectura.Periodo
                            objetoalca.Numeroperiodo = objetlectura.Numeroperiodo
                            objetoalca.Total = rs(My.Settings.camposaneamiento)

                            objetoalca.Totalcondescuento = rs(My.Settings.camposaneamiento)

                            '''' aqui meter algoritmo de descuento




                            If saneamiento.llevaiva Then
                                objetoalca.totaliva = Math.Round(objetoalca.Totalcondescuento * (variable_iva / 100), 2)
                                saneamiento.pagodeiva += objetoalca.totaliva
                                objetoalca.totalconiva = objetoalca.Totalcondescuento + objetoalca.totaliva
                            End If


                            saneamiento.collection.Add(objetoalca)
                            saneamiento.Pago = saneamiento.Pago + objetoalca.Totalcondescuento
                        Next


                    Else

                        saneamiento.tarifa = Tarifa
                        saneamiento.cobroaporcentaje = False

                        saneamiento.descontartodoslosperiodos = descontartodoslosperiodosdealcantarillado
                        saneamiento.pordescuento = descuentoaalcantarillado
                        saneamiento.periodoscondescuento = periodoscondescuentodealcantarillado
                        saneamiento.descontartodoslosperiodos = descontartodoslosperiodosdealcantarillado

                        For i = 1 To consumo.collection.Count
                            Dim objetoalca As New ClsRegistrolectura
                            Dim objetlectura As New ClsRegistrolectura
                            objetlectura = consumo.collection.Item(i)
                            objetoalca.Mes = objetlectura.Mes
                            objetoalca.Periodo = objetlectura.Periodo
                            objetoalca.Numeroperiodo = objetlectura.Numeroperiodo
                            objetoalca.Total = (rs(My.Settings.Porcentajedesaneamiento) / 100) * objetlectura.Totalcondescuento



                            '''' aqui meter algoritmo de descuento
                            objetoalca.Descuento = Math.Round(objetoalca.Total * (descuentoasaneamiento / 100), 2)
                            objetoalca.Totalcondescuento = objetoalca.Total - objetoalca.Descuento

                            If saneamiento.llevaiva Then
                                objetoalca.totaliva = Math.Round(objetoalca.Totalcondescuento * (variable_iva / 100), 2)
                                saneamiento.pagodeiva += objetoalca.totaliva
                                objetoalca.totalconiva = objetoalca.Totalcondescuento + objetoalca.totaliva
                            End If

                            objetoalca.Totalcondescuento = Math.Round(objetoalca.Total - objetoalca.Descuento, 2)
                            saneamiento.collection.Add(objetoalca)
                            saneamiento.Pago = saneamiento.Pago + objetoalca.Totalcondescuento
                        Next






                    End If
                    If saneamiento.Pago > 0 Then

                        desglosesaneamiento = saneamiento.collection

                        Dim conceptoalcan As New Clsconcepto
                        conceptoalcan.Clave = My.Settings.clavedesaneamiento
                        conceptoalcan.Cantidad = 1
                        conceptoalcan.Concepto = "SANEAMIENTO"
                        conceptoalcan.Preciounitario = saneamiento.Pago
                        conceptoalcan.calcula()
                        conceptoalcan.IVA = saneamiento.pagodeiva

                        totaldeudasaneamiento = conceptoalcan.importe ' acumulado para el cierre
                        totaldeudaiva += conceptoalcan.IVA
                        conceptoalcan.Clave = My.Settings.clavedesaneamiento

                        Listadeconceptos.Add(conceptoalcan, "Saneamiento") ' añade el concepto de alcantarillado
                    End If

                End If ' fin de si se cobro saneamiento
            End If
        Catch ex As Exception

        End Try

        Try

            If rs(My.Settings.booleanrecargos) <> 0 Then
                Dim recargos As New Clsrecargo

                If Tarifaconiva Then
                    recargos.llevaiva = llevaivarecargo
                End If

                recargos.descontartodoslosperiodos = descontartodoslosperiodosderecargo
                recargos.periodoscondescuento = periodoscondescuentoderecargo
                recargos.pordescuento = descuentoarecargo

                recargos.calcular(consumo.collection, "MEDIDO")
                If recargos.recargo > 0 Then

                    desgloserecargo = recargos.collection
                    Dim conceptoreca As New Clsconcepto
                    conceptoreca.Clave = My.Settings.Clavederecargo
                    conceptoreca.Cantidad = 1
                    conceptoreca.Concepto = "RECARGO "
                    conceptoreca.Preciounitario = recargos.pagocondescuento
                    conceptoreca.calcula()
                    conceptoreca.IVA = recargos.pagodeiva

                    conceptoreca.Clave = My.Settings.Clavederecargo
                    totaldeudarecargos = conceptoreca.importe ' acumulado para el cierre
                    totaldeudaiva += conceptoreca.IVA

                    Listadeconceptos.Add(conceptoreca, "Recargo") ' añade el concepto de saneamiento
                End If

            End If ' fin 
        Catch ex As Exception

        End Try
        Ttotaldescuento = totaldescuento
    End Sub

    Public Sub llenaotros()

        Dim x As New base
        Dim DATOS As OdbcDataReader = x.consultasql("SELECT  CANTIDAD, CONCEPTO, RESTA AS MONTO, conceptoscxc.id_concepto AS CLAVE , otrosconceptos.CLAVE AS CLAVEMOV,conceptoscxc.aplicaiva as aplicaiva FROM otrosconceptos,conceptoscxc WHERE otrosconceptos.CUENTA=" & cuenta & " AND PAGADO=0 AND otrosconceptos.ESTADO<>'CANCELADO' AND conceptoscxc.id_concepto=otrosconceptos.id_concepto")
        totaldeudaotros = 0
        While DATOS.Read
            Dim Xy As New Clsconcepto
            Xy.Cantidad = 1
            Xy.Concepto = DATOS("CONCEPTO")
            If DATOS("APLICAIVA") Then
                Xy.Preciounitario = Math.Round(DATOS("Monto") / (1 + (variable_iva / 100)), 2)
            Else
                Xy.Preciounitario = DATOS("Monto")
            End If

            Xy.Clave = DATOS("CLAVE")
            Xy.CLAVEMOV = DATOS("CLAVEmov")
            Xy.calcula()
            If DATOS("APLICAIVA") Then
                Xy.IVA = Xy.importe * (variable_iva / 100)
            Else
                Xy.IVA = 0
            End If
            totaldeudaotros += Xy.importe ' acumulado para el cierre
            totaldeudaiva += Xy.IVA
            Try
                Listadeconceptos.Add(Xy, "Renglon" & Xy.CLAVEMOV)
            Catch ex As Exception

            End Try


        End While
        x.conexion.Dispose()
    End Sub

    Public Sub calculavalvulista()
        Dim v As New Clsvalvulista
        Dim meses As Integer = desgloseconsumo.Count + desgloserezago.Count

        If meses > 0 Then
            v.id = valvulista
            v.nmeses = meses

            v.calcular()

            Dim con As New Clsconcepto
            con.Cantidad = v.nmeses
            con.Clave = My.Settings.clavevalvulista
            con.Concepto = "PAGO VALVULISTA " + periodo
            con.Preciounitario = v.Preciouni
            con.importe = v.Importe
            con.IVA = Math.Round(v.Importe * (variable_iva / 100), 2)
            con.calcula()
            If con.importe > 0 Then
                Listadeconceptos.Add(con, "PAGO VALVULISTA")
            End If

        End If


    End Sub
End Class

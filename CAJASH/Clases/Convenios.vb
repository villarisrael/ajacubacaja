Imports System.Threading
Module Convenios

    Public porceConsumoActual As Decimal = 0
    Public porceConsumoRezago As Decimal = 0
    Public porceAlcantarilladoActual As Decimal = 0
    Public porceAlcantarilladoRezago As Decimal = 0
    Public porceSaneamientoActual As Decimal = 0
    Public porceSaneamientoRezago As Decimal = 0
    Public porceOtros As Decimal = 0
    Public porceRecargosActual As Decimal = 0
    Public porceRecargosRezago As Decimal = 0
    Public porceIva As Decimal = 0
    Public total As Decimal = 0
    Public ConsumoAgua As Decimal = 0
    Public ConsumoAguaRe As Decimal = 0
    Public ConsumoAlca As Decimal = 0
    Public ConsumoAlcaRe As Decimal = 0
    Public ConsumoSana As Decimal = 0
    Public ConsumoSanaRe As Decimal = 0
    Public ConsumoOtros As Decimal = 0
    Public ConsumoRec As Decimal = 0
    Public ConsumoRecRe As Decimal = 0
    Public ConsumoIva As Decimal = 0
    Dim hoy As Date = Date.Now
    Public Sub Porcentajes(cuentaCliente As Integer, efectivoRecibido As Double)

        Dim cadenaAgua As String
        Dim agua As Decimal
        Dim aguaRe As Decimal
        Dim alca As Decimal
        Dim alcaRe As Decimal
        Dim sana As Decimal
        Dim sanaRe As Decimal
        Dim otros As Decimal
        Dim rec As Decimal
        Dim recRe As Decimal
        Dim iva As Decimal
        ' Dim totalRecargo As Decimal
        cadenaAgua = "select porceConsumoActual, porceConsumoRezago, porceAlcantarilladoActual, porceAlcantarilladoRezago,porceSaneamientoActual, porceSaneamientoRezago, porceOtros, porceRecargosActual, porceRecargosRezago, porceIva from EncConvenio where idcuenta =" & cuentaCliente
        Dim dr As IDataReader = ConsultaSql(cadenaAgua).ExecuteReader()
        dr.Read()
        agua = dr("porceConsumoActual")
        aguaRe = dr("porceConsumoRezago")
        alca = dr("porceAlcantarilladoActual")
        alcaRe = dr("porceAlcantarilladoRezago")
        sana = dr("porceSaneamientoActual")
        sanaRe = dr("porceSaneamientoRezago")
        otros = dr("porceOtros")
        rec = dr("porceRecargosActual")
        recRe = dr("porceRecargosRezago")
        iva = dr("porceIva")


        ConsumoAgua = efectivoRecibido * agua
        ConsumoAguaRe = efectivoRecibido * aguaRe
        ConsumoAlca = efectivoRecibido * alca
        ConsumoAlcaRe = efectivoRecibido * alcaRe
        ConsumoSana = efectivoRecibido * sana
        ConsumoSanaRe = efectivoRecibido * sanaRe
        ConsumoOtros = efectivoRecibido * otros
        ConsumoRec = efectivoRecibido * rec
        ConsumoRecRe = efectivoRecibido * recRe

        'totalRecargo = ConsumoAguaRe + ConsumoAlcaRe + ConsumoSanaRe + ConsumoRecRe
        Dim años As String = Strings.Right(Year(Now), 2)
        Dim añ As Integer = CInt(años)
        ' Dim mes As Double = DateTime.Now.ToString("MM")
        ' Dim mesLetra As String = Thread.CurrentThread.CurrentCulture.DateTimeFormat.MonthNames(mes - 1)

        Dim mesLetra As String = Mid(cadenames(Now), 1, 3)

        Try
            If ConsumoAgua > 0 Then
                Ejecucion("INSERT INTO pago_mes (PERIODO, MES, ANO, CONCEPTO, FECHA, RECIBO, CAJA,SERIE, CUENTA,MONTO, DESCUENTO,MONTOPAGADO) VALUES ('" & CadenaNumeroMes(mesLetra) & añ & "','" & NOMBREDEMES3CAR(hoy.Month) & "'," & Year(Now) & ",'CONSUMO','" & UnixDateFormat(hoy) & "'," & My.Settings.folio + 1 & ",'" & My.Settings.caja & "', '" & My.Settings.serie & "'," & cuentaCliente & "," & ConsumoAgua & "," & 0 & "," & ConsumoAgua & ")")

            End If
            If ConsumoAlca > 0 Then
                Ejecucion("INSERT INTO pago_mes (PERIODO, MES, ANO, CONCEPTO, FECHA, RECIBO, CAJA,SERIE, CUENTA,MONTO, DESCUENTO,MONTOPAGADO) VALUES ('" & CadenaNumeroMes(mesLetra) & añ & "','" & NOMBREDEMES3CAR(hoy.Month) & "'," & Year(Now) & ",'ALCANTARILLADO','" & UnixDateFormat(hoy) & "'," & My.Settings.folio + 1 & ",'" & My.Settings.caja & "', '" & My.Settings.serie & "'," & cuentaCliente & "," & ConsumoAlca & "," & 0 & "," & ConsumoAlca & ")")

            End If

            If ConsumoSana > 0 Then
                Ejecucion("INSERT INTO pago_mes (PERIODO, MES, ANO, CONCEPTO, FECHA, RECIBO, CAJA,SERIE, CUENTA,MONTO, DESCUENTO,MONTOPAGADO) VALUES ('" & CadenaNumeroMes(mesLetra) & añ & "','" & NOMBREDEMES3CAR(hoy.Month) & "'," & Year(Now) & ",'SANEAMIENTO','" & UnixDateFormat(hoy) & "'," & My.Settings.folio + 1 & ",'" & My.Settings.caja & "', '" & My.Settings.serie & "'," & cuentaCliente & "," & ConsumoSana & "," & 0 & "," & ConsumoSana & ")")

            End If
            If ConsumoRec > 0 Then
                Ejecucion("INSERT INTO pago_mes (PERIODO, MES, ANO, CONCEPTO, FECHA, RECIBO, CAJA,SERIE, CUENTA,MONTO, DESCUENTO,MONTOPAGADO) VALUES ('" & CadenaNumeroMes(mesLetra) & añ & "','" & NOMBREDEMES3CAR(hoy.Month) & "'," & Year(Now) & ",'RECARGO','" & UnixDateFormat(hoy) & "'," & My.Settings.folio + 1 & ",'" & My.Settings.caja & "', '" & My.Settings.serie & "'," & cuentaCliente & "," & ConsumoRec & "," & 0 & "," & ConsumoRec & ")")

            End If

            If ConsumoAguaRe > 0 Then
                Ejecucion("INSERT INTO pago_mes (PERIODO, MES, ANO, CONCEPTO, FECHA, RECIBO, CAJA,SERIE, CUENTA,MONTO, DESCUENTO,MONTOPAGADO) VALUES ('" & CadenaNumeroMes(mesLetra) & añ & "','" & NOMBREDEMES3CAR(hoy.Month) & "'," & Year(Now) - 1 & ",'CONSUMO','" & UnixDateFormat(hoy) & "'," & My.Settings.folio + 1 & ",'" & My.Settings.caja & "', '" & My.Settings.serie & "'," & cuentaCliente & "," & ConsumoAgua & "," & 0 & "," & ConsumoAgua & ")")

            End If
            If ConsumoAlcaRe > 0 Then
                Ejecucion("INSERT INTO pago_mes (PERIODO, MES, ANO, CONCEPTO, FECHA, RECIBO, CAJA,SERIE, CUENTA,MONTO, DESCUENTO,MONTOPAGADO) VALUES ('" & CadenaNumeroMes(mesLetra) & añ & "','" & NOMBREDEMES3CAR(hoy.Month) & "'," & Year(Now) - 1 & ",'ALCANTARILLADO','" & UnixDateFormat(hoy) & "'," & My.Settings.folio + 1 & ",'" & My.Settings.caja & "', '" & My.Settings.serie & "'," & cuentaCliente & "," & ConsumoAlca & "," & 0 & "," & ConsumoAlca & ")")

            End If

            If ConsumoSanaRe > 0 Then
                Ejecucion("INSERT INTO pago_mes (PERIODO, MES, ANO, CONCEPTO, FECHA, RECIBO, CAJA,SERIE, CUENTA,MONTO, DESCUENTO,MONTOPAGADO) VALUES ('" & CadenaNumeroMes(mesLetra) & añ & "','" & NOMBREDEMES3CAR(hoy.Month) & "'," & Year(Now) - 1 & ",'SANEAMIENTO','" & UnixDateFormat(hoy) & "'," & My.Settings.folio + 1 & ",'" & My.Settings.caja & "', '" & My.Settings.serie & "'," & cuentaCliente & "," & ConsumoSana & "," & 0 & "," & ConsumoSana & ")")

            End If
            If ConsumoRecRe > 0 Then
                Ejecucion("INSERT INTO pago_mes (PERIODO, MES, ANO, CONCEPTO, FECHA, RECIBO, CAJA,SERIE, CUENTA,MONTO, DESCUENTO,MONTOPAGADO) VALUES ('" & CadenaNumeroMes(mesLetra) & añ & "','" & NOMBREDEMES3CAR(hoy.Month) & "'," & Year(Now) - 1 & ",'RECARGO','" & UnixDateFormat(hoy) & "'," & My.Settings.folio + 1 & ",'" & My.Settings.caja & "', '" & My.Settings.serie & "'," & cuentaCliente & "," & ConsumoRec & "," & 0 & "," & ConsumoRec & ")")

            End If
            If ConsumoOtros > 0 Then
                Ejecucion("INSERT INTO pago_mes (PERIODO, MES, ANO, CONCEPTO, FECHA, RECIBO, CAJA,SERIE, CUENTA,MONTO, DESCUENTO,MONTOPAGADO) VALUES ('" & CadenaNumeroMes(mesLetra) & añ & "','" & NOMBREDEMES3CAR(hoy.Month) & "'," & Year(Now) & ",'OTROS','" & UnixDateFormat(hoy) & "'," & My.Settings.folio + 1 & ",'" & My.Settings.caja & "', '" & My.Settings.serie & "'," & cuentaCliente & "," & ConsumoOtros & "," & 0 & "," & ConsumoOtros & ")")

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    'Public Sub convenio(cuentaCliente As Integer, efectivoRecibido As Double)

    '    Dim CONSULTA As String
    '    Dim veces As Integer
    '    veces = obtenerCampo("select pagos from encconvenio where idcuenta=" & cuentaCliente, "pagos")

    '    Dim ncon As Integer
    '    Dim convenio As String = "select convenio from usuario where cuenta=" & cuentaCliente
    '    Dim dra As IDataReader = ConsultaSql(convenio).ExecuteReader()
    '    dra.Read()

    '    If Not IsDBNull(dra("convenio")) Then
    '        ncon = dra("convenio")
    '    Else
    '        ncon = Nothing

    '    End If

    '    If ncon = 1 Then
    '        Dim resta As Double
    '        Dim adeudo As String = obtenerCampo("select sum(resta) as total from otrosconceptos as d inner join encconvenio  as e on d.idconvenio= e.idencconvenio where idcuenta=" & cuentaCliente, "total")
    '        ' MsgBox(adeudo)
    '        '  Dim obtenvalor() As String = pagoefe.txtefectivo_recibido.Text.Split
    '        'Dim recibido As Integer = CInt(obtenvalor(0))
    '        Dim res As Double = Double.Parse(adeudo)
    '        Dim recibido As Double = efectivoRecibido
    '        Dim pagado As Boolean


    '        If recibido > res Then
    '            res = recibido - res
    '            'MsgBox(adeudo)
    '            CONSULTA = "select fecha, resta, pagado from detconvenio as d inner join encconvenio as e on d.idconvenio=e.idencconvenio where  idcuenta=" & cuentaCliente
    '            Dim dr As IDataReader = ConsultaSql(CONSULTA).ExecuteReader()
    '            For con = 1 To veces

    '                dr.Read()
    '                Dim fechadet As String
    '                Dim montodet As Double

    '                If Not IsDBNull(dr("fecha")) Then
    '                    fechadet = dr("fecha")
    '                Else
    '                    fechadet = Nothing

    '                End If
    '                If Not IsDBNull(dr("resta")) Then
    '                    montodet = dr("resta")
    '                Else
    '                    montodet = Nothing

    '                End If
    '                If Not IsDBNull(dr("pagado")) Then
    '                    pagado = dr("pagado")
    '                Else
    '                    pagado = Nothing

    '                End If

    '                If recibido > montodet And fechadet = fechadet Then
    '                    recibido = recibido - montodet

    '                    resta = 0
    '                    Ejecucion("update detconvenio as d inner join encconvenio as e on d.idconvenio= e.idencconvenio set resta=" & resta & ", pagado= 1 where idcuenta=" & cuentaCliente & " and d.fecha='" & fechadet & "'")
    '                End If
    '                If recibido < montodet And fechadet = fechadet Then
    '                    Throw New Exception("salir del for")
    '                End If
    '            Next

    '        End If

    '        If recibido < res Then
    '            res = res - recibido
    '            'MsgBox(adeudo)
    '            CONSULTA = "select fecha, resta, pagado from detconvenio as d inner join encconvenio as e on d.idconvenio=e.idencconvenio where  idcuenta=" & cuentaCliente
    '            Dim dr As IDataReader = ConsultaSql(CONSULTA).ExecuteReader()
    '            Dim resultado As Double = Double.Parse(efectivoRecibido)
    '            recibido = resultado
    '            Try
    '                For con = 1 To veces

    '                    dr.Read()
    '                    Dim fechadet As String
    '                    Dim montodet As Double
    '                    If Not IsDBNull(dr("fecha")) Then
    '                        fechadet = dr("fecha")
    '                    Else
    '                        fechadet = Nothing

    '                    End If
    '                    If Not IsDBNull(dr("resta")) Then
    '                        montodet = dr("resta")
    '                    Else
    '                        montodet = Nothing

    '                    End If
    '                    If Not IsDBNull(dr("pagado")) Then
    '                        pagado = dr("pagado")
    '                    Else
    '                        pagado = Nothing

    '                    End If

    '                    Try
    '                        If recibido < montodet And fechadet = fechadet Then

    '                            resta = montodet - recibido
    '                            Ejecucion("update detconvenio as d inner join encconvenio as e on d.idconvenio= e.idencconvenio set resta=" & resta & ", pagado= 0 where idcuenta=" & cuentaCliente & " and d.fecha='" & fechadet & "'")
    '                            recibido = 0
    '                            Throw New Exception("ya hice esta")
    '                        End If

    '                        If recibido > montodet And fechadet = fechadet Then
    '                            recibido = recibido - montodet

    '                            resta = 0
    '                            Ejecucion("update detconvenio as d inner join encconvenio as e on d.idconvenio= e.idencconvenio set resta=" & resta & ", pagado= 1 where idcuenta=" & cuentaCliente & " and d.fecha='" & fechadet & "'")
    '                        End If
    '                    Catch ex As Exception

    '                    End Try


    '                    If recibido <= 0 Then
    '                        Throw New Exception("salte del for")
    '                    End If
    '                Next
    '            Catch ex As Exception

    '            End Try

    '        End If
    '    End If
    'End Sub









End Module

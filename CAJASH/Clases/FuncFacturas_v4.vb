Imports MultiFacturasSDK

Module FuncFacturas_v4
    Public Function MakeFileToSendMultifacturas_v4(ByVal control As Clscontrolpago, ByVal _IVA As Double, ByVal _subtotal As Double, ByVal _total As Double, ByVal _serie As String, ByVal _folio As Integer, _FORMAFACTURADO As String, metodopago As String, uso As String, Optional _NOMBRERECEPTOR As String = "PÚBLICO EN GENERAL", Optional _rfcreceptor As String = "XAXX010101000", Optional _EMPRESA As String = "COMISION DE AGUA Y ALCANTARILLADO DEL MUNICIPIO DE ACTOPAN HIDALGO", Optional regimenFiscReceptorP As String = "", Optional domicilioFiscReceptorP As String = "", Optional TipoFacPubGeneral As Boolean = False) As MFSDK

        Dim banderaTipoFactorTasa As Boolean = False
        Dim banderaTipoFactorExento As Boolean = False
        'Dim basem As New base
        'Dim empresa As OdbcDataReader
        'empresa = ConsultaSql("select * from empresa;").ExecuteReader
        ' empresa.Read()
        Dim sdk As MFSDK = New MFSDK()

        sdk = New MFSDK()
        sdk.Iniciales.Add("version_cfdi", "4.0")
        sdk.Iniciales.Add("MODOINI", "DIVISOR")

        sdk.Iniciales.Add("cfdi", "c:\sdk24\timbrados\cfdi_ejemplo_factura4_2.xml")
        sdk.Iniciales.Add("xml_debug", "c:\sdk24\timbrados\sin_timbrar_ejemplo_factura4_2.xml")
        'sdk.Iniciales.Add("produccion", "NO")
        sdk.Iniciales.Add("RESPUESTA_UTF8", "SI")



        Dim emi As MFObject = New MFObject("emisor")

        emi("rfc") = obtenerCampo("select * from empresa ", "cnif")
            emi("nombre") = _EMPRESA
            emi("RegimenFiscal") = My.Settings.Regimen

        Dim receptor As MFObject = New MFObject("receptor")
        receptor("rfc") = _rfcreceptor
        receptor("nombre") = _NOMBRERECEPTOR
        receptor("UsoCFDI") = uso
        receptor("DomicilioFiscalReceptor") = domicilioFiscReceptorP
        receptor("RegimenFiscalReceptor") = regimenFiscReceptorP



        ' Concepto Normal



        Dim conceptos As MFObject = New MFObject("conceptos")

        Dim acuiva As Double = 0
        Dim acusubtotal As Double = 0
        Dim acubase As Double = 0
        Dim acubaseExento As Double = 0

        conceptos.Subnodos.Clear()

        For i = 1 To control.Listadeconceptos.Count
            Dim concepto As Clsconcepto = control.Listadeconceptos.Item(i)

            If concepto.Preciounitario > 0 Then
                Dim concepto0 As MFObject = New MFObject(i.ToString())
                Dim clavesat As String



                'If TipoFacPubGeneral = True Then

                '    clavesat = obtenerCampo("select ClaveSat From conceptoscxc where Descripcion='" & concepto.Concepto & "'", "ClaveSat")

                'Else

                '    clavesat = obtenerCampo("select ClaveSat From conceptoscxc where id_concepto='" & concepto.Clave & "'", "ClaveSat")

                'End If




                'v4.0
                'concepto0("unidad") = "PZA"

                'concepto0("ID") = "15"
                '
                concepto0("Cantidad") = concepto.Cantidad
                concepto0("Descripcion") = concepto.Concepto
                concepto0("ValorUnitario") = concepto.Preciounitario
                concepto0("Importe") = concepto.importe

                concepto0("ClaveProdServ") = concepto.clavesat
                concepto0("ClaveUnidad") = concepto.unidadsat
                concepto0("NoIdentificacion") = "COD" & i.ToString()


                If concepto.IVA > 0 Then
                    concepto0("ObjetoImp") = "02"
                Else
                    concepto0("ObjetoImp") = "01"
                End If







                If concepto.IVA > 0 Then

                    ' Impuestos de conceptos
                    Dim impuestos As MFObject = New MFObject("Impuestos")
                    ' Traslados
                    Dim itras As MFObject = New MFObject("Traslados")
                    Dim itra0 As MFObject = New MFObject(i.ToString())


                    Dim IVA As Decimal = Math.Round(concepto.importe * 0.16, 2)

                    acuiva = acuiva + IVA
                    acubase += Math.Round(concepto.importe, 2)
                    itra0("TasaOCuota") = "0.160000"
                    itra0("Importe") = IVA
                    itra0("TipoFactor") = "Tasa"

                    banderaTipoFactorTasa = True

                    itra0("Base") = Math.Round(concepto.importe, 2)
                    itra0("Impuesto") = "002"


                    itras.AgregaSubnodo(itra0)
                    impuestos.AgregaSubnodo(itras)
                    concepto0.AgregaSubnodo(impuestos)

                    'Else

                    '    itra0("TipoFactor") = "Exento"
                    '    acubaseExento += concepto.importe

                    '    banderaTipoFactorExento = True
                End If




                acusubtotal += Math.Round(concepto.importe, 2)


                conceptos.AgregaSubnodo(concepto0)


            End If
        Next







        Dim total As Decimal = acusubtotal + acuiva
        ' Impuestos
        Dim impuestos3 As MFObject = New MFObject("impuestos")
        _IVA = Math.Round(acuiva, 2)

        ' prueben que si el acumulado del iva es 0 no poner el nodo
        ' o bien que la suma de de todos los iva de los campos sea igual al acumlado del iva
        If banderaTipoFactorTasa = True Then

            impuestos3("TotalImpuestosTrasladados") = acuiva

        End If



        Dim itraslados As MFObject = New MFObject("translados")

        Dim acumulaTipoFactorTasa As Double = 0.0
        Dim acumulaTipoFactorExento As Double = 0.0
        'Dim banderaTipoFactorTasa As Boolean = False
        Dim itraslado0 As MFObject = New MFObject("0")

        'El nodo impuestos translados tiene 3 casos
        '1.- Si hay solo conceptos con TipoFactor = Tasa quiere decir que tienen IVA se agregara el nodo contemplando las bases solo de los conceptos que lleven IVA
        '2.- Si hay conceptos que tengan TipoFactor = Tasa y TasaFactor = Exento, en el nodo solo se van a sumar las bases de los conceptos con TipoFactor = Tasa con el respecto importe de la suma de los IVA
        '3.- Si en los conceptos solo hay TipoFactor = Exento, se crea el nodo sumando las bases de los conceptos, se especifíca que el tipoFactor = Exento y se excluyen las propiedades Importe y TasaOCuota


        If banderaTipoFactorTasa = True And banderaTipoFactorExento = False Then


            itraslado0("Impuesto") = "002"
            itraslado0("Base") = acubase
            itraslado0("Importe") = acuiva
            itraslado0("TasaOCuota") = "0.160000"
            itraslado0("TipoFactor") = "Tasa"

            itraslados.AgregaSubnodo(itraslado0)
            impuestos3.AgregaSubnodo(itraslados)

        ElseIf banderaTipoFactorTasa = True And banderaTipoFactorExento = True Then

            itraslado0("Impuesto") = "002"
            itraslado0("Base") = acubase
            itraslado0("Importe") = acuiva
            itraslado0("TasaOCuota") = "0.160000"
            itraslado0("TipoFactor") = "Tasa"

            itraslados.AgregaSubnodo(itraslado0)
            impuestos3.AgregaSubnodo(itraslados)

        ElseIf banderaTipoFactorTasa = False And banderaTipoFactorExento = True Then

            itraslado0("Impuesto") = "002"
            itraslado0("Base") = acubaseExento
            itraslado0("TipoFactor") = "Exento"

            itraslados.AgregaSubnodo(itraslado0)
            impuestos3.AgregaSubnodo(itraslados)

        End If


        Dim InformacionGlobal As MFObject = New MFObject("InformacionGlobal")
        If TipoFacPubGeneral = True Then

            InformacionGlobal("Periodicidad") = My.Settings.Periodicidad
            InformacionGlobal("Meses") = DateTime.Now.ToString("MM")
            InformacionGlobal("Año") = DateTime.Now.ToString("yyyy")
        End If



        Dim fact As MFObject = New MFObject("factura")
        'fact("condicionesDePago") = "condiciones"
        fact("fecha_expedicion") = DateTime.Now.AddMinutes(-70).ToString("s")
        fact("folio") = _folio
        fact("forma_pago") = _FORMAFACTURADO
        fact("LugarExpedicion") = obtenerCampo("select * from empresa ", "ccodpos")
        fact("metodo_pago") = metodopago
        fact("moneda") = "MXN"
        fact("serie") = _serie
        fact("subtotal") = acusubtotal '
        fact("tipocambio") = "1"
        fact("tipocomprobante") = "I"

        fact("total") = total ' 100.0
        fact("Exportacion") = "01"

        '#NODO [InformacionGlobal]
        'Dim InformacionGlobal As MFObject = New MFObject("InformacionGlobal")
        'InformacionGlobal("Periodicidad") = "02"
        'InformacionGlobal("Meses") = "05"
        'InformacionGlobal("AÃ±o") = "2022"

        sdk.AgregaObjeto(PAC())
        sdk.AgregaObjeto(Conf())
        sdk.AgregaObjeto(fact)

        sdk.AgregaObjeto(emi)
        sdk.AgregaObjeto(receptor)

        If TipoFacPubGeneral = True Then
            sdk.AgregaObjeto(InformacionGlobal)
        End If

        'sdk.AgregaObjeto(InformacionGlobal)
        sdk.AgregaObjeto(conceptos)
        sdk.AgregaObjeto(impuestos3)


        Return sdk


    End Function 'para multifacturas

    Public Function PAC() As MFObject
        Dim p As MFObject = New MFObject("PAC")
        If My.Settings.TimbrarPrueba.ToLower = "si" Then

            p("usuario") = My.Settings.UsuarioMultifacturas
            p("pass") = My.Settings.PassFacturaMultifacturas
            p("produccion") = "NO"
        Else

            p("usuario") = My.Settings.UsuarioMultifacturas
            p("pass") = My.Settings.PassFacturaMultifacturas
            p("produccion") = "SI"
        End If

        Return p
    End Function
    Public Function PAC2() As MFObject
        Dim p As MFObject = New MFObject("PAC")
        If My.Settings.TimbrarPrueba.ToLower = "si" Then

            p("usuario") = My.Settings.UsuarioMultifacturas
            p("pass") = My.Settings.PassFacturaMultifacturas
            p("produccion") = "NO"
        Else

            p("usuario") = My.Settings.UsuarioMultifacturas
            p("pass") = My.Settings.PassFacturaMultifacturas
            p("produccion") = "SI"
        End If
        Return p
    End Function

    Public Function Conf() As MFObject
        Dim con As MFObject = New MFObject("conf")



        con("cer") = My.Settings.CER
            con("key") = My.Settings.KEY
            con("pass") = My.Settings.KeyContrasena


        Return con
    End Function

    Public Function consulta_saldo() As String
        Dim sdk As MFSDK = New MFSDK()
        sdk.Iniciales.Add("SALDO", "SI")
        sdk.AgregaObjeto(PAC2())
        ' Muestras la estructura
        Dim sdkresp As SDKRespuesta
        sdkresp = sdk.Timbrar("C:\sdk2\timbrar32.bat", "C:\sdk2\timbrados\", "factura", False)
        Dim cadenas As String()
        cadenas = sdkresp.Codigo_MF_Numero.Split("=")

        Dim saldo As String = cadenas(2)

        Return saldo
    End Function

    Public Function cancela_factura(UUID As String, RFC As String) As MFSDK
        Dim sdk As MFSDK = New MFSDK()

        sdk.Iniciales.Add("modulo", "cancelacion2018")
        sdk.Iniciales.Add("accion", "cancelar")



        sdk.Iniciales.Add("b64Cer", My.Settings.CER & ".pem")
            sdk.Iniciales.Add("b64Key", My.Settings.KEY & ".pem")
            sdk.Iniciales.Add("password", My.Settings.KeyContrasena)



        sdk.Iniciales.Add("uuid", UUID)
        sdk.Iniciales.Add("rfc", RFC)

        If (My.Settings.TimbrarPrueba.ToUpper = "SI") Then


            sdk.Iniciales.Add("produccion", "NO")
        Else


            sdk.Iniciales.Add("produccion", "SI")
        End If



        ''sdk.Iniciales.Add("cfdi", FXML)
        ''sdk.Iniciales.Add("cancelar", "SI")
        sdk.AgregaObjeto(PAC())
        ''sdk.AgregaObjeto(Conf())
        ' Muestras la estructura
        Return sdk
    End Function

End Module

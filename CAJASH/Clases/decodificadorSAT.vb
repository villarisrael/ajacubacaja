Public Class decodificadorSAT

    Public Function getRegimeng(_regimen As String) As String

        If _regimen = "601" Then

            Return "Regimen Fiscal: 601 General de Ley Personas Morales"
        End If

        If _regimen = "603" Then

            Return "Regimen Fiscal: 603 Personas Morales con Fines no Lucrativos"
        End If

        If _regimen = "605" Then

            Return "Regimen Fiscal: 605 Sueldos y Salarios e Ingresos Asimilados a Salarios"
        End If

        If _regimen = "606" Then

            Return "Regimen Fiscal: 606 Arrendamiento"
        End If

        If _regimen = "607" Then

            Return "Regimen Fiscal: 607 Régimen de Enajenación o Adquisición de Bienes"
        End If

        If _regimen = "608" Then

            Return "Regimen Fiscal: 608 Demás ingresos"
        End If

        If _regimen = "609" Then

            Return "Regimen Fiscal: 609 Consolidación"
        End If

        If _regimen = "610" Then

            Return "Regimen Fiscal: 610 Residentes en el Extranjero sin Establecimiento Permanente en México"
        End If

        If _regimen = "611" Then

            Return "Regimen Fiscal: 611 Ingresos por Dividendos (socios y accionistasthen"
        End If

        If _regimen = "612" Then

            Return "Regimen Fiscal: 612 Personas Físicas con Actividades Empresariales y Profesionales"
        End If

        If _regimen = "614" Then

            Return "Regimen Fiscal: 614 Ingresos por intereses"
        End If

        If _regimen = "615" Then

            Return "Regimen Fiscal: 615 Régimen de los ingresos por obtención de premios"
        End If

        If _regimen = "616" Then

            Return "Regimen Fiscal: 616 Sin obligaciones fiscales"
        End If
        If _regimen = "620" Then

            Return "Regimen Fiscal: 620 Sociedades Cooperativas de Producción que optan por diferir sus ingresos"
        End If

        If _regimen = "621" Then

            Return "Regimen Fiscal: 621 Incorporación Fiscal"
        End If

        If _regimen = "622" Then

            Return "Regimen Fiscal: 622 Actividades Agrícolas, Ganaderas, Silvícolas y Pesqueras"
        End If

        If _regimen = "623" Then

            Return "Regimen Fiscal: 623 Opcional para Grupos de Sociedades"
        End If

        If _regimen = "624" Then

            Return "Regimen Fiscal: 624 Coordinados"
        End If

        If _regimen = "625" Then

            Return "Regimen Fiscal: 625 Régimen de las Actividades Empresariales con ingresos a través de Plataformas Tecnológicas"
        End If

        If _regimen = "626" Then

            Return "Regimen Fiscal: 626 Régimen Simplificado de Confianza"
        End If

        If _regimen = "628" Then

            Return "Regimen Fiscal: 628 Hidrocarburos"
        End If

        If _regimen = "629" Then

            Return "Regimen Fiscal: 629 De los Regímenes Fiscales Preferentes y de las Empresas Multinacionales"
        End If

        If _regimen = "630" Then

            Return "Regimen Fiscal: 630 Enajenación de acciones en bolsa de valores"
        End If


        Return _regimen

    End Function

    Public Function getUso(_uso As String) As String

        If _uso = "G01" Then

            Return "G01: Adquisición de mercancias"
        End If

        If _uso = "G02" Then

            Return "G02: Devoluciones, descuentos o bonificaciones"
        End If

        If _uso = "G03" Then

            Return "G03: Gastos en general"
        End If

        If _uso = "I01" Then

            Return "I01: Construcciones"
        End If

        If _uso = "I02" Then

            Return "I02: Mobilario y equipo de oficina por inversiones"
        End If

        If _uso = "I03" Then

            Return "I03: Equipo de transporte"
        End If

        If _uso = "I04" Then

            Return "I04: Equipo de computo y accesorios"
        End If

        If _uso = "I05" Then

            Return "I05: Dados, troqueles, moldes, matrices y herramental"
        End If

        If _uso = "I06" Then

            Return "I06: Comunicaciones telefónicas"
        End If

        If _uso = "I08" Then

            Return "I08: Otra maquinaria y equipo"
        End If

        If _uso = "D01" Then

            Return "D01: Honorarios médicos, dentales y gastos hospitalarios."
        End If

        If _uso = "D02" Then

            Return "D02: Gastos médicos por incapacidad o discapacidad"
        End If

        If _uso = "D03" Then

            Return "D03: Gastos funerales."
        End If

        If _uso = "G01" Then

            Return "G01: Adquisición de mercancias"
        End If

        If _uso = "D05" Then

            Return "D05: Intereses reales efectivamente pagados por créditos hipotecarios Casa habitaciónthen."
        End If

        If _uso = "D06" Then

            Return "D06: Aportaciones voluntarias al SAR."
        End If

        If _uso = "D07" Then

            Return "D07: Primas por seguros de gastos médicos."
        End If

        If _uso = "G01" Then

            Return "G01: Adquisición de mercancias"
        End If

        If _uso = "D08" Then

            Return "D08: Gastos de transportación escolar obligatoria."
        End If

        If _uso = "D09" Then

            Return "D09: Depósitos en cuentas para el ahorro, primas que tengan como base planes de pensiones."
        End If


        If _uso = "D10" Then

            Return "D10: Pagos por servicios educativos Colegiaturasthen"
        End If

        If _uso = "P01" Then

            Return "P01: Por definir"
        End If

        If _uso = "S01" Then

            Return "S01: Sin efectos fiscales"
        End If

        If _uso = "CP01" Then

            Return "P01: Pagos"
        End If

        If _uso = "CN01" Then

            Return "CN01: Nómina"
        End If
        Return _uso



    End Function

    Public Function getFormapago(_forma As String) As String

        If _forma = "01" Then

            Return "01: Efectivo"
        End If

        If _forma = "02" Then

            Return "02: Cheque nominativo"
        End If

        If _forma = "03" Then

            Return "03: Transferencia electrónica de fondos"
        End If

        If _forma = "04" Then

            Return "04: Tarjeta de crédito"
        End If

        If _forma = "05" Then

            Return "05: Monedero electrónico"
        End If

        If _forma = "06" Then

            Return "06: Dinero electrónico"
        End If

        If _forma = "08" Then

            Return "08: Vales de despensa"
        End If

        If _forma = "12" Then

            Return "12: Dación en pago"
        End If

        If _forma = "13" Then

            Return "13: Pago por subrogación"
        End If

        If _forma = "14" Then

            Return "14: Pago por consignación"
        End If

        If _forma = "15" Then

            Return "15: Condonación"
        End If

        If _forma = "17" Then

            Return "17: Compensación"
        End If

        If _forma = "23" Then

            Return "23: Novación"
        End If

        If _forma = "24" Then

            Return "24: Confusión"
        End If

        If _forma = "25" Then

            Return "25: Remisión de deuda"
        End If

        If _forma = "26" Then

            Return "26: Prescripción o caducidad"
        End If

        If _forma = "01" Then

            Return "01: Efectivo"
        End If

        If _forma = "27" Then

            Return "27: A satisfacción del acreedor"
        End If

        If _forma = "28" Then

            Return "28: Tarjeta de débito"
        End If

        If _forma = "29" Then

            Return "29: Tarjeta de servicios"
        End If

        If _forma = "99" Then

            Return "99: Por definir"
        End If
        Return _forma

    End Function

    Public Function getMetodo(_metodoPago As String) As String

        If _metodoPago = "PUE" Then

            Return "PUE: Pago en una sola exhibición"
        End If

        If _metodoPago = "PIP" Then

            Return "PIP: Pago en parcialidades o diferido"
        End If

        If _metodoPago = "PPD" Then

            Return "PPD: Pago en parcialidades o diferido"
        End If

        Return _metodoPago
    End Function


    Public Function getTipoderelacion(Clave As String) As String



        Dim Valor As String = ""
        If Clave = "01" Then

            Valor = "01 Nota de crédito de los documentos relacionados"
        End If

        If Clave = "02" Then

            Valor = "02 Nota de débito de los documentos relacionados"
        End If

        If Clave = "03" Then

            Valor = "03 Devolución de mercancía sobre facturas o traslados previos"
        End If
        If Clave = "04" Then

            Valor = "04 Sustitución de los CFDI previos"
        End If
        If Clave = "05" Then

            Valor = "05 Traslados de mercancias facturados previamente"
        End If
        If Clave = "06" Then

            Valor = "06 Factura generada por los traslados previos"
        End If
        If Clave = "07" Then

            Valor = "07 CFDI por aplicación de anticipo"
        End If
        Return Valor

    End Function

    Public Function getBanco(RFC As String) As String

        Dim STRBANCO As String = ""
        If RFC = "BNM840515VB1" Then STRBANCO = " BANAMEX"
        If RFC = "BBA830831LJ2" Then STRBANCO = " BBVA BANCOMER"
        If RFC = "BSM970519DU8" Then STRBANCO = " SANTANDER"
        If RFC = "BNE820901682" Then STRBANCO = " BANJERCITO"
        If RFC = "HMI950125KG8" Then STRBANCO = " HSBC"
        If RFC = "BBA940707IE1" Then STRBANCO = " BAJIO"
        If RFC = "IBA950503GTA" Then STRBANCO = " IXE"
        If RFC = "BII931004P61" Then STRBANCO = " INBURSA"
        If RFC = "BIN931011519" Then STRBANCO = " INTERACCIONES"
        If RFC = "BAF950102JP5" Then STRBANCO = " MIFEL"
        If RFC = "SIN9412025I4" Then STRBANCO = " SCOTIABANK"
        If RFC = "BRM940216EQ6" Then STRBANCO = " BANREGIO"
        If RFC = "BIN940223KE0" Then STRBANCO = " INVEX"
        If RFC = "BAF950102JP5" Then STRBANCO = " AFIRME"
        If RFC = "BMN930209927" Then STRBANCO = " BANORTE"
        If RFC = "AEB960223JP7" Then STRBANCO = " AMERICAN EXPRESS"
        If RFC = "BJP950104LJ5" Then STRBANCO = " JP MORGAN"
        If RFC = "BMI9704113PA" Then STRBANCO = " BMONEX"
        If RFC = "BAI0205236Y8" Then STRBANCO = " AZTECA"
        If RFC = "BAM0511076B3" Then STRBANCO = " AUTOFIN"
        If RFC = "BCI001030ECA" Then STRBANCO = " COMPARTAMOS"
        If RFC = "BAF060524EV6" Then STRBANCO = " BANCO FAMSA"
        If RFC = "PBI061115SC6" Then STRBANCO = " ACTINVER"
        If RFC = "BWM0611132A9" Then STRBANCO = " WAL - MART"
        If RFC = "IBI061030GD4" Then STRBANCO = " INTERBANCO"
        If RFC = "BSI061110963" Then STRBANCO = " BANCOPPEL"
        If RFC = "CIB850918BN8" Then STRBANCO = " CIBANCO"
        If RFC = "BAN500901167" Then STRBANCO = " BANSEFI"
        If RFC = "BBA130722BR7" Then STRBANCO = " Bancrea"
        Return STRBANCO
    End Function


    Public Function getTipoComprobante(_tC As String) As String

        If _tC = "I" Then

            Return "Ingreso"
        End If

        If _tC = "E" Then

            Return "Egreso"
        End If
        Return "Ingreso"

    End Function



End Class

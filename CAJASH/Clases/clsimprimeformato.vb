Imports System.Data.Odbc
Imports System.Globalization
Imports System.Threading

Public Class clsimprimeformato
    Public imp As New Imprimirnet
    Public impresora As String = ""
    Public datos As odbcDatareader
    Public datosesclavo As OdbcDataReader
    Dim _txtesclavo As String
    Dim basem As New base
    Dim baser As New base
    Dim basee As New base
    Dim basecuerpo As New base
    Public formato As String
    Public ESPREVIEW As Boolean = False

    Public Sub imprime(ByVal txtmaestro As String, ByVal _formato As String, ByVal txtesclavo As String, ByVal _ESPREV As Boolean, Optional ByVal imprimohistorial As Boolean = False, Optional reimpresion As Boolean = False)

        impresora = imp.prtSettings.PrinterName
        imp.prtSettings.Copies = 1
        _txtesclavo = txtesclavo
        Try
            datos = baser.consultasql(txtmaestro)

            datos.Read()
        Catch ex As Exception
            MessageBox.Show("Error al leer los datos del recibo para imprimir")
        End Try

        formato = _formato
        ESPREVIEW = _ESPREV


        imprimeencabezado(imprimohistorial, _formato, reimpresion)
        ''If My.Settings.FormatoCopia <> "" Then
        ''    imprimeencabezado(imprimohistorial, My.Settings.FormatoCopia, reimpresion)
        ''End If
        imp.mandardocumento(ESPREVIEW)

        imprimeencabezado(imprimohistorial, _formato, reimpresion)
        imp.mandardocumento(ESPREVIEW)



        basem.conexion.Dispose()
        basecuerpo.conexion.Dispose()
        basee.conexion.Dispose()
        baser.conexion.Dispose()
    End Sub

    Public Sub desconectarhilosdeimpresora()
        basem.conexion.Dispose()
        basecuerpo.conexion.Dispose()
        basee.conexion.Dispose()
        baser.conexion.Dispose()
    End Sub



    Public Sub imprimeencabezado(ByVal imprimohistorial As Boolean, ByVal _formato As String, ByVal reimpresion As Boolean)
        Dim baseencabezado As New base
        Dim enca As OdbcDataReader


        Try
            enca = baseencabezado.consultasql("SELECT * FROM FORMATORECIBO WHERE IDFORMATO='" & _formato & "'")

        Catch ex As Exception
            MessageBox.Show("ERROR AL LEER EL FORMATO DE IMPRESION")
        End Try



        Dim ali As Lineaimprimir.alineacion
        Try
            While enca.Read()
                Try
                    Dim alinea As String = enca("Alineacion")
                    Dim letraAUTILIZAR As String = enca("letra")
                    Dim x, y As Integer
                    x = enca("COLUMNA")
                    y = enca("FILA")
                    Dim tamanoDELETRA As Double
                    tamanoDELETRA = enca("size")

                    Select Case alinea
                        Case "Izquierda"
                            ali = Lineaimprimir.alineacion.Izquierda
                        Case "Derecha"
                            ali = Lineaimprimir.alineacion.Derecha
                        Case "Centrado"
                            ali = Lineaimprimir.alineacion.Centrado

                    End Select

                    Dim tipocampo As String = ""


                    tipocampo = enca("tipo")

                    Dim campo As String = enca("Concepto")

                    Dim fec As Date

                    Dim mensaje As String = ""
                    Try
                        Select Case tipocampo
                            Case "CampoTexto"
                                ali = Lineaimprimir.alineacion.Izquierda
                                mensaje = datos(campo)
                            Case "CampoDia"
                                fec = Date.Parse(datos("Fecha_Act").ToString()).ToShortDateString()
                                mensaje = fec.ToString
                            Case "CampoMes"
                                'fec = datos(campo)
                                'mensaje = fec.Month.ToString
                            Case "CampoAno"
                                'fec = datos(campo)
                                'mensaje = fec.Year.ToString
                            Case "CampoNumero"
                                mensaje = datos(campo).ToString
                            Case "CampoMoneda"
                                Dim moneda As Double = datos(campo)
                                mensaje = moneda.ToString("C", CultureInfo.CurrentCulture)
                            Case "ConvertirLetras"
                                Dim moneda As Double = datos(campo)
                                mensaje = ConvertCurrencyToSpanish(moneda, "pesos")
                            Case "Texto"
                                mensaje = enca("concepto").ToString
                            Case "Hora"
                                mensaje = Now.Hour & ":" & Now.Minute
                            Case "Fecha"
                                mensaje = Date.Parse(datos("Fecha_Act").ToString()).ToShortDateString()
                                'Case "CampoFolio"
                                '    mensaje = My.Settings.folio + 1
                            Case "Datos Lectura"
                                mensaje = ""
                                Try
                                    'Dim datoslec As IDataReader = ConsultaSql("Select * from lecturas where cuenta = " & datos("cuenta") & " order by an_per desc").ExecuteReader()

                                    Dim datosLectura As IDataReader = ConsultaSql($"Select lectura , LectAnt, consumo, valornummes(mes, an_per) as ordenado, mes, an_per from lecturas where cuenta = {datos("cuenta")}  order by ordenado desc limit 1").ExecuteReader

                                    If datosLectura.Read() Then
                                        Dim PENULTIMALECTURA As String = datosLectura("LectAnt").ToString()
                                        Dim ULTIMALECTURA As String = datosLectura("lectura").ToString()
                                        Dim CONSUMO As String = datosLectura("consumo").ToString()
                                        Dim MESANO As String = datosLectura("mes") & " " & datosLectura("AN_PER")

                                        mensaje = MESANO & " PEN LEC:" & PENULTIMALECTURA & " ULT LECT:" & ULTIMALECTURA & " CONSUMO: " & CONSUMO
                                    End If
                                Catch ex As Exception

                                End Try

                        End Select
                    Catch ex As Exception
                        ''  MessageBox.Show(ex.Message)
                    End Try


                    imp.imprimir(x, y, mensaje, ali, letraAUTILIZAR, tamanoDELETRA)
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try


            End While
        Catch ex As Exception
            MessageBox.Show("ERROR AL LEER EL ENCABEZADO DEL DOCUMENTO")
        End Try


        Try

        Catch ex As Exception

        End Try

        Try
            Dim cuerpo As OdbcDataReader = basecuerpo.consultasql("select * from cuerporecibo where idformato='" & _formato & "' limit 1")
            cuerpo.Read()

            Dim linea As Integer = 300

            Try
                linea = cuerpo("LINEA_INICIAL")
            Catch ex As Exception

            End Try

            Dim COLUMNAIMPORTE As Integer = 600
            Try
                COLUMNAIMPORTE = cuerpo("COLUMNA_IMPORTE")
            Catch ex As Exception

            End Try

            Dim COLUMNACONCEPTO As Integer = 50
            Try
                COLUMNACONCEPTO = cuerpo("COLUMNA_CONCEPTO")
            Catch ex As Exception

            End Try
            Dim CANTIDAD As Integer = 5
            Try
                CANTIDAD = cuerpo("COLUMNA_CANTIDAD")
            Catch ex As Exception

            End Try

            Dim CPRECIO As Integer = 500
            Try
                CPRECIO = cuerpo("COLUMNA_PRECIO")
            Catch ex As Exception

            End Try
            Dim LETRA As String = "Microsoft Sans serif"
            Try
                LETRA = cuerpo("letra")
            Catch ex As Exception

            End Try
            Dim tamano As Double
            Try
                tamano = cuerpo("TAMANO_LETRA")
            Catch ex As Exception

            End Try
            Dim AVANCE As Integer = 11
            Try
                AVANCE = cuerpo("AVANCE")
            Catch ex As Exception

            End Try
            datosesclavo = basee.consultasql(_txtesclavo)

            While (datosesclavo.Read)

                Try
                    ali = Lineaimprimir.alineacion.Centrado
                    Dim MENSAJE As String
                    MENSAJE = datosesclavo("cantidad")
                    imp.imprimir(CANTIDAD, linea, MENSAJE, ali, LETRA, tamano)

                    ali = Lineaimprimir.alineacion.Izquierda
                    MENSAJE = datosesclavo("CONCEPTO")
                    Dim mensaje2 = ""
                    If MENSAJE.Length > 100 Then
                        MENSAJE = MENSAJE.Substring(0, 100)
                        mensaje2 = MENSAJE.Substring(100, MENSAJE.Length - 100)
                    End If
                    imp.imprimir(COLUMNACONCEPTO, linea, MENSAJE, ali, LETRA, tamano)

                    ali = Lineaimprimir.alineacion.Derecha
                    Dim moneda As Double = datosesclavo("MONTO")
                    MENSAJE = moneda.ToString("C", CultureInfo.CurrentCulture)
                    imp.imprimir(CPRECIO, linea, MENSAJE, ali, LETRA, tamano)


                    ali = Lineaimprimir.alineacion.Derecha
                    Dim moneda2 As Double = datosesclavo("IMPORTE")
                    MENSAJE = moneda2.ToString("C", CultureInfo.CurrentCulture)
                    imp.imprimir(COLUMNAIMPORTE, linea, MENSAJE, ali, LETRA, tamano)

                    linea = linea + AVANCE
                    If mensaje2 <> "" Then
                        imp.imprimir(COLUMNACONCEPTO, linea, MENSAJE, ali, LETRA, tamano)
                    End If
                    linea = linea + AVANCE
                Catch ex As Exception

                End Try

            End While
        Catch ex As Exception
            MessageBox.Show("ERROR AL CARGAR LOS DATOS DEL CUERPO DEL RECIBO")
        End Try

        '''''''''

        If imprimohistorial = True And My.Settings.imprimirhitorial = "SI" Then


            Try
                Dim cuerpolectura As OdbcDataReader = basecuerpo.consultasql("select * from cuerpolectura where idformato='" & _formato & "' limit 1")
                cuerpolectura.Read()

                Dim linea As Integer = 260

                Try
                    linea = cuerpolectura("LINEA_INICIAL")
                Catch ex As Exception

                End Try

                Dim COLUMNA_MES As Integer = 600
                Try
                    COLUMNA_MES = cuerpolectura("COLUMNA_MES")
                Catch ex As Exception

                End Try

                Dim COLUMNA_PERIODO As Integer = 50
                Try
                    COLUMNA_PERIODO = cuerpolectura("COLUMNA_PERIODO")
                Catch ex As Exception

                End Try
                Dim COLUMNA_LECTURA_ACT As Integer = 5
                Try
                    COLUMNA_LECTURA_ACT = cuerpolectura("COLUMNA_LECTURA_ACT")
                Catch ex As Exception

                End Try

                Dim COLUMNA_CONSUMO As Integer = 500
                Try
                    COLUMNA_CONSUMO = cuerpolectura("COLUMNA_CONSUMO")
                Catch ex As Exception
                End Try

                Dim COLUMNA_PROMEDIO As Integer = 500
                Try
                    COLUMNA_PROMEDIO = cuerpolectura("COLUMNA_PROMEDIO")
                Catch ex As Exception
                End Try

                Dim LINEA_PROMEDIO As Integer = 500
                Try
                    LINEA_PROMEDIO = cuerpolectura("LINEA_PROMEDIO")
                Catch ex As Exception
                End Try


                Dim LETRA As String = "Microsoft Sans serif"
                Try
                    LETRA = cuerpolectura("letra")
                Catch ex As Exception

                End Try
                Dim tamano As Double
                Try
                    tamano = cuerpolectura("TAMANO_LETRA")
                Catch ex As Exception

                End Try
                Dim AVANCE As Integer = 11
                Try
                    AVANCE = cuerpolectura("AVANCE")
                Catch ex As Exception

                End Try
                Dim LECTURAS As OdbcDataReader = basecuerpo.consultasql("select MES, AN_PER,LECTURA,CONSUMO,CONSUMOCOBRADO from lecturas   where cuenta=" & datos("cuenta") & " order by valornummes(mes,an_per) desc limit 3")

                Dim acu As Integer = 0



                While (LECTURAS.Read)


                    Try
                        ali = Lineaimprimir.alineacion.Centrado
                        Dim MENSAJE As String
                        MENSAJE = LECTURAS("MES")
                        imp.imprimir(COLUMNA_MES, linea, MENSAJE, ali, LETRA, tamano)

                        ali = Lineaimprimir.alineacion.Izquierda
                        MENSAJE = LECTURAS("AN_PER")
                        imp.imprimir(COLUMNA_PERIODO, linea, MENSAJE, ali, LETRA, tamano)

                        ali = Lineaimprimir.alineacion.Derecha
                        MENSAJE = "LECTURA " & LECTURAS("LECTURA")
                        imp.imprimir(COLUMNA_LECTURA_ACT, linea, MENSAJE, ali, LETRA, tamano)


                        ali = Lineaimprimir.alineacion.Derecha
                        'MENSAJE = "CONSUMO " & LECTURAS("CONSUMO")
                        MENSAJE = "" & LECTURAS("CONSUMO")
                        imp.imprimir(COLUMNA_CONSUMO, linea, MENSAJE, ali, LETRA, tamano)

                        acu = acu + LECTURAS("CONSUMO")


                        linea = linea + AVANCE
                    Catch ex As Exception

                    End Try

                End While
                '''''




                Dim prom As Integer = 0
                Try
                    prom = Math.Round(acu / 3, 0)
                Catch ex As Exception

                End Try

                Dim MENSAJEPROM As String
                ali = Lineaimprimir.alineacion.Derecha
                MENSAJEPROM = prom
                imp.imprimir(COLUMNA_PROMEDIO, LINEA_PROMEDIO, MENSAJEPROM, ali, LETRA, tamano)








                '' ''''
                ' ''IMPRIMIR FOLIO
                '' '''''''''''''
                ' '' ''imp.imprimir(630, 110, My.Settings.folio + 1, Imprimirnet.alineacion.Izquierda, "Microsoft Sans Serif", 7)
                ' '' ''imp.imprimir(630, 670, My.Settings.folio + 1, Imprimirnet.alineacion.Izquierda, "Microsoft Sans Serif", 7)







                ''''''''''
                'OTROSS CAMPOS DIRECTAMENTE AL IMPRIMIR 
                '''''''''''
                'Dim otroscampos As OdbcDataReader = basecuerpo.consultasql("select consumocobrado, a.descrip as Descrip, concat(sit_pad,sit_med,Sit_Hid) as SIT from lecturas as l, algoritmos as a where cuenta=" & datos("cuenta") & " and a.clave=l.algoritmo order by valornummes(mes,an_per) desc limit 1;")

                'While (otroscampos.Read)
                '    Try
                '        ali = Lineaimprimir.alineacion.Centrado
                '        Dim MENSAJE As String
                '        MENSAJE = "Ultimo Consumo: " & otroscampos("consumocobrado")
                '        imp.imprimir(480, 180, MENSAJE, Imprimirnet.alineacion.Izquierda, "Microsoft Sans Serif", 7)
                '        imp.imprimir(480, 730, MENSAJE, Imprimirnet.alineacion.Izquierda, "Microsoft Sans Serif", 7)

                '        MENSAJE = "" & otroscampos("Descrip")
                '        imp.imprimir(480, 190, MENSAJE, Imprimirnet.alineacion.Izquierda, "Microsoft Sans Serif", 7)
                '        imp.imprimir(480, 740, MENSAJE, Imprimirnet.alineacion.Izquierda, "Microsoft Sans Serif", 7)

                '        MENSAJE = "Situacion: " & otroscampos("Sit")
                '        imp.imprimir(480, 200, MENSAJE, Imprimirnet.alineacion.Izquierda, "Microsoft Sans Serif", 7)
                '        imp.imprimir(480, 750, MENSAJE, Imprimirnet.alineacion.Izquierda, "Microsoft Sans Serif", 7)

                '    Catch ex As Exception

                '    End Try

                'End While






                '''''''''''
                ''CONSUMO COBRADO 
                '''''''''''
                '''BLOQUE 1
                'imp.imprimir(180, 251, "M3", Imprimirnet.alineacion.Izquierda, "Microsoft Sans Serif", 7)
                'imp.imprimir(210, 251, "M3 COB", Imprimirnet.alineacion.Izquierda, "Microsoft Sans Serif", 7)
                'imp.imprimir(270, 251, "SIT", Imprimirnet.alineacion.Izquierda, "Microsoft Sans Serif", 7)

                '''BLOQUE 2
                'imp.imprimir(180, 801, "M3", Imprimirnet.alineacion.Izquierda, "Microsoft Sans Serif", 7)
                'imp.imprimir(210, 801, "M3 COB", Imprimirnet.alineacion.Izquierda, "Microsoft Sans Serif", 7)
                'imp.imprimir(270, 801, "SIT", Imprimirnet.alineacion.Izquierda, "Microsoft Sans Serif", 7)


                'Dim cconbrado As OdbcDataReader = basecuerpo.consultasql("select consumo, consumocobrado, concat(sit_pad,sit_med,Sit_Hid) as SIT, mes, an_per from lecturas where cuenta=" & datos("cuenta") & "  order by valornummes(mes,an_per) desc limit 3;")
                'Dim salto As Int16 = 10

                'While (cconbrado.Read)
                '    Try
                '        ali = Lineaimprimir.alineacion.Centrado
                '        Dim MENSAJE As String

                '        Dim SITUACION As String = ""

                '        MENSAJE = "" & Math.Round(cconbrado("consumocobrado"), 0)
                '        imp.imprimir(238, 254 + salto, MENSAJE, Imprimirnet.alineacion.Derecha, "Microsoft Sans Serif", 7)
                '        imp.imprimir(238, 804 + salto, MENSAJE, Imprimirnet.alineacion.Derecha, "Microsoft Sans Serif", 7)

                '        SITUACION = "" & cconbrado("SIT")
                '        imp.imprimir(280, 254 + salto, SITUACION, Imprimirnet.alineacion.Derecha, "Microsoft Sans Serif", 7)
                '        imp.imprimir(280, 804 + salto, SITUACION, Imprimirnet.alineacion.Derecha, "Microsoft Sans Serif", 7)

                '        salto = salto + 13

                '    Catch ex As Exception

                '    End Try

                'End While



            Catch ex As Exception
                MessageBox.Show("ERROR AL CARGAR LOS DATOS DEL CUERPO DEL RECIBO")
            End Try

        End If


        baseencabezado.conexion.Dispose()
    End Sub
End Class


Imports System.IO
Imports System.Text
Imports MultiFacturasSDK

Public Class CLSFACTURA
    Public NUMERO As String
    Public NOMBRE As String
    Public Subtotal As Double
    Public Iva As Double
    Public Total As Double
    Public Tipodeusuario As String
    Public uuid As String
    Public serie As String
    Public fecha As Date = Now



    Public Function cancelarbyid(ByVal UUID As String, ByVal RFC As String) As Boolean


        Dim cer As String = String.Empty
        Dim key As String = String.Empty
        Dim passkey = String.Empty
        'Dim usuariomf As String = String.Empty
        'Dim passmf As String = String.Empty
        'Dim Rfc = Rfc


        'Try
        '    usuariomf = My.MySettings.Default.UsuarioMultifacturas
        '    passmf = My.MySettings.Default.PassFacturaMultifacturas

        '    If My.Settings.TimbrarPrueba.ToUpper = "SI" Then
        '        key = File.ReadAllText("C:\\SDK2\\CERTIFICADOS\\PRUEBA.KEY")
        '        cer = File.ReadAllText("C:\\SDK2\\CERTIFICADOS\\PRUEBA.CER")
        '        passkey = "123456789a"
        '    Else
        key = File.ReadAllText(My.MySettings.Default.KEY & ".pem")
        cer = File.ReadAllText(My.MySettings.Default.CER & ".pem")


        '        'Dim keyt = New X509Certificate2(My.MySettings.Default.KEY)
        '        'Dim keyc = keyt.GetPublicKeyString




        '        'key = keyc
        '        'cer = cert.ToString(True)


        passkey = My.MySettings.Default.KeyContrasena
        '    End If

        '    'key = Convert.ToBase64String(Encoding.UTF8.GetBytes(key))
        '    'cer = Convert.ToBase64String(Encoding.UTF8.GetBytes(cer))

        '    'passkey = Convert.ToBase64String(Encoding.UTF8.GetBytes(passkey))

        '    'Dim passkey = Convert.ToBase64String(Encoding.UTF8.GetBytes(certificado.PassCertificado));

        '    '   xml = Convert.ToBase64String(Encoding.UTF8.GetBytes(xml))
        '    ' SoapCancelar.RespuestaNET respuesta = New SoapCancelar.RespuestaNET();

        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString())
        'End Try





        Try

            Dim sdk As MFSDK = New MFSDK()



            sdk.Iniciales.Add("cfdi", "c:\sdk2\timbrados\FACTURACANCELAR.XML")
            sdk.Iniciales.Add("cancelar", "SI")

            sdk.AgregaObjeto(PAC())
            sdk.AgregaObjeto(Conf())


            sdk.CreaINI(".\\cancela.ini")
            Dim ini As String = File.ReadAllText(".\\cancela.ini")
            ' MessageBox.Show(ini)


            Dim cliente As New CAJAS.SoapCancelar.TimbradoRemotodeINI

            Dim ini64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(ini))

            Dim passmf As String = My.MySettings.Default.PassFacturaMultifacturas

            'Encoding e2 = Encoding.GetEncoding("utf-32")

            '    Dim CodificarRes As SoapCancelar.RespuestaWS = cliente.timbrarini1(RFC, ini64, cer, key, passkey)

            Dim CodificarRes As SDKRespuesta = sdk.Timbrar("C:\sdk2\timbrar32.bat", "C:\sdk2\timbrados\", "factura", False)

            'Dim result As String = System.Text.Encoding.GetEncoding("iso-8859-1").GetString(CodificarRes)

            'Dim respuesta As String = cliente.cancelar(RFC, passmf, UUID, cer, key, "123456789a")




            If CodificarRes.Codigo_MF_Numero.Contains("OK") Or CodificarRes.Codigo_MF_Numero.Contains("PREVIAMENTE") Then
                Return True
            Else
                Return False
            End If


            MessageBox.Show(CodificarRes.Codigo_MF_Texto)

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try



        Return False

    End Function
    Public Function Conf() As MFObject
        Dim con As MFObject = New MFObject("conf")

        If My.Settings.TimbrarPrueba.ToLower = "si" Then

            con("cer") = "C:\\SDK2\\certificados\\lan7008173r5.cer.pem"
            con("key") = "C:\\SDK2\\certificados\\lan7008173r5.key.pem"
            con("pass") = "12345678a"
        Else

            con("cer") = My.Settings.CER
            con("key") = My.Settings.KEY
            con("pass") = My.Settings.KeyContrasena
        End If
        ' Se indica la ruta y contraseña de los archivos CSD (Archivos CSD de pruebas)


        Return con
    End Function
    Public Function PAC() As MFObject
        Dim p As MFObject = New MFObject("PAC")
        If My.Settings.TimbrarPrueba.ToLower = "si" Then

            p("usuario") = "DEMO700101XXX"
            p("pass") = "DEMO700101XXX"
            p("produccion") = "NO"
        Else

            p("usuario") = My.Settings.UsuarioMultifacturas
            p("pass") = My.Settings.PassFacturaMultifacturas
            p("produccion") = "SI"
        End If

        Return p
    End Function

    'Public String Cancela40(String uuid, String motivo, int IDFactura, int UserID, String viene)
    '    {
    '        //MotivoCancelacionContext db = New MotivoCancelacionContext();


    '        //Certificados de prueba
    '        String usuario = @"C:\sdk2\certificados\lan7008173r5.cer.pem";
    '        String pass = @"C:\sdk2\certificados\lan7008173r5.key.pem";
    '        String rfc = "LAN7008173R5";

    '        String cer = Convert.ToBase64String(File.ReadAllBytes(@"C:\sdk2\certificados\lan7008173r5.cer.pem"));
    '        String key = Convert.ToBase64String(File.ReadAllBytes(@"C:\sdk2\certificados\lan7008173r5.key.pem"));
    '        String passcer = "12345678a";


    '        If (Settings.Default.TimbrarPrueba.ToUpper() == "NO")
    '        {
    '            String SQLEmpresa = "select * from Empresa";
    '            Empresa datosNomList = New CobroDefault().Database.SqlQuery < Empresa > (SQLEmpresa).FirstOrDefault();


    '            //usuario y pass los proporcionara maestra Gaby
    '            usuario = Settings.Default.UsuarioMultifacturas;
    '            pass = Settings.Default.PasswordMultifacturas;

    '            //Obtener los certificados de Actopan desde Settings o hacer una tabla donde se carguen los certificados y obtenerlos
    '            cer = Convert.ToBase64String(File.ReadAllBytes(Settings.Default.CertificadoCliente));
    '            key = Convert.ToBase64String(File.ReadAllBytes(Settings.Default.keycliente));
    '            //passcer = Convert.ToBase64String(Encoding.UTF8.GetBytes(certificado.PassCertificado));
    '            passcer = Settings.Default.passsCliente;
    '            rfc = datosNomList.RFC; //emisor
    '        }



    '        WSCancela.Cancelarcfdi40SAT cliente = New WSCancela.Cancelarcfdi40SAT();
    '        WSCancela.datos datos = New WSCancela.datos();

    '        datos.accion = "cancelar";
    '        datos.b64Cer = cer;
    '        datos.b64Key = key;
    '        datos.motivo = motivo;
    '        datos.pass = pass;
    '        datos.password = passcer;
    '        datos.produccion = "SI";
    '        datos.usuario = usuario;
    '        datos.uuid = uuid;
    '        //datos.folioSustitucion = uuidsusti;
    '        datos.rfc = rfc;



    '        Try
    '        {

    '            WSCancela.respuesta respuesta = cliente.cancelarCfdi(datos);

    '            If (respuesta.codigo_mf_texto.Contains("OK"))
    '            {


    '                //if (viene == "Factura")
    '                //{
    '                //    string insert = "insert into FacturasCanceladasSAT (Estado, IDFactura,Fecha,Usuario) values " +
    '                //   "('C'," + IDFactura + ",sysdatetime()," + "UsuarioDelSistema" + ")";
    '                //    //MessageBox.Show("Hacer procedimiento");
    '                //    New CobroDefault().Database.ExecuteSqlCommand(insert);
    '                //}
    '                //else
    '                //{
    '                //    string insert = "insert into FacturasCanceladasSAT (Estado, IDFactura,Fecha,Usuario) values " +
    '                //   "('C'," + IDFactura + ",sysdatetime()," + "UsuarioDelSistema" + ")";
    '                //    //MessageBox.Show("Hacer procedimiento");
    '                //    New CobroDefault().Database.ExecuteSqlCommand(insert);
    '                //}
    '                //  MessageBox.Show("La factura fue cancelada ante el sat");
    '                MessageBox.Show("UUID CANCELADO CORRECTAMENTE");
    '                Return respuesta.acuse;
    '            }
    '            Else
    '            {
    '                MessageBox.Show("OCURRIO UN ERROR AL CANCELAR EL UUID");
    '                Return "";
    '            }
    '        }
    '        Catch (Exception err)
    '        {
    '            MessageBox.Show("OCURRIO UN ERROR AL CANCELAR EL UUID \n" + err);
    '            Return "";
    '        }
    '    }

    Public Function Cancela40(uuidP As String, uuidSustitutoP As String, motivoP As String, IDFacturaP As Integer)
        'WSCancela.Cancelarcfdi40SAT cliente = New WSCancela.Cancelarcfdi40SAT();
        '    WSCancela.datos datos = New WSCancela.datos();
        Dim RFCEmpresa As String = "lan7008173r5"
        Dim retorna As String = ""

        Try

            Dim usuario As String = ""
            Dim pass As String = ""
            Dim rfc As String = ""

            Dim cer As String = ""
            Dim key As String = ""
            Dim passcer As String = ""

            If My.Settings.TimbrarPrueba.ToUpper = "SI" Then


                usuario = "C:\sdk2\certificados\lan7008173r5.cer.pem"
                pass = "C:\sdk2\certificados\lan7008173r5.key.pem"
                rfc = "LAN7008173R5"

                cer = Convert.ToBase64String(File.ReadAllBytes("C:\sdk2\certificados\lan7008173r5.cer.pem"))
                key = Convert.ToBase64String(File.ReadAllBytes("C:\sdk2\certificados\lan7008173r5.key.pem"))
                passcer = "12345678a"


            ElseIf My.Settings.TimbrarPrueba.ToUpper = "NO" Then

                RFCEmpresa = obtenerCampo("select CNIF from Empresa where codemp = 1", "CNIF")

                'CERDENCIALES REALES

                usuario = My.Settings.UsuarioMultifacturas
                pass = My.Settings.PassFacturaMultifacturas

                'CERTIFICADOS REALES

                cer = Convert.ToBase64String(File.ReadAllBytes(My.Settings.CER))
                key = Convert.ToBase64String(File.ReadAllBytes(My.Settings.KEY))
                passcer = My.Settings.KeyContrasena

            End If

            Dim objCancelar As New WSCancelarFactura40.Cancelarcfdi40SAT
            Dim datosCancelar As New WSCancelarFactura40.datos

            datosCancelar.accion = "cancelar"
            datosCancelar.b64Cer = cer
            datosCancelar.b64Key = key
            datosCancelar.motivo = motivoP
            datosCancelar.pass = pass
            datosCancelar.password = passcer
            datosCancelar.produccion = "SI"
            datosCancelar.usuario = usuario
            datosCancelar.uuid = uuidP
            datosCancelar.folioSustitucion = uuidSustitutoP
            datosCancelar.rfc = RFCEmpresa

            '' coordinac
            Try
                Dim respuestaSAT As WSCancelarFactura40.respuesta = objCancelar.cancelarCfdi(datosCancelar)

                If respuestaSAT.codigo_mf_texto.Contains("OK") Then
                    MessageBox.Show("UUID CANCELADO EXITOSAMENTE")

                    retorna = respuestaSAT.acuse

                Else
                    MessageBox.Show("OCURRIO UN ERROR AL CANCELAR EL UUID")

                    retorna = ""

                End If


            Catch ex As Exception
                MessageBox.Show(ex.ToString())
            End Try

        Catch ex As Exception
            MessageBox.Show("Ocurrio un error al cancelar la factura " & ex.ToString())
        End Try

        Return retorna
    End Function

End Class

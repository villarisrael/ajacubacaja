Imports System.Data.Odbc
Imports System.IO
Imports System.Net.Mail
Imports System.Net
Imports System.Text
Module FuncFacturas
    Public usuariodelsistema As String
    Public niveldedescuento As Double
    Public usuariotem As String
    Public niveldedescuentotem As Double
    Public variable_iva As Double = 16
    Public serieregresada As String = ""

    Public salida = "Exitosa"


    Public Function WebServiceToInvoice(ByVal Documento As String, ByVal cliente As String, ByVal passcliente As String, ByVal nombre As String, Optional ByVal maildeusuario As String = "enviofacturas@caposa.gob.mx") As Long
        ' Create a request using a URL that can receive a post.

        Try

            Dim request As WebRequest
            Dim Data = New StringBuilder()

            Dim byt As Byte() = System.Text.Encoding.UTF8.GetBytes(Documento)
            Dim Documento2 As String = Convert.ToBase64String(byt)

            If My.MySettings.Default.TimbrarPrueba = "SI" Then
                request = WebRequest.Create("http://dkcc.dyndns.org:8181/prometheus/ws_timbrado_factupronto.php")
                Data.Append("usuario=eduardovime@outlook.com")
                Data.Append("&pass=Kelcay74@")
                Data.Append("&txt=" & Documento2)

            Else
                request = WebRequest.Create("http://www.factupronto.mx/prometheus/txt_timbrado_factupronto.php")
                Data.Append("usuario=" & cliente)
                Data.Append("&pass=" & passcliente)
                Data.Append("&txt=" & Documento2)
            End If
            ' Set the Method property of the request to POST.
            request.Method = "POST"
            'request.Credentials = New System.Net.NetworkCredential("eduardovime@outlook.com", "Kelcay74@")
            'request.UseDefaultCredentials = False




            Dim encodedString As Byte() = Encoding.UTF8.GetBytes(Data.ToString())

            'Dim PosData As String = "usuario=eduardovime@outlook.com&Pass=Kelcay74@&txt=''" ' & Documento
            Dim byteArray As Byte() = UTF8Encoding.UTF8.GetBytes(Data.ToString())
            request.ContentType = "application/x-www-form-urlencoded"
            request.ContentLength = byteArray.Length
            Dim dataStream As Stream = request.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
            dataStream.Close()

            ' Get the response.
            Dim response As WebResponse = request.GetResponse()
            'Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)
            dataStream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()


            Dim ContenidoWeb() As String = responseFromServer.ToString.Split("|")
            Dim xmlfile As String, pdffile As String = String.Empty, xmlFactura As String, folioregresado As String = ""
            Dim directorio As String = ""
            Dim directorio2 As String = ""

            '   If ContenidoWeb(0) = 1 Then



            If ContenidoWeb(1).Contains("501") Then
                MessageBox.Show("El usuario y contraseña posiblemente han cambiado")
                Return 0
            End If
            If ContenidoWeb(1).Contains("503") Then
                MessageBox.Show("No hay timbres disponibles")
                Return 0
            End If
            If ContenidoWeb(1).Contains("507") Then

                MessageBox.Show("La serie no es correcta")
                Return 0
            End If
            If ContenidoWeb(1).Contains("200") Then
            End If
            If Not ContenidoWeb(1).Contains("200") Then
                MessageBox.Show("Error en el servidor  y/o servicio factuprontro")
                Return 0
            End If

            xmlfile = ContenidoWeb(2)

            Dim b As Byte() = Convert.FromBase64String(xmlfile)
            xmlFactura = System.Text.Encoding.UTF8.GetString(b)

            'Dim xmltexto As String
            'xmltexto = ' Convert.FromBase64CharArray(xmlfile, 0, xmlfile.Length)
            ''Dim xmltexto As String = ConvertUTF8IntPtrtoString(xmlfile)

            pdffile = ContenidoWeb(3)
            Dim FacturaFaltam As Integer = ContenidoWeb(4) - 1

            serieregresada = ContenidoWeb(5)
            folioregresado = ContenidoWeb(6)

            If Not My.Computer.FileSystem.DirectoryExists(Application.StartupPath & "\facturas\" & nombre) Then

                My.Computer.FileSystem.CreateDirectory(Application.StartupPath & "\facturas\" & nombre)
            End If

            If Not My.Computer.FileSystem.DirectoryExists(Application.StartupPath & "\facturas" & Year(Now) & acompletacero(Month(Now), 2) & "\") Then

                My.Computer.FileSystem.CreateDirectory(Application.StartupPath & "\facturas" & Year(Now) & acompletacero(Month(Now), 2) & "\")
            End If

            directorio = Application.StartupPath & "\facturas\" & nombre
            directorio2 = Application.StartupPath & "\facturas" & Year(Now) & acompletacero(Month(Now), 2) & "\"

            Try
                '  DownloadFile(, directorio & "\XmlFactura" & serieregresada & folioregresado & ".xml")
                Dim arch As New clsDocumentoTXT
                arch.guardartxt(xmlFactura, directorio & "\XmlFactura" & serieregresada & folioregresado & ".xml")
                arch.guardartxt(xmlFactura, directorio2 & "\XmlFactura" & serieregresada & folioregresado & ".xml")

                DownloadFile(pdffile, directorio & "\PdfFactura" & serieregresada & folioregresado & ".pdf")
                DownloadFile(pdffile, directorio2 & "\PdfFactura" & serieregresada & folioregresado & ".pdf")

            Catch ex As Exception
                MessageBox.Show("Error" & ex.Message)
            End Try

            Dim timbres As String
            timbres = ContenidoWeb(4)

            If Integer.Parse(timbres) <= 3000 Then
                MessageBox.Show("Te quedan " & timbres & " Timbres")
            End If

            Dim folio As Long = 0
            Long.TryParse(folioregresado, folio)
            'reader.Close()
            'dataStream.Close()
            'response.Close()
            Dim x As New FrmVisor()
            x.pdfvisor.Tag = directorio & "\PdfFactura" & serieregresada & folioregresado & ".pdf"

            ' FrmVisor.pdfvisor
            '   FrmVisor.MdiParent = Me
            x.Show()
            '   x.pdfvisor.Print()
            '  x.pdfvisor.Print()
            Try
                With New Process

                    .StartInfo.Verb = "print"
                    .StartInfo.CreateNoWindow = False
                    .StartInfo.FileName = directorio & "\PdfFactura" & serieregresada & folioregresado & ".pdf"
                    .Start()
                    .WaitForExit(10000)
                    .CloseMainWindow()
                    .Close()
                End With

              
            Catch ex As Exception

            End Try

            Try
                With New Process

                    .StartInfo.Verb = "print"
                    .StartInfo.CreateNoWindow = False
                    .StartInfo.FileName = directorio & "\PdfFactura" & serieregresada & folioregresado & ".pdf"
                    .Start()
                    .WaitForExit(10000)
                    .CloseMainWindow()
                    .Close()
                End With


            Catch ex As Exception

            End Try

          

            '   x.Close()

            Dim mail As New MailMessage
            Dim autenticar As New NetworkCredential
            Dim send As New SmtpClient


            Try
                mail.To.Clear()
                mail.Body = My.MySettings.Default.MENSAJECORREO

                ' mail.Body = My.MySettings.Default.
                mail.Subject = My.MySettings.Default.Asuntocorreo
                mail.IsBodyHtml = False
                mail.To.Add(Trim(maildeusuario))
                mail.Attachments.Add(New Attachment(directorio & "\XmlFactura" & serieregresada & folioregresado & ".xml"))
                mail.Attachments.Add(New Attachment(directorio & "\PdfFactura" & serieregresada & folioregresado & ".pdf"))
                mail.From = New MailAddress(My.MySettings.Default.CorreoFacturas, "CAPOSA HUICHAPAN")
                send.Credentials = New NetworkCredential(My.MySettings.Default.Usuariocorreo, My.MySettings.Default.Passwordcorreo)
                'If cboMail.SelectedIndex = 1 Then
                send.Host = "smtp.gmail.com"
                send.Port = 587
                send.EnableSsl = True
                send.DeliveryMethod = SmtpDeliveryMethod.Network
                '        End If
                'If cboMail.SelectedIndex = 2 Then
                '    send.Host = "smtp.live.com"
                '    send.Port = 587
                '    send.EnableSsl = False
                'End If
                send.Send(mail)
                ' Me.Cursor = Cursors.Default
                '  Me.Text = "Enviar Mensaje"
                MsgBox("Un correo fue enviado correctamente", MsgBoxStyle.Information, "Mensaje")

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Mensajeria", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            salida = "Exitosa"
            Return folio
            'Else
            ' MessageBox.Show("Error Del Contenido " & ContenidoWeb(1).ToString)

            ' Return 0
            ' End If

            Console.WriteLine(responseFromServer)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            salida = "Erronea"
            Return 0
        End Try



        Return 0
    End Function



    Public Function DownloadFile(ByVal remoteFilename As [String], ByVal localFilename As [String]) As Integer
        ' Function will return the number of bytes processed
        ' to the caller. Initialize to 0 here.

        If Mid(remoteFilename, 1, 5) <> "<?xml" Then
            Dim bytesProcessed As Integer = 0

            ' Assign values to these objects here so that they can
            ' be referenced in the finally block
            Dim remoteStream As Stream = Nothing
            Dim localStream As Stream = Nothing
            Dim response As WebResponse = Nothing

            ' Use a try/catch/finally block as both the WebRequest and Stream
            ' classes throw exceptions upon error
            Try
                ' Create a request for the specified remote file name
                Dim request As WebRequest = WebRequest.Create(remoteFilename)
                If request IsNot Nothing Then
                    ' Send the request to the server and retrieve the
                    ' WebResponse object
                    response = request.GetResponse()
                    If response IsNot Nothing Then
                        ' Once the WebResponse object has been retrieved,
                        ' get the stream object associated with the response's data
                        remoteStream = response.GetResponseStream()

                        ' Create the local file
                        localStream = File.Create(localFilename)

                        ' Allocate a 1k buffer
                        Dim buffer As Byte() = New Byte(1023) {}
                        Dim bytesRead As Integer

                        ' Simple do/while loop to read from stream until
                        ' no bytes are returned
                        Do
                            ' Read data (up to 1k) from the stream
                            bytesRead = remoteStream.Read(buffer, 0, buffer.Length)

                            ' Write the data to the local file
                            localStream.Write(buffer, 0, bytesRead)

                            ' Increment total bytes processed
                            bytesProcessed += bytesRead
                        Loop While bytesRead > 0
                    End If
                End If
            Catch e As Exception
                Console.WriteLine(e.Message)
            Finally
                ' Close the response and streams objects here
                ' to make sure they're closed even if an exception
                ' is thrown at some point
                If response IsNot Nothing Then
                    response.Close()
                End If
                If remoteStream IsNot Nothing Then
                    remoteStream.Close()
                End If
                If localStream IsNot Nothing Then
                    localStream.Close()
                End If
            End Try

            ' Return total bytes processed to caller.
            Return bytesProcessed
        Else
            Return vbNull
        End If
    End Function


    Public Function ToBase64(ByVal data() As Byte) As String
        If data Is Nothing Then Throw New ArgumentNullException("data")
        Return Convert.ToBase64String(data)
    End Function

    Public Function ConvertFromBase64(ByVal Input As String)
        Dim B As Byte() = System.Convert.FromBase64String(Input)
        Return System.Text.Encoding.UTF8.GetString(B)
    End Function


End Module

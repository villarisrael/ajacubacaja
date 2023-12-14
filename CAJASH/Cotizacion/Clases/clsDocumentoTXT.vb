Imports System.IO
Public Class clsDocumentoTXT

    Public Function leerArchivo(ByVal archivo As String) As String
        ' Dim Open As New OpenFileDialog()
        Dim myStreamReader As System.IO.StreamReader
        Dim contenido As String
        Try
            myStreamReader = System.IO.File.OpenText(archivo)
            contenido = myStreamReader.ReadToEnd()
        Catch ex As Exception

        End Try
        Return contenido
    End Function

    Public Sub guardartxt(ByVal frase As String, ByVal Archivo As String)
        Dim myStreamWriter As System.IO.StreamWriter
        Try
            myStreamWriter = System.IO.File.AppendText(Archivo)
            myStreamWriter.Write(frase & ";" & Chr(13))
            myStreamWriter.Flush()
            myStreamWriter.Close()
        Catch ex As Exception
            If ex.Message.Contains("No se puede encontrar una parte de la ruta de acceso") Then
                My.Computer.FileSystem.CreateDirectory("c:\cajamovil")
                myStreamWriter = System.IO.File.AppendText(Archivo)
                myStreamWriter.Write(frase & ";" & Chr(13))
                myStreamWriter.Flush()
                myStreamWriter.Close()
            End If
        End Try
    End Sub
End Class

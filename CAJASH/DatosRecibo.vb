Partial Class DatosRecibo
    Partial Class LecturasDataTable

        Private Sub LecturasDataTable_ColumnChanging(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.ConsumocobradoColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

    Partial Class ConceptosDataTable



    End Class

    Partial Class DatosDataTable

    End Class

    Partial Class DocumentosDataTable



    End Class

End Class

Namespace DatosReciboTableAdapters
    Partial Public Class UsuarioTableAdapter
    End Class
End Namespace

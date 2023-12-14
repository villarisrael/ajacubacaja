Public Class Clspagoenlinea


    Public Sub creaServicio(Nombredelservicio As String, nombredelabase As String)
        Ejecucion("create schema " & Nombredelservicio)
        Ejecucion("create table " & Nombredelservicio & ".datos ( fecha DATETIME ) ENGINE=storage_engine")
        Ejecucion("insert into datos (fecha) values ('" & Now.ToShortTimeString() & "')")
        Ejecucion("create table " & Nombredelservicio & ".usuario like " & nombredelabase & ".usuario")
        Ejecucion("insert into " & Nombredelservicio & ".usuario select * from  " & nombredelabase & ".usuario")
        Ejecucion("create table " & Nombredelservicio & ".otrosconceptos like " & nombredelabase & ".otrosconceptos")
        Ejecucion("insert into " & Nombredelservicio & ".otrosconceptos select * from  " & nombredelabase & ".otrosconceptos where pagado=0 and estado<>'Cancel'")

        Ejecucion("create table " & Nombredelservicio & ".Vbancomer select * from  " & nombredelabase & ".vbancomer")

        Ejecucion("create table " & Nombredelservicio & ".lecturas like " & nombredelabase & ".lecturas")
    End Sub

    Public Sub pasalecturas(Nombredelservicio As String, nombredelabase As String, cuenta As Integer)
        Ejecucion("insert into " & Nombredelservicio & ".lecturas select * from  " & nombredelabase & ".lecturas where cuenta=" & cuenta & " and pagado=0")
    End Sub

    Public Sub pagarusuario(Nombredelservicio As String, nombredelabase As String, cuenta As Integer)
        Ejecucion("update " & nombredelabase & ".lecturas set pagado=1 where cuenta=" & cuenta & " llave= " & Nombredelservicio & ".lecturas.llave")
        Ejecucion("update " & nombredelabase & ".usuario " & Nombredelservicio & ".datos set " & nombredelabase & ".usuario.deudafec=" & Nombredelservicio & ".datos.fecha")
    End Sub



End Class

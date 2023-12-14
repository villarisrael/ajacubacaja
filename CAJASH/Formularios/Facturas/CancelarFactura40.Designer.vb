<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CancelarFactura40
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TxtFolioF = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LblFolioF = New System.Windows.Forms.Label()
        Me.btnCancelarSAT = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.ComboBoxCancelar = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'TxtFolioF
        '
        Me.TxtFolioF.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtFolioF.Location = New System.Drawing.Point(111, 103)
        Me.TxtFolioF.Name = "TxtFolioF"
        Me.TxtFolioF.Size = New System.Drawing.Size(311, 20)
        Me.TxtFolioF.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(41, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 21)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Motivo"
        '
        'LblFolioF
        '
        Me.LblFolioF.AutoSize = True
        Me.LblFolioF.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFolioF.Location = New System.Drawing.Point(12, 103)
        Me.LblFolioF.Name = "LblFolioF"
        Me.LblFolioF.Size = New System.Drawing.Size(93, 21)
        Me.LblFolioF.TabIndex = 3
        Me.LblFolioF.Text = "Folio Fiscal"
        '
        'btnCancelarSAT
        '
        Me.btnCancelarSAT.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelarSAT.Location = New System.Drawing.Point(137, 161)
        Me.btnCancelarSAT.Name = "btnCancelarSAT"
        Me.btnCancelarSAT.Size = New System.Drawing.Size(188, 32)
        Me.btnCancelarSAT.TabIndex = 4
        Me.btnCancelarSAT.Text = "Cancelar Factura SAT"
        Me.btnCancelarSAT.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft YaHei UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(343, 161)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(79, 32)
        Me.Button2.TabIndex = 5
        Me.Button2.Text = "Cerrar"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'ComboBoxCancelar
        '
        Me.ComboBoxCancelar.FormattingEnabled = True
        Me.ComboBoxCancelar.Location = New System.Drawing.Point(111, 46)
        Me.ComboBoxCancelar.Name = "ComboBoxCancelar"
        Me.ComboBoxCancelar.Size = New System.Drawing.Size(311, 21)
        Me.ComboBoxCancelar.TabIndex = 6
        '
        'CancelarFactura40
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(457, 220)
        Me.Controls.Add(Me.ComboBoxCancelar)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.btnCancelarSAT)
        Me.Controls.Add(Me.LblFolioF)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtFolioF)
        Me.Name = "CancelarFactura40"
        Me.Text = "CancelarFactura40"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents LblFolioF As Label
    Friend WithEvents btnCancelarSAT As Button
    Friend WithEvents Button2 As Button
    Public WithEvents TxtFolioF As TextBox
    Friend WithEvents ComboBoxCancelar As ComboBox
End Class

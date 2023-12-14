<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmfolio
    Inherits Telerik.WinControls.UI.RadForm

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.iinuevofolio = New DevComponents.Editors.IntegerInput()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.btnaceptar = New Telerik.WinControls.UI.RadButton()
        Me.btnsalir = New Telerik.WinControls.UI.RadButton()
        CType(Me.iinuevofolio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.btnaceptar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsalir, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Nuevo Folio"
        '
        'iinuevofolio
        '
        '
        '
        '
        Me.iinuevofolio.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.iinuevofolio.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.iinuevofolio.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.iinuevofolio.Location = New System.Drawing.Point(111, 13)
        Me.iinuevofolio.Name = "iinuevofolio"
        Me.iinuevofolio.ShowUpDown = True
        Me.iinuevofolio.Size = New System.Drawing.Size(80, 22)
        Me.iinuevofolio.TabIndex = 1
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.btnaceptar)
        Me.RadGroupBox3.Controls.Add(Me.btnsalir)
        Me.RadGroupBox3.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Bold)
        Me.RadGroupBox3.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office
        Me.RadGroupBox3.HeaderAlignment = Telerik.WinControls.UI.HeaderAlignment.Center
        Me.RadGroupBox3.HeaderText = "ACCIONES"
        Me.RadGroupBox3.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadGroupBox3.Location = New System.Drawing.Point(12, 56)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        '
        '
        '
        Me.RadGroupBox3.RootElement.Padding = New System.Windows.Forms.Padding(2, 18, 2, 2)
        Me.RadGroupBox3.Size = New System.Drawing.Size(295, 76)
        Me.RadGroupBox3.TabIndex = 12
        Me.RadGroupBox3.Text = "ACCIONES"
        '
        'btnaceptar
        '
        Me.btnaceptar.Image = Global.CAJAS.My.Resources.Resources.IcoAgregar
        Me.btnaceptar.ImageAlignment = System.Drawing.ContentAlignment.TopCenter
        Me.btnaceptar.Location = New System.Drawing.Point(23, 21)
        Me.btnaceptar.Name = "btnaceptar"
        Me.btnaceptar.Size = New System.Drawing.Size(89, 48)
        Me.btnaceptar.TabIndex = 0
        Me.btnaceptar.Text = "ACEPTAR"
        Me.btnaceptar.TextAlignment = System.Drawing.ContentAlignment.BottomCenter
        '
        'btnsalir
        '
        Me.btnsalir.Image = Global.CAJAS.My.Resources.Resources.IcoSalir
        Me.btnsalir.ImageAlignment = System.Drawing.ContentAlignment.TopCenter
        Me.btnsalir.Location = New System.Drawing.Point(186, 26)
        Me.btnsalir.Name = "btnsalir"
        Me.btnsalir.Size = New System.Drawing.Size(89, 48)
        Me.btnsalir.TabIndex = 1
        Me.btnsalir.Text = "Salir"
        Me.btnsalir.TextAlignment = System.Drawing.ContentAlignment.BottomCenter
        '
        'frmfolio
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(314, 144)
        Me.Controls.Add(Me.RadGroupBox3)
        Me.Controls.Add(Me.iinuevofolio)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmfolio"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Folio"
        CType(Me.iinuevofolio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        CType(Me.btnaceptar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsalir, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents iinuevofolio As DevComponents.Editors.IntegerInput
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnaceptar As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsalir As Telerik.WinControls.UI.RadButton
End Class


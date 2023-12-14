<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmabono
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
        Me.lblclaveconcepto = New DevComponents.DotNetBar.LabelX()
        Me.diAbono = New DevComponents.Editors.DoubleInput()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.btnaceptar = New Telerik.WinControls.UI.RadButton()
        Me.btnsalir = New Telerik.WinControls.UI.RadButton()
        CType(Me.diAbono, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.btnaceptar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsalir, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblclaveconcepto
        '
        '
        '
        '
        Me.lblclaveconcepto.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblclaveconcepto.Location = New System.Drawing.Point(21, 12)
        Me.lblclaveconcepto.Name = "lblclaveconcepto"
        Me.lblclaveconcepto.Size = New System.Drawing.Size(294, 23)
        Me.lblclaveconcepto.TabIndex = 0
        Me.lblclaveconcepto.Text = "LabelX1"
        '
        'diAbono
        '
        '
        '
        '
        Me.diAbono.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.diAbono.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.diAbono.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.diAbono.Increment = 1.0R
        Me.diAbono.Location = New System.Drawing.Point(134, 49)
        Me.diAbono.Name = "diAbono"
        Me.diAbono.ShowUpDown = True
        Me.diAbono.Size = New System.Drawing.Size(118, 22)
        Me.diAbono.TabIndex = 1
        '
        'LabelX1
        '
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Location = New System.Drawing.Point(21, 48)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(75, 23)
        Me.LabelX1.TabIndex = 2
        Me.LabelX1.Text = "Abono "
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
        Me.RadGroupBox3.Location = New System.Drawing.Point(12, 77)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        '
        '
        '
        Me.RadGroupBox3.RootElement.Padding = New System.Windows.Forms.Padding(2, 18, 2, 2)
        Me.RadGroupBox3.Size = New System.Drawing.Size(295, 74)
        Me.RadGroupBox3.TabIndex = 11
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
        'Frmabono
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(316, 165)
        Me.Controls.Add(Me.RadGroupBox3)
        Me.Controls.Add(Me.LabelX1)
        Me.Controls.Add(Me.diAbono)
        Me.Controls.Add(Me.lblclaveconcepto)
        Me.Name = "Frmabono"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Abono"
        CType(Me.diAbono, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        CType(Me.btnaceptar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsalir, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblclaveconcepto As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents diAbono As DevComponents.Editors.DoubleInput
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnaceptar As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsalir As Telerik.WinControls.UI.RadButton
End Class


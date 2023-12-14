<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPago
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
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cmbforma = New System.Windows.Forms.ComboBox()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.btnsalir = New Telerik.WinControls.UI.RadButton()
        Me.btningresar = New Telerik.WinControls.UI.RadButton()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.btnsalir, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btningresar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.RadGroupBox1.Controls.Add(Me.cmbforma)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office
        Me.RadGroupBox1.HeaderAlignment = Telerik.WinControls.UI.HeaderAlignment.Center
        Me.RadGroupBox1.HeaderText = "FORMA DE PAGO"
        Me.RadGroupBox1.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadGroupBox1.Location = New System.Drawing.Point(24, 28)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        '
        '
        '
        Me.RadGroupBox1.RootElement.ControlBounds = New System.Drawing.Rectangle(24, 28, 200, 100)
        Me.RadGroupBox1.RootElement.Padding = New System.Windows.Forms.Padding(2, 18, 2, 2)
        Me.RadGroupBox1.Size = New System.Drawing.Size(462, 84)
        Me.RadGroupBox1.TabIndex = 10
        Me.RadGroupBox1.Text = "FORMA DE PAGO"
        '
        'cmbforma
        '
        Me.cmbforma.FormattingEnabled = True
        Me.cmbforma.Location = New System.Drawing.Point(29, 36)
        Me.cmbforma.Name = "cmbforma"
        Me.cmbforma.Size = New System.Drawing.Size(400, 21)
        Me.cmbforma.TabIndex = 0
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.RadGroupBox3.Controls.Add(Me.RadButton1)
        Me.RadGroupBox3.Controls.Add(Me.btnsalir)
        Me.RadGroupBox3.Controls.Add(Me.btningresar)
        Me.RadGroupBox3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox3.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office
        Me.RadGroupBox3.HeaderText = "ACCIONES"
        Me.RadGroupBox3.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadGroupBox3.Location = New System.Drawing.Point(24, 130)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        '
        '
        '
        Me.RadGroupBox3.RootElement.ControlBounds = New System.Drawing.Rectangle(24, 130, 200, 100)
        Me.RadGroupBox3.RootElement.Padding = New System.Windows.Forms.Padding(2, 18, 2, 2)
        Me.RadGroupBox3.Size = New System.Drawing.Size(477, 76)
        Me.RadGroupBox3.TabIndex = 11
        Me.RadGroupBox3.Text = "ACCIONES"
        '
        'btnsalir
        '
        Me.btnsalir.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.btnsalir.Image = Global.CAJAS.My.Resources.Resources.IcoSalir
        Me.btnsalir.ImageAlignment = System.Drawing.ContentAlignment.TopCenter
        Me.btnsalir.Location = New System.Drawing.Point(390, 21)
        Me.btnsalir.Name = "btnsalir"
        '
        '
        '
        Me.btnsalir.RootElement.ControlBounds = New System.Drawing.Rectangle(390, 21, 110, 24)
        Me.btnsalir.Size = New System.Drawing.Size(72, 47)
        Me.btnsalir.TabIndex = 1
        Me.btnsalir.Text = "SALIR"
        Me.btnsalir.TextAlignment = System.Drawing.ContentAlignment.BottomCenter
        '
        'btningresar
        '
        Me.btningresar.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.btningresar.Image = Global.CAJAS.My.Resources.Resources.IcoAgregar
        Me.btningresar.ImageAlignment = System.Drawing.ContentAlignment.TopCenter
        Me.btningresar.Location = New System.Drawing.Point(18, 21)
        Me.btningresar.Name = "btningresar"
        '
        '
        '
        Me.btningresar.RootElement.ControlBounds = New System.Drawing.Rectangle(18, 21, 110, 24)
        Me.btningresar.Size = New System.Drawing.Size(72, 47)
        Me.btningresar.TabIndex = 0
        Me.btningresar.Text = "Aceptar"
        Me.btningresar.TextAlignment = System.Drawing.ContentAlignment.BottomCenter
        '
        'RadButton1
        '
        Me.RadButton1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.RadButton1.Font = New System.Drawing.Font("Segoe UI", 14.0!)
        Me.RadButton1.Image = Global.CAJAS.My.Resources.Resources.Desglozar
        Me.RadButton1.ImageAlignment = System.Drawing.ContentAlignment.TopCenter
        Me.RadButton1.Location = New System.Drawing.Point(188, 21)
        Me.RadButton1.Name = "RadButton1"
        '
        '
        '
        Me.RadButton1.RootElement.ControlBounds = New System.Drawing.Rectangle(18, 21, 110, 24)
        Me.RadButton1.Size = New System.Drawing.Size(72, 47)
        Me.RadButton1.TabIndex = 2
        Me.RadButton1.Text = "Mixto"
        Me.RadButton1.TextAlignment = System.Drawing.ContentAlignment.BottomCenter
        '
        'FrmPago
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(539, 222)
        Me.Controls.Add(Me.RadGroupBox3)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "FrmPago"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = ""
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        CType(Me.btnsalir, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btningresar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnsalir As Telerik.WinControls.UI.RadButton
    Friend WithEvents btningresar As Telerik.WinControls.UI.RadButton
    Public WithEvents cmbforma As ComboBox
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
End Class


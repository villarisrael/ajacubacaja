<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRestriccionDesc
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
        Me.txtusuario = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.RadLabel2 = New Telerik.WinControls.UI.RadLabel()
        Me.txtcontrasena = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.RadLabel1 = New Telerik.WinControls.UI.RadLabel()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.btnsalir = New Telerik.WinControls.UI.RadButton()
        Me.btningresar = New Telerik.WinControls.UI.RadButton()
        Me.RadLabel3 = New Telerik.WinControls.UI.RadLabel()
        Me.txtdescuento = New DevComponents.DotNetBar.Controls.TextBoxX()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.btnsalir, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btningresar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtusuario
        '
        '
        '
        '
        Me.txtusuario.Border.Class = "TextBoxBorder"
        Me.txtusuario.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtusuario.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtusuario.Location = New System.Drawing.Point(152, 21)
        Me.txtusuario.Name = "txtusuario"
        Me.txtusuario.Size = New System.Drawing.Size(283, 30)
        Me.txtusuario.TabIndex = 0
        Me.txtusuario.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtusuario.WatermarkImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RadLabel2
        '
        Me.RadLabel2.AutoSize = True
        Me.RadLabel2.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(18, 65)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(128, 27)
        Me.RadLabel2.TabIndex = 7
        Me.RadLabel2.Text = "Contraseña:"
        '
        'txtcontrasena
        '
        '
        '
        '
        Me.txtcontrasena.Border.Class = "TextBoxBorder"
        Me.txtcontrasena.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtcontrasena.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcontrasena.Location = New System.Drawing.Point(152, 65)
        Me.txtcontrasena.Name = "txtcontrasena"
        Me.txtcontrasena.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtcontrasena.Size = New System.Drawing.Size(283, 30)
        Me.txtcontrasena.TabIndex = 1
        Me.txtcontrasena.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtcontrasena.WatermarkImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RadLabel1
        '
        Me.RadLabel1.AutoSize = True
        Me.RadLabel1.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(54, 22)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(91, 27)
        Me.RadLabel1.TabIndex = 8
        Me.RadLabel1.Text = "Usuario:"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.btnsalir)
        Me.RadGroupBox3.Controls.Add(Me.btningresar)
        Me.RadGroupBox3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox3.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office
        Me.RadGroupBox3.HeaderText = "ACCIONES"
        Me.RadGroupBox3.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadGroupBox3.Location = New System.Drawing.Point(12, 170)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        '
        '
        '
        Me.RadGroupBox3.RootElement.Padding = New System.Windows.Forms.Padding(2, 18, 2, 2)
        Me.RadGroupBox3.Size = New System.Drawing.Size(443, 76)
        Me.RadGroupBox3.TabIndex = 3
        Me.RadGroupBox3.Text = "ACCIONES"
        '
        'btnsalir
        '
        Me.btnsalir.Image = Global.CAJAS.My.Resources.Resources.IcoSalir
        Me.btnsalir.ImageAlignment = System.Drawing.ContentAlignment.TopCenter
        Me.btnsalir.Location = New System.Drawing.Point(22, 21)
        Me.btnsalir.Name = "btnsalir"
        Me.btnsalir.Size = New System.Drawing.Size(72, 47)
        Me.btnsalir.TabIndex = 1
        Me.btnsalir.Text = "CANCELAR"
        Me.btnsalir.TextAlignment = System.Drawing.ContentAlignment.BottomCenter
        '
        'btningresar
        '
        Me.btningresar.Image = Global.CAJAS.My.Resources.Resources.Login1
        Me.btningresar.ImageAlignment = System.Drawing.ContentAlignment.TopCenter
        Me.btningresar.Location = New System.Drawing.Point(357, 24)
        Me.btningresar.Name = "btningresar"
        Me.btningresar.Size = New System.Drawing.Size(72, 47)
        Me.btningresar.TabIndex = 0
        Me.btningresar.Text = "INGRESAR"
        Me.btningresar.TextAlignment = System.Drawing.ContentAlignment.BottomCenter
        '
        'RadLabel3
        '
        Me.RadLabel3.AutoSize = True
        Me.RadLabel3.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(57, 117)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(221, 27)
        Me.RadLabel3.TabIndex = 9
        Me.RadLabel3.Text = "Descuento permitido:"
        '
        'txtdescuento
        '
        '
        '
        '
        Me.txtdescuento.Border.Class = "TextBoxBorder"
        Me.txtdescuento.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtdescuento.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdescuento.Location = New System.Drawing.Point(284, 117)
        Me.txtdescuento.Name = "txtdescuento"
        Me.txtdescuento.Size = New System.Drawing.Size(151, 30)
        Me.txtdescuento.TabIndex = 2
        Me.txtdescuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtdescuento.WatermarkImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmRestriccionDesc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(468, 264)
        Me.Controls.Add(Me.txtdescuento)
        Me.Controls.Add(Me.RadLabel3)
        Me.Controls.Add(Me.txtusuario)
        Me.Controls.Add(Me.RadLabel2)
        Me.Controls.Add(Me.txtcontrasena)
        Me.Controls.Add(Me.RadLabel1)
        Me.Controls.Add(Me.RadGroupBox3)
        Me.ForeColor = System.Drawing.Color.Black
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmRestriccionDesc"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = ""
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        CType(Me.btnsalir, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btningresar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtusuario As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents RadLabel2 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txtcontrasena As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents RadLabel1 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnsalir As Telerik.WinControls.UI.RadButton
    Friend WithEvents btningresar As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel3 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txtdescuento As DevComponents.DotNetBar.Controls.TextBoxX
End Class


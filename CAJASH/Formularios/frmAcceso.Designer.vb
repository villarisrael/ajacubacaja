<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAcceso
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAcceso))
        Me.RadLabel1 = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel2 = New Telerik.WinControls.UI.RadLabel()
        Me.lblfolio = New Telerik.WinControls.UI.RadLabel()
        Me.txtusuario = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtclave = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtcontrasena = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.btnsalir = New Telerik.WinControls.UI.RadButton()
        Me.btningresar = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadLabel4 = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel5 = New Telerik.WinControls.UI.RadLabel()
        Me.lblserie = New Telerik.WinControls.UI.RadLabel()
        Me.lblcaja = New Telerik.WinControls.UI.RadLabel()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfolio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.btnsalir, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btningresar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblserie, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblcaja, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadLabel1
        '
        Me.RadLabel1.AutoSize = True
        Me.RadLabel1.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(44, 75)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(91, 27)
        Me.RadLabel1.TabIndex = 3
        Me.RadLabel1.Text = "Usuario:"
        '
        'RadLabel2
        '
        Me.RadLabel2.AutoSize = True
        Me.RadLabel2.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(8, 118)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(128, 27)
        Me.RadLabel2.TabIndex = 2
        Me.RadLabel2.Text = "Contraseña:"
        '
        'lblfolio
        '
        Me.lblfolio.AutoSize = True
        Me.lblfolio.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfolio.Location = New System.Drawing.Point(72, 33)
        Me.lblfolio.Name = "lblfolio"
        Me.lblfolio.Size = New System.Drawing.Size(63, 27)
        Me.lblfolio.TabIndex = 5
        Me.lblfolio.Text = "Folio:"
        '
        'txtusuario
        '
        '
        '
        '
        Me.txtusuario.Border.Class = "TextBoxBorder"
        Me.txtusuario.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtusuario.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtusuario.Location = New System.Drawing.Point(142, 74)
        Me.txtusuario.Name = "txtusuario"
        Me.txtusuario.Size = New System.Drawing.Size(302, 30)
        Me.txtusuario.TabIndex = 1
        Me.txtusuario.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtusuario.WatermarkImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtclave
        '
        '
        '
        '
        Me.txtclave.Border.Class = "TextBoxBorder"
        Me.txtclave.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtclave.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtclave.Location = New System.Drawing.Point(142, 32)
        Me.txtclave.Name = "txtclave"
        Me.txtclave.Size = New System.Drawing.Size(175, 30)
        Me.txtclave.TabIndex = 0
        Me.txtclave.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtcontrasena
        '
        '
        '
        '
        Me.txtcontrasena.Border.Class = "TextBoxBorder"
        Me.txtcontrasena.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtcontrasena.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcontrasena.Location = New System.Drawing.Point(142, 118)
        Me.txtcontrasena.Name = "txtcontrasena"
        Me.txtcontrasena.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtcontrasena.Size = New System.Drawing.Size(302, 30)
        Me.txtcontrasena.TabIndex = 2
        Me.txtcontrasena.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtcontrasena.WatermarkImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
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
        Me.RadGroupBox3.Location = New System.Drawing.Point(27, 286)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        '
        '
        '
        Me.RadGroupBox3.RootElement.Padding = New System.Windows.Forms.Padding(2, 18, 2, 2)
        Me.RadGroupBox3.Size = New System.Drawing.Size(477, 76)
        Me.RadGroupBox3.TabIndex = 0
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
        Me.btnsalir.Text = "SALIR"
        Me.btnsalir.TextAlignment = System.Drawing.ContentAlignment.BottomCenter
        '
        'btningresar
        '
        Me.btningresar.Image = Global.CAJAS.My.Resources.Resources.Login1
        Me.btningresar.ImageAlignment = System.Drawing.ContentAlignment.TopCenter
        Me.btningresar.Location = New System.Drawing.Point(381, 21)
        Me.btningresar.Name = "btningresar"
        Me.btningresar.Size = New System.Drawing.Size(72, 47)
        Me.btningresar.TabIndex = 0
        Me.btningresar.Text = "INGRESAR"
        Me.btningresar.TextAlignment = System.Drawing.ContentAlignment.BottomCenter
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtusuario)
        Me.RadGroupBox1.Controls.Add(Me.lblfolio)
        Me.RadGroupBox1.Controls.Add(Me.txtclave)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.txtcontrasena)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office
        Me.RadGroupBox1.HeaderText = "DATOS DE ACCESO"
        Me.RadGroupBox1.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadGroupBox1.Location = New System.Drawing.Point(27, 86)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        '
        '
        '
        Me.RadGroupBox1.RootElement.Padding = New System.Windows.Forms.Padding(2, 18, 2, 2)
        Me.RadGroupBox1.Size = New System.Drawing.Size(477, 184)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = "DATOS DE ACCESO"
        '
        'RadLabel4
        '
        Me.RadLabel4.AutoSize = True
        Me.RadLabel4.Font = New System.Drawing.Font("Segoe UI", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(40, 14)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(98, 44)
        Me.RadLabel4.TabIndex = 8
        Me.RadLabel4.Text = "Serie: "
        '
        'RadLabel5
        '
        Me.RadLabel5.AutoSize = True
        Me.RadLabel5.Font = New System.Drawing.Font("Segoe UI", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(354, 13)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(80, 44)
        Me.RadLabel5.TabIndex = 1
        Me.RadLabel5.Text = "Caja:"
        '
        'lblserie
        '
        Me.lblserie.AutoSize = True
        Me.lblserie.Font = New System.Drawing.Font("Segoe UI", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblserie.ForeColor = System.Drawing.Color.Teal
        Me.lblserie.Location = New System.Drawing.Point(133, 9)
        Me.lblserie.Name = "lblserie"
        Me.lblserie.Size = New System.Drawing.Size(34, 53)
        Me.lblserie.TabIndex = 0
        Me.lblserie.Text = "x"
        '
        'lblcaja
        '
        Me.lblcaja.AutoSize = True
        Me.lblcaja.Font = New System.Drawing.Font("Segoe UI", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcaja.ForeColor = System.Drawing.Color.Teal
        Me.lblcaja.Location = New System.Drawing.Point(438, 7)
        Me.lblcaja.Name = "lblcaja"
        Me.lblcaja.Size = New System.Drawing.Size(34, 53)
        Me.lblcaja.TabIndex = 1
        Me.lblcaja.Text = "x"
        '
        'FrmAcceso
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(531, 390)
        Me.Controls.Add(Me.lblcaja)
        Me.Controls.Add(Me.lblserie)
        Me.Controls.Add(Me.RadLabel5)
        Me.Controls.Add(Me.RadLabel4)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Controls.Add(Me.RadGroupBox3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FrmAcceso"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = ""
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfolio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        CType(Me.btnsalir, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btningresar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblserie, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblcaja, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadLabel1 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel2 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents lblfolio As Telerik.WinControls.UI.RadLabel
    Friend WithEvents btningresar As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsalir As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtusuario As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents txtclave As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents txtcontrasena As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadLabel4 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel5 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents lblserie As Telerik.WinControls.UI.RadLabel
    Friend WithEvents lblcaja As Telerik.WinControls.UI.RadLabel
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
End Class


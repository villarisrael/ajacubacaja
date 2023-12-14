<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmvalidafactura
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
        Me.txtnombre = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtcalle = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtnumext = New System.Windows.Forms.TextBox()
        Me.txtnuminterior = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtcolonia = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtpoblacion = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtrfc = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtestado = New System.Windows.Forms.TextBox()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.BTNGUARDAR = New Telerik.WinControls.UI.RadButton()
        Me.btncancelar = New Telerik.WinControls.UI.RadButton()
        Me.btnaceptar = New Telerik.WinControls.UI.RadButton()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtcp = New System.Windows.Forms.MaskedTextBox()
        Me.txtmail = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtPais = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtdelegacion = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtObservaciones = New System.Windows.Forms.TextBox()
        Me.groupcfdi = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.lblReg = New System.Windows.Forms.Label()
        Me.cmbmetodo = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cmbuso = New System.Windows.Forms.ComboBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.cmbformapago = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.GroupPanel1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.chksinrecibo = New System.Windows.Forms.CheckBox()
        Me.chkmandarmail = New System.Windows.Forms.CheckBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.txtcuentabancaria = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.cmbbanco = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.lbltipodeusuario = New System.Windows.Forms.Label()
        Me.cmbRegimen = New System.Windows.Forms.ComboBox()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.BTNGUARDAR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btncancelar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnaceptar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupcfdi.SuspendLayout()
        Me.GroupPanel1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.DarkRed
        Me.Label1.Location = New System.Drawing.Point(13, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "*Nombre"
        '
        'txtnombre
        '
        Me.txtnombre.Location = New System.Drawing.Point(103, 13)
        Me.txtnombre.MaxLength = 255
        Me.txtnombre.Name = "txtnombre"
        Me.txtnombre.Size = New System.Drawing.Size(423, 20)
        Me.txtnombre.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.DarkRed
        Me.Label2.Location = New System.Drawing.Point(13, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "*Calle"
        '
        'txtcalle
        '
        Me.txtcalle.Location = New System.Drawing.Point(103, 42)
        Me.txtcalle.MaxLength = 255
        Me.txtcalle.Name = "txtcalle"
        Me.txtcalle.Size = New System.Drawing.Size(423, 20)
        Me.txtcalle.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.DarkRed
        Me.Label3.Location = New System.Drawing.Point(13, 71)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Num Exterior"
        '
        'txtnumext
        '
        Me.txtnumext.Location = New System.Drawing.Point(103, 71)
        Me.txtnumext.MaxLength = 40
        Me.txtnumext.Name = "txtnumext"
        Me.txtnumext.Size = New System.Drawing.Size(87, 20)
        Me.txtnumext.TabIndex = 2
        '
        'txtnuminterior
        '
        Me.txtnuminterior.Location = New System.Drawing.Point(309, 71)
        Me.txtnuminterior.MaxLength = 45
        Me.txtnuminterior.Name = "txtnuminterior"
        Me.txtnuminterior.Size = New System.Drawing.Size(87, 20)
        Me.txtnuminterior.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.DarkRed
        Me.Label4.Location = New System.Drawing.Point(216, 73)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Num Interior"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.DarkRed
        Me.Label5.Location = New System.Drawing.Point(13, 100)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "*Colonia"
        '
        'txtcolonia
        '
        Me.txtcolonia.Location = New System.Drawing.Point(103, 100)
        Me.txtcolonia.MaxLength = 255
        Me.txtcolonia.Name = "txtcolonia"
        Me.txtcolonia.Size = New System.Drawing.Size(423, 20)
        Me.txtcolonia.TabIndex = 4
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.DarkRed
        Me.Label6.Location = New System.Drawing.Point(14, 129)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(63, 13)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "*Poblacion"
        '
        'txtpoblacion
        '
        Me.txtpoblacion.Location = New System.Drawing.Point(103, 129)
        Me.txtpoblacion.MaxLength = 255
        Me.txtpoblacion.Name = "txtpoblacion"
        Me.txtpoblacion.Size = New System.Drawing.Size(423, 20)
        Me.txtpoblacion.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ForeColor = System.Drawing.Color.DarkRed
        Me.Label7.Location = New System.Drawing.Point(13, 216)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(25, 13)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "*CP"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.ForeColor = System.Drawing.Color.DarkRed
        Me.Label8.Location = New System.Drawing.Point(215, 218)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(32, 13)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "*RFC"
        '
        'txtrfc
        '
        Me.txtrfc.Location = New System.Drawing.Point(276, 215)
        Me.txtrfc.MaxLength = 15
        Me.txtrfc.Name = "txtrfc"
        Me.txtrfc.Size = New System.Drawing.Size(164, 20)
        Me.txtrfc.TabIndex = 10
        Me.txtrfc.Text = "XAXX010101000"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ForeColor = System.Drawing.Color.DarkRed
        Me.Label9.Location = New System.Drawing.Point(13, 187)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(47, 13)
        Me.Label9.TabIndex = 16
        Me.Label9.Text = "*Estado"
        '
        'txtestado
        '
        Me.txtestado.Location = New System.Drawing.Point(103, 187)
        Me.txtestado.MaxLength = 255
        Me.txtestado.Name = "txtestado"
        Me.txtestado.Size = New System.Drawing.Size(194, 20)
        Me.txtestado.TabIndex = 7
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.RadGroupBox2.Controls.Add(Me.BTNGUARDAR)
        Me.RadGroupBox2.Controls.Add(Me.btncancelar)
        Me.RadGroupBox2.Controls.Add(Me.btnaceptar)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office
        Me.RadGroupBox2.HeaderText = "ACCIONES"
        Me.RadGroupBox2.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadGroupBox2.Location = New System.Drawing.Point(434, 381)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        '
        '
        '
        Me.RadGroupBox2.RootElement.ControlBounds = New System.Drawing.Rectangle(434, 381, 200, 100)
        Me.RadGroupBox2.RootElement.Padding = New System.Windows.Forms.Padding(2, 18, 2, 2)
        Me.RadGroupBox2.Size = New System.Drawing.Size(553, 78)
        Me.RadGroupBox2.TabIndex = 22
        Me.RadGroupBox2.Text = "ACCIONES"
        '
        'BTNGUARDAR
        '
        Me.BTNGUARDAR.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.BTNGUARDAR.Image = Global.CAJAS.My.Resources.Resources.Contacts_Folder_32
        Me.BTNGUARDAR.ImageAlignment = System.Drawing.ContentAlignment.TopCenter
        Me.BTNGUARDAR.Location = New System.Drawing.Point(232, 21)
        Me.BTNGUARDAR.Name = "BTNGUARDAR"
        '
        '
        '
        Me.BTNGUARDAR.RootElement.ControlBounds = New System.Drawing.Rectangle(232, 21, 110, 24)
        Me.BTNGUARDAR.Size = New System.Drawing.Size(97, 47)
        Me.BTNGUARDAR.TabIndex = 2
        Me.BTNGUARDAR.Text = "Guardar"
        Me.BTNGUARDAR.TextAlignment = System.Drawing.ContentAlignment.BottomCenter
        '
        'btncancelar
        '
        Me.btncancelar.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.btncancelar.Image = Global.CAJAS.My.Resources.Resources.INI
        Me.btncancelar.ImageAlignment = System.Drawing.ContentAlignment.TopCenter
        Me.btncancelar.Location = New System.Drawing.Point(29, 21)
        Me.btncancelar.Name = "btncancelar"
        '
        '
        '
        Me.btncancelar.RootElement.ControlBounds = New System.Drawing.Rectangle(29, 21, 110, 24)
        Me.btncancelar.Size = New System.Drawing.Size(97, 47)
        Me.btncancelar.TabIndex = 1
        Me.btncancelar.Text = "CANCELAR"
        Me.btncancelar.TextAlignment = System.Drawing.ContentAlignment.BottomCenter
        '
        'btnaceptar
        '
        Me.btnaceptar.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.btnaceptar.Image = Global.CAJAS.My.Resources.Resources.IcoAgregar
        Me.btnaceptar.ImageAlignment = System.Drawing.ContentAlignment.TopCenter
        Me.btnaceptar.Location = New System.Drawing.Point(434, 21)
        Me.btnaceptar.Name = "btnaceptar"
        '
        '
        '
        Me.btnaceptar.RootElement.ControlBounds = New System.Drawing.Rectangle(434, 21, 110, 24)
        Me.btnaceptar.Size = New System.Drawing.Size(97, 47)
        Me.btnaceptar.TabIndex = 0
        Me.btnaceptar.Text = "ACEPTAR"
        Me.btnaceptar.TextAlignment = System.Drawing.ContentAlignment.BottomCenter
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.ForeColor = System.Drawing.Color.DarkRed
        Me.Label10.Location = New System.Drawing.Point(14, 245)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(39, 13)
        Me.Label10.TabIndex = 23
        Me.Label10.Text = "*email"
        '
        'txtcp
        '
        Me.txtcp.Location = New System.Drawing.Point(103, 216)
        Me.txtcp.Mask = "00000"
        Me.txtcp.Name = "txtcp"
        Me.txtcp.Size = New System.Drawing.Size(54, 20)
        Me.txtcp.TabIndex = 9
        '
        'txtmail
        '
        Me.txtmail.Location = New System.Drawing.Point(103, 245)
        Me.txtmail.Name = "txtmail"
        Me.txtmail.Size = New System.Drawing.Size(378, 20)
        Me.txtmail.TabIndex = 11
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.ForeColor = System.Drawing.Color.DarkRed
        Me.Label11.Location = New System.Drawing.Point(319, 186)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(27, 13)
        Me.Label11.TabIndex = 26
        Me.Label11.Text = "Pais"
        '
        'txtPais
        '
        Me.txtPais.Location = New System.Drawing.Point(357, 189)
        Me.txtPais.MaxLength = 255
        Me.txtPais.Name = "txtPais"
        Me.txtPais.Size = New System.Drawing.Size(167, 20)
        Me.txtPais.TabIndex = 8
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.ForeColor = System.Drawing.Color.DarkRed
        Me.Label12.Location = New System.Drawing.Point(14, 158)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(65, 13)
        Me.Label12.TabIndex = 28
        Me.Label12.Text = "Delegacion"
        '
        'txtdelegacion
        '
        Me.txtdelegacion.Location = New System.Drawing.Point(103, 158)
        Me.txtdelegacion.MaxLength = 255
        Me.txtdelegacion.Name = "txtdelegacion"
        Me.txtdelegacion.Size = New System.Drawing.Size(423, 20)
        Me.txtdelegacion.TabIndex = 6
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.ForeColor = System.Drawing.Color.DarkRed
        Me.Label13.Location = New System.Drawing.Point(14, 287)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(82, 13)
        Me.Label13.TabIndex = 29
        Me.Label13.Text = "Observaciones"
        '
        'txtObservaciones
        '
        Me.txtObservaciones.Location = New System.Drawing.Point(103, 280)
        Me.txtObservaciones.Name = "txtObservaciones"
        Me.txtObservaciones.Size = New System.Drawing.Size(378, 20)
        Me.txtObservaciones.TabIndex = 30
        '
        'groupcfdi
        '
        Me.groupcfdi.CanvasColor = System.Drawing.SystemColors.Control
        Me.groupcfdi.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.groupcfdi.Controls.Add(Me.cmbRegimen)
        Me.groupcfdi.Controls.Add(Me.lblReg)
        Me.groupcfdi.Controls.Add(Me.cmbmetodo)
        Me.groupcfdi.Controls.Add(Me.Label16)
        Me.groupcfdi.Controls.Add(Me.cmbuso)
        Me.groupcfdi.Controls.Add(Me.Label17)
        Me.groupcfdi.Controls.Add(Me.cmbformapago)
        Me.groupcfdi.Controls.Add(Me.Label14)
        Me.groupcfdi.DisabledBackColor = System.Drawing.Color.Empty
        Me.groupcfdi.Location = New System.Drawing.Point(599, 149)
        Me.groupcfdi.Name = "groupcfdi"
        Me.groupcfdi.Size = New System.Drawing.Size(359, 134)
        '
        '
        '
        Me.groupcfdi.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.groupcfdi.Style.BackColorGradientAngle = 90
        Me.groupcfdi.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.groupcfdi.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.groupcfdi.Style.BorderBottomWidth = 1
        Me.groupcfdi.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.groupcfdi.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.groupcfdi.Style.BorderLeftWidth = 1
        Me.groupcfdi.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.groupcfdi.Style.BorderRightWidth = 1
        Me.groupcfdi.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.groupcfdi.Style.BorderTopWidth = 1
        Me.groupcfdi.Style.CornerDiameter = 4
        Me.groupcfdi.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.groupcfdi.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.groupcfdi.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.groupcfdi.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.groupcfdi.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.groupcfdi.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.groupcfdi.TabIndex = 38
        Me.groupcfdi.Text = "CFDI"
        '
        'lblReg
        '
        Me.lblReg.AutoSize = True
        Me.lblReg.BackColor = System.Drawing.Color.Transparent
        Me.lblReg.ForeColor = System.Drawing.Color.DarkRed
        Me.lblReg.Location = New System.Drawing.Point(12, 90)
        Me.lblReg.Name = "lblReg"
        Me.lblReg.Size = New System.Drawing.Size(83, 13)
        Me.lblReg.TabIndex = 42
        Me.lblReg.Text = "Regimen Fiscal"
        '
        'cmbmetodo
        '
        Me.cmbmetodo.FormattingEnabled = True
        Me.cmbmetodo.Location = New System.Drawing.Point(112, 29)
        Me.cmbmetodo.Name = "cmbmetodo"
        Me.cmbmetodo.Size = New System.Drawing.Size(212, 21)
        Me.cmbmetodo.TabIndex = 40
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.ForeColor = System.Drawing.Color.DarkRed
        Me.Label16.Location = New System.Drawing.Point(12, 29)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(94, 13)
        Me.Label16.TabIndex = 39
        Me.Label16.Text = "Metodo de pago"
        '
        'cmbuso
        '
        Me.cmbuso.FormattingEnabled = True
        Me.cmbuso.Location = New System.Drawing.Point(112, 56)
        Me.cmbuso.Name = "cmbuso"
        Me.cmbuso.Size = New System.Drawing.Size(213, 21)
        Me.cmbuso.TabIndex = 38
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.ForeColor = System.Drawing.Color.DarkRed
        Me.Label17.Location = New System.Drawing.Point(12, 59)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(73, 13)
        Me.Label17.TabIndex = 37
        Me.Label17.Text = "Uso del CFDI"
        '
        'cmbformapago
        '
        Me.cmbformapago.FormattingEnabled = True
        Me.cmbformapago.Location = New System.Drawing.Point(112, 6)
        Me.cmbformapago.Name = "cmbformapago"
        Me.cmbformapago.Size = New System.Drawing.Size(212, 21)
        Me.cmbformapago.TabIndex = 34
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.ForeColor = System.Drawing.Color.DarkRed
        Me.Label14.Location = New System.Drawing.Point(12, 6)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(85, 13)
        Me.Label14.TabIndex = 33
        Me.Label14.Text = "Forma de pago"
        '
        'GroupPanel1
        '
        Me.GroupPanel1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel1.Controls.Add(Me.chksinrecibo)
        Me.GroupPanel1.Controls.Add(Me.chkmandarmail)
        Me.GroupPanel1.Controls.Add(Me.CheckBox1)
        Me.GroupPanel1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel1.Location = New System.Drawing.Point(599, 30)
        Me.GroupPanel1.Name = "GroupPanel1"
        Me.GroupPanel1.Size = New System.Drawing.Size(345, 112)
        '
        '
        '
        Me.GroupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupPanel1.Style.BackColorGradientAngle = 90
        Me.GroupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GroupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderBottomWidth = 1
        Me.GroupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderLeftWidth = 1
        Me.GroupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderRightWidth = 1
        Me.GroupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderTopWidth = 1
        Me.GroupPanel1.Style.CornerDiameter = 4
        Me.GroupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel1.TabIndex = 37
        Me.GroupPanel1.Text = "Miscelanea"
        '
        'chksinrecibo
        '
        Me.chksinrecibo.AutoSize = True
        Me.chksinrecibo.BackColor = System.Drawing.Color.Transparent
        Me.chksinrecibo.Location = New System.Drawing.Point(35, 53)
        Me.chksinrecibo.Name = "chksinrecibo"
        Me.chksinrecibo.Size = New System.Drawing.Size(196, 17)
        Me.chksinrecibo.TabIndex = 36
        Me.chksinrecibo.Text = "Generar factura sin grabar recibo"
        Me.chksinrecibo.UseVisualStyleBackColor = False
        '
        'chkmandarmail
        '
        Me.chkmandarmail.AutoSize = True
        Me.chkmandarmail.BackColor = System.Drawing.Color.Transparent
        Me.chkmandarmail.Location = New System.Drawing.Point(35, 27)
        Me.chkmandarmail.Name = "chkmandarmail"
        Me.chkmandarmail.Size = New System.Drawing.Size(90, 17)
        Me.chkmandarmail.TabIndex = 34
        Me.chkmandarmail.Text = "Mandar mail"
        Me.chkmandarmail.UseVisualStyleBackColor = False
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.BackColor = System.Drawing.Color.Transparent
        Me.CheckBox1.Location = New System.Drawing.Point(35, 3)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(167, 17)
        Me.CheckBox1.TabIndex = 33
        Me.CheckBox1.Text = "Visualizar antes de imprimir"
        Me.CheckBox1.UseVisualStyleBackColor = False
        '
        'txtcuentabancaria
        '
        '
        '
        '
        Me.txtcuentabancaria.Border.Class = "TextBoxBorder"
        Me.txtcuentabancaria.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtcuentabancaria.Location = New System.Drawing.Point(103, 340)
        Me.txtcuentabancaria.MaxLength = 10
        Me.txtcuentabancaria.Name = "txtcuentabancaria"
        Me.txtcuentabancaria.PreventEnterBeep = True
        Me.txtcuentabancaria.Size = New System.Drawing.Size(117, 22)
        Me.txtcuentabancaria.TabIndex = 44
        '
        'cmbbanco
        '
        Me.cmbbanco.DisplayMember = "Text"
        Me.cmbbanco.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbbanco.FormattingEnabled = True
        Me.cmbbanco.ItemHeight = 15
        Me.cmbbanco.Location = New System.Drawing.Point(103, 314)
        Me.cmbbanco.Name = "cmbbanco"
        Me.cmbbanco.Size = New System.Drawing.Size(393, 21)
        Me.cmbbanco.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.cmbbanco.TabIndex = 43
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.ForeColor = System.Drawing.Color.DarkRed
        Me.Label18.Location = New System.Drawing.Point(14, 342)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(44, 13)
        Me.Label18.TabIndex = 42
        Me.Label18.Text = "Cuenta"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.ForeColor = System.Drawing.Color.DarkRed
        Me.Label15.Location = New System.Drawing.Point(14, 321)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(38, 13)
        Me.Label15.TabIndex = 41
        Me.Label15.Text = "Banco"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(17, 402)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(99, 13)
        Me.Label19.TabIndex = 45
        Me.Label19.Text = "TIPO DE USUARIO"
        '
        'lbltipodeusuario
        '
        Me.lbltipodeusuario.AutoSize = True
        Me.lbltipodeusuario.Location = New System.Drawing.Point(132, 402)
        Me.lbltipodeusuario.Name = "lbltipodeusuario"
        Me.lbltipodeusuario.Size = New System.Drawing.Size(0, 13)
        Me.lbltipodeusuario.TabIndex = 46
        '
        'cmbRegimen
        '
        Me.cmbRegimen.FormattingEnabled = True
        Me.cmbRegimen.Location = New System.Drawing.Point(111, 83)
        Me.cmbRegimen.Name = "cmbRegimen"
        Me.cmbRegimen.Size = New System.Drawing.Size(213, 21)
        Me.cmbRegimen.TabIndex = 43
        '
        'Frmvalidafactura
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(999, 463)
        Me.Controls.Add(Me.lbltipodeusuario)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.txtcuentabancaria)
        Me.Controls.Add(Me.cmbbanco)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.groupcfdi)
        Me.Controls.Add(Me.GroupPanel1)
        Me.Controls.Add(Me.txtObservaciones)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txtdelegacion)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtPais)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtmail)
        Me.Controls.Add(Me.txtcp)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.RadGroupBox2)
        Me.Controls.Add(Me.txtestado)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtrfc)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtpoblacion)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtcolonia)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtnuminterior)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtnumext)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtcalle)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtnombre)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Frmvalidafactura"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Validando Datos"
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.BTNGUARDAR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btncancelar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnaceptar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupcfdi.ResumeLayout(False)
        Me.groupcfdi.PerformLayout()
        Me.GroupPanel1.ResumeLayout(False)
        Me.GroupPanel1.PerformLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtnombre As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtcalle As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtnumext As System.Windows.Forms.TextBox
    Friend WithEvents txtnuminterior As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtcolonia As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtpoblacion As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtrfc As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtestado As System.Windows.Forms.TextBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btncancelar As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnaceptar As Telerik.WinControls.UI.RadButton
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtcp As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtmail As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtPais As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtdelegacion As System.Windows.Forms.TextBox
    Friend WithEvents BTNGUARDAR As Telerik.WinControls.UI.RadButton
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtObservaciones As System.Windows.Forms.TextBox
    Friend WithEvents groupcfdi As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents cmbmetodo As ComboBox
    Friend WithEvents Label16 As Label
    Friend WithEvents cmbuso As ComboBox
    Friend WithEvents Label17 As Label
    Friend WithEvents cmbformapago As ComboBox
    Friend WithEvents Label14 As Label
    Friend WithEvents GroupPanel1 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents chksinrecibo As CheckBox
    Friend WithEvents chkmandarmail As CheckBox
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents txtcuentabancaria As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents cmbbanco As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents Label18 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents lbltipodeusuario As Label
    Friend WithEvents lblReg As Label
    Friend WithEvents cmbRegimen As ComboBox
End Class


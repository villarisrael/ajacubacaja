<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmvalidafacturapublico
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
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
        Me.DTPincio = New System.Windows.Forms.DateTimePicker()
        Me.DtPfin = New System.Windows.Forms.DateTimePicker()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.btncargar = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtobservaciones = New System.Windows.Forms.TextBox()
        Me.DtgConceptos = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lblsutotal = New System.Windows.Forms.Label()
        Me.lblIva = New System.Windows.Forms.Label()
        Me.Lbltotal = New System.Windows.Forms.Label()
        Me.txtcaja = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CONCEPTO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CANTIDAD = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PRECIOUNI = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IMPORTE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me._IVA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CONCEPTOSAT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UNIDADSAT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.BTNGUARDAR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btncancelar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnaceptar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DtgConceptos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.DarkRed
        Me.Label1.Location = New System.Drawing.Point(21, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "*Nombre"
        '
        'txtnombre
        '
        Me.txtnombre.Location = New System.Drawing.Point(111, 42)
        Me.txtnombre.MaxLength = 255
        Me.txtnombre.Name = "txtnombre"
        Me.txtnombre.Size = New System.Drawing.Size(423, 20)
        Me.txtnombre.TabIndex = 0
        Me.txtnombre.Text = "PUBLICO EN GENERAL"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.DarkRed
        Me.Label2.Location = New System.Drawing.Point(21, 69)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "*Calle"
        '
        'txtcalle
        '
        Me.txtcalle.Location = New System.Drawing.Point(111, 69)
        Me.txtcalle.MaxLength = 255
        Me.txtcalle.Name = "txtcalle"
        Me.txtcalle.Size = New System.Drawing.Size(387, 20)
        Me.txtcalle.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.DarkRed
        Me.Label3.Location = New System.Drawing.Point(513, 69)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Num Exterior"
        '
        'txtnumext
        '
        Me.txtnumext.Location = New System.Drawing.Point(592, 69)
        Me.txtnumext.MaxLength = 40
        Me.txtnumext.Name = "txtnumext"
        Me.txtnumext.Size = New System.Drawing.Size(69, 20)
        Me.txtnumext.TabIndex = 2
        '
        'txtnuminterior
        '
        Me.txtnuminterior.Location = New System.Drawing.Point(745, 66)
        Me.txtnuminterior.MaxLength = 45
        Me.txtnuminterior.Name = "txtnuminterior"
        Me.txtnuminterior.Size = New System.Drawing.Size(57, 20)
        Me.txtnuminterior.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.DarkRed
        Me.Label4.Location = New System.Drawing.Point(667, 69)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Num Interior"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.DarkRed
        Me.Label5.Location = New System.Drawing.Point(21, 95)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "*Colonia"
        '
        'txtcolonia
        '
        Me.txtcolonia.Location = New System.Drawing.Point(111, 95)
        Me.txtcolonia.MaxLength = 255
        Me.txtcolonia.Name = "txtcolonia"
        Me.txtcolonia.Size = New System.Drawing.Size(185, 20)
        Me.txtcolonia.TabIndex = 4
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.DarkRed
        Me.Label6.Location = New System.Drawing.Point(306, 95)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(63, 13)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "*Poblacion"
        '
        'txtpoblacion
        '
        Me.txtpoblacion.Location = New System.Drawing.Point(395, 95)
        Me.txtpoblacion.MaxLength = 255
        Me.txtpoblacion.Name = "txtpoblacion"
        Me.txtpoblacion.Size = New System.Drawing.Size(423, 20)
        Me.txtpoblacion.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ForeColor = System.Drawing.Color.DarkRed
        Me.Label7.Location = New System.Drawing.Point(316, 150)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(25, 13)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "*CP"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.ForeColor = System.Drawing.Color.DarkRed
        Me.Label8.Location = New System.Drawing.Point(519, 153)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(32, 13)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "*RFC"
        '
        'txtrfc
        '
        Me.txtrfc.Location = New System.Drawing.Point(579, 150)
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
        Me.Label9.Location = New System.Drawing.Point(489, 122)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(47, 13)
        Me.Label9.TabIndex = 16
        Me.Label9.Text = "*Estado"
        '
        'txtestado
        '
        Me.txtestado.Location = New System.Drawing.Point(579, 121)
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
        Me.RadGroupBox2.Location = New System.Drawing.Point(526, 455)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        '
        '
        '
        Me.RadGroupBox2.RootElement.ControlBounds = New System.Drawing.Rectangle(290, 389, 200, 100)
        Me.RadGroupBox2.RootElement.Padding = New System.Windows.Forms.Padding(2, 18, 2, 2)
        Me.RadGroupBox2.Size = New System.Drawing.Size(553, 73)
        Me.RadGroupBox2.TabIndex = 22
        Me.RadGroupBox2.Text = "ACCIONES"
        '
        'BTNGUARDAR
        '
        Me.BTNGUARDAR.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.BTNGUARDAR.Image = Global.CAJAS.My.Resources.Resources.Contacts_Folder_32
        Me.BTNGUARDAR.ImageAlignment = System.Drawing.ContentAlignment.TopCenter
        Me.BTNGUARDAR.Location = New System.Drawing.Point(226, 21)
        Me.BTNGUARDAR.Name = "BTNGUARDAR"
        '
        '
        '
        Me.BTNGUARDAR.RootElement.ControlBounds = New System.Drawing.Rectangle(226, 21, 110, 24)
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
        Me.btncancelar.Location = New System.Drawing.Point(450, 21)
        Me.btncancelar.Name = "btncancelar"
        '
        '
        '
        Me.btncancelar.RootElement.ControlBounds = New System.Drawing.Rectangle(450, 21, 110, 24)
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
        Me.btnaceptar.Location = New System.Drawing.Point(25, 21)
        Me.btnaceptar.Name = "btnaceptar"
        '
        '
        '
        Me.btnaceptar.RootElement.ControlBounds = New System.Drawing.Rectangle(25, 21, 110, 24)
        Me.btnaceptar.Size = New System.Drawing.Size(97, 47)
        Me.btnaceptar.TabIndex = 0
        Me.btnaceptar.Text = "ACEPTAR"
        Me.btnaceptar.TextAlignment = System.Drawing.ContentAlignment.BottomCenter
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.ForeColor = System.Drawing.Color.DarkRed
        Me.Label10.Location = New System.Drawing.Point(22, 174)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(39, 13)
        Me.Label10.TabIndex = 23
        Me.Label10.Text = "*email"
        '
        'txtcp
        '
        Me.txtcp.Location = New System.Drawing.Point(406, 148)
        Me.txtcp.Mask = "00000"
        Me.txtcp.Name = "txtcp"
        Me.txtcp.Size = New System.Drawing.Size(54, 20)
        Me.txtcp.TabIndex = 9
        '
        'txtmail
        '
        Me.txtmail.Location = New System.Drawing.Point(111, 174)
        Me.txtmail.MaxLength = 150
        Me.txtmail.Name = "txtmail"
        Me.txtmail.Size = New System.Drawing.Size(374, 20)
        Me.txtmail.TabIndex = 11
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.ForeColor = System.Drawing.Color.DarkRed
        Me.Label11.Location = New System.Drawing.Point(22, 150)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(27, 13)
        Me.Label11.TabIndex = 26
        Me.Label11.Text = "Pais"
        '
        'txtPais
        '
        Me.txtPais.Location = New System.Drawing.Point(111, 147)
        Me.txtPais.MaxLength = 255
        Me.txtPais.Name = "txtPais"
        Me.txtPais.ReadOnly = True
        Me.txtPais.Size = New System.Drawing.Size(167, 20)
        Me.txtPais.TabIndex = 8
        Me.txtPais.Text = "Mexico"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.ForeColor = System.Drawing.Color.DarkRed
        Me.Label12.Location = New System.Drawing.Point(22, 121)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(65, 13)
        Me.Label12.TabIndex = 28
        Me.Label12.Text = "Delegacion"
        '
        'txtdelegacion
        '
        Me.txtdelegacion.Location = New System.Drawing.Point(111, 121)
        Me.txtdelegacion.MaxLength = 255
        Me.txtdelegacion.Name = "txtdelegacion"
        Me.txtdelegacion.Size = New System.Drawing.Size(353, 20)
        Me.txtdelegacion.TabIndex = 6
        '
        'DTPincio
        '
        Me.DTPincio.Location = New System.Drawing.Point(111, 13)
        Me.DTPincio.Name = "DTPincio"
        Me.DTPincio.Size = New System.Drawing.Size(200, 20)
        Me.DTPincio.TabIndex = 29
        '
        'DtPfin
        '
        Me.DtPfin.Location = New System.Drawing.Point(334, 13)
        Me.DtPfin.Name = "DtPfin"
        Me.DtPfin.Size = New System.Drawing.Size(200, 20)
        Me.DtPfin.TabIndex = 30
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.ForeColor = System.Drawing.Color.DarkRed
        Me.Label13.Location = New System.Drawing.Point(20, 13)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(52, 13)
        Me.Label13.TabIndex = 31
        Me.Label13.Text = "*Periodo"
        '
        'btncargar
        '
        Me.btncargar.Location = New System.Drawing.Point(698, 13)
        Me.btncargar.Name = "btncargar"
        Me.btncargar.Size = New System.Drawing.Size(75, 23)
        Me.btncargar.TabIndex = 33
        Me.btncargar.Text = "Cargar ....."
        Me.btncargar.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.ForeColor = System.Drawing.Color.DarkRed
        Me.Label14.Location = New System.Drawing.Point(22, 199)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(71, 13)
        Me.Label14.TabIndex = 34
        Me.Label14.Text = "Observacion"
        '
        'txtobservaciones
        '
        Me.txtobservaciones.Location = New System.Drawing.Point(111, 199)
        Me.txtobservaciones.MaxLength = 250
        Me.txtobservaciones.Name = "txtobservaciones"
        Me.txtobservaciones.Size = New System.Drawing.Size(732, 20)
        Me.txtobservaciones.TabIndex = 35
        '
        'DtgConceptos
        '
        Me.DtgConceptos.AllowUserToAddRows = False
        Me.DtgConceptos.AllowUserToDeleteRows = False
        Me.DtgConceptos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DtgConceptos.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id, Me.CONCEPTO, Me.CANTIDAD, Me.PRECIOUNI, Me.IMPORTE, Me._IVA, Me.CONCEPTOSAT, Me.UNIDADSAT})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DtgConceptos.DefaultCellStyle = DataGridViewCellStyle3
        Me.DtgConceptos.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.DtgConceptos.Location = New System.Drawing.Point(23, 225)
        Me.DtgConceptos.Name = "DtgConceptos"
        Me.DtgConceptos.ReadOnly = True
        Me.DtgConceptos.Size = New System.Drawing.Size(1056, 186)
        Me.DtgConceptos.TabIndex = 36
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(30, 427)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(51, 13)
        Me.Label15.TabIndex = 37
        Me.Label15.Text = "Subtotal"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(321, 427)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(23, 13)
        Me.Label16.TabIndex = 38
        Me.Label16.Text = "IVA"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(599, 427)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(32, 13)
        Me.Label17.TabIndex = 39
        Me.Label17.Text = "Total"
        '
        'lblsutotal
        '
        Me.lblsutotal.AutoSize = True
        Me.lblsutotal.ForeColor = System.Drawing.Color.Blue
        Me.lblsutotal.Location = New System.Drawing.Point(101, 427)
        Me.lblsutotal.Name = "lblsutotal"
        Me.lblsutotal.Size = New System.Drawing.Size(13, 13)
        Me.lblsutotal.TabIndex = 40
        Me.lblsutotal.Text = "0"
        '
        'lblIva
        '
        Me.lblIva.AutoSize = True
        Me.lblIva.ForeColor = System.Drawing.Color.Blue
        Me.lblIva.Location = New System.Drawing.Point(369, 427)
        Me.lblIva.Name = "lblIva"
        Me.lblIva.Size = New System.Drawing.Size(13, 13)
        Me.lblIva.TabIndex = 41
        Me.lblIva.Text = "0"
        '
        'Lbltotal
        '
        Me.Lbltotal.AutoSize = True
        Me.Lbltotal.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbltotal.ForeColor = System.Drawing.Color.Blue
        Me.Lbltotal.Location = New System.Drawing.Point(769, 427)
        Me.Lbltotal.Name = "Lbltotal"
        Me.Lbltotal.Size = New System.Drawing.Size(22, 25)
        Me.Lbltotal.TabIndex = 42
        Me.Lbltotal.Text = "0"
        '
        'txtcaja
        '
        Me.txtcaja.Location = New System.Drawing.Point(599, 17)
        Me.txtcaja.MaxLength = 40
        Me.txtcaja.Name = "txtcaja"
        Me.txtcaja.Size = New System.Drawing.Size(72, 20)
        Me.txtcaja.TabIndex = 43
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.ForeColor = System.Drawing.Color.DarkRed
        Me.Label18.Location = New System.Drawing.Point(561, 22)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(29, 13)
        Me.Label18.TabIndex = 44
        Me.Label18.Text = "Caja"
        '
        'id
        '
        Me.id.DataPropertyName = "id"
        Me.id.HeaderText = "id"
        Me.id.Name = "id"
        Me.id.ReadOnly = True
        Me.id.Width = 50
        '
        'CONCEPTO
        '
        Me.CONCEPTO.DataPropertyName = "concepto"
        Me.CONCEPTO.HeaderText = "CONCEPTO"
        Me.CONCEPTO.Name = "CONCEPTO"
        Me.CONCEPTO.ReadOnly = True
        Me.CONCEPTO.Width = 250
        '
        'CANTIDAD
        '
        Me.CANTIDAD.DataPropertyName = "CANTIDAD"
        Me.CANTIDAD.HeaderText = "CANTIDAD"
        Me.CANTIDAD.Name = "CANTIDAD"
        Me.CANTIDAD.ReadOnly = True
        '
        'PRECIOUNI
        '
        Me.PRECIOUNI.DataPropertyName = "PRECIOUNI"
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.PRECIOUNI.DefaultCellStyle = DataGridViewCellStyle1
        Me.PRECIOUNI.HeaderText = "PRECIOUNI"
        Me.PRECIOUNI.Name = "PRECIOUNI"
        Me.PRECIOUNI.ReadOnly = True
        '
        'IMPORTE
        '
        Me.IMPORTE.DataPropertyName = "IMPORTE"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.IMPORTE.DefaultCellStyle = DataGridViewCellStyle2
        Me.IMPORTE.HeaderText = "IMPORTE"
        Me.IMPORTE.Name = "IMPORTE"
        Me.IMPORTE.ReadOnly = True
        '
        '_IVA
        '
        Me._IVA.DataPropertyName = "IVA"
        Me._IVA.HeaderText = "IVA"
        Me._IVA.Name = "_IVA"
        Me._IVA.ReadOnly = True
        '
        'CONCEPTOSAT
        '
        Me.CONCEPTOSAT.DataPropertyName = "CONCEPTOSAT"
        Me.CONCEPTOSAT.HeaderText = "CONCEPTOSAT"
        Me.CONCEPTOSAT.Name = "CONCEPTOSAT"
        Me.CONCEPTOSAT.ReadOnly = True
        '
        'UNIDADSAT
        '
        Me.UNIDADSAT.DataPropertyName = "UNIDADSAT"
        Me.UNIDADSAT.HeaderText = "UNIDADSAT"
        Me.UNIDADSAT.Name = "UNIDADSAT"
        Me.UNIDADSAT.ReadOnly = True
        '
        'Frmvalidafacturapublico
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1097, 575)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.txtcaja)
        Me.Controls.Add(Me.Lbltotal)
        Me.Controls.Add(Me.lblIva)
        Me.Controls.Add(Me.lblsutotal)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.DtgConceptos)
        Me.Controls.Add(Me.txtobservaciones)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.btncargar)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.DtPfin)
        Me.Controls.Add(Me.DTPincio)
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
        Me.Name = "Frmvalidafacturapublico"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Validando Datos Factura publico en  General"
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.BTNGUARDAR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btncancelar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnaceptar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DtgConceptos, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents DTPincio As System.Windows.Forms.DateTimePicker
    Friend WithEvents DtPfin As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents btncargar As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtobservaciones As System.Windows.Forms.TextBox
    Friend WithEvents DtgConceptos As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents lblsutotal As System.Windows.Forms.Label
    Friend WithEvents lblIva As System.Windows.Forms.Label
    Friend WithEvents Lbltotal As System.Windows.Forms.Label
    Friend WithEvents txtcaja As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents id As DataGridViewTextBoxColumn
    Friend WithEvents CONCEPTO As DataGridViewTextBoxColumn
    Friend WithEvents CANTIDAD As DataGridViewTextBoxColumn
    Friend WithEvents PRECIOUNI As DataGridViewTextBoxColumn
    Friend WithEvents IMPORTE As DataGridViewTextBoxColumn
    Friend WithEvents _IVA As DataGridViewTextBoxColumn
    Friend WithEvents CONCEPTOSAT As DataGridViewTextBoxColumn
    Friend WithEvents UNIDADSAT As DataGridViewTextBoxColumn
End Class


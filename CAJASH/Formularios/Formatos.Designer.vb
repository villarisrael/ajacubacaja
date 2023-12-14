<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Formatos
    Inherits DevComponents.DotNetBar.OfficeForm

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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DGFormato = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.IdDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TipoDataGridViewTextBoxColumn = New DevComponents.DotNetBar.Controls.DataGridViewTextBoxDropDownColumn()
        Me.ConceptoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FILADataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.COLUMNADataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LetraDataGridViewTextBoxColumn = New DevComponents.DotNetBar.Controls.DataGridViewTextBoxDropDownColumn()
        Me.SizeDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AlineacionDataGridViewTextBoxColumn = New DevComponents.DotNetBar.Controls.DataGridViewTextBoxDropDownColumn()
        Me.IDFORMATODataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FormatoBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DatosRecibo = New CAJAS.DatosRecibo()
        Me.FormatoTableAdapter = New CAJAS.DatosReciboTableAdapters.FormatoTableAdapter()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.txtidformato = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.cmbformato = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.btncargar = New DevComponents.DotNetBar.ButtonX()
        Me.panelEx4 = New DevComponents.DotNetBar.PanelEx()
        Me.cmbletras2 = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.CuerpoReciboBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DatosRecibo1 = New CAJAS.DatosRecibo()
        Me.LabelX18 = New DevComponents.DotNetBar.LabelX()
        Me.iiprecio = New DevComponents.Editors.IntegerInput()
        Me.LabelX17 = New DevComponents.DotNetBar.LabelX()
        Me.iicantidad = New DevComponents.Editors.IntegerInput()
        Me.labelX14 = New DevComponents.DotNetBar.LabelX()
        Me.iiavance = New DevComponents.Editors.IntegerInput()
        Me.labelX12 = New DevComponents.DotNetBar.LabelX()
        Me.IIimporte = New DevComponents.Editors.IntegerInput()
        Me.labelX11 = New DevComponents.DotNetBar.LabelX()
        Me.iicolconcep = New DevComponents.Editors.IntegerInput()
        Me.labelX10 = New DevComponents.DotNetBar.LabelX()
        Me.Ditamano = New DevComponents.Editors.DoubleInput()
        Me.labelX9 = New DevComponents.DotNetBar.LabelX()
        Me.labelX8 = New DevComponents.DotNetBar.LabelX()
        Me.IIlininicial = New DevComponents.Editors.IntegerInput()
        Me.labelX7 = New DevComponents.DotNetBar.LabelX()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.btnaceptar = New Telerik.WinControls.UI.RadButton()
        Me.btnsalir = New Telerik.WinControls.UI.RadButton()
        Me.cmbtipo = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.ComboItem1 = New DevComponents.Editors.ComboItem()
        Me.ComboItem2 = New DevComponents.Editors.ComboItem()
        Me.ComboItem3 = New DevComponents.Editors.ComboItem()
        Me.ComboItem4 = New DevComponents.Editors.ComboItem()
        Me.ComboItem5 = New DevComponents.Editors.ComboItem()
        Me.ComboItem6 = New DevComponents.Editors.ComboItem()
        Me.ComboItem7 = New DevComponents.Editors.ComboItem()
        Me.ComboItem17 = New DevComponents.Editors.ComboItem()
        Me.ComboItem18 = New DevComponents.Editors.ComboItem()
        Me.GroupPanel1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.btnagregar = New DevComponents.DotNetBar.ButtonX()
        Me.cmbalineacion = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.ComboItem15 = New DevComponents.Editors.ComboItem()
        Me.ComboItem16 = New DevComponents.Editors.ComboItem()
        Me.LabelX16 = New DevComponents.DotNetBar.LabelX()
        Me.iitamano = New DevComponents.Editors.IntegerInput()
        Me.LabelX15 = New DevComponents.DotNetBar.LabelX()
        Me.cmbletras = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.LabelX13 = New DevComponents.DotNetBar.LabelX()
        Me.iicolumna = New DevComponents.Editors.IntegerInput()
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX()
        Me.iifila = New DevComponents.Editors.IntegerInput()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.txt = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.rbtexto = New System.Windows.Forms.RadioButton()
        Me.rbpago = New System.Windows.Forms.RadioButton()
        Me.cmbbase = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.ComboItem10 = New DevComponents.Editors.ComboItem()
        Me.ComboItem11 = New DevComponents.Editors.ComboItem()
        Me.ComboItem12 = New DevComponents.Editors.ComboItem()
        Me.ComboItem13 = New DevComponents.Editors.ComboItem()
        Me.ComboItem14 = New DevComponents.Editors.ComboItem()
        Me.ComboItem8 = New DevComponents.Editors.ComboItem()
        Me.ComboItem9 = New DevComponents.Editors.ComboItem()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.CuerpoReciboTableAdapter = New CAJAS.DatosReciboTableAdapters.CuerpoReciboTableAdapter()
        Me.ComboItem19 = New DevComponents.Editors.ComboItem()
        CType(Me.DGFormato, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FormatoBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DatosRecibo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelEx4.SuspendLayout()
        CType(Me.CuerpoReciboBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DatosRecibo1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.iiprecio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.iicantidad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.iiavance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IIimporte, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.iicolconcep, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Ditamano, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IIlininicial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.btnaceptar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsalir, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupPanel1.SuspendLayout()
        CType(Me.iitamano, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.iicolumna, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.iifila, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGFormato
        '
        Me.DGFormato.AllowUserToAddRows = False
        Me.DGFormato.AutoGenerateColumns = False
        Me.DGFormato.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGFormato.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IdDataGridViewTextBoxColumn, Me.TipoDataGridViewTextBoxColumn, Me.ConceptoDataGridViewTextBoxColumn, Me.FILADataGridViewTextBoxColumn, Me.COLUMNADataGridViewTextBoxColumn, Me.LetraDataGridViewTextBoxColumn, Me.SizeDataGridViewTextBoxColumn, Me.AlineacionDataGridViewTextBoxColumn, Me.IDFORMATODataGridViewTextBoxColumn})
        Me.DGFormato.DataSource = Me.FormatoBindingSource
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGFormato.DefaultCellStyle = DataGridViewCellStyle3
        Me.DGFormato.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.DGFormato.Location = New System.Drawing.Point(10, 170)
        Me.DGFormato.Name = "DGFormato"
        Me.DGFormato.Size = New System.Drawing.Size(655, 376)
        Me.DGFormato.TabIndex = 5
        '
        'IdDataGridViewTextBoxColumn
        '
        Me.IdDataGridViewTextBoxColumn.DataPropertyName = "id"
        Me.IdDataGridViewTextBoxColumn.HeaderText = "id"
        Me.IdDataGridViewTextBoxColumn.Name = "IdDataGridViewTextBoxColumn"
        Me.IdDataGridViewTextBoxColumn.Visible = False
        Me.IdDataGridViewTextBoxColumn.Width = 30
        '
        'TipoDataGridViewTextBoxColumn
        '
        Me.TipoDataGridViewTextBoxColumn.AutoCompleteCustomSource.AddRange(New String() {"ConvertirLetras", "CampoDia", "CampoMes", "CampoAno", "CampoMoneda", "CampoTexto", "Texto"})
        Me.TipoDataGridViewTextBoxColumn.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.TipoDataGridViewTextBoxColumn.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.TipoDataGridViewTextBoxColumn.BackColor = System.Drawing.SystemColors.Window
        '
        '
        '
        Me.TipoDataGridViewTextBoxColumn.BackgroundStyle.Class = "DataGridViewIpAddressBorder"
        Me.TipoDataGridViewTextBoxColumn.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.TipoDataGridViewTextBoxColumn.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.TipoDataGridViewTextBoxColumn.DataPropertyName = "Tipo"
        Me.TipoDataGridViewTextBoxColumn.ForeColor = System.Drawing.SystemColors.ControlText
        Me.TipoDataGridViewTextBoxColumn.HeaderText = "Tipo"
        Me.TipoDataGridViewTextBoxColumn.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TipoDataGridViewTextBoxColumn.Name = "TipoDataGridViewTextBoxColumn"
        Me.TipoDataGridViewTextBoxColumn.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.TipoDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TipoDataGridViewTextBoxColumn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TipoDataGridViewTextBoxColumn.Text = ""
        '
        'ConceptoDataGridViewTextBoxColumn
        '
        Me.ConceptoDataGridViewTextBoxColumn.DataPropertyName = "Concepto"
        Me.ConceptoDataGridViewTextBoxColumn.HeaderText = "Concepto"
        Me.ConceptoDataGridViewTextBoxColumn.Name = "ConceptoDataGridViewTextBoxColumn"
        '
        'FILADataGridViewTextBoxColumn
        '
        Me.FILADataGridViewTextBoxColumn.DataPropertyName = "FILA"
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.Format = "N0"
        DataGridViewCellStyle1.NullValue = "0"
        Me.FILADataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle1
        Me.FILADataGridViewTextBoxColumn.HeaderText = "FILA"
        Me.FILADataGridViewTextBoxColumn.Name = "FILADataGridViewTextBoxColumn"
        Me.FILADataGridViewTextBoxColumn.Width = 60
        '
        'COLUMNADataGridViewTextBoxColumn
        '
        Me.COLUMNADataGridViewTextBoxColumn.DataPropertyName = "COLUMNA"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N0"
        DataGridViewCellStyle2.NullValue = "0"
        Me.COLUMNADataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle2
        Me.COLUMNADataGridViewTextBoxColumn.HeaderText = "COLUMNA"
        Me.COLUMNADataGridViewTextBoxColumn.Name = "COLUMNADataGridViewTextBoxColumn"
        Me.COLUMNADataGridViewTextBoxColumn.Width = 60
        '
        'LetraDataGridViewTextBoxColumn
        '
        Me.LetraDataGridViewTextBoxColumn.AutoCompleteCustomSource.AddRange(New String() {"Arial", "Times New Roman", "SansSerif", "Tahoma"})
        Me.LetraDataGridViewTextBoxColumn.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.LetraDataGridViewTextBoxColumn.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.LetraDataGridViewTextBoxColumn.BackColor = System.Drawing.SystemColors.Window
        '
        '
        '
        Me.LetraDataGridViewTextBoxColumn.BackgroundStyle.Class = "DataGridViewIpAddressBorder"
        Me.LetraDataGridViewTextBoxColumn.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LetraDataGridViewTextBoxColumn.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.LetraDataGridViewTextBoxColumn.DataPropertyName = "Letra"
        Me.LetraDataGridViewTextBoxColumn.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LetraDataGridViewTextBoxColumn.HeaderText = "Letra"
        Me.LetraDataGridViewTextBoxColumn.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LetraDataGridViewTextBoxColumn.Name = "LetraDataGridViewTextBoxColumn"
        Me.LetraDataGridViewTextBoxColumn.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.LetraDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.LetraDataGridViewTextBoxColumn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LetraDataGridViewTextBoxColumn.Text = ""
        '
        'SizeDataGridViewTextBoxColumn
        '
        Me.SizeDataGridViewTextBoxColumn.DataPropertyName = "Size"
        Me.SizeDataGridViewTextBoxColumn.HeaderText = "Size"
        Me.SizeDataGridViewTextBoxColumn.Name = "SizeDataGridViewTextBoxColumn"
        Me.SizeDataGridViewTextBoxColumn.Width = 60
        '
        'AlineacionDataGridViewTextBoxColumn
        '
        Me.AlineacionDataGridViewTextBoxColumn.AutoCompleteCustomSource.AddRange(New String() {"Izquierda", "Derecha"})
        Me.AlineacionDataGridViewTextBoxColumn.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.AlineacionDataGridViewTextBoxColumn.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.AlineacionDataGridViewTextBoxColumn.BackColor = System.Drawing.SystemColors.Window
        '
        '
        '
        Me.AlineacionDataGridViewTextBoxColumn.BackgroundStyle.Class = "DataGridViewIpAddressBorder"
        Me.AlineacionDataGridViewTextBoxColumn.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.AlineacionDataGridViewTextBoxColumn.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.AlineacionDataGridViewTextBoxColumn.DataPropertyName = "Alineacion"
        Me.AlineacionDataGridViewTextBoxColumn.ForeColor = System.Drawing.SystemColors.ControlText
        Me.AlineacionDataGridViewTextBoxColumn.HeaderText = "Alineacion"
        Me.AlineacionDataGridViewTextBoxColumn.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.AlineacionDataGridViewTextBoxColumn.Name = "AlineacionDataGridViewTextBoxColumn"
        Me.AlineacionDataGridViewTextBoxColumn.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.AlineacionDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.AlineacionDataGridViewTextBoxColumn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.AlineacionDataGridViewTextBoxColumn.Text = ""
        '
        'IDFORMATODataGridViewTextBoxColumn
        '
        Me.IDFORMATODataGridViewTextBoxColumn.DataPropertyName = "IDFORMATO"
        Me.IDFORMATODataGridViewTextBoxColumn.HeaderText = "IDFORMATO"
        Me.IDFORMATODataGridViewTextBoxColumn.Name = "IDFORMATODataGridViewTextBoxColumn"
        '
        'FormatoBindingSource
        '
        Me.FormatoBindingSource.DataMember = "Formato"
        Me.FormatoBindingSource.DataSource = Me.DatosRecibo
        '
        'DatosRecibo
        '
        Me.DatosRecibo.DataSetName = "DatosRecibo"
        Me.DatosRecibo.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'FormatoTableAdapter
        '
        Me.FormatoTableAdapter.ClearBeforeFill = True
        '
        'LabelX1
        '
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Location = New System.Drawing.Point(13, 42)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(155, 23)
        Me.LabelX1.TabIndex = 1
        Me.LabelX1.Text = "Encabezado y pie de recibo"
        '
        'LabelX2
        '
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.ForeColor = System.Drawing.Color.Maroon
        Me.LabelX2.Location = New System.Drawing.Point(13, 13)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(313, 23)
        Me.LabelX2.TabIndex = 2
        Me.LabelX2.Text = "Escribe el nombre del formato que deseas crear o editar"
        '
        'txtidformato
        '
        Me.txtidformato.BackColor = System.Drawing.Color.Yellow
        '
        '
        '
        Me.txtidformato.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtidformato.Location = New System.Drawing.Point(298, 15)
        Me.txtidformato.MaxLength = 4
        Me.txtidformato.Name = "txtidformato"
        Me.txtidformato.PreventEnterBeep = True
        Me.txtidformato.Size = New System.Drawing.Size(76, 20)
        Me.txtidformato.TabIndex = 0
        '
        'LabelX3
        '
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Location = New System.Drawing.Point(506, 12)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.Size = New System.Drawing.Size(216, 23)
        Me.LabelX3.TabIndex = 2
        Me.LabelX3.Text = "puedes seleccionarlo de estas opciones"
        '
        'cmbformato
        '
        Me.cmbformato.DisplayMember = "Text"
        Me.cmbformato.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbformato.FormattingEnabled = True
        Me.cmbformato.ItemHeight = 14
        Me.cmbformato.Location = New System.Drawing.Point(729, 16)
        Me.cmbformato.Name = "cmbformato"
        Me.cmbformato.Size = New System.Drawing.Size(161, 20)
        Me.cmbformato.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.cmbformato.TabIndex = 3
        '
        'btncargar
        '
        Me.btncargar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btncargar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btncargar.Location = New System.Drawing.Point(392, 16)
        Me.btncargar.Name = "btncargar"
        Me.btncargar.Size = New System.Drawing.Size(62, 23)
        Me.btncargar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btncargar.TabIndex = 1
        Me.btncargar.Text = "Cargar"
        '
        'panelEx4
        '
        Me.panelEx4.CanvasColor = System.Drawing.SystemColors.Control
        Me.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.panelEx4.Controls.Add(Me.cmbletras2)
        Me.panelEx4.Controls.Add(Me.LabelX18)
        Me.panelEx4.Controls.Add(Me.iiprecio)
        Me.panelEx4.Controls.Add(Me.LabelX17)
        Me.panelEx4.Controls.Add(Me.iicantidad)
        Me.panelEx4.Controls.Add(Me.labelX14)
        Me.panelEx4.Controls.Add(Me.iiavance)
        Me.panelEx4.Controls.Add(Me.labelX12)
        Me.panelEx4.Controls.Add(Me.IIimporte)
        Me.panelEx4.Controls.Add(Me.labelX11)
        Me.panelEx4.Controls.Add(Me.iicolconcep)
        Me.panelEx4.Controls.Add(Me.labelX10)
        Me.panelEx4.Controls.Add(Me.Ditamano)
        Me.panelEx4.Controls.Add(Me.labelX9)
        Me.panelEx4.Controls.Add(Me.labelX8)
        Me.panelEx4.Controls.Add(Me.IIlininicial)
        Me.panelEx4.Controls.Add(Me.labelX7)
        Me.panelEx4.DisabledBackColor = System.Drawing.Color.Empty
        Me.panelEx4.Location = New System.Drawing.Point(679, 175)
        Me.panelEx4.Name = "panelEx4"
        Me.panelEx4.Size = New System.Drawing.Size(286, 296)
        Me.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.panelEx4.Style.GradientAngle = 90
        Me.panelEx4.TabIndex = 6
        '
        'cmbletras2
        '
        Me.cmbletras2.DataBindings.Add(New System.Windows.Forms.Binding("Tag", Me.CuerpoReciboBindingSource, "LETRA", True))
        Me.cmbletras2.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.CuerpoReciboBindingSource, "LETRA", True))
        Me.cmbletras2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbletras2.FormattingEnabled = True
        Me.cmbletras2.ItemHeight = 14
        Me.cmbletras2.Location = New System.Drawing.Point(27, 95)
        Me.cmbletras2.Name = "cmbletras2"
        Me.cmbletras2.Size = New System.Drawing.Size(221, 20)
        Me.cmbletras2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.cmbletras2.TabIndex = 3
        '
        'CuerpoReciboBindingSource
        '
        Me.CuerpoReciboBindingSource.DataMember = "CuerpoRecibo"
        Me.CuerpoReciboBindingSource.DataSource = Me.DatosRecibo1
        '
        'DatosRecibo1
        '
        Me.DatosRecibo1.DataSetName = "DatosRecibo"
        Me.DatosRecibo1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'LabelX18
        '
        Me.LabelX18.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX18.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX18.Location = New System.Drawing.Point(29, 207)
        Me.LabelX18.Name = "LabelX18"
        Me.LabelX18.Size = New System.Drawing.Size(116, 19)
        Me.LabelX18.TabIndex = 10
        Me.LabelX18.Text = "Columna Precio"
        '
        'iiprecio
        '
        '
        '
        '
        Me.iiprecio.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.iiprecio.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.iiprecio.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.iiprecio.Location = New System.Drawing.Point(170, 207)
        Me.iiprecio.MaxValue = 1200
        Me.iiprecio.MinValue = 1
        Me.iiprecio.Name = "iiprecio"
        Me.iiprecio.ShowUpDown = True
        Me.iiprecio.Size = New System.Drawing.Size(59, 20)
        Me.iiprecio.TabIndex = 11
        Me.iiprecio.Value = 1
        '
        'LabelX17
        '
        Me.LabelX17.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX17.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX17.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX17.Location = New System.Drawing.Point(28, 156)
        Me.LabelX17.Name = "LabelX17"
        Me.LabelX17.Size = New System.Drawing.Size(116, 19)
        Me.LabelX17.TabIndex = 6
        Me.LabelX17.Text = "Columna Cantidad"
        '
        'iicantidad
        '
        '
        '
        '
        Me.iicantidad.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.iicantidad.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.iicantidad.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.iicantidad.Location = New System.Drawing.Point(170, 156)
        Me.iicantidad.MaxValue = 1000
        Me.iicantidad.MinValue = 1
        Me.iicantidad.Name = "iicantidad"
        Me.iicantidad.ShowUpDown = True
        Me.iicantidad.Size = New System.Drawing.Size(59, 20)
        Me.iicantidad.TabIndex = 7
        Me.iicantidad.Value = 1
        '
        'labelX14
        '
        Me.labelX14.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.labelX14.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.labelX14.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelX14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.labelX14.Location = New System.Drawing.Point(9, 3)
        Me.labelX14.Name = "labelX14"
        Me.labelX14.Size = New System.Drawing.Size(229, 23)
        Me.labelX14.TabIndex = 34
        Me.labelX14.Text = "Detalle de recibo"
        '
        'iiavance
        '
        '
        '
        '
        Me.iiavance.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.iiavance.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.iiavance.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.iiavance.Location = New System.Drawing.Point(170, 256)
        Me.iiavance.MaxValue = 100
        Me.iiavance.MinValue = 3
        Me.iiavance.Name = "iiavance"
        Me.iiavance.ShowUpDown = True
        Me.iiavance.Size = New System.Drawing.Size(59, 20)
        Me.iiavance.TabIndex = 15
        Me.iiavance.Value = 3
        '
        'labelX12
        '
        Me.labelX12.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.labelX12.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.labelX12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelX12.Location = New System.Drawing.Point(28, 256)
        Me.labelX12.Name = "labelX12"
        Me.labelX12.Size = New System.Drawing.Size(116, 19)
        Me.labelX12.TabIndex = 14
        Me.labelX12.Text = "Avance de linea"
        '
        'IIimporte
        '
        '
        '
        '
        Me.IIimporte.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.IIimporte.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.IIimporte.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.IIimporte.Location = New System.Drawing.Point(170, 232)
        Me.IIimporte.MaxValue = 1200
        Me.IIimporte.MinValue = 1
        Me.IIimporte.Name = "IIimporte"
        Me.IIimporte.ShowUpDown = True
        Me.IIimporte.Size = New System.Drawing.Size(59, 20)
        Me.IIimporte.TabIndex = 13
        Me.IIimporte.Value = 1
        '
        'labelX11
        '
        Me.labelX11.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.labelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.labelX11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelX11.Location = New System.Drawing.Point(29, 182)
        Me.labelX11.Name = "labelX11"
        Me.labelX11.Size = New System.Drawing.Size(116, 19)
        Me.labelX11.TabIndex = 8
        Me.labelX11.Text = "Columna Concepto"
        '
        'iicolconcep
        '
        '
        '
        '
        Me.iicolconcep.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.iicolconcep.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.iicolconcep.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.iicolconcep.Location = New System.Drawing.Point(170, 182)
        Me.iicolconcep.MaxValue = 1200
        Me.iicolconcep.MinValue = 1
        Me.iicolconcep.Name = "iicolconcep"
        Me.iicolconcep.ShowUpDown = True
        Me.iicolconcep.Size = New System.Drawing.Size(59, 20)
        Me.iicolconcep.TabIndex = 9
        Me.iicolconcep.Value = 1
        '
        'labelX10
        '
        Me.labelX10.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.labelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.labelX10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelX10.Location = New System.Drawing.Point(28, 232)
        Me.labelX10.Name = "labelX10"
        Me.labelX10.Size = New System.Drawing.Size(116, 19)
        Me.labelX10.TabIndex = 12
        Me.labelX10.Text = "Columna Importe"
        '
        'Ditamano
        '
        '
        '
        '
        Me.Ditamano.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.Ditamano.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.Ditamano.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.Ditamano.Increment = 1.0R
        Me.Ditamano.Location = New System.Drawing.Point(99, 121)
        Me.Ditamano.Name = "Ditamano"
        Me.Ditamano.ShowUpDown = True
        Me.Ditamano.Size = New System.Drawing.Size(80, 20)
        Me.Ditamano.TabIndex = 5
        '
        'labelX9
        '
        Me.labelX9.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.labelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.labelX9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelX9.Location = New System.Drawing.Point(28, 121)
        Me.labelX9.Name = "labelX9"
        Me.labelX9.Size = New System.Drawing.Size(116, 19)
        Me.labelX9.TabIndex = 4
        Me.labelX9.Text = "Tamaño"
        '
        'labelX8
        '
        Me.labelX8.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.labelX8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelX8.Location = New System.Drawing.Point(28, 70)
        Me.labelX8.Name = "labelX8"
        Me.labelX8.Size = New System.Drawing.Size(202, 19)
        Me.labelX8.TabIndex = 2
        Me.labelX8.Text = "Letra de detalle"
        '
        'IIlininicial
        '
        '
        '
        '
        Me.IIlininicial.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.IIlininicial.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.IIlininicial.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.IIlininicial.Location = New System.Drawing.Point(170, 45)
        Me.IIlininicial.MinValue = 1
        Me.IIlininicial.Name = "IIlininicial"
        Me.IIlininicial.ShowUpDown = True
        Me.IIlininicial.Size = New System.Drawing.Size(59, 20)
        Me.IIlininicial.TabIndex = 1
        Me.IIlininicial.Value = 1
        '
        'labelX7
        '
        Me.labelX7.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.labelX7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelX7.Location = New System.Drawing.Point(27, 45)
        Me.labelX7.Name = "labelX7"
        Me.labelX7.Size = New System.Drawing.Size(202, 19)
        Me.labelX7.TabIndex = 0
        Me.labelX7.Text = "Línea inicial del detalle "
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
        Me.RadGroupBox3.Location = New System.Drawing.Point(671, 477)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        '
        '
        '
        Me.RadGroupBox3.RootElement.Padding = New System.Windows.Forms.Padding(2, 18, 2, 2)
        Me.RadGroupBox3.Size = New System.Drawing.Size(295, 74)
        Me.RadGroupBox3.TabIndex = 7
        Me.RadGroupBox3.Text = "ACCIONES"
        '
        'btnaceptar
        '
        Me.btnaceptar.Image = Global.CAJAS.My.Resources.Resources.IcoAgregar
        Me.btnaceptar.ImageAlignment = System.Drawing.ContentAlignment.TopCenter
        Me.btnaceptar.Location = New System.Drawing.Point(173, 21)
        Me.btnaceptar.Name = "btnaceptar"
        Me.btnaceptar.Size = New System.Drawing.Size(89, 48)
        Me.btnaceptar.TabIndex = 1
        Me.btnaceptar.Text = "ACEPTAR"
        Me.btnaceptar.TextAlignment = System.Drawing.ContentAlignment.BottomCenter
        '
        'btnsalir
        '
        Me.btnsalir.Image = Global.CAJAS.My.Resources.Resources.IcoSalir
        Me.btnsalir.ImageAlignment = System.Drawing.ContentAlignment.TopCenter
        Me.btnsalir.Location = New System.Drawing.Point(9, 21)
        Me.btnsalir.Name = "btnsalir"
        Me.btnsalir.Size = New System.Drawing.Size(89, 48)
        Me.btnsalir.TabIndex = 0
        Me.btnsalir.Text = "Salir"
        Me.btnsalir.TextAlignment = System.Drawing.ContentAlignment.BottomCenter
        '
        'cmbtipo
        '
        Me.cmbtipo.DisplayMember = "Text"
        Me.cmbtipo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbtipo.FormattingEnabled = True
        Me.cmbtipo.ItemHeight = 14
        Me.cmbtipo.Items.AddRange(New Object() {Me.ComboItem1, Me.ComboItem2, Me.ComboItem3, Me.ComboItem4, Me.ComboItem5, Me.ComboItem6, Me.ComboItem7, Me.ComboItem17, Me.ComboItem18, Me.ComboItem19})
        Me.cmbtipo.Location = New System.Drawing.Point(106, 3)
        Me.cmbtipo.Name = "cmbtipo"
        Me.cmbtipo.Size = New System.Drawing.Size(121, 20)
        Me.cmbtipo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.cmbtipo.TabIndex = 0
        '
        'ComboItem1
        '
        Me.ComboItem1.Text = "CampoDia"
        Me.ComboItem1.Value = "CampoDia"
        '
        'ComboItem2
        '
        Me.ComboItem2.Text = "CampoMes"
        Me.ComboItem2.Value = ""
        '
        'ComboItem3
        '
        Me.ComboItem3.Text = "CampoAno"
        '
        'ComboItem4
        '
        Me.ComboItem4.Text = "ConvertirLetras"
        '
        'ComboItem5
        '
        Me.ComboItem5.Text = "CampoMoneda"
        '
        'ComboItem6
        '
        Me.ComboItem6.Text = "CampoTexto"
        '
        'ComboItem7
        '
        Me.ComboItem7.Text = "Texto"
        '
        'ComboItem17
        '
        Me.ComboItem17.Text = "Fecha"
        '
        'ComboItem18
        '
        Me.ComboItem18.Text = "Hora"
        '
        'GroupPanel1
        '
        Me.GroupPanel1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel1.Controls.Add(Me.btnagregar)
        Me.GroupPanel1.Controls.Add(Me.cmbalineacion)
        Me.GroupPanel1.Controls.Add(Me.LabelX16)
        Me.GroupPanel1.Controls.Add(Me.iitamano)
        Me.GroupPanel1.Controls.Add(Me.LabelX15)
        Me.GroupPanel1.Controls.Add(Me.cmbletras)
        Me.GroupPanel1.Controls.Add(Me.LabelX13)
        Me.GroupPanel1.Controls.Add(Me.iicolumna)
        Me.GroupPanel1.Controls.Add(Me.LabelX6)
        Me.GroupPanel1.Controls.Add(Me.iifila)
        Me.GroupPanel1.Controls.Add(Me.LabelX5)
        Me.GroupPanel1.Controls.Add(Me.txt)
        Me.GroupPanel1.Controls.Add(Me.rbtexto)
        Me.GroupPanel1.Controls.Add(Me.rbpago)
        Me.GroupPanel1.Controls.Add(Me.cmbbase)
        Me.GroupPanel1.Controls.Add(Me.LabelX4)
        Me.GroupPanel1.Controls.Add(Me.cmbtipo)
        Me.GroupPanel1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel1.Location = New System.Drawing.Point(10, 72)
        Me.GroupPanel1.Name = "GroupPanel1"
        Me.GroupPanel1.Size = New System.Drawing.Size(940, 97)
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
        Me.GroupPanel1.TabIndex = 4
        Me.GroupPanel1.Text = "Añadir un renglon"
        '
        'btnagregar
        '
        Me.btnagregar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnagregar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnagregar.Location = New System.Drawing.Point(765, 48)
        Me.btnagregar.Name = "btnagregar"
        Me.btnagregar.Size = New System.Drawing.Size(112, 23)
        Me.btnagregar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnagregar.TabIndex = 8
        Me.btnagregar.Text = "Agregar"
        '
        'cmbalineacion
        '
        Me.cmbalineacion.DisplayMember = "Text"
        Me.cmbalineacion.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbalineacion.FormattingEnabled = True
        Me.cmbalineacion.ItemHeight = 14
        Me.cmbalineacion.Items.AddRange(New Object() {Me.ComboItem15, Me.ComboItem16})
        Me.cmbalineacion.Location = New System.Drawing.Point(598, 48)
        Me.cmbalineacion.Name = "cmbalineacion"
        Me.cmbalineacion.Size = New System.Drawing.Size(121, 20)
        Me.cmbalineacion.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.cmbalineacion.TabIndex = 7
        '
        'ComboItem15
        '
        Me.ComboItem15.Text = "Izquierda"
        '
        'ComboItem16
        '
        Me.ComboItem16.Text = "Derecha"
        '
        'LabelX16
        '
        Me.LabelX16.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX16.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX16.Location = New System.Drawing.Point(502, 49)
        Me.LabelX16.Name = "LabelX16"
        Me.LabelX16.Size = New System.Drawing.Size(75, 23)
        Me.LabelX16.TabIndex = 6
        Me.LabelX16.Text = "7 . Alineación"
        '
        'iitamano
        '
        '
        '
        '
        Me.iitamano.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.iitamano.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.iitamano.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.iitamano.Location = New System.Drawing.Point(394, 49)
        Me.iitamano.MaxValue = 48
        Me.iitamano.MinValue = 4
        Me.iitamano.Name = "iitamano"
        Me.iitamano.ShowUpDown = True
        Me.iitamano.Size = New System.Drawing.Size(80, 20)
        Me.iitamano.TabIndex = 5
        Me.iitamano.Value = 12
        '
        'LabelX15
        '
        Me.LabelX15.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX15.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX15.Location = New System.Drawing.Point(312, 49)
        Me.LabelX15.Name = "LabelX15"
        Me.LabelX15.Size = New System.Drawing.Size(75, 23)
        Me.LabelX15.TabIndex = 26
        Me.LabelX15.Text = "6 .  Tamaño"
        '
        'cmbletras
        '
        Me.cmbletras.DisplayMember = "Text"
        Me.cmbletras.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbletras.FormattingEnabled = True
        Me.cmbletras.ItemHeight = 14
        Me.cmbletras.Location = New System.Drawing.Point(85, 50)
        Me.cmbletras.Name = "cmbletras"
        Me.cmbletras.Size = New System.Drawing.Size(221, 20)
        Me.cmbletras.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.cmbletras.TabIndex = 4
        '
        'LabelX13
        '
        Me.LabelX13.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX13.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX13.Location = New System.Drawing.Point(3, 50)
        Me.LabelX13.Name = "LabelX13"
        Me.LabelX13.Size = New System.Drawing.Size(75, 23)
        Me.LabelX13.TabIndex = 24
        Me.LabelX13.Text = "5 . Letra"
        '
        'iicolumna
        '
        '
        '
        '
        Me.iicolumna.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.iicolumna.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.iicolumna.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.iicolumna.Location = New System.Drawing.Point(841, -3)
        Me.iicolumna.MaxValue = 1200
        Me.iicolumna.MinValue = 0
        Me.iicolumna.Name = "iicolumna"
        Me.iicolumna.ShowUpDown = True
        Me.iicolumna.Size = New System.Drawing.Size(80, 20)
        Me.iicolumna.TabIndex = 3
        '
        'LabelX6
        '
        Me.LabelX6.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX6.Location = New System.Drawing.Point(771, -3)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.Size = New System.Drawing.Size(64, 23)
        Me.LabelX6.TabIndex = 22
        Me.LabelX6.Text = "4 . Columna"
        '
        'iifila
        '
        '
        '
        '
        Me.iifila.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.iifila.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.iifila.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.iifila.Location = New System.Drawing.Point(685, -3)
        Me.iifila.MaxValue = 1200
        Me.iifila.MinValue = 0
        Me.iifila.Name = "iifila"
        Me.iifila.ShowUpDown = True
        Me.iifila.Size = New System.Drawing.Size(80, 20)
        Me.iifila.TabIndex = 2
        '
        'LabelX5
        '
        Me.LabelX5.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Location = New System.Drawing.Point(634, -3)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.Size = New System.Drawing.Size(45, 23)
        Me.LabelX5.TabIndex = 20
        Me.LabelX5.Text = "3 .Fila"
        '
        'txt
        '
        '
        '
        '
        Me.txt.Border.Class = "TextBoxBorder"
        Me.txt.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txt.Location = New System.Drawing.Point(376, 23)
        Me.txt.Name = "txt"
        Me.txt.PreventEnterBeep = True
        Me.txt.Size = New System.Drawing.Size(228, 20)
        Me.txt.TabIndex = 19
        '
        'rbtexto
        '
        Me.rbtexto.AutoSize = True
        Me.rbtexto.BackColor = System.Drawing.Color.Transparent
        Me.rbtexto.Location = New System.Drawing.Point(254, 23)
        Me.rbtexto.Name = "rbtexto"
        Me.rbtexto.Size = New System.Drawing.Size(52, 17)
        Me.rbtexto.TabIndex = 18
        Me.rbtexto.Text = "Texto"
        Me.rbtexto.UseVisualStyleBackColor = False
        '
        'rbpago
        '
        Me.rbpago.AutoSize = True
        Me.rbpago.BackColor = System.Drawing.Color.Transparent
        Me.rbpago.Checked = True
        Me.rbpago.Location = New System.Drawing.Point(254, 3)
        Me.rbpago.Name = "rbpago"
        Me.rbpago.Size = New System.Drawing.Size(118, 17)
        Me.rbpago.TabIndex = 17
        Me.rbpago.TabStop = True
        Me.rbpago.Text = "Concepto de recibo"
        Me.rbpago.UseVisualStyleBackColor = False
        '
        'cmbbase
        '
        Me.cmbbase.DisplayMember = "Field"
        Me.cmbbase.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbbase.FormattingEnabled = True
        Me.cmbbase.ItemHeight = 14
        Me.cmbbase.Items.AddRange(New Object() {Me.ComboItem10, Me.ComboItem11, Me.ComboItem12, Me.ComboItem13, Me.ComboItem14, Me.ComboItem8, Me.ComboItem9})
        Me.cmbbase.Location = New System.Drawing.Point(378, 3)
        Me.cmbbase.Name = "cmbbase"
        Me.cmbbase.Size = New System.Drawing.Size(121, 20)
        Me.cmbbase.Sorted = True
        Me.cmbbase.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.cmbbase.TabIndex = 1
        '
        'ComboItem10
        '
        Me.ComboItem10.Text = "CampoAno"
        '
        'ComboItem11
        '
        Me.ComboItem11.Text = "ConvertirLetras"
        '
        'ComboItem12
        '
        Me.ComboItem12.Text = "CampoMoneda"
        '
        'ComboItem13
        '
        Me.ComboItem13.Text = "CampoTexto"
        '
        'ComboItem14
        '
        Me.ComboItem14.Text = "Texto"
        '
        'ComboItem8
        '
        Me.ComboItem8.Text = "CampoDia"
        Me.ComboItem8.Value = "CampoDia"
        '
        'ComboItem9
        '
        Me.ComboItem9.Text = "CampoMes"
        Me.ComboItem9.Value = ""
        '
        'LabelX4
        '
        Me.LabelX4.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Location = New System.Drawing.Point(3, 0)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.Size = New System.Drawing.Size(75, 23)
        Me.LabelX4.TabIndex = 14
        Me.LabelX4.Text = "1 .Tipo"
        '
        'CuerpoReciboTableAdapter
        '
        Me.CuerpoReciboTableAdapter.ClearBeforeFill = True
        '
        'ComboItem19
        '
        Me.ComboItem19.Text = "Auditoria"
        '
        'Formatos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(962, 546)
        Me.Controls.Add(Me.GroupPanel1)
        Me.Controls.Add(Me.RadGroupBox3)
        Me.Controls.Add(Me.panelEx4)
        Me.Controls.Add(Me.btncargar)
        Me.Controls.Add(Me.cmbformato)
        Me.Controls.Add(Me.LabelX3)
        Me.Controls.Add(Me.txtidformato)
        Me.Controls.Add(Me.LabelX2)
        Me.Controls.Add(Me.LabelX1)
        Me.Controls.Add(Me.DGFormato)
        Me.DoubleBuffered = True
        Me.Name = "Formatos"
        Me.Text = "Formatos"
        CType(Me.DGFormato, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FormatoBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DatosRecibo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelEx4.ResumeLayout(False)
        CType(Me.CuerpoReciboBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DatosRecibo1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.iiprecio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.iicantidad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.iiavance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IIimporte, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.iicolconcep, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Ditamano, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IIlininicial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        CType(Me.btnaceptar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsalir, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupPanel1.ResumeLayout(False)
        Me.GroupPanel1.PerformLayout()
        CType(Me.iitamano, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.iicolumna, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.iifila, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DGFormato As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents FormatoBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DatosRecibo As CAJAS.DatosRecibo
    Friend WithEvents FormatoTableAdapter As CAJAS.DatosReciboTableAdapters.FormatoTableAdapter
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtidformato As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents cmbformato As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents btncargar As DevComponents.DotNetBar.ButtonX
    Private WithEvents panelEx4 As DevComponents.DotNetBar.PanelEx
    Private WithEvents labelX14 As DevComponents.DotNetBar.LabelX
    Private WithEvents iiavance As DevComponents.Editors.IntegerInput
    Private WithEvents labelX12 As DevComponents.DotNetBar.LabelX
    Private WithEvents IIimporte As DevComponents.Editors.IntegerInput
    Private WithEvents labelX11 As DevComponents.DotNetBar.LabelX
    Private WithEvents iicolconcep As DevComponents.Editors.IntegerInput
    Private WithEvents labelX10 As DevComponents.DotNetBar.LabelX
    Private WithEvents Ditamano As DevComponents.Editors.DoubleInput
    Private WithEvents labelX9 As DevComponents.DotNetBar.LabelX
    Private WithEvents labelX8 As DevComponents.DotNetBar.LabelX
    Private WithEvents IIlininicial As DevComponents.Editors.IntegerInput
    Private WithEvents labelX7 As DevComponents.DotNetBar.LabelX
    Friend WithEvents IdDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TipoDataGridViewTextBoxColumn As DevComponents.DotNetBar.Controls.DataGridViewTextBoxDropDownColumn
    Friend WithEvents ConceptoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FILADataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents COLUMNADataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LetraDataGridViewTextBoxColumn As DevComponents.DotNetBar.Controls.DataGridViewTextBoxDropDownColumn
    Friend WithEvents SizeDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AlineacionDataGridViewTextBoxColumn As DevComponents.DotNetBar.Controls.DataGridViewTextBoxDropDownColumn
    Friend WithEvents IDFORMATODataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnaceptar As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsalir As Telerik.WinControls.UI.RadButton
    Friend WithEvents cmbtipo As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents txt As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents ComboItem1 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem2 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem3 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem4 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem5 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem6 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem7 As DevComponents.Editors.ComboItem
    Friend WithEvents GroupPanel1 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents iicolumna As DevComponents.Editors.IntegerInput
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents iifila As DevComponents.Editors.IntegerInput
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents rbtexto As System.Windows.Forms.RadioButton
    Friend WithEvents rbpago As System.Windows.Forms.RadioButton
    Friend WithEvents cmbbase As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents ComboItem8 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem9 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem10 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem11 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem12 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem13 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem14 As DevComponents.Editors.ComboItem
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents cmbletras As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents LabelX13 As DevComponents.DotNetBar.LabelX
    Friend WithEvents iitamano As DevComponents.Editors.IntegerInput
    Friend WithEvents LabelX15 As DevComponents.DotNetBar.LabelX
    Friend WithEvents cmbalineacion As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents ComboItem15 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem16 As DevComponents.Editors.ComboItem
    Friend WithEvents LabelX16 As DevComponents.DotNetBar.LabelX
    Friend WithEvents btnagregar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents ComboItem17 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem18 As DevComponents.Editors.ComboItem
    Private WithEvents LabelX17 As DevComponents.DotNetBar.LabelX
    Private WithEvents iicantidad As DevComponents.Editors.IntegerInput
    Private WithEvents LabelX18 As DevComponents.DotNetBar.LabelX
    Private WithEvents iiprecio As DevComponents.Editors.IntegerInput
    Friend WithEvents cmbletras2 As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents CuerpoReciboBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DatosRecibo1 As CAJAS.DatosRecibo
    Friend WithEvents CuerpoReciboTableAdapter As CAJAS.DatosReciboTableAdapters.CuerpoReciboTableAdapter
    Friend WithEvents ComboItem19 As DevComponents.Editors.ComboItem
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmlistadofacturas
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frmlistadofacturas))
        Me.superTabrecibos = New DevComponents.DotNetBar.SuperTabControl()
        Me.superTabControlPanel1 = New DevComponents.DotNetBar.SuperTabControlPanel()
        Me.toolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btncancelarrecibo = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnreimprimir = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.btnmail = New System.Windows.Forms.ToolStripButton()
        Me.ToolCancelaSAT = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.dataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Maestro = New DevComponents.DotNetBar.SuperTabItem()
        Me.superTabControlPanel2 = New DevComponents.DotNetBar.SuperTabControlPanel()
        Me.advDetalles = New DevComponents.AdvTree.AdvTree()
        Me.nodeConnector2 = New DevComponents.AdvTree.NodeConnector()
        Me.elementStyle2 = New DevComponents.DotNetBar.ElementStyle()
        Me.Esclavo = New DevComponents.DotNetBar.SuperTabItem()
        Me.label2 = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.dtfinal = New System.Windows.Forms.DateTimePicker()
        Me.dtinicio = New System.Windows.Forms.DateTimePicker()
        Me.btncerrar = New DevComponents.DotNetBar.ButtonX()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.txtCaja = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.btncargar = New DevComponents.DotNetBar.ButtonX()
        Me.lblencontradas = New DevComponents.DotNetBar.LabelX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.lblsaldo = New DevComponents.DotNetBar.LabelX()
        CType(Me.superTabrecibos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.superTabrecibos.SuspendLayout()
        Me.superTabControlPanel1.SuspendLayout()
        Me.toolStrip1.SuspendLayout()
        CType(Me.dataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.superTabControlPanel2.SuspendLayout()
        CType(Me.advDetalles, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'superTabrecibos
        '
        Me.superTabrecibos.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        '
        '
        '
        '
        '
        '
        Me.superTabrecibos.ControlBox.CloseBox.Name = ""
        '
        '
        '
        Me.superTabrecibos.ControlBox.MenuBox.Name = ""
        Me.superTabrecibos.ControlBox.Name = ""
        Me.superTabrecibos.ControlBox.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.superTabrecibos.ControlBox.MenuBox, Me.superTabrecibos.ControlBox.CloseBox})
        Me.superTabrecibos.Controls.Add(Me.superTabControlPanel1)
        Me.superTabrecibos.Controls.Add(Me.superTabControlPanel2)
        Me.superTabrecibos.Location = New System.Drawing.Point(17, 88)
        Me.superTabrecibos.Name = "superTabrecibos"
        Me.superTabrecibos.ReorderTabsEnabled = True
        Me.superTabrecibos.SelectedTabFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.superTabrecibos.SelectedTabIndex = 0
        Me.superTabrecibos.Size = New System.Drawing.Size(906, 559)
        Me.superTabrecibos.TabFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.superTabrecibos.TabIndex = 34
        Me.superTabrecibos.Tabs.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.Maestro, Me.Esclavo})
        Me.superTabrecibos.Text = "superTabControl1"
        '
        'superTabControlPanel1
        '
        Me.superTabControlPanel1.Controls.Add(Me.toolStrip1)
        Me.superTabControlPanel1.Controls.Add(Me.dataGridView1)
        Me.superTabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.superTabControlPanel1.Location = New System.Drawing.Point(0, 25)
        Me.superTabControlPanel1.Name = "superTabControlPanel1"
        Me.superTabControlPanel1.Size = New System.Drawing.Size(906, 534)
        Me.superTabControlPanel1.TabIndex = 1
        Me.superTabControlPanel1.TabItem = Me.Maestro
        '
        'toolStrip1
        '
        Me.toolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btncancelarrecibo, Me.toolStripSeparator1, Me.btnreimprimir, Me.ToolStripButton2, Me.ToolStripButton1, Me.btnmail, Me.ToolCancelaSAT, Me.ToolStripLabel1})
        Me.toolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.toolStrip1.Name = "toolStrip1"
        Me.toolStrip1.Size = New System.Drawing.Size(906, 25)
        Me.toolStrip1.TabIndex = 1
        Me.toolStrip1.Text = "toolStrip1"
        '
        'btncancelarrecibo
        '
        Me.btncancelarrecibo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btncancelarrecibo.Image = Global.CAJAS.My.Resources.Resources.c1
        Me.btncancelarrecibo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btncancelarrecibo.Name = "btncancelarrecibo"
        Me.btncancelarrecibo.Size = New System.Drawing.Size(23, 22)
        Me.btncancelarrecibo.ToolTipText = "Cancelar Factura ante el SAT"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnreimprimir
        '
        Me.btnreimprimir.Image = CType(resources.GetObject("btnreimprimir.Image"), System.Drawing.Image)
        Me.btnreimprimir.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnreimprimir.Name = "btnreimprimir"
        Me.btnreimprimir.Size = New System.Drawing.Size(85, 22)
        Me.btnreimprimir.Text = "Genera pdf"
        Me.btnreimprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.btnreimprimir.ToolTipText = "Genera el pdf de nuevo"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(100, 22)
        Me.ToolStripButton2.Text = "Generar todas"
        Me.ToolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.ToolStripButton2.ToolTipText = "Genera el pdf de nuevo"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Image = Global.CAJAS.My.Resources.Resources.Abono
        Me.ToolStripButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(134, 22)
        Me.ToolStripButton1.Text = "Complemento Pago"
        Me.ToolStripButton1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        '
        'btnmail
        '
        Me.btnmail.Image = Global.CAJAS.My.Resources.Resources.login16
        Me.btnmail.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnmail.Name = "btnmail"
        Me.btnmail.Size = New System.Drawing.Size(106, 22)
        Me.btnmail.Text = "Enviar por mail"
        '
        'ToolCancelaSAT
        '
        Me.ToolCancelaSAT.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolCancelaSAT.Image = CType(resources.GetObject("ToolCancelaSAT.Image"), System.Drawing.Image)
        Me.ToolCancelaSAT.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolCancelaSAT.Name = "ToolCancelaSAT"
        Me.ToolCancelaSAT.Size = New System.Drawing.Size(23, 22)
        Me.ToolCancelaSAT.Text = "ToolStripButton3"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(75, 22)
        Me.ToolStripLabel1.Text = "Cancelar SAT"
        '
        'dataGridView1
        '
        Me.dataGridView1.AllowUserToAddRows = False
        Me.dataGridView1.AllowUserToDeleteRows = False
        Me.dataGridView1.AllowUserToOrderColumns = True
        Me.dataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dataGridView1.Location = New System.Drawing.Point(6, 36)
        Me.dataGridView1.Name = "dataGridView1"
        Me.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dataGridView1.Size = New System.Drawing.Size(897, 483)
        Me.dataGridView1.TabIndex = 0
        '
        'Maestro
        '
        Me.Maestro.AttachedControl = Me.superTabControlPanel1
        Me.Maestro.GlobalItem = False
        Me.Maestro.Name = "Maestro"
        Me.Maestro.Text = "Facturas"
        '
        'superTabControlPanel2
        '
        Me.superTabControlPanel2.Controls.Add(Me.advDetalles)
        Me.superTabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.superTabControlPanel2.Location = New System.Drawing.Point(0, 0)
        Me.superTabControlPanel2.Name = "superTabControlPanel2"
        Me.superTabControlPanel2.Size = New System.Drawing.Size(903, 389)
        Me.superTabControlPanel2.TabIndex = 0
        Me.superTabControlPanel2.TabItem = Me.Esclavo
        Me.superTabControlPanel2.Visible = False
        '
        'advDetalles
        '
        Me.advDetalles.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline
        Me.advDetalles.BackColor = System.Drawing.SystemColors.Window
        '
        '
        '
        Me.advDetalles.BackgroundStyle.Class = "TreeBorderKey"
        Me.advDetalles.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.advDetalles.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F"
        Me.advDetalles.Location = New System.Drawing.Point(3, 3)
        Me.advDetalles.Name = "advDetalles"
        Me.advDetalles.NodesConnector = Me.nodeConnector2
        Me.advDetalles.NodeStyle = Me.elementStyle2
        Me.advDetalles.PathSeparator = ";"
        Me.advDetalles.Size = New System.Drawing.Size(724, 191)
        Me.advDetalles.Styles.Add(Me.elementStyle2)
        Me.advDetalles.TabIndex = 0
        Me.advDetalles.Text = "advTree1"
        '
        'nodeConnector2
        '
        Me.nodeConnector2.LineColor = System.Drawing.SystemColors.ControlText
        '
        'elementStyle2
        '
        Me.elementStyle2.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.elementStyle2.Name = "elementStyle2"
        Me.elementStyle2.TextColor = System.Drawing.SystemColors.ControlText
        '
        'Esclavo
        '
        Me.Esclavo.AttachedControl = Me.superTabControlPanel2
        Me.Esclavo.GlobalItem = False
        Me.Esclavo.Name = "Esclavo"
        Me.Esclavo.Text = "Detalles"
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(14, 44)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(14, 13)
        Me.label2.TabIndex = 33
        Me.label2.Text = "A"
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(14, 17)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(21, 13)
        Me.label1.TabIndex = 32
        Me.label1.Text = "De"
        '
        'dtfinal
        '
        Me.dtfinal.Location = New System.Drawing.Point(63, 38)
        Me.dtfinal.Name = "dtfinal"
        Me.dtfinal.Size = New System.Drawing.Size(200, 20)
        Me.dtfinal.TabIndex = 29
        '
        'dtinicio
        '
        Me.dtinicio.Location = New System.Drawing.Point(63, 12)
        Me.dtinicio.Name = "dtinicio"
        Me.dtinicio.Size = New System.Drawing.Size(200, 20)
        Me.dtinicio.TabIndex = 28
        '
        'btncerrar
        '
        Me.btncerrar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btncerrar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btncerrar.Image = CType(resources.GetObject("btncerrar.Image"), System.Drawing.Image)
        Me.btncerrar.Location = New System.Drawing.Point(827, 21)
        Me.btncerrar.Name = "btncerrar"
        Me.btncerrar.Size = New System.Drawing.Size(89, 37)
        Me.btncerrar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btncerrar.TabIndex = 31
        Me.btncerrar.Text = "Cerrar"
        '
        'LabelX1
        '
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Location = New System.Drawing.Point(283, 7)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(52, 23)
        Me.LabelX1.TabIndex = 35
        Me.LabelX1.Text = "Caja"
        '
        'txtCaja
        '
        '
        '
        '
        Me.txtCaja.Border.Class = "TextBoxBorder"
        Me.txtCaja.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtCaja.Location = New System.Drawing.Point(326, 12)
        Me.txtCaja.Name = "txtCaja"
        Me.txtCaja.PreventEnterBeep = True
        Me.txtCaja.Size = New System.Drawing.Size(42, 22)
        Me.txtCaja.TabIndex = 36
        '
        'btncargar
        '
        Me.btncargar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btncargar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btncargar.Image = CType(resources.GetObject("btncargar.Image"), System.Drawing.Image)
        Me.btncargar.Location = New System.Drawing.Point(719, 21)
        Me.btncargar.Name = "btncargar"
        Me.btncargar.Size = New System.Drawing.Size(102, 37)
        Me.btncargar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btncargar.TabIndex = 30
        Me.btncargar.Text = "Cargar"
        '
        'lblencontradas
        '
        '
        '
        '
        Me.lblencontradas.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblencontradas.Font = New System.Drawing.Font("Segoe UI", 16.0!)
        Me.lblencontradas.Location = New System.Drawing.Point(11, 32)
        Me.lblencontradas.Name = "lblencontradas"
        Me.lblencontradas.Size = New System.Drawing.Size(104, 23)
        Me.lblencontradas.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010
        Me.lblencontradas.TabIndex = 38
        Me.lblencontradas.Text = "0"
        Me.lblencontradas.TextAlignment = System.Drawing.StringAlignment.Far
        '
        'LabelX2
        '
        Me.LabelX2.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.ForeColor = System.Drawing.Color.Gray
        Me.LabelX2.Location = New System.Drawing.Point(79, 3)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(112, 23)
        Me.LabelX2.TabIndex = 37
        Me.LabelX2.Text = "Encontradas"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.LabelX2)
        Me.Panel1.Controls.Add(Me.lblencontradas)
        Me.Panel1.Location = New System.Drawing.Point(403, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(148, 58)
        Me.Panel1.TabIndex = 41
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.CAJAS.My.Resources.Resources.IcoPatron
        Me.PictureBox1.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(33, 37)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.DodgerBlue
        Me.Panel2.Controls.Add(Me.PictureBox1)
        Me.Panel2.Location = New System.Drawing.Point(414, 7)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(54, 43)
        Me.Panel2.TabIndex = 42
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.DodgerBlue
        Me.Panel4.Controls.Add(Me.PictureBox2)
        Me.Panel4.Location = New System.Drawing.Point(568, 4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(54, 43)
        Me.Panel4.TabIndex = 44
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.CAJAS.My.Resources.Resources.IcoPatron
        Me.PictureBox2.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(33, 37)
        Me.PictureBox2.TabIndex = 0
        Me.PictureBox2.TabStop = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.White
        Me.Panel3.Controls.Add(Me.LabelX3)
        Me.Panel3.Controls.Add(Me.lblsaldo)
        Me.Panel3.Location = New System.Drawing.Point(557, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(143, 58)
        Me.Panel3.TabIndex = 45
        '
        'LabelX3
        '
        Me.LabelX3.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.ForeColor = System.Drawing.Color.Gray
        Me.LabelX3.Location = New System.Drawing.Point(71, 3)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.Size = New System.Drawing.Size(54, 23)
        Me.LabelX3.TabIndex = 37
        Me.LabelX3.Text = "Saldo"
        '
        'lblsaldo
        '
        '
        '
        '
        Me.lblsaldo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblsaldo.Font = New System.Drawing.Font("Segoe UI", 16.0!)
        Me.lblsaldo.Location = New System.Drawing.Point(21, 32)
        Me.lblsaldo.Name = "lblsaldo"
        Me.lblsaldo.Size = New System.Drawing.Size(104, 23)
        Me.lblsaldo.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010
        Me.lblsaldo.TabIndex = 38
        Me.lblsaldo.Text = "0"
        Me.lblsaldo.TextAlignment = System.Drawing.StringAlignment.Far
        '
        'Frmlistadofacturas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(928, 650)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.txtCaja)
        Me.Controls.Add(Me.LabelX1)
        Me.Controls.Add(Me.superTabrecibos)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.dtfinal)
        Me.Controls.Add(Me.dtinicio)
        Me.Controls.Add(Me.btncerrar)
        Me.Controls.Add(Me.btncargar)
        Me.Name = "Frmlistadofacturas"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Listado de Facturas"
        CType(Me.superTabrecibos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.superTabrecibos.ResumeLayout(False)
        Me.superTabControlPanel1.ResumeLayout(False)
        Me.superTabControlPanel1.PerformLayout()
        Me.toolStrip1.ResumeLayout(False)
        Me.toolStrip1.PerformLayout()
        CType(Me.dataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.superTabControlPanel2.ResumeLayout(False)
        CType(Me.advDetalles, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents superTabrecibos As DevComponents.DotNetBar.SuperTabControl
    Private WithEvents superTabControlPanel1 As DevComponents.DotNetBar.SuperTabControlPanel
    Private WithEvents toolStrip1 As System.Windows.Forms.ToolStrip
    Private WithEvents btncancelarrecibo As System.Windows.Forms.ToolStripButton
    Private WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents dataGridView1 As System.Windows.Forms.DataGridView
    Private WithEvents Maestro As DevComponents.DotNetBar.SuperTabItem
    Private WithEvents superTabControlPanel2 As DevComponents.DotNetBar.SuperTabControlPanel
    Private WithEvents advDetalles As DevComponents.AdvTree.AdvTree
    Private WithEvents nodeConnector2 As DevComponents.AdvTree.NodeConnector
    Private WithEvents elementStyle2 As DevComponents.DotNetBar.ElementStyle
    Private WithEvents Esclavo As DevComponents.DotNetBar.SuperTabItem
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents dtfinal As System.Windows.Forms.DateTimePicker
    Private WithEvents dtinicio As System.Windows.Forms.DateTimePicker
    Private WithEvents btncerrar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtCaja As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents btnreimprimir As System.Windows.Forms.ToolStripButton
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents ToolStripButton2 As ToolStripButton
    Private WithEvents btncargar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents lblencontradas As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents Panel1 As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblsaldo As DevComponents.DotNetBar.LabelX
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents btnmail As ToolStripButton
    Friend WithEvents ToolCancelaSAT As ToolStripButton
    Friend WithEvents ToolStripLabel1 As ToolStripLabel
End Class


<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmHistorial
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ReflectionLabel1 = New DevComponents.DotNetBar.Controls.ReflectionLabel()
        Me.DGlecturas = New System.Windows.Forms.DataGridView()
        Me.AdvHistorial = New DevComponents.AdvTree.AdvTree()
        Me.fecha = New DevComponents.AdvTree.ColumnHeader()
        Me.Recibo = New DevComponents.AdvTree.ColumnHeader()
        Me.periodo = New DevComponents.AdvTree.ColumnHeader()
        Me.Subtotal = New DevComponents.AdvTree.ColumnHeader()
        Me.IVA = New DevComponents.AdvTree.ColumnHeader()
        Me.Vale = New DevComponents.AdvTree.ColumnHeader()
        Me.total = New DevComponents.AdvTree.ColumnHeader()
        Me.descuento = New DevComponents.AdvTree.ColumnHeader()
        Me.observacion = New DevComponents.AdvTree.ColumnHeader()
        Me.NodeConnector1 = New DevComponents.AdvTree.NodeConnector()
        Me.ElementStyle1 = New DevComponents.DotNetBar.ElementStyle()
        Me.SuperTabControl1 = New DevComponents.DotNetBar.SuperTabControl()
        Me.SuperTabControlPanel1 = New DevComponents.DotNetBar.SuperTabControlPanel()
        Me.btnimprimir = New System.Windows.Forms.Button()
        Me.SuperTabItem1 = New DevComponents.DotNetBar.SuperTabItem()
        Me.SuperTabControlPanel2 = New DevComponents.DotNetBar.SuperTabControlPanel()
        Me.Btnhabilitar = New DevComponents.DotNetBar.ButtonX()
        Me.btnpagado = New DevComponents.DotNetBar.ButtonX()
        Me.btnimprimirlecturas = New System.Windows.Forms.Button()
        Me.ReflectionLabel2 = New DevComponents.DotNetBar.Controls.ReflectionLabel()
        Me.SuperTabItem2 = New DevComponents.DotNetBar.SuperTabItem()
        Me.SuperTabControlPanel3 = New DevComponents.DotNetBar.SuperTabControlPanel()
        Me.ReflectionLabel3 = New DevComponents.DotNetBar.Controls.ReflectionLabel()
        Me.DGHistorialAnticipos = New System.Windows.Forms.DataGridView()
        Me.SuperTabItem3 = New DevComponents.DotNetBar.SuperTabItem()
        CType(Me.DGlecturas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AdvHistorial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SuperTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuperTabControl1.SuspendLayout()
        Me.SuperTabControlPanel1.SuspendLayout()
        Me.SuperTabControlPanel2.SuspendLayout()
        Me.SuperTabControlPanel3.SuspendLayout()
        CType(Me.DGHistorialAnticipos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ReflectionLabel1
        '
        Me.ReflectionLabel1.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.ReflectionLabel1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.ReflectionLabel1.Location = New System.Drawing.Point(28, 16)
        Me.ReflectionLabel1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ReflectionLabel1.Name = "ReflectionLabel1"
        Me.ReflectionLabel1.Size = New System.Drawing.Size(387, 54)
        Me.ReflectionLabel1.TabIndex = 1
        Me.ReflectionLabel1.Text = "<b><font size=""+6""><i>His</i><font color=""#B02B2C"">torial de pago</font></font></" &
    "b>"
        '
        'DGlecturas
        '
        Me.DGlecturas.AllowUserToAddRows = False
        Me.DGlecturas.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.DGlecturas.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGlecturas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGlecturas.Location = New System.Drawing.Point(28, 78)
        Me.DGlecturas.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DGlecturas.Name = "DGlecturas"
        Me.DGlecturas.ReadOnly = True
        Me.DGlecturas.RowHeadersWidth = 51
        Me.DGlecturas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGlecturas.Size = New System.Drawing.Size(929, 271)
        Me.DGlecturas.TabIndex = 3
        '
        'AdvHistorial
        '
        Me.AdvHistorial.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline
        Me.AdvHistorial.BackColor = System.Drawing.SystemColors.Window
        '
        '
        '
        Me.AdvHistorial.BackgroundStyle.Class = "TreeBorderKey"
        Me.AdvHistorial.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.AdvHistorial.Columns.Add(Me.fecha)
        Me.AdvHistorial.Columns.Add(Me.Recibo)
        Me.AdvHistorial.Columns.Add(Me.periodo)
        Me.AdvHistorial.Columns.Add(Me.Subtotal)
        Me.AdvHistorial.Columns.Add(Me.IVA)
        Me.AdvHistorial.Columns.Add(Me.Vale)
        Me.AdvHistorial.Columns.Add(Me.total)
        Me.AdvHistorial.Columns.Add(Me.descuento)
        Me.AdvHistorial.Columns.Add(Me.observacion)
        Me.AdvHistorial.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F"
        Me.AdvHistorial.Location = New System.Drawing.Point(28, 78)
        Me.AdvHistorial.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.AdvHistorial.Name = "AdvHistorial"
        Me.AdvHistorial.NodesConnector = Me.NodeConnector1
        Me.AdvHistorial.NodeStyle = Me.ElementStyle1
        Me.AdvHistorial.PathSeparator = ";"
        Me.AdvHistorial.Size = New System.Drawing.Size(1279, 271)
        Me.AdvHistorial.Styles.Add(Me.ElementStyle1)
        Me.AdvHistorial.TabIndex = 4
        Me.AdvHistorial.Text = "AdvTree1"
        '
        'fecha
        '
        Me.fecha.Name = "fecha"
        Me.fecha.Text = "Fecha"
        Me.fecha.Width.Absolute = 110
        '
        'Recibo
        '
        Me.Recibo.Name = "Recibo"
        Me.Recibo.Text = "Recibos"
        Me.Recibo.Width.Absolute = 80
        '
        'periodo
        '
        Me.periodo.Name = "periodo"
        Me.periodo.Text = "Periodo"
        Me.periodo.Width.Absolute = 150
        '
        'Subtotal
        '
        Me.Subtotal.ImageAlignment = DevComponents.AdvTree.eColumnImageAlignment.Right
        Me.Subtotal.Name = "Subtotal"
        Me.Subtotal.Text = "Subtotal"
        Me.Subtotal.Width.Absolute = 100
        '
        'IVA
        '
        Me.IVA.ImageAlignment = DevComponents.AdvTree.eColumnImageAlignment.Right
        Me.IVA.Name = "IVA"
        Me.IVA.Text = "IVA"
        Me.IVA.Width.Absolute = 100
        '
        'Vale
        '
        Me.Vale.Name = "Vale"
        Me.Vale.Text = "Vale"
        Me.Vale.Width.Absolute = 80
        '
        'total
        '
        Me.total.ImageAlignment = DevComponents.AdvTree.eColumnImageAlignment.Right
        Me.total.Name = "total"
        Me.total.Text = "Total"
        Me.total.Width.Absolute = 150
        '
        'descuento
        '
        Me.descuento.ImageAlignment = DevComponents.AdvTree.eColumnImageAlignment.Right
        Me.descuento.Name = "descuento"
        Me.descuento.Text = "Descuento"
        Me.descuento.Width.Absolute = 150
        '
        'observacion
        '
        Me.observacion.Name = "observacion"
        Me.observacion.Text = "Observacion"
        Me.observacion.Width.Absolute = 300
        '
        'NodeConnector1
        '
        Me.NodeConnector1.LineColor = System.Drawing.SystemColors.ControlText
        '
        'ElementStyle1
        '
        Me.ElementStyle1.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.ElementStyle1.Name = "ElementStyle1"
        Me.ElementStyle1.TextColor = System.Drawing.SystemColors.ControlText
        '
        'SuperTabControl1
        '
        '
        '
        '
        '
        '
        '
        Me.SuperTabControl1.ControlBox.CloseBox.Name = ""
        '
        '
        '
        Me.SuperTabControl1.ControlBox.MenuBox.Name = ""
        Me.SuperTabControl1.ControlBox.Name = ""
        Me.SuperTabControl1.ControlBox.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.SuperTabControl1.ControlBox.MenuBox, Me.SuperTabControl1.ControlBox.CloseBox})
        Me.SuperTabControl1.Controls.Add(Me.SuperTabControlPanel1)
        Me.SuperTabControl1.Controls.Add(Me.SuperTabControlPanel2)
        Me.SuperTabControl1.Controls.Add(Me.SuperTabControlPanel3)
        Me.SuperTabControl1.Location = New System.Drawing.Point(16, 15)
        Me.SuperTabControl1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SuperTabControl1.Name = "SuperTabControl1"
        Me.SuperTabControl1.ReorderTabsEnabled = True
        Me.SuperTabControl1.SelectedTabFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.SuperTabControl1.SelectedTabIndex = 0
        Me.SuperTabControl1.Size = New System.Drawing.Size(1341, 406)
        Me.SuperTabControl1.TabFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SuperTabControl1.TabIndex = 9
        Me.SuperTabControl1.Tabs.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.SuperTabItem2, Me.SuperTabItem1, Me.SuperTabItem3})
        Me.SuperTabControl1.Text = "Lecturas reales"
        '
        'SuperTabControlPanel1
        '
        Me.SuperTabControlPanel1.Controls.Add(Me.btnimprimir)
        Me.SuperTabControlPanel1.Controls.Add(Me.ReflectionLabel1)
        Me.SuperTabControlPanel1.Controls.Add(Me.AdvHistorial)
        Me.SuperTabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SuperTabControlPanel1.Location = New System.Drawing.Point(0, 28)
        Me.SuperTabControlPanel1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SuperTabControlPanel1.Name = "SuperTabControlPanel1"
        Me.SuperTabControlPanel1.Size = New System.Drawing.Size(1341, 378)
        Me.SuperTabControlPanel1.TabIndex = 1
        Me.SuperTabControlPanel1.TabItem = Me.SuperTabItem1
        '
        'btnimprimir
        '
        Me.btnimprimir.Location = New System.Drawing.Point(504, 16)
        Me.btnimprimir.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnimprimir.Name = "btnimprimir"
        Me.btnimprimir.Size = New System.Drawing.Size(204, 42)
        Me.btnimprimir.TabIndex = 5
        Me.btnimprimir.Text = "Imprimir"
        Me.btnimprimir.UseVisualStyleBackColor = True
        '
        'SuperTabItem1
        '
        Me.SuperTabItem1.AttachedControl = Me.SuperTabControlPanel1
        Me.SuperTabItem1.GlobalItem = False
        Me.SuperTabItem1.Name = "SuperTabItem1"
        Me.SuperTabItem1.Text = "Historial de Pagos"
        '
        'SuperTabControlPanel2
        '
        Me.SuperTabControlPanel2.Controls.Add(Me.Btnhabilitar)
        Me.SuperTabControlPanel2.Controls.Add(Me.btnpagado)
        Me.SuperTabControlPanel2.Controls.Add(Me.btnimprimirlecturas)
        Me.SuperTabControlPanel2.Controls.Add(Me.ReflectionLabel2)
        Me.SuperTabControlPanel2.Controls.Add(Me.DGlecturas)
        Me.SuperTabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SuperTabControlPanel2.Location = New System.Drawing.Point(0, 28)
        Me.SuperTabControlPanel2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SuperTabControlPanel2.Name = "SuperTabControlPanel2"
        Me.SuperTabControlPanel2.Size = New System.Drawing.Size(1341, 378)
        Me.SuperTabControlPanel2.TabIndex = 0
        Me.SuperTabControlPanel2.TabItem = Me.SuperTabItem2
        '
        'Btnhabilitar
        '
        Me.Btnhabilitar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.Btnhabilitar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.Btnhabilitar.Location = New System.Drawing.Point(985, 177)
        Me.Btnhabilitar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Btnhabilitar.Name = "Btnhabilitar"
        Me.Btnhabilitar.Size = New System.Drawing.Size(100, 85)
        Me.Btnhabilitar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.Btnhabilitar.TabIndex = 8
        Me.Btnhabilitar.Text = "Habilitar para pago"
        '
        'btnpagado
        '
        Me.btnpagado.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnpagado.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnpagado.Location = New System.Drawing.Point(985, 78)
        Me.btnpagado.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnpagado.Name = "btnpagado"
        Me.btnpagado.Size = New System.Drawing.Size(100, 80)
        Me.btnpagado.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnpagado.TabIndex = 7
        Me.btnpagado.Text = "Dar por pagado "
        '
        'btnimprimirlecturas
        '
        Me.btnimprimirlecturas.Location = New System.Drawing.Point(529, 16)
        Me.btnimprimirlecturas.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnimprimirlecturas.Name = "btnimprimirlecturas"
        Me.btnimprimirlecturas.Size = New System.Drawing.Size(204, 42)
        Me.btnimprimirlecturas.TabIndex = 6
        Me.btnimprimirlecturas.Text = "Imprimir"
        Me.btnimprimirlecturas.UseVisualStyleBackColor = True
        '
        'ReflectionLabel2
        '
        Me.ReflectionLabel2.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.ReflectionLabel2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.ReflectionLabel2.Location = New System.Drawing.Point(28, 16)
        Me.ReflectionLabel2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ReflectionLabel2.Name = "ReflectionLabel2"
        Me.ReflectionLabel2.Size = New System.Drawing.Size(387, 47)
        Me.ReflectionLabel2.TabIndex = 3
        Me.ReflectionLabel2.Text = "<b><font size=""+6""><i>His</i><font color=""#B02B2C"">torial de lecturas</font></fon" &
    "t></b>"
        '
        'SuperTabItem2
        '
        Me.SuperTabItem2.AttachedControl = Me.SuperTabControlPanel2
        Me.SuperTabItem2.GlobalItem = False
        Me.SuperTabItem2.Name = "SuperTabItem2"
        Me.SuperTabItem2.Text = "Historial de Lecturas"
        '
        'SuperTabControlPanel3
        '
        Me.SuperTabControlPanel3.Controls.Add(Me.ReflectionLabel3)
        Me.SuperTabControlPanel3.Controls.Add(Me.DGHistorialAnticipos)
        Me.SuperTabControlPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SuperTabControlPanel3.Location = New System.Drawing.Point(0, 31)
        Me.SuperTabControlPanel3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SuperTabControlPanel3.Name = "SuperTabControlPanel3"
        Me.SuperTabControlPanel3.Size = New System.Drawing.Size(1125, 375)
        Me.SuperTabControlPanel3.TabIndex = 0
        Me.SuperTabControlPanel3.TabItem = Me.SuperTabItem3
        '
        'ReflectionLabel3
        '
        Me.ReflectionLabel3.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.ReflectionLabel3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.ReflectionLabel3.Location = New System.Drawing.Point(28, 16)
        Me.ReflectionLabel3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ReflectionLabel3.Name = "ReflectionLabel3"
        Me.ReflectionLabel3.Size = New System.Drawing.Size(387, 54)
        Me.ReflectionLabel3.TabIndex = 10
        Me.ReflectionLabel3.Text = "<b><font size=""+6""><i>His</i><font color=""#B02B2C"">torial de Anticipos</font></fo" &
    "nt></b>"
        '
        'DGHistorialAnticipos
        '
        Me.DGHistorialAnticipos.AllowUserToAddRows = False
        Me.DGHistorialAnticipos.AllowUserToDeleteRows = False
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DimGray
        Me.DGHistorialAnticipos.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.DGHistorialAnticipos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGHistorialAnticipos.Location = New System.Drawing.Point(28, 78)
        Me.DGHistorialAnticipos.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DGHistorialAnticipos.Name = "DGHistorialAnticipos"
        Me.DGHistorialAnticipos.ReadOnly = True
        Me.DGHistorialAnticipos.RowHeadersWidth = 51
        Me.DGHistorialAnticipos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGHistorialAnticipos.Size = New System.Drawing.Size(1067, 271)
        Me.DGHistorialAnticipos.TabIndex = 10
        '
        'SuperTabItem3
        '
        Me.SuperTabItem3.AttachedControl = Me.SuperTabControlPanel3
        Me.SuperTabItem3.GlobalItem = False
        Me.SuperTabItem3.Name = "SuperTabItem3"
        Me.SuperTabItem3.Text = "Historial de Anticipos"
        '
        'FrmHistorial
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1426, 450)
        Me.Controls.Add(Me.SuperTabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.Name = "FrmHistorial"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Historial de pagos"
        CType(Me.DGlecturas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AdvHistorial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SuperTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SuperTabControl1.ResumeLayout(False)
        Me.SuperTabControlPanel1.ResumeLayout(False)
        Me.SuperTabControlPanel2.ResumeLayout(False)
        Me.SuperTabControlPanel3.ResumeLayout(False)
        CType(Me.DGHistorialAnticipos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ReflectionLabel1 As DevComponents.DotNetBar.Controls.ReflectionLabel
    Friend WithEvents DGlecturas As System.Windows.Forms.DataGridView
    Friend WithEvents AdvHistorial As DevComponents.AdvTree.AdvTree
    Friend WithEvents NodeConnector1 As DevComponents.AdvTree.NodeConnector
    Friend WithEvents ElementStyle1 As DevComponents.DotNetBar.ElementStyle
    Friend WithEvents fecha As DevComponents.AdvTree.ColumnHeader
    Friend WithEvents Recibo As DevComponents.AdvTree.ColumnHeader
    Friend WithEvents Subtotal As DevComponents.AdvTree.ColumnHeader
    Friend WithEvents IVA As DevComponents.AdvTree.ColumnHeader
    Friend WithEvents total As DevComponents.AdvTree.ColumnHeader
    Friend WithEvents descuento As DevComponents.AdvTree.ColumnHeader
    Friend WithEvents observacion As DevComponents.AdvTree.ColumnHeader
    Friend WithEvents periodo As DevComponents.AdvTree.ColumnHeader
    Friend WithEvents SuperTabControl1 As DevComponents.DotNetBar.SuperTabControl
    Friend WithEvents SuperTabControlPanel2 As DevComponents.DotNetBar.SuperTabControlPanel
    Friend WithEvents ReflectionLabel2 As DevComponents.DotNetBar.Controls.ReflectionLabel
    Friend WithEvents SuperTabItem2 As DevComponents.DotNetBar.SuperTabItem
    Friend WithEvents SuperTabControlPanel1 As DevComponents.DotNetBar.SuperTabControlPanel
    Friend WithEvents SuperTabItem1 As DevComponents.DotNetBar.SuperTabItem
    Friend WithEvents SuperTabControlPanel3 As DevComponents.DotNetBar.SuperTabControlPanel
    Friend WithEvents SuperTabItem3 As DevComponents.DotNetBar.SuperTabItem
    Friend WithEvents ReflectionLabel3 As DevComponents.DotNetBar.Controls.ReflectionLabel
    Friend WithEvents DGHistorialAnticipos As System.Windows.Forms.DataGridView
    Friend WithEvents btnimprimir As System.Windows.Forms.Button
    Friend WithEvents btnimprimirlecturas As Button
    Friend WithEvents btnpagado As DevComponents.DotNetBar.ButtonX
    Friend WithEvents Btnhabilitar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents Vale As DevComponents.AdvTree.ColumnHeader
End Class


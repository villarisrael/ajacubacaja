<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmShowMeses
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
        Me.lblNumMeses = New DevComponents.DotNetBar.LabelX()
        Me.lblMesActual = New DevComponents.DotNetBar.LabelX()
        Me.txtMesAct = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtNumM = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Mes = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Periodo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.M3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Monto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BtnCancelar = New DevComponents.DotNetBar.ButtonX()
        Me.ButtonX3 = New DevComponents.DotNetBar.ButtonX()
        Me.lblUltLec = New DevComponents.DotNetBar.LabelX()
        Me.lblPromedio = New DevComponents.DotNetBar.LabelX()
        Me.txtUltLec = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtPromedio = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.ButtonX2 = New DevComponents.DotNetBar.ButtonX()
        Me.SuperTooltip1 = New DevComponents.DotNetBar.SuperTooltip()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblNumMeses
        '
        Me.lblNumMeses.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lblNumMeses.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblNumMeses.ForeColor = System.Drawing.Color.DarkOrange
        Me.lblNumMeses.Location = New System.Drawing.Point(42, 76)
        Me.lblNumMeses.Name = "lblNumMeses"
        Me.lblNumMeses.Size = New System.Drawing.Size(74, 22)
        Me.lblNumMeses.TabIndex = 0
        Me.lblNumMeses.Text = "Num Meses:"
        '
        'lblMesActual
        '
        Me.lblMesActual.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lblMesActual.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblMesActual.ForeColor = System.Drawing.Color.DarkOrange
        Me.lblMesActual.Location = New System.Drawing.Point(55, 104)
        Me.lblMesActual.Name = "lblMesActual"
        Me.lblMesActual.Size = New System.Drawing.Size(61, 22)
        Me.lblMesActual.TabIndex = 1
        Me.lblMesActual.Text = "Mes Actual"
        '
        'txtMesAct
        '
        '
        '
        '
        Me.txtMesAct.Border.Class = "TextBoxBorder"
        Me.txtMesAct.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtMesAct.Location = New System.Drawing.Point(122, 106)
        Me.txtMesAct.Name = "txtMesAct"
        Me.txtMesAct.PreventEnterBeep = True
        Me.txtMesAct.Size = New System.Drawing.Size(140, 22)
        Me.txtMesAct.TabIndex = 2
        '
        'txtNumM
        '
        '
        '
        '
        Me.txtNumM.Border.Class = "TextBoxBorder"
        Me.txtNumM.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtNumM.Location = New System.Drawing.Point(126, 76)
        Me.txtNumM.Name = "txtNumM"
        Me.txtNumM.PreventEnterBeep = True
        Me.txtNumM.Size = New System.Drawing.Size(136, 22)
        Me.txtNumM.TabIndex = 3
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Mes, Me.Periodo, Me.M3, Me.Monto})
        Me.DataGridView1.Location = New System.Drawing.Point(278, 18)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(310, 108)
        Me.DataGridView1.TabIndex = 4
        '
        'Mes
        '
        Me.Mes.HeaderText = "Mes"
        Me.Mes.Name = "Mes"
        Me.Mes.ReadOnly = True
        Me.Mes.Width = 50
        '
        'Periodo
        '
        Me.Periodo.HeaderText = "Periodo"
        Me.Periodo.Name = "Periodo"
        Me.Periodo.Width = 50
        '
        'M3
        '
        Me.M3.HeaderText = "M3"
        Me.M3.Name = "M3"
        Me.M3.Width = 50
        '
        'Monto
        '
        Me.Monto.HeaderText = "Monto"
        Me.Monto.Name = "Monto"
        Me.Monto.ReadOnly = True
        '
        'BtnCancelar
        '
        Me.BtnCancelar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.BtnCancelar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.BtnCancelar.Location = New System.Drawing.Point(507, 161)
        Me.BtnCancelar.Name = "BtnCancelar"
        Me.BtnCancelar.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlDel)
        Me.BtnCancelar.Size = New System.Drawing.Size(110, 37)
        Me.BtnCancelar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.BtnCancelar.TabIndex = 5
        Me.BtnCancelar.Text = "Ctrl + Del =Cancelar"
        '
        'ButtonX3
        '
        Me.ButtonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.ButtonX3.Location = New System.Drawing.Point(260, 160)
        Me.ButtonX3.Name = "ButtonX3"
        Me.ButtonX3.Size = New System.Drawing.Size(97, 37)
        Me.ButtonX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ButtonX3.TabIndex = 7
        Me.ButtonX3.Text = "Rellenar con Promedio"
        '
        'lblUltLec
        '
        Me.lblUltLec.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lblUltLec.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblUltLec.ForeColor = System.Drawing.Color.DarkOrange
        Me.lblUltLec.Location = New System.Drawing.Point(42, 20)
        Me.lblUltLec.Name = "lblUltLec"
        Me.lblUltLec.Size = New System.Drawing.Size(78, 22)
        Me.lblUltLec.TabIndex = 8
        Me.lblUltLec.Text = "Ultima Lectura:"
        '
        'lblPromedio
        '
        Me.lblPromedio.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lblPromedio.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblPromedio.ForeColor = System.Drawing.Color.DarkOrange
        Me.lblPromedio.Location = New System.Drawing.Point(5, 48)
        Me.lblPromedio.Name = "lblPromedio"
        Me.lblPromedio.Size = New System.Drawing.Size(115, 22)
        Me.lblPromedio.TabIndex = 9
        Me.lblPromedio.Text = "Promedio de Consumo:"
        '
        'txtUltLec
        '
        '
        '
        '
        Me.txtUltLec.Border.Class = "TextBoxBorder"
        Me.txtUltLec.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtUltLec.Location = New System.Drawing.Point(126, 20)
        Me.txtUltLec.Name = "txtUltLec"
        Me.txtUltLec.PreventEnterBeep = True
        Me.txtUltLec.Size = New System.Drawing.Size(136, 22)
        Me.txtUltLec.TabIndex = 10
        '
        'txtPromedio
        '
        '
        '
        '
        Me.txtPromedio.Border.Class = "TextBoxBorder"
        Me.txtPromedio.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtPromedio.Location = New System.Drawing.Point(126, 48)
        Me.txtPromedio.Name = "txtPromedio"
        Me.txtPromedio.PreventEnterBeep = True
        Me.txtPromedio.Size = New System.Drawing.Size(136, 22)
        Me.txtPromedio.TabIndex = 11
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.lblUltLec)
        Me.RadGroupBox1.Controls.Add(Me.txtPromedio)
        Me.RadGroupBox1.Controls.Add(Me.txtMesAct)
        Me.RadGroupBox1.Controls.Add(Me.lblNumMeses)
        Me.RadGroupBox1.Controls.Add(Me.DataGridView1)
        Me.RadGroupBox1.Controls.Add(Me.lblPromedio)
        Me.RadGroupBox1.Controls.Add(Me.txtUltLec)
        Me.RadGroupBox1.Controls.Add(Me.txtNumM)
        Me.RadGroupBox1.Controls.Add(Me.lblMesActual)
        Me.RadGroupBox1.HeaderText = "Detalles                                                                         " &
    "       Capturar Lecturas"
        Me.RadGroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        '
        '
        '
        Me.RadGroupBox1.RootElement.Padding = New System.Windows.Forms.Padding(2, 18, 2, 2)
        Me.RadGroupBox1.Size = New System.Drawing.Size(605, 142)
        Me.RadGroupBox1.TabIndex = 14
        Me.RadGroupBox1.Text = "Detalles                                                                         " &
    "       Capturar Lecturas"
        '
        'ButtonX2
        '
        Me.ButtonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.ButtonX2.Location = New System.Drawing.Point(363, 160)
        Me.ButtonX2.Name = "ButtonX2"
        Me.ButtonX2.Size = New System.Drawing.Size(138, 37)
        Me.ButtonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ButtonX2.TabIndex = 15
        Me.ButtonX2.Text = "Aceptar"
        '
        'SuperTooltip1
        '
        Me.SuperTooltip1.DefaultTooltipSettings = New DevComponents.DotNetBar.SuperTooltipInfo("", "", "", Nothing, Nothing, DevComponents.DotNetBar.eTooltipColor.Gray)
        Me.SuperTooltip1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 209)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(507, 13)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Presiona el boton de Cancelar o las Teclas Ctrl + Del  para indicar que no quiere" &
    "s agregar lecturas"
        '
        'FrmShowMeses
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(642, 231)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ButtonX2)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Controls.Add(Me.ButtonX3)
        Me.Controls.Add(Me.BtnCancelar)
        Me.ForeColor = System.Drawing.Color.Gray
        Me.Name = "FrmShowMeses"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Ingresa el estimado de las lecturas faltantes y anticipadas"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblNumMeses As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblMesActual As DevComponents.DotNetBar.LabelX
    Public WithEvents txtMesAct As DevComponents.DotNetBar.Controls.TextBoxX
    Public WithEvents txtNumM As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents BtnCancelar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents ButtonX3 As DevComponents.DotNetBar.ButtonX
    Friend WithEvents lblUltLec As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblPromedio As DevComponents.DotNetBar.LabelX
    Public WithEvents txtUltLec As DevComponents.DotNetBar.Controls.TextBoxX
    Public WithEvents txtPromedio As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents ButtonX2 As DevComponents.DotNetBar.ButtonX
    Friend WithEvents SuperTooltip1 As DevComponents.DotNetBar.SuperTooltip
    Friend WithEvents Mes As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Periodo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents M3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Monto As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class


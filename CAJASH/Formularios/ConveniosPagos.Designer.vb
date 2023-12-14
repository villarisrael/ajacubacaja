<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ConveniosPagos
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.dataConv = New System.Windows.Forms.DataGridView()
        Me.gpDatCont = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.btnAplicar = New DevComponents.DotNetBar.ButtonX()
        CType(Me.dataConv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gpDatCont.SuspendLayout()
        Me.SuspendLayout()
        '
        'dataConv
        '
        Me.dataConv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dataConv.Location = New System.Drawing.Point(8, 3)
        Me.dataConv.Name = "dataConv"
        Me.dataConv.Size = New System.Drawing.Size(591, 274)
        Me.dataConv.TabIndex = 0
        '
        'gpDatCont
        '
        Me.gpDatCont.CanvasColor = System.Drawing.SystemColors.Control
        Me.gpDatCont.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.gpDatCont.Controls.Add(Me.dataConv)
        Me.gpDatCont.DisabledBackColor = System.Drawing.Color.Empty
        Me.gpDatCont.Location = New System.Drawing.Point(1, 36)
        Me.gpDatCont.Name = "gpDatCont"
        Me.gpDatCont.Size = New System.Drawing.Size(613, 280)
        '
        '
        '
        Me.gpDatCont.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.gpDatCont.Style.BackColorGradientAngle = 90
        Me.gpDatCont.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.gpDatCont.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpDatCont.Style.BorderBottomWidth = 1
        Me.gpDatCont.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.gpDatCont.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpDatCont.Style.BorderLeftWidth = 1
        Me.gpDatCont.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpDatCont.Style.BorderRightWidth = 1
        Me.gpDatCont.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpDatCont.Style.BorderTopWidth = 1
        Me.gpDatCont.Style.CornerDiameter = 4
        Me.gpDatCont.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.gpDatCont.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.gpDatCont.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.gpDatCont.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.gpDatCont.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.gpDatCont.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.gpDatCont.TabIndex = 1
        Me.gpDatCont.Text = "Detalles del convenio"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(110, 10)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(200, 20)
        Me.DateTimePicker1.TabIndex = 2
        '
        'LabelX1
        '
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Location = New System.Drawing.Point(12, 10)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(75, 23)
        Me.LabelX1.TabIndex = 3
        Me.LabelX1.Text = "Aplicar hasta la fecha:"
        '
        'btnAplicar
        '
        Me.btnAplicar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnAplicar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnAplicar.Location = New System.Drawing.Point(316, 7)
        Me.btnAplicar.Name = "btnAplicar"
        Me.btnAplicar.Size = New System.Drawing.Size(75, 23)
        Me.btnAplicar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnAplicar.TabIndex = 4
        Me.btnAplicar.Text = "Aplicar"
        '
        'ConveniosPagos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(615, 329)
        Me.Controls.Add(Me.btnAplicar)
        Me.Controls.Add(Me.LabelX1)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.gpDatCont)
        Me.Name = "ConveniosPagos"
        Me.Text = "ConveniosPagos"
        CType(Me.dataConv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gpDatCont.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents dataConv As DataGridView
    Private WithEvents gpDatCont As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents btnAplicar As DevComponents.DotNetBar.ButtonX
End Class

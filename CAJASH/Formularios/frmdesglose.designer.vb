<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmdesglose
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Dtgdesglose = New System.Windows.Forms.DataGridView()
        Me.lbldeque = New DevComponents.DotNetBar.Controls.ReflectionLabel()
        Me.Mes = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ano = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Monto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Descuento = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Total = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.iva = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Totalconiva = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.Dtgdesglose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Dtgdesglose
        '
        Me.Dtgdesglose.AllowUserToAddRows = False
        Me.Dtgdesglose.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Dtgdesglose.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Mes, Me.ano, Me.Monto, Me.Descuento, Me.Total, Me.iva, Me.Totalconiva})
        Me.Dtgdesglose.Location = New System.Drawing.Point(8, 57)
        Me.Dtgdesglose.Name = "Dtgdesglose"
        Me.Dtgdesglose.ReadOnly = True
        Me.Dtgdesglose.Size = New System.Drawing.Size(572, 255)
        Me.Dtgdesglose.TabIndex = 0
        '
        'lbldeque
        '
        '
        '
        '
        Me.lbldeque.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbldeque.Location = New System.Drawing.Point(8, 13)
        Me.lbldeque.Name = "lbldeque"
        Me.lbldeque.Size = New System.Drawing.Size(656, 38)
        Me.lbldeque.TabIndex = 1
        Me.lbldeque.Text = "<b><font size=""+6""><i>Desglose de</i><font color=""#B02B2C""> Consumo</font></font>" & _
            "</b>"
        '
        'Mes
        '
        Me.Mes.HeaderText = "Mes"
        Me.Mes.Name = "Mes"
        Me.Mes.ReadOnly = True
        Me.Mes.Width = 50
        '
        'ano
        '
        Me.ano.HeaderText = "Año"
        Me.ano.Name = "ano"
        Me.ano.ReadOnly = True
        Me.ano.Width = 50
        '
        'Monto
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.Format = "C2"
        DataGridViewCellStyle1.NullValue = "0"
        Me.Monto.DefaultCellStyle = DataGridViewCellStyle1
        Me.Monto.HeaderText = "Monto"
        Me.Monto.MaxInputLength = 50
        Me.Monto.Name = "Monto"
        Me.Monto.ReadOnly = True
        '
        'Descuento
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "C2"
        DataGridViewCellStyle2.NullValue = "0"
        Me.Descuento.DefaultCellStyle = DataGridViewCellStyle2
        Me.Descuento.HeaderText = "Descuento"
        Me.Descuento.Name = "Descuento"
        Me.Descuento.ReadOnly = True
        Me.Descuento.Width = 70
        '
        'Total
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "C2"
        DataGridViewCellStyle3.NullValue = "0"
        Me.Total.DefaultCellStyle = DataGridViewCellStyle3
        Me.Total.HeaderText = "Total"
        Me.Total.Name = "Total"
        Me.Total.ReadOnly = True
        '
        'iva
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight
        DataGridViewCellStyle4.Format = "C2"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.iva.DefaultCellStyle = DataGridViewCellStyle4
        Me.iva.HeaderText = "IVA"
        Me.iva.Name = "iva"
        Me.iva.ReadOnly = True
        Me.iva.Width = 50
        '
        'Totalconiva
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight
        DataGridViewCellStyle5.Format = "C2"
        DataGridViewCellStyle5.NullValue = Nothing
        Me.Totalconiva.DefaultCellStyle = DataGridViewCellStyle5
        Me.Totalconiva.HeaderText = "Total_con_IVA"
        Me.Totalconiva.Name = "Totalconiva"
        Me.Totalconiva.ReadOnly = True
        '
        'frmdesglose
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(587, 322)
        Me.Controls.Add(Me.lbldeque)
        Me.Controls.Add(Me.Dtgdesglose)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmdesglose"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Desglose"
        CType(Me.Dtgdesglose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Dtgdesglose As System.Windows.Forms.DataGridView
    Friend WithEvents lbldeque As DevComponents.DotNetBar.Controls.ReflectionLabel
    Friend WithEvents Mes As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ano As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Monto As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Descuento As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Total As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents iva As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Totalconiva As System.Windows.Forms.DataGridViewTextBoxColumn
End Class


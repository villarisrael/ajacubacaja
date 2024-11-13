<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmReportexrubros
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
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.btncancelar = New Telerik.WinControls.UI.RadButton()
        Me.btnaceptar = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.dtpfechafinal = New System.Windows.Forms.MonthCalendar()
        Me.dtpfechainicio = New System.Windows.Forms.MonthCalendar()
        Me.RadLabel2 = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel1 = New Telerik.WinControls.UI.RadLabel()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadLabel5 = New Telerik.WinControls.UI.RadLabel()
        Me.IIfoliofinal = New DevComponents.Editors.IntegerInput()
        Me.IIfolioinicial = New DevComponents.Editors.IntegerInput()
        Me.RadLabel4 = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel3 = New Telerik.WinControls.UI.RadLabel()
        Me.txtcaja = New System.Windows.Forms.TextBox()
        Me.CheckExcel = New Telerik.WinControls.UI.RadCheckBox()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.btncancelar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnaceptar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IIfoliofinal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IIfolioinicial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CheckExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.btncancelar)
        Me.RadGroupBox2.Controls.Add(Me.btnaceptar)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office
        Me.RadGroupBox2.HeaderText = "ACCIONES"
        Me.RadGroupBox2.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadGroupBox2.Location = New System.Drawing.Point(25, 330)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        '
        '
        '
        Me.RadGroupBox2.RootElement.Padding = New System.Windows.Forms.Padding(2, 18, 2, 2)
        Me.RadGroupBox2.Size = New System.Drawing.Size(553, 78)
        Me.RadGroupBox2.TabIndex = 2
        Me.RadGroupBox2.Text = "ACCIONES"
        '
        'btncancelar
        '
        Me.btncancelar.Image = Global.CAJAS.My.Resources.Resources.INI
        Me.btncancelar.ImageAlignment = System.Drawing.ContentAlignment.TopCenter
        Me.btncancelar.Location = New System.Drawing.Point(450, 21)
        Me.btncancelar.Name = "btncancelar"
        Me.btncancelar.Size = New System.Drawing.Size(97, 47)
        Me.btncancelar.TabIndex = 1
        Me.btncancelar.Text = "CANCELAR"
        Me.btncancelar.TextAlignment = System.Drawing.ContentAlignment.BottomCenter
        '
        'btnaceptar
        '
        Me.btnaceptar.Image = Global.CAJAS.My.Resources.Resources.IcoAgregar
        Me.btnaceptar.ImageAlignment = System.Drawing.ContentAlignment.TopCenter
        Me.btnaceptar.Location = New System.Drawing.Point(25, 21)
        Me.btnaceptar.Name = "btnaceptar"
        Me.btnaceptar.Size = New System.Drawing.Size(97, 47)
        Me.btnaceptar.TabIndex = 0
        Me.btnaceptar.Text = "ACEPTAR"
        Me.btnaceptar.TextAlignment = System.Drawing.ContentAlignment.BottomCenter
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.dtpfechafinal)
        Me.RadGroupBox1.Controls.Add(Me.dtpfechainicio)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office
        Me.RadGroupBox1.HeaderText = "PERIODO DEL REPORTE"
        Me.RadGroupBox1.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadGroupBox1.Location = New System.Drawing.Point(25, 16)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        '
        '
        '
        Me.RadGroupBox1.RootElement.Padding = New System.Windows.Forms.Padding(2, 18, 2, 2)
        Me.RadGroupBox1.Size = New System.Drawing.Size(553, 217)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = "PERIODO DEL REPORTE"
        '
        'dtpfechafinal
        '
        Me.dtpfechafinal.Location = New System.Drawing.Point(299, 41)
        Me.dtpfechafinal.Name = "dtpfechafinal"
        Me.dtpfechafinal.TabIndex = 22
        '
        'dtpfechainicio
        '
        Me.dtpfechainicio.Location = New System.Drawing.Point(25, 41)
        Me.dtpfechainicio.Name = "dtpfechainicio"
        Me.dtpfechainicio.TabIndex = 21
        '
        'RadLabel2
        '
        Me.RadLabel2.AutoSize = True
        Me.RadLabel2.Location = New System.Drawing.Point(330, 21)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(73, 18)
        Me.RadLabel2.TabIndex = 20
        Me.RadLabel2.Text = "FECHA FINAL"
        '
        'RadLabel1
        '
        Me.RadLabel1.AutoSize = True
        Me.RadLabel1.Location = New System.Drawing.Point(53, 21)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(93, 18)
        Me.RadLabel1.TabIndex = 19
        Me.RadLabel1.Text = "FECHA DE INICIO"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.CheckExcel)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel5)
        Me.RadGroupBox3.Controls.Add(Me.IIfoliofinal)
        Me.RadGroupBox3.Controls.Add(Me.IIfolioinicial)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel4)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox3.Controls.Add(Me.txtcaja)
        Me.RadGroupBox3.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox3.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office
        Me.RadGroupBox3.HeaderText = "Filtro"
        Me.RadGroupBox3.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadGroupBox3.Location = New System.Drawing.Point(25, 240)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        '
        '
        '
        Me.RadGroupBox3.RootElement.Padding = New System.Windows.Forms.Padding(2, 18, 2, 2)
        Me.RadGroupBox3.Size = New System.Drawing.Size(553, 78)
        Me.RadGroupBox3.TabIndex = 1
        Me.RadGroupBox3.Text = "Filtro"
        '
        'RadLabel5
        '
        Me.RadLabel5.AutoSize = True
        Me.RadLabel5.Location = New System.Drawing.Point(284, 32)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(15, 18)
        Me.RadLabel5.TabIndex = 24
        Me.RadLabel5.Text = "al"
        '
        'IIfoliofinal
        '
        '
        '
        '
        Me.IIfoliofinal.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.IIfoliofinal.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.IIfoliofinal.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.IIfoliofinal.Location = New System.Drawing.Point(309, 32)
        Me.IIfoliofinal.Name = "IIfoliofinal"
        Me.IIfoliofinal.ShowUpDown = True
        Me.IIfoliofinal.Size = New System.Drawing.Size(80, 19)
        Me.IIfoliofinal.TabIndex = 23
        '
        'IIfolioinicial
        '
        '
        '
        '
        Me.IIfolioinicial.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.IIfolioinicial.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.IIfolioinicial.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.IIfolioinicial.Location = New System.Drawing.Point(192, 30)
        Me.IIfolioinicial.Name = "IIfolioinicial"
        Me.IIfolioinicial.ShowUpDown = True
        Me.IIfolioinicial.Size = New System.Drawing.Size(80, 19)
        Me.IIfolioinicial.TabIndex = 22
        '
        'RadLabel4
        '
        Me.RadLabel4.AutoSize = True
        Me.RadLabel4.Location = New System.Drawing.Point(121, 33)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(54, 18)
        Me.RadLabel4.TabIndex = 21
        Me.RadLabel4.Text = "Folios del"
        '
        'RadLabel3
        '
        Me.RadLabel3.AutoSize = True
        Me.RadLabel3.Location = New System.Drawing.Point(5, 32)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(27, 18)
        Me.RadLabel3.TabIndex = 20
        Me.RadLabel3.Text = "Caja"
        '
        'txtcaja
        '
        Me.txtcaja.Location = New System.Drawing.Point(53, 32)
        Me.txtcaja.Name = "txtcaja"
        Me.txtcaja.Size = New System.Drawing.Size(64, 19)
        Me.txtcaja.TabIndex = 0
        '
        'CheckExcel
        '
        Me.CheckExcel.AutoSize = True
        Me.CheckExcel.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckExcel.Location = New System.Drawing.Point(433, 21)
        Me.CheckExcel.Name = "CheckExcel"
        Me.CheckExcel.Size = New System.Drawing.Size(111, 21)
        Me.CheckExcel.TabIndex = 25
        Me.CheckExcel.Text = "Exportar Excel"
        '
        'FrmReportexrubros
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(606, 420)
        Me.Controls.Add(Me.RadGroupBox3)
        Me.Controls.Add(Me.RadGroupBox2)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "FrmReportexrubros"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = ""
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.btncancelar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnaceptar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IIfoliofinal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IIfolioinicial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CheckExcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btncancelar As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnaceptar As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadLabel2 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel1 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents dtpfechafinal As System.Windows.Forms.MonthCalendar
    Friend WithEvents dtpfechainicio As System.Windows.Forms.MonthCalendar
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadLabel4 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel3 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txtcaja As System.Windows.Forms.TextBox
    Friend WithEvents RadLabel5 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents IIfoliofinal As DevComponents.Editors.IntegerInput
    Friend WithEvents IIfolioinicial As DevComponents.Editors.IntegerInput
    Friend WithEvents CheckExcel As Telerik.WinControls.UI.RadCheckBox
End Class


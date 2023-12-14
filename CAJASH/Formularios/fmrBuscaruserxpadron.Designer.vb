<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fmrBuscaruserxpadron
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
        Me.UsuarioBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.txtcolonia = New Telerik.WinControls.UI.RadTextBox()
        Me.txtdireccion = New Telerik.WinControls.UI.RadTextBox()
        Me.txtnombre = New Telerik.WinControls.UI.RadTextBox()
        Me.RadLabel3 = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel2 = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel1 = New Telerik.WinControls.UI.RadLabel()
        Me.txtcuenta = New Telerik.WinControls.UI.RadTextBox()
        Me.lblCuenta = New Telerik.WinControls.UI.RadLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.btncancelar = New Telerik.WinControls.UI.RadButton()
        Me.btnagregar = New Telerik.WinControls.UI.RadButton()
        Me.DGVContenido = New System.Windows.Forms.DataGridView()
        CType(Me.UsuarioBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcolonia, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdireccion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtnombre, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcuenta, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCuenta, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.btncancelar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnagregar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVContenido, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'UsuarioBindingSource
        '
        Me.UsuarioBindingSource.DataMember = "usuario"
        '
        'txtcolonia
        '
        Me.txtcolonia.Location = New System.Drawing.Point(456, 70)
        Me.txtcolonia.Name = "txtcolonia"
        Me.txtcolonia.Size = New System.Drawing.Size(282, 20)
        Me.txtcolonia.TabIndex = 3
        Me.txtcolonia.TabStop = False
        Me.txtcolonia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtdireccion
        '
        Me.txtdireccion.Location = New System.Drawing.Point(456, 30)
        Me.txtdireccion.Name = "txtdireccion"
        Me.txtdireccion.Size = New System.Drawing.Size(282, 20)
        Me.txtdireccion.TabIndex = 2
        Me.txtdireccion.TabStop = False
        Me.txtdireccion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtnombre
        '
        Me.txtnombre.Location = New System.Drawing.Point(68, 70)
        Me.txtnombre.Name = "txtnombre"
        Me.txtnombre.Size = New System.Drawing.Size(282, 20)
        Me.txtnombre.TabIndex = 1
        Me.txtnombre.TabStop = False
        Me.txtnombre.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'RadLabel3
        '
        Me.RadLabel3.AutoSize = True
        Me.RadLabel3.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(405, 74)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(49, 15)
        Me.RadLabel3.TabIndex = 18
        Me.RadLabel3.Text = "COLONIA:"
        '
        'RadLabel2
        '
        Me.RadLabel2.AutoSize = True
        Me.RadLabel2.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(396, 30)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(58, 15)
        Me.RadLabel2.TabIndex = 17
        Me.RadLabel2.Text = "DIRECCIÓN:"
        '
        'RadLabel1
        '
        Me.RadLabel1.AutoSize = True
        Me.RadLabel1.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(16, 74)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(48, 15)
        Me.RadLabel1.TabIndex = 16
        Me.RadLabel1.Text = "NOMBRE:"
        '
        'txtcuenta
        '
        Me.txtcuenta.Location = New System.Drawing.Point(68, 30)
        Me.txtcuenta.Name = "txtcuenta"
        Me.txtcuenta.Size = New System.Drawing.Size(282, 20)
        Me.txtcuenta.TabIndex = 0
        Me.txtcuenta.TabStop = False
        Me.txtcuenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblCuenta
        '
        Me.lblCuenta.AutoSize = True
        Me.lblCuenta.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCuenta.Location = New System.Drawing.Point(19, 30)
        Me.lblCuenta.Name = "lblCuenta"
        Me.lblCuenta.Size = New System.Drawing.Size(45, 15)
        Me.lblCuenta.TabIndex = 0
        Me.lblCuenta.Text = "CUENTA:"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.txtcolonia)
        Me.RadGroupBox2.Controls.Add(Me.txtcuenta)
        Me.RadGroupBox2.Controls.Add(Me.txtdireccion)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox2.Controls.Add(Me.lblCuenta)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox2.Controls.Add(Me.txtnombre)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office
        Me.RadGroupBox2.HeaderText = "BÚSQUEDA DE USUARIOS FUERA DE PADRÓN"
        Me.RadGroupBox2.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadGroupBox2.Location = New System.Drawing.Point(22, 5)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        '
        '
        '
        Me.RadGroupBox2.RootElement.Padding = New System.Windows.Forms.Padding(2, 18, 2, 2)
        Me.RadGroupBox2.Size = New System.Drawing.Size(754, 107)
        Me.RadGroupBox2.TabIndex = 8
        Me.RadGroupBox2.Text = "BÚSQUEDA DE USUARIOS FUERA DE PADRÓN"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.btncancelar)
        Me.RadGroupBox1.Controls.Add(Me.btnagregar)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office
        Me.RadGroupBox1.HeaderText = "ACCIONES"
        Me.RadGroupBox1.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadGroupBox1.Location = New System.Drawing.Point(22, 324)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        '
        '
        '
        Me.RadGroupBox1.RootElement.Padding = New System.Windows.Forms.Padding(2, 18, 2, 2)
        Me.RadGroupBox1.Size = New System.Drawing.Size(754, 71)
        Me.RadGroupBox1.TabIndex = 20
        Me.RadGroupBox1.Text = "ACCIONES"
        '
        'btncancelar
        '
        Me.btncancelar.Image = Global.CAJAS.My.Resources.Resources.INI
        Me.btncancelar.Location = New System.Drawing.Point(647, 25)
        Me.btncancelar.Name = "btncancelar"
        Me.btncancelar.Size = New System.Drawing.Size(91, 34)
        Me.btncancelar.TabIndex = 2
        Me.btncancelar.Text = "CANCELAR"
        Me.btncancelar.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnagregar
        '
        Me.btnagregar.Image = Global.CAJAS.My.Resources.Resources.IcoAgregar
        Me.btnagregar.Location = New System.Drawing.Point(19, 25)
        Me.btnagregar.Name = "btnagregar"
        Me.btnagregar.Size = New System.Drawing.Size(91, 34)
        Me.btnagregar.TabIndex = 1
        Me.btnagregar.Text = "Aceptar"
        Me.btnagregar.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'DGVContenido
        '
        Me.DGVContenido.AllowUserToAddRows = False
        Me.DGVContenido.AllowUserToDeleteRows = False
        Me.DGVContenido.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.DGVContenido.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVContenido.Location = New System.Drawing.Point(22, 118)
        Me.DGVContenido.Name = "DGVContenido"
        Me.DGVContenido.ReadOnly = True
        Me.DGVContenido.Size = New System.Drawing.Size(754, 200)
        Me.DGVContenido.TabIndex = 22
        '
        'fmrBuscaruserxpadron
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(799, 418)
        Me.Controls.Add(Me.DGVContenido)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Controls.Add(Me.RadGroupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "fmrBuscaruserxpadron"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = ""
        CType(Me.UsuarioBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcolonia, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdireccion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtnombre, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcuenta, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCuenta, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.btncancelar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnagregar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVContenido, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents UsuarioBindingSource As System.Windows.Forms.BindingSource

    Friend WithEvents txtcuenta As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents lblCuenta As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txtcolonia As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents txtdireccion As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents txtnombre As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents RadLabel3 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel2 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel1 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents CUENTADataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NOMBREDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DIRECCIONDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColoniaDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btncancelar As Telerik.WinControls.UI.RadButton
    Friend WithEvents DGVContenido As System.Windows.Forms.DataGridView
    Friend WithEvents btnagregar As Telerik.WinControls.UI.RadButton
End Class


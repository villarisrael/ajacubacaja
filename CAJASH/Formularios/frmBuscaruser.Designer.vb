<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBuscaruser
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.btncancelar = New Telerik.WinControls.UI.RadButton()
        Me.btnaceptar = New Telerik.WinControls.UI.RadButton()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.DTGbusqueda = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtUbicacion = New Telerik.WinControls.UI.RadTextBox()
        Me.RadLabel5 = New Telerik.WinControls.UI.RadLabel()
        Me.txtcuentaAnterior = New Telerik.WinControls.UI.RadTextBox()
        Me.RadLabel4 = New Telerik.WinControls.UI.RadLabel()
        Me.txtcolonia = New Telerik.WinControls.UI.RadTextBox()
        Me.txtcuenta = New Telerik.WinControls.UI.RadTextBox()
        Me.txtdireccion = New Telerik.WinControls.UI.RadTextBox()
        Me.RadLabel3 = New Telerik.WinControls.UI.RadLabel()
        Me.lblCuenta = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel2 = New Telerik.WinControls.UI.RadLabel()
        Me.txtnombre = New Telerik.WinControls.UI.RadTextBox()
        Me.RadLabel1 = New Telerik.WinControls.UI.RadLabel()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.btncancelar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnaceptar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DTGbusqueda, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.txtUbicacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcuentaAnterior, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcolonia, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcuenta, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdireccion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCuenta, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtnombre, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.RadGroupBox2.Controls.Add(Me.btncancelar)
        Me.RadGroupBox2.Controls.Add(Me.btnaceptar)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office
        Me.RadGroupBox2.HeaderAlignment = Telerik.WinControls.UI.HeaderAlignment.Center
        Me.RadGroupBox2.HeaderText = "ACCIONES"
        Me.RadGroupBox2.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadGroupBox2.Location = New System.Drawing.Point(249, 321)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        '
        '
        '
        Me.RadGroupBox2.RootElement.ControlBounds = New System.Drawing.Rectangle(249, 321, 200, 100)
        Me.RadGroupBox2.RootElement.Padding = New System.Windows.Forms.Padding(2, 18, 2, 2)
        Me.RadGroupBox2.Size = New System.Drawing.Size(726, 84)
        Me.RadGroupBox2.TabIndex = 8
        Me.RadGroupBox2.Text = "ACCIONES"
        '
        'btncancelar
        '
        Me.btncancelar.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.btncancelar.Image = Global.CAJAS.My.Resources.Resources.INI
        Me.btncancelar.ImageAlignment = System.Drawing.ContentAlignment.TopCenter
        Me.btncancelar.Location = New System.Drawing.Point(569, 21)
        Me.btncancelar.Name = "btncancelar"
        '
        '
        '
        Me.btncancelar.RootElement.ControlBounds = New System.Drawing.Rectangle(569, 21, 110, 24)
        Me.btncancelar.Size = New System.Drawing.Size(97, 47)
        Me.btncancelar.TabIndex = 7
        Me.btncancelar.Text = "CANCELAR"
        Me.btncancelar.TextAlignment = System.Drawing.ContentAlignment.BottomCenter
        '
        'btnaceptar
        '
        Me.btnaceptar.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.btnaceptar.Image = Global.CAJAS.My.Resources.Resources.Buscar
        Me.btnaceptar.ImageAlignment = System.Drawing.ContentAlignment.TopCenter
        Me.btnaceptar.Location = New System.Drawing.Point(54, 21)
        Me.btnaceptar.Name = "btnaceptar"
        '
        '
        '
        Me.btnaceptar.RootElement.ControlBounds = New System.Drawing.Rectangle(54, 21, 110, 24)
        Me.btnaceptar.Size = New System.Drawing.Size(97, 47)
        Me.btnaceptar.TabIndex = 7
        Me.btnaceptar.Text = "ACEPTAR"
        Me.btnaceptar.TextAlignment = System.Drawing.ContentAlignment.BottomCenter
        '
        'Timer1
        '
        Me.Timer1.Interval = 20000
        '
        'DTGbusqueda
        '
        Me.DTGbusqueda.AllowUserToAddRows = False
        Me.DTGbusqueda.AllowUserToDeleteRows = False
        Me.DTGbusqueda.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.DTGbusqueda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DTGbusqueda.DefaultCellStyle = DataGridViewCellStyle1
        Me.DTGbusqueda.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.DTGbusqueda.Location = New System.Drawing.Point(14, 125)
        Me.DTGbusqueda.Name = "DTGbusqueda"
        Me.DTGbusqueda.ReadOnly = True
        Me.DTGbusqueda.Size = New System.Drawing.Size(958, 186)
        Me.DTGbusqueda.TabIndex = 9
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.RadGroupBox3.Controls.Add(Me.txtUbicacion)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel5)
        Me.RadGroupBox3.Controls.Add(Me.txtcuentaAnterior)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel4)
        Me.RadGroupBox3.Controls.Add(Me.txtcolonia)
        Me.RadGroupBox3.Controls.Add(Me.txtcuenta)
        Me.RadGroupBox3.Controls.Add(Me.txtdireccion)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox3.Controls.Add(Me.lblCuenta)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox3.Controls.Add(Me.txtnombre)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox3.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office
        Me.RadGroupBox3.HeaderText = "BÚSQUEDA DE USUARIOS REGISTRADOS"
        Me.RadGroupBox3.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadGroupBox3.Location = New System.Drawing.Point(20, 12)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        '
        '
        '
        Me.RadGroupBox3.RootElement.ControlBounds = New System.Drawing.Rectangle(20, 12, 200, 100)
        Me.RadGroupBox3.RootElement.Padding = New System.Windows.Forms.Padding(2, 18, 2, 2)
        Me.RadGroupBox3.Size = New System.Drawing.Size(950, 107)
        Me.RadGroupBox3.TabIndex = 10
        Me.RadGroupBox3.Text = "BÚSQUEDA DE USUARIOS REGISTRADOS"
        '
        'txtUbicacion
        '
        Me.txtUbicacion.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtUbicacion.Location = New System.Drawing.Point(404, 30)
        Me.txtUbicacion.Name = "txtUbicacion"
        '
        '
        '
        Me.txtUbicacion.RootElement.ControlBounds = New System.Drawing.Rectangle(404, 30, 100, 20)
        Me.txtUbicacion.RootElement.StretchVertically = True
        Me.txtUbicacion.Size = New System.Drawing.Size(81, 20)
        Me.txtUbicacion.TabIndex = 22
        Me.txtUbicacion.TabStop = False
        Me.txtUbicacion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'RadLabel5
        '
        Me.RadLabel5.AutoSize = True
        Me.RadLabel5.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.RadLabel5.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(336, 30)
        Me.RadLabel5.Name = "RadLabel5"
        '
        '
        '
        Me.RadLabel5.RootElement.ControlBounds = New System.Drawing.Rectangle(336, 30, 100, 18)
        Me.RadLabel5.Size = New System.Drawing.Size(48, 15)
        Me.RadLabel5.TabIndex = 21
        Me.RadLabel5.Text = "Ubicacion"
        '
        'txtcuentaAnterior
        '
        Me.txtcuentaAnterior.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtcuentaAnterior.Location = New System.Drawing.Point(238, 30)
        Me.txtcuentaAnterior.Name = "txtcuentaAnterior"
        '
        '
        '
        Me.txtcuentaAnterior.RootElement.ControlBounds = New System.Drawing.Rectangle(238, 30, 100, 20)
        Me.txtcuentaAnterior.RootElement.StretchVertically = True
        Me.txtcuentaAnterior.Size = New System.Drawing.Size(81, 20)
        Me.txtcuentaAnterior.TabIndex = 20
        Me.txtcuentaAnterior.TabStop = False
        Me.txtcuentaAnterior.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'RadLabel4
        '
        Me.RadLabel4.AutoSize = True
        Me.RadLabel4.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.RadLabel4.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(187, 30)
        Me.RadLabel4.Name = "RadLabel4"
        '
        '
        '
        Me.RadLabel4.RootElement.ControlBounds = New System.Drawing.Rectangle(187, 30, 100, 18)
        Me.RadLabel4.Size = New System.Drawing.Size(44, 15)
        Me.RadLabel4.TabIndex = 19
        Me.RadLabel4.Text = "Contrato"
        '
        'txtcolonia
        '
        Me.txtcolonia.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtcolonia.Location = New System.Drawing.Point(455, 70)
        Me.txtcolonia.Name = "txtcolonia"
        '
        '
        '
        Me.txtcolonia.RootElement.ControlBounds = New System.Drawing.Rectangle(455, 70, 100, 20)
        Me.txtcolonia.RootElement.StretchVertically = True
        Me.txtcolonia.Size = New System.Drawing.Size(246, 20)
        Me.txtcolonia.TabIndex = 3
        Me.txtcolonia.TabStop = False
        Me.txtcolonia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtcuenta
        '
        Me.txtcuenta.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtcuenta.Location = New System.Drawing.Point(85, 30)
        Me.txtcuenta.Name = "txtcuenta"
        '
        '
        '
        Me.txtcuenta.RootElement.ControlBounds = New System.Drawing.Rectangle(85, 30, 100, 20)
        Me.txtcuenta.RootElement.StretchVertically = True
        Me.txtcuenta.Size = New System.Drawing.Size(81, 20)
        Me.txtcuenta.TabIndex = 0
        Me.txtcuenta.TabStop = False
        Me.txtcuenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtdireccion
        '
        Me.txtdireccion.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtdireccion.Location = New System.Drawing.Point(631, 30)
        Me.txtdireccion.Name = "txtdireccion"
        '
        '
        '
        Me.txtdireccion.RootElement.ControlBounds = New System.Drawing.Rectangle(631, 30, 100, 20)
        Me.txtdireccion.RootElement.StretchVertically = True
        Me.txtdireccion.Size = New System.Drawing.Size(246, 20)
        Me.txtdireccion.TabIndex = 2
        Me.txtdireccion.TabStop = False
        Me.txtdireccion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'RadLabel3
        '
        Me.RadLabel3.AutoSize = True
        Me.RadLabel3.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.RadLabel3.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(404, 74)
        Me.RadLabel3.Name = "RadLabel3"
        '
        '
        '
        Me.RadLabel3.RootElement.ControlBounds = New System.Drawing.Rectangle(404, 74, 100, 18)
        Me.RadLabel3.Size = New System.Drawing.Size(49, 15)
        Me.RadLabel3.TabIndex = 18
        Me.RadLabel3.Text = "COLONIA:"
        '
        'lblCuenta
        '
        Me.lblCuenta.AutoSize = True
        Me.lblCuenta.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblCuenta.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCuenta.Location = New System.Drawing.Point(36, 30)
        Me.lblCuenta.Name = "lblCuenta"
        '
        '
        '
        Me.lblCuenta.RootElement.ControlBounds = New System.Drawing.Rectangle(36, 30, 100, 18)
        Me.lblCuenta.Size = New System.Drawing.Size(45, 15)
        Me.lblCuenta.TabIndex = 0
        Me.lblCuenta.Text = "CUENTA:"
        '
        'RadLabel2
        '
        Me.RadLabel2.AutoSize = True
        Me.RadLabel2.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.RadLabel2.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(545, 30)
        Me.RadLabel2.Name = "RadLabel2"
        '
        '
        '
        Me.RadLabel2.RootElement.ControlBounds = New System.Drawing.Rectangle(545, 30, 100, 18)
        Me.RadLabel2.Size = New System.Drawing.Size(58, 15)
        Me.RadLabel2.TabIndex = 17
        Me.RadLabel2.Text = "DIRECCIÓN:"
        '
        'txtnombre
        '
        Me.txtnombre.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtnombre.Location = New System.Drawing.Point(85, 70)
        Me.txtnombre.Name = "txtnombre"
        '
        '
        '
        Me.txtnombre.RootElement.ControlBounds = New System.Drawing.Rectangle(85, 70, 100, 20)
        Me.txtnombre.RootElement.StretchVertically = True
        Me.txtnombre.Size = New System.Drawing.Size(246, 20)
        Me.txtnombre.TabIndex = 1
        Me.txtnombre.TabStop = False
        Me.txtnombre.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'RadLabel1
        '
        Me.RadLabel1.AutoSize = True
        Me.RadLabel1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.RadLabel1.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(33, 74)
        Me.RadLabel1.Name = "RadLabel1"
        '
        '
        '
        Me.RadLabel1.RootElement.ControlBounds = New System.Drawing.Rectangle(33, 74, 100, 18)
        Me.RadLabel1.Size = New System.Drawing.Size(48, 15)
        Me.RadLabel1.TabIndex = 16
        Me.RadLabel1.Text = "NOMBRE:"
        '
        'frmBuscaruser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1036, 428)
        Me.Controls.Add(Me.RadGroupBox3)
        Me.Controls.Add(Me.DTGbusqueda)
        Me.Controls.Add(Me.RadGroupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmBuscaruser"
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
        CType(Me.DTGbusqueda, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.txtUbicacion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcuentaAnterior, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcolonia, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcuenta, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdireccion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCuenta, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtnombre, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btncancelar As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnaceptar As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents DTGbusqueda As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtcolonia As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents txtcuenta As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents txtdireccion As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents RadLabel3 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents lblCuenta As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel2 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txtnombre As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents RadLabel1 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txtcuentaAnterior As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents RadLabel4 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txtUbicacion As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents RadLabel5 As Telerik.WinControls.UI.RadLabel
End Class


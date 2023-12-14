<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDescuentos
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
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkrecargo = New System.Windows.Forms.CheckBox()
        Me.chksaneamiento = New System.Windows.Forms.CheckBox()
        Me.chkalcantarillado = New System.Windows.Forms.CheckBox()
        Me.chkconsumo = New System.Windows.Forms.CheckBox()
        Me.diporcrecargo = New DevComponents.Editors.DoubleInput()
        Me.IImesesderecargo = New DevComponents.Editors.IntegerInput()
        Me.RadLabel6 = New Telerik.WinControls.UI.RadLabel()
        Me.diporcsanemiento = New DevComponents.Editors.DoubleInput()
        Me.diporcalcantarillado = New DevComponents.Editors.DoubleInput()
        Me.IImesesdescsaneamiento = New DevComponents.Editors.IntegerInput()
        Me.IImesesdescalcanta = New DevComponents.Editors.IntegerInput()
        Me.diporcconsumo = New DevComponents.Editors.DoubleInput()
        Me.IImesesdecconsumo = New DevComponents.Editors.IntegerInput()
        Me.RadLabel5 = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel4 = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel3 = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel2 = New Telerik.WinControls.UI.RadLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.btncancelar = New Telerik.WinControls.UI.RadButton()
        Me.btnaceptar = New Telerik.WinControls.UI.RadButton()
        Me.chkrezago = New System.Windows.Forms.CheckBox()
        Me.RadLabel1 = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel7 = New Telerik.WinControls.UI.RadLabel()
        Me.IImesesrezago = New DevComponents.Editors.IntegerInput()
        Me.DiporcRezago = New DevComponents.Editors.DoubleInput()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.diporcrecargo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IImesesderecargo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.diporcsanemiento, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.diporcalcantarillado, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IImesesdescsaneamiento, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IImesesdescalcanta, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.diporcconsumo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IImesesdecconsumo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.btncancelar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnaceptar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IImesesrezago, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DiporcRezago, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.DiporcRezago)
        Me.RadGroupBox1.Controls.Add(Me.IImesesrezago)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel7)
        Me.RadGroupBox1.Controls.Add(Me.chkrezago)
        Me.RadGroupBox1.Controls.Add(Me.chkrecargo)
        Me.RadGroupBox1.Controls.Add(Me.chksaneamiento)
        Me.RadGroupBox1.Controls.Add(Me.chkalcantarillado)
        Me.RadGroupBox1.Controls.Add(Me.chkconsumo)
        Me.RadGroupBox1.Controls.Add(Me.diporcrecargo)
        Me.RadGroupBox1.Controls.Add(Me.IImesesderecargo)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel6)
        Me.RadGroupBox1.Controls.Add(Me.diporcsanemiento)
        Me.RadGroupBox1.Controls.Add(Me.diporcalcantarillado)
        Me.RadGroupBox1.Controls.Add(Me.IImesesdescsaneamiento)
        Me.RadGroupBox1.Controls.Add(Me.IImesesdescalcanta)
        Me.RadGroupBox1.Controls.Add(Me.diporcconsumo)
        Me.RadGroupBox1.Controls.Add(Me.IImesesdecconsumo)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel5)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel4)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office
        Me.RadGroupBox1.HeaderText = "DESCUENTOS"
        Me.RadGroupBox1.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadGroupBox1.Location = New System.Drawing.Point(21, 16)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        '
        '
        '
        Me.RadGroupBox1.RootElement.Padding = New System.Windows.Forms.Padding(2, 18, 2, 2)
        Me.RadGroupBox1.Size = New System.Drawing.Size(503, 262)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = "DESCUENTOS"
        '
        'chkrecargo
        '
        Me.chkrecargo.AutoSize = True
        Me.chkrecargo.Location = New System.Drawing.Point(11, 213)
        Me.chkrecargo.Name = "chkrecargo"
        Me.chkrecargo.Size = New System.Drawing.Size(124, 17)
        Me.chkrecargo.TabIndex = 12
        Me.chkrecargo.Text = "Todos los periodos"
        Me.chkrecargo.UseVisualStyleBackColor = True
        '
        'chksaneamiento
        '
        Me.chksaneamiento.AutoSize = True
        Me.chksaneamiento.Location = New System.Drawing.Point(11, 176)
        Me.chksaneamiento.Name = "chksaneamiento"
        Me.chksaneamiento.Size = New System.Drawing.Size(124, 17)
        Me.chksaneamiento.TabIndex = 9
        Me.chksaneamiento.Text = "Todos los periodos"
        Me.chksaneamiento.UseVisualStyleBackColor = True
        '
        'chkalcantarillado
        '
        Me.chkalcantarillado.AutoSize = True
        Me.chkalcantarillado.Location = New System.Drawing.Point(11, 137)
        Me.chkalcantarillado.Name = "chkalcantarillado"
        Me.chkalcantarillado.Size = New System.Drawing.Size(124, 17)
        Me.chkalcantarillado.TabIndex = 6
        Me.chkalcantarillado.Text = "Todos los periodos"
        Me.chkalcantarillado.UseVisualStyleBackColor = True
        '
        'chkconsumo
        '
        Me.chkconsumo.AutoSize = True
        Me.chkconsumo.Location = New System.Drawing.Point(11, 63)
        Me.chkconsumo.Name = "chkconsumo"
        Me.chkconsumo.Size = New System.Drawing.Size(124, 17)
        Me.chkconsumo.TabIndex = 0
        Me.chkconsumo.Text = "Todos los periodos"
        Me.chkconsumo.UseVisualStyleBackColor = True
        '
        'diporcrecargo
        '
        '
        '
        '
        Me.diporcrecargo.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.diporcrecargo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.diporcrecargo.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.diporcrecargo.Increment = 1.0R
        Me.diporcrecargo.Location = New System.Drawing.Point(414, 210)
        Me.diporcrecargo.MaxValue = 100.0R
        Me.diporcrecargo.Name = "diporcrecargo"
        Me.diporcrecargo.ShowUpDown = True
        Me.diporcrecargo.Size = New System.Drawing.Size(64, 22)
        Me.diporcrecargo.TabIndex = 14
        '
        'IImesesderecargo
        '
        '
        '
        '
        Me.IImesesderecargo.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.IImesesderecargo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.IImesesderecargo.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.IImesesderecargo.Location = New System.Drawing.Point(277, 208)
        Me.IImesesderecargo.MaxValue = 999
        Me.IImesesderecargo.Name = "IImesesderecargo"
        Me.IImesesderecargo.ShowUpDown = True
        Me.IImesesderecargo.Size = New System.Drawing.Size(97, 22)
        Me.IImesesderecargo.TabIndex = 13
        '
        'RadLabel6
        '
        Me.RadLabel6.AutoSize = True
        Me.RadLabel6.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(208, 214)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(62, 18)
        Me.RadLabel6.TabIndex = 43
        Me.RadLabel6.Text = "RECARGO:"
        '
        'diporcsanemiento
        '
        '
        '
        '
        Me.diporcsanemiento.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.diporcsanemiento.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.diporcsanemiento.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.diporcsanemiento.Increment = 1.0R
        Me.diporcsanemiento.Location = New System.Drawing.Point(414, 171)
        Me.diporcsanemiento.MaxValue = 100.0R
        Me.diporcsanemiento.Name = "diporcsanemiento"
        Me.diporcsanemiento.ShowUpDown = True
        Me.diporcsanemiento.Size = New System.Drawing.Size(64, 22)
        Me.diporcsanemiento.TabIndex = 11
        '
        'diporcalcantarillado
        '
        '
        '
        '
        Me.diporcalcantarillado.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.diporcalcantarillado.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.diporcalcantarillado.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.diporcalcantarillado.Increment = 1.0R
        Me.diporcalcantarillado.Location = New System.Drawing.Point(414, 132)
        Me.diporcalcantarillado.MaxValue = 100.0R
        Me.diporcalcantarillado.Name = "diporcalcantarillado"
        Me.diporcalcantarillado.ShowUpDown = True
        Me.diporcalcantarillado.Size = New System.Drawing.Size(64, 22)
        Me.diporcalcantarillado.TabIndex = 8
        '
        'IImesesdescsaneamiento
        '
        '
        '
        '
        Me.IImesesdescsaneamiento.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.IImesesdescsaneamiento.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.IImesesdescsaneamiento.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.IImesesdescsaneamiento.Location = New System.Drawing.Point(277, 170)
        Me.IImesesdescsaneamiento.MaxValue = 999
        Me.IImesesdescsaneamiento.Name = "IImesesdescsaneamiento"
        Me.IImesesdescsaneamiento.ShowUpDown = True
        Me.IImesesdescsaneamiento.Size = New System.Drawing.Size(97, 22)
        Me.IImesesdescsaneamiento.TabIndex = 10
        '
        'IImesesdescalcanta
        '
        '
        '
        '
        Me.IImesesdescalcanta.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.IImesesdescalcanta.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.IImesesdescalcanta.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.IImesesdescalcanta.Location = New System.Drawing.Point(277, 132)
        Me.IImesesdescalcanta.MaxValue = 999
        Me.IImesesdescalcanta.Name = "IImesesdescalcanta"
        Me.IImesesdescalcanta.ShowUpDown = True
        Me.IImesesdescalcanta.Size = New System.Drawing.Size(97, 22)
        Me.IImesesdescalcanta.TabIndex = 7
        '
        'diporcconsumo
        '
        '
        '
        '
        Me.diporcconsumo.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.diporcconsumo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.diporcconsumo.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.diporcconsumo.Increment = 1.0R
        Me.diporcconsumo.Location = New System.Drawing.Point(414, 63)
        Me.diporcconsumo.MaxValue = 100.0R
        Me.diporcconsumo.Name = "diporcconsumo"
        Me.diporcconsumo.ShowUpDown = True
        Me.diporcconsumo.Size = New System.Drawing.Size(64, 22)
        Me.diporcconsumo.TabIndex = 2
        '
        'IImesesdecconsumo
        '
        '
        '
        '
        Me.IImesesdecconsumo.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.IImesesdecconsumo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.IImesesdecconsumo.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.IImesesdecconsumo.Location = New System.Drawing.Point(277, 64)
        Me.IImesesdecconsumo.MaxValue = 999
        Me.IImesesdecconsumo.Name = "IImesesdecconsumo"
        Me.IImesesdecconsumo.ShowUpDown = True
        Me.IImesesdecconsumo.Size = New System.Drawing.Size(97, 22)
        Me.IImesesdecconsumo.TabIndex = 1
        '
        'RadLabel5
        '
        Me.RadLabel5.AutoSize = True
        Me.RadLabel5.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(436, 36)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(22, 25)
        Me.RadLabel5.TabIndex = 31
        Me.RadLabel5.Text = "%"
        '
        'RadLabel4
        '
        Me.RadLabel4.AutoSize = True
        Me.RadLabel4.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(285, 39)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(72, 21)
        Me.RadLabel4.TabIndex = 30
        Me.RadLabel4.Text = "No. Meses"
        '
        'RadLabel3
        '
        Me.RadLabel3.AutoSize = True
        Me.RadLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(179, 175)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(91, 18)
        Me.RadLabel3.TabIndex = 21
        Me.RadLabel3.Text = "SANEAMIENTO:"
        '
        'RadLabel2
        '
        Me.RadLabel2.AutoSize = True
        Me.RadLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(161, 136)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(109, 18)
        Me.RadLabel2.TabIndex = 20
        Me.RadLabel2.Text = "ALCANTARILLADO:"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.btncancelar)
        Me.RadGroupBox2.Controls.Add(Me.btnaceptar)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office
        Me.RadGroupBox2.HeaderText = "ACCIONES"
        Me.RadGroupBox2.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadGroupBox2.Location = New System.Drawing.Point(21, 284)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        '
        '
        '
        Me.RadGroupBox2.RootElement.Padding = New System.Windows.Forms.Padding(2, 18, 2, 2)
        Me.RadGroupBox2.Size = New System.Drawing.Size(503, 78)
        Me.RadGroupBox2.TabIndex = 1
        Me.RadGroupBox2.Text = "ACCIONES"
        '
        'btncancelar
        '
        Me.btncancelar.Image = Global.CAJAS.My.Resources.Resources.INI
        Me.btncancelar.ImageAlignment = System.Drawing.ContentAlignment.TopCenter
        Me.btncancelar.Location = New System.Drawing.Point(381, 26)
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
        Me.btnaceptar.Location = New System.Drawing.Point(11, 21)
        Me.btnaceptar.Name = "btnaceptar"
        Me.btnaceptar.Size = New System.Drawing.Size(97, 47)
        Me.btnaceptar.TabIndex = 0
        Me.btnaceptar.Text = "ACEPTAR"
        Me.btnaceptar.TextAlignment = System.Drawing.ContentAlignment.BottomCenter
        '
        'chkrezago
        '
        Me.chkrezago.AutoSize = True
        Me.chkrezago.Location = New System.Drawing.Point(11, 96)
        Me.chkrezago.Name = "chkrezago"
        Me.chkrezago.Size = New System.Drawing.Size(124, 17)
        Me.chkrezago.TabIndex = 3
        Me.chkrezago.Text = "Todos los periodos"
        Me.chkrezago.UseVisualStyleBackColor = True
        '
        'RadLabel1
        '
        Me.RadLabel1.AutoSize = True
        Me.RadLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(202, 67)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(68, 18)
        Me.RadLabel1.TabIndex = 19
        Me.RadLabel1.Text = "CONSUMO:"
        '
        'RadLabel7
        '
        Me.RadLabel7.AutoSize = True
        Me.RadLabel7.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel7.Location = New System.Drawing.Point(202, 96)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(54, 18)
        Me.RadLabel7.TabIndex = 45
        Me.RadLabel7.Text = "REZAGO:"
        '
        'IImesesrezago
        '
        '
        '
        '
        Me.IImesesrezago.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.IImesesrezago.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.IImesesrezago.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.IImesesrezago.Location = New System.Drawing.Point(277, 96)
        Me.IImesesrezago.MaxValue = 999
        Me.IImesesrezago.Name = "IImesesrezago"
        Me.IImesesrezago.ShowUpDown = True
        Me.IImesesrezago.Size = New System.Drawing.Size(97, 22)
        Me.IImesesrezago.TabIndex = 4
        '
        'DiporcRezago
        '
        '
        '
        '
        Me.DiporcRezago.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.DiporcRezago.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.DiporcRezago.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.DiporcRezago.Increment = 1.0R
        Me.DiporcRezago.Location = New System.Drawing.Point(414, 96)
        Me.DiporcRezago.MaxValue = 100.0R
        Me.DiporcRezago.Name = "DiporcRezago"
        Me.DiporcRezago.ShowUpDown = True
        Me.DiporcRezago.Size = New System.Drawing.Size(64, 22)
        Me.DiporcRezago.TabIndex = 5
        '
        'FrmDescuentos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(550, 374)
        Me.Controls.Add(Me.RadGroupBox2)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "FrmDescuentos"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = ""
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.diporcrecargo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IImesesderecargo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.diporcsanemiento, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.diporcalcantarillado, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IImesesdescsaneamiento, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IImesesdescalcanta, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.diporcconsumo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IImesesdecconsumo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.btncancelar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnaceptar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IImesesrezago, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DiporcRezago, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadLabel2 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel3 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel5 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel4 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btncancelar As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnaceptar As Telerik.WinControls.UI.RadButton
    Friend WithEvents diporcconsumo As DevComponents.Editors.DoubleInput
    Friend WithEvents IImesesdecconsumo As DevComponents.Editors.IntegerInput
    Friend WithEvents diporcsanemiento As DevComponents.Editors.DoubleInput
    Friend WithEvents diporcalcantarillado As DevComponents.Editors.DoubleInput
    Friend WithEvents IImesesdescsaneamiento As DevComponents.Editors.IntegerInput
    Friend WithEvents IImesesdescalcanta As DevComponents.Editors.IntegerInput
    Friend WithEvents diporcrecargo As DevComponents.Editors.DoubleInput
    Friend WithEvents IImesesderecargo As DevComponents.Editors.IntegerInput
    Friend WithEvents RadLabel6 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents chkrecargo As System.Windows.Forms.CheckBox
    Friend WithEvents chksaneamiento As System.Windows.Forms.CheckBox
    Friend WithEvents chkalcantarillado As System.Windows.Forms.CheckBox
    Friend WithEvents chkconsumo As System.Windows.Forms.CheckBox
    Friend WithEvents DiporcRezago As DevComponents.Editors.DoubleInput
    Friend WithEvents IImesesrezago As DevComponents.Editors.IntegerInput
    Friend WithEvents RadLabel7 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents chkrezago As System.Windows.Forms.CheckBox
    Friend WithEvents RadLabel1 As Telerik.WinControls.UI.RadLabel
End Class


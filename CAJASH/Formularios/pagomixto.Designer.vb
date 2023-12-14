<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class pagomixto
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.btnsalir = New Telerik.WinControls.UI.RadButton()
        Me.btningresar = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.IIcheque = New System.Windows.Forms.TextBox()
        Me.iitarjetadedebito = New System.Windows.Forms.TextBox()
        Me.iitarjetadecredito = New System.Windows.Forms.TextBox()
        Me.iitransferencia = New System.Windows.Forms.TextBox()
        Me.iiefectivo = New System.Windows.Forms.TextBox()
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX()
        Me.txtobservacioncheque = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txttrjetadebito = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txttarjetacredito = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtobservaciontransferencia = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtobservacionefectivo = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbltotalapagar = New System.Windows.Forms.Label()
        Me.lblsuma = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.btnsalir, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btningresar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.RadGroupBox3.Controls.Add(Me.btnsalir)
        Me.RadGroupBox3.Controls.Add(Me.btningresar)
        Me.RadGroupBox3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox3.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office
        Me.RadGroupBox3.HeaderText = "ACCIONES"
        Me.RadGroupBox3.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadGroupBox3.Location = New System.Drawing.Point(335, 269)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        '
        '
        '
        Me.RadGroupBox3.RootElement.ControlBounds = New System.Drawing.Rectangle(335, 269, 200, 100)
        Me.RadGroupBox3.RootElement.Padding = New System.Windows.Forms.Padding(2, 18, 2, 2)
        Me.RadGroupBox3.Size = New System.Drawing.Size(477, 76)
        Me.RadGroupBox3.TabIndex = 13
        Me.RadGroupBox3.Text = "ACCIONES"
        '
        'btnsalir
        '
        Me.btnsalir.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.btnsalir.Enabled = False
        Me.btnsalir.Image = Global.CAJAS.My.Resources.Resources.IcoSalir
        Me.btnsalir.ImageAlignment = System.Drawing.ContentAlignment.TopCenter
        Me.btnsalir.Location = New System.Drawing.Point(390, 21)
        Me.btnsalir.Name = "btnsalir"
        '
        '
        '
        Me.btnsalir.RootElement.ControlBounds = New System.Drawing.Rectangle(390, 21, 110, 24)
        Me.btnsalir.Size = New System.Drawing.Size(72, 47)
        Me.btnsalir.TabIndex = 1
        Me.btnsalir.Text = "SALIR"
        Me.btnsalir.TextAlignment = System.Drawing.ContentAlignment.BottomCenter
        '
        'btningresar
        '
        Me.btningresar.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.btningresar.Image = Global.CAJAS.My.Resources.Resources.IcoAgregar
        Me.btningresar.ImageAlignment = System.Drawing.ContentAlignment.TopCenter
        Me.btningresar.Location = New System.Drawing.Point(18, 21)
        Me.btningresar.Name = "btningresar"
        '
        '
        '
        Me.btningresar.RootElement.ControlBounds = New System.Drawing.Rectangle(18, 21, 110, 24)
        Me.btningresar.Size = New System.Drawing.Size(72, 47)
        Me.btningresar.TabIndex = 0
        Me.btningresar.Text = "Aceptar"
        Me.btningresar.TextAlignment = System.Drawing.ContentAlignment.BottomCenter
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.RadGroupBox1.Controls.Add(Me.IIcheque)
        Me.RadGroupBox1.Controls.Add(Me.iitarjetadedebito)
        Me.RadGroupBox1.Controls.Add(Me.iitarjetadecredito)
        Me.RadGroupBox1.Controls.Add(Me.iitransferencia)
        Me.RadGroupBox1.Controls.Add(Me.iiefectivo)
        Me.RadGroupBox1.Controls.Add(Me.LabelX6)
        Me.RadGroupBox1.Controls.Add(Me.txtobservacioncheque)
        Me.RadGroupBox1.Controls.Add(Me.txttrjetadebito)
        Me.RadGroupBox1.Controls.Add(Me.txttarjetacredito)
        Me.RadGroupBox1.Controls.Add(Me.txtobservaciontransferencia)
        Me.RadGroupBox1.Controls.Add(Me.txtobservacionefectivo)
        Me.RadGroupBox1.Controls.Add(Me.LabelX5)
        Me.RadGroupBox1.Controls.Add(Me.LabelX4)
        Me.RadGroupBox1.Controls.Add(Me.LabelX3)
        Me.RadGroupBox1.Controls.Add(Me.LabelX2)
        Me.RadGroupBox1.Controls.Add(Me.LabelX1)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office
        Me.RadGroupBox1.HeaderAlignment = Telerik.WinControls.UI.HeaderAlignment.Center
        Me.RadGroupBox1.HeaderText = "FORMA DE PAGO"
        Me.RadGroupBox1.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadGroupBox1.Location = New System.Drawing.Point(12, 29)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        '
        '
        '
        Me.RadGroupBox1.RootElement.ControlBounds = New System.Drawing.Rectangle(12, 29, 200, 100)
        Me.RadGroupBox1.RootElement.Padding = New System.Windows.Forms.Padding(2, 18, 2, 2)
        Me.RadGroupBox1.Size = New System.Drawing.Size(819, 211)
        Me.RadGroupBox1.TabIndex = 12
        Me.RadGroupBox1.Text = "FORMA DE PAGO"
        '
        'IIcheque
        '
        Me.IIcheque.Location = New System.Drawing.Point(381, 88)
        Me.IIcheque.Name = "IIcheque"
        Me.IIcheque.Size = New System.Drawing.Size(100, 19)
        Me.IIcheque.TabIndex = 46
        '
        'iitarjetadedebito
        '
        Me.iitarjetadedebito.Location = New System.Drawing.Point(381, 168)
        Me.iitarjetadedebito.Name = "iitarjetadedebito"
        Me.iitarjetadedebito.Size = New System.Drawing.Size(100, 19)
        Me.iitarjetadedebito.TabIndex = 52
        '
        'iitarjetadecredito
        '
        Me.iitarjetadecredito.Location = New System.Drawing.Point(381, 142)
        Me.iitarjetadecredito.Name = "iitarjetadecredito"
        Me.iitarjetadecredito.Size = New System.Drawing.Size(100, 19)
        Me.iitarjetadecredito.TabIndex = 50
        '
        'iitransferencia
        '
        Me.iitransferencia.Location = New System.Drawing.Point(381, 117)
        Me.iitransferencia.Name = "iitransferencia"
        Me.iitransferencia.Size = New System.Drawing.Size(100, 19)
        Me.iitransferencia.TabIndex = 48
        '
        'iiefectivo
        '
        Me.iiefectivo.Location = New System.Drawing.Point(381, 63)
        Me.iiefectivo.Name = "iiefectivo"
        Me.iiefectivo.Size = New System.Drawing.Size(100, 19)
        Me.iiefectivo.TabIndex = 44
        '
        'LabelX6
        '
        '
        '
        '
        Me.LabelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX6.Location = New System.Drawing.Point(511, 21)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.Size = New System.Drawing.Size(289, 23)
        Me.LabelX6.TabIndex = 43
        Me.LabelX6.Text = "Observacion"
        '
        'txtobservacioncheque
        '
        '
        '
        '
        Me.txtobservacioncheque.Border.Class = "TextBoxBorder"
        Me.txtobservacioncheque.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtobservacioncheque.Location = New System.Drawing.Point(511, 89)
        Me.txtobservacioncheque.Name = "txtobservacioncheque"
        Me.txtobservacioncheque.PreventEnterBeep = True
        Me.txtobservacioncheque.Size = New System.Drawing.Size(289, 19)
        Me.txtobservacioncheque.TabIndex = 47
        '
        'txttrjetadebito
        '
        '
        '
        '
        Me.txttrjetadebito.Border.Class = "TextBoxBorder"
        Me.txttrjetadebito.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txttrjetadebito.Location = New System.Drawing.Point(511, 169)
        Me.txttrjetadebito.Name = "txttrjetadebito"
        Me.txttrjetadebito.PreventEnterBeep = True
        Me.txttrjetadebito.Size = New System.Drawing.Size(289, 19)
        Me.txttrjetadebito.TabIndex = 53
        '
        'txttarjetacredito
        '
        '
        '
        '
        Me.txttarjetacredito.Border.Class = "TextBoxBorder"
        Me.txttarjetacredito.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txttarjetacredito.Location = New System.Drawing.Point(511, 143)
        Me.txttarjetacredito.Name = "txttarjetacredito"
        Me.txttarjetacredito.PreventEnterBeep = True
        Me.txttarjetacredito.Size = New System.Drawing.Size(289, 19)
        Me.txttarjetacredito.TabIndex = 51
        '
        'txtobservaciontransferencia
        '
        '
        '
        '
        Me.txtobservaciontransferencia.Border.Class = "TextBoxBorder"
        Me.txtobservaciontransferencia.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtobservaciontransferencia.Location = New System.Drawing.Point(511, 117)
        Me.txtobservaciontransferencia.Name = "txtobservaciontransferencia"
        Me.txtobservaciontransferencia.PreventEnterBeep = True
        Me.txtobservaciontransferencia.Size = New System.Drawing.Size(289, 19)
        Me.txtobservaciontransferencia.TabIndex = 49
        '
        'txtobservacionefectivo
        '
        '
        '
        '
        Me.txtobservacionefectivo.Border.Class = "TextBoxBorder"
        Me.txtobservacionefectivo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtobservacionefectivo.Location = New System.Drawing.Point(511, 64)
        Me.txtobservacionefectivo.Name = "txtobservacionefectivo"
        Me.txtobservacionefectivo.PreventEnterBeep = True
        Me.txtobservacionefectivo.Size = New System.Drawing.Size(289, 19)
        Me.txtobservacionefectivo.TabIndex = 45
        '
        'LabelX5
        '
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Location = New System.Drawing.Point(12, 89)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.Size = New System.Drawing.Size(334, 23)
        Me.LabelX5.TabIndex = 32
        Me.LabelX5.Text = "02 CHEQUE ......................................................................." &
    "............................................"
        '
        'LabelX4
        '
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Location = New System.Drawing.Point(12, 172)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.Size = New System.Drawing.Size(334, 23)
        Me.LabelX4.TabIndex = 27
        Me.LabelX4.Text = "28 TARJETA DE DEBITO ............................................................" &
    "......................................................."
        '
        'LabelX3
        '
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Location = New System.Drawing.Point(12, 143)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.Size = New System.Drawing.Size(334, 23)
        Me.LabelX3.TabIndex = 26
        Me.LabelX3.Text = "04 TARJETA DE CREDITO ..........................................................." &
    "........................................................"
        '
        'LabelX2
        '
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Location = New System.Drawing.Point(12, 114)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(334, 23)
        Me.LabelX2.TabIndex = 25
        Me.LabelX2.Text = "03 TRANSFERENCIA ................................................................" &
    "..................................................."
        '
        'LabelX1
        '
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Location = New System.Drawing.Point(14, 63)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(334, 23)
        Me.LabelX1.TabIndex = 24
        Me.LabelX1.Text = "01 EFECTIVO ....................................................................." &
    ".............................................."
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 243)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 13)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Total a pagar"
        '
        'lbltotalapagar
        '
        Me.lbltotalapagar.AutoSize = True
        Me.lbltotalapagar.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltotalapagar.Location = New System.Drawing.Point(134, 243)
        Me.lbltotalapagar.Name = "lbltotalapagar"
        Me.lbltotalapagar.Size = New System.Drawing.Size(16, 17)
        Me.lbltotalapagar.TabIndex = 15
        Me.lbltotalapagar.Text = "0"
        '
        'lblsuma
        '
        Me.lblsuma.AutoSize = True
        Me.lblsuma.Location = New System.Drawing.Point(393, 242)
        Me.lblsuma.Name = "lblsuma"
        Me.lblsuma.Size = New System.Drawing.Size(13, 13)
        Me.lblsuma.TabIndex = 16
        Me.lblsuma.Text = "0"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(26, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(244, 13)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Escribe las cantidades sin como y sin simbolo de $"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Red
        Me.PictureBox1.Location = New System.Drawing.Point(477, 241)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(71, 14)
        Me.PictureBox1.TabIndex = 18
        Me.PictureBox1.TabStop = False
        '
        'pagomixto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.PaleGoldenrod
        Me.ClientSize = New System.Drawing.Size(856, 377)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblsuma)
        Me.Controls.Add(Me.lbltotalapagar)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.RadGroupBox3)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Name = "pagomixto"
        Me.Text = "Pago Mixto"
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        CType(Me.btnsalir, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btningresar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnsalir As Telerik.WinControls.UI.RadButton
    Friend WithEvents btningresar As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Public WithEvents IIcheque As TextBox
    Public WithEvents iitarjetadedebito As TextBox
    Public WithEvents iitarjetadecredito As TextBox
    Public WithEvents iitransferencia As TextBox
    Public WithEvents iiefectivo As TextBox
    Public WithEvents txtobservacioncheque As DevComponents.DotNetBar.Controls.TextBoxX
    Public WithEvents txttrjetadebito As DevComponents.DotNetBar.Controls.TextBoxX
    Public WithEvents txttarjetacredito As DevComponents.DotNetBar.Controls.TextBoxX
    Public WithEvents txtobservaciontransferencia As DevComponents.DotNetBar.Controls.TextBoxX
    Public WithEvents txtobservacionefectivo As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents Label1 As Label
    Friend WithEvents lbltotalapagar As Label
    Friend WithEvents lblsuma As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents PictureBox1 As PictureBox
End Class

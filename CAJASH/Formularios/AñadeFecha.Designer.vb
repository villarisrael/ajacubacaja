<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AñadeFecha
    Inherits System.Windows.Forms.Form

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
        Me.btnaceptar = New Telerik.WinControls.UI.RadButton()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.fecha = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        CType(Me.btnaceptar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fecha, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnaceptar
        '
        Me.btnaceptar.Image = Global.CAJAS.My.Resources.Resources.IcoAgregar
        Me.btnaceptar.ImageAlignment = System.Drawing.ContentAlignment.TopCenter
        Me.btnaceptar.Location = New System.Drawing.Point(151, 61)
        Me.btnaceptar.Name = "btnaceptar"
        Me.btnaceptar.Size = New System.Drawing.Size(89, 49)
        Me.btnaceptar.TabIndex = 2
        Me.btnaceptar.Text = "ACEPTAR"
        Me.btnaceptar.TextAlignment = System.Drawing.ContentAlignment.BottomCenter
        '
        'LabelX1
        '
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Location = New System.Drawing.Point(13, 13)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(210, 23)
        Me.LabelX1.TabIndex = 3
        Me.LabelX1.Text = "Añade fecha, ejemplo: XXXX-X-XX"
        '
        'fecha
        '
        '
        '
        '
        Me.fecha.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.fecha.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.fecha.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown
        Me.fecha.ButtonDropDown.Visible = True
        Me.fecha.IsPopupCalendarOpen = False
        Me.fecha.Location = New System.Drawing.Point(12, 35)
        '
        '
        '
        '
        '
        '
        Me.fecha.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.fecha.MonthCalendar.CalendarDimensions = New System.Drawing.Size(1, 1)
        Me.fecha.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.fecha.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.fecha.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.fecha.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.fecha.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.fecha.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.fecha.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.fecha.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.fecha.MonthCalendar.DisplayMonth = New Date(2019, 6, 1, 0, 0, 0, 0)
        '
        '
        '
        Me.fecha.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.fecha.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.fecha.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.fecha.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.fecha.MonthCalendar.TodayButtonVisible = True
        Me.fecha.Name = "fecha"
        Me.fecha.Size = New System.Drawing.Size(200, 20)
        Me.fecha.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.fecha.TabIndex = 4
        '
        'AñadeFecha
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(252, 121)
        Me.Controls.Add(Me.fecha)
        Me.Controls.Add(Me.LabelX1)
        Me.Controls.Add(Me.btnaceptar)
        Me.Name = "AñadeFecha"
        Me.Text = "Fecha"
        CType(Me.btnaceptar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fecha, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnaceptar As Telerik.WinControls.UI.RadButton
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents fecha As DevComponents.Editors.DateTimeAdv.DateTimeInput
End Class

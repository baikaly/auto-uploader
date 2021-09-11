<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Main
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
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

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Main))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Btn_SelFolder = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TB_ProgramName = New System.Windows.Forms.TextBox()
        Me.TB_Username = New System.Windows.Forms.TextBox()
        Me.TB_Password = New System.Windows.Forms.TextBox()
        Me.TB_UpdateDate = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TB_Description = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Btn_Upload = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TB_FolderPath = New System.Windows.Forms.TextBox()
        Me.Btn_SelProgram = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.CB_DeleteLocal = New System.Windows.Forms.CheckBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TB_SeverPath = New System.Windows.Forms.TextBox()
        Me.CB_SeverIP = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TB_MainVersion = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.LV_Filelist = New Uploader.myListViewNF()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 24)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 17)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "选择主程序"
        '
        'Btn_SelFolder
        '
        Me.Btn_SelFolder.Font = New System.Drawing.Font("Microsoft YaHei", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Btn_SelFolder.Location = New System.Drawing.Point(662, 44)
        Me.Btn_SelFolder.Margin = New System.Windows.Forms.Padding(2)
        Me.Btn_SelFolder.Name = "Btn_SelFolder"
        Me.Btn_SelFolder.Size = New System.Drawing.Size(22, 22)
        Me.Btn_SelFolder.TabIndex = 2
        Me.Btn_SelFolder.Text = "..."
        Me.Btn_SelFolder.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(5, 70)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 17)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "服务器IP"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(5, 112)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 17)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "用户名"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(5, 134)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(36, 17)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "密码"
        '
        'TB_ProgramName
        '
        Me.TB_ProgramName.Location = New System.Drawing.Point(88, 22)
        Me.TB_ProgramName.Margin = New System.Windows.Forms.Padding(2)
        Me.TB_ProgramName.Name = "TB_ProgramName"
        Me.TB_ProgramName.Size = New System.Drawing.Size(567, 22)
        Me.TB_ProgramName.TabIndex = 9
        '
        'TB_Username
        '
        Me.TB_Username.Location = New System.Drawing.Point(88, 111)
        Me.TB_Username.Margin = New System.Windows.Forms.Padding(2)
        Me.TB_Username.Name = "TB_Username"
        Me.TB_Username.Size = New System.Drawing.Size(114, 22)
        Me.TB_Username.TabIndex = 11
        '
        'TB_Password
        '
        Me.TB_Password.Location = New System.Drawing.Point(88, 134)
        Me.TB_Password.Margin = New System.Windows.Forms.Padding(2)
        Me.TB_Password.Name = "TB_Password"
        Me.TB_Password.Size = New System.Drawing.Size(114, 22)
        Me.TB_Password.TabIndex = 12
        '
        'TB_UpdateDate
        '
        Me.TB_UpdateDate.Location = New System.Drawing.Point(88, 157)
        Me.TB_UpdateDate.Margin = New System.Windows.Forms.Padding(2)
        Me.TB_UpdateDate.Name = "TB_UpdateDate"
        Me.TB_UpdateDate.Size = New System.Drawing.Size(114, 22)
        Me.TB_UpdateDate.TabIndex = 14
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(5, 158)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 17)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "更新日期"
        '
        'TB_Description
        '
        Me.TB_Description.Location = New System.Drawing.Point(88, 180)
        Me.TB_Description.Margin = New System.Windows.Forms.Padding(2)
        Me.TB_Description.Multiline = True
        Me.TB_Description.Name = "TB_Description"
        Me.TB_Description.Size = New System.Drawing.Size(567, 73)
        Me.TB_Description.TabIndex = 16
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(5, 180)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(64, 17)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "更新描述"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(300, 489)
        Me.Button2.Margin = New System.Windows.Forms.Padding(2)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(72, 33)
        Me.Button2.TabIndex = 17
        Me.Button2.Text = "生成XML"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Btn_Upload
        '
        Me.Btn_Upload.Location = New System.Drawing.Point(460, 489)
        Me.Btn_Upload.Margin = New System.Windows.Forms.Padding(2)
        Me.Btn_Upload.Name = "Btn_Upload"
        Me.Btn_Upload.Size = New System.Drawing.Size(77, 33)
        Me.Btn_Upload.TabIndex = 19
        Me.Btn_Upload.Text = "上传"
        Me.Btn_Upload.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(5, 46)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(64, 17)
        Me.Label8.TabIndex = 22
        Me.Label8.Text = "选择目录"
        '
        'TB_FolderPath
        '
        Me.TB_FolderPath.Location = New System.Drawing.Point(88, 44)
        Me.TB_FolderPath.Margin = New System.Windows.Forms.Padding(2)
        Me.TB_FolderPath.Name = "TB_FolderPath"
        Me.TB_FolderPath.Size = New System.Drawing.Size(567, 22)
        Me.TB_FolderPath.TabIndex = 23
        '
        'Btn_SelProgram
        '
        Me.Btn_SelProgram.Font = New System.Drawing.Font("Microsoft YaHei", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Btn_SelProgram.Location = New System.Drawing.Point(662, 21)
        Me.Btn_SelProgram.Margin = New System.Windows.Forms.Padding(2)
        Me.Btn_SelProgram.Name = "Btn_SelProgram"
        Me.Btn_SelProgram.Size = New System.Drawing.Size(22, 21)
        Me.Btn_SelProgram.TabIndex = 24
        Me.Btn_SelProgram.Text = "..."
        Me.Btn_SelProgram.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CB_DeleteLocal)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.TB_SeverPath)
        Me.GroupBox1.Controls.Add(Me.CB_SeverIP)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.TB_MainVersion)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Btn_SelProgram)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.TB_FolderPath)
        Me.GroupBox1.Controls.Add(Me.Btn_SelFolder)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.TB_ProgramName)
        Me.GroupBox1.Controls.Add(Me.TB_Description)
        Me.GroupBox1.Controls.Add(Me.TB_Username)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.TB_Password)
        Me.GroupBox1.Controls.Add(Me.TB_UpdateDate)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 9)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Size = New System.Drawing.Size(690, 258)
        Me.GroupBox1.TabIndex = 25
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "设置"
        '
        'CB_DeleteLocal
        '
        Me.CB_DeleteLocal.AutoSize = True
        Me.CB_DeleteLocal.Location = New System.Drawing.Point(242, 92)
        Me.CB_DeleteLocal.Name = "CB_DeleteLocal"
        Me.CB_DeleteLocal.Size = New System.Drawing.Size(142, 21)
        Me.CB_DeleteLocal.TabIndex = 30
        Me.CB_DeleteLocal.Text = "删除本地多余文件"
        Me.CB_DeleteLocal.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(204, 69)
        Me.Label10.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(36, 17)
        Me.Label10.TabIndex = 29
        Me.Label10.Text = "路径"
        '
        'TB_SeverPath
        '
        Me.TB_SeverPath.Location = New System.Drawing.Point(242, 67)
        Me.TB_SeverPath.Name = "TB_SeverPath"
        Me.TB_SeverPath.Size = New System.Drawing.Size(413, 22)
        Me.TB_SeverPath.TabIndex = 28
        '
        'CB_SeverIP
        '
        Me.CB_SeverIP.FormattingEnabled = True
        Me.CB_SeverIP.Location = New System.Drawing.Point(88, 67)
        Me.CB_SeverIP.Name = "CB_SeverIP"
        Me.CB_SeverIP.Size = New System.Drawing.Size(114, 24)
        Me.CB_SeverIP.TabIndex = 27
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(5, 92)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(78, 17)
        Me.Label9.TabIndex = 25
        Me.Label9.Text = "主程序版本"
        '
        'TB_MainVersion
        '
        Me.TB_MainVersion.Location = New System.Drawing.Point(88, 90)
        Me.TB_MainVersion.Margin = New System.Windows.Forms.Padding(2)
        Me.TB_MainVersion.Name = "TB_MainVersion"
        Me.TB_MainVersion.Size = New System.Drawing.Size(114, 22)
        Me.TB_MainVersion.TabIndex = 26
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.FlowLayoutPanel1)
        Me.GroupBox2.Controls.Add(Me.LV_Filelist)
        Me.GroupBox2.Location = New System.Drawing.Point(9, 272)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox2.Size = New System.Drawing.Size(690, 204)
        Me.GroupBox2.TabIndex = 26
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "文件列表"
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.AutoScroll = True
        Me.FlowLayoutPanel1.AutoScrollMinSize = New System.Drawing.Size(81, 186)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(2, 17)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(81, 185)
        Me.FlowLayoutPanel1.TabIndex = 22
        '
        'LV_Filelist
        '
        Me.LV_Filelist.isAutoColumnWidth = False
        Me.LV_Filelist.isAutoScroll = False
        Me.LV_Filelist.Location = New System.Drawing.Point(88, 16)
        Me.LV_Filelist.Margin = New System.Windows.Forms.Padding(2)
        Me.LV_Filelist.Name = "LV_Filelist"
        Me.LV_Filelist.Size = New System.Drawing.Size(567, 184)
        Me.LV_Filelist.TabIndex = 20
        Me.LV_Filelist.UseCompatibleStateImageBehavior = False
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(123, 489)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(77, 33)
        Me.Button1.TabIndex = 27
        Me.Button1.Text = "搜索文件"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Frm_Main
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(712, 540)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Btn_Upload)
        Me.Controls.Add(Me.Button2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MinimumSize = New System.Drawing.Size(650, 510)
        Me.Name = "Frm_Main"
        Me.Text = "Auto uploader"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Btn_SelFolder As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents TB_ProgramName As TextBox
    Friend WithEvents TB_Username As TextBox
    Friend WithEvents TB_Password As TextBox
    Friend WithEvents TB_UpdateDate As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TB_Description As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents Btn_Upload As Button
    Friend WithEvents LV_Filelist As myListViewNF
    Friend WithEvents Label8 As Label
    Friend WithEvents TB_FolderPath As TextBox
    Friend WithEvents Btn_SelProgram As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label9 As Label
    Friend WithEvents TB_MainVersion As TextBox
    Friend WithEvents CB_SeverIP As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TB_SeverPath As System.Windows.Forms.TextBox
    Friend WithEvents CB_DeleteLocal As System.Windows.Forms.CheckBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
End Class

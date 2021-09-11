Imports System.IO
Imports System.Diagnostics
Imports System.Threading

Public Class Frm_Main
    Dim UpdateUrl As String
    Dim UpdateUrlRoot As String = String.Empty
    Dim UpdateProjectFolder As String
    Dim InitFrmWidth As Integer
    Dim InitFrmHeight As Integer
    Dim Username As String
    Dim Password As String
    Dim ActiveFilter As New List(Of String)
    Dim AllFileType As New List(Of String)
    Public Shared isContinue As Boolean = True

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Btn_SelFolder.Click
        TB_FolderPath.Text = myFileIO.GetFolderPath
    End Sub

    Private Sub Btn_SelMainProgram_Click(sender As Object, e As EventArgs) Handles Btn_SelProgram.Click
        Dim Filepath As String = myFileIO.OpenFilePath("exe", TB_FolderPath.Text)
        If Not Filepath = String.Empty Then
            Dim fileVer As FileVersionInfo = FileVersionInfo.GetVersionInfo(Filepath)
            Dim fileinfo As New FileInfo(Filepath)
            TB_ProgramName.Text = System.IO.Path.GetFileName(Filepath)
            TB_FolderPath.Text = System.IO.Path.GetDirectoryName(Filepath)
            TB_MainVersion.Text = fileVer.FileVersion
            TB_UpdateDate.Text = fileinfo.LastWriteTime.ToString("yyyy-MM-dd")
            TB_SeverPath.Text = "/Data/app_upgrade/" + TB_ProgramName.Text.Replace(".exe", "") + "/"
            UpdateProjectFolder = TB_ProgramName.Text.Replace(".exe", "")
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Dim filelist As New List(Of String)
        If TB_Description.Text = String.Empty Then
            Dim r = MessageBox.Show("没有对此次更新添加任何描述信息，是否继续？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If r = Windows.Forms.DialogResult.No Then Exit Sub
        End If

        UpdateUrl = "ftp://" & CB_SeverIP.Text & TB_SeverPath.Text
        UpdateUrlRoot = UpdateUrl.Replace(UpdateProjectFolder + "/", "")


        Dim xmlPath As String = System.IO.Path.Combine(TB_FolderPath.Text, "Version.xml")
        If File.Exists(xmlPath) Then
            File.Delete(xmlPath)
        End If
        If LV_Filelist.Items.Count > 0 Then
            Dim xmlVersion As New myXMLhelper(xmlPath)
            Dim FL As List(Of String) = GetListViewFilelist()
            If Not FL.Contains("Version.xml") Then AddFile(xmlPath)
        End If
    End Sub

    Public Function GetFileList(isClearPrevious As Boolean) As List(Of String)
        Dim list As New List(Of String)
        'Dim r As Integer
        'Dim filepath_related As String
        If isClearPrevious Then
            LV_Filelist.Items.Clear()
            LV_Filelist.BeginUpdate()
            Call GetAllFiles(TB_FolderPath.Text)
            LV_Filelist.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
            LV_Filelist.EndUpdate()
        End If

        Return GetListViewFilelist()
    End Function


    Public Function GetListViewFilelist() As List(Of String)
        Dim list As New List(Of String)
        Dim filepath_related As String
        Dim r As Integer
        If LV_Filelist.Items.Count > 0 Then
            For r = 0 To LV_Filelist.Items.Count - 1
                If LV_Filelist.Items(r).SubItems(2).Text = "." Then
                    filepath_related = LV_Filelist.Items(r).SubItems(1).Text
                Else
                    filepath_related = LV_Filelist.Items(r).SubItems(2).Text + "\" + LV_Filelist.Items(r).SubItems(1).Text
                End If
                list.Add(filepath_related)
            Next
            Return list
        Else
            Return Nothing
        End If
    End Function

    Public Function GetListViewFileVerlist() As List(Of String)
        Dim list As New List(Of String)
        Dim r As Integer
        If LV_Filelist.Items.Count > 0 Then
            For r = 0 To LV_Filelist.Items.Count - 1
                list.Add(LV_Filelist.Items(r).SubItems(3).Text)
            Next
        End If
        Return list
    End Function
    Private Sub LV_Filelist_Init()
        LV_Filelist.BeginUpdate()
        LV_Filelist.View = View.Details '设置视图   获取或设置项在控件中的显示方式
        LV_Filelist.FullRowSelect = True '设置是否行选择模式
        LV_Filelist.GridLines = True '设置网格线
        LV_Filelist.AllowColumnReorder = True '设置是否可拖动列标头来对改变列的顺序。
        LV_Filelist.MultiSelect = True '设置是否可以选择多个项
        LV_Filelist.LabelEdit = True '设置用户是否可以编辑控件中项的标签，对于Detail视图，只能编辑行第一列的内容
        LV_Filelist.CheckBoxes = False '设置控件中各项的旁边是否显示复选框  
        LV_Filelist.isAutoScroll = False
        LV_Filelist.isAutoColumnWidth = True
        If LV_Filelist.Columns.Count = 0 Then
            LV_Filelist.Columns.Add("No", 30, HorizontalAlignment.Left)
            LV_Filelist.Columns.Add("Filename", 100, HorizontalAlignment.Left)
            LV_Filelist.Columns.Add("Relative Path", 200, HorizontalAlignment.Left)
            LV_Filelist.Columns.Add("Version", 80, HorizontalAlignment.Left)
            LV_Filelist.Columns.Add("Size(KB)", 100, HorizontalAlignment.Left)
            LV_Filelist.Columns.Add("Status", 80, HorizontalAlignment.Left)
        End If
        'LV_Filelist.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
        LV_Filelist.EndUpdate()
    End Sub

    Private Sub GetAllFiles(ByVal strDirect As String)  '搜索所有目录下的文件
        If Not (strDirect Is Nothing) Then
            Dim mFileInfo As System.IO.FileInfo
            Dim mDir As System.IO.DirectoryInfo
            Dim mDirInfo As New System.IO.DirectoryInfo(strDirect)
            Dim no As Integer = 0
            Try
                For Each mFileInfo In mDirInfo.GetFiles("*.*")
                    'Debug.Print(mFileInfo.FullName)
                    Dim filetype As String = mFileInfo.Extension.Replace(".", "")
                    If ActiveFilter.Count > 0 Then
                        If Not ActiveFilter.Contains(filetype) Then Continue For
                    Else
                        If Not AllFileType.Contains(filetype) Then
                            AllFileType.Add(filetype)
                            LV_AddFileFilter(filetype)
                        End If

                    End If
                    Dim Ver As String = FileVersionInfo.GetVersionInfo(mFileInfo.FullName).FileVersion
                    If IsNothing(Ver) Then
                        Ver = mFileInfo.LastWriteTime.ToString("yyyyMMdd.hhmmss")
                    Else

                        Ver = Ver.Replace(",", ".").Replace(" ", "")
                        If Ver = String.Empty Or (Not IsNumeric(Ver.Replace(".", ""))) Then
                            Ver = mFileInfo.LastWriteTime.Year.ToString + "." + mFileInfo.LastWriteTime.DayOfYear.ToString + "." + mFileInfo.LastWriteTime.TimeOfDay.TotalSeconds.ToString
                        End If

                    End If
                    Dim size As Single = Math.Round(mFileInfo.Length / 1024, 1)
                    Dim RelativePath As String = mFileInfo.DirectoryName.Replace(TB_FolderPath.Text + "\", "").Replace(TB_FolderPath.Text, "")
                    If RelativePath = String.Empty Then RelativePath = "."
                    Dim FileInfo As String() = {mFileInfo.Name, RelativePath, Ver, size, "未上传"}
                    If mFileInfo.Name.ToLower = "updater.exe" Or mFileInfo.Name.ToLower = "uploader.exe" Or mFileInfo.Name.ToLower = "version.xml" Or mFileInfo.Name.EndsWith(".db") Then
                        '忽略自身和updater
                    Else
                        LV_Filelist.AddRow(Color.Black, False, FileInfo)
                        'Application.DoEvents()
                    End If

                Next

                For Each mDir In mDirInfo.GetDirectories
                    'Debug.Print("******目录回调*******")
                    GetAllFiles(mDir.FullName)
                Next
            Catch ex As System.IO.DirectoryNotFoundException
                Debug.Print("目录没找到：" + ex.Message)
            End Try
        End If
    End Sub

    Private Sub AddFile(ByVal FilePath As String)  '添加一个文件
        If Not (FilePath Is Nothing) Then
            Dim mFileInfo As New System.IO.FileInfo(FilePath)
            Dim no As Integer = 0
            Try
                Debug.Print(mFileInfo.FullName)
                Dim Ver As String = FileVersionInfo.GetVersionInfo(mFileInfo.FullName).FileVersion
                Dim size As Single = Math.Round(mFileInfo.Length / 1024, 1)
                If Ver = String.Empty Then Ver = TB_MainVersion.Text
                Dim RelativePath As String = mFileInfo.DirectoryName.Replace(TB_FolderPath.Text + "\", "").Replace(TB_FolderPath.Text, "")
                If RelativePath = String.Empty Then RelativePath = "."
                Dim FileInfo As String() = {mFileInfo.Name, RelativePath, Ver, size, "未上传"}
                LV_Filelist.AddRow(Color.Black, True, FileInfo)
                Application.DoEvents()
            Catch ex As System.IO.DirectoryNotFoundException
                Debug.Print(ex.Message)
            End Try
        End If
    End Sub

    Public Function CreateElementDic() As Dictionary(Of String, String)
        Dim dic As New Dictionary(Of String, String)
        dic.Add("StartingName", TB_ProgramName.Text)
        dic.Add("UpdateUrl", UpdateUrl)
        If TB_Username.Text.Length > 0 Then dic.Add("Username", TB_Username.Text)
        If TB_Password.Text.Length > 0 Then dic.Add("Password", TB_Password.Text)
        dic.Add("MainVersion", TB_MainVersion.Text)
        dic.Add("LastUpdateTime", TB_UpdateDate.Text)
        dic.Add("DeleteLocalFile", CB_DeleteLocal.Checked.ToString)
        dic.Add("UpdateDescription", TB_Description.Text.Replace(vbCrLf, "\n  "))
        Return dic
    End Function

    Private Sub Frm_Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text += " " + ProductVersion
        CB_SeverIP.Items.Add("106.54.128.169")
        CB_SeverIP.Items.Add("43.226.149.108")
        CB_SeverIP.Items.Add("43.226.149.114")
        CB_SeverIP.SelectedIndex = 0
        InitFrmWidth = Me.Width
        InitFrmHeight = Me.Height
        LV_Filelist_Init()
        'LV_AddDefaultFilter()
    End Sub

    '    Private Sub Btn_Upload_Click(sender As Object, e As EventArgs) Handles Btn_Upload.Click
    '        If LV_Filelist.Items.Count = 0 Then Exit Sub
    '        Dim TryCnt As Integer = 3, CurTry As Integer
    '        Dim r As Boolean
    '        Dim FtpRemotePath As String = UpdateUrlRoot.Replace("ftp://" + CB_SeverIP.Text + "/", "")
    '        If TB_Username.Text = String.Empty Then
    '            Select Case CB_SeverIP.Text
    '                Case "106.54.128.169"
    '                    Username = "ftp6321126"
    '                    Password = "6bs9QJL1x91NOAR7BBSFDY20"
    '                Case "43.226.149.108"
    '                    Username = "ftp6319254"
    '                    Password = "ABCDefg123"
    '                Case "43.226.149.114"
    '                    Username = "ftp6321126"
    '                    Password = "6bs9QJL1x91NOAR7BBSFDY20"
    '            End Select
    '        Else
    '            Username = TB_Username.Text
    '            Password = TB_Password.Text
    '        End If
    '        Dim myFTP As New FtpHelper(CB_SeverIP.Text, FtpRemotePath.Substring(0, Len(FtpRemotePath) - 1), Username, Password)
    '        myFTP.CheckProjectDir(UpdateProjectFolder)
    '        Dim LocalFile As New List(Of String)
    '        Dim RemoteFile As New List(Of String)
    '        Dim RelativeFolder As New List(Of String)
    '        Dim FileName As New List(Of String)
    '        Dim lvi As ListViewItem
    '        Dim f As String, i As Integer
    '        If LV_Filelist.Items.Count > 0 Then
    '            '准备要上传的文件列表
    '            For Each lvi In LV_Filelist.Items
    '                Dim RelPath As String = lvi.SubItems(2).Text
    '                If RelPath = "." Then
    '                    f = lvi.SubItems(1).Text
    '                Else
    '                    f = RelPath + "\" + lvi.SubItems(1).Text
    '                End If
    '                RelativeFolder.Add(RelPath)
    '                FileName.Add(lvi.SubItems(1).Text)
    '                LocalFile.Add(TB_FolderPath.Text + "\" + f)
    '                RemoteFile.Add(TB_ProgramName.Text.Replace(".exe", "") + "/" + f.Replace("\", "/"))
    '            Next

    '            '开始上传
    '            For i = 0 To LocalFile.Count - 1
    '                If Not LV_Filelist.Items(i).SubItems(5).Text = "已上传" Then
    '                    CurTry = 0
    'retry:
    '                    CurTry += 1
    '                    If RelativeFolder(i) = "." Then
    '                        '上传根目录
    '                        myFTP.GotoDirectory("Data/app_upgrade/" & UpdateProjectFolder, True)
    '                        If i = 0 Then Debug.Print("Current folder is " & myFTP.ftpCurFolder)
    '                        LV_Filelist.Items(i).SubItems(5).Text = "正在上传..."
    '                        r = myFTP.Upload(LocalFile(i), LV_Filelist, i)
    '                        If r Then
    '                            LV_Filelist.Items(i).SubItems(5).Text = "已上传"
    '                        ElseIf CurTry <= TryCnt Then
    '                            GoTo retry
    '                        Else
    '                            LV_Filelist.Items(i).SubItems(5).Text = "上传失败"
    '                        End If
    '                        Application.DoEvents()

    '                    Else
    '                        '上传子目录
    '                        If i = 0 Then

    '                        Else
    '                            If RelativeFolder(i) <> RelativeFolder(i - 1) Then
    '                                Dim RelateFolderToPreviousItem As String
    '                                If RelativeFolder(i).Contains(RelativeFolder(i - 1)) Then '是上级目录的子目录
    '                                    myFTP.GotoDirectory("Data/app_upgrade/" & UpdateProjectFolder & "/" & RelativeFolder(i - 1).Replace("\", "/"), True)
    '                                    RelateFolderToPreviousItem = RelativeFolder(i).Replace(RelativeFolder(i - 1), "")
    '                                Else '独立于上一个目录的其他目录
    '                                    myFTP.GotoDirectory("Data/app_upgrade/" & UpdateProjectFolder, True)
    '                                    RelateFolderToPreviousItem = RelativeFolder(i)
    '                                End If

    '                                If RelateFolderToPreviousItem.Contains("\") Then
    '                                    Dim fd = Split(RelateFolderToPreviousItem, "\")
    '                                    Dim j As Integer
    '                                    For j = 0 To fd.Length - 1
    '                                        If Not myFTP.DirectoryExist(fd(j)) Then
    '                                            r = myFTP.MakeDir(fd(j))
    '                                            If Not r Then
    '                                                If CurTry <= TryCnt Then
    '                                                    GoTo retry
    '                                                Else
    '                                                    LV_Filelist.Items(i).SubItems(5).Text = "上传失败" : Continue For
    '                                                End If
    '                                            End If

    '                                        End If
    '                                        myFTP.GotoDirectory(fd(j), False)
    '                                    Next
    '                                Else
    '                                    If Not myFTP.DirectoryExist(RelateFolderToPreviousItem) Then
    '                                        r = myFTP.MakeDir(RelateFolderToPreviousItem)
    '                                        If Not r Then
    '                                            If CurTry <= TryCnt Then
    '                                                GoTo retry
    '                                            Else
    '                                                LV_Filelist.Items(i).SubItems(5).Text = "上传失败" : Continue For
    '                                            End If
    '                                        End If
    '                                    End If
    '                                    myFTP.GotoDirectory(RelateFolderToPreviousItem, False)
    '                                End If


    '                                Debug.Print("Current folder is " & myFTP.ftpCurFolder)
    '                            End If
    '                        End If
    '                        LV_Filelist.Items(i).SubItems(5).Text = "正在上传..."
    '                        r = myFTP.Upload(LocalFile(i), LV_Filelist, i)
    '                        If r Then
    '                            LV_Filelist.Items(i).SubItems(5).Text = "已上传"
    '                        ElseIf CurTry <= TryCnt Then
    '                            GoTo retry
    '                        Else
    '                            LV_Filelist.Items(i).SubItems(5).Text = "上传失败"
    '                        End If
    '                        Application.DoEvents()
    '                        End If
    '                End If
    '            Next


    '        End If


    '    End Sub

    Private Sub Frm_Main_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If Me.Width < 200 Or Me.Height < 100 Then Exit Sub
        Dim WidthChange As Integer = Me.Width - InitFrmWidth
        Dim HeightChange As Integer = Me.Height - InitFrmHeight
        Dim LayoutSetting As New myLayoutSetting
        If WidthChange <> 0 Or HeightChange <> 0 Then
            LayoutSetting.LayoutAdapt(Me, WidthChange, HeightChange)
            If WidthChange <> 0 Then InitFrmWidth = Me.Width
            If HeightChange <> 0 Then InitFrmHeight = Me.Height
        End If

    End Sub


    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        If TB_ProgramName.Text.Length > 0 Then
            If ActiveFilter.Count = 0 Then
                FlowLayoutPanel1.Controls.Clear()
                AllFileType.Clear()
            End If
            
            Dim filelist As New List(Of String)
            filelist = GetFileList(True)
        End If
    End Sub



    Private Sub FlowLayoutPanel1_doubleclick(sender As Object, e As MouseEventArgs) Handles FlowLayoutPanel1.DoubleClick
        Dim strin = InputBox("输入要添加的文件扩展名", "添加过滤器")
        LV_AddFileFilter(strin)
    End Sub

    Private Sub LV_AddDefaultFilter()
        Dim filter As New List(Of String)
        filter.Add("exe")
        filter.Add("dll")
        filter.Add("ini")
        filter.Add("txt")
        filter.Add("xml")
        For Each ft As String In filter
            LV_AddFileFilter(ft)
        Next
    End Sub
    Private Sub LV_AddFileFilter(filter As String)
        If Not filter = String.Empty Then
            Dim btn As New CheckBox
            btn.Text = filter.ToLower
            btn.Padding = New Padding(0, 0, 0, 0)
            btn.Margin = New Padding(3, 0, 3, 0)

            FlowLayoutPanel1.Controls.Add(btn)
            AddHandler btn.CheckedChanged, AddressOf LV_AddFilterEvent
        End If
    End Sub

    Private Sub LV_AddFilterEvent(cb As CheckBox, e As EventArgs)
        If cb.Checked Then
            ActiveFilter.Add(cb.Text)
        Else
            ActiveFilter.Remove(cb.Text)
        End If
    End Sub













    Dim Td As Thread
    Public Event CB_Thread_Finish()
    Public Delegate Function CtlTxt_Read_Delegate(Control As Object) As String
    Public Delegate Sub CtlTxt_Write_Delegate(Control As Object, str As String)
    Public Delegate Function ListView_ReadItems_Delegate(LV As ListView) As List(Of ListViewItem)
    Public Delegate Sub ListView_WriteStr_Delegate(LV As ListView, row As Integer, col As Integer, str As String)
    Public Delegate Function GetListView_Delegate(LV As ListView) As ListView


    Public Function GetListView(LV As ListView) As ListView
        If LV.InvokeRequired Then
            Dim dt As New GetListView_Delegate(AddressOf GetListView)
            Dim ia As IAsyncResult = LV.BeginInvoke(dt, New Object() {LV})
            Return CType(LV.EndInvoke(ia), ListView)
        Else
            Return LV
        End If
    End Function

    Public Function ListView_ReadItems(LV As ListView) As List(Of ListViewItem)
        Dim ListViewItems As New List(Of ListViewItem)
        If LV.InvokeRequired Then
            Dim dt As New ListView_ReadItems_Delegate(AddressOf ListView_ReadItems)
            Dim ia As IAsyncResult = LV.BeginInvoke(dt, New Object() {LV})
            Return CType(LV.EndInvoke(ia), List(Of ListViewItem))
        Else
            Dim i As Integer
            For i = 0 To LV.Items.Count - 1
                ListViewItems.Add(LV.Items(i))
            Next
            Return ListViewItems
        End If
    End Function

    Public Sub ListView_WriteStr(LV As ListView, row As Integer, col As Integer, str As String)
        If LV.InvokeRequired Then
            Dim dt As ListView_WriteStr_Delegate = New ListView_WriteStr_Delegate(AddressOf ListView_WriteStr)
            LV.Invoke(dt, New Object() {LV, row, col, str})
        Else
            LV.Items(row).SubItems(col).Text = str
        End If
    End Sub

    Public Function CtlTxt_Read(cb As Object) As String
        If cb.InvokeRequired Then
            Dim dt As New CtlTxt_Read_Delegate(AddressOf CtlTxt_Read)
            Dim ia As IAsyncResult = cb.beginInvoke(dt, New Object() {cb})
            Return cb.endinvoke(ia).ToString
        Else
            Return cb.text
        End If
    End Function
    Public Sub CtlTxt_Write(cb As Object, str As String)
        If cb.InvokeRequired Then
            Dim dt As CtlTxt_Write_Delegate = New CtlTxt_Write_Delegate(AddressOf CtlTxt_Write)
            cb.Invoke(dt, New Object() {cb, str})
        Else
            cb.Text = str
        End If
    End Sub


    Private Sub UploadEventHandler() Handles Me.CB_Thread_Finish
        Td.Join()
        Btn_Upload.Text = "上传"
    End Sub


    Private Sub Btn_Upload_Click(sender As Object, e As EventArgs) Handles Btn_Upload.Click
        If UpdateUrlRoot.Length = 0 Then
            Dim r = MessageBox.Show("还没有创建更新配置xml文件，是否直接开始上传", "注意", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If r = vbNo Then Exit Sub
        End If
        UpdateUrl = "ftp://" & CB_SeverIP.Text & TB_SeverPath.Text
        UpdateUrlRoot = UpdateUrl.Replace(UpdateProjectFolder + "/", "")

        If LV_Filelist.Items.Count = 0 Then Exit Sub
        If Btn_Upload.Text = "上传" Then
            Td = New Thread(AddressOf myThreadProcess)
            Td.Start()
            isContinue = True
            Btn_Upload.Text = "停止"
        ElseIf Btn_Upload.Text = "停止" Then
            isContinue = False
        End If

    End Sub

    Private Sub myThreadProcess()
        Dim TryCnt As Integer = 1, CurTry As Integer
        Dim r As Boolean
        Dim ServerIP = CtlTxt_Read(CB_SeverIP)
        Dim FolderPath = CtlTxt_Read(TB_FolderPath)
        Dim ProgramName = CtlTxt_Read(TB_ProgramName)
        Dim myLVlist As List(Of ListViewItem)
        myLVlist = ListView_ReadItems(LV_Filelist)

        Dim FtpRemotePath As String = UpdateUrlRoot.Replace("ftp://" + ServerIP + "/", "")
        If CtlTxt_Read(TB_Username) = String.Empty Then
            Select Case ServerIP
                Case "106.54.128.169"
                    Username = "ftp6321126"
                    Password = "6bs9QJL1x91NOAR7BBSFDY20"
                Case "43.226.149.108"
                    Username = "ftp6319254"
                    Password = "ABCDefg123"
                Case "43.226.149.114"
                    Username = "ftp6321126"
                    Password = "6bs9QJL1x91NOAR7BBSFDY20"
            End Select
        Else
            Username = CtlTxt_Read(TB_Username)
            Password = CtlTxt_Read(TB_Password)
        End If

        Dim myFTP As New FtpHelper(ServerIP, FtpRemotePath.Substring(0, Len(FtpRemotePath) - 1), Username, Password, LV_Filelist)
        r = myFTP.CheckProjectDir(UpdateProjectFolder)
        If Not r Then
            MessageBox.Show("连接服务器失败", "错误", MessageBoxButtons.YesNo, MessageBoxIcon.Error)
            GoTo Finish
        End If
        Dim LocalFile As New List(Of String)
        Dim RemoteFile As New List(Of String)
        Dim RelativeFolder As New List(Of String)
        Dim FileName As New List(Of String)
        Dim lvi As ListViewItem
        Dim f As String, i As Integer

        '准备要上传的文件列表
        For Each lvi In myLVlist
            Dim RelPath As String = lvi.SubItems(2).Text
            If lvi.SubItems(5).Text = "上传失败" Or lvi.SubItems(5).Text = "操作取消" Then
                ListView_WriteStr(LV_Filelist, i, 5, "未上传")
            End If
            i = i + 1

            If RelPath = "." Then
                f = lvi.SubItems(1).Text
            Else
                f = RelPath + "\" + lvi.SubItems(1).Text
            End If
            RelativeFolder.Add(RelPath)
            FileName.Add(lvi.SubItems(1).Text)
            LocalFile.Add(FolderPath + "\" + f)
            RemoteFile.Add(ProgramName.Replace(".exe", "") + "/" + f.Replace("\", "/"))
        Next

        '开始上传
        For i = 0 To LocalFile.Count - 1
            If Not myLVlist(i).SubItems(5).Text = "已上传" Then
                CurTry = 0
retry:
                If Not isContinue Then
                    ListView_WriteStr(LV_Filelist, i, 5, "操作取消")
                    GoTo Finish
                End If
                CurTry += 1
                If RelativeFolder(i) = "." Then
                    '上传根目录
                    myFTP.GotoDirectory("Data/app_upgrade/" & UpdateProjectFolder, True)
                    If i = 0 Then Debug.Print("Current folder is " & myFTP.ftpCurFolder)
                    ListView_WriteStr(LV_Filelist, i, 5, "正在上传...")
                    r = myFTP.Upload(LocalFile(i), i, 5)
                    'a()

                    If r Then
                        'myLVlist(i).SubItems(5).Text = "已上传"
                        ListView_WriteStr(LV_Filelist, i, 5, "已上传")
                    ElseIf CurTry <= TryCnt Then
                        GoTo retry
                    Else
                        'myLVlist(i).SubItems(5).Text = "上传失败"
                        ListView_WriteStr(LV_Filelist, i, 5, "上传失败")
                    End If
                    'Application.DoEvents()

                Else
                    '上传子目录
                    Dim RelateFolderToPreviousItem As String

                    If i = 0 Then
                        myFTP.GotoDirectory("Data/app_upgrade/" & UpdateProjectFolder, True)
                        RelateFolderToPreviousItem = RelativeFolder(i)
                        If RelateFolderToPreviousItem.Contains("\") Then
                            Dim fd = Split(RelateFolderToPreviousItem, "\")
                            Dim j As Integer
                            For j = 0 To fd.Length - 1
                                If Not myFTP.DirectoryExist(fd(j)) Then
                                    r = myFTP.MakeDir(fd(j))
                                    If Not r Then
                                        If CurTry <= TryCnt Then
                                            GoTo retry
                                        Else
                                            ListView_WriteStr(LV_Filelist, i, 5, "上传失败") : Continue For
                                        End If
                                    End If

                                End If
                                myFTP.GotoDirectory(fd(j), False)
                            Next
                        Else
                            If Not myFTP.DirectoryExist(RelateFolderToPreviousItem) Then
                                r = myFTP.MakeDir(RelateFolderToPreviousItem)
                                If Not r Then
                                    If CurTry <= TryCnt Then
                                        GoTo retry
                                    Else
                                        ListView_WriteStr(LV_Filelist, i, 5, "上传失败") : Continue For
                                    End If
                                End If
                            End If
                            myFTP.GotoDirectory(RelateFolderToPreviousItem, False)
                        End If
                        Debug.Print("Current folder is " & myFTP.ftpCurFolder)
                    ElseIf RelativeFolder(i) <> RelativeFolder(i - 1) Then
                        If RelativeFolder(i).Contains(RelativeFolder(i - 1)) Then '是上级目录的子目录
                            myFTP.GotoDirectory("Data/app_upgrade/" & UpdateProjectFolder & "/" & RelativeFolder(i - 1).Replace("\", "/"), True)
                            RelateFolderToPreviousItem = RelativeFolder(i).Replace(RelativeFolder(i - 1), "")
                        Else '独立于上一个目录的其他目录
                            myFTP.GotoDirectory("Data/app_upgrade/" & UpdateProjectFolder, True)
                            RelateFolderToPreviousItem = RelativeFolder(i)
                        End If

                        If RelateFolderToPreviousItem.Contains("\") Then
                            Dim fd = Split(RelateFolderToPreviousItem, "\")
                            Dim j As Integer
                            For j = 0 To fd.Length - 1
                                If Not myFTP.DirectoryExist(fd(j)) Then
                                    r = myFTP.MakeDir(fd(j))
                                    If Not r Then
                                        If CurTry <= TryCnt Then
                                            GoTo retry
                                        Else
                                            ListView_WriteStr(LV_Filelist, i, 5, "上传失败") : Continue For
                                        End If
                                    End If

                                End If
                                myFTP.GotoDirectory(fd(j), False)
                            Next
                        Else
                            If Not myFTP.DirectoryExist(RelateFolderToPreviousItem) Then
                                r = myFTP.MakeDir(RelateFolderToPreviousItem)
                                If Not r Then
                                    If CurTry <= TryCnt Then
                                        GoTo retry
                                    Else
                                        ListView_WriteStr(LV_Filelist, i, 5, "上传失败") : Continue For
                                    End If
                                End If
                            End If
                            myFTP.GotoDirectory(RelateFolderToPreviousItem, False)
                        End If

                        Debug.Print("Current folder is " & myFTP.ftpCurFolder)
                    End If




                    ListView_WriteStr(LV_Filelist, i, 5, "正在上传...")
                    r = myFTP.Upload(LocalFile(i), i, 5)
                    If r Then
                        ListView_WriteStr(LV_Filelist, i, 5, "已上传")
                    ElseIf CurTry <= TryCnt Then
                        GoTo retry
                    Else
                        ListView_WriteStr(LV_Filelist, i, 5, "上传失败")
                    End If
                    Application.DoEvents()
                End If
            End If
        Next



Finish:
        CtlTxt_Write(Btn_Upload, "上传")
        RaiseEvent CB_Thread_Finish()
    End Sub

    Private Sub Frm_Main_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        System.Environment.Exit(0)
    End Sub
End Class

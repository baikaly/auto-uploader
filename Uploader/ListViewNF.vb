Imports System.IO
Public Class myListViewNF        '无闪烁的ListView
    Inherits System.Windows.Forms.ListView
    Public Property isAutoScroll As Boolean
    Public Property isAutoColumnWidth As Boolean
    Private WithEvents strip As New ContextMenuStrip
    Private RightClickMenuItem As String() = {"删除选中项目", "仅保留选中项"}

    Public Sub New()
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.EnableNotifyMessage, True)
        UpdateStyles()
        AddStripMenu()
    End Sub

    Private Sub AddStripMenu()
        Dim i As Integer
        For i = 0 To RightClickMenuItem.Length - 1
            strip.Items.Add(RightClickMenuItem(i))
        Next
    End Sub
    Protected Overrides Sub OnNotifyMessage(m As Message)
        If (m.Msg <> &H14) Then
            MyBase.OnNotifyMessage(m)
        End If
    End Sub

    'Public Overloads Sub AddRow(c As Color, ParamArray paras() As String)
    '    Dim LVI As New ListViewItem()
    '    LVI.SubItems.Clear()
    '    LVI.Text = MyBase.Items.Count.ToString + 1
    '    If paras.Length < MyBase.Columns.Count Then
    '        For Each para In paras
    '            LVI.SubItems.Add(para)
    '        Next
    '        LVI.ForeColor = c
    '        MyBase.BeginUpdate()
    '        MyBase.Items.Add(LVI)
    '        MyBase.Items(MyBase.Items.Count - 1).EnsureVisible()  '滚动到最后
    '        MyBase.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
    '        MyBase.EndUpdate()
    '    Else
    '        Debug.Print("数据个数超出Listview的列数")
    '    End If
    'End Sub
    Public Overloads Sub AddRow(c As Color, isRunTimeUpdate As Boolean, ParamArray paras() As String)
        Dim LVI As New ListViewItem()
        LVI.Text = MyBase.Items.Count.ToString + 1
        If paras.Length < MyBase.Columns.Count Then
            For Each para In paras
                LVI.SubItems.Add(para)
            Next
            LVI.ForeColor = c
            If isRunTimeUpdate Then MyBase.BeginUpdate()
            MyBase.Items.Add(LVI)
            If isRunTimeUpdate Then
                If isAutoScroll Then MyBase.Items(MyBase.Items.Count - 1).EnsureVisible() '滚动到最后
                If isAutoColumnWidth Then MyBase.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
                MyBase.EndUpdate()
                Application.DoEvents()
            End If

        Else
            Debug.Print("数据个数超出Listview的列数")
        End If
    End Sub

    Public Overloads Sub AddRowDefault(ParamArray paras() As String)        '自动判断PASS还是FAIL
        Dim LVI As New ListViewItem()
        Dim isPass As Boolean = True
        LVI.SubItems.Clear()
        LVI.Text = MyBase.Items.Count.ToString + 1
        If paras.Length < MyBase.Columns.Count And paras.Length > 4 Then
            For Each para In paras
                LVI.SubItems.Add(para)
            Next
            If IsNumeric(paras(3)) And (IsNumeric(paras(2)) Or IsNumeric(paras(4))) Then
                If IsNumeric(paras(2)) Then
                    If CSng(paras(3)) < CSng(paras(2)) Then isPass = False
                End If
                If IsNumeric(paras(4)) Then
                    If CSng(paras(3)) > CSng(paras(4)) Then isPass = False
                End If
            End If
            If isPass Then LVI.ForeColor = Color.Black Else LVI.ForeColor = Color.Red
            MyBase.BeginUpdate()
            MyBase.Items.Add(LVI)
            If isAutoScroll Then MyBase.Items(MyBase.Items.Count - 1).EnsureVisible() '滚动到最后
            If isAutoColumnWidth Then MyBase.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
            MyBase.EndUpdate()
        Else
            Debug.Print("数据不符合标准格式")
        End If
    End Sub


    Public Function SaveToFile(Optional Filepath As String = "") As Boolean
        If MyBase.Items.Count = 0 Then Return False
        Dim ResultFolder = Application.StartupPath + "\TestResult"
        Dim Day As String = Date.Today.ToString("yyyy_MM_dd")
        Dim Time As String = Date.Now.ToString("hh_mm_ss")
        Dim ResultFolderDate = ResultFolder + "\" + Day
        Dim FullPath = ResultFolderDate + "\Result_" + Time + ".csv"
        Dim i As Integer, j As Integer, s As String = ""
        Try
            For i = 0 To MyBase.Columns.Count - 1
                If i = MyBase.Columns.Count - 1 Then s += MyBase.Columns(i).Text Else s += MyBase.Columns(i).Text + "," '列表头
            Next
            s += vbCrLf
            For i = 0 To MyBase.Items.Count - 1
                's += MyBase.Items(i).Text + ","       
                For j = 0 To MyBase.Items(i).SubItems.Count - 1
                    If j = MyBase.Items(i).SubItems.Count - 1 Then s += MyBase.Items(i).SubItems(j).Text Else s += MyBase.Items(i).SubItems(j).Text + "," '数据
                Next
                s += vbCrLf
            Next

            If Filepath = "" Then
                If Not Directory.Exists(ResultFolder) Then Directory.CreateDirectory(ResultFolder)
                If Not Directory.Exists(ResultFolderDate) Then Directory.CreateDirectory(ResultFolderDate)
                Using Writer As New StreamWriter(FullPath, False, System.Text.Encoding.GetEncoding("UTF-8"))
                    Writer.Write(s)
                    Writer.Close()
                End Using
            Else

            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ListViewHandler_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then
            strip.Show(Me, e.Location)
        End If
    End Sub

    Private Sub DeletSel(sender As Object, e As ToolStripItemClickedEventArgs) Handles strip.ItemClicked
        Dim i As Integer
        If e.ClickedItem.Text = RightClickMenuItem(0) Then          '删除选中项目
            Me.BeginUpdate()
            For i = Me.SelectedItems.Count - 1 To 0 Step -1
                Dim lvi As ListViewItem = Me.SelectedItems(i)
                Me.Items.Remove(lvi)
            Next
        ElseIf e.ClickedItem.Text = RightClickMenuItem(1) Then      '仅保留选中项
            For i = Me.Items.Count - 1 To 0 Step -1
                Dim lvi As ListViewItem = Me.Items(i)
                If Not Me.Items(i).Selected Then Me.Items.Remove(lvi)
            Next
        End If
        Me.EndUpdate()
    End Sub


End Class

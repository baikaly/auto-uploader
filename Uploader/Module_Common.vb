Module Module_Common
    Public Class myLayoutSetting
        Private allCtrls As New Queue(Of Control)

        '递归遍历指定区域上的所有控件
        Private Sub CheckAllCtrls(ByVal item As Control)
            Dim i As Integer
            For i = 0 To item.Controls.Count - 1
                If item.Controls(i).HasChildren Then
                    CheckAllCtrls(item.Controls(i))
                Else
                    'allCtrls.Enqueue(item.Controls(i))   如果只要子控件不要更下层的控件，那么这个语句在else里
                End If
                allCtrls.Enqueue(item.Controls(i))
            Next
        End Sub

        Public Function GetAllCtrls(SearchedCtrl As Control) As Queue(Of Control)
            CheckAllCtrls(SearchedCtrl)
            Return allCtrls
        End Function


        '对于遍历控件的应用
        Public Sub LayoutAdapt(SearchedCtrl As Control, WidthChange As Integer, HeightChange As Integer)
            Dim CtlQue As New Queue(Of Control)
            CtlQue = GetAllCtrls(SearchedCtrl)
            If CtlQue.Count > 0 Then
                For Each ctl In CtlQue
                    If (TypeOf (ctl) Is GroupBox) Or (TypeOf (ctl) Is TextBox) Or (TypeOf (ctl) Is ListView) Then
                        ctl.Width += WidthChange
                        If ctl.Name = "LV_Filelist" Or ctl.Name = "GroupBox2" Then
                            ctl.Height += HeightChange
                        End If
                    End If
                    If (TypeOf (ctl) Is Button) Then
                        If ctl.Name.Contains("_Sel") Then
                            ctl.Left += WidthChange
                        Else
                            ctl.Left += WidthChange / 2
                            ctl.Top += HeightChange
                        End If
                    End If

                    If (TypeOf (ctl) Is CheckBox) Then
                        If ctl.Name.Contains("DeleteLocal") Then
                            ctl.Left += WidthChange
                        End If
                    End If
                Next
            End If
        End Sub
    End Class




    Class TextClass
        ''' <summary>
        ''' GB2312转换成UTF8
        ''' </summary>
        ''' <param name="text"></param>
        ''' <returns></returns>
        Public Shared Function gb2312_utf8(ByVal text As String) As String
            Dim utf8, gb2312 As System.Text.Encoding
            gb2312 = System.Text.Encoding.GetEncoding("gb2312")
            utf8 = System.Text.Encoding.GetEncoding("utf-8")
            Dim gb As Byte()
            gb = gb2312.GetBytes(text)
            gb = System.Text.Encoding.Convert(gb2312, utf8, gb)
            Return utf8.GetString(gb)
        End Function


        Public Shared Function ascii_utf8(ByVal text As String) As String
            Dim utf8, ascii As System.Text.Encoding
            ascii = System.Text.Encoding.GetEncoding("us-ascii")
            utf8 = System.Text.Encoding.GetEncoding("utf-8")
            Dim Asc As Byte()
            Asc = ascii.GetBytes(text)
            Asc = System.Text.Encoding.Convert(ascii, utf8, Asc)
            Return utf8.GetString(Asc)
        End Function


        ''' <summary>
        ''' UTF8转换成GB2312
        ''' </summary>
        ''' <param name="text"></param>
        ''' <returns></returns>
        Public Shared Function utf8_gb2312(ByVal text As String) As String
            Dim utf8, gb2312 As System.Text.Encoding
            utf8 = System.Text.Encoding.GetEncoding("utf-8")
            gb2312 = System.Text.Encoding.GetEncoding("gb2312")
            Dim utf As Byte()
            utf = utf8.GetBytes(text)
            utf = System.Text.Encoding.Convert(utf8, gb2312, utf)
            Return gb2312.GetString(utf)
        End Function
    End Class
End Module

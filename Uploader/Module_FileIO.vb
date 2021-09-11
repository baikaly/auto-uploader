Imports System.IO

Namespace myFileIO
    Module Module_FileIO
        Public Log_Enable As Boolean = False
        Function ExportDataTable(DT As DataTable) As Boolean

            Return True
        End Function

        Function OpenFilePath(Optional Filter As String = "", Optional InitFolder As String = "") As String          '未测试
            Dim fd As OpenFileDialog = New OpenFileDialog()
            Dim filename As String = ""
            fd.Title = "指定要打开的文件:"
            If Not InitFolder = String.Empty Then
                fd.InitialDirectory = InitFolder
            Else
                fd.InitialDirectory = Application.StartupPath
            End If

            Select Case Filter.ToLower
                Case "pic"
                    fd.Filter = "图像文件(*.jpg;*.gif;*.png)|*.jpg;*.gif;*.png"
                    'If fd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    'Dim fn As Image = Image.FromFile(fd.FileName)
                    'Dim bitmap As New Bitmap(fn)
                    'Dim handle As IntPtr = bitmap.GetHicon()
                    'Dim myCursor As New Cursor(handle)
                    'Me.Cursor = myCursor
                    'End If
                Case "csv"
                    fd.Filter = "逗号分隔值文件(*.csv)|*.csv"
                Case "log"
                    fd.Filter = "日志文件(*.log)|*.log"
                Case "wav"
                    fd.Filter = "音频文件(*.wav)|*.wav"
                Case "exe"
                    fd.Filter = "应用程序(*.exe)|*.exe"
                Case Else
                    fd.Filter = "所有文件(*.*)|*.*"
            End Select
            If fd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                filename = fd.FileName
            End If
            fd.Dispose()
            Return filename
        End Function


        Function GetSaveFilePath(Optional Filter As String = "") As String
            Dim sd As New SaveFileDialog
            Dim filename As String = ""
            With sd
                .Title = "保存文件到:"
                .InitialDirectory = Application.StartupPath
                Select Case Filter.ToLower
                    Case "csv"
                        .Filter = "逗号分隔值文件(*.csv)|*.csv"
                    Case "log"
                        .Filter = "日志文件(*.log)|*.log"
                    Case "wav"
                        .Filter = "音频文件(*.wav)|*.wav"
                    Case "excel"
                        .Filter = "Excel文件(*.xlsx)|*.xlsx"
                    Case Else
                        .Filter = "所有文件(*.*)|*.*"
                End Select

                .AddExtension = True
                If .ShowDialog() = DialogResult.OK Then
                    filename = .FileName
                End If
                sd.Dispose()
                Return filename
            End With
        End Function




        Function GetFolderPath() As String
            Dim fbd As New FolderBrowserDialog

            fbd.SelectedPath = Application.StartupPath
            fbd.Description = "请选择文件夹"

            If (fbd.ShowDialog() = DialogResult.OK) Then
                Return fbd.SelectedPath
            Else
                Return String.Empty
            End If

        End Function



        Function FileWrite(str As String, filepath As String, Optional isWaitForOccupy As Boolean = False) As Boolean
            If isWaitForOccupy Then
                Dim strwriter As StreamWriter = Nothing
                Dim r As Boolean = False
                Dim Msgbox_No As Integer = 0
                While Not r
                    strwriter = Nothing
                    r = CheckFileOccupy(filepath, strwriter)
                    If Not r Then Msgbox_No = MsgBox("打开文件""" & filepath & """失败，如果此文件已经被打开，请关闭该文件后重试!", vbRetryCancel, "写入失败")
                    If Msgbox_No = vbCancel Then
                        Return False
                    End If
                End While
                Try
                    'ChDir(Application.StartupPath)
                    strwriter.Write(str)
                    Return True
                Catch ex As Exception
                    Return False
                Finally
                    strwriter.Flush()
                    strwriter.Close()
                    strwriter.Dispose()
                End Try


            Else
                Using StrWriter As New StreamWriter(filepath, True)
                    Try
                        StrWriter.Write(str)
                        Return True
                    Catch ex As Exception
                        Debug.Print("写入文件" & filepath & "出错,错误产生于函数FileWriteLine" & vbCrLf & ex.Message)
                        Return False
                    Finally
                        StrWriter.Flush()
                        StrWriter.Close()
                    End Try
                End Using
            End If

        End Function

        Function FileWriteLine(str As String, filepath As String, Optional isWaitForOccupy As Boolean = False) As Boolean
            If isWaitForOccupy Then
                Dim strwriter As StreamWriter = Nothing
                Dim r As Boolean = False
                Dim Msgbox_No As Integer = 0
                While Not r
                    strwriter = Nothing
                    r = CheckFileOccupy(filepath, strwriter)
                    If Not r Then Msgbox_No = MsgBox("打开文件""" & filepath & """失败，如果此文件已经被打开，请关闭该文件后重试!", vbRetryCancel, "写入失败")
                    If Msgbox_No = vbCancel Then
                        Return False
                    End If
                End While
                Try
                    'ChDir(Application.StartupPath)
                    strwriter.WriteLine(str)
                    Return True
                Catch ex As Exception
                    Return False
                Finally
                    strwriter.Flush()
                    strwriter.Close()
                    strwriter.Dispose()
                End Try


            Else
                Using StrWriter As New StreamWriter(filepath, True)
                    Try
                        StrWriter.WriteLine(str)
                        Return True
                    Catch ex As Exception
                        Debug.Print("写入文件" & filepath & "出错,错误产生于函数FileWriteLine" & vbCrLf & ex.Message)
                        Return False
                    Finally
                        StrWriter.Flush()
                        StrWriter.Close()
                    End Try
                End Using
            End If

        End Function
        Public Function CheckFileOccupy(Path As String, ByRef Fs As StreamWriter) As Boolean
            Try
                Fs = New StreamWriter(Path, False, System.Text.Encoding.UTF8)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Function WriteLog(txt As String, LogPath As String, Optional isLineWriting As Boolean = True) As Boolean
            If Log_Enable = False Then Return True
            If LogPath = "" Then LogPath = Application.StartupPath & "\Log\Log.log"
            Dim r As Integer
            If isLineWriting Then
                FileWriteLine(txt, LogPath)
            Else
                FileWrite(txt, LogPath)
            End If

            If r Then Return True Else Return False
        End Function
    End Module
End Namespace
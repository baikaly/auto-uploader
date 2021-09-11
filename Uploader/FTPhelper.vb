Imports System.IO
Imports System.Net
Imports System.Text


Public Class FtpHelper
    Public Property ftpServerIP As String
    Public Property ftpCurFolder As String
    Public Property ftpUserID As String
    Public Property ftpPassword As String
    Public Property ftpURI As String
    Public Property ProjectName As String
    Public Property TransByte As Integer
    Public Property TransPercent As Integer
    Public Property ftpTimeout As Integer = 10000
    Public Property ftpKeepAlive As Boolean = False
    Dim LV As ListView

    ''' <summary>
    ''' 连接FTP
    ''' </summary>
    ''' <param name="ServerIP">FTP连接地址</param>
    ''' <param name="RemotePath">指定FTP连接成功后的当前目录, 如果不指定即默认为根目录</param>
    ''' <param name="UserID">用户名</param>
    ''' <param name="Password">密码</param>
    Public Sub New(ByVal ServerIP As String, ByVal RemotePath As String, ByVal UserID As String, ByVal Password As String, Optional ByRef obj As Object = Nothing)
        ftpServerIP = ServerIP
        ftpCurFolder = RemotePath
        ftpUserID = UserID
        ftpPassword = Password
        ftpURI = "ftp://" & ftpServerIP & "/" & ftpCurFolder & "/"
        If TypeOf obj Is ListView Then LV = CType(obj, ListView)
        System.Net.ServicePointManager.DefaultConnectionLimit = 50
    End Sub


    '''<summary>
    '''上传
    '''</summary>
    ''' <param name="filename"></param>
    Public Overloads Function Upload(ByVal filename As String) As Boolean
        Dim fileInf As FileInfo = New FileInfo(filename)
        Dim uri As String = ftpURI & fileInf.Name
        Dim reqFTP As FtpWebRequest
        reqFTP = CType(FtpWebRequest.Create(New Uri(uri)), FtpWebRequest)
        reqFTP.Credentials = New NetworkCredential(ftpUserID, ftpPassword)
        reqFTP.KeepAlive = ftpKeepAlive
        reqFTP.Timeout = ftpTimeout
        reqFTP.Method = WebRequestMethods.Ftp.UploadFile
        reqFTP.UseBinary = True
        reqFTP.UsePassive = False
        reqFTP.ContentLength = fileInf.Length
        Dim buffLength As Integer = 2048
        Dim buff As Byte() = New Byte(buffLength - 1) {}
        Dim contentLen As Integer
        Dim fs As FileStream = fileInf.OpenRead()

        Try
            Dim strm As Stream = reqFTP.GetRequestStream()
            contentLen = fs.Read(buff, 0, buffLength)

            While contentLen <> 0
                strm.Write(buff, 0, contentLen)
                contentLen = fs.Read(buff, 0, buffLength)
            End While

            strm.Close()
            fs.Close()
            Return True
        Catch ex As Exception
            Debug.Print("Ftphelper Upload Error --> " & ex.Message)
            Return False
        End Try
    End Function
    Public Overloads Function Upload(ByVal filename As String, ByVal row As Integer, ByVal col As Integer) As Boolean
        Dim fileInf As FileInfo = New FileInfo(filename)
        Dim uri As String = ftpURI & fileInf.Name
        Dim reqFTP As FtpWebRequest
        reqFTP = CType(FtpWebRequest.Create(New Uri(uri)), FtpWebRequest)
        reqFTP.Credentials = New NetworkCredential(ftpUserID, ftpPassword)
        reqFTP.KeepAlive = ftpKeepAlive
        reqFTP.Timeout = ftpTimeout
        'reqFTP.Proxy = Proxy
        reqFTP.Method = WebRequestMethods.Ftp.UploadFile
        reqFTP.UseBinary = True
        reqFTP.UsePassive = False
        reqFTP.ContentLength = fileInf.Length
        Dim buffLength As Integer = 2048
        Dim buff As Byte() = New Byte(buffLength - 1) {}
        Dim contentLen As Integer
        Dim fs As FileStream = fileInf.OpenRead()
        Dim strm As Stream = Nothing
        Try
            TransByte = 0
            TransPercent = 0
            Dim AllByte As Integer = fs.Length
            strm = reqFTP.GetRequestStream()
            contentLen = fs.Read(buff, 0, buffLength)

            While contentLen <> 0
                If Frm_Main.isContinue Then
                    strm.Write(buff, 0, contentLen)
                    contentLen = fs.Read(buff, 0, buffLength)
                    TransByte += contentLen
                    TransPercent = 100 * (TransByte / AllByte)
                    Frm_Main.ListView_WriteStr(LV, row, col, TransPercent.ToString & "%")
                    'Frm_Main.LV_Filelist.Items(row).SubItems(5).Text = TransPercent.ToString & "%"
                    'Application.DoEvents()
                Else
                    Return False        '操作取消
                End If

            End While
            Return True
        Catch ex As Exception
            Debug.Print("Ftphelper Upload Error --> " & ex.Message)
            Return False
        Finally
            If Not IsNothing(strm) Then strm.Close()
            If Not IsNothing(fs) Then fs.Close()
            reqFTP.Abort()
            'System.GC.Collect()
        End Try
    End Function
    Public Overloads Function Upload(ByVal filename As String, ByRef LV As ListView, ByVal r As Integer) As Boolean
        Dim fileInf As FileInfo = New FileInfo(filename)
        Dim uri As String = ftpURI & fileInf.Name
        Dim reqFTP As FtpWebRequest
        reqFTP = CType(FtpWebRequest.Create(New Uri(uri)), FtpWebRequest)
        reqFTP.Credentials = New NetworkCredential(ftpUserID, ftpPassword)
        reqFTP.KeepAlive = ftpKeepAlive
        reqFTP.Timeout = ftpTimeout
        reqFTP.Method = WebRequestMethods.Ftp.UploadFile
        reqFTP.UseBinary = True
        reqFTP.UsePassive = False
        reqFTP.ContentLength = fileInf.Length
        Dim buffLength As Integer = 2048
        Dim buff As Byte() = New Byte(buffLength - 1) {}
        Dim contentLen As Integer
        Dim fs As FileStream = fileInf.OpenRead()

        Try
            TransByte = 0
            TransPercent = 0
            Dim AllByte As Integer = fs.Length
            Dim strm As Stream = reqFTP.GetRequestStream()
            contentLen = fs.Read(buff, 0, buffLength)

            While contentLen <> 0
                strm.Write(buff, 0, contentLen)
                contentLen = fs.Read(buff, 0, buffLength)
                TransByte += contentLen
                TransPercent = 100 * (TransByte / AllByte)
                LV.Items(r).SubItems(5).Text = TransPercent.ToString & "%"
                Application.DoEvents()
            End While

            strm.Close()
            fs.Close()
            Return True
        Catch ex As Exception
            Debug.Print("Ftphelper Upload Error --> " & ex.Message)
            Return False
        End Try
    End Function
    
    ''' <summary>
    ''' 下载
    ''' </summary>
    ''' <param name="filePath"></param>
    ''' <param name="fileName"></param>
    Public Function Download(ByVal filePath As String, ByVal fileName As String) As Boolean
        Dim reqFTP As FtpWebRequest

        Try
            Dim outputStream As FileStream = New FileStream(filePath & "\" & fileName, FileMode.Create)
            reqFTP = CType(FtpWebRequest.Create(New Uri(ftpURI & fileName)), FtpWebRequest)
            reqFTP.Method = WebRequestMethods.Ftp.DownloadFile
            reqFTP.UseBinary = True

            reqFTP.UsePassive = False
            reqFTP.Timeout = ftpTimeout
            reqFTP.KeepAlive = ftpKeepAlive
            reqFTP.Credentials = New NetworkCredential(ftpUserID, ftpPassword)
            Dim response As FtpWebResponse = CType(reqFTP.GetResponse(), FtpWebResponse)
            Dim ftpStream As Stream = response.GetResponseStream()
            Dim cl As Long = response.ContentLength
            Dim bufferSize As Integer = 2048
            Dim readCount As Integer
            Dim buffer As Byte() = New Byte(bufferSize - 1) {}
            readCount = ftpStream.Read(buffer, 0, bufferSize)

            While readCount > 0
                outputStream.Write(buffer, 0, readCount)
                readCount = ftpStream.Read(buffer, 0, bufferSize)
            End While

            ftpStream.Close()
            outputStream.Close()
            response.Close()
            Return True
        Catch ex As Exception
            Debug.Print("FtpHelper Download Error --> " & ex.Message)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 删除文件
    ''' </summary>
    ''' <param name="fileName"></param>
    Public Function Delete(ByVal fileName As String) As Boolean
        Try
            Dim uri As String = ftpURI & fileName
            Dim reqFTP As FtpWebRequest
            reqFTP = CType(FtpWebRequest.Create(New Uri(uri)), FtpWebRequest)
            reqFTP.Credentials = New NetworkCredential(ftpUserID, ftpPassword)
            reqFTP.KeepAlive = ftpKeepAlive
            reqFTP.Timeout = ftpTimeout
            reqFTP.Method = WebRequestMethods.Ftp.DeleteFile
            reqFTP.UsePassive = False
            Dim result As String = String.Empty
            Dim response As FtpWebResponse = CType(reqFTP.GetResponse(), FtpWebResponse)
            Dim size As Long = response.ContentLength
            Dim datastream As Stream = response.GetResponseStream()
            Dim sr As StreamReader = New StreamReader(datastream)
            result = sr.ReadToEnd()
            sr.Close()
            datastream.Close()
            response.Close()
            Return True
        Catch ex As Exception
            Debug.Print("FtpHelper Delete Error --> " & ex.Message & "  文件名:" & fileName)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 删除文件夹
    ''' </summary>
    ''' <param name="folderName"></param>
    Public Function RemoveDirectory(ByVal folderName As String) As Boolean
        Try
            Dim uri As String = ftpURI & folderName
            Dim reqFTP As FtpWebRequest
            reqFTP = CType(FtpWebRequest.Create(New Uri(uri)), FtpWebRequest)
            reqFTP.Credentials = New NetworkCredential(ftpUserID, ftpPassword)
            reqFTP.KeepAlive = ftpKeepAlive
            reqFTP.Timeout = ftpTimeout
            reqFTP.Method = WebRequestMethods.Ftp.RemoveDirectory
            reqFTP.UsePassive = False
            Dim result As String = String.Empty
            Dim response As FtpWebResponse = CType(reqFTP.GetResponse(), FtpWebResponse)
            Dim size As Long = response.ContentLength
            Dim datastream As Stream = response.GetResponseStream()
            Dim sr As StreamReader = New StreamReader(datastream)
            result = sr.ReadToEnd()
            sr.Close()
            datastream.Close()
            response.Close()
            Return True
        Catch ex As Exception
            Debug.Print("FtpHelper Delete Error --> " & ex.Message & "  文件名:" & folderName)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 获取当前目录下明细(包含文件和文件夹)
    ''' </summary>
    ''' <returns></returns>
    Public Function GetFilesDetailList() As String()
        Try
            Dim result As StringBuilder = New StringBuilder()
            Dim ftp As FtpWebRequest
            ftp = CType(FtpWebRequest.Create(New Uri(ftpURI)), FtpWebRequest)
            ftp.Credentials = New NetworkCredential(ftpUserID, ftpPassword)
            ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails
            ftp.UsePassive = False

            ftp.Timeout = ftpTimeout
            ftp.KeepAlive = ftpKeepAlive
            Dim response As WebResponse = ftp.GetResponse()
            Dim reader As StreamReader = New StreamReader(response.GetResponseStream(), Encoding.UTF8)
            Dim line As String = reader.ReadLine()

            While line IsNot Nothing
                result.Append(line)
                result.Append(vbLf)
                line = reader.ReadLine()
            End While

            If result.Length > 0 Then
                result.Remove(result.ToString().LastIndexOf(vbLf), 1)
                reader.Close()
                response.Close()
                Return result.ToString().Split(vbLf)
            Else
                Return Nothing
            End If
            
        Catch ex As Exception
            Debug.Print("FtpHelper  Error --> " & ex.Message)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' 获取当前目录下文件列表(仅文件)
    ''' </summary>
    ''' <returns></returns>
    Public Function GetFileList(ByVal mask As String) As String()
        Dim downloadFiles As String()
        Dim result As StringBuilder = New StringBuilder()
        Dim reqFTP As FtpWebRequest

        Try
            reqFTP = CType(FtpWebRequest.Create(New Uri(ftpURI)), FtpWebRequest)
            reqFTP.UseBinary = True
            reqFTP.Credentials = New NetworkCredential(ftpUserID, ftpPassword)
            reqFTP.Method = WebRequestMethods.Ftp.ListDirectory
            reqFTP.UsePassive = False
            reqFTP.Timeout = ftpTimeout
            reqFTP.KeepAlive = ftpKeepAlive
            Dim response As WebResponse = reqFTP.GetResponse()
            Dim reader As StreamReader = New StreamReader(response.GetResponseStream(), Encoding.UTF8)
            Dim line As String = reader.ReadLine()

            While line IsNot Nothing

                If mask.Trim() <> String.Empty AndAlso mask.Trim() <> "*.*" Then
                    Dim mask_ As String = mask.Substring(0, mask.IndexOf("*"))

                    If line.Substring(0, mask_.Length) = mask_ Then
                        result.Append(line)
                        result.Append(vbLf)
                    End If
                Else
                    result.Append(line)
                    result.Append(vbLf)
                End If

                line = reader.ReadLine()
            End While

            result.Remove(result.ToString().LastIndexOf(vbLf), 1)
            reader.Close()
            response.Close()
            Return result.ToString().Split(vbLf)
        Catch ex As Exception
            downloadFiles = Nothing

            If ex.Message.Trim() <> "远程服务器返回错误: (550) 文件不可用(例如，未找到文件，无法访问文件)。" Then
                Debug.Print("FtpHelper GetFileList Error --> " & ex.Message.ToString())
            End If

            Return downloadFiles
        End Try
    End Function

    ''' <summary>
    ''' 获取当前目录下所有的文件夹列表(仅文件夹)
    ''' </summary>
    ''' <returns></returns>
    Public Function GetDirectoryList() As String()
        Dim drectory As String() = GetFilesDetailList()
        If IsNothing(drectory) Then Return Nothing
        Dim m As String = String.Empty

        For Each str As String In drectory
            Dim dirPos As Integer = str.IndexOf("<DIR>")

            If dirPos > 0 Then
                m += str.Substring(dirPos + 5).Trim() & vbLf
            ElseIf str.Trim().Substring(0, 1).ToUpper() = "D" Then
                'Dim dir As String = str.Substring(54).Trim()
                Dim dir As String = str.Substring(56).Trim()
                If dir <> "." AndAlso dir <> ".." Then
                    m += dir & vbLf
                End If
            End If
        Next

        Dim n As Char() = New Char() {vbLf}
        Return m.Split(n)
    End Function

    ''' <summary>
    ''' 判断当前目录下指定的子目录是否存在
    ''' </summary>
    ''' <param name="RemoteDirectoryName">指定的目录名</param>
    Public Function DirectoryExist(ByVal RemoteDirectoryName As String) As Boolean
        Dim dirList As String() = GetDirectoryList()
        If IsNothing(dirList) Then Return False
        For Each str As String In dirList

            If str.Trim() = RemoteDirectoryName.Trim() Then
                Return True
            End If
        Next

        Return False
    End Function

    ''' <summary>
    ''' 判断当前目录下指定的文件是否存在
    ''' </summary>
    ''' <param name="RemoteFileName">远程文件名</param>
    Public Function FileExist(ByVal RemoteFileName As String) As Boolean
        Dim fileList As String() = GetFileList("*.*")

        For Each str As String In fileList

            If str.Trim() = RemoteFileName.Trim() Then
                Return True
            End If
        Next

        Return False
    End Function

    ''' <summary>
    ''' 创建文件夹
    ''' </summary>
    ''' <param name="dirName"></param>
    Public Function MakeDir(ByVal dirName As String) As Boolean
        Dim reqFTP As FtpWebRequest

        Try
            Dim uri As String = ftpURI & dirName
            'uri = ftpURI & HttpUtility.UrlEncode(dirName)
            reqFTP = CType(FtpWebRequest.Create(New Uri(uri)), FtpWebRequest)
            reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory
            reqFTP.UseBinary = True
            reqFTP.UsePassive = False
            reqFTP.KeepAlive = ftpKeepAlive
            reqFTP.Timeout = ftpTimeout
            reqFTP.Credentials = New NetworkCredential(ftpUserID, ftpPassword)
            Dim response As FtpWebResponse = CType(reqFTP.GetResponse(), FtpWebResponse)
            Dim ftpStream As Stream = response.GetResponseStream()
            ftpStream.Close()
            response.Close()
            Return True
        Catch ex As Exception
            Debug.Print("FtpHelper MakeDir Error --> " & ex.Message)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 获取指定文件大小
    ''' </summary>
    ''' <param name="filename"></param>
    ''' <returns></returns>
    Public Function GetFileSize(ByVal filename As String) As Long
        Dim reqFTP As FtpWebRequest
        Dim fileSize As Long = 0

        Try
            reqFTP = CType(FtpWebRequest.Create(New Uri(ftpURI & filename)), FtpWebRequest)
            reqFTP.Method = WebRequestMethods.Ftp.GetFileSize
            reqFTP.UseBinary = True
            reqFTP.UsePassive = False
            reqFTP.KeepAlive = ftpKeepAlive
            reqFTP.Timeout = ftpTimeout
            reqFTP.Credentials = New NetworkCredential(ftpUserID, ftpPassword)
            Dim response As FtpWebResponse = CType(reqFTP.GetResponse(), FtpWebResponse)
            Dim ftpStream As Stream = response.GetResponseStream()
            fileSize = response.ContentLength
            ftpStream.Close()
            response.Close()
            Return fileSize
        Catch ex As Exception
            Debug.Print("FtpHelper GetFileSize Error --> " & ex.Message)
            Return 0
        End Try

        Return fileSize
    End Function

    ''' <summary>
    ''' 重命名
    ''' </summary>
    ''' <param name="currentFilename"></param>
    ''' <param name="newFilename"></param>
    Public Function ReName(ByVal currentFilename As String, ByVal newFilename As String) As Boolean
        Dim reqFTP As FtpWebRequest

        Try
            reqFTP = CType(FtpWebRequest.Create(New Uri(ftpURI & currentFilename)), FtpWebRequest)
            reqFTP.Method = WebRequestMethods.Ftp.Rename
            reqFTP.RenameTo = newFilename
            reqFTP.UseBinary = True
            reqFTP.UsePassive = False
            reqFTP.KeepAlive = ftpKeepAlive
            reqFTP.Timeout = ftpTimeout
            reqFTP.Credentials = New NetworkCredential(ftpUserID, ftpPassword)
            Dim response As FtpWebResponse = CType(reqFTP.GetResponse(), FtpWebResponse)
            Dim ftpStream As Stream = response.GetResponseStream()
            ftpStream.Close()
            response.Close()
            Return True
        Catch ex As Exception
            Debug.Print("FtpHelper ReName Error --> " & ex.Message)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 移动文件
    ''' </summary>
    ''' <param name="currentFilename"></param>
    ''' <param name="newDirectory"></param>
    Public Function MovieFile(ByVal currentFilename As String, ByVal newDirectory As String) As Boolean
        If ReName(currentFilename, newDirectory) Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' 切换当前目录
    ''' </summary>
    ''' <param name="DirectoryName"></param>
    ''' <param name="IsRoot">true 绝对路径   false 相对路径</param>
    Public Sub GotoDirectory(ByVal DirectoryName As String, ByVal IsRoot As Boolean)
        If IsRoot Then
            ftpCurFolder = DirectoryName
        Else
            ftpCurFolder += "/" & DirectoryName
        End If

        ftpURI = "ftp://" & ftpServerIP & "/" & ftpCurFolder & "/"
    End Sub




    ''' <summary>
    ''' 检查、创建和进入Project目录
    ''' </summary>
    ''' <param name="ProjectName">要应用的项目名称</param>
    Public Function CheckProjectDir(ProjectName As String) As Boolean
        Dim r As Boolean = True
        If Not DirectoryExist(ProjectName) Then
            r = MakeDir(ProjectName)
        End If
        GotoDirectory(ProjectName, False)
        If r Then Return True Else Return False
    End Function

    ''' <summary>
    ''' 删除订单目录
    ''' </summary>
    ''' <param name="ftpServerIP">FTP 主机地址</param>
    ''' <param name="folderToDelete">FTP 用户名</param>
    ''' <param name="ftpUserID">FTP 用户名</param>
    ''' <param name="ftpPassword">FTP 密码</param>
    Public Shared Sub DeleteOrderDirectory(ByVal ftpServerIP As String, ByVal folderToDelete As String, ByVal ftpUserID As String, ByVal ftpPassword As String)
        Try

            If Not String.IsNullOrEmpty(ftpServerIP) AndAlso Not String.IsNullOrEmpty(folderToDelete) AndAlso Not String.IsNullOrEmpty(ftpUserID) AndAlso Not String.IsNullOrEmpty(ftpPassword) Then
                Dim fw As FtpHelper = New FtpHelper(ftpServerIP, folderToDelete, ftpUserID, ftpPassword)
                fw.GotoDirectory(folderToDelete, True)
                Dim folders As String() = fw.GetDirectoryList()

                For Each folder As String In folders

                    If Not String.IsNullOrEmpty(folder) OrElse folder <> "" Then
                        Dim subFolder As String = folderToDelete & "/" & folder
                        fw.GotoDirectory(subFolder, True)
                        Dim files As String() = fw.GetFileList("*.*")

                        If files IsNot Nothing Then

                            For Each file As String In files
                                fw.Delete(file)
                            Next
                        End If

                        fw.GotoDirectory(folderToDelete, True)
                        fw.RemoveDirectory(folder)
                    End If
                Next

                Dim parentFolder As String = folderToDelete.Remove(folderToDelete.LastIndexOf("/"c))
                Dim orderFolder As String = folderToDelete.Substring(folderToDelete.LastIndexOf("/"c) + 1)
                fw.GotoDirectory(parentFolder, True)
                fw.RemoveDirectory(orderFolder)
            Else
                Throw New Exception("FTP 及路径不能为空！")
            End If

        Catch ex As Exception
            Throw New Exception("删除订单时发生错误，错误信息为：" & ex.Message)
        End Try
    End Sub
End Class

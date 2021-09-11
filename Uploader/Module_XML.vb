Imports System.Xml

Module Module_XML
    Public Class myXMLhelper
        Dim mXmlDoc As New XmlDocument
        Public xmlPath As String
        'reference https://www.cnblogs.com/leonliuyifan/p/7044438.html



        Public Sub New(ByVal xmlPath As String)
            If Not System.IO.File.Exists(xmlPath) Then
                MyClass.CreateXmlFile(xmlPath)
            End If
            MyClass.xmlPath = xmlPath
            MyClass.mXmlDoc.Load(MyClass.xmlPath)       '加载配置文件  
        End Sub

        ''' <summary>
        ''' 取得XML元素值
        ''' </summary>
        ''' <param name="node">节点</param>
        ''' <param name="element">元素名</param>
        ''' <returns>字符型元素值</returns>
        ''' <remarks>错误返回空值</remarks>
        Public Function GetElement(ByVal node As String, ByVal element As String) As String
            On Error GoTo Err
            Dim mXmlNode As System.Xml.XmlNode = mXmlDoc.SelectSingleNode("//" + node)

            '读数据  
            Dim xmlNode As System.Xml.XmlNode = mXmlNode.SelectSingleNode(element)
            Return xmlNode.InnerText.ToString
Err:
            Return ""
        End Function

        ''' <summary>
        ''' 修改XML元素值
        ''' </summary>
        ''' <param name="node">节点</param>
        ''' <param name="element">元素名</param>
        ''' <param name="val">值</param>
        ''' <returns>True--保存成功, False--保存失败</returns>
        ''' <remarks></remarks>
        Public Function UpdateElement(ByVal node As String, ByVal element As String, ByVal val As String) As Boolean
            Try
                Dim mXmlNode As System.Xml.XmlNode = mXmlDoc.SelectSingleNode("//" + node)
                Dim xmlNodeNew As System.Xml.XmlNode

                xmlNodeNew = mXmlNode.SelectSingleNode(element)
                xmlNodeNew.InnerText = val
                mXmlDoc.Save(MyClass.xmlPath)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function



        '创建
        Public Function CreateXmlFile(ByVal xmlPath As String)
            'Try
            '声明XML头部信息
            Dim dec As XmlDeclaration = mXmlDoc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
            '添加进mXmlDoc对象子节点
            mXmlDoc.AppendChild(dec)

            '创建根节点
            Dim root As XmlElement = mXmlDoc.CreateElement("AutoUpdater")
            mXmlDoc.AppendChild(root)

            '再创建根节点下的Updater子节点
            Dim Updater As XmlElement = mXmlDoc.CreateElement("Updater")
            'Updater.SetAttribute("SysID", "1")
            'Updater子节点下创建标记
            Dim dic1 = Frm_Main.CreateElementDic()
            For Each Setting In dic1
                Dim Element As XmlElement = mXmlDoc.CreateElement(Setting.Key)
                Dim Text As XmlText = mXmlDoc.CreateTextNode(Setting.Value)
                Element.AppendChild(Text)
                Updater.AppendChild(Element)
            Next
            root.AppendChild(Updater)

            '再创建根节点下的UpdateFileList子节点
            Dim UpdateFileList As XmlElement = mXmlDoc.CreateElement("UpdateFileList")
            'UpdateFileList子节点下创建标记
            Dim Filelist As New List(Of String)
            'Dim FileVersion As String = Frm_Main.TB_MainVersion.Text
            Dim FileVersion As New List(Of String)
            Filelist = Frm_Main.GetListViewFilelist
            FileVersion = Frm_Main.GetListViewFileVerlist

            Dim i As Integer
            For i = 0 To Filelist.Count - 1
                Dim Element As XmlElement = mXmlDoc.CreateElement("UpdateFile")
                Element.SetAttribute("Version", FileVersion(i))
                Element.SetAttribute("Name", Filelist(i))
                UpdateFileList.AppendChild(Element)
            Next

            'For Each file In Filelist
            '    Dim Element As XmlElement = mXmlDoc.CreateElement("UpdateFile")
            '    Element.SetAttribute("Version", FileVersion)
            '    Element.SetAttribute("Name", file)
            '    UpdateFileList.AppendChild(Element)
            'Next
            root.AppendChild(UpdateFileList)



            mXmlDoc.Save(xmlPath)
            Return True
            ' Catch ex As Exception
            'Return False
            'End Try


        End Function
    End Class


End Module

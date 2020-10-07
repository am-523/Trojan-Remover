Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Threading
Imports Microsoft.VisualBasic.CompilerServices
Imports Microsoft.Win32
''Project AM
'' YOUTUBE : https://www.youtube.com/channel/UCwI8AQlBewsdxbyk2r4n9CQ?view_as=subscriber
'' Am523
'' SUBSCRIBE :)
Public Class Form1
    Private ListThreats As String
    Private keyName0 As String
    Private keyName As String
    Private AnalyzeDone As String
    Private HCU As RegistryKey
    Private HLM As RegistryKey
    Private HCU0 As RegistryKey
    Private HLM0 As RegistryKey
    Private Hasil As String
    Public Delegate Sub _OK(Text As String)
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
        Interaction.MsgBox("Warning From Project AM" & vbCrLf & "The normal size of this tool is 214 KB" & vbCrLf & "If less or more watch out!", MsgBoxStyle.Information, Nothing)


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim saveFileDialog As SaveFileDialog = New SaveFileDialog()
        saveFileDialog.Filter = "DOC|*.rtf"
        saveFileDialog.FileName = "LOG.rtf"
        If saveFileDialog.ShowDialog() = DialogResult.OK Then
            Me.RichTextBox1.SaveFile(saveFileDialog.FileName)
            Interaction.MsgBox(saveFileDialog.FileName, MsgBoxStyle.Information, Nothing)
        End If
    End Sub

    Private Sub OpenFileLocationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenFileLocationToolStripMenuItem.Click
        Try
            For Each obj As Object In Me.ListView1.SelectedItems
                Dim listViewItem As ListViewItem = CType(obj, ListViewItem)
                Process.Start(listViewItem.SubItems(2).Text)
            Next
        Finally
            Dim enumerator As IEnumerator
            If TypeOf enumerator Is IDisposable Then
                TryCast(enumerator, IDisposable).Dispose()
            End If
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Button3.Enabled = False
        Try
            For Each obj As Object In Me.ListView1.Items
                Dim listViewItem As ListViewItem = CType(obj, ListViewItem)
                Dim thread As Thread = New Thread(Sub(a0 As Object)
                                                      Me.scanprocess(Conversions.ToString(a0))
                                                  End Sub)
                thread.Start(String.Concat(New String() {listViewItem.SubItems(2).Text, "\", listViewItem.Text, vbCrLf, listViewItem.SubItems(3).Text, vbCrLf, listViewItem.SubItems(4).Text}))
            Next
        Finally
            Dim enumerator As IEnumerator
            If TypeOf enumerator Is IDisposable Then
                TryCast(enumerator, IDisposable).Dispose()
            End If
        End Try
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        Try
            For Each obj As Object In Me.ListView1.SelectedItems
                Dim listViewItem As ListViewItem = CType(obj, ListViewItem)
                Dim thread As Thread = New Thread(Sub(a0 As Object)
                                                      Me.scanprocess(Conversions.ToString(a0))
                                                  End Sub)
                thread.Start(String.Concat(New String() {listViewItem.SubItems(2).Text, "\", listViewItem.Text, vbCrLf, listViewItem.SubItems(3).Text, vbCrLf, listViewItem.SubItems(4).Text}))
            Next
        Finally
            Dim enumerator As IEnumerator
            If TypeOf enumerator Is IDisposable Then
                TryCast(enumerator, IDisposable).Dispose()
            End If
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Me.Label1.Text = Me.Hasil
        Me.Label2.Text = "Malicious Files : " + Conversions.ToString(Me.ListView1.Items.Count)
    End Sub
    'Public Function STV(n As String, t As Object, typ As RegistryValueKind) As Boolean
    '    Dim result As Boolean
    '    Try
    '        Registry.CurrentUser.CreateSubKey("software\Microsoft\Internet Explorer").SetValue(n, RuntimeHelpers.GetObjectValue(t), typ)
    '        result = True
    '    Catch ex As Exception
    '        result = False
    '    End Try
    '    Return result
    'End Function
    'Public Function GTV(n As String, ret As Object) As Object
    '    Dim objectValue As Object
    '    Try
    '        objectValue = RuntimeHelpers.GetObjectValue(Registry.CurrentUser.OpenSubKey("software\Microsoft\Internet Explorer").GetValue(n, RuntimeHelpers.GetObjectValue(ret)))
    '    Catch ex As Exception
    '        objectValue = RuntimeHelpers.GetObjectValue(ret)
    '    End Try
    '    Return objectValue
    'End Function
    Public Function GetKey(key As String) As RegistryKey
        Dim result As RegistryKey
        If key.StartsWith(Registry.ClassesRoot.Name) Then
            Dim name As String = key.Replace(Registry.ClassesRoot.Name + "\", "")
            result = Registry.ClassesRoot.OpenSubKey(name, True)
        ElseIf key.StartsWith(Registry.CurrentUser.Name) Then
            Dim name As String = key.Replace(Registry.CurrentUser.Name + "\", "")
            result = Registry.CurrentUser.OpenSubKey(name, True)
        ElseIf key.StartsWith(Registry.LocalMachine.Name) Then
            Dim name As String = key.Replace(Registry.LocalMachine.Name + "\", "")
            result = Registry.LocalMachine.OpenSubKey(name, True)
        ElseIf key.StartsWith(Registry.Users.Name) Then
            Dim name As String = key.Replace(Registry.Users.Name + "\", "")
            result = Registry.Users.OpenSubKey(name, True)
        Else
            result = Nothing
        End If
        Return result
    End Function
    Public Function scanprocess(s As String) As Boolean
        Dim array As String() = Strings.Split(s, vbCrLf, -1, CompareMethod.Binary)
        Dim text As String = ""
        Dim value As String = ""
        Dim fileInfo As FileInfo = New FileInfo(array(0))
        For Each process As Process In Process.GetProcesses()
            Try
                If Operators.CompareString(process.MainModule.FileName.ToString(), fileInfo.FullName, False) = 0 Then
                    Try
                        value = Conversions.ToString(process.Id)
                    Catch ex As Exception
                    End Try
                End If
            Catch ex2 As Exception
            End Try
        Next
        Try
            Process.GetProcessById(Conversions.ToInteger(value)).Kill()
            Thread.Sleep(2000)
            If File.Exists(fileInfo.FullName) Then
                File.Delete(fileInfo.FullName)
            End If
        Catch ex3 As Exception
        End Try
        text = text + Conversions.ToString(DateAndTime.TimeOfDay) + vbCrLf & "++++++++++++++++++++++++++++++" & vbCrLf
        text = text + "Malicious Files : " + fileInfo.Name + vbCrLf
        If Not File.Exists(fileInfo.FullName) Then
            text = text + "File Delete: " + fileInfo.FullName + vbCrLf
        End If
        Try
            If File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\" + array(2) + fileInfo.Extension) Then
                Try
                    FileSystem.SetAttr(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\" + array(2) + fileInfo.Extension, CType(128, FileAttribute))
                Catch ex4 As Exception
                End Try
                Try
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\" + array(2) + fileInfo.Extension)
                Catch ex5 As Exception
                End Try
                text = String.Concat(New String() {text, "Startup File Deleted: ", array(2), fileInfo.Extension, vbCrLf})
            End If
        Catch ex6 As Exception
        End Try
        Try
            FileSystem.SetAttr(fileInfo.FullName, FileAttribute.Normal)
        Catch ex7 As Exception
        End Try
        Try
            Interaction.Shell("netsh firewall delete allowedprogram """ + fileInfo.FullName + """", AppWinStyle.Hide, False, -1)
        Catch ex8 As Exception
        End Try
        Try
            Me.GetKey(array(1)).DeleteValue(array(2), False)
            text = String.Concat(New String() {text, "Startup Key Deleted: ", array(1), "\", array(2), vbCrLf})
        Catch ex9 As Exception
        End Try
        Try
            Registry.CurrentUser.OpenSubKey("Software\", True).DeleteSubKeyTree(array(2))
            text = String.Concat(New String() {text, "Subkey Deleted: ", Registry.CurrentUser.ToString(), "\Software\", array(2), vbCrLf})
        Catch ex10 As Exception
        End Try
        text = text + "++++++++++++++++++++++++++++++" & vbCrLf & "[-----;------]" + array(2)
        Dim oThread As New _
            Thread(AddressOf Me.OK)
        oThread.Start()

        ' Me.Invoke(AddressOf Me.OK, New Object() {text})
        Me.Hasil = ""
        Dim result As Boolean
        Return result
    End Function
    Public Sub OK(s As String)
        Dim array As String() = Strings.Split(s, "[-----;------]", -1, CompareMethod.Binary)
        Try
            Me.ListView1.FindItemWithText(array(1)).Remove()
        Catch ex As Exception
        End Try
        Me.RichTextBox1.AppendText(array(0))
        Me.RichTextBox1.ScrollToCaret()
        If Me.RichTextBox1.TextLength > 0 Then
            Me.Button3.Enabled = True
        End If
        Me.ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
    End Sub

    Public Sub Scaning()
        Me.AnalyzeDone = ""
        Me.ListView1.Items.Clear()
        Dim num As Integer = 0
        Dim obj As Object = Nothing
        While True
IL_37B:
            obj = RuntimeHelpers.GetObjectValue(Me.HCU)
            While True
                Try
                    For Each obj2 As Object In CType(NewLateBinding.LateGet(obj, Nothing, "GetValueNames", New Object(-1) {}, Nothing, Nothing, Nothing), IEnumerable)
                        Dim objectValue As Object = RuntimeHelpers.GetObjectValue(obj2)
                        Try
                            Dim instance As Object = obj
                            Dim type As Type = Nothing
                            Dim memberName As String = "GetValue"
                            Dim array As Object() = New Object() {RuntimeHelpers.GetObjectValue(objectValue)}
                            Dim arguments As Object() = array
                            Dim argumentNames As String() = Nothing
                            Dim typeArguments As Type() = Nothing
                            Dim array2 As Boolean() = New Boolean() {True}
                            Dim value As Object = NewLateBinding.LateGet(instance, type, memberName, arguments, argumentNames, typeArguments, array2)
                            If array2(0) Then
                                objectValue = RuntimeHelpers.GetObjectValue(array(0))
                            End If
                            Dim num2 As Integer = Strings.InStr(Conversions.ToString(value), "..", CompareMethod.Binary)
                            If num2 <> 0 Then
                                Dim instance2 As Object = obj
                                Dim type2 As Type = Nothing
                                Dim memberName2 As String = "GetValue"
                                Dim array3 As Object() = New Object() {RuntimeHelpers.GetObjectValue(objectValue)}
                                Dim arguments2 As Object() = array3
                                Dim argumentNames2 As String() = Nothing
                                Dim typeArguments2 As Type() = Nothing
                                array2 = New Boolean() {True}
                                Dim obj3 As Object = NewLateBinding.LateGet(instance2, type2, memberName2, arguments2, argumentNames2, typeArguments2, array2)
                                If array2(0) Then
                                    objectValue = RuntimeHelpers.GetObjectValue(array3(0))
                                End If
                                Dim objectValue2 As Object = RuntimeHelpers.GetObjectValue(obj3)
                                objectValue2 = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue2, Nothing, "replace", New Object() {"""", ""}, Nothing, Nothing, Nothing), Nothing, "replace", New Object() {" ..", ""}, Nothing, Nothing, Nothing))
                                Dim fileInfo As FileInfo = New FileInfo(Conversions.ToString(objectValue2))
                                Me.ListThreats = Conversions.ToString(Operators.AddObject(Me.ListThreats, Operators.ConcatenateObject(Operators.ConcatenateObject(fileInfo.FullName + vbCrLf + obj.ToString() + vbCrLf, objectValue), vbCrLf)))
                                Dim listViewItem As ListViewItem = New ListViewItem()
                                If File.Exists(fileInfo.FullName) Or File.Exists(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\", objectValue), fileInfo.Extension))) Then
                                    Try
                                        Me.ImageList1.Images.Add(Conversions.ToString(objectValue), Icon.ExtractAssociatedIcon(fileInfo.FullName))
                                    Catch ex As Exception
                                    End Try
                                    listViewItem.ImageKey = Conversions.ToString(objectValue)
                                    listViewItem.Text = fileInfo.Name
                                    listViewItem.SubItems.Add(fileInfo.Extension)
                                    listViewItem.SubItems.Add(fileInfo.DirectoryName)
                                    listViewItem.SubItems.Add(obj.ToString())
                                    Dim subItems As Object = listViewItem.SubItems
                                    Dim type3 As Type = Nothing
                                    Dim memberName3 As String = "Add"
                                    array3 = New Object() {RuntimeHelpers.GetObjectValue(objectValue)}
                                    Dim arguments3 As Object() = array3
                                    Dim argumentNames3 As String() = Nothing
                                    Dim typeArguments3 As Type() = Nothing
                                    array2 = New Boolean() {True}
                                    NewLateBinding.LateCall(subItems, type3, memberName3, arguments3, argumentNames3, typeArguments3, array2, True)
                                    If array2(0) Then
                                        objectValue = RuntimeHelpers.GetObjectValue(array3(0))
                                    End If
                                    Me.ListView1.Items.Add(listViewItem)
                                End If
                            End If
                        Catch ex2 As Exception
                        End Try
                    Next
                Finally
                    Dim enumerator As IEnumerator
                    If TypeOf enumerator Is IDisposable Then
                        TryCast(enumerator, IDisposable).Dispose()
                    End If
                End Try
                num += 1
                If num >= 4 Then
                    GoTo IL_389
                End If
                obj = Nothing
                Select Case num
                    Case 0
                        GoTo IL_37B
                    Case 1
                        obj = RuntimeHelpers.GetObjectValue(Me.HLM)
                    Case 2
                        obj = RuntimeHelpers.GetObjectValue(Me.HCU0)
                    Case 3
                        obj = RuntimeHelpers.GetObjectValue(Me.HLM0)
                End Select
            End While
        End While
IL_389:
        If num = 4 Then
            Me.ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
            Interaction.MsgBox("Scanned Success  " & vbCrLf & "Malicious Files : " + Conversions.ToString(Me.ListView1.Items.Count), MsgBoxStyle.Information, Nothing)
            If Me.ListView1.Items.Count > 0 Then
                Me.Button2.Enabled = True
            Else
                Me.Button2.Enabled = False
            End If
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Scaning()
        Me.Timer1.Start()
    End Sub
End Class

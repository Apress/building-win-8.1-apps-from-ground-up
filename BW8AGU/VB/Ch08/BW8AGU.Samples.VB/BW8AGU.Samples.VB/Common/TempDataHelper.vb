Public NotInheritable Class TempDataHelper
    Private Sub New()
    End Sub
    Public Shared Async Sub SaveTempData(data As String)
        Dim tempFile As StorageFile =
            Await ApplicationData.Current.TemporaryFolder.CreateFileAsync(
                "temporary.txt", CreationCollisionOption.ReplaceExisting)
        Await FileIO.WriteTextAsync(tempFile, data)
    End Sub

    Public Shared Async Function GetTempData() As Task(Of [String])
        Try
            Dim tempFile As StorageFile =
                Await ApplicationData.Current.TemporaryFolder.GetFileAsync("temporary.txt")
            Dim content As [String] = Await FileIO.ReadTextAsync(tempFile)
            Return content
        Catch
            Throw New Exception("Data not found!")
        End Try
    End Function
End Class


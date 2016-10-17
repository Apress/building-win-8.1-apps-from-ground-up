Public NotInheritable Class VersionHelper
    Private Sub New()
    End Sub
    Public Shared Sub CheckVersion()
        Dim version As UInteger = ApplicationData.Current.Version

        Select Case version
            Case 0
                'Need to upgrade data from v0 to v1
                Upgrade_Version0_to_Version1()
                Exit Select
            Case 1
                'Right version, do nothing...
                Exit Select
            Case Else
                Throw New Exception("Unexpected ApplicationData Version: " + version)
        End Select
    End Sub

    Private Shared Async Sub Upgrade_Version0_to_Version1()
        Await ApplicationData.Current.SetVersionAsync(1,
                                                      New ApplicationDataSetVersionHandler(AddressOf SetVersion1Handler))
    End Sub

    Private Shared Sub SetVersion1Handler(setVersionRequest As SetVersionRequest)
        Dim deferral As SetVersionDeferral = setVersionRequest.GetDeferral()

        'Change the data format for all needed settings
        ApplicationData.Current.LocalSettings.Values("LocalData") = "this-is-a-new-data-format"

        deferral.Complete()
    End Sub

    Public Shared ReadOnly Property AppVersion() As UInteger
        Get
            Return ApplicationData.Current.Version
        End Get
    End Property
End Class

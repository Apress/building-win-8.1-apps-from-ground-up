Imports Windows.Security.Credentials

Public Class PasswordVaultHelper
    Private Shared vault As PasswordVault = New Windows.Security.Credentials.PasswordVault()

    Public Shared Function LoadPasswordVault() As IList(Of PasswordCredential)
        'Load all credentials
        Dim creds As IReadOnlyList(Of PasswordCredential) = vault.RetrieveAll()

        Return creds.ToList()
    End Function

    Public Shared Function CheckPassword(username As String, password As String) As Boolean
        Dim res As Boolean = False

        Try
            Dim creds As IReadOnlyList(Of PasswordCredential) = vault.FindAllByUserName(username)

            For Each cred As PasswordCredential In creds
                cred.RetrievePassword()
                res = cred.Password = password
            Next
            'do nothing
        Catch
        End Try

        Return res
    End Function

    Public Shared Sub SavePasswordToVault(username As String, password As String)
        LoadPasswordVault()

        Dim cred As New PasswordCredential("BW8AGU", username, password)

        vault.Add(cred)
    End Sub
End Class


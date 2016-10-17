Imports Microsoft.WindowsAzure.MobileServices

Public NotInheritable Class AzureMobileServicesHelper
    Private Sub New()
    End Sub
    Public Shared Async Sub InsertDataFromAzureMobileSvc(item As SimpleAppointment)
        Await App.bw8agu_mobileserviceClient.GetTable(Of SimpleAppointment)().InsertAsync(item)
    End Sub

    Public Shared Async Function GetDataFromAzureMobileSvc() As Task(Of MobileServiceCollection(Of SimpleAppointment, SimpleAppointment))
        Try
            Dim table As IMobileServiceTable(Of SimpleAppointment) = App.bw8agu_mobileserviceClient.GetTable(Of SimpleAppointment)()

            Return Await table.ToCollectionAsync()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Async Sub UpdateDataFromAzureMobileSvc(item As SimpleAppointment)
        Try
            Dim table As IMobileServiceTable(Of SimpleAppointment) =
                App.bw8agu_mobileserviceClient.GetTable(Of SimpleAppointment)()

            Await table.UpdateAsync(item)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class



Partial Public Class FakeServiceClient
    Inherits System.ServiceModel.ClientBase(Of FakeServiceReference.IFakeService)
    Implements FakeServiceReference.IFakeService

    ' Implementation is moved after the FakeServiceClient instantiation because partial method must have empty body
    Partial Private Shared Sub ConfigureEndpoint(ByVal serviceEndpoint As System.ServiceModel.Description.ServiceEndpoint,
                                                 ByVal clientCredentials As System.ServiceModel.Description.ClientCredentials)
    End Sub

    Public Function GetSimpleAppointmentAsync() As Task(Of FakeServiceReference.SimpleAppointment) Implements FakeServiceReference.IFakeService.GetSimpleAppointmentAsync
        Return MyBase.Channel.GetSimpleAppointmentAsync
    End Function
End Class

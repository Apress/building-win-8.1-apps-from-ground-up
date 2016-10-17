Public Class SimpleAppointment
    Public Property Id() As Integer
        Get
            Return m_Id
        End Get
        Set(value As Integer)
            m_Id = value
        End Set
    End Property
    Private m_Id As Integer
    Public Property Subject() As String
        Get
            Return m_Subject
        End Get
        Set(value As String)
            m_Subject = Value
        End Set
    End Property
    Private m_Subject As String
    Public Property Location() As String
        Get
            Return m_Location
        End Get
        Set(value As String)
            m_Location = Value
        End Set
    End Property
    Private m_Location As String
    Public Property StartDate() As DateTime
        Get
            Return m_StartDate
        End Get
        Set(value As DateTime)
            m_StartDate = Value
        End Set
    End Property
    Private m_StartDate As DateTime
    Public Property EndDate() As DateTime
        Get
            Return m_EndDate
        End Get
        Set(value As DateTime)
            m_EndDate = Value
        End Set
    End Property
    Private m_EndDate As DateTime
End Class

Namespace Common

    ''' <summary>
    ''' SuspensionManager captures global session state to simplify process lifetime management
    ''' for an application.  Note that session state will be automatically cleared under a variety
    ''' of conditions and should only be used to store information that would be convenient to
    ''' carry across sessions, but that should be discarded when an application crashes or is
    ''' upgraded.
    ''' </summary>
    Friend NotInheritable Class SuspensionManager
        Public Shared Async Function SaveAsync() As Task
            Try
                'Save everything in here
            Catch ex As Exception
                Throw New SuspensionManagerException(ex)
            End Try
        End Function

        Public Shared Async Function RestoreAsync() As Task
            Try
                'Restore everything in here
            Catch ex As Exception
                Throw New SuspensionManagerException(ex)
            End Try
        End Function
    End Class

    Public Class SuspensionManagerException
        Inherits Exception
        Public Sub New()
        End Sub

        Public Sub New(ByRef e As Exception)
            MyBase.New("SuspensionManager failed", e)
        End Sub
    End Class
End Namespace

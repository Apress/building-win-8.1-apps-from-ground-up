Imports Windows.Web.Syndication

Public NotInheritable Class RssHelper
    Private Sub New()
    End Sub
    Public Shared Async Function GetFeedTitleAsync() As Task(Of String)
        Dim response As String = [String].Empty
        Dim feed As New SyndicationFeed()
        Dim client As New SyndicationClient()

        Try
            feed = Await client.RetrieveFeedAsync(New Uri("http://www.apress.com/index.php/dailydeals/index/rss"))

            response = feed.GetXmlDocument(SyndicationFormat.Rss20).GetXml()
        Catch ex As Exception
            Dim status As SyndicationErrorStatus = SyndicationError.GetStatus(ex.HResult)
            If status = SyndicationErrorStatus.InvalidXml Then
                response += "Invalid XML!"
            End If

            If status = SyndicationErrorStatus.Unknown Then
                response = ex.Message
            End If
        End Try

        Return response
    End Function
End Class

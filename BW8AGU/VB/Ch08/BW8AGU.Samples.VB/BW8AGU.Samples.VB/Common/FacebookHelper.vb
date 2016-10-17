Imports Windows.Security.Authentication.Web
Imports System.Net.Http

Public NotInheritable Class FacebookHelper
    Private Sub New()
    End Sub
    Private Const _appID As String = "<your_app_id>"
    'retrieved from Facebook app page
    Private Const _url As String = "https://www.facebook.com/connect/login_success.html"
    Private Const _wallUrl As String = "https://graph.facebook.com/<your_facebook_username>/feed"
    Private Shared _accessToken As String = [String].Empty
    Private Shared _urlAccessToken As String = [String].Empty

    Public Shared Async Function Authenticate() As Task(Of Boolean)
        Dim res As Boolean = False

        Try
            Dim FacebookURL As [String] = "https://www.facebook.com/dialog/oauth?client_id=" +
                Uri.EscapeDataString(_appID) + "&redirect_uri=" + Uri.EscapeDataString(_url) +
                "&scope=read_stream,user_about_me,read_stream," +
                "publish_stream&display=popup&response_type=token"

            Dim requestUri As System.Uri = New Uri(FacebookURL)
            Dim callbackUri As System.Uri = New Uri(_url)

            Dim WebAuthenticationResult As WebAuthenticationResult =
                Await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, requestUri, callbackUri)

            If WebAuthenticationResult.ResponseStatus = WebAuthenticationStatus.Success Then
                _urlAccessToken = WebAuthenticationResult.ResponseData.ToString()
                _accessToken = _urlAccessToken.Substring(_urlAccessToken.IndexOf("="c) + 1)
            ElseIf WebAuthenticationResult.ResponseStatus = WebAuthenticationStatus.ErrorHttp Then
                Throw New Exception("HTTP Error: " + WebAuthenticationResult.ResponseErrorDetail.ToString())
            Else
                Throw New Exception("Error: " + WebAuthenticationResult.ResponseStatus.ToString())
            End If

            res = True
            'log the error somewhere
        Catch [Error] As Exception
        End Try

        Return res
    End Function

    Public Shared Async Function PostOnFacebook(text As String) As Task(Of String)
        Dim res As String = [String].Empty

        Try
            Dim client = New HttpClient()

            ' Create the HttpContent for the form to be posted.
			Dim requestContent = New FormUrlEncodedContent(New () {
			                                               New KeyValuePair(Of String, String)("message", text)})

            ' Get the response.
            Dim response As HttpResponseMessage = Await client.PostAsync(
                Convert.ToString("https://graph.facebook.com/<your_facebook_username>/feed?access_token=") & _accessToken, requestContent)

            ' Get the response content.
            Dim responseContent As HttpContent = response.Content

            ' Get the stream of the content.
            Using reader = New StreamReader(Await responseContent.ReadAsStreamAsync())
                ' Write the output.
                res = reader.ReadToEnd()
            End Using
        Catch ex As Exception
            'log the error somewhere
            res = ex.Message
        End Try

        Return res
    End Function
End Class


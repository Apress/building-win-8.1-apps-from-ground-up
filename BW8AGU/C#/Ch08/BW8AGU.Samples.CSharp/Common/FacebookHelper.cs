using BW8AGU.Samples.CSharp.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store;
using Windows.Security.Authentication.Web;

namespace BW8AGU.Samples.CSharp.Common
{
    public static class FacebookHelper
    {
        private const string _appID = "<your_app_id>"; //retrieved from Facebook app page
        private const string _url = "https://www.facebook.com/connect/login_success.html";
        private const string _wallUrl = "https://graph.facebook.com/<your_facebook_username>/feed";
        private static string _accessToken = String.Empty;
        private static string _urlAccessToken = String.Empty;

        public static async Task<bool> Authenticate()
        {
            bool res = false;

            try
            {
                String FacebookURL = "https://www.facebook.com/dialog/oauth?client_id=" +
                    Uri.EscapeDataString(_appID) +
                    "&redirect_uri=" + Uri.EscapeDataString(_url) +
                    "&scope=read_stream,user_about_me,read_stream," +
                    "publish_stream&display=popup&response_type=token";

                System.Uri requestUri = new Uri(FacebookURL);
                System.Uri callbackUri = new Uri(_url);

                WebAuthenticationResult WebAuthenticationResult =
                    await WebAuthenticationBroker.AuthenticateAsync(
                                            WebAuthenticationOptions.None,
                                            requestUri,
                                            callbackUri);

                if (WebAuthenticationResult.ResponseStatus ==
                    WebAuthenticationStatus.Success)
                {
                    _urlAccessToken = WebAuthenticationResult.ResponseData.ToString();
                    _accessToken = _urlAccessToken.Substring(_urlAccessToken.IndexOf('=') + 1);
                }
                else if (WebAuthenticationResult.ResponseStatus ==
                    WebAuthenticationStatus.ErrorHttp)
                {
                    throw new Exception("HTTP Error: " +
                        WebAuthenticationResult.ResponseErrorDetail.ToString());
                }
                else
                {
                    throw new Exception("Error: " +
                        WebAuthenticationResult.ResponseStatus.ToString());
                }

                res = true;
            }
            catch (Exception Error)
            {
                //log the error somewhere
            }

            return res;
        }

        public static async Task<string> PostOnFacebook(string text)
        {
            string res = String.Empty;

            try
            {
                var client = new HttpClient();

                // Create the HttpContent for the form to be posted.
                var requestContent = new FormUrlEncodedContent(new[] {
                        new KeyValuePair<string, string>("message", text)
                    });

                // Get the response.
                HttpResponseMessage response = await client.PostAsync(
                    "https://graph.facebook.com/<your_facebook_username>/feed?access_token=" + _accessToken,
                    requestContent);

                // Get the response content.
                HttpContent responseContent = response.Content;

                // Get the stream of the content.
                using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
                {
                    // Write the output.
                    res = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                //log the error somewhere
                res = ex.Message; 
            }

            return res;
        }
    }
}



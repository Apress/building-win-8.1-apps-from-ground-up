using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Syndication;

namespace BW8AGU.Samples.CSharp.Common
{
    public static class RssHelper
    {
        public async static Task<string> GetFeedTitleAsync()
        {
            string response = String.Empty;
            SyndicationFeed feed = new SyndicationFeed();
            SyndicationClient client = new SyndicationClient();

            try
            {
                feed = await client.RetrieveFeedAsync(
                    new Uri("http://www.apress.com/index.php/dailydeals/index/rss"));

                response = feed.GetXmlDocument(SyndicationFormat.Rss20).GetXml();
            }
            catch (Exception ex)
            {
                SyndicationErrorStatus status = SyndicationError.GetStatus(ex.HResult);
                if (status == SyndicationErrorStatus.InvalidXml)
                {
                    response += "Invalid XML!";
                }

                if (status == SyndicationErrorStatus.Unknown)
                {
                    response = ex.Message;
                }
            }

            return response;
        }
    }
}

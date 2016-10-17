using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleBackgroundTask
{
    internal static class MessagesDownloader
    {
        public static Task<List<string>> GetMessagesAsync()
        {
            return Task<List<string>>.Run(() =>
            {
                List<string> messages = new List<string>();
                for (int i = 0; i < 20; i++)
                {
                    messages.Add(string.Format("This is message {0}", i));
                }
                return messages;
            });
        }
    }
}

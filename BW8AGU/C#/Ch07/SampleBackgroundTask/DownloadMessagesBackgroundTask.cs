using Windows.ApplicationModel.Background;

namespace SampleBackgroundTask
{
    public sealed class DownloadMessagesBackgroundTask : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();
            await MessagesDownloader.GetMessagesAsync();
            deferral.Complete();
        }
    }
}

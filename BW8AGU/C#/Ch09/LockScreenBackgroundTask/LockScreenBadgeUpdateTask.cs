using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace LockScreenBackgroundTask
{
    public sealed class LockScreenBadgeUpdateTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();
            
            Random randomizer = new Random();
            var template = BadgeUpdateManager.GetTemplateContent(BadgeTemplateType.BadgeNumber);
            (template.GetElementsByTagName("badge")[0] as XmlElement).SetAttribute("value", randomizer.Next(1,99).ToString());
            BadgeUpdater updater = BadgeUpdateManager.CreateBadgeUpdaterForApplication();
            updater.Update(new BadgeNotification(template));

            deferral.Complete();
        }
    }
}

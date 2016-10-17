using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace BW8AGU.Samples.CSharp.Common
{
    public static class VersionHelper
    {
        public static void CheckVersion()
        {
            uint version = ApplicationData.Current.Version;

            switch (version)
            {
                case 0:
                    //Need to upgrade data from v0 to v1
                    Upgrade_Version0_to_Version1();
                    break;
                case 1:
                    //Right version, do nothing...
                    break;
                default:
                    throw new Exception("Unexpected ApplicationData Version: " + version);
            }
        }

        static async void Upgrade_Version0_to_Version1()
        {
            await ApplicationData.Current.SetVersionAsync(1,
                new ApplicationDataSetVersionHandler(SetVersion1Handler));
        }

        private static void SetVersion1Handler(SetVersionRequest setVersionRequest)
        {
            SetVersionDeferral deferral = setVersionRequest.GetDeferral();

            //Change the data format for all needed settings
            ApplicationData.Current.LocalSettings.Values["LocalData"] = "this-is-a-new-data-format";

            deferral.Complete();
        }

        public static uint AppVersion
        {
            get { return ApplicationData.Current.Version; }
        }

    }
}

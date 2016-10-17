using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW8AGU.Samples.CSharp.Helpers
{
        public class ApplicationSettingsHelper
        {
            private static ApplicationSettingsHelper instance = new ApplicationSettingsHelper();

            private ApplicationSettingsHelper()
            {
                AvailableLanguages =
                    new ObservableCollection<CultureInfo>
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("it-IT"),
                };
            }

            public static ApplicationSettingsHelper Instance
            {
                get
                {
                    return instance;
                }
            }

            public ObservableCollection<CultureInfo> AvailableLanguages { get; set; }

            public CultureInfo CurrentCulture
            {
                //Read Settings in Get
                get;
                //Save Settings in Set
                set;
            }
        }
}

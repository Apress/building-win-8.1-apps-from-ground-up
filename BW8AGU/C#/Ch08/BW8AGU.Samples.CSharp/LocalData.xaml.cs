using BW8AGU.Samples.CSharp.Common;
using BW8AGU.Samples.CSharp.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Item Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234232

namespace BW8AGU.Samples.CSharp
{
    /// <summary>
    /// A page that displays details for a single item within a group.
    /// </summary>
    public sealed partial class LocalData : Page
    {
        #region Local Data Sample
        public CultureInfo CurrentCulture
        {
            get
            {
                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                CultureInfo culture;

                if (localSettings.Containers.ContainsKey("localization"))
                {
                    var cultureName = localSettings.Containers["localization"].Values["language"] as String;
                    culture = new CultureInfo(cultureName);
                }
                else
                {
                    culture = CultureInfo.CurrentCulture;
                }

                return culture;
            }
            set
            {
                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

                if (!localSettings.Containers.ContainsKey("localization"))
                {
                    //Create a new container
                    localSettings.CreateContainer("localization", Windows.Storage.ApplicationDataCreateDisposition.Always);
                }

                localSettings.Containers["localization"].Values["language"] = value.Name;
            }
        }

        public CultureInfo CurrentCultureDetailed
        {
            get
            {
                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

                if (localSettings.Containers.ContainsKey("detailedLocalization"))
                {
                    ApplicationDataCompositeValue composite =
                        (ApplicationDataCompositeValue)localSettings.Containers["detailedLocalization"].Values["language"];

                    return (composite != null) ? new CultureInfo(composite["selectedLanguage"].ToString())
                        : CultureInfo.CurrentCulture;
                }
                else
                {
                    return new CultureInfo("en-US");
                }
            }
            set
            {
                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

                if (!localSettings.Containers.ContainsKey("detailedLocalization"))
                {
                    //Create a container
                    localSettings.CreateContainer("detailedLocalization",
                        Windows.Storage.ApplicationDataCreateDisposition.Always);
                }

                ApplicationDataCompositeValue composite = new ApplicationDataCompositeValue();
                composite["selectedLanguage"] = value.DisplayName;
                composite["lastChangeTime"] = DateTime.Now.ToString();

                localSettings.Containers["detailedLocalization"].Values["language"] = composite;
            }
        }

        protected void GetCompositeData()
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            if (localSettings.Containers.ContainsKey("detailedLocalization"))
            {
                ApplicationDataCompositeValue composite =
                    (ApplicationDataCompositeValue)localSettings.Containers["detailedLocalization"].Values["language"];

                LocalSettingsCompositeLanguage.Text = composite["selectedLanguage"].ToString();
                LocalSettingsCompositeLastTimeChanged.Text = composite["lastChangeTime"].ToString();
            }
        }

        #endregion


        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        public LocalData()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;

            //Local data - Single value
            LocalSettingsSingleValue.Text = CurrentCulture.DisplayName;

            //Local data - Composite data 
            CurrentCultureDetailed = new CultureInfo("en-US");
            GetCompositeData();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            // TODO: Create an appropriate data model for your problem domain to replace the sample data
            var item = await SampleDataSource.GetItemAsync((String)e.NavigationParameter);
            this.DefaultViewModel["Item"] = item;
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
    }
}
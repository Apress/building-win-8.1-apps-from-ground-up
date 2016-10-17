using Bing.Maps;
using Windows.Devices.Geolocation;
using System;
using BW8AGU.Samples.CSharp.Common;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace BW8AGU.Samples.CSharp
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class GeoLocationSample : Page
    {
        private Geolocator locator = null;
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public GeoLocationSample()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
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

        private void OnGetPositionButtonClick(object sender, RoutedEventArgs e)
        {
            InitializeGeolocator();
        }

        private async void InitializeGeolocator()
        {
            if (locator == null)
            {
                locator = new Geolocator();
                locator.DesiredAccuracyInMeters = 2;
                locator.MovementThreshold = 1;
                locator.PositionChanged += OnLocatorPositionChanged;
                var actualPosition = await locator.GetGeopositionAsync();
                locator.StatusChanged += OnStatusChanged;
            }
        }

        private void OnStatusChanged(Geolocator sender, StatusChangedEventArgs args)
        {
            switch (args.Status)
            {
                case PositionStatus.Disabled:
                    //Location service are disabled consider to disable every functionality depends on it
                    break;
                case PositionStatus.Initializing:
                    //the systems is initialiazing the locations services, for this reason, wait until ready
                    break;
                case PositionStatus.NoData:
                    //unable to find data about the user position
                    break;
                case PositionStatus.NotAvailable:
                    //location services are not available in this moment
                    break;
                case PositionStatus.NotInitialized:
                    //geolocator not initialized because you don’t make a request 
                    break;
                case PositionStatus.Ready:
                    //geolocator is ready to provide data
                    break;
            }

        }

        private async void OnLocatorPositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            DefaultViewModel["Coordinate"] = args.Position.Coordinate;
            await MapControl.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
            () =>
            {
                MapControl.Center = new Bing.Maps.Location
                {
                    Longitude = args.Position.Coordinate.Point.Position.Longitude,
                    Latitude = args.Position.Coordinate.Point.Position.Latitude,
                };
                Pushpin pin = new Bing.Maps.Pushpin();
                pin.SetValue(Bing.Maps.MapLayer.PositionProperty, new Bing.Maps.Location(MapControl.Center));
                MapControl.Children.Clear();
                MapControl.Children.Add(pin);
                
                MapControl.SetZoomLevel(10,TimeSpan.FromSeconds(1));
            });
        }

    }
}

using BW8AGU.Samples.CSharp.Common;
using BW8AGU.Samples.CSharp.Helpers;
using BW8AGU.Samples.CSharp.Search;
using BW8AGU.Samples.CSharp.SettingsPanels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Search;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Hub App template is documented at http://go.microsoft.com/fwlink/?LinkId=286574

namespace BW8AGU.Samples.CSharp
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton Application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active

            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();
                //Associate the frame with a SuspensionManager key                                
                SuspensionManager.RegisterFrame(rootFrame, "AppFrame");

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // Restore the saved session state only when appropriate
                    try
                    {
                        await SuspensionManager.RestoreAsync();
                    }
                    catch (SuspensionManagerException)
                    {
                        //Something went wrong restoring state.
                        //Assume there is no state and continue
                    }
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
                SearchPane.GetForCurrentView().SuggestionsRequested += OnSearchSuggestionRequested;
                //SearchPane.GetForCurrentView().QuerySubmitted += OnSearchQuerySubmitted;
            }
            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof(HubPage)))
                {
                    throw new Exception("Failed to create initial page");
                }
            }
            InitSettingsPane();
            // Ensure the current window is active
            Window.Current.Activate();
        }
        
        #region Search Contract

        private void OnSearchQuerySubmitted(SearchPane sender, SearchPaneQuerySubmittedEventArgs args)
        {
            //search in your app here
        }

        private async void OnSearchSuggestionRequested(SearchPane sender, SearchPaneSuggestionsRequestedEventArgs args)
        {
            var deferral = args.Request.GetDeferral();
            var suggestions = await SuggestionProvider.GetSuggestionsAsync(args.QueryText);
            args.Request.SearchSuggestionCollection.AppendQuerySuggestions(suggestions);
            deferral.Complete();
        }

        protected async override void OnSearchActivated(Windows.ApplicationModel.Activation.SearchActivatedEventArgs args)
        {
            var previousContent = Window.Current.Content;
            var frame = previousContent as Frame;

            // If the app does not contain a top-level frame, it is possible that this 
            // is the initial launch of the app. Typically this method and OnLaunched 
            // in App.xaml.cs can call a common method.
            if (frame == null)
            {
                // Create a Frame to act as the navigation context and associate it with
                // a SuspensionManager key
                frame = new Frame();
                SuspensionManager.RegisterFrame(frame, "AppFrame");

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // Restore the saved session state only when appropriate
                    try
                    {
                        await SuspensionManager.RestoreAsync();
                    }
                    catch (SuspensionManagerException)
                    {
                        //Something went wrong restoring state.
                        //Assume there is no state and continue
                    }
                }
            }

            frame.Navigate(typeof(SearchResultsPage), args.QueryText);
            Window.Current.Content = frame;

            // Ensure the current window is active
            Window.Current.Activate();
        }


        #endregion

        #region Settings Contract

        private SettingsPane settingsPane = null;

        private void InitSettingsPane()
        {
            settingsPane = Windows.UI.ApplicationSettings.SettingsPane.GetForCurrentView();
            settingsPane.CommandsRequested += OnSettingsPaneCommandsRequested;
        }

        private void OnSettingsPaneCommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            args.Request.ApplicationCommands.Clear();
            args.Request.ApplicationCommands.Add(
                new SettingsCommand(
                    "applicationCommand",
                    "General Settings",
                    ApplicationCommandInvokedHandler));
        }

        private void ApplicationCommandInvokedHandler(IUICommand command)
        {
            ApplicationSettings content =
                new ApplicationSettings
                {
                    Width = 346,
                    Height = Window.Current.Bounds.Height
                };
            Popup settingsPopup = SettingsPanelHelper.CreateSettingsPanel(content, Window.Current.Bounds);
            settingsPopup.IsOpen = true;
        }



        #endregion

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            await SuspensionManager.SaveAsync();
            deferral.Complete();
        }
    }
}

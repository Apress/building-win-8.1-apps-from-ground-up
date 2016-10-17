using BW8AGU.Samples.CSharp.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace BW8AGU.Samples.CSharp.SettingsPanels
{
    public sealed partial class ApplicationSettings : UserControl
    {
        public ApplicationSettings()
        {
            this.InitializeComponent();
        }


        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            languageCombobox.ItemsSource = ApplicationSettingsHelper.Instance.AvailableLanguages;
            languageCombobox.SelectedItem = ApplicationSettingsHelper.Instance.CurrentCulture;
            languageCombobox.SelectionChanged += OnLanguageSelectionChanged;
        }

        private void OnLanguageSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Set the language immediatly (from Settings Charm guidelines)
            ApplicationSettingsHelper.Instance.CurrentCulture = e.AddedItems.First() as CultureInfo;
        }
        private void OnApplicationSettingsBackButtonClick(object sender, RoutedEventArgs e)
        {
            Popup parent = this.Parent as Popup;
            if (parent != null)
            {
                parent.IsOpen = false;
                Windows.UI.ApplicationSettings.SettingsPane.Show();
            }
        }

    }
}

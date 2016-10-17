using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media.Animation;

namespace BW8AGU.Samples.CSharp.Helpers
{
    public class SettingsPanelHelper
    {
        public static Popup CreateSettingsPanel(UserControl content, Rect bounds, double settingsSize = 346)
        {
            Popup p = new Popup
            {
                IsLightDismissEnabled = true,
                Width = settingsSize,
                Height = bounds.Height
            };
            p.ChildTransitions = new TransitionCollection
            {
                new PaneThemeTransition
                {
                    Edge = SettingsPane.Edge == SettingsEdgeLocation.Right
                    ? EdgeTransitionLocation.Right
                    : EdgeTransitionLocation.Left
                }
            };
            p.SetValue(Canvas.LeftProperty, SettingsPane.Edge == SettingsEdgeLocation.Right ? (bounds.Width - settingsSize) : 0);

            p.SetValue(Canvas.TopProperty, 0);
            p.Child = content;
            return p;
        }
    }

}

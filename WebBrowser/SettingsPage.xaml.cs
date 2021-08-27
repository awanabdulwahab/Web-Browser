using System;
using System.Collections.Generic;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WebBrowser
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingPage : Page
    {
        public SettingPage()
        {
            this.InitializeComponent();
        }

        private void SettingsView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var selectedItem = (NavigationViewItem)args.SelectedItem;
            string tag = ((string)selectedItem.Tag);
            if (!args.IsSettingsSelected)
            {
                if (tag == "accountSetMenu")
                {
                    ContentFrame.Navigate(typeof(AccountSetMenu),null, args.RecommendedNavigationTransitionInfo);
                }

                else if (tag == "bookmarkSetMenu")
                {
                    ContentFrame.Navigate(typeof(BookmarkSetMenu), null, args.RecommendedNavigationTransitionInfo);
                }

                else if (tag == "historySetMenu")
                {
                    ContentFrame.Navigate(typeof(HistorySetMenu), null, args.RecommendedNavigationTransitionInfo);
                }
                else if (tag == "searchSetMenu")
                {
                    ContentFrame.Navigate(typeof(SearchSetMenu), null, args.RecommendedNavigationTransitionInfo);
                }
            }
        }

        private void SettingsView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            this.Frame.Navigate(typeof(MainPage));

        }

        private void settingsPage_Loaded(object sender, RoutedEventArgs e)
        {
            SettingsView.SelectedItem = SettingsView.MenuItems[0];
        }
    }
}

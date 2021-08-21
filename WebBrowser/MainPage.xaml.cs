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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WebBrowser
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            //DataAccess data = new DataAccess();
            //data.CreateSettingsFile();
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            if (webBrowser.CanGoBack)
            {
                webBrowser.GoBack(); 
            }
        } 

        private void forwardBtn_Click(object sender, RoutedEventArgs e)
        {
            if (webBrowser.CanGoForward)
            {
                webBrowser.GoForward(); 
            }
        }

        private void txt_searchBar_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                Search();
            }
        }

        //Custom Function

        private void Search()
        {
            webBrowser.Source = new Uri("https://www.google.com/search?q=" + txt_searchBar.Text);
        }

        private void homeBtn_Click(object sender, RoutedEventArgs e)
        {
            webBrowser.Refresh();
        }

        private void settingBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SettingPage));
        }

        private void MainBrowserWindow_Loading(FrameworkElement sender, object args)
        {

        }
    }
}

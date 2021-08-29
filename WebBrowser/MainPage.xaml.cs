using Microsoft.UI.Xaml.Controls;
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
using muxc = Microsoft.UI.Xaml.Controls;



// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WebBrowser
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        int settingsTabCount = 0;
        muxc.TabViewItem currentSelectedTab = null;
        WebView currentSelectedWebView = null;

        public MainPage()
        {
            this.InitializeComponent();
            DataAccess data = new DataAccess();
            data.CreateSettingsFile();
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

            if (currentSelectedWebView == null || !string.IsNullOrEmpty(txt_searchBar.Text))
            {
                webBrowser.Source = new Uri("https://www.google.com/search?q=" + txt_searchBar.Text); 
            }
            else
            {
                currentSelectedWebView.Source = new Uri("https://www.google.com/search?q=" + txt_searchBar.Text);
            }
        }

        private void homeBtn_Click(object sender, RoutedEventArgs e)
        {
            webBrowser.Refresh();
        }

        private void settingBtn_Click(object sender, RoutedEventArgs e)
        {
            //this.Frame.Navigate(typeof(SettingPage));
            if (settingsTabCount == 0)
            {
                AddSettingsTab();
                settingsTabCount++;
            }
        }

        private void AddSettingsTab()
        {
            var settingsTab = new muxc.TabViewItem();
            settingsTab.Header = "Settings";
            settingsTab.Name = "settingsTab";
            settingsTab.IconSource = new muxc.SymbolIconSource()
            {
                Symbol = Symbol.Setting
            };

            Frame frame = new Frame();
            frame.Navigate(typeof(SettingPage));
            settingsTab.Content = frame;

            // TODO: Look for this issue after first built
            TabControl.TabItems.Add(settingsTab);
            TabControl.SelectedItem = settingsTab;

        }

        private void MainBrowserWindow_Loading(FrameworkElement sender, object args)
        {
            
        }

        private void webBrowser_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
           
        }

        private void webBrowser_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            bool isSSL = false;
            statusBar.Text = webBrowser.Source.AbsoluteUri;
            TitleBarLabel.Text = "Awan Brother" +" | "+ webBrowser.DocumentTitle;

            DataTransfer dataTransfer = new DataTransfer();
            if (!string.IsNullOrEmpty(txt_searchBar.Text))
            {
                dataTransfer.SaveSearchTerm(txt_searchBar.Text, webBrowser.DocumentTitle, webBrowser.Source.AbsoluteUri); 
            }


            if (webBrowser.Source.AbsolutePath.Contains("https"))
            {
                isSSL = true;
                // Change Icon Image
                sslIcon.FontFamily = new FontFamily("Segoe MDL2 Assets");
                sslIcon.Glyph = "\xe785";

                ToolTip toolTip = new ToolTip();
                toolTip.Content = "This website has a SSL certificate.";
                ToolTipService.SetToolTip(sslBtn, toolTip);
            }
            else
            {
                isSSL = false;
                // Change Icon Image
                sslIcon.FontFamily = new FontFamily("Segoe MDL2 Assets");
                sslIcon.Glyph = "\xe72e";

                ToolTip toolTip = new ToolTip();
                toolTip.Content = "This website is unsafe because it doesn't have SSL certificate.";
                ToolTipService.SetToolTip(sslBtn, toolTip);
                
            }

        }

        private void webBrowser_Loading(FrameworkElement sender, object args)
        {
            statusBar.Text = webBrowser.Source.AbsoluteUri;
        }

        private void webBrowser_NavigationStarting_1(WebView sender, WebViewNavigationStartingEventArgs args)
        {


        }

        private void TabView_AddTabButtonClick(Microsoft.UI.Xaml.Controls.TabView sender, object args)
        {
            var newTab = new muxc.TabViewItem();
            newTab.IconSource = new muxc.SymbolIconSource()
            {
                Symbol = Symbol.Document
            };
            newTab.Header = "Blank Page";
            WebView wbView = new WebView();
            newTab.Content = wbView;
            wbView.Navigate(new Uri("https://www.google.com"));

            sender.TabItems.Add(newTab);

            sender.SelectedItem = newTab;
        }

        private void TabView_TabCloseRequested(muxc.TabView sender, muxc.TabViewTabCloseRequestedEventArgs args)
        {
            sender.TabItems.Remove(args.Tab);
            if (args.Tab.Name == "settingsTab")
            {
                settingsTabCount = 0; 
            }
        }

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            //search
            if (string.IsNullOrEmpty(txt_searchBar.Text))
            {
                Search(); 
            }
        }

        private void TabView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            currentSelectedTab = TabControl.SelectedItem as muxc.TabViewItem;
            currentSelectedWebView = currentSelectedTab.Content as WebView;

            TitleBarLabel.Text = "Awan Browser | " + currentSelectedWebView.DocumentTitle;
            statusBar.Text = currentSelectedWebView.Source.AbsoluteUri;

        }
    }
}

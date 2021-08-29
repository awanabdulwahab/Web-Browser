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
    public sealed partial class HistorySetMenu : Page
    {
        int LBICount = 0;
        public HistorySetMenu()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataTransfer dataTransfer = new DataTransfer();
            List<string> historyURLItems = await dataTransfer.Fetch("siteurl");

            foreach (var item in historyURLItems)
            {
                ListBoxItem newLBI = new ListBoxItem();
                newLBI.Name = "LBI" + LBICount;
                LBICount++;

                Style style = Application.Current.Resources["HistoryListBoxItem"] as Style;
                newLBI.Style = style;

                newLBI.Content = item;
                listory.Items.Add(newLBI);

            }
        }
    }
}

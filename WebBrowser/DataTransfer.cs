using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Storage;

namespace WebBrowser
{
    class DataTransfer
    {
        public string fileName = "settings.xml";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SearchTerm"></param>
        /// <param name="Title"></param>
        /// <param name="url"></param>
        public async void SaveSearchTerm(string SearchTerm, string Title, string url)
        {
            var doc = await DocumentLoad().AsAsyncOperation(); // Load the xml file

            var history = doc.GetElementsByTagName("history");

            XmlElement elSearchItem = doc.CreateElement("searchterm");
            XmlElement elSiteName = doc.CreateElement("sitename");
            XmlElement elSiteUrl = doc.CreateElement("siteurl");

            var historyItem = history[0].AppendChild(doc.CreateElement("historyItem"));

            historyItem.AppendChild(elSearchItem);
            historyItem.AppendChild(elSiteName);
            historyItem.AppendChild(elSiteUrl);

            elSearchItem.InnerText = SearchTerm;
            elSiteName.InnerText = Title;
            elSiteUrl.InnerText = url;

            SaveDocument(doc);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task<XmlDocument> DocumentLoad()
        {
            XmlDocument result = null;

            await Task.Run(async () =>
            {
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                XmlDocument doc = await XmlDocument.LoadFromFileAsync(file);
                result = doc;
            });
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        private async void SaveDocument(XmlDocument doc)
        {
            var file = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
            await doc.SaveToFileAsync(file);
        }
    }
}

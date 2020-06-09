using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace Dx2WikiWriter
{
    public class NewsScrapper
    {
        private HtmlWeb newsPageWeb = new HtmlWeb();
        private HtmlWeb newsItemWeb = new HtmlWeb();
        private string baseUrl = "https://d2-megaten-l.sega.com/en/news/";
        public Dictionary<string, News> NewsList;

        public NewsScrapper(int maxPages)
        {
            NewsList = new Dictionary<string, News>();
            GatherData(maxPages);
        }

        private void GatherData(int maxPages)
        {
            if (maxPages == -1)
                maxPages = 2000;
            
            for (var i = 1; i < maxPages+1; i++)
            {
                var page = GetNewsPage(i);
                var newsItems = GetNewsItems(page);

                foreach(var newsItem in newsItems)
                {
                    var ni = newsItemWeb.Load(newsItem.Key);


                    NewsList.Add(newsItem.Key, newsItem.Value);
                }
            }
        }

        private Dictionary<string, News> GetNewsItems(HtmlDocument document)
        {
            var newsItems = new Dictionary<string, News>();

            var urls = document.DocumentNode.SelectNodes("//*[@class='news-list-title']/a");
            var info = document.DocumentNode.SelectNodes("//*[@class='newslist-hed cf']");

            for (int i = 0; i < urls.Count; i++)
            {
                var link = urls[i].GetAttributeValue("href", "");
                if (!string.IsNullOrEmpty(link))
                {
                    var news = new News();
                    news.Category = info[i].SelectSingleNode("img").GetAttributeValue("alt", "");
                    news.Title = urls[i].SelectSingleNode("h3").InnerText;

                    var date = info[i].SelectSingleNode("p").InnerText;
                    date = Regex.Replace(date, @"[^0-9:/]", " ");

                    DateTime tryDate;
                    var worked = DateTime.TryParse(date, out tryDate);

                    if (worked)
                    {
                        news.Date = tryDate;
                        newsItems.Add(baseUrl + link, news);
                    }
                    else
                    {
                        MessageBox.Show($"Could not parse date: {date}");
                    }
                }
            }

            return newsItems;
        }

        private HtmlDocument GetNewsPage(int page)
        {
            return (page == 1) ? newsPageWeb.Load($"https://d2-megaten-l.sega.com/en/news/index.html")
                : newsPageWeb.Load($"https://d2-megaten-l.sega.com/en/news/index_{page}.html");
        }

    }
}

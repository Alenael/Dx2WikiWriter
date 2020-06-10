using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
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
        private MainForm Callback;
        private bool FinishedScan = false;

        public NewsScrapper(MainForm mainForm)
        {
            Callback = mainForm;
        }

        public async void ScrapePages(int maxPages)
        {
            NewsList = new Dictionary<string, News>();
            FinishedScan = false;

            await GatherData(maxPages);
            var mergeHappened = false;

            foreach(var news in NewsList)
            {
                var found = Callback.NewsDb.AsEnumerable().Any(r => r.Field<String>("Url").Equals(news.Key));
                if (!found)
                {
                    Callback.AppendTextBox($"Merging news to DB: <{news.Value.Url}>\n");
                    Callback.NewsDb.Rows.Add(news.Value.GetAsDataRow());
                    mergeHappened = true;
                }
            }

            if (mergeHappened)
            {
                Callback.DBManager.SaveCSV(Callback.NewsDb, Path.Combine(Callback.LoadedPath, "SMT Dx2 Database - News.csv"));
                Callback.AppendTextBox("News Db Merged and Saved Succesfully\n");
            }
            else
                Callback.AppendTextBox("No new news was located.\n");
        }

        private async Task GatherData(int maxPages)
        {
            Callback.AppendTextBox("Started Gathering News Data\n");

            if (maxPages == -1)
                maxPages = 2000;

            for (var i = 1; i < maxPages + 1; i++)
            {
                if (FinishedScan)
                    break;
                await GetPage(i);
            }

            Callback.AppendTextBox("Finished Gathering News Data\n");
        }

        private async Task GetPage(int i)
        {
            Callback.AppendTextBox($"Starting on page {i}\n");
            var page = await GetNewsPage(i);
            var newsItems = GetNewsItems(page);

            foreach (var newsItem in newsItems)
            {
                await GetNewsItems(newsItem);
                await Task.Delay(100);
            }
        }

        private async Task GetNewsItems(KeyValuePair<string, News> newsItem)
        {
            Callback.AppendTextBox($"Starting on news item: {newsItem.Value.Title} <{newsItem.Key}>\n");
            var ni = await newsItemWeb.LoadFromWebAsync(newsItem.Key);

            newsItem.Value.InnerHtml =
                ni.DocumentNode.SelectSingleNode("//*[@class='news-mainbox']").InnerHtml.Replace("\"", "\"\"").Replace("\n", "\\n");

            NewsList.Add(newsItem.Key, newsItem.Value);
        }

        private Dictionary<string, News> GetNewsItems(HtmlDocument document)
        {
            var newsItems = new Dictionary<string, News>();

            var urls = document.DocumentNode.SelectNodes("//*[@class='news-list-title']/a");
            var info = document.DocumentNode.SelectNodes("//*[@class='newslist-hed cf']");

            if (urls != null)
            {
                for (int i = 0; i < urls.Count; i++)
                {
                    var link = urls[i].GetAttributeValue("href", "");
                    if (!string.IsNullOrEmpty(link))
                    {
                        //Get Basic Info
                        var news = new News();
                        news.Category = info[i].SelectSingleNode("img").GetAttributeValue("alt", "");
                        news.Title = urls[i].SelectSingleNode("h3").InnerText.Replace("\"", "\"\"");
                        news.Url = baseUrl + link;
                        news.Image = urls[i].SelectSingleNode("div/img")?.GetAttributeValue("src", "");

                        if (news.Image == null)
                            news.Image = "";

                        //Get Date which is required
                        var date = info[i].SelectSingleNode("p").InnerText;
                        date = Regex.Replace(date, @"[^0-9:/]", " ");
                        DateTime tryDate;
                        var worked = DateTime.TryParse(date, out tryDate);

                        if (worked)
                        {
                            news.Date = tryDate;
                            newsItems.Add(news.Url, news);
                        }
                        else
                        {
                            MessageBox.Show($"Could not parse date: {date}");
                        }
                    }
                }
            }
            else
                FinishedScan = true;

            return newsItems;
        }

        private Task<HtmlDocument> GetNewsPage(int page)
        {
            return (page == 1) ? newsPageWeb.LoadFromWebAsync($"https://d2-megaten-l.sega.com/en/news/index.html")
                : newsPageWeb.LoadFromWebAsync($"https://d2-megaten-l.sega.com/en/news/index_{page}.html");
        }

    }
}

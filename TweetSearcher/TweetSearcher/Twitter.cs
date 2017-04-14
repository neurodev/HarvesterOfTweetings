using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;

namespace TweetSearcher
{
    public static class Twitter
    {
        public static List<Tweet> Search(DateTime startDate, DateTime endDate)
        {
            var result = new List<Tweet>();
            var webClient = new WebClient();
            var jsonString = webClient.DownloadString("https://twitter.com/i/search/timeline?vertical=news&q=since%3A2016-07-01%20until%3A2016-07-02&l=en&src=typd&include_available_features=1&include_entities=1&max_position=TWEET-");

            dynamic json = System.Web.Helpers.Json.Decode(jsonString);

            var html = json.items_html;
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var containers = doc.DocumentNode.SelectNodes("//li").Where(x => x.Attributes["class"] != null && x.Attributes["class"].Value.Contains("stream-item")).ToList();
            var lastID = "";
            var firstID = containers[1].Attributes["data-item-id"].Value;
            foreach (var tweet in containers)
            {
                var t = new Tweet()
                {
                    Text = getTweetContent(tweet),
                    UniqueID = tweet.Attributes["data-item-id"].Value
                };
                result.Add(t);
                lastID = tweet.Attributes["data-item-id"].Value;

            }

            while (result.Count < 1000)
            {
                jsonString = webClient.DownloadString("https://twitter.com/i/search/timeline?vertical=news&q=since%3A2016-07-01%20until%3A2016-07-02&l=en&&near%3A\"USA\"&src=typd&include_available_features=1&include_entities=1&max_position=TWEET-" + firstID + "-" + lastID);
                json = System.Web.Helpers.Json.Decode(jsonString);

                 html = json.items_html;
                doc = new HtmlDocument();
                doc.LoadHtml(html);
                containers = doc.DocumentNode.SelectNodes("//li").Where(x => x.Attributes["class"] != null && x.Attributes["class"].Value.Contains("stream-item")).ToList();
                
                foreach (var tweet in containers)
                {
                    var t = new Tweet()
                    {
                        Text = getTweetContent(tweet),
                        UniqueID = tweet.Attributes["data-item-id"].Value
                    };
                    result.Add(t);
                    lastID = tweet.Attributes["data-item-id"].Value;

                }
            }
            

            return result;
        }
        

        private static string getTweetContent(HtmlNode node)
        {
            return node.SelectNodes(".//p").Where(y => y.Attributes["class"] != null && y.Attributes["class"].Value.Contains("tweet-text")).FirstOrDefault().InnerText;


        }
    }
}

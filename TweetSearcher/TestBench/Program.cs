using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBench
{
    class Program
    {
        static void Main(string[] args)
        {
            var tweets = TweetSearcher.Twitter.Search(DateTime.Now, DateTime.Now);
            foreach(var tweet in tweets)
            {
                Console.WriteLine(tweet.UniqueID + ": " +tweet.Text);
            }
            Console.WriteLine(tweets.Count);
            Console.ReadKey();
        }
    }
}

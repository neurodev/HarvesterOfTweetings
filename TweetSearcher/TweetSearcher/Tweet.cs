using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetSearcher
{
    public class Tweet
    {
        public string Text { get; set; }
        public Tweeter User { get; set; }
        public DateTime TweetTime { get; set; }
        public string UniqueID { get; set; }
    }
}

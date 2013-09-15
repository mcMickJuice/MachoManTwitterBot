using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToTwitter.Common;
using LinqToTwitter;
using System.Configuration;
using TwitterTest.Classes;
using TwitterTest.Objects;

namespace TwitterTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TwitterBot tBot = new TwitterBot();
            int tweetsTweeted = 0;
            //entrypoint
            while (true)
            {
                tweetsTweeted = tBot.checkTwitter();
                Console.WriteLine("Checked Twitter - tweeted out {0} tweets",tweetsTweeted);
                System.Threading.Thread.Sleep(60000);
            }
            //Console.WriteLine("Program Done");
            //Console.ReadKey();
        }
    }
}

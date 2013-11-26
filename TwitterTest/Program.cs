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
using StatsTwitterBot.Classes;
using StatsTwitterBot.Objects;

namespace StatsTwitterBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var tBot = new TwitterBot();
            tBot.Run();
        }
    }
}

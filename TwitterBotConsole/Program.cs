using System;
using StatsTwitterBot.Classes;

namespace TwitterBotConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            TwitterBot tBot = new TwitterBot();
            tBot.Run();
            Console.WriteLine("Done");
        }
    }
}

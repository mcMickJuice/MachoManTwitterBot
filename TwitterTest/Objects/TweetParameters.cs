using LinqToTwitter;
using System.Collections.Generic;
using System;


namespace StatsTwitterBot
{
    public class TweetParameters
    {
        public string Team { get; set; }
        public string Number { get; set; }
        public int? Season { get; set; }
        public string StatType { get; set; }
        public string Name { get; set; }

        public TweetParameters(List<HashTagEntity> hashtags)
        {
            foreach (var hashtag in hashtags)
            {
                //season needs to start with year
                if (hashtag.Tag.StartsWith("year"))
                {
                    Season = Convert.ToInt32(hashtag.Tag.ToUpper().Replace("YEAR", ""));
                }
                else if ((hashtag.Tag.IndexOf('x') == 2 || hashtag.Tag.IndexOf('x') == 3) && hashtag.Tag.Length < 7)//makes sure it's not a name
                {
                    string[] splithashtag = hashtag.Tag.Split('x');
                    Team = splithashtag[0];
                    Number = splithashtag[1];
                }
                else if (hashtag.Tag.ToUpper().StartsWith("STAT"))
                {
                    StatType = hashtag.Tag.ToUpper().Replace("STAT", "");
                }
                else
                {
                    Name = hashtag.Tag.ToUpper().Replace(" ", "");
                }
            }
        }

        public TweetParameters()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToTwitter;
using System.Text.RegularExpressions;
using TwitterTest.Objects;

namespace TwitterTest.Classes
{
    class TwitterBot
    {
        List<Status> Tweets = new List<Status>();
        TwitterAction twitterAction = new TwitterAction();
        DataAccessor dbAccess = new DataAccessor();
        List<string> pendingTweets = new List<string>();
        List<string> hashtags = new List<string>();

        //usetwitter
        public int checkTwitter()
        {

            long _lastID = dbAccess.GetTweetID();
            int numberOfTweets = 0;

            Tweets = twitterAction.GetMentionedTweets(Convert.ToUInt64(_lastID));

            if (Tweets != null)
            {
                foreach (var tweet in Tweets)
                {
                    string originaltweet = tweet.Text.Replace("@MachoManFF ","");
                    TweetParameters tParams = new TweetParameters();
                    string replyto = "@" + tweet.User.Identifier.ScreenName;

                    //Console.WriteLine("{0}", tweet.User.Status.

                    foreach (var hashtag in tweet.Entities.HashTagEntities)
                    {
                        //season needs to start with year
                        if (hashtag.Tag.StartsWith("year"))
                        {
                            tParams.Season = Convert.ToInt32(hashtag.Tag.Replace("year", ""));
                        }
                        else if (hashtag.Tag.IndexOf('x') == 2 || hashtag.Tag.IndexOf('x') == 3)
                        {
                            string[] splithashtag = hashtag.Tag.Split('x');
                            tParams.Team = splithashtag[0];
                            tParams.Number = splithashtag[1];
                        }
                        else if (hashtag.Tag.StartsWith("stat"))
                        {
                            tParams.StatType = hashtag.Tag.Replace("stat","");   
                        }
                        else if (hashtag.Tag.StartsWith("first"))
                        {
                            tParams.FirstName = hashtag.Tag.Replace("first", "");
                        }
                        else if (hashtag.Tag.StartsWith("last"))
                        {
                            tParams.LastName = hashtag.Tag.Replace("last", "");
                        };
                    }

                    if (tParams.FirstName != null && tParams.LastName != null)
                    {
                        pendingTweets.Add(String.Format("{0}\n{1}", replyto, dbAccess.QueryDBwithName(tParams)));
                    }
                    else if (tParams.Number != null && tParams.Team != null)
                    {
                        pendingTweets.Add(String.Format("{0}\n{1}", replyto, dbAccess.QueryDBWithTeamNumber(tParams)));
                    }
                    else
                    {
                        string message = "I can't do anything with this: ";
                        int truncatedlength = message.Length + 3 + replyto.Length;
                        if (message.Length + originaltweet.Length > 140)
                        {
                            originaltweet = originaltweet.Substring(0, 140 - truncatedlength) + "...";
                        }
                        pendingTweets.Add(String.Format("{0}\nI can't do anything with this: {1}", replyto, originaltweet));
                    }
                    foreach (string t in pendingTweets)
                    {
                        twitterAction.TweetSomething(t,tweet.StatusID);
                        numberOfTweets += 1;
                        System.Threading.Thread.Sleep(2000);
                    }

                    dbAccess.InsertTweetID(Convert.ToInt64(tweet.StatusID));

                    pendingTweets.Clear();
                    hashtags.Clear();
                }
            }
            return numberOfTweets;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToTwitter;
using System.Text.RegularExpressions;
using StatsTwitterBot.Objects;

namespace StatsTwitterBot.Classes
{
    public class TwitterBot
    {
        List<Status> _tweets;
        TwitterAction _twitterAction;
        DataAccessor _dbAccess;
        StatSetFactory _statSetFactory;

        public TwitterBot()
        {
            _tweets = new List<Status>();
            _twitterAction = new TwitterAction();
            try
            {
                _twitterAction.AuthorizeTwitterAction();
            }
            catch (Exception e)
            {
                //log exception
            }

            _dbAccess = new DataAccessor();
            _statSetFactory = new StatSetFactory();
        }

        public int Run()
        {
            long _lastID = _dbAccess.GetLastTweetID();
            int numberOfTweets = 0;

            if (!_twitterAction.IsAuthorized)
            {
                //add session maintainer
                return -1;
            }

            _tweets = _twitterAction.GetMentionedTweets(Convert.ToUInt64(_lastID));

            if (_tweets != null)
            {
                foreach (var tweet in _tweets)
                {
                    string outgoingtweet = BuildReplyTweet(tweet);

                    _twitterAction.TweetSomething(outgoingtweet);
                    numberOfTweets += 1;
                    System.Threading.Thread.Sleep(2000);

                    _dbAccess.InsertTweetID(Convert.ToInt64(tweet.StatusID));
                }
            }

            return numberOfTweets;
        }

        private string BuildReplyTweet(Status tweet)
        {
            string replyTo = tweet.User.Identifier.ScreenName;
            TweetParameters tParams = ParseTweetParameters(tweet.Entities.HashTagEntities);
            string statType = "";
            string number ="";
            string team = "";
            int? season = tParams.Season;
            
            if (tParams.FirstName != null && tParams.LastName != null)
            {
                Tuple<string,string> numberAndTeam = _dbAccess.GetNumberAndTeam(tParams.FirstName, tParams.LastName);
                if (numberAndTeam == null)
                {
                    //tweet return cant do anything tweet
                }

                number = numberAndTeam.Item1;
                team = numberAndTeam.Item2;
            }
            else if (tParams.Number != null && tParams.Team != null)
            {
                number = tParams.Number;
                team = tParams.Team;
            }
            else
            {
                return GetFinalizedTweet(replyTo, tweet.Text, false);
            }

            statType = tParams.StatType != null ? tParams.StatType : _dbAccess.GetStatType(number, team);

            if (statType == null)
            {
                return GetFinalizedTweet(replyTo, tweet.Text, false);
            }

            StatSet statSet = _statSetFactory.GetStatSet(statType);
            string statString = statSet.GetStatString(season, number, team);

            return GetFinalizedTweet(replyTo, statString, true);

        }

        private TweetParameters ParseTweetParameters(List<HashTagEntity> hashtags)
        {
            var tParams = new TweetParameters();

            foreach (var hashtag in hashtags)
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
                else if (hashtag.Tag.ToUpper().StartsWith("STAT"))
                {
                    tParams.StatType = hashtag.Tag.ToUpper().Replace("STAT", "");
                }
                else if (hashtag.Tag.ToUpper().StartsWith("FIRST"))
                {
                    tParams.FirstName = hashtag.Tag.ToUpper().Replace("FIRST", "");
                }
                else if (hashtag.Tag.ToUpper().StartsWith("LAST"))
                {
                    tParams.LastName = hashtag.Tag.ToUpper().Replace("LAST", "");
                };
            }

            return tParams;
        }

        private string GetFinalizedTweet(string replyto, string tweettext, bool isvalidtweet)
        {
            var currentTime = DateTime.Now.ToShortTimeString();
            string message = "";
            string originaltweet = tweettext.Replace("@MachoManFF ", "");

            if (isvalidtweet)
            {
                message = tweettext;
            }
            else
            {
                message = "I can't do anything with this: ";
            }

            int truncatedlength = message.Length
                       + ("@" + replyto).Length
                       + currentTime.Length
                       + 3;

            if (message.Length + originaltweet.Length > 140)
            {
                originaltweet = originaltweet.Substring(0, 140 - truncatedlength) + "...";
            }

            return String.Format("{0}\n{1}\n{2}\n{3}", message, "@" + replyto, (isvalidtweet ? "" : originaltweet), currentTime);
        }
    }
}

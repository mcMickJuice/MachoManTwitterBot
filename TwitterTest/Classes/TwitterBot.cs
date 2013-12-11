using System;
using System.Collections.Generic;
using System.Linq;
using LinqToTwitter;
using NLog;
using StatsTwitterBot.Objects;

namespace StatsTwitterBot.Classes
{
    public class TwitterBot
    {
        List<Status> _tweets;
        TwitterAction _twitterAction;
        DataAccessor _dbAccess;
        StatSetFactory _statSetFactory;
        protected static NLog.Logger logger = LogManager.GetCurrentClassLogger();

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
                logger.Error("Error in TwitterBot Initialization", e);
            }

            _dbAccess = new DataAccessor();
            _statSetFactory = new StatSetFactory();
        }

        public int Run()
        {
            long lastID = _dbAccess.GetLastTweetID();
            int numberOfTweets = 0;

            if (!_twitterAction.IsAuthorized)
            {
                try
                {
                    logger.Warn("Twitter Auth lost. Attempting Reauthorization");
                    _twitterAction.AuthorizeTwitterAction();
                }
                catch (Exception e)
                {
                    logger.Error("Twitter Auth lost. Failed Reauthorization", e);
                    throw e;
                }
            }

            _tweets = _twitterAction.GetMentionedTweets(Convert.ToUInt64(lastID));

            foreach (var tweet in _tweets)
            {
                List<string> outgoingTweets = BuildReplyTweets(tweet);

                numberOfTweets += _twitterAction.TweetSomething(outgoingTweets);

                outgoingTweets.ForEach(outgoingTweet =>
                    {
                        _dbAccess.InsertTweetID(Convert.ToInt64(tweet.StatusID), tweet.Text, outgoingTweet);
                    });
                
            }

            logger.Trace(String.Format("{0} tweets tweeted", numberOfTweets));
            return numberOfTweets;
        }

        private List<string> BuildReplyTweets(Status tweet)
        {
            string replyTo = tweet.User.Identifier.ScreenName;
            var tParams = new TweetParameters(tweet.Entities.HashTagEntities);
            string statType = "";
            var playerIds = new List<int>();
            var finalizedTweets = new List<string>();
            
            if (tParams.Number != null && tParams.Team != null)
            {
                playerIds.Add(_dbAccess.GetIdByNumberAndTeam(tParams.Team, tParams.Number));
            }
            else if (tParams.Name != null)
            {
                playerIds = _dbAccess.GetIdByName(tParams.Name);
            }
            

            playerIds.ForEach(id =>
                {
                    statType = tParams.StatType ?? _dbAccess.GetStatType(id);
                    StatSet statSet = _statSetFactory.GetStatSet(statType);
                    string statString = statSet.GetStatString(tParams.Season, id);
                    finalizedTweets.Add(GetFinalizedTweet(replyTo, statString, true));
                });

            return finalizedTweets;

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
                message = "I can't do anything with this: "; //please refer to my README
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

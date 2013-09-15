using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToTwitter;
using System.Data.Entity;
using TwitterTest.Objects;

namespace TwitterTest.Classes
{
    class TwitterAction
    {
        string UserScreenName { get; set; }
        TwitterAuthorizer tAuth = new TwitterAuthorizer();
        ESPNStatsEntities dbcontext = new ESPNStatsEntities();

        public void TweetSomething(string tweet, string statusid)
        {
            try
            {
                var auth = tAuth.getPin();
                if (!auth.IsAuthorized)
                {
                    auth.Authorize();
                }

                using (TwitterContext twitContext = new TwitterContext(auth))
                {
                    twitContext.UpdateStatus(tweet);

                    Logger log = Log.LoggerCtr(String.Format("Successful Tweet of the following message - \n{0}", tweet), "Success");

                    dbcontext.Logger.Add(log);
                    dbcontext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Logger log = Log.LoggerCtr(String.Format("Tweet failed - {0}. Here's the tweet - \n{1}",e.Message, tweet),"Error");

                dbcontext.Logger.Add(log);
                dbcontext.SaveChanges();
            }
        }

        public void SendDM(string dm, string userscreenname)
        {
            try
            {
                var auth = tAuth.getPin();
                if (!auth.IsAuthorized)
                {
                    auth.Authorize();
                }

                using (TwitterContext twitContext = new TwitterContext(auth))
                {
                    twitContext.NewDirectMessage(userscreenname, dm);
                    Logger log = Log.LoggerCtr(String.Format("Success sent DM to {0} with message: \n{1}", userscreenname, dm), "Success");

                    dbcontext.Logger.Add(log);
                    dbcontext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Logger log = Log.LoggerCtr(String.Format("Error sending DM - \n{0}", e.Message), "Error");

                dbcontext.Logger.Add(log);
                dbcontext.SaveChanges();
            }
        }

        public List<Status> GetMentionedTweets(ulong lasttweetid)
        {
            try
            {
                var auth = tAuth.getPin();
                if (!auth.IsAuthorized)
                {
                    auth.Authorize();
                }

                using (TwitterContext twitContext = new TwitterContext(auth))
                {
                    var tweets = (from tweet in twitContext.Status
                                 where tweet.Type == StatusType.Mentions && tweet.SinceID == lasttweetid
                                 select tweet).Where(t=>t.Entities.HashTagEntities.Count > 0); //filters out tweets that have no hashtags

                    if (tweets.Count() == 0)
                    {
                        return null;
                    }

                    return tweets.ToList();

                }
            }
            catch (Exception e)
            {
                Logger log = Log.LoggerCtr(String.Format("There was an error querying Twitter - {0}", e.Message), "Error");

                dbcontext.Logger.Add(log);
                dbcontext.SaveChanges();

                return null;
            }
        }
    }
}

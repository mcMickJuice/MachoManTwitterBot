using System;
using System.Collections.Generic;
using System.Linq;
using LinqToTwitter;

namespace StatsTwitterBot.Classes
{
    public class TwitterAction
    {
        private PinAuthorizer _pinAuth;
        public bool IsAuthorized { get; private set; }
        //protected static NLog.Logger logger = LogManager.GetCurrentClassLogger();

        public void AuthorizeTwitterAction(string consumerkey, string consumersecret, string oauthtoken, string accesstoken)
        {
            var tAuth = new TwitterAuthorizer();

            try
            {
                _pinAuth = tAuth.getPin(consumerkey, consumersecret, oauthtoken, accesstoken);

                if (!_pinAuth.IsAuthorized)
                {
                    _pinAuth.Authorize();
                }

                IsAuthorized = true;
            }
            catch (Exception e)
            {
                //logger.Error("Error in AuthorizeTwitterAction", e);
                IsAuthorized = false;
                throw e;
            }
        }

        public int TweetSomething(List<string> tweets)
        {
            int numOfTweets = 0;
            if (IsAuthorized)
            {
                using (TwitterContext twitContext = new TwitterContext(_pinAuth))
                {
                    tweets.ForEach(tweet =>
                    {
                        try
                        {
                            twitContext.UpdateStatus(tweet);
                            //logger.Info(String.Format("Tweeted {0}", tweet));

                        }
                        catch (Exception e)
                        {
                            //logger.Error(String.Format("Error tweeting message {0}", tweet), e);
                            throw e;
                        }
                        numOfTweets++;
                        System.Threading.Thread.Sleep(2000);
                    });


                }
            }
            return numOfTweets;
        }

        public void SendDM(string dm, string userscreenname)
        {
            if (IsAuthorized)
            {
                using (TwitterContext twitContext = new TwitterContext(_pinAuth))
                {
                    try
                    {
                        twitContext.NewDirectMessage(userscreenname, dm);
                        //logger.Info("Sent DM {0} to user {1}", dm, userscreenname);
                    }
                    catch (Exception e)
                    {
                        //logger.Error(String.Format("Error sending DM {0} to user {1}", dm, userscreenname), e);
                        throw e;
                    }

                }
            }
        }

        public List<Status> GetMentionedTweets(ulong lasttweetid)
        {
            if (IsAuthorized)
            {
                try
                {
                    using (TwitterContext twitContext = new TwitterContext(_pinAuth))
                    {
                        var tweets = twitContext.Status.Where(x => x.Type == StatusType.Mentions
                                                                && x.SinceID == lasttweetid
                                                                && x.Entities.HashTagEntities.Count > 0);

                        return tweets == null ? null : tweets.ToList();
                    }
                }
                catch (Exception e)
                {
                    //logger.Error("Error occurred on GetMentionedTweets", e);
                    throw e;
                    //return null;
                }

            }
            else return null;
        }
    }
}

using System.Configuration;
using LinqToTwitter;

namespace StatsTwitterBot.Classes
{
    internal class TwitterAuthorizer
    {

        public PinAuthorizer getPin(string consumerKey, string consumerSecret, string oauthtoken, string accesstoken)
        {

            var auth = new PinAuthorizer
            {
                Credentials = new InMemoryCredentials
                {
                    ConsumerKey = consumerKey,
                    ConsumerSecret = consumerSecret,
                    OAuthToken = oauthtoken,
                    AccessToken = accesstoken
                },
                UseCompression = true

            };
            return auth;
        }
    }
}

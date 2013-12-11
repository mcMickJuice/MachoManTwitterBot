using System.Configuration;
using LinqToTwitter;

namespace StatsTwitterBot.Classes
{
    class TwitterAuthorizer
    {
        
        public PinAuthorizer getPin()
        {
          
                var auth = new PinAuthorizer
            {
                Credentials = new InMemoryCredentials
                {
                    ConsumerKey = ConfigurationManager.AppSettings["twitterConsumerKey"],
                    ConsumerSecret = ConfigurationManager.AppSettings["twitterConsumerSecret"],
                    OAuthToken = ConfigurationManager.AppSettings["twitterOAuthToken"],
                    AccessToken = ConfigurationManager.AppSettings["twitterAccessToken"]
                },
                UseCompression = true

            };
                return auth;
            }
    }
}

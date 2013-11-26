using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToTwitter;
using System.Configuration;
using LinqToTwitter.Common;
using LinqToTwitter.Security;
using LinqToTwitter.Serialization;
using System.Diagnostics;

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

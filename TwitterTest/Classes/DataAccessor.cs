using System;
using System.Collections.Generic;
using System.Linq;

namespace StatsTwitterBot.Classes
{
    public class DataAccessor
    {
        private ESPNStatsEntities _context;

        public DataAccessor()
        {
            _context = new ESPNStatsEntities();
        }


        public long GetLastTweetID()
        {
            return _context.TweetIDs.Max(ids => ids.TweetID);
        }

        public void InsertTweetID(long tweetid, string incomingtweet, string outgoingtweet)
        {
            var tweetids = new TweetIDs();
            tweetids.TweetID = tweetid;
            tweetids.CreatedDate = DateTime.Now;
            tweetids.IncomingTweet = incomingtweet;
            tweetids.OutgoingTweet = outgoingtweet;
            _context.TweetIDs.Add(tweetids);
            _context.SaveChanges();
        }

        public Tuple<string,string> GetNumberAndTeam(string firstname, string lastname)
        {

            var query = _context.Players.FirstOrDefault(player => player.PlayerName == firstname + " " + lastname);

            if (query != null)
            {
                return new Tuple<string, string>(query.Number, query.NFLTeams.TeamAbbrev);
            }
            else
            {
                return null;
            }
        }

        public List<int> GetIdByName(string name)
        {
            var query = _context.Players.Where(player => player.PlayerName.Replace(" ", "") == name)
                                        .Select(player => player.PlayerID)
                                        .ToList();

            return query;
        }

        public int GetIdByNumberAndTeam(string teamabbrev, string number)
        {
            var query = _context.Players.Where(player => player.NFLTeams.TeamAbbrev.ToUpper() == teamabbrev.ToUpper() 
                                                               && player.Number == number)
                                        .Select(player => player.PlayerID)
                                        .FirstOrDefault();

            return query;
        }

        public string GetStatType(string number, string teamabbrev)
        {
            string[] defensivepositions = new string[] { "CB", "DB", "DE", "DL", "DT", "LB", "NT", "S" };
            string position;
            Dictionary<string, string> positionToStatTypeMapping = new Dictionary<string, string> {
                                                                                                    {"QB","PASSING"},
                                                                                                    {"RB", "RUSHING"},
                                                                                                    {"WR", "RECEIVING"},
                                                                                                    {"TE", "RECEIVING"},
                                                                                                    {"DEFENSIVE", "DEFENSIVE"}
                                                                                                };

            var query = _context.Players.FirstOrDefault(player => player.Number == number && player.NFLTeams.TeamAbbrev == teamabbrev);

            if (query != null)
            {
                position = defensivepositions.Contains(query.Position) ? "DEFENSIVE" : query.Position;
                return positionToStatTypeMapping[position];
            }
            else
            {
                throw new ApplicationException(String.Format("Player was not found with Number: {0} and Team Abbrev: {1}",number,teamabbrev));
            }
        }

        public string GetStatType(int playerid)
        {
            string[] defensivePositions = new string[] { "CB", "DB", "DE", "DL", "DT", "LB", "NT", "S" };
            string position;
            Dictionary<string, string> positionToStatTypeMapping = new Dictionary<string, string> {
                                                                                                    {"QB","PASSING"},
                                                                                                    {"RB", "RUSHING"},
                                                                                                    {"WR", "RECEIVING"},
                                                                                                    {"TE", "RECEIVING"},
                                                                                                    {"DEFENSIVE", "DEFENSIVE"},
                                                                                                    {"OTHER", "SCORING"}
                                                                                                };

            var query = _context.Players.FirstOrDefault(player => player.PlayerID == playerid);

            //position = defensivepositions.Contains(query.Position) ? "DEFENSIVE" : query.Position;
            //if (!positionToStatTypeMapping.TryGetValue(query.Position,out position))
            if(defensivePositions.Contains(query.Position))  
            {
                position = "DEFENSIVE";
            }
            else if (!positionToStatTypeMapping.TryGetValue(query.Position, out position))
            {
                position = "SCORING";
            }
            return position;
        }
    }
}

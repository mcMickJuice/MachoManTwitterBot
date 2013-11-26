using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsTwitterBot.Objects;

namespace StatsTwitterBot.Classes
{
    internal class DataAccessor
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

        public void InsertTweetID(long tweetid)
        {
            var tweetids = new TweetIDs();
            tweetids.TweetID = tweetid;
            tweetids.CreatedDate = DateTime.Now;
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
                throw new ApplicationException(String.Format("Player was not found with Numbrer: {0} and Team Abbrev: {1}",number,teamabbrev));
            }
        }
    }
}

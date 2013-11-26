using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsTwitterBot.Classes
{
    class RushingStatSet: StatSet
    {
        public RushingStatSet()
            :base()
        {
        }

        public override string GetStatString(int? year, string playernumber, string playerteam)
        {
            string outputString = "";

            var query = Context.RushingStats.Where(stat => stat.Players.NFLTeams.TeamAbbrev == playerteam
                                                        && stat.Players.Number == playernumber)
                                                .Select(stat => new { stat.Players.PlayerName, stat.Season, stat.Yards, stat.Touchdowns, stat.FumblesLost, stat.GamesPlayed });

            if (year == null)
            {
                outputString = String.Format("{0}: Career Rushing Stats\r\nYards: {1}\r\nTouchdowns: {2}\r\nFumbles: {3}\r\nGames Played: {4}"
                                                       , query.Select(x => x.PlayerName).First()
                                                       , query.Sum(x => x.Yards), query.Sum(x => x.Touchdowns), query.Sum(x => x.FumblesLost),query.Sum(x => x.GamesPlayed));
            }
            else
            {
                var seasonResults = query.First(stat => stat.Season == year);

                outputString = String.Format("{0}-{1}: Rushing Stats\r\nYards: {2}\r\nTouchdowns: {3}\r\nFumbles: {4}\r\nGames Played: {5}"
                                                       , seasonResults.PlayerName, year, seasonResults.Yards, seasonResults.Touchdowns, seasonResults.FumblesLost, seasonResults.GamesPlayed);
            }

            return outputString;
        }
    }
}

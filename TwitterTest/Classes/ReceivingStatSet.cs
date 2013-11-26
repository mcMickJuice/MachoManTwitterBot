using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsTwitterBot.Classes
{
    class ReceivingStatSet: StatSet
    {
        public ReceivingStatSet()
            :base()
        {
        }

        public override string GetStatString(int? year, string playernumber, string playerteam)
        {
            string outputString = "";

            var query = Context.ReceivingStats.Where(stat => stat.Players.NFLTeams.TeamAbbrev == playerteam
                                                        && stat.Players.Number == playernumber)
                                                .Select(stat => new { stat.Players.PlayerName, stat.Season, stat.Yards, stat.Touchdowns, stat.Receptions, stat.GamesPlayed });

            if (year == null)
            {
                outputString = String.Format("{0}: Career Receiving Stats\r\nYards: {1}\r\nReceptions: {2}\r\nTouchdowns: {3}\r\nGames Played: {4}"
                                                       , query.Select(x => x.PlayerName).First()
                                                       , query.Sum(x => x.Yards), query.Sum(x => x.Receptions), query.Sum(x => x.Touchdowns), query.Sum(x => x.GamesPlayed));
            }
            else
            {
                var seasonResults = query.First(stat => stat.Season == year);

                outputString = String.Format("{0}-{1}: Receiving Stats\r\nYards: {2}\r\nReceptions: {3}\r\nTouchdowns: {4}\r\nGames Played: {5}"
                                                       , seasonResults.PlayerName, year, seasonResults.Yards, seasonResults.Receptions, seasonResults.Touchdowns, seasonResults.GamesPlayed);
            }

            return outputString;
        }
    }
}

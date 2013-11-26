using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsTwitterBot.Classes
{
    class DefensiveStatSet: StatSet
    {
        public DefensiveStatSet()
            :base()
        {
        }

        public override string GetStatString(int? year, string playernumber, string playerteam)
        {
            string outputString = "";

            var query = Context.DefensiveStats.Where(stat => stat.Players.NFLTeams.TeamAbbrev == playerteam
                                                        && stat.Players.Number == playernumber)
                                                .Select(stat => new { stat.Players.PlayerName, stat.Season, stat.TotalTackles, stat.Sack, stat.Interceptions, stat.GamesPlayed });

            if (year == null)
            {
                outputString = String.Format("{0}: Career Defensive Stats\r\nTotal Tackles: {1}\r\nSacks: {2}\r\nInterceptions: {3}\r\nGames Played: {4}"
                                                       , query.Select(x => x.PlayerName).First()
                                                       , query.Sum(x => x.TotalTackles), query.Sum(x => x.Sack), query.Sum(x => x.Interceptions), query.Sum(x => x.GamesPlayed));
            }
            else
            {
                var seasonResults = query.First(stat => stat.Season == year);

                outputString = String.Format("{0}-{1}: Defensive Stats\r\nTotal Tackles: {2}\r\nSacks: {3}\r\nInterceptions: {4}\r\nGames Played: {5}"
                                                       , seasonResults.PlayerName, year, seasonResults.TotalTackles, seasonResults.Sack, seasonResults.Interceptions
                                                       , seasonResults.GamesPlayed);
            }

            return outputString;
        }
    }
}

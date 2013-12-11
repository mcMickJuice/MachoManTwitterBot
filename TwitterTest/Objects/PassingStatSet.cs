using System;
using System.Linq;

namespace StatsTwitterBot.Objects
{
    class PassingStatSet : StatSet
    {
        public PassingStatSet()
            :base()
        {
        }

        //public override string GetStatString(int? year, string playernumber, string playerteam)
        //{
        //    string outputString = "";

        //    var query = Context.PassingStats.Where(stat => stat.Players.NFLTeams.TeamAbbrev == playerteam
        //                                                && stat.Players.Number == playernumber)
        //                                        .Select(stat => new { stat.Players.PlayerName, stat.Season, stat.Yards, stat.Touchdowns, stat.Interceptions, stat.GamesPlayed });

        //    if (year == null)
        //    {
        //        outputString = String.Format("{0}: Career Passing Stats\r\nYards: {1}\r\nTouchdowns: {2}\r\nInterceptions {3}\r\nGames Played: {4}"
        //                                               , query.Select(x=> x.PlayerName).First()
        //                                               ,query.Sum(x => x.Yards), query.Sum(x => x.Touchdowns), query.Sum(x => x.Interceptions),query.Sum(x => x.GamesPlayed));
        //    }
        //    else
        //    {
        //        var seasonResults = query.First(stat => stat.Season == year);

        //        outputString = String.Format("{0}-{1}: Passing Stats\r\nYards: {2}\r\nTouchdowns: {3}\r\nInterceptions: {4}\r\nGames Played: {5}"
        //                                               , seasonResults.PlayerName, year, seasonResults.Yards, seasonResults.Touchdowns, seasonResults.Interceptions, seasonResults.GamesPlayed);
        //    }

        //    return outputString;
        //}

        public override string GetStatString(int? year, int playerid)
        {
            string outputString = "";

            var query = Context.PassingStats.Where(stat => stat.Players.PlayerID == playerid)
                                                .Select(stat => new { stat.Players.PlayerName, stat.Season, stat.Yards, stat.Touchdowns, stat.Interceptions, stat.GamesPlayed });

            if (year == null)
            {
                outputString = String.Format("{0}: Career Passing Stats\r\nYards: {1}\r\nTouchdowns: {2}\r\nInterceptions {3}\r\nGames Played: {4}"
                                                       , query.Select(x => x.PlayerName).First()
                                                       , query.Sum(x => x.Yards), query.Sum(x => x.Touchdowns), query.Sum(x => x.Interceptions), query.Sum(x => x.GamesPlayed));
            }
            else
            {
                var seasonResults = query.First(stat => stat.Season == year);

                outputString = String.Format("{0}-{1}: Passing Stats\r\nYards: {2}\r\nTouchdowns: {3}\r\nInterceptions: {4}\r\nGames Played: {5}"
                                                       , seasonResults.PlayerName, year, seasonResults.Yards, seasonResults.Touchdowns, seasonResults.Interceptions, seasonResults.GamesPlayed);
            }

            return outputString;
        }
    }
}

using System;
using System.Linq;

namespace StatsTwitterBot.Objects
{
    class ScoringStatSet: StatSet
    {
        public ScoringStatSet()
            :base()
        {
        }

        //public override string GetStatString(int? year, string playernumber, string playerteam)
        //{
        //    string outputString = "";

        //    var query = Context.ScoringStats.Where(stat => stat.Players.NFLTeams.TeamAbbrev == playerteam
        //                                                && stat.Players.Number == playernumber)
        //                                        .Select(stat => new { stat.Players.PlayerName, stat.Season, stat.PassTDs, stat.RushTDs, stat.RecTDs, stat.TotalTDs, stat.GamesPlayed });

        //    if (year == null)
        //    {
        //        outputString = String.Format("{0}: Career Scoring Stats\r\nPass TDs: {1}\r\nRush TDs: {2}\r\nRec TDs: {3}\r\nTotal TDs: {4}\r\nGames Played: {5}"
        //                                               , query.Select(x => x.PlayerName).First()
        //                                               , query.Sum(x => x.PassTDs), query.Sum(x => x.RushTDs), query.Sum(x => x.RecTDs), query.Sum(x => x.TotalTDs)
        //                                               , query.Sum(x => x.GamesPlayed));
        //    }
        //    else
        //    {
        //        var seasonResults = query.First(stat => stat.Season == year);

        //        outputString = String.Format("{0}-{1}: Scoring Stats\r\nPass TDs: {2}\r\nRush TDs: {3}\r\nRec TDs: {4}\r\nTotal TDs: {5}\r\nGames Played: {6}"
        //                                               , seasonResults.PlayerName, year, seasonResults.PassTDs, seasonResults.RushTDs, seasonResults.RecTDs
        //                                               , seasonResults.TotalTDs, seasonResults.GamesPlayed);
        //    }

        //    return outputString;
        //}

        public override string GetStatString(int? year, int playerid)
        {
            string outputString = "";

            var query = Context.ScoringStats.Where(stat => stat.Players.PlayerID == playerid)
                                                .Select(stat => new { stat.Players.PlayerName, stat.Season, stat.PassTDs, stat.RushTDs, stat.RecTDs, stat.TotalTDs, stat.GamesPlayed });

            if (year == null)
            {
                outputString = String.Format("{0}: Career Scoring Stats\r\nPass TDs: {1}\r\nRush TDs: {2}\r\nRec TDs: {3}\r\nTotal TDs: {4}\r\nGames Played: {5}"
                                                       , query.Select(x => x.PlayerName).First()
                                                       , query.Sum(x => x.PassTDs), query.Sum(x => x.RushTDs), query.Sum(x => x.RecTDs), query.Sum(x => x.TotalTDs)
                                                       , query.Sum(x => x.GamesPlayed));
            }
            else
            {
                var seasonResults = query.First(stat => stat.Season == year);

                outputString = String.Format("{0}-{1}: Scoring Stats\r\nPass TDs: {2}\r\nRush TDs: {3}\r\nRec TDs: {4}\r\nTotal TDs: {5}\r\nGames Played: {6}"
                                                       , seasonResults.PlayerName, year, seasonResults.PassTDs, seasonResults.RushTDs, seasonResults.RecTDs
                                                       , seasonResults.TotalTDs, seasonResults.GamesPlayed);
            }

            return outputString;
        }
    }
}

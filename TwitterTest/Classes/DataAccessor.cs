using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterTest.Objects;

namespace TwitterTest.Classes
{
    class DataAccessor
    {
        ESPNStatsEntities dbcontext = new ESPNStatsEntities();
        private string _playername;


        public string QueryDBWithTeamNumber(TweetParameters tweetparams)
        {
            //use tweetparams to query db
            string playerposition = GetPlayerType(tweetparams);

            if (playerposition.StartsWith("Player not found"))
            {
                return playerposition;
            }

            if ((playerposition == "QB" && tweetparams.StatType == null) || tweetparams.StatType == "Passing")
            {
                if (tweetparams.Season != 0)
                {
                    var resultset = (from statset in dbcontext.PassingStats
                                     where statset.Players.NFLTeams.TeamAbbrev == tweetparams.Team && statset.Players.Number == tweetparams.Number && statset.Season == tweetparams.Season
                                     select new { statset.Season, statset.Players.PlayerName, statset.Team, statset.Yards, statset.Touchdowns, statset.Interceptions }).FirstOrDefault();

                    if (resultset != null)
                    {
                        return String.Format("Passing Stats - {0}\nSeason: {1}\nTeam: {2}\nPassing Yards: {3}\nTDs: {4}\nInts: {5}\n{6}", resultset.PlayerName, resultset.Season, resultset.Team, resultset.Yards, resultset.Touchdowns, resultset.Interceptions, DateTime.Now.ToString("hh:mm:ss"));
                    }
                }
                else // career stats
                {
                    var resultset = from statset in dbcontext.PassingStats
                                    where statset.Players.NFLTeams.TeamAbbrev == tweetparams.Team && statset.Players.Number == tweetparams.Number
                                    select new { statset.Players.PlayerName, statset.Yards, statset.Touchdowns, statset.Interceptions };

                    if (resultset != null)
                    {
                        string _playername = "";

                        foreach (var set in resultset) //HACK
                        {
                            _playername = set.PlayerName;
                            break;
                        }

                        return String.Format("Career Passing Stats: {0}\nYards: {1}\nTDs: {2}\nInts: {3}\n{4}", _playername, resultset.Sum(x => x.Yards), resultset.Sum(x => x.Touchdowns), resultset.Sum(x => x.Interceptions), DateTime.Now.ToString("hh:mm:ss"));
                    }
                }
            }
            else if ((playerposition == "RB" && tweetparams.StatType == null) || (playerposition == "FB" && tweetparams.StatType == null) || tweetparams.StatType == "Rushing")
            {
                if (tweetparams.Season != 0)
                {
                    var resultset = (from statset in dbcontext.RushingStats
                                     where statset.Players.NFLTeams.TeamAbbrev == tweetparams.Team && statset.Players.Number == tweetparams.Number && statset.Season == tweetparams.Season
                                     select new { statset.Season, statset.Players.PlayerName, statset.Team, statset.Yards, statset.Touchdowns, statset.FumblesLost,}).FirstOrDefault();
                    if (resultset != null)
                    {
                        return String.Format("Rushing Stats - {0}\nSeason: {1}\nTeam: {2}\nRushing Yards: {3}\nTDs: {4}\nFumbles: {5}\n{6}", resultset.PlayerName, resultset.Season, resultset.Team, resultset.Yards, resultset.Touchdowns, resultset.FumblesLost, DateTime.Now.ToString("hh:mm:ss"));
                    }
                }
                else // career stats
                {
                    var resultset = from statset in dbcontext.RushingStats
                                    where statset.Players.NFLTeams.TeamAbbrev == tweetparams.Team && statset.Players.Number == tweetparams.Number
                                    select new { statset.Players.PlayerName, statset.Yards, statset.Touchdowns, statset.FumblesLost };

                    if (resultset != null)
                    {
                        string _playername = "";

                        foreach (var set in resultset) //HACK
                        {
                            _playername = set.PlayerName;
                            break;
                        }

                        return String.Format("Career Rushing Stats: {0}\nYards: {1}\nTDs: {2}\nFumbles: {3}\n{4}", _playername, resultset.Sum(x => x.Yards), resultset.Sum(x => x.Touchdowns), resultset.Sum(x => x.FumblesLost), DateTime.Now.ToString("hh:mm:ss"));
                    }
                }
            }
            else if ((playerposition == "WR" && tweetparams.StatType == null) || (playerposition == "TE" && tweetparams.StatType == null) || tweetparams.StatType == "Receiving")
            {
                if (tweetparams.Season != 0)
                {
                    var resultset = (from statset in dbcontext.ReceivingStats
                                     where statset.Players.NFLTeams.TeamAbbrev == tweetparams.Team && statset.Players.Number == tweetparams.Number && statset.Season == tweetparams.Season
                                     select new { statset.Season, statset.Players.PlayerName, statset.Team, statset.Yards, statset.Touchdowns, statset.Receptions, }).FirstOrDefault();

                    if (resultset != null)
                    {
                        return String.Format("Receiving Stats - {0}\nSeason: {1}\nTeam: {2}\nReceptions: {3}\nReceiving Yards: {4}\nTDs: {5}\n{6}", resultset.PlayerName, resultset.Season, resultset.Team, resultset.Receptions, resultset.Yards, resultset.Touchdowns, DateTime.Now.ToString("hh:mm:ss"));
                    }
                }
                else // career stats
                {
                    var resultset = from statset in dbcontext.ReceivingStats
                                    where statset.Players.NFLTeams.TeamAbbrev == tweetparams.Team && statset.Players.Number == tweetparams.Number
                                    select new { statset.Players.PlayerName, statset.Yards, statset.Receptions, statset.Touchdowns };

                    if (resultset != null)
                    {
                        string _playername = "";

                        foreach (var set in resultset) //HACK
                        {
                            _playername = set.PlayerName;
                            break;
                        }

                        return String.Format("Career Receiving Stats: {0}\nReceptions: {1}\nYards: {2}\nTDs: {3}\n{4}", _playername, resultset.Sum(x => x.Receptions), resultset.Sum(x => x.Yards), resultset.Sum(x => x.Touchdowns), DateTime.Now.ToString("hh:mm:ss"));
                    }
                }
            }
            else if ((playerposition == "Defensive" && tweetparams.StatType == null) || tweetparams.StatType.StartsWith("Defens"))
            {
                if (tweetparams.Season != 0)
                {
                    var resultset = (from statset in dbcontext.DefensiveStats
                                    where statset.Players.NFLTeams.TeamAbbrev == tweetparams.Team && statset.Players.Number == tweetparams.Number && statset.Season == tweetparams.Season
                                    select new { statset.Season, statset.Players.PlayerName,statset.Team, statset.CombinedTackles, statset.FumblesRecovered, statset.Sack, statset.Interceptions }).FirstOrDefault();

                    if (resultset != null)
                    {
                        return String.Format("Defensive Stats - {0}\nSeason: {1}\nTeam: {2}\nTackles: {3}\nFumbles: {4}\nSacks: {5}\nInts: {6}\n{7}", resultset.PlayerName, resultset.Season, resultset.Team, resultset.CombinedTackles, resultset.FumblesRecovered, resultset.Sack, resultset.Interceptions, DateTime.Now.ToString("hh:mm:ss"));
                    }
                }
                else
                {
                    var resultset = from statset in dbcontext.DefensiveStats
                                    where statset.Players.NFLTeams.TeamAbbrev == tweetparams.Team && statset.Players.Number == tweetparams.Number
                                    select new { statset.Players.PlayerName, statset.CombinedTackles, statset.FumblesRecovered, statset.Sack, statset.Interceptions };

                    if (resultset != null)
                    {
                        string _playername = "";

                        foreach (var set in resultset) //HACK
                        {
                            _playername = set.PlayerName;
                            break;
                        }

                        return String.Format("Career Defensive Stats: {0}\nTackles: {1}\nFumbles: {2}\nSacks: {3}\nInts: {4}\n{5}", _playername, resultset.Sum(x => x.CombinedTackles), resultset.Sum(x => x.FumblesRecovered), resultset.Sum(x => x.Sack), resultset.Sum(x => x.Interceptions), DateTime.Now.ToString("hh:mm:ss"));
                    }
                    
                }
            }
            else if (tweetparams.StatType == "Scoring")
            {
                if (tweetparams.Season != 0)
                {
                    var resultset = (from statset in dbcontext.ScoringStats
                                     where statset.Players.NFLTeams.TeamAbbrev == tweetparams.Team && statset.Players.Number == tweetparams.Number && statset.Season == tweetparams.Season
                                     select new { statset.Season, statset.Players.PlayerName, statset.Team, statset.PassTDs, statset.RushTDs, statset.RecTDs, statset.TotalTDs, statset.TotalPoints }).FirstOrDefault();

                    if (resultset != null)
                    {
                        return String.Format("Scoring Stats - {0}\nSeason: {1}\nTeam: {2}\nPass TDs: {3}\nRush TDs: {4}\nRec TDs: {5}\nTotal TDs: {6}\nTotal Points: {7}\n{8}", resultset.PlayerName, resultset.Season, resultset.Team, resultset.PassTDs, resultset.RushTDs, resultset.RecTDs, resultset.TotalTDs, resultset.TotalPoints, DateTime.Now.ToString("hh:mm:ss"));
                    }
                }
                else
                {
                    var resultset = from statset in dbcontext.ScoringStats
                                    where statset.Players.NFLTeams.TeamAbbrev == tweetparams.Team && statset.Players.Number == tweetparams.Number
                                    select new { statset.Players.PlayerName, statset.PassTDs, statset.RushTDs, statset.RecTDs, statset.TotalTDs, statset.TotalPoints };

                    if (resultset != null)
                    {
                        string _playername = "";

                        foreach (var set in resultset) //HACK
                        {
                            _playername = set.PlayerName;
                            break;
                        }

                        return String.Format("Career Scoring Stats: {0}\nPass TDs: {1}\nRush TDs: {2}\nRec TDs: {3}\nTotal TDs: {4}\nTotal Points: {5}\n{6}", _playername, resultset.Sum(x => x.PassTDs), resultset.Sum(x => x.RushTDs), resultset.Sum(x => x.RecTDs), resultset.Sum(x => x.TotalTDs), resultset.Sum(x => x.TotalPoints), DateTime.Now.ToString("hh:mm:ss"));
                    }
                }
            }

            //Only reach here if no stats are found
            var name = dbcontext.Players.First(x => x.NFLTeams.TeamAbbrev == tweetparams.Team && x.Number == tweetparams.Number);
            
            return String.Format("No stats found using the following parameters:\nPlayer: {0}\nTeam: {1}\nNumber: {2}{3}", name.PlayerName, tweetparams.Team, tweetparams.Number,(tweetparams.Season == 0 ? "" : "\nSeason: "+ tweetparams.Season));
            

            
        }

        public string QueryDBwithName(TweetParameters tweetparams)
        {
            string playerposition = GetPlayerType(tweetparams);

            if (playerposition.StartsWith("Player not found"))
            {
                return playerposition;
            }

            if ((playerposition == "QB" && tweetparams.StatType == null) || tweetparams.StatType == "Passing")
            {
                if (tweetparams.Season != 0)
                {
                    var resultset = (from statset in dbcontext.PassingStats
                                     where statset.Players.PlayerName == tweetparams.FirstName + " " + tweetparams.LastName && statset.Season == tweetparams.Season
                                     select new { statset.Season, statset.Players.PlayerName, statset.Team, statset.Yards, statset.Touchdowns, statset.Interceptions }).FirstOrDefault();

                    if (resultset != null)
                    {
                        return String.Format("Passing Stats - {0}\nSeason: {1}\nTeam: {2}\nPassing Yards: {3}\nTDs: {4}\nInts: {5}\n{6}", resultset.PlayerName, resultset.Season, resultset.Team, resultset.Yards, resultset.Touchdowns, resultset.Interceptions, DateTime.Now.ToString("hh:mm:ss"));
                    }
                }
                else // career stats
                {
                    var resultset = from statset in dbcontext.PassingStats
                                    where statset.Players.PlayerName == tweetparams.FirstName + " " + tweetparams.LastName
                                    select new { statset.Players.PlayerName, statset.Yards, statset.Touchdowns, statset.Interceptions };

                    if (resultset != null)
                    {
                        string _playername = "";

                        foreach (var set in resultset) //HACK
                        {
                            _playername = set.PlayerName;
                            break;
                        }

                        return String.Format("Career Passing Stats: {0}\nYards: {1}\nTDs: {2}\nInts: {3}\n{4}", _playername, resultset.Sum(x => x.Yards), resultset.Sum(x => x.Touchdowns), resultset.Sum(x => x.Interceptions), DateTime.Now.ToString("hh:mm:ss"));
                    }
                }
            }
            else if ((playerposition == "RB" && tweetparams.StatType == null) || (playerposition == "FB" && tweetparams.StatType == null) || tweetparams.StatType == "Rushing")
            {
                if (tweetparams.Season != 0)
                {
                    var resultset = (from statset in dbcontext.RushingStats
                                     where statset.Players.PlayerName == tweetparams.FirstName + " " + tweetparams.LastName && statset.Season == tweetparams.Season
                                     select new { statset.Season, statset.Players.PlayerName, statset.Team, statset.Yards, statset.Touchdowns, statset.FumblesLost, }).FirstOrDefault();
                    if (resultset != null)
                    {
                        return String.Format("Rushing Stats - {0}\nSeason: {1}\nTeam: {2}\nRushing Yards: {3}\nTDs: {4}\nFumbles: {5}\n{6}", resultset.PlayerName, resultset.Season, resultset.Team, resultset.Yards, resultset.Touchdowns, resultset.FumblesLost, DateTime.Now.ToString("hh:mm:ss"));
                    }
                }
                else // career stats
                {
                    var resultset = from statset in dbcontext.RushingStats
                                    where statset.Players.PlayerName == tweetparams.FirstName + " " + tweetparams.LastName
                                    select new { statset.Players.PlayerName, statset.Yards, statset.Touchdowns, statset.FumblesLost };

                    if (resultset != null)
                    {
                        string _playername = "";

                        foreach (var set in resultset) //HACK
                        {
                            _playername = set.PlayerName;
                            break;
                        }

                        return String.Format("Career Rushing Stats: {0}\nYards: {1}\nTDs: {2}\nFumbles: {3}\n{4}", _playername, resultset.Sum(x => x.Yards), resultset.Sum(x => x.Touchdowns), resultset.Sum(x => x.FumblesLost), DateTime.Now.ToString("hh:mm:ss"));
                    }
                }
            }
            else if ((playerposition == "WR" && tweetparams.StatType == null) || (playerposition == "TE" && tweetparams.StatType == null) || tweetparams.StatType == "Receiving")
            {
                if (tweetparams.Season != 0)
                {
                    var resultset = (from statset in dbcontext.ReceivingStats
                                     where statset.Players.PlayerName == tweetparams.FirstName + " " + tweetparams.LastName && statset.Season == tweetparams.Season
                                     select new { statset.Season, statset.Players.PlayerName, statset.Team, statset.Yards, statset.Touchdowns, statset.Receptions, }).FirstOrDefault();

                    if (resultset != null)
                    {
                        return String.Format("Receiving Stats - {0}\nSeason: {1}\nTeam: {2}\nReceptions: {3}\nReceiving Yards: {4}\nTDs: {5}\n{6}", resultset.PlayerName, resultset.Season, resultset.Team, resultset.Receptions, resultset.Yards, resultset.Touchdowns, DateTime.Now.ToString("hh:mm:ss"));
                    }
                }
                else // career stats
                {
                    var resultset = from statset in dbcontext.ReceivingStats
                                    where statset.Players.PlayerName == tweetparams.FirstName + " " + tweetparams.LastName
                                    select new { statset.Players.PlayerName, statset.Yards, statset.Receptions, statset.Touchdowns };

                    if (resultset != null)
                    {
                        string _playername = "";

                        foreach (var set in resultset) //HACK
                        {
                            _playername = set.PlayerName;
                            break;
                        }

                        return String.Format("Career Receiving Stats: {0}\nReceptions: {1}\nYards: {2}\nTDs: {3}\n{4}", _playername, resultset.Sum(x => x.Receptions), resultset.Sum(x => x.Yards), resultset.Sum(x => x.Touchdowns), DateTime.Now.ToString("hh:mm:ss"));
                    }
                }
            }
            else if ((playerposition == "Defensive" && tweetparams.StatType == null) || tweetparams.StatType.StartsWith("Defens"))
            {
                if (tweetparams.Season != 0)
                {
                    var resultset = (from statset in dbcontext.DefensiveStats
                                     where statset.Players.PlayerName == tweetparams.FirstName + " " + tweetparams.LastName && statset.Season == tweetparams.Season
                                     select new { statset.Season, statset.Players.PlayerName, statset.Team, statset.CombinedTackles, statset.FumblesRecovered, statset.Sack, statset.Interceptions }).FirstOrDefault();

                    if (resultset != null)
                    {
                        return String.Format("Defensive Stats - {0}\nSeason: {1}\nTeam: {2}\nTackles: {3}\nFumbles: {4}\nSacks: {5}\nInts: {6}\n{7}", resultset.PlayerName, resultset.Season, resultset.Team, resultset.CombinedTackles, resultset.FumblesRecovered, resultset.Sack, resultset.Interceptions, DateTime.Now.ToString("hh:mm:ss"));
                    }
                }
                else
                {
                    var resultset = from statset in dbcontext.DefensiveStats
                                    where statset.Players.PlayerName == tweetparams.FirstName + " " + tweetparams.LastName
                                    select new { statset.Players.PlayerName, statset.CombinedTackles, statset.FumblesRecovered, statset.Sack, statset.Interceptions };

                    if (resultset != null)
                    {
                        string _playername = "";

                        foreach (var set in resultset) //HACK
                        {
                            _playername = set.PlayerName;
                            break;
                        }

                        return String.Format("Career Defensive Stats: {0}\nTackles: {1}\nFumbles: {2}\nSacks: {3}\nInts: {4}\n{5}", _playername, resultset.Sum(x => x.CombinedTackles), resultset.Sum(x => x.FumblesRecovered), resultset.Sum(x => x.Sack), resultset.Sum(x => x.Interceptions), DateTime.Now.ToString("hh:mm:ss"));
                    }

                }
            }
            else if (tweetparams.StatType == "Scoring")
            {
                if (tweetparams.Season != 0)
                {
                    var resultset = (from statset in dbcontext.ScoringStats
                                     where statset.Players.PlayerName == tweetparams.FirstName + " " + tweetparams.LastName && statset.Season == tweetparams.Season
                                     select new { statset.Season, statset.Players.PlayerName, statset.Team, statset.PassTDs, statset.RushTDs, statset.RecTDs, statset.TotalTDs, statset.TotalPoints }).FirstOrDefault();

                    if (resultset != null)
                    {
                        return String.Format("Scoring Stats - {0}\nSeason: {1}\nTeam: {2}\nPass TDs: {3}\nRush TDs: {4}\nRec TDs: {5}\nTotal TDs: {6}\nTotal Points: {7}\n{8}", resultset.PlayerName, resultset.Season, resultset.Team, resultset.PassTDs, resultset.RushTDs, resultset.RecTDs, resultset.TotalTDs, resultset.TotalPoints, DateTime.Now.ToString("hh:mm:ss"));
                    }
                }
                else
                {
                    var resultset = from statset in dbcontext.ScoringStats
                                    where statset.Players.PlayerName == tweetparams.FirstName + " " + tweetparams.LastName
                                    select new { statset.Players.PlayerName, statset.PassTDs, statset.RushTDs, statset.RecTDs, statset.TotalTDs, statset.TotalPoints };

                    if (resultset != null)
                    {
                        string _playername = "";

                        foreach (var set in resultset) //HACK
                        {
                            _playername = set.PlayerName;
                            break;
                        }

                        return String.Format("Career Scoring Stats: {0}\nPass TDs: {1}\nRush TDs: {2}\nRec TDs: {3}\nTotal TDs: {4}\nTotal Points: {5}\n{6}", _playername, resultset.Sum(x => x.PassTDs), resultset.Sum(x => x.RushTDs), resultset.Sum(x => x.RecTDs), resultset.Sum(x => x.TotalTDs), resultset.Sum(x => x.TotalPoints), DateTime.Now.ToString("hh:mm:ss"));
                    }
                }
            }

            //Only reach here if no stats are found

            return String.Format("No stats found using the following parameters:\nPlayer: {0}{1}{2}", tweetparams.FirstName + " " + tweetparams.LastName, tweetparams.Season == 0 ? "" : "\nSeason: " + tweetparams.Season, tweetparams.StatType != null ? "\nStat: " + tweetparams.StatType : "");
        }

        public long GetTweetID()
        {
            return dbcontext.TweetIDs.Max(ids => ids.TweetID);
        }

        public void InsertTweetID(long tweetid)
        {
            TweetIDs tweetids = new TweetIDs();
            tweetids.TweetID = tweetid;
            tweetids.CreatedDate = DateTime.Now;
            dbcontext.TweetIDs.Add(tweetids);
            dbcontext.SaveChanges();
        }

        private string GetPlayerType(TweetParameters tweetparams)
        {
            string[] defensivepositions = new string[] { "CB", "DB", "DE", "DL", "DT", "LB", "NT", "S" };

            string position = "";
            
            if (tweetparams.FirstName == null && tweetparams.LastName == null)
            {
                position = (from player in dbcontext.Players
                            where player.NFLTeams.TeamAbbrev == tweetparams.Team && player.Number == tweetparams.Number
                            select new { player.Position }).FirstOrDefault().Position.ToString();
            }
            else
            {
                position = (from player in dbcontext.Players
                            where player.PlayerName == tweetparams.FirstName + " " + tweetparams.LastName
                            select new { player.Position }).FirstOrDefault().Position.ToString();
            }
            if (position == null)
            {
                string team = tweetparams.Team;
                string number = tweetparams.Number;
                string name = tweetparams.FirstName + " " + tweetparams.LastName;
                
                return String.Format("Player not found using the following parameters: {0}{1}{2}\nDoes this player exist?", team != null ? "\nTeam: " + team : "", number != null ? "\nNumber: " + number : "", name != null ? "Name: " + name : "");
            }

            if (defensivepositions.Contains(position))
            {
                return "Defensive";
            }

            return position;
        }
    }
}

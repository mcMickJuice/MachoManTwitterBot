READ ME for twitterbot-test

This is my first foray into working with the Twitter API using the Linq to Twitter library.

The goal of this project is to create a bot that will be able to go out, grab new tweets (based on SinceID), read hashtag in the tweet and tweet back a "smart response".

Tweet Parameters-
Handle:
All tweets need to be "tweeted at"/mention @MachoManFF.

TeamAbbreviation x Number (Player) - #(teamabbreviation)x(playernumber):
All tweets require a player to return results.  To determine a player, the only "required" parameter is a hashtag "(teamabbreviation)x(number of player". For example, if you want stats for Aaron Rodgers, you would provide the following hashtag - #gbx12.  The program splits on the "x" which is assigned to the "Team" and "Number" members of the TweetParameters object.  If the DB returns an empty result set, the bot will return a tweet of "Could not find a player with the following Team and Number parameters".  Player Team and Number is based on a player's current team. The stat type returned in based on the player's position - i.e. Passing Stats returned for QB, Rushing stats returned for RB and FB, etc.  For all defensive players, only defensive stats are returned.

Here are the team abbreviations for each team:
Abbrev		Team
Ari		Arizona Cardinals
Atl		Atlanta Falcons
Bal		Baltimore Ravens
Buf		Buffalo Bills
Car		Carolina Panthers
Chi		Chicago Bears
Cin		Cincinnati Bengals
Cle		Cleveland Browns
Dal		Dallas Cowboys
Den		Denver Broncos
Det		Detroit Lions
GB		Green Bay Packers
Hou		Houston Texans
Ind		Indianapolis Colts
Jac		Jacksonville Jaguars
KC		Kansas City Chiefs
Mia		Miami Dolphins
Min		Minnesota Vikings
NE		New England Patriots
NO		New Orleans Saints
NYG		New York Giants
NYJ		New York Jets
Oak		Oakland Raiders
Phi		Philadelphia Eagles
Pit		Pittsburgh Steelers
SD		San Diego Chargers
Sea		Seattle Seahawks
SF		San Francisco 49ers
StL		St. Louis Rams
TB		Tampa Bay Buccaneers
Ten		Tennessee Titans
Wsh		Washington Redskins


Season - #year(year of season):
If you would like stats of a player for a given season, the #year(year) parameter can be tweeted.  Program checks to see if a hashtag starts with "year".  If so, it assigns the (year) value to the Season member of TweetParameter object.  If, for a given player, the year provided results in an empty result set, 

StatType - #stat(StatType):
If a stattype is not provided, the stattype returned is based on position (i.e. QB - Passing, RB - Rushing, etc.).  However you can "override" by providing a stattype parameter.  #stat(StatType).  Note that the only way to return scoring stats is by providing a stattype

Here are the stattypes for each stat:
Passing
Rushing
Receiving
Defensive
Scoring

Future Parameters:
FirstName and LastName - for retired players
Team Records
Fantasy Football Stats
Weekly Stats
YTD Stats

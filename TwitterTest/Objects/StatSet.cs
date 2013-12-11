
namespace StatsTwitterBot.Objects
{
    public abstract class StatSet
    {
        protected ESPNStatsEntities Context;

        protected StatSet()
        {
            Context = new ESPNStatsEntities();
        }

        //public abstract string GetStatString(int? year, string playernumber, string playerteam);
        public abstract string GetStatString(int? year, int playerid);
    }
}

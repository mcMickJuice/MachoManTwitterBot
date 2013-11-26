using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsTwitterBot.Classes
{
    internal abstract class StatSet
    {
        protected ESPNStatsEntities Context;

        protected StatSet()
        {
            Context = new ESPNStatsEntities();
        }

        public abstract string GetStatString(int? year, string playernumber, string playerteam);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsTwitterBot.Objects;

namespace StatsTwitterBot.Classes
{
    class StatSetFactory
    {
        public StatSet GetStatSet(string stattype)
        {
            switch (stattype)
            {
                case "PASSING":
                    return new PassingStatSet();
                case "RUSHING":
                    return new RushingStatSet();
                case "RECEIVING":
                    return new ReceivingStatSet();
                case "DEFENSIVE":
                    return new DefensiveStatSet();
                case "SCORING":
                    return new ScoringStatSet();
                default:
                    throw new ApplicationException("Somehow this fell through");
            }

        }
    }
}

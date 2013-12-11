using System;

namespace StatsTwitterBot.Objects
{
    internal class StatSetFactory
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

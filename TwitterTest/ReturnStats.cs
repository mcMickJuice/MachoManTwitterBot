//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StatsTwitterBot
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReturnStats
    {
        public int ReturnStatsID { get; set; }
        public Nullable<int> PlayerID { get; set; }
        public Nullable<int> Season { get; set; }
        public string Team { get; set; }
        public Nullable<int> GamesPlayed { get; set; }
        public Nullable<int> PuntReturnAttempts { get; set; }
        public Nullable<int> PuntReturnYards { get; set; }
        public Nullable<int> PuntReturnTDs { get; set; }
        public Nullable<int> PuntFairCatches { get; set; }
        public Nullable<int> PuntReturnLong { get; set; }
        public Nullable<int> KickReturnAttempts { get; set; }
        public Nullable<int> KickReturnYards { get; set; }
        public Nullable<int> KickReturnTDs { get; set; }
        public Nullable<int> KickFairCatches { get; set; }
        public Nullable<int> KickReturnLong { get; set; }
    
        public virtual Players Players { get; set; }
    }
}

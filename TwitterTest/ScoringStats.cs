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
    
    public partial class ScoringStats
    {
        public int ScoringStatsID { get; set; }
        public int PlayerID { get; set; }
        public Nullable<int> Season { get; set; }
        public string Team { get; set; }
        public Nullable<int> GamesPlayed { get; set; }
        public Nullable<int> PassTDs { get; set; }
        public Nullable<int> RushTDs { get; set; }
        public Nullable<int> RecTDs { get; set; }
        public Nullable<int> ReturnTDs { get; set; }
        public Nullable<int> TotalTDs { get; set; }
        public Nullable<int> PAT2 { get; set; }
        public Nullable<int> PAT { get; set; }
        public Nullable<int> FG { get; set; }
        public Nullable<int> TotalPoints { get; set; }
    
        public virtual Players Players { get; set; }
    }
}

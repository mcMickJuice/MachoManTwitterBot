//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TwitterTest
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReceivingStats
    {
        public int ReceivingStatsID { get; set; }
        public int PlayerID { get; set; }
        public Nullable<int> Season { get; set; }
        public string Team { get; set; }
        public Nullable<int> GamesPlayed { get; set; }
        public Nullable<int> Receptions { get; set; }
        public Nullable<int> Targets { get; set; }
        public Nullable<int> Yards { get; set; }
        public Nullable<int> Long { get; set; }
        public Nullable<int> Touchdowns { get; set; }
        public Nullable<int> FirstDowns { get; set; }
        public Nullable<int> Fumbles { get; set; }
        public Nullable<int> FumblesLost { get; set; }
    
        public virtual Players Players { get; set; }
    }
}

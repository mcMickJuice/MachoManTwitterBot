﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ESPNStatsEntities : DbContext
    {
        public ESPNStatsEntities()
            : base("name=ESPNStatsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<DefensiveStats> DefensiveStats { get; set; }
        public DbSet<KickingStats> KickingStats { get; set; }
        public DbSet<Logger> Logger { get; set; }
        public DbSet<NFLTeams> NFLTeams { get; set; }
        public DbSet<PassingStats> PassingStats { get; set; }
        public DbSet<Players> Players { get; set; }
        public DbSet<PuntingStats> PuntingStats { get; set; }
        public DbSet<ReceivingStats> ReceivingStats { get; set; }
        public DbSet<ReturnStats> ReturnStats { get; set; }
        public DbSet<RushingStats> RushingStats { get; set; }
        public DbSet<ScoringStats> ScoringStats { get; set; }
        public DbSet<TweetIDs> TweetIDs { get; set; }
    }
}

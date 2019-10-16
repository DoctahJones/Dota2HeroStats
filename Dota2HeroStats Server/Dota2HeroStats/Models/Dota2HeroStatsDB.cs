using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Dota2HeroStats.Models
{
    public class Dota2HeroStatsDB : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public Dota2HeroStatsDB()
            : base("name=Dota2HeroStatsDB")
        {
            this.Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public System.Data.Entity.DbSet<Dota2HeroStats.Models.Hero> Heroes { get; set; }
        public System.Data.Entity.DbSet<Dota2HeroStats.Models.Role> Roles { get; set; }


        public System.Data.Entity.DbSet<Dota2HeroStats.Models.AbilityDraftMatch> AbilityDraftMatches { get; set; }
        public System.Data.Entity.DbSet<Dota2HeroStats.Models.Player> Players { get; set; }
        public System.Data.Entity.DbSet<Dota2HeroStats.Models.Ability> Abilities { get; set; }

        public System.Data.Entity.DbSet<Dota2HeroStats.Models.HeroAbilityStat> HeroAbilityStats { get; set; }

        public System.Data.Entity.DbSet<Dota2HeroStats.Models.Admin> Admins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Hero>()
                        .HasMany<Role>(h => h.Roles)
                        .WithMany(r => r.Heroes)
                        .Map(hr =>
                        {
                            hr.MapLeftKey("HeroRefId");
                            hr.MapRightKey("RoleRefId");
                            hr.ToTable("HeroRole");
                        });

            modelBuilder.Entity<Player>()
                        .HasMany<Ability>(p => p.Abilities)
                        .WithMany(a => a.Players)
                        .Map(pa =>
                        {
                            pa.MapLeftKey("PlayerRefMatchId", "PlayerRefSlotId");
                            pa.MapRightKey("AbilityRefId");
                            pa.ToTable("PlayerAbility");
                        });
        }

    }
}

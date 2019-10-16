namespace Dota2HeroStats.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Abilities",
                c => new
                    {
                        AbilityId = c.Int(nullable: false),
                        Name = c.String(),
                        Img = c.String(),
                        IsUltimate = c.Boolean(nullable: false),
                        OriginalAbilityOwner_HeroId = c.Int(),
                    })
                .PrimaryKey(t => t.AbilityId)
                .ForeignKey("dbo.Heroes", t => t.OriginalAbilityOwner_HeroId)
                .Index(t => t.OriginalAbilityOwner_HeroId);
            
            CreateTable(
                "dbo.HeroAbilityStats",
                c => new
                    {
                        HeroId = c.Int(nullable: false),
                        AbilityId = c.Int(nullable: false),
                        Matches = c.Int(nullable: false),
                        Wins = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.HeroId, t.AbilityId })
                .ForeignKey("dbo.Abilities", t => t.AbilityId, cascadeDelete: true)
                .ForeignKey("dbo.Heroes", t => t.HeroId, cascadeDelete: true)
                .Index(t => t.HeroId)
                .Index(t => t.AbilityId);
            
            CreateTable(
                "dbo.Heroes",
                c => new
                    {
                        HeroId = c.Int(nullable: false),
                        Name = c.String(),
                        LocalizedName = c.String(),
                        PrimaryAttr = c.String(),
                        AttackType = c.String(),
                        Legs = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HeroId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        MatchId = c.Long(nullable: false),
                        PlayerSlot = c.Int(nullable: false),
                        AccountId = c.Int(),
                        IsRadiant = c.Boolean(nullable: false),
                        Item_0 = c.Int(),
                        Item_1 = c.Int(),
                        Item_2 = c.Int(),
                        Item_3 = c.Int(),
                        Item_4 = c.Int(),
                        Item_5 = c.Int(),
                        Backpack_0 = c.Int(),
                        Backpack_1 = c.Int(),
                        Backpack_2 = c.Int(),
                        HeroLevel = c.Int(nullable: false),
                        Kills = c.Int(nullable: false),
                        Deaths = c.Int(nullable: false),
                        Assists = c.Int(nullable: false),
                        LastHits = c.Int(nullable: false),
                        Denies = c.Int(nullable: false),
                        GoldPerMin = c.Int(nullable: false),
                        XpPerMin = c.Int(nullable: false),
                        HeroDamage = c.Int(nullable: false),
                        HeroHealing = c.Int(nullable: false),
                        TowerDamage = c.Int(nullable: false),
                        Hero_HeroId = c.Int(),
                    })
                .PrimaryKey(t => new { t.MatchId, t.PlayerSlot })
                .ForeignKey("dbo.AbilityDraftMatches", t => t.MatchId, cascadeDelete: true)
                .ForeignKey("dbo.Heroes", t => t.Hero_HeroId)
                .Index(t => t.MatchId)
                .Index(t => t.Hero_HeroId);
            
            CreateTable(
                "dbo.AbilityDraftMatches",
                c => new
                    {
                        MatchId = c.Long(nullable: false),
                        StartTime = c.Int(nullable: false),
                        DurationSeconds = c.Int(nullable: false),
                        ServerCluster = c.Int(nullable: false),
                        PatchNumber = c.Int(nullable: false),
                        SkillLevel = c.Int(),
                        GameMode = c.Int(nullable: false),
                        DireKillScore = c.Int(nullable: false),
                        RadiantKillScore = c.Int(nullable: false),
                        RadiantWin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MatchId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        SteamId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.SteamId);
            
            CreateTable(
                "dbo.PlayerAbility",
                c => new
                    {
                        PlayerRefMatchId = c.Long(nullable: false),
                        PlayerRefSlotId = c.Int(nullable: false),
                        AbilityRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlayerRefMatchId, t.PlayerRefSlotId, t.AbilityRefId })
                .ForeignKey("dbo.Players", t => new { t.PlayerRefMatchId, t.PlayerRefSlotId }, cascadeDelete: true)
                .ForeignKey("dbo.Abilities", t => t.AbilityRefId, cascadeDelete: true)
                .Index(t => new { t.PlayerRefMatchId, t.PlayerRefSlotId })
                .Index(t => t.AbilityRefId);
            
            CreateTable(
                "dbo.HeroRole",
                c => new
                    {
                        HeroRefId = c.Int(nullable: false),
                        RoleRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.HeroRefId, t.RoleRefId })
                .ForeignKey("dbo.Heroes", t => t.HeroRefId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleRefId, cascadeDelete: true)
                .Index(t => t.HeroRefId)
                .Index(t => t.RoleRefId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Abilities", "OriginalAbilityOwner_HeroId", "dbo.Heroes");
            DropForeignKey("dbo.HeroRole", "RoleRefId", "dbo.Roles");
            DropForeignKey("dbo.HeroRole", "HeroRefId", "dbo.Heroes");
            DropForeignKey("dbo.Players", "Hero_HeroId", "dbo.Heroes");
            DropForeignKey("dbo.Players", "MatchId", "dbo.AbilityDraftMatches");
            DropForeignKey("dbo.PlayerAbility", "AbilityRefId", "dbo.Abilities");
            DropForeignKey("dbo.PlayerAbility", new[] { "PlayerRefMatchId", "PlayerRefSlotId" }, "dbo.Players");
            DropForeignKey("dbo.HeroAbilityStats", "HeroId", "dbo.Heroes");
            DropForeignKey("dbo.HeroAbilityStats", "AbilityId", "dbo.Abilities");
            DropIndex("dbo.HeroRole", new[] { "RoleRefId" });
            DropIndex("dbo.HeroRole", new[] { "HeroRefId" });
            DropIndex("dbo.PlayerAbility", new[] { "AbilityRefId" });
            DropIndex("dbo.PlayerAbility", new[] { "PlayerRefMatchId", "PlayerRefSlotId" });
            DropIndex("dbo.Players", new[] { "Hero_HeroId" });
            DropIndex("dbo.Players", new[] { "MatchId" });
            DropIndex("dbo.HeroAbilityStats", new[] { "AbilityId" });
            DropIndex("dbo.HeroAbilityStats", new[] { "HeroId" });
            DropIndex("dbo.Abilities", new[] { "OriginalAbilityOwner_HeroId" });
            DropTable("dbo.HeroRole");
            DropTable("dbo.PlayerAbility");
            DropTable("dbo.Admins");
            DropTable("dbo.Roles");
            DropTable("dbo.AbilityDraftMatches");
            DropTable("dbo.Players");
            DropTable("dbo.Heroes");
            DropTable("dbo.HeroAbilityStats");
            DropTable("dbo.Abilities");
        }
    }
}

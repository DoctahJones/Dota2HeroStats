using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dota2HeroStats.Models
{
    public partial class Dota2HeroStatsDBInitializer : System.Data.Entity.CreateDatabaseIfNotExists<Dota2HeroStatsDB>
    {
        protected override void Seed(Dota2HeroStatsDB context)
        {
           
            Admin admin = new Admin { SteamId = AdminSteamId };
            context.Admins.Add(admin);

            var roleC = new Role { Name = "Carry" };
            var roleE = new Role { Name = "Escape" };
            var roleN = new Role { Name = "Nuker" };
            var roleI = new Role { Name = "Initiator" };
            var roleDu = new Role { Name = "Durable" };
            var roleDi = new Role { Name = "Disabler" };
            var roleJ = new Role { Name = "Jungle" };
            var roleP = new Role { Name = "Pusher" };
            var roleS = new Role { Name = "Support" };
            context.Roles.Add(roleC);
            context.Roles.Add(roleE);
            context.Roles.Add(roleN);
            context.Roles.Add(roleI);
            context.Roles.Add(roleDu);
            context.Roles.Add(roleDi);
            context.Roles.Add(roleJ);
            context.Roles.Add(roleP);
            context.Roles.Add(roleS);

            var am = new Hero
            {
                HeroId = 1,
                Name = "npc_dota_hero_antimage",
                LocalizedName = "Anti-Mage",
                PrimaryAttr = "agi",
                AttackType = "Melee",
                Legs = 2
            };
            am.Roles = new HashSet<Role>();
            am.Roles.Add(roleC);
            am.Roles.Add(roleE);
            am.Roles.Add(roleN);
            context.Heroes.Add(am);

            var abilityAMageA = new Ability
            {
                AbilityId = 5004,
                Name = "Blink",
                IsUltimate = false,
                OriginalAbilityOwner = am,
                Img = "anti-mage-blink-5004-fe84e312ce0bb56da08495794d6dae0c5a0b38cd94e7ef4e5a77f7623abe34ff.jpg"
            };
            var abilityAMageB = new Ability
            {
                AbilityId = 5003,
                Name = "Mana Break",
                IsUltimate = false,
                OriginalAbilityOwner = am,
                Img = "anti-mage-mana-break-5003-a6a469cb89e075790ec16420e1874dafc6b459f8ed60e49559b10f6b81cb2719.jpg"
            };
            var abilityAMageC = new Ability
            {
                AbilityId = 5005,
                Name = "Spell Shield",
                IsUltimate = false,
                OriginalAbilityOwner = am,
                Img = "anti-mage-spell-shield-5005-45e604ddd0c9a30e41cff593b1ceb34ead4cfcaee175916a7b83afa68dc5765e.jpg"
            };
            var abilityAMageD = new Ability
            {
                AbilityId = 5006,
                Name = "Mana Void",
                IsUltimate = true,
                OriginalAbilityOwner = am,
                Img = "anti-mage-mana-void-5006-803fc47e49d7bb11fa8ce5df24b17f3b46500cc3e45b764a451682a196d8b991.jpg"
            };
            context.Abilities.Add(abilityAMageA);
            context.Abilities.Add(abilityAMageB);
            context.Abilities.Add(abilityAMageC);
            context.Abilities.Add(abilityAMageD);


            var axe = new Hero
            {
                HeroId = 2,
                Name = "npc_dota_hero_axe",
                LocalizedName = "Axe",
                PrimaryAttr = "str",
                AttackType = "Melee",
                Legs = 2
            };
            axe.Roles = new HashSet<Role>();
            axe.Roles.Add(roleI);
            axe.Roles.Add(roleDu);
            axe.Roles.Add(roleDi);
            axe.Roles.Add(roleJ);
            context.Heroes.Add(axe);

            var abilityAxeA = new Ability
            {
                AbilityId = 5007,
                Name = "Berserker's Call",
                IsUltimate = false,
                OriginalAbilityOwner = axe,
                Img = "axe-berserkers-call-5007-7a2a49cee8958130a6d808f555f799602b0bda0a6bfe9a99d3f758f38db4d7b4.jpg"
            };
            var abilityAxeB = new Ability
            {
                AbilityId = 5008,
                Name = "Battle Hunger",
                IsUltimate = false,
                OriginalAbilityOwner = axe,
                Img = "axe-battle-hunger-5008-ee628cf379462a374e03fa8fea04b065e97aaf0fe241629a266a102041106267.jpg"
            };
            var abilityAxeC = new Ability
            {
                AbilityId = 5009,
                Name = "Counter Helix",
                IsUltimate = false,
                OriginalAbilityOwner = axe,
                Img = "axe-counter-helix-5009-2df53f065ea0a4388da6d03a2c1716deee1a37f464da75c62d7226aea444580c.jpg"
            };
            var abilityAxeD = new Ability
            {
                AbilityId = 5010,
                Name = "Culling Blade",
                IsUltimate = true,
                OriginalAbilityOwner = axe,
                Img = "axe-culling-blade-5010-7334574abc38980c3f2df4040f384572361c4fd4d8c136c766a4c76c49624aee.jpg"
            };
            context.Abilities.Add(abilityAxeA);
            context.Abilities.Add(abilityAxeB);
            context.Abilities.Add(abilityAxeC);
            context.Abilities.Add(abilityAxeD);


            var bane = new Hero
            {
                HeroId = 3,
                Name = "npc_dota_hero_bane",
                LocalizedName = "Bane",
                PrimaryAttr = "int",
                AttackType = "Ranged",
                Legs = 4
            };
            bane.Roles = new HashSet<Role>();
            bane.Roles.Add(roleS);
            bane.Roles.Add(roleDu);
            bane.Roles.Add(roleDi);
            bane.Roles.Add(roleN);
            context.Heroes.Add(bane);

            var abilityBaneA = new Ability
            {
                AbilityId = 5012,
                Name = "Enfeeble",
                IsUltimate = false,
                OriginalAbilityOwner = bane,
                Img = "bane-enfeeble-5012-6da100300c2ed9bdad136b495334eb55734251f772855696ac6efe700cab5f55.jpg"
            };
            var abilityBaneB = new Ability
            {
                AbilityId = 5011,
                Name = "Brain Sap",
                IsUltimate = false,
                OriginalAbilityOwner = bane,
                Img = "bane-brain-sap-5011-135a8e3cf6a8432344909847907b9c86b30616e5fcea1f9db880e07569b550eb.jpg"
            };
            var abilityBaneC = new Ability
            {
                AbilityId = 5014,
                Name = "Nightmare",
                IsUltimate = false,
                OriginalAbilityOwner = bane,
                Img = "bane-nightmare-5014-6e694f66da3c336f18edada5cbcbf2f32ef05eb114c304f3862427bce89ebcba.jpg"
            };
            var abilityBaneD = new Ability
            {
                AbilityId = 5013,
                Name = "Fiend's Grip",
                IsUltimate = true,
                OriginalAbilityOwner = bane,
                Img = "bane-fiends-grip-5013-6406b6e8e78bbc6c23b6c372ae56b461194744d61afbcfa2bde85f8ac4eb9324.jpg"
            };
            context.Abilities.Add(abilityBaneA);
            context.Abilities.Add(abilityBaneB);
            context.Abilities.Add(abilityBaneC);
            context.Abilities.Add(abilityBaneD);


            var puck = new Hero
            {
                HeroId = 13,
                Name = "npc_dota_hero_puck",
                LocalizedName = "Puck",
                PrimaryAttr = "int",
                AttackType = "Ranged",
                Legs = 2
            };
            puck.Roles = new HashSet<Role>();
            puck.Roles.Add(roleI);
            puck.Roles.Add(roleE);
            puck.Roles.Add(roleDi);
            puck.Roles.Add(roleN);
            context.Heroes.Add(puck);

            var abilityPuckA = new Ability
            {
                AbilityId = 5069,
                Name = "Illusory Orb",
                IsUltimate = false,
                OriginalAbilityOwner = puck,
                Img = "puck-illusory-orb-5069-292bacfd21dd2c39025ee4bb58b054a000d971bd066b6a244e303913cbdf13e0.jpg"
            };
            var abilityPuckB = new Ability
            {
                AbilityId = 5071,
                Name = "Waning Rift",
                IsUltimate = false,
                OriginalAbilityOwner = puck,
                Img = "puck-waning-rift-5071-4753adfec71b68e1322609393934a64fd2adbbecd0055ac978d058eebe7059f0.jpg"
            };
            var abilityPuckC = new Ability
            {
                AbilityId = 5072,
                Name = "Phase Shift",
                IsUltimate = false,
                OriginalAbilityOwner = puck,
                Img = "puck-phase-shift-5072-7f62729df112f68a76a3a6fc393b4a3aa070f39acef9b71c1881087bb9aad3cd.jpg"
            };
            var abilityPuckD = new Ability
            {
                AbilityId = 5073,
                Name = "Dream Coil",
                IsUltimate = true,
                OriginalAbilityOwner = puck,
                Img = "puck-dream-coil-5073-261579767391e69bc42c1f3f088ea23afcc06ebbf54b384fcec3af33238c9710.jpg"
            };
            context.Abilities.Add(abilityPuckA);
            context.Abilities.Add(abilityPuckB);
            context.Abilities.Add(abilityPuckC);
            context.Abilities.Add(abilityPuckD);


            var tide = new Hero
            {
                HeroId = 29,
                Name = "npc_dota_hero_tidehunter",
                LocalizedName = "Tidehunter",
                PrimaryAttr = "str",
                AttackType = "Melee",
                Legs = 2
            };
            tide.Roles = new HashSet<Role>();
            tide.Roles.Add(roleI);
            tide.Roles.Add(roleDu);
            tide.Roles.Add(roleDi);
            tide.Roles.Add(roleN);
            context.Heroes.Add(tide);

            var abilityTideA = new Ability
            {
                AbilityId = 5118,
                Name = "Gush",
                IsUltimate = false,
                OriginalAbilityOwner = tide,
                Img = "tidehunter-gush-5118-90f1fd2083d7983cee7da7204daf3fa06b1704246e625edbb455b27a645e201d.jpg"
            };
            var abilityTideB = new Ability
            {
                AbilityId = 5119,
                Name = "Kraken Shell",
                IsUltimate = false,
                OriginalAbilityOwner = tide,
                Img = "tidehunter-kraken-shell-5119-ba9a8a57852c15923b0b4d745b1a1c6c20c871579b5d939d4bbdba5b9efe1268.jpg"
            };
            var abilityTideC = new Ability
            {
                AbilityId = 5120,
                Name = "Anchor Smash",
                IsUltimate = false,
                OriginalAbilityOwner = tide,
                Img = "tidehunter-anchor-smash-5120-5489123a467664393b6ecf605f1acfc19ef2dde4a38f7d0fd03e046b82e1f0f3.jpg"
            };
            var abilityTideD = new Ability
            {
                AbilityId = 5121,
                Name = "Ravage",
                IsUltimate = true,
                OriginalAbilityOwner = tide,
                Img = "tidehunter-ravage-5121-0394d66dfdb7ab2ec9b6d9523fdf4165c6981ee12d2a7a20fc6eaf01238c0179.jpg"
            };
            context.Abilities.Add(abilityTideA);
            context.Abilities.Add(abilityTideB);
            context.Abilities.Add(abilityTideC);
            context.Abilities.Add(abilityTideD);


            var tinker = new Hero
            {
                HeroId = 34,
                Name = "npc_dota_hero_tinker",
                LocalizedName = "Tinker",
                PrimaryAttr = "int",
                AttackType = "Ranged",
                Legs = 2
            };
            tinker.Roles = new HashSet<Role>();
            tinker.Roles.Add(roleC);
            tinker.Roles.Add(roleN);
            tinker.Roles.Add(roleP);
            context.Heroes.Add(tinker);

            var abilityTinkerA = new Ability
            {
                AbilityId = 5150,
                Name = "Laser",
                IsUltimate = false,
                OriginalAbilityOwner = tinker,
                Img = "tinker-laser-5150-14cffb75d4e2c3736c99fabb405eef8e4b85a4948f0d7b9eb2dc1e6bc3effa36.jpg"
            };
            var abilityTinkerB = new Ability
            {
                AbilityId = 5151,
                Name = "Heat-Seeking Missile",
                IsUltimate = false,
                OriginalAbilityOwner = tinker,
                Img = "tinker-heat-seeking-missile-5151-6a2b789237af796a1313e3cbc940815c1d4ddf6ce588c89a865551fa83e0fddc.jpg"
            };
            var abilityTinkerC = new Ability
            {
                AbilityId = 5152,
                Name = "March of the Machines",
                IsUltimate = false,
                OriginalAbilityOwner = tinker,
                Img = "tinker-march-of-the-machines-5152-5baed02865c8044a2dc464210c660c06083a64b031166ae5198637ece8b192f3.jpg"
            };
            var abilityTinkerD = new Ability
            {
                AbilityId = 5153,
                Name = "Rearm",
                IsUltimate = true,
                OriginalAbilityOwner = tinker,
                Img = "tinker-rearm-5153-5fd55feb8e298001f48cc6692e0e5b2c8f537b7b868fc0201af8210bdfbf4d66.jpg"
            };
            context.Abilities.Add(abilityTinkerA);
            context.Abilities.Add(abilityTinkerB);
            context.Abilities.Add(abilityTinkerC);
            context.Abilities.Add(abilityTinkerD);

            var p1 = new Player
            {
                MatchId = 3706501206,
                PlayerSlot = 0,
                AccountId = null,
                IsRadiant = true,
                HeroLevel = 17,
                Kills = 12,
                Deaths = 0,
                Assists = 8,
                LastHits = 34,
                Denies = 2,
                GoldPerMin = 426,
                XpPerMin = 512,
                HeroDamage = 9700,
                HeroHealing = 0,
                TowerDamage = 983,
            };
            p1.Hero = am;
            p1.Abilities = new HashSet<Ability>();
            p1.Abilities.Add(abilityAMageA);
            p1.Abilities.Add(abilityAMageB);
            p1.Abilities.Add(abilityAMageC);
            p1.Abilities.Add(abilityAMageD);
            context.Players.Add(p1);

            var p2 = new Player
            {
                MatchId = 3706501206,
                PlayerSlot = 1,
                AccountId = null,
                IsRadiant = true,
                HeroLevel = 18,
                Kills = 5,
                Deaths = 2,
                Assists = 13,
                LastHits = 110,
                Denies = 0,
                GoldPerMin = 326,
                XpPerMin = 613,
                HeroDamage = 9680,
                HeroHealing = 1,
                TowerDamage = 83,
            };
            p2.Hero = axe;
            p2.Abilities = new HashSet<Ability>();
            p2.Abilities.Add(abilityAxeA);
            p2.Abilities.Add(abilityAxeB);
            p2.Abilities.Add(abilityAxeC);
            p2.Abilities.Add(abilityAxeD);
            context.Players.Add(p2);

            var p3 = new Player
            {
                MatchId = 3706501206,
                PlayerSlot = 2,
                AccountId = null,
                IsRadiant = true,
                HeroLevel = 15,
                Kills = 4,
                Deaths = 2,
                Assists = 10,
                LastHits = 222,
                Denies = 3,
                GoldPerMin = 426,
                XpPerMin = 513,
                HeroDamage = 17680,
                HeroHealing = 0,
                TowerDamage = 123,
            };
            p3.Hero = tinker;
            p3.Abilities = new HashSet<Ability>();
            p3.Abilities.Add(abilityTinkerA);
            p3.Abilities.Add(abilityTinkerB);
            p3.Abilities.Add(abilityTinkerC);
            p3.Abilities.Add(abilityTinkerD);
            context.Players.Add(p3);

            var p4 = new Player
            {
                MatchId = 3706501206,
                PlayerSlot = 3,
                AccountId = null,
                IsRadiant = true,
                HeroLevel = 19,
                Kills = 2,
                Deaths = 10,
                Assists = 3,
                LastHits = 22,
                Denies = 2,
                GoldPerMin = 326,
                XpPerMin = 413,
                HeroDamage = 7680,
                HeroHealing = 10,
                TowerDamage = 423,
            };
            p4.Hero = tide;
            p4.Abilities = new HashSet<Ability>();
            p4.Abilities.Add(abilityTideA);
            p4.Abilities.Add(abilityTideB);
            p4.Abilities.Add(abilityTideC);
            p4.Abilities.Add(abilityTideD);
            context.Players.Add(p4);

            var p5 = new Player
            {
                MatchId = 3706501206,
                PlayerSlot = 4,
                AccountId = null,
                IsRadiant = true,
                HeroLevel = 8,
                Kills = 19,
                Deaths = 20,
                Assists = 8,
                LastHits = 2,
                Denies = 16,
                GoldPerMin = 226,
                XpPerMin = 372,
                HeroDamage = 3139,
                HeroHealing = 0,
                TowerDamage = 213,
            };
            p5.Hero = bane;
            p5.Abilities = new HashSet<Ability>();
            p5.Abilities.Add(abilityBaneA);
            p5.Abilities.Add(abilityBaneB);
            p5.Abilities.Add(abilityBaneC);
            p5.Abilities.Add(abilityBaneD);
            context.Players.Add(p5);

            var p6 = new Player
            {
                MatchId = 3706501206,
                PlayerSlot = 128,
                AccountId = null,
                IsRadiant = false,
                HeroLevel = 15,
                Kills = 9,
                Deaths = 2,
                Assists = 6,
                LastHits = 55,
                Denies = 2,
                GoldPerMin = 417,
                XpPerMin = 609,
                HeroDamage = 4539,
                HeroHealing = 3,
                TowerDamage = 429,
            };
            p6.Hero = puck;
            p6.Abilities = new HashSet<Ability>();
            p6.Abilities.Add(abilityPuckA);
            p6.Abilities.Add(abilityPuckB);
            p6.Abilities.Add(abilityPuckC);
            p6.Abilities.Add(abilityPuckD);
            context.Players.Add(p6);

            var match1 = new AbilityDraftMatch
            {
                MatchId = 3706501206,
                StartTime = 1515506286,
                DurationSeconds = 3168,
                ServerCluster = 188,
                PatchNumber = 26,
                SkillLevel = 1,
                GameMode = 18,
                DireKillScore = 20,
                RadiantKillScore = 40,
                RadiantWin = true
            };
            match1.Players = new HashSet<Player>();
            match1.Players.Add(p1);
            match1.Players.Add(p2);
            match1.Players.Add(p3);
            match1.Players.Add(p4);
            match1.Players.Add(p5);
            match1.Players.Add(p6);


            context.AbilityDraftMatches.Add(match1);

            var stat1 = new HeroAbilityStat
            {
                HeroId = am.HeroId,
                Hero = am,
                Ability = abilityAMageA,
                Matches = 10,
                Wins = 5
            };
            var stat2 = new HeroAbilityStat
            {
                HeroId = am.HeroId,
                Hero = am,
                Ability = abilityAMageB,
                Matches = 8,
                Wins = 5
            };
            var stat3 = new HeroAbilityStat
            {
                HeroId = am.HeroId,
                Hero = am,
                Ability = abilityAMageC,
                Matches = 4,
                Wins = 1
            };
            var stat4 = new HeroAbilityStat
            {
                HeroId = am.HeroId,
                Hero = am,
                Ability = abilityAMageD,
                Matches = 27,
                Wins = 0
            };
            var stat5 = new HeroAbilityStat
            {
                HeroId = axe.HeroId,
                Hero = axe,
                Ability = abilityAxeA,
                Matches = 14,
                Wins = 6
            };
            var stat6 = new HeroAbilityStat
            {
                HeroId = axe.HeroId,
                Hero = axe,
                Ability = abilityAxeB,
                Matches = 3,
                Wins = 3
            };
            var stat7 = new HeroAbilityStat
            {
                HeroId = axe.HeroId,
                Hero = axe,
                Ability = abilityAxeC,
                Matches = 5,
                Wins = 2
            };
            var stat8 = new HeroAbilityStat
            {
                HeroId = axe.HeroId,
                Hero = axe,
                Ability = abilityAxeD,
                Matches = 33,
                Wins = 13
            };
            var stat9 = new HeroAbilityStat
            {
                HeroId = am.HeroId,
                Hero = am,
                Ability = abilityAxeA,
                Matches = 21,
                Wins = 13
            };
            var stat10 = new HeroAbilityStat
            {
                HeroId = am.HeroId,
                Hero = am,
                Ability = abilityAxeB,
                Matches = 15,
                Wins = 7
            };
            var stat11 = new HeroAbilityStat
            {
                HeroId = am.HeroId,
                Hero = am,
                Ability = abilityAxeC,
                Matches = 22,
                Wins = 20
            };
            var stat12 = new HeroAbilityStat
            {
                HeroId = am.HeroId,
                Hero = am,
                Ability = abilityAxeD,
                Matches = 6,
                Wins = 5
            };
            var stat13 = new HeroAbilityStat
            {
                HeroId = tinker.HeroId,
                Hero = tinker,
                Ability = abilityTinkerD,
                Matches = 8,
                Wins = 5
            };
            var stat14 = new HeroAbilityStat
            {
                HeroId = tide.HeroId,
                Hero = tide,
                Ability = abilityTideD,
                Matches = 3,
                Wins = 1
            };
            var stat15 = new HeroAbilityStat
            {
                HeroId = bane.HeroId,
                Hero = bane,
                Ability = abilityBaneC,
                Matches = 57,
                Wins = 3
            };
            var stat16 = new HeroAbilityStat
            {
                HeroId = puck.HeroId,
                Hero = puck,
                Ability = abilityPuckB,
                Matches = 23,
                Wins = 17
            };
            context.HeroAbilityStats.Add(stat1);
            context.HeroAbilityStats.Add(stat2);
            context.HeroAbilityStats.Add(stat3);
            context.HeroAbilityStats.Add(stat4);
            context.HeroAbilityStats.Add(stat5);
            context.HeroAbilityStats.Add(stat6);
            context.HeroAbilityStats.Add(stat7);
            context.HeroAbilityStats.Add(stat8);
            context.HeroAbilityStats.Add(stat9);
            context.HeroAbilityStats.Add(stat10);
            context.HeroAbilityStats.Add(stat11);
            context.HeroAbilityStats.Add(stat12);
            context.HeroAbilityStats.Add(stat13);
            context.HeroAbilityStats.Add(stat14);
            context.HeroAbilityStats.Add(stat15);
            context.HeroAbilityStats.Add(stat16);
            base.Seed(context);
        }
    }
}
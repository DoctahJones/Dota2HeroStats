using Dota2HeroStats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Dota2HeroStats.Services.JSONModels
{
    public class JSONAbilityDraftGameToLocalCopy
    {
        private readonly IDataSource dataSource;

        public JSONAbilityDraftGameToLocalCopy(IDataSource dataSource)
        {
            this.dataSource = dataSource;
        }

        public async Task<AbilityDraftMatch> ToLocalAbilityDraftMatch(AbiltiyDraftGameJSON json)
        {
            var match = new AbilityDraftMatch
            {
                MatchId = json.match_id,
                StartTime = json.start_time,
                DurationSeconds = json.duration,
                ServerCluster = json.cluster,
                PatchNumber = json.patch,
                SkillLevel = json.skill,
                GameMode = json.game_mode,
                DireKillScore = json.dire_score,
                RadiantKillScore = json.radiant_score,
                RadiantWin = json.radiant_win,
                Players = new HashSet<Dota2HeroStats.Models.Player>()
            };

            foreach (JSONModels.Player player in json.players)
            {
                var p = new Dota2HeroStats.Models.Player
                {
                    MatchId = player.match_id,
                    PlayerSlot = player.player_slot,
                    AccountId = player.account_id,
                    IsRadiant = player.isRadiant,

                    Item_0 = player.item_0,
                    Item_1 = player.item_1,
                    Item_2 = player.item_2,
                    Item_3 = player.item_3,
                    Item_4 = player.item_4,
                    Item_5 = player.item_5,
                    Backpack_0 = player.backpack_0,
                    Backpack_1 = player.backpack_1,
                    Backpack_2 = player.backpack_2,

                    HeroLevel = player.level,
                    Kills = player.kills,
                    Deaths = player.deaths,
                    Assists = player.assists,

                    LastHits = player.last_hits ?? 0,
                    Denies = player.denies ?? 0,

                    GoldPerMin = player.gold_per_min ?? 0,
                    XpPerMin = player.xp_per_min ?? 0,

                    HeroDamage = player.hero_damage ?? 0,
                    HeroHealing = player.hero_healing ?? 0,
                    TowerDamage = player.tower_damage ?? 0,

                    Abilities = new HashSet<Dota2HeroStats.Models.Ability>()
                };

                //Lookup heroid in database to see if it already exists and if so retrieve it.
                var hero = await LookupHero(player.hero_id);
                if (hero != null)
                {
                    p.Hero = hero;
                }
                else
                {
                    p.Hero = new Hero
                    {
                        HeroId = player.hero_id,
                        Name = "unknown",
                        LocalizedName = "unknown",
                        PrimaryAttr = "unknown",
                        AttackType = "unknown",
                        Legs = 0,
                        Roles = new HashSet<Role>()
                    };
                }
                //Lookup and add each ability if it exists.
                if (player.ability_upgrades_arr != null)//some older matches dont have a record of these and will be null.
                {
                    var distinctAbilities = player.ability_upgrades_arr.Distinct();
                    foreach (int abilityId in distinctAbilities)
                    {
                        var ability = await LookupAbility(abilityId);
                        if (ability != null)
                        {
                            p.Abilities.Add(ability);
                        }
                        else
                        {
                            p.Abilities.Add(new Ability
                            {
                                AbilityId = abilityId,
                                Name = "unknown",
                                Img = "unknown",
                                IsUltimate = false
                            });
                        }
                    }
                }
                match.Players.Add(p);
            }
            return match;
        }


        private async Task<Hero> LookupHero(int heroId)
        {
            return await this.dataSource.LookupHero(heroId);
        }

        private async Task<Ability> LookupAbility(int abilityId)
        {
            return await this.dataSource.LookupAbility(abilityId);
        }

    }
}
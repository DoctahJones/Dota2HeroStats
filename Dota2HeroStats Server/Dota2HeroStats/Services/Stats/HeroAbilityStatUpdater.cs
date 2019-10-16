using Dota2HeroStats.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Dota2HeroStats.Services.Stats
{
    public class HeroAbilityStatUpdater : ISpecificStatUpdater
    {
        private IDataSource dataSource;

        public HeroAbilityStatUpdater(IDataSource dataSource)
        {
            this.dataSource = dataSource;
        }

        public async Task UpdateStats(AbilityDraftMatch match)
        {
            foreach (Player player in match.Players)
            {
                foreach (Ability ability in player.Abilities)
                {
                    var existingHeroAbilityStat = await dataSource.LookupHeroAbilityStat(player.Hero.HeroId, ability.AbilityId);

                    if (existingHeroAbilityStat == null)
                    {
                        var newStat = await CreateNewHeroAbilityStat(match, player, ability);
                        await AddHeroAbilityStat(newStat);
                    }
                    else
                    {
                        existingHeroAbilityStat.Matches++;
                        existingHeroAbilityStat.Wins = PlayerWonMatch(match, player) ? existingHeroAbilityStat.Wins + 1 : existingHeroAbilityStat.Wins;
                        existingHeroAbilityStat.EntityState = ModelEntityState.Modified;
                        await UpdateHeroAbilityStat(existingHeroAbilityStat);
                    }
                }
            }
        }

        private async Task<HeroAbilityStat> CreateNewHeroAbilityStat(AbilityDraftMatch match, Player player, Ability ability)
        {
            //lookup existing hero and ability from datasource. These should be in there because we add the match to the datasource before updating stats.
            var existingHero = await dataSource.LookupHero(player.Hero.HeroId);
            var existingAbility = await dataSource.LookupAbility(ability.AbilityId);
            if (existingHero == null)
            {
                throw new StatUpdaterException("An existing hero with heroId " + player.Hero.HeroId + " could not be found.");
            }
            if (existingAbility == null)
            {
                throw new StatUpdaterException("An existing ability with abilityId " + ability.AbilityId + " could not be found.");
            }
            existingHero.EntityState = ModelEntityState.Unchanged;
            existingAbility.EntityState = ModelEntityState.Unchanged;

            return new HeroAbilityStat
            {
                HeroId = existingHero.HeroId,
                AbilityId = existingAbility.AbilityId,
                Matches = 1,
                Wins = PlayerWonMatch(match, player) ? 1 : 0,
                Hero = existingHero,
                Ability = existingAbility,
                EntityState = ModelEntityState.Added
            };
        }

        private static bool PlayerWonMatch(AbilityDraftMatch match, Player player)
        {
            return (match.RadiantWin && player.IsRadiant) || (!match.RadiantWin && !player.IsRadiant);
        }

        public async Task AddHeroAbilityStat(HeroAbilityStat stat)
        {
            bool success;
            DbUpdateException caughtException = null;
            try
            {
                await dataSource.AddEntity<HeroAbilityStat>(stat);
                success = true;
            }
            catch (DbUpdateException e)
            {
                success = false;
                caughtException = e;
            }
            if (!success)
            {
                //check if there is already a stat with this id and if so throw error due to duplicate
                var lookupStat = await dataSource.LookupHeroAbilityStat(stat.HeroId, stat.AbilityId);
                if (lookupStat != null)
                {
                    throw new StatUpdaterException("A duplicate stat already exists.", caughtException);
                }
                else
                {
                    throw new StatUpdaterException("An error occurred when adding a stat.", caughtException);
                }
            }
        }

        public async Task UpdateHeroAbilityStat(HeroAbilityStat stat)
        {
            bool success;
            DbUpdateConcurrencyException caughtException = null;
            try
            {
                await dataSource.UpdateEntity<HeroAbilityStat>(stat);
                success = true;
            }
            catch (DbUpdateConcurrencyException e)
            {
                success = false;
                caughtException = e;
            }
            if (!success)
            {
                //check if there exists this stat if there isnt theres a problem or its been deleted etc so throw exception
                var lookupStat = await dataSource.LookupHeroAbilityStat(stat.HeroId, stat.AbilityId);
                if (lookupStat == null)
                {
                    //throw error no stat to update
                    throw new StatUpdaterException("No existing stat was found to update.", caughtException);
                }
                //if it does exist but we cant update for some reason rethrow original exception.
                throw new StatUpdaterException("An exception occurred when trying to update a stat.", caughtException);
            }
        }



    }
}
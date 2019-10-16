using Dota2HeroStats.Models;
using Dota2HeroStats.Services.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Dota2HeroStats.Services
{
    /// <summary>
    /// Class which manages adding an AbilityDraftMatch to the datasource and update the various stats which are also stored using the match data.
    /// </summary>
    public class MatchDetailUpdater
    {
        private IDataSource dataSource;

        private IStatUpdater statUpdater;


        public MatchDetailUpdater(IDataSource dataSource, IStatUpdater statUpdater)
        {
            this.dataSource = dataSource;
            this.statUpdater = statUpdater;
        }

        public async Task UpdateDataSourceAndStats(AbilityDraftMatch match)
        {
            match.EntityState = ModelEntityState.Added;
            var addedAbilities = new List<Ability>(); //some abilities such as talents can be learned by multiple heroes in the same match so we need to make sure
            //we dont add that ability multiple times from the same match.

            foreach (var player in match.Players)
            {
                player.EntityState = ModelEntityState.Added;
                var foundHero = await dataSource.LookupHero(player.Hero.HeroId);
                player.Hero.EntityState = foundHero == null ? ModelEntityState.Added : ModelEntityState.Unchanged;  //add it if not found else leave it unchanged.

                foreach (var ability in player.Abilities.ToList())
                {
                    //lookup ability in abilities added this match first, else lookup in datasource.
                    var foundAbility = addedAbilities.FirstOrDefault(a => a.AbilityId == ability.AbilityId); 
                    if (foundAbility == null)
                    {
                        foundAbility = await dataSource.LookupAbility(ability.AbilityId);
                        if (foundAbility != null)
                        {
                            foundAbility.EntityState = ModelEntityState.Unchanged; //if we got the item from the database we don't want it to be readded.
                            addedAbilities.Add(foundAbility);//add it to local list so we don't have to lookup from datasource multiple times.
                        }
                    }
                   
                    if (foundAbility != null)//if ability exists in datasource or abilities added this game.
                    {
                        player.Abilities.Remove(ability);  //remove the object that has the same abilityid and replace it with the one that is either already in the
                        //datasource or has been set up to be added to the datasource. This prevents 2 objects in the datasource with the same abilityId.
                        player.Abilities.Add(foundAbility);
                    }
                    else
                    {
                        ability.EntityState = ModelEntityState.Added;
                        addedAbilities.Add(ability);
                    }
                }
            }
            bool matchSuccessfullyAdded = true;
            try
            {
                await this.dataSource.AddEntity<AbilityDraftMatch>(match);
            }
            catch (DataSourceException)
            {
                //if there's an issue adding the match to the datastore then we don't update stats with this match.
                matchSuccessfullyAdded = false;

                throw;
            }
            if (matchSuccessfullyAdded) //don't update stats if the match wasn't added successfully otherwise in future when we add the match again we might count stats twice.
            {
                try
                {
                   await this.statUpdater.UpdateStats(match);
                }
                catch (StatUpdaterException)
                {
                    throw;
                } 
            }
        }





    }
}
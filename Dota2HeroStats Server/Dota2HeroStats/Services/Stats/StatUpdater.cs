using Dota2HeroStats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Dota2HeroStats.Services.Stats
{
    public class StatUpdater : Dota2HeroStats.Services.Stats.IStatUpdater
    {
        public List<ISpecificStatUpdater> StatUpdaters { get; private set; }



        public StatUpdater() : this(new List<ISpecificStatUpdater>())
        {
        }

        public StatUpdater(List<ISpecificStatUpdater> statUpdaters)
        {
            StatUpdaters = statUpdaters;
        }

        public async Task UpdateStats(AbilityDraftMatch match)
        {
           await Task.WhenAll(StatUpdaters.Select(updater => updater.UpdateStats(match)).ToArray());
        }

    }
}
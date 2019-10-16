using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Dota2HeroStats.Services.Stats
{
    public interface IStatUpdater
    {
        List<ISpecificStatUpdater> StatUpdaters { get; }
        Task UpdateStats(Dota2HeroStats.Models.AbilityDraftMatch match);
    }
}

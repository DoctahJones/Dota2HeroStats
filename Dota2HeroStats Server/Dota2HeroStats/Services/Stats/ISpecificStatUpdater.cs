using Dota2HeroStats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2HeroStats.Services.Stats
{
    public interface ISpecificStatUpdater
    {
        Task UpdateStats(AbilityDraftMatch match);
    }
}
